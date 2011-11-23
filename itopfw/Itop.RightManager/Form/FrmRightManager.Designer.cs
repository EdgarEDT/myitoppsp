namespace Itop.RightManager.UI {
    partial class FrmRightManager {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRightManager));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbDel = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRights = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.tsbEdit,
            this.tsbDel,
            this.toolStripSeparator3,
            this.tsbRefresh,
            this.toolStripSeparator1,
            this.tsbRights,
            this.toolStripSeparator2,
            this.tsbClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(468, 31);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbEdit
            // 
            this.tsbEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbEdit.Image")));
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(57, 28);
            this.tsbEdit.Text = "修改";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // tsbDel
            // 
            this.tsbDel.Image = ((System.Drawing.Image)(resources.GetObject("tsbDel.Image")));
            this.tsbDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDel.Name = "tsbDel";
            this.tsbDel.Size = new System.Drawing.Size(57, 28);
            this.tsbDel.Text = "删除";
            this.tsbDel.Click += new System.EventHandler(this.tsbDel_Click);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(57, 28);
            this.tsbRefresh.Text = "刷新";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbRights
            // 
            this.tsbRights.Image = ((System.Drawing.Image)(resources.GetObject("tsbRights.Image")));
            this.tsbRights.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRights.Name = "tsbRights";
            this.tsbRights.Size = new System.Drawing.Size(57, 28);
            this.tsbRights.Text = "授权";
            this.tsbRights.Click += new System.EventHandler(this.tsbRights_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbClose
            // 
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(57, 28);
            this.tsbClose.Text = "关闭";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(69, 28);
            this.toolStripButton1.Text = "添加组";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(81, 28);
            this.toolStripButton2.Text = "添加用户";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "411214019201153424.jpg");
            this.imageList2.Images.SetKeyName(1, "411213517201116566.jpg");
            this.imageList2.Images.SetKeyName(2, "411153517201194678.jpg");
            this.imageList2.Images.SetKeyName(3, "411144019201118803.jpg");
            this.imageList2.Images.SetKeyName(4, "320521016201164845.jpg");
            this.imageList2.Images.SetKeyName(5, "320351016201136457.jpg");
            this.imageList2.Images.SetKeyName(6, "320231016201139934.jpg");
            this.imageList2.Images.SetKeyName(7, "320171016201169252.jpg");
            this.imageList2.Images.SetKeyName(8, "320111016201145884.jpg");
            this.imageList2.Images.SetKeyName(9, "320101116201195738.gif");
            this.imageList2.Images.SetKeyName(10, "41114411201110771.jpg");
            this.imageList2.Images.SetKeyName(11, "41103517201128217.jpg");
            this.imageList2.Images.SetKeyName(12, "35554410201158450.jpg");
            this.imageList2.Images.SetKeyName(13, "35454510201194671.jpg");
            this.imageList2.Images.SetKeyName(14, "35454410201181867.jpg");
            this.imageList2.Images.SetKeyName(15, "35424410201173094.jpg");
            this.imageList2.Images.SetKeyName(16, "35414510201178758.jpg");
            this.imageList2.Images.SetKeyName(17, "35384510201147551.jpg");
            this.imageList2.Images.SetKeyName(18, "35384410201145964.jpg");
            this.imageList2.Images.SetKeyName(19, "35274510201124305.jpg");
            this.imageList2.Images.SetKeyName(20, "35184410201119097.jpg");
            this.imageList2.Images.SetKeyName(21, "32071016201140511.jpg");
            this.imageList2.Images.SetKeyName(22, "32058916201135885.jpg");
            this.imageList2.Images.SetKeyName(23, "32054916201161968.jpg");
            this.imageList2.Images.SetKeyName(24, "32051116201123163.gif");
            this.imageList2.Images.SetKeyName(25, "32049916201188663.jpg");
            this.imageList2.Images.SetKeyName(26, "32046916201159189.jpg");
            this.imageList2.Images.SetKeyName(27, "32032916201159747.gif");
            this.imageList2.Images.SetKeyName(28, "32001116201111804.gif");
            this.imageList2.Images.SetKeyName(29, "22854011201186031.jpg");
            this.imageList2.Images.SetKeyName(30, "22849011201180230.jpg");
            this.imageList2.Images.SetKeyName(31, "22836011201161010.jpg");
            this.imageList2.Images.SetKeyName(32, "22825211201114363.jpg");
            this.imageList2.Images.SetKeyName(33, "22816211201166746.jpg");
            this.imageList2.Images.SetKeyName(34, "3564410201164488.jpg");
            this.imageList2.Images.SetKeyName(35, "3514410201153870.jpg");
            this.imageList2.Images.SetKeyName(36, "2286211201162170.jpg");
            this.imageList2.Images.SetKeyName(37, "1604[1].bmp");
            this.imageList2.Images.SetKeyName(38, "1603[1].bmp");
            this.imageList2.Images.SetKeyName(39, "1602[1].bmp");
            this.imageList2.Images.SetKeyName(40, "1599[1].bmp");
            this.imageList2.Images.SetKeyName(41, "1598[1].bmp");
            this.imageList2.Images.SetKeyName(42, "1273[1].bmp");
            this.imageList2.Images.SetKeyName(43, "1270[1].bmp");
            this.imageList2.Images.SetKeyName(44, "1043[1].bmp");
            this.imageList2.Images.SetKeyName(45, "1037[1].bmp");
            this.imageList2.Images.SetKeyName(46, "1026[1].bmp");
            this.imageList2.Images.SetKeyName(47, "1024[1].bmp");
            this.imageList2.Images.SetKeyName(48, "Windows Messenger.png");
            this.imageList2.Images.SetKeyName(49, "ICS client.ico");
            this.imageList2.Images.SetKeyName(50, "user.png");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folderopen.ico");
            this.imageList1.Images.SetKeyName(1, "ICS client.ico");
            this.imageList1.Images.SetKeyName(2, "user.ico");
            this.imageList1.Images.SetKeyName(3, "users.ico");
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 238);
            this.panel1.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList2;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(468, 238);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // FrmRightManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 269);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmRightManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRightManager";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripButton tsbDel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbRights;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
    }
}