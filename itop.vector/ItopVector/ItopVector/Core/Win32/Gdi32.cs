namespace ItopVector.Core.Win32
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class Gdi32
    {
        // Methods
        public Gdi32()
        {
        }

        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern bool BeginPath(IntPtr n);

        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern ItopVector.Core.Win32.Enum.Bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, ItopVector.Core.Win32.Enum.TernaryRasterOperations dwRop);

		[DllImport("gdi32.dll",CharSet=CharSet.Auto)]
		public static extern bool BitBlt(
			IntPtr hdcDest, //目标设备的句柄 
			int nXDest,  // 目标对象的左上角的X坐标
			int nYDest,  // 目标对象的左上角的X坐标
			int nWidth,  // 目标对象的矩形的宽度
			int nHeight, // 目标对象的矩形的长度 
			IntPtr hdcSrc,  // 源设备的句柄
			int nXSrc,   // 源对象的左上角的X坐标 
			int nYSrc,   // 源对象的左上角的X坐标 
			System.Int32 dwRop  // 光栅的操作值 
			);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern int CombineRgn(IntPtr dest, IntPtr src1, IntPtr src2, int flags);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr CreateBrushIndirect(ref ItopVector.Core.Win32.LOGBRUSH brush);

        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr CreatePen(int style, int width, int color);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr CreatePenIndirect(ref ItopVector.Core.Win32.LOGPEN pen);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr CreateRectRgnIndirect(ref ItopVector.Core.Win32.RECT rect);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern long CreateSolidBrush(COLOR color);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern bool DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr DeleteObject(ItopVector.Core.Win32.LOGBRUSH brush);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern bool EndPath(IntPtr n);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern int GetClipBox(IntPtr hDC, ref ItopVector.Core.Win32.RECT rectBox);

        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern int GetPath(IntPtr n, PointF[] p, byte[] b, int nCount);

        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern IntPtr GetStockObject(IntPtr hdc, int index);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern int InvertRect(IntPtr hDC, ItopVector.Core.Win32.RECT rect);

        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern bool LineTo(IntPtr n, int x, int y);

        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern bool MoveToEx(IntPtr n, int x, int y, Point point);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern bool PatBlt(IntPtr hDC, int x, int y, int width, int height, uint flags);

        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern bool PolyDraw(IntPtr n, Point[] p, byte[] b, int nCount);

        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern bool Polygon(IntPtr n, Point[] p, int nCount);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern bool Rectangle(IntPtr hDC, int left, int top, int right, int bottom);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern int SelectClipRgn(IntPtr hDC, IntPtr hRgn);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr SelectObject(IntPtr hDC, long hbrush);

        [DllImport("gdi32.dll", ExactSpelling=true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern COLOR SetDCBrushColor(IntPtr hDC, COLOR color);

        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
        public static extern COLOR SetDCPenColor(IntPtr hDC, COLOR color);

        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern int SetROP2(IntPtr n, int i);

        [DllImport("gdi32.dll", SetLastError=true, ExactSpelling=true)]
        public static extern bool StrokePath(IntPtr n);

    }
}

