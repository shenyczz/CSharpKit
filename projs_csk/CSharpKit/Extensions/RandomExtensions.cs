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
using System.Collections.Generic;

namespace CSharpKit
{
    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a random boolean.
        /// </summary>
        /// <param name="rnd">The random number generator.</param>
        /// <remarks>
        /// This extension is thread-safe if and only if called on an random number
        /// generator provided by Math.NET Numerics or derived from the RandomSource class.
        /// </remarks>
        public static bool NextBoolean(this Random rnd)
        {
            return rnd.NextDouble() >= 0.5;
        }

        /// <summary>
        /// Returns an array of uniform random bytes.
        /// </summary>
        /// <param name="rnd">The random number generator.</param>
        /// <param name="count">The size of the array to fill.</param>
        /// <remarks>
        /// This extension is thread-safe if and only if called on an random number
        /// generator provided by Math.NET Numerics or derived from the RandomSource class.
        /// </remarks>
        public static byte[] NextBytes(this Random rnd, int count)
        {
            var values = new byte[count];
            rnd.NextBytes(values);
            return values;
        }

        /// <summary>
        /// Returns a nonnegative random number less than <see cref="Int64.MaxValue"/>.
        /// </summary>
        /// <param name="rnd">The random number generator.</param>
        /// <returns>
        /// A 64-bit signed integer greater than or equal to 0, and less than <see cref="Int64.MaxValue"/>; that is,
        /// the range of return values includes 0 but not <see cref="Int64.MaxValue"/>.
        /// </returns>
        /// <seealso cref="NextFullRangeInt64"/>
        /// <remarks>
        /// This extension is thread-safe if and only if called on an random number
        /// generator provided by Math.NET Numerics or derived from the RandomSource class.
        /// </remarks>
        public static long NextInt64(this Random rnd)
        {
            var buffer = new byte[8];

            rnd.NextBytes(buffer);
            var candidate = BitConverter.ToInt64(buffer, 0);

            // wrap negative numbers around, mapping every negative number to a distinct nonnegative number
            // MinValue -> 0, -1 -> MaxValue
            candidate &= long.MaxValue;

            // skip candidate if it is MaxValue. Recursive since rare.
            return (candidate == long.MaxValue) ? rnd.NextInt64() : candidate;
        }

        /// <summary>
        /// Returns a random number of the full Int32 range.
        /// </summary>
        /// <param name="rnd">The random number generator.</param>
        /// <returns>
        /// A 32-bit signed integer of the full range, including 0, negative numbers,
        /// <see cref="Int32.MaxValue"/> and <see cref="Int32.MinValue"/>.
        /// </returns>
        /// <seealso cref="System.Random.Next()"/>
        /// <remarks>
        /// This extension is thread-safe if and only if called on an random number
        /// generator provided by Math.NET Numerics or derived from the RandomSource class.
        /// </remarks>
        public static int NextFullRangeInt32(this Random rnd)
        {
            var buffer = new byte[4];
            rnd.NextBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        /// <summary>
        /// Returns a random number of the full Int64 range.
        /// </summary>
        /// <param name="rnd">The random number generator.</param>
        /// <returns>
        /// A 64-bit signed integer of the full range, including 0, negative numbers,
        /// <see cref="Int64.MaxValue"/> and <see cref="Int64.MinValue"/>.
        /// </returns>
        /// <seealso cref="NextInt64"/>
        /// <remarks>
        /// This extension is thread-safe if and only if called on an random number
        /// generator provided by Math.NET Numerics or derived from the RandomSource class.
        /// </remarks>
        public static long NextFullRangeInt64(this Random rnd)
        {
            var buffer = new byte[8];
            rnd.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// Returns a nonnegative decimal floating point random number less than 1.0.
        /// </summary>
        /// <param name="rnd">The random number generator.</param>
        /// <returns>
        /// A decimal floating point number greater than or equal to 0.0, and less than 1.0; that is,
        /// the range of return values includes 0.0 but not 1.0.
        /// </returns>
        /// <remarks>
        /// This extension is thread-safe if and only if called on an random number
        /// generator provided by Math.NET Numerics or derived from the RandomSource class.
        /// </remarks>
        public static decimal NextDecimal(this Random rnd)
        {
            decimal candidate;

            // 50.049 % chance that the number is below 1.0. Try until we have one.
            // Guarantees that any decimal in the interval can
            // indeed be reached, with uniform probability.
            do
            {
                candidate = new decimal(
                    rnd.NextFullRangeInt32(),
                    rnd.NextFullRangeInt32(),
                    rnd.NextFullRangeInt32(),
                    false,
                    28);
            }
            while (candidate >= 1.0m);

            return candidate;
        }


        static IEnumerable<int> NextInt32SequenceEnumerable(Random rnd, int minInclusive, int maxExclusive)
        {
            while (true)
            {
                yield return rnd.Next(minInclusive, maxExclusive);
            }
        }
        static IEnumerable<double> NextDoubleSequenceEnumerable(Random rnd)
        {
            while (true)
            {
                yield return rnd.NextDouble();
            }
        }

        //}}@@@
    }

}

