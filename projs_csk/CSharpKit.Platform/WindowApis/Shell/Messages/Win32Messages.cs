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
using System.Diagnostics;

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// Win32Messages
    /// </summary>
    public sealed partial class Win32Messages
    {
        private Win32Messages() { }
        String _str = "";
    }


    /// <summary>
    /// NativeMessages
    /// </summary>
    partial class Win32Messages
    {
        public const int WM_NULL = 0x0000;
        public const int WM_CREATE = 0x0001;
        public const int WM_DESTROY = 0x0002;
        public const int WM_MOVE = 0x0003;
        public const int WM_SIZE = 0x0005;
        public const int WM_ACTIVATE = 0x0006;
        public const int WM_SETFOCUS = 0x0007;
        public const int WM_KILLFOCUS = 0x0008;
        public const int WM_ENABLE = 0x000A;
        public const int WM_SETREDRAW = 0x000B;
        public const int WM_SETTEXT = 0x000C;
        public const int WM_GETTEXT = 0x000D;
        public const int WM_GETTEXTLENGTH = 0x000E;
        public const int WM_PAINT = 0x000F;
        public const int WM_CLOSE = 0x0010;
        public const int WM_QUIT = 0x0012;
        public const int WM_ERASEBKGND = 0x0014;
        public const int WM_SYSCOLORCHANGE = 0x0015;
        public const int WM_SHOWWINDOW = 0x0018;
        public const int WM_WININICHANGE = 0x001A;
        public const int WM_DEVMODECHANGE = 0x001B;
        public const int WM_ACTIVATEAPP = 0x001C;
        public const int WM_FONTCHANGE = 0x001D;
        public const int WM_TIMECHANGE = 0x001E;
        public const int WM_CANCELMODE = 0x001F;
        public const int WM_SETCURSOR = 0x0020;
        public const int WM_MOUSEACTIVATE = 0x0021;
        public const int WM_CHILDACTIVATE = 0x0022;
        public const int WM_QUEUESYNC = 0x0023;
        public const int WM_GETMINMAXINFO = 0x0024;
        public const int WM_PAINTICON = 0x0026;
        public const int WM_ICONERASEBKGND = 0x0027;
        public const int WM_NEXTDLGCTL = 0x0028;
        public const int WM_SPOOLERSTATUS = 0x002A;
        public const int WM_DRAWITEM = 0x002B;
        public const int WM_MEASUREITEM = 0x002C;
        public const int WM_DELETEITEM = 0x002D;
        public const int WM_VKEYTOITEM = 0x002E;
        public const int WM_CHARTOITEM = 0x002F;
        public const int WM_SETFONT = 0x0030;
        public const int WM_GETFONT = 0x0031;
        public const int WM_SETHOTKEY = 0x0032;
        public const int WM_GETHOTKEY = 0x0033;
        public const int WM_QUERYDRAGICON = 0x0037;
        public const int WM_COMPAREITEM = 0x0039;

        public const int WM_WINDOWPOSCHANGED = 0x0047;
        public const int WM_WINDOWPOSCHANGING = 0x0046;
        public const int WM_COPYDATA = 0x004A;
        public const int WM_CANCELJOURNAL = 0x004B;
        public const int WM_NOTIFY = 0x004E;
        public const int WM_INPUTLANGCHANGEREQUEST = 0x0050;
        public const int WM_INPUTLANGCHANGE = 0x0051;
        public const int WM_TCARD = 0x0052;
        public const int WM_HELP = 0x0053;
        public const int WM_USERCHANGED = 0x0054;
        public const int WM_NOTIFYFORMAT = 0x0055;

        public const int WM_CONTEXTMENU = 0x007B;
        public const int WM_STYLECHANGING = 0x007C;
        public const int WM_STYLECHANGED = 0x007D;
        public const int WM_DISPLAYCHANGE = 0x007E;
        public const int WM_GETICON = 0x007F;
        public const int WM_SETICON = 0x0080;

        // 非客户区消息
        public const int WM_NCCREATE = 0x0081;
        public const int WM_NCDESTROY = 0x0082;
        public const int WM_NCCALCSIZE = 0x0083;
        public const int WM_NCHITTEST = 0x0084;
        public const int WM_NCPAINT = 0x0085;
        public const int WM_NCACTIVATE = 0x0086;
        public const int WM_NCMOUSEMOVE = 0x00A0;
        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int WM_NCLBUTTONUP = 0x00A2;
        public const int WM_NCLBUTTONDBLCLK = 0x00A3;
        public const int WM_NCRBUTTONDOWN = 0x00A4;
        public const int WM_NCRBUTTONUP = 0x00A5;
        public const int WM_NCRBUTTONDBLCLK = 0x00A6;
        public const int WM_NCMBUTTONDOWN = 0x00A7;
        public const int WM_NCMBUTTONUP = 0x00A8;
        public const int WM_NCMBUTTONDBLCLK = 0x00A9;
        public const int WM_NCXBUTTONDOWN = 0x00AB;
        public const int WM_NCXBUTTONUP = 0x00AC;
        public const int WM_NCXBUTTONDBLCLK = 0x00AD;

        public const int WM_INPUT_DEVICE_CHANGE = 0x00FE;
        public const int WM_INPUT = 0x00FF;

        // 键盘
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_CHAR = 0x0102;
        public const int WM_DEADCHAR = 0x0103;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;
        public const int WM_SYSCHAR = 0x0106;
        public const int WM_SYSDEADCHAR = 0x0107;

        // IME
        public const int WM_IME_STARTCOMPOSITION = 0x010D;
        public const int WM_IME_ENDCOMPOSITION = 0x010E;
        public const int WM_IME_COMPOSITION = 0x010F;
        public const int WM_IME_KEYLAST = 0x010F;

        // Dialog
        public const int WM_INITDIALOG = 0x0110;
        public const int WM_COMMAND = 0x0111;
        public const int WM_SYSCOMMAND = 0x0112;
        public const int WM_TIMER = 0x0113;
        public const int WM_HSCROLL = 0x0114;
        public const int WM_VSCROLL = 0x0115;
        public const int WM_INITMENU = 0x0116;
        public const int WM_INITMENUPOPUP = 0x0117;

        public const int WM_GESTURE = 0x0119;
        public const int WM_GESTURENOTIFY = 0x011A;


        public const int WM_MENUSELECT = 0x011F;
        public const int WM_MENUCHAR = 0x0120;
        public const int WM_ENTERIDLE = 0x0121;

        // Control
        public const int WM_CTLCOLORMSGBOX = 0x0132;
        public const int WM_CTLCOLOREDIT = 0x0133;
        public const int WM_CTLCOLORLISTBOX = 0x0134;
        public const int WM_CTLCOLORBTN = 0x0135;
        public const int WM_CTLCOLORDLG = 0x0136;
        public const int WM_CTLCOLORSCROLLBAR = 0x0137;
        public const int WM_CTLCOLORSTATIC = 0x0138;
        public const int MN_GETHMENU = 0x01E1;


        // 鼠标
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_RBUTTONUP = 0x0205;
        public const int WM_RBUTTONDBLCLK = 0x0206;
        public const int WM_MBUTTONDOWN = 0x0207;
        public const int WM_MBUTTONUP = 0x0208;
        public const int WM_MBUTTONDBLCLK = 0x0209;
        public const int WM_MOUSEWHEEL = 0x020A;
        public const int WM_XBUTTONDOWN = 0x020B;
        public const int WM_XBUTTONUP = 0x020C;
        public const int WM_XBUTTONDBLCLK = 0x020D;
        public const int WM_MOUSEHWHEEL = 0x020E;


        public const int WM_PARENTNOTIFY = 0x0210;
        public const int WM_ENTERMENULOOP = 0x0211;
        public const int WM_EXITMENULOOP = 0x0212;
        public const int WM_NEXTMENU = 0x0213;
        public const int WM_SIZING = 0x0214;
        public const int WM_CAPTURECHANGED = 0x0215;
        public const int WM_MOVING = 0x0216;
        public const int WM_POWERBROADCAST = 0x0218;


        //All Message Numbers below 0x0400 are RESERVED.
        public const int WM_USER = 0x0400;


        //#define WHEEL_DELTA                     120
        //#define GET_WHEEL_DELTA_WPARAM(wParam)  ((short)HIWORD(wParam))

        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_RESTORE = 0xF120;


    }


    /// <summary>
    /// NativeMessages - 
    /// </summary>
    partial class Win32Messages
    {
        public const int LOAD_LIBRARY_AS_DATAFILE = 0x02;

        // Constant declarations
        public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x100;
        public const int FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;
        public const int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;
        public const int FORMAT_MESSAGE_FROM_HMODULE = 0x800;


        // WINHTTP DLL Error range
        public const int WINHTTP_ERROR_BASE = 12000;
        public const int WINHTTP_ERROR_LAST = WINHTTP_ERROR_BASE + 184;

        // NETMSG DLL Error range
        public const int NERR_BASE = 2100;
        public const int MAX_NERR = NERR_BASE + 899;
    }


    /// <summary>
    /// NativeMessages
    /// </summary>
    partial class Win32Messages
    {
        public static void TraceMsg(int Msg)
        {
#if DEBUG
            Debug.WriteLine("Msg = 0x{0:x04}", Msg);
#endif
        }


        public static string FormatMsg(int Msg)
        {
            string s = "";

            switch(Msg)
            {
                case WM_NULL:
                {
                    break;
                }

                case WM_CREATE:
                {
                    s = string.Format("");
                    break;
                }


                default:
                    break;
            }

            return s;
        }

        //public const int WM_CREATE = 0x0001;
        //public const int WM_DESTROY = 0x0002;



    }


}

