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
    /// Curve
    /// </summary>
    public abstract class Curve : MultiPoint, ICurve
    {

        /// <summary>
        /// Gets a value indicating the start point of the curve
        /// </summary>
        public IPoint StartPoint
        {
            get
            {
                return (Count <= 0) ? null : this[0];
            }
        }

        /// <summary>
        /// Gets a value indicating the end point of the curve
        /// </summary>
        public IPoint EndPoint
        {
            get
            {
                return (Count <= 0) ? null : this[Count - 1];
            }
        }

        /// <summary>
        /// Gets a value indicating that the curve is closed. 
        /// In this case <see cref="StartPoint"/> an <see cref="EndPoint"/> are equal.
        /// </summary>
        public bool IsClosed
        {
            get { return StartPoint == EndPoint; }
            //get { return this[0].X == this[Count - 1].X && this[0].Y == this[Count - 1].Y; }
        }

        /// <summary>
        /// Gets a value indicating that the curve is a ring. 
        /// </summary>
        public bool IsRing { get => false; }

        //@@@
    }






































}
