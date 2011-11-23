using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace Forms
{
	/// <summary>
	/// frmReg 的摘要说明。
	/// </summary>
	public class frmReg : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label l3;
        private TextBox t1;
        private TextBox t2;
        private Button button1;
        private Button button2;
	
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		[DllImport("librcode2.dll",EntryPoint="GetHDSNo")] 
		private static extern int GetHDSNo( 
											string lpszProductID, 
											 StringBuilder  lpszRetVal
											); 
		[DllImport("librcode2.dll",EntryPoint="VerifyRCode")] 
		private static extern int VerifyRCode( 
											string lpszProductID, 
											string  lpszRCode
											);
        string AppSysID = string.Empty;
		public frmReg()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();
            AppSysID = Itop.Common.ConfigurationHelper.GetValue("AppSysID");
			StringBuilder  a=new StringBuilder(50);//"                                                             ";
            GetHDSNo(AppSysID, a);
			t1.Text=a.ToString();
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.l3 = new System.Windows.Forms.Label();
            this.t1 = new System.Windows.Forms.TextBox();
            this.t2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(41, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "注册码:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(41, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "ID:";
            // 
            // l3
            // 
            this.l3.ForeColor = System.Drawing.Color.OrangeRed;
            this.l3.Location = new System.Drawing.Point(41, 88);
            this.l3.Name = "l3";
            this.l3.Size = new System.Drawing.Size(184, 24);
            this.l3.TabIndex = 5;
            // 
            // t1
            // 
            this.t1.Location = new System.Drawing.Point(97, 17);
            this.t1.Name = "t1";
            this.t1.Size = new System.Drawing.Size(275, 21);
            this.t1.TabIndex = 6;
            // 
            // t2
            // 
            this.t2.Location = new System.Drawing.Point(97, 56);
            this.t2.Name = "t2";
            this.t2.Size = new System.Drawing.Size(275, 21);
            this.t2.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(201, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(297, 88);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmReg
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(406, 124);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.t2);
            this.Controls.Add(this.t1);
            this.Controls.Add(this.l3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmReg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注册";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmReg_Closing);
            this.Load += new System.EventHandler(this.frmReg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void simpleButton1_Click(object sender, System.EventArgs e)
		{
			

			
		}

		private void frmReg_Load(object sender, System.EventArgs e)
		{
		
		}

		private void simpleButton2_Click(object sender, System.EventArgs e)
		{
			this.Close();
			Application.Exit();
		}

		private void frmReg_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Application.Exit();
		}

        private void button1_Click(object sender, EventArgs e)
        {
            int i = VerifyRCode(AppSysID, t2.Text);
            if (i == 0)
            {
                l3.Text = "注册码错误，请重新输入！";
            }
            if (i == 1)
            {
                RegistryKey key = Registry.Users.CreateSubKey(".DEFAULT\\Software\\Itopsoft");
                key.SetValue(AppSysID, t2.Text);
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
	}
}
