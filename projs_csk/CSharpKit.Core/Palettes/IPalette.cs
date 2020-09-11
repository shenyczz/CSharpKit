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
    /// IPalette
    /// TODO:[20171103,syc]反转彩虹调色板
    /// </summary>
    public interface IPalette : IList<IPaletteItem>
    {
        /// <summary>
        /// 单位
        /// </summary>
        string Unit { get; set; }

        /// <summary>
        /// 调色板条目
        /// </summary>
        List<IPaletteItem> Items { get; set; }

        /// <summary>
        /// 调色板类型
        /// </summary>
        PaletteType PaletteType { get; }

        /// <summary>
        /// 透明色
        /// </summary>
        Color TransparentColor { get; set; }

        /// <summary>
        /// 有透明色
        /// </summary>
        Boolean HasTransparentColor { get; set; }

        /// <summary>
        /// 有效条目数量
        /// </summary>
        int ValidItemCount { get; }

        void Add(Double value, Argb argb);
        void Add(Double value, Argb argb, String comment);
        void Add(Double value, Byte r, Byte g, Byte b, String comment);
        void Add(Double value, Byte a, Byte r, Byte g, Byte b, String comment);

        int IndexOf(Double value);

        /// <summary>
        /// 取得指定值的颜色
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultColor"></param>
        /// <returns></returns>
        Color GetColor(Double value, Color defaultColor);
    }
}
