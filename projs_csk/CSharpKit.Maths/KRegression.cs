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

namespace CSharpKit.Maths
{
    /// <summary>
    /// Regression - 回归
    /// </summary>
    public static class KRegression
    {
        /// <summary>
        /// 一元线性回归分析
        /// </summary>
        /// <param name="x">一维数组,长度为n,存放自变量x的n个取值</param>
        /// <param name="y">一维数组,长度为n,存放与自变量x对应的随机变量y的观测值</param>
        /// <param name="n"></param>
        /// <param name="a">
        /// 一维数组,长度为2。
        ///     a[0]返回回归系数a0(截距)，
        ///     a[1]返回回归系数a1(斜率)
        /// </param>
        /// <param name="dt">
        /// 一维数组,长度为6。
        ///     dt[0]：返回偏差平方和
        ///     dt[1]：返回平均标准偏差
        ///     dt[2]：返回回归平方和
        ///     dt[3]：返回最大偏差
        ///     dt[4]：返回最小偏差
        ///     dt[5]：返回平均偏差
        /// </param>
        /// <remarks>
        /// 一元线性方程：y = a0 + a1*x
        /// </remarks>
        public static int Linear1D(double[] x, double[] y, int n, out double[] a, out double[] dt)
        {
            a = new double[2];
            dt = new double[6];

            try
            {
                if (x.Length != y.Length)
                    return -1;

                //if (x.Length != n)
                //    return -2;

                //int n = x.Length;
                int num = n;

                // x,y平均值
                double xavg = 0, yavg = 0;
                for (int i = 0; i < num; i++)
                {
                    xavg += x[i] / num;
                    yavg += y[i] / num;
                }

                // 计算方程系数
                double e = 0, f = 0;
                for (int i = 0; i < num; i++)
                {
                    double dx = x[i] - xavg;
                    double dy = y[i] - yavg;
                    e += dx * dx;
                    f += dx * dy;
                }
                a[1] = f / e;
                a[0] = yavg - a[1] * xavg;
                //---------------------
                double p = 0, q = 0;
                double u = 0, umax = 0, umin = (1.0e+30);
                for (int i = 0; i < num; i++)
                {
                    double s = a[1] * x[i] + a[0];

                    q += (y[i] - s) * (y[i] - s);
                    p += (s - yavg) * (s - yavg);
                    e = Math.Abs(y[i] - s);
                    if (e > umax) umax = e;
                    if (e < umin) umin = e;
                    u += e / num;
                }

                dt[0] = q;                  // 偏差平方和
                dt[1] = Math.Sqrt(q / num);   // 平均标准偏差
                dt[2] = p;                  // 回归平方和
                dt[3] = umax;               // 最大偏差
                dt[4] = umin;               // 最小偏差
                dt[5] = u;                  // 平均偏差

            }
            catch (Exception)
            {
                return -2;
            }

            return 0;

        }


