namespace ItopVector.Core.Win32
{
    using System;

    public class Enum
    {
        // Methods
        public Enum()
        {
			
        }


        // Nested Types
        public enum AlphaFlags : byte
        {
            // Fields
            AC_SRC_ALPHA = 1,
            AC_SRC_OVER = 0
        }

        public enum AnimateFlags
        {
            // Fields
            AW_ACTIVATE = 0x20000,
            AW_BLEND = 0x80000,
            AW_CENTER = 0x10,
            AW_HIDE = 0x10000,
            AW_HOR_NEGATIVE = 2,
            AW_HOR_POSITIVE = 1,
            AW_SLIDE = 0x40000,
            AW_VER_NEGATIVE = 8,
            AW_VER_POSITIVE = 4
        }

        public enum Bool
        {
            // Fields
            False = 0,
            True = 1
        }

        public enum BrushStyles
        {
            // Fields
            BS_DIBPATTERN = 5,
            BS_DIBPATTERN8X8 = 8,
            BS_DIBPATTERNPT = 6,
            BS_HATCHED = 2,
            BS_HOLLOW = 1,
            BS_INDEXED = 4,
            BS_MONOPATTERN = 9,
            BS_NULL = 1,
            BS_PATTERN = 3,
            BS_PATTERN8X8 = 7,
            BS_SOLID = 0
        }

        public enum CombineFlags
        {
            // Fields
            RGN_AND = 1,
            RGN_COPY = 5,
            RGN_DIFF = 4,
            RGN_OR = 2,
            RGN_XOR = 3
        }

        public enum Cursors : uint
        {
            // Fields
            IDC_APPSTARTING = 0x7f8a,
            IDC_ARROW = 0x7f00,
            IDC_CROSS = 0x7f03,
            IDC_HAND = 0x7f89,
            IDC_HELP = 0x7f8b,
            IDC_IBEAM = 0x7f01,
            IDC_ICON = 0x7f81,
            IDC_NO = 0x7f88,
            IDC_SIZE = 0x7f80,
            IDC_SIZEALL = 0x7f86,
            IDC_SIZENESW = 0x7f83,
            IDC_SIZENS = 0x7f85,
            IDC_SIZENWSE = 0x7f82,
            IDC_SIZEWE = 0x7f84,
            IDC_UPARROW = 0x7f04,
            IDC_WAIT = 0x7f02
        }

        public enum DialogCodes
        {
            // Fields
            DLGC_BUTTON = 0x2000,
            DLGC_DEFPUSHBUTTON = 0x10,
            DLGC_HASSETSEL = 8,
            DLGC_RADIOBUTTON = 0x40,
            DLGC_STATIC = 0x100,
            DLGC_UNDEFPUSHBUTTON = 0x20,
            DLGC_WANTALLKEYS = 4,
            DLGC_WANTARROWS = 1,
            DLGC_WANTCHARS = 0x80,
            DLGC_WANTMESSAGE = 4,
            DLGC_WANTTAB = 2
        }

        public enum GetWindowLongFlags
        {
            // Fields
            GWL_EXSTYLE = -20,
            GWL_HINSTANCE = -6,
            GWL_HWNDPARENT = -8,
            GWL_ID = -12,
            GWL_STYLE = -16,
            GWL_USERDATA = -21,
            GWL_WNDPROC = -4
        }

        public enum HatchStyles
        {
            // Fields
            HS_BDIAGONAL = 3,
            HS_CROSS = 4,
            HS_DIAGCROSS = 5,
            HS_FDIAGONAL = 2,
            HS_HORIZONTAL = 0,
            HS_VERTICAL = 1
        }

        public enum HitTest
        {
            // Fields
            HTBORDER = 0x12,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 0x10,
            HTBOTTOMRIGHT = 0x11,
            HTCAPTION = 2,
            HTCLIENT = 1,
            HTCLOSE = 20,
            HTERROR = -2,
            HTGROWBOX = 4,
            HTHELP = 0x15,
            HTHSCROLL = 6,
            HItopEFT = 10,
            HTMAXBUTTON = 9,
            HTMENU = 5,
            HTMINBUTTON = 8,
            HTNOWHERE = 0,
            HTOBJECT = 0x13,
            HTREDUCE = 8,
            HTRIGHT = 11,
            HTSIZE = 4,
            HTSIZEFIRST = 10,
            HTSIZELAST = 0x11,
            HTSYSMENU = 3,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTTRANSPARENT = -1,
            HTVSCROLL = 7,
            HTZOOM = 9
        }

