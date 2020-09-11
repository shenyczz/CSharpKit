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
    /// 彩虹调色板
    /// </summary>
    public class RainbowPalette : PaletteBase
    {
        public RainbowPalette()
        {
            this.PaletteType = PaletteType.Rainbow;

            _ValueMin = 0.0;
            _ValueMax = 1.0;

            // 采用 Rey G.Biv 的彩虹
            Add(0.00, new Argb(255, 0, 0));
            Add(0.17, new Argb(255, 165, 0));
            Add(0.33, new Argb(255, 255, 0));
            Add(0.50, new Argb(0, 255, 0));
            Add(0.67, new Argb(0, 0, 255));
            Add(0.84, new Argb(75, 0, 130));
            Add(1.00, new Argb(238, 130, 238));
        }

        #region Private Fields

        private double _ValueMin;   //最小值
        private double _ValueMax;   //最大值

        #endregion

        /// <summary>
        /// 取得对应颜色
        /// </summary>
        /// <param name="value">数值</param>
        /// <param name="colorDefault">默认颜色</param>
        /// <returns></returns>
        public override Color GetColor(double value, Color colorDefault)
        {
            if (value < _ValueMin)
            {
                value = _ValueMin;
            }

            if (value > _ValueMax)
            {
                value = _ValueMax;
            }

            return LookupColor((value - _ValueMin) / (_ValueMax - _ValueMin), colorDefault);
        }

        /// <summary>
        /// 采用插值方法在颜色表中查找数值value对应的颜色
        /// </summary>
        /// <param name="value"></param>
        /// <param name="colorDefault"></param>
        /// <returns></returns>
        private Color LookupColor(double value, Color colorDefault)
        {
            Color retColor = colorDefault;
            int iValidCount = this.ValidItemCount;  // 有效数量

            for (int i = 0; i < iValidCount; i++)
            {
                IPaletteItem item_cur = this[i];

                // 取得调色板当前值
                double valueCurrent = item_cur.Value;

                // 大于 >
                if (value > valueCurrent)
                {
                    continue;
                }

                // 等于 ==
                if (Math.Abs(value - valueCurrent) < double.Epsilon)
                {
                    retColor = item_cur.Color;
                    break;
                }

                // 小于 <
                if (i == 0)
                {
                    retColor = item_cur.Color;
                    break;
                }
                else
                {
                    // i > 0
                    IPaletteItem item_prv = this[i - 1];
                    double ratio = (value - item_prv.Value) / (item_cur.Value - item_prv.Value);

                    Byte rBeg = item_prv.Color.R;
                    Byte gBeg = item_prv.Color.G;
                    Byte bBeg = item_prv.Color.B;

                    Byte rEnd = item_cur.Color.R;
                    Byte gEnd = item_cur.Color.G;
                    Byte bEnd = item_cur.Color.B;

                    Byte r = (Byte)((rBeg + (rEnd - rBeg) * ratio) + 0.5);
                    Byte g = (Byte)((gBeg + (gEnd - gBeg) * ratio) + 0.5);
                    Byte b = (Byte)((bBeg + (bEnd - bBeg) * ratio) + 0.5);

                    retColor = Color.FromArgb((Byte)0xFF, r, g, b);
                    break;
                }

            }//next i

            return retColor;
        }

        public void SetValueRange(double vmin, double vmax)
        {
            if (vmin > vmax)
            {
                double temp = vmin;
                vmin = vmax;
                vmax = temp;
            }

            _ValueMin = vmin;
            _ValueMax = vmax;
        }
        public void GetValueRange(out double vmin, out double vmax)
        {
            vmin = _ValueMin; vmax = _ValueMax;
        }

        public new RainbowPalette Clone()
        {
            RainbowPalette paletteClone = new RainbowPalette();
            double vmin, vmax;
            this.GetValueRange(out vmin, out vmax);
            paletteClone.SetValueRange(vmin, vmax);

            return paletteClone;
        }

        //@@@
    }
}
