				
namespace Itop.Client.Console {
    partial class TreeStyleMenu {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeStyleMenu));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.m_treeViewOne = new System.Windows.Forms.TreeView();
            this.m_treeViewTwo = new System.Windows.Forms.TreeView();
            this.m_toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.LeftToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.m_toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(295, 447);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.m_toolStrip);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(417, 447);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            this.toolStripContainer1.TopToolStripPanelVisible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.m_treeViewOne);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_treeViewTwo);
            this.splitContainer1.Size = new System.Drawing.Size(295, 447);
            this.splitContainer1.SplitterDistance = 132;
            this.splitContainer1.TabIndex = 0;
            // 
            // m_treeViewOne
            // 
            this.m_treeViewOne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_treeViewOne.Location = new System.Drawing.Point(0, 0);
            this.m_treeViewOne.Name = "m_treeViewOne";
            this.m_treeViewOne.Size = new System.Drawing.Size(132, 447);
            this.m_treeViewOne.TabIndex = 0;
            this.m_treeViewOne.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.m_treeViewOne_NodeMouseDoubleClick);
            this.m_treeViewOne.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.m_treeViewOne_AfterSelect);
            this.m_treeViewOne.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_treeViewOne_MouseMove);
            // 
            // m_treeViewTwo
            // 
            this.m_treeViewTwo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_treeViewTwo.Location = new System.Drawing.Point(0, 0);
            this.m_treeViewTwo.Name = "m_treeViewTwo";
            this.m_treeViewTwo.Size = new System.Drawing.Size(159, 447);
            this.m_treeViewTwo.TabIndex = 0;
            this.m_treeViewTwo.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.m_treeViewOne_NodeMouseDoubleClick);
            this.m_treeViewTwo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.m_treeViewOne_MouseMove);
            // 
            // m_toolStrip
            // 
            this.m_toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.m_toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.m_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.m_toolStrip.Location = new System.Drawing.Point(0, 3);
            this.m_toolStrip.Name = "m_toolStrip";
            this.m_toolStrip.ShowItemToolTips = false;
            this.m_toolStrip.Size = new System.Drawing.Size(122, 44);
            this.m_toolStrip.TabIndex = 0;
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(120, 20);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // TreeStyleMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "TreeStyleMenu";
            this.Size = new System.Drawing.Size(417, 447);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.LeftToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.LeftToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.m_toolStrip.ResumeLayout(false);
            this.m_toolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip m_toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView m_treeViewOne;
        private System.Windows.Forms.TreeView m_treeViewTwo;
    }
}
