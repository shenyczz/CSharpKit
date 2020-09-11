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
    /// 标准调色板
    /// </summary>
    public class StandardPalette : PaletteBase
    {
        public StandardPalette()
        {
            this.PaletteType = PaletteType.Standard;
        }

        /// <summary>
        /// 取得数值对应的颜色
        /// </summary>
        /// <remarks>
        /// 如果没有找到则返回默认颜色
        /// </remarks>
        /// <param name="value">数值</param>
        /// <param name="colorDefault">默认颜色</param>
        /// <returns>颜色值</returns>
        public override Color GetColor(double value, Color colorDefault)
        {
            Color clrReturn = colorDefault;
            //---------------------------------------------
            // 无效的浮点值(-9999),返回
            if (Math.Abs(value - KitConstants.InvalidValue) < 1.0e-12)
            {
                return colorDefault;
            }

            // 有效条目数量
            int nValidItem = this.ValidItemCount;

            double maxValidValue = 0;           // 有效条目最大值
            Color maxValidColor = Color.Black;    // 有效条目最大值颜色
            if (nValidItem > 0)
            {
                maxValidValue = this[nValidItem - 1].Value;
                maxValidColor = this[nValidItem - 1].Color;
            }

            //数值索引
            int index = this.IndexOf(value);
            if (index >= nValidItem)
            {//是特殊值
                clrReturn = this[index].Color;
            }
            else
            {//不是特殊值

                if (value > maxValidValue)
                {//大于最大有效值，取最大有效值对应的颜色
                    clrReturn = maxValidColor;
                }
                else
                {//
                    for (int i = 0; i < nValidItem; i++)
                    {
                        IPaletteItem item = this[i];
                        // <= vv
                        if (value <= item.Value)
                        {
                            clrReturn = item.Color;
                            break;
                        }

                        continue;
                    }
                }
            }
           //---------------------------------------------
            return clrReturn;
        }

        //@@@
    }
}
