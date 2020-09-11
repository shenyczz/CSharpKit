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
using System.Runtime.InteropServices;

namespace CSharpKit.GeoApi.Geometries
{
    /// <summary>
    /// 线环
    /// </summary>
    public class LinearRing : LinearString, ILinearRing
    {
        /// <summary>
        /// 是否逆时针方向<br/>
        /// Gets a value indicating whether this ring is oriented counter-clockwise(逆时针方向).
        /// </summary>
        public bool IsCCW { get=>true; }

    }

}
