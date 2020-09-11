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

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// Win32Api
    /// </summary>
    public sealed partial class Win32Api
    {
        private Win32Api() { }

        /// <summary>
        /// internal class that describes resource DLL range and name
        /// </summary>
        internal class DllDescriptor
        {
            /// <summary>
            /// First message in the range reserved for this DLL
            /// </summary>
            public int firstMessage;
            /// <summary>
            /// Last message in the range reserved for this DLL
            /// </summary>
            public int lastMessage;
            /// <summary>
            /// The name of the DLL, e.g. "WINHTTP.DLL"
            /// </summary>
            public String dllName;

            /// <summary>
            /// Describes a DLL that can be searched for error messages
            /// </summary>
            /// <param name="first">First error number in the range</param>
            /// <param name="last">Last error number in the range</param>
            /// <param name="dll">File name of the associated dll, e.g. WINHTTP.DLL</param>
            public DllDescriptor(int first, int last, String dll)
            {
                firstMessage = first;
                lastMessage = last;
                dllName = dll;
            }
        }


        //}}@@@
    }







}
