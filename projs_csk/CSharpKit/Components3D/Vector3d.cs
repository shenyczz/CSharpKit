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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CSharpKit
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
    public struct Vector3d : ICloneable, IEquatable<Vector3d>, IEnumerable<double>
    {
        public Vector3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        private Vector3d(Vector3d rhs)
        {
            X = rhs.X;
            Y = rhs.Y;
            Z = rhs.Z;
        }

        /// <summary>
        /// 空
        /// </summary>
        public static readonly Vector3d Empty = new Vector3d();


        public double X;
        public double Y;
        public double Z;




        /// <summary>
        /// 长度
        /// </summary>
        public double Length => Math.Sqrt(X * X + Y * Y + Z * Z);



        /// <summary>
        /// 规范化
        /// </summary>
        public void Normalize()
        {
            double len = Length;
            if (len > 0)
            {
                X /= len;
                Y /= len;
                Z /= len;
            }
            else
            {
                X = 0;
                Y = 0;
                Z = 0;
            }
        }


        #region Functions - override

        public override int GetHashCode()
        {
            var hashCode = -307843816;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return string.Format("{0:F3}, {1:F3}, {2:F3}"
                , X, Y, Z);
        }

        #endregion


        #region ICloneable

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public Vector3d Clone()
        {
            return new Vector3d(this);
        }
        object ICloneable.Clone() => throw new NotImplementedException();

        #endregion


        #region IEquatable<Vector3>

        public bool Equals(Vector3d other)
        {
            bool equal = false;

            try
            {
                equal = true
                    && X == other.X
                    && Y == other.Y
                    && Z == other.Z;
            }
            catch (Exception)
            {
            }

            return equal;
        }

        public override bool Equals(object obj)
        {
            return Equals((Vector3d)obj);
        }

        #endregion


        #region IEnumerable<Double>

        IEnumerator<double> IEnumerable<double>.GetEnumerator()
        {
            double[] items = new double[] { X, Y, Z };
            foreach (double item in items)
                yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion







        /// <summary>
        /// ==
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Vector3d a, Vector3d b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Vector3d a, Vector3d b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// +(+=)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3d operator +(Vector3d a, Vector3d b)
        {
            return new Vector3d(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        /// <summary>
        /// -(-=)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3d operator -(Vector3d a, Vector3d b)
        {
            return new Vector3d(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        /// <summary>
        /// *(*=)
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Vector3d operator *(Vector3d vec, double scale)
        {
            return new Vector3d(vec.X * scale, vec.Y * scale, vec.Z * scale);
        }
        /// <summary>
        /// /(/=)
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Vector3d operator /(Vector3d vec, double scale)
        {
            return new Vector3d(vec.X / scale, vec.Y / scale, vec.Z / scale);
        }

        //}}@@@
    }

}
