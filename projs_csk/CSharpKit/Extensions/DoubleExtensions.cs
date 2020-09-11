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
    /// 
    /// </summary>
    public static class DoubleExtensions
    {
        #region AlmostEqual Comparison - 几乎相等比较

        /// <summary>
        /// Compares two doubles and determines if they are equal to within the specified number of decimal places or not, using the
        /// number of decimal places as an absolute measure.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <param name="decimalPlaces">The number of decimal places.(小数位数)</param>
        public static bool AlmostEqual(this double a, double b, int decimalPlaces = 11)
        {
            var diff = a - b;
            var err_abs_max = 1.0 / Math.Pow(10, decimalPlaces);
            return AlmostEqualNorm(a, b, diff, err_abs_max);
        }

        /// <summary>
        /// 几乎相等
        /// Compares two doubles and determines if they are equal
        /// within the specified maximum absolute error.
        /// </summary>
        /// <param name="a">The norm of the first value (can be negative).</param>
        /// <param name="b">The norm of the second value (can be negative).</param>
        /// <param name="diff">The norm of the difference of the two values(两个值之差的范数) (can be negative).</param>
        /// <param name="maximumAbsoluteError">The absolute accuracy required for being almost equal.</param>
        /// <returns>True if both doubles are almost equal up to the specified maximum absolute error, false otherwise.</returns>
        public static bool AlmostEqualNorm(this double a, double b, double diff, double maximumAbsoluteError)
        {
            // If A or B are infinity (positive or negative) then
            // only return true if they are exactly equal to each other -
            // that is, if they are both infinities of the same sign.
            if (double.IsInfinity(a) || double.IsInfinity(b))
            {
                return a == b;
            }

            // If A or B are a NAN, return false. NANs are equal to nothing,
            // not even themselves.
            if (double.IsNaN(a) || double.IsNaN(b))
            {
                return false;
            }

            return Math.Abs(diff) < maximumAbsoluteError;
        }


        /// <summary>
        /// Compares two doubles and determines if they are equal to within the specified number of decimal places or not. If the numbers
        /// are very close to zero an absolute difference is compared, otherwise the relative difference is compared.
        /// </summary>
        /// <param name="a">The first value.</param>
        /// <param name="b">The second value.</param>
        /// <param name="decimalPlaces">The number of decimal places.</param>
        // public static bool AlmostEqualRelative(this double a, double b, int decimalPlaces)
        // {
        //     return AlmostEqualNormRelative(a, b, a - b, decimalPlaces);
        // }
        /// <summary>
        /// Compares two doubles and determines if they are equal to within the specified number of decimal places or not. If the numbers
        /// are very close to zero an absolute difference is compared, otherwise the relative difference is compared.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The values are equal if the difference between the two numbers is smaller than 10^(-numberOfDecimalPlaces). We divide by
        /// two so that we have half the range on each side of the numbers, e.g. if <paramref name="decimalPlaces"/> == 2, then 0.01 will equal between
        /// 0.005 and 0.015, but not 0.02 and not 0.00
        /// </para>
        /// </remarks>
        /// <param name="a">The norm of the first value (can be negative).</param>
        /// <param name="b">The norm of the second value (can be negative).</param>
        /// <param name="diff">The norm of the difference of the two values (can be negative).</param>
        /// <param name="decimalPlaces">The number of decimal places.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="decimalPlaces"/> is smaller than zero.</exception>
        // public static bool AlmostEqualNormRelative(this double a, double b, double diff, int decimalPlaces)
        // {
        //     if (decimalPlaces < 0)
        //     {
        //         // Can't have a negative number of decimal places
        //         throw new ArgumentOutOfRangeException(nameof(decimalPlaces));
        //     }

        //     // If A or B are a NAN, return false. NANs are equal to nothing,
        //     // not even themselves.
        //     if (double.IsNaN(a) || double.IsNaN(b))
        //     {
        //         return false;
        //     }

        //     // If A or B are infinity (positive or negative) then
        //     // only return true if they are exactly equal to each other -
        //     // that is, if they are both infinities of the same sign.
        //     if (double.IsInfinity(a) || double.IsInfinity(b))
        //     {
        //         return a == b;
        //     }

        //     // If both numbers are equal, get out now. This should remove the possibility of both numbers being zero
        //     // and any problems associated with that.
        //     if (a.Equals(b))
        //     {
        //         return true;
        //     }

        //     // If one is almost zero, fall back to absolute equality
        //     if (Math.Abs(a) < PrecisionUtils.DoublePrecision || Math.Abs(b) < PrecisionUtils.DoublePrecision)
        //     {
        //         // The values are equal if the difference between the two numbers is smaller than
        //         // 10^(-numberOfDecimalPlaces). We divide by two so that we have half the range
        //         // on each side of the numbers, e.g. if decimalPlaces == 2,
        //         // then 0.01 will equal between 0.005 and 0.015, but not 0.02 and not 0.00
        //         return Math.Abs(diff) < Math.Pow(10, -decimalPlaces) / 2d;
        //     }

        //     // If the magnitudes of the two numbers are equal to within one magnitude the numbers could potentially be equal
        //     int magnitudeOfFirst = Magnitude(a);
        //     int magnitudeOfSecond = Magnitude(b);
        //     int magnitudeOfMax = Math.Max(magnitudeOfFirst, magnitudeOfSecond);
        //     if (magnitudeOfMax > (Math.Min(magnitudeOfFirst, magnitudeOfSecond) + 1))
        //     {
        //         return false;
        //     }

        //     // The values are equal if the difference between the two numbers is smaller than
        //     // 10^(-numberOfDecimalPlaces). We divide by two so that we have half the range
        //     // on each side of the numbers, e.g. if decimalPlaces == 2,
        //     // then 0.01 will equal between 0.00995 and 0.01005, but not 0.0015 and not 0.0095
        //     return Math.Abs(diff) < Math.Pow(10, magnitudeOfMax - decimalPlaces) / 2d;
        // }




        #endregion


        #region 求值

        /// <summary>
        /// Evaluates the minimum distance to the next distinguishable number near the argument value.
        /// 计算到参数值附近的下一个可区分数字的最小距离
        /// </summary>
        /// <param name="value">The value used to determine the minimum distance.</param>
        /// <returns>
        /// Relative Epsilon (positive double or NaN).
        /// </returns>
        /// <remarks>Evaluates the <b>negative</b> epsilon. The more common positive epsilon is equal to two times this negative epsilon.</remarks>
        public static double EpsilonOf(this double value)
        {
            if (double.IsInfinity(value) || double.IsNaN(value))
            {
                return double.NaN;
            }

            long signed64 = BitConverter.DoubleToInt64Bits(value);
            if (signed64 == 0)
            {
                signed64++;
                return BitConverter.Int64BitsToDouble(signed64) - value;
            }
            if (signed64-- < 0)
            {
                return BitConverter.Int64BitsToDouble(signed64) - value;
            }

            return value - BitConverter.Int64BitsToDouble(signed64);
        }

        /// <summary>
        /// Evaluates the minimum distance to the next distinguishable number near the argument value.
        /// </summary>
        /// <param name="value">The value used to determine the minimum distance.</param>
        /// <returns>Relative Epsilon (positive double or NaN)</returns>
        /// <remarks>Evaluates the <b>positive</b> epsilon. See also <see cref="EpsilonOf(double)"/></remarks>
        /// <seealso cref="EpsilonOf(double)"/>
        public static double PositiveEpsilonOf(this double value)
        {
            return 2 * EpsilonOf(value);
        }

        #endregion


        /// <summary>
        /// 数值级数
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The magnitude of the number.</returns>
        public static int Magnitude(this double value)
        {
            // Can't do this with zero because the 10-log of zero doesn't exist.
            if (value.Equals(0.0))
            {
                return 0;
            }

            // Note that we need the absolute value of the input because Log10 doesn't
            // work for negative numbers (obviously).
            double magnitude = Math.Log10(Math.Abs(value));
            var truncated = (int)Math.Truncate(magnitude);

            // To get the right number we need to know if the value is negative or positive
            // truncating a positive number will always give use the correct magnitude
            // truncating a negative number will give us a magnitude that is off by 1 (unless integer)
            return magnitude < 0d && truncated != magnitude
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
        public static double MagnitudeScaleUnit(this double value)
        {
            if (value.Equals(0.0))
            {
                return value;
            }

            int magnitude = value.Magnitude();
            return value * Math.Pow(10, -magnitude);
        }
        public static double ScaleUnitMagnitude(this double value)
        {
            if (value.Equals(0.0))
            {
                return value;
            }

            int magnitude = value.Magnitude();
            return value * Math.Pow(10, -magnitude);
        }

        /// <summary>
        /// 将浮点数递增到下一个由数据类型表示的更大数字
        /// </summary>
        /// <param name="value">The value which needs to be incremented.</param>
        /// <param name="count">How many times the number should be incremented.</param>
        /// <remarks>
        /// The incrementation step length depends on the provided value.
        /// Increment(double.MaxValue) will return positive infinity.
        /// </remarks>
        /// <returns>The next larger floating point value.</returns>
        public static double Increment(this double value, int count = 1)
        {
            if (double.IsInfinity(value) || double.IsNaN(value) || count == 0)
            {
                return value;
            }

            if (count < 0)
            {
                return Decrement(value, -count);
            }

            // Translate the bit pattern of the double to an integer.
            // Note that this leads to:
            // double > 0 --> long > 0, growing as the double value grows
            // double < 0 --> long < 0, increasing in absolute magnitude as the double
            //                          gets closer to zero!
            //                          i.e. 0 - double.epsilon will give the largest long value!
            long intValue = BitConverter.DoubleToInt64Bits(value);
            if (intValue < 0)
            {
                intValue -= count;
            }
            else
            {
                intValue += count;
            }

            // Note that long.MinValue has the same bit pattern as -0.0.
            if (intValue == long.MinValue)
            {
                return 0;
            }

            // Note that not all long values can be translated into double values. There's a whole bunch of them
            // which return weird values like infinity and NaN
            return BitConverter.Int64BitsToDouble(intValue);
        }
        /// <summary>
        /// 将浮点数减到下一个由数据类型表示的较小的数字。
        /// </summary>
        /// <param name="value">The value which should be decremented.</param>
        /// <param name="count">How many times the number should be decremented.</param>
        /// <remarks>
        /// The decrementation step length depends on the provided value.
        /// Decrement(double.MinValue) will return negative infinity.
        /// </remarks>
        /// <returns>The next smaller floating point value.</returns>
        public static double Decrement(this double value, int count = 1)
        {
            if (double.IsInfinity(value) || double.IsNaN(value) || count == 0)
            {
                return value;
            }

            if (count < 0)
            {
                return Increment(value, -count);
            }

            // Translate the bit pattern of the double to an integer.
            // Note that this leads to:
            // double > 0 --> long > 0, growing as the double value grows
            // double < 0 --> long < 0, increasing in absolute magnitude as the double
            //                          gets closer to zero!
            //                          i.e. 0 - double.epsilon will give the largest long value!
            long intValue = BitConverter.DoubleToInt64Bits(value);

            // If the value is zero then we'd really like the value to be -0. So we'll make it -0
            // and then everything else should work out.
            if (intValue == 0)
            {
                // Note that long.MinValue has the same bit pattern as -0.0.
                intValue = long.MinValue;
            }

            if (intValue < 0)
            {
                intValue += count;
            }
            else
            {
                intValue -= count;
            }

            // Note that not all long values can be translated into double values. There's a whole bunch of them
            // which return weird values like infinity and NaN
            return BitConverter.Int64BitsToDouble(intValue);
        }


















        #region Converter

        public static Boolean ToBoolean(this Double v) => Math.Abs(v) > 0;
        public static Char ToChar(this Double v) => (Char)v;
        public static Byte ToByte(this Double v) => (Byte)v;
        public static SByte ToSByte(this Double v) => (SByte)v;
        public static Int16 ToInt16(this Double v) => (Int16)v;
        public static Int32 ToInt32(this Double v) => (Int32)v;
        public static Int64 ToInt64(this Double v) => (Int64)v;
        public static UInt16 ToUInt16(this Double v) => (UInt16)v;
        public static UInt32 ToUInt32(this Double v) => (UInt32)v;
        public static UInt64 ToUInt64(this Double v) => (UInt64)v;
        public static Single ToSingle(this Double v) => (Single)v;
        public static Double ToDouble(this Double v) => (Double)v;
        public static Decimal ToDecimal(this Double v) => (Decimal)v;

        #endregion


        //}}@@@
    }








}
