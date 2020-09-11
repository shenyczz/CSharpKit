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

namespace CSharpKit.GeoApi.CoordinateSystems.Transformations
{
    /// <summary>
    /// Semantic type of transform used in coordinate transformation.
    /// </summary>
    public enum TransformType : int
    {
        /// <summary>
        /// Unknown or unspecified type of transform.
        /// </summary>
        Other = 0,

        /// <summary>
        /// Transform depends only on defined parameters. For example, a cartographic projection.
        /// 转换只取决于定义的参数。 例如，制图投影。
        /// </summary>
        Conversion = 1,

        /// <summary>
        /// Transform depends only on empirically derived parameters. For example a datum transformation.
        /// 变换只取决于经验导出的参数。 例如基准转换。
        /// </summary>
        Transformation = 2,

        /// <summary>
        /// Transform depends on both defined and empirical parameters.
        /// 变换取决于定义参数和经验参数。
        /// </summary>
        ConversionAndTransformation = 3

        //@@@
    }


}
