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
    /// 目标
    /// </summary>
    public interface ITarget : IOwner, ITag
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        String Uid { get; }

        /// <summary>
        /// 标识
        /// </summary>
        String Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        String Name { get; set; }


        //@@@
    }


}
