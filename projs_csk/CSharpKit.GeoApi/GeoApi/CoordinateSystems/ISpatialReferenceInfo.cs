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

namespace CSharpKit.GeoApi.CoordinateSystems
{
    /// <summary>
    /// ISpatialReferenceInfo - 空间参考信息<br/>
    /// The ISpatialReferenceInfo interface defines the standard 
    /// information stored with spatial reference objects. This
    /// interface is reused for many of the spatial reference
    /// objects in the system.
    /// ISpatialReferenceInfo 接口定义了的标准信息。 该接口可用于系统中的许多空间参考对象。
    /// </summary>
    public interface ISpatialReferenceInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 权威机构<br/>
        /// e.g., "<c>EPSG</c>"<br/>
        /// Returns <c>CUSTOM</c> if this is a custom object.<br/>
        /// </summary>
        string Authority { get; }

        /// <summary>
        /// 机构代码
        /// </summary>
        long AuthorityCode { get; }

        /// <summary>
        /// 别名
        /// </summary>
        string Alias { get; }

        /// <summary>
        /// 缩写
        /// </summary>
        string Abbreviation { get; }

        /// <summary>
        /// 备注
        /// </summary>
        string Remarks { get; }

        /// <summary>
        /// 返回简单特性规范中定义的此空间引用对象的知名文本(Well-known text)<br/>
        /// </summary>
        string WKT { get; }

        /// <summary>
        /// 获取此对象的XML表示。
        /// </summary>
        string XML { get; }

        /// <summary>
        /// 参数相等<br/>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool EqualParams(object obj);

        //}}@@@
    }

}
