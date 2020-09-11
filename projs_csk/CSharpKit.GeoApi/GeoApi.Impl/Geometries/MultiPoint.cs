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

namespace CSharpKit.GeoApi.Geometries
{
    /// <summary>
    /// MultiPoint - 多点
    /// </summary>
    public abstract class MultiPoint : GeometryCollection<IPoint>, IMultiPoint
    {
        public override void Offset(double dx, double dy)
        {
            foreach (var point in this)
            {
                point.Offset(dx, dy);
            }

            base.Offset(dx, dy);
        }
    }


}
