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
    public abstract class XFileDataInfo : DataInfo
    {
        protected XFileDataInfo(IDataInstance owner)
            : base(owner) { }

        /// <summary>
        /// 文件标识
        /// </summary>
        public object FileId { get; set; }

        /// <summary>
        /// 基本值
        /// </summary>
        public Double BaseValue { get; set; }

        /// <summary>
        /// 缩放因子
        /// </summary>
        public Double Scale { get; set; }

        /// <summary>
        /// 格式代码
        /// </summary>
        public Int32 FormatCode { get; set; }

        /// <summary>
        /// 要素代码
        /// </summary>
        public Int32 ElementCode { get; set; }

        /// <summary>
        /// 平均值
        /// </summary>
        public Double AverageValue { get; set; }

        /// <summary>
        /// 极小值
        /// </summary>
        public Double ExtremumValueMin { get; set; }

        /// <summary>
        /// 极大值
        /// </summary>
        public Double ExtremumValueMax { get; set; }


        //}}@@@
    }



}
