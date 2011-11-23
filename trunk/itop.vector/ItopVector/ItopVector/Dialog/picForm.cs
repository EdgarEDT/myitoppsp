using System;
using System.Drawing;
using System.Collections;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Windows.Forms;
using ItopVector.Core;
using ItopVector.Core.Win32;


namespace ItopVector.Dialog
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class picForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		//
		//自定义变量
		//
		private Point pot;
		private Rectangle area = Rectangle.Empty;
		public Image img;
		private int index = -1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private bool saveFile;
		//
		//系统生成定义
		//
		private System.ComponentModel.Container components = null;

		public picForm(bool save)
		{
			this.Bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
			this.BackgroundImage =CaptureDesktopWindow();
			//			this.BackgroundImage=bmp;
			
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
			SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
			saveFile=save;
			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			// 
			// picForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(456, 320);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "picForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.picForm_KeyDown);
			this.DoubleClick += new System.EventHandler(this.picForm_DoubleClick);

		}
		#endregion

		/// <summary>
		/// 重载MouseDown
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			//MessageBox.Show("ok");
			base.OnMouseDown (e);
			//if(this.a == 1)
		{
			if(this.area == Rectangle.Empty && e.Button == MouseButtons.Left)
			{
				this.area.Location = new Point(e.X,e.Y);
			}
			this.pot = new Point(e.X,e.Y);
			this.index = this.GetSelectedHandle(new Point(e.X,e.Y));
			this.SetCursor();
		}
		}
		public  Bitmap CaptureDesktopWindow()
		{			
			//创建屏幕句柄
			/*Graphics grpWindow =this.CreateGraphics();

			//创建一幅保存图像
			Bitmap bitmap = new Bitmap((int)grpWindow.VisibleClipBounds.Width, (int)grpWindow.VisibleClipBounds.Height, grpWindow);
				
			//创建bitmap相关的Grp类
			Graphics grpBitmap = Graphics.FromImage(bitmap);
			
			//窗口上下文
			IntPtr hdcWindow = grpWindow.GetHdc();
			
			//图片的上下文
			IntPtr hdcBitmap = grpBitmap.GetHdc();
			
			//拷贝
			Gdi32.BitBlt(hdcBitmap, 0, 0, bitmap.Width, bitmap.Height, hdcWindow, 0, 0, 0x00CC0020);
			
			//释放关联grpBitmap句柄
			grpBitmap.ReleaseHdc(hdcBitmap);
			
			//释放关联grpWindow句柄
			grpWindow.ReleaseHdc(hdcWindow);

			//释放grpBitmap对象
			grpBitmap.Dispose();

			//释放grpWindow对象
			grpWindow.Dispose();*/

			Bitmap myImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width, 
 
				Screen.PrimaryScreen.Bounds.Height); 
 
			Graphics gr1 = Graphics.FromImage(myImage); 
 
			IntPtr dc1 = gr1.GetHdc(); 
 
			IntPtr dc2 = User32.GetWindowDC(User32.GetDesktopWindow()); 
 
			Gdi32.BitBlt(dc1, 0, 0, Screen.PrimaryScreen.Bounds.Width, 
 
				Screen.PrimaryScreen.Bounds.Height, dc2, 0, 0, 0x00CC0020); 
 
			gr1.ReleaseHdc(dc1);  

			gr1.Dispose();


			//返回图像
			return myImage;			
		}
		/// <summary>
		/// 设置鼠标方案
		/// </summary>
		private void SetCursor()
		{
			Cursor cr = Cursors.Default;
			
			if(index == 1 || index == 5)
			{
				cr = Cursors.SizeNWSE;
			}
			else if(index == 2 || index == 6)
			{
				cr = Cursors.SizeNS;
			}
			else if(index == 3 || index == 7)
			{
				cr = Cursors.SizeNESW;
			}
			else if(index == 4 || index == 8)
			{
				cr = Cursors.SizeWE;
			}
			else if(index == 0)
			{
				cr = Cursors.SizeAll;
			}
			Cursor.Current = cr;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//  base.OnPaint (e);
//			base.OnPaint(e);
			e.Graphics.DrawRectangle(new Pen(Color.Black),this.area);
   
			for(int i = 1;i < 9;i++)
			{
				e.Graphics.FillRectangle(new SolidBrush(Color.Red),this.GetHandleRect(i));
			}
		}

		private Rectangle GetHandleRect(int index)
		{
			Point point = GetHandle(index);
			return new Rectangle(point.X - 3, point.Y - 3, 7, 7);
		}

		private  Point GetHandle(int index)
		{
			int x, y, xCenter, yCenter;

			xCenter = area.X + area.Width/2;
			yCenter = area.Y + area.Height/2;
			x = area.X;
			y = area.Y;

			switch ( index )
			{
				case 1:
					x = area.X;
					y = area.Y;
					break;
				case 2:
					x = xCenter;
					y = area.Y;
					break;
				case 3:
					x = area.Right;
					y = area.Y;
					break;
				case 4:
					x = area.Right;
					y = yCenter;
					break;
				case 5:
					x = area.Right;
					y = area.Bottom;
					break;
				case 6:
					x = xCenter;
					y = area.Bottom;
					break;
				case 7:
					x = area.X;
					y = area.Bottom;
					break;
				case 8:
					x = area.X;
					y = yCenter;
					break;
			}

			return new Point(x, y);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp (e);
			int left = area.Left;
			int top = area.Top;
			int right = area.Right;
			int bottom = area.Bottom;
			area.X = Math.Min(left,right);
			area.Y = Math.Min(top,bottom);
			area.Width = Math.Abs(left-right);
			area.Height = Math.Abs(top-bottom);
			if(e.Button == MouseButtons.Right)
			{
				if(this.area == Rectangle.Empty)
				{
					this.DialogResult=DialogResult.Cancel;
				}
				else
				{
					this.area=Rectangle.Empty;
					this.Invalidate();
				}
			}
			this.index = this.GetSelectedHandle(new Point(e.X,e.Y));
			this.SetCursor();
		}

		private int GetSelectedHandle(Point p)
		{
			int index = -1;
			for(int i = 1;i < 9;i++)
			{
				if(GetHandleRect(i).Contains(p))
				{
					index=i;
					break;
				}
			}
			if(this.area.Contains(p))index = 0;
			System.Diagnostics.Trace.WriteLine(area.ToString());
			System.Diagnostics.Trace.WriteLine(p.ToString());
			System.Diagnostics.Trace.WriteLine(index.ToString());
			return index;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove (e);
			if(this.Capture)
			{
				this.MoveHandleTo(new Point(e.X,e.Y));
				this.Invalidate();
			}
			else
			{
				this.index = this.GetSelectedHandle(new Point(e.X,e.Y));
				this.SetCursor();
			}
		}

		private  void MoveHandleTo(Point point)
		{
			int left = area.Left;
			int top = area.Top;
			int right = area.Right;
			int bottom = area.Bottom;

			switch ( index )
			{
				case 0:
					area.X +=point.X - this.pot.X;
					area.Y += point.Y - pot.Y;
					this.pot = point;
					return;
				case 1:
					left = point.X;
					top = point.Y;
					break;
				case 2:
					top = point.Y;
					break;
				case 3:
					right = point.X;
					top = point.Y;
					break;
				case 4:
					right = point.X;
					break;
				case 5:
					right = point.X;
					bottom = point.Y;
					break;
				case 6:
					bottom = point.Y;
					break;
				case 7:
					left = point.X;
					bottom = point.Y;
					break;
				case 8:
					left = point.X;
					break;
			}
			this.pot = point;
			area.X = left;
			area.Y = top;
			area.Width = right - left;
			area.Height = bottom - top;
		}

		private void picForm_DoubleClick(object sender, System.EventArgs e)
		{
			Bitmap bm = new Bitmap(this.BackgroundImage);
            if(this.area.Width>0 && this.area.Height>0){
			this.img = bm.Clone(this.area,System.Drawing.Imaging.PixelFormat.Format16bppArgb1555);
			if(saveFile)
			{
				saveFileDialog1.Filter="jpg文件 (*.jpg)|*.jpg|png文件 (*.png)|*.png|gif文件 (*.gif)|*.gif|tif文件 (*.tif)|*.tif";
				saveFileDialog1.ShowDialog();
				string fname=saveFileDialog1.FileName;
				if(fname!="")
				{
                    if (img.Width > 0 && img.Height > 0)
                    {
                        string type = fname.Substring(fname.LastIndexOf(".") + 1).ToLower();
                        switch (type)
                        {
                            case "bmp":
                                img.Save(fname, ImageFormat.Bmp);
                                break;
                            case "jpg":
                                img.Save(fname, ImageFormat.Jpeg);
                                break;
                            case "png":
                                img.Save(fname, ImageFormat.Png);
                                break;
                            case "gif":
                                img.Save(fname, ImageFormat.Gif);
                                break;
                            case "tif":
                                img.Save(fname, ImageFormat.Tiff);
                                break;
                        }
                    }
				}
			}
            }
			this.Close();
		}

		private void picForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyData == Keys.Escape)
			{
				this.Close();
			}
		}
	}
}
