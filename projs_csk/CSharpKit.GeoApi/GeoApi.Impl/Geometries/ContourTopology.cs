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
    // 等值线和矩形边界构成的多边形分4种情况
    // 1. 等值线的起止点在对边上
    // 2. 等值线的起止点在相邻边上
    // 3. 等值线的起止点在同一条边上
    // 4. 等值线是闭合的等值线
    public enum ContourTopology : int
    {
        /// <summary>
        /// 未知
        /// </summary>
        Unkonw = 0,
        /// <summary>
        /// 等值线的起止点在对边上
        /// </summary>
        OppositeSide = 1,
        /// <summary>
        /// 等值线的起止点在相邻边上
        /// </summary>
        AdjacentSide = 2,
        /// <summary>
        /// 等值线的起止点在同一条边上
        /// </summary>
        SameSide = 3,
        /// <summary>
        /// 等值线是闭合的等值线
        /// </summary>
        Close = 4,
    };

































}
