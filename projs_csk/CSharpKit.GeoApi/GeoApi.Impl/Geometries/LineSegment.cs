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
    /// LineSegment
    /// </summary>
    public class LineSegment : Geometry
    {
        #region Constructors

        public LineSegment()
            : this(0, 0, 0, 0) { }

        public LineSegment(LineSegment rhs)
            : this(rhs.P0.X, rhs.P0.Y, rhs.P1.X, rhs.P1.Y) { }

        public LineSegment(Double x0, Double y0, Double x1, Double y1)
            : this(new Point(x0, y0), new Point(x1, y1)) { }

        public LineSegment(Point p0, Point p1)
        {
            P0 = p0; P1 = p1;
        }

        #endregion

        #region Properties

        public Point P0 { get; set; }
        public Point P1 { get; set; }

        #endregion

        #region Public Functions

        /// <summary>
        /// 使图形偏移指定的量
        /// </summary>
        /// <param name="dx">X坐标的偏移量</param>
        /// <param name="dy">Y坐标的偏移量</param>
        public override void Offset(Double dx, Double dy)
        {
            this.P0.Offset(dx, dy);
            this.P1.Offset(dx, dy);
            base.Offset(dx, dy);
        }

        #endregion

        //@@@
    }






































}
