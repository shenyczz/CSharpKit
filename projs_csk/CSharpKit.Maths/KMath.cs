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

namespace CSharpKit.Maths
{
    public static class KMath
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap<T>(ref T a, ref T b)
        {
            T t = a;
            a = b;
            b = t;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static int Distance(int x1, int y1, int x2, int y2)
        {
            double dx = (x2 - x1);
            double dy = (y2 - y1);
            return (int)(Math.Sqrt((dx * dx) + (dy * dy) + 0.5));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double Distance(double x1, double y1, double x2, double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static float Distance(float x1, float y1, float x2, float y2)
        {
            float dx = x2 - x1;
            float dy = y2 - y1;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        // 线性插值

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="s0"></param>
        /// <param name="s1"></param>
        /// <param name="v0"></param>
        /// <param name="v1"></param>
        /// <returns></returns>
        static double Linear_bak(double s, double s0, double s1,  double v0, double v1)
        {
            var result = default(double);

            try
            {
                var vtemp = result;

                if (Math.Abs(s1 - s0) > 0)
                {
                    var t = (s - s0) / (s1 - s0);
                    vtemp = v0 + t * (v1 - v0);
                }

                result = vtemp;
            }
            catch (Exception)
            {
            }

            return result;
        }


        public static T Linear<T>(T v0, T v1, double t)
        {
            return v0 + t * ((dynamic)v1 - (dynamic)v0);
        }
        public static T Linear<T>(double s, double s0, double s1, T v0, T v1)
        {
            var result = default(T);

            try
            {
                var vtemp = result;

                if (Math.Abs(s1 - s0) > 0)
                {
                    var t = (s - s0) / (s1 - s0);
                    //vtemp = Linear(v0, v1, t);
                    vtemp = v0 + t * ((dynamic)v1 - (dynamic)v0);
                }

                result = vtemp;
            }
            catch (Exception)
            {
            }

            return result;
        }



        /// <summary>
        /// dotnet 支持的类型参数约束有以下五种：
        /// where T : struct                ----  T 必须是一个值类型
        /// where T : class                 ----  T 必须是一个引用类型
        /// where T : new()                 ----  T 必须要有一个无参构造函数, (即他要求类型参数必须提供一个无参数的构造函数)
        /// where T : NameOfBaseClass       ----  T 必须继承名为NameOfBaseClass的类
        /// where T : NameOfInterface       ----  T 必须实现名为NameOfInterface的接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x1"></param>
        /// <param name="outv"></param>
        /// <returns></returns>
        static void _B123<T>(T x1, ref T outv) where T : struct
        {
            //T a = 0;
            //return a;
            //Int16

            //T a = (T)0;

            return;
        }


        //@@@
    }

}

