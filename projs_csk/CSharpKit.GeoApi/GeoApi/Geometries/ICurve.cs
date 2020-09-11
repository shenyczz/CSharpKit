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
    /// 泛型曲线接口<br/>
    /// <seealso cref="ILinearString" />
    /// </summary>
    public interface ICurve : IGeometry
    {
        //ICoordinateSequence CoordinateSequence { get; }

        /// <summary>
        /// Gets a value indicating the start point of the curve
        /// </summary>
        IPoint StartPoint { get; }

        /// <summary>
        /// Gets a value indicating the end point of the curve
        /// </summary>
        IPoint EndPoint { get; }

        /// <summary>
        /// Gets a value indicating that the curve is closed. 
        /// In this case <see cref="StartPoint"/> an <see cref="EndPoint"/> are equal.
        /// </summary>
        bool IsClosed { get; }

        /// <summary>
        /// Gets a value indicating that the curve is a ring. 
        /// </summary>
        bool IsRing { get; }

        //@EndOf(ICurve)
    }





}
