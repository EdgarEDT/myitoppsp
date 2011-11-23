namespace Itop.Client.Layouts
{
    partial class FrmTypes_Excel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTypes_Excel));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ChapterNameTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.RemarkMemoExEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.dsoExcelControl1 = new Itop.Client.Layouts.DSOExcelControl();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barList = new DevExpress.XtraBars.BarSubItem();
            this.barAdditem = new DevExpress.XtraBars.BarButtonItem();
            this.barAdd1item = new DevExpress.XtraBars.BarButtonItem();
            this.barEdititem = new DevExpress.XtraBars.BarButtonItem();
            this.barDelitem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barSave = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChapterNameTextEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemarkMemoExEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 33);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeList1);
            this.splitContainerControl1.Panel1.Text = "splitContainerControl1_Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.dsoExcelControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.textBox1);
            this.splitContainerControl1.Panel2.Text = "splitContainerControl1_Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(822, 454);
            this.splitContainerControl1.SplitterPosition = 199;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // treeList1
            // 
            this.treeList1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.treeList1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn4,
            this.treeListColumn5});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.KeyFieldName = "UID";
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.AutoFocusNewNode = true;
            this.treeList1.OptionsSelection.InvertSelection = true;
            this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ChapterNameTextEdit,
            this.RemarkMemoExEdit});
            this.treeList1.Size = new System.Drawing.Size(191, 446);
            this.treeList1.TabIndex = 0;
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "目录";
            this.treeListColumn1.ColumnEdit = this.ChapterNameTextEdit;
            this.treeListColumn1.FieldName = "Title";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // ChapterNameTextEdit
            // 
            this.ChapterNameTextEdit.AutoHeight = false;
            this.ChapterNameTextEdit.MaxLength = 100;
            this.ChapterNameTextEdit.Name = "ChapterNameTextEdit";
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "备注";
            this.treeListColumn2.ColumnEdit = this.RemarkMemoExEdit;
            this.treeListColumn2.FieldName = "Remark";
            this.treeListColumn2.Name = "treeListColumn2";
            // 
            // RemarkMemoExEdit
            // 
            this.RemarkMemoExEdit.AutoHeight = false;
            this.RemarkMemoExEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.RemarkMemoExEdit.MaxLength = 300;
            this.RemarkMemoExEdit.Name = "RemarkMemoExEdit";
            this.RemarkMemoExEdit.ReadOnly = true;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "UID";
            this.treeListColumn3.FieldName = "UID";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "ParentId";
            this.treeListColumn4.FieldName = "ParentID";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "创建日期";
            this.treeListColumn5.FieldName = "CreateDate";
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.OptionsColumn.ShowInCustomizationForm = false;
            this.treeListColumn5.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            // 
            // dsoExcelControl1
            // 
            this.dsoExcelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dsoExcelControl1.IsDispalyMenuBar = true;
            this.dsoExcelControl1.IsDispalyTitleBar = true;
            this.dsoExcelControl1.IsDispalyToolBar = true;
            this.dsoExcelControl1.Location = new System.Drawing.Point(0, 0);
            this.dsoExcelControl1.Name = "dsoExcelControl1";
            this.dsoExcelControl1.Size = new System.Drawing.Size(611, 446);
            this.dsoExcelControl1.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(118, -385);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 1;
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
            this.barButtonItem1,
            this.barClose,
            this.barList,
            this.barAdditem,
            this.barAdd1item,
            this.barEdititem,
            this.barDelitem,
            this.barButtonItem2,
            this.barPrint,
            this.barSave,
            this.barButtonItem3,
            this.barButtonItem4});
            this.barManager1.MaxItemId = 28;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBox1});
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 1";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barList, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barClose)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.RotateWhenVertical = false;
            this.bar1.Text = "Custom 1";
            // 
            // barList
            // 
            this.barList.Caption = "目录维护";
            this.barList.Id = 12;
            this.barList.ImageIndex = 0;
            this.barList.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAdditem),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAdd1item),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEdititem),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelitem)});
            this.barList.Name = "barList";
            // 
            // barAdditem
            // 
            this.barAdditem.Caption = "添加目录";
            this.barAdditem.Id = 13;
            this.barAdditem.ImageIndex = 6;
            this.barAdditem.Name = "barAdditem";
            this.barAdditem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdditem_ItemClick);
            // 
            // barAdd1item
            // 
            this.barAdd1item.Caption = "添加子目录";
            this.barAdd1item.Id = 14;
            this.barAdd1item.ImageIndex = 7;
            this.barAdd1item.Name = "barAdd1item";
            this.barAdd1item.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd1item_ItemClick);
            // 
            // barEdititem
            // 
            this.barEdititem.Caption = "修改目录";
            this.barEdititem.Id = 15;
            this.barEdititem.ImageIndex = 9;
            this.barEdititem.Name = "barEdititem";
            this.barEdititem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdititem_ItemClick);
            // 
            // barDelitem
            // 
            this.barDelitem.Caption = "删除目录";
            this.barDelitem.Id = 16;
            this.barDelitem.ImageIndex = 1;
            this.barDelitem.Name = "barDelitem";
            this.barDelitem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelitem_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "导入Excel";
            this.barButtonItem3.Id = 24;
            this.barButtonItem3.ImageIndex = 15;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "导出Excel";
            this.barButtonItem4.Id = 27;
            this.barButtonItem4.ImageIndex = 17;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barSave
            // 
            this.barSave.Caption = "保存";
            this.barSave.Id = 22;
            this.barSave.ImageIndex = 12;
            this.barSave.Name = "barSave";
            this.barSave.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSave_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "选择";
            this.barPrint.Id = 19;
            this.barPrint.ImageIndex = 3;
            this.barPrint.Name = "barPrint";
            this.barPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭";
            this.barClose.Id = 5;
            this.barClose.ImageIndex = 18;
            this.barClose.Name = "barClose";
            this.barClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "三等功发1.ico");
            this.imageList1.Images.SetKeyName(1, "删除.ico");
            this.imageList1.Images.SetKeyName(2, "审核.ico");
            this.imageList1.Images.SetKeyName(3, "审批.ico");
            this.imageList1.Images.SetKeyName(4, "授权.ico");
            this.imageList1.Images.SetKeyName(5, "刷新.ico");
            this.imageList1.Images.SetKeyName(6, "添加同级.ico");
            this.imageList1.Images.SetKeyName(7, "添加下级.ico");
            this.imageList1.Images.SetKeyName(8, "新建.ico");
            this.imageList1.Images.SetKeyName(9, "修改.ico");
            this.imageList1.Images.SetKeyName(10, "作废.ico");
            this.imageList1.Images.SetKeyName(11, "xxxx.ico");
            this.imageList1.Images.SetKeyName(12, "保存.ico");
            this.imageList1.Images.SetKeyName(13, "布局.ico");
            this.imageList1.Images.SetKeyName(14, "查询.ico");
            this.imageList1.Images.SetKeyName(15, "打回重新编.ico");
            this.imageList1.Images.SetKeyName(16, "打印.ico");
            this.imageList1.Images.SetKeyName(17, "发送.ico");
            this.imageList1.Images.SetKeyName(18, "关闭.ico");
            this.imageList1.Images.SetKeyName(19, "角色.ico");
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "添加文字资料";
            this.barButtonItem2.Id = 17;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FrmTypes_Excel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 487);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmTypes_Excel";
            this.Text = "负荷特性分析";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmEconomyAnalysis_FormClosing);
            this.Load += new System.EventHandler(this.FrmLayoutContents_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChapterNameTextEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemarkMemoExEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ChapterNameTextEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit RemarkMemoExEdit;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barClose;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.BarSubItem barList;
        private DevExpress.XtraBars.BarButtonItem barAdditem;
        private DevExpress.XtraBars.BarButtonItem barAdd1item;
        private DevExpress.XtraBars.BarButtonItem barEdititem;
        private DevExpress.XtraBars.BarButtonItem barDelitem;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraBars.BarButtonItem barSave;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ImageList imageList2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DSOExcelControl dsoExcelControl1;
    }
}