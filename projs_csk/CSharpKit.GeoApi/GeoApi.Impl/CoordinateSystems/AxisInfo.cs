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

using System;
using System.Globalization;

namespace CSharpKit.GeoApi.CoordinateSystems
{
	/// <summary>
	/// AxisInfo - 轴信息
	/// Details of axis. This is used to label axes, and indicate the orientation.
	/// </summary>
	public class AxisInfo : IAxisInfo
	{
		/// <summary>
		/// Initializes a new instance of an AxisInfo.
		/// </summary>
		/// <param name="name">Name of axis</param>
		/// <param name="orientation">Axis orientation</param>
		public AxisInfo(string name, AxisOrientations orientation)
		{
			Name = name;
			Orientation = orientation;
		}

		/// <summary>
		/// Human readable name for axis. Possible values are X, Y, Long, Lat or any other short string.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets enumerated value for orientation.
		/// </summary>
		public AxisOrientations Orientation { get; set; }

		/// <summary>
		/// Returns the Well-known text for this object
		/// as defined in the simple features specification.
		/// </summary>
		public string WKT
		{
			get
			{
				return String.Format("AXIS[\"{0}\", {1}]"
					, Name
					, Orientation.ToString().ToUpper(CultureInfo.InvariantCulture));
			}
		}

		/// <summary>
		/// Gets an XML representation of this object
		/// </summary>
		public string XML
		{
			get
			{
				return String.Format(CultureInfo.InvariantCulture.NumberFormat,
					"<CS_AxisInfo Name=\"{0}\" Orientation=\"{1}\"/>"
					, Name
					, Orientation.ToString().ToUpper(CultureInfo.InvariantCulture));
			}
		}

		//@@@
	}

}
