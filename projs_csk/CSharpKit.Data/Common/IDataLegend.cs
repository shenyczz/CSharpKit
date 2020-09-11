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
    /// 数据图例接口
    /// </summary>
    public interface IDataLegend : IColorTranformer<double>
    {

        int DataCode { get; }

        int DataLevel { get; }

        Int32 Background { get; set; }

        String Unit { get; set; }

        ILegendElement this[int index] { get; }

        bool UseIndexColor { get; set; }


        /// <summary>
        /// 取得颜色索引
        /// </summary>
        /// <param name="dValue"></param>
        /// <returns></returns>
        int GetColorIndex(double dValue);
        /// <summary>
        /// 取得索引颜色
        /// </summary>
        /// <param name="dValue"></param>
        /// <returns></returns>
        int GetIndexColor(double dValue);
        /// <summary>
        /// 取得线性插值颜色
        /// </summary>
        /// <param name="dValue"></param>
        /// <returns></returns>
        int GetLinearColor(double dValue);

        //@EndOf(IDataLegend)
    }

}
