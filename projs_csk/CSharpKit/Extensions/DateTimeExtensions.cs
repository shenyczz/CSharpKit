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
    /// 
    /// </summary>
    public static class DateTimeExtensions
    {
        public static bool IsDay(this DateTime dt, DateTimeKind kind = DateTimeKind.Utc)
        {
            bool result = false;

            try
            {
                var vtemp = result;
                {
                    var isUnspecified = dt.Kind == DateTimeKind.Unspecified;
                    var dtemp = isUnspecified ? new DateTime(dt.Ticks, DateTimeKind.Utc) : dt;
                    dtemp = dtemp.ToLocalTime();
                    vtemp = (dtemp.Hour >= 6 && dtemp.Hour <= 18);
                }
                result = vtemp;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return result;
        }

        public static char ToChar(this DateTime dt)
        {
            var v = dt.ToBinary();
            return (char)(v < char.MinValue ? char.MinValue
                : v > char.MaxValue ? char.MaxValue
                : v);
        }


        //}}@@@
    }





}
