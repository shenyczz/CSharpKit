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

using System.Runtime.InteropServices;

namespace CSharpKit
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// dotnet 支持的类型参数约束有以下五种：
    /// where T : struct                ----  T 必须是一个值类型
    /// where T : class                 ----  T 必须是一个引用类型
    /// where T : new()                 ----  T 必须要有一个无参构造函数, (即他要求类型参数必须提供一个无参数的构造函数)
    /// where T : NameOfBaseClass       ----  T 必须继承名为NameOfBaseClass的类
    /// where T : NameOfInterface       ----  T 必须实现名为NameOfInterface的接口
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
    class _sample_Clsss_Struct<T> where T : class
    {
        /// <summary>
        /// 本结构字节大小
        /// </summary>
        public const int Size = 0;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        byte[] ucFileName;


        void foo(params int[] iTables)
        {
            int len = iTables.Length;
        }


        void test()
        {
            foo(1);
            foo(1,2,3);
        }


        //@EndOf(_sample_Clsss_Struct)
    }

}
