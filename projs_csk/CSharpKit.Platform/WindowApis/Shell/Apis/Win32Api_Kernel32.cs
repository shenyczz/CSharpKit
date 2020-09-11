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

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// Win32Api - Kernel32.dll
    /// </summary>
    partial class Win32Api
    {
        private const string dllKernel32 = "Kernel32.dll";



        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr LoadLibrary(String path);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr LoadLibraryEx(
            string fileName,    // file name of module
            int[] hFile,        // reserved, must be NULL
            uint dwFlags        // entry-point execution option
            );

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int FreeLibrary(IntPtr hModule);


        [DllImport(dllKernel32, CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string module);


        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetProcAddress(IntPtr lib, String funcName);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int FormatMessage(
            int dwFlags,
            IntPtr lpSource,
            int dwMessageId,
            int dwLanguageId,
            out IntPtr MsgBuffer,
            int nSize,
            IntPtr Arguments
            );

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void CloseHandle(IntPtr handle);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern UInt32 GetCurrentThreadId();

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "OpenProcess")]
        public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Boolean bInheritHandle, UInt32 dwProcessId);

        [DllImport("Kernel32.dll")]
        public static extern Int32 GetPrivateProfileInt(
            String sectionName,
            String keyName,
            Int32 defValue,
            String fileName);

        [DllImport("Kernel32.dll")]
        public static extern Int32 GetPrivateProfileString(
            String sectionName,
            String keyName,
            String defValue,
            StringBuilder retString,
            Int32 size,
            String fileName);

        [DllImport("Kernel32.dll")]
        public static extern Boolean WritePrivateProfileString(
            String sectionName,
            String keyName,
            String strValue,
            String fileName);


        [DllImport("kernel32")]
        public static extern IntPtr GetProcessHeap();

        [DllImport("kernel32")]
        public static extern IntPtr HeapAlloc(IntPtr hHeap, int flags, int size);

        [DllImport("kernel32")]
        public static extern bool HeapFree(IntPtr hHeap, int flags, IntPtr block);




        //@EndOf(Win32Api)
    }
}
