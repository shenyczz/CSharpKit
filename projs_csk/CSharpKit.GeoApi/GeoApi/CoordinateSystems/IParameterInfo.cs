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
    /// The IParameterInfo interface provides an interface through which clients of a
    /// Projected Coordinate System or of a Projection can set the parameters of the
    /// projection. It provides a generic interface for discovering the names and default
    /// values of parameters, and for setting and getting parameter values. Subclasses of
    /// this interface may provide projection specific parameter access methods.
    /// </summary>
    public interface IParameterInfo
	{
		/// <summary>
		/// Gets the number of parameters expected.
		/// </summary>
		int NumParameters { get; }

		/// <summary>
		/// Returns the default parameters for this projection.
		/// </summary>
		/// <returns></returns>
		IParameter[] DefaultParameters();

		/// <summary>
		/// Gets or sets the parameters set for this projection.
		/// </summary>
		List<IParameter> Parameters { get; set; }

		/// <summary>
		/// Gets the parameter by its name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		IParameter GetParameterByName(string name);
	}

}
