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

namespace CSharpKit.GeoApi.CoordinateSystems
{

	public class Parameter : IParameter
	{
		public Parameter(string name, double value)
		{
			Name = name;
			Value = value;
		}


		/// <summary>
		/// Parameter name
		/// </summary>
		public string Name { get; set; }


		/// <summary>
		/// Parameter value
		/// </summary>
		public double Value { get; set; }


		//@EndOf(Parameter)
	}


}
