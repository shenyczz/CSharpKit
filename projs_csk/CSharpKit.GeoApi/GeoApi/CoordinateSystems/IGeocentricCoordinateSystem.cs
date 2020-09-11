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
    /// IGeocentricCoordinateSystem - 地心坐标系。
    /// A 3D coordinate system, with its origin at the center of the Earth.
    /// </summary>
    public interface IGeocentricCoordinateSystem : ICoordinateSystem
	{
		/// <summary>
		/// Returns the HorizontalDatum. The horizontal datum is used to determine where
		/// the centre of the Earth is considered to be. All coordinate points will be 
		/// measured from the centre of the Earth, and not the surface.
		/// </summary>
		IHorizontalDatum HorizontalDatum { get; set; }

		/// <summary>
		/// Gets the units used along all the axes.
		/// </summary>
		ILinearUnit LinearUnit { get; set; }

		/// <summary>
		/// Returns the PrimeMeridian.
		/// </summary>
		IPrimeMeridian PrimeMeridian { get; set; }
	}


}
