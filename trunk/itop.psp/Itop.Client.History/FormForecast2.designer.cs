namespace Itop.Client.History
{
    partial class FormForecast2
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormForecast2));
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barSubItem11 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.btCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.bt434 = new DevExpress.XtraBars.BarButtonItem();
            this.bt513 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem20 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem21 = new DevExpress.XtraBars.BarButtonItem();
            this.btGetHistory = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.bt_add = new DevExpress.XtraBars.BarButtonItem();
            this.bt_edit = new DevExpress.XtraBars.BarButtonItem();
            this.bt_del = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeList2 = new DevExpress.XtraTreeList.TreeList();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
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
            this.barSubItem3,
            this.barButtonItem20,
            this.barButtonItem21,
            this.btGetHistory,
            this.barButtonItem6,
            this.barSubItem11,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.bt_add,
            this.bt_edit,
            this.bt_del,
            this.barSubItem2,
            this.bt434,
            this.btCopy,
            this.bt513,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barButtonItem7,
            this.barButtonItem8,
            this.barButtonItem9});
            this.barManager1.MaxItemId = 74;
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 1";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem11),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem2, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.btGetHistory, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem9, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem8, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 1";
            // 
            // barSubItem11
            // 
            this.barSubItem11.Caption = "�������";
            this.barSubItem11.Id = 46;
            this.barSubItem11.ImageIndex = 13;
            this.barSubItem11.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.btCopy)});
            this.barSubItem11.Name = "barSubItem11";
            this.barSubItem11.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSubItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "���";
            this.barButtonItem1.Id = 47;
            this.barButtonItem1.ImageIndex = 17;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "�޸�";
            this.barButtonItem3.Id = 56;
            this.barButtonItem3.ImageIndex = 18;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "ɾ��";
            this.barButtonItem2.Id = 48;
            this.barButtonItem2.ImageIndex = 10;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // btCopy
            // 
            this.btCopy.Caption = "����";
            this.btCopy.Id = 66;
            this.btCopy.ImageIndex = 15;
            this.btCopy.Name = "btCopy";
            this.btCopy.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btCopy_ItemClick);
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "ͳ��";
            this.barSubItem2.Id = 63;
            this.barSubItem2.ImageIndex = 11;
            this.barSubItem2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bt434),
            new DevExpress.XtraBars.LinkPersistInfo(this.bt513)});
            this.barSubItem2.Name = "barSubItem2";
            this.barSubItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // bt434
            // 
            this.bt434.Caption = "ȫ�е�������Ԥ����";
            this.bt434.Id = 64;
            this.bt434.Name = "bt434";
            this.bt434.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bt434_ItemClick);
            // 
            // bt513
            // 
            this.bt513.Caption = "�����ص�������Ԥ����";
            this.bt513.Id = 67;
            this.bt513.Name = "bt513";
            this.bt513.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bt532_ItemClick);
            // 
            // barSubItem3
            // 
            this.barSubItem3.Caption = "����";
            this.barSubItem3.Id = 29;
            this.barSubItem3.ImageIndex = 5;
            this.barSubItem3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem20),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem21)});
            this.barSubItem3.Name = "barSubItem3";
            this.barSubItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barSubItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barButtonItem20
            // 
            this.barButtonItem20.Caption = "��������";
            this.barButtonItem20.Id = 30;
            this.barButtonItem20.ImageIndex = 5;
            this.barButtonItem20.Name = "barButtonItem20";
            this.barButtonItem20.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem20.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem20_ItemClick);
            // 
            // barButtonItem21
            // 
            this.barButtonItem21.Caption = "����ͼ��";
            this.barButtonItem21.Id = 31;
            this.barButtonItem21.ImageIndex = 5;
            this.barButtonItem21.Name = "barButtonItem21";
            this.barButtonItem21.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem21.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem21.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem21_ItemClick);
            // 
            // btGetHistory
            // 
            this.btGetHistory.Caption = "ˢ����ʷ����";
            this.btGetHistory.Id = 32;
            this.btGetHistory.ImageIndex = 18;
            this.btGetHistory.Name = "btGetHistory";
            this.btGetHistory.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.btGetHistory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btGetHistory_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "�鿴ͼ��";
            this.barButtonItem5.Id = 69;
            this.barButtonItem5.ImageIndex = 9;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // barButtonItem9
            // 
            this.barButtonItem9.Caption = "��Ӧ�󳣹�+�û�Ԥ�ⷽ��";
            this.barButtonItem9.Id = 73;
            this.barButtonItem9.ImageIndex = 11;
            this.barButtonItem9.Name = "barButtonItem9";
            this.barButtonItem9.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem9.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem9_ItemClick);
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Caption = "��ȡ��ӦԤ��ֵ";
            this.barButtonItem8.Id = 72;
            this.barButtonItem8.ImageIndex = 14;
            this.barButtonItem8.Name = "barButtonItem8";
            this.barButtonItem8.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem8_ItemClick);
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "�ر�";
            this.barButtonItem6.Id = 35;
            this.barButtonItem6.ImageIndex = 7;
            this.barButtonItem6.Name = "barButtonItem6";
            this.barButtonItem6.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem6_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "���������ʼ���";
            this.barButtonItem4.Id = 68;
            this.barButtonItem4.ImageIndex = 9;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "����.ico");
            this.imageList1.Images.SetKeyName(1, "����.ico");
            this.imageList1.Images.SetKeyName(2, "��ѯ.ico");
            this.imageList1.Images.SetKeyName(3, "������±�.ico");
            this.imageList1.Images.SetKeyName(4, "��ӡ.ico");
            this.imageList1.Images.SetKeyName(5, "����.ico");
            this.imageList1.Images.SetKeyName(6, "�ر�1.ico");
            this.imageList1.Images.SetKeyName(7, "�ر�.ico");
            this.imageList1.Images.SetKeyName(8, "��ɫ.ico");
            this.imageList1.Images.SetKeyName(9, "���ȹ���1.ico");
            this.imageList1.Images.SetKeyName(10, "ɾ��.ico");
            this.imageList1.Images.SetKeyName(11, "���.ico");
            this.imageList1.Images.SetKeyName(12, "����.ico");
            this.imageList1.Images.SetKeyName(13, "��Ȩ.ico");
            this.imageList1.Images.SetKeyName(14, "ˢ��.ico");
            this.imageList1.Images.SetKeyName(15, "���ͬ��.ico");
            this.imageList1.Images.SetKeyName(16, "����¼�.ico");
            this.imageList1.Images.SetKeyName(17, "�½�.ico");
            this.imageList1.Images.SetKeyName(18, "�޸�.ico");
            this.imageList1.Images.SetKeyName(19, "����.ico");
            // 
            // bt_add
            // 
            this.bt_add.Caption = "���";
            this.bt_add.Id = 59;
            this.bt_add.ImageIndex = 17;
            this.bt_add.Name = "bt_add";
            // 
            // bt_edit
            // 
            this.bt_edit.Caption = "�޸�";
            this.bt_edit.Id = 60;
            this.bt_edit.ImageIndex = 18;
            this.bt_edit.Name = "bt_edit";
            // 
            // bt_del
            // 
            this.bt_del.Caption = "ɾ��";
            this.bt_del.Id = 61;
            this.bt_del.ImageIndex = 10;
            this.bt_del.Name = "bt_del";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "�鿴ͼ��";
            this.barButtonItem7.Id = 70;
            this.barButtonItem7.Name = "barButtonItem7";
            // 
            // splitterControl1
            // 
            this.splitterControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl1.Location = new System.Drawing.Point(0, 34);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(975, 4);
            this.splitterControl1.TabIndex = 5;
            this.splitterControl1.TabStop = false;
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 38);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.treeList1);
            this.splitContainerControl2.Panel1.ShowCaption = true;
            this.splitContainerControl2.Panel1.Text = "����";
            this.splitContainerControl2.Panel2.Controls.Add(this.treeList2);
            this.splitContainerControl2.Panel2.ShowCaption = true;
            this.splitContainerControl2.Panel2.Text = "����";
            this.splitContainerControl2.Size = new System.Drawing.Size(975, 587);
            this.splitContainerControl2.SplitterPosition = 287;
            this.splitContainerControl2.TabIndex = 10;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // treeList1
            // 
            this.treeList1.AllowDrop = true;
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(969, 266);
            this.treeList1.TabIndex = 0;
            this.treeList1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeList1_KeyDown);
            this.treeList1.AfterDragNode += new DevExpress.XtraTreeList.NodeEventHandler(this.treeList1_AfterDragNode);
            this.treeList1.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeList1_CellValueChanged);
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            this.treeList1.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.treeList1_ShowingEditor);
            // 
            // treeList2
            // 
            this.treeList2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList2.Location = new System.Drawing.Point(0, 0);
            this.treeList2.Name = "treeList2";
            this.treeList2.OptionsSelection.MultiSelect = true;
            this.treeList2.Size = new System.Drawing.Size(969, 275);
            this.treeList2.TabIndex = 1;
            this.treeList2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeList2_KeyDown);
            this.treeList2.CellValueChanged += new DevExpress.XtraTreeList.CellValueChangedEventHandler(this.treeList2_CellValueChanged);
            this.treeList2.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.treeList2_ShowingEditor);
            // 
            // FormForecast2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 625);
            this.Controls.Add(this.splitContainerControl2);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FormForecast2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "����Ԥ���";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form8Forecast_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private System.Windows.Forms.ImageList imageList1;

        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarSubItem barSubItem3;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem20;
        private DevExpress.XtraBars.BarButtonItem barButtonItem21;
        private DevExpress.XtraBars.BarButtonItem btGetHistory;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraBars.BarSubItem barSubItem11;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem bt_add;
        private DevExpress.XtraBars.BarButtonItem bt_edit;
        private DevExpress.XtraBars.BarButtonItem bt_del;
        private DevExpress.XtraTreeList.TreeList treeList2;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarButtonItem bt434;
        private DevExpress.XtraBars.BarButtonItem btCopy;
        private DevExpress.XtraBars.BarButtonItem bt513;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraBars.BarButtonItem barButtonItem8;
        private DevExpress.XtraBars.BarButtonItem barButtonItem9;

    }
}