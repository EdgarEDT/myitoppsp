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
	/// PrinterSetup 的摘要说明。
	/// </summary>
	public class PrinterSetup : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton rbHorizontal;
		private System.Windows.Forms.RadioButton rbVertical;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cbxPapers;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbTop;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown emTop;
		private System.Windows.Forms.NumericUpDown emBottom;
		private System.Windows.Forms.NumericUpDown emLeft;
		private System.Windows.Forms.NumericUpDown emRight;
		private PageSettings pageSettings1;
		private const float unitMM=3.543307f; 
		public event EventHandler PageSettingsChanged;
		private bool notifychanged;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PrinterSetup()
		{
			pageSettings1=new PageSettings();
			InitializeComponent();
			notifychanged=false;

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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rbHorizontal = new System.Windows.Forms.RadioButton();
			this.rbVertical = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cbxPapers = new System.Windows.Forms.ComboBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.emLeft = new System.Windows.Forms.NumericUpDown();
			this.emRight = new System.Windows.Forms.NumericUpDown();
			this.emBottom = new System.Windows.Forms.NumericUpDown();
			this.emTop = new System.Windows.Forms.NumericUpDown();
			this.tbTop = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.emLeft)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emRight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emBottom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.emTop)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.rbHorizontal);
			this.groupBox2.Controls.Add(this.rbVertical);
			this.groupBox2.Location = new System.Drawing.Point(8, 200);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 48);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "页面方向";
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
			this.rbHorizontal.CheckedChanged += new System.EventHandler(this.rbHorizontal_CheckedChanged);
			// 
			// rbVertical
			// 
			this.rbVertical.Location = new System.Drawing.Point(88, 16);
			this.rbVertical.Name = "rbVertical";
			this.rbVertical.Size = new System.Drawing.Size(96, 24);
			this.rbVertical.TabIndex = 0;
			this.rbVertical.Text = "纵向(&L):";
			this.rbVertical.CheckedChanged += new System.EventHandler(this.rbHorizontal_CheckedChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cbxPapers);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(200, 56);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "打印纸";
			// 
			// cbxPapers
			// 
			this.cbxPapers.Location = new System.Drawing.Point(16, 24);
			this.cbxPapers.Name = "cbxPapers";
			this.cbxPapers.Size = new System.Drawing.Size(168, 20);
			this.cbxPapers.TabIndex = 0;
			this.cbxPapers.Text = "papers";
			this.cbxPapers.SelectedIndexChanged += new System.EventHandler(this.cbxPapers_SelectedIndexChanged);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.emLeft);
			this.groupBox3.Controls.Add(this.emRight);
			this.groupBox3.Controls.Add(this.emBottom);
			this.groupBox3.Controls.Add(this.emTop);
			this.groupBox3.Controls.Add(this.tbTop);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Controls.Add(this.label6);
			this.groupBox3.Location = new System.Drawing.Point(8, 80);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(200, 112);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "边距(毫米)";
			// 
			// emLeft
			// 
			this.emLeft.DecimalPlaces = 2;
			this.emLeft.Location = new System.Drawing.Point(48, 72);
			this.emLeft.Name = "emLeft";
			this.emLeft.Size = new System.Drawing.Size(48, 21);
			this.emLeft.TabIndex = 2;
			// 
			// emRight
			// 
			this.emRight.DecimalPlaces = 2;
			this.emRight.Location = new System.Drawing.Point(136, 72);
			this.emRight.Name = "emRight";
			this.emRight.Size = new System.Drawing.Size(48, 21);
			this.emRight.TabIndex = 2;
			// 
			// emBottom
			// 
			this.emBottom.DecimalPlaces = 2;
			this.emBottom.Location = new System.Drawing.Point(136, 32);
			this.emBottom.Name = "emBottom";
			this.emBottom.Size = new System.Drawing.Size(48, 21);
			this.emBottom.TabIndex = 2;
			// 
			// emTop
			// 
			this.emTop.DecimalPlaces = 2;
			this.emTop.Location = new System.Drawing.Point(48, 32);
			this.emTop.Name = "emTop";
			this.emTop.Size = new System.Drawing.Size(48, 21);
			this.emTop.TabIndex = 2;
			// 
			// tbTop
			// 
			this.tbTop.Location = new System.Drawing.Point(48, 32);
			this.tbTop.Name = "tbTop";
			this.tbTop.Size = new System.Drawing.Size(40, 21);
			this.tbTop.TabIndex = 1;
			this.tbTop.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 24);
			this.label1.TabIndex = 0;
			this.label1.Text = "上(&T):";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(96, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 24);
			this.label4.TabIndex = 0;
			this.label4.Text = "下(&B):";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 72);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(48, 24);
			this.label5.TabIndex = 0;
			this.label5.Text = "左(&L):";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(96, 72);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 24);
			this.label6.TabIndex = 0;
			this.label6.Text = "右(&R):";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PrinterSetup
			// 
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox3);
			this.Name = "PrinterSetup";
			this.Size = new System.Drawing.Size(224, 264);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.emLeft)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emRight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emBottom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.emTop)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		///= 1.25f; //点
		///= 90f; //英寸
		///= 35.43307f;厘米
		///= 3.543307f;毫米
		private void InitData()
		{
			this.notifychanged=false;
			this.cbxPapers.Items.Clear();
			this.cbxPapers.Text = pageSettings1.PaperSize.PaperName;
			foreach(PaperSize paper in pageSettings1.PrinterSettings.PaperSizes)
			{
				this.cbxPapers.Items.Add(paper.PaperName);
			}
			this.rbHorizontal.Checked = this.pageSettings1.Landscape;
			this.rbVertical.Checked = !this.pageSettings1.Landscape;

			this.SetMargins();
			this.notifychanged=true;
		}
		private void SetMargins()
		{
//			float ft1=(pageSettings1.Margins.Left *.9f /35.43307f);
			this.emLeft.Value =(decimal)(pageSettings1.Margins.Left *.9f /unitMM);
			this.emRight.Value = (decimal) (pageSettings1.Margins.Right *.9f /unitMM);
			this.emBottom.Value = (decimal) (pageSettings1.Margins.Bottom *.9f /unitMM);
			this.emTop.Value = (decimal) (pageSettings1.Margins.Top *.9f /unitMM);
			
		}

		private void cbxPapers_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.pageSettings1.PaperSize = pageSettings1.PrinterSettings.PaperSizes[cbxPapers.SelectedIndex];
			this.SetMargins();
			if(this.PageSettingsChanged!=null && notifychanged)
			{
				this.PageSettingsChanged(this,EventArgs.Empty);
			}
		}

		private void rbHorizontal_CheckedChanged(object sender, System.EventArgs e)
		{
			this.pageSettings1.Landscape = rbHorizontal.Checked;
			if(this.PageSettingsChanged!=null && notifychanged)
			{
				this.PageSettingsChanged(this,EventArgs.Empty);
			}
		}
	
		public PageSettings PageSettings
		{
			get
			{
//				float ft1 =(float)this.emTop.Value/.9f*unitMM ;
				this.pageSettings1.Margins.Left =(int)Math.Round(((float)this.emLeft.Value/.9f*unitMM ));
				this.pageSettings1.Margins.Top = (int)Math.Round(((float)this.emTop.Value/.9f*unitMM));
				this.pageSettings1.Margins.Bottom=(int)Math.Round(((float)this.emBottom.Value/.9f*unitMM));
				this.pageSettings1.Margins.Right =(int)Math.Round(((float)this.emRight.Value/.9f *unitMM));

				return this.pageSettings1;
			}
			set
			{
				if(value==null)return;
				this.pageSettings1 = value;
				InitData();
			}
		}
	}
}
