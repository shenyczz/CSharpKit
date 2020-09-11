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
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CSharpKit.GeoApi.Geometries
{
    /// <summary>
    /// 折线
    /// </summary>
    public class Polyline : LinearString, IPolyline
    {
        #region Constructors

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Polyline() { }

        #endregion

        #region Properties

        /// <summary>
        /// 长度
        /// </summary>
        public Double Length
        {
            get { return ComputeLength(); }
        }
        public Boolean IsClose
        {
            get
            {
                return true
                    && this[0].X == this[Count - 1].X
                    && this[0].Y == this[Count - 1].Y;
            }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// 使图形偏移指定的量
        /// </summary>
        /// <param name="dx">X坐标的偏移量</param>
        /// <param name="dy">Y坐标的偏移量</param>
        //public override void Offset(Double dx, Double dy)
        //{
        //    base.Offset(dx, dy);
        //}

        //todo:使用 NetTopology,不使用 Gdi32.dll
        /// <summary>
        /// 点在范围内部
        /// </summary>
        /// <param name="x">点的x坐标</param>
        /// <param name="y">点的y坐标</param>
        /// <returns>true or false</returns>
        public Boolean PointInside(Double x, Double y)
        {
            List<IPoint> gps = new List<IPoint>();
            foreach (Point gp in this)
            {
                gps.Add(gp);
            }
            if (!this.IsClosed)
            {
                gps.Add(this[0]);
            }

            int times = 10000;
            Point[] kpts = new Point[gps.Count];
            for (int i = 0; i < gps.Count; i++)
            {
                kpts[i].X = (int)(gps[i].X * times + 0.1);
                kpts[i].Y = (int)(gps[i].Y * times + 0.1);
            }

            IntPtr hRgn = CreatePolygonRgn(kpts, kpts.Length, 1);   //FillModes.ALTERNATE
            bool isInSide = PtInRegion(hRgn, (int)(x * times), (int)(y * times));
            return isInSide;
        }

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr CreatePolygonRgn(Point[] points, int pointCount, int fillMode);

        [DllImport("Gdi32.dll", CharSet = CharSet.Auto)]
        static extern Boolean PtInRegion(IntPtr hRgn, int x, int y);

        #endregion

        #region Private Functions

        /// <summary>
        /// 计算长度
        /// </summary>
        /// <returns></returns>
        private Double ComputeLength()
        {
            Double len = 0;

            int pointCount = this.Count;
            for (int i = 0; i < pointCount - 1; i++)
            {
                Point point1 = this[i] as Point;
                Point point2 = this[i + 1] as Point;
                len += point1.Distance(point2);
            }

            return len;
        }

        #endregion

        //@endOf(Polyline)
    }

}
