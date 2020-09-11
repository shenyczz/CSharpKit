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
    /// IPrimeMeridian - 本初子午线
    /// The IPrimeMeridian interface defines the standard information stored with prime
    /// meridian objects. Any prime meridian object must implement this interface as
    /// well as the ISpatialReferenceInfo interface.
    /// </summary>
    public interface IPrimeMeridian : ISpatialReferenceInfo
	{
		/// <summary>
		/// Gets or sets the longitude of the prime meridian (relative to the Greenwich prime meridian).
		/// </summary>
		double Longitude { get; set; }

		/// <summary>
		/// Gets or sets the AngularUnits.
		/// </summary>
		IAngularUnit AngularUnit { get; set; }
	}

}
