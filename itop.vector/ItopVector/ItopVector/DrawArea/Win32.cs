using ItopVector.Core.Win32;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace ItopVector.DrawArea
{

	public class Win32
	{
		// Methods
		public Win32()
		{
			this.first = true;
		}

		public void ReleaseDC()
		{
			User32.ReleaseDC(this.WhWnd, this.hdc);	
			Gdi32.DeleteObject(hpen);
		}

		public IntPtr W32GetDC(IntPtr hWnd)
		{
			this.WhWnd = hWnd;
			hpen =ItopVector.Core.Win32.Gdi32.CreatePen(2,1,0);
			return User32.GetDC(hWnd);
		}

		public bool W32Rectangle(int left, int top, int right, int bottom)
		{
			return Gdi32.Rectangle(this.hdc,left,top,right,bottom);
		}
		public bool W32PolyDraw(GraphicsPath gPath)
		{
			if ((gPath != null) && (gPath.PointCount > 0))
			{
				GraphicsPath path1 = (GraphicsPath) gPath.Clone();
				this.pf = (PointF[]) path1.PathPoints.Clone();
				this.bg = path1.PathTypes;
				this.nCount = path1.PointCount;
				this.p = new Point[this.nCount];
				this.b = new byte[this.nCount];
				for (int num1 = 0; num1 < this.nCount; num1++)
				{
					this.p[num1] = new Point((int) this.pf[num1].X, (int) this.pf[num1].Y);
					switch (this.bg[num1])
					{
						case 0:
						{
							this.b[num1] = 6;
							goto Label_012B;
						}
						case 1:
						{
							this.b[num1] = 2;
							goto Label_012B;
						}
						case 2:
						case 130:
						{
							goto Label_012B;
						}
						case 3:
						{
							this.b[num1] = 4;
							goto Label_012B;
						}
						case 0x80:
						{
							this.b[num1] = 1;
							goto Label_012B;
						}
						case 0x81:
						{
							this.b[num1] = 3;
							goto Label_012B;
						}
						case 0x83:
						{
							break;
						}
						default:
						{
							goto Label_012B;
						}
					}
					this.b[num1] = 5;
				Label_012B:;
				}	

				
							
				IntPtr old_pen = ItopVector.Core.Win32.Gdi32.SelectObject(hdc,hpen);

				Gdi32.PolyDraw(this.hdc, this.p, this.b, this.nCount);
				Gdi32.SelectObject(hdc,old_pen);
			}
			return true;
		}

		public int W32SetROP2(int i)
		{
			return Gdi32.SetROP2(this.hdc,i);
		}


		private IntPtr hpen;
		// Fields
		public byte[] b;
		public byte[] bg;
		public bool first;
		public IntPtr hdc;
		public int nCount;
		public Point[] p;
		public PointF[] pf;
		public IntPtr WhWnd;
	}
}

