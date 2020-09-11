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
    /// Geometry - 几何图形
    /// </summary>
    public abstract class Geometry : Target, IGeometry
    {
        protected Geometry()
        {
        }


        /// <summary>
        /// 范围
        /// </summary>
        public virtual IExtent Extent { get; set; }

        /// <summary>
        /// 位置 
        /// </summary>
        public IPoint Location { get; set; }

        /// <summary>
        /// 宽度(像素)
        /// </summary>
        public Double Width { get; set; }

        /// <summary>
        /// 高度(像素)
        /// </summary>
        public Double Height { get; set; }

        /// <summary>
        /// 使图形偏移指定的量
        /// </summary>
        /// <param name="dx">X坐标的偏移量</param>
        /// <param name="dy">Y坐标的偏移量</param>
        /// <remarks>
        /// 是虚函数，没有实现代码
        /// </remarks>
        public virtual void Offset(Double dx, Double dy)
        {
            this.Extent?.Offset(dx, dy);
        }

        /// <summary>
        /// 计算包围盒
        /// </summary>
        /// <returns></returns>
        protected virtual IExtent ComputeBoundingBox()
        {
            return null;
        }


        //@EndOf(Geometry)
    }



    /*

    public interface IGeometry : ICloneable, IComparable, IComparable<IGeometry>
    {
        //
        // 摘要:
        //     The GeoAPI.Geometries.IPrecisionModel the GeoAPI.Geometries.IGeometry.Factory
        //     used to create this.
        IPrecisionModel PrecisionModel { get; }
        //
        // 摘要:
        //     Gets the spatial reference id
        int SRID { get; set; }
        //
        // 摘要:
        //     Gets the geometry type
        string GeometryType { get; }
        //
        // 摘要:
        //     Gets the OGC geometry type
        OgcGeometryType OgcGeometryType { get; }
        //
        // 摘要:
        //     Gets the area of this geometry if applicable, otherwise 0d
        //
        // 言论：
        //     A GeoAPI.Geometries.ISurface method moved in IGeometry
        double Area { get; }
        //
        // 摘要:
        //     Gets the length of this geometry if applicable, otherwise 0d
        //
        // 言论：
        //     A GeoAPI.Geometries.ICurve method moved in IGeometry
        double Length { get; }
        //
        // 摘要:
        //     Gets the envelope this GeoAPI.Geometries.IGeometry would fit into.
        IGeometry Envelope { get; }
        //
        // 摘要:
        //     Gets the envelope this GeoAPI.Geometries.IGeometry would fit into.
        Envelope EnvelopeInternal { get; }
        //
        // 摘要:
        //     Get the number of coordinates, that make up this geometry
        //
        // 言论：
        //     A GeoAPI.Geometries.ILineString method moved to IGeometry
        int NumPoints { get; }
        //
        // 摘要:
        //     Gets a point that is ensured to lie inside this geometry.
        IPoint InteriorPoint { get; }
        bool IsRectangle { get; }
        bool IsEmpty { get; }
        //
        // 摘要:
        //     A ISurface method moved in IGeometry
        IPoint PointOnSurface { get; }
        //
        // 摘要:
        //     Gets or sets the user data associated with this geometry
        object UserData { get; set; }

        //
        // 摘要:
        //     Performs an operation with or on this Geometry and its component Geometry's.
        //     Only GeometryCollections and Polygons have component Geometry's; for Polygons
        //     they are the LinearRings of the shell and holes.
        //
        // 参数:
        //   filter:
        //     The filter to apply to this Geometry.
        void Apply(IGeometryComponentFilter filter);
        //
        // 摘要:
        //     Performs an operation with or on this Geometry and its subelement Geometrys (if
        //     any). Only GeometryCollections and subclasses have subelement Geometry's.
        //
        // 参数:
        //   filter:
        //     The filter to apply to this Geometry (and its children, if it is a GeometryCollection).
        void Apply(IGeometryFilter filter);
        //
        // 摘要:
        //     Performs an operation on the coordinates in this Geometry's GeoAPI.Geometries.ICoordinateSequences.
        //     If this method modifies any coordinate values, GeoAPI.Geometries.IGeometry.GeometryChanged
        //     must be called to update the geometry state.
        //
        // 参数:
        //   filter:
        //     The filter to apply
        void Apply(ICoordinateSequenceFilter filter);
        //
        // 摘要:
        //     Performs an operation with or on this Geometry's coordinates. If you are using
        //     this method to modify the point, be sure to call GeoAPI.Geometries.IGeometry.GeometryChanged
        //     afterwards. Note that you cannot use this method to modify this Geometry if its
        //     underlying GeoAPI.Geometries.ICoordinateSequence's Get method returns a copy
        //     of the GeoAPI.Geometries.IGeometry.Coordinate, rather than the actual Coordinate
        //     stored (if it even stores Coordinates at all).
        //
        // 参数:
        //   filter:
        //     The filter to apply to this Geometry's coordinates
        void Apply(ICoordinateFilter filter);
        //
        // 摘要:
        //     Gets the Well-Known-Binary representation of this geometry
        //
        // 返回结果:
        //     A byte array describing this geometry
        byte[] AsBinary();
        //
        // 摘要:
        //     Gets the Well-Known-Text representation of this geometry
        //
        // 返回结果:
        //     A text describing this geometry
        string AsText();
        IGeometry Buffer(double distance);
        [Obsolete]
        IGeometry Buffer(double distance, BufferStyle endCapStyle);
        [Obsolete]
        IGeometry Buffer(double distance, int quadrantSegments, BufferStyle endCapStyle);
        IGeometry Buffer(double distance, int quadrantSegments, EndCapStyle endCapStyle);
        IGeometry Buffer(double distance, int quadrantSegments);
        IGeometry Buffer(double distance, IBufferParameters bufferParameters);
        bool Contains(IGeometry g);
        //
        // 摘要:
        //     Computes the convex hull for this geometry
        //
        // 返回结果:
        //     The convex hull
        IGeometry ConvexHull();
        //
        // 摘要:
        //     Creates and returns a full copy of this GeoAPI.Geometries.IGeometry object (including
        //     all coordinates contained by it).
        //     Subclasses are responsible for implementing this method and copying their internal
        //     data.
        //
        // 返回结果:
        //     A clone of this instance
        IGeometry Copy();
        bool CoveredBy(IGeometry g);
        bool Covers(IGeometry g);
        bool Crosses(IGeometry g);
        IGeometry Difference(IGeometry other);
        bool Disjoint(IGeometry g);
        //
        // 摘要:
        //     Returns the minimum distance between this Geometry and the Geometry g.
        //
        // 参数:
        //   g:
        //     The Geometry from which to compute the distance.
        double Distance(IGeometry g);
        [Obsolete("Favor either EqualsTopologically or EqualsExact instead.")]
        bool Equals(IGeometry other);
        bool EqualsExact(IGeometry other, double tolerance);
        bool EqualsExact(IGeometry other);
        //
        // 摘要:
        //     Tests whether two geometries are exactly equal in their normalized forms.
        //
        // 参数:
        //   g:
        //     A geometry
        //
        // 返回结果:
        //     true if the input geometries are exactly equal in their normalized form
        bool EqualsNormalized(IGeometry g);
        //
        // 摘要:
        //     Tests whether this geometry is topologically equal to the argument geometry as
        //     defined by the SFS equals predicate.
        //
        // 参数:
        //   other:
        //     A geometry
        //
        // 返回结果:
        //     true if this geometry is topologically equal to other
        bool EqualsTopologically(IGeometry other);
        //
        // 摘要:
        //     Notifies this geometry that its coordinates have been changed by an external
        //     party (using a CoordinateFilter, for example). The Geometry will flush and/or
        //     update any information it has cached (such as its GeoAPI.Geometries.IEnvelope).
        void GeometryChanged();
        //
        // 摘要:
        //     Notifies this Geometry that its Coordinates have been changed by an external
        //     party. When GeoAPI.Geometries.IGeometry.GeometryChanged is called, this method
        //     will be called for this Geometry and its component geometries.
        void GeometryChangedAction();
        //
        // 摘要:
        //     Gets the geometry at the given index
        //
        // 参数:
        //   n:
        //     The index of the geometry to get
        //
        // 返回结果:
        //     A geometry that is part of the GeoAPI.Geometries.IGeometryCollection
        //
        // 言论：
        //     A GeoAPI.Geometries.IGeometryCollection method moved in IGeometry
        IGeometry GetGeometryN(int n);
        //
        // 摘要:
        //     Gets an array of System.Double ordinate values.
        double[] GetOrdinates(Ordinate ordinate);
        IGeometry Intersection(IGeometry other);
        bool Intersects(IGeometry g);
        bool IsWithinDistance(IGeometry geom, double distance);
        //
        // 摘要:
        //     Normalizes this geometry
        void Normalize();
        //
        // 摘要:
        //     Creates a new Geometry which is a normalized copy of this Geometry.
        //
        // 返回结果:
        //     A normalized copy of this geometry.
        IGeometry Normalized();
        bool Overlaps(IGeometry g);
        IntersectionMatrix Relate(IGeometry g);
        bool Relate(IGeometry g, string intersectionPattern);
        //
        IGeometry Reverse();
        IGeometry SymmetricDifference(IGeometry other);
        bool Touches(IGeometry g);
        IGeometry Union();
        IGeometry Union(IGeometry other);
        bool Within(IGeometry g);
    }
     */


}
