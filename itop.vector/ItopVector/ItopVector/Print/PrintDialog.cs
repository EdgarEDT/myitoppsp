namespace ItopVector.Dialog
{
	using ItopVector;
	using ItopVector.DrawArea;
	using ItopVector.Core.Figure;
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Drawing.Printing;
	using System.Windows.Forms;

	internal class PrintDialog : Form
	{
		// Methods
		internal PrintDialog(ItopVector.DrawArea.DrawArea vcontrol)
		{
			this.components = null;
			this.vectorControl = vcontrol;
			this.pageSetting =  vcontrol.PageSettings;
			this.pageSetupdlg=new PageSetupDialog();
			this.pageSetupdlg.PageSettings=this.pageSetting;
			this.margin = 10;
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

		private void btnPrint_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.PrintDialog dialog1 = new System.Windows.Forms.PrintDialog();
			this.printdoc.DefaultPageSettings = this.pageSetting;
			dialog1.Document = this.printdoc;
			if (dialog1.ShowDialog(this) == DialogResult.OK)
			{
				try
				{
					this.printdoc.Print();
				}
				catch(Exception err)
				{
					MessageBox.Show(err.Message,"打印失败");
					return;
				}				

				base.Close();
			}
		}

		private void btnSe0Up_Click(object sender, EventArgs e)
		{
			
			if (pageSetupdlg.ShowDialog(this) == DialogResult.OK)
			{
//				this.pageSetting = pageSetupdlg.PageSettings.Clone() as PageSettings;
				CalculateScale();
				this.label8.Invalidate();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			PrintPreviewDialog dialog1 = new PrintPreviewDialog();
			this.printdoc.DefaultPageSettings = this.pageSetting;
			dialog1.Document = this.printdoc;
			dialog1.ShowDialog(this);
		}

		private void CalculateScale()
		{
			RectangleF rectangle1;
//			if(vectorControl.FullDrawMode==true)
//			{
//				
//				 rectangle1 = vectorControl.Bounds;
//			}
//			else
//			{
				 rectangle1 = this.pageSetting.Bounds; 
//			}
			rectangle1.Width*=.9f;
			rectangle1.Height*=.9f;

			int num1 = this.label8.Width;
			int num2 = this.label8.Height;
			this.scaley = ((float) (num2 - (2 * this.margin))) / ((float) rectangle1.Height);
			this.scalex = ((float) (num1 - (2 * this.margin))) / ((float) rectangle1.Width);
			Margins margins1 = this.pageSetting.Margins.Clone() as Margins;
			margins1.Left = (int)(margins1.Left*.9f);
			margins1.Right = (int)(margins1.Right*.9f);
			margins1.Top = (int)(margins1.Top*.9f);
			margins1.Bottom = (int)(margins1.Bottom*.9f);

			int num3 = this.margin + ((int) (this.scalex * margins1.Left));
			int num4 = this.margin + ((int) (this.scaley * margins1.Top));
			num1 = (int) (((this.scalex * rectangle1.Width) - (this.scalex * margins1.Left)) - (this.scalex * margins1.Right));
			num2 = (int) (((this.scaley * rectangle1.Height) - (this.scaley * margins1.Top)) - (this.scaley * margins1.Bottom));
			Rectangle rectangle2 = new Rectangle(num3, num4, num1, num2);
			SizeF size1 = this.vectorControl.DocumentSize;

			if (this.radioFit.Checked)
			{
				this.scalex = ((float) rectangle2.Width) / ((float) size1.Width);
				this.scaley = ((float) rectangle2.Height) / ((float) size1.Height);
			}
			else if (this.radioCustom.Checked)
			{
				this.scalex *= (((float) this.updownWidth.Value) / 100f);
				this.scaley *= (((float) this.updownHeight.Value) / 100f);
			}
		}

		private void chkLock_CheckedChanged(object sender, EventArgs e)
		{
			this.updownHeight.Value = this.updownWidth.Value;
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.chkLock = new System.Windows.Forms.CheckBox();
			this.updownHeight = new System.Windows.Forms.NumericUpDown();
			this.updownWidth = new System.Windows.Forms.NumericUpDown();
			this.radioCustom = new System.Windows.Forms.RadioButton();
			this.radioFit = new System.Windows.Forms.RadioButton();
			this.radioNoSale = new System.Windows.Forms.RadioButton();
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnSe0Up = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rdoHighSpeed = new System.Windows.Forms.RadioButton();
			this.rdoHighQuality = new System.Windows.Forms.RadioButton();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.updownHeight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updownWidth)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label8
			// 
			this.label8.BackColor = System.Drawing.SystemColors.ControlDark;
			this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label8.Location = new System.Drawing.Point(8, 8);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(240, 320);
			this.label8.TabIndex = 4;
			this.label8.Paint += new System.Windows.Forms.PaintEventHandler(this.label8_Paint);
			this.label8.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label8_MouseMove);
			this.label8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label8_MouseDown);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.chkLock);
			this.groupBox2.Controls.Add(this.updownHeight);
			this.groupBox2.Controls.Add(this.updownWidth);
			this.groupBox2.Controls.Add(this.radioCustom);
			this.groupBox2.Controls.Add(this.radioFit);
			this.groupBox2.Controls.Add(this.radioNoSale);
			this.groupBox2.Location = new System.Drawing.Point(256, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(304, 120);
			this.groupBox2.TabIndex = 8;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "缩 放";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(104, 88);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(16, 23);
			this.label7.TabIndex = 7;
			this.label7.Text = "W";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(176, 88);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(16, 23);
			this.label6.TabIndex = 6;
			this.label6.Text = "H";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkLock
			// 
			this.chkLock.Checked = true;
			this.chkLock.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLock.Enabled = false;
			this.chkLock.Location = new System.Drawing.Point(248, 88);
			this.chkLock.Name = "chkLock";
			this.chkLock.Size = new System.Drawing.Size(48, 24);
			this.chkLock.TabIndex = 5;
			this.chkLock.Text = "锁定";
			this.chkLock.CheckedChanged += new System.EventHandler(this.chkLock_CheckedChanged);
			// 
			// updownHeight
			// 
			this.updownHeight.Enabled = false;
			this.updownHeight.Location = new System.Drawing.Point(192, 88);
			this.updownHeight.Maximum = new System.Decimal(new int[] {
																		 1000,
																		 0,
																		 0,
																		 0});
			this.updownHeight.Minimum = new System.Decimal(new int[] {
																		 2,
																		 0,
																		 0,
																		 0});
			this.updownHeight.Name = "updownHeight";
			this.updownHeight.Size = new System.Drawing.Size(48, 21);
			this.updownHeight.TabIndex = 4;
			this.updownHeight.Value = new System.Decimal(new int[] {
																	   100,
																	   0,
																	   0,
																	   0});
			this.updownHeight.ValueChanged += new System.EventHandler(this.updownHeight_ValueChanged);
			// 
			// updownWidth
			// 
			this.updownWidth.Enabled = false;
			this.updownWidth.Location = new System.Drawing.Point(120, 88);
			this.updownWidth.Maximum = new System.Decimal(new int[] {
																		1000,
																		0,
																		0,
																		0});
			this.updownWidth.Minimum = new System.Decimal(new int[] {
																		2,
																		0,
																		0,
																		0});
			this.updownWidth.Name = "updownWidth";
			this.updownWidth.Size = new System.Drawing.Size(48, 21);
			this.updownWidth.TabIndex = 3;
			this.updownWidth.Value = new System.Decimal(new int[] {
																	  100,
																	  0,
																	  0,
																	  0});
			this.updownWidth.ValueChanged += new System.EventHandler(this.updownHeight_ValueChanged);
			// 
			// radioCustom
			// 
			this.radioCustom.Location = new System.Drawing.Point(16, 88);
			this.radioCustom.Name = "radioCustom";
			this.radioCustom.Size = new System.Drawing.Size(64, 24);
			this.radioCustom.TabIndex = 2;
			this.radioCustom.Text = "自定义";
			this.radioCustom.CheckedChanged += new System.EventHandler(this.radioCustom_CheckedChanged);
			// 
			// radioFit
			// 
			this.radioFit.Location = new System.Drawing.Point(16, 56);
			this.radioFit.Name = "radioFit";
			this.radioFit.TabIndex = 1;
			this.radioFit.Text = "适应页面";
			this.radioFit.CheckedChanged += new System.EventHandler(this.radioCustom_CheckedChanged);
			// 
			// radioNoSale
			// 
			this.radioNoSale.Checked = true;
			this.radioNoSale.Location = new System.Drawing.Point(16, 24);
			this.radioNoSale.Name = "radioNoSale";
			this.radioNoSale.TabIndex = 0;
			this.radioNoSale.TabStop = true;
			this.radioNoSale.Text = "不缩放";
			this.radioNoSale.CheckedChanged += new System.EventHandler(this.radioCustom_CheckedChanged);
			// 
			// btnPrint
			// 
			this.btnPrint.Location = new System.Drawing.Point(376, 296);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(80, 24);
			this.btnPrint.TabIndex = 9;
			this.btnPrint.Text = "打印";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnSe0Up
			// 
			this.btnSe0Up.Location = new System.Drawing.Point(272, 296);
			this.btnSe0Up.Name = "btnSe0Up";
			this.btnSe0Up.Size = new System.Drawing.Size(80, 24);
			this.btnSe0Up.TabIndex = 10;
			this.btnSe0Up.Text = "打印设置";
			this.btnSe0Up.Visible = false;
			this.btnSe0Up.Click += new System.EventHandler(this.btnSe0Up_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rdoHighSpeed);
			this.groupBox1.Controls.Add(this.rdoHighQuality);
			this.groupBox1.Location = new System.Drawing.Point(256, 152);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(304, 100);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "质量";
			// 
			// rdoHighSpeed
			// 
			this.rdoHighSpeed.Location = new System.Drawing.Point(16, 64);
			this.rdoHighSpeed.Name = "rdoHighSpeed";
			this.rdoHighSpeed.TabIndex = 1;
			this.rdoHighSpeed.Text = "低质量";
			this.rdoHighSpeed.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// rdoHighQuality
			// 
			this.rdoHighQuality.Checked = true;
			this.rdoHighQuality.Location = new System.Drawing.Point(16, 24);
			this.rdoHighQuality.Name = "rdoHighQuality";
			this.rdoHighQuality.TabIndex = 0;
			this.rdoHighQuality.TabStop = true;
			this.rdoHighQuality.Text = "高质量";
			this.rdoHighQuality.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(272, 256);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 24);
			this.button1.TabIndex = 12;
			this.button1.Text = "预览";
			this.button1.Visible = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(480, 296);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(80, 24);
			this.button2.TabIndex = 13;
			this.button2.Text = "关闭";
			// 
			// PrintDialog
			// 
			this.AcceptButton = this.btnPrint;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(570, 335);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnSe0Up);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label8);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PrintDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "打印";
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.updownHeight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updownWidth)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		private void label8_MouseDown(object sender, MouseEventArgs e)
		{
			this.startPoint = Point.Empty;
			if (e.Button == MouseButtons.Left)
			{
				this.startPoint = new Point(e.X, e.Y);
			}
			this.oriPoint = this.pos;
		}

		private void label8_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				int num1 = this.oriPoint.X;
				int num2 = this.oriPoint.Y;
				int num3 = e.X - this.startPoint.X;
				int num4 = e.Y - this.startPoint.Y;
				num1 -= num3;
				num2 -= num4;
				Point point1 = new Point(num1, num2);
				if (point1 != this.pos)
				{
					this.pos = point1;
					this.label8.Invalidate();
				}
			}
		}

		private void label8_Paint(object sender, PaintEventArgs e)
		{
			RectangleF rectangle1;
			Label label1 = sender as Label;
			
//			if(vectorControl.FullDrawMode==true)
//			{
//				rectangle1 = vectorControl.Bounds;
//			}
//			else
//			{
				rectangle1 = this.pageSetting.Bounds;
//			}
			rectangle1.Width*=.9f;
			rectangle1.Height*=.9f;
			int num1 = label1.Width;
			float single1 = ((float) (label1.Height - (2 * this.margin))) / ((float) rectangle1.Height);//y缩放
			float single2 = ((float) (num1 - (2 * this.margin))) / ((float) rectangle1.Width);//x缩放
			Rectangle rectangle2 = new Rectangle(this.margin, this.margin, (int) (single2 * rectangle1.Width), (int) (single1 * rectangle1.Height));
			e.Graphics.FillRectangle(Brushes.White, rectangle2);
			e.Graphics.DrawRectangle(Pens.Black, rectangle2);

			Margins margins1 = this.pageSetting.Margins.Clone() as Margins;
			margins1.Left = (int)(margins1.Left*.9f);
			margins1.Right = (int)(margins1.Right*.9f);
			margins1.Top = (int)(margins1.Top*.9f);
			margins1.Bottom = (int)(margins1.Bottom*.9f);

			int num3 = this.margin + ((int) (single2 * margins1.Left));
			int num4 = this.margin + ((int) (single1 * margins1.Top));
			num1 = (int) (((single2 * rectangle1.Width) - (single2 * margins1.Left)) - (single2 * margins1.Right));
			int num2 = (int) (((single1 * rectangle1.Height) - (single1 * margins1.Top)) - (single1 * margins1.Bottom));
			rectangle2 = new Rectangle(num3, num4, num1, num2);
			using (Pen pen1 = new Pen(Color.Blue, 1f))
			{
				e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
				if (this.rdoHighSpeed.Checked)
				{
					e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
				}
				SizeF size1 = this.vectorControl.DocumentSize;

				PointF offset ;
				if (this.radioFit.Checked)
				{					
					offset=new PointF(rectangle2.X,rectangle2.Y);                    
				}
				else
				{
					offset=new PointF(this.margin,this.margin);
				}

				e.Graphics.TranslateTransform((float) -this.pos.X, (float) -this.pos.Y);

				e.Graphics.TranslateTransform(offset.X, offset.Y);

				e.Graphics.ScaleTransform(this.scalex, this.scaley);

				this.RenderTo(e.Graphics);

				e.Graphics.ResetTransform();
				e.Graphics.ResetClip();
				float[] singleArray1 = new float[] { 2f, 2f } ;
				pen1.DashPattern = singleArray1;
				pen1.Color = Color.Black;
				
				e.Graphics.DrawRectangle(pen1, (float) (-this.pos.X+offset.X), (float) (-this.pos.Y+offset.Y), (float) (size1.Width * this.scalex), (float) (size1.Height * this.scaley));
				
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
			margins1.Left = (int)(margins1.Left*.9f);
			margins1.Right = (int)(margins1.Right*.9f);
			margins1.Top = (int)(margins1.Top*.9f);
			margins1.Bottom = (int)(margins1.Bottom*.9f);

			SizeF size1 = this.vectorControl.DocumentSize;

			Rectangle rectangle2=rectangle1;
			rectangle2.Offset(margins1.Left,margins1.Top);
			rectangle2.Width -=rectangle2.X + margins1.Right;
			rectangle2.Height -=rectangle2.Y + margins1.Bottom;			

			float single2 = ((float) (this.label8.Height - (2 * this.margin))) / ((float) rectangle1.Height);
			float single3 = ((float) (this.label8.Width - (2 * this.margin))) / ((float) rectangle1.Width);
			int num1 = (int) (single1 * margins1.Left);
			int num2 = (int) (single1 * margins1.Top);

			
			e.Graphics.SetClip(rectangle2);

			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			if (this.rdoHighSpeed.Checked)
			{
				e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
			}
			e.Graphics.TranslateTransform(((float) -this.pos.X) / single3, ((float) -this.pos.Y) / single2);
			if (this.radioFit.Checked)
			{	
				e.Graphics.TranslateTransform((float) num1, (float) num2);//页边距
			}            			

			e.Graphics.ScaleTransform(this.scalex / single3, this.scaley / single2);

			this.RenderTo(e.Graphics);
		}
		private void RenderTo(Graphics g)
		{
			Matrix matrix1=new Matrix();
			Matrix matrix2=new Matrix();			
			matrix1=((SVG)this.vectorControl.SVGDocument.RootElement).GraphTransform.Matrix;
			matrix2.Multiply(matrix1);
			matrix1.Reset();
			matrix1.Multiply(g.Transform);
			g.ResetTransform();
			try
			{
				this.vectorControl.SVGDocument.BeginPrint =true;

				this.vectorControl.RenderTo(g);
			}
			finally
			{
				this.vectorControl.SVGDocument.BeginPrint =false;
				g.Transform=matrix1.Clone();
				matrix1.Reset();
				matrix1.Multiply(matrix2);
			}
		}


		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			this.label8.Invalidate();
		}

		private void radioCustom_CheckedChanged(object sender, EventArgs e)
		{
			bool flag1;
			this.chkLock.Enabled = flag1 = this.radioCustom.Checked;
			this.updownHeight.Enabled = flag1 = flag1;
			this.updownWidth.Enabled = flag1;
			this.CalculateScale();
			this.label8.Invalidate();
		}

		private void updownHeight_ValueChanged(object sender, EventArgs e)
		{
			if (this.chkLock.Checked)
			{
				decimal num1;
				this.updownWidth.Value = num1 = (sender as NumericUpDown).Value;
				this.updownHeight.Value = num1;
			}
			this.CalculateScale();
			this.label8.Invalidate();
		}


		// Fields
		private Button btnPrint;
		private Button btnSe0Up;
		private Button button1;
		private Button button2;
		private CheckBox chkLock;
		private Container components;
		private GroupBox groupBox1;
		private GroupBox groupBox2;
		private Label label6;
		private Label label7;
		private Label label8;
		private int margin;
		private Point oriPoint;
		private PageSettings pageSetting;
		private Point pos;
		private PrintDocument printdoc;
		private RadioButton radioCustom;
		private RadioButton radioFit;
		private RadioButton radioNoSale;
		private RadioButton rdoHighQuality;
		private RadioButton rdoHighSpeed;
		private float scalex;
		private float scaley;
		private Point startPoint;
		private NumericUpDown updownHeight;
		private NumericUpDown updownWidth;
		private ItopVector.DrawArea.DrawArea vectorControl;
		private PageSetupDialog pageSetupdlg;
	}
}

