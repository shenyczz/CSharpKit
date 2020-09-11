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
 * Announce: Written by ShenYongchen(shenyczz@163.com),ZhengZhou,HeNan.
 *           All rights reserved.
 *
 *     Name: GridParam
 *  Version: 
 * Function: 
 *
 *   Author: 申永辰.郑州 (shenyczz@163.com)
 * DateTime: 2000 -
 * Web Site: 
 *
 * Modifier: 
 * DateTime: 
 *  Explain: 
 *
 *    Usage: 
 *
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpKit.Maths.Interpolation
{
    /// <summary>
    /// 格点参数
    /// </summary>
    public class GridParam
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public GridParam()
        {
            Xnumber = 0;
            Ynumber = 0;
            Xmin = 0;
            Ymin = 0;
            Xmax = 0;
            Ymax = 0;
            Xinterval = 0;
            Yinterval = 0;
            Vgrid = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="ymin"></param>
        /// <param name="xmax"></param>
        /// <param name="ymax"></param>
        /// <param name="xinterval"></param>
        /// <param name="yinterval"></param>
        public GridParam(Double xmin, Double ymin, Double xmax, Double ymax, Double xinterval, Double yinterval)
        {
            Xmin = xmin;
            Ymin = ymin;
            Xmax = xmax;
            Ymax = ymax;
            Xinterval = xinterval;
            Yinterval = yinterval;
            // 计算格点数
            Xnumber = (Int32)((Xmax - Xmin) / Xinterval + 1);
            Ynumber = (Int32)((Ymax - Ymin) / Yinterval + 1);

            Vgrid = new Double[Ynumber, Xnumber];
        }
        /// <summary>
        /// 参数构造函数
        /// </summary>
        /// <param name="xnumber"></param>
        /// <param name="ynumber"></param>
        /// <param name="xmin"></param>
        /// <param name="ymin"></param>
        /// <param name="xinterval"></param>
        /// <param name="yinterval"></param>
        public GridParam(Int32 xnumber, Int32 ynumber, Double xmin, Double ymin, Double xinterval, Double yinterval)
        {
            Xnumber = xnumber;
            Ynumber = ynumber;
            Xmin = xmin;
            Ymin = ymin;
            Xinterval = xinterval;
            Yinterval = yinterval;
            Vgrid = null;
            // 计算最大坐标
            Xmax = Xmin + Xinterval * (Xnumber - 1);
            Ymax = Ymin + Yinterval * (Xnumber - 1);

            Vgrid = new Double[Ynumber, Xnumber];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmin"></param>
        /// <param name="ymin"></param>
        /// <param name="xmax"></param>
        /// <param name="ymax"></param>
        /// <param name="xinterval"></param>
        /// <param name="yinterval"></param>
        /// <param name="vgrid"></param>
        public GridParam(Double xmin, Double ymin, Double xmax, Double ymax, Double xinterval, Double yinterval, Double[,] vgrid)
        {
            Xmin = xmin;
            Ymin = ymin;
            Xmax = xmax;
            Ymax = ymax;
            Xinterval = xinterval;
            Yinterval = yinterval;
            Vgrid = vgrid;
            // 计算格点数
            Xnumber = (Int32)((Xmax - Xmin) / Xinterval + 1);
            Ynumber = (Int32)((Ymax - Ymin) / Yinterval + 1);

            if (Vgrid == null)
                Vgrid = new Double[Ynumber, Xnumber];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xnumber"></param>
        /// <param name="ynumber"></param>
        /// <param name="xmin"></param>
        /// <param name="ymin"></param>
        /// <param name="xinterval"></param>
        /// <param name="yinterval"></param>
        /// <param name="vgrid"></param>
        public GridParam(Int32 xnumber, Int32 ynumber, Double xmin, Double ymin, Double xinterval, Double yinterval, Double[,] vgrid)
        {
            Xnumber = xnumber;
            Ynumber = ynumber;
            Xmin = xmin;
            Ymin = ymin;
            Xinterval = xinterval;
            Yinterval = yinterval;
            Vgrid = vgrid;
            // 计算最大坐标
            Xmax = Xmin + Xinterval * (Xnumber - 1);
            Ymax = Ymin + Yinterval * (Xnumber - 1);

            if (Vgrid == null)
                Vgrid = new Double[Ynumber, Xnumber];
        }

        /// <summary>
        /// X 方向格点数量
        /// </summary>
        public Int32 Xnumber { get; set; }
        /// <summary>
        /// Y 方向格点数量
        /// </summary>
        public Int32 Ynumber { get; set; }
        /// <summary>
        /// 网格点 X 向最小值
        /// </summary>
        public Double Xmin { get; set; }
        /// <summary>
        /// 网格点 y 向最小值
        /// </summary>
        public Double Ymin { get; set; }
        /// <summary>
        /// 网格点 x 向最大值
        /// </summary>
        public Double Xmax { get; set; }
        /// <summary>
        /// 网格点 y 向最大值
        /// </summary>
        public Double Ymax { get; set; }
        /// <summary>
        /// X 方向网格点间隔
        /// </summary>
        public Double Xinterval { get; set; }
        /// <summary>
        /// Y 方向网格点间隔
        /// </summary>
        public Double Yinterval { get; set; }
        /// <summary>
        /// 格点值
        /// </summary>
        public Double[,] Vgrid { get; set; }

        public Double GetX(int index)
        {
            return Xmin + Xinterval * index;
        }
        public Double GetY(int index)
        {
            return Ymin + Yinterval * index;
        }
    }
}
