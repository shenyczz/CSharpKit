using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpKit.Platform.Windows
{
    /// <summary>
    /// WindowStyle - 窗口风格<br/>
    /// WS<br/>
    /// WindowStyleOps<br/>
    /// WindowStyleOptions(选项)<br/>
    /// </summary>
    [Flags]
    public enum WS : uint
    {
        WS_OVERLAPPED = 0x00000000,

        WS_POPUP = 0x80000000,
        WS_CHILD = 0x40000000,
        WS_MINIMIZE = 0x20000000,
        WS_VISIBLE = 0x10000000,

        WS_DISABLED = 0x08000000,
        WS_CLIPSIBLINGS = 0x04000000,
        WS_CLIPCHILDREN = 0x02000000,
        WS_MAXIMIZE = 0x01000000,

        WS_BORDER = 0x00800000,
        WS_DLGFRAME = 0x00400000,
        WS_VSCROLL = 0x00200000,
        WS_HSCROLL = 0x00100000,

        WS_SYSMENU = 0x00080000,
        WS_THICKFRAME = 0x00040000,
        WS_GROUP = 0x00020000,
        WS_TABSTOP = 0x00010000,

        WS_MINIMIZEBOX = 0x00020000,
        WS_MAXIMIZEBOX = 0x00010000,

        // 
        WS_TILED = WS_OVERLAPPED,               // 0x00000000
        WS_CHILDWINDOW = WS_CHILD,              // 0x40000000
        WS_ICONIC = WS_MINIMIZE,                // 0x20000000
        WS_SIZEBOX = WS_THICKFRAME,             // 0x00040000
        WS_CAPTION = WS_OVERLAPPED              // 0x00C00000
            | WS_BORDER
            | WS_DLGFRAME,

        WS_POPUPWINDOW = WS_OVERLAPPED          // 0x80880000
            | WS_POPUP
            | WS_BORDER
            | WS_SYSMENU,

        WS_OVERLAPPEDWINDOW = WS_OVERLAPPED     // 0x00CF0000
            | WS_CAPTION
            | WS_SYSMENU
            | WS_THICKFRAME
            | WS_MINIMIZEBOX
            | WS_MAXIMIZEBOX,

        WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,   // 0x00CF0000
    }




    public enum WindowClassStyles
    {
        /// <summary>
        /// Aligns the window's client area on a byte boundary (in the x direction).
        /// This style affects the width of the window and its horizontal placement on the display.
        /// </summary>
        CS_BYTEALIGNCLIENT = 0x1000,
        /// <summary>
        /// Aligns the window on a byte boundary (in the x direction).
        /// This style affects the width of the window and its horizontal placement on the display.
        /// </summary>
        CS_BYTEALIGNWINDOW = 0x2000,



        //@@@
    }

    /*


CS_CLASSDC
0x0040
Allocates one device context to be shared by all windows in the class. Because window classes are process specific, it is possible for multiple threads of an application to create a window of the same class. It is also possible for the threads to attempt to use the device context simultaneously. When this happens, the system allows only one thread to successfully finish its drawing operation.

CS_DBLCLKS
0x0008
Sends a double-click message to the window procedure when the user double-clicks the mouse while the cursor is within a window belonging to the class.

CS_DROPSHADOW
0x00020000
Enables the drop shadow effect on a window. The effect is turned on and off through SPI_SETDROPSHADOW. Typically, this is enabled for small, short-lived windows such as menus to emphasize their Z-order relationship to other windows. Windows created from a class with this style must be top-level windows; they may not be child windows.

CS_GLOBALCLASS
0x4000
Indicates that the window class is an application global class. For more information, see the "Application Global Classes" section of About Window Classes.

CS_HREDRAW
0x0002
Redraws the entire window if a movement or size adjustment changes the width of the client area.

CS_NOCLOSE
0x0200
Disables Close on the window menu.

CS_OWNDC
0x0020
Allocates a unique device context for each window in the class.

CS_PARENTDC
0x0080
Sets the clipping rectangle of the child window to that of the parent window so that the child can draw on the parent. A window with the CS_PARENTDC style bit receives a regular device context from the system's cache of device contexts. It does not give the child the parent's device context or device context settings. Specifying CS_PARENTDC enhances an application's performance.

CS_SAVEBITS
0x0800
Saves, as a bitmap, the portion of the screen image obscured by a window of this class. When the window is removed, the system uses the saved bitmap to restore the screen image, including other windows that were obscured. Therefore, the system does not send WM_PAINT messages to windows that were obscured if the memory used by the bitmap has not been discarded and if other screen actions have not invalidated the stored image.
This style is useful for small windows (for example, menus or dialog boxes) that are displayed briefly and then removed before other screen activity takes place. This style increases the time required to display the window, because the system must first allocate memory to store the bitmap.

CS_VREDRAW
0x0001
Redraws the entire window if a movement or size adjustment changes the height of the client area.
    */



}
