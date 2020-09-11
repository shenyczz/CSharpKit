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

namespace CSharpKit.Platform.Windows
{
    public sealed class Contants
    {
        private Contants() { }

 
        #region Menu Format

        /// <summary>
        /// Indicates that uIDEnableItem gives the identifier of the menu item.
        /// If neither the MF_BYCOMMAND nor MF_BYPOSITION flag is specified, the MF_BYCOMMAND flag is the default flag.
        /// </summary>
        public const ulong MF_BYCOMMAND = 0x00000000L;
        /// <summary>
        /// Indicates that the menu item is enabled and restored from a grayed state so that it can be selected.
        /// </summary>
        public const ulong MF_ENABLED = 0x00000000L;
        /// <summary>
        /// Indicates that the menu item is disabled and grayed so that it cannot be selected.
        /// </summary>
        public const ulong MF_GRAYED = 0x00000001L;
        /// <summary>
        /// Indicates that the menu item is disabled, but not grayed, so it cannot be selected.
        /// </summary>
        public const ulong MF_DISABLED = 0x00000002L;
        /// <summary>
        /// Indicates that uIDEnableItem gives the zero-based relative position of the menu item.
        /// </summary>
        public const ulong MF_BYPOSITION = 0x00000400L;

        #endregion

        // Multimonitor API.
        public const int MONITOR_DEFAULTTONULL = 0x00000000;
        public const int MONITOR_DEFAULTTOPRIMARY = 0x00000001;
        public const int MONITOR_DEFAULTTONEAREST = 0x00000002;

        // WINHTTP DLL Error range
        public const int WINHTTP_ERROR_BASE = 12000;
        public const int WINHTTP_ERROR_LAST = WINHTTP_ERROR_BASE + 184;

        // NETMSG DLL Error range
        public const int NERR_BASE = 2100;
        public const int MAX_NERR = NERR_BASE + 899;
    }


}
