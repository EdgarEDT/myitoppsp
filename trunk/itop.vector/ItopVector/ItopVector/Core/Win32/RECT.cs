namespace ItopVector.Core.Win32
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
        public RECT(int l, int t, int r, int b)
        {
            this.left = l;
            this.top = t;
            this.right = r;
            this.bottom = b;
        }
    }
}

