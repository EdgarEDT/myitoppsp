
namespace Itop.Client.Stutistics
{
    partial class FrmPSP_EachListDialog
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        /// </summary>
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
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabPage = new DevExpress.XtraTab.XtraTabPage();
            this.vGridControl = new DevExpress.XtraVerticalGrid.VGridControl();
            this.ItemTextEditID = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditListName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemDateEditCreateDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.ItemTextEditParentID = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditTypes = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rowID = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowListName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowRemark = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowCreateDate = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowParentID = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTypes = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditListName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemDateEditCreateDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditParentID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.xtraTabControl1);
            this.panelControl.Controls.Add(this.btnOK);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(395, 133);
            this.panelControl.TabIndex = 0;
            this.panelControl.Text = "panelControl1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraTabControl1.Location = new System.Drawing.Point(7, 7);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabPage;
            this.xtraTabControl1.Size = new System.Drawing.Size(382, 91);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(376, 75);
            // 
            // vGridControl
            // 
            this.vGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vGridControl.Location = new System.Drawing.Point(3, 3);
            this.vGridControl.Name = "vGridControl";
            this.vGridControl.RecordWidth = 234;
            this.vGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ItemTextEditID,
            this.ItemTextEditListName,
            this.ItemTextEditRemark,
            this.ItemDateEditCreateDate,
            this.ItemTextEditParentID,
            this.ItemTextEditTypes});
            this.vGridControl.RowHeaderWidth = 129;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowID,
            this.rowListName,
            this.rowRemark,
            this.rowCreateDate,
            this.rowParentID,
            this.rowTypes});
            this.vGridControl.Size = new System.Drawing.Size(369, 69);
            this.vGridControl.TabIndex = 0;
            // 
            // ItemTextEditID
            // 
            this.ItemTextEditID.AutoHeight = false;
            this.ItemTextEditID.Name = "ItemTextEditID";
            // 
            // ItemTextEditListName
            // 
            this.ItemTextEditListName.AutoHeight = false;
            this.ItemTextEditListName.MaxLength = 100;
            this.ItemTextEditListName.Name = "ItemTextEditListName";
            // 
            // ItemTextEditRemark
            // 
            this.ItemTextEditRemark.AutoHeight = false;
            this.ItemTextEditRemark.MaxLength = 300;
            this.ItemTextEditRemark.Name = "ItemTextEditRemark";
            // 
            // ItemDateEditCreateDate
            // 
            this.ItemDateEditCreateDate.AutoHeight = false;
            this.ItemDateEditCreateDate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ItemDateEditCreateDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.ItemDateEditCreateDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.ItemDateEditCreateDate.EditFormat.FormatString = "yyyy-MM-dd";
            this.ItemDateEditCreateDate.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.ItemDateEditCreateDate.Mask.EditMask = "yyyy-MM-dd";
            this.ItemDateEditCreateDate.Name = "ItemDateEditCreateDate";
            this.ItemDateEditCreateDate.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // ItemTextEditParentID
            // 
            this.ItemTextEditParentID.AutoHeight = false;
            this.ItemTextEditParentID.MaxLength = 36;
            this.ItemTextEditParentID.Name = "ItemTextEditParentID";
            // 
            // ItemTextEditTypes
            // 
            this.ItemTextEditTypes.AutoHeight = false;
            this.ItemTextEditTypes.MaxLength = 50;
            this.ItemTextEditTypes.Name = "ItemTextEditTypes";
            // 
            // rowID
            // 
            this.rowID.Height = 25;
            this.rowID.Name = "rowID";
            this.rowID.Properties.FieldName = "ID";
            this.rowID.Properties.ImageIndex = 25;
            this.rowID.Properties.RowEdit = this.ItemTextEditID;
            this.rowID.Visible = false;
            // 
            // rowListName
            // 
            this.rowListName.Height = 32;
            this.rowListName.Name = "rowListName";
            this.rowListName.Properties.Caption = "分类名称";
            this.rowListName.Properties.FieldName = "ListName";
            this.rowListName.Properties.ImageIndex = 25;
            this.rowListName.Properties.RowEdit = this.ItemTextEditListName;
            // 
            // rowRemark
            // 
            this.rowRemark.Height = 32;
            this.rowRemark.Name = "rowRemark";
            this.rowRemark.Properties.Caption = "备注";
            this.rowRemark.Properties.FieldName = "Remark";
            this.rowRemark.Properties.ImageIndex = 25;
            this.rowRemark.Properties.RowEdit = this.ItemTextEditRemark;
            // 
            // rowCreateDate
            // 
            this.rowCreateDate.Height = 25;
            this.rowCreateDate.Name = "rowCreateDate";
            this.rowCreateDate.Properties.Caption = "创建日期";
            this.rowCreateDate.Properties.FieldName = "CreateDate";
            this.rowCreateDate.Properties.ImageIndex = 25;
            this.rowCreateDate.Properties.RowEdit = this.ItemDateEditCreateDate;
            this.rowCreateDate.Visible = false;
            // 
            // rowParentID
            // 
            this.rowParentID.Height = 25;
            this.rowParentID.Name = "rowParentID";
            this.rowParentID.Properties.Caption = "父节点";
            this.rowParentID.Properties.FieldName = "ParentID";
            this.rowParentID.Properties.ImageIndex = 25;
            this.rowParentID.Properties.RowEdit = this.ItemTextEditParentID;
            this.rowParentID.Visible = false;
            // 
            // rowTypes
            // 
            this.rowTypes.Height = 25;
            this.rowTypes.Name = "rowTypes";
            this.rowTypes.Properties.FieldName = "Types";
            this.rowTypes.Properties.ImageIndex = 25;
            this.rowTypes.Properties.RowEdit = this.ItemTextEditTypes;
            this.rowTypes.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(210, 102);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(314, 102);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // FrmPSP_EachListDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(395, 133);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPSP_EachListDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmPSP_EachListDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditListName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemDateEditCreateDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditParentID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTypes)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabPage;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraVerticalGrid.VGridControl vGridControl;

        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditID;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowID;

        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditListName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowListName;

        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditRemark;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowRemark;

        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit ItemDateEditCreateDate;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowCreateDate;

        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditParentID;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowParentID;

        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditTypes;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTypes;

    }
}