        public enum MouseActivateFlags
        {
            // Fields
            MA_ACTIVATE = 1,
            MA_ACTIVATEANDEAT = 2,
            MA_NOACTIVATE = 3,
            MA_NOACTIVATEANDEAT = 4
        }

        public enum Msgs
        {
            // Fields
            WM_ACTIVATE = 6,
            WM_ACTIVATEAPP = 0x1c,
            WM_AFXFIRST = 0x360,
            WM_AFXLAST = 0x37f,
            WM_APP = 0x8000,
            WM_ASKCBFORMATNAME = 780,
            WM_CANCELJOURNAL = 0x4b,
            WM_CANCELMODE = 0x1f,
            WM_CAPTURECHANGED = 0x215,
            WM_CHANGECBCHAIN = 0x30d,
            WM_CHAR = 0x102,
            WM_CHARTOITEM = 0x2f,
            WM_CHILDACTIVATE = 0x22,
            WM_CLEAR = 0x303,
            WM_CLOSE = 0x10,
            WM_COMMAND = 0x111,
            WM_COMMNOTIFY = 0x44,
            WM_COMPACTING = 0x41,
            WM_COMPAREITEM = 0x39,
            WM_CONTEXTMENU = 0x7b,
            WM_COPY = 0x301,
            WM_COPYDATA = 0x4a,
            WM_CREATE = 1,
            WM_CItopCOLORBTN = 0x135,
            WM_CItopCOLORDLG = 310,
            WM_CItopCOLOREDIT = 0x133,
            WM_CItopCOLORLISTBOX = 0x134,
            WM_CItopCOLORMSGBOX = 0x132,
            WM_CItopCOLORSCROLLBAR = 0x137,
            WM_CItopCOLORSTATIC = 0x138,
            WM_CUT = 0x300,
            WM_DEADCHAR = 0x103,
            WM_DELETEITEM = 0x2d,
            WM_DESTROY = 2,
            WM_DESTROYCLIPBOARD = 0x307,
            WM_DEVICECHANGE = 0x219,
            WM_DEVMODECHANGE = 0x1b,
            WM_DISPLAYCHANGE = 0x7e,
            WM_DRAWCLIPBOARD = 0x308,
            WM_DRAWITEM = 0x2b,
            WM_DROPFILES = 0x233,
            WM_ENABLE = 10,
            WM_ENDSESSION = 0x16,
            WM_ENTERIDLE = 0x121,
            WM_ENTERMENULOOP = 0x211,
            WM_ENTERSIZEMOVE = 0x231,
            WM_ERASEBKGND = 20,
            WM_EXITMENULOOP = 530,
            WM_EXITSIZEMOVE = 0x232,
            WM_FONTCHANGE = 0x1d,
            WM_GETDLGCODE = 0x87,
            WM_GETFONT = 0x31,
            WM_GETHOTKEY = 0x33,
            WM_GETICON = 0x7f,
            WM_GETMINMAXINFO = 0x24,
            WM_GETOBJECT = 0x3d,
            WM_GETTEXT = 13,
            WM_GETTEXItopENGTH = 14,
            WM_HANDHELDFIRST = 0x358,
            WM_HANDHELDLAST = 0x35f,
            WM_HELP = 0x53,
            WM_HOTKEY = 0x312,
            WM_HSCROLL = 0x114,
            WM_HSCROLLCLIPBOARD = 0x30e,
            WM_ICONERASEBKGND = 0x27,
            WM_IME_CHAR = 0x286,
            WM_IME_COMPOSITION = 0x10f,
            WM_IME_COMPOSITIONFULL = 0x284,
            WM_IME_CONTROL = 0x283,
            WM_IME_ENDCOMPOSITION = 270,
            WM_IME_KEYDOWN = 0x290,
            WM_IME_KEYLAST = 0x10f,
            WM_IME_KEYUP = 0x291,
            WM_IME_NOTIFY = 0x282,
            WM_IME_REQUEST = 0x288,
            WM_IME_SELECT = 0x285,
            WM_IME_SETCONTEXT = 0x281,
            WM_IME_STARTCOMPOSITION = 0x10d,
            WM_INITDIALOG = 0x110,
            WM_INITMENU = 0x116,
            WM_INITMENUPOPUP = 0x117,
            WM_INPUItopANGCHANGE = 0x51,
            WM_INPUItopANGCHANGEREQUEST = 80,
            WM_KEYDOWN = 0x100,
            WM_KEYLAST = 0x108,
            WM_KEYUP = 0x101,
            WM_KILLFOCUS = 8,
            WM_LBUTTONDBLCLK = 0x203,
            WM_LBUTTONDOWN = 0x201,
            WM_LBUTTONUP = 0x202,
            WM_MBUTTONDBLCLK = 0x209,
            WM_MBUTTONDOWN = 0x207,
            WM_MBUTTONUP = 520,
            WM_MDIACTIVATE = 0x222,
            WM_MDICASCADE = 0x227,
            WM_MDICREATE = 0x220,
            WM_MDIDESTROY = 0x221,
            WM_MDIGETACTIVE = 0x229,
            WM_MDIICONARRANGE = 0x228,
            WM_MDIMAXIMIZE = 0x225,
            WM_MDINEXT = 0x224,
            WM_MDIREFRESHMENU = 0x234,
            WM_MDIRESTORE = 0x223,
            WM_MDISETMENU = 560,
            WM_MDITILE = 550,
            WM_MEASUREITEM = 0x2c,
            WM_MENUCHAR = 0x120,
            WM_MENUCOMMAND = 0x126,
            WM_MENUDRAG = 0x123,
            WM_MENUGETOBJECT = 0x124,
            WM_MENURBUTTONUP = 290,
            WM_MENUSELECT = 0x11f,
            WM_MOUSEACTIVATE = 0x21,
            WM_MOUSEHOVER = 0x2a1,
            WM_MOUSELEAVE = 0x2a3,
            WM_MOUSEMOVE = 0x200,
            WM_MOUSEWHEEL = 0x20a,
            WM_MOVE = 3,
            WM_MOVING = 0x216,
            WM_NCACTIVATE = 0x86,
            WM_NCCALCSIZE = 0x83,
            WM_NCCREATE = 0x81,
            WM_NCDESTROY = 130,
            WM_NCHITTEST = 0x84,
            WM_NCLBUTTONDBLCLK = 0xa3,
            WM_NCLBUTTONDOWN = 0xa1,
            WM_NCLBUTTONUP = 0xa2,
            WM_NCMBUTTONDBLCLK = 0xa9,
            WM_NCMBUTTONDOWN = 0xa7,
            WM_NCMBUTTONUP = 0xa8,
            WM_NCMOUSEMOVE = 160,
            WM_NCPAINT = 0x85,
            WM_NCRBUTTONDBLCLK = 0xa6,
            WM_NCRBUTTONDOWN = 0xa4,
            WM_NCRBUTTONUP = 0xa5,
            WM_NCXBUTTONDOWN = 0xab,
            WM_NCXBUTTONUP = 0xac,
            WM_NEXTDLGCItop = 40,
            WM_NEXTMENU = 0x213,
            WM_NOTIFY = 0x4e,
            WM_NOTIFYFORMAT = 0x55,
            WM_NULL = 0,
            WM_PAINT = 15,
            WM_PAINTCLIPBOARD = 0x309,
            WM_PAINTICON = 0x26,
            WM_PALETTECHANGED = 0x311,
            WM_PALETTEISCHANGING = 0x310,
            WM_PARENTNOTIFY = 0x210,
            WM_PASTE = 770,
            WM_PENWINFIRST = 0x380,
            WM_PENWINLAST = 0x38f,
            WM_POWER = 0x48,
            WM_PRINT = 0x317,
            WM_PRINTCLIENT = 0x318,
            WM_QUERYDRAGICON = 0x37,
            WM_QUERYENDSESSION = 0x11,
            WM_QUERYNEWPALETTE = 0x30f,
            WM_QUERYOPEN = 0x13,
            WM_QUEUESYNC = 0x23,
            WM_QUIT = 0x12,
            WM_RBUTTONDBLCLK = 0x206,
            WM_RBUTTONDOWN = 0x204,
            WM_RBUTTONUP = 0x205,
            WM_RENDERALLFORMATS = 0x306,
            WM_RENDERFORMAT = 0x305,
            WM_SETCURSOR = 0x20,
            WM_SETFOCUS = 7,
            WM_SETFONT = 0x30,
            WM_SETHOTKEY = 50,
            WM_SETICON = 0x80,
            WM_SETREDRAW = 11,
            WM_SETTEXT = 12,
            WM_SETTINGCHANGE = 0x1a,
            WM_SHOWWINDOW = 0x18,
            WM_SIZE = 5,
            WM_SIZECLIPBOARD = 0x30b,
            WM_SIZING = 0x214,
            WM_SPOOLERSTATUS = 0x2a,
            WM_STYLECHANGED = 0x7d,
            WM_STYLECHANGING = 0x7c,
            WM_SYNCPAINT = 0x88,
            WM_SYSCHAR = 0x106,
            WM_SYSCOLORCHANGE = 0x15,
            WM_SYSCOMMAND = 0x112,
            WM_SYSDEADCHAR = 0x107,
            WM_SYSKEYDOWN = 260,
            WM_SYSKEYUP = 0x105,
            WM_TCARD = 0x52,
            WM_TIMECHANGE = 30,
            WM_TIMER = 0x113,
            WM_UNDO = 0x304,
            WM_UNINITMENUPOPUP = 0x125,
            WM_USER = 0x400,
            WM_USERCHANGED = 0x54,
            WM_VKEYTOITEM = 0x2e,
            WM_VSCROLL = 0x115,
            WM_VSCROLLCLIPBOARD = 0x30a,
            WM_WINDOWPOSCHANGED = 0x47,
            WM_WINDOWPOSCHANGING = 70,
            WM_WININICHANGE = 0x1a,
            WM_XBUTTONDBLCLK = 0x20d,
            WM_XBUTTONDOWN = 0x20b,
            WM_XBUTTONUP = 0x20c
        }

