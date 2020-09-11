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
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace CSharpKit.ComponentModel
{
    // Spectrum
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
        AllowMultiple = false, Inherited = false)]
    public class SpectrumAttribute : Attribute
    {
        public SpectrumAttribute(string name)
        {
            Spectrum = name;
        }

        public string Spectrum { get; private set; }

        //@@@
    }



    public static partial class SpectrumExtensions
    {
        public static string Spectrum(this Object vobj)
        {
            var result = "";

            try
            {
                var vtemp = result;
                {
                    FieldInfo fi = vobj.GetType().GetField(vobj.ToString());
                    var attrs = fi.GetCustomAttributes(typeof(SpectrumAttribute), false) as SpectrumAttribute[];
                    vtemp = attrs.Length > 0 ? attrs[0].Spectrum : "";
                }
                result = vtemp;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return result;
        }

        //@@@
    }





}
