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
    /// Win32Api - Gdi32
    /// </summary>
    partial class Win32Api
    {
        private const string dllGdi32 = "Gdi32.dll";

        #region --区域函数(Region)

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreatePolygonRgn(POINT[] points, int pointCount, FillModes fillMode);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern Boolean PtInRegion(IntPtr hRgn, int x, int y);

        #endregion

        [DllImport(dllGdi32, CharSet = CharSet.Auto)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 DeleteObject(IntPtr hObject);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, Int32 width, Int32 height);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SelectClipRgn(IntPtr hdc, IntPtr hRgn);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateRectRgn(Int32 left, Int32 top, Int32 right, Int32 bottom);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 BitBlt(
                IntPtr hdcDest,
                Int32 nXDest,
                Int32 nYDest,
                Int32 nWidth,
                Int32 nHeight,
                IntPtr hdcSrc,
                Int32 nXSrc,
                Int32 nYSrc,
                Raster3Operations dwRop);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 DeleteDC(IntPtr hdc);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 GetClipBox(IntPtr hdc, out RECT rect);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateRectRgnIndirect(ref RECT rect);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreatePen(PenStyle penStyle, Int32 width, UInt32 color);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetStockObject(Int32 fnObject);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern Boolean Rectangle(IntPtr hdc, Int32 left, Int32 top, Int32 right, Int32 bottom);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        public static extern Boolean Polyline(IntPtr hdc, POINT[] points, Int32 pointCount);
    }
}
