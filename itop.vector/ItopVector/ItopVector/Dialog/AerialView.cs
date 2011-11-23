using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using ItopVector.Core.Figure;

namespace ItopVector.Dialog
{
	public class AerialView : Form
	{
		// Methods
		public int type = 0;
		private ItopVectorControl tl1;

		public AerialView(DrawArea.DrawArea vcontrol,ItopVectorControl tlVector)
		{
			
			this.components = null;
			tl1=tlVector;
			this.vectorControl = vcontrol;
			this.pageSetting = vcontrol.PageSettings;
			this.pageSetupdlg = new PageSetupDialog();
			this.pageSetupdlg.PageSettings = this.pageSetting;
			this.margin = 0;
			this.printdoc = new PrintDocument();
			this.pos = Point.Empty;
			this.scalex = 1f;
			this.scaley = 1f;
			this.startPoint = Point.Empty;
			this.oriPoint = Point.Empty;
			this.InitializeComponent();

			this.printdoc.PrintPage += new PrintPageEventHandler(this.printdoc_PrintPage);

			this.label8.Cursor = SpecialCursors.handCurosr;
			this.CalculateScale();
			
		}

		private void CalculateScale()
		{
			RectangleF rectangle1 = this.pageSetting.Bounds;
			rectangle1.Width *= .9f;
			rectangle1.Height *= .9f;

			int num1 = this.label8.Width;
			int num2 = this.label8.Height;
			this.scaley = ((float) (num2 - (2*this.margin)))/((float) rectangle1.Height);
			this.scalex = ((float) (num1 - (2*this.margin)))/((float) rectangle1.Width);
			Margins margins1 = this.pageSetting.Margins.Clone() as Margins;
			margins1.Left = (int) (margins1.Left*.9f);
			margins1.Right = (int) (margins1.Right*.9f);
			margins1.Top = (int) (margins1.Top*.9f);
			margins1.Bottom = (int) (margins1.Bottom*.9f);
//			margins1.Left = (int)(margins1.Left);
//			margins1.Right = (int)(margins1.Right);
//			margins1.Top = (int)(margins1.Top);
//			margins1.Bottom = (int)(margins1.Bottom);

			int num3 = this.margin + ((int) (this.scalex*margins1.Left));
			int num4 = this.margin + ((int) (this.scaley*margins1.Top));
			num1 = (int) (((this.scalex*rectangle1.Width) - (this.scalex*margins1.Left)) - (this.scalex*margins1.Right));
			num2 = (int) (((this.scaley*rectangle1.Height) - (this.scaley*margins1.Top)) - (this.scaley*margins1.Bottom));
			Rectangle rectangle2 = new Rectangle(num3, num4, num1, num2);
			SizeF size1 = this.vectorControl.DocumentSize;

//			if (this.radioFit.Checked)
//			{
			this.scalex = ((float) rectangle2.Width)/size1.Width;
			this.scaley = ((float) rectangle2.Height)/size1.Height;
//			}

		}


		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.label8 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label8
			// 
			this.label8.BackColor = System.Drawing.SystemColors.ControlDark;
			this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label8.Location = new System.Drawing.Point(0, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(240, 320);
			this.label8.TabIndex = 4;
			this.label8.Paint += new System.Windows.Forms.PaintEventHandler(this.label8_Paint);
			this.label8.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label8_MouseUp);
			this.label8.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label8_MouseMove);
			this.label8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label8_MouseDown);
			// 
			// AerialView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(234, 319);
			this.Controls.Add(this.label8);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AerialView";
			this.ShowInTaskbar = false;
			this.Text = "ƒÒ¿¿Õº";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.AerialView_Closing);
			this.Load += new System.EventHandler(this.AerialView_Load);
			this.ResumeLayout(false);

		}

		private void label8_MouseDown(object sender, MouseEventArgs e)
		{
			
			this.startPoint = Point.Empty;
			try
			{
				if (e.Button == MouseButtons.Left)
				{

					tl1.top = e.Y*(tl1.DrawArea.ViewSize.Height/320);
					tl1.left = e.X*(tl1.DrawArea.ViewSize.Width/240);
					
					if (loadData != null)
					{
						loadData();
					}
				}
			}
			catch
			{
				type = 1;
				//this.Close();
			}
		}

		private void label8_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.Button == MouseButtons.Left)
				{
					//this.oriPoint = this.pos;
//					tl1 = this.Owner.ActiveControl as ItopVectorControl;
					tl1.top = e.Y*(tl1.DrawArea.ViewSize.Height/320);
					tl1.left = e.X*(tl1.DrawArea.ViewSize.Width/240);
					if (loadData != null)
					{
						loadData();
					}
				}
			}
			catch
			{
				type = 1;
				//this.Close();
			}
		}

		private void label8_Paint(object sender, PaintEventArgs e)
		{
			Label label1 = sender as Label;
			RectangleF rectangle1 = this.pageSetting.Bounds;
			rectangle1.Width *= .9f;
			rectangle1.Height *= .9f;
			int num1 = label1.Width;
			float single1 = ((float) (label1.Height - (2*this.margin)))/((float) rectangle1.Height); //yÀı∑≈
			float single2 = ((float) (num1 - (2*this.margin)))/((float) rectangle1.Width); //xÀı∑≈
			Rectangle rectangle2 = new Rectangle(this.margin, this.margin, (int) (single2*rectangle1.Width), (int) (single1*rectangle1.Height));
			e.Graphics.FillRectangle(Brushes.White, rectangle2);
			e.Graphics.DrawRectangle(Pens.Black, rectangle2);

			Margins margins1 = this.pageSetting.Margins.Clone() as Margins;
			margins1.Left = (int) (margins1.Left*.9f); 
			margins1.Right = (int) (margins1.Right*.9f);
			margins1.Top = (int) (margins1.Top*.9f);
			margins1.Bottom = (int) (margins1.Bottom*.9f);

			int num3 = this.margin + ((int) (single2*margins1.Left));
			int num4 = this.margin + ((int) (single1*margins1.Top));
			num1 = (int) (((single2*rectangle1.Width) - (single2*margins1.Left)) - (single2*margins1.Right));
			int num2 = (int) (((single1*rectangle1.Height) - (single1*margins1.Top)) - (single1*margins1.Bottom));
			rectangle2 = new Rectangle(num3, num4, num1, num2);
			using (Pen pen1 = new Pen(Color.Blue, 1f))
			{
				e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

				SizeF size1 = this.vectorControl.DocumentSize;

				PointF offset;
//				if (this.radioFit.Checked)
//				{					
				offset = new PointF(rectangle2.X, rectangle2.Y);
//				}


				e.Graphics.TranslateTransform((float) -this.pos.X, (float) -this.pos.Y);

				e.Graphics.TranslateTransform(offset.X, offset.Y);

				e.Graphics.ScaleTransform(this.scalex, this.scaley);

				this.RenderTo(e.Graphics);

				e.Graphics.ResetTransform();
				e.Graphics.ResetClip();
				float[] singleArray1 = new float[] {2f, 2f};
				pen1.DashPattern = singleArray1;
				pen1.Color = Color.Black;

				e.Graphics.DrawRectangle(pen1, (float) (-this.pos.X + offset.X), (float) (-this.pos.Y + offset.Y), (float) (size1.Width*this.scalex), (float) (size1.Height*this.scaley));

				pen1.DashStyle = DashStyle.Solid;
				pen1.Color = Color.Blue;
				e.Graphics.DrawRectangle(pen1, rectangle2);
			}
		}

		private void printdoc_PrintPage(object sender, PrintPageEventArgs e)
		{
			Rectangle rectangle1 = this.pageSetting.Bounds;
			float single1 = 1f;
			Margins margins1 = this.pageSetting.Margins.Clone() as Margins;
			margins1.Left = (int) (margins1.Left*.9f);
			margins1.Right = (int) (margins1.Right*.9f);
			margins1.Top = (int) (margins1.Top*.9f);
			margins1.Bottom = (int) (margins1.Bottom*.9f);

//			SizeF size1 = this.vectorControl.DocumentSize;

			Rectangle rectangle2 = rectangle1;
			rectangle2.Offset(margins1.Left, margins1.Top);
			rectangle2.Width -= rectangle2.X + margins1.Right;
			rectangle2.Height -= rectangle2.Y + margins1.Bottom;

			float single2 = ((float) (this.label8.Height - (2*this.margin)))/((float) rectangle1.Height);
			float single3 = ((float) (this.label8.Width - (2*this.margin)))/((float) rectangle1.Width);
			int num1 = (int) (single1*margins1.Left);
			int num2 = (int) (single1*margins1.Top);


			e.Graphics.SetClip(rectangle2);

			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

			e.Graphics.TranslateTransform(((float) -this.pos.X)/single3, ((float) -this.pos.Y)/single2);
//			if (this.radioFit.Checked)
//			{	
			e.Graphics.TranslateTransform((float) num1, (float) num2); //“≥±ﬂæ‡
//			}            			

			e.Graphics.ScaleTransform(this.scalex/single3, this.scaley/single2);

			this.RenderTo(e.Graphics);
		}

		private void RenderTo(Graphics g)
		{
			Matrix matrix1 = new Matrix();
			Matrix matrix2 = new Matrix();
			matrix1 = ((SVG) this.vectorControl.SVGDocument.RootElement).GraphTransform.Matrix;
			matrix2.Multiply(matrix1);
			matrix1.Reset();
			matrix1.Multiply(g.Transform);
			g.ResetTransform();
			try
			{
				this.vectorControl.RenderTo(g);
			}
			finally
			{
				g.Transform = matrix1.Clone();
				matrix1.Reset();
				matrix1.Multiply(matrix2);
			}
		}


		public delegate void loadDateDelegate();

		public event loadDateDelegate loadData;
		private Container components;
		private Label label8;
		private int margin;
		private Point oriPoint;
		private PageSettings pageSetting;
		private Point pos;
		private PrintDocument printdoc;
		private float scalex;
		private float scaley;
		private Point startPoint;
		private DrawArea.DrawArea vectorControl;

		private PageSetupDialog pageSetupdlg;

		private void AerialView_Load(object sender, EventArgs e)
		{

		}

		private void AerialView_Closing(object sender, CancelEventArgs e)
		{
			ItopVectorControl.isOpen = 0;
		}

		private void label8_MouseUp(object sender, MouseEventArgs e)
		{
			if (type == 1)
			{
				this.Close();
			}
		}

		private void pictureBox1_Click(object sender, System.EventArgs e)
		{
		
		}

		private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		
		}
	}
}