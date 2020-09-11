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
using System.Linq;
using System.Collections.Generic;

namespace CSharpKit
{
    /// <summary>
    /// Slice - 切片
    /// </summary>
    /// <typeparam name="_Ty"></typeparam>
    /// <remarks>
    /// dotnet 支持的类型参数约束有以下五种：
    /// where T : struct                ----  T 必须是一个值类型
    /// where T : class                 ----  T 必须是一个引用类型
    /// where T : new()                 ----  T 必须要有一个无参构造函数, (即他要求类型参数必须提供一个无参数的构造函数)
    /// where T : NameOfBaseClass       ----  T 必须继承名为NameOfBaseClass的类
    /// where T : NameOfInterface       ----  T 必须实现名为NameOfInterface的接口
    /// </remarks>
    public sealed class TSlice<_Ty>
    {
        public TSlice(int width, int height)
        {
            Width = width;
            Height = height;

            _Elements = new _Ty[width * height];
        }


        #region Fields

        private _Ty[] _Elements;    // 要素值

        #endregion


        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Size => Width * Height;
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public _Ty this[int pos] => _Elements[pos];

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public _Ty this[int i, int j]
        {
            get => _Elements[i * Width + j];
            set => _Elements[i * Width + j] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_elements"></param>
        public void SetData(IEnumerable<_Ty> _elements)
        {
            List<_Ty> lst = new List<_Ty>();
            _elements.ForEach(p => lst.Add(p));
            _Elements = lst.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<_Ty> GetData()
        {
            return _Elements;
        }



        //@EndOf(Slice)
    }



}

