
namespace Itop.Client.Stutistics
{
	partial class FrmPowerStuffDialog
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabPage = new DevExpress.XtraTab.XtraTabPage();
            this.vGridControl = new DevExpress.XtraVerticalGrid.VGridControl();
            this.ItemTextEditStuffName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditVolume = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditTotal = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditType = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditLengths = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditRemark = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rowStuffName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowVolume = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTotal = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLengths = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowRemark = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditStuffName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLengths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditRemark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.textBox1);
            this.panelControl.Controls.Add(this.xtraTabControl1);
            this.panelControl.Controls.Add(this.btnOK);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(403, 244);
            this.panelControl.TabIndex = 0;
            this.panelControl.Text = "panelControl1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, -211);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 2;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraTabControl1.Location = new System.Drawing.Point(7, 7);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabPage;
            this.xtraTabControl1.Size = new System.Drawing.Size(390, 202);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(384, 175);
            this.tabPage.Text = "输变电设备情况";
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
            this.ItemTextEditStuffName,
            this.ItemTextEditVolume,
            this.ItemTextEditTotal,
            this.ItemTextEditType,
            this.ItemTextEditLengths,
            this.ItemTextEditRemark,
            this.repositoryItemComboBox1,
            this.repositoryItemButtonEdit1});
            this.vGridControl.RowHeaderWidth = 129;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowStuffName,
            this.rowVolume,
            this.rowTotal,
            this.rowType,
            this.rowLengths,
            this.rowRemark});
            this.vGridControl.Size = new System.Drawing.Size(377, 168);
            this.vGridControl.TabIndex = 0;
            // 
            // ItemTextEditStuffName
            // 
            this.ItemTextEditStuffName.AutoHeight = false;
            this.ItemTextEditStuffName.MaxLength = 100;
            this.ItemTextEditStuffName.Name = "ItemTextEditStuffName";
            // 
            // ItemTextEditVolume
            // 
            this.ItemTextEditVolume.AutoHeight = false;
            this.ItemTextEditVolume.MaxLength = 50;
            this.ItemTextEditVolume.Name = "ItemTextEditVolume";
            // 
            // ItemTextEditTotal
            // 
            this.ItemTextEditTotal.AutoHeight = false;
            this.ItemTextEditTotal.MaxLength = 50;
            this.ItemTextEditTotal.Name = "ItemTextEditTotal";
            // 
            // ItemTextEditType
            // 
            this.ItemTextEditType.AutoHeight = false;
            this.ItemTextEditType.MaxLength = 50;
            this.ItemTextEditType.Name = "ItemTextEditType";
            // 
            // ItemTextEditLengths
            // 
            this.ItemTextEditLengths.AutoHeight = false;
            this.ItemTextEditLengths.MaxLength = 50;
            this.ItemTextEditLengths.Name = "ItemTextEditLengths";
            // 
            // ItemTextEditRemark
            // 
            this.ItemTextEditRemark.AutoHeight = false;
            this.ItemTextEditRemark.MaxLength = 300;
            this.ItemTextEditRemark.Name = "ItemTextEditRemark";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // rowStuffName
            // 
            this.rowStuffName.Height = 25;
            this.rowStuffName.Name = "rowStuffName";
            this.rowStuffName.Properties.Caption = "名称";
            this.rowStuffName.Properties.FieldName = "StuffName";
            this.rowStuffName.Properties.ImageIndex = 25;
            this.rowStuffName.Properties.RowEdit = this.ItemTextEditStuffName;
            // 
            // rowVolume
            // 
            this.rowVolume.Height = 25;
            this.rowVolume.Name = "rowVolume";
            this.rowVolume.Properties.Caption = "总容量";
            this.rowVolume.Properties.FieldName = "Volume";
            this.rowVolume.Properties.ImageIndex = 25;
            this.rowVolume.Properties.RowEdit = this.ItemTextEditVolume;
            // 
            // rowTotal
            // 
            this.rowTotal.Height = 25;
            this.rowTotal.Name = "rowTotal";
            this.rowTotal.Properties.Caption = "变电设备台数";
            this.rowTotal.Properties.FieldName = "Total";
            this.rowTotal.Properties.ImageIndex = 25;
            this.rowTotal.Properties.RowEdit = this.ItemTextEditTotal;
            // 
            // rowType
            // 
            this.rowType.Height = 25;
            this.rowType.Name = "rowType";
            this.rowType.Properties.Caption = "导线型号";
            this.rowType.Properties.FieldName = "Type";
            this.rowType.Properties.ImageIndex = 25;
            this.rowType.Properties.RowEdit = this.ItemTextEditType;
            // 
            // rowLengths
            // 
            this.rowLengths.Height = 25;
            this.rowLengths.Name = "rowLengths";
            this.rowLengths.Properties.Caption = "长度";
            this.rowLengths.Properties.FieldName = "Lengths";
            this.rowLengths.Properties.ImageIndex = 25;
            this.rowLengths.Properties.RowEdit = this.ItemTextEditLengths;
            // 
            // rowRemark
            // 
            this.rowRemark.Height = 25;
            this.rowRemark.Name = "rowRemark";
            this.rowRemark.Properties.Caption = "备注";
            this.rowRemark.Properties.FieldName = "Remark";
            this.rowRemark.Properties.ImageIndex = 25;
            this.rowRemark.Properties.RowEdit = this.ItemTextEditRemark;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(214, 214);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(318, 214);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // FrmPowerStuffDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 242);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPowerStuffDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输变电设备情况";
            this.Load += new System.EventHandler(this.FrmPowerStuffDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditStuffName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLengths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditRemark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
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

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditStuffName;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowStuffName;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditVolume;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowVolume;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditTotal;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTotal;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditType;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowType;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditLengths;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLengths;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditRemark;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowRemark;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private System.Windows.Forms.TextBox textBox1;

	}
}
