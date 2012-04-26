namespace Itop.Client.Projects
{
    partial class frmProjUser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProjUser));
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.btnAddAll = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddOne = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemoveOne = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemoveAll = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.btnAddAll);
            this.xtraTabPage1.Controls.Add(this.btnAddOne);
            this.xtraTabPage1.Controls.Add(this.btnRemoveOne);
            this.xtraTabPage1.Controls.Add(this.btnRemoveAll);
            this.xtraTabPage1.Controls.Add(this.groupControl2);
            this.xtraTabPage1.Controls.Add(this.groupControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(517, 350);
            this.xtraTabPage1.Text = "项目用户";
            // 
            // btnAddAll
            // 
            this.btnAddAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAll.Location = new System.Drawing.Point(242, 192);
            this.btnAddAll.Name = "btnAddAll";
            this.btnAddAll.Size = new System.Drawing.Size(36, 27);
            this.btnAddAll.TabIndex = 7;
            this.btnAddAll.Text = "<<";
            this.btnAddAll.ToolTip = "添加所有用户";
            this.btnAddAll.Click += new System.EventHandler(this.btnAddAll_Click);
            // 
            // btnAddOne
            // 
            this.btnAddOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddOne.Location = new System.Drawing.Point(242, 151);
            this.btnAddOne.Name = "btnAddOne";
            this.btnAddOne.Size = new System.Drawing.Size(36, 27);
            this.btnAddOne.TabIndex = 6;
            this.btnAddOne.Text = "<";
            this.btnAddOne.ToolTip = "添加用户";
            this.btnAddOne.Click += new System.EventHandler(this.btnAddOne_Click);
            // 
            // btnRemoveOne
            // 
            this.btnRemoveOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveOne.Location = new System.Drawing.Point(242, 105);
            this.btnRemoveOne.Name = "btnRemoveOne";
            this.btnRemoveOne.Size = new System.Drawing.Size(36, 27);
            this.btnRemoveOne.TabIndex = 5;
            this.btnRemoveOne.Text = ">";
            this.btnRemoveOne.ToolTip = "移除用户";
            this.btnRemoveOne.Click += new System.EventHandler(this.btnRemoveOne_Click);
            // 
            // btnRemoveAll
            // 
            this.btnRemoveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAll.Location = new System.Drawing.Point(242, 63);
            this.btnRemoveAll.Name = "btnRemoveAll";
            this.btnRemoveAll.Size = new System.Drawing.Size(36, 27);
            this.btnRemoveAll.TabIndex = 3;
            this.btnRemoveAll.Text = ">>";
            this.btnRemoveAll.ToolTip = "移除所有用户";
            this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.treeView1);
            this.groupControl2.Location = new System.Drawing.Point(283, 18);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(224, 315);
            this.groupControl2.TabIndex = 4;
            this.groupControl2.Text = "所有用户";
            // 
            // treeView1
            // 
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList2;
            this.treeView1.Location = new System.Drawing.Point(8, 26);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(209, 274);
            this.treeView1.TabIndex = 2;
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
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.treeView2);
            this.groupControl1.Location = new System.Drawing.Point(13, 18);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(224, 315);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "当前项目用户";
            // 
            // treeView2
            // 
            this.treeView2.ImageIndex = 0;
            this.treeView2.ImageList = this.imageList2;
            this.treeView2.Location = new System.Drawing.Point(10, 26);
            this.treeView2.Name = "treeView2";
            this.treeView2.SelectedImageIndex = 0;
            this.treeView2.Size = new System.Drawing.Size(209, 274);
            this.treeView2.TabIndex = 3;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Location = new System.Drawing.Point(12, 9);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(524, 380);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(313, 400);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 27);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(434, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            // 
            // frmProjUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 439);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "frmProjUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "项目用户";
            this.Load += new System.EventHandler(this.frmProjUser_Load);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private System.Windows.Forms.TreeView treeView1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.TreeView treeView2;
        private DevExpress.XtraEditors.SimpleButton btnAddAll;
        private DevExpress.XtraEditors.SimpleButton btnAddOne;
        private DevExpress.XtraEditors.SimpleButton btnRemoveOne;
        private DevExpress.XtraEditors.SimpleButton btnRemoveAll;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.ImageList imageList2;
    }
}