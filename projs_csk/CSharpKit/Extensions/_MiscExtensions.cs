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
using System.Diagnostics;

namespace CSharpKit
{
    public static class _MiscExtensions
    {
        //bool、sbyte、byte、short、ushort、int、uint、long、ulong、char、float、double、decimal

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks>
        /// dotnet 支持的类型参数约束有以下五种：
        /// where T : struct                ----  T 必须是一个值类型
        /// where T : class                 ----  T 必须是一个引用类型
        /// where T : new()                 ----  T 必须要有一个无参构造函数, (即他要求类型参数必须提供一个无参数的构造函数)
        /// where T : NameOfBaseClass       ----  T 必须继承名为NameOfBaseClass的类
        /// where T : NameOfInterface       ----  T 必须实现名为NameOfInterface的接口
        /// </remarks>
        public static int IndexOf<T>(this T[] array, T value) where T : class
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i] == value)
                    return i;

            return -1;
        }

        public static T GetValueOrDefault<T>(this WeakReference wr)
        {
            if (wr == null || !wr.IsAlive)
                return default(T);

            return (T)wr.Target;
        }









        #region SwapByteOrder - 转换字节序

        public static T SwapByteOrder<T>(this T value)
        {
            T result = value;

            try
            {
                Type ttype = typeof(T);

                if (ttype == typeof(Byte) || ttype == typeof(SByte))
                {
                    var vtemp = Convert.ToByte(value);
                    result = (T)Convert.ChangeType((vtemp).SwapByteOrder(), ttype);
                }
                else if (ttype == typeof(Int16))
                {
                    var vtemp = Convert.ToInt16(value);
                    result = (T)Convert.ChangeType((vtemp).SwapByteOrder(), ttype);
                }
                else if (ttype == typeof(UInt16))
                {
                    var vtemp = Convert.ToUInt16(value);
                    result = (T)Convert.ChangeType((vtemp).SwapByteOrder(), ttype);
                }
                else if (ttype == typeof(Int32))
                {
                    var vtemp = Convert.ToInt32(value);
                    result = (T)Convert.ChangeType((vtemp).SwapByteOrder(), ttype);
                }
                else if (ttype == typeof(UInt32))
                {
                    var vtemp = Convert.ToUInt32(value);
                    result = (T)Convert.ChangeType((vtemp).SwapByteOrder(), ttype);
                }
                else if (ttype == typeof(Int64))
                {
                    var vtemp = Convert.ToInt64(value);
                    result = (T)Convert.ChangeType((vtemp).SwapByteOrder(), ttype);
                }
                else if (ttype == typeof(UInt64))
                {
                    var vtemp = Convert.ToUInt64(value);
                    result = (T)Convert.ChangeType((vtemp).SwapByteOrder(), ttype);
                }
                else if (ttype == typeof(Single))
                {
                    var vtemp = Convert.ToSingle(value);
                    result = (T)Convert.ChangeType((vtemp).SwapByteOrder(), ttype);
                }
                else if (ttype == typeof(Double))
                {
                    var vtemp = Convert.ToDouble(value);
                    result = (T)Convert.ChangeType((vtemp).SwapByteOrder(), ttype);
                }
                else
                {
                    result = value;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return result;
        }

        private static Byte SwapByteOrder(this Byte value)
        {
            return value;
        }
        private static SByte SwapByteOrder(this SByte value)
        {
            return value;
        }
        private static Int16 SwapByteOrder(this Int16 value)
        {
            return (Int16)SwapByteOrder((UInt16)value);
        }
        private static UInt16 SwapByteOrder(this UInt16 value)
        {
            return (UInt16)((0x00FF & (value >> 8)) | (0xFF00 & (value << 8)));
        }
        private static Int32 SwapByteOrder(this Int32 value)
        {
            return (Int32)SwapByteOrder((UInt32)value);
        }
        private static UInt32 SwapByteOrder(this UInt32 value)
        {
            UInt32 swapped = ((0x000000FF) & (value >> 24)
                             | (0x0000FF00) & (value >> 8)
                             | (0x00FF0000) & (value << 8)
                             | (0xFF000000) & (value << 24));
            return swapped;
        }
        private static Int64 SwapByteOrder(this Int64 value)
        {
            return (Int64)SwapByteOrder((UInt64)value);
        }
        private static UInt64 SwapByteOrder(this UInt64 value)
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
        private static Single SwapByteOrder(this Single value)
        {
            byte[] rows = BitConverter.GetBytes(value);
            Int32 int_swaped = BitConverter.ToInt32(rows, 0).SwapByteOrder();
            rows = BitConverter.GetBytes(int_swaped);
            return BitConverter.ToSingle(rows, 0);
        }
        private static Double SwapByteOrder(this Double value)
        {
            Int64 bits = BitConverter.DoubleToInt64Bits(value);
            bits = SwapByteOrder(bits);
            return BitConverter.Int64BitsToDouble(bits);
        }

        #endregion


        //@@@
    }










}
