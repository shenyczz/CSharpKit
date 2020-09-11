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
/******************************************************************************
 *
 *    Usage: 
 *    // 1.声明等值线构造器
 *    ContourBuilder cb = new ContourBuilder();
 *    // 2.设置网格信息
 *    cb.Grid = gridInfo;
 *    // 3.设置网格数据
 *    cb.GridValue = double[,];
 *    // 4.设置等值线管理器
 *    cb.ContourManager = new ContourManager();
 *    cb.ContourManager.SetTraceValue(...);
 *    // 5.设置等值线追踪器
 *    cb.ContourTracer = new ContourTracer();
 *    // 6.追踪等值线
 *    cb.BuildContours();
 *    
 *    // 等值线追踪结果：
 *    ContourPlexCollection cpc = GetContourPlexCollection(ContourStates contourState)；
 *    
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using CSharpKit;
using CSharpKit.GeoApi;
using CSharpKit.GeoApi.Geometries;
using GeoPoint = CSharpKit.GeoApi.Geometries.Point;

namespace CSharpKit.Maths.ContourTracing
{
    /// <summary>
    /// ContourBuilder - 等值线构造器
    /// </summary>
    public class ContourBuilder
    {
        #region Constructors

        public ContourBuilder()
            : this(null) { }

        public ContourBuilder(ContourTracer contourTracer)
        {
            this.ContourTracer = contourTracer;
            _ContourPlexCollection = new ContourPlexCollection();
            _ContourPlexCollection_closed = new ContourPlexCollection();
        }

        #endregion

        #region Fields

        private ContourPlexCollection _ContourPlexCollection = null;
        private ContourPlexCollection _ContourPlexCollection_closed = null;

        #endregion

        #region Events

        //public event EventHandler<ProgressingEventArgs> Progressing;
        //private void FireEvent_Progressing(object sender, ProgressingEventArgs e)
        //{
        //    if (Progressing != null)
        //    {
        //        Progressing(sender, e);
        //    }
        //}

        #endregion

        #region Properties

        /// <summary>
        /// 网格信息
        /// </summary>
        public IGridInfo GridInfo { get; set; }

        /// <summary>
        /// 格点数据
        /// </summary>
        public Double[,] GridValues { get; set; }

        /// <summary>
        /// 等值线管理器
        /// </summary>
        public ContourManager ContourManager { set; get; }

        /// <summary>
        /// 等值线追踪器
        /// </summary>
        public ContourTracer ContourTracer { get; set; }

        #endregion

        #region Public Functions

        /// <summary>
        /// 等值线集
        /// </summary>
        public ContourPlexCollection GetContourPlexCollection(ContourStates contourState)
        {
            return contourState == ContourStates.Original
                    ? _ContourPlexCollection
                    : _ContourPlexCollection_closed;
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public void Clear()
        {
            if (_ContourPlexCollection != null)
                _ContourPlexCollection.Clear();

            if (_ContourPlexCollection_closed != null)
                _ContourPlexCollection_closed.Clear();
        }

        /// <summary>
        /// 构造等值线
        /// </summary>
        /// <returns></returns>
        public virtual Boolean BuildContours()
        {
            int i;

            //if (Grid == null)
            //    return false;
            if (GridInfo.Width==0)
                return false;

            if (GridValues == null)
                return false;

            // 等值线值数量
            if (ContourManager.TraceValues.Count <= 0)
                return false;

            // 删除全部等值线
            _ContourPlexCollection.Clear();
            _ContourPlexCollection_closed.Clear();

            // 设置等值线追踪器
            this.ContourTracer.Grid = this.GridInfo;
            this.ContourTracer.GridValue = this.GridValues;

            // 要追踪的等值线值数量
            int iContourValueCount = this.ContourManager.TraceValues.Count;
            for (i = 0; i < iContourValueCount; i++)
            {
                // 设置追踪值
                double dTraceValue = this.ContourManager.TraceValues[i];
                this.ContourTracer.TraceValue = dTraceValue;

                // 追踪等值线
                bool bTrace = ContourTracer.Tracing();
                if (!bTrace)
                    continue;

                // 获取等值线
                ContourPlex contourPlex = ContourTracer.ContourPlex;
                if (contourPlex == null || contourPlex.Count <= 0)
                    continue;

                // 为等值线组赋值
                contourPlex.Value = dTraceValue;

                // 等值线搜集
                _ContourPlexCollection.Add(contourPlex);

            }// next i

            // 追踪辅助等值线
            // ...

            // 计算邦定矩形
            // ...

            // 把所有等值线转化为闭合的曲线
            // 用于绘制等值线色斑图
            this.ConvertToCloseContour();

            return true;
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// 计算包围盒
        /// </summary>
        private void ComputeBoundingBox()
        {
            // waiting...
        }

        /// <summary>
        /// 把所有等值线转化为闭合的曲线,用于绘制等值线色斑图
        /// </summary>
        private void ConvertToCloseContour()
        {
            int i, j, k;

            // 原始等值线集
            ContourPlexCollection contourPlexCollection_original = this.GetContourPlexCollection(ContourStates.Original);
            int iContourPlexCount = contourPlexCollection_original.Count;
            if (iContourPlexCount == 0)
                return;

            // 新的等值线集(闭合等值线集)
            ContourPlexCollection contourPlexCollection_closed = this.GetContourPlexCollection(ContourStates.Closed);
            contourPlexCollection_closed.Clear();

            // 有5组等值线
            // 0. 最大的等值线组(剪切矩形)
            // 1. 等值线的起止点在对边上
            // 2. 等值线的起止点在相邻边上
            // 3. 等值线的起止点在同一条边上
            // 4. 等值线是闭合的等值线
            for (i = 0; i < 5; i++)
            {
                contourPlexCollection_closed.Add(new ContourPlex());
            }

            // 新的等值线集中的等值线由原始等值线转换而来
            // 全部是闭合的曲线
            // 以闭合曲线的面积大小排序
            int nx = GridInfo.Width;
            int ny = GridInfo.Height;
            double xmin = GridInfo.MinX;
            double ymin = GridInfo.MinY;
            double xinterval = GridInfo.IntervalX;
            double yinterval = GridInfo.IntervalY;
            double xmax = xmin + xinterval * (nx - 1);
            double ymax = ymin + yinterval * (ny - 1);

            //-------------------------------------------------
            // 构造一个最大的闭合曲线(顺时针)
            //
            // P1(xmin,ymax) A-------------> P2(xmax,ymax)
            //               |             |
            //               |             |
            //               |             |
            //               |             |
            // P0(xmin,ymin) O<------------V P3(xmax,ymin)
            //
            Contour contourBox = new Contour();
            contourBox.Add(new GeoPoint(xmin, ymin));  // 左下角(P0)
            contourBox.AddBetweenPoint(xmin, ymin, xmin, ymax);	            // 左下角(P0) -> 左上角(P1)
            contourBox.AddBetweenPoint(xmin, ymax, xmax, ymax);	            // 左上角(P1) -> 右上角(P2)
            contourBox.AddBetweenPoint(xmax, ymax, xmax, ymin);	            // 右上角(P2) -> 右下角(P3)
            contourBox.AddBetweenPoint(xmax, ymin, xmin, ymin);	            // 右下角(P3) -> 左下角(P0)

            contourBox.Id = "ContourBox";
            contourBox.Value = this.ContourManager.TraceValues[0];
            contourBox.Extent = new Extent(xmin, ymin, xmax, ymax);

            // 保存
            contourPlexCollection_closed[0].Add(contourBox);
            //-------------------------------------------------
            for (i = 0; i < iContourPlexCount; i++)
            {
                // 原始等值线
                ContourPlex contourPlex = (ContourPlex)contourPlexCollection_original[i];
                int iContourCount = contourPlex.Count;
                for (j = 0; j < iContourCount; j++)
                {
                    // 新的曲线
                    Contour curveNew = new Contour();
                    // 原来曲线
                    Contour curveOld = contourPlex[j] as Contour;

                    // 标识
                    curveNew.Id = curveOld.Id;
                    // 曲线值
                    curveNew.Value = curveOld.Value;
                    // 范围
                    curveNew.Extent = (curveOld as Contour).Extent.Clone() as Extent;

                    int iPointCount = curveOld.Count;
                    for (k = 0; k < iPointCount; k++)
                    {
                        GeoPoint point = curveOld[k] as GeoPoint;
                        curveNew.Add(new GeoPoint(point.X, point.Y));
                    }// next k

                    // 曲线和包围矩形的拓扑关系
                    ContourTopology eTopology = curveNew.BuildCloseContour(xmin, ymin, xmax, ymax);
                    if (eTopology == ContourTopology.Unkonw)
                        continue;

                    // 根据曲线面积以及曲线与包围盒的拓扑关系为等值线添加闭合曲线(降序)
                    ContourPlex contourPlexNew = contourPlexCollection_closed[(int)eTopology];
                    contourPlexNew.Add(curveNew);

                    if (contourPlexNew.Count > 1)
                    {
                        // 根据面积排序(降序)
                        contourPlexNew.Sort((x, y) =>
                        {
                            if (x == null || y == null)
                                return 0;

                            double xa = (x as Contour).Area;
                            double ya = (y as Contour).Area;

                            return xa < ya ? 1 : (xa > ya ? -1 : 0);
                        });
                    }

                }// next j

            }// next i

            //-------------------------------------------------
            //TODO:[20171108,syc]最大的闭合曲线的值不稳定？算法？
            // 重新设置最大的闭合曲线的值
            double dValue1 = -998;
            double dValue2 = -999;

            // 把所有等值线按面积降序排列(不包括contourBox)
            ContourPlex contourPlexSort = new ContourPlex();
            for (i = 1; i <= 4; i++)
            {
                ContourPlex contourTemp = (ContourPlex)contourPlexCollection_closed[i];
                int iCurveCount = contourTemp.Count;
                for (j = 0; j < iCurveCount; j++)
                {
                    contourPlexSort.Add(contourTemp[j]);
                }
            }

            if (contourPlexSort.Count == 0)
                return;

            // 根据面积排序(降序)
            contourPlexSort.Sort((x, y)=>
            {
                if (x == null || y == null)
                    return 0;

                double xa = (x as Contour).Area;
                double ya = (y as Contour).Area;
                return xa < ya ? 1 : (xa > ya ? -1 : 0);
            });

            // 最大面积曲线值
            Contour curveMaxArea = contourPlexSort[0] as Contour;
            dValue1 = curveMaxArea.Value;

            bool hasValue2 = false;
            int iCount = contourPlexSort.Count;
            for (i = 1; i < iCount; i++)
            {
                String id = contourPlexSort[i].Id;
                Contour curveTemp = this.FindCurve(contourPlexCollection_original, id);
                if (curveTemp == null)
                    continue;

                int pointCount = curveTemp.Count;
                if (pointCount < 3)
                    continue;

                // 取等值线上中间点(不稳定算法？)
                GeoPoint point = curveTemp[pointCount / 2] as GeoPoint;
                if (point == null)
                    continue;

                if (!curveMaxArea.PointInside(point.X, point.Y))
                {
                    dValue2 = curveTemp.Value;
                    hasValue2 = Math.Abs(dValue2 - dValue1) > 0;
                    if (hasValue2)
                    {
                        break;
                    }
                }

            }//next i

            //不稳定
            if (hasValue2)
            {
                contourBox.Value = 0.5 * (dValue1 + dValue2);
            }
            else
            {
                //contourBox.Value = this.ContourManager.TraceValues[0];
                contourBox.Value = contourBox.Value > 0 ? contourBox.Value * 0.5 : contourBox.Value * 1.5;
            }
            //-------------------------------------------------
            // 取平均值
            double v = 0;
            this.ContourManager.TraceValues.ForEach(p =>
            {
                v += p;
            });
            v /= this.ContourManager.TraceValues.Count;
            contourBox.Value = v;
            //-------------------------------------------------
            return;
        }

        /// <summary>
        /// 取得指定标记的曲线
        /// </summary>
        /// <param name="contours">等值线集合</param>
        /// <param name="idCurve">曲线标识</param>
        /// <returns></returns>
        private Contour FindCurve(ContourPlexCollection contours, String idCurve)
        {
            int contourCount = contours.Count;
            for (int i = 0; i < contourCount; i++)
            {
                ContourPlex contour = contours[i];
                int curveCount = contour.Count;
                for (int j = 0; j < curveCount; j++)
                {
                    Contour curve = contour[j] as Contour;
                    if (curve.Id == idCurve)
                    {
                        return curve;
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
