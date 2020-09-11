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
using GeoPoint = CSharpKit.GeoApi.Geometries.Point;

namespace CSharpKit.GeoApi.Geometries
{
    /// <summary>
    /// Contour - 等值线
    /// </summary>
    public class Contour : SmoothCurve
    {
        #region Properties

        /// <summary>
        /// 等值线和矩形边界的拓扑关系
        /// </summary>
        public ContourTopology ContourTopology { get; set; }

        private IExtent _Extent = null;
        /// <summary>
        /// 重载包围盒属性
        /// </summary>
        public new IExtent Extent
        {
            get
            {
                if (_Extent == null || _Extent.IsEmpty)
                {
                    _Extent = ComputeBoundingBox();
                }
                return _Extent;
            }
            set { _Extent = value; }
        }

        /// <summary>
        /// 曲线的值
        /// </summary>
        public Double Value { get; set; }

        private Double _Area = Double.NaN;
        /// <summary>
        /// 面积
        /// </summary>
        /// <returns></returns>
        public Double Area
        {
            get
            {
                if (Double.IsNaN(_Area))
                    _Area = ComputeArea();

                return _Area;
            }
        }

        /// <summary>
        /// 周长
        /// </summary>
        public Double Perimeter
        {
            get { return this.Length; }
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// 使图形偏移指定的量
        /// </summary>
        /// <param name="dx">X坐标的偏移量</param>
        /// <param name="dy">Y坐标的偏移量</param>
        public override void Offset(Double dx, Double dy)
        {
            base.Offset(dx, dy);
            //...
        }

        /// <summary>
        /// 为构造闭合多边形增加点
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <remarks>
        /// <para>在(x1,y1)和(x2,y2)之间增加点</para>
        /// <para>不增加(x1,y1) 但增加 (x2,y2)</para>
        /// </remarks>
        public void AddBetweenPoint(double x1, double y1, double x2, double y2)
        {
            int i;
            int k = 50; // 等分比例

            double xInterval = (x2 - x1) / k;
            double yInterval = (y2 - y1) / k;

            for (i = 1; i < k - 1; i++)
            {
                double x = x1 + xInterval * i;
                double y = y1 + yInterval * i;
                this.Add(new GeoPoint(x, y));
            }

            this.Add(new GeoPoint(x2, y2));
            return;
        }

        /// <summary>
        /// 构造闭合的曲线
        /// </summary>
        /// <param name="xmin">包围盒坐标x最小值</param>
        /// <param name="ymin">包围盒坐标y最小值</param>
        /// <param name="xmax">包围盒坐标x最大值</param>
        /// <param name="ymax">包围盒坐标y最大值</param>
        /// <returns>曲线和边界的拓扑关系代码</returns>
        /// <remarks>
        /// <para>根据给出的包围盒,构造一个闭合曲线。</para>
        /// <para>曲线和边界的拓扑关系分下面4种情况：</para>
        /// <para>1. 多边形的起止点在对边上</para>
        /// <para>2. 多边形的起止点在相邻边上</para>
        /// <para>3. 多边形的起止点在同一条边上</para>
        /// <para>4. 多边形是闭合的</para>
        /// </remarks>
        public ContourTopology BuildCloseContour(double xmin, double ymin, double xmax, double ymax)
        {
            int iPointCount = this.Count;
            if (iPointCount == 0)
                return ContourTopology.Unkonw;

            // 闭合的
            if (this.IsClosed)
                return ContourTopology.Close;

            ContourTopology eTopology = ContourTopology.Unkonw;

            // 曲线的起点
            GeoPoint pt_beg = this[0] as GeoPoint;
            double xb = pt_beg.X;
            double yb = pt_beg.Y;

            // 曲线的终点
            GeoPoint pt_end = this[iPointCount - 1] as GeoPoint;
            double xe = pt_end.X;
            double ye = pt_end.Y;

            // 按顺时针方向增加点 - beg
            //
            // 1.多边形的起止点在对边上
            // 1-1.起点在左边,终点在右边
            if (Math.Abs(xb - xmin) < 1.0e-12 && Math.Abs(xe - xmax) < 1.0e-12)
            {
                eTopology = ContourTopology.OppositeSide; // 对边

                // 增加(x1,y1)和(x2,y2)之间的若干点
                // 不增加(x1,y1) 但增加 (x2,y2)
                AddBetweenPoint(xe, ye, xmax, ymin);
                AddBetweenPoint(xmax, ymin, xmin, ymin);
                AddBetweenPoint(xmin, ymin, xb, yb);
            }
            // 1-2.起点在右边,终点在左边
            else if (xb == xmax && xe == xmin)
            {
                eTopology = ContourTopology.OppositeSide;

                //增加(x1,y1)和(x2,y2)之间的若干点
                //不增加(x1,y1) 但增加 (x2,y2)
                AddBetweenPoint(xe, ye, xmin, ymax);
                AddBetweenPoint(xmin, ymax, xmax, ymax);
                AddBetweenPoint(xmax, ymax, xb, yb);
            }
            // 1-3. 起点在下边,终点在上边
            else if (yb == ymin && ye == ymax)
            {
                eTopology = ContourTopology.OppositeSide;

                //增加(x1,y1)和(x2,y2)之间的若干点
                //不增加(x1,y1) 但增加 (x2,y2)
                AddBetweenPoint(xe, ye, xmax, ymax);
                AddBetweenPoint(xmax, ymax, xmax, ymin);
                AddBetweenPoint(xmax, ymin, xb, yb);
            }
            // 1-4. 起点在上边,终点在下边
            else if (yb == ymax && ye == ymin)
            {
                eTopology = ContourTopology.OppositeSide;

                //增加(x1,y1)和(x2,y2)之间的若干点
                //不增加(x1,y1) 但增加 (x2,y2)
                AddBetweenPoint(xe, ye, xmin, ymin);
                AddBetweenPoint(xmin, ymin, xmin, ymax);
                AddBetweenPoint(xmin, ymax, xb, yb);
            }

            // 2.等值线的起止点在相邻边上
            // 2-1. 左下角
            else if ((xb == xmin && ye == ymin) || (xe == xmin && yb == ymin))
            {
                eTopology = ContourTopology.AdjacentSide;

                //增加(x1,y1)和(x2,y2)之间的若干点
                //不增加(x1,y1) 但增加 (x2,y2)
                AddBetweenPoint(xe, ye, xmin, ymin);
                AddBetweenPoint(xmin, ymin, xb, yb);
            }
            // 2-2. 左上角
            else if ((xb == xmin && ye == ymax) || (xe == xmin && yb == ymax))
            {
                eTopology = ContourTopology.AdjacentSide;

                //增加(x1,y1)和(x2,y2)之间的若干点
                //不增加(x1,y1) 但增加 (x2,y2)
                AddBetweenPoint(xe, ye, xmin, ymax);
                AddBetweenPoint(xmin, ymax, xb, yb);
            }
            // 2-3. 右上角
            else if ((yb == ymax && xe == xmax) || (ye == ymax && xb == xmax))
            {
                eTopology = ContourTopology.AdjacentSide;

                //增加(x1,y1)和(x2,y2)之间的若干点
                //不增加(x1,y1) 但增加 (x2,y2)
                AddBetweenPoint(xe, ye, xmax, ymax);
                AddBetweenPoint(xmax, ymax, xb, yb);
            }
            // 2-4. 右下角
            else if ((yb == ymin && xe == xmax) || (ye == ymin && xb == xmax))
            {
                eTopology = ContourTopology.AdjacentSide;

                //增加(x1,y1)和(x2,y2)之间的若干点
                //不增加(x1,y1) 但增加 (x2,y2)
                AddBetweenPoint(xe, ye, xmax, ymin);
                AddBetweenPoint(xmax, ymin, xb, yb);
            }

            // 3. 等值线的起止点在同一条边上
            else if (xb == xe || yb == ye)
            {
                eTopology = ContourTopology.SameSide;

                //增加(x1,y1)和(x2,y2)之间的若干点
                //不增加(x1,y1) 但增加 (x2,y2)
                AddBetweenPoint(xe, ye, xb, yb);
            }
            //
            // 按顺时针方向增加点 - end
            // 

            this.ContourTopology = eTopology;

            return eTopology;
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// 计算绑定矩形
        /// </summary>
        /// <returns></returns>
        private new IExtent ComputeBoundingBox()
        {
            int iPointCount = this.Count;
            if (iPointCount == 0)
                return new Extent();

            double xmin = double.MaxValue;
            double ymin = double.MaxValue;

            double xmax = double.MinValue;
            double ymax = double.MinValue;

            foreach (Point point in this)
            {
                xmin = Math.Min(xmin, point.X);
                ymin = Math.Min(ymin, point.Y);
                xmax = Math.Max(xmax, point.X);
                ymax = Math.Max(ymax, point.Y);
            }

            _Extent = new Extent(xmin, ymin, xmax, ymax);
            return _Extent;
        }

        /// <summary>
        /// 计算闭合曲线面积
        /// </summary>
        /// <returns></returns>
        private Double ComputeArea()
        {
            if (!this.IsClosed)
                return Double.NaN;

            int pointCount = this.Count;
            if (pointCount < 3)
                return Double.NaN;

            // 计算面积
            Point point;
            double x1, y1, x2, y2;
            double areas = 0;
            for (int i = 0; i < pointCount - 1; i++)
            {
                point = this[i] as Point;
                x1 = point.X;
                y1 = point.Y;

                point = this[i + 1] as Point;
                x2 = point.X;
                y2 = point.Y;

                areas += (x1 * y2 - x2 * y1);
            }

            //最后一点和第一点
            point = this[pointCount - 1] as Point;
            x1 = point.X;
            y1 = point.Y;

            point = this[0] as Point;
            x2 = point.X;
            y2 = point.Y;

            areas += (x1 * y2 - x2 * y1);

            return Math.Abs(areas);
        }

        #endregion
    }





































}
