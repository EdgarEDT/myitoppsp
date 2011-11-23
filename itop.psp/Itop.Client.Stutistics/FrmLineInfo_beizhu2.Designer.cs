namespace Itop.Client.Stutistics
{
    partial class FrmLineInfo_beizhu2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLineInfo_beizhu2));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barDel = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.barAdd1 = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit1 = new DevExpress.XtraBars.BarButtonItem();
            this.barDel1 = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barInsert = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ctrlLine_Info1 = new Itop.Client.Stutistics.CtrlLine_beizhu(z);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowMoveBarOnToolbar = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.AllowShowToolbarsPopup = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItem1,
            this.barSubItem2,
            this.barPrint,
            this.barClose,
            this.barAdd,
            this.barEdit,
            this.barDel,
            this.barAdd1,
            this.barEdit1,
            this.barDel1,
            this.barButtonItem1,
            this.barInsert,
            this.barButtonItem2});
            this.barManager1.MaxItemId = 15;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 1";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barInsert),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barClose)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.RotateWhenVertical = false;
            this.bar1.Text = "Custom 1";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "线路维护";
            this.barSubItem1.Id = 0;
            this.barSubItem1.ImageIndex = 0;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDel)});
            this.barSubItem1.Name = "barSubItem1";
            this.barSubItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barAdd
            // 
            this.barAdd.Caption = "添加";
            this.barAdd.Id = 4;
            this.barAdd.ImageIndex = 4;
            this.barAdd.Name = "barAdd";
            this.barAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barEdit
            // 
            this.barEdit.Caption = "修改";
            this.barEdit.Id = 5;
            this.barEdit.ImageIndex = 5;
            this.barEdit.Name = "barEdit";
            this.barEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdit_ItemClick);
            // 
            // barDel
            // 
            this.barDel.Caption = "删除";
            this.barDel.Id = 6;
            this.barDel.ImageIndex = 3;
            this.barDel.Name = "barDel";
            this.barDel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDel_ItemClick);
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "分类维护";
            this.barSubItem2.Id = 1;
            this.barSubItem2.ImageIndex = 6;
            this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAdd1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEdit1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDel1)});
            this.barSubItem2.Name = "barSubItem2";
            this.barSubItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barAdd1
            // 
            this.barAdd1.Caption = "添加";
            this.barAdd1.Id = 7;
            this.barAdd1.ImageIndex = 4;
            this.barAdd1.Name = "barAdd1";
            this.barAdd1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barAdd1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd1_ItemClick);
            // 
            // barEdit1
            // 
            this.barEdit1.Caption = "修改";
            this.barEdit1.Id = 8;
            this.barEdit1.ImageIndex = 5;
            this.barEdit1.Name = "barEdit1";
            this.barEdit1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barEdit1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdit1_ItemClick);
            // 
            // barDel1
            // 
            this.barDel1.Caption = "删除";
            this.barDel1.Id = 9;
            this.barDel1.ImageIndex = 3;
            this.barDel1.Name = "barDel1";
            this.barDel1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barDel1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDel1_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "打印";
            this.barPrint.Id = 2;
            this.barPrint.ImageIndex = 2;
            this.barPrint.Name = "barPrint";
            this.barPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "选择";
            this.barButtonItem1.Id = 10;
            this.barButtonItem1.ImageIndex = 11;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barInsert
            // 
            this.barInsert.Caption = "导入";
            this.barInsert.Id = 11;
            this.barInsert.ImageIndex = 10;
            this.barInsert.Name = "barInsert";
            this.barInsert.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barInsert.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barInsert_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "导出";
            this.barButtonItem2.Id = 12;
            this.barButtonItem2.ImageIndex = 12;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭";
            this.barClose.Id = 3;
            this.barClose.ImageIndex = 13;
            this.barClose.Name = "barClose";
            this.barClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ftp.ico");
            this.imageList1.Images.SetKeyName(1, "布局.ico");
            this.imageList1.Images.SetKeyName(2, "打印.ico");
            this.imageList1.Images.SetKeyName(3, "删除.ico");
            this.imageList1.Images.SetKeyName(4, "新建.ico");
            this.imageList1.Images.SetKeyName(5, "修改.ico");
            this.imageList1.Images.SetKeyName(6, "books.ico");
            this.imageList1.Images.SetKeyName(7, "folder_home.ico");
            this.imageList1.Images.SetKeyName(8, "关闭.ico");
            this.imageList1.Images.SetKeyName(9, "审批.ico");
            this.imageList1.Images.SetKeyName(10, "布局.ico");
            this.imageList1.Images.SetKeyName(11, "审批.ico");
            this.imageList1.Images.SetKeyName(12, "打回重新编.ico");
            this.imageList1.Images.SetKeyName(13, "关闭.ico");
            // 
            // ctrlLine_Info1
            // 
            this.ctrlLine_Info1.AllowUpdate = true;
            this.ctrlLine_Info1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlLine_Info1.Flag = "";
            this.ctrlLine_Info1.IsSelect = false;
            this.ctrlLine_Info1.Location = new System.Drawing.Point(0, 33);
            this.ctrlLine_Info1.Name = "ctrlLine_Info1";
            this.ctrlLine_Info1.Size = new System.Drawing.Size(679, 318);
            this.ctrlLine_Info1.TabIndex = 4;
            this.ctrlLine_Info1.Type = "";
            this.ctrlLine_Info1.Type2 = "";
            // 
            // FrmLineInfo_beizhu2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 351);
            this.Controls.Add(this.ctrlLine_Info1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmLineInfo_beizhu2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLineInfo_beizhu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLineInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion





        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barDel;
        private DevExpress.XtraBars.BarButtonItem barAdd1;
        private DevExpress.XtraBars.BarButtonItem barEdit1;
        private DevExpress.XtraBars.BarButtonItem barDel1;
        private System.Windows.Forms.ImageList imageList1;
        public CtrlLine_beizhu ctrlLine_Info1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barInsert;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
    }
}