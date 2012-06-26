namespace Itop.Client
{
    partial class FormProjectManage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProjectManage));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barAddDir = new DevExpress.XtraBars.BarButtonItem();
            this.barAddProj = new DevExpress.XtraBars.BarButtonItem();
            this.barEdit = new DevExpress.XtraBars.BarButtonItem();
            this.barDel = new DevExpress.XtraBars.BarButtonItem();
            this.barCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barUp = new DevExpress.XtraBars.BarButtonItem();
            this.barDown = new DevExpress.XtraBars.BarButtonItem();
            this.barUser = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageList4 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
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
            this.barAddDir,
            this.barAddProj,
            this.barEdit,
            this.barDel,
            this.barUp,
            this.barDown,
            this.barUser,
            this.barCopy,
            this.barButtonItem1});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 14;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barAddDir),
            new DevExpress.XtraBars.LinkPersistInfo(this.barAddProj),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDel),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCopy),
            new DevExpress.XtraBars.LinkPersistInfo(this.barUp),
            new DevExpress.XtraBars.LinkPersistInfo(this.barDown),
            new DevExpress.XtraBars.LinkPersistInfo(this.barUser),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barAddDir
            // 
            this.barAddDir.Caption = "添加目录";
            this.barAddDir.Hint = "添加目录";
            this.barAddDir.Id = 0;
            this.barAddDir.ImageIndex = 7;
            this.barAddDir.Name = "barAddDir";
            this.barAddDir.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddDir_ItemClick);
            // 
            // barAddProj
            // 
            this.barAddProj.Caption = "添加项目";
            this.barAddProj.Hint = "添加项目";
            this.barAddProj.Id = 1;
            this.barAddProj.ImageIndex = 8;
            this.barAddProj.Name = "barAddProj";
            this.barAddProj.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barAddProj_ItemClick);
            // 
            // barEdit
            // 
            this.barEdit.Caption = "修改";
            this.barEdit.Hint = "修改";
            this.barEdit.Id = 2;
            this.barEdit.ImageIndex = 3;
            this.barEdit.Name = "barEdit";
            this.barEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barEdit_ItemClick);
            // 
            // barDel
            // 
            this.barDel.Caption = "删除";
            this.barDel.Hint = "删除";
            this.barDel.Id = 3;
            this.barDel.ImageIndex = 1;
            this.barDel.Name = "barDel";
            this.barDel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDel_ItemClick);
            // 
            // barCopy
            // 
            this.barCopy.Caption = "拷贝项目";
            this.barCopy.Hint = "拷贝项目";
            this.barCopy.Id = 7;
            this.barCopy.ImageIndex = 4;
            this.barCopy.Name = "barCopy";
            this.barCopy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barCopy_ItemClick);
            // 
            // barUp
            // 
            this.barUp.Caption = "上移";
            this.barUp.Hint = "上移";
            this.barUp.Id = 4;
            this.barUp.ImageIndex = 0;
            this.barUp.Name = "barUp";
            this.barUp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barUp_ItemClick);
            // 
            // barDown
            // 
            this.barDown.Caption = "下移";
            this.barDown.Hint = "下移";
            this.barDown.Id = 5;
            this.barDown.ImageIndex = 2;
            this.barDown.Name = "barDown";
            this.barDown.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barDown_ItemClick);
            // 
            // barUser
            // 
            this.barUser.Caption = "项目用户";
            this.barUser.Hint = "项目用户";
            this.barUser.Id = 6;
            this.barUser.ImageIndex = 9;
            this.barUser.Name = "barUser";
            this.barUser.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barUser_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "退出";
            this.barButtonItem1.Hint = "退出";
            this.barButtonItem1.Id = 13;
            this.barButtonItem1.ImageIndex = 10;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(408, 26);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 340);
            this.barDockControlBottom.Size = new System.Drawing.Size(408, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 26);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 314);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(408, 26);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 314);
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
            // 
            // treeList1
            // 
            this.treeList1.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.Empty.BackColor2 = System.Drawing.Color.White;
            this.treeList1.Appearance.Empty.Options.UseBackColor = true;
            this.treeList1.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.EvenRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.EvenRow.Options.UseBackColor = true;
            this.treeList1.Appearance.EvenRow.Options.UseBorderColor = true;
            this.treeList1.Appearance.EvenRow.Options.UseForeColor = true;
            this.treeList1.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.treeList1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.treeList1.Appearance.FocusedCell.Options.UseForeColor = true;
            this.treeList1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(109)))), ((int)(((byte)(189)))));
            this.treeList1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(139)))), ((int)(((byte)(206)))));
            this.treeList1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.treeList1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.treeList1.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.treeList1.Appearance.FocusedRow.Options.UseForeColor = true;
            this.treeList1.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.treeList1.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.treeList1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.FooterPanel.Options.UseBackColor = true;
            this.treeList1.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.treeList1.Appearance.FooterPanel.Options.UseForeColor = true;
            this.treeList1.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.treeList1.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.treeList1.Appearance.GroupButton.Options.UseBackColor = true;
            this.treeList1.Appearance.GroupButton.Options.UseBorderColor = true;
            this.treeList1.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(216)))), ((int)(((byte)(254)))));
            this.treeList1.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(216)))), ((int)(((byte)(254)))));
            this.treeList1.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.GroupFooter.Options.UseBackColor = true;
            this.treeList1.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.treeList1.Appearance.GroupFooter.Options.UseForeColor = true;
            this.treeList1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.treeList1.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.treeList1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.treeList1.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.treeList1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.treeList1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(170)))), ((int)(((byte)(225)))));
            this.treeList1.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(215)))));
            this.treeList1.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.treeList1.Appearance.HideSelectionRow.Options.UseBorderColor = true;
            this.treeList1.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.treeList1.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.treeList1.Appearance.HorzLine.Options.UseBackColor = true;
            this.treeList1.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.OddRow.Options.UseBackColor = true;
            this.treeList1.Appearance.OddRow.Options.UseBorderColor = true;
            this.treeList1.Appearance.OddRow.Options.UseForeColor = true;
            this.treeList1.Appearance.Preview.Font = new System.Drawing.Font("Verdana", 7.5F);
            this.treeList1.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(215)))));
            this.treeList1.Appearance.Preview.Options.UseFont = true;
            this.treeList1.Appearance.Preview.Options.UseForeColor = true;
            this.treeList1.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.treeList1.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.treeList1.Appearance.Row.Options.UseBackColor = true;
            this.treeList1.Appearance.Row.Options.UseForeColor = true;
            this.treeList1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(155)))), ((int)(((byte)(215)))));
            this.treeList1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.treeList1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.treeList1.Appearance.SelectedRow.Options.UseForeColor = true;
            this.treeList1.Appearance.TreeLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(133)))), ((int)(((byte)(195)))));
            this.treeList1.Appearance.TreeLine.Options.UseBackColor = true;
            this.treeList1.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.treeList1.Appearance.VertLine.Options.UseBackColor = true;
            this.treeList1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn1,
            this.treeListColumn4,
            this.treeListColumn5});
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeList1.KeyFieldName = "UID";
            this.treeList1.Location = new System.Drawing.Point(0, 26);
            this.treeList1.LookAndFeel.SkinName = "Money Twins";
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.AutoFocusNewNode = true;
            this.treeList1.OptionsBehavior.AutoMoveRowFocus = true;
            this.treeList1.OptionsBehavior.DragNodes = true;
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsView.EnableAppearanceEvenRow = true;
            this.treeList1.OptionsView.EnableAppearanceOddRow = true;
            this.treeList1.ParentFieldName = "ProjectManager";
            this.treeList1.Size = new System.Drawing.Size(408, 314);
            this.treeList1.StateImageList = this.imageList4;
            this.treeList1.TabIndex = 4;
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            this.treeList1.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeList1_GetStateImage);
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.treeListColumn2.AppearanceCell.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.treeListColumn2.AppearanceCell.Options.UseBackColor = true;
            this.treeListColumn2.AppearanceHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.treeListColumn2.AppearanceHeader.Options.UseBackColor = true;
            this.treeListColumn2.Caption = "项目管理";
            this.treeListColumn2.FieldName = "ProjectName";
            this.treeListColumn2.ImageIndex = 1;
            this.treeListColumn2.MinWidth = 73;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.OptionsColumn.AllowEdit = false;
            this.treeListColumn2.OptionsColumn.AllowMove = false;
            this.treeListColumn2.OptionsColumn.AllowMoveToCustomizationForm = false;
            this.treeListColumn2.OptionsColumn.AllowSize = false;
            this.treeListColumn2.OptionsColumn.AllowSort = false;
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            this.treeListColumn2.Width = 187;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "项目说明";
            this.treeListColumn3.FieldName = "ProjectCode";
            this.treeListColumn3.Name = "treeListColumn3";
            this.treeListColumn3.OptionsColumn.AllowEdit = false;
            this.treeListColumn3.Width = 173;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "创建时间";
            this.treeListColumn1.FieldName = "CreateDate";
            this.treeListColumn1.Name = "treeListColumn1";
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "排序";
            this.treeListColumn4.FieldName = "SortID";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "创建用户";
            this.treeListColumn5.FieldName = "GuiDangName";
            this.treeListColumn5.Name = "treeListColumn5";
            // 
            // imageList4
            // 
            this.imageList4.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList4.ImageStream")));
            this.imageList4.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList4.Images.SetKeyName(0, "application.ico");
            this.imageList4.Images.SetKeyName(1, "folder.ico");
            // 
            // FormProjectManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.ClientSize = new System.Drawing.Size(408, 340);
            this.Controls.Add(this.treeList1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormProjectManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FormProjectManage_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormProjectManage_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
        private System.Windows.Forms.ImageList imageList4;
        private DevExpress.XtraBars.BarButtonItem barAddDir;
        private DevExpress.XtraBars.BarButtonItem barAddProj;
        private DevExpress.XtraBars.BarButtonItem barEdit;
        private DevExpress.XtraBars.BarButtonItem barDel;
        private DevExpress.XtraBars.BarButtonItem barUp;
        private DevExpress.XtraBars.BarButtonItem barDown;
        private DevExpress.XtraBars.BarButtonItem barUser;
        private System.Windows.Forms.ImageList imageList3;
        private DevExpress.XtraBars.BarButtonItem barCopy;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        
       
    }
}