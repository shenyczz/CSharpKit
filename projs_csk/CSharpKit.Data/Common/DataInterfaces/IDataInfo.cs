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
using CSharpKit.GeoApi;

namespace CSharpKit.Data
{
    /// <summary>
    /// IDataInfo - 数据信息接口 
    /// </summary>
    public interface IDataInfo : IOwner
    {
        /// <summary>
        /// 档案级别
        /// </summary>
        ArchiveLevel ArchiveLevel { get; set; }

        /// <summary>
        /// 魔术标识
        /// </summary>
        Int32 MagicID { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        String Comment { get; set; }

        /// <summary>
        /// 日期时间
        /// </summary>
        DateTime DateTime { get; set; }

        /// <summary>
        /// 数据代码
        /// </summary>
        Int32 DataCode { get; set; }

        /// <summary>
        /// 数据范围
        /// </summary>
        IExtent Extent { get; set; }

        /// <summary>
        /// 网格信息
        /// </summary>
        IGridInfo GridInfo { get; set; }

        /// <summary>
        /// 等值线信息
        /// </summary>
        IContourInfo ContourInfo { get; set; }


        //@@@
    }

}
