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
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class picForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// ����������������
		/// </summary>
		//
		//�Զ������
		//
		private Point pot;
		private Rectangle area = Rectangle.Empty;
		public Image img;
		private int index = -1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private bool saveFile;
		//
		//ϵͳ���ɶ���
		//
		private System.ComponentModel.Container components = null;

		public picForm(bool save)
		{
			this.Bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
			this.BackgroundImage =CaptureDesktopWindow();
			//			this.BackgroundImage=bmp;
			
			//
			// Windows ���������֧���������
			//
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true); // ��ֹ��������.
			SetStyle(ControlStyles.DoubleBuffer, true); // ˫����
			saveFile=save;
			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
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
		/// ����MouseDown
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
			//������Ļ���
			/*Graphics grpWindow =this.CreateGraphics();

			//����һ������ͼ��
			Bitmap bitmap = new Bitmap((int)grpWindow.VisibleClipBounds.Width, (int)grpWindow.VisibleClipBounds.Height, grpWindow);
				
			//����bitmap��ص�Grp��
			Graphics grpBitmap = Graphics.FromImage(bitmap);
			
			//����������
			IntPtr hdcWindow = grpWindow.GetHdc();
			
			//ͼƬ��������
			IntPtr hdcBitmap = grpBitmap.GetHdc();
			
			//����
			Gdi32.BitBlt(hdcBitmap, 0, 0, bitmap.Width, bitmap.Height, hdcWindow, 0, 0, 0x00CC0020);
			
			//�ͷŹ���grpBitmap���
			grpBitmap.ReleaseHdc(hdcBitmap);
			
			//�ͷŹ���grpWindow���
			grpWindow.ReleaseHdc(hdcWindow);

			//�ͷ�grpBitmap����
			grpBitmap.Dispose();

			//�ͷ�grpWindow����
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


			//����ͼ��
			return myImage;			
		}
		/// <summary>
		/// ������귽��
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
				saveFileDialog1.Filter="jpg�ļ� (*.jpg)|*.jpg|png�ļ� (*.png)|*.png|gif�ļ� (*.gif)|*.gif|tif�ļ� (*.tif)|*.tif";
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
