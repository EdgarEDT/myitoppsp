namespace ItopVector.Core.Win32
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public class RopDraw
    {
        // Methods
        static RopDraw()
        {
            RopDraw._halfToneBrush = IntPtr.Zero;
        }

        public RopDraw()
        {
        }

        protected static IntPtr GetHalfToneBrush()
        {
            if (RopDraw._halfToneBrush == IntPtr.Zero)
            {
                Bitmap bitmap1 = new Bitmap(8, 8, PixelFormat.Format32bppArgb);
                Color color1 = Color.FromArgb(0xff, 0xff, 0xff, 0xff);
                Color color2 = Color.FromArgb(0xff, 0, 0, 0);
                bool flag1 = true;
                int num1 = 0;
                while (num1 < 8)
                {
                    int num2 = 0;
                    while (num2 < 8)
                    {
                        bitmap1.SetPixel(num1, num2, flag1 ? color1 : color2);
                        num2++;
                        flag1 = !flag1;
                    }
                    num1++;
                    flag1 = !flag1;
                }
                IntPtr ptr1 = bitmap1.GetHbitmap();
                ItopVector.Core.Win32.LOGBRUSH logbrush1 = new ItopVector.Core.Win32.LOGBRUSH();
                logbrush1.lbStyle = 3;
                logbrush1.lbHatch = (uint) ((int) ptr1);
                RopDraw._halfToneBrush = Gdi32.CreateBrushIndirect(ref logbrush1);
            }
            return RopDraw._halfToneBrush;
        }

        public static bool W32PolyDraw(IntPtr hdc, GraphicsPath gPath)
        {
            if ((gPath != null) && (gPath.PointCount > 0))
            {
                byte[] buffer1 = gPath.PathTypes;
                int num1 = gPath.PointCount;
                Point[] pointArray1 = new Point[num1];
                byte[] buffer2 = new byte[num1];
                for (int num2 = 0; num2 < num1; num2++)
                {
                    pointArray1[num2] = new Point((int) gPath.PathPoints[num2].X, (int) gPath.PathPoints[num2].Y);
                    switch (buffer1[num2])
                    {
                        case 0:
                        {
                            buffer2[num2] = 6;
                            goto Label_00D1;
                        }
                        case 1:
                        {
                            buffer2[num2] = 2;
                            goto Label_00D1;
                        }
                        case 2:
                        case 130:
                        {
                            goto Label_00D1;
                        }
                        case 3:
                        {
                            buffer2[num2] = 4;
                            goto Label_00D1;
                        }
                        case 0x80:
                        {
                            buffer2[num2] = 1;
                            goto Label_00D1;
                        }
                        case 0x81:
                        {
                            buffer2[num2] = 3;
                            goto Label_00D1;
                        }
                        case 0x83:
                        {
                            break;
                        }
                        default:
                        {
                            goto Label_00D1;
                        }
                    }
                    buffer2[num2] = 5;
                Label_00D1:;
                }
                Gdi32.PolyDraw(hdc, pointArray1, buffer2, num1);
            }
            return true;
        }

        public static bool Win32PolyPolygon(IntPtr hdc, GraphicsPath gPath)
        {
            if ((gPath != null) && (gPath.PointCount > 0))
            {
                GraphicsPath path1 = (GraphicsPath) gPath.Clone();
                path1.Flatten(new Matrix(), 0.25f);
                int num1 = path1.PointCount;
                Point[] pointArray1 = new Point[num1];
                for (int num2 = 0; num2 < num1; num2++)
                {
                    pointArray1[num2] = Point.Round(path1.PathPoints[num2]);
                }
                Gdi32.Polygon(hdc, pointArray1, num1);
            }
            return true;
        }


        // Fields
        protected static IntPtr _halfToneBrush;
    }
}

