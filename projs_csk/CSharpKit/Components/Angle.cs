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
using System.Globalization;

namespace CSharpKit
{
    /// <summary>
    /// 角<br/>
    /// Usage:<br/>
    /// <see cref="Angle.FromDegrees(double)"/>
    /// <see cref="Angle.FromRadians(double)"/>
    /// </summary>
    public sealed class Angle : IEquatable<Angle>
    {
        Angle() { }

        #region Fields - Static

        static readonly double D2R = Math.PI / 180;
        static readonly double R2D = 180.0 / Math.PI;

        /// <summary>
        /// A zeroed angle
        /// </summary>
        public static readonly Angle Zero;

        /// <summary>
        /// Minimum value for angle
        /// </summary>
        public static readonly Angle MinValue = FromRadians(Double.MinValue);

        /// <summary>
        /// Maximum value for angle
        /// </summary>
        public static readonly Angle MaxValue = FromRadians(Double.MaxValue);

        /// <summary>
        /// Angle containing Not a Number
        /// </summary>
        public static readonly Angle NaN = FromRadians(Double.NaN);

        #endregion

        #region Properties

        /// <summary>
        /// 弧度
        /// </summary>
        public double Radians { get; private set; }

        /// <summary>
        /// 度
        /// </summary>
        public double Degrees => Radians * R2D;


        #endregion

        #region Public Functions

        /// <summary>
        /// Normalizes the angle so it is between 0 and 360
        /// </summary>
        public void Normalize()
        {
            if (Radians > Math.PI * 2)
                Radians -= Math.PI * 2;
            if (Radians < -Math.PI * 2)
                Radians += Math.PI * 2;
        }

        /// <summary>
        /// Converts degrees to degrees/minutes/seconds
        /// </summary>
        /// <returns>String on format dd^mm'ss.sss"</returns>
        public string ToStringDms()
        {
            double decimalDegrees = this.Degrees;
            double d = Math.Abs(decimalDegrees);
            double m = (60 * (d - Math.Floor(d)));
            double s = (60 * (m - Math.Floor(m)));

            return String.Format("{0}'{1}'{2:f3}\"",
                (int)d * Math.Sign(decimalDegrees),
                (int)m,
                s);
        }

        #endregion

        #region Override Functions

        public override bool Equals(object obj)
        {
            return Equals((Angle)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode()
                ^ Radians.GetHashCode();
        }

        public override string ToString()
        {
            return Degrees.ToString(CultureInfo.InvariantCulture) + "DEG";
        }

        #endregion

        #region IEquatable<Angle>

        public bool Equals(Angle other)
        {
            return (other == null)
                ? false
                : Math.Abs(Radians - other.Radians) < Single.Epsilon;
        }

        #endregion

        public static double ToRadians(double deg)
        {
            return deg * D2R;
        }
        public static double ToDegrees(double rad)
        {
            return rad * R2D;
        }


        #region Static Functions

        /// <summary>
        /// Creates a new angle from angle in radians.
        /// </summary>
        public static Angle FromRadians(double radians)
        {
            Angle res = new Angle
            {
                Radians = radians
            };
            return res;
        }

        /// <summary>
        /// Creates a new angle from angle in degrees.
        /// </summary>
        public static Angle FromDegrees(double degrees)
        {
            Angle res = new Angle
            {
                Radians = degrees * D2R
            };
            return res;
        }

        /// <summary>
        /// Returns the absolute value of the specified angle
        /// </summary>
        public static Angle Abs(Angle angle)
        {
            return FromRadians(Math.Abs(angle.Radians));
        }

        /// <summary>
        /// Checks for angle containing "Not a Number"
        /// </summary>
        public static bool IsNaN(Angle angle)
        {
            return double.IsNaN(angle.Radians);
        }

        public static bool operator ==(Angle a, Angle b)
        {
            return Math.Abs(a.Radians - b.Radians) < float.Epsilon;
        }

        public static bool operator !=(Angle a, Angle b)
        {
            return Math.Abs(a.Radians - b.Radians) > float.Epsilon;
        }

        public static bool operator <(Angle a, Angle b)
        {
            return a.Radians < b.Radians;
        }

        public static bool operator >(Angle a, Angle b)
        {
            return a.Radians > b.Radians;
        }

        public static Angle operator +(Angle a, Angle b)
        {
            double res = a.Radians + b.Radians;
            return Angle.FromRadians(res);
        }

        public static Angle operator -(Angle a, Angle b)
        {
            double res = a.Radians - b.Radians;
            return Angle.FromRadians(res);
        }

        public static Angle operator *(Angle a, double times)
        {
            return Angle.FromRadians(a.Radians * times);
        }

        public static Angle operator *(double times, Angle a)
        {
            return Angle.FromRadians(a.Radians * times);
        }

        public static Angle operator /(double divisor, Angle a)
        {
            return Angle.FromRadians(a.Radians / divisor);
        }

        public static Angle operator /(Angle a, double divisor)
        {
            return Angle.FromRadians(a.Radians / divisor);
        }

        #endregion

        //@EndOf(Angle)
    }


}
