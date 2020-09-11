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
using System.Runtime.InteropServices;

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// 包含有关窗口的最大大小的信息和位置及其最小和最大跟踪的范围。
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct MINMAXINFO
    {
        /// <summary>
        /// 保留以供内部使用
        /// </summary>
        public POINT ptReserved;
        /// <summary>
        /// 指定该最大化的宽度 (point.x) 和 " 最大化的高度 (point.y) 窗口。
        /// </summary>
        public POINT ptMaxSize;
        /// <summary>
        /// 指定最大化的窗口 (point.x) 的左边的位置和最大化的窗口 (point.y) 顶部的位置。
        /// </summary>
        public POINT ptMaxPosition;
        /// <summary>
        /// 指定最低履带宽度 (point.x) 和最小跟踪的高度 (point.y) 窗口。
        /// </summary>
        public POINT ptMinTrackSize;
        /// <summary>
        /// 指定最大履带宽度 (point.x) 和最大值跟踪的高度 (point.y) 窗口。
        /// </summary>
        public POINT ptMaxTrackSize;
    }

}
