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

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// WM_NCHITTEST and MOUSEHOOKSTRUCT Mouse Position Codes
    /// </summary>
    public enum HitTestOptions
    {
        HT_ERROR = (-2),
        HT_TRANSPARENT = (-1),
        HT_NOWHERE = 0,
        HT_CLIENT = 1,
        HT_CAPTION = 2,
        HT_SYSMENU = 3,
        HT_GROWBOX = 4,
        HT_SIZE = 4,
        HT_MENU = 5,
        HT_HSCROLL = 6,
        HT_VSCROLL = 7,
        HT_MINBUTTON = 8,
        HT_MAXBUTTON = 9,
        HT_LEFT = 10,
        HT_RIGHT = 11,
        HT_TOP = 12,
        HT_TOPLEFT = 13,
        HT_TOPRIGHT = 14,
        HT_BOTTOM = 15,
        HT_BOTTOMLEFT = 16,
        HT_BOTTOMRIGHT = 17,
        HT_BORDER = 18,
        HT_REDUCE = 8,
        HT_ZOOM = 9,
        HT_SIZEFIRST = 10,
        HT_SIZELAST = 17,
        HT_OBJECT = 19,
        HT_CLOSE = 20,
        HT_HELP = 21
    }
    public enum HitTest
    {
        HTERROR = -2,
        HTTRANSPARENT = -1,
        HTNOWHERE = 0,
        HTCLIENT = 1,
        HTCAPTION = 2,
        HTSYSMENU = 3,
        HTGROWBOX = 4,
        HTSIZE = 4,
        HTMENU = 5,
        HTHSCROLL = 6,
        HTVSCROLL = 7,
        HTMINBUTTON = 8,
        HTMAXBUTTON = 9,
        HTLEFT = 10,
        HTRIGHT = 11,
        HTTOP = 12,
        HTTOPLEFT = 13,
        HTTOPRIGHT = 14,
        HTBOTTOM = 15,
        HTBOTTOMLEFT = 16,
        HTBOTTOMRIGHT = 17,
        HTBORDER = 18,
        HTREDUCE = 8,
        HTZOOM = 9,
        HTSIZEFIRST = 10,
        HTSIZELAST = 17,
        HTOBJECT = 19,
        HTCLOSE = 20,
        HTHELP = 21
    }

}
