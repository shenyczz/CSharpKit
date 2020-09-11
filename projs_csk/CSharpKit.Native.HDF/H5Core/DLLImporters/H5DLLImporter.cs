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

#region using - LIBHDF

using haddr_t = System.UInt64;
using hbool_t = System.UInt32;
using herr_t = System.Int32;
using htri_t = System.Int32;
using size_t = System.IntPtr;
using ssize_t = System.IntPtr;
using hsize_t = System.UInt64;
using hssize_t = System.Int64;
using time_t = System.UInt64;
using uint32_t = System.UInt32;
using uint64_t = System.UInt64;
using off_t = System.Int64;

using H5O_msg_crt_idx_t = System.UInt32;

#if HDF5_VER1_10
using hid_t = System.Int64;
#else
using hid_t = System.Int32;
#endif

#endregion

namespace CSharpKit.Native.HDF
{
    internal delegate T Converter<T>(IntPtr address); 

    /// <summary>
    /// Helper class used to fetch public variables (e.g. native type values)
    /// exported by the HDF5 DLL
    /// </summary>
    internal abstract class H5DLLImporter
    {
        const string DLLFileName = H5Common.DLLFileName;


        /// <summary>
        /// H5LibImporter
        /// </summary>
        static H5DLLImporter()
        {
            H5.open();

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    Instance = new H5WindowsDLLImporter(DLLFileName);
                    break;

                case PlatformID.Xbox:
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                    Instance = new H5UnixDLLImporter(DLLFileName);
                    break;

                default:
                    throw new NotImplementedException(); ;
            }
        }

        /// <summary>
        /// 本类实例
        /// </summary>
        public static readonly H5DLLImporter Instance;

        protected abstract IntPtr _GetAddress(string varName);

        public IntPtr GetAddress(string varName)
        {
            return _GetAddress(varName);
        }

        public bool GetAddress(string varName, out IntPtr address)
        {
            address = _GetAddress(varName);
            return (address != IntPtr.Zero);
        }

        public unsafe hid_t GetHid(string varName)
        {
            return *(hid_t*)this.GetAddress(varName);
        }

        //}}@@@
    }



}
