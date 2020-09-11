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
	public interface ICoordinateTransformationCore
    {
		/// <summary>
		/// Source coordinate system.
		/// </summary>
		ICoordinateSystem SourceCS { get; }

		/// <summary>
		/// Target coordinate system.
		/// </summary>
		ICoordinateSystem TargetCS { get; }
	}


	/// <summary>
	/// Describes a coordinate transformation. This interface only describes a 
	/// coordinate transformation, it does not actually perform the transform 
	/// operation on points. To transform points you must use a math transform.
	/// </summary>
	public interface ICoordinateTransformation : ICoordinateTransformationCore
	{
		/// <summary>
		/// Human readable description of domain in source coordinate system.
		/// </summary>
		string AreaOfUse { get; }

		/// <summary>
		/// Name of transformation.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Authority which defined transformation and parameter values.
		/// </summary>
		/// <remarks>
		/// An Authority is an organization that maintains definitions of Authority Codes. For example the European Petroleum Survey Group (EPSG) maintains a database of coordinate systems, and other spatial referencing objects, where each object has a code number ID. For example, the EPSG code for a WGS84 Lat/Lon coordinate system is ?326?
		/// </remarks>
		string Authority { get; }

		/// <summary>
		/// Code used by authority to identify transformation. An empty string is used for no code.
		/// </summary>
		/// <remarks>The AuthorityCode is a compact string defined by an Authority to reference a particular spatial reference object. For example, the European Survey Group (EPSG) authority uses 32 bit integers to reference coordinate systems, so all their code strings will consist of a few digits. The EPSG code for WGS84 Lat/Lon is ?326?</remarks>
		long AuthorityCode { get; }

		/// <summary>
		/// Gets the provider-supplied remarks.
		/// </summary>
		string Remarks { get; }


		/// <summary>
		/// Source coordinate system.
		/// </summary>
		//ICoordinateSystem SourceCS { get; }

		/// <summary>
		/// Target coordinate system.
		/// </summary>
		//ICoordinateSystem TargetCS { get; }


		/// <summary>
		/// Gets math transform.
		/// </summary>
		IMathTransform MathTransform { get; }

		/// <summary>
		/// Semantic type of transform. For example, a datum transformation or a coordinate conversion.
		/// </summary>
		TransformType TransformType { get; }
	}


}
