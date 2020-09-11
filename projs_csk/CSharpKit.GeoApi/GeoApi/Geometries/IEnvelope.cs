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

namespace CSharpKit.GeoApi.Geometries
{
    public interface IEnvelope
    {
        /// <summary>
        /// 最小 X 坐标
        /// </summary>
        Double MinX { get; }

        /// <summary>
        /// 最小 Y 坐标
        /// </summary>
        Double MinY { get; }

        /// <summary>
        /// 最大 X 坐标
        /// </summary>
        Double MaxX { get; }

        /// <summary>
        /// 最大 Y 坐标
        /// </summary>
        Double MaxY { get; }

        /// <summary>
        /// 面积
        /// </summary>
        Double Area { get; }

        /// <summary>
        /// 宽度
        /// </summary>
        Double Width { get; }

        /// <summary>
        /// 高度
        /// </summary>
        Double Height { get; }

        /// <summary>
        /// 是否空
        /// </summary>
        Boolean IsNull { get; }

        //}}@@@
    }







}
