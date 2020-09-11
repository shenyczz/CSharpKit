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
    public static class FloatExtensions
    {
        /// <summary>
        /// Compares two doubles and determines if they are equal to within the specified number of decimal places or not, using the
        /// number of decimal places as an absolute measure.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <param name="decimalPlaces">The number of decimal places.(小数位数)</param>
        public static bool AlmostEqual(this float a, float b, int decimalPlaces = 11)
        {
            var diff = a - b;
            var err_abs_max = 1.0 / Math.Pow(10, decimalPlaces);
            return AlmostEqualNorm(a, b, diff, (float)err_abs_max);
        }

        /// <summary>
        /// 几乎相等
        /// Compares two doubles and determines if they are equal
        /// within the specified maximum absolute error.
        /// </summary>
        /// <param name="a">The norm of the first value (can be negative).</param>
        /// <param name="b">The norm of the second value (can be negative).</param>
        /// <param name="diff">The norm of the difference of the two values (can be negative).</param>
        /// <param name="maximumAbsoluteError">The absolute accuracy required for being almost equal.</param>
        /// <returns>True if both doubles are almost equal up to the specified maximum absolute error, false otherwise.</returns>
        public static bool AlmostEqualNorm(this float a, float b, float diff, float maximumAbsoluteError)
        {
            // If A or B are infinity (positive or negative) then
            // only return true if they are exactly equal to each other -
            // that is, if they are both infinities of the same sign.
            if (float.IsInfinity(a) || float.IsInfinity(b))
            {
                return a == b;
            }

            // If A or B are a NAN, return false. NANs are equal to nothing,
            // not even themselves.
            if (float.IsNaN(a) || float.IsNaN(b))
            {
                return false;
            }

            return Math.Abs(diff) < maximumAbsoluteError;
        }


        /// <summary>
        /// Evaluates the minimum distance to the next distinguishable number near the argument value.
        /// </summary>
        /// <param name="value">The value used to determine the minimum distance.</param>
        /// <returns>
        /// Relative Epsilon (positive float or NaN).
        /// </returns>
        /// <remarks>Evaluates the <b>negative</b> epsilon. The more common positive epsilon is equal to two times this negative epsilon.</remarks>
        public static float EpsilonOf(this float value)
        {
            if (float.IsInfinity(value) || float.IsNaN(value))
            {
                return float.NaN;
            }

            int signed32 = SingleToInt32Bits(value);
            if (signed32 == 0)
            {
                signed32++;
                return Int32BitsToSingle(signed32) - value;
            }
            if (signed32-- < 0)
            {
                return Int32BitsToSingle(signed32) - value;
            }
            return value - Int32BitsToSingle(signed32);
        }
        /// <summary>
        /// Evaluates the minimum distance to the next distinguishable number near the argument value.
        /// </summary>
        /// <param name="value">The value used to determine the minimum distance.</param>
        /// <returns>Relative Epsilon (positive float or NaN)</returns>
        /// <remarks>Evaluates the <b>positive</b> epsilon. See also <see cref="EpsilonOf(float)"/></remarks>
        /// <seealso cref="EpsilonOf(float)"/>
        public static float PositiveEpsilonOf(this float value)
        {
            return 2 * EpsilonOf(value);
        }




        /// <summary>
        /// 数值级数
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The magnitude of the number.</returns>
        public static int Magnitude(this float value)
        {
            // Can't do this with zero because the 10-log of zero doesn't exist.
            if (value.Equals(0.0f))
            {
                return 0;
            }

            // Note that we need the absolute value of the input because Log10 doesn't
            // work for negative numbers (obviously).
            var magnitude = Convert.ToSingle(Math.Log10(Math.Abs(value)));
            var truncated = (int)Math.Truncate(magnitude);

            // To get the right number we need to know if the value is negative or positive
            // truncating a positive number will always give use the correct magnitude
            // truncating a negative number will give us a magnitude that is off by 1 (unless integer)
            return magnitude < 0f && truncated != magnitude
                ? truncated - 1
                : truncated;
        }
        /// <summary>
        /// 数值的刻度单位，返回-10到10之间的数字
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The value of the number.</returns>
        /// <remarks>
        /// 0.002 = 2e-3;
        /// Magnitude = -3
        /// MagnitudeScaleUnit = 2
        /// </remarks>
        public static double MagnitudeScaleUnit(this float value)
        {
            if (value.Equals(0.0))
            {
                return value;
            }

            int magnitude = value.Magnitude();
            return value * Math.Pow(10, -magnitude);
        }












        #region Converter

        public static Boolean ToBoolean(this Single v) => Math.Abs(v) > 0;
        public static Char ToChar(this Single v) => (Char)v;
        public static Byte ToByte(this Single v) => (Byte)v;
        public static SByte ToSByte(this Single v) => (SByte)v;
        public static Int16 ToInt16(this Single v) => (Int16)v;
        public static Int32 ToInt32(this Single v) => (Int32)v;
        public static Int64 ToInt64(this Single v) => (Int64)v;
        public static UInt16 ToUInt16(this Single v) => (UInt16)v;
        public static UInt32 ToUInt32(this Single v) => (UInt32)v;
        public static UInt64 ToUInt64(this Single v) => (UInt64)v;
        public static Single ToSingle(this Single v) => (Single)v;
        public static Double ToDouble(this Single v) => (Double)v;
        public static Decimal ToDecimal(this Single v) => (Decimal)v;

        #endregion



        #region Utilities

        static int SingleToInt32Bits(float value)
        {
            var union = new SingleIntUnion { Single = value };
            return union.Int32;
        }
        static float Int32BitsToSingle(int value)
        {
            var union = new SingleIntUnion { Int32 = value };
            return union.Single;
        }

        #endregion



        //}}@@@
    }





}
