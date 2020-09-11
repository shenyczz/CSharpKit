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
    internal class H5UnixDLLImporter : H5DLLImporter
    {
        private IntPtr _hLib;
        private const int RTLD_NOW = 2; // for dlopen's flags

        public H5UnixDLLImporter(string libName)
        {
            if (libName == "hdf5.dll")
            {
                libName = "/usr/lib/libhdf5.so";

            }
            if (libName == "hdf5_hd.dll")
            {
                libName = "/usr/lib/libhdf5_hl.so";
            }

            _hLib = dlopen(libName, RTLD_NOW);

            if (_hLib == IntPtr.Zero)
            {
                throw new ArgumentException(
                    String.Format(
                        "Unable to load unmanaged module \"{0}\"",
                        libName));
            }
        }





        [DllImport("libdl.so")]
        protected static extern IntPtr dlopen(string filename, int flags);

        [DllImport("libdl.so")]
        protected static extern IntPtr dlsym(IntPtr handle, string symbol);

        [DllImport("libdl.so")]
        protected static extern IntPtr dlerror();


        protected override IntPtr _GetAddress(string varName)
        {
            var address = dlsym(_hLib, varName);
            var errPtr = dlerror();
            if (errPtr != IntPtr.Zero)
            {
                throw new Exception("dlsym: " + Marshal.PtrToStringAnsi(errPtr));
            }

            return address;
        }

        //}}@@@
    }



}
