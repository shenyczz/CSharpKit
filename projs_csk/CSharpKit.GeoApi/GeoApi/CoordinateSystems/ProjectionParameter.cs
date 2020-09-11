/******************************************************************************
 * 
 * Announce: CSharp Kit, used to achieve data visualization.
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

using System;
using System.Globalization;

namespace CSharpKit.GeoApi.CoordinateSystems
{

	public class ProjectionParameter : Parameter, IProjectionParameter
	{
		/// <summary>
		/// Initializes an instance of a ProjectionParameter
		/// </summary>
		/// <param name="name">Name of parameter</param>
		/// <param name="value">Parameter value</param>
		public ProjectionParameter(string name, double value)
			: base(name, value) { }

		/// <summary>
		/// Returns the Well-known text for this object
		/// as defined in the simple features specification.
		/// </summary>
		public string WKT
		{
			get
			{
				return String.Format(CultureInfo.InvariantCulture.NumberFormat, "PARAMETER[\"{0}\", {1}]", Name, Value);
			}
		}

		/// <summary>
		/// Gets an XML representation of this object
		/// </summary>
		public string XML
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture.NumberFormat, "<CS_ProjectionParameter Name=\"{0}\" Value=\"{1}\"/>", Name, Value);
			}
		}

		/// <summary>
		/// Function to get a textual representation of this envelope
		/// </summary>
		/// <returns>A textual representation of this envelope</returns>
		public override string ToString()
		{
			return string.Format("ProjectionParameter '{0}': {1}", Name, Value);
		}

		//@EndOf(ProjectionParameter)
	}



}
