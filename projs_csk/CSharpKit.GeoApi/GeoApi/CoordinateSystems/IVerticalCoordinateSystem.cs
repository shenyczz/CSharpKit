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

namespace CSharpKit.GeoApi.CoordinateSystems
{
    /// <summary>
    /// IVerticalCoordinateSystem - 垂直坐标系
    /// A one-dimensional coordinate system suitable for vertical measurements.
    /// </summary>
    public interface IVerticalCoordinateSystem : ICoordinateSystem
    {
        /// <summary>
        /// Gets the vertical datum, which indicates the measurement method
        /// </summary>
        IVerticalDatum VerticalDatum { get; set; }

        /// <summary>
        /// Gets the units used along the vertical axis.
        /// </summary>
        ILinearUnit VerticalUnit { get; set; }
    }

}
