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
    /// 网格信息
    /// </summary>
    public interface IGridInfo
    {
        double MinX { get; set; }
        double MinY { get; set; }

        double MaxX { get; set; }
        double MaxY { get; set; }


        double IntervalX { get; set; }
        double IntervalY { get; set; }

        int Width { get; set; }
        int Height { get; set; }

        Boolean IsEmpty { get; }

        /// <summary>
        /// 实际坐标转换为格点坐标<br/>
        /// 如果在两个格点之间，取较小值
        /// </summary>
        /// <param name="x">横坐标 - 相当经度</param>
        /// <param name="y">纵坐标 - 相当纬度</param>
        /// <param name="I">格点行标</param>
        /// <param name="J">格点列标</param>
        /// <returns></returns>
        bool XY2IJ(double x, double y, out int I, out int J);

        /// <summary>
        /// 格点坐标转换为实际坐标
        /// </summary>
        /// <param name="x">横坐标 - 相当经度</param>
        /// <param name="y">纵坐标 - 相当纬度</param>
        /// <param name="I">格点行标</param>
        /// <param name="J">格点列标</param>
        /// <returns></returns>
        bool IJ2XY(int I, int J, out double x, out double y);



        IGridInfo Clone();

        //@@@
    }


}