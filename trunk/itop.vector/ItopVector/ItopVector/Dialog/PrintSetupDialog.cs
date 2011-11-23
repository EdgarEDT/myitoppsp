using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace ItopVector.Dialog
{
	/// <summary>
	/// PrintSetupDialog 的摘要说明。
	/// </summary>
	public class PrintSetupDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private ItopVector.Dialog.PrinterSetup printerSetup1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TabPage tabPage2;
		private ItopVector.Dialog.PaperSetup paperSetup1;
		private ItopVector.DrawArea.DrawArea vectorcontrol;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PrintSetupDialog(ItopVector.DrawArea.DrawArea vcontrol)
		{
			this.vectorcontrol = vcontrol;

			InitializeComponent();

			this.printerSetup1.PageSettings = vcontrol.PageSettings.Clone() as PageSettings;
			this.paperSetup1.PageSettings = this.printerSetup1.PageSettings;
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.printerSetup1 = new ItopVector.Dialog.PrinterSetup();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.paperSetup1 = new ItopVector.Dialog.PaperSetup();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(8, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(226, 290);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.printerSetup1);
			this.tabPage1.Location = new System.Drawing.Point(4, 21);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(218, 265);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "打印设置";
			// 
			// printerSetup1
			// 
			this.printerSetup1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.printerSetup1.Location = new System.Drawing.Point(0, 0);
			this.printerSetup1.Name = "printerSetup1";
			this.printerSetup1.Size = new System.Drawing.Size(218, 265);
			this.printerSetup1.TabIndex = 0;
			this.printerSetup1.PageSettingsChanged += new System.EventHandler(this.printerSetup1_PageSettingsChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.paperSetup1);
			this.tabPage2.Location = new System.Drawing.Point(4, 21);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(474, 265);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "页面尺寸";
			// 
			// paperSetup1
			// 
			this.paperSetup1.BackColor = System.Drawing.SystemColors.Control;
			this.paperSetup1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.paperSetup1.Location = new System.Drawing.Point(0, 0);
			this.paperSetup1.Name = "paperSetup1";
			this.paperSetup1.Size = new System.Drawing.Size(474, 265);
			this.paperSetup1.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(98, 306);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(64, 22);
			this.button1.TabIndex = 1;
			this.button1.Text = "确认(&O)";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(170, 306);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(64, 22);
			this.button2.TabIndex = 1;
			this.button2.Text = "取消(&C)";
			// 
			// PrintSetupDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(242, 336);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.button2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PrintSetupDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "页面设置";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.vectorcontrol.PageSettings = this.printerSetup1.PageSettings.Clone() as PageSettings;

			this.vectorcontrol.DocumentSize =this.paperSetup1.DocumentSize;
			this.DialogResult =DialogResult.OK;
			this.Close();
		}

		private void printerSetup1_PageSettingsChanged(object sender, System.EventArgs e)
		{
			this.paperSetup1.PageSettings =this.printerSetup1.PageSettings;
		}
	}
}
