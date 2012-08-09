namespace ItopVector.Tools
{
    partial class frmLayerTreeManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayerTreeManager));
            this.btAdd = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btDel = new DevExpress.XtraEditors.SimpleButton();
            this.btEdit = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmClearLink = new System.Windows.Forms.ToolStripMenuItem();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.checkedListBoxControl2 = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.dateEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.button2 = new DevExpress.XtraEditors.SimpleButton();
            this.button1 = new DevExpress.XtraEditors.SimpleButton();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.增加方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除方案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.bbtaddfa = new DevExpress.XtraBars.BarButtonItem();
            this.bbteditfa = new DevExpress.XtraBars.BarButtonItem();
            this.bbtdelfa = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.bbtaddlayer = new DevExpress.XtraBars.BarButtonItem();
            this.bbteditlayer = new DevExpress.XtraBars.BarButtonItem();
            this.bbtdellayer = new DevExpress.XtraBars.BarButtonItem();
            this.bbtncopy = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnuion = new DevExpress.XtraBars.BarButtonItem();
            this.bbtnup = new DevExpress.XtraBars.BarButtonItem();
            this.bbtndown = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // btAdd
            // 
            this.btAdd.Image = ((System.Drawing.Image)(resources.GetObject("btAdd.Image")));
            this.btAdd.Location = new System.Drawing.Point(6, 108);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(36, 27);
            this.btAdd.TabIndex = 1;
            this.btAdd.Text = "增加";
            this.btAdd.ToolTip = "增加";
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Location = new System.Drawing.Point(232, -44);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(2, 336);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "groupControl1";
            this.groupControl1.Visible = false;
            // 
            // btDel
            // 
            this.btDel.Image = ((System.Drawing.Image)(resources.GetObject("btDel.Image")));
            this.btDel.Location = new System.Drawing.Point(90, 108);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(37, 27);
            this.btDel.TabIndex = 1;
            this.btDel.Text = "删除";
            this.btDel.ToolTip = "删除";
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // btEdit
            // 
            this.btEdit.Image = ((System.Drawing.Image)(resources.GetObject("btEdit.Image")));
            this.btEdit.Location = new System.Drawing.Point(47, 108);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(38, 27);
            this.btEdit.TabIndex = 1;
            this.btEdit.Text = "修改";
            this.btEdit.ToolTip = "修改";
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(241, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "被选中图层为\r\n当前操作图层";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label2.Location = new System.Drawing.Point(244, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "打钩为显示,\r\n不打钩为隐藏";
            this.label2.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全选ToolStripMenuItem,
            this.全消ToolStripMenuItem,
            this.tmClearLink});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 70);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.全选ToolStripMenuItem.Text = "全选";
            this.全选ToolStripMenuItem.Click += new System.EventHandler(this.全选ToolStripMenuItem_Click);
            // 
            // 全消ToolStripMenuItem
            // 
            this.全消ToolStripMenuItem.Name = "全消ToolStripMenuItem";
            this.全消ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.全消ToolStripMenuItem.Text = "全消";
            this.全消ToolStripMenuItem.Click += new System.EventHandler(this.全消ToolStripMenuItem_Click);
            // 
            // tmClearLink
            // 
            this.tmClearLink.Name = "tmClearLink";
            this.tmClearLink.Size = new System.Drawing.Size(118, 22);
            this.tmClearLink.Text = "清除关联";
            this.tmClearLink.Click += new System.EventHandler(this.清除关联ToolStripMenuItem_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(202, 108);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(37, 27);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "上移";
            this.simpleButton1.ToolTip = "上移";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(244, 108);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(37, 27);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "下移";
            this.simpleButton2.ToolTip = "下移";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.Image")));
            this.simpleButton3.Location = new System.Drawing.Point(132, 108);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(30, 27);
            this.simpleButton3.TabIndex = 1;
            this.simpleButton3.Text = "复制";
            this.simpleButton3.ToolTip = "复制";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton4.Image")));
            this.simpleButton4.Location = new System.Drawing.Point(167, 108);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(30, 27);
            this.simpleButton4.TabIndex = 6;
            this.simpleButton4.Text = "合并";
            this.simpleButton4.ToolTip = "合并";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // checkedListBoxControl2
            // 
            this.checkedListBoxControl2.CheckOnClick = true;
            this.checkedListBoxControl2.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("单层复制"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("多层复制"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("年图层复制")});
            this.checkedListBoxControl2.Location = new System.Drawing.Point(147, 26);
            this.checkedListBoxControl2.Name = "checkedListBoxControl2";
            this.checkedListBoxControl2.Size = new System.Drawing.Size(110, 65);
            this.checkedListBoxControl2.TabIndex = 8;
            this.checkedListBoxControl2.Visible = false;
            this.checkedListBoxControl2.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxControl2_ItemCheck);
            this.checkedListBoxControl2.Leave += new System.EventHandler(this.checkedListBoxControl2_Leave);
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(8, 36);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "数据有效时间";
            this.checkEdit1.Size = new System.Drawing.Size(121, 19);
            this.checkEdit1.TabIndex = 10;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 14);
            this.label3.TabIndex = 13;
            this.label3.Text = "起";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 14);
            this.label4.TabIndex = 13;
            this.label4.Text = "止";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(86, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 14);
            this.label5.TabIndex = 15;
            this.label5.Text = "年";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(86, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 14);
            this.label6.TabIndex = 15;
            this.label6.Text = "年";
            // 
            // dateEdit1
            // 
            this.dateEdit1.Location = new System.Drawing.Point(27, 57);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Mask.EditMask = "f0";
            this.dateEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.dateEdit1.Properties.MaxLength = 4;
            this.dateEdit1.Size = new System.Drawing.Size(56, 21);
            this.dateEdit1.TabIndex = 17;
            // 
            // dateEdit2
            // 
            this.dateEdit2.Location = new System.Drawing.Point(27, 82);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Mask.EditMask = "f0";
            this.dateEdit2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.dateEdit2.Properties.MaxLength = 4;
            this.dateEdit2.Size = new System.Drawing.Size(56, 21);
            this.dateEdit2.TabIndex = 17;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(106, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(43, 27);
            this.button2.TabIndex = 18;
            this.button2.Text = "确定";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(246, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 27);
            this.button1.TabIndex = 19;
            this.button1.Text = "参";
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.simpleButton5_Click_1);
            // 
            // treeList1
            // 
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn4});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.KeyFieldName = "SUID";
            this.treeList1.Location = new System.Drawing.Point(0, 26);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.DragNodes = true;
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsSelection.InvertSelection = true;
            this.treeList1.OptionsSelection.MultiSelect = true;
            this.treeList1.OptionsView.ShowCheckBoxes = true;
            this.treeList1.OptionsView.ShowHorzLines = false;
            this.treeList1.OptionsView.ShowIndicator = false;
            this.treeList1.OptionsView.ShowVertLines = false;
            this.treeList1.Size = new System.Drawing.Size(330, 388);
            this.treeList1.TabIndex = 20;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "图层名称";
            this.treeListColumn1.FieldName = "NAME";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "treeListColumn2";
            this.treeListColumn2.FieldName = "SUID";
            this.treeListColumn2.Name = "treeListColumn2";
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "treeListColumn3";
            this.treeListColumn3.FieldName = "ParentID";
            this.treeListColumn3.Name = "treeListColumn3";
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "OrderID";
            this.treeListColumn4.FieldName = "OrderID";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.Visible = true;
            this.treeListColumn4.VisibleIndex = 1;
            this.treeListColumn4.Width = 20;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripMenuItem1,
            this.增加方案ToolStripMenuItem,
            this.修改方案ToolStripMenuItem,
            this.删除方案ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(119, 98);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Enabled = false;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuItem3.Text = "清除关联";
            this.toolStripMenuItem3.Visible = false;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(115, 6);
            // 
            // 增加方案ToolStripMenuItem
            // 
            this.增加方案ToolStripMenuItem.Name = "增加方案ToolStripMenuItem";
            this.增加方案ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.增加方案ToolStripMenuItem.Text = "增加方案";
            this.增加方案ToolStripMenuItem.Click += new System.EventHandler(this.增加方案ToolStripMenuItem_Click);
            // 
            // 修改方案ToolStripMenuItem
            // 
            this.修改方案ToolStripMenuItem.Name = "修改方案ToolStripMenuItem";
            this.修改方案ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.修改方案ToolStripMenuItem.Text = "修改方案";
            this.修改方案ToolStripMenuItem.Click += new System.EventHandler(this.修改方案ToolStripMenuItem_Click);
            // 
            // 删除方案ToolStripMenuItem
            // 
            this.删除方案ToolStripMenuItem.Name = "删除方案ToolStripMenuItem";
            this.删除方案ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.删除方案ToolStripMenuItem.Text = "删除方案";
            this.删除方案ToolStripMenuItem.Click += new System.EventHandler(this.删除方案ToolStripMenuItem_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(247, 286);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(120, 89);
            this.checkedListBox1.TabIndex = 21;
            this.checkedListBox1.Visible = false;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageList3;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItem1,
            this.barSubItem2,
            this.bbtncopy,
            this.bbtnuion,
            this.bbtnup,
            this.bbtndown,
            this.bbtaddfa,
            this.bbteditfa,
            this.bbtdelfa,
            this.bbtaddlayer,
            this.bbteditlayer,
            this.bbtdellayer});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 12;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbtncopy),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem1, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barSubItem2, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbtnuion),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbtnup),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbtndown)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "方案管理";
            this.barSubItem1.Id = 0;
            this.barSubItem1.ImageIndex = 5;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbtaddfa),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbteditfa),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbtdelfa)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // bbtaddfa
            // 
            this.bbtaddfa.Caption = "增加";
            this.bbtaddfa.Id = 6;
            this.bbtaddfa.ImageIndex = 7;
            this.bbtaddfa.Name = "bbtaddfa";
            this.bbtaddfa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtaddfa_ItemClick);
            // 
            // bbteditfa
            // 
            this.bbteditfa.Caption = "修改";
            this.bbteditfa.Id = 7;
            this.bbteditfa.ImageIndex = 11;
            this.bbteditfa.Name = "bbteditfa";
            this.bbteditfa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbteditfa_ItemClick);
            // 
            // bbtdelfa
            // 
            this.bbtdelfa.Caption = "删除";
            this.bbtdelfa.Id = 8;
            this.bbtdelfa.ImageIndex = 12;
            this.bbtdelfa.Name = "bbtdelfa";
            this.bbtdelfa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtdelfa_ItemClick);
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "图层管理";
            this.barSubItem2.Id = 1;
            this.barSubItem2.ImageIndex = 8;
            this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbtaddlayer),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbteditlayer),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbtdellayer)});
            this.barSubItem2.Name = "barSubItem2";
            // 
            // bbtaddlayer
            // 
            this.bbtaddlayer.Caption = "增加";
            this.bbtaddlayer.Id = 9;
            this.bbtaddlayer.ImageIndex = 7;
            this.bbtaddlayer.Name = "bbtaddlayer";
            this.bbtaddlayer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtaddlayer_ItemClick);
            // 
            // bbteditlayer
            // 
            this.bbteditlayer.Caption = "修改";
            this.bbteditlayer.Id = 10;
            this.bbteditlayer.ImageIndex = 11;
            this.bbteditlayer.Name = "bbteditlayer";
            this.bbteditlayer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbteditlayer_ItemClick);
            // 
            // bbtdellayer
            // 
            this.bbtdellayer.Caption = "删除";
            this.bbtdellayer.Id = 11;
            this.bbtdellayer.ImageIndex = 12;
            this.bbtdellayer.Name = "bbtdellayer";
            this.bbtdellayer.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtdellayer_ItemClick);
            // 
            // bbtncopy
            // 
            this.bbtncopy.Caption = "复制";
            this.bbtncopy.Hint = "复制";
            this.bbtncopy.Id = 2;
            this.bbtncopy.ImageIndex = 3;
            this.bbtncopy.Name = "bbtncopy";
            this.bbtncopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbrncopy_ItemClick);
            // 
            // bbtnuion
            // 
            this.bbtnuion.Caption = "合并";
            this.bbtnuion.Hint = "合并";
            this.bbtnuion.Id = 3;
            this.bbtnuion.ImageIndex = 4;
            this.bbtnuion.Name = "bbtnuion";
            this.bbtnuion.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnuion_ItemClick);
            // 
            // bbtnup
            // 
            this.bbtnup.Caption = "上移";
            this.bbtnup.Hint = "上移";
            this.bbtnup.Id = 4;
            this.bbtnup.ImageIndex = 0;
            this.bbtnup.Name = "bbtnup";
            this.bbtnup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtnup_ItemClick);
            // 
            // bbtndown
            // 
            this.bbtndown.Caption = "下移";
            this.bbtndown.Hint = "下移";
            this.bbtndown.Id = 5;
            this.bbtndown.ImageIndex = 2;
            this.bbtndown.Name = "bbtndown";
            this.bbtndown.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbtndown_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(330, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 414);
            this.barDockControlBottom.Size = new System.Drawing.Size(330, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 388);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(330, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 388);
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList3.Images.SetKeyName(0, "blue_arrow_up_32.png");
            this.imageList3.Images.SetKeyName(1, "web_layout_error_32_close.png");
            this.imageList3.Images.SetKeyName(2, "blue_arrow_down_32.png");
            this.imageList3.Images.SetKeyName(3, "kate.ico");
            this.imageList3.Images.SetKeyName(4, "kcmdf.ico");
            this.imageList3.Images.SetKeyName(5, "package_development.ico");
            this.imageList3.Images.SetKeyName(6, "web_layout_chart_32.png");
            this.imageList3.Images.SetKeyName(7, "add_32.png");
            this.imageList3.Images.SetKeyName(8, "activity_monitor_add.png");
            this.imageList3.Images.SetKeyName(9, "Users.ico");
            this.imageList3.Images.SetKeyName(10, "close.ico");
            this.imageList3.Images.SetKeyName(11, "Edit1.png");
            this.imageList3.Images.SetKeyName(12, "Del.png");
            // 
            // frmLayerTreeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 414);
            this.Controls.Add(this.checkedListBoxControl2);
            this.Controls.Add(this.treeList1);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dateEdit2);
            this.Controls.Add(this.dateEdit1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btEdit);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLayerTreeManager";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "图层管理";
            this.Load += new System.EventHandler(this.frmLayerManager_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLayerManager_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btAdd;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btDel;
        private DevExpress.XtraEditors.SimpleButton btEdit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;       
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全消ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tmClearLink;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit dateEdit1;
        private DevExpress.XtraEditors.TextEdit dateEdit2;
        private DevExpress.XtraEditors.SimpleButton button2;
        private DevExpress.XtraEditors.SimpleButton button1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        public System.Windows.Forms.CheckedListBox checkedListBox1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem 增加方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除方案ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarButtonItem bbtncopy;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbtaddfa;
        private DevExpress.XtraBars.BarButtonItem bbteditfa;
        private DevExpress.XtraBars.BarButtonItem bbtdelfa;
        private DevExpress.XtraBars.BarButtonItem bbtaddlayer;
        private DevExpress.XtraBars.BarButtonItem bbteditlayer;
        private DevExpress.XtraBars.BarButtonItem bbtdellayer;
        private DevExpress.XtraBars.BarButtonItem bbtnuion;
        private DevExpress.XtraBars.BarButtonItem bbtnup;
        private DevExpress.XtraBars.BarButtonItem bbtndown;
        private System.Windows.Forms.ImageList imageList3;
    }
}