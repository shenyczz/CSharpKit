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
    /// IProvider - 数据提供者接口
    /// </summary>
    public interface IProvider : ITarget, ITag, IDisposable
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        String ConnectionString { get; set; }

        /// <summary>
        /// 数据实例
        /// </summary>
        IDataInstance DataInstance { get; }

        /// <summary>
        /// 是否打开
        /// </summary>
        Boolean IsOpen { get; }

        /// <summary>
        /// 打开
        /// </summary>
        /// <returns></returns>
        void Open();

        /// <summary>
        /// 带参数打开
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tag"></param>
        void Open(String connectionString, Object tag);

        /// <summary>
        /// 关闭
        /// </summary>
        void Close();
    }


}
