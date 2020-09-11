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
    public static class UInt64Extensions
    {

        /// <summary>
        /// Find out whether the provided 64 bit integer is an even number.
        /// </summary>
        /// <param name="number">The number to very whether it's even.</param>
        /// <returns>True if and only if it is an even number.</returns>
        public static bool IsEven(this long number)
        {
            return (number & 0x1) == 0x0;
        }

        /// <summary>
        /// Find out whether the provided 64 bit integer is an odd number.
        /// </summary>
        /// <param name="number">The number to very whether it's odd.</param>
        /// <returns>True if and only if it is an odd number.</returns>
        public static bool IsOdd(this long number)
        {
            return (number & 0x1) == 0x1;
        }

        /// <summary>
        /// Find out whether the provided 64 bit integer is a perfect power of two.
        /// </summary>
        /// <param name="number">The number to very whether it's a power of two.</param>
        /// <returns>True if and only if it is a power of two.</returns>
        public static bool IsPowerOfTwo(this long number)
        {
            return number > 0 && (number & (number - 1)) == 0x0;
        }

        /// <summary>
        /// Find out whether the provided 64 bit integer is a perfect square, i.e. a square of an integer.
        /// </summary>
        /// <param name="number">The number to very whether it's a perfect square.</param>
        /// <returns>True if and only if it is a perfect square.</returns>
        public static bool IsPerfectSquare(this long number)
        {
            if (number < 0)
            {
                return false;
            }

            int lastHexDigit = (int)(number & 0xF);
            if (lastHexDigit > 9)
            {
                return false; // return immediately in 6 cases out of 16.
            }

            if (lastHexDigit == 0 || lastHexDigit == 1 || lastHexDigit == 4 || lastHexDigit == 9)
            {
                long t = (long)Math.Floor(Math.Sqrt(number) + 0.5);
                return (t * t) == number;
            }

            return false;
        }

        /// <summary>
        /// Raises 2 to the provided integer exponent (0 &lt;= exponent &lt; 63).
        /// </summary>
        /// <param name="exponent">The exponent to raise 2 up to.</param>
        /// <returns>2 ^ exponent.</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static long PowerOfTwo(this long exponent)
        {
            if (exponent < 0 || exponent >= 63)
            {
                throw new ArgumentOutOfRangeException(nameof(exponent));
            }

            return ((long)1) << (int)exponent;
        }

        /// <summary>
        /// Find the closest perfect power of two that is larger or equal to the provided
        /// 64 bit integer.
        /// </summary>
        /// <param name="number">The number of which to find the closest upper power of two.</param>
        /// <returns>A power of two.</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static long CeilingToPowerOfTwo(this long number)
        {
            if (number == Int64.MinValue)
            {
                return 0;
            }

            const long maxPowerOfTwo = 0x4000000000000000;
            if (number > maxPowerOfTwo)
            {
                throw new ArgumentOutOfRangeException(nameof(number));
            }

            number--;
            number |= number >> 1;
            number |= number >> 2;
            number |= number >> 4;
            number |= number >> 8;
            number |= number >> 16;
            number |= number >> 32;
            return number + 1;
        }


        #region Converters

        public static bool ToBoolean(this UInt64 v) => v != 0;
        public static Char ToChar(this UInt64 v) => (Char)v;
        public static Byte ToByte(this UInt64 v) => (Byte)v;
        public static SByte ToSByte(this UInt64 v) => (SByte)v;
        public static Int16 ToInt16(this UInt64 v) => (Int16)v;
        public static Int32 ToInt32(this UInt64 v) => (Int32)v;
        public static Int64 ToInt64(this UInt64 v) => (Int64)v;
        public static UInt16 ToUInt16(this UInt64 v) => (UInt16)v;
        public static UInt32 ToUInt32(this UInt64 v) => (UInt32)v;
        public static UInt64 ToUInt64(this UInt64 v) => (UInt64)v;
        public static Single ToSingle(this UInt64 v) => (Single)v;
        public static Double ToDouble(this UInt64 v) => (Double)v;
        public static Decimal ToDecimal(this UInt64 v) => (Decimal)v;

        #endregion



        //}}@@@
    }


}
