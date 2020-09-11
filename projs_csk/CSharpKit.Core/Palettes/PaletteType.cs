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
    /// PaletteType - 调色板类型
    /// </summary>
    public enum PaletteType
    {
        /// <summary>
        /// 未知调色板
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// 标准调色板
        /// </summary>
        Standard = 1,

        /// <summary>
        /// 索引调色板
        /// </summary>
        Indexing = 2,

        /// <summary>
        /// 分段调色板
        /// </summary>
        Segment = 4,

        /// <summary>
        /// 彩虹调色板
        /// </summary>
        Rainbow = 3,

        /// <summary>
        /// 线性调色板
        /// </summary>
        Linear = 5,
    }
}
