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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// GetWindowLongPtr Gwl.UserData
    /// </summary>
    public enum GWL : int
    {
        /// <summary>
        /// Retrieves the user data associated with the window.
        /// This data is intended for use by the application that created the window.
        /// Its value is initially zero. 
        /// </summary>
        GWL_USERDATA = (-21),
        /// <summary>
        /// Retrieves the extended window styles. 
        /// </summary>
        GWL_EXSTYLE = (-20),
        /// <summary>
        /// Retrieves the window styles. 
        /// </summary>
        GWL_STYLE = (-16),
        /// <summary>
        /// Retrieves the identifier of the window. 
        /// </summary>
        GWL_ID = (-12),
        /// <summary>
        /// Retrieves a handle to the parent window, if there is one. 
        /// </summary>
        GWL_HWNDPARENT = (-8),
        /// <summary>
        /// Retrieves a handle to the application instance. 
        /// </summary>
        GWL_HINSTANCE = (-6),
        /// <summary>
        /// Retrieves the pointer to the window procedure,
        /// or a handle representing the pointer to the window procedure. 
        /// You must use the CallWindowProc function to call the window procedure. 
        /// </summary>
        WndProc = (-4),
    }

    // GWL - WLT
    public enum WindowLongType1 : int
    {
        WndProc = (-4),
        HInstance = (-6),
        HwndParent = (-8),
        Style = (-16),
        ExtendedStyle = (-20),
        UserData = (-21),
        Id = (-12)
    }


}
