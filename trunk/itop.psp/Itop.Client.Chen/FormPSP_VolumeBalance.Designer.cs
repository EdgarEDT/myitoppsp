namespace Itop.Client.Chen
{
    partial class FormPSP_VolumeBalance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPSP_VolumeBalance));
            this.ctrlPSP_VolumeBalance1 = new Itop.Client.Chen.CtrlPSP_VolumeBalance();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.SuspendLayout();
            // 
            // il
            // 
            this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.il.ImageSize = new System.Drawing.Size(24, 24);
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.Images.SetKeyName(0, "");
            this.il.Images.SetKeyName(1, "");
            this.il.Images.SetKeyName(2, "");
            this.il.Images.SetKeyName(3, "");
            this.il.Images.SetKeyName(4, "");
            this.il.Images.SetKeyName(5, "help.ico");
            this.il.Images.SetKeyName(6, "Trans.ico");
            this.il.Images.SetKeyName(7, "Utilities Folder.ico");
            this.il.Images.SetKeyName(8, "xxxx.ico");
            this.il.Images.SetKeyName(9, "保存.ico");
            this.il.Images.SetKeyName(10, "布局.ico");
            this.il.Images.SetKeyName(11, "查询.ico");
            this.il.Images.SetKeyName(12, "打回重新编.ico");
            this.il.Images.SetKeyName(13, "打印.ico");
            this.il.Images.SetKeyName(14, "发送.ico");
            this.il.Images.SetKeyName(15, "关闭1.ico");
            this.il.Images.SetKeyName(16, "关闭.ico");
            this.il.Images.SetKeyName(17, "角色.ico");
            this.il.Images.SetKeyName(18, "三等功发1.ico");
            this.il.Images.SetKeyName(19, "审核.ico");
            this.il.Images.SetKeyName(20, "审批.ico");
            this.il.Images.SetKeyName(21, "授权.ico");
            this.il.Images.SetKeyName(22, "刷新.ico");
            this.il.Images.SetKeyName(23, "添加同级.ico");
            this.il.Images.SetKeyName(24, "添加下级.ico");
            this.il.Images.SetKeyName(25, "新建.ico");
            this.il.Images.SetKeyName(26, "修改.ico");
            this.il.Images.SetKeyName(27, "作废.ico");
            // 
            // ctrlPSP_VolumeBalance1
            // 
            this.ctrlPSP_VolumeBalance1.ADdRight = false;
            this.ctrlPSP_VolumeBalance1.AllowUpdate = true;
            this.ctrlPSP_VolumeBalance1.DEleteRight = false;
            this.ctrlPSP_VolumeBalance1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlPSP_VolumeBalance1.EDitRight = false;
            this.ctrlPSP_VolumeBalance1.Flag = "";
            this.ctrlPSP_VolumeBalance1.Loadrate = "";
            this.ctrlPSP_VolumeBalance1.Location = new System.Drawing.Point(0, 34);
            this.ctrlPSP_VolumeBalance1.Name = "ctrlPSP_VolumeBalance1";
            this.ctrlPSP_VolumeBalance1.PRintRight = false;
            this.ctrlPSP_VolumeBalance1.Size = new System.Drawing.Size(622, 357);
            this.ctrlPSP_VolumeBalance1.TabIndex = 4;
            this.ctrlPSP_VolumeBalance1.Volumecalc0 = "";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "统计";
            this.barButtonItem1.Id = 7;
            this.barButtonItem1.ImageIndex = 19;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // FormPSP_VolumeBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 391);
            this.Controls.Add(this.ctrlPSP_VolumeBalance1);
            this.Name = "FormPSP_VolumeBalance";
            this.Text = "";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormPSP_VolumeBalance_Load);
            this.Controls.SetChildIndex(this.ctrlPSP_VolumeBalance1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlPSP_VolumeBalance ctrlPSP_VolumeBalance1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
    }
}