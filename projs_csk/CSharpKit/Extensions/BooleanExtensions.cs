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
    public static class BooleanExtensions
    {

        #region Converter
        
        public static Boolean ToBoolean(this bool v) => v;
        public static Char ToChar(this bool v) => (char)(v ? 1 : 0);
        public static Byte ToByte(this bool v) => (byte)(v ? 1 : 0);
        public static SByte ToSByte(this bool v) => (sbyte)(v ? 1 : 0);
        public static Int16 ToInt16(this bool v) => (Int16)(v ? 1 : 0);
        public static UInt16 ToUInt16(this bool v) => (UInt16)(v ? 1 : 0);
        public static Int32 ToInt32(this bool v) => (Int32)(v ? 1 : 0);
        public static UInt32 ToUInt32(this bool v) => (UInt32)(v ? 1 : 0);
        public static Int64 ToInt64(this bool v) => (Int64)(v ? 1 : 0);
        public static UInt64 ToUInt64(this bool v) => (UInt64)(v ? 1 : 0);
        public static Single ToSingle(this bool v) => (Single)(v ? 1 : 0);
        public static Double ToDouble(this bool v) => (Double)(v ? 1 : 0);
        public static Decimal ToDecimal(this bool v) => (Decimal)(v ? 1 : 0);

        #endregion

        public static string ToString(this bool v, string format, IFormatProvider formatProvider)
        {
            return v ? "True" : "False";
        }


        //}}@@@
    }


}
