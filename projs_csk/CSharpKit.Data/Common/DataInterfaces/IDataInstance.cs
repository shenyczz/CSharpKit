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
    /// IDataInstance - 数据实例接口
    /// </summary>
    public interface IDataInstance : IOwner, IDisposable
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// 数据算法
        /// </summary>
        IAlgorithm Algorithm { get; }

        /// <summary>
        /// 数据信息
        /// </summary>
        IDataInfo DataInfo { get; }

        /// <summary>
        /// 数据处理器
        /// </summary>
        IProcessor Processor { get; }

        /// <summary>
        /// 数据图例
        /// </summary>
        IDataLegend Legend { get; }

        /// <summary>
        /// 调色板
        /// </summary>
        IPalette Palette { get; set; }


        /// <summary>
        /// 源数据（从文件读取的数据）
        /// </summary>
        object OriginsData { get; set; }

        /// <summary>
        /// 源数据图像（主图像）
        /// </summary>
        object OriginsImage { get; set; }

        /// <summary>
        /// 产品数据（所有由Processor生成的最终数据）
        /// </summary>
        object ProductData { get; set; }

        /// <summary>
        /// 产品格点数据（所有由Processor生成的最终数据）
        /// </summary>
        object ProductGridData { get; set; }

        /// <summary>
        /// 产品图像（辅助图像）
        /// </summary>
        object ProductImageOld { get; set; }

        /// <summary>
        ///  产品图像
        /// </summary>
        Dictionary<int, Image> ProductImage { get; }

        // 取得产品数据
        double GetProductValue(double lon, double lat);

        //}}@@@
    }


}
