/******************************************************************************
 * 
 * Announce: CSharp Kit, used to achieve data visualization.
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

namespace CSharpKit.Platform
{
    public static class POS
    {
        static POS() 
        {
            var os = Environment.OSVersion;
            var pid = os.Platform;
        }

        public static readonly PlatformID PID = Environment.OSVersion.Platform;


        public static bool IsWin32S => PID == PlatformID.Win32S;
        public static bool IsWin32Windows => PID == PlatformID.Win32Windows;
        public static bool IsWin32NT => PID == PlatformID.Win32NT;
        public static bool IsWinCE => PID == PlatformID.WinCE;
        public static bool IsUnix => PID == PlatformID.Unix;
        public static bool IsXbox => PID == PlatformID.Xbox;
        public static bool IsMacOSX => PID == PlatformID.MacOSX;

        //}}@@@
    }


}