        public enum PeekMessageFlags
        {
            // Fields
            PM_NOREMOVE = 0,
            PM_NOYIELD = 2,
            PM_REMOVE = 1
        }

        public enum RasterOperations : uint
        {
            // Fields
            BLACKNESS = 0x42,
            DSTINVERT = 0x550009,
            MERGECOPY = 0xc000ca,
            MERGEPAINT = 0xbb0226,
            NOTSRCCOPY = 0x330008,
            NOTSRCERASE = 0x1100a6,
            PATCOPY = 0xf00021,
            PATINVERT = 0x5a0049,
            PATPAINT = 0xfb0a09,
            SRCAND = 0x8800c6,
            SRCCOPY = 0xcc0020,
            SRCERASE = 0x440328,
            SRCINVERT = 0x660046,
            SRCPAINT = 0xee0086,
            WHITENESS = 0xff0062
        }

        public enum SetWindowPosFlags : uint
        {
            // Fields
            SWP_ASYNCWINDOWPOS = 0x4000,
            SWP_DEFERERASE = 0x2000,
            SWP_DRAWFRAME = 0x20,
            SWP_FRAMECHANGED = 0x20,
            SWP_HIDEWINDOW = 0x80,
            SWP_NOACTIVATE = 0x10,
            SWP_NOCOPYBITS = 0x100,
            SWP_NOMOVE = 2,
            SWP_NOOWNERZORDER = 0x200,
            SWP_NOREDRAW = 8,
            SWP_NOREPOSITION = 0x200,
            SWP_NOSENDCHANGING = 0x400,
            SWP_NOSIZE = 1,
            SWP_NOZORDER = 4,
            SWP_SHOWWINDOW = 0x40
        }

