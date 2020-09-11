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

namespace CSharpKit.GeoApi.CoordinateSystems
{
    public interface IWgs84ConversionInfo : IEquatable<IWgs84ConversionInfo>
    {
        /// <summary>
        /// Bursa Wolf shift in meters .
        /// </summary>
        double Dx { get; set; }
        /// <summary>
        /// Bursa Wolf shift in meters .
        /// </summary>
        double Dy { get; set; }
        /// <summary>
        /// Bursa Wolf shift in meters .
        /// </summary>
        double Dz { get; set; }
        /// <summary>
        /// Bursa Wolf rotation（旋转） in arc seconds（以弧秒为单位）.
        /// </summary>
        double Ex { get; set; }
        /// <summary>
        /// Bursa Wolf rotation in arc seconds.
        /// </summary>
        double Ey { get; set; }
        /// <summary>
        /// Bursa Wolf rotation in arc seconds.
        /// </summary>
        double Ez { get; set; }
        /// <summary>
        /// Bursa Wolf scaling in parts per million（按百万分之一比例缩放）.
        /// </summary>
        double Ppm { get; set; }

        /// <summary>
        /// true if all 7 parameter(Dx,Dy,Dz,Ex,Ey,Ez) values are 0.0
        /// </summary>
        bool HasZeroValuesOnly { get; }


        /// <summary>
        /// Human readable text describing intended region of transformation.
        /// </summary>
        string AreaOfUse { get; set; }
        /// <summary>
        /// the Well Known Text (WKT) for this object.
        /// </summary>
        /// <remarks>
        /// The WKT format of this object is: <code>TOWGS84[dx, dy, dz, ex, ey, ez, ppm]</code>
        /// </remarks>
        string WKT { get; }
        /// <summary>
        /// Gets an XML representation of this object
        /// </summary>
        string XML { get; }
    }




}
