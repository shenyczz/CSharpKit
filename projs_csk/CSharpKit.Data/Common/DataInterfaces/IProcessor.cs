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
    /// IDataProcessor - 数据处理器接口
    /// </summary>
    public interface IProcessor : IOwner
    {
        /// <summary>
        /// 是否装载
        /// </summary>
        Boolean IsLoad { get; set; }

        /// <summary>
        /// 装载
        /// </summary>
        Boolean Load();

        /// <summary>
        /// 保存
        /// </summary>
        Boolean Save();

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Boolean SaveAs(String fileName);
    }


}
