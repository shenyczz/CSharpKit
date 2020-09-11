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

#region using - HDF

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
    public unsafe sealed class H5R : H5Common
    {
        static H5R() { H5.open(); }

        /// <summary>
        /// Reference types allowed.
        /// </summary>
        public enum type_t
        {
            /// <summary>
            /// invalid Reference Type
            /// </summary>
            BADTYPE = -1,
            /// <summary>
            /// Object reference
            /// </summary>
            OBJECT,
            /// <summary>
            /// Dataset Region Reference
            /// </summary>
            DATASET_REGION,
            /// <summary>
            /// highest type (Invalid as true type)
            /// </summary>
            MAXTYPE
        }

        public const int OBJ_REF_BUF_SIZE = sizeof(haddr_t);

        public const int DSET_REG_REF_BUF_SIZE = sizeof(haddr_t) + 4;

        /// <summary>
        /// Creates a reference.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5R.html#Reference-Create
        /// </summary>
        /// <param name="refer">Reference created by the function call.</param>
        /// <param name="loc_id">Location identifier used to locate the object
        /// being pointed to.</param>
        /// <param name="name">Name of object at location
        /// <paramref name="loc_id"/>.</param>
        /// <param name="ref_type">Type of reference.</param>
        /// <param name="space_id">Dataspace identifier with selection. Used
        /// only for dataset region references; pass as -1 if reference is an
        /// object reference.</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Rcreate",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t create
            (IntPtr refer, hid_t loc_id, byte[] name, type_t ref_type,
            hid_t space_id);

        /// <summary>
        /// Creates a reference.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5R.html#Reference-Create
        /// </summary>
        /// <param name="refer">Reference created by the function call.</param>
        /// <param name="loc_id">Location identifier used to locate the object
        /// being pointed to.</param>
        /// <param name="name">Name of object at location
        /// <paramref name="loc_id"/>.</param>
        /// <param name="ref_type">Type of reference.</param>
        /// <param name="space_id">Dataspace identifier with selection. Used
        /// only for dataset region references; pass as -1 if reference is an
        /// object reference.</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        /// <remarks>ASCII strings ONLY!</remarks>
        [DllImport(DLLFileName, EntryPoint = "H5Rcreate",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t create
            (IntPtr refer, hid_t loc_id, string name, type_t ref_type,
            hid_t space_id);

#if HDF5_VER1_10
        /// <summary>
        /// Opens the HDF5 object referenced.
        /// </summary>
        /// <param name="obj_id">Valid identifier for the file containing the
        /// referenced object or any object in that file.</param>
        /// <param name="oapl_id"></param>
        /// <param name="ref_type">The reference type of
        /// <paramref name="refer"/>.</param>
        /// <param name="refer">Reference to open.</param>
        /// <returns>Returns identifier of referenced object if successful;
        /// otherwise returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Rdereference2",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern hid_t dereference
            (hid_t obj_id, hid_t oapl_id, type_t ref_type, IntPtr refer);
#else
        /// <summary>
        /// Opens the HDF5 object referenced.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5R.html#Reference-Dereference
        /// </summary>
        /// <param name="obj_id">Valid identifier for the file containing the
        /// referenced object or any object in that file.</param>
        /// <param name="ref_type">The reference type of
        /// <paramref name="refer"/>.</param>
        /// <param name="refer">Reference to open.</param>
        /// <returns>Returns identifier of referenced object if successful;
        /// otherwise returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Rdereference",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern hid_t dereference
            (hid_t obj_id, type_t ref_type, IntPtr refer);
#endif

        /// <summary>
        /// Retrieves a name for a referenced object.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5R.html#Reference-GetName
        /// </summary>
        /// <param name="loc_id">Identifier for the file containing the
        /// reference or for any object in that file.</param>
        /// <param name="ref_type">Type of reference.</param>
        /// <param name="refer">An object or dataset region reference.</param>
        /// <param name="name">A buffer to place the name of the referenced
        /// object or dataset region. If <code>NULL</code>, then this call will
        /// return the size in bytes of the name.</param>
        /// <param name="size">The size of the <paramref name="name"/>
        /// buffer.</param>
        /// <returns>Returns the length of the name if successful, returning 0
        /// (zero) if no name is associated with the identifier. Otherwise
        /// returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Rget_name",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern ssize_t get_name
            (hid_t loc_id, type_t ref_type, IntPtr refer, [In][Out]byte[] name,
            size_t size);

        /// <summary>
        /// Retrieves a name for a referenced object.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5R.html#Reference-GetName
        /// </summary>
        /// <param name="loc_id">Identifier for the file containing the
        /// reference or for any object in that file.</param>
        /// <param name="ref_type">Type of reference.</param>
        /// <param name="refer">An object or dataset region reference.</param>
        /// <param name="name">A buffer to place the name of the referenced
        /// object or dataset region. If <code>NULL</code>, then this call will
        /// return the size in bytes of the name.</param>
        /// <param name="size">The size of the <paramref name="name"/>
        /// buffer.</param>
        /// <returns>Returns the length of the name if successful, returning 0
        /// (zero) if no name is associated with the identifier. Otherwise
        /// returns a negative value.</returns>
        /// <remarks>ASCII strings ONLY!</remarks>
        [DllImport(DLLFileName, EntryPoint = "H5Rget_name",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern ssize_t get_name
            (hid_t loc_id, type_t ref_type, IntPtr refer, [In][Out]StringBuilder name,
            size_t size);

        /// <summary>
        /// Retrieves the type of object that an object reference points to.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5R.html#Reference-GetObjType2
        /// </summary>
        /// <param name="loc_id">The dataset containing the reference object or
        /// the group containing that dataset.</param>
        /// <param name="ref_type">Type of reference to query.</param>
        /// <param name="refer">Reference to query.</param>
        /// <param name="obj_type">Type of referenced object.</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Rget_obj_type2",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t get_obj_type
            (hid_t loc_id, type_t ref_type, IntPtr refer,
            ref H5O.type_t obj_type);

        /// <summary>
        /// Sets up a dataspace and selection as specified by a region reference.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5R.html#Reference-GetRegion
        /// </summary>
        /// <param name="loc_id">File identifier or identifier for any object
        /// in the file containing the referenced region</param>
        /// <param name="ref_type">Reference type of <paramref name="refer"/>,
        /// which must be <code>DATASET_REGION</code>.</param>
        /// <param name="refer">Region reference to open</param>
        /// <returns>Returns a valid dataspace identifier if successful;
        /// otherwise returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Rget_region",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern hid_t get_region
            (hid_t loc_id, type_t ref_type, IntPtr refer);
    }





}
