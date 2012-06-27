namespace ItopVector.Tools
{
    partial class frmLayerGrade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayerGrade));
            this.nodeAdd = new DevExpress.XtraEditors.SimpleButton();
            this.nodeDel = new DevExpress.XtraEditors.SimpleButton();
            this.changeName = new DevExpress.XtraEditors.SimpleButton();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.simpleButtonOK = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonCancle = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // nodeAdd
            // 
            this.nodeAdd.Location = new System.Drawing.Point(22, 9);
            this.nodeAdd.Name = "nodeAdd";
            this.nodeAdd.Size = new System.Drawing.Size(47, 27);
            this.nodeAdd.TabIndex = 1;
            this.nodeAdd.Text = "增加";
            this.nodeAdd.Click += new System.EventHandler(this.LayerGradeAdd);
            // 
            // nodeDel
            // 
            this.nodeDel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.nodeDel.Location = new System.Drawing.Point(76, 9);
            this.nodeDel.Name = "nodeDel";
            this.nodeDel.Size = new System.Drawing.Size(47, 27);
            this.nodeDel.TabIndex = 2;
            this.nodeDel.Text = "删除";
            this.nodeDel.Click += new System.EventHandler(this.LayerGradeDel);
            // 
            // changeName
            // 
            this.changeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeName.Location = new System.Drawing.Point(129, 9);
            this.changeName.Name = "changeName";
            this.changeName.Size = new System.Drawing.Size(47, 27);
            this.changeName.TabIndex = 4;
            this.changeName.Text = "修改";
            this.changeName.Click += new System.EventHandler(this.LayerGradeChange);
            // 
            // treeList1
            // 
            this.treeList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeList1.BestFitVisibleOnly = true;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn3,
            this.treeListColumn4,
            this.treeListColumn2});
            this.treeList1.CustomizationFormBounds = new System.Drawing.Rectangle(81, 294, 208, 163);
            this.treeList1.KeyFieldName = "SUID";
            this.treeList1.Location = new System.Drawing.Point(-1, 43);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsMenu.EnableColumnMenu = false;
            this.treeList1.OptionsMenu.EnableFooterMenu = false;
            this.treeList1.OptionsSelection.MultiSelect = true;
            this.treeList1.OptionsView.ShowCheckBoxes = true;
            this.treeList1.Size = new System.Drawing.Size(468, 444);
            this.treeList1.TabIndex = 5;
            this.treeList1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeList1_MouseDown);
            this.treeList1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeList1_KeyDown);
            this.treeList1.GetStateImage += new DevExpress.XtraTreeList.GetStateImageEventHandler(this.treeList1_GetStateImage);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "分类名称";
            this.treeListColumn1.FieldName = "Name";
            this.treeListColumn1.MinWidth = 27;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "SvgDataUid";
            this.treeListColumn3.FieldName = "SvgDataUid";
            this.treeListColumn3.Name = "treeListColumn3";
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "ParentID";
            this.treeListColumn4.FieldName = "ParentID";
            this.treeListColumn4.Name = "treeListColumn4";
            this.treeListColumn4.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "SUID";
            this.treeListColumn2.FieldName = "SUID";
            this.treeListColumn2.Name = "treeListColumn2";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // simpleButtonOK
            // 
            this.simpleButtonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButtonOK.Location = new System.Drawing.Point(268, 495);
            this.simpleButtonOK.Name = "simpleButtonOK";
            this.simpleButtonOK.Size = new System.Drawing.Size(70, 27);
            this.simpleButtonOK.TabIndex = 6;
            this.simpleButtonOK.Text = "确定";
            // 
            // simpleButtonCancle
            // 
            this.simpleButtonCancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButtonCancle.Location = new System.Drawing.Point(356, 495);
            this.simpleButtonCancle.Name = "simpleButtonCancle";
            this.simpleButtonCancle.Size = new System.Drawing.Size(70, 27);
            this.simpleButtonCancle.TabIndex = 7;
            this.simpleButtonCancle.Text = "取消";
            // 
            // frmLayerGrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 535);
            this.Controls.Add(this.simpleButtonCancle);
            this.Controls.Add(this.simpleButtonOK);
            this.Controls.Add(this.treeList1);
            this.Controls.Add(this.changeName);
            this.Controls.Add(this.nodeDel);
            this.Controls.Add(this.nodeAdd);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLayerGrade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图层分级管理";
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton nodeAdd;
        private DevExpress.XtraEditors.SimpleButton nodeDel;
        private DevExpress.XtraEditors.SimpleButton changeName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraEditors.SimpleButton simpleButtonOK;
        private DevExpress.XtraEditors.SimpleButton simpleButtonCancle;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        public DevExpress.XtraTreeList.TreeList treeList1;
        private System.Windows.Forms.ImageList imageList1;

    }
}