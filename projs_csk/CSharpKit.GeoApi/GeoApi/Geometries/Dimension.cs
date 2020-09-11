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

namespace CSharpKit.GeoApi.Geometries
{
    /// <summary>
    /// Provides constants representing the dimensions of a point, a curve and a surface.
    /// 提供表示点、线和面维度常数
    /// </summary>
    /// <remarks>
    /// Also provides constants representing the dimensions of the empty geometry and
    /// non-empty geometries, and the wildcard constant <see cref="Dontcare"/> meaning "any dimension".
    /// These constants are used as the entries in <c>IntersectionMatrix</c>/>s.
    /// </remarks>
    public enum Dimension
    {
        /// <summary>
        /// Dimension value of a point (0).
        /// </summary>
        Point = 0,

        /// <summary>
        /// Dimension value of a curve (1).
        /// </summary>
        Curve = 1,

        /// <summary>
        /// Dimension value of a surface (2).
        /// </summary>
        Surface = 2,

        /// <summary>
        /// Dimension value of a empty point (-1).
        /// </summary>
        False = -1,

        /// <summary>
        /// Dimension value of non-empty geometries (= {Point,Curve,A}).
        /// </summary>
        True = -2,

        /// <summary>
        /// Dimension value for any dimension (= {False, True}).
        /// </summary>
        Dontcare = -3
    }







}
