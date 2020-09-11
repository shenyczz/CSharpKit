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
using System.Drawing;

namespace CSharpKit.Data
{
    /// <summary>
    /// 数据图例
    /// </summary>
    public class DataLegend : Target, IDataLegend
    {
        static DataLegend()
        {
            _Empty = new DataLegend(0) { };
        }

        protected DataLegend(int iDataCode)
            : this(iDataCode, false) { }

        protected DataLegend(int iDataCode, bool useIndexColor)
            : base(Guid.NewGuid().ToString("N"))
        {
            this.DataCode = iDataCode;
            this.UseIndexColor = useIndexColor;
            this.DataLevel = 0;
            this.Background = 0;
            this.Unit = "N/A";

            this._LegendElement = new LegendElement[ColorTableSize];
            for (int i = 0; i < ColorTableSize; i++)
            {
                this._LegendElement[i] = new LegendElement() { Text = "", };
            }

            MakeLegend();
        }

        #region Empty - Static Properties

        static IDataLegend _Empty;
        public static IDataLegend Empty => _Empty;

        #endregion


        #region 彩虹7色

        //
        // 彩虹7色: 赤橙黄绿青蓝紫
        //
        // Add(0.00, new Argb(255, 000, 000));
        // Add(0.17, new Argb(255, 165, 000));
        // Add(0.33, new Argb(255, 255, 000));
        // Add(0.50, new Argb(000, 255, 000));
        // Add(0.67, new Argb(000, 000, 255));
        // Add(0.84, new Argb(075, 000, 130));
        // Add(1.00, new Argb(238, 130, 238));


        //
        // 彩虹7色: 紫蓝青绿黄橙赤
        //
        // Add(0.00, new Argb(238, 130, 238));
        // Add(0.17, new Argb(075, 000, 130));
        // Add(0.33, new Argb(000, 000, 255));
        // Add(0.50, new Argb(000, 255, 000));
        // Add(0.67, new Argb(255, 255, 000));
        // Add(0.84, new Argb(255, 165, 000));
        // Add(1.00, new Argb(255, 000, 000));

        #endregion



        #region Contants 

        /// <summary>
        /// 颜色表大小
        /// </summary>
        protected const int ColorTableSize = 257;

        #endregion


        #region Fields

        /// <summary>
        /// 数据图例
        /// </summary>
        protected ILegendElement[] _LegendElement;

        #endregion



        #region Properties

        /// <summary>
        /// 数据代码
        /// </summary>
        public int DataCode { get; private set; }

        /// <summary>
        /// 索引调色板
        /// </summary>
        public bool UseIndexColor { get; set; }

        /// <summary>
        /// 数据级别
        /// </summary>
        public int DataLevel { get; protected set; }

        /// <summary>
        /// 背景颜色
        /// </summary>
        public Int32 Background { get; set; }

        /// <summary>
        /// 图例元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ILegendElement this[int index]
        {
            get
            {
                return index >= 0 && index < ColorTableSize
                    ? _LegendElement[index]
                    : LegendElement.Empty;
            }
        }

        /// <summary>
        /// 量纲单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEmpty => (DataCode == 0 || DataLevel == 0);

        #endregion


        #region Make Legend

        protected virtual void MakeLegend() { }

        protected virtual void MakeLegend_example()
        {

            //int index = 0;
            //NxtDataCode dataCode = (AwxDataCode)this.DataCode;

            switch (this.DataCode)
            {
                case 0:
                    break;

                default:
                    MakeLegend_Def();
                    break;
            }

            // end
        }

        void MakeLegend_Def()
        {
            int index = 0;
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), -999, "无效数据"); // 00
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 01
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 02
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 03
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 04
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 05
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 06
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 07
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 08
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 09
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 10
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 11
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 12
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 13
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 14
            FillLegend(index++, Color.FromArgb(0x00, 0x00, 0x00).ToArgb(), 0.00, "");    // 15

            this.DataLevel = index;
            this.Unit = "N/A";

            return;
        }


        protected void FillLegend(int index, int color, double v, string text)
        {
            this._LegendElement[index].Color = color;
            this._LegendElement[index].Value = v;

            if (text == null || string.IsNullOrEmpty(text))
                this._LegendElement[index].Text = string.Format("{0:F2}", v);
            else
                this._LegendElement[index].Text = text;

            // end
        }


        #endregion




        #region Get Color


        public int GetColor(double dValue)
        {
            return UseIndexColor 
                ? GetIndexColor(dValue)
                : GetLinearColor(dValue);
        }

        /// <summary>
        /// 取得颜色索引()
        /// </summary>
        /// <param name="dValue"></param>
        /// <returns></returns>
        public int GetColorIndex(double dValue)
        {
            int iColorIndex = -1;

            // 数据级别
            int iDataLevel = this.DataLevel;

            for (int i = 0; i < iDataLevel; i++)
            {
                // 相邻两个物理值
                double v0 = _LegendElement[i + 0].Value;
                double v1 = _LegendElement[i + 1].Value;

                // 最大索引(大于最大值去最大值对应颜色)
                if (i == iDataLevel - 1 && dValue >= v0)
                {
                    iColorIndex = i;
                    break;
                }

                // v >= v0 && v < v1
                if (dValue >= v0 && dValue < v1)
                {
                    iColorIndex = i;
                    break;
                }
            }

            return iColorIndex;
        }
        /// <summary>
        /// 取得索引颜色
        /// </summary>
        /// <param name="dValue"></param>
        /// <returns></returns>
        public int GetIndexColor(double dValue)
        {
            int li = GetColorIndex(dValue);
            return li < 0 ? Background : _LegendElement[li].Color;
        }
        /// <summary>
        /// 取得线性插值颜色(定义上限)
        /// </summary>
        /// <param name="dValue"></param>
        /// <returns></returns>
        public virtual int GetLinearColor(double dValue)
        {
            int color = this.Background;

            double v = dValue;
            int iDataLevel = this.DataLevel;

            for (int i = 0; i < iDataLevel; i++)
            {
                // 最大级别
                if (i == iDataLevel - 1)
                {
                    color = _LegendElement[i].Color;
                    break;
                }
                else
                {// i >= 0 && i <= iDataLevel - 2

                    // 取相邻两个物理值
                    double v0 = _LegendElement[i + 0].Value;
                    double v1 = _LegendElement[i + 1].Value;

                    if (v < v0)
                    {
                        //(x,v0)
                        color = _LegendElement[i + 0].Color;
                        break;
                    }
                    else if (v < v1)
                    {
                        //[v0,v1)
                        Color clr0 = Color.FromArgb(_LegendElement[i + 0].Color);
                        Color clr1 = Color.FromArgb(_LegendElement[i + 1].Color);

                        double dv0 = v - v0;
                        double dv1 = v1 - v;
                        double v0_v1 = v1 - v0;

                        int r = (byte)(clr0.R * dv1 / v0_v1 + clr1.R * dv0 / v0_v1);
                        int g = (byte)(clr0.G * dv1 / v0_v1 + clr1.G * dv0 / v0_v1);
                        int b = (byte)(clr0.B * dv1 / v0_v1 + clr1.B * dv0 / v0_v1);

                        color = Color.FromArgb(r, g, b).ToArgb();
                        break;
                    }
                    else
                    {
                        continue; // next i
                    }

                }

            }//for(i)

            return color;
        }

        #endregion



        //}}@@@
    }

}
