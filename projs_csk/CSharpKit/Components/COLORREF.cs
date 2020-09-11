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
using System.Runtime.InteropServices;
using System.Text;

namespace CSharpKit
{
    /// <summary>
    /// 接收 C 的 COLORREF 值
    /// </summary>
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Auto, Pack = 1, Size = 4)]
    public struct COLORREF
    {
        #region Fields

        /// <summary>
        /// B 分量 
        /// </summary>
        [FieldOffset(0)]
        public Byte Blue;
        /// <summary>
        /// G 分量 
        /// </summary>
        [FieldOffset(1)]
        public Byte Green;
        /// <summary>
        /// R 分量 
        /// </summary>
        [FieldOffset(2)]
        public Byte Red;
        /// <summary>
        /// A 分量
        /// </summary>
        [FieldOffset(3)]
        public Byte Reserved;

        #endregion

        #region Constructors

        public COLORREF(Byte r, Byte g, Byte b)
        {
            Red = r;
            Green = g;
            Blue = b;
            Reserved = 0xFF;
        }

        #endregion

        #region Public Functions - override

        /// <summary>
        /// 哈希码
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Red.GetHashCode() ^ Green.GetHashCode() ^ Blue.GetHashCode() ^ Reserved.GetHashCode();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            if (!(obj is Rgb))
                return false;

            Rgb other = (Rgb)obj;
            return true
                && Red == other.Red
                && Green == other.Green
                && Blue == other.Blue
                && Reserved == other.Reserved
                ;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{R,G,B} = {");
            sb.Append(String.Format("{0},{1},{2}", Red, Green, Blue));
            sb.Append("}");
            return sb.ToString();
        }

        #endregion

        #region Operator Override

        public static Boolean operator ==(COLORREF rgb1, COLORREF rgb2)
        {
            return rgb1.Equals(rgb2);
        }
        public static Boolean operator !=(COLORREF argb1, COLORREF argb2)
        {
            return !argb1.Equals(argb2);
        }

        #endregion


        #region DataType Transform

        public static implicit operator COLORREF(Int32 iv)
        {
            return (COLORREF)(UInt32)iv;
        }
        public static implicit operator COLORREF(UInt32 iv)
        {
            byte[] iva = BitConverter.GetBytes(iv); // Lit=>RGBA(0123)    Big=>ABGR(0123)
            if (BitConverter.IsLittleEndian)
                return new COLORREF(iva[0], iva[1], iva[2]);
            else
                return new COLORREF(iva[3], iva[2], iva[1]);
        }

        public static implicit operator Int32(COLORREF colorref)
        {
            return (Int32)(UInt32)colorref;
        }
        public static implicit operator UInt32(COLORREF colorref)
        {
            byte[] iva = new byte[4] { colorref.Blue, colorref.Green, colorref.Red, colorref.Reserved };
            return BitConverter.ToUInt32(iva, 0);
        }

        #endregion





    }








}
