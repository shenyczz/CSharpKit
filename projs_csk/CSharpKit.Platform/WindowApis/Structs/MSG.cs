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
    /// MSG - 实现 Windows 消息
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct MSG
    {
        /// <summary>
        /// 窗口句柄
        /// </summary>
        public IntPtr HWnd;
        /// <summary>
        /// 消息的 ID 号
        /// </summary>
        public int Msg;
        /// <summary>
        /// 消息的 WParam 字段
        /// </summary>
        public IntPtr WParam;
        /// <summary>
        /// 消息的 LParam 字段
        /// </summary>
        public IntPtr LParam;
        /// <summary>
        /// 
        /// </summary>
        public ulong time;
        /// <summary>
        /// 
        /// </summary>
        public POINT Point;
    }
}
