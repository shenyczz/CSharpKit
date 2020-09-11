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

namespace CSharpKit.GeoApi.Geometries
{
    /// <summary>
    /// 光滑曲线
    /// </summary>
    public class SmoothCurve : Polyline, ISmoothCurve
    {

        #region Properties

        private Double _Tension = 0.35;

        /// <summary>
        /// 曲线的张力系数
        /// </summary>
        public Double Tension
        {
            get { return _Tension; }
            set { _Tension = value; }
        }

        #endregion

        /// <summary>
        /// 使图形偏移指定的量
        /// </summary>
        /// <param name="dx">X坐标的偏移量</param>
        /// <param name="dy">Y坐标的偏移量</param>
        public override void Offset(Double dx, Double dy)
        {
            base.Offset(dx, dy);
        }

        //@EndOf(SmoothCurve)
    }






































}
