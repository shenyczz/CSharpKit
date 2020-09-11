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
using System.Text;

namespace CSharpKit.Maths.Interpolation
{
    /// <summary>
    /// 规则网格点到离散点插值器
    /// </summary>
    public class G2VInterpolater
    {
        #region ---字段、属性---

        /// <summary>
        /// 格点参数
        /// </summary>
        public GridParam GridParam { get; set; }
        /// <summary>
        /// 目标数据 X 坐标集
        /// </summary>
        public Double[] Xtarget { get; set; }
        /// <summary>
        /// 目标数据 Y 坐标集
        /// </summary>
        public Double[] Ytarget { get; set; }
        /// <summary>
        /// 目标数据 V 坐标集
        /// </summary>
        public Double[] Vtarget { get; set; }

        /// <summary>
        /// 源数据 X 坐标集
        /// </summary>
        private double[] _Xsource = null;
        /// <summary>
        /// 源数据 Y 坐标集
        /// </summary>
        private double[] _Ysource = null;
        /// <summary>
        /// 源数据 V 坐标集
        /// </summary>
        private double[] _Vsource = null;

        #endregion

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public G2VInterpolater()
        {
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

            Interp.Xsource = _Xsource;
            Interp.Ysource = _Ysource;
            Interp.Vsource = _Vsource;

            Interp.Xtarget = Xtarget;
            Interp.Ytarget = Ytarget;
            Interp.Vtarget = Vtarget;

            Interp.NumbersOfPoint = 5;  // 9

            Interp.Transact();

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
            _Xsource = new double[totalSize];
            _Ysource = new double[totalSize];
            _Vsource = new double[totalSize];

            for (int i = 0; i < ny; i++)
            {
                for (int j = 0; j < nx; j++)
                {
                    _Xsource[i * nx + j] = x0 + xInterval * j;
                    _Ysource[i * nx + j] = y0 + yInterval * i;
                    _Vsource[i * nx + j] = GridParam.Vgrid[i, j];
                }
            }

        }// ParseGridParam - end
    }
}
