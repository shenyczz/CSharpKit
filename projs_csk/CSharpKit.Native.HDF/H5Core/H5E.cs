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
    /// H5E - H5 Error 
    /// </summary>
    public unsafe sealed partial class H5E : H5Common
    {
        static H5E() { H5.open(); }

        #region Fields - static

        static readonly hid_t H5E_ERR_CLS_g = H5DLLImporter.Instance.GetHid("H5E_ERR_CLS_g");

        public static hid_t ERR_CLS { get { return H5E_ERR_CLS_g; } }

        static readonly hid_t H5E_DATASET_g = H5DLLImporter.Instance.GetHid("H5E_DATASET_g");

        public static hid_t DATASET { get { return H5E_DATASET_g; } }

        static readonly hid_t H5E_FUNC_g = H5DLLImporter.Instance.GetHid("H5E_FUNC_g");

        public static hid_t FUNC { get { return H5E_FUNC_g; } }

        static readonly hid_t H5E_STORAGE_g = H5DLLImporter.Instance.GetHid("H5E_STORAGE_g");

        public static hid_t STORAGE { get { return H5E_STORAGE_g; } }

        static readonly hid_t H5E_FILE_g = H5DLLImporter.Instance.GetHid("H5E_FILE_g");

        public static hid_t FILE { get { return H5E_FILE_g; } }

        static readonly hid_t H5E_SOHM_g = H5DLLImporter.Instance.GetHid("H5E_SOHM_g");

        public static hid_t SOHM { get { return H5E_SOHM_g; } }

        static readonly hid_t H5E_SYM_g = H5DLLImporter.Instance.GetHid("H5E_SYM_g");

        public static hid_t SYM { get { return H5E_SYM_g; } }

        static readonly hid_t H5E_PLUGIN_g = H5DLLImporter.Instance.GetHid("H5E_PLUGIN_g");

        public static hid_t PLUGIN { get { return H5E_PLUGIN_g; } }

        static readonly hid_t H5E_VFL_g = H5DLLImporter.Instance.GetHid("H5E_VFL_g");

        public static hid_t VFL { get { return H5E_VFL_g; } }

        static readonly hid_t H5E_INTERNAL_g = H5DLLImporter.Instance.GetHid("H5E_INTERNAL_g");

        public static hid_t INTERNAL { get { return H5E_INTERNAL_g; } }

        static readonly hid_t H5E_BTREE_g = H5DLLImporter.Instance.GetHid("H5E_BTREE_g");

        public static hid_t BTREE { get { return H5E_BTREE_g; } }

        static readonly hid_t H5E_REFERENCE_g = H5DLLImporter.Instance.GetHid("H5E_REFERENCE_g");

        public static hid_t REFERENCE { get { return H5E_REFERENCE_g; } }

        static readonly hid_t H5E_DATASPACE_g = H5DLLImporter.Instance.GetHid("H5E_DATASPACE_g");

        public static hid_t DATASPACE { get { return H5E_DATASPACE_g; } }

        static readonly hid_t H5E_RESOURCE_g = H5DLLImporter.Instance.GetHid("H5E_RESOURCE_g");

        public static hid_t RESOURCE { get { return H5E_RESOURCE_g; } }

        static readonly hid_t H5E_PLIST_g = H5DLLImporter.Instance.GetHid("H5E_PLIST_g");

        public static hid_t PLIST { get { return H5E_PLIST_g; } }

        static readonly hid_t H5E_LINK_g = H5DLLImporter.Instance.GetHid("H5E_LINK_g");

        public static hid_t LINK { get { return H5E_LINK_g; } }

        static readonly hid_t H5E_DATATYPE_g = H5DLLImporter.Instance.GetHid("H5E_DATATYPE_g");

        public static hid_t DATATYPE { get { return H5E_DATATYPE_g; } }

        static readonly hid_t H5E_RS_g = H5DLLImporter.Instance.GetHid("H5E_RS_g");

        public static hid_t RS { get { return H5E_RS_g; } }

        static readonly hid_t H5E_HEAP_g = H5DLLImporter.Instance.GetHid("H5E_HEAP_g");

        public static hid_t HEAP { get { return H5E_HEAP_g; } }

        static readonly hid_t H5E_OHDR_g = H5DLLImporter.Instance.GetHid("H5E_OHDR_g");

        public static hid_t OHDR { get { return H5E_OHDR_g; } }

        static readonly hid_t H5E_ATOM_g = H5DLLImporter.Instance.GetHid("H5E_ATOM_g");

        public static hid_t ATOM { get { return H5E_ATOM_g; } }

        static readonly hid_t H5E_ATTR_g = H5DLLImporter.Instance.GetHid("H5E_ATTR_g");

        public static hid_t ATTR { get { return H5E_ATTR_g; } }

        static readonly hid_t H5E_NONE_MAJOR_g = H5DLLImporter.Instance.GetHid("H5E_NONE_MAJOR_g");

        public static hid_t NONE_MAJOR { get { return H5E_NONE_MAJOR_g; } }

        static readonly hid_t H5E_IO_g = H5DLLImporter.Instance.GetHid("H5E_IO_g");

        public static hid_t IO { get { return H5E_IO_g; } }

        static readonly hid_t H5E_SLIST_g = H5DLLImporter.Instance.GetHid("H5E_SLIST_g");

        public static hid_t SLIST { get { return H5E_SLIST_g; } }

        static readonly hid_t H5E_EFL_g = H5DLLImporter.Instance.GetHid("H5E_EFL_g");

        public static hid_t EFL { get { return H5E_EFL_g; } }

        static readonly hid_t H5E_TST_g = H5DLLImporter.Instance.GetHid("H5E_TST_g");

        public static hid_t TST { get { return H5E_TST_g; } }

        static readonly hid_t H5E_ARGS_g = H5DLLImporter.Instance.GetHid("H5E_ARGS_g");

        public static hid_t ARGS { get { return H5E_ARGS_g; } }

        static readonly hid_t H5E_ERROR_g = H5DLLImporter.Instance.GetHid("H5E_ERROR_g");

        public static hid_t ERROR { get { return H5E_ERROR_g; } }

        static readonly hid_t H5E_PLINE_g = H5DLLImporter.Instance.GetHid("H5E_PLINE_g");

        public static hid_t PLINE { get { return H5E_PLINE_g; } }

        static readonly hid_t H5E_FSPACE_g = H5DLLImporter.Instance.GetHid("H5E_FSPACE_g");

        public static hid_t FSPACE { get { return H5E_FSPACE_g; } }

        static readonly hid_t H5E_CACHE_g = H5DLLImporter.Instance.GetHid("H5E_CACHE_g");

        public static hid_t CACHE { get { return H5E_CACHE_g; } }

        static readonly hid_t H5E_SEEKERROR_g = H5DLLImporter.Instance.GetHid("H5E_SEEKERROR_g");

        public static hid_t SEEKERROR { get { return H5E_SEEKERROR_g; } }

        static readonly hid_t H5E_READERROR_g = H5DLLImporter.Instance.GetHid("H5E_READERROR_g");

        public static hid_t READERROR { get { return H5E_READERROR_g; } }

        static readonly hid_t H5E_WRITEERROR_g = H5DLLImporter.Instance.GetHid("H5E_WRITEERROR_g");

        public static hid_t WRITEERROR { get { return H5E_WRITEERROR_g; } }

        static readonly hid_t H5E_CLOSEERROR_g = H5DLLImporter.Instance.GetHid("H5E_CLOSEERROR_g");

        public static hid_t CLOSEERROR { get { return H5E_CLOSEERROR_g; } }

        static readonly hid_t H5E_OVERFLOW_g = H5DLLImporter.Instance.GetHid("H5E_OVERFLOW_g");

        public static hid_t OVERFLOW { get { return H5E_OVERFLOW_g; } }

        static readonly hid_t H5E_FCNTL_g = H5DLLImporter.Instance.GetHid("H5E_FCNTL_g");

        public static hid_t FCNTL { get { return H5E_FCNTL_g; } }

        static readonly hid_t H5E_NOSPACE_g = H5DLLImporter.Instance.GetHid("H5E_NOSPACE_g");

        public static hid_t NOSPACE { get { return H5E_NOSPACE_g; } }

        static readonly hid_t H5E_CANTALLOC_g = H5DLLImporter.Instance.GetHid("H5E_CANTALLOC_g");

        public static hid_t CANTALLOC { get { return H5E_CANTALLOC_g; } }

        static readonly hid_t H5E_CANTCOPY_g = H5DLLImporter.Instance.GetHid("H5E_CANTCOPY_g");

        public static hid_t CANTCOPY { get { return H5E_CANTCOPY_g; } }

        static readonly hid_t H5E_CANTFREE_g = H5DLLImporter.Instance.GetHid("H5E_CANTFREE_g");

        public static hid_t CANTFREE { get { return H5E_CANTFREE_g; } }

        static readonly hid_t H5E_ALREADYEXISTS_g = H5DLLImporter.Instance.GetHid("H5E_ALREADYEXISTS_g");

        public static hid_t ALREADYEXISTS { get { return H5E_ALREADYEXISTS_g; } }

        static readonly hid_t H5E_CANTLOCK_g = H5DLLImporter.Instance.GetHid("H5E_CANTLOCK_g");

        public static hid_t CANTLOCK { get { return H5E_CANTLOCK_g; } }

        static readonly hid_t H5E_CANTUNLOCK_g = H5DLLImporter.Instance.GetHid("H5E_CANTUNLOCK_g");

        public static hid_t CANTUNLOCK { get { return H5E_CANTUNLOCK_g; } }

        static readonly hid_t H5E_CANTGC_g = H5DLLImporter.Instance.GetHid("H5E_CANTGC_g");

        public static hid_t CANTGC { get { return H5E_CANTGC_g; } }

        static readonly hid_t H5E_CANTGETSIZE_g = H5DLLImporter.Instance.GetHid("H5E_CANTGETSIZE_g");

        public static hid_t CANTGETSIZE { get { return H5E_CANTGETSIZE_g; } }

        static readonly hid_t H5E_OBJOPEN_g = H5DLLImporter.Instance.GetHid("H5E_OBJOPEN_g");

        public static hid_t OBJOPEN { get { return H5E_OBJOPEN_g; } }

        static readonly hid_t H5E_CANTRESTORE_g = H5DLLImporter.Instance.GetHid("H5E_CANTRESTORE_g");

        public static hid_t CANTRESTORE { get { return H5E_CANTRESTORE_g; } }

        static readonly hid_t H5E_CANTCOMPUTE_g = H5DLLImporter.Instance.GetHid("H5E_CANTCOMPUTE_g");

        public static hid_t CANTCOMPUTE { get { return H5E_CANTCOMPUTE_g; } }

        static readonly hid_t H5E_CANTEXTEND_g = H5DLLImporter.Instance.GetHid("H5E_CANTEXTEND_g");

        public static hid_t CANTEXTEND { get { return H5E_CANTEXTEND_g; } }

        static readonly hid_t H5E_CANTATTACH_g = H5DLLImporter.Instance.GetHid("H5E_CANTATTACH_g");

        public static hid_t CANTATTACH { get { return H5E_CANTATTACH_g; } }

        static readonly hid_t H5E_CANTUPDATE_g = H5DLLImporter.Instance.GetHid("H5E_CANTUPDATE_g");

        public static hid_t CANTUPDATE { get { return H5E_CANTUPDATE_g; } }

        static readonly hid_t H5E_CANTOPERATE_g = H5DLLImporter.Instance.GetHid("H5E_CANTOPERATE_g");

        public static hid_t CANTOPERATE { get { return H5E_CANTOPERATE_g; } }

        static readonly hid_t H5E_CANTINIT_g = H5DLLImporter.Instance.GetHid("H5E_CANTINIT_g");

        public static hid_t CANTINIT { get { return H5E_CANTINIT_g; } }

        static readonly hid_t H5E_ALREADYINIT_g = H5DLLImporter.Instance.GetHid("H5E_ALREADYINIT_g");

        public static hid_t ALREADYINIT { get { return H5E_ALREADYINIT_g; } }

        static readonly hid_t H5E_CANTRELEASE_g = H5DLLImporter.Instance.GetHid("H5E_CANTRELEASE_g");

        public static hid_t CANTRELEASE { get { return H5E_CANTRELEASE_g; } }

        static readonly hid_t H5E_CANTGET_g = H5DLLImporter.Instance.GetHid("H5E_CANTGET_g");

        public static hid_t CANTGET { get { return H5E_CANTGET_g; } }

        static readonly hid_t H5E_CANTSET_g = H5DLLImporter.Instance.GetHid("H5E_CANTSET_g");

        public static hid_t CANTSET { get { return H5E_CANTSET_g; } }

        static readonly hid_t H5E_DUPCLASS_g = H5DLLImporter.Instance.GetHid("H5E_DUPCLASS_g");

        public static hid_t DUPCLASS { get { return H5E_DUPCLASS_g; } }

        static readonly hid_t H5E_SETDISALLOWED_g = H5DLLImporter.Instance.GetHid("H5E_SETDISALLOWED_g");

        public static hid_t SETDISALLOWED { get { return H5E_SETDISALLOWED_g; } }

        static readonly hid_t H5E_CANTMERGE_g = H5DLLImporter.Instance.GetHid("H5E_CANTMERGE_g");

        public static hid_t CANTMERGE { get { return H5E_CANTMERGE_g; } }

        static readonly hid_t H5E_CANTREVIVE_g = H5DLLImporter.Instance.GetHid("H5E_CANTREVIVE_g");

        public static hid_t CANTREVIVE { get { return H5E_CANTREVIVE_g; } }

        static readonly hid_t H5E_CANTSHRINK_g = H5DLLImporter.Instance.GetHid("H5E_CANTSHRINK_g");

        public static hid_t CANTSHRINK { get { return H5E_CANTSHRINK_g; } }

        static readonly hid_t H5E_LINKCOUNT_g = H5DLLImporter.Instance.GetHid("H5E_LINKCOUNT_g");

        public static hid_t LINKCOUNT { get { return H5E_LINKCOUNT_g; } }

        static readonly hid_t H5E_VERSION_g = H5DLLImporter.Instance.GetHid("H5E_VERSION_g");

        public static hid_t VERSION { get { return H5E_VERSION_g; } }

        static readonly hid_t H5E_ALIGNMENT_g = H5DLLImporter.Instance.GetHid("H5E_ALIGNMENT_g");

        public static hid_t ALIGNMENT { get { return H5E_ALIGNMENT_g; } }

        static readonly hid_t H5E_BADMESG_g = H5DLLImporter.Instance.GetHid("H5E_BADMESG_g");

        public static hid_t BADMESG { get { return H5E_BADMESG_g; } }

        static readonly hid_t H5E_CANTDELETE_g = H5DLLImporter.Instance.GetHid("H5E_CANTDELETE_g");

        public static hid_t CANTDELETE { get { return H5E_CANTDELETE_g; } }

        static readonly hid_t H5E_BADITER_g = H5DLLImporter.Instance.GetHid("H5E_BADITER_g");

        public static hid_t BADITER { get { return H5E_BADITER_g; } }

        static readonly hid_t H5E_CANTPACK_g = H5DLLImporter.Instance.GetHid("H5E_CANTPACK_g");

        public static hid_t CANTPACK { get { return H5E_CANTPACK_g; } }

        static readonly hid_t H5E_CANTRESET_g = H5DLLImporter.Instance.GetHid("H5E_CANTRESET_g");

        public static hid_t CANTRESET { get { return H5E_CANTRESET_g; } }

        static readonly hid_t H5E_CANTRENAME_g = H5DLLImporter.Instance.GetHid("H5E_CANTRENAME_g");

        public static hid_t CANTRENAME { get { return H5E_CANTRENAME_g; } }

        static readonly hid_t H5E_SYSERRSTR_g = H5DLLImporter.Instance.GetHid("H5E_SYSERRSTR_g");

        public static hid_t SYSERRSTR { get { return H5E_SYSERRSTR_g; } }

        static readonly hid_t H5E_NOFILTER_g = H5DLLImporter.Instance.GetHid("H5E_NOFILTER_g");

        public static hid_t NOFILTER { get { return H5E_NOFILTER_g; } }

        static readonly hid_t H5E_CALLBACK_g = H5DLLImporter.Instance.GetHid("H5E_CALLBACK_g");

        public static hid_t CALLBACK { get { return H5E_CALLBACK_g; } }

        static readonly hid_t H5E_CANAPPLY_g = H5DLLImporter.Instance.GetHid("H5E_CANAPPLY_g");

        public static hid_t CANAPPLY { get { return H5E_CANAPPLY_g; } }

        static readonly hid_t H5E_SETLOCAL_g = H5DLLImporter.Instance.GetHid("H5E_SETLOCAL_g");

        public static hid_t SETLOCAL { get { return H5E_SETLOCAL_g; } }

        static readonly hid_t H5E_NOENCODER_g = H5DLLImporter.Instance.GetHid("H5E_NOENCODER_g");

        public static hid_t NOENCODER { get { return H5E_NOENCODER_g; } }

        static readonly hid_t H5E_CANTFILTER_g = H5DLLImporter.Instance.GetHid("H5E_CANTFILTER_g");

        public static hid_t CANTFILTER { get { return H5E_CANTFILTER_g; } }

        static readonly hid_t H5E_CANTOPENOBJ_g = H5DLLImporter.Instance.GetHid("H5E_CANTOPENOBJ_g");

        public static hid_t CANTOPENOBJ { get { return H5E_CANTOPENOBJ_g; } }

        static readonly hid_t H5E_CANTCLOSEOBJ_g = H5DLLImporter.Instance.GetHid("H5E_CANTCLOSEOBJ_g");

        public static hid_t CANTCLOSEOBJ { get { return H5E_CANTCLOSEOBJ_g; } }

        static readonly hid_t H5E_COMPLEN_g = H5DLLImporter.Instance.GetHid("H5E_COMPLEN_g");

        public static hid_t COMPLEN { get { return H5E_COMPLEN_g; } }

        static readonly hid_t H5E_PATH_g = H5DLLImporter.Instance.GetHid("H5E_PATH_g");

        public static hid_t PATH { get { return H5E_PATH_g; } }

        static readonly hid_t H5E_NONE_MINOR_g = H5DLLImporter.Instance.GetHid("H5E_NONE_MINOR_g");

        public static hid_t NONE_MINOR { get { return H5E_NONE_MINOR_g; } }

        static readonly hid_t H5E_OPENERROR_g = H5DLLImporter.Instance.GetHid("H5E_OPENERROR_g");

        public static hid_t OPENERROR { get { return H5E_OPENERROR_g; } }

        static readonly hid_t H5E_FILEEXISTS_g = H5DLLImporter.Instance.GetHid("H5E_FILEEXISTS_g");

        public static hid_t FILEEXISTS { get { return H5E_FILEEXISTS_g; } }

        static readonly hid_t H5E_FILEOPEN_g = H5DLLImporter.Instance.GetHid("H5E_FILEOPEN_g");

        public static hid_t FILEOPEN { get { return H5E_FILEOPEN_g; } }

        static readonly hid_t H5E_CANTCREATE_g = H5DLLImporter.Instance.GetHid("H5E_CANTCREATE_g");

        public static hid_t CANTCREATE { get { return H5E_CANTCREATE_g; } }

        static readonly hid_t H5E_CANTOPENFILE_g = H5DLLImporter.Instance.GetHid("H5E_CANTOPENFILE_g");

        public static hid_t CANTOPENFILE { get { return H5E_CANTOPENFILE_g; } }

        static readonly hid_t H5E_CANTCLOSEFILE_g = H5DLLImporter.Instance.GetHid("H5E_CANTCLOSEFILE_g");

        public static hid_t CANTCLOSEFILE { get { return H5E_CANTCLOSEFILE_g; } }

        static readonly hid_t H5E_NOTHDF5_g = H5DLLImporter.Instance.GetHid("H5E_NOTHDF5_g");

        public static hid_t NOTHDF5 { get { return H5E_NOTHDF5_g; } }

        static readonly hid_t H5E_BADFILE_g = H5DLLImporter.Instance.GetHid("H5E_BADFILE_g");

        public static hid_t BADFILE { get { return H5E_BADFILE_g; } }

        static readonly hid_t H5E_TRUNCATED_g = H5DLLImporter.Instance.GetHid("H5E_TRUNCATED_g");

        public static hid_t TRUNCATED { get { return H5E_TRUNCATED_g; } }

        static readonly hid_t H5E_MOUNT_g = H5DLLImporter.Instance.GetHid("H5E_MOUNT_g");

        public static hid_t MOUNT { get { return H5E_MOUNT_g; } }

        static readonly hid_t H5E_BADATOM_g = H5DLLImporter.Instance.GetHid("H5E_BADATOM_g");

        public static hid_t BADATOM { get { return H5E_BADATOM_g; } }

        static readonly hid_t H5E_BADGROUP_g = H5DLLImporter.Instance.GetHid("H5E_BADGROUP_g");

        public static hid_t BADGROUP { get { return H5E_BADGROUP_g; } }

        static readonly hid_t H5E_CANTREGISTER_g = H5DLLImporter.Instance.GetHid("H5E_CANTREGISTER_g");

        public static hid_t CANTREGISTER { get { return H5E_CANTREGISTER_g; } }

        static readonly hid_t H5E_CANTINC_g = H5DLLImporter.Instance.GetHid("H5E_CANTINC_g");

        public static hid_t CANTINC { get { return H5E_CANTINC_g; } }

        static readonly hid_t H5E_CANTDEC_g = H5DLLImporter.Instance.GetHid("H5E_CANTDEC_g");

        public static hid_t CANTDEC { get { return H5E_CANTDEC_g; } }

        static readonly hid_t H5E_NOIDS_g = H5DLLImporter.Instance.GetHid("H5E_NOIDS_g");

        public static hid_t NOIDS { get { return H5E_NOIDS_g; } }

        static readonly hid_t H5E_CANTFLUSH_g = H5DLLImporter.Instance.GetHid("H5E_CANTFLUSH_g");

        public static hid_t CANTFLUSH { get { return H5E_CANTFLUSH_g; } }

        static readonly hid_t H5E_CANTSERIALIZE_g = H5DLLImporter.Instance.GetHid("H5E_CANTSERIALIZE_g");

        public static hid_t CANTSERIALIZE { get { return H5E_CANTSERIALIZE_g; } }

        static readonly hid_t H5E_CANTLOAD_g = H5DLLImporter.Instance.GetHid("H5E_CANTLOAD_g");

        public static hid_t CANTLOAD { get { return H5E_CANTLOAD_g; } }

        static readonly hid_t H5E_PROTECT_g = H5DLLImporter.Instance.GetHid("H5E_PROTECT_g");

        public static hid_t PROTECT { get { return H5E_PROTECT_g; } }

        static readonly hid_t H5E_NOTCACHED_g = H5DLLImporter.Instance.GetHid("H5E_NOTCACHED_g");

        public static hid_t NOTCACHED { get { return H5E_NOTCACHED_g; } }

        static readonly hid_t H5E_SYSTEM_g = H5DLLImporter.Instance.GetHid("H5E_SYSTEM_g");

        public static hid_t SYSTEM { get { return H5E_SYSTEM_g; } }

        static readonly hid_t H5E_CANTINS_g = H5DLLImporter.Instance.GetHid("H5E_CANTINS_g");

        public static hid_t CANTINS { get { return H5E_CANTINS_g; } }

        static readonly hid_t H5E_CANTPROTECT_g = H5DLLImporter.Instance.GetHid("H5E_CANTPROTECT_g");

        public static hid_t CANTPROTECT { get { return H5E_CANTPROTECT_g; } }

        static readonly hid_t H5E_CANTUNPROTECT_g = H5DLLImporter.Instance.GetHid("H5E_CANTUNPROTECT_g");

        public static hid_t CANTUNPROTECT { get { return H5E_CANTUNPROTECT_g; } }

        static readonly hid_t H5E_CANTPIN_g = H5DLLImporter.Instance.GetHid("H5E_CANTPIN_g");

        public static hid_t CANTPIN { get { return H5E_CANTPIN_g; } }

        static readonly hid_t H5E_CANTUNPIN_g = H5DLLImporter.Instance.GetHid("H5E_CANTUNPIN_g");

        public static hid_t CANTUNPIN { get { return H5E_CANTUNPIN_g; } }

        static readonly hid_t H5E_CANTMARKDIRTY_g = H5DLLImporter.Instance.GetHid("H5E_CANTMARKDIRTY_g");

        public static hid_t CANTMARKDIRTY { get { return H5E_CANTMARKDIRTY_g; } }

        static readonly hid_t H5E_CANTDIRTY_g = H5DLLImporter.Instance.GetHid("H5E_CANTDIRTY_g");

        public static hid_t CANTDIRTY { get { return H5E_CANTDIRTY_g; } }

        static readonly hid_t H5E_CANTEXPUNGE_g = H5DLLImporter.Instance.GetHid("H5E_CANTEXPUNGE_g");

        public static hid_t CANTEXPUNGE { get { return H5E_CANTEXPUNGE_g; } }

        static readonly hid_t H5E_CANTRESIZE_g = H5DLLImporter.Instance.GetHid("H5E_CANTRESIZE_g");

        public static hid_t CANTRESIZE { get { return H5E_CANTRESIZE_g; } }

        static readonly hid_t H5E_TRAVERSE_g = H5DLLImporter.Instance.GetHid("H5E_TRAVERSE_g");

        public static hid_t TRAVERSE { get { return H5E_TRAVERSE_g; } }

        static readonly hid_t H5E_NLINKS_g = H5DLLImporter.Instance.GetHid("H5E_NLINKS_g");

        public static hid_t NLINKS { get { return H5E_NLINKS_g; } }

        static readonly hid_t H5E_NOTREGISTERED_g = H5DLLImporter.Instance.GetHid("H5E_NOTREGISTERED_g");

        public static hid_t NOTREGISTERED { get { return H5E_NOTREGISTERED_g; } }

        static readonly hid_t H5E_CANTMOVE_g = H5DLLImporter.Instance.GetHid("H5E_CANTMOVE_g");

        public static hid_t CANTMOVE { get { return H5E_CANTMOVE_g; } }

        static readonly hid_t H5E_CANTSORT_g = H5DLLImporter.Instance.GetHid("H5E_CANTSORT_g");

        public static hid_t CANTSORT { get { return H5E_CANTSORT_g; } }

        static readonly hid_t H5E_MPI_g = H5DLLImporter.Instance.GetHid("H5E_MPI_g");

        public static hid_t MPI { get { return H5E_MPI_g; } }

        static readonly hid_t H5E_MPIERRSTR_g = H5DLLImporter.Instance.GetHid("H5E_MPIERRSTR_g");

        public static hid_t MPIERRSTR { get { return H5E_MPIERRSTR_g; } }

        static readonly hid_t H5E_CANTRECV_g = H5DLLImporter.Instance.GetHid("H5E_CANTRECV_g");

        public static hid_t CANTRECV { get { return H5E_CANTRECV_g; } }

        static readonly hid_t H5E_CANTCLIP_g = H5DLLImporter.Instance.GetHid("H5E_CANTCLIP_g");

        public static hid_t CANTCLIP { get { return H5E_CANTCLIP_g; } }

        static readonly hid_t H5E_CANTCOUNT_g = H5DLLImporter.Instance.GetHid("H5E_CANTCOUNT_g");

        public static hid_t CANTCOUNT { get { return H5E_CANTCOUNT_g; } }

        static readonly hid_t H5E_CANTSELECT_g = H5DLLImporter.Instance.GetHid("H5E_CANTSELECT_g");

        public static hid_t CANTSELECT { get { return H5E_CANTSELECT_g; } }

        static readonly hid_t H5E_CANTNEXT_g = H5DLLImporter.Instance.GetHid("H5E_CANTNEXT_g");

        public static hid_t CANTNEXT { get { return H5E_CANTNEXT_g; } }

        static readonly hid_t H5E_BADSELECT_g = H5DLLImporter.Instance.GetHid("H5E_BADSELECT_g");

        public static hid_t BADSELECT { get { return H5E_BADSELECT_g; } }

        static readonly hid_t H5E_CANTCOMPARE_g = H5DLLImporter.Instance.GetHid("H5E_CANTCOMPARE_g");

        public static hid_t CANTCOMPARE { get { return H5E_CANTCOMPARE_g; } }

        static readonly hid_t H5E_UNINITIALIZED_g = H5DLLImporter.Instance.GetHid("H5E_UNINITIALIZED_g");

        public static hid_t UNINITIALIZED { get { return H5E_UNINITIALIZED_g; } }

        static readonly hid_t H5E_UNSUPPORTED_g = H5DLLImporter.Instance.GetHid("H5E_UNSUPPORTED_g");

        public static hid_t UNSUPPORTED { get { return H5E_UNSUPPORTED_g; } }

        static readonly hid_t H5E_BADTYPE_g = H5DLLImporter.Instance.GetHid("H5E_BADTYPE_g");

        public static hid_t BADTYPE { get { return H5E_BADTYPE_g; } }

        static readonly hid_t H5E_BADRANGE_g = H5DLLImporter.Instance.GetHid("H5E_BADRANGE_g");

        public static hid_t BADRANGE { get { return H5E_BADRANGE_g; } }

        static readonly hid_t H5E_BADVALUE_g = H5DLLImporter.Instance.GetHid("H5E_BADVALUE_g");

        public static hid_t BADVALUE { get { return H5E_BADVALUE_g; } }

        static readonly hid_t H5E_NOTFOUND_g = H5DLLImporter.Instance.GetHid("H5E_NOTFOUND_g");

        public static hid_t NOTFOUND { get { return H5E_NOTFOUND_g; } }

        static readonly hid_t H5E_EXISTS_g = H5DLLImporter.Instance.GetHid("H5E_EXISTS_g");

        public static hid_t EXISTS { get { return H5E_EXISTS_g; } }

        static readonly hid_t H5E_CANTENCODE_g = H5DLLImporter.Instance.GetHid("H5E_CANTENCODE_g");

        public static hid_t CANTENCODE { get { return H5E_CANTENCODE_g; } }

        static readonly hid_t H5E_CANTDECODE_g = H5DLLImporter.Instance.GetHid("H5E_CANTDECODE_g");

        public static hid_t CANTDECODE { get { return H5E_CANTDECODE_g; } }

        static readonly hid_t H5E_CANTSPLIT_g = H5DLLImporter.Instance.GetHid("H5E_CANTSPLIT_g");

        public static hid_t CANTSPLIT { get { return H5E_CANTSPLIT_g; } }

        static readonly hid_t H5E_CANTREDISTRIBUTE_g = H5DLLImporter.Instance.GetHid("H5E_CANTREDISTRIBUTE_g");

        public static hid_t CANTREDISTRIBUTE { get { return H5E_CANTREDISTRIBUTE_g; } }

        static readonly hid_t H5E_CANTSWAP_g = H5DLLImporter.Instance.GetHid("H5E_CANTSWAP_g");

        public static hid_t CANTSWAP { get { return H5E_CANTSWAP_g; } }

        static readonly hid_t H5E_CANTINSERT_g = H5DLLImporter.Instance.GetHid("H5E_CANTINSERT_g");

        public static hid_t CANTINSERT { get { return H5E_CANTINSERT_g; } }

        static readonly hid_t H5E_CANTLIST_g = H5DLLImporter.Instance.GetHid("H5E_CANTLIST_g");

        public static hid_t CANTLIST { get { return H5E_CANTLIST_g; } }

        static readonly hid_t H5E_CANTMODIFY_g = H5DLLImporter.Instance.GetHid("H5E_CANTMODIFY_g");

        public static hid_t CANTMODIFY { get { return H5E_CANTMODIFY_g; } }

        static readonly hid_t H5E_CANTREMOVE_g = H5DLLImporter.Instance.GetHid("H5E_CANTREMOVE_g");

        public static hid_t CANTREMOVE { get { return H5E_CANTREMOVE_g; } }

        static readonly hid_t H5E_CANTCONVERT_g = H5DLLImporter.Instance.GetHid("H5E_CANTCONVERT_g");

        public static hid_t CANTCONVERT { get { return H5E_CANTCONVERT_g; } }

        static readonly hid_t H5E_BADSIZE_g = H5DLLImporter.Instance.GetHid("H5E_BADSIZE_g");

        public static hid_t BADSIZE { get { return H5E_BADSIZE_g; } }



#if HDF5_VER1_10
        static readonly hid_t H5E_EARRAY_g = H5DLLImporter.Instance.GetHid("H5E_EARRAY_g");

        public static hid_t EARRAY { get { return H5E_EARRAY_g; } }

        static readonly hid_t H5E_FARRAY_g = H5DLLImporter.Instance.GetHid("H5E_FARRAY_g");

        public static hid_t FARRAY { get { return H5E_FARRAY_g; } }

        static readonly hid_t H5E_CANTDEPEND_g = H5DLLImporter.Instance.GetHid("H5E_CANTDEPEND_g");

        public static hid_t CANTDEPEND { get { return H5E_CANTDEPEND_g; } }

        static readonly hid_t H5E_CANTUNDEPEND_g = H5DLLImporter.Instance.GetHid("H5E_CANTUNDEPEND_g");

        public static hid_t CANTUNDEPEND { get { return H5E_CANTUNDEPEND_g; } }

        static readonly hid_t H5E_CANTNOTIFY_g = H5DLLImporter.Instance.GetHid("H5E_CANTNOTIFY_g");

        public static hid_t CANTNOTIFY { get { return H5E_CANTNOTIFY_g; } }

        static readonly hid_t H5E_LOGFAIL_g = H5DLLImporter.Instance.GetHid("H5E_LOGFAIL_g");

        public static hid_t LOGFAIL { get { return H5E_LOGFAIL_g; } }

        static readonly hid_t H5E_CANTCORK_g = H5DLLImporter.Instance.GetHid("H5E_CANTCORK_g");

        public static hid_t CANTCORK { get { return H5E_CANTCORK_g; } }

        static readonly hid_t H5E_CANTUNCORK_g = H5DLLImporter.Instance.GetHid("H5E_CANTUNCORK_g");

        public static hid_t CANTUNCORK { get { return H5E_CANTUNCORK_g; } }

        static readonly hid_t H5E_CANTAPPEND_g = H5DLLImporter.Instance.GetHid("H5E_CANTAPPEND_g");

        public static hid_t CANTAPPEND { get { return H5E_CANTAPPEND_g; } }

#endif

        #endregion

        #region Consts - 常量

        /// <summary>
        /// Value for the default error stack
        /// </summary>
        public const hid_t DEFAULT = 0;

        #endregion

        #region Enums - 枚举

        /// <summary>
        /// Different kinds of error information
        /// </summary>
        public enum type_t
        {
            MAJOR,
            MINOR
        }

        /// <summary>
        /// Error stack traversal direction
        /// </summary>
        public enum direction_t
        {
            /// <summary>
            /// begin deep, end at API function [value = 0]
            /// </summary>
            H5E_WALK_UPWARD = 0,
            /// <summary>
            /// begin at API function, end deep [value = 1]
            /// </summary>
            H5E_WALK_DOWNWARD = 1
        }

        #endregion

        #region Structs - 结构

        /// <summary>
        /// Information about an error; element of error stack
        /// </summary>
        public struct error_t
        {
            /// <summary>
            /// class ID
            /// </summary>
            public hid_t cls_id;

            /// <summary>
            /// major error ID
            /// </summary>
            public hid_t maj_num;

            /// <summary>
            /// minor error ID
            /// </summary>
            public hid_t min_num;

            /// <summary>
            /// line in file where error occurs
            /// </summary>
            public uint line;

            /// <summary>
            /// function in which error occurred
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string func_name;

            /// <summary>
            /// file in which error occurred
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string file_name;

            /// <summary>
            /// optional supplied description
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string desc;
        };

        #endregion

        #region Delegates - 委托

        /// <summary>
        /// Callback for error handling.
        /// </summary>
        /// <param name="estack">Error stack identifier</param>
        /// <param name="client_data">Pointer to client data in the format
        /// expected by the user-defined function.</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate herr_t auto_t
            (hid_t estack, IntPtr client_data);

        /// <summary>
        /// Callback for H5E.walk
        /// </summary>
        /// <param name="n">Indexed position of the error in the stack.</param>
        /// <param name="err_desc">Reference to a data structure describing the
        /// error.</param>
        /// <param name="client_data">Pointer to client data in the format
        /// expected by the user-defined function.</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate herr_t walk_t
            (uint n, ref error_t err_desc, IntPtr client_data);

        #endregion


        /// <summary>
        /// Determines type of error stack.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-AutoIsV2
        /// </summary>
        /// <param name="estack_id">The error stack identifier</param>
        /// <param name="is_stack">A flag indicating which error stack typedef
        /// the specified error stack conforms to.</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Eauto_is_v2",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t auto_is_v2
            (hid_t estack_id, ref uint is_stack);

        /// <summary>
        /// Clears the specified error stack or the error stack for the current
        /// thread.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-Clear2
        /// </summary>
        /// <param name="estack_id">Error stack identifier.</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Eclear2",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t clear(hid_t estack_id);

        /// <summary>
        /// Closes an error message identifier.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-CloseMsg
        /// </summary>
        /// <param name="msg_id">Error message identifier.</param>
        /// <returns>Returns a non-negative value on success; otherwise returns
        /// a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Eclose_msg",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t close_msg(hid_t msg_id);

        /// <summary>
        /// Closes object handle for error stack.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-CloseStack
        /// </summary>
        /// <param name="estack_id">Error stack identifier.</param>
        /// <returns>Returns a non-negative value on success; otherwise returns
        /// a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Eclose_stack",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t close_stack(hid_t estack_id);

        /// <summary>
        /// Add major error message to an error class.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-CreateMsg
        /// </summary>
        /// <param name="cls">Error class identifier.</param>
        /// <param name="msg_type">The type of the error message.</param>
        /// <param name="msg">Major error message.</param>
        /// <returns>Returns a message identifier on success; otherwise returns
        /// a negative value.</returns>
        /// <remarks>ASCII strings ONLY.</remarks>
        [DllImport(DLLFileName, EntryPoint = "H5Ecreate_msg",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern hid_t create_msg
            (hid_t cls, type_t msg_type,
            [MarshalAs(UnmanagedType.LPStr)]string msg);

        /// <summary>
        /// Creates a new empty error stack.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-CreateStack
        /// </summary>
        /// <returns>Returns an error stack identifier on success; otherwise
        /// returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Ecreate_stack",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern hid_t create_stack();

        /// <summary>
        /// Returns the settings for the automatic error stack traversal
        /// function and its data.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-GetAuto2
        /// </summary>
        /// <param name="estack_id">Error stack identifier.
        /// <code>H5E_DEFAULT</code> indicates the current stack.</param>
        /// <param name="func">The function currently set to be called upon an
        /// error condition.</param>
        /// <param name="client_data">Data currently set to be passed to the
        /// error function.</param>
        /// <returns></returns>
        [DllImport(DLLFileName, EntryPoint = "H5Eget_auto2",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t get_auto
            (hid_t estack_id, ref auto_t func, ref IntPtr client_data);

        /// <summary>
        /// Retrieves error class name.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-GetClassName
        /// </summary>
        /// <param name="class_id">Error class identifier.</param>
        /// <param name="name">The name of the class to be queried.</param>
        /// <param name="size">The length of class name to be returned by
        /// this function.</param>
        /// <returns>Returns non-negative value as on success; otherwise
        /// returns negative value.</returns>
        /// <remarks>ASCII strings ONLY!</remarks>
        [DllImport(DLLFileName, EntryPoint = "H5Eget_class_name",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern ssize_t get_class_name(
            hid_t class_id, [In][Out]StringBuilder name, size_t size);

        /// <summary>
        /// Returns copy of current error stack.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-GetCurrentStack
        /// </summary>
        /// <returns>Returns an error stack identifier on success; otherwise
        /// returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Eget_current_stack",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern hid_t get_current_stack();

        /// <summary>
        /// Retrieves an error message.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-GetMsg
        /// </summary>
        /// <param name="msg_id">Idenfier for error message to be queried.</param>
        /// <param name="msg_type">The type of the error message.</param>
        /// <param name="msg">Error message buffer.</param>
        /// <param name="size">The length of error message to be returned by
        /// this function.</param>
        /// <returns>Returns the size of the error message in bytes on success;
        /// otherwise returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Eget_msg",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern ssize_t get_msg(
            hid_t msg_id, ref type_t msg_type, [In][Out]StringBuilder msg, size_t size);

        /// <summary>
        /// Retrieves the number of error messages in an error stack.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-GetNum
        /// </summary>
        /// <param name="estack_id">Error stack identifier.</param>
        /// <returns>Returns a non-negative value on success; otherwise returns
        /// a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Eget_num",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern ssize_t get_num(hid_t estack_id);

        /// <summary>
        /// Deletes specified number of error messages from the error stack.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-Pop
        /// </summary>
        /// <param name="estack_id">Error stack identifier.</param>
        /// <param name="count">The number of error messages to be deleted from
        /// the top of error stack.</param>
        /// <returns></returns>
        [DllImport(DLLFileName, EntryPoint = "H5Epop",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t pop(hid_t estack_id, size_t count);

        /// <summary>
        /// Prints the specified error stack in a default manner.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-Print2
        /// </summary>
        /// <param name="estack_id">Identifier of the error stack to be printed.</param>
        /// <param name="stream">File pointer, or stderr if NULL.</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Eprint2",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t print
            (hid_t estack_id, IntPtr stream);

        /// <summary>
        /// Pushes new error record onto error stack.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-Push2
        /// </summary>
        /// <param name="estack_id">Identifier of the error stack to which the
        /// error record is to be pushed. If the identifier is
        /// <code>H5E.DEFAULT</code> , the error record will be pushed to the
        /// current stack.</param>
        /// <param name="file">Name of the file in which the error was
        /// detected.</param>
        /// <param name="func">Name of the function in which the error was
        /// detected.</param>
        /// <param name="line">Line number within the file at which the error
        /// was detected.</param>
        /// <param name="class_id">Error class identifier.</param>
        /// <param name="major_id">Major error identifier.</param>
        /// <param name="minor_id">Minor error identifier.</param>
        /// <param name="msg">Error description string.</param>
        /// <returns>Returns a non-negative value if successful; otherwise
        /// returns a negative value.</returns>
        /// <remarks>ASCII strings ONLY!</remarks>
        [DllImport(DLLFileName, EntryPoint = "H5Epush2",
            CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t push
            (hid_t estack_id, string file, string func, uint line,
            hid_t class_id, hid_t major_id, hid_t minor_id, string msg);

        /// <summary>
        /// Registers a client library or application program to the HDF5 error
        /// API.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-RegisterClass
        /// </summary>
        /// <param name="cls_name">Name of the error class.</param>
        /// <param name="lib_name">Name of the client library or application to
        /// which the error class belongs.</param>
        /// <param name="version">Version of the client library or application
        /// to which the error class belongs. A NULL can be passed in.</param>
        /// <returns>Returns a class identifier on success; otherwise returns a
        /// negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Eregister_class",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern hid_t register_class
            ([MarshalAs(UnmanagedType.LPStr)]string cls_name,
            [MarshalAs(UnmanagedType.LPStr)]string lib_name,
            [MarshalAs(UnmanagedType.LPStr)]string version);

        /// <summary>
        /// Turns automatic error printing on or off.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-SetAuto2
        /// </summary>
        /// <param name="estack_id">Error stack identifier.</param>
        /// <param name="func">Function to be called upon an error condition.</param>
        /// <param name="client_data">Data passed to the error function.</param>
        /// <returns>Returns a non-negative value on success; otherwise returns
        /// a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Eset_auto2",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t set_auto
            (hid_t estack_id, auto_t func, IntPtr client_data);

        /// <summary>
        /// Replaces the current error stack.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-SetCurrentStack
        /// </summary>
        /// <param name="estack_id">Error stack identifier.</param>
        /// <returns>Returns a non-negative value on success; otherwise returns
        /// a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Eset_current_stack",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t set_current_stack(hid_t estack_id);

        /// <summary>
        /// Removes an error class.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-UnregisterClass
        /// </summary>
        /// <param name="class_id">Error class identifier.</param>
        /// <returns>Returns a non-negative value on success; otherwise returns
        /// a negative value.</returns>
        [DllImport(DLLFileName, EntryPoint = "H5Eunregister_class",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t unregister_class(hid_t class_id);

        /// <summary>
        /// Walks the specified error stack, calling the specified function.
        /// See https://www.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-Walk2
        /// </summary>
        /// <param name="estack_id">Error stack identifier.</param>
        /// <param name="direction">Direction in which the error stack is to be
        /// walked.</param>
        /// <param name="func">Function to be called for each error encountered.</param>
        /// <param name="client_data">Data to be passed with
        /// <paramref name="func"/>.</param>
        /// <returns></returns>
        [DllImport(DLLFileName, EntryPoint = "H5Ewalk2",
            CallingConvention = CallingConvention.Cdecl),
        SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
        public static extern herr_t walk
            (hid_t estack_id, direction_t direction, walk_t func,
            IntPtr client_data);

        //}}@@@
    }







}
