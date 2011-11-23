using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ItopVectorDraw
{
	/// <summary>
	/// FrmMapTest 的摘要说明。
	/// </summary>
	public class FrmMapTest : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox comboBox1;
		private ItopVectorDraw.MapControl mapControl1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FrmMapTest()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
			this.mapControl1.tlVectorControl1.NewFile();

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
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.mapControl1 = new ItopVectorDraw.MapControl();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.Items.AddRange(new object[] {
														   "4",
														   "2",
														   "1",
														   ".4",
														   ".2",
														   ".1"});
			this.comboBox1.Location = new System.Drawing.Point(8, 8);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 20);
			this.comboBox1.TabIndex = 0;
			this.comboBox1.Text = "1";
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// mapControl1
			// 
			this.mapControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.mapControl1.Location = new System.Drawing.Point(0, 40);
			this.mapControl1.Name = "mapControl1";
			this.mapControl1.Size = new System.Drawing.Size(536, 328);
			this.mapControl1.TabIndex = 1;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.DecimalPlaces = 5;
			this.numericUpDown1.Increment = new System.Decimal(new int[] {
																			 5,
																			 0,
																			 0,
																			 131072});
			this.numericUpDown1.Location = new System.Drawing.Point(160, 8);
			this.numericUpDown1.Maximum = new System.Decimal(new int[] {
																		   1000,
																		   0,
																		   0,
																		   0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(112, 21);
			this.numericUpDown1.TabIndex = 2;
			this.numericUpDown1.Value = new System.Decimal(new int[] {
																		 117740,
																		 0,
																		 0,
																		 196608});
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.DecimalPlaces = 5;
			this.numericUpDown2.Increment = new System.Decimal(new int[] {
																			 5,
																			 0,
																			 0,
																			 131072});
			this.numericUpDown2.Location = new System.Drawing.Point(280, 8);
			this.numericUpDown2.Maximum = new System.Decimal(new int[] {
																		   1000,
																		   0,
																		   0,
																		   0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(112, 21);
			this.numericUpDown2.TabIndex = 2;
			this.numericUpDown2.Value = new System.Decimal(new int[] {
																		 30916,
																		 0,
																		 0,
																		 196608});
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(408, 8);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(40, 24);
			this.button1.TabIndex = 3;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// FrmMapTest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(536, 365);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.mapControl1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.numericUpDown2);
			this.Name = "FrmMapTest";
			this.Text = "FrmMapTest";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.mapControl1.tlVectorControl1.ScaleRatio = float.Parse(comboBox1.Text);
		
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
//            this.mapControl1.mapview.ZeroLongLat=new Itop.MapView.LongLat(this.numericUpDown1.Value,this.numericUpDown2.Value);
//			mapControl1.Invalidate(true);
			string str = string.Empty;
			str+=Guid.NewGuid().ToString("N")+"\n";
			str+=Guid.NewGuid().ToString("D").Substring(24)+"\n";
			str+=Guid.NewGuid().ToString("B")+"\n";
			str+=Guid.NewGuid().ToString("P")+"\n";
			MessageBox.Show(str);


		}
	}
}
