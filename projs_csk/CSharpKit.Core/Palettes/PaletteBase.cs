/******************************************************************************
 * 
 * Announce: CSharpKit, Basic algorithms, components and definitions.
 *           Copyright (C) ShenYongchen.
 *           All rights reserved.
 *   Author: 申永辰.郑州 (shenyczz@163.com)
 *  WebSite: http://github.com/shenyczz/CSharpKit
 *
 * THIS CODE IS LICENSED UNDER THE MIT LICENSE (MIT).
 * THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF 
 * ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
 * IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
 * PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
 * 
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;

namespace CSharpKit.Palettes
{
    /// <summary>
    /// Palette
    /// </summary>
    public abstract class PaletteBase : Target, IPalette
    {
        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        protected PaletteBase()
            : this("N/A") { }

        protected PaletteBase(string unit)
        {
            this.Items = new List<IPaletteItem>();
            this.PaletteType = PaletteType.Unknown;
            this.TransparentColor = Color.Black;
            this.HasTransparentColor = false;
            this.Unit = unit;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 调色板条目
        /// </summary>
        public List<IPaletteItem> Items { get; set; }

        /// <summary>
        /// 调色板类型
        /// </summary>
        public PaletteType PaletteType { get; protected set; }

        /// <summary>
        /// 透明色
        /// </summary>
        public Color TransparentColor { get; set; }

        /// <summary>
        /// 有透明色
        /// </summary>
        public Boolean HasTransparentColor { get; set; }

        public int ValidItemCount
        {
            get
            {
                return GetValidItemCount();
            }
        }

        #endregion

        #region Public Functions

        public void Add(Double value, Argb argb)
        {
            this.Add(value, argb.A, argb.R, argb.G, argb.B, value.ToString("F2"));
        }
        public void Add(Double value, Argb argb, String comment)
        {
            this.Add(value, argb.A, argb.R, argb.G, argb.B, comment);
        }
        public void Add(Double value, Byte r, Byte g, Byte b, String comment)
        {
            this.Add(value, 255, r, g, b, comment);
        }
        public void Add(Double value, Byte a, Byte r, Byte g, Byte b, String comment)
        {
            IPaletteItem item = new PaletteItem(value, Color.FromArgb(a, r, g, b), comment);
            this.Add(item);
        }

        public int IndexOf(Double value)
        {
            return Items.FindIndex(item => item.Value == value);
        }

        /// <summary>
        /// 取得指定值的颜色
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultColor">默认值</param>
        /// <returns></returns>
        public virtual Color GetColor(Double value, Color defaultColor)
        {
            return defaultColor;
        }

        [Obsolete("没有使用",true)]
        public Color GetNextColor1(Double value)
        {
            Color clrRet = Color.Black;

            // 无效的浮点值(-9999)
            if (Math.Abs(value - KitConstants.InvalidValue) < 1.0e-12)
            {
                return clrRet;
            }

            // 有效条目数量
            int numValidItem = this.ValidItemCount;

            double dValidmax = 0;               // 有效条目最大值
            Color clrValidmax = Color.Black;    // 有效条目最大值颜色
            if (numValidItem > 0)
            {
                dValidmax = this[numValidItem - 1].Value;
                clrValidmax = this[numValidItem - 1].Color;
            }

            int index = this.IndexOf(value);
            // 是特殊值
            if (index >= numValidItem)
            {
                return this[index].Color;
            }

            // 不是特殊值
            for (int i = 0; i < numValidItem; i++)
            {
                IPaletteItem item = this[i];
                double itemValue = item.Value;

                if (value < this[0].Value)
                {
                    clrRet = this[0].Color;
                    break;
                }
                if (value > dValidmax)
                {
                    clrRet = clrValidmax;
                    break;
                }

                // <= vv
                if (value <= itemValue)
                {
                    // == vv 取对应颜色
                    clrRet = item.Color;

                    // != vv 取下一个颜色值(NextColor)
                    if (Math.Abs(value - itemValue) > 1.0e-12 && i > 0)
                    {
                        int ii = i == numValidItem - 1 ? i : i + 1;
                        IPaletteItem itemNxt = this[ii];
                        clrRet = itemNxt.Color;
                    }

                    break;
                }
            }

            return clrRet;
        }


        #endregion

        #region Private Functions

        /// <summary>
        /// 取得有效条目数量
        /// </summary>
        /// <returns>有效条目数量</returns>
        private int GetValidItemCount()
        {
            //特殊值最小值
            double spv_min = KitConstants.MinSpv;

            //条目总数量
            int iCount = Items.Count;

            //在特殊值前的有效颜色数量
            int numValid = iCount;

            //计算有效颜色数量
            for (int i = 0; i < iCount; i++)
            {
                double value = Items[i].Value;
                if (value > spv_min || Math.Abs(value - spv_min) < 1.0e-12)
                {//value>=spv_min
                    numValid = i;
                    break;
                }
            }

            return numValid;

        }

        #endregion

        #region IList<IPaletteItem> 成员

        public int IndexOf(IPaletteItem item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, IPaletteItem item)
        {
            Items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        public IPaletteItem this[int index]
        {
            get { return Items[index]; }
            set
            {
                IPaletteItem item = Items[index];
                if (item == value)
                    return;

                Items[index] = value;
            }
        }

        #endregion

        #region ICollection<IPaletteItem> 成员

        public void Add(IPaletteItem item)
        {
            Items.Add(item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(IPaletteItem item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(IPaletteItem[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(IPaletteItem item)
        {
            return Items.Remove(item);
        }

        #endregion

        #region IEnumerable<IPaletteItem> 成员

        public IEnumerator<IPaletteItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (IPaletteItem item in Items)
            {
                yield return item;
            }
        }

        #endregion

        #region ICloneable 成员

        public PaletteBase Clone()
        {
            throw new NotImplementedException("请在派生类重载Clone函数!");
        }

        #endregion

        //@@@PaletteBase
    }

}
