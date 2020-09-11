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
    /// 多边形
    /// </summary>
    public class Polygon : Polyline
    {

        /*
        
            int times = 10000;
            POINT[] kpts = new POINT[gps.Count];
            for (int i = 0; i < gps.Count; i++)
            {
                kpts[i].X = (int)(gps[i].X * times + 0.1);
                kpts[i].Y = (int)(gps[i].Y * times + 0.1);
            }

            IntPtr hRgn = Win32Api.CreatePolygonRgn(kpts, kpts.Length, FillModes.ALTERNATE);
            bool isInSide = Win32Api.PtInRegion(hRgn, (int)(x * times), (int)(y * times));

         */
    }

}
