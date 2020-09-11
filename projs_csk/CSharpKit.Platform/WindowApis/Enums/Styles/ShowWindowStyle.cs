/******************************************************************************
 * 
 * Announce: Designed by ShenYongchen(shenyczz@163.com),ZhengZhou,HeNan.
 *           Copyright (C) shenyc. All rights reserved.
 *
 *     Name: ShowWindowCommand
 *  Version: 
 * Function: 
 * 
 *   Author: 申永辰.郑州 (shenyczz@163.com)
 * DateTime: 2010 - 2013
 *  WebSite: 
 *
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// SWS
    /// </summary>
    [Flags]
    public enum ShowWindowStyle
    {
        SW_HIDE             = 0,
        SW_SHOWNORMAL       = 1,
        SW_NORMAL           = 1,
        SW_SHOWMINIMIZED    = 2,
        SW_SHOWMAXIMIZED    = 3,
        SW_MAXIMIZE         = 3,
        SW_SHOWNOACTIVATE   = 4,
        SW_SHOW             = 5,
        SW_MINIMIZE         = 6,
        SW_SHOWMINNOACTIVE  = 7,
        SW_SHOWNA           = 8,
        SW_RESTORE          = 9,
        SW_SHOWDEFAULT      = 10,
        SW_FORCEMINIMIZE    = 11,
        SW_MAX              = 11,
    }
}
