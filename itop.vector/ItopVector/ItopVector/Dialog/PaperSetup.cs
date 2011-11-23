using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace ItopVector.Dialog
{
	/// <summary>
	/// PaperSetup 的摘要说明。
	/// </summary>
	public class PaperSetup : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton rbHorizontal;
		private System.Windows.Forms.RadioButton rbCustomsize;
		private System.Windows.Forms.RadioButton rbPapersize;
		private PageSettings pageSettings1;
		private const float unitMM=3.543307f;
		private System.Windows.Forms.NumericUpDown emWidth;
		private System.Windows.Forms.NumericUpDown emHeight;
		private System.Windows.Forms.RadioButton rbVertical; 
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PaperSetup()
		{
			this.pageSettings1 =new PageSettings();

			InitializeComponent();

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

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.emWidth = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.rbPapersize = new System.Windows.Forms.RadioButton();
			this.rbCustomsize = new System.Windows.Forms.RadioButton();
			this.emHeight = new System.Windows.Forms.NumericUpDown();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rbHorizontal = new System.Windows.Forms.RadioButton();
			this.rbVertical = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.emWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emHeight)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.emWidth);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.rbPapersize);
			this.groupBox1.Controls.Add(this.rbCustomsize);
			this.groupBox1.Controls.Add(this.emHeight);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 152);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "页面尺寸";
			// 
			// emWidth
			// 
			this.emWidth.Enabled = false;
			this.emWidth.Location = new System.Drawing.Point(24, 88);
			this.emWidth.Maximum = new System.Decimal(new int[] {
																	32768,
																	0,
																	0,
																	0});
			this.emWidth.Name = "emWidth";
			this.emWidth.Size = new System.Drawing.Size(64, 21);
			this.emWidth.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Enabled = false;
			this.label1.Location = new System.Drawing.Point(96, 92);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(16, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "X";
			// 
			// rbPapersize
			// 
			this.rbPapersize.Checked = true;
			this.rbPapersize.Location = new System.Drawing.Point(8, 24);
			this.rbPapersize.Name = "rbPapersize";
			this.rbPapersize.Size = new System.Drawing.Size(176, 24);
			this.rbPapersize.TabIndex = 0;
			this.rbPapersize.TabStop = true;
			this.rbPapersize.Text = "与打印机纸张大小相同(&S)";
			this.rbPapersize.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// rbCustomsize
			// 
			this.rbCustomsize.Location = new System.Drawing.Point(8, 56);
			this.rbCustomsize.Name = "rbCustomsize";
			this.rbCustomsize.Size = new System.Drawing.Size(176, 24);
			this.rbCustomsize.TabIndex = 0;
			this.rbCustomsize.Text = "自定义大小(毫米)(&C):";
			this.rbCustomsize.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
			// 
			// emHeight
			// 
			this.emHeight.Enabled = false;
			this.emHeight.Location = new System.Drawing.Point(112, 88);
			this.emHeight.Maximum = new System.Decimal(new int[] {
																	 32768,
																	 0,
																	 0,
																	 0});
			this.emHeight.Name = "emHeight";
			this.emHeight.Size = new System.Drawing.Size(64, 21);
			this.emHeight.TabIndex = 3;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.rbHorizontal);
			this.groupBox2.Controls.Add(this.rbVertical);
			this.groupBox2.Location = new System.Drawing.Point(8, 192);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 48);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "页面方向";
			this.groupBox2.Visible = false;
			// 
			// rbHorizontal
			// 
			this.rbHorizontal.Checked = true;
			this.rbHorizontal.Location = new System.Drawing.Point(8, 16);
			this.rbHorizontal.Name = "rbHorizontal";
			this.rbHorizontal.Size = new System.Drawing.Size(72, 24);
			this.rbHorizontal.TabIndex = 0;
			this.rbHorizontal.TabStop = true;
			this.rbHorizontal.Text = "横向(&O)";
			// 
			// rbVertical
			// 
			this.rbVertical.Location = new System.Drawing.Point(88, 16);
			this.rbVertical.Name = "rbVertical";
			this.rbVertical.Size = new System.Drawing.Size(96, 24);
			this.rbVertical.TabIndex = 0;
			this.rbVertical.Text = "纵向(&L):";
			// 
			// PaperSetup
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox2);
			this.Name = "PaperSetup";
			this.Size = new System.Drawing.Size(224, 264);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.emWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emHeight)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void radioButton1_CheckedChanged(object sender, System.EventArgs e)
		{
			bool flag1 =rbCustomsize.Checked;
			this.emHeight.Enabled=flag1;
			this.emWidth.Enabled =flag1;
			this.Focus();
			
            if(flag1)
            {
//				MessageBox.Show(this.emWidth.Value.ToString());
				
            }
			else
            {
				this.emHeight.Value=0;
				this.emHeight.Value = (Decimal)(this.pageSettings1.Bounds.Height * .9f /unitMM);
				this.emWidth.Value=0;
				this.emWidth.Value = (Decimal)(this.pageSettings1.Bounds.Width * .9f /unitMM);
				
            }
			
		}
		public PageSettings PageSettings
		{
			get
			{
				return this.pageSettings1;
			}
			set
			{
				if(value==null)return;
				this.pageSettings1 = value;
				this.radioButton1_CheckedChanged(null,null);
			}
		}
		public SizeF DocumentSize
		{
			get
			{
				float ft1= (float)this.emWidth.Value * unitMM;
				float ft2= (float)this.emHeight.Value * unitMM;
				return new SizeF(ft1,ft2);
			}
		}
	}
}
