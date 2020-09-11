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
    /// 调色板条目
    /// </summary>
    public interface IPaletteItem: ICloneable
    {
        /// <summary>
        /// 数值
        /// </summary>
        Double Value { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        Color Color { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        String Comment { get; set; }
    }
}
