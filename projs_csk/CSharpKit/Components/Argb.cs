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
    /// Argb
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1, Size = 4)]
    public struct Argb
    {
        #region Constructors

        public Argb(Byte r, Byte g, Byte b)
            : this(0xff, r, g, b) { }

        public Argb(Byte a, Byte r, Byte g, Byte b)
        {
            A = a; R = r; G = g; B = b;
        }

        #endregion

        #region Fields

        /// <summary>
        /// B 分量 
        /// </summary>
        public Byte B;
        /// <summary>
        /// G 分量 
        /// </summary>
        public Byte G;
        /// <summary>
        /// R 分量 
        /// </summary>
        public Byte R;
        /// <summary>
        /// A 分量
        /// </summary>
        public Byte A;

        public static readonly Argb Black = new Argb(0, 0, 0);
        public static readonly Argb White = new Argb(255, 255, 255);

        public static readonly Argb Red = new Argb(255, 0, 0);
        public static readonly Argb Green = new Argb(0, 255, 0);
        public static readonly Argb Blue = new Argb(0, 0, 255);

        #endregion

        #region Public Functions

        /// <summary>
        /// 哈希码
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return A.GetHashCode() ^ R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            Argb other = (Argb)obj;
            return true
                && A == other.A
                && R == other.R
                && G == other.G
                && B == other.B
                ;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{A,R,G,B} = {");
            sb.Append(String.Format("{0},{1},{2},{3}", A, R, G, B));
            sb.Append("}");
            return sb.ToString();
        }

        #endregion

        #region Operator Override - 操作符重载

        public static Boolean operator ==(Argb argb1, Argb argb2)
        {
            return argb1.Equals(argb2);
        }
        public static Boolean operator !=(Argb argb1, Argb argb2)
        {
            return !argb1.Equals(argb2);
        }

        #endregion

        #region DataType Transform - 数据类型转换

        /// <summary>
        /// 32位整数隐式转换为Argb [implicit/explicit]
        /// </summary>
        /// <param name="iv">32位整数</param>
        /// <returns>Argb结构值</returns>
        public static implicit operator Argb(Int32 iv)
        {
            return (Argb)(UInt32)iv;
        }
        public static implicit operator Argb(UInt32 iv)
        {
            byte[] iva = BitConverter.GetBytes(iv); // Lit=>BGRA(0123)    Big=>ARGB(0123)
            if (BitConverter.IsLittleEndian)
                return new Argb(iva[3], iva[2], iva[1], iva[0]);
            else
                return new Argb(iva[0], iva[1], iva[2], iva[3]);
        }

        /// <summary>
        /// Argb隐式转换32位整数
        /// </summary>
        /// <param name="argb">Argb结构值</param>
        /// <returns>32位整数值</returns>
        public static implicit operator Int32(Argb argb)
        {
            return (Int32)(UInt32)argb;
        }
        public static implicit operator UInt32(Argb argb)
        {
            byte[] iva = new byte[4] { argb.B, argb.G, argb.R, argb.A };
            return BitConverter.ToUInt32(iva, 0);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strRgb"></param>
        /// <returns></returns>
        public static Argb FromString(String strRgb)
        {
            Argb argb = new Argb();
            string[] rgbArray = strRgb.Split(new Char[] { ' ', ',', ';', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (rgbArray.Length >= 4)
            {
                Byte.TryParse(rgbArray[0], out argb.A);
                Byte.TryParse(rgbArray[1], out argb.R);
                Byte.TryParse(rgbArray[2], out argb.G);
                Byte.TryParse(rgbArray[3], out argb.B);
            }
            return argb;
        }

        //@EndOf(Argb)
    }


}
