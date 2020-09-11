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

namespace CSharpKit
{
    public static class IntPtrExtensions
    {
        public static IntPtr From<T>(this IntPtr ptr, T[] objs) where T : new()
        {
            ptr = IntPtr.Zero;

            try
            {
                int size = Marshal.SizeOf(typeof(T));
                int length = objs.Length;
                ptr = Marshal.AllocHGlobal(size * length);

                IntPtr i_ptr = ptr;
                for (int i = 0; i < length; i++)
                {
                    T t = objs[i];
                    Marshal.StructureToPtr(t, i_ptr, true);
                    i_ptr = IntPtr.Add(i_ptr, size);
                }
            }
            catch (Exception)
            {
            }

            return ptr;
        }

        static IntPtr Allocate<T>(T[] x) where T : new()
        {
            if (x == null)
                return IntPtr.Zero;

            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)) * x.Length);
            IntPtr i_ptr_nodes = ptr;
            for (int i = 0; i < x.Length; i++)
            {
                T node = x[i];
                Marshal.StructureToPtr(node, i_ptr_nodes, true);
                i_ptr_nodes = IntPtr.Add(i_ptr_nodes, Marshal.SizeOf(typeof(T)));
            }

            return ptr;
        }


        public static void Free(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return;

            Marshal.DestroyStructure(ptr, typeof(IntPtr));
            Marshal.FreeHGlobal(ptr);
            ptr = IntPtr.Zero;
        }



        public static bool ToBoolean(this IntPtr v)
        {
            return System.Math.Abs((int)v) > 0;
        }

        //}}@@@
    }

}
