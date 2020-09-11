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

namespace CSharpKit
{
    /// <summary>
    /// 网格信息 - 不要IGridInfo
    /// </summary>
    public sealed class GridInfo : IGridInfo
    {
        #region Constructors

        GridInfo()
            : this((int)-1, (int)-1, 0.0F, 0.0F, double.NaN, double.NaN) { }

        GridInfo(GridInfo rhs)
            : this(rhs.Width, rhs.Height, rhs.MinX, rhs.MinY, rhs.IntervalX, rhs.IntervalY) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="ymin"></param>
        /// <param name="xmax"></param>
        /// <param name="ymax"></param>
        /// <param name="xinterval"></param>
        /// <param name="yinterval"></param>
        public GridInfo(Double xmin, Double ymin, Double xmax, Double ymax, Double xinterval, Double yinterval)
        {
            MinX = xmin;
            MinY = ymin;

            _MaxX = xmax;
            _MaxY = ymax;

            IntervalX = xinterval;
            IntervalY = yinterval;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnum">X向格点数</param>
        /// <param name="ynum">Y向格点数</param>
        /// <param name="xmin">X向最小值</param>
        /// <param name="ymin">Y向最小值</param>
        /// <param name="xinterval">X向间隔</param>
        /// <param name="yinterval">Y向间隔</param>
        public GridInfo(int xnum, int ynum, Double xmin, Double ymin, Double xinterval, Double yinterval)
        {
            MinX = xmin;
            MinY = ymin;

            _Width = xnum;
            _Height = ynum;

            IntervalX = xinterval;
            IntervalY = yinterval;
        }

        #endregion


        #region Empty

        static GridInfo _Empty = new GridInfo();
        public static GridInfo Empty => _Empty;

        public bool IsEmpty
        {
            get
            {
                return false
                    || Width <= 0
                    || Height <= 0
                    ;
            }
        }

        #endregion


        #region MinX

        /// <summary>
        /// 网格点 X 向最小值
        /// </summary>
        public Double MinX { get; set; }

        #endregion

        #region MinY

        /// <summary>
        /// 网格点 Y 向最小值
        /// </summary>
        public Double MinY { get; set; }

        #endregion


        #region IntervalX

        /// <summary> 
        /// X 方向网格点间隔
        /// </summary>
        public Double IntervalX { get; set; }

        #endregion

        #region IntervalY

        /// <summary>
        /// Y 方向网格点间隔 
        /// </summary>
        public Double IntervalY { get; set; }

        #endregion


        #region MaxX

        private Double _MaxX = Double.NegativeInfinity;
        /// <summary>
        /// 网格点 X 向最大值
        /// </summary>
        public Double MaxX
        {
            get
            {
                if(Double.IsNegativeInfinity(_MaxX))
                {
                    _MaxX = MinX + IntervalX * (_Width - 1);
                }
                return _MaxX;
            }

            set => _MaxX = value;
        }

        #endregion

        #region MaxY

        private Double _MaxY = Double.NegativeInfinity;
        /// <summary>
        /// 网格点 y 向最大值
        /// </summary>
        public Double MaxY
        {
            get
            {
                if (Double.IsNegativeInfinity(_MaxY))
                {
                    _MaxY = MinY + IntervalY * (_Height - 1);
                }

                return _MaxY;
            }

            set => _MaxY = value;
        }

        #endregion


        #region Width

        private Int32 _Width = -1;
        /// <summary>
        /// 网格宽度
        /// </summary>
        public Int32 Width
        {
            get
            {
                if (_Width < 0)
                {

                    _Width = Math.Abs(IntervalX) > 0
                        ? (Int32)(Math.Abs((_MaxX - MinX) / IntervalX) + 1)
                        : -1;
                }
                return _Width;
            }

            set => _Width = value;
        }

        #endregion

        #region Height

        private Int32 _Height = -1;
        /// <summary>
        /// 网格高度
        /// </summary>
        public Int32 Height
        {
            get
            {
                if (_Height < 0)
                {
                    _Height = Math.Abs(IntervalY) > 0
                        ? (Int32)(Math.Abs((_MaxY - MinY) / IntervalY) + 1)
                        : -1;
                }

                return _Height;
            }

            set => _Height = value;
        }

        #endregion



        #region 坐标转换

        public bool LonLat2RowCol(double lon, double lat, out int row, out int col)
        {
            return XY2IJ(lon, lat, out row, out col);
        }

        public bool RowCol2LonLat(int row, int col, out double lon, out double lat)
        {
            return IJ2XY(row, col, out lon, out lat);
        }

        /// <summary>
        /// 实际坐标转换为格点坐标<br/>
        /// 如果在两个格点之间，取较小值
        /// </summary>
        /// <param name="x">横坐标 - 相当经度</param>
        /// <param name="y">纵坐标 - 相当纬度</param>
        /// <param name="I">格点行标</param>
        /// <param name="J">格点列标</param>
        /// <returns></returns>
        public bool XY2IJ(double x, double y, out int I, out int J)
        {
            J = (int)((x - MinX) / IntervalX + 0.0);    // 不进行4舍5入
            I = (int)((y - MinY) / IntervalY + 0.0);    // 不进行4舍5入
            return true
                && (J >= 0 && J <= Width)
                && (I >= 0 && I <= Height)
                ;
        }
        /// <summary>
        /// 格点坐标转换为实际坐标
        /// </summary>
        /// <param name="x">横坐标 - 相当经度</param>
        /// <param name="y">纵坐标 - 相当纬度</param>
        /// <param name="I">格点行标</param>
        /// <param name="J">格点列标</param>
        /// <returns></returns>
        public bool IJ2XY(int I, int J, out double x, out double y)
        {
            x = MinX + IntervalX * J;
            y = MinY + IntervalY * I;
            return true
                && (x >= MinX && x <= MaxX)
                && (y >= MinY && y <= MaxY)
                ;
        }

        #endregion


        public IGridInfo Clone()
        {
            return new GridInfo(this);
        }


        //@@@
    }

}