        public enum SetWindowPosZ
        {
            // Fields
            HWND_BOTTOM = 1,
            HWND_NOTOPMOST = -2,
            HWND_TOP = 0,
            HWND_TOPMOST = -1
        }

        public enum ShowWindowStyles : short
        {
            // Fields
            SW_FORCEMINIMIZE = 11,
            SW_HIDE = 0,
            SW_MAX = 11,
            SW_MAXIMIZE = 3,
            SW_MINIMIZE = 6,
            SW_NORMAL = 1,
            SW_RESTORE = 9,
            SW_SHOW = 5,
            SW_SHOWDEFAULT = 10,
            SW_SHOWMAXIMIZED = 3,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOWNORMAL = 1
        }

        public enum SPIActions
        {
            // Fields
            SPI_GETACCESSTIMEOUT = 60,
            SPI_GETACTIVEWINDOWTRACKING = 0x1000,
            SPI_GETACTIVEWNDTRKTIMEOUT = 0x2002,
            SPI_GETACTIVEWNDTRKZORDER = 0x100c,
            SPI_GETANIMATION = 0x48,
            SPI_GETBEEP = 1,
            SPI_GETBORDER = 5,
            SPI_GETCARETWIDTH = 0x2006,
            SPI_GETCOMBOBOXANIMATION = 0x1004,
            SPI_GETCURSORSHADOW = 0x101a,
            SPI_GETDEFAULTINPUItopANG = 0x59,
            SPI_GETDESKWALLPAPER = 0x73,
            SPI_GETDRAGFULLWINDOWS = 0x26,
            SPI_GETDROPSHADOW = 0x1024,
            SPI_GETFASTTASKSWITCH = 0x23,
            SPI_GETFILTERKEYS = 50,
            SPI_GETFLATMENU = 0x1022,
            SPI_GETFOCUSBORDERHEIGHT = 0x2010,
            SPI_GETFOCUSBORDERWIDTH = 0x200e,
            SPI_GETFONTSMOOTHING = 0x4a,
            SPI_GETFONTSMOOTHINGCONTRAST = 0x200c,
            SPI_GETFONTSMOOTHINGTYPE = 0x200a,
            SPI_GETFOREGROUNDFLASHCOUNT = 0x2004,
            SPI_GETFOREGROUNDLOCKTIMEOUT = 0x2000,
            SPI_GETGRADIENTCAPTIONS = 0x1008,
            SPI_GETGRIDGRANULARITY = 0x12,
            SPI_GETHIGHCONTRAST = 0x42,
            SPI_GETHOTTRACKING = 0x100e,
            SPI_GETICONMETRICS = 0x2d,
            SPI_GETICONTIItopELOGFONT = 0x1f,
            SPI_GETICONTIItopEWRAP = 0x19,
            SPI_GETKEYBOARDCUES = 0x100a,
            SPI_GETKEYBOARDDELAY = 0x16,
            SPI_GETKEYBOARDPREF = 0x44,
            SPI_GETKEYBOARDSPEED = 10,
            SPI_GEItopISTBOXSMOOTHSCROLLING = 0x1006,
            SPI_GEItopOWPOWERACTIVE = 0x53,
            SPI_GEItopOWPOWERTIMEOUT = 0x4f,
            SPI_GETMENUANIMATION = 0x1002,
            SPI_GETMENUDROPALIGNMENT = 0x1b,
            SPI_GETMENUFADE = 0x1012,
            SPI_GETMENUSHOWDELAY = 0x6a,
            SPI_GETMENUUNDERLINES = 0x100a,
            SPI_GETMINIMIZEDMETRICS = 0x2b,
            SPI_GETMOUSE = 3,
            SPI_GETMOUSECLICKLOCK = 0x101e,
            SPI_GETMOUSECLICKLOCKTIME = 0x2008,
            SPI_GETMOUSEHOVERHEIGHT = 100,
            SPI_GETMOUSEHOVERTIME = 0x66,
            SPI_GETMOUSEHOVERWIDTH = 0x62,
            SPI_GETMOUSEKEYS = 0x36,
            SPI_GETMOUSESONAR = 0x101c,
            SPI_GETMOUSESPEED = 0x70,
            SPI_GETMOUSETRAILS = 0x5e,
            SPI_GETMOUSEVANISH = 0x1020,
            SPI_GETNONCLIENTMETRICS = 0x29,
            SPI_GETPOWEROFFACTIVE = 0x54,
            SPI_GETPOWEROFFTIMEOUT = 80,
            SPI_GETSCREENREADER = 70,
            SPI_GETSCREENSAVEACTIVE = 0x10,
            SPI_GETSCREENSAVERRUNNING = 0x72,
            SPI_GETSCREENSAVETIMEOUT = 14,
            SPI_GETSELECTIONFADE = 0x1014,
            SPI_GETSERIALKEYS = 0x3e,
            SPI_GETSHOWIMEUI = 110,
            SPI_GETSHOWSOUNDS = 0x38,
            SPI_GETSNAPTODEFBUTTON = 0x5f,
            SPI_GETSOUNDSENTRY = 0x40,
            SPI_GETSTICKYKEYS = 0x3a,
            SPI_GETTOGGLEKEYS = 0x34,
            SPI_GETTOOLTIPANIMATION = 0x1016,
            SPI_GETTOOLTIPFADE = 0x1018,
            SPI_GETUIEFFECTS = 0x103e,
            SPI_GETWHEELSCROLLLINES = 0x68,
            SPI_GETWINDOWSEXTENSION = 0x5c,
            SPI_GETWORKAREA = 0x30,
            SPI_ICONHORIZONTALSPACING = 13,
            SPI_ICONVERTICALSPACING = 0x18,
            SPI_LANGDRIVER = 12,
            SPI_SCREENSAVERRUNNING = 0x61,
            SPI_SETACCESSTIMEOUT = 0x3d,
            SPI_SETACTIVEWINDOWTRACKING = 0x1001,
            SPI_SETACTIVEWNDTRKTIMEOUT = 0x2003,
            SPI_SETACTIVEWNDTRKZORDER = 0x100d,
            SPI_SETANIMATION = 0x49,
            SPI_SETBEEP = 2,
            SPI_SETBORDER = 6,
            SPI_SETCARETWIDTH = 0x2007,
            SPI_SETCOMBOBOXANIMATION = 0x1005,
            SPI_SETCURSORS = 0x57,
            SPI_SETCURSORSHADOW = 0x101b,
            SPI_SETDEFAULTINPUItopANG = 90,
            SPI_SETDESKPATTERN = 0x15,
            SPI_SETDESKWALLPAPER = 20,
            SPI_SETDOUBLECLICKTIME = 0x20,
            SPI_SETDOUBLECLKHEIGHT = 30,
            SPI_SETDOUBLECLKWIDTH = 0x1d,
            SPI_SETDRAGFULLWINDOWS = 0x25,
            SPI_SETDRAGHEIGHT = 0x4d,
            SPI_SETDRAGWIDTH = 0x4c,
            SPI_SETDROPSHADOW = 0x1025,
            SPI_SETFASTTASKSWITCH = 0x24,
            SPI_SETFILTERKEYS = 0x33,
            SPI_SETFLATMENU = 0x1023,
            SPI_SETFOCUSBORDERHEIGHT = 0x2011,
            SPI_SETFOCUSBORDERWIDTH = 0x200f,
            SPI_SETFONTSMOOTHING = 0x4b,
            SPI_SETFONTSMOOTHINGCONTRAST = 0x200d,
            SPI_SETFONTSMOOTHINGTYPE = 0x200b,
            SPI_SETFOREGROUNDFLASHCOUNT = 0x2005,
            SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001,
            SPI_SETGRADIENTCAPTIONS = 0x1009,
            SPI_SETGRIDGRANULARITY = 0x13,
            SPI_SETHANDHELD = 0x4e,
            SPI_SETHIGHCONTRAST = 0x43,
            SPI_SETHOTTRACKING = 0x100f,
            SPI_SETICONMETRICS = 0x2e,
            SPI_SETICONS = 0x58,
            SPI_SETICONTIItopELOGFONT = 0x22,
            SPI_SETICONTIItopEWRAP = 0x1a,
            SPI_SETKEYBOARDCUES = 0x100b,
            SPI_SETKEYBOARDDELAY = 0x17,
            SPI_SETKEYBOARDPREF = 0x45,
            SPI_SETKEYBOARDSPEED = 11,
            SPI_SEItopANGTOGGLE = 0x5b,
            SPI_SEItopISTBOXSMOOTHSCROLLING = 0x1007,
            SPI_SEItopOWPOWERACTIVE = 0x55,
            SPI_SEItopOWPOWERTIMEOUT = 0x51,
            SPI_SETMENUANIMATION = 0x1003,
            SPI_SETMENUDROPALIGNMENT = 0x1c,
            SPI_SETMENUFADE = 0x1013,
            SPI_SETMENUSHOWDELAY = 0x6b,
            SPI_SETMENUUNDERLINES = 0x100b,
            SPI_SETMINIMIZEDMETRICS = 0x2c,
            SPI_SETMOUSE = 4,
            SPI_SETMOUSEBUTTONSWAP = 0x21,
            SPI_SETMOUSECLICKLOCK = 0x101f,
            SPI_SETMOUSECLICKLOCKTIME = 0x2009,
            SPI_SETMOUSEHOVERHEIGHT = 0x65,
            SPI_SETMOUSEHOVERTIME = 0x67,
            SPI_SETMOUSEHOVERWIDTH = 0x63,
            SPI_SETMOUSEKEYS = 0x37,
            SPI_SETMOUSESONAR = 0x101d,
            SPI_SETMOUSESPEED = 0x71,
            SPI_SETMOUSETRAILS = 0x5d,
            SPI_SETMOUSEVANISH = 0x1021,
            SPI_SETNONCLIENTMETRICS = 0x2a,
            SPI_SETPENWINDOWS = 0x31,
            SPI_SETPOWEROFFACTIVE = 0x56,
            SPI_SETPOWEROFFTIMEOUT = 0x52,
            SPI_SETSCREENREADER = 0x47,
            SPI_SETSCREENSAVEACTIVE = 0x11,
            SPI_SETSCREENSAVERRUNNING = 0x61,
            SPI_SETSCREENSAVETIMEOUT = 15,
            SPI_SETSELECTIONFADE = 0x1015,
            SPI_SETSERIALKEYS = 0x3f,
            SPI_SETSHOWIMEUI = 0x6f,
            SPI_SETSHOWSOUNDS = 0x39,
            SPI_SETSNAPTODEFBUTTON = 0x60,
            SPI_SETSOUNDSENTRY = 0x41,
            SPI_SETSTICKYKEYS = 0x3b,
            SPI_SETTOGGLEKEYS = 0x35,
            SPI_SETTOOLTIPANIMATION = 0x1017,
            SPI_SETTOOLTIPFADE = 0x1019,
            SPI_SETUIEFFECTS = 0x103f,
            SPI_SETWHEELSCROLLLINES = 0x69,
            SPI_SETWORKAREA = 0x2f
        }

