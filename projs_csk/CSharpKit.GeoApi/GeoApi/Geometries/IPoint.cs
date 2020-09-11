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

    public interface IPoint : IGeometry
    {
        double X { get; set; }
        double Y { get; set; }
        double Z { get; set; }
        double M { get; set; }

        ICoordinateSequence CoordinateSequence { get; }

        //@EndOf(IPoint)
    }

}
