namespace ItopVector.Core.Win32
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct COLOR
    {
        public int R;
        public int G;
        public int B;
        public COLOR(int r, int g, int b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }
    }
}

