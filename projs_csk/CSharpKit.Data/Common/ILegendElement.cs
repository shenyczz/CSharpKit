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
    /// ILegendElement - 图例元素
    /// </summary>
    public interface ILegendElement
    {
        /// <summary>
        /// 要素
        /// </summary>
        Double Value { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        Int32 Color { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        String Text { get; set; }

    };


}
