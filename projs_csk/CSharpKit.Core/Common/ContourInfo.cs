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
    /// ContourInfo - 等值线信息
    /// </summary>
    public sealed class ContourInfo : IContourInfo
    {
        public ContourInfo()
        {
            // 最多99组等值线
            ContourValues = new Double[MaxContourNums];
        }

        public const int MaxContourNums = 99;

        #region Properties

        /// <summary>
        /// 等值线数量(CID)
        /// </summary>
        public Int32 ContourNums { get; set; }

        /// <summary>
        /// 自动规划等值线时的份数
        /// </summary>
        public Int32 ContourFraction { get; set; }

        /// <summary>
        /// 等值线间隔
        /// </summary>
        public Double ContourInterval
        {
            get { return ContourValues[0]; }
            set { ContourValues[0] = value; }
        }

        /// <summary>
        /// 等值线最小值
        /// </summary>
        public Double ContourMin
        {
            get { return ContourValues[1]; }
            set { ContourValues[1] = value; }
        }

        /// <summary>
        /// 等值线最大值
        /// </summary>
        public Double ContourMax
        {
            get { return ContourValues[2]; }
            set { ContourValues[2] = value; }
        }

        /// <summary>
        /// 加粗显示的等值线值
        /// </summary>
        public Double ContourBoldValue { get; set; }

        /// <summary>
        /// 等值线值
        /// </summary>
        public Double[] ContourValues { get; set; }

        #endregion


        //@EndOf(ContourInfo)
    }


}
