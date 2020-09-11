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

namespace CSharpKit
{
    /// <summary>
    /// IContourInfo - 等值线信息
    /// </summary>
    public interface IContourInfo
    {
        /// <summary>
        /// 等值线数量(CID)
        /// </summary>
        int ContourNums { get; set; }
        /// <summary>
        /// 自动规划等值线时的份数
        /// </summary>
        int ContourFraction { get; set; }

        /// <summary>
        /// 等值线间隔
        /// </summary>
        double ContourInterval { get; set; }
        /// <summary>
        /// 等值线最小值
        /// </summary>
        double ContourMax { get; set; }
        /// <summary>
        /// 等值线最大值
        /// </summary>
        double ContourMin { get; set; }
        /// <summary>
        /// 加粗显示的等值线值
        /// </summary>
        Double ContourBoldValue { get; set; }
        /// <summary>
        /// 等值线值
        /// </summary>
        double[] ContourValues { get; set; }
    }
}