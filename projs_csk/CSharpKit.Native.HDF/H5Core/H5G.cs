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
    /// <summary>
    /// H5G
    /// </summary>
    public unsafe sealed class H5G : H5Common
    {
        static H5G() { H5.open(); }

        const string dllName = DLLFileName;

        /// <summary>
        /// Types of link storage for groups
        /// </summary>
        public enum storage_type_t
        {
            /// <summary>
            /// Unknown link storage type [value = -1].
            /// </summary>
            UNKNOWN = -1,
            /// <summary>
            /// Links in group are stored with a "symbol table"
            /// (this is sometimes called "old-style" groups) [value = 0].
            /// </summary>
            SYMBOL_TABLE,
            /// <summary>
            /// Links are stored in object header [value = 1].
            /// </summary>
            COMPACT,
            /// <summary>
            /// Links are stored in fractal heap and indexed with v2 B-tree
            /// [value = 2].
            /// </summary>
            DENSE
        }

        /// <summary>
        /// Information struct for group
        /// (for H5Gget_info/H5Gget_info_by_name/H5Gget_info_by_idx)
        /// </summary>
        public struct info_t
        {
            /// <summary>
            /// Type of storage for links in group
            /// </summary>
            public storage_type_t storage_type;
            /// <summary>
            /// Number of links in group
            /// </summary>
            public hsize_t nlinks;
            /// <summary>
            /// Current max. creation order value for group
            /// </summary>
            public long max_corder;
            /// <summary>
            /// Whether group has a file mounted on it
            /// </summary>
            public hbool_t mounted;
        }



        /// <summary>
        /// Closes the specified group.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Close
        /// </summary>
        /// <param name="group_id">Group identifier to release.</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        [SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        [DllImport(dllName, EntryPoint = "H5Gclose",
            CallingConvention = CallingConvention.Cdecl)]
        public extern static herr_t close(hid_t group_id);

        /// <summary>
        /// Creates a new group and links it into the file.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Create2
        /// </summary>
        /// <param name="loc_id">File or group identifier</param>
        /// <param name="name">Absolute or relative name of the link to the
        /// new group</param>
        /// <param name="lcpl_id">Link creation property list identifier</param>
        /// <param name="gcpl_id">Group creation property list identifier</param>
        /// <param name="gapl_id">Group access property list identifier</param>
        /// <returns>Returns a group identifier if successful; otherwise returns
        /// a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Gcreate2",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public extern static hid_t create(
            hid_t loc_id, byte[] name, hid_t lcpl_id = H5P.DEFAULT,
            hid_t gcpl_id = H5P.DEFAULT, hid_t gapl_id = H5P.DEFAULT);

        /// <summary>
        /// Creates a new group and links it into the file.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Create2
        /// </summary>
        /// <param name="loc_id">File or group identifier</param>
        /// <param name="name">Absolute or relative name of the link to the
        /// new group</param>
        /// <param name="lcpl_id">Link creation property list identifier</param>
        /// <param name="gcpl_id">Group creation property list identifier</param>
        /// <param name="gapl_id">Group access property list identifier</param>
        /// <returns>Returns a group identifier if successful; otherwise returns
        /// a negative value.</returns>
        /// <remarks>ASCII strings ONLY!</remarks>
        [DllImport(DLLFileName, EntryPoint = "H5Gcreate2",
            CallingConvention = CallingConvention.Cdecl,
            CharSet = CharSet.Ansi),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public extern static hid_t create(
            hid_t loc_id, string name, hid_t lcpl_id = H5P.DEFAULT,
            hid_t gcpl_id = H5P.DEFAULT, hid_t gapl_id = H5P.DEFAULT);

        /// <summary>
        /// Creates a new empty group without linking it into the file structure.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-CreateAnon
        /// </summary>
        /// <param name="loc_id">File or group identifier specifying the file
        /// in which the new group is to be created</param>
        /// <param name="gcpl_id">Group creation property list identifier</param>
        /// <param name="gapl_id">Group access property list identifier</param>
        /// <returns>Returns a new group identifier if successful; otherwise
        /// returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Gcreate_anon",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public extern static hid_t create_anon(
            hid_t loc_id, hid_t gcpl_id = H5P.DEFAULT,
            hid_t gapl_id = H5P.DEFAULT);

        /// <summary>
        /// Gets a group creation property list identifier.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-GetCreatePlist
        /// </summary>
        /// <param name="group_id"> Identifier of the group.</param>
        /// <returns>Returns an identifier for the group’s creation property
        /// list if successful. Otherwise returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Gget_create_plist",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public extern static hid_t get_create_plist(hid_t group_id);

#if HDF5_VER1_10

        /// <summary>
        /// Flushes all buffers associated with a group to disk.
        /// See https://www.hdfgroup.org/HDF5/docNewFeatures/FineTuneMDC/H5Gflush.htm
        /// </summary>
        /// <param name="group_id">Identifier of the group to be flushed.</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Gflush",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public extern static herr_t flush(hid_t group_id);

