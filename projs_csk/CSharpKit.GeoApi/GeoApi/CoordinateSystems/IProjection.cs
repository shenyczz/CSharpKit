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
	/// The IProjection interface defines the standard information stored with projection
	/// objects. A projection object implements a coordinate transformation from a geographic
	/// coordinate system to a projected coordinate system, given the ellipsoid for the
	/// geographic coordinate system. It is expected that each coordinate transformation of
	/// interest, e.g., Transverse Mercator, Lambert, will be implemented as a COM class of
	/// coType Projection, supporting the IProjection interface.
	/// </summary>
	public interface IProjection : ISpatialReferenceInfo
	{
		/// <summary>
		/// Gets number of parameters of the projection.
		/// </summary>
		int NumParameters { get; }

		/// <summary>
		/// Gets the projection classification name (e.g. 'Transverse_Mercator').
		/// </summary>
		string ClassName { get; }

		/// <summary>
		/// Gets an indexed parameter of the projection.
		/// </summary>
		/// <param name="index">Index of parameter</param>
		/// <returns>n'th parameter</returns>
		IProjectionParameter GetParameter(int index);

		/// <summary>
		/// Gets an named parameter of the projection.
		/// </summary>
		/// <remarks>The parameter name is case insensitive</remarks>
		/// <param name="name">Name of parameter</param>
		/// <returns>parameter or null if not found</returns>
		IProjectionParameter GetParameter(string name);
	}

}
