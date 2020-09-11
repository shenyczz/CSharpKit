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
    /// Byte
    /// </summary>
    public static class UInt08Extensions
    {
        public static Boolean ToBoolean(this Byte v) => v > 0;
        public static Char ToChar(this Byte v) => (Char)v;
        public static Byte ToByte(this Byte v) => (Byte)v;
        public static SByte ToSByte(this Byte v) => (SByte)v;
        public static Int16 ToInt16(this Byte v) => (Int16)v;
        public static Int32 ToInt32(this Byte v) => (Int32)v;
        public static Int64 ToInt64(this Byte v) => (Int64)v;
        public static UInt16 ToUInt16(this Byte v) => (UInt16)v;
        public static UInt32 ToUInt32(this Byte v) => (UInt32)v;
        public static UInt64 ToUInt64(this Byte v) => (UInt64)v;
        public static Single ToSingle(this Byte v) => (Single)v;
        public static Double ToDouble(this Byte v) => (Double)v;
        public static Decimal ToDecimal(this Byte v) => (Decimal)v;




        //}}@@@
    }


}
