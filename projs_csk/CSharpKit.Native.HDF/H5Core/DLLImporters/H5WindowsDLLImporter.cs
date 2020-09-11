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

#region using - LIBHDF


#if HDF5_VER1_10
using hid_t = System.Int64;
#else
#endif

#endregion

namespace CSharpKit.Native.HDF
{
    /// <summary>
    /// 
    /// </summary>
    internal class H5WindowsDLLImporter : H5DLLImporter
    {
        private IntPtr _hLib;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="libName"></param>
        public H5WindowsDLLImporter(string libName)
        {
            _hLib = GetModuleHandle(libName);

            // if the library not been loaded
            if (_hLib == IntPtr.Zero)
            {
                _hLib = LoadLibrary(libName);

                if (_hLib == IntPtr.Zero)
                {
                    try
                    {
                        Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
                    }
                    catch (Exception e)
                    {
                        throw new Exception(string.Format("Couldn't load library \"{0}\"", libName), e);
                    }
                }
            }
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetModuleHandle(string lpszLib);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr LoadLibrary(string lpszLib);


        protected override IntPtr _GetAddress(string varName)
        {
            return GetProcAddress(_hLib, varName);
        }

        //}}@@@
    }



}
