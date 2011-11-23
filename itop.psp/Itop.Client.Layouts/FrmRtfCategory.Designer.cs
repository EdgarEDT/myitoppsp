namespace Itop.Client.Layouts
{
    partial class FrmRtfCategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRtfCategory));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.ChapterNameTextEdit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.RemarkMemoExEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.dsoFramerWordControl2 = new TONLI.BZH.UI.DSOFramerWordControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.ctrlRtfAttachFiles1 = new Itop.Client.Layouts.CtrlRtfAttachFiles();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barList = new DevExpress.XtraBars.BarSubItem();
            this.barAdditem = new DevExpress.XtraBars.BarButtonItem();
            this.barAdd1item = new DevExpress.XtraBars.BarButtonItem();
            this.barEdititem = new DevExpress.XtraBars.BarButtonItem();
            this.barDelitem = new DevExpress.XtraBars.BarButtonItem();
            this.barSelect = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChapterNameTextEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemarkMemoExEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 34);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeList1);
            this.splitContainerControl1.Panel1.Text = "splitContainerControl1_Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.xtraTabControl1);
            this.splitContainerControl1.Panel2.Text = "splitContainerControl1_Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(679, 445);
            this.splitContainerControl1.SplitterPosition = 276;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // treeList1
            // 
            this.treeList1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.treeList1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn3,
            this.treeListColumn4,
            this.treeListColumn2});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.KeyFieldName = "UID";
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ChapterNameTextEdit,
            this.RemarkMemoExEdit,
            this.repositoryItemTextEdit1});
            this.treeList1.Size = new System.Drawing.Size(270, 439);
            this.treeList1.TabIndex = 0;
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "目录名称";
            this.treeListColumn1.ColumnEdit = this.ChapterNameTextEdit;
            this.treeListColumn1.FieldName = "Title";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.VisibleIndex = 0;
            this.treeListColumn1.Width = 153;
            // 
            // ChapterNameTextEdit
            // 
            this.ChapterNameTextEdit.AutoHeight = false;
            this.ChapterNameTextEdit.MaxLength = 100;
            this.ChapterNameTextEdit.Name = "ChapterNameTextEdit";
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
            // treeListColumn2
            // 
            this.treeListColumn2.AppearanceCell.Options.UseTextOptions = true;
            this.treeListColumn2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.treeListColumn2.Caption = "序号";
            this.treeListColumn2.ColumnEdit = this.repositoryItemTextEdit1;
            this.treeListColumn2.FieldName = "SortNo";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.OptionsColumn.AllowEdit = false;
            this.treeListColumn2.OptionsColumn.ShowInCustomizationForm = false;
            this.treeListColumn2.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.treeListColumn2.Width = 101;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.DisplayFormat.FormatString = "n0";
            this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
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
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(393, 439);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.dsoFramerWordControl2);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(389, 412);
            this.xtraTabPage1.Text = "文字资料";
            // 
            // dsoFramerWordControl2
            // 
            this.dsoFramerWordControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dsoFramerWordControl2.Location = new System.Drawing.Point(0, 0);
            this.dsoFramerWordControl2.Name = "dsoFramerWordControl2";
            this.dsoFramerWordControl2.Size = new System.Drawing.Size(389, 412);
            this.dsoFramerWordControl2.TabIndex = 2;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.ctrlRtfAttachFiles1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(389, 412);
            this.xtraTabPage2.Text = "多媒体";
            // 
            // ctrlRtfAttachFiles1
            // 
            this.ctrlRtfAttachFiles1.AllowUpdate = true;
            this.ctrlRtfAttachFiles1.Category = "";
            this.ctrlRtfAttachFiles1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlRtfAttachFiles1.Location = new System.Drawing.Point(0, 0);
            this.ctrlRtfAttachFiles1.Name = "ctrlRtfAttachFiles1";
            this.ctrlRtfAttachFiles1.Size = new System.Drawing.Size(389, 412);
            this.ctrlRtfAttachFiles1.TabIndex = 0;
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
            this.barSelect,
            this.barPrint,
            this.barButtonItem3,
            this.barButtonItem2});
            this.barManager1.MaxItemId = 22;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.barSelect),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barPrint),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
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
            this.barList.ImageIndex = 8;
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
            this.barEdititem.ImageIndex = 15;
            this.barEdititem.Name = "barEdititem";
            this.barEdititem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdititem_ItemClick);
            // 
            // barDelitem
            // 
            this.barDelitem.Caption = "删除";
            this.barDelitem.Id = 16;
            this.barDelitem.ImageIndex = 6;
            this.barDelitem.Name = "barDelitem";
            this.barDelitem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDelitem_ItemClick);
            // 
            // barSelect
            // 
            this.barSelect.Caption = "文档选择";
            this.barSelect.Id = 17;
            this.barSelect.ImageIndex = 16;
            this.barSelect.Name = "barSelect";
            this.barSelect.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barSelect.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barSelect_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "打印设置";
            this.barButtonItem3.Id = 20;
            this.barButtonItem3.ImageIndex = 13;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barPrint
            // 
            this.barPrint.Caption = "打印";
            this.barPrint.Id = 18;
            this.barPrint.ImageIndex = 7;
            this.barPrint.Name = "barPrint";
            this.barPrint.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barPrint_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "设置格式";
            this.barButtonItem2.Id = 21;
            this.barButtonItem2.ImageIndex = 15;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick_1);
            // 
            // barClose
            // 
            this.barClose.Caption = "关闭";
            this.barClose.Id = 5;
            this.barClose.ImageIndex = 14;
            this.barClose.Name = "barClose";
            this.barClose.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barClose_ItemClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "新建.ico");
            this.imageList1.Images.SetKeyName(1, "添加下级.ico");
            this.imageList1.Images.SetKeyName(2, "添加同级.ico");
            this.imageList1.Images.SetKeyName(3, "关闭.ico");
            this.imageList1.Images.SetKeyName(4, "审批.ico");
            this.imageList1.Images.SetKeyName(5, "修改.ico");
            this.imageList1.Images.SetKeyName(6, "删除.ico");
            this.imageList1.Images.SetKeyName(7, "打印.ico");
            this.imageList1.Images.SetKeyName(8, "books.ico");
            this.imageList1.Images.SetKeyName(9, "关闭.ico");
            this.imageList1.Images.SetKeyName(10, "审批.ico");
            this.imageList1.Images.SetKeyName(11, "新建.ico");
            this.imageList1.Images.SetKeyName(12, "修改.ico");
            this.imageList1.Images.SetKeyName(13, "ftp.ico");
            this.imageList1.Images.SetKeyName(14, "关闭.ico");
            this.imageList1.Images.SetKeyName(15, "修改.ico");
            this.imageList1.Images.SetKeyName(16, "审批.ico");
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(172, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmRtfCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 479);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FrmRtfCategory";
            this.Text = "文字资料";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLayoutContents_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmRtfCategory_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChapterNameTextEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemarkMemoExEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
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
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem barSelect;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private CtrlRtfAttachFiles ctrlRtfAttachFiles1;
        private DevExpress.XtraBars.BarButtonItem barPrint;
        private System.Windows.Forms.Button button1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private TONLI.BZH.UI.DSOFramerWordControl dsoFramerWordControl2;
    }
}