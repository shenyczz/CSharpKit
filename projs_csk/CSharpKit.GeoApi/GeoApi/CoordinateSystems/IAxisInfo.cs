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
    /// This is used to label axes, and indicate the orientation.
    /// </summary>
    public interface IAxisInfo
	{
		/// <summary>
		/// Human readable name for axis. Possible values are X, Y, Long, Lat or any other short string.
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Orientation of axis.
		/// </summary>
		AxisOrientations Orientation { get; set; }
		/// <summary>
		/// Returns the Well-known text for this object
		/// as defined in the simple features specification.
		/// </summary>
		string WKT { get; }
		/// <summary>
		/// Gets an XML representation of this object
		/// </summary>
		string XML { get; }
	}

}
