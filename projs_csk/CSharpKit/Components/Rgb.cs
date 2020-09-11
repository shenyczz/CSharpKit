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
    /// Rgb - RGB 结构
    /// </summary>
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi, Pack = 1, Size = 4)]
    public struct Rgb
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public Rgb(Byte r, Byte g, Byte b)
        {
            Red = r;
            Green = g;
            Blue = b;
            Reserved = 0xFF;
        }

        #endregion

        /// <summary>
        /// Size - byte size of this struct
        /// </summary>
        public const int Size = 4;

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

        #region Public Functions

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

        public static Boolean operator ==(Rgb rgb1, Rgb rgb2)
        {
            return rgb1.Equals(rgb2);
        }
        public static Boolean operator !=(Rgb argb1, Rgb argb2)
        {
            return !argb1.Equals(argb2);
        }

        #endregion

        #region DataType Transform

        /// <summary>
        /// Rgb --> Argb
        /// </summary>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static explicit operator Argb(Rgb rgb)
        {
            Argb argb;
            argb.R = rgb.Red;
            argb.G = rgb.Green;
            argb.B = rgb.Blue;
            argb.A = 0xff;
            return argb;
        }

        public static implicit operator Rgb(Int32 iv)
        {
            return (Rgb)(UInt32)iv;
        }
        public static implicit operator Rgb(UInt32 iv)
        {
            byte[] iva = BitConverter.GetBytes(iv); // Lit=>BGRA(0123)    Big=>ARGB(0123)
            if (BitConverter.IsLittleEndian)
                return new Rgb(iva[2], iva[1], iva[0]);
            else
                return new Rgb(iva[1], iva[2], iva[3]);
        }

        public static implicit operator Rgb(COLORREF colorref)
        {
            return new Rgb(colorref.Red, colorref.Green, colorref.Blue);
        }

        #endregion

        #region Public Functions - Static

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strRgb"></param>
        /// <returns></returns>
        public static Rgb FromString(String strRgb)
        {
            Rgb rgb = new Rgb();
            string[] rgbArray = strRgb.Split(new Char[] { ' ', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (rgbArray.Length >= 3)
            {
                Byte.TryParse(rgbArray[0], out rgb.Red);
                Byte.TryParse(rgbArray[1], out rgb.Green);
                Byte.TryParse(rgbArray[2], out rgb.Blue);
            }
            return rgb;
        }

        #endregion


        //}}@@@
    }





}