///*
// * Key State Masks for Mouse Messages
// */
//#define MK_LBUTTON          0x0001
//#define MK_RBUTTON          0x0002
//#define MK_SHIFT            0x0004
//#define MK_CONTROL          0x0008
//#define MK_MBUTTON          0x0010
//#if(_WIN32_WINNT >= 0x0500)
//#define MK_XBUTTON1         0x0020
//#define MK_XBUTTON2         0x0040
//#endif /* _WIN32_WINNT >= 0x0500 */

//#endif /* !NOKEYSTATES */


//#if(_WIN32_WINNT >= 0x0400)
//#ifndef NOTRACKMOUSEEVENT

//#define TME_HOVER       0x00000001
//#define TME_LEAVE       0x00000002
//#if(WINVER >= 0x0500)
//#define TME_NONCLIENT   0x00000010
//#endif /* WINVER >= 0x0500 */
//#define TME_QUERY       0x40000000
//#define TME_CANCEL      0x80000000


//#define HOVER_DEFAULT   0xFFFFFFFF
//#endif /* _WIN32_WINNT >= 0x0400 */

//#if(_WIN32_WINNT >= 0x0400)

//#pragma region Desktop Family
//#if WINAPI_FAMILY_PARTITION(WINAPI_PARTITION_DESKTOP)

//typedef struct tagTRACKMOUSEEVENT {
//    DWORD cbSize;
//    DWORD dwFlags;
//    HWND  hwndTrack;
//    DWORD dwHoverTime;
//} TRACKMOUSEEVENT, *LPTRACKMOUSEEVENT;

//WINUSERAPI
//BOOL
//WINAPI
//TrackMouseEvent(
//    _Inout_ LPTRACKMOUSEEVENT lpEventTrack);


