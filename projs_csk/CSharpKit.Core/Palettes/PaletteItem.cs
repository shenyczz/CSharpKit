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

namespace CSharpKit.Palettes
{
    /// <summary>
    /// PaletteItem - 调色板条目
    /// </summary>
    public class PaletteItem : IPaletteItem
    {
        #region Constructors

        public PaletteItem()
        {
            this.Value = 0;
            this.Color = Color.Transparent;
            this.Comment = "";
        }

        public PaletteItem(PaletteItem rhs)
        {
            this.Value = rhs.Value;
            this.Color = rhs.Color;
            this.Comment = rhs.Comment;
        }

        public PaletteItem(Double value, Color color)
            : this(value, color, value.ToString("F2")) { }

        public PaletteItem(Double value, Color color, String comment)
        {
            this.Value = value;
            this.Color = color;
            this.Comment = comment;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 数值
        /// </summary>
        public Double Value { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public String Comment { get; set; }

        #endregion

        #region ICloneable 成员

        public PaletteItem Clone()
        {
            return new PaletteItem(this.Value, this.Color, this.Comment);
        }

        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
