namespace Itop.Client.Layouts
{
    partial class FrmLayoutContents
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ChapterNameTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.RemarkMemoExEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.dsoFramerWordControl1 = new TONLI.BZH.UI.DSOFramerWordControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barList = new DevExpress.XtraBars.BarSubItem();
            this.barAdditem = new DevExpress.XtraBars.BarButtonItem();
            this.barAdd1item = new DevExpress.XtraBars.BarButtonItem();
            this.barEdititem = new DevExpress.XtraBars.BarButtonItem();
            this.barDelitem = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem44 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem8 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem29 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem30 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem26 = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
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
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 21);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeList1);
            this.splitContainerControl1.Panel1.Text = "splitContainerControl1_Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.dsoFramerWordControl1);
            this.splitContainerControl1.Panel2.Text = "splitContainerControl1_Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(679, 458);
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
            this.treeList1.OptionsBehavior.AutoMoveRowFocus = true;
            this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ChapterNameTextEdit,
            this.RemarkMemoExEdit});
            this.treeList1.Size = new System.Drawing.Size(193, 452);
            this.treeList1.TabIndex = 0;
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "章节名称";
            this.treeListColumn1.ColumnEdit = this.ChapterNameTextEdit;
            this.treeListColumn1.FieldName = "ChapterName";
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
            this.treeListColumn4.FieldName = "ParentId";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "treeListColumn5";
            this.treeListColumn5.FieldName = "CreateDate";
            this.treeListColumn5.Name = "treeListColumn5";
            this.treeListColumn5.OptionsColumn.ShowInCustomizationForm = false;
            this.treeListColumn5.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            // 
            // dsoFramerWordControl1
            // 
            this.dsoFramerWordControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dsoFramerWordControl1.Location = new System.Drawing.Point(0, 0);
            this.dsoFramerWordControl1.Name = "dsoFramerWordControl1";
            this.dsoFramerWordControl1.Size = new System.Drawing.Size(470, 452);
            this.dsoFramerWordControl1.TabIndex = 2;
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
            this.barPrint,
            this.barButtonItem26,
            this.barSubItem8,
            this.barButtonItem29,
            this.barButtonItem30,
            this.barButtonItem44,
            this.barButtonItem2});
            this.barManager1.MaxItemId = 80;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem44, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem8, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem26, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrint, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barClose, true)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.RotateWhenVertical = false;
            this.bar1.Text = "Custom 1";
            // 
            // barList
            // 
            this.barList.Caption = "章节维护";
            this.barList.Id = 12;
            this.barList.ImageIndex = 13;
            this.barList.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAdditem),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAdd1item),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEdititem),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDelitem)});
            this.barList.Name = "barList";
            // 
            // barAdditem
            // 
            this.barAdditem.Caption = "添加同级";
            this.barAdditem.Id = 13;
            this.barAdditem.ImageIndex = 2;
            this.barAdditem.Name = "barAdditem";
            this.barAdditem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdditem_ItemClick);
            // 
            // barAdd1item
            // 
            this.barAdd1item.Caption = "添加下级";
            this.barAdd1item.Id = 14;
            this.barAdd1item.ImageIndex = 1;
            this.barAdd1item.Name = "barAdd1item";
            this.barAdd1item.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAdd1item_ItemClick);
            // 
            // barEdititem
            // 
            this.barEdititem.Caption = "修改";
            this.barEdititem.Id = 15;
            this.barEdititem.ImageIndex = 22;
            this.barEdititem.Name = "barEdititem";
            this.barEdititem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdititem_ItemClick);
            // 
            // barDelitem
            // 
            this.barDelitem.Caption = "删除";
            this.barDelitem.Id = 16;
            this.barDelitem.ImageIndex = 3;
            this.barDelitem.Name = "barDelitem";
            this.barDelitem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelitem_ItemClick);
            // 
            // barButtonItem44
            // 
            this.barButtonItem44.Caption = "设置封面";
            this.barButtonItem44.Id = 73;
            this.barButtonItem44.ImageIndex = 22;
            this.barButtonItem44.Name = "barButtonItem44";
            this.barButtonItem44.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem44.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem44_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "设置封底";
            this.barButtonItem2.Id = 79;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barSubItem8
            // 
            this.barSubItem8.Caption = "导出";
            this.barSubItem8.Id = 53;
            this.barSubItem8.ImageIndex = 19;
            this.barSubItem8.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem29),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem30)});
            this.barSubItem8.Name = "barSubItem8";
            this.barSubItem8.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // barButtonItem29
            // 
            this.barButtonItem29.Caption = "章节导出";
            this.barButtonItem29.Id = 54;
            this.barButtonItem29.ImageIndex = 21;
            this.barButtonItem29.Name = "barButtonItem29";
            this.barButtonItem29.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem29.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem29_ItemClick);
            // 
            // barButtonItem30
            // 
            this.barButtonItem30.Caption = "全部导出";
            this.barButtonItem30.Id = 55;
            this.barButtonItem30.ImageIndex = 21;
            this.barButtonItem30.Name = "barButtonItem30";
            this.barButtonItem30.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem30.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem30_ItemClick);
            // 
            // barButtonItem26
            // 
            this.barButtonItem26.Caption = "打印设置";
            this.barButtonItem26.Id = 50;
            this.barButtonItem26.ImageIndex = 15;
            this.barButtonItem26.Name = "barButtonItem26";
            this.barButtonItem26.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem26.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem26.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem26_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "打印";
            this.barPrint.Id = 19;
            this.barPrint.ImageIndex = 6;
            this.barPrint.Name = "barPrint";
            this.barPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭";
            this.barClose.Id = 5;
            this.barClose.ImageIndex = 20;
            this.barClose.Name = "barClose";
            this.barClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Id = 78;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // FrmLayoutContents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 479);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmLayoutContents";
            this.Text = "章节维护";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLayoutContents_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLayoutContents_FormClosing);
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
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private DevExpress.XtraBars.BarSubItem barSubItem8;
        private DevExpress.XtraBars.BarButtonItem barButtonItem26;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem29;
        private DevExpress.XtraBars.BarButtonItem barButtonItem30;
        private TONLI.BZH.UI.DSOFramerWordControl dsoFramerWordControl1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem44;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;

    }
}