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
using System.Collections;
using System.Runtime.InteropServices;

namespace CSharpKit.Platform.Windows
{
    partial class Win32Api
    {
        /// <summary>
        /// 应用程序空闲 
        /// </summary>
        public static Boolean IsAppIdle
        {
            get
            {
                // 所有窗口都没有消息
                return (PeekMessage(out MSG msg, IntPtr.Zero, 0, 0, 0) == 0);
            }
        }


        private static ArrayList _dllDescriptors = new ArrayList();
        /// <summary>
        /// Get message string given error number
        /// </summary>
        /// <param name="lastError">The error number</param>
        /// <returns>Associated error message</returns>
        public static string GetErrorMessage(int lastError)
        {
            IntPtr hModule = IntPtr.Zero; // handle to resource DLL (if any)
            IntPtr pMessageBuffer; // pointer to unmananged message string
            int dwBufferLength; // length of the above

            // initialize the error message we will return with a generic one
            string errorMessage = String.Format("Last Win32 Error #{0:X8}", lastError);

            // set up the format flags: 
            int dwFormatFlags =
                Win32Messages.FORMAT_MESSAGE_ALLOCATE_BUFFER |  // allocate the memory for us
                Win32Messages.FORMAT_MESSAGE_IGNORE_INSERTS |   // no placeholder replacements
                Win32Messages.FORMAT_MESSAGE_FROM_SYSTEM;       // search system tables too

            // loop over known DLLs with corresponding message ranges
            foreach (DllDescriptor dllDesc in _dllDescriptors)
            {
                // If lastError is in the matching range, load the message source.
                if (lastError >= dllDesc.firstMessage && lastError <= dllDesc.lastMessage)
                {
                    // load DLL as datafile
                    hModule = LoadLibraryEx(dllDesc.dllName, null, Win32Messages.LOAD_LIBRARY_AS_DATAFILE);

                    // if successful, add corresponding bit - will search in module first
                    if (hModule != IntPtr.Zero) dwFormatFlags |= Win32Messages.FORMAT_MESSAGE_FROM_HMODULE;
                    break; // exit the loop - even if hModule is null, makes no sense to search further
                }
            }

            dwBufferLength = FormatMessage(dwFormatFlags,
                hModule,
                lastError,
                1024, // MAKELANGID (LANG_NEUTRAL, SUBLANG_DEFAULT)
                out pMessageBuffer,
                0,
                IntPtr.Zero);

            if (dwBufferLength > 0)
            {
                errorMessage = Marshal.PtrToStringUni(pMessageBuffer);
                Marshal.FreeHGlobal(pMessageBuffer);
            }

            if (hModule != IntPtr.Zero) FreeLibrary(hModule);

            return errorMessage;
        }



        //}}@@@
    }

}
