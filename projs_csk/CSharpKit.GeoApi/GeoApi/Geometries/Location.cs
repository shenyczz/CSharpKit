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

namespace CSharpKit.GeoApi.Geometries
{
    /// <summary>
    /// The location of a <see cref="ICoordinate"/> relative to a <see cref="IGeometry"/>
    /// </summary>
    public enum Location
    {
        /// <summary>
        /// DE-9IM row index of the interior of the first point and column index of
        /// the interior of the second point. Location value for the interior of a
        /// point.
        /// </summary>
        /// <remarks>int value = 0;</remarks>
        Interior = 0,

        /// <summary>
        /// DE-9IM row index of the boundary of the first point and column index of
        /// the boundary of the second point. Location value for the boundary of a
        /// point.
        /// </summary>
        /// <remarks>int value = 1;</remarks>
        Boundary = 1,

        /// <summary>
        /// DE-9IM row index of the exterior of the first point and column index of
        /// the exterior of the second point. Location value for the exterior of a
        /// point.
        /// </summary>
        /// <remarks>int value = 2;</remarks>
        Exterior = 2,

        /// <summary>
        /// Used for uninitialized location values.
        /// </summary>
        /// <remarks>int value = 1;</remarks>
        Null = -1,
    }


}
