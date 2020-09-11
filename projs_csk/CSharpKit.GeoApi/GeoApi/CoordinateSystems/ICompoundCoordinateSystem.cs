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
    /// ICompoundCoordinateSystem - 复合坐标系
    /// An aggregate of two coordinate systems (CRS). One of these is usually a 
    /// CRS based on a two dimensional coordinate system such as a geographic or
    /// a projected coordinate system with a horizontal datum. The other is a 
    /// vertical CRS which is a one-dimensional coordinate system with a vertical
    /// datum.
    /// </summary>
    public interface ICompoundCoordinateSystem : ICoordinateSystem
	{
		/// <summary>
		/// Gets first sub-coordinate system.
		/// </summary>
		ICoordinateSystem HeadCS { get; }

		/// <summary>
		/// Gets second sub-coordinate system.
		/// </summary>
		ICoordinateSystem TailCS { get; }
	}


}
