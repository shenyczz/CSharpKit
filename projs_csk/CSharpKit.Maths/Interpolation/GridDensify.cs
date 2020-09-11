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
    /// 网格加密
    /// </summary>
    /// <remarks>
    /// if it works, I know it was written by
    /// ShenYongchen(shenyczz@163.com),lives in Zhengzhou,Henan.
    /// if not, then I don't know nothing.
    /// </remarks>
    public class GridDensify1
    {
        #region ---字段、属性---

        private GridParam _GridParamIntpu = null;
        /// <summary>
        /// 输入网格
        /// </summary>
        public GridParam GridParamIntpu
        {
            get { return _GridParamIntpu; }
            set { _GridParamIntpu = value; }
        }
        /// <summary>
        /// 输出网格
        /// </summary>
        public GridParam GridParamOutput { get; set; } = null;

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public GridDensify1()
        {
        }

        /// <summary>
        /// 实施网格加密
        /// </summary>
        public void Transact()
        {
            // 准备输出网格参数
            PrepareGridParamOutput();

            int nx = GridParamOutput.Xnumber;
            int ny = GridParamOutput.Ynumber;

            double xx, yy;

            for (int i = 0; i < ny; i++)
            {
                yy = GridParamOutput.Ymin + GridParamOutput.Yinterval * i;

                for (int j = 0; j < nx; j++)
                {
                    xx = GridParamOutput.Xmin + GridParamOutput.Xinterval * j;

                    // 在原来网格中进行线性插值
                    GridParamOutput.Vgrid[i, j] = InterpolateOnePoint(xx, yy);
                }
            }

        }// Transact - end

        /// <summary>
        /// 准备输出网格参数
        /// </summary>
        private void PrepareGridParamOutput()
        {
            if (GridParamIntpu == null || GridParamOutput == null)
                throw new IndexOutOfRangeException();

            double xmin, ymin, xmax, ymax;
	        // 网格的坐标范围
            xmin = GridParamIntpu.Xmin;
            ymin = GridParamIntpu.Ymin;
            xmax = GridParamIntpu.Xmin + GridParamIntpu.Xinterval * (GridParamIntpu.Xnumber - 0);
            ymax = GridParamIntpu.Ymin + GridParamIntpu.Yinterval * (GridParamIntpu.Ynumber - 0);

	        // 加密后的网格属性
            //
            GridParamOutput.Xmin = GridParamIntpu.Xmin;
            GridParamOutput.Ymin = GridParamIntpu.Ymin;
            // 加密后x方向格点数
            GridParamOutput.Xnumber = (int)Math.Abs((xmax - xmin) / GridParamOutput.Xinterval) + 1;
            // 加密后y方向格点数
            GridParamOutput.Ynumber = (int)Math.Abs((ymax - ymin) / GridParamOutput.Yinterval) + 1;

            //GridParamOutput.Xinterval = 不变
            //GridParamOutput.Yinterval = 不变

            // 格点数据
            GridParamOutput.Vgrid = new Double[GridParamOutput.Ynumber, GridParamOutput.Xnumber];
        }
        /// <summary>
        /// 对某个点插值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Double InterpolateOnePoint(Double x, Double y)
        {
            // 计算点(x,y)在源网格中的行号、列号
            int i = (int)((y - GridParamIntpu.Ymin) / GridParamIntpu.Yinterval);	//行号
            int j = (int)((x - GridParamIntpu.Xmin) / GridParamIntpu.Xinterval);	//列号

            if (i < 0 || j < 0)
                throw new IndexOutOfRangeException("行号或列号超出范围!");

	        // 取得所在网格4个顶点的坐标和要素值
            //
            int ii, jj;
            double[] xx = new double[4];
            double[] yy = new double[4];
            double[] vv = new double[4];

            ii = i; jj = j;
            // 如果是最后一行
            ii = ii > GridParamIntpu.Ynumber - 1 ? GridParamIntpu.Ynumber - 1 : ii;
            // 如果是最后一列
            jj = jj > GridParamIntpu.Xnumber - 1 ? GridParamIntpu.Xnumber - 1 : jj;
            // 取得格点数值
            xx[0] = GridParamIntpu.Xmin + GridParamIntpu.Xinterval * jj;
            yy[0] = GridParamIntpu.Ymin + GridParamIntpu.Yinterval * ii;
            vv[0] = GridParamIntpu.Vgrid[ii, jj];

            ii = i; jj = j + 1;
            // 如果是最后一行
            ii = ii > GridParamIntpu.Ynumber - 1 ? GridParamIntpu.Ynumber - 1 : ii;
            // 如果是最后一列
            jj = jj > GridParamIntpu.Xnumber - 1 ? GridParamIntpu.Xnumber - 1 : jj;
            // 取得格点数值
            xx[1] = GridParamIntpu.Xmin + GridParamIntpu.Xinterval * jj;
            yy[1] = GridParamIntpu.Ymin + GridParamIntpu.Yinterval * ii;
            vv[1] = GridParamIntpu.Vgrid[ii, jj];

            ii = i + 1; jj = j + 1;
            // 如果是最后一行
            ii = ii > GridParamIntpu.Ynumber - 1 ? GridParamIntpu.Ynumber - 1 : ii;
            // 如果是最后一列
            jj = jj > GridParamIntpu.Xnumber - 1 ? GridParamIntpu.Xnumber - 1 : jj;
            // 取得格点数值
            xx[2] = GridParamIntpu.Xmin + GridParamIntpu.Xinterval * jj;
            yy[2] = GridParamIntpu.Ymin + GridParamIntpu.Yinterval * ii;
            vv[2] = GridParamIntpu.Vgrid[ii, jj];

            ii = i + 1; jj = j;
            // 如果是最后一行
            ii = ii > GridParamIntpu.Ynumber - 1 ? GridParamIntpu.Ynumber - 1 : ii;
            // 如果是最后一列
            jj = jj > GridParamIntpu.Xnumber - 1 ? GridParamIntpu.Xnumber - 1 : jj;
            // 取得格点数值
            xx[3] = GridParamIntpu.Xmin + GridParamIntpu.Xinterval * jj;
            yy[3] = GridParamIntpu.Ymin + GridParamIntpu.Yinterval * ii;
            vv[3] = GridParamIntpu.Vgrid[ii, jj];

	        // 反距离权重插值
            //
            double[] dd = new double[4];    // 点(x,y)到所在网格四个顶点的距离的倒数
            double vvsum = 0, ddsum = 0;
            for (int k = 0; k < 4; k++)
            {
                double distance = Distance(x, y, xx[k], yy[k]);

                if (distance < 1.0e-12)
                    distance = 1.0e-6;

                dd[k] = 1.0f / distance;

                vvsum += vv[k] * dd[k];
                ddsum += dd[k];
            }

            return vvsum / ddsum;

        }// InterpolateOnePoint - end

        /// <summary>
        /// 计算两点距离
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <returns></returns>
        private Double Distance(Double x0, Double y0, Double x1, Double y1)
        {
            double dValue = 0;
            dValue = Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));
            return dValue;

        }// Distance - end

    }



    // class GridDensify - end
}
              
