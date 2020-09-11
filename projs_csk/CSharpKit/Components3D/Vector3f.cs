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
    public struct Vector3f : ICloneable, IEquatable<Vector3f>, IEnumerable<float>
    {
        public Vector3f(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        private Vector3f(Vector3f rhs)
        {
            X = rhs.X;
            Y = rhs.Y;
            Z = rhs.Z;
        }

        /// <summary>
        /// 空
        /// </summary>
        public static readonly Vector3f Empty = new Vector3f();


        public float X;
        public float Y;
        public float Z;




        /// <summary>
        /// 长度
        /// </summary>
        public float Length => (float)Math.Sqrt(X * X + Y * Y + Z * Z);



        /// <summary>
        /// 规范化
        /// </summary>
        public void Normalize()
        {
            float len = Length;
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
        public Vector3f Clone()
        {
            return new Vector3f(this);
        }
        object ICloneable.Clone() => throw new NotImplementedException();

        #endregion


        #region IEquatable<Vector3>

        public bool Equals(Vector3f other)
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
            return Equals((Vector3f)obj);
        }

        #endregion


        #region IEnumerable<Double>

        IEnumerator<float> IEnumerable<float>.GetEnumerator()
        {
            float[] items = new float[] { X, Y, Z };
            foreach (float item in items)
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
        public static bool operator ==(Vector3f a, Vector3f b)
        {
            return a.Equals(b);
        }
        /// <summary>
        /// !=
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Vector3f a, Vector3f b)
        {
            return !a.Equals(b);
        }


        /// <summary>
        /// +(+=)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3f operator +(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        /// <summary>
        /// -(-=)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3f operator -(Vector3f a, Vector3f b)
        {
            return new Vector3f(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        /// <summary>
        /// *(*=)
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Vector3f operator *(Vector3f vec, float scale)
        {
            return new Vector3f(vec.X * scale, vec.Y * scale, vec.Z * scale);
        }
        /// <summary>
        /// /(/=)
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Vector3f operator /(Vector3f vec, float scale)
        {
            return new Vector3f(vec.X / scale, vec.Y / scale, vec.Z / scale);
        }


        //}}@@@
    }

}
