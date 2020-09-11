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
using System.Globalization;

namespace CSharpKit
{
    /// <summary>
    /// 
    /// </summary>
    public static class IFormatProviderExtensions
    {

        /// <summary>
        /// Tries to get a <see cref="NumberFormatInfo"/> from the format
        /// provider, returning the current culture if it fails.
        /// </summary>
        /// <param name="formatProvider">
        /// An <see cref="IFormatProvider"/> that supplies culture-specific
        /// formatting information.
        /// </param>
        /// <returns>A <see cref="NumberFormatInfo"/> instance.</returns>
        public static NumberFormatInfo GetNumberFormatInfo(this IFormatProvider formatProvider)
        {
            return NumberFormatInfo.GetInstance(formatProvider);
        }
        public static TextInfo GetTextInfo(this IFormatProvider formatProvider)
        {
            if (formatProvider == null)
            {
                return CultureInfo.CurrentCulture.TextInfo;
            }

            return (formatProvider.GetFormat(typeof(TextInfo)) as TextInfo)
                ?? GetCultureInfo(formatProvider).TextInfo;
        }
        public static CultureInfo GetCultureInfo(this IFormatProvider formatProvider)
        {
            if (formatProvider == null)
            {
                return CultureInfo.CurrentCulture;
            }

            return (formatProvider as CultureInfo)
                ?? (formatProvider.GetFormat(typeof(CultureInfo)) as CultureInfo)
                    ?? CultureInfo.CurrentCulture;
        }


    }






}
