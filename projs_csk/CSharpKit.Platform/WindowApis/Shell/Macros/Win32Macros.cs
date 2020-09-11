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
    /// 本地宏功能<br/>
    /// <see cref="Win32Macros"/>
    /// </summary>
    public sealed class Win32Macros
    {
        Win32Macros() { }

        //sbyte cc;
        //byte cc;

        //short a;
        //int b;
        //long c;

        //ushort a;
        //uint b;
        //ulong c;


        #region --LOBYTE

        public static byte LOBYTE(ushort word)
        {
            return (byte)(((uint)(word)) & 0xff);
        }

        #endregion


        #region --HIBYTE

        public static byte HIBYTE(ushort word)
        {
            return (byte)((((uint)(word)) >> 8) & 0xff);
        }

        #endregion



        #region --LOWORD

        public static ushort LOWORD(IntPtr dword)
        {
            return _getLoWord((uint)dword);
        }

        public static ushort LOWORD(int dword)
        {
            return _getLoWord((uint)dword);
        }
        public static ushort LOWORD(uint dword)
        {
            return _getLoWord((uint)dword);
        }

        private static ushort _getLoWord(uint dword)
        {
            return (ushort)(dword & 0xffff);
        }

        #endregion


        #region --HIWORD

        public static ushort HIWORD(IntPtr dword)
        {
            return _getHiWord((uint)dword);
        }
        public static ushort HIWORD(int dword)
        {
            return _getHiWord((uint)dword);
        }
        public static ushort HIWORD(uint dword)
        {
            return _getHiWord((uint)dword);
        }

        private static ushort _getHiWord(uint dword)
        {
            return (ushort)((dword >> 16) & 0xffff);
        }

        #endregion



        public static ushort MAKEWORD(int a, int b)
        {
            return ((ushort)(((byte)(((uint)(a)) & 0xff)) | ((ushort)((byte)(((uint)(b)) & 0xff))) << 8));
;
        }
        public static uint MAKELONG(int a, int b)
        {
            return ((uint)(((ushort)(((uint)(a)) & 0xffff)) | ((uint)((ushort)(((uint)(b)) & 0xffff))) << 16));
        }

        //#define WHEEL_DELTA                     120
        //#define GET_WHEEL_DELTA_WPARAM(wParam)  ((short)HIWORD(wParam))
        public static short GET_WHEEL_DELTA_WPARAM(uint wParam)
        {
            return (short)HIWORD(wParam);
        }


        //@EndOf(NativeMacrofuns)
    }

}


