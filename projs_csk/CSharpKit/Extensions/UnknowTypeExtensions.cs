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
    /// <summary>
    /// unmanaged => bool、sbyte、byte、short、ushort、int、uint、long、ulong、char、float、double、decimal
    /// </summary>
    public static class UnknowTypeExtensions
    {
        public static bool BoolValue<T>(this T t) => t.Boolean();
        public static byte ByteValue<T>(this T t) => t.Byte();
        public static sbyte SByteValue<T>(this T t) => t.SByte();
        public static char CharValue<T>(this T t) { return Char(t); }
        public static short Int16Value<T>(this T t) { return Int16(t); }
        public static ushort UInt16Value<T>(this T t) => t.UInt16();
        public static int Int32Value<T>(this T t) { return Int32(t); }
        public static uint UInt32Value<T>(this T t) => t.UInt32();
        public static long Int64Value<T>(this T t) { return Int64(t); }
        public static ulong UInt64Value<T>(this T t) => t.UInt64();
        public static float SingleValue<T>(this T t) { return Single(t); }
        public static double DoubleValue<T>(this T t) { return Double(t); }
        public static decimal DecimalValue<T>(this T t) { return Decimal(t); }


        static bool Boolean<T>(this T t)
        {
            bool result = default;

            try
            {
                var vtemp = result;

                Type type = t.GetType();
                switch (Type.GetTypeCode(t.GetType()))
                {
                    case TypeCode.Boolean:
                        vtemp = Convert.ToBoolean(t);
                        break;

                    case TypeCode.Char:
                        vtemp = ((Char)Convert.ChangeType(t, TypeCode.Char)).ToBoolean();
                        break;

                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                        vtemp = Convert.ToBoolean(t);
                        break;

                    case TypeCode.String:
                        vtemp = ((String)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                        break;

                    default:
                        vtemp = false;
                        break;
                }

                result = vtemp;
            }
            catch (Exception)
            {
            }

            return result;
        }
        static Byte Byte<T>(this T t)
        {
            Byte result = default;

            try
            {
                var vtemp = result;

                Type type = t.GetType();
                switch (Type.GetTypeCode(t.GetType()))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    case TypeCode.Byte:
                        vtemp = Convert.ToByte(t);
                        break;

                    case TypeCode.SByte:
                        vtemp = ((SByte)Convert.ChangeType(t, TypeCode.SByte)).ToByte();
                        break;

                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                        vtemp = Convert.ToByte(t);
                        break;

                    default: break;
                }

                result = vtemp;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return result;
        }
        static SByte SByte<T>(this T t)
        {
            SByte result = default;

            try
            {
                var vtemp = result;

                Type type = t.GetType();
                switch (Type.GetTypeCode(t.GetType()))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                        vtemp = Convert.ToSByte(t);
                        break;

                    //case TypeCode.Char:
                    //    vtemp = ((Char)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    //case TypeCode.String:
                    //    vtemp = ((String)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    default: break;
                }

                result = vtemp;
            }
            catch (Exception)
            {
            }

            return result;
        }
        static Char Char<T>(this T t)
        {
            Char result = default;

            try
            {
                var vtemp = result;

                Type type = t.GetType();
                switch (Type.GetTypeCode(t.GetType()))
                {
                    case TypeCode.Boolean:
                        vtemp = ((bool)Convert.ChangeType(t, TypeCode.Boolean)).ToChar();
                        break;

                    case TypeCode.Char:
                        vtemp = ((Char)Convert.ChangeType(t, TypeCode.Char)).ToChar();
                        break;
                    case TypeCode.SByte:
                        vtemp = ((SByte)Convert.ChangeType(t, TypeCode.SByte)).ToChar();
                        break;
                    case TypeCode.Byte:
                        vtemp = ((Byte)Convert.ChangeType(t, TypeCode.Byte)).ToChar();
                        break;
                    case TypeCode.Int16:
                        vtemp = ((Int16)Convert.ChangeType(t, TypeCode.Int16)).ToChar();
                        break;
                    case TypeCode.UInt16:
                        vtemp = ((UInt16)Convert.ChangeType(t, TypeCode.UInt16)).ToChar();
                        break;
                    case TypeCode.Int32:
                        vtemp = ((Int32)Convert.ChangeType(t, TypeCode.Int32)).ToChar();
                        break;
                    case TypeCode.UInt32:
                        vtemp = ((UInt32)Convert.ChangeType(t, TypeCode.UInt32)).ToChar();
                        break;
                    case TypeCode.Int64:
                        vtemp = ((Int64)Convert.ChangeType(t, TypeCode.Int64)).ToChar();
                        break;
                    case TypeCode.UInt64:
                        vtemp = ((UInt64)Convert.ChangeType(t, TypeCode.UInt64)).ToChar();
                        break;
                    case TypeCode.Single:
                        vtemp = ((Single)Convert.ChangeType(t, TypeCode.Single)).ToChar();
                        break;
                    case TypeCode.Double:
                        vtemp = ((Double)Convert.ChangeType(t, TypeCode.Double)).ToChar();
                        break;
                    case TypeCode.Decimal:
                        vtemp = ((Decimal)Convert.ChangeType(t, TypeCode.Decimal)).ToChar();
                        break;
                    case TypeCode.DateTime:
                        vtemp = ((DateTime)Convert.ChangeType(t, TypeCode.DateTime)).ToChar();
                        break;
                    case TypeCode.String:
                        vtemp = ((String)Convert.ChangeType(t, TypeCode.String)).ToChar();
                        break;

                    default:
                        break;
                }

                result = vtemp;
            }
            catch (Exception)
            {
            }

            return result;
        }
        static Int16 Int16<T>(this T t)
        {
            Int16 result = default;

            try
            {
                var vtemp = result;

                Type type = t.GetType();
                switch (Type.GetTypeCode(t.GetType()))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                        vtemp = Convert.ToInt16(t);
                        break;

                    //case TypeCode.Char:
                    //    vtemp = ((Char)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    //case TypeCode.String:
                    //    vtemp = ((String)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    default: break;
                }

                result = vtemp;
            }
            catch (Exception)
            {
            }

            return result;
        }
        static UInt16 UInt16<T>(this T t)
        {
            UInt16 result = default;

            try
            {
                var vtemp = result;

                Type type = t.GetType();
                switch (Type.GetTypeCode(t.GetType()))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                        vtemp = Convert.ToUInt16(t);
                        break;

                    //case TypeCode.Char:
                    //    vtemp = ((Char)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    //case TypeCode.String:
                    //    vtemp = ((String)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    default: break;
                }

                result = vtemp;
            }
            catch (Exception)
            {
            }

            return result;
        }
        static Int32 Int32<T>(this T t)
        {
            Int32 result = default;

            try
            {
                var vtemp = result;

                Type type = t.GetType();
                switch (Type.GetTypeCode(t.GetType()))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                        vtemp = Convert.ToInt32(t);
                        break;

                    //case TypeCode.Char:
                    //    vtemp = ((Char)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    //case TypeCode.String:
                    //    vtemp = ((String)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    default: break;
                }

                result = vtemp;
            }
            catch (Exception)
            {
            }

            return result;
        }
        static UInt32 UInt32<T>(this T t)
        {
            UInt32 result = default;

            try
            {
                var vtemp = result;

                Type type = t.GetType();
                switch (Type.GetTypeCode(t.GetType()))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                        vtemp = Convert.ToUInt32(t);
                        break;

                    //case TypeCode.Char:
                    //    vtemp = ((Char)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    //case TypeCode.String:
                    //    vtemp = ((String)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    default: break;
                }

                result = vtemp;
            }
            catch (Exception)
            {
            }

            return result;
        }
        static Int64 Int64<T>(this T t)
        {
            Int64 result = default;

            try
            {
                var vtemp = result;

                Type type = t.GetType();
                switch (Type.GetTypeCode(t.GetType()))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                        vtemp = Convert.ToInt64(t);
                        break;

                    //case TypeCode.Char:
                    //    vtemp = ((Char)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    //case TypeCode.String:
                    //    vtemp = ((String)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    default: break;
                }

                result = vtemp;
            }
            catch (Exception)
            {
            }

            return result;
        }
        static UInt64 UInt64<T>(this T t)
        {
            UInt64 result = default;

            try
            {
                var vtemp = result;

                Type type = t.GetType();
                switch (Type.GetTypeCode(t.GetType()))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                        vtemp = Convert.ToUInt64(t);
                        break;

                    //case TypeCode.Char:
                    //    vtemp = ((Char)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    //case TypeCode.String:
                    //    vtemp = ((String)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    default: break;
                }

                result = vtemp;
            }
            catch (Exception)
            {
            }

            return result;
        }
        static Single Single<T>(this T t)
        {
            Single result = default;

            try
            {
                var vtemp = result;

                Type type = t.GetType();
                switch (Type.GetTypeCode(t.GetType()))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                        vtemp = Convert.ToSingle(t);
                        break;

                    //case TypeCode.Char:
                    //    vtemp = ((Char)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    //case TypeCode.String:
                    //    vtemp = ((String)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    default: break;
                }

                result = vtemp;
            }
            catch (Exception)
            {
            }

            return result;
        }
        static Double Double<T>(this T t)
        {
            Double result = default;

            try
            {
                var vtemp = result;

                Type type = t.GetType();
                switch (Type.GetTypeCode(t.GetType()))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                        vtemp = Convert.ToDouble(t);
                        break;

                    //case TypeCode.Char:
                    //    vtemp = ((Char)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    //case TypeCode.String:
                    //    vtemp = ((String)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    default: break;
                }

                result = vtemp;
            }
            catch (Exception)
            {
            }

            return result;
        }
        static Decimal Decimal<T>(this T t)
        {
            Decimal result = default;

            try
            {
                var vtemp = result;

                Type type = t.GetType();
                switch (Type.GetTypeCode(t.GetType()))
                {
                    case TypeCode.Boolean:
                    case TypeCode.Char:
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                        vtemp = Convert.ToDecimal(t);
                        break;

                    //case TypeCode.Char:
                    //    vtemp = ((Char)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    //case TypeCode.String:
                    //    vtemp = ((String)Convert.ChangeType(t, TypeCode.String)).ToBoolean();
                    //    break;

                    default: break;
                }

                result = vtemp;
            }
            catch (Exception)
            {
            }

            return result;
        }


        //public static bool GtZero<T>(this T t) { return t.Single() > 0f; }
        //public static bool LtZero<T>(this T t) { return t.Single() < 0f; }
        //public static bool EqualZero<T>(this T t) { return !(t.NotEqualZero()); }
        //public static bool NotEqualZero<T>(this T t) { return (t.GtZero() || t.LtZero()); }







        //}}@@@
    }


}
