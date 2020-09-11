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

namespace CSharpKit.GeoApi.CoordinateSystems.Transformations
{
	/// <summary>
	/// Flags indicating parts of domain covered by a convex hull(凸包覆盖的部分区域). 
	/// </summary>
	/// <remarks>
	/// These flags can be combined. For example, the value 3 
	/// corresponds to a combination of <see cref="Inside"/> and <see cref="Outside"/>,
	/// which means that some parts of the convex hull are inside the 
	/// domain, and some parts of the convex hull are outside the domain.
	/// </remarks>
	public enum DomainFlags : int
	{
		/// <summary>
		/// At least one point in a convex hull is inside the transform's domain.
		/// 凸包中至少有一点在变换的域内。
		/// </summary>
		Inside = 1,

		/// <summary>
		/// At least one point in a convex hull is outside the transform's domain.
		/// </summary>
		Outside = 2,

		/// <summary>
		/// At least one point in a convex hull is not transformed continuously.
		/// 凸壳中至少有一点不连续变换。
		/// </summary>
		/// <remarks>
		/// As an example, consider a "Longitude_Rotation" transform which adjusts 
		/// longitude coordinates to take account of a change in Prime Meridian. If
		/// the rotation is 5 degrees east, then the point (Lat=175,Lon=0) is not 
		/// transformed continuously, since it is on the meridian line which will 
		/// be split at +180/-180 degrees.
		/// </remarks>
		Discontinuous = 4
	}

}
