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
    public static class Int32Extensions
    {

        #region 奇偶数判别

        /// <summary>
        /// 偶数
        /// Find out whether the provided 32 bit integer is an even number.
        /// </summary>
        /// <param name="number">The number to very whether it's even.</param>
        /// <returns>True if and only if it is an even number.</returns>
        public static bool IsEven(this int number)
        {
            return (number & 0x1) == 0x0;
        }

        /// <summary>
        /// 奇数
        /// Find out whether the provided 32 bit integer is an odd number.
        /// </summary>
        /// <param name="number">The number to very whether it's odd.</param>
        /// <returns>True if and only if it is an odd number.</returns>
        public static bool IsOdd(this int number)
        {
            return (number & 0x1) == 0x1;
        }

        #endregion


        /// <summary>
        /// 完美平方（）
        /// Find out whether the provided 32 bit integer is a perfect square, i.e. a square of an integer.
        /// </summary>
        /// <param name="number">The number to very whether it's a perfect square.</param>
        /// <returns>True if and only if it is a perfect square.</returns>
        public static bool IsPerfectSquare(this int number)
        {
            if (number < 0)
            {
                return false;
            }

            int lastHexDigit = number & 0xF;
            if (lastHexDigit > 9)
            {
                return false; // return immediately in 6 cases out of 16.
            }

            if (lastHexDigit == 0 || lastHexDigit == 1 || lastHexDigit == 4 || lastHexDigit == 9)
            {
                int t = (int)Math.Floor(Math.Sqrt(number) + 0.5);
                return (t * t) == number;
            }

            return false;
        }

        /// <summary>
        /// Find out whether the provided 32 bit integer is a perfect power of two.
        /// </summary>
        /// <param name="number">The number to very whether it's a power of two.</param>
        /// <returns>True if and only if it is a power of two.</returns>
        public static bool IsPowerOfTwo(this int number)
        {
            return number > 0 && (number & (number - 1)) == 0x0;
        }

        /// <summary>
        /// 2的幂次方
        /// Raises 2 to the provided integer exponent (0 &lt;= exponent &lt; 31).
        /// </summary>
        /// <param name="exponent">The exponent to raise 2 up to.</param>
        /// <returns>2 ^ exponent.</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static int PowerOfTwo(this int exponent)
        {
            if (exponent < 0 || exponent >= 31)
            {
                throw new ArgumentOutOfRangeException(nameof(exponent));
            }

            return 1 << exponent;
        }

        /// <summary>
        /// Evaluate the binary logarithm of an integer number.
        /// </summary>
        /// <remarks>Two-step method using a De Bruijn-like sequence table lookup.</remarks>
        public static int Log2(this int number)
        {
            number |= number >> 1;
            number |= number >> 2;
            number |= number >> 4;
            number |= number >> 8;
            number |= number >> 16;

            return MultiplyDeBruijnBitPosition[(uint)(number * 0x07C4ACDDU) >> 27];
        }

        /// <summary>
        /// Find the closest perfect power of two that is larger or equal to the provided 32 bit integer.
        /// </summary>
        /// <param name="number">The number of which to find the closest upper power of two.</param>
        /// <returns>A power of two.</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static int CeilingToPowerOfTwo(this int number)
        {
            if (number == Int32.MinValue)
            {
                return 0;
            }

            const int maxPowerOfTwo = 0x40000000;
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
            return number + 1;
        }


        #region Converters

        public static bool ToBoolean(this Int32 v) => v != 0;
        public static Char ToChar(this Int32 v) => (Char)(v);
        public static Byte ToByte(this Int32 v) => (Byte)v;
        public static SByte ToSByte(this Int32 v) => (SByte)v;
        public static Int16 ToInt16(this Int32 v) => (Int16)v;
        public static Int32 ToInt32(this Int32 v) => (Int32)v;
        public static Int64 ToInt64(this Int32 v) => (Int64)v;
        public static UInt16 ToUInt16(this Int32 v) => (UInt16)v;
        public static UInt32 ToUInt32(this Int32 v) => (UInt32)v;
        public static UInt64 ToUInt64(this Int32 v) => (UInt64)v;
        public static Single ToSingle(this Int32 v) => (Single)v;
        public static Double ToDouble(this Int32 v) => (Double)v;
        public static Decimal ToDecimal(this Int32 v) => (Decimal)v;

        #endregion



        static readonly int[] MultiplyDeBruijnBitPosition = new int[32]
        {
            0, 9, 1, 10, 13, 21, 2, 29, 11, 14, 16, 18, 22, 25, 3, 30,
            8, 12, 20, 28, 15, 17, 24, 7, 19, 27, 23, 6, 26, 5, 4, 31
        };


        //}}@@@
    }


}
