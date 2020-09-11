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

namespace CSharpKit.ComponentModel
{
    /// <summary>
    /// INotifyCustomPropertyChanged - 向客户端发出某一自定义属性值已更改的通知
    /// </summary>
    public interface INotifyCustomPropertyChanged
    {
        /// <summary>
        /// 事件 - 自定义属性值更改
        /// </summary>
        event EventHandler<CustomPropertyChangedEventArgs> CustomPropertyChanged;
    }
}
