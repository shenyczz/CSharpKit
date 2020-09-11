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

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// Type of a variant
    /// </summary>
    [Flags]
    public enum VariantType : ushort
    {
        /// <summary>
        /// Simple value
        /// </summary>
        Default = 0x0000,

        /// <summary>
        /// Vector value.
        /// </summary>
        Vector = 0x1000,

        /// <summary>
        /// Array value.
        /// </summary>
        Array = 0x2000,

        /// <summary>
        /// By reference.
        /// </summary>
        ByRef = 0x4000,

        /// <summary>
        /// Reserved value.
        /// </summary>
        Reserved = 0x8000,
    }


}
