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
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace CSharpKit.GeoApi.Geometries
{
    /// <summary>
    /// Coordinate - 坐标
    /// </summary>
    [Serializable]
    public sealed class Coordinate : ICoordinate, ICloneable, IComparable, IComparable<ICoordinate>, IEquatable<ICoordinate>
    {
        #region Constructors

        public Coordinate()
            : this(NullOrdinate, NullOrdinate, NullOrdinate) { }

        private Coordinate(Coordinate rhs)
            : this(rhs.X, rhs.Y, rhs.Z) { }

        public Coordinate(Double x, Double y)
            : this(x, y, NullOrdinate) { }

        public Coordinate(Double x, Double y, Double z)
        {
            X = x; Y = y; Z = z;
        }

        #endregion

        #region Constants

        /// <summary>
        /// 空坐标值
        /// The value used to indicate a null or missing ordinate value.
        /// In particular, used for the value of ordinates for dimensions
        /// greater than the defined dimension of a coordinate.
        /// </summary>
        public const Double NullOrdinate = Double.NaN;

        #endregion

        #region Fields - Static

        public static Coordinate Empty { get; } = new Coordinate();

        #endregion

        #region Properties

        /// <summary>
        /// X coordinate.
        /// </summary>
        public Double X { get; set; }

        /// <summary>
        /// X coordinate.
        /// </summary>
        public Double Y { get; set; }

        /// <summary>
        /// X coordinate.
        /// </summary>
        public Double Z { get; set; }

        /// <summary>
        /// 是否空值
        /// </summary>
        public Boolean IsEmpty
        {
            get
            {
                return false
                    || Double.IsNaN(X)
                    || Double.IsNaN(Y)
                    ;
            }
        }

        /// <summary>
        /// Gets or sets the ordinate value for the given index.
        /// The supported values for the index are 
        /// <see cref="Ordinate.X"/>, <see cref="Ordinate.Y"/> and <see cref="Ordinate.Z"/>.
        /// </summary>
        /// <param name="ordinateIndex">The ordinate index</param>
        /// <returns>The ordinate value</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="ordinateIndex"/> is not in the valid range.</exception>
        public double this[Ordinate ordinateIndex]
        {
            get
            {
                switch (ordinateIndex)
                {
                    case Ordinate.X:
                        return X;
                    case Ordinate.Y:
                        return Y;
                    case Ordinate.Z:
                        return Z;
                }
                throw new ArgumentOutOfRangeException("ordinateIndex");
            }
            set
            {
                switch (ordinateIndex)
                {
                    case Ordinate.X:
                        X = value;
                        return;
                    case Ordinate.Y:
                        Y = value;
                        return;
                    case Ordinate.Z:
                        Z = value;
                        return;
                }
                throw new ArgumentOutOfRangeException("ordinateIndex");
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// 两个坐标间距离
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public double Distance(ICoordinate other)
        {
            var dx = X - other.X;
            var dy = Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        public Double Distance3D(ICoordinate other)
        {
            var dx = X - other.X;
            var dy = Y - other.Y;
            var dz = Z - other.Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        /// <summary>
        /// Returns whether the planar projections of the two <other>Coordinate</other>s are equal.
        /// the Z coordinates do not have to be equal.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Boolean Equals2D(ICoordinate other)
        {
            return true
                && X == other.X
                && Y == other.Y;
        }

        /// <summary>
        /// Tests if another coordinate has the same value for X and Y, within a tolerance.
        /// </summary>
        /// <param name="other">A <see cref="Coordinate"/>.</param>
        /// <param name="tolerance">The tolerance value.</param>
        /// <returns><c>true</c> if the X and Y ordinates are within the given tolerance.</returns>
        /// <remarks>The Z ordinate is ignored.</remarks>
        public bool Equals2D(ICoordinate other, double tolerance)
        {
            return true
                && EqualsWithTolerance(X, other.X, tolerance)
                && EqualsWithTolerance(Y, other.Y, tolerance);
        }
        private static bool EqualsWithTolerance(double x1, double x2, double tolerance)
        {
            return Math.Abs(x1 - x2) <= tolerance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Boolean Equals3D(ICoordinate other)
        {
            return true
                && (X == other.X)
                && (Y == other.Y)
                && ((Z == other.Z) || (Double.IsNaN(Z) && Double.IsNaN(other.Z)))
                ;
        }

        #endregion

        #region ICloneable 成员

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ICoordinate Clone()
        {
            return new Coordinate(this);
        }

        object ICloneable.Clone()
        {
            return base.MemberwiseClone();
        }

        #endregion

        #region IComparable, IComparable<ICoordinate> Members

        public int CompareTo(object obj)
        {
            return CompareTo(obj as ICoordinate);
        }

        /// <summary>
        /// Compares this object with the specified object for order.
        /// Since Coordinates are 2.5D, this routine ignores the z value when making the comparison.
        /// Returns
        ///   -1  : this.x lowerthan other.x || ((this.x == other.x) AND (this.y lowerthan other.y))
        ///    0  : this.x == other.x AND this.y = other.y
        ///    1  : this.x greaterthan other.x || ((this.x == other.x) AND (this.y greaterthan other.y))
        /// </summary>
        /// <param name="other"><other>Coordinate</other> with which this <other>Coordinate</other> is being compared.</param>
        /// <returns>
        /// A negative integer, zero, or a positive integer as this <other>Coordinate</other>
        ///         is less than, equal to, or greater than the specified <other>Coordinate</other>.
        /// </returns>
        public int CompareTo(ICoordinate other)
        {
            if (X < other.X)
                return -1;
            if (X > other.X)
                return 1;
            if (Y < other.Y)
                return -1;
            return Y > other.Y ? 1 : 0;
        }

        #endregion

        #region IEquatable<ICoordinate> 成员

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Boolean Equals(ICoordinate other)
        {
            return Equals2D(other);
        }

        #endregion

        #region Override Functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as ICoordinate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode()
                ^ X.GetHashCode()
                ^ Y.GetHashCode()
                ^ Z.GetHashCode()
                ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ")";
            //return "(" + X.ToString("R", NumberFormatInfo.InvariantInfo) + ", " +
            //             Y.ToString("R", NumberFormatInfo.InvariantInfo) + ", " +
            //             Z.ToString("R", NumberFormatInfo.InvariantInfo) + ")";
        }

        #endregion

        #region Operator Override

        /// <summary>
        /// operator +
        /// </summary>
        public static Coordinate operator +(Coordinate coord1, double d)
        {
            return new Coordinate(coord1.X + d, coord1.Y + d, coord1.Z + d);
        }
        public static Coordinate operator +(Coordinate coord1, ICoordinate coord2)
        {
            return new Coordinate(coord1.X + coord2.X, coord1.Y + coord2.Y, coord1.Z + coord2.Z);
        }

        #endregion


        //@@@
    }

    /*
    public class Coordinate : ICoordinate, IComparable<Coordinate>
    {

        /// <summary>
        /// Overloaded * operator.
        /// </summary>
        public static Coordinate operator *(Coordinate coord1, Coordinate coord2)
        {
            return new Coordinate(coord1.X * coord2.X, coord1.Y * coord2.Y, coord1.Z * coord2.Z);
        }

        /// <summary>
        /// Overloaded * operator.
        /// </summary>
        public static Coordinate operator *(Coordinate coord1, double d)
        {
            return new Coordinate(coord1.X * d, coord1.Y * d, coord1.Z * d);
        }

        /// <summary>
        /// Overloaded - operator.
        /// </summary>
        public static Coordinate operator -(Coordinate coord1, double d)
        {
            return new Coordinate(coord1.X - d, coord1.Y - d, coord1.Z - d);
        }
        /// <summary>
        /// Overloaded - operator.
        /// </summary>
        public static Coordinate operator -(Coordinate coord1, Coordinate coord2)
        {
            return new Coordinate(coord1.X - coord2.X, coord1.Y - coord2.Y, coord1.Z - coord2.Z);
        }


        /// <summary>
        /// Overloaded / operator.
        /// </summary>
        public static Coordinate operator /(Coordinate coord1, double d)
        {
            return new Coordinate(coord1.X / d, coord1.Y / d, coord1.Z / d);
        }
        /// <summary>
        /// Overloaded / operator.
        /// </summary>
        public static Coordinate operator /(Coordinate coord1, Coordinate coord2)
        {
            return new Coordinate(coord1.X / coord2.X, coord1.Y / coord2.Y, coord1.Z / coord2.Z);
        }

    }


     */

}

