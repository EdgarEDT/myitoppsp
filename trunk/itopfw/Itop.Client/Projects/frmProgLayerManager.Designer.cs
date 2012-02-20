namespace Itop.Client.Projects
{
    partial class frmProgLayerManager
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Location = new System.Drawing.Point(0, -1);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(255, 248);
            this.treeView1.TabIndex = 3;
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterCheck);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(167, 265);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 29);
            this.simpleButton2.TabIndex = 24;
            this.simpleButton2.Text = "关闭(&C)";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(77, 265);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 29);
            this.simpleButton1.TabIndex = 23;
            this.simpleButton1.Text = "确认(&O)";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // frmProgLayerManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 306);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.treeView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProgLayerManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关联图层";
            this.Load += new System.EventHandler(this.frmProgLayerManager_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}