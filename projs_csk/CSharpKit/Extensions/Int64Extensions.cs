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
    public static class Int64Extensions
    {
        public static bool ToBoolean(this Int64 v) => v != 0;
        public static Char ToChar(this Int64 v) => (Char)v;
        public static Byte ToByte(this Int64 v) => (Byte)v;
        public static SByte ToSByte(this Int64 v) => (SByte)v;
        public static Int16 ToInt16(this Int64 v) => (Int16)v;
        public static Int32 ToInt32(this Int64 v) => (Int32)v;
        public static Int64 ToInt64(this Int64 v) => (Int64)v;
        public static UInt16 ToUInt16(this Int64 v) => (UInt16)v;
        public static UInt32 ToUInt32(this Int64 v) => (UInt32)v;
        public static UInt64 ToUInt64(this Int64 v) => (UInt64)v;
        public static Single ToSingle(this Int64 v) => (Single)v;
        public static Double ToDouble(this Int64 v) => (Double)v;
        public static Decimal ToDecimal(this Int64 v) => (Decimal)v;


        //}}@@@
    }


}
