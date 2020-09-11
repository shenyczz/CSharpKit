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
    public unsafe sealed class H5FD : H5Common
    {
        static H5FD() { H5.open(); }

        /// <summary>
        /// Define enum for the source of file image callbacks
        /// </summary>
        public enum file_image_op_t
        {
            NO_OP,
            PROPERTY_LIST_SET,
            PROPERTY_LIST_COPY,
            PROPERTY_LIST_GET,
            PROPERTY_LIST_CLOSE,
            FILE_OPEN,
            FILE_RESIZE,
            FILE_CLOSE
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr image_malloc_t
        (size_t size, file_image_op_t file_image_op, IntPtr udata);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr image_memcpy_t
        (IntPtr dest, IntPtr src, size_t size, file_image_op_t file_image_op,
        IntPtr udata);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr image_realloc_t
        (IntPtr ptr, size_t size, file_image_op_t file_image_op, IntPtr udata);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate herr_t image_free_t
        (IntPtr ptr, file_image_op_t file_image_op, IntPtr udata);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr udata_copy_t(IntPtr udata);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate herr_t udata_free_t(IntPtr udata);

        /// <summary>
        /// Define structure to hold file image callbacks
        /// </summary>
        public struct file_image_callbacks_t
        {
            public image_malloc_t image_malloc;

            public image_memcpy_t image_memcpy;

            public image_realloc_t image_realloc;

            public image_free_t image_free;

            public udata_copy_t udata_copy;

            public udata_free_t udata_free;

            public IntPtr udata;
        }

        //}}@@@
    }










}
