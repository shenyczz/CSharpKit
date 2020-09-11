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
using System.Collections.Generic;
using System.Drawing;
using CSharpKit.Palettes;

namespace CSharpKit.Data
{
    /// <summary>
    /// DataInstance - 数据实例
    /// </summary>
    public abstract class DataInstance : Target, IDataInstance
    {
        protected DataInstance(IProvider owner)
            : base(owner)
        {
            try
            {
                this.Tag = (owner as ITag)?.Tag;
                this.ConnectionString = owner.ConnectionString;
                this.ProductImage = new Dictionary<int, Image>();
                this.Initialize();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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
        /// 调色板
        /// </summary>
        public IPalette Palette { get; set; }

        /// <summary>
        /// 数据图例
        /// </summary>
        public IDataLegend Legend { get; protected set; }


        /// <summary>
        /// 源数据
        /// </summary>
        public object OriginsData { get; set; }

        /// <summary>
        /// 源图像
        /// </summary>
        public object OriginsImage { get; set; }

        /// <summary>
        /// 产品数据
        /// </summary>
        public object ProductData { get; set; }

        /// <summary>
        /// 产品格点数据
        /// </summary>
        public object ProductGridData { get; set; }

        /// <summary>
        /// 产品图像（辅助图像）
        /// </summary>
        public object ProductImageOld { get; set; }

        /// <summary>
        /// 产品图像（替代 ProductImageOld）
        /// </summary>
        public Dictionary<int, Image> ProductImage { get; private set; }

        /// <summary>
        /// 取得产品格点要素值
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public virtual double GetProductValue(double lon, double lat)
        {
            return double.NaN;
        }


        /// <summary>
        /// 初始化
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// TODO:构建图例 移到 Processor
        /// </summary>
        /// <param name="iDataCode"></param>
        public abstract void BuildLegend(int iDataCode);


        //}}@@@
    }


}
