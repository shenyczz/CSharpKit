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

namespace CSharpKit.Data
{
    /// <summary>
    /// ArchiveLevel - 文档级别
    /// </summary>
    public enum ArchiveLevel
    {
        NONE = 0,
        L0,     // 原始数据

        L1,     // 定标数据
        L1A,     // 定标数据
        L1B,     // 定标数据

        L2,     // 产品数据
        L3,     // 综合产品数据
    }

    /// <summary>
    /// 文档类型 - 移到 mtk 
    /// </summary>
    [Flags]
    public enum ArchiveType
    {
        NONE = 0x00,
        
        AMD,
        AWX,
        CVS,
        GPF,
        HDF,
        HDS,
        IMG,
        LDX,
        MICAPS,

        //@@@
    }
    public enum MetsatArchive
    {
        NONE = 0,

        /// <summary>
        /// AWX
        /// </summary>
        AWX,
        /// <summary>
        /// CVS
        /// </summary>
        CVS,
        /// <summary>
        /// GPF
        /// </summary>
        GPF,
        /// <summary>
        /// HDF
        /// </summary>
        HDF,
        /// <summary>
        /// LDF,LD2,LD3
        /// </summary>
        LDX,
        /// <summary>
        /// NetCDF
        /// </summary>
        NC,

        //@@@
    }



}
