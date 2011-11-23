using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ItopVectorDraw
{
	/// <summary>
	/// AboutBox 的摘要说明。
	/// </summary>
	public class frmAbout : Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Diagnostics.Process process1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label3;
	
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmAbout()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmAbout));
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.process1 = new System.Diagnostics.Process();
			this.label7 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label6 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(168, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 40);
			this.label1.TabIndex = 0;
			this.label1.Text = "图形管理系统";
			// 
			// pictureBox1
			// 
			this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(16, 16);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(96, 32);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// process1
			// 
			this.process1.SynchronizingObject = this;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 232);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(112, 32);
			this.label7.TabIndex = 5;
			this.label7.Text = "版本： 1.0";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(336, 48);
			this.label2.TabIndex = 5;
			this.label2.Text = "警告：本计算机程序受版权法保护，如未经授权而擅自复制或传播本程序(或其中任何部分)，将受到严厉的民事和刑事制裁，并在法律许可的最大范围内受到起诉。";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 160);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 16);
			this.label4.TabIndex = 5;
			this.label4.Text = "网址：";
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(80, 160);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(136, 24);
			this.linkLabel1.TabIndex = 6;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "http://www.tonli.com";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 184);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(160, 24);
			this.label6.TabIndex = 5;
			this.label6.Text = "电话：     0451-82280175";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(272, 232);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(56, 24);
			this.button1.TabIndex = 7;
			this.button1.Text = "确 定";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.groupBox1.Location = new System.Drawing.Point(8, 216);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(336, 8);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 136);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(240, 24);
			this.label3.TabIndex = 5;
			this.label3.Text = "版权所有： 哈尔滨通力软件开发有限公司";
			// 
			// frmAbout
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(354, 271);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmAbout";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "关于";
			this.Load += new System.EventHandler(this.AboutBox_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void AboutBox_Load(object sender, System.EventArgs e)
		{

		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.process1.StartInfo.FileName="iexplore.exe";
			this.process1.StartInfo.Arguments="www.tonli.com";
			this.process1.Start();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		
	}
}
