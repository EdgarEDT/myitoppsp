
namespace Itop.Client.Stutistics
{
	partial class FrmPowerLineDialog
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
            this.ItemTextEditPowerName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemDateEditCreateDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.ItemTextEditRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rowPowerName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowRemark = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditPowerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemDateEditCreateDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.xtraTabControl1);
            this.panelControl.Controls.Add(this.btnOK);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(401, 164);
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
            this.xtraTabControl1.Size = new System.Drawing.Size(388, 122);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(382, 95);
            this.tabPage.Text = "输变电项目";
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
            this.ItemTextEditPowerName,
            this.ItemDateEditCreateDate,
            this.ItemTextEditRemark,
            this.repositoryItemMemoEdit1});
            this.vGridControl.RowHeaderWidth = 129;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowPowerName,
            this.rowRemark});
            this.vGridControl.Size = new System.Drawing.Size(375, 88);
            this.vGridControl.TabIndex = 0;
            // 
            // ItemTextEditPowerName
            // 
            this.ItemTextEditPowerName.AutoHeight = false;
            this.ItemTextEditPowerName.MaxLength = 100;
            this.ItemTextEditPowerName.Name = "ItemTextEditPowerName";
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
            // ItemTextEditRemark
            // 
            this.ItemTextEditRemark.AutoHeight = false;
            this.ItemTextEditRemark.MaxLength = 300;
            this.ItemTextEditRemark.Name = "ItemTextEditRemark";
            // 
            // rowPowerName
            // 
            this.rowPowerName.Height = 25;
            this.rowPowerName.Name = "rowPowerName";
            this.rowPowerName.Properties.Caption = "项目名称";
            this.rowPowerName.Properties.FieldName = "PowerName";
            this.rowPowerName.Properties.ImageIndex = 25;
            this.rowPowerName.Properties.RowEdit = this.ItemTextEditPowerName;
            // 
            // rowRemark
            // 
            this.rowRemark.Height = 50;
            this.rowRemark.Name = "rowRemark";
            this.rowRemark.Properties.Caption = "说明";
            this.rowRemark.Properties.FieldName = "Remark";
            this.rowRemark.Properties.ImageIndex = 25;
            this.rowRemark.Properties.RowEdit = this.repositoryItemMemoEdit1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(212, 135);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(316, 135);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // FrmPowerLineDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 163);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPowerLineDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输变电项目";
            this.Load += new System.EventHandler(this.FrmPowerLineDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditPowerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemDateEditCreateDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditRemark)).EndInit();
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

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditPowerName;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowPowerName;

        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit ItemDateEditCreateDate;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditRemark;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;

	}
}
