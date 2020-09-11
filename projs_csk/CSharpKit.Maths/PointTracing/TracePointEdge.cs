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
using System.Collections.Generic;

namespace CSharpKit.Maths.PointTracing
{
    /// <summary>
    /// 边界
    /// </summary>
    public class TracePointEdge : TracePointCollectionBase
    {
        public TracePointEdge()
        {
        }
        public TracePointEdge(IEnumerable<TracePoint> points)
        {
            this.AddRange(points);
        }


        #region Centroid - 质心

        private TracePoint _Centroid;
        public TracePoint Centroid
        {
            get
            {
                if (_Centroid == null)
                {
                    CalcCentroid(this);
                }
                return _Centroid;
            }
        }
        /// <summary>
        /// 计算质心
        /// </summary>
        private void CalcCentroid(IList<TracePoint> points)
        {
            double sum_x = 0;
            double sum_y = 0;
            double sum_area = 0;

            if (points.Count < 3)
                return;

            TracePoint p0 = points[0];
            TracePoint p1 = points[1];
            for (int i = 0; i < points.Count; i++)
            {
                TracePoint p2 = points[i];
                double area = Area(p0, p1, p2);
                sum_area += area;
                sum_x += (points[0].Lon + p1.Lon + p2.Lon) * area;
                sum_y += (points[0].Lat + p1.Lat + p2.Lat) * area;
                p1 = p2;
            }

            double xx= sum_x / sum_area / 3;
            double yy = sum_y / sum_area / 3;

            _Centroid = new TracePoint
            {
                Lon = (float)xx,
                Lat = (float)yy
            };

        }

        private double Area(TracePoint p0, TracePoint p1, TracePoint p2)
        {
            double area = 0;
            area = p0.Lon * p1.Lat + p1.Lon * p2.Lat + p2.Lon * p0.Lat - p1.Lon * p0.Lat - p2.Lon * p1.Lat - p0.Lon * p2.Lat;
            return area / 2;
        }

        #endregion


        public override string ToString()
        {
            return string.Format("[TracePointEdge] Count={0} Centroid=({1:F3},{2:F3})",
                Count, Centroid.Lon, Centroid.Lat);
        }


        /// <summary>
        /// implicit - 隐式转换
        /// explicit - 显式转换
        /// </summary>
        /// <param name="points"></param>
        public static implicit operator TracePointEdge(List<TracePoint> points)
        {
            return new TracePointEdge(points);
        }
        /// <summary>
        /// implicit - 隐式转换
        /// </summary>
        /// <param name="points"></param>
        public static implicit operator List<TracePoint>(TracePointEdge points)
        {
            return new List<TracePoint>(points);
        }

    }



}
