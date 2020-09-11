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

namespace CSharpKit
{
    /// <summary>
    /// Point3D
    /// </summary>
    public class Point3D : IEquatable<Point3D>
    {
        #region Constructors

        public Point3D()
            : this(0, 0, 0) { }

        public Point3D(Point3D rhs)
            : this(rhs.X, rhs.Y, rhs.Z) { }

        public Point3D(Double x, Double y, Double z)
        {
            X = x; Y = y; Z = z;
        }

        #endregion

        #region Properties

        public Double X { get; set; }
        public Double Y { get; set; }
        public Double Z { get; set; }

        public Double Length
        {
            get { return Math.Sqrt(X * X + Y * Y + Z * Z); }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// 模(L2 norm)
        /// </summary>
        /// <returns></returns>
        public Double Norm()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        /// <summary>
        /// 模平方
        /// </summary>
        /// <returns></returns>
        public Double Norm2()
        {
            return X * X + Y * Y + Z * Z;
        }

        /// <summary>
        /// 标准化
        /// </summary>
        /// <returns></returns>
        public Point3D Normalize()
        {
            double norm = Norm();
            return new Point3D(X / norm, Y / norm, Z / norm);
        }

        /// <summary>
        /// 相等比较
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override Boolean Equals(Object other)
        {
            try
            {
                if (other is Point3D)
                    return this == (Point3D)other;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 取得对象的哈希表
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        #endregion

        #region Operator Override

        public static Point3D operator +(Point3D P1, Point3D P2)	// addition 2
        {
            return new Point3D(P1.X + P2.X, P1.Y + P2.Y, P1.Z + P2.Z);
        }

        public static Point3D operator -(Point3D P1, Point3D P2)	// subtraction 2
        {
            return new Point3D(P1.X - P2.X, P1.Y - P2.Y, P1.Z - P2.Z);
        }

        public static Point3D operator -(Point3D P)	// negation
        {
            return new Point3D(-P.X, -P.Y, -P.Z);
        }

        public static Point3D operator *(Point3D P, double k)	// multiply by real 2
        {
            return new Point3D(P.X * k, P.Y * k, P.Z * k);
        }

        public static Point3D operator *(double k, Point3D P)	// and its reverse order!
        {
            return new Point3D(P.X * k, P.Y * k, P.Z * k);
        }

        public static Point3D operator *(Point3D P1, Point3D P2)
        {
            return new Point3D(P1.Y * P2.Z - P1.Z * P2.Y,
                P1.Z * P2.X - P1.X * P2.Z, P1.X * P2.Y - P1.Y * P2.X);
        }

        public static Point3D operator /(Point3D P, double k)	// divide by real 2
        {
            return new Point3D(P.X / k, P.Y / k, P.Z / k);
        }

        public static Boolean operator ==(Point3D P1, Point3D P2) // equal?
        {
            return (P1.X == P2.X && P1.Y == P2.Y && P1.Z == P2.Z);
        }

        public static Boolean operator !=(Point3D P1, Point3D P2) // equal?
        {
            return (P1.X != P2.X || P1.Y != P2.Y || P1.Z != P2.Z);
        }

        #endregion

        #region Static Functions

        /// <summary>
        /// 叉积
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <returns></returns>
        public static Point3D Cross(Point3D P1, Point3D P2)
        {
            return P1 * P2;
        }

        /// <summary>
        /// 点积
        /// </summary>
        /// <param name="P1"></param>
        /// <param name="P2"></param>
        /// <returns></returns>
        public static Double Dot(Point3D P1, Point3D P2) // inner product 2
        {
            return (P1.X * P2.X + P1.Y * P2.Y + P1.Z * P2.Z);
        }

        /// <summary>
        /// 角度
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Angle GetAngle(Point3D p1, Point3D p2)
        {
            var result = Angle.FromRadians(Math.Acos(Dot(p1, p2) / (p1.Length * p2.Length)));
            return result;
        }

        #endregion

        #region IEquatable<Point3D> 成员

        public bool Equals(Point3D other)
        {
            return true
                && X == other.X
                && Y == other.Y
                && Z == other.Z
                ;
        }

        #endregion
    }



}
