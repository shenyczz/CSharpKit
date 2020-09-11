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
using CSharpKit.GeoApi.Geometries;

namespace CSharpKit.GeoApi
{
    public sealed class Extent : IExtent
    {
        #region Constructors

        public Extent()
            : this(Double.MaxValue, Double.MaxValue, Double.MinValue, Double.MinValue) { }

        private Extent(Extent rhs)
            : this(rhs.MinX, rhs.MinY, rhs.MaxX, rhs.MaxY) { }


        public Extent(Double minx, Double miny, Double maxx, Double maxy)
        {
            MinX = minx; MinY = miny;
            MaxX = maxx; MaxY = maxy;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 最小 X 坐标
        /// </summary>
        public Double MinX { get; set; }
        /// <summary>
        /// 最小 Y 坐标
        /// </summary>
        public Double MinY { get; set; }
        /// <summary>
        /// 最大 X 坐标
        /// </summary>
        public Double MaxX { get; set; }
        /// <summary>
        /// 最大 Y 坐标
        /// </summary>
        public Double MaxY { get; set; }

        public Object Owner { get; set; }

        public Double Area
        {
            get { return Width * Height; }
        }
        public Double Width
        {
            get { return MaxX - MinX; }
        }
        public Double Height
        {
            get { return MaxY - MinY; }
        }
        public ICoordinate Center
        {
            get
            {
                return IsEmpty
                    ? Coordinate.Empty
                    : new Coordinate((MinX + MaxX) / 2.0, (MinY + MaxY) / 2.0);
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return false
                    || Width <= 0
                    || Height <= 0
                    ;
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// 偏移
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void Offset(Double dx, Double dy)
        {
            MinX += dx; MinY += dy;
            MaxX += dx; MaxY += dy;
        }

        public IExtent Union(IExtent other)
        {
            if (other != null)
            {
                MinX = Math.Min(MinX, other.MinX);
                MinY = Math.Min(MinY, other.MinY);
                MaxX = Math.Max(MaxX, other.MaxX);
                MaxY = Math.Max(MaxY, other.MaxY);

            }
            return this;
        }

        /// <summary>
        /// 相交
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Intersects(Double x, Double y)
        {
            return true
                && !IsEmpty
                && !(x > MaxX || x < MinX || y > MaxY || y < MinY)
                ;
        }

        /// <summary>
        /// 包含
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Contains(Double x, Double y)
        {
            return true
                && !IsEmpty
                && x >= MinX
                && x <= MaxX
                && y >= MinY
                && y <= MaxY;
        }

        /// <summary>
        /// 点在范围内部
        /// </summary>
        /// <param name="x">点的x坐标</param>
        /// <param name="y">点的y坐标</param>
        /// <returns>true or false</returns>
        public bool PointInside(Double x, Double y)
        {
            //if (this.IsEmpty)
            //    return false;

            //return true
            //    && x > this.MinX && x < this.MaxX
            //    && y > this.MinY && y < this.MaxY
            //    ;
            return true
               && !IsEmpty
               && x > this.MinX && x < this.MaxX
               && y > this.MinY && y < this.MaxY
               ;
        }

        #endregion

        #region Public Functions - Override

        public override bool Equals(object obj)
        {
            return this.Equals(obj as IExtent);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode()
                ^ MinX.GetHashCode()
                ^ MinY.GetHashCode()
                ^ MaxX.GetHashCode()
                ^ MaxY.GetHashCode()
                ;
        }

        public override String ToString()
        {
            return "Extent" + "[" + MinX + " : " + MaxX + ", " + MinY + " : " + MaxY + "]";
        }

        #endregion

        #region IEquatable<IExtent> 成员

        public Boolean Equals(IExtent other)
        {
            return other != null
                && MinX.Equals(other.MinX)
                && MinY.Equals(other.MinY)
                && MaxX.Equals(other.MaxX)
                && MaxY.Equals(other.MaxY)
                ;
        }

        #endregion

        #region ICloneable 成员

        public IExtent Clone()
        {
            return new Extent(this);
        }

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }

        #endregion

        //@EndOf(Extent)
    }

}
