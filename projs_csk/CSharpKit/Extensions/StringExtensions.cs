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
using System.Linq;
using System.Text;

namespace CSharpKit
{
    /// <summary>
    /// 字符串类功能扩展
    /// </summary>
    public static class StringExtensions
    {
        public static byte[] ToBytes(this string s)
        {
#if NETCORE
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
#endif
            return Encoding.GetEncoding("GB18030").GetBytes(s);
        }

        public static char[] ToChars(this string s)
        {
#if NETCORE
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
#endif
            return Encoding.GetEncoding("GB18030").GetChars(s.ToBytes());
        }

        public static int ToInt(this string s, int defaultValue = 0)
        {
            int iret = defaultValue;
            try { iret = int.Parse(s); }
            catch { }
            finally { }
            return iret;
        }

        public static Single ToSingle(this string s, Single defaultValue = 0)
        {
            Single iret = defaultValue;
            try { iret = Single.Parse(s); }
            catch { }
            finally { }
            return iret;
        }

        public static Single ToFloat(this string s, Single defaultValue = 0)
        {
            Single iret = defaultValue;
            try { iret = Single.Parse(s); }
            catch { }
            finally { }
            return iret;
        }


        public static Double ToDouble(this string s, Double defaultValue = 0)
        {
            Double iret = defaultValue;
            try { iret = double.Parse(s); }
            catch { }
            finally { }
            return iret;
        }

        public static Boolean ToBoolean(this string s, Boolean defaultValue = false)
        {
            Boolean vret = defaultValue;
            //---------------------------------------------
            try
            {
                if (!String.IsNullOrEmpty(s))
                {
                    String bstring = s.Trim().ToLower(System.Globalization.CultureInfo.InvariantCulture);
                    switch (bstring)
                    {
                        case "0":
                        case "f":
                        case "false":
                            bstring = "False";
                            break;

                        case "1":
                        case "t":
                        case "true":
                            bstring = "True";
                            break;

                        default:
                            bstring = "False";
                            break;
                    }

                    vret = Boolean.Parse(bstring);
                }
            }
            catch { }
            finally { }
            //---------------------------------------------
            return vret;
        }


        public static DateTime ToDateTime(this string s)
        {
            DateTime iret = new DateTime(1970, 1, 1);
            try { iret = DateTime.Parse(s); }
            catch { }
            finally { }
            return iret;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string Repeat(this string s, char c, int count)
        {
            return BitConverter.ToString(Enumerable.Repeat(Convert.ToByte(c), count).ToArray());
        }


        public static char ToChar(this string s)
        {
            var chs = s.ToChars();
            return chs.Length > 0 ? chs[0] : '0';
        }



        //}}@@@
    }

}
