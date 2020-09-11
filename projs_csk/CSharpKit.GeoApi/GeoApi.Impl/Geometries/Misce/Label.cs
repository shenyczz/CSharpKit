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
    /// <summary>
    /// Text
    /// </summary>
    public class Label : Geometry
    {
        /// <summary>
        /// 使图形偏移指定的量
        /// </summary>
        /// <param name="dx">X坐标的偏移量</param>
        /// <param name="dy">Y坐标的偏移量</param>
        public override void Offset(Double dx, Double dy)
        {
            Location.Offset(dx, dy);
            base.Offset(dx, dy);
        }

        //@@@
    }
























}
