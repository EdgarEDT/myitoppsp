
namespace Itop.Client.Stutistics
{
	partial class FrmPowerEachListDialog
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabPage = new DevExpress.XtraTab.XtraTabPage();
            this.vGridControl = new DevExpress.XtraVerticalGrid.VGridControl();
            this.ItemTextEditListName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colListName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.colRemark = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.editorRow3 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.editorRow4 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.editorRow5 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditListName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditRemark)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.simpleButton1);
            this.panelControl.Controls.Add(this.xtraTabControl1);
            this.panelControl.Controls.Add(this.btnOK);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(401, 121);
            this.panelControl.TabIndex = 0;
            this.panelControl.Text = "panelControl1";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(57, 91);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "关联图层";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraTabControl1.Location = new System.Drawing.Point(7, 7);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabPage;
            this.xtraTabControl1.Size = new System.Drawing.Size(388, 78);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(384, 51);
            this.tabPage.Text = "分类";
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
            this.ItemTextEditListName,
            this.ItemTextEditRemark});
            this.vGridControl.RowHeaderWidth = 129;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.colListName,
            this.colRemark,
            this.editorRow3,
            this.editorRow4,
            this.editorRow5});
            this.vGridControl.Size = new System.Drawing.Size(376, 45);
            this.vGridControl.TabIndex = 0;
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
            // colListName
            // 
            this.colListName.Height = 40;
            this.colListName.Name = "colListName";
            this.colListName.Properties.Caption = "名称";
            this.colListName.Properties.FieldName = "ListName";
            // 
            // colRemark
            // 
            this.colRemark.Name = "colRemark";
            this.colRemark.Properties.Caption = "备注";
            this.colRemark.Properties.FieldName = "Remark";
            this.colRemark.Visible = false;
            // 
            // editorRow3
            // 
            this.editorRow3.Name = "editorRow3";
            this.editorRow3.Properties.Caption = "日期";
            this.editorRow3.Properties.FieldName = "CreateDate";
            this.editorRow3.Visible = false;
            // 
            // editorRow4
            // 
            this.editorRow4.Name = "editorRow4";
            this.editorRow4.Properties.Caption = "父ID";
            this.editorRow4.Properties.FieldName = "ParentID";
            this.editorRow4.Visible = false;
            // 
            // editorRow5
            // 
            this.editorRow5.Name = "editorRow5";
            this.editorRow5.Properties.Caption = "类型";
            this.editorRow5.Properties.FieldName = "Types";
            this.editorRow5.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(201, 91);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(294, 91);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmPowerEachListDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(400, 122);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPowerEachListDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "分类";
            this.Load += new System.EventHandler(this.FrmPowerEachListDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditListName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditRemark)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private DevExpress.XtraEditors.PanelControl panelControl;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraTab.XtraTabPage tabPage;
		private DevExpress.XtraEditors.SimpleButton btnOK;
		private DevExpress.XtraEditors.SimpleButton btnCancel;
		private DevExpress.XtraVerticalGrid.VGridControl vGridControl;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditListName;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowListName;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditRemark;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowRemark;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow colListName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow colRemark;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow editorRow3;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow editorRow4;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow editorRow5;

	}
}
