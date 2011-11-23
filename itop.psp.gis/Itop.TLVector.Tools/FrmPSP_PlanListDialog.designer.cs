
namespace ItopVector.Tools
{
	partial class FrmPSP_PlanListDialog
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
            this.ItemTextEditUID = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditLineName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditcol1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditcol2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditcol3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.rowUID = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLineName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowRemark = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowcol1 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowcol2 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowcol3 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditUID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLineName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl.Controls.Add(this.xtraTabControl1);
            this.panelControl.Controls.Add(this.btnOK);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Location = new System.Drawing.Point(3, 3);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(395, 195);
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
            this.xtraTabControl1.Size = new System.Drawing.Size(382, 153);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(378, 138);
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
            this.ItemTextEditUID,
            this.ItemTextEditLineName,
            this.ItemTextEditRemark,
            this.ItemTextEditcol1,
            this.ItemTextEditcol2,
            this.ItemTextEditcol3,
            this.repositoryItemMemoEdit1});
            this.vGridControl.RowHeaderWidth = 129;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowUID,
            this.rowLineName,
            this.rowRemark,
            this.rowcol1,
            this.rowcol2,
            this.rowcol3});
            this.vGridControl.Size = new System.Drawing.Size(371, 131);
            this.vGridControl.TabIndex = 0;
            // 
            // ItemTextEditUID
            // 
            this.ItemTextEditUID.AutoHeight = false;
            this.ItemTextEditUID.MaxLength = 50;
            this.ItemTextEditUID.Name = "ItemTextEditUID";
            // 
            // ItemTextEditLineName
            // 
            this.ItemTextEditLineName.AutoHeight = false;
            this.ItemTextEditLineName.MaxLength = 50;
            this.ItemTextEditLineName.Name = "ItemTextEditLineName";
            // 
            // ItemTextEditRemark
            // 
            this.ItemTextEditRemark.AutoHeight = false;
            this.ItemTextEditRemark.MaxLength = 500;
            this.ItemTextEditRemark.Name = "ItemTextEditRemark";
            // 
            // ItemTextEditcol1
            // 
            this.ItemTextEditcol1.AutoHeight = false;
            this.ItemTextEditcol1.MaxLength = 50;
            this.ItemTextEditcol1.Name = "ItemTextEditcol1";
            // 
            // ItemTextEditcol2
            // 
            this.ItemTextEditcol2.AutoHeight = false;
            this.ItemTextEditcol2.MaxLength = 50;
            this.ItemTextEditcol2.Name = "ItemTextEditcol2";
            // 
            // ItemTextEditcol3
            // 
            this.ItemTextEditcol3.AutoHeight = false;
            this.ItemTextEditcol3.MaxLength = 50;
            this.ItemTextEditcol3.Name = "ItemTextEditcol3";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // rowUID
            // 
            this.rowUID.Height = 25;
            this.rowUID.Name = "rowUID";
            this.rowUID.Properties.FieldName = "UID";
            this.rowUID.Properties.ImageIndex = 25;
            this.rowUID.Properties.RowEdit = this.ItemTextEditUID;
            this.rowUID.Visible = false;
            // 
            // rowLineName
            // 
            this.rowLineName.Height = 25;
            this.rowLineName.Name = "rowLineName";
            this.rowLineName.Properties.Caption = "线路名称";
            this.rowLineName.Properties.FieldName = "LineName";
            this.rowLineName.Properties.ImageIndex = 25;
            this.rowLineName.Properties.RowEdit = this.ItemTextEditLineName;
            // 
            // rowRemark
            // 
            this.rowRemark.Height = 95;
            this.rowRemark.Name = "rowRemark";
            this.rowRemark.Properties.Caption = "描述";
            this.rowRemark.Properties.FieldName = "Remark";
            this.rowRemark.Properties.ImageIndex = 25;
            this.rowRemark.Properties.RowEdit = this.repositoryItemMemoEdit1;
            // 
            // rowcol1
            // 
            this.rowcol1.Height = 25;
            this.rowcol1.Name = "rowcol1";
            this.rowcol1.Properties.FieldName = "col1";
            this.rowcol1.Properties.ImageIndex = 25;
            this.rowcol1.Properties.RowEdit = this.ItemTextEditcol1;
            this.rowcol1.Visible = false;
            // 
            // rowcol2
            // 
            this.rowcol2.Height = 25;
            this.rowcol2.Name = "rowcol2";
            this.rowcol2.Properties.FieldName = "col2";
            this.rowcol2.Properties.ImageIndex = 25;
            this.rowcol2.Properties.RowEdit = this.ItemTextEditcol2;
            this.rowcol2.Visible = false;
            // 
            // rowcol3
            // 
            this.rowcol3.Height = 25;
            this.rowcol3.Name = "rowcol3";
            this.rowcol3.Properties.FieldName = "col3";
            this.rowcol3.Properties.ImageIndex = 25;
            this.rowcol3.Properties.RowEdit = this.ItemTextEditcol3;
            this.rowcol3.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(221, 164);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(302, 164);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // FrmPSP_PlanListDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 198);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPSP_PlanListDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "线路规划";
            this.Load += new System.EventHandler(this.FrmPSP_PlanListDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditUID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLineName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private DevExpress.XtraEditors.PanelControl panelControl;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraTab.XtraTabPage tabPage;
		private DevExpress.XtraEditors.SimpleButton btnOK;
		private DevExpress.XtraEditors.SimpleButton btnCancel;
		private DevExpress.XtraVerticalGrid.VGridControl vGridControl;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditUID;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowUID;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditLineName;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLineName;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditRemark;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowRemark;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditcol1;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowcol1;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditcol2;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowcol2;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditcol3;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowcol3;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;

	}
}
