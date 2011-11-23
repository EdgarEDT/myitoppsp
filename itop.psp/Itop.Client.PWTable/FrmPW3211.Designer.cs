namespace Itop.Client.PWTable
{
    partial class FrmPW3211
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPW3211));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barAdd = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonDel = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn5 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand24 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand25 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand19 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand20 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand21 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
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
            this.barAdd,
            this.barEdit,
            this.barButtonDel,
            this.barPrint,
            this.barClose,
            this.barButtonItem1,
            this.barButtonItem2});
            this.barManager1.MaxItemId = 8;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 1";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.FloatLocation = new System.Drawing.Point(62, 161);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barClose)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.Hidden = true;
            this.bar1.OptionsBar.RotateWhenVertical = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 1";
            // 
            // barAdd
            // 
            this.barAdd.Caption = "添加";
            this.barAdd.Id = 1;
            this.barAdd.ImageIndex = 18;
            this.barAdd.Name = "barAdd";
            this.barAdd.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd_ItemClick);
            // 
            // barEdit
            // 
            this.barEdit.Caption = "修改";
            this.barEdit.Id = 2;
            this.barEdit.ImageIndex = 19;
            this.barEdit.Name = "barEdit";
            this.barEdit.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdit_ItemClick);
            // 
            // barButtonDel
            // 
            this.barButtonDel.Caption = "删除";
            this.barButtonDel.Id = 3;
            this.barButtonDel.ImageIndex = 11;
            this.barButtonDel.Name = "barButtonDel";
            this.barButtonDel.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonDel_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "打印";
            this.barPrint.Id = 4;
            this.barPrint.ImageIndex = 5;
            this.barPrint.Name = "barPrint";
            this.barPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "导入";
            this.barButtonItem1.Id = 6;
            this.barButtonItem1.ImageIndex = 2;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "导出";
            this.barButtonItem2.Id = 7;
            this.barButtonItem2.ImageIndex = 4;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭";
            this.barClose.Id = 5;
            this.barClose.ImageIndex = 8;
            this.barClose.Name = "barClose";
            this.barClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "xxxx.ico");
            this.imageList1.Images.SetKeyName(1, "保存.ico");
            this.imageList1.Images.SetKeyName(2, "布局.ico");
            this.imageList1.Images.SetKeyName(3, "查询.ico");
            this.imageList1.Images.SetKeyName(4, "打回重新编.ico");
            this.imageList1.Images.SetKeyName(5, "打印.ico");
            this.imageList1.Images.SetKeyName(6, "发送.ico");
            this.imageList1.Images.SetKeyName(7, "关闭1.ico");
            this.imageList1.Images.SetKeyName(8, "关闭.ico");
            this.imageList1.Images.SetKeyName(9, "角色.ico");
            this.imageList1.Images.SetKeyName(10, "三等功发1.ico");
            this.imageList1.Images.SetKeyName(11, "删除.ico");
            this.imageList1.Images.SetKeyName(12, "审核.ico");
            this.imageList1.Images.SetKeyName(13, "审批.ico");
            this.imageList1.Images.SetKeyName(14, "授权.ico");
            this.imageList1.Images.SetKeyName(15, "刷新.ico");
            this.imageList1.Images.SetKeyName(16, "添加同级.ico");
            this.imageList1.Images.SetKeyName(17, "添加下级.ico");
            this.imageList1.Images.SetKeyName(18, "新建.ico");
            this.imageList1.Images.SetKeyName(19, "修改.ico");
            this.imageList1.Images.SetKeyName(20, "作废.ico");
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.EmbeddedNavigator.Name = "";
            this.gridControl.Location = new System.Drawing.Point(0, 33);
            this.gridControl.MainView = this.advBandedGridView1;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(741, 336);
            this.gridControl.TabIndex = 4;
            this.gridControl.UseEmbeddedNavigator = true;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView1});
            this.gridControl.Click += new System.EventHandler(this.gridControl_Click);
            // 
            // advBandedGridView1
            // 
            this.advBandedGridView1.AppearancePrint.BandPanel.Options.UseTextOptions = true;
            this.advBandedGridView1.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.advBandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand5,
            this.gridBand6,
            this.gridBand8});
            this.advBandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumn1,
            this.bandedGridColumn3,
            this.bandedGridColumn4,
            this.bandedGridColumn5});
            this.advBandedGridView1.GridControl = this.gridControl;
            this.advBandedGridView1.GroupPanelText = " ";
            this.advBandedGridView1.Name = "advBandedGridView1";
            this.advBandedGridView1.OptionsBehavior.Editable = false;
            this.advBandedGridView1.OptionsMenu.EnableColumnMenu = false;
            this.advBandedGridView1.OptionsPrint.PrintHeader = false;
            this.advBandedGridView1.OptionsPrint.UsePrintStyles = true;
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.Caption = "片区名称";
            this.gridBand1.Columns.Add(this.bandedGridColumn1);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.Width = 162;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.FieldName = "PQName";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.OptionsColumn.ShowCaption = false;
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.Width = 162;
            // 
            // gridBand5
            // 
            this.gridBand5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand5.Caption = "馈线（回）";
            this.gridBand5.Columns.Add(this.bandedGridColumn3);
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.Width = 171;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.FieldName = "Num1";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.Visible = true;
            this.bandedGridColumn3.Width = 171;
            // 
            // gridBand6
            // 
            this.gridBand6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand6.Caption = "开闭所（座）";
            this.gridBand6.Columns.Add(this.bandedGridColumn4);
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.Width = 147;
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.FieldName = "KBS";
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.Visible = true;
            this.bandedGridColumn4.Width = 147;
            // 
            // gridBand8
            // 
            this.gridBand8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand8.Caption = "柱上开关（台）";
            this.gridBand8.Columns.Add(this.bandedGridColumn5);
            this.gridBand8.Name = "gridBand8";
            this.gridBand8.Width = 188;
            // 
            // bandedGridColumn5
            // 
            this.bandedGridColumn5.FieldName = "KG";
            this.bandedGridColumn5.Name = "bandedGridColumn5";
            this.bandedGridColumn5.Visible = true;
            this.bandedGridColumn5.Width = 188;
            // 
            // gridBand24
            // 
            this.gridBand24.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand24.Caption = "线路名称";
            this.gridBand24.Name = "gridBand24";
            this.gridBand24.Width = 73;
            // 
            // gridBand25
            // 
            this.gridBand25.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand25.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand25.Caption = "主干长度";
            this.gridBand25.Name = "gridBand25";
            this.gridBand25.Width = 73;
            // 
            // gridBand19
            // 
            this.gridBand19.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand19.Caption = "架空线";
            this.gridBand19.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand20,
            this.gridBand21});
            this.gridBand19.Name = "gridBand19";
            this.gridBand19.Width = 162;
            // 
            // gridBand20
            // 
            this.gridBand20.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand20.Caption = "绝缘线";
            this.gridBand20.Name = "gridBand20";
            this.gridBand20.Width = 73;
            // 
            // gridBand21
            // 
            this.gridBand21.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand21.Caption = "裸导线";
            this.gridBand21.Name = "gridBand21";
            this.gridBand21.Width = 89;
            // 
            // gridBand3
            // 
            this.gridBand3.Caption = "变电站10kV出线";
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.Width = 130;
            // 
            // gridBand4
            // 
            this.gridBand4.Caption = "架空线";
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.Width = 117;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "局属",
            "专用",
            "全部"});
            this.comboBox1.Location = new System.Drawing.Point(192, 7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.Text = "局属";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // FrmPW3211
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 369);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmPW3211";
            this.Text = "市区10kV公用线路开关及环网柜统计情况";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmProject_Sum_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem barAdd;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        private DevExpress.XtraBars.BarButtonItem barButtonDel;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand19;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand20;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand21;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand8;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand24;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand25;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn5;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}