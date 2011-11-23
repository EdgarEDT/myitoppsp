using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.Client.Forms
{
	public class frmOption : FormBase
	{ 
		private DevExpress.XtraNavBar.NavBarControl navBarControl1;
		private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
		private DevExpress.XtraNavBar.NavBarItem navBarItem1;
		private DevExpress.XtraEditors.SplitterControl splitterControl1;
		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraNavBar.NavBarItem navBarItem2;
		private System.ComponentModel.IContainer components = null;

		public frmOption()
		{
			// 该调用是 Windows 窗体设计器所必需的。
			InitializeComponent();
			this.Text="设置";
			
			

			// TODO: 在 InitializeComponent 调用后添加任何初始化
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItem1 = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItem2 = new DevExpress.XtraNavBar.NavBarItem();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItem1,
            this.navBarItem2});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 144;
            this.navBarControl1.Size = new System.Drawing.Size(144, 341);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarControl1_LinkClicked);
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "用户界面";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.SmallIconsText;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem1),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItem2)});
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // navBarItem1
            // 
            this.navBarItem1.Caption = "导航栏";
            this.navBarItem1.Name = "navBarItem1";
            this.navBarItem1.Visible = false;
            // 
            // navBarItem2
            // 
            this.navBarItem2.Caption = "窗口样式";
            this.navBarItem2.Name = "navBarItem2";
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(144, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(6, 341);
            this.splitterControl1.TabIndex = 1;
            this.splitterControl1.TabStop = false;
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(150, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(402, 341);
            this.panelControl1.TabIndex = 3;
            // 
            // frmOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(552, 341);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.navBarControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOption";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.frmOption_Load);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
        //public PageOutlook m_outlook=null;
		private UserControl m_curObj;

		private void navBarControl1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
		{
			UserControl oldObj;
			oldObj=m_curObj;
			switch(e.Link.Caption)
			{
				case "导航栏":
                    //if (oldObj!=null && oldObj.Created&&typeof(userOption1).IsInstanceOfType(oldObj))
                    //{
                    //    return;
                    //}
                    //showOption1();	
                    //if (oldObj!=null && oldObj.Created)
                    //    oldObj.Dispose();					
					break;
				case "窗口样式":
					if (oldObj!=null && oldObj.Created&&typeof(userOption2).IsInstanceOfType(oldObj))
					{
						return;
					}
					showOption2();	
					if (oldObj!=null && oldObj.Created)
						oldObj.Dispose();					
					break;

			}
		}
		//[导航栏]
		private void showOption1()
		{
            //this.SuspendLayout();
            //this.panelControl1.SuspendLayout();

            //userOption1 obj = new userOption1();					
            //obj.Bounds=this.panelControl1.DisplayRectangle;
            //obj.Dock=DockStyle.Fill;
            //this.panelControl1.Controls.Add(obj);  
					
            //this.panelControl1.ResumeLayout(true);
            //this.ResumeLayout(true);
            //m_curObj=obj;		

		}
		private void showOption2()
		{
			this.SuspendLayout();
			this.panelControl1.SuspendLayout();

			userOption2 obj = new userOption2();					
			obj.Bounds=this.panelControl1.DisplayRectangle;
			obj.Dock=DockStyle.Fill;
			this.panelControl1.Controls.Add(obj);  
					
			this.panelControl1.ResumeLayout(true);
			this.ResumeLayout(true);
			m_curObj=obj;		

		}

		private void frmOption_Load(object sender, System.EventArgs e)
		{
			showOption2();
            //this.LookAndFeel.UseDefaultLookAndFeel=true;
			
		}
	}	
}

