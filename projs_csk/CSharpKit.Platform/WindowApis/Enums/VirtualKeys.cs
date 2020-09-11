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
    /// 虚拟键
    /// </summary>
    public enum VirtualKeys
    {
        /// <summary>
        /// 没有按键
        /// </summary>
        VK_NONE = 0x00,
        /// <summary>
        /// 鼠标左按钮
        /// </summary>
        VK_LBUTTON = 0x01,
        /// <summary>
        /// 鼠标右按钮
        /// </summary>
        VK_RBUTTON = 0x02,
        /// <summary>
        /// 取消键
        /// </summary>
        VK_CANCEL = 0x03,



        /// <summary>
        /// ESC key = 27
        /// </summary>
        VK_ESCAPE = 0x1B,




        //@EndOf(VirtualKeys)
    }

    public enum Keys_bak
    {
        KeyCode = 65535,    // 0x0FFFF
        Modifiers = -65536, // 0x10000

        None = 0,
        LButton = 1,
        RButton = 2,
        Cancel = 3,
        MButton = 4,
        XButton1 = 5,
        XButton2 = 6,
        Back = 8,
        Tab = 9,
        LineFeed = 10,
        Clear = 12,
        Return = 13,
        Enter = 13,
        ShiftKey = 16,
        ControlKey = 17,
        Menu = 18,
        Pause = 19,
        Capital = 20,
        CapsLock = 20,
        KanaMode = 21,
        HanguelMode = 21,
        HangulMode = 21,
        JunjaMode = 23,
        FinalMode = 24,
        HanjaMode = 25,
        KanjiMode = 25,
        Escape = 27,
        IMEConvert = 28,
        IMENonconvert = 29,
        IMEAccept = 30,
        IMEAceept = 30,
        IMEModeChange = 31,
        Space = 32,
        Prior = 33,
        PageUp = 33,
        Next = 34,
        PageDown = 34,
        End = 35,
        Home = 36,
        Left = 37,
        Up = 38,
        Right = 39,
        Down = 40,
        Select = 41,
        Print = 42,
        Execute = 43,
        Snapshot = 44,
        PrintScreen = 44,
        Insert = 45,
        Delete = 46,
        Help = 47,
        D0 = 48,
        D1 = 49,
        D2 = 50,
        D3 = 51,
        D4 = 52,
        D5 = 53,
        D6 = 54,
        D7 = 55,
        D8 = 56,
        D9 = 57,
        A = 65,
        B = 66,
        C = 67,
        D = 68,
        E = 69,
        F = 70,
        G = 71,
        H = 72,
        I = 73,
        J = 74,
        K = 75,
        L = 76,
        M = 77,
        N = 78,
        O = 79,
        P = 80,
        Q = 81,
        R = 82,
        S = 83,
        T = 84,
        U = 85,
        V = 86,
        W = 87,
        X = 88,
        Y = 89,
        Z = 90,
        LWin = 91,
        RWin = 92,
        Apps = 93,
        Sleep = 95,
        NumPad0 = 96,
        NumPad1 = 97,
        NumPad2 = 98,
        NumPad3 = 99,
        NumPad4 = 100,
        NumPad5 = 101,
        NumPad6 = 102,
        NumPad7 = 103,
        NumPad8 = 104,
        NumPad9 = 105,
        Multiply = 106,
        Add = 107,
        Separator = 108,
        Subtract = 109,
        Decimal = 110,
        Divide = 111,
        F1 = 112,
        F2 = 113,
        F3 = 114,
        F4 = 115,
        F5 = 116,
        F6 = 117,
        F7 = 118,
        F8 = 119,
        F9 = 120,
        F10 = 121,
        F11 = 122,
        F12 = 123,
        F13 = 124,
        F14 = 125,
        F15 = 126,
        F16 = 127,
        F17 = 128,
        F18 = 129,
        F19 = 130,
        F20 = 131,
        F21 = 132,
        F22 = 133,
        F23 = 134,
        F24 = 135,
        NumLock = 144,
        Scroll = 145,
        LShiftKey = 160,
        RShiftKey = 161,
        LControlKey = 162,
        RControlKey = 163,
        LMenu = 164,
        RMenu = 165,
        BrowserBack = 166,
        BrowserForward = 167,
        BrowserRefresh = 168,
        BrowserStop = 169,
        BrowserSearch = 170,
        BrowserFavorites = 171,
        BrowserHome = 172,
        VolumeMute = 173,
        VolumeDown = 174,
        VolumeUp = 175,
        MediaNextTrack = 176,
        MediaPreviousTrack = 177,
        MediaStop = 178,
        MediaPlayPause = 179,
        LaunchMail = 180,
        SelectMedia = 181,
        LaunchApplication1 = 182,
        LaunchApplication2 = 183,
        OemSemicolon = 186,
        Oem1 = 186,
        Oemplus = 187,
        Oemcomma = 188,
        OemMinus = 189,
        OemPeriod = 190,
        OemQuestion = 191,
        Oem2 = 191,
        Oemtilde = 192,
        Oem3 = 192,
        OemOpenBrackets = 219,
        Oem4 = 219,
        OemPipe = 220,
        Oem5 = 220,
        OemCloseBrackets = 221,
        Oem6 = 221,
        OemQuotes = 222,
        Oem7 = 222,
        Oem8 = 223,
        OemBackslash = 226,
        Oem102 = 226,
        ProcessKey = 229,
        Packet = 231,
        Attn = 246,
        Crsel = 247,
        Exsel = 248,
        EraseEof = 249,
        Play = 250,
        Zoom = 251,
        NoName = 252,
        Pa1 = 253,
        OemClear = 254,
        Shift = 65536,
        Control = 131072,
        Alt = 262144
    }

}

