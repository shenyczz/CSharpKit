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
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CSharpKit.Utils
{
    /// <summary>
    /// 工具
    /// </summary>
    public static class KUtils
    {
        public static int SizeOf<T>()
        {
            return Marshal.SizeOf(typeof(T));
        }

        public static int SizeOf<T>(T obj)
        {
            return Marshal.SizeOf(obj.GetType());
        }

        // public static TypeNames TypeNameOf<T>()
        // {
        //     return TypeNameOf(typeof(T).Name);
        // }

        // public static TypeNames TypeNameOf(string name)
        // {
        //     return TypeNameOf(name, true);
        // }
        // public static TypeNames TypeNameOf(string name, bool ignoreCase)
        // {
        //     return Enum.TryParse(name, ignoreCase, out TypeNames typeName)
        //         ? typeName
        //         : TypeNames.None;
        // }



        public static bool IsBit32 => (IntPtr.Size == 4);

        public static bool IsBit64 => (IntPtr.Size == 8);




        /// <summary>
        /// 结构字段的偏移地址（可用 Marshal.OffsetOf() 代替）
        /// </summary>
        /// <param name="type">结构类型</param>
        /// <param name="field_name"></param>
        /// <returns></returns>
        public static int FieldOffset(Type type, string field_name )
        {
            int offset = -1;

            //Marshal.OffsetOf();

            try
            {
                MemberInfo[] mis = type.GetMembers();

                if (mis.ToList().FindIndex(p => p.Name == field_name)>=0)
                {
                    offset = 0;

                    for (int i = 0; i < mis.Length; i++)
                    {
                        var mi = mis[i];
                        if (mi.MemberType == MemberTypes.Field)
                        {
                            var fi = mi as FieldInfo;
                            Type fld_type = fi.FieldType;

                            if (fi.Name == field_name)
                            {
                                break;
                            }

                            offset += Marshal.SizeOf(fld_type);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return offset;
        }



        
        //@@@
    }



}

