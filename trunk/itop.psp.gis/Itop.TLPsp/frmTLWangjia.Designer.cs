namespace Itop.TLPsp
{
    partial class frmTLWangjia
    {
        /// <summary>
        /// 必需的设计器变量。

        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。

        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。

        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tlVectorControl1 = new ItopVector.ItopVectorControl();
            this.dotNetBarManager1 = new DevComponents.DotNetBar.DotNetBarManager(this.components);
            this.barBottomDockSite = new DevComponents.DotNetBar.DockSite();
            this.barLeftDockSite = new DevComponents.DotNetBar.DockSite();
            this.barRightDockSite = new DevComponents.DotNetBar.DockSite();
            this.barTopDockSite = new DevComponents.DotNetBar.DockSite();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmLineConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlVectorControl1
            // 
            this.tlVectorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tlVectorControl1.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.tlVectorControl1.CanEdit = false;
            this.tlVectorControl1.FullDrawMode = false;
            this.tlVectorControl1.IsPasteGrid = false;
            this.tlVectorControl1.IsShowGrid = true;
            this.tlVectorControl1.IsShowRule = true;
            this.tlVectorControl1.IsShowTip = false;
            this.tlVectorControl1.Location = new System.Drawing.Point(171, 88);
            this.tlVectorControl1.Name = "tlVectorControl1";
            this.tlVectorControl1.Size = new System.Drawing.Size(848, 619);
            this.tlVectorControl1.TabIndex = 9;
            this.tlVectorControl1.Load += new System.EventHandler(this.tlVectorControl1_Load);
            // 
            // dotNetBarManager1
            // 
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.F1);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlC);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlA);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlV);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlX);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlZ);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlY);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Del);
            this.dotNetBarManager1.AutoDispatchShortcuts.Add(DevComponents.DotNetBar.eShortcut.Ins);
            this.dotNetBarManager1.BottomDockSite = this.barBottomDockSite;
            this.dotNetBarManager1.DefinitionName = "frmTLWangjia.dotNetBarManager1.xml";
            this.dotNetBarManager1.EnableFullSizeDock = false;
            this.dotNetBarManager1.LeftDockSite = this.barLeftDockSite;
            this.dotNetBarManager1.ParentForm = this;
            this.dotNetBarManager1.RightDockSite = this.barRightDockSite;
            this.dotNetBarManager1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.dotNetBarManager1.ThemeAware = false;
            this.dotNetBarManager1.TopDockSite = this.barTopDockSite;
            this.dotNetBarManager1.ItemClick += new System.EventHandler(this.dotNetBarManager1_ItemClick);
            // 
            // barBottomDockSite
            // 
            this.barBottomDockSite.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.barBottomDockSite.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barBottomDockSite.Location = new System.Drawing.Point(0, 708);
            this.barBottomDockSite.Name = "barBottomDockSite";
            this.barBottomDockSite.NeedsLayout = false;
            this.barBottomDockSite.Size = new System.Drawing.Size(1019, 0);
            this.barBottomDockSite.TabIndex = 13;
            this.barBottomDockSite.TabStop = false;
            // 
            // barLeftDockSite
            // 
            this.barLeftDockSite.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.barLeftDockSite.Dock = System.Windows.Forms.DockStyle.Left;
            this.barLeftDockSite.Location = new System.Drawing.Point(0, 88);
            this.barLeftDockSite.Name = "barLeftDockSite";
            this.barLeftDockSite.NeedsLayout = false;
            this.barLeftDockSite.Size = new System.Drawing.Size(173, 620);
            this.barLeftDockSite.TabIndex = 10;
            this.barLeftDockSite.TabStop = false;
            // 
            // barRightDockSite
            // 
            this.barRightDockSite.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.barRightDockSite.Dock = System.Windows.Forms.DockStyle.Right;
            this.barRightDockSite.Location = new System.Drawing.Point(1019, 88);
            this.barRightDockSite.Name = "barRightDockSite";
            this.barRightDockSite.NeedsLayout = false;
            this.barRightDockSite.Size = new System.Drawing.Size(0, 620);
            this.barRightDockSite.TabIndex = 11;
            this.barRightDockSite.TabStop = false;
            // 
            // barTopDockSite
            // 
            this.barTopDockSite.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.barTopDockSite.Dock = System.Windows.Forms.DockStyle.Top;
            this.barTopDockSite.Location = new System.Drawing.Point(0, 0);
            this.barTopDockSite.Name = "barTopDockSite";
            this.barTopDockSite.NeedsLayout = false;
            this.barTopDockSite.Size = new System.Drawing.Size(1019, 88);
            this.barTopDockSite.TabIndex = 12;
            this.barTopDockSite.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Location = new System.Drawing.Point(489, 348);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dToolStripMenuItem,
            this.sToolStripMenuItem,
            this.moveMenuItem,
            this.jxtToolStripMenuItem,
            this.printToolStripMenuItem,
            this.SubToolStripMenuItem,
            this.tmLineConnect});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(143, 158);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // dToolStripMenuItem
            // 
            this.dToolStripMenuItem.Name = "dToolStripMenuItem";
            this.dToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.dToolStripMenuItem.Text = "打开";
            this.dToolStripMenuItem.Visible = false;
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.sToolStripMenuItem.Text = "属性";
            // 
            // moveMenuItem
            // 
            this.moveMenuItem.Name = "moveMenuItem";
            this.moveMenuItem.Size = new System.Drawing.Size(142, 22);
            this.moveMenuItem.Text = "短路计算";
            // 
            // jxtToolStripMenuItem
            // 
            this.jxtToolStripMenuItem.Name = "jxtToolStripMenuItem";
            this.jxtToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.jxtToolStripMenuItem.Text = "接线图";
            this.jxtToolStripMenuItem.Visible = false;
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.printToolStripMenuItem.Text = "区域打印";
            this.printToolStripMenuItem.Visible = false;
            // 
            // SubToolStripMenuItem
            // 
            this.SubToolStripMenuItem.Name = "SubToolStripMenuItem";
            this.SubToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.SubToolStripMenuItem.Text = "分类统计报表";
            this.SubToolStripMenuItem.Visible = false;
            // 
            // tmLineConnect
            // 
            this.tmLineConnect.Name = "tmLineConnect";
            this.tmLineConnect.Size = new System.Drawing.Size(142, 22);
            this.tmLineConnect.Text = "两线路连接";
            this.tmLineConnect.Visible = false;
            // 
            // frmTLWangjia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 708);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.barLeftDockSite);
            this.Controls.Add(this.tlVectorControl1);
            this.Controls.Add(this.barRightDockSite);
            this.Controls.Add(this.barTopDockSite);
            this.Controls.Add(this.barBottomDockSite);
            this.Name = "frmTLWangjia";
            this.Text = "网架优化";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTLpsp_FormClosing);
            this.Load += new System.EventHandler(this.frmTLpsp_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ItopVector.ItopVectorControl tlVectorControl1;
        private DevComponents.DotNetBar.DotNetBarManager dotNetBarManager1;
        private DevComponents.DotNetBar.DockSite barBottomDockSite;
        private DevComponents.DotNetBar.DockSite barLeftDockSite;
        private DevComponents.DotNetBar.DockSite barRightDockSite;
        private DevComponents.DotNetBar.DockSite barTopDockSite;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tmLineConnect;
    }
}