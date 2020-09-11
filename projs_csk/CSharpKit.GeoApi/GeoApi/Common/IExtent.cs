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

namespace CSharpKit.GeoApi
{
    /// <summary>
    /// 范围 Scope
    /// </summary>
    public interface IExtent : ICloneable, IOwner, IEquatable<IExtent>
    {
        /// <summary>
        /// 最小 X 坐标
        /// </summary>
        Double MinX { get; set; }

        /// <summary>
        /// 最小 Y 坐标
        /// </summary>
        Double MinY { get; set; }

        /// <summary>
        /// 最大 X 坐标
        /// </summary>
        Double MaxX { get; set; }

        /// <summary>
        /// 最大 Y 坐标
        /// </summary>
        Double MaxY { get; set; }

        /// <summary>
        /// 面积
        /// </summary>
        Double Area { get; }

        /// <summary>
        /// 宽度
        /// </summary>
        Double Width { get; }

        /// <summary>
        /// 高度
        /// </summary>
        Double Height { get; }

        /// <summary>
        /// 是否空
        /// </summary>
        Boolean IsEmpty { get; }


        /// <summary>
        /// 合并
        /// </summary>
        /// <param name="extent"></param>
        /// <returns></returns>
        IExtent Union(IExtent extent);

        /// <summary>
        /// 偏移
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        void Offset(Double dx, Double dy);

        /// <summary>
        /// 点在范围内部
        /// </summary>
        /// <param name="x">点的x坐标</param>
        /// <param name="y">点的y坐标</param>
        /// <returns>true or false</returns>
        Boolean PointInside(Double x, Double y);

        //@@@
    }












}
