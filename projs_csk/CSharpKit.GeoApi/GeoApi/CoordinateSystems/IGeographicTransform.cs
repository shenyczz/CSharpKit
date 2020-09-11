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

using System.Collections.Generic;

namespace CSharpKit.GeoApi.CoordinateSystems
{
	/// <summary>
	/// The IGeographicTransform interface is implemented on geographic transformation
	/// objects and implements datum transformations between geographic coordinate systems.
	/// </summary>
	public interface IGeographicTransform// : IInfo
	{
		/// <summary>
		/// Gets or sets source geographic coordinate system for the transformation.
		/// </summary>
		IGeographicCoordinateSystem SourceGCS { get; set; }

		/// <summary>
		/// Gets or sets the target geographic coordinate system for the transformation.
		/// </summary>
		IGeographicCoordinateSystem TargetGCS { get; set; }

		/// <summary>
		/// Returns an accessor interface to the parameters for this geographic transformation.
		/// </summary>
		IParameterInfo ParameterInfo { get; }

		/// <summary>
		/// Transforms an array of points from the source geographic coordinate system
		/// to the target geographic coordinate system.
		/// </summary>
		/// <param name="points">Points in the source geographic coordinate system</param>
		/// <returns>Points in the target geographic coordinate system</returns>
		List<double[]> Forward(List<double[]> points);

		/// <summary>
		/// Transforms an array of points from the target geographic coordinate system
		/// to the source geographic coordinate system.
		/// </summary>
		/// <param name="points">Points in the target geographic coordinate system</param>
		/// <returns>Points in the source geographic coordinate system</returns>
		List<double[]> Inverse(List<double[]> points);
	}

}
