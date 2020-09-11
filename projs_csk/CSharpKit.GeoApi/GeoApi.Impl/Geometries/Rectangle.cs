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
    /// <summary>
    /// 矩形
    /// </summary>
    public class Rectangle : Geometry
    {
        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Rectangle()
            : this(0, 0, 0, 0) { }

        /// <summary>
        /// 拷贝构造函数
        /// </summary>
        /// <param name="rhs"></param>
        public Rectangle(Rectangle rhs)
            : this(rhs.X, rhs.Y, rhs.Width, rhs.Height) { }

        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Rectangle(Double x, Double y, Double width, Double height)
        {
            X = x; Y = y; Width = width; Height = height;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 矩形左上角的 x 坐标
        /// </summary>
        public Double X { get; set; }

        /// <summary>
        /// 矩形左上角的 y 坐标
        /// </summary>
        public Double Y { get; set; }

        #endregion

        #region Public Functions

        /// <summary>
        /// 使图形偏移指定的量
        /// </summary>
        /// <param name="dx">X坐标向的偏移量</param>
        /// <param name="dy">Y坐标向的偏移量</param>
        public override void Offset(Double dx, Double dy)
        {
            this.X += dx;
            this.Y += dy;
            base.Offset(dx, dy);
        }

        #endregion

        //@@@
    }



}
