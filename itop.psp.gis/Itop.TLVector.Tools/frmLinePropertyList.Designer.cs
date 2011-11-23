namespace ItopVector.Tools
{
    partial class frmLinePropertyList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLinePropertyList));
            this.ctrlLineProperty1 = new ItopVector.Tools.CtrlLineProperty();
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
            this.il.Images.SetKeyName(5, "打回重新编.ico");
            // 
            // ctrlLineProperty1
            // 
            this.ctrlLineProperty1.AllowUpdate = true;
            this.ctrlLineProperty1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlLineProperty1.Location = new System.Drawing.Point(0, 34);
            this.ctrlLineProperty1.Name = "ctrlLineProperty1";
            this.ctrlLineProperty1.Size = new System.Drawing.Size(544, 342);
            this.ctrlLineProperty1.TabIndex = 4;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "导出";
            this.barButtonItem1.Id = 7;
            this.barButtonItem1.ImageIndex = 5;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // frmLinePropertyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 376);
            this.Controls.Add(this.ctrlLineProperty1);
            this.Name = "frmLinePropertyList";
            this.Text = "";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmglebePropertyList_Load);
            this.Controls.SetChildIndex(this.ctrlLineProperty1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlLineProperty ctrlLineProperty1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;


    }
}