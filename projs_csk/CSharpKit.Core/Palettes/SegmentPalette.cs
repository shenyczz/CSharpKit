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

namespace CSharpKit.Palettes
{
    /// <summary>
    /// 分段调色板
    /// </summary>
    public class SegmentPalette : PaletteBase
    {
        public SegmentPalette()
            : this("N/A") { }

        public SegmentPalette(string unit)
            : base(unit)
        {
            this.PaletteType = PaletteType.Segment;
        }

        /// <summary>
        /// todo: 做到分段。。。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="colorDefault"></param>
        /// <returns></returns>
        public override Color GetColor(Double value, Color colorDefault)
        {
            Color clrRet = colorDefault;

            // 无效的浮点值(-9999)
            if (Math.Abs(value - KitConstants.InvalidValue) < 1.0e-12)
            {
                return colorDefault;
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

                    // != vv 取颜色平均值
                    if (Math.Abs(value - itemValue) > 1.0e-12 && i > 0)
                    {
                        IPaletteItem itemPrv = this[i - 1];

                        Byte r = (Byte)((item.Color.R + itemPrv.Color.R) / 2);
                        Byte g = (Byte)((item.Color.G + itemPrv.Color.G) / 2);
                        Byte b = (Byte)((item.Color.B + itemPrv.Color.B) / 2);

                        clrRet = Color.FromArgb((Byte)255, r, g, b);
                    }

                    break;
                }
            }

            return clrRet;
        }

        public new SegmentPalette Clone()
        {
            SegmentPalette palette = new SegmentPalette();
            this.Clear();

            foreach (PaletteItem item in Items)
            {
                palette.Add(item);
            }

            return palette;
        }

        //@@@
    }
}
