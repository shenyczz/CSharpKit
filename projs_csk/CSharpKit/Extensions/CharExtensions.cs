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
    public static class CharExtensions
    {
        public static Boolean ToBoolean(this Char c)
        {
            bool result = default;

            switch (c)
            {
                case '0':
                case 'f':
                case 'F':
                    result = false;
                    break;

                case '1':
                case 't':
                case 'T':
                    result = true;
                    break;

                default:
                    result = false;
                    break;
            }

            return result;
        }
        public static Char ToChar(this Char c) => c;
        public static Byte ToByte(this Char c) => (Byte)(c);
        public static SByte ToSByte(this Char c) => (SByte)(c);
        public static Int16 ToInt16(this Char c) => (Int16)(c);
        public static UInt16 ToUInt16(this Char c) => (UInt16)(c);
        public static Int32 ToInt32(this Char c) => (Int32)(c);
        public static UInt32 ToUInt32(this Char c) => (UInt32)(c);
        public static Int64 ToInt64(this Char c) => (Int64)(c);
        public static UInt64 ToUInt64(this Char c) => (UInt64)(c);
        public static Single ToSingle(this Char c) => (Single)(c);
        public static Double ToDouble(this Char c) => (Double)(c);
        public static Decimal ToDecimal(this Char c) => (Decimal)(c);





        //}}@@@
    }

}
