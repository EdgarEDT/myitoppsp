namespace ItopVector.Dialog
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;
	using ItopVector.Core.Figure;
	using System.Drawing.Imaging;
	using System.Reflection;


	internal class ExportImageDialog : Form
	{
		// Fields
		private Button button2;
		private RadioButton radioButton2;
		private Point pos;
		private SaveFileDialog savefiledlg1;
		private ItopVector.DrawArea.DrawArea vectorcontrol;
		private CheckBox chkTransparent;
		private GroupBox groupBox1;
		private CheckBox chkView;
		private RadioButton radioButton1;
		private ComboBox comboFormat;
		private CheckBox chkSelection;
		private Container components;
		private Point startPoint;
		private Label panel1;
		private RectangleF contectBounds;
		private Button button1;
		private Point oriPoint;
		private Label label1;
		// Methods 
		public ExportImageDialog(ItopVector.DrawArea.DrawArea vectorcontrol)
		{
			this.components = null; 
			this.savefiledlg1 = new SaveFileDialog();
			this.vectorcontrol = null;
			this.startPoint = Point.Empty;
			this.pos = Point.Empty;
			this.contectBounds = RectangleF.Empty;
			this.oriPoint = Point.Empty;
			this.InitializeComponent();
			base.SetStyle(ControlStyles.DoubleBuffer | (ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint), true);
			this.vectorcontrol = vectorcontrol;
			this.comboFormat.SelectedIndex = 0;
			this.panel1.Cursor = ItopVector.SpecialCursors.handCurosr;
		}

		private void panel1_mousemove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				int num1 = this.oriPoint.X;
				int num2 = this.oriPoint.Y;
				int num3 = e.X - this.startPoint.X;
				int num4 = e.Y - this.startPoint.Y;
				num1 -= num3;
				num2 -= num4;
				num1 = (int) Math.Max((float) 0f, Math.Min((float) num1, (float) (this.contectBounds.Width - this.panel1.Width)));
				num2 = (int) Math.Max((float) 0f, Math.Min((float) num2, (float) (this.contectBounds.Height - this.panel1.Height)));
				Point point1 = new Point(num1, num2);
				if (point1 != this.pos)
				{
					this.pos = point1;
					this.panel1.Invalidate();
				}
			}
		}

		/// <summary>
		/// 输出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_click(object sender, EventArgs e)
		{
			string text1 = this.comboFormat.Text.ToLower();
			string[] textArray1 = new string[] { text1, "文件(*.", text1, ")|*.", text1 } ;
			this.savefiledlg1.Filter = string.Concat(textArray1);
			if (this.savefiledlg1.ShowDialog(this) == DialogResult.OK)
			{
				try
				{
					string text2 = this.savefiledlg1.FileName;
					SizeF ef2 = this.vectorcontrol.ContentBounds.Size;
					SizeF ef1 = this.vectorcontrol.DocumentSize;
					ef1 =new SizeF(ef2.Width>ef1.Width?ef2.Width:ef1.Width,ef2.Height>ef1.Height?ef2.Height:ef1.Height);
					float single1 = ef1.Width +20;//+ 3700f;
					float single2 = ef1.Height+20;// + 700f;
					if (ef1.Width != ((int) ef1.Width))
					{
						single1 = ((int) single1) + 1;
					}
					if (ef1.Height != ((int) ef1.Height))
					{
						single2 = ((int) single2) + 1;
					}
					if ((single1 <= 0f) || (single2 <= 0f))
					{
						return;
					}
                    if (single1>10000)
                    {
                        single1 = 10000;
                    }
                    if (single2 > 10000)
                    {
                        single2 = 10000;
                    }
					using (Bitmap bitmap1 = new Bitmap((int) single1, (int) single2))
					{
						using (Graphics graphics1 = Graphics.FromImage(bitmap1))
						{
							if (this.radioButton1.Checked)
							{
								graphics1.SmoothingMode = SmoothingMode.HighQuality;
							}
							else
							{
								graphics1.SmoothingMode = SmoothingMode.HighSpeed;
							}
							graphics1.FillRectangle((this.chkTransparent.Visible && this.chkTransparent.Checked) ? Brushes.Transparent : Brushes.White, new RectangleF(0f, 0f, single1, single2));
							graphics1.TranslateTransform(-this.contectBounds.Left, -this.contectBounds.Top);
							this.RenderTo(graphics1);
						}
						ImageFormat format1=bitmap1.RawFormat;
						switch(text1.ToLower())
						{
							case "bmp":
								format1=ImageFormat.Bmp;
								break;
							case "jpg":
								format1=ImageFormat.Jpeg;
								break;
							case "gif":
								format1=ImageFormat.Gif;
								break;
							case "tiff":
								format1=ImageFormat.Tiff;
								break;
							case "png":
								format1=ImageFormat.Png;
								break;
							case "exif":
								format1=ImageFormat.Exif;
								break;
							case "ico":
								format1=ImageFormat.Icon;
								break;
							case "wmf":
								format1=ImageFormat.Wmf;
								break;
						}
						bitmap1.Save(text2, format1);
						
						MessageBox.Show("图片导出成功.","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
						bitmap1.Dispose();
						
					}
					
					base.Close();
				}
				catch (Exception exception1)
				{
					MessageBox.Show(exception1.Message);
				}
			}
		}
		private void RenderTo(Graphics g)
		{
			Matrix matrix1=new Matrix();
			Matrix matrix2=new Matrix();			
			matrix1=((SVG)this.vectorcontrol.SVGDocument.RootElement).GraphTransform.Matrix;
			matrix2.Multiply(matrix1);
			matrix1.Reset();
			matrix1.Multiply(g.Transform);
			g.ResetTransform();
			g.TranslateTransform(10,10);
			try
			{
				this.vectorcontrol.RenderTo(g);
			}
			finally
			{
				g.Transform=matrix1.Clone();
				matrix1.Reset();
				matrix1.Multiply(matrix2);
			}
		}

		private void panel1_mousedown(object sender, MouseEventArgs e)
		{
			this.startPoint = Point.Empty;
			if (e.Button == MouseButtons.Left)
			{
				this.startPoint = new Point(e.X, e.Y);
			}
			this.oriPoint = this.pos;
		}

		private void checkedChanged(object sender, EventArgs e)
		{
			this.panel1.Invalidate();
		}

		private void panel1_paint(object sender, PaintEventArgs e)
		{
			Graphics graphics1 = e.Graphics;
			if (this.radioButton1.Checked)
			{
				graphics1.SmoothingMode = SmoothingMode.HighQuality;
			}
			else
			{
				graphics1.SmoothingMode = SmoothingMode.HighSpeed;
			}
			e.Graphics.FillRectangle(Brushes.White, new RectangleF(0f, 0f, (float) base.Width, (float) base.Height));
			RectangleF ef1 = this.vectorcontrol.ContentBounds;
			if (!ef1.IsEmpty)
			{
				e.Graphics.TranslateTransform((float) -this.pos.X, (float) -this.pos.Y);
				int num1 = this.panel1.Width;
				int num2 = this.panel1.Height;
				int num3 = 0;
				int num4 = 0;
				num4 = (int) Math.Max((float) 0f, (float) ((num2 - ef1.Height) / 2f));
				num3 = (int) Math.Max((float) 0f, (float) ((num1 - ef1.Width) / 2f));
				e.Graphics.TranslateTransform(-ef1.Left + num3, -ef1.Top + num4);
				this.RenderTo(graphics1);
			}
		}

		private void comboFormat_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.chkTransparent.Visible = this.comboFormat.Text.ToLower() == "gif";
		}

		#region 析构
		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		#endregion
		#region 构造
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.comboFormat = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.chkSelection = new System.Windows.Forms.CheckBox();
			this.chkView = new System.Windows.Forms.CheckBox();
			this.chkTransparent = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.Color.DarkGray;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.panel1.Location = new System.Drawing.Point(8, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(0, 70);
			this.panel1.TabIndex = 0;
			this.panel1.Visible = false;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_paint);
			this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_mousemove);
			this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_mousedown);
			// 
			// button1
			// 
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.button1.Location = new System.Drawing.Point(8, 44);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(64, 26);
			this.button1.TabIndex = 1;
			this.button1.Text = "确定(&O)";
			this.button1.Click += new System.EventHandler(this.button1_click);
			// 
			// button2
			// 
			this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(88, 44);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(64, 26);
			this.button2.TabIndex = 2;
			this.button2.Text = "取消(&C)";
			// 
			// comboFormat
			// 
			this.comboFormat.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.comboFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboFormat.Items.AddRange(new object[] {
															 "Bmp",
															 "Jpg",
															 "Gif",
															 "Tiff",
															 "Png",
															 "Emf"});
			this.comboFormat.Location = new System.Drawing.Point(64, 12);
			this.comboFormat.Name = "comboFormat";
			this.comboFormat.Size = new System.Drawing.Size(88, 20);
			this.comboFormat.TabIndex = 4;
			this.comboFormat.SelectedIndexChanged += new System.EventHandler(this.comboFormat_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label1.Location = new System.Drawing.Point(8, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "格式";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Location = new System.Drawing.Point(14, 88);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(136, 80);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "质量";
			this.groupBox1.Visible = false;
			// 
			// radioButton2
			// 
			this.radioButton2.Location = new System.Drawing.Point(20, 48);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.TabIndex = 1;
			this.radioButton2.Text = "低质量";
			this.radioButton2.CheckedChanged += new System.EventHandler(this.checkedChanged);
			// 
			// radioButton1
			// 
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(20, 24);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "高质量";
			this.radioButton1.CheckedChanged += new System.EventHandler(this.checkedChanged);
			// 
			// chkSelection
			// 
			this.chkSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkSelection.Location = new System.Drawing.Point(22, 192);
			this.chkSelection.Name = "chkSelection";
			this.chkSelection.Size = new System.Drawing.Size(128, 24);
			this.chkSelection.TabIndex = 6;
			this.chkSelection.Text = "只绘制选区内容";
			this.chkSelection.Visible = false;
			this.chkSelection.CheckedChanged += new System.EventHandler(this.checkedChanged);
			// 
			// chkView
			// 
			this.chkView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkView.Location = new System.Drawing.Point(22, 232);
			this.chkView.Name = "chkView";
			this.chkView.Size = new System.Drawing.Size(128, 24);
			this.chkView.TabIndex = 7;
			this.chkView.Text = "只绘制视图内容";
			this.chkView.Visible = false;
			this.chkView.CheckedChanged += new System.EventHandler(this.checkedChanged);
			// 
			// chkTransparent
			// 
			this.chkTransparent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkTransparent.Location = new System.Drawing.Point(22, 136);
			this.chkTransparent.Name = "chkTransparent";
			this.chkTransparent.Size = new System.Drawing.Size(128, 24);
			this.chkTransparent.TabIndex = 8;
			this.chkTransparent.Text = "允许透明";
			this.chkTransparent.Visible = false;
			// 
			// ExportImageDialog
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(160, 85);
			this.Controls.Add(this.chkTransparent);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.comboFormat);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.chkView);
			this.Controls.Add(this.chkSelection);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExportImageDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "导出";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			this.contectBounds = this.vectorcontrol.ContentBounds;
			this.contectBounds.Width +=20;
			this.contectBounds.Height+=20;
		}
	}
}

