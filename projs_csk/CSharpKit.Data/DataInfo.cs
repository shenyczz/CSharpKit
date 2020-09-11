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
    /// DataInfo
    /// </summary>
    public abstract class DataInfo : Target, IDataInfo
    {
        protected DataInfo(IDataInstance owner)
            : base(Guid.NewGuid().ToString("N"), owner)
        {
            IsUTC = true;
            DateTime = DateTime.UtcNow;
            Comment = string.Empty;
            Tag = (owner as ITag)?.Tag;

            this.GridInfo = CSharpKit.GridInfo.Empty;
            this.ContourInfo = new ContourInfo();
        }

        /// <summary>
        /// 魔术标识
        /// </summary>
        public Int32 MagicID { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public String Comment { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 名称：数据代码<br/>
        /// 备注：
        /// </summary>
        public int DataCode { get; set; }

        /// <summary>
        /// 数据范围
        /// </summary>
        public IExtent Extent { get; set; }

        /// <summary>
        /// 网格信息
        /// </summary>
        public IGridInfo GridInfo { get; set; }

        /// <summary>
        /// 等值线信息
        /// </summary>
        public IContourInfo ContourInfo { get; set; }




        /// <summary>
        /// 档案级别
        /// </summary>
        public ArchiveLevel ArchiveLevel { get; set; }

        /// <summary>
        /// 协调世界时 Universal Time Coordinated
        /// </summary>
        public bool IsUTC { get; set; }


        /// <summary>
        /// 标记
        /// </summary>
        public Int32 Flag { get; set; }

        /// <summary>
        /// 无效数据
        /// </summary>
        public Int32 InvalidData { get; set; }

        /// <summary>
        /// 是无效数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Boolean IsInvalidData(Double value)
        {
            return false
                || Math.Abs(value - this.InvalidData) < 1.0e-12 // 指定的无效值
                //|| Math.Abs(value - KitConstants.InvalidData) < 1.0e-12 // -9999
                ;
        }

        /// <summary>
        /// 是特定数据
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Boolean IsSpecialData(Double value)
        {
            return false;
        }


        //}}@@@
    }


}
