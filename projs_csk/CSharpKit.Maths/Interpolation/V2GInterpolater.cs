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
 *
 *    Usage: 
 *    1.
 *    V2GInterpolater v2g = new V2GInterpolater();
 *    
 *    2.
 *    v2g.Xsource = ?
 *    v2g.Ysource = ?
 *    v2g.Vsource = ?
 *    
 *    3.
 *    v2g.GridParam = ?
 *    
 *    4. 插值
 *    v2g.Transact()
 *    
 *    5. 结果在 GridParam.Vgrid[,]
 *
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpKit.Maths.Interpolation
{
    /// <summary>
    /// 离散点到规则网格点插值器
    /// </summary>
    public class V2GInterpolater
    {
        #region ---字段、属性---

        /// <summary>
        /// 源数据 X 坐标集
        /// </summary>
        public double[] Xsource { get; set; }
        /// <summary>
        /// 源数据 Y 坐标集
        /// </summary>
        public double[] Ysource { get; set; }
        /// <summary>
        /// 源数据 V 坐标集
        /// </summary>
        public double[] Vsource { get; set; }
        /// <summary>
        /// 格点参数
        /// </summary>
        public GridParam GridParam { get; set; }

        /// <summary>
        /// 目标数据 X 坐标集
        /// </summary>
        private Double[] _Xtarget = null;
        /// <summary>
        /// 目标数据 Y 坐标集
        /// </summary>
        private Double[] _Ytarget = null;
        /// <summary>
        /// 目标数据 V 坐标集
        /// </summary>
        private Double[] _Vtarget = null;

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public V2GInterpolater()
        {
            GridParam = null;
        }

        /// <summary>
        /// 插值处理
        /// </summary>
        public void Transact()
        {
            if (GridParam == null)
            {
                throw new InvalidOperationException("网格参数没有设置");
            }
            else
            {
                ParseGridParam();
            }

            V2VInterpolater Interp = new V2VInterpolater();

            Interp.Xsource = Xsource;
            Interp.Ysource = Ysource;
            Interp.Vsource = Vsource;

            Interp.Xtarget = _Xtarget;
            Interp.Ytarget = _Ytarget;
            Interp.Vtarget = _Vtarget;

            Interp.NumbersOfPoint = 5;

            Interp.Transact();

            int nx = GridParam.Xnumber;
            int ny = GridParam.Ynumber;
            GridParam.Vgrid = new double[ny, nx];
            for (int i = 0; i < ny; i++)
            {
                for (int j = 0; j < nx; j++)
                {
                    GridParam.Vgrid[i, j] = _Vtarget[i * nx + j];
                }
            }

        }// Transact - end

        /// <summary>
        /// 解析网格参数
        /// </summary>
        private void ParseGridParam()
        {
            int nx = GridParam.Xnumber;
            int ny = GridParam.Ynumber;
            double x0 = GridParam.Xmin;
            double y0 = GridParam.Ymin;
            double xInterval = GridParam.Xinterval;
            double yInterval = GridParam.Yinterval;

            int totalSize = nx * ny;
            _Xtarget = new double[totalSize];
            _Ytarget = new double[totalSize];
            _Vtarget = new double[totalSize];

            for (int i = 0; i < ny; i++)
            {
                for (int j = 0; j < nx; j++)
                {
                    _Xtarget[i * nx + j] = x0 + xInterval * j;
                    _Ytarget[i * nx + j] = y0 + yInterval * i;
                    _Vtarget[i * nx + j] = 0.0;
                }
            }

        }


        //}}@@@
    }


}
