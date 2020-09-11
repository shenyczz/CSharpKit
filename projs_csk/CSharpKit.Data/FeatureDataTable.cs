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
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using CSharpKit.Palettes;

namespace CSharpKit.Data
{
    /// <summary>
    /// FeatureDataTable - 专题数据表
    /// </summary>
    public class FeatureDataTable : DataTable, IDataInstance, IEnumerable
    {
        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public FeatureDataTable()
            : base() { }

        /// <summary>
        /// 参数构造函数 - 用指定的表名和命名空间初始化
        /// </summary>
        /// <param name="tableName">表的名称。
        /// 如果 tableName 为 null 或是空字符串,则在添加到 FeatureDataTableCollection 中时指定默认名称</param>
        public FeatureDataTable(string tableName)
            : base(tableName) { }

        /// <summary>
        /// 参数构造函数 - 用指定的表名和命名空间初始化
        /// </summary>
        /// <param name="tableName">表的名称。
        /// 如果 tableName 为 null 或是空字符串,则在添加到 FeatureDataTableCollection 中时指定默认名称</param>
        /// <param name="tableNamespace">存储在 FeatureDataTable 中的数据的 XML 表示形式的命名空间。</param>
        public FeatureDataTable(String tableName, String tableNamespace)
            : base(tableName, tableNamespace) { }

        #endregion

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return base.Rows.GetEnumerator();
        }

        #endregion

        #region IDataInstnce

        /// <summary>
        /// 拥有者
        /// </summary>
        public Object Owner { get; set; }

        /// <summary>
        /// 连接字符串(或文件名)
        /// </summary>
        public String ConnectionString { get; protected set; }

        /// <summary>
        /// 数据算法
        /// </summary>
        public IAlgorithm Algorithm { get; protected set; }

        /// <summary>
        /// 数据信息
        /// </summary>
        public IDataInfo DataInfo { get; protected set; }

        /// <summary>
        /// 数据处理器
        /// </summary>
        public IProcessor Processor { get; protected set; }

        /// <summary>
        /// 数据图例
        /// </summary>
        public IDataLegend Legend { get; protected set; }

        /// <summary>
        /// 调色板
        /// </summary>
        public IPalette Palette { get; set; }

        /// <summary>
        /// 源数据:OriginsData
        /// </summary>
        public object OriginsData { get; set; }

        /// <summary>
        /// 主图像
        /// </summary>
        public object OriginsImage { get; set; }

        /// <summary>
        /// 产品数据
        /// </summary>
        public object ProductData { get; set; }

        /// <summary>
        /// 产品数据
        /// </summary>
        public object ProductGridData { get; set; }

        /// <summary>
        /// 产品图像（辅助图像）
        /// </summary>
        public object ProductImageOld { get; set; }

        public Dictionary<int, Image> ProductImage { get; set; }

        public double GetProductValue(double lon, double lat)
        {
            return double.NaN;
        }

        #endregion

        //}}@@@
    }

}
