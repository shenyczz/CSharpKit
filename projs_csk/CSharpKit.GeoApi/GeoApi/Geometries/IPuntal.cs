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

namespace CSharpKit.GeoApi.Geometries
{
    /// <summary>
    /// Interface to identify all <c>IGeometry</c> subclasses
    /// that have a <c>Dimension</c> of <see cref="Dimension.Point"/>
    /// and have components that ar <see cref="IPoint"/>s.
    /// </summary>
    /// <seealso cref="ILineal"/>
    /// <seealso cref="IPolygonal"/>
    public interface IPuntal { }

}
