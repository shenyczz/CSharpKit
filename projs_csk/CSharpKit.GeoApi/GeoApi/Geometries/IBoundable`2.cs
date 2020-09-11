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
    class _geoms
    {
        /* Enums
         * 
         * Dimension
         * PrecisionModels
         * Ordinate
         *
         */

        /* GeoApi Interface
         * 
         * ICoordinate
         * IEnvelope
         * IGeometry
         * IGeometryCollection
         * 
         */

        /* GeoApi Class
         * 
         * Envelope
         * Coordinate
         * Geometry
         * Point
         * 
         * 
         * IGeometryComponentFilter
         * IGeometryFactory
         * 
         * IPoint
         * IPrecisionModel
         * 
         * 
         * 
         */
    }

    public interface IBoundable<out T, out TItem>
        where T : IIntersectable<T>, IExpandable<T>
    {
        /// <summary> 
        /// Returns a representation of space that encloses this Boundable, preferably
        /// not much bigger than this Boundable's boundary yet fast to test for intersection
        /// with the bounds of other Boundables. The class of object returned depends
        /// on the subclass of AbstractSTRtree.
        /// </summary>
        /// <returns> 
        /// An Envelope (for STRtrees), an Interval (for SIRtrees), or other object
        /// (for other subclasses of AbstractSTRtree).
        /// </returns>
        T Bounds { get; }

        /// <summary>
        /// Gets the item that is bounded
        /// </summary>
        TItem Item { get; }
    }







}