        //
        // y=a0x0 + a1x1 + ... + a(m-1)x(m-1) + am
        // xy	-->矩阵,体积(m+1)*n;
        //         第i行存放第i个自变量xi的n个观测值(i=0,1,...,m-1)
        //         第m行(最后一行)存放随机变量y的n个观测值
        //
        //         返回时xy的内容将被破坏
        //         第0列xy(i,0)存放m+1个回归系数a0,a1,...,a(m)  (i=0,1,2,...,m)
        //         第1列xy(i,1)存放m个自变量的偏相关系数
        //         第2列xy(i,2)存放4个数值,
        //              xy(0,2)存放偏差平方和
        //              xy(1,2)存放平均标准偏差
        //              xy(2,2)存放复相关系数
        //              xy(3,2)存放回归平方和
        /// <summary>
        /// 多元线性回归分析(m个变量n个观测值,n>=4)
        /// </summary>
        /// <param name="xy"></param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        public static int LinearMD(double[,] xy)
        {
            try
            {
                // int i, j;

                // double r = (0), s = (0);
                // double q = (0), e = (0), u = (0);
                // double yy = (0);
                // double[] dt = new double[4];

                /*

	            int m = (int)xy.GetRowNum() - 1;			//自变量个数
	            int n = (int)xy.GetColNum();				//观测数据的组数

	            //为调用平方根法求解对称正定方程组函数准备，其两参数须matrix类型
	            KMatrix b((m+1),(m+1));
	            KMatrix aa(m+1,1);

	            double* v = new double[m+1];
	            double* y = new double[n];

	            for(i=0;i<m+1;i++)
	            {
		            v[i]=0.0;
	            }
	            for(i=0;i<n;i++)
	            {
		            y[i]=xy(m,i);
		            if(i<=4) dt[i]=0.0;
	            }

                int mm = m + 1;
	            b(m, m) = n;

                for( j=0; j<m; j++)
                {
		            double p(0);
                    for(int i=0; i<n; i++)	p = p + xy(j,i);
		            b(m,j) = p;
		            b(j,m) = p;
                }
	            //=====================
	            //b.Trace();
 	            //=====================
               for(i=0; i<m; i++)
                  for(j=i; j<m; j++)
                  {
		              double p(0);
                      for(int k=0; k<n; k++)	p=p+xy(i,k)*xy(j,k);
		              b(j,i) = p;
		              b(i,j) = p;

                  }
	            //=====================
	            //b.Trace();
	            //b.IsSymmetry();
	            //b.IsRegular();
 	            //=====================

	            aa(m,0) = 0.0;
                for(i=0; i<n; i++) aa(m,0) += y[i];
                for(i=0; i<m; i++)
                {
		            aa(i,0) = 0.0;
		            for(j=0; j<n; j++)	aa(i,0) = aa(i,0) + xy(i,j) * y[j];
                }
	            //=====================
	            //aa.Trace();
 	            //=====================

	            //调用平方根法求解对称正定方程组的函数
                if(KLinearEquation::SymmetryRegularEuationSquareRoot(b,aa)<1)
	            {
		            //TRACE("Matrix is not Symmetry and Regular!\n");
		            goto END;
	            }
    
                for (i=0; i<n; i++)	yy = yy + y[i] / n;
                for(i=0; i<n; i++)
                {
		            double p = aa(m,0);
		            for(j=0; j<m; j++)	p = p + aa(j,0) * xy(j,i);
                    q=q+(y[i]-p)*(y[i]-p);
                    e=e+(y[i]-yy)*(y[i]-yy);
                    u=u+(yy-p)*(yy-p);
                }
                s = sqrt(q/n);
                r = sqrt(1.0-q/e);

                for(j=0; j<m; j++)
                {
		            double p(0);
                    for (i=0; i<n; i++)
                    {
			            double pp = aa(m,0);
                        for(int k=0; k<m; k++)
                          if(k!=j) pp = pp + aa(k,0) * xy(k,i);
                        p = p + (y[i] - pp) * (y[i] - pp);
                    }
                    v[j] = sqrt(1.0 - q / p);
                }
                dt[0] = q;
	            dt[1] = s;
	            dt[2] = r;
	            dt[3] = u;

            END:
	            //---
	            v[m]=0.0;
	            xy.SetSize(m+1,n);
	            for(i=0;i<m+1;i++)
	            {
		            xy(i,0)=aa(i,0);
		            xy(i,1)=v[i];
		            if(i<=4)	xy(i,2)=dt[i];
	            }
	            //---
	            delete []v;
	            delete []y;
	            //---
	            return;
                 */

            }
            catch (Exception)
            {
                return -2;
            }

            return 0;
        }


        ////逐步回归分析
        //static void Stepwise(KMatrix& xy, double f1, double f2,
        //    double* b, double* xx, double* v, double* s,
        //    double* ye, double* yr,
        //    KMatrix& r,
        //    double dt[2],
        //    double eps = 1.0e-12);


        // 逻辑回归

        // BP、SVM、


    }


}
