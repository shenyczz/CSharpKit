using System;
using System.Diagnostics.CodeAnalysis;
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

namespace CSharpKit.GeoApi.Geometries
{
    /// <summary>
    /// 点 - 几何图形
    /// </summary>
    public class Point : Geometry, IPoint, ICloneable<IPoint>
    {
        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Point()
            : this(0, 0) { }

        /// <summary>
        /// 构造函数 - 2 参数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Point(Double x, Double y)
        {
            X = x; Y = y;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 点X坐标
        /// </summary>
        public Double X { get; set; }
        /// <summary>
        /// 点Y坐标
        /// </summary>
        public Double Y { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Double Z { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Double M { get; set; }

        public ICoordinateSequence CoordinateSequence => null;


        #endregion

        #region Public Functions

        /// <summary>
        /// 坐标间距离 
        /// </summary>
        /// <param name="other">其他坐标</param>
        /// <returns></returns>
        public virtual Double Distance(Point other)
        {
            double dx = X - other.X;
            double dy = Y - other.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// 使图形偏移指定的量
        /// </summary>
        /// <param name="dx">X坐标的偏移量</param>
        /// <param name="dy">Y坐标的偏移量</param>
        public override void Offset(double dx, double dy)
        {
            X += dx; Y += dy;
            base.Offset(dx, dy);
        }

        /// <summary>
        /// 取得点的哈希码
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode()
                ^ X.GetHashCode()
                ^ Y.GetHashCode();
        }

        #endregion

        #region Override Functions



        #endregion

        #region ICloneable<IPoint>

        public IPoint Clone()
        {
            return new Point(X, Y);
        }

        #endregion

        //@@@
    }


}
