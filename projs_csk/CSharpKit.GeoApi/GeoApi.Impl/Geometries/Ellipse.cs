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

namespace CSharpKit.GeoApi.Geometries
{
    public class Ellipse : Rectangle
    {
        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Ellipse()
            : this(0, 0, 0, 0) { }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public Ellipse(Ellipse rhs)
            : this(rhs.X, rhs.Y, rhs.Width, rhs.Height) { }

        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Ellipse(Double x, Double y, Double width, Double height)
            : base(x, y, width, height) { }

        #endregion

        //@@@
    }


}
