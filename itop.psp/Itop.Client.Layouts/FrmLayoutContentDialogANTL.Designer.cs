
namespace Itop.Client.Layouts
{
    partial class FrmLayoutContentDialogANTL
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
            this.ItemTextEditChapterName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditContents = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditContentType = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.rowChapterName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowRemark = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditChapterName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditContents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditContentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
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
            this.panelControl.Size = new System.Drawing.Size(404, 189);
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
            this.xtraTabControl1.Size = new System.Drawing.Size(391, 147);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(387, 120);
            this.tabPage.Text = "Ŀ¼����";
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
            this.ItemTextEditChapterName,
            this.ItemTextEditContents,
            this.ItemTextEditContentType,
            this.ItemTextEditRemark,
            this.repositoryItemMemoEdit1});
            this.vGridControl.RowHeaderWidth = 129;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowChapterName,
            this.rowRemark});
            this.vGridControl.Size = new System.Drawing.Size(378, 113);
            this.vGridControl.TabIndex = 0;
            // 
            // ItemTextEditChapterName
            // 
            this.ItemTextEditChapterName.AutoHeight = false;
            this.ItemTextEditChapterName.MaxLength = 100;
            this.ItemTextEditChapterName.Name = "ItemTextEditChapterName";
            // 
            // ItemTextEditContents
            // 
            this.ItemTextEditContents.AutoHeight = false;
            this.ItemTextEditContents.Name = "ItemTextEditContents";
            // 
            // ItemTextEditContentType
            // 
            this.ItemTextEditContentType.AutoHeight = false;
            this.ItemTextEditContentType.MaxLength = 10;
            this.ItemTextEditContentType.Name = "ItemTextEditContentType";
            // 
            // ItemTextEditRemark
            // 
            this.ItemTextEditRemark.AutoHeight = false;
            this.ItemTextEditRemark.MaxLength = 300;
            this.ItemTextEditRemark.Name = "ItemTextEditRemark";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.MaxLength = 300;
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // rowChapterName
            // 
            this.rowChapterName.Height = 25;
            this.rowChapterName.Name = "rowChapterName";
            this.rowChapterName.Properties.Caption = "�½�����";
            this.rowChapterName.Properties.FieldName = "ChapterName";
            this.rowChapterName.Properties.ImageIndex = 25;
            this.rowChapterName.Properties.RowEdit = this.ItemTextEditChapterName;
            // 
            // rowRemark
            // 
            this.rowRemark.Height = 75;
            this.rowRemark.Name = "rowRemark";
            this.rowRemark.Properties.Caption = "��ע";
            this.rowRemark.Properties.FieldName = "Remark";
            this.rowRemark.Properties.ImageIndex = 25;
            this.rowRemark.Properties.RowEdit = this.repositoryItemMemoEdit1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(204, 154);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "ȷ��";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(308, 154);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "ȡ��";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmLayoutContentDialogANTL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 189);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLayoutContentDialogANTL";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ŀ¼����";
            this.Load += new System.EventHandler(this.FrmLayoutContentDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditChapterName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditContents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditContentType)).EndInit();
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

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditChapterName;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowChapterName;

        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditContents;

        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditContentType;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditRemark;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;

	}
}
