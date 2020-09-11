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
using System.Drawing;

namespace CSharpKit.GeoApi.Geometries
{
    /// <summary>
    /// Legend
    /// TODO:[20171104,syc]只显示选中图层的图例
    /// </summary>
    public class Legend : Geometry, ILegend
    {
        #region Properties

        /// <summary>
        /// 内容(图片)
        /// </summary>
        public Object Content { get; set; }

        /// <summary>
        /// 宽度(像素)
        /// </summary>
        public new Double Width
        {
            get
            {
                return Content == null ? 0 : (Double)(Content as Image)?.Width;
            }
        }

        /// <summary>
        /// 高度(像素)
        /// </summary>
        public new Double Height
        {
            get
            {
                return Content == null ? 0 : (Double)(Content as Image).Height;
            }
        }

        /// <summary>
        /// 调色板
        /// </summary>
        // public IPalette Palette { get; set; }

        #endregion

        #region Public Functions

        /// <summary>
        /// 使图形偏移指定的量
        /// </summary>
        /// <param name="dx">X坐标的偏移量</param>
        /// <param name="dy">Y坐标的偏移量</param>
        public override void Offset(Double dx, Double dy)
        {
            this.Location.Offset(dx, dy);
            base.Offset(dx, dy);
        }

        #endregion

        //@@@
    }


}
