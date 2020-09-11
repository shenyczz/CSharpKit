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
    /// BytesEncoder - 字节编码器
    /// </summary>
    public static class BytesEncoder
    {
        /// <summary>
        /// Returns the value encoded in Big Endian (PPC, XDR) format.
        /// </summary>
        /// <param name="value">Value to encode.</param>
        /// <returns>Big-endian encoded value.</returns>
        public static UInt16 GetBigEndian(UInt16 value)
        {
            return BitConverter.IsLittleEndian
                       ? swapByteOrder(value)
                       : value;
        }
        public static Int16 GetBigEndian(Int16 value)
        {
            return BitConverter.IsLittleEndian
                       ? swapByteOrder(value)
                       : value;
        }

        /// <summary>
        /// Returns the value encoded in Little Endian (x86, NDR) format.
        /// </summary>
        /// <param name="value">Value to encode.</param>
        /// <returns>Little-endian encoded value.</returns>
        public static UInt16 GetLittleEndian(UInt16 value)
        {
            return BitConverter.IsLittleEndian
                       ? value
                       : swapByteOrder(value);
        }
        public static Int16 GetLittleEndian(Int16 value)
        {
            return BitConverter.IsLittleEndian
                       ? value
                       : swapByteOrder(value);
        }

        /// <summary>
        /// Returns the value encoded in Big Endian (PPC, XDR) format.
        /// </summary>
        /// <param name="value">Value to encode.</param>
        /// <returns>Big-endian encoded value.</returns>
        public static UInt32 GetBigEndian(UInt32 value)
        {
            return BitConverter.IsLittleEndian
                       ? swapByteOrder(value)
                       : value;
        }
        /// <summary>
        /// Returns the value encoded in Big Endian (PPC, XDR) format.
        /// </summary>
        /// <param name="value">Value to encode.</param>
        /// <returns>Big-endian encoded value.</returns>
        public static Int32 GetBigEndian(Int32 value)
        {
            if (BitConverter.IsLittleEndian)
            {
                return swapByteOrder(value);
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Returns the value encoded in Little Endian (x86, NDR) format.
        /// </summary>
        /// <param name="value">Value to encode.</param>
        /// <returns>Little-endian encoded value.</returns>
        public static Int32 GetLittleEndian(Int32 value)
        {
            return BitConverter.IsLittleEndian
                       ? value
                       : swapByteOrder(value);
        }

        /// <summary>
        /// Returns the value encoded in Little Endian (x86, NDR) format.
        /// </summary>
        /// <param name="value">Value to encode.</param>
        /// <returns>Little-endian encoded value.</returns>
        public static UInt32 GetLittleEndian(UInt32 value)
        {
            return BitConverter.IsLittleEndian
                       ? value
                       : swapByteOrder(value);
        }

        public static UInt64 GetBigEndian(UInt64 value)
        {
            return BitConverter.IsLittleEndian
                       ? swapByteOrder(value)
                       : value;
        }
        public static Int64 GetBigEndian(Int64 value)
        {
            return BitConverter.IsLittleEndian
                       ? swapByteOrder(value)
                       : value;
        }

        public static UInt64 GetLittleEndian(UInt64 value)
        {
            return BitConverter.IsLittleEndian
                       ? value
                       : swapByteOrder(value);
        }
        public static Int64 GetLittleEndian(Int64 value)
        {
            return BitConverter.IsLittleEndian
                       ? value
                       : swapByteOrder(value);
        }

        public static float GetBigEndian(float value)
        {
            return BitConverter.IsLittleEndian
                       ? swapByteOrder(value)
                       : value;
        }
        public static float GetLittleEndian(float value)
        {
            return BitConverter.IsLittleEndian
                       ? value
                       : swapByteOrder(value);
        }


        /// <summary>
        /// Returns the value encoded in Big Endian (PPC, XDR) format.
        /// </summary>
        /// <param name="value">Value to encode.</param>
        /// <returns>Big-endian encoded value.</returns>
        public static Double GetBigEndian(Double value)
        {
            return BitConverter.IsLittleEndian
                       ? swapByteOrder(value)
                       : value;
        }

        /// <summary>
        /// Returns the value encoded in Little Endian (x86, NDR) format.
        /// </summary>
        /// <param name="value">Value to encode.</param>
        /// <returns>Little-endian encoded value.</returns>
        public static Double GetLittleEndian(Double value)
        {
            return BitConverter.IsLittleEndian
                       ? value
                       : swapByteOrder(value);
        }

        #region --私有辅助函数--

        /// <summary>
        /// Swaps the byte order of a <see cref="UInt16"/>.
        /// </summary>
        /// <param name="value"><see cref="UInt16"/> to swap the bytes of.</param>
        /// <returns>Byte order swapped <see cref="UInt16"/>.</returns>
        private static UInt16 swapByteOrder(UInt16 value)
        {
            return (UInt16)((0x00FF & (value >> 8)) | (0xFF00 & (value << 8)));
        }
        private static Int16 swapByteOrder(Int16 value)
        {
            return (Int16)((0x00FF & (value >> 8)) | (0xFF00 & (value << 8)));
        }

        /// <summary>
        /// Swaps the byte order of a <see cref="UInt32"/>.
        /// </summary>
        /// <param name="value"><see cref="UInt32"/> to swap the bytes of.</param>
        /// <returns>Byte order swapped <see cref="UInt32"/>.</returns>
        private static UInt32 swapByteOrder(UInt32 value)
        {
            UInt32 swapped = ((0x000000FF) & (value >> 24)
                             | (0x0000FF00) & (value >> 8)
                             | (0x00FF0000) & (value << 8)
                             | (0xFF000000) & (value << 24));
            return swapped;
        }

        /// <summary>
        /// Swaps the Byte order of an <see cref="Int32"/>.
        /// </summary>
        /// <param name="value"><see cref="Int32"/> to swap the bytes of.</param>
        /// <returns>Byte order swapped <see cref="Int32"/>.</returns>
        private static Int32 swapByteOrder(Int32 value)
        {
            return (Int32)swapByteOrder((UInt32)value);
        }

        /// <summary>
        /// Swaps the byte order of a <see cref="Int64"/>.
        /// </summary>
        /// <param name="value"><see cref="Int64"/> to swap the bytes of.</param>
        /// <returns>Byte order swapped <see cref="Int64"/>.</returns>
        private static UInt64 swapByteOrder(UInt64 value)
        {
            UInt64 swapped = ((0x00000000000000FF) & (value >> 56)
                             | (0x000000000000FF00) & (value >> 40)
                             | (0x0000000000FF0000) & (value >> 24)
                             | (0x00000000FF000000) & (value >> 8)
                             | (0x000000FF00000000) & (value << 8)
                             | (0x0000FF0000000000) & (value << 24)
                             | (0x00FF000000000000) & (value << 40)
                             | (0xFF00000000000000) & (value << 56));
            return swapped;
        }
        private static Int64 swapByteOrder(Int64 value)
        {
            return (Int64)swapByteOrder((UInt64)value);
        }

        private static float swapByteOrder(float value)
        {
            return (float)swapByteOrder((double)value);
        }


        /// <summary>
        /// Swaps the byte order of a <see cref="Double"/> (double precision IEEE 754)
        /// </summary>
        /// <param name="value"><see cref="Double"/> to swap.</param>
        /// <returns>Byte order swapped <see cref="Double"/> value.</returns>
        private static Double swapByteOrder(Double value)
        {
            Int64 bits = BitConverter.DoubleToInt64Bits(value);
            bits = swapByteOrder(bits);
            return BitConverter.Int64BitsToDouble(bits);
        }

        #endregion

        //}}@@@
    }




}
