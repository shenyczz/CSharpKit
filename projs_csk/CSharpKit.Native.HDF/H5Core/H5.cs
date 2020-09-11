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
using System.Security;
using System.Text;

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
    /*
    H5 库函数：H5函数的通用目的 
    H5A 注解(Annotation)接口：属性存取(access)和操作例程
    H5D 数据集接口：数据集存取和操作接口 
    H5E 错误接口：错误处理例程 
    H5F 文件接口：文件存取例程 
    H5G 群组接口：群组创建和运行例程 
    H5I 标识号接口：标识号例程 
    H5P 特性（Property）列表接口：对象特性列表操作例程 
    H5R 引用接口：引用例程 
    H5S 数据大小接口：数据大小定义和存取例程 
    H5T 数据类型接口：数据类型创建和操作例程 
    H5Z 压缩接口：压缩例程
     */
    //public unsafe sealed class H5// : H5Api
    /// <summary>
    /// H5，调用 H5.open 来初始化 HDF 库
    /// </summary>
    public unsafe sealed class H5 : H5Common
    {
        static H5()
        {
            H5NativeDependency.ResolvePathToExternalDependencies();
        }


        #region Consts - 常量 

        public const hsize_t HSIZE_UNDEF = unchecked((hsize_t)(hssize_t)(-1));
        public const hsize_t HADDR_UNDEF = unchecked((haddr_t)(Int64)(-1));
        public const hsize_t HADDR_MAX = HADDR_UNDEF - 1;

        #endregion

        #region Enums - 枚举

        /// <summary>
        /// Common iteration orders
        /// </summary>
        public enum iter_order_t : int
        {
            /// <summary>
            /// Unknown order [value = -1].
            /// </summary>
            UNKNOWN = -1,
            /// <summary>
            /// Increasing order [value = 0].
            /// </summary>
            INC,
            /// <summary>
            /// Decreasing order [value = 1].
            /// </summary>
            DEC,
            /// <summary>
            /// No particular order, whatever is fastest [value = 2].
            /// </summary>
            NATIVE,
            /// <summary>
            /// Number of iteration orders [value = 3].
            /// </summary>
            N
        }

        ///<summary>
        /// Iteration callback values
        /// (Actually, any postive value will cause the iterator to stop and
        /// pass back that positive value to the function that called the
        /// iterator)
        ///</summary>
        public enum H5IterationResult : int
        {
            /// <summary>
            /// Failure [value = -1].
            /// </summary>
            FAILURE = -1,
            /// <summary>
            /// Success and continue [value = 0].
            /// </summary>
            CONT = 0,
            /// <summary>
            /// Success and stop [value = 1].
            /// </summary>
            STOP = 1
        }

        /// <summary>
        /// The types of indices on links in groups/attributes on objects.
        /// Primarily used for "[do] [foo] by index" routines and for iterating
        /// over links in groups/attributes on objects.
        /// </summary>
        public enum index_t : int
        {
            /// <summary>
            /// Unknown index type [value = -1].
            /// </summary>
            UNKNOWN = -1,
            /// <summary>
            /// Index on names [value = 0].
            /// </summary>
            NAME,
            /// <summary>
            /// Index on creation order [value = 1].
            /// </summary>
            CRT_ORDER,
            /// <summary>
            /// Number of indices defined [value = 2].
            /// </summary>
            N
        };

        #endregion

        #region Structs - 结构

        /// <summary>
        /// Storage info struct used by H5O_info_t and H5F_info_t 
        /// </summary>
        public struct ih_info_t
        {
            /// <summary>
            /// btree and/or list
            /// </summary>
            public hsize_t index_size;
            public hsize_t heap_size;
        }

        #endregion


        /// <summary>
        /// Allocates memory that will later be freed internally by the HDF5
        /// Library.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-AllocateMemory
        /// </summary>
        /// <param name="size">
        /// Specifies the size in bytes of the buffer to be allocated.
        /// </param>
        /// <param name="clear">
        /// Specifies whether the new buffer is to be initialized to 0 (zero).
        /// </param>
        /// <returns>
        /// On success, returns pointer to newly allocated buffer or returns
        /// NULL if size is 0 (zero). Returns NULL on failure.
        /// </returns>
        [DllImport(DLLFileName, EntryPoint = "H5allocate_memory",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern IntPtr allocate_memory(IntPtr size, hbool_t clear);

        /// <summary>
        /// Flushes all data to disk, closes all open identifiers, and cleans
        /// up memory.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-Close
        /// </summary>
        /// <returns>
        /// Returns a non-negative value if successful; otherwise returns a
        /// negative value.
        /// </returns>
        [DllImport(DLLFileName, EntryPoint = "H5close",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t close();

        /// <summary>
        /// Instructs library not to install atexit cleanup routine.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-DontAtExit
        /// </summary>
        /// <returns>
        /// Returns a non-negative value if successful; otherwise returns a
        /// negative value.
        /// </returns>
        [DllImport(DLLFileName, EntryPoint = "H5dont_atexit",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t dont_atexit();

        /// <summary>
        /// Frees memory allocated by the HDF5 Library.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-FreeMemory
        /// </summary>
        /// <param name="buf">
        /// Buffer to be freed. Can be NULL.
        /// </param>
        /// <returns>
        /// Returns a non-negative value if successful; otherwise returns a
        /// negative value.
        /// </returns>
        [DllImport(DLLFileName, EntryPoint = "H5free_memory",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t free_memory(IntPtr buf);

        /// <summary>
        /// Garbage collects on all free-lists of all types.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-GarbageCollect
        /// </summary>
        /// <returns>
        /// Returns a non-negative value if successful; otherwise returns a
        /// negative value.
        /// </returns>
        [DllImport(DLLFileName, EntryPoint = "H5garbage_collect",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t garbage_collect();

        /// <summary>
        /// Returns the HDF library release number.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-Version
        /// </summary>
        /// <param name="majnum">
        /// The major version of the library.
        /// </param>
        /// <param name="minnum">
        /// The minor version of the library.
        /// </param>
        /// <param name="relnum">
        /// The release number of the library.
        /// </param>
        /// <returns>
        /// Returns a non-negative value if successful; otherwise returns a
        /// negative value.
        /// </returns>
        [DllImport(DLLFileName, EntryPoint = "H5get_libversion",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t get_libversion
            (ref uint majnum, ref uint minnum, ref uint relnum);

        /// <summary>
        /// Determine whether the HDF5 Library was built with the thread-safety
        /// feature enabled.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-IsLibraryThreadsafe
        /// </summary>
        /// <param name="is_ts">
        /// Boolean value indicating whether the library was built with
        /// thread-safety enabled.
        /// </param>
        /// <returns>
        /// Returns a non-negative value if successful; otherwise returns a
        /// negative value.
        /// </returns>
        [DllImport(DLLFileName, EntryPoint = "H5is_library_threadsafe",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t is_library_threadsafe(ref hbool_t is_ts);

        /// <summary>
        /// Initializes the HDF5 library.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-Open
        /// </summary>
        /// <returns>
        /// Returns a non-negative value if successful; otherwise returns a
        /// negative value.
        /// </returns>
        [DllImport(DLLFileName, EntryPoint = "H5open",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t open();

        /// <summary>
        /// Resizes and possibly re-allocates memory that will later be freed
        /// internally by the HDF5 Library.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-ResizeMemory
        /// </summary>
        /// <param name="mem">
        /// Pointer to a buffer to be resized. May be NULL.
        /// </param>
        /// <param name="size">
        /// New size of the buffer, in bytes.
        /// </param>
        /// <returns>
        /// On success, returns pointer to resized or reallocated buffer or
        /// returns NULL if size is 0 (zero). Returns NULL on failure.
        /// </returns>
        [DllImport(DLLFileName, EntryPoint = "H5resize_memory",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern IntPtr resize_memory(IntPtr mem, IntPtr size);


        /// <summary>
        /// Sets free-list size limits.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-SetFreeListLimits
        /// </summary>
        /// <param name="reg_global_lim">
        /// The cumulative limit, in bytes, on memory used for all regular
        /// free lists. (Default: 1MB)
        /// </param>
        /// <param name="reg_list_lim">
        /// The limit, in bytes, on memory used for each regular free list.
        /// (Default: 64KB)
        /// </param>
        /// <param name="arr_global_lim">
        /// The cumulative limit, in bytes, on memory used for all array
        /// free lists.(Default: 4MB)
        /// </param>
        /// <param name="arr_list_lim">
        /// The limit, in bytes, on memory used for each array free list.
        /// (Default: 256KB)
        /// </param>
        /// <param name="blk_global_lim">
        /// The cumulative limit, in bytes, on memory used for all block
        /// free lists and, separately, for all factory free lists.
        /// (Default: 16MB)
        /// </param>
        /// <param name="blk_list_lim">
        /// The limit, in bytes, on memory used for each block or factory
        /// free list. (Default: 1MB)
        /// </param>
        /// <returns>
        /// Returns a non-negative value if successful; otherwise returns a
        /// negative value.
        /// </returns>
        [DllImport(DLLFileName,
            EntryPoint = "H5set_free_list_limits",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t set_free_list_limits
            (int reg_global_lim, int reg_list_lim, int arr_global_lim,
            int arr_list_lim, int blk_global_lim, int blk_list_lim);

        //}}@@@
    }





}
