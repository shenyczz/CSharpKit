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
 *    Usage: 
 *    MyDll.dll -> int Add(int v1,int v2);
 *    
 *    public delegate int AddCallback(int v1,int v2);
 *    
 *    DllInvoke.BeginInvoke("MyDll.dll");
 *    AddCallback add = (AddCallback)DllInvoke.Invoke("Add",typeof(AddCallback));
 *    int v = add(1,2);
 *    DllInvoke.EndInvoke();
 *    
******************************************************************************/
using System;
using System.Runtime.InteropServices;

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public class DllInvoke
    {
        private IntPtr hLib;


        public void BeginInvoke(String dllPath)
        {
            hLib = Win32Api.LoadLibrary(dllPath);
        }

        /// <summary>
        /// 调用
        /// </summary>
        /// <param name="apiName"></param>
        /// <param name="t">委托函数的类型</param>
        /// <returns></returns>
        public Delegate Invoke(String apiName, Type t)
        {
            IntPtr apiPtr = Win32Api.GetProcAddress(hLib, apiName);
            return Marshal.GetDelegateForFunctionPointer(apiPtr, t);

        }

        public void EndInvoke()
        {
            Win32Api.FreeLibrary(hLib);
        }

        //}}@@@
    }
}
