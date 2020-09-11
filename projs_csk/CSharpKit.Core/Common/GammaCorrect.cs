/******************************************************************************
 * 
 * Announce: Meteorological Toolkit（MTK）.
 *           Copyright (C) ShenYongchen.
 *           All rights reserved.
 *   Author: 申永辰.郑州 (shenyczz@163.com)
 *  WebSite: http://github.com/shenyczz/meteoToolkit
 *
 * THIS CODE IS LICENSED UNDER THE MIT LICENSE (MIT).
 * THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF 
 * ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
 * IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
 * PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
 * 
******************************************************************************/

using System;
using System.Diagnostics;

namespace CSharpKit
{
	/// <summary>
	/// 数据伽马校正 f=(I)^r
	/// GammaCorrect => GMC
	/// GammaAdjust => GMA gma GAMA ?
	/// </summary>
	public class GammaCorrect
	{
		#region Costructors

		public GammaCorrect(double vmin, double vmax)
			: this(vmin, vmax, 1.0, false) { }

		public GammaCorrect(double vmin, double vmax, double gamma)
			: this(vmin, vmax, gamma, false) { }

		public GammaCorrect(double vmin, double vmax, bool inverted)
			: this(vmin, vmax, 1.0, inverted) { }

		public GammaCorrect(double vmin, double vmax, double gamma, bool inverted)
		{
			_min = vmin;
			_max = vmax;
			_gamma = gamma;
			_inverted = inverted;
		}

		public static readonly GammaCorrect Default = new GammaCorrect(0, 100, 1, false);

		#endregion


		#region Fields

		/// <summary>
		/// 标准化最小值
		/// </summary>
		double _min;
		/// <summary>
		/// 标准化最大值
		/// </summary>
		double _max;
		/// <summary>
		/// Gamma校正值
		/// </summary>
		double _gamma;

		/// <summary>
		///  反相
		/// </summary>
		bool _inverted;

		#endregion


		public byte ToGary(double v)
		{
			byte result = default;

			try
			{
				var vtemp = result;
                {
					// 标准化
					var gray = (Math.Abs(_max - _min) > 0)
						? (v - _min) / (_max - _min)
						: 0.001;

					// 规范在[0,1]
					gray = gray <= 0 ? 0.001 : gray;
					gray = gray >= 1 ? 0.999 : gray;

					// 预补偿
					gray = (Math.Abs(_gamma) > 0)
						? Math.Pow(gray, 1.0 / _gamma)
						: 0;

					// 转换为灰度
					vtemp = (byte)(short)(gray * 256 - 0.5);

					if (_inverted)
					{
						vtemp = (byte)(255 - vtemp);
					}
				}
				result = vtemp;

			}
			catch (Exception ex)
			{
#if DEBUG
				Debug.WriteLine(ex);
#endif
			}

			return result;
		}


		//@@@
	}



}

