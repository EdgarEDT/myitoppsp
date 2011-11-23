namespace ItopVector.Core.Win32
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct LOGPEN
    {
        public uint lpStyle;
        public uint lpWidth;
        public COLOR lpColor;
    }
	
}

