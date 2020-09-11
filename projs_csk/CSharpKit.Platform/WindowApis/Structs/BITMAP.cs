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
    /// 定义了一个逻辑位图.的高度、宽度、颜色和格式位值
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAP
    {
        /// <summary>
        /// 指定位图类型。此成员必须为 0。
        /// </summary>
        public int bmType;

        /// <summary>
        /// 以像素为单位指定位图的宽度。  此宽度都必须大于 0。 
        /// </summary>
        public int bmWidth;

        /// <summary>
        /// 光栅行指定位图的高度。  该高度都必须大于 0。 
        /// </summary>
        public int bmHeight;

        /// <summary>
        /// 在每光栅行指定字节数。
        /// 此值必须是偶数，因为图形设备(GDI)接口，假设位图的位值窗体整数 (2 字节) 值。
        /// 换言之， bmWidthBytes * 8 必须是下的倍数 16 大于或等于获取的值，当 bmWidth 成员乘以 bmBitsPixel 成员时。 
        /// </summary>
        public int bmWidthBytes;

        /// <summary>
        /// 在位图指定颜色产生的数目。 
        /// </summary>
        public Byte bmPlanes;

        /// <summary>
        /// 在必要的每帧的结束平面指定相邻颜色的位数定义像素。 
        /// </summary>
        public Byte bmBitsPixel;

        // <summary>
        // 指向位值的位置位图的。  bmBits 成员必须是较长的指针为 1 字节的值。 
        // </summary>
        //LPVOID bmBits;
    }

}


