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
using System.Text;

namespace CSharpKit.Palettes
{
    /// <summary>
    /// LinearPalette - 线性调色板
    /// </summary>
    public class LinearPalette : RainbowPalette
    {
        /// <summary>
        /// 线性调色板
        /// </summary>
        public LinearPalette()
            : this(0) { }

        /// <summary>
        /// 线性调色板
        /// </summary>
        /// <param name="flag">
        /// 0：浅绿色过度到红色<br/>
        /// 1：红色过度到浅绿色<br/>
        /// </param>
        public LinearPalette(int flag)
        {
            this.PaletteType = PaletteType.Linear;
            this.Clear();

            if (flag == 0)
            {
                // 浅绿色过度到红色
                Add(0.00, new Argb(0, 128, 0));
                Add(0.20, new Argb(0, 200, 0));
                Add(0.40, new Argb(128, 255, 0));
                Add(0.60, new Argb(255, 238, 0));
                Add(0.80, new Argb(255, 97, 0));
                Add(1.00, new Argb(255, 0, 0));
            }
            else if (flag == 1)
            {
                // 红色过度到浅绿色
                Add(0.00, new Argb(255, 0, 0));
                Add(0.20, new Argb(255, 97, 0));
                Add(0.40, new Argb(255, 238, 0));
                Add(0.60, new Argb(128, 255, 0));
                Add(0.80, new Argb(0, 200, 0));
                Add(1.00, new Argb(0, 128, 0));
            }
            else
            {
                // 红色过度到浅绿色
                Add(0.00, new Argb(255, 0, 0));
                Add(0.20, new Argb(255, 97, 0));
                Add(0.40, new Argb(255, 238, 0));
                Add(0.60, new Argb(128, 255, 0));
                Add(0.80, new Argb(0, 200, 0));
                Add(1.00, new Argb(0, 128, 0));
            }

        }


        public new LinearPalette Clone()
        {
            return new LinearPalette();
        }

        //@@@
    }
}