#endif

        /// <summary>
        /// Retrieves information about a group.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-GetInfo
        /// </summary>
        /// <param name="loc_id">Group identifier</param>
        /// <param name="ginfo">Struct in which group information is returned
        /// </param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Gget_info",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public extern static herr_t get_info(hid_t loc_id, ref info_t ginfo);

        /// <summary>
        /// Retrieves information about a group, according to the group’s
        /// position within an index.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-GetInfoByIdx
        /// </summary>
        /// <param name="loc_id">File or group identifier</param>
        /// <param name="group_name">Name of group containing group for which
        /// information is to be retrieved</param>
        /// <param name="idx_type">Index type</param>
        /// <param name="order">Order of the count in the index</param>
        /// <param name="n">Position in the index of the group for which
        /// information is retrieved</param>
        /// <param name="ginfo">Struct in which group information is returned</param>
        /// <param name="lapl_id">Link access property list</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Gget_info_by_idx",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public extern static herr_t get_info_by_idx(
            hid_t loc_id, byte[] group_name,
            H5.index_t idx_type, H5.iter_order_t order, hsize_t n,
            ref info_t ginfo, hid_t lapl_id = H5P.DEFAULT);

        /// <summary>
        /// Retrieves information about a group, according to the group’s
        /// position within an index.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-GetInfoByIdx
        /// </summary>
        /// <param name="loc_id">File or group identifier</param>
        /// <param name="group_name">Name of group containing group for which
        /// information is to be retrieved</param>
        /// <param name="idx_type">Index type</param>
        /// <param name="order">Order of the count in the index</param>
        /// <param name="n">Position in the index of the group for which
        /// information is retrieved</param>
        /// <param name="ginfo">Struct in which group information is returned</param>
        /// <param name="lapl_id">Link access property list</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        /// <remarks>ASCII strings ONLY!</remarks>
        [SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        [DllImport(DLLFileName, EntryPoint = "H5Gget_info_by_idx",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl)]
        public extern static herr_t get_info_by_idx
            (hid_t loc_id, string group_name,
            H5.index_t idx_type, H5.iter_order_t order, hsize_t n,
            ref info_t ginfo, hid_t lapl_id = H5P.DEFAULT);

        /// <summary>
        /// Retrieves information about a group.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-GetInfoByName
        /// </summary>
        /// <param name="loc_id">File or group identifier</param>
        /// <param name="name">Name of group for which information is to be
        /// retrieved</param>
        /// <param name="ginfo">Struct in which group information is returned</param>
        /// <param name="lapl_id">Link access property list</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Gget_info_by_name",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public extern static herr_t get_info_by_name
            (hid_t loc_id, byte[] name, ref info_t ginfo,
            hid_t lapl_id = H5P.DEFAULT);

        /// <summary>
        /// Retrieves information about a group.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-GetInfoByName
        /// </summary>
        /// <param name="loc_id">File or group identifier</param>
        /// <param name="name">Name of group for which information is to be
        /// retrieved</param>
        /// <param name="ginfo">Struct in which group information is returned</param>
        /// <param name="lapl_id">Link access property list</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        /// <remarks>ASCII strngs ONLY!</remarks>
        [DllImport(DLLFileName, EntryPoint = "H5Gget_info_by_name",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public extern static herr_t get_info_by_name
            (hid_t loc_id, string name, ref info_t ginfo,
            hid_t lapl_id = H5P.DEFAULT);

        /// <summary>
        /// Opens an existing group with a group access property list.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Open2
        /// </summary>
        /// <param name="loc_id">File or group identifier specifying the
        /// location of the group to be opened</param>
        /// <param name="name">Name of the group to open</param>
        /// <param name="gapl_id">Group access property list identifier</param>
        /// <returns>Returns a group identifier if successful; otherwise
        /// returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Gopen2",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public extern static hid_t open(hid_t loc_id,
            byte[] name,
            hid_t gapl_id = H5P.DEFAULT);

        /// <summary>
        /// Opens an existing group with a group access property list.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Open2
        /// </summary>
        /// <param name="loc_id">File or group identifier specifying the
        /// location of the group to be opened</param>
        /// <param name="name">Name of the group to open</param>
        /// <param name="gapl_id">Group access property list identifier</param>
        /// <returns>Returns a group identifier if successful; otherwise
        /// returns a negative value.</returns>
        /// <remarks>ASCII strings ONLY!</remarks>
        [SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        [DllImport(DLLFileName, EntryPoint = "H5Gopen2",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static hid_t open(hid_t loc_id,
            string name,
            hid_t gapl_id = H5P.DEFAULT);

#if HDF5_VER1_10

        /// <summary>
        /// Refreshes all buffers associated with a group.
        /// See https://www.hdfgroup.org/HDF5/docNewFeatures/FineTuneMDC/H5Grefresh.htm
        /// </summary>
        /// <param name="group_id">Identifier of the group to be refreshed.</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        [SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        [DllImport(DLLFileName, EntryPoint = "H5Grefresh",
            CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public extern static herr_t refresh(hid_t group_id);

#endif

        //}}@@@
    }










}