        public enum SPIWinINIFlags
        {
            // Fields
            SPIF_SENDCHANGE = 2,
            SPIF_SENDWININICHANGE = 2,
            SPIF_UPDATEINIFILE = 1
        }

        public enum TernaryRasterOperations
        {
            // Fields
            BLACKNESS = 0x42,
            DSTINVERT = 0x550009,
            MERGECOPY = 0xc000ca,
            MERGEPAINT = 0xbb0226,
            NOTSRCCOPY = 0x330008,
            NOTSRCERASE = 0x1100a6,
            PATCOPY = 0xf00021,
            PATINVERT = 0x5a0049,
            PATPAINT = 0xfb0a09,
            SRCAND = 0x8800c6,
            SRCCOPY = 0xcc0020,
            SRCERASE = 0x440328,
            SRCINVERT = 0x660046,
            SRCPAINT = 0xee0086,
            WHITENESS = 0xff0062
        }

        public enum TrackerEventFlags : uint
        {
            // Fields
            TME_CANCEL = 0x80000000,
            TME_HOVER = 1,
            TME_LEAVE = 2,
            TME_QUERY = 0x40000000
        }

        public enum UpdateLayeredWindowsFlags
        {
            // Fields
            ULW_ALPHA = 2,
            ULW_COLORKEY = 1,
            ULW_OPAQUE = 4
        }

