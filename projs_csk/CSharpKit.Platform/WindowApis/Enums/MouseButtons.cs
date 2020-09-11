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
    [Flags]
    public enum MouseButtons_0
    {
        None        = 0x00000000,
        Left        = 0x00100000,          // 1048576,
        Right       = 0x00200000,         // 2097152,
        Middle      = 0x00400000,        // 4194304,
        Xbutton1    = 0x00800000,      // 8388608,
        Xbutton2    = 0x01000000,      // 16777216
    }


}
