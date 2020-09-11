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

namespace CSharpKit.Data
{
    /// <summary>
    /// 图例元素
    /// </summary>
    public class LegendElement : ILegendElement
    {
        public static LegendElement Empty = new LegendElement() { Text = "", };

        /// <summary>
        /// 要素
        /// </summary>
        public Double Value { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public Int32 Color { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        public String Text { get; set; }

        public override string ToString()
        {
            return string.Format("{0:F2}, {2:X}"
                , Value
                , Text
                , Color
                );
        }

        //@EndOf(LegendElement)
    }



}
