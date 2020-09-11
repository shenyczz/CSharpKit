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
    /// IEllipsoid - 椭球体
    /// The IEllipsoid interface defines the standard information stored with ellipsoid objects.
    /// </summary>
    public interface IEllipsoid : ISpatialReferenceInfo
	{
		/// <summary>
		/// Gets or sets the value of the semi-major axis.
		/// </summary>
		double SemiMajorAxis { get; set; }

		/// <summary>
		/// Gets or sets the value of the semi-minor axis.
		/// </summary>
		double SemiMinorAxis { get; set; }

		/// <summary>
		/// Gets or sets the value of the inverse of the flattening constant of the ellipsoid.
		/// </summary>
		double InverseFlattening { get; set; }

		/// <summary>
		/// Gets or sets the value of the axis unit.
		/// </summary>
		ILinearUnit AxisUnit { get; set; }

		/// <summary>
		/// Is the Inverse Flattening definitive for this ellipsoid? Some ellipsoids use the
		/// IVF as the defining value, and calculate the polar radius whenever asked. Other
		/// ellipsoids use the polar radius to calculate the IVF whenever asked. This
		/// distinction can be important to avoid floating-point rounding errors.
		/// </summary>
		bool IsIvfDefinitive { get; set; }
	}

}
