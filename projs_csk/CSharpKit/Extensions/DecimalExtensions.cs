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
    public static class DecimalExtensions
    {
        #region Convert
        
        public static Boolean ToBoolean(this Decimal v) => Math.Abs(v) > 0;
        public static Char ToChar(this Decimal v) => (Char)v;
        public static Byte ToByte(this Decimal v) => (Byte)v;
        public static SByte ToSByte(this Decimal v) => (SByte)v;
        public static Int16 ToInt16(this Decimal v) => (Int16)v;
        public static Int32 ToInt32(this Decimal v) => (Int32)v;
        public static Int64 ToInt64(this Decimal v) => (Int64)v;
        public static UInt16 ToUInt16(this Decimal v) => (UInt16)v;
        public static UInt32 ToUInt32(this Decimal v) => (UInt32)v;
        public static UInt64 ToUInt64(this Decimal v) => (UInt64)v;
        public static Single ToSingle(this Decimal v) => (Single)v;
        public static Double ToDouble(this Decimal v) => (Double)v;
        public static Decimal ToDecimal(this Decimal v) => (Decimal)v;
            
        #endregion

        //}}@@@
    }


}
