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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// Win32Api - User32.dll
    /// </summary>
    partial class Win32Api
    {
        private const string dllUser32 = "User32.dll";


        [DllImport(dllUser32, CharSet = CharSet.Auto)]
        public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);


        [DllImport(dllUser32, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);


        [DllImport(dllUser32, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool InvalidateRgn(IntPtr hWnd, IntPtr hRgn, bool bErase);




        #region --DC Operating

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ReleaseDC(IntPtr hWnd, IntPtr hdc);

        #endregion


        #region --Message Operating 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int DispatchMessage(ref MSG msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int TranslateMessage(ref MSG msg);


        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean PostMessage(IntPtr hWnd, int Msg, int wParam, uint lParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nExitCode"></param>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern void PostQuitMessage(int nExitCode);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="hWnd"></param>
        /// <param name="msgFilterMin"></param>
        /// <param name="msgFilterMax"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetMessage(out MSG msg, IntPtr hWnd, uint msgFilterMin, uint msgFilterMax);

        /// <summary>
        /// The PeekMessage function dispatches incoming sent messages, 
        /// checks the thread message queue for a posted message, 
        /// and retrieves the message (if any exist).
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        [DllImport("User32.dll", EntryPoint = "PeekMessage", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.I4)]
        public static extern int PeekMessage(out MSG msg, IntPtr hWnd,
            uint msgFilterMin, uint msgFilterMax, PeekMessageOptions flags);


        #endregion





        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DragDetect(IntPtr hWnd, POINT point);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetFocus();

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowScrollBar(IntPtr hWnd, int wBar, ScrollBarStyle flags);

        //*********************************
        // FxCop bug, suppress the message
        //*********************************
        [SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "0")]
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr WindowFromPoint(POINT point);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowsHookEx(HookType code, HookProc func, IntPtr hInstance, int threadID);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int UnhookWindowsHookEx(IntPtr hhook);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CallNextHookEx(IntPtr hhook, int code, IntPtr wParam, IntPtr lParam);

        [DllImport("user32")]
        public static extern Boolean GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("User32")]
        public static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);


        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr MonitorFromRect(ref RECT rect, int flags);

        [DllImport("user32.dll", EntryPoint = "GetMonitorInfo", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMonitorInfo(HandleRef hMonitor, [In, Out] MONITORINFOEX info);


        [DllImport("user32.dll", EntryPoint = "SetParent", CharSet = CharSet.Unicode)]
        public static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);



        [DllImport("user32.dll", EntryPoint = "GetClientRect")]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT rect);



        #region --Windows Create

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wndProc"></param>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport(dllUser32, CharSet = CharSet.Auto)]
        public static extern IntPtr CallWindowProc(IntPtr wndProc, IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);


        [DllImport(dllUser32, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateWindowEx(WindowStyleEx dwExStyle,
                                                   string lpszClassName,
                                                   string lpszWindowName,
                                                   WS style,
                                                   int x, int y,
                                                   int width, int height,
                                                   IntPtr hwndParent,
                                                   IntPtr hMenu,
                                                   IntPtr hInst,
                                                   object pvParam);


        [DllImport(dllUser32, CharSet = CharSet.Auto)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);
        public static readonly WndProc DefaultWindowProc = DefWindowProc;

        [DllImport(dllUser32, CharSet = CharSet.Auto)]
        public static extern bool DestroyWindow(IntPtr hwnd);


        [DllImport(dllUser32, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.U2)]
        public static extern short RegisterClassEx([In] ref WNDCLASSEX lpwcx);

        #endregion

        #region --Window Capture

        [DllImport(dllUser32, CharSet = CharSet.Auto)]
        public static extern IntPtr GetCapture();

        [DllImport(dllUser32, CharSet = CharSet.Auto)]
        public static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport(dllUser32, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReleaseCapture();

        #endregion

        #region --Windows Operating

        [DllImport("user32.dll", EntryPoint = "BringWindowToTop")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hWnd, ShowWindowStyle showStyle);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern Int32 ScrollWindow(IntPtr hWnd, Int32 dx, Int32 dy, int rect, int clipRect);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowPos(IntPtr hWnd,
                                              IntPtr hWndAfter,
                                              int X, int Y,
                                              int Width, int Height,
                                              SwpOptions flags);

        #endregion

        #region SetWindowLong

        public static IntPtr SetWindowLong(IntPtr hwnd, GWL index, IntPtr wndProcPtr)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLong32(hwnd, index, wndProcPtr);
            }
            return SetWindowLong64(hwnd, index, wndProcPtr);
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Unicode)]
        private static extern IntPtr SetWindowLong32(IntPtr hwnd, GWL index, IntPtr wndProc);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Unicode)]
        private static extern IntPtr SetWindowLong64(IntPtr hwnd, GWL index, IntPtr wndProc);

        #endregion

        #region GetWindowLong

        [return: MarshalAs(UnmanagedType.I4)]
        public static IntPtr GetWindowLong(IntPtr hWnd, GWL index)
        {
            if (IntPtr.Size == 4)
            {
                return GetWindowLong32(hWnd, index);
            }
            return GetWindowLong64(hWnd, index);
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Unicode)]
        private static extern IntPtr GetWindowLong32(IntPtr hwnd, GWL index);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Unicode)]
        private static extern IntPtr GetWindowLong64(IntPtr hwnd, GWL index);

        #endregion





        //@@@
    }


}
