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

namespace CSharpKit.Data.Esri
{
    public enum ShapeType
    {
        /// <summary>
        /// 空类型
        /// </summary>
        Null = 0,

        /// <summary>
        /// 点类型
        /// </summary>
        Point = 1,

        /// <summary>
        /// 折线
        /// </summary>
        Polyline = 3,

        /// <summary>
        /// 多边形
        /// </summary>
        Polygon = 5,

        /// <summary>
        /// 点集
        /// </summary>
        MultiPoint = 8,

        /// <summary>
        /// A 3D <see cref="ShapeType.Point">point</see>.
        /// </summary>
        PointZ = 11,

        /// <summary>
        /// A 3D <see cref="ShapeType.Polyline"/>, consisting of <see cref="PointZ"/> points.
        /// </summary>
        PolyLineZ = 13,

        /// <summary>
        /// A 3D <see cref="ShapeType.Polygon"/>, consisting of <see cref="PointZ"/> points.
        /// </summary>
        PolygonZ = 15,

        /// <summary>
        /// A set of <see cref="PointZ"/>s.
        /// </summary>
        MultiPointZ = 18,

        /// <summary>
        /// A <see cref="ShapeType.Point"/> plus a measure value as a Double-precision floating point.
        /// </summary>
        PointM = 21,

        /// <summary>
        /// A <see cref="ShapeType.Polyline"/>, consisting of <see cref="PointM"/> points.
        /// </summary>
        PolyLineM = 23,

        /// <summary>
        /// A <see cref="ShapeType.Polyline"/>, consisting of <see cref="PointM"/> points.
        /// </summary>
        PolygonM = 25,

        /// <summary>
        /// A set of <see cref="PointM"/>s.
        /// </summary>
        MultiPointM = 28,

        /// <summary>
        /// A set of surface patches.
        /// </summary>
        MultiPatch = 31,
    }








}
