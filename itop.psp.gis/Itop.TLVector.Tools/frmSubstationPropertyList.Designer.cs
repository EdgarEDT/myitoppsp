namespace ItopVector.Tools
{
    partial class frmSubstationPropertyList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSubstationPropertyList));
            this.ctrlSubStationProperty1 = new ItopVector.Tools.CtrlSubStationProperty();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.btsel = new DevExpress.XtraBars.BarButtonItem();
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
            // 
            // ctrlSubStationProperty1
            // 
            this.ctrlSubStationProperty1.AllowUpdate = true;
            this.ctrlSubStationProperty1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlSubStationProperty1.Location = new System.Drawing.Point(0, 34);
            this.ctrlSubStationProperty1.Name = "ctrlSubStationProperty1";
            this.ctrlSubStationProperty1.Size = new System.Drawing.Size(563, 342);
            this.ctrlSubStationProperty1.TabIndex = 4;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 7;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // btsel
            // 
            this.btsel.Caption = "选择";
            this.btsel.Id = 8;
            this.btsel.ImageIndex = 0;
            this.btsel.ImageIndexDisabled = 0;
            this.btsel.Name = "btsel";
            this.btsel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btsel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btsel_ItemClick);
            // 
            // frmSubstationPropertyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 376);
            this.Controls.Add(this.ctrlSubStationProperty1);
            this.Name = "frmSubstationPropertyList";
            this.Text = "";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmglebePropertyList_Load);
            this.Controls.SetChildIndex(this.ctrlSubStationProperty1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlSubStationProperty ctrlSubStationProperty1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btsel;



    }
}