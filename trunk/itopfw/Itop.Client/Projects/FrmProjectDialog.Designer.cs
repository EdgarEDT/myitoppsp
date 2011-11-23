
namespace Itop.Client.Projects
{
	partial class FrmProjectDialog
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		/// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
		/// </summary>
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
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabPage = new DevExpress.XtraTab.XtraTabPage();
            this.vGridControl = new DevExpress.XtraVerticalGrid.VGridControl();
            this.ItemTextEditProjectCode = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditProjectName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemDateEditCreateDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.rowProjectName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowCreateDate = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.editorRow1 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowProjectCode = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditProjectCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditProjectName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemDateEditCreateDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.xtraTabControl1);
            this.panelControl.Controls.Add(this.btnOK);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(395, 314);
            this.panelControl.TabIndex = 0;
            this.panelControl.Text = "panelControl1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraTabControl1.Location = new System.Drawing.Point(6, 6);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabPage;
            this.xtraTabControl1.Size = new System.Drawing.Size(384, 274);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(375, 243);
            this.tabPage.Text = "��Ŀ����";
            // 
            // vGridControl
            // 
            this.vGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vGridControl.Location = new System.Drawing.Point(3, 3);
            this.vGridControl.Name = "vGridControl";
            this.vGridControl.RecordWidth = 248;
            this.vGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ItemTextEditProjectCode,
            this.ItemTextEditProjectName,
            this.ItemDateEditCreateDate,
            this.repositoryItemMemoEdit1,
            this.repositoryItemColorEdit1});
            this.vGridControl.RowHeaderWidth = 102;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowProjectName,
            this.rowCreateDate,
            this.editorRow1,
            this.rowProjectCode});
            this.vGridControl.Size = new System.Drawing.Size(368, 236);
            this.vGridControl.TabIndex = 0;
            // 
            // ItemTextEditProjectCode
            // 
            this.ItemTextEditProjectCode.AutoHeight = false;
            this.ItemTextEditProjectCode.MaxLength = 50;
            this.ItemTextEditProjectCode.Name = "ItemTextEditProjectCode";
            // 
            // ItemTextEditProjectName
            // 
            this.ItemTextEditProjectName.AutoHeight = false;
            this.ItemTextEditProjectName.MaxLength = 100;
            this.ItemTextEditProjectName.Name = "ItemTextEditProjectName";
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
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.MaxLength = 500;
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemColorEdit1
            // 
            this.repositoryItemColorEdit1.AutoHeight = false;
            this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemColorEdit1.ColorText = DevExpress.XtraEditors.Controls.ColorText.Integer;
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            this.repositoryItemColorEdit1.NullText = "\"\"";
            this.repositoryItemColorEdit1.StoreColorAsInteger = true;
            this.repositoryItemColorEdit1.ColorChanged += new System.EventHandler(this.repositoryItemColorEdit1_ColorChanged);
            // 
            // rowProjectName
            // 
            this.rowProjectName.Height = 25;
            this.rowProjectName.Name = "rowProjectName";
            this.rowProjectName.Properties.Caption = "��Ŀ����";
            this.rowProjectName.Properties.FieldName = "ProjectName";
            this.rowProjectName.Properties.ImageIndex = 25;
            this.rowProjectName.Properties.RowEdit = this.ItemTextEditProjectName;
            // 
            // rowCreateDate
            // 
            this.rowCreateDate.Height = 25;
            this.rowCreateDate.Name = "rowCreateDate";
            this.rowCreateDate.Properties.Caption = "����ʱ��";
            this.rowCreateDate.Properties.FieldName = "CreateDate";
            this.rowCreateDate.Properties.ImageIndex = 25;
            this.rowCreateDate.Properties.RowEdit = this.ItemDateEditCreateDate;
            // 
            // editorRow1
            // 
            this.editorRow1.Height = 25;
            this.editorRow1.Name = "editorRow1";
            this.editorRow1.Properties.Caption = "��ɫ";
            this.editorRow1.Properties.FieldName = "SortID";
            this.editorRow1.Properties.RowEdit = this.repositoryItemColorEdit1;
            this.editorRow1.Properties.Value = "<Null>";
            // 
            // rowProjectCode
            // 
            this.rowProjectCode.Height = 111;
            this.rowProjectCode.Name = "rowProjectCode";
            this.rowProjectCode.Properties.Caption = "��Ŀ˵��";
            this.rowProjectCode.Properties.FieldName = "ProjectCode";
            this.rowProjectCode.Properties.ImageIndex = 25;
            this.rowProjectCode.Properties.RowEdit = this.repositoryItemMemoEdit1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(199, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "ȷ��";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(303, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "ȡ��";
            // 
            // FrmProjectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 315);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProjectDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "��Ŀ����";
            this.Load += new System.EventHandler(this.FrmProjectDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditProjectCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditProjectName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemDateEditCreateDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private DevExpress.XtraEditors.PanelControl panelControl;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraTab.XtraTabPage tabPage;
		private DevExpress.XtraEditors.SimpleButton btnOK;
		private DevExpress.XtraEditors.SimpleButton btnCancel;
		private DevExpress.XtraVerticalGrid.VGridControl vGridControl;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditProjectCode;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowProjectCode;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditProjectName;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowProjectName;

		private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit ItemDateEditCreateDate;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowCreateDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow editorRow1;

	}
}
