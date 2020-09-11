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
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace CSharpKit
{
    /// <summary>
    /// 
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ary"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToEnumerable<T>(this Array ary)
        {
            IEnumerable<T> result = null;

            try
            {
                List<T> temp = new List<T>();

                foreach (var a in ary)
                {
                    temp.Add((T)Convert.ChangeType(a, typeof(T)));
                }

                result = temp;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.WriteLine(ex);
#endif
            }

            return result;

        }


        public static double MinimumAbsolute(this double[] data)
        {
            return Math.Abs(data.Min());
        }
        public static double MaximumAbsolute(this double[] data)
        {
            return Math.Abs(data.Max());
        }


        /// <summary>
        /// 算术平均
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double Mean(this double[] data)
        {
            if (data.Length == 0)
            {
                return double.NaN;
            }

            double mean = 0;
            ulong m = 0;
            for (int i = 0; i < data.Length; i++)
            {
                mean += (data[i] - mean) / ++m;
            }

            return mean;
        }

        /// <summary>
        /// 集合平均
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double GeometricMean(this double[] data)
        {
            if (data.Length == 0)
            {
                return double.NaN;
            }

            double sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += Math.Log(data[i]);
            }

            return Math.Exp(sum / data.Length);
        }

        /// <summary>
        /// 调和平均
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static double HarmonicMean(this double[] data)
        {
            if (data.Length == 0)
            {
                return double.NaN;
            }

            double sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += 1.0 / data[i];
            }

            return data.Length / sum;
        }

        /// <summary>
        /// 方差
        /// Estimates the unbiased population variance from the provided samples as unsorted array.
        /// On a dataset of size N will use an N-1 normalizer (Bessel's correction).
        /// Returns NaN if data has less than two entries or if any entry is NaN.
        /// </summary>
        /// <param name="samples">Sample array, no sorting is assumed.</param>
        public static double Variance(this double[] samples)
        {
            if (samples.Length <= 1)
            {
                return double.NaN;
            }

            double variance = 0;
            double t = samples[0];
            for (int i = 1; i < samples.Length; i++)
            {
                t += samples[i];
                double diff = ((i + 1) * samples[i]) - t;
                variance += (diff * diff) / ((i + 1.0) * i);
            }

            return variance / (samples.Length - 1);
        }


        /// <summary>
        /// 总方差
        /// Evaluates the population variance from the full population provided as unsorted array.
        /// On a dataset of size N will use an N normalizer and would thus be biased if applied to a subset.
        /// Returns NaN if data is empty or if any entry is NaN.
        /// </summary>
        /// <param name="population">Sample array, no sorting is assumed.</param>
        public static double PopulationVariance(this double[] population)
        {
            if (population.Length == 0)
            {
                return double.NaN;
            }

            double variance = 0;
            double t = population[0];
            for (int i = 1; i < population.Length; i++)
            {
                t += population[i];
                double diff = ((i + 1) * population[i]) - t;
                variance += (diff * diff) / ((i + 1.0) * i);
            }

            return variance / population.Length;
        }

        /// <summary>
        /// 均方根
        /// Estimates the root mean square (RMS) also known as quadratic mean from the unsorted data array.
        /// Returns NaN if data is empty or any entry is NaN.
        /// </summary>
        /// <param name="data">Sample array, no sorting is assumed.</param>
        public static double RootMeanSquare(this double[] data)
        {
            if (data.Length == 0)
            {
                return double.NaN;
            }

            double mean = 0;
            ulong m = 0;
            for (int i = 0; i < data.Length; i++)
            {
                mean += (data[i] * data[i] - mean) / ++m;
            }

            return Math.Sqrt(mean);
        }

        /// <summary>
        /// 标准偏差
        /// Estimates the unbiased population standard deviation from the provided samples as unsorted array.
        /// On a dataset of size N will use an N-1 normalizer (Bessel's correction).
        /// Returns NaN if data has less than two entries or if any entry is NaN.
        /// </summary>
        /// <param name="samples">Sample array, no sorting is assumed.</param>
        public static double StandardDeviation(this double[] samples)
        {
            return Math.Sqrt((samples).Variance());
        }
        /// <summary>
        /// 总标准偏差
        /// Evaluates the population standard deviation from the full population provided as unsorted array.
        /// On a dataset of size N will use an N normalizer and would thus be biased if applied to a subset.
        /// Returns NaN if data is empty or if any entry is NaN.
        /// </summary>
        /// <param name="population">Sample array, no sorting is assumed.</param>
        public static double PopulationStandardDeviation(this double[] population)
        {
            return Math.Sqrt((population).PopulationVariance());
        }



        /// <summary>
        /// 估计中值
        /// Estimates the median value from the unsorted data array.
        /// WARNING: Works inplace and can thus causes the data array to be reordered.
        /// </summary>
        /// <param name="data">Sample array, no sorting is assumed. Will be reordered.</param>
        public static double MedianInplace(this double[] data)
        {
            var k = data.Length / 2;
            return data.Length.IsOdd()
                ? SelectInplace(data, k)
                : (SelectInplace(data, k - 1) + SelectInplace(data, k)) / 2.0;
        }



        /// <summary>
        /// 从未排序的数据数组中估计 tau 分位数。<br/>
        /// </summary>
        /// <param name="data">Sample array, no sorting is assumed. Will be reordered.</param>
        /// <param name="tau">Quantile selector, between 0.0 and 1.0 (inclusive).</param>
        /// <remarks>
        /// 分位数是累积分布函数跨越tau的数据值。会导致数组重新排序<br/>
        /// R-8, SciPy-(1/3,1/3):
        /// Linear interpolation of the approximate medians for order statistics.
        /// When tau &lt; (2/3) / (N + 1/3), use x1. When tau &gt;= (N - 1/3) / (N + 1/3), use xN.
        /// </remarks>
        public static double QuantileInplace(this double[] data, double tau)
        {
            if (tau < 0d || tau > 1d || data.Length == 0)
            {
                return double.NaN;
            }

            double h = (data.Length + 1d / 3d) * tau + 1d / 3d;
            var hf = (int)h;

            if (hf <= 0 || tau == 0d)
            {
                return data.Min();
            }

            if (hf >= data.Length || tau == 1d)
            {
                return (data).Max();
            }

            var a = SelectInplace(data, hf - 1);
            var b = SelectInplace(data, hf);
            return a + (h - hf) * (b - a);
        }



        #region Copy

        public static void Copy<T>(this T[] source, T[] dest)
        {
            Buffer.BlockCopy(source, 0, dest, 0, source.Length * sizeof(double));
        }

        /// <summary>
        /// Copies the values from on array to another.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <param name="dest">The destination array.</param>
        public static void Copy(this double[] source, double[] dest)
        {
            Buffer.BlockCopy(source, 0, dest, 0, source.Length * sizeof(double));
        }

        /// <summary>
        /// Copies the values from on array to another.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <param name="dest">The destination array.</param>
        public static void Copy(this float[] source, float[] dest)
        {
            Buffer.BlockCopy(source, 0, dest, 0, source.Length * sizeof(double));
        }

        /// <summary>
        /// Copies the values from on array to another.
        /// </summary>
        /// <param name="source">The source array.</param>
        /// <param name="dest">The destination array.</param>
        public static void Copy(this Complex[] source, Complex[] dest)
        {
            Array.Copy(source, 0, dest, 0, source.Length);
        }

        #endregion











        /// <summary>
        /// 选择位置
        /// </summary>
        /// <param name="workingData"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        private static double SelectInplace(double[] workingData, int rank)
        {
            // Numerical Recipes: select
            // http://en.wikipedia.org/wiki/Selection_algorithm
            if (rank <= 0)
            {
                return (workingData).Min();
            }

            if (rank >= workingData.Length - 1)
            {
                return (workingData).Max();
            }

            double[] a = workingData;
            int low = 0;
            int high = a.Length - 1;

            while (true)
            {
                if (high <= low + 1)
                {
                    if (high == low + 1 && a[high] < a[low])
                    {
                        var tmp = a[low];
                        a[low] = a[high];
                        a[high] = tmp;
                    }

                    return a[rank];
                }

                int middle = (low + high) >> 1;

                var tmp1 = a[middle];
                a[middle] = a[low + 1];
                a[low + 1] = tmp1;

                if (a[low] > a[high])
                {
                    var tmp = a[low];
                    a[low] = a[high];
                    a[high] = tmp;
                }

                if (a[low + 1] > a[high])
                {
                    var tmp = a[low + 1];
                    a[low + 1] = a[high];
                    a[high] = tmp;
                }

                if (a[low] > a[low + 1])
                {
                    var tmp = a[low];
                    a[low] = a[low + 1];
                    a[low + 1] = tmp;
                }

                int begin = low + 1;
                int end = high;
                double pivot = a[begin];

                while (true)
                {
                    do
                    {
                        begin++;
                    }
                    while (a[begin] < pivot);

                    do
                    {
                        end--;
                    }
                    while (a[end] > pivot);

                    if (end < begin)
                    {
                        break;
                    }

                    var tmp = a[begin];
                    a[begin] = a[end];
                    a[end] = tmp;
                }

                a[low + 1] = a[end];
                a[end] = pivot;

                if (end >= rank)
                {
                    high = end - 1;
                }

                if (end <= rank)
                {
                    low = begin;
                }
            }
        }



        //}}@@@
    }

}
