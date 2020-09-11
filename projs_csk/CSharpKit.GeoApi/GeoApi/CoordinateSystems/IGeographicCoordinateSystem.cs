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
    /// IGeographicCoordinateSystem - 地理坐标系
    /// The IGeographicCoordinateSystem interface is a subclass of IGeodeticSpatialReference and
    /// defines the standard information stored with geographic coordinate system objects.
    /// </summary>
    public interface IGeographicCoordinateSystem : IHorizontalCoordinateSystem
    {
        /// <summary>
        /// Gets or sets the angular units of the geographic coordinate system.
        /// </summary>
        IAngularUnit AngularUnit { get; set; }

        /// <summary>
        /// Gets or sets the prime meridian of the geographic coordinate system.
        /// </summary>
        IPrimeMeridian PrimeMeridian { get; set; }

        /// <summary>
        /// Gets the number of available conversions to WGS84 coordinates.
        /// </summary>
        int NumConversionToWGS84 { get; }

        /// <summary>
        /// Gets details on a conversion to WGS84.
        /// </summary>
        IWgs84ConversionInfo GetWgs84ConversionInfo(int index);
    }

}
