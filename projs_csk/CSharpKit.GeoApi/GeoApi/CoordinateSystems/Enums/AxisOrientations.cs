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
	/// AxisOrientations - 轴的方向
	/// Orientation of axis. Some coordinate systems use non-standard orientations. 
	/// For example, the first axis in South African grids usually points West, 
	/// instead of East. This information is obviously relevant for algorithms
	/// converting South African grid coordinates into Lat/Long.
	/// </summary>
	public enum AxisOrientations : short
	{
		/// <summary>
		/// Unknown or unspecified axis orientation.
		/// This can be used for local or fitted coordinate systems.
		/// </summary>
		Other = 0,

		/// <summary>
		/// Increasing ordinates values go North. This is usually used for Grid Y coordinates and Latitude.
		/// </summary>
		North = 1,

		/// <summary>
		/// Increasing ordinates values go South. This is rarely used.
		/// </summary>
		South = 2,

		/// <summary>
		/// Increasing ordinates values go East. This is rarely used.
		/// </summary>
		East = 3,

		/// <summary>
		/// Increasing ordinates values go West. This is usually used for Grid X coordinates and Longitude.
		/// </summary>
		West = 4,

		/// <summary>
		/// Increasing ordinates values go up. This is used for vertical coordinate systems.
		/// </summary>
		Up = 5,

		/// <summary>
		/// Increasing ordinates values go down. This is used for vertical coordinate systems.
		/// </summary>
		Down = 6
	}


}