        public enum VirtualKeys
        {
            // Fields
            VK_0 = 0x30,
            VK_1 = 0x31,
            VK_2 = 50,
            VK_3 = 0x33,
            VK_4 = 0x34,
            VK_5 = 0x35,
            VK_6 = 0x36,
            VK_7 = 0x37,
            VK_8 = 0x38,
            VK_9 = 0x39,
            VK_A = 0x41,
            VK_ADD = 0x6b,
            VK_APPS = 0x5d,
            VK_ATTN = 0xf6,
            VK_B = 0x42,
            VK_BACK = 8,
            VK_C = 0x43,
            VK_CANCEL = 3,
            VK_CAPITAL = 20,
            VK_CLEAR = 12,
            VK_CONTROL = 0x11,
            VK_CRSEL = 0xf7,
            VK_D = 0x44,
            VK_DECIMAL = 110,
            VK_DIVIDE = 0x6f,
            VK_DOWN = 40,
            VK_E = 0x45,
            VK_END = 0x23,
            VK_EREOF = 0xf9,
            VK_ESCAPE = 0x1b,
            VK_EXECUTE = 0x2b,
            VK_EXSEL = 0xf8,
            VK_F = 70,
            VK_G = 0x47,
            VK_H = 0x48,
            VK_HELP = 0x2f,
            VK_HOME = 0x24,
            VK_I = 0x49,
            VK_J = 0x4a,
            VK_K = 0x4b,
            VK_L = 0x4c,
            VK_LBUTTON = 1,
            VK_LCONTROL = 0xa2,
            VK_LEFT = 0x25,
            VK_LMENU = 0xa4,
            VK_LSHIFT = 160,
            VK_LWIN = 0x5b,
            VK_M = 0x4d,
            VK_MENU = 0x12,
            VK_MULTIPLY = 0x6a,
            VK_N = 0x4e,
            VK_NEXT = 0x22,
            VK_NONAME = 0xfc,
            VK_NUMPAD0 = 0x60,
            VK_NUMPAD1 = 0x61,
            VK_NUMPAD2 = 0x62,
            VK_NUMPAD3 = 0x63,
            VK_NUMPAD4 = 100,
            VK_NUMPAD5 = 0x65,
            VK_NUMPAD6 = 0x66,
            VK_NUMPAD7 = 0x67,
            VK_NUMPAD8 = 0x68,
            VK_NUMPAD9 = 0x69,
            VK_O = 0x4f,
            VK_OEM_CLEAR = 0xfe,
            VK_P = 80,
            VK_PA1 = 0xfd,
            VK_PLAY = 250,
            VK_PRIOR = 0x21,
            VK_Q = 0x51,
            VK_R = 0x52,
            VK_RCONTROL = 0xa3,
            VK_RETURN = 13,
            VK_RIGHT = 0x27,
            VK_RMENU = 0xa5,
            VK_RSHIFT = 0xa1,
            VK_RWIN = 0x5c,
            VK_S = 0x53,
            VK_SELECT = 0x29,
            VK_SEPARATOR = 0x6c,
            VK_SHIFT = 0x10,
            VK_SNAPSHOT = 0x2c,
            VK_SPACE = 0x20,
            VK_SUBTRACT = 0x6d,
            VK_T = 0x54,
            VK_TAB = 9,
            VK_U = 0x55,
            VK_UP = 0x26,
            VK_V = 0x56,
            VK_W = 0x57,
            VK_X = 0x58,
            VK_Y = 0x59,
            VK_Z = 90,
            VK_ZOOM = 0xfb
        }

