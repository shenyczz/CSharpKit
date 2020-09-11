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

using CSharpKit.GeoApi.CoordinateSystems.Transformations;

namespace CSharpKit.GeoApi.CoordinateSystems
{
    /// <summary>
    /// IFittedCoordinateSystem - 适体坐标系 （aptamer coordinate system？）
    /// A coordinate system which sits inside another coordinate system. The fitted 
    /// coordinate system can be rotated and shifted, or use any other math transform
    /// to inject itself into the base coordinate system.
    /// </summary>
    public interface IFittedCoordinateSystem : ICoordinateSystem
	{
		/// <summary>
		/// Gets underlying coordinate system.
		/// </summary>
		ICoordinateSystem BaseCoordinateSystem { get; }

		IMathTransform ToBaseTransform { get; }

		/// <summary>
		/// Gets Well-Known Text of a math transform to the base coordinate system. 
		/// The dimension of this fitted coordinate system is determined by the source 
		/// dimension of the math transform. The transform should be one-to-one within 
		/// this coordinate system's domain, and the base coordinate system dimension 
		/// must be at least as big as the dimension of this coordinate system.
		/// </summary>
		/// <returns></returns>
		string ToBase();


		//@EndOf(IFittedCoordinateSystem)
	}


}
