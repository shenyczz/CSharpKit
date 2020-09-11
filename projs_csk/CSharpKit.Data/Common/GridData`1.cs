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
using System.Diagnostics;

namespace CSharpKit.Data
{
    public sealed class GridData<T> : IGridData<T>
    {
        #region Constructors

        public GridData() { }
        public GridData(T[,] elements, IGridInfo gridInfo)
        {
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
            GridInfo = gridInfo ?? throw new ArgumentNullException(nameof(gridInfo));
        }
        public GridData(IGridInfo gridInfo, T[,] elements)
        {
            GridInfo = gridInfo ?? throw new ArgumentNullException(nameof(gridInfo));
            Elements = elements ?? throw new ArgumentNullException(nameof(elements));
        }

        private GridData(GridData<T> rhs)
        {
            var gridInfo = rhs.GridInfo ?? throw new ArgumentNullException(nameof(rhs.GridInfo));
            var elements = rhs.Elements ?? throw new ArgumentNullException(nameof(rhs.Elements));

            int rows = gridInfo.Height;
            int cols = gridInfo.Width;

            GridInfo = gridInfo.Clone();
            Elements = new T[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Elements[i, j] = elements[i, j];
                }
            }
        }

        #endregion


        /// <summary>
        /// 网格信息
        /// </summary>
        public IGridInfo GridInfo { get; set; }

        /// <summary>
        /// 数据元素
        /// </summary>
        public T[,] Elements { get; set; }


        /// <summary>
        ///示意图<br/>
        ///
        ///    v00                v01              
        ///    (x0,y0)            (x1,y0)           
        ///              |vx0                      
        ///        |-----|----------|
        ///        |     |          |
        ///      --|-----P----------|--
        ///        |     |(x,y)     |
        ///        |     |          |
        ///        |     |          |
        ///        |-----|----------|
        ///              |vx1
        ///    v10                v11
        ///    (x0,y1)            (x1,y1)
        /// 
        /// <br/><br/>
        ///    1.先 x 方向线性插值<br/>
        ///    KMath.Linear(double x0, double x1, double v00, double v01, double x, out double vx0)<br/>
        ///    KMath.Linear(double x0, double x1, double v10, double v11, double x, out double vx1)<br/>
        ///<br/>
        ///    2.再 y 方向线性插值<br/>
        ///    KMath.Linear(double y0, double y1, double vx0, double vx1, double y, out double vxy)<br/>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="invalid">无效数据</param>
        /// <returns></returns>
        public T GetValue(double x, double y, T invalid = default)
        {
            var result = invalid;

            try
            {
                var vtemp = result;

                if (GridInfo.XY2IJ(x, y, out int i, out int j))
                {
                    // 保存4个顶点格点坐标
                    var I = new int[2] { i, i + 1 };
                    var J = new int[2] { j, j + 1 };

                    // 格点坐标转换为实际坐标
                    var b0 = GridInfo.IJ2XY(I[0], J[0], out double x0, out double y0);
                    var b1 = GridInfo.IJ2XY(I[1], J[1], out double x1, out double y1);

                    if (b0 && b1)
                    {
                        var X = new double[2] { x0, x1 };
                        var Y = new double[2] { y0, y1 };

                        // 4个顶点要素值
                        var V = new T[2, 2]
                        {
                            { Elements[I[0], J[0]], Elements[I[0], J[1]] },
                            { Elements[I[1], J[0]], Elements[I[1], J[1]] }
                        };

                        // 双线性插值
                        // 1.先 x 方向线性插值
                        // var vx0 = KMath.Linear(x, X[0], X[1], V[0, 0], V[0, 1]);
                        // var vx1 = KMath.Linear(x, X[0], X[1], V[1, 0], V[1, 1]);

                        // 2.再 y 方向线性插值
                        // var vxy = KMath.Linear(y, Y[0], Y[1], vx0, vx1);

                        // vtemp = vxy;
                    }
                }

                result = vtemp;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return result;
        }


        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public IGridData<T> Clone()
        {
            return new GridData<T>(this);
        }


        //}}@@@
    }

}
