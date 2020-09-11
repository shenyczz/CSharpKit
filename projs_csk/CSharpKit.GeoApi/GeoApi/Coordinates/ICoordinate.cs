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

namespace CSharpKit.GeoApi.Geometries
{
    /// <summary>
    /// ICoordinate - 坐标接口
    /// </summary>
    public interface ICoordinate
    {
        Double X { get; set; }
        Double Y { get; set; }
        Double Z { get; set; }

        double this[Ordinate index] { get; set; }

        double Distance(ICoordinate other);

        bool Equals2D(ICoordinate other);

        bool Equals3D(ICoordinate other);
    }

}
