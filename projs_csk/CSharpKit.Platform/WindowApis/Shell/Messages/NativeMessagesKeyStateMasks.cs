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


namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// Key State Masks for Mouse Messages
    /// </summary>
    public sealed class NativeMessagesKeyStateMasks
    {
        NativeMessagesKeyStateMasks() { }

        public const int MK_LBUTTON = 0x0001;
        public const int MK_RBUTTON = 0x0002;
        public const int MK_SHIFT = 0x0004;
        public const int MK_CONTROL = 0x0008;
        public const int MK_MBUTTON = 0x0010;
        public const int MK_XBUTTON1 = 0x0020;
        public const int MK_XBUTTON2 = 0x0040;

    }


}

///*
// * Key State Masks for Mouse Messages
// */

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