        public enum WindowExStyles
        {
            // Fields
            WS_EX_ACCEPTFILES = 0x10,
            WS_EX_APPWINDOW = 0x40000,
            WS_EX_CLIENTEDGE = 0x200,
            WS_EX_CONTEXTHELP = 0x400,
            WS_EX_CONTROLPARENT = 0x10000,
            WS_EX_DLGMODALFRAME = 1,
            WS_EX_LAYERED = 0x80000,
            WS_EX_LEFT = 0,
            WS_EX_LEFTSCROLLBAR = 0x4000,
            WS_EX_LTRREADING = 0,
            WS_EX_MDICHILD = 0x40,
            WS_EX_NOPARENTNOTIFY = 4,
            WS_EX_OVERLAPPEDWINDOW = 0x300,
            WS_EX_PALETTEWINDOW = 0x188,
            WS_EX_RIGHT = 0x1000,
            WS_EX_RIGHTSCROLLBAR = 0,
            WS_EX_RItopREADING = 0x2000,
            WS_EX_STATICEDGE = 0x20000,
            WS_EX_TOOLWINDOW = 0x80,
            WS_EX_TOPMOST = 8,
            WS_EX_TRANSPARENT = 0x20,
            WS_EX_WINDOWEDGE = 0x100
        }

        public enum WindowStyles : uint
        {
            // Fields
            WS_BORDER = 0x800000,
            WS_CAPTION = 0xc00000,
            WS_CHILD = 0x40000000,
            WS_CHILDWINDOW = 0x40000000,
            WS_CLIPCHILDREN = 0x2000000,
            WS_CLIPSIBLINGS = 0x4000000,
            WS_DISABLED = 0x8000000,
            WS_DLGFRAME = 0x400000,
            WS_GROUP = 0x20000,
            WS_HSCROLL = 0x100000,
            WS_ICONIC = 0x20000000,
            WS_MAXIMIZE = 0x1000000,
            WS_MAXIMIZEBOX = 0x10000,
            WS_MINIMIZE = 0x20000000,
            WS_MINIMIZEBOX = 0x20000,
            WS_OVERLAPPED = 0,
            WS_OVERLAPPEDWINDOW = 0xcf0000,
            WS_POPUP = 0x80000000,
            WS_POPUPWINDOW = 0x80880000,
            WS_SIZEBOX = 0x40000,
            WS_SYSMENU = 0x80000,
            WS_TABSTOP = 0x10000,
            WS_THICKFRAME = 0x40000,
            WS_TILED = 0,
            WS_TILEDWINDOW = 0xcf0000,
            WS_VISIBLE = 0x10000000,
            WS_VSCROLL = 0x200000
        }
    }
}

