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
    /// Procedure used to measure positions on the surface of the Earth.
    /// </summary>
    public interface IHorizontalDatum : IDatum
    {
        /// <summary>
        /// Gets or sets the ellipsoid of the datum.
        /// </summary>
        IEllipsoid Ellipsoid { get; set; }

        /// <summary>
        /// Gets preferred parameters for a Bursa Wolf transformation into WGS84. The 7 returned values 
        /// correspond to (dx,dy,dz) in meters, (ex,ey,ez) in arc-seconds, and scaling in parts-per-million.
        /// </summary>
        IWgs84ConversionInfo Wgs84Parameters { get; set; }
    }



}
