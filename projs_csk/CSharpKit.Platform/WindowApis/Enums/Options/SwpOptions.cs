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
    /// SwpOptions - 设置窗口位置选项(SetWindowPosition)
    /// </summary>
    [Flags]
    public enum SwpOptions : uint
    {
        /// <summary>
        /// Retains current size (ignores the cx and cy members).
        /// </summary>
        SWP_NOSIZE          = 0x0001,
        /// <summary>
        /// Retains current position (ignores the x and y members).
        /// </summary>
        SWP_NOMOVE          = 0x0002,
        /// <summary>
        /// Retains current ordering (ignores the hwndInsertAfter member).
        /// </summary>
        SWP_NOZORDER        = 0x0004,
        /// <summary>
        /// Does not redraw changes. 
        /// </summary>
        SWP_NOREDRAW        = 0x0008,
        /// <summary>
        /// Does not activate the window. 
        /// </summary>
        SWP_NOACTIVATE      = 0x0010,
        /// <summary>
        /// Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed.
        /// If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
        /// </summary>
        SWP_FRAMECHANGED    = 0x0020,
        /// <summary>
        /// Displays the window. 
        /// </summary>
        SWP_SHOWWINDOW      = 0x0040,
        /// <summary>
        /// Hides the window. 
        /// </summary>
        SWP_HIDEWINDOW      = 0x0080,
        /// <summary>
        /// Discards the entire contents of the client area.
        /// If this flag is not specified, the valid contents of the client area are saved and
        /// copied back into the client area after the window is sized or repositioned.
        /// </summary>
        SWP_NOCOPYBITS      = 0x0100,
        /// <summary>
        /// Does not change the owner window's position in the Z-order. 
        /// </summary>
        SWP_NOOWNERZORDER   = 0x0200,
        /// <summary>
        /// Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
        /// </summary>
        SWP_NOSENDCHANGING  = 0x0400,
        /// <summary>
        /// Draws a frame (defined in the class description for the window) around the window.
        /// The window receives a WM_NCCALCSIZE message. 
        /// </summary>
        SWP_DRAWFRAME       = 0x0020,
        /// <summary>
        /// Same as SWP_NOOWNERZORDER.
        /// </summary>
        SWP_NOREPOSITION    = 0x0200,
        /// <summary>
        /// Prevents generation of the WM_SYNCPAINT message. 
        /// </summary>
        SWP_DEFERERASE      = 0x2000,
        /// <summary>
        /// If the calling thread and the thread that owns the window are attached to different input queues,
        /// the system posts the request to the thread that owns the window.
        /// This prevents the calling thread from blocking its execution while other threads process the request. 
        /// </summary>
        SWP_ASYNCWINDOWPOS  = 0x4000
    }


    internal enum SetWindowPosFlags : uint
    {
        /// <summary>If the calling thread and the thread that owns the window are attached to different input queues,
        /// the system posts the request to the thread that owns the window. This prevents the calling thread from
        /// blocking its execution while other threads process the request.</summary>
        /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
        SynchronousWindowPosition = 0x4000,
        /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
        /// <remarks>SWP_DEFERERASE</remarks>
        DeferErase = 0x2000,
        /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
        /// <remarks>SWP_DRAWFRAME</remarks>
        DrawFrame = 0x0020,
        /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to
        /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE
        /// is sent only when the window's size is being changed.</summary>
        /// <remarks>SWP_FRAMECHANGED</remarks>
        FrameChanged = 0x0020,
        /// <summary>Hides the window.</summary>
        /// <remarks>SWP_HIDEWINDOW</remarks>
        HideWindow = 0x0080,
        /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the
        /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
        /// parameter).</summary>
        /// <remarks>SWP_NOACTIVATE</remarks>
        DoNotActivate = 0x0010,
        /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid
        /// contents of the client area are saved and copied back into the client area after the window is sized or
        /// repositioned.</summary>
        /// <remarks>SWP_NOCOPYBITS</remarks>
        DoNotCopyBits = 0x0100,
        /// <summary>Retains the current position (ignores X and Y parameters).</summary>
        /// <remarks>SWP_NOMOVE</remarks>
        IgnoreMove = 0x0002,
        /// <summary>Does not change the owner window's position in the Z order.</summary>
        /// <remarks>SWP_NOOWNERZORDER</remarks>
        DoNotChangeOwnerZOrder = 0x0200,
        /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to
        /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent
        /// window uncovered as a result of the window being moved. When this flag is set, the application must
        /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
        /// <remarks>SWP_NOREDRAW</remarks>
        DoNotRedraw = 0x0008,
        /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
        /// <remarks>SWP_NOREPOSITION</remarks>
        DoNotReposition = 0x0200,
        /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
        /// <remarks>SWP_NOSENDCHANGING</remarks>
        DoNotSendChangingEvent = 0x0400,
        /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
        /// <remarks>SWP_NOSIZE</remarks>
        IgnoreResize = 0x0001,
        /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
        /// <remarks>SWP_NOZORDER</remarks>
        IgnoreZOrder = 0x0004,
        /// <summary>Displays the window.</summary>
        /// <remarks>SWP_SHOWWINDOW</remarks>
        ShowWindow = 0x0040,
    }

    /// <summary>
    /// WM_SYSCOMMAND.wParam
    /// The type(wParam) of system command requested.
    /// This parameter can be one of the following values. 
    /// </summary>
    public enum SC
    {
        /// <summary>
        /// Sizes the window.
        /// </summary>
        SC_SIZE = 0xF000,
        /// <summary>
        /// Moves the window.
        /// </summary>
        SC_MOVE = 0xF010,
        /// <summary>
        /// Minimizes the window.
        /// </summary>
        SC_MINIMIZE = 0xF020,
        /// <summary>
        /// Maximizes the window.
        /// </summary>
        SC_MAXIMIZE = 0xF030,
        /// <summary>
        /// Moves to the next window.
        /// </summary>
        SC_NEXTWINDOW = 0xF040,
        /// <summary>
        /// Moves to the previous window.
        /// </summary>
        SC_PREVWINDOW = 0xF050,
        /// <summary>
        /// Closes the window.
        /// </summary>
        SC_CLOSE = 0xF060,
        /// <summary>
        /// Scrolls vertically.
        /// </summary>
        SC_VSCROLL = 0xF070,
        /// <summary>
        /// Scrolls horizontally.
        /// </summary>
        SC_HSCROLL = 0xF080,
        /// <summary>
        /// Retrieves the window menu as a result of a mouse click.
        /// </summary>
        SC_MOUSEMENU = 0xF090,
        /// <summary>
        /// Retrieves the window menu as a result of a keystroke.
        /// For more information, see the Remarks section.
        /// </summary>
        SC_KEYMENU = 0xF100,
        SC_ARRANGE = 0xF110,
        /// <summary>
        /// Restores the window to its normal position and size.
        /// </summary>
        SC_RESTORE = 0xF120,
        /// <summary>
        /// Activates the Start menu.
        /// </summary>
        SC_TASKLIST = 0xF130,
        /// <summary>
        /// Executes the screen saver application specified in the [boot] section of the System.ini file.
        /// </summary>
        SC_SCREENSAVE = 0xF140,
        /// <summary>
        /// Activates the window associated with the application-specified hot key.
        /// The lParam parameter identifies the window to activate.
        /// </summary>
        SC_HOTKEY = 0xF150,
        /// <summary>
        /// Selects the default item; the user double-clicked the window menu.
        /// </summary>
        SC_DEFAULT = 0xF160,
        /// <summary>
        /// Sets the state of the display.
        /// This command supports devices that have power-saving features, such as a battery-powered personal computer. 
        ///
        /// The lParam parameter can have the following values:
        /// -1 (the display is powering on)
        ///  1 (the display is going to low power)
        ///  2 (the display is being shut off)
        /// </summary>
        SC_MONITORPOWER = 0xF170,
        /// <summary>
        /// Changes the cursor to a question mark with a pointer.
        /// If the user then clicks a control in the dialog box, the control receives a WM_HELP message.
        /// </summary>
        SC_CONTEXTHELP = 0xF180,
        SC_SEPARATOR = 0xF00F,
        /// <summary>
        /// SCF_ISSECURE
        /// Indicates whether the screen saver is secure. 
        /// </summary>
        SCF_ISSECURE = 0x00000001,

        SC_ICON = SC_MINIMIZE,
        SC_ZOOM = SC_MAXIMIZE,

        //@@@
    }

}
