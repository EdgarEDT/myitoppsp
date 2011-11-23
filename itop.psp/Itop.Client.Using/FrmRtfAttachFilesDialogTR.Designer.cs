
namespace Itop.Client.Using
{
	partial class FrmRtfAttachFilesDialogTR
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
            this.ItemTextEditDes = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditFileType = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditFileSize = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemDateEditCreateDate = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rowDes = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.editorRow1 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowFileType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowFileSize = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditDes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditFileType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditFileSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemDateEditCreateDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.xtraTabControl1);
            this.panelControl.Controls.Add(this.btnOK);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(408, 193);
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
            this.xtraTabControl1.Size = new System.Drawing.Size(396, 148);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(392, 121);
            this.tabPage.Text = "附件";
            // 
            // vGridControl
            // 
            this.vGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vGridControl.Location = new System.Drawing.Point(3, 3);
            this.vGridControl.Name = "vGridControl";
            this.vGridControl.RecordWidth = 244;
            this.vGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ItemTextEditDes,
            this.ItemTextEditFileType,
            this.ItemTextEditFileSize,
            this.ItemDateEditCreateDate,
            this.repositoryItemButtonEdit1});
            this.vGridControl.RowHeaderWidth = 129;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowDes,
            this.editorRow1,
            this.rowFileType,
            this.rowFileSize});
            this.vGridControl.Size = new System.Drawing.Size(384, 115);
            this.vGridControl.TabIndex = 0;
            // 
            // ItemTextEditDes
            // 
            this.ItemTextEditDes.AutoHeight = false;
            this.ItemTextEditDes.MaxLength = 100;
            this.ItemTextEditDes.Name = "ItemTextEditDes";
            // 
            // ItemTextEditFileType
            // 
            this.ItemTextEditFileType.AutoHeight = false;
            this.ItemTextEditFileType.MaxLength = 10;
            this.ItemTextEditFileType.Name = "ItemTextEditFileType";
            this.ItemTextEditFileType.ReadOnly = true;
            // 
            // ItemTextEditFileSize
            // 
            this.ItemTextEditFileSize.AutoHeight = false;
            this.ItemTextEditFileSize.DisplayFormat.FormatString = "n0";
            this.ItemTextEditFileSize.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditFileSize.EditFormat.FormatString = "n0";
            this.ItemTextEditFileSize.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditFileSize.Mask.EditMask = "n0";
            this.ItemTextEditFileSize.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditFileSize.Name = "ItemTextEditFileSize";
            this.ItemTextEditFileSize.ReadOnly = true;
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
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.ReadOnly = true;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // rowDes
            // 
            this.rowDes.Height = 25;
            this.rowDes.Name = "rowDes";
            this.rowDes.Properties.Caption = "标题";
            this.rowDes.Properties.FieldName = "Des";
            this.rowDes.Properties.ImageIndex = 25;
            this.rowDes.Properties.RowEdit = this.ItemTextEditDes;
            // 
            // editorRow1
            // 
            this.editorRow1.Height = 25;
            this.editorRow1.Name = "editorRow1";
            this.editorRow1.Properties.Caption = "文件";
            this.editorRow1.Properties.FieldName = "FileName";
            this.editorRow1.Properties.RowEdit = this.repositoryItemButtonEdit1;
            // 
            // rowFileType
            // 
            this.rowFileType.Height = 25;
            this.rowFileType.Name = "rowFileType";
            this.rowFileType.Properties.Caption = "文件类型";
            this.rowFileType.Properties.FieldName = "FileType";
            this.rowFileType.Properties.ImageIndex = 25;
            this.rowFileType.Properties.RowEdit = this.ItemTextEditFileType;
            // 
            // rowFileSize
            // 
            this.rowFileSize.Height = 25;
            this.rowFileSize.Name = "rowFileSize";
            this.rowFileSize.Properties.Caption = "文件大小(Byte)";
            this.rowFileSize.Properties.FieldName = "FileSize";
            this.rowFileSize.Properties.ImageIndex = 25;
            this.rowFileSize.Properties.RowEdit = this.ItemTextEditFileSize;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(217, 163);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(321, 163);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(314, -100);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 1;
            // 
            // FrmRtfAttachFilesDialogTR
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(409, 192);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRtfAttachFilesDialogTR";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "附件";
            this.Load += new System.EventHandler(this.FrmRtfAttachFilesDialogTR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditDes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditFileType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditFileSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemDateEditCreateDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private DevExpress.XtraEditors.PanelControl panelControl;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraTab.XtraTabPage tabPage;
		private DevExpress.XtraEditors.SimpleButton btnOK;
		private DevExpress.XtraEditors.SimpleButton btnCancel;
		private DevExpress.XtraVerticalGrid.VGridControl vGridControl;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditDes;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowDes;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditFileType;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowFileType;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditFileSize;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowFileSize;

        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit ItemDateEditCreateDate;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow editorRow1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private System.Windows.Forms.TextBox textBox1;

	}
}