/*
 
 //
    // 摘要:
    //     指定键代码和修饰符。
    [ComVisible(true)]
    [Editor("System.Windows.Forms.Design.ShortcutKeysEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
    [Flags]
    [TypeConverter(typeof(KeysConverter))]
    public enum Keys
    {
        //
        // 摘要:
        //     鼠标中按钮（三个按钮的鼠标）。
        MButton = 4,
        //
        // 摘要:
        //     第一个 X 鼠标按钮（五个按钮的鼠标）。
        XButton1 = 5,
        //
        // 摘要:
        //     第二个 X 鼠标按钮（五个按钮的鼠标）。
        XButton2 = 6,
        //
        // 摘要:
        //     Backspace 键。
        Back = 8,
        //
        // 摘要:
        //     The TAB key.
        Tab = 9,
        //
        // 摘要:
        //     LINEFEED 键。
        LineFeed = 10,
        //
        // 摘要:
        //     Clear 键。
        Clear = 12,
        //
        // 摘要:
        //     Return 键。
        Return = 13,
        //
        // 摘要:
        //     Enter 键。
        Enter = 13,
        //
        // 摘要:
        //     Shift 键。
        ShiftKey = 16,
        //
        // 摘要:
        //     The CTRL key.
        ControlKey = 17,
        //
        // 摘要:
        //     Alt 键。
        Menu = 18,
        //
        // 摘要:
        //     Pause 键。
        Pause = 19,
        //
        // 摘要:
        //     The CAPS LOCK key.
        Capital = 20,
        //
        // 摘要:
        //     The CAPS LOCK key.
        CapsLock = 20,
        //
        // 摘要:
        //     IME Kana 模式键。
        KanaMode = 21,
        //
        // 摘要:
        //     IME Hanguel 模式键。（为了保持兼容性而设置；使用 HangulMode）
        HanguelMode = 21,
        //
        // 摘要:
        //     IME Hangul 模式键。
        HangulMode = 21,
        //
        // 摘要:
        //     IME Junja 模式键。
        JunjaMode = 23,
        //
        // 摘要:
        //     IME 最终模式键。
        FinalMode = 24,
        //
        // 摘要:
        //     IME Hanja 模式键。
        HanjaMode = 25,
        //
        // 摘要:
        //     IME Kanji 模式键。
        KanjiMode = 25,
        //
        // 摘要:
        //     IME 转换键。
        IMEConvert = 28,
        //
        // 摘要:
        //     IME 非转换键。
        IMENonconvert = 29,
        //
        // 摘要:
        //     IME 接受键，替换 System.Windows.Forms.Keys.IMEAceept。
        IMEAccept = 30,
        //
        // 摘要:
        //     IME 接受键。已过时，请改用 System.Windows.Forms.Keys.IMEAccept。
        IMEAceept = 30,
        //
        // 摘要:
        //     IME 模式更改键。
        IMEModeChange = 31,
        //
        // 摘要:
        //     空格键。
        Space = 32,
        //
        // 摘要:
        //     The PAGE UP key.
        Prior = 33,
        //
        // 摘要:
        //     The PAGE UP key.
        PageUp = 33,
        //
        // 摘要:
        //     The PAGE DOWN key.
        Next = 34,
        //
        // 摘要:
        //     The PAGE DOWN key.
        PageDown = 34,
        //
        // 摘要:
        //     The END key.
        End = 35,
        //
        // 摘要:
        //     The HOME key.
        Home = 36,
        //
        // 摘要:
        //     向左键。
        Left = 37,
        //
        // 摘要:
        //     向上键。
        Up = 38,
        //
        // 摘要:
        //     向右键。
        Right = 39,
        //
        // 摘要:
        //     向下键。
        Down = 40,
        //
        // 摘要:
        //     Select 键。
        Select = 41,
        //
        // 摘要:
        //     Print 键。
        Print = 42,
        //
        // 摘要:
        //     EXECUTE 键。
        Execute = 43,
        //
        // 摘要:
        //     Print Screen 键。
        Snapshot = 44,
        //
        // 摘要:
        //     Print Screen 键。
        PrintScreen = 44,
        //
        // 摘要:
        //     The INS key.
        Insert = 45,
        //
        // 摘要:
        //     The DEL key.
        Delete = 46,
        //
        // 摘要:
        //     The HELP key.
        Help = 47,
        //
        // 摘要:
        //     The 0 key.
        D0 = 48,
        //
        // 摘要:
        //     The 1 key.
        D1 = 49,
        //
        // 摘要:
        //     The 2 key.
        D2 = 50,
        //
        // 摘要:
        //     The 3 key.
        D3 = 51,
        //
        // 摘要:
        //     The 4 key.
        D4 = 52,
        //
        // 摘要:
        //     The 5 key.
        D5 = 53,
        //
        // 摘要:
        //     The 6 key.
        D6 = 54,
        //
        // 摘要:
        //     The 7 key.
        D7 = 55,
        //
        // 摘要:
        //     The 8 key.
        D8 = 56,
        //
        // 摘要:
        //     The 9 key.
        D9 = 57,
        //
        // 摘要:
        //     A 键。
        A = 65,
        //
        // 摘要:
        //     B 键。
        B = 66,
        //
        // 摘要:
        //     C 键。
        C = 67,
        //
        // 摘要:
        //     D 键。
        D = 68,
        //
        // 摘要:
        //     E 键。
        E = 69,
        //
        // 摘要:
        //     F 键。
        F = 70,
        //
        // 摘要:
        //     G 键。
        G = 71,
        //
        // 摘要:
        //     H 键。
        H = 72,
        //
        // 摘要:
        //     I 键。
        I = 73,
        //
        // 摘要:
        //     J 键。
        J = 74,
        //
        // 摘要:
        //     K 键。
        K = 75,
        //
        // 摘要:
        //     L 键。
        L = 76,
        //
        // 摘要:
        //     M 键。
        M = 77,
        //
        // 摘要:
        //     N 键。
        N = 78,
        //
        // 摘要:
        //     O 键。
        O = 79,
        //
        // 摘要:
        //     P 键。
        P = 80,
        //
        // 摘要:
        //     Q 键。
        Q = 81,
        //
        // 摘要:
        //     R 键。
        R = 82,
        //
        // 摘要:
        //     S 键。
        S = 83,
        //
        // 摘要:
        //     T 键。
        T = 84,
        //
        // 摘要:
        //     U 键。
        U = 85,
        //
        // 摘要:
        //     V 键。
        V = 86,
        //
        // 摘要:
        //     W 键。
        W = 87,
        //
        // 摘要:
        //     X 键。
        X = 88,
        //
        // 摘要:
        //     Y 键。
        Y = 89,
        //
        // 摘要:
        //     Z 键。
        Z = 90,
        //
        // 摘要:
        //     左 Windows 徽标键（Microsoft Natural Keyboard，人体工程学键盘）。
        LWin = 91,
        //
        // 摘要:
        //     右 Windows 徽标键（Microsoft Natural Keyboard，人体工程学键盘）。
        RWin = 92,
        //
        // 摘要:
        //     应用程序键（Microsoft Natural Keyboard，人体工程学键盘）。
        Apps = 93,
        //
        // 摘要:
        //     计算机睡眠键。
        Sleep = 95,
        //
        // 摘要:
        //     The 0 key on the numeric keypad.
        NumPad0 = 96,
        //
        // 摘要:
        //     The 1 key on the numeric keypad.
        NumPad1 = 97,
        //
        // 摘要:
        //     数字键盘上的 2 键。
        NumPad2 = 98,
        //
        // 摘要:
        //     数字键盘上的 3 键。
        NumPad3 = 99,
        //
        // 摘要:
        //     数字键盘上的 4 键。
        NumPad4 = 100,
        //
        // 摘要:
        //     数字键盘上的 5 键。
        NumPad5 = 101,
        //
        // 摘要:
        //     数字键盘上的 6 键。
        NumPad6 = 102,
        //
        // 摘要:
        //     数字键盘上的 7 键。
        NumPad7 = 103,
        //
        // 摘要:
        //     The 8 key on the numeric keypad.
        NumPad8 = 104,
        //
        // 摘要:
        //     The 9 key on the numeric keypad.
        NumPad9 = 105,
        //
        // 摘要:
        //     乘号键。
        Multiply = 106,
        //
        // 摘要:
        //     加号键。
        Add = 107,
        //
        // 摘要:
        //     分隔符键。
        Separator = 108,
        //
        // 摘要:
        //     减号键。
        Subtract = 109,
        //
        // 摘要:
        //     句点键。
        Decimal = 110,
        //
        // 摘要:
        //     除号键。
        Divide = 111,
        //
        // 摘要:
        //     The F1 key.
        F1 = 112,
        //
        // 摘要:
        //     The F2 key.
        F2 = 113,
        //
        // 摘要:
        //     The F3 key.
        F3 = 114,
        //
        // 摘要:
        //     The F4 key.
        F4 = 115,
        //
        // 摘要:
        //     The F5 key.
        F5 = 116,
        //
        // 摘要:
        //     The F6 key.
        F6 = 117,
        //
        // 摘要:
        //     The F7 key.
        F7 = 118,
        //
        // 摘要:
        //     The F8 key.
        F8 = 119,
        //
        // 摘要:
        //     The F9 key.
        F9 = 120,
        //
        // 摘要:
        //     The F10 key.
        F10 = 121,
        //
        // 摘要:
        //     The F11 key.
        F11 = 122,
        //
        // 摘要:
        //     The F12 key.
        F12 = 123,
        //
        // 摘要:
        //     The F13 key.
        F13 = 124,
        //
        // 摘要:
        //     The F14 key.
        F14 = 125,
        //
        // 摘要:
        //     The F15 key.
        F15 = 126,
        //
        // 摘要:
        //     The F16 key.
        F16 = 127,
        //
        // 摘要:
        //     The F17 key.
        F17 = 128,
        //
        // 摘要:
        //     The F18 key.
        F18 = 129,
        //
        // 摘要:
        //     The F19 key.
        F19 = 130,
        //
        // 摘要:
        //     The F20 key.
        F20 = 131,
        //
        // 摘要:
        //     The F21 key.
        F21 = 132,
        //
        // 摘要:
        //     The F22 key.
        F22 = 133,
        //
        // 摘要:
        //     The F23 key.
        F23 = 134,
        //
        // 摘要:
        //     The F24 key.
        F24 = 135,
        //
        // 摘要:
        //     The NUM LOCK key.
        NumLock = 144,
        //
        // 摘要:
        //     Scroll Lock 键。
        Scroll = 145,
        //
        // 摘要:
        //     左 Shift 键。
        LShiftKey = 160,
        //
        // 摘要:
        //     右 Shift 键。
        RShiftKey = 161,
        //
        // 摘要:
        //     左 Ctrl 键。
        LControlKey = 162,
        //
        // 摘要:
        //     右 Ctrl 键。
        RControlKey = 163,
        //
        // 摘要:
        //     左 Alt 键。
        LMenu = 164,
        //
        // 摘要:
        //     右 Alt 键。
        RMenu = 165,
        //
        // 摘要:
        //     浏览器后退键（Windows 2000 或更高版本）。
        BrowserBack = 166,
        //
        // 摘要:
        //     浏览器前进键（Windows 2000 或更高版本）。
        BrowserForward = 167,
        //
        // 摘要:
        //     浏览器刷新键（Windows 2000 或更高版本）。
        BrowserRefresh = 168,
        //
        // 摘要:
        //     浏览器停止键（Windows 2000 或更高版本）。
        BrowserStop = 169,
        //
        // 摘要:
        //     浏览器搜索键（Windows 2000 或更高版本）。
        BrowserSearch = 170,
        //
        // 摘要:
        //     浏览器收藏夹键（Windows 2000 或更高版本）。
        BrowserFavorites = 171,
        //
        // 摘要:
        //     浏览器主页键（Windows 2000 或更高版本）。
        BrowserHome = 172,
        //
        // 摘要:
        //     静音键（Windows 2000 或更高版本）。
        VolumeMute = 173,
        //
        // 摘要:
        //     减小音量键（Windows 2000 或更高版本）。
        VolumeDown = 174,
        //
        // 摘要:
        //     增大音量键（Windows 2000 或更高版本）。
        VolumeUp = 175,
        //
        // 摘要:
        //     媒体下一曲目键（Windows 2000 或更高版本）。
        MediaNextTrack = 176,
        //
        // 摘要:
        //     媒体上一曲目键（Windows 2000 或更高版本）。
        MediaPreviousTrack = 177,
        //
        // 摘要:
        //     媒体停止键（Windows 2000 或更高版本）。
        MediaStop = 178,
        //
        // 摘要:
        //     媒体播放暂停键（Windows 2000 或更高版本）。
        MediaPlayPause = 179,
        //
        // 摘要:
        //     启动邮件键（Windows 2000 或更高版本）。
        LaunchMail = 180,
        //
        // 摘要:
        //     选择媒体键（Windows 2000 或更高版本）。
        SelectMedia = 181,
        //
        // 摘要:
        //     启动应用程序一键（Windows 2000 或更高版本）。
        LaunchApplication1 = 182,
        //
        // 摘要:
        //     启动应用程序二键（Windows 2000 或更高版本）。
        LaunchApplication2 = 183,
        //
        // 摘要:
        //     美式标准键盘上的 OEM 分号键（Windows 2000 或更高版本）。
        OemSemicolon = 186,
        //
        // 摘要:
        //     The OEM 1 key.
        Oem1 = 186,
        //
        // 摘要:
        //     任何国家/地区键盘上的 OEM 加号键（Windows 2000 或更高版本）。
        Oemplus = 187,
        //
        // 摘要:
        //     任何国家/地区键盘上的 OEM 逗号键（Windows 2000 或更高版本）。
        Oemcomma = 188,
        //
        // 摘要:
        //     任何国家/地区键盘上的 OEM 减号键（Windows 2000 或更高版本）。
        OemMinus = 189,
        //
        // 摘要:
        //     任何国家/地区键盘上的 OEM 句点键（Windows 2000 或更高版本）。
        OemPeriod = 190,
        //
        // 摘要:
        //     美式标准键盘上的 OEM 问号键（Windows 2000 或更高版本）。
        OemQuestion = 191,
        //
        // 摘要:
        //     The OEM 2 key.
        Oem2 = 191,
        //
        // 摘要:
        //     美式标准键盘上的 OEM 波形符键（Windows 2000 或更高版本）。
        Oemtilde = 192,
        //
        // 摘要:
        //     The OEM 3 key.
        Oem3 = 192,
        //
        // 摘要:
        //     美式标准键盘上的 OEM 左括号键（Windows 2000 或更高版本）。
        OemOpenBrackets = 219,
        //
        // 摘要:
        //     The OEM 4 key.
        Oem4 = 219,
        //
        // 摘要:
        //     美式标准键盘上的 OEM 管道键（Windows 2000 或更高版本）。
        OemPipe = 220,
        //
        // 摘要:
        //     The OEM 5 key.
        Oem5 = 220,
        //
        // 摘要:
        //     美式标准键盘上的 OEM 右括号键（Windows 2000 或更高版本）。
        OemCloseBrackets = 221,
        //
        // 摘要:
        //     The OEM 6 key.
        Oem6 = 221,
        //
        // 摘要:
        //     美式标准键盘上的 OEM 单/双引号键（Windows 2000 或更高版本）。
        OemQuotes = 222,
        //
        // 摘要:
        //     The OEM 7 key.
        Oem7 = 222,
        //
        // 摘要:
        //     The OEM 8 key.
        Oem8 = 223,
        //
        // 摘要:
        //     RT 102 键的键盘上的 OEM 尖括号或反斜杠键（Windows 2000 或更高版本）。
        OemBackslash = 226,
        //
        // 摘要:
        //     The OEM 102 key.
        Oem102 = 226,
        //
        // 摘要:
        //     Process Key 键。
        ProcessKey = 229,
        //
        // 摘要:
        //     用于将 Unicode 字符当作键击传递。Packet 键值是用于非键盘输入法的 32 位虚拟键值的低位字。
        Packet = 231,
        //
        // 摘要:
        //     The ATTN key.
        Attn = 246,
        //
        // 摘要:
        //     Crsel 键。
        Crsel = 247,
        //
        // 摘要:
        //     Exsel 键。
        Exsel = 248,
        //
        // 摘要:
        //     ERASE EOF 键。
        EraseEof = 249,
        //
        // 摘要:
        //     The PLAY key.
        Play = 250,
        //
        // 摘要:
        //     The ZOOM key.
        Zoom = 251,
        //
        // 摘要:
        //     保留以备将来使用的常数。
        NoName = 252,
        //
        // 摘要:
        //     PA1 键。
        Pa1 = 253,
        //
        // 摘要:
        //     Clear 键。
        OemClear = 254,
        //
        // 摘要:
        //     从键值提取键代码的位屏蔽。
        KeyCode = 65535,
        //
        // 摘要:
        //     Shift 修改键。
        Shift = 65536,
        //
        // 摘要:
        //     Ctrl 修改键。
        Control = 131072,
        //
        // 摘要:
        //     Alt 修改键。
        Alt = 262144

     */
