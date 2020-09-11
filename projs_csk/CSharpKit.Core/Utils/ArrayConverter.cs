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
using System.CodeDom;
using System.Diagnostics;

namespace CSharpKit.Utils
{
    /// <summary>
    /// 数组转换器
    /// </summary>
    public sealed class ArrayConverter
    {
        ArrayConverter() { }
        public static readonly ArrayConverter Instance = new ArrayConverter();


        #region Array1D

        public void Array1D<U, V>(U[] uSource, out V[] vOut)
        {
            vOut = default;

            try
            {
                int len = uSource.Length;

                var vtype = typeof(V);
                V[] vdest = new V[len];

                for (int i = 0; i < len; i++)
                {
                    vdest[i] = (V)Convert.ChangeType(uSource[i], vtype);
                }

                vOut = vdest;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return;
        }

        #endregion

        #region Array1D2D

        public T[,] Array1D2D<T>(T[] tSource, int rows, int cols)
        {
            Array1D2D(tSource, rows, cols, out T[,] tOut);
            return tOut;
        }
        public void Array1D2D<T>(T[] tSource, int rows, int cols, out T[,] tOut)
        {
            tOut = null;

            try
            {
                var vtemp = tOut;
                {
                    vtemp = new T[rows, cols];
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            vtemp[i, j] = tSource[i * cols + j];
                        }
                    }
                }
                tOut = vtemp;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return;
        }

        #endregion

        #region Array2D

        public void Array2D<U, V>(U[,] uSource, out V[,] vOut)
        {
            Array2D(uSource,
                (V)Convert.ChangeType(1, typeof(V)),
                (V)Convert.ChangeType(0, typeof(V)),
                null,
                out vOut);
        }
        public void Array2D<U, V>(U[,] uSource, V scale, V offset, Predicate<double> isValid, out V[,] vOut)
        {
           vOut = default;

            try
            {
                int rows = uSource.GetLength(0);
                int cols = uSource.GetLength(1);

                dynamic sc = scale;
                dynamic of = offset;

                //var vtype = typeof(V);
                V[,] vdest = new V[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        dynamic sv = uSource[i, j];

                        if (isValid != null)
                        {
                            vdest[i, j] = isValid(sv) ? sv * sc + of : sv;
                        }
                        else
                        {
                            vdest[i, j] = sv * sc + of;
                        }
                    }
                }

                vOut = vdest;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return;
        }

        void Array2D_bak<U, V>(U[,] uSource, out V[,] vOut)
        {
            vOut = default;

            try
            {
                int rows = uSource.GetLength(0);
                int cols = uSource.GetLength(1);

                var vtype = typeof(V);
                V[,] vdest = new V[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        vdest[i, j] = (V)Convert.ChangeType(uSource[i, j], vtype);
                    }
                }

                vOut = vdest;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return;
        }
        void Array2D_bak<U, V>(U[,] uSource, V scale, V offset, out V[,] vOut)
        {
            vOut = default;

            try
            {
                int rows = uSource.GetLength(0);
                int cols = uSource.GetLength(1);

                dynamic sc = scale;
                dynamic of = offset;


                var vtype = typeof(V);
                V[,] vdest = new V[rows, cols];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        //dynamic v = uSource[i, j];
                        vdest[i, j] = uSource[i, j] * sc + of;
                        //vdest[i, j] = (V)Convert.ChangeType(uSource[i, j], vtype);
                    }
                }

                vOut = vdest;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return;
        }


        #endregion

        #region Array2D1D

        //public (T[] raws, int rows, int cols) Array2D1D<T>(T[,] datas)
        //{
        //    Array2D1D(datas, out T[] raws, out int rows, out int cols);
        //    return (raws, rows, cols);
        //}

        public T[] Array2D1D<T>(T[,] datas)
        {
            Array2D1D(datas, out T[] raws, out int rows, out int cols);
            return raws;
        }

        public void Array2D1D<T>(T[,] datas, out T[] raws, out int rows, out int cols)
        {
            rows = 0;
            cols = 0;
            raws = null;

            try
            {
                rows = datas.GetLength(0);
                cols = datas.GetLength(1);

                var vtemp = new T[cols * rows];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        vtemp[i * cols + j] = datas[i, j];
                    }
                }

                raws = vtemp;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif          
            }

            return;
        }

        #endregion

        #region ArrayMirrory2D

        public T[,] ArrayMirrory2D<T>(T[,] tSource)
        {
            ArrayMirrory2D(tSource, out T[,] tOut);
            return tOut;
        }
        public void ArrayMirrory2D<T>(T[,] tSource, out T[,] tOut)
        {
            tOut = default;

            try
            {
                var vtemp = tOut;
                {
                    int rows = tSource.GetLength(0);
                    int cols = tSource.GetLength(1);

                    vtemp = new T[rows, cols];
                    for (int i = 0; i < rows; i++)
                    {
                        int ii = (rows - 1) - i;
                        for (int j = 0; j < cols; j++)
                        {
                            vtemp[ii, j] = tSource[i, j];
                        }
                    }
                }
                tOut = vtemp;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return;
        }

        #endregion




        //}}@@@
    }


}
