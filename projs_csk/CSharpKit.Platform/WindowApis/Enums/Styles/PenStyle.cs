/******************************************************************************
 * 
 * Announce: Designed by ShenYongchen(shenyczz@163.com),ZhengZhou,HeNan.
 *           Copyright (C) shenyc. All rights reserved.
 *
 *     Name: PenStyle
 *  Version: 
 * Function: 
 * 
 *   Author: 申永辰.郑州 (shenyczz@163.com)
 * DateTime: 2010 - 2013
 *  WebSite: 
 *
******************************************************************************/
using System;

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// PenStyle - GDI 画笔风格
    /// </summary>
    public enum PenStyle
    {
        PS_SOLID        = 0,
        PS_DASH         = 1,   // -------
        PS_DOT          = 2,   // .......
        PS_DASHDOT      = 3,   // _._._._
        PS_DASHDOTDOT   = 4,   // _.._.._
        PS_NULL         = 5,
        PS_INSIDEFRAME  = 6,
        PS_USERSTYLE    = 7,
        PS_ALTERNATE    = 8,
    }
}
