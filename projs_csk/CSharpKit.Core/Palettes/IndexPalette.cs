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
    /// 索引调色板
    /// </summary>
    public class IndexPalette : PaletteBase
    {
        public IndexPalette()
        {
            this.PaletteType = PaletteType.Indexing;
        }

        public override Color GetColor(double value, Color colorDefault)
        {
            Color clrReturn = colorDefault;
            //---------------------------------------------
            // 无效的浮点值(-9999),返回
            if (Math.Abs(value - KitConstants.InvalidValue) < double.Epsilon)
            {
                return colorDefault;
            }

            // 有效条目数量
            int nValidItem = this.ValidItemCount;

            double maxValidValue = 0;           // 有效条目最大值
            Color maxValidColor = Color.Black;  // 有效条目最大值颜色
            if (nValidItem > 0)
            {
                maxValidValue = this[nValidItem - 1].Value;
                maxValidColor = this[nValidItem - 1].Color;
            }

            //数值索引(严格按照索引)
            int index = this.IndexOf(value);
            if (index < 0)
            {//没有找到索引
                clrReturn = colorDefault;
            }
            else
            {//是特殊值
                clrReturn = this[index].Color;
            }
            //---------------------------------------------
            return clrReturn;
        }

        //@@@
    }
}
