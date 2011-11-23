
namespace Itop.Client.Chen
{
	partial class FrmPSP_VolumeBalance_CalcDialog
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.vGridControl = new DevExpress.XtraVerticalGrid.VGridControl();
            this.ItemTextEditTitle = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditLX1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditLX2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditVol = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemDateEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.rowTitle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLX1 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLX2 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowVol = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.editorRow2 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.editorRow1 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLX2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditVol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
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
            this.panelControl.Size = new System.Drawing.Size(395, 268);
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
            this.xtraTabControl1.Size = new System.Drawing.Size(382, 226);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.textBox1);
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(378, 199);
            this.tabPage.Text = "供电负荷";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(-284, 165);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 1;
            // 
            // vGridControl
            // 
            this.vGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vGridControl.Location = new System.Drawing.Point(3, 3);
            this.vGridControl.Name = "vGridControl";
            this.vGridControl.RecordWidth = 223;
            this.vGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ItemTextEditTitle,
            this.ItemTextEditLX1,
            this.ItemTextEditLX2,
            this.ItemTextEditVol,
            this.repositoryItemComboBox1,
            this.repositoryItemComboBox2,
            this.repositoryItemDateEdit1,
            this.repositoryItemMemoExEdit1,
            this.repositoryItemMemoEdit1});
            this.vGridControl.RowHeaderWidth = 129;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowTitle,
            this.rowLX1,
            this.rowLX2,
            this.rowVol,
            this.editorRow2,
            this.editorRow1});
            this.vGridControl.Size = new System.Drawing.Size(371, 192);
            this.vGridControl.TabIndex = 0;
            // 
            // ItemTextEditTitle
            // 
            this.ItemTextEditTitle.AutoHeight = false;
            this.ItemTextEditTitle.MaxLength = 500;
            this.ItemTextEditTitle.Name = "ItemTextEditTitle";
            // 
            // ItemTextEditLX1
            // 
            this.ItemTextEditLX1.AutoHeight = false;
            this.ItemTextEditLX1.MaxLength = 100;
            this.ItemTextEditLX1.Name = "ItemTextEditLX1";
            // 
            // ItemTextEditLX2
            // 
            this.ItemTextEditLX2.AutoHeight = false;
            this.ItemTextEditLX2.MaxLength = 100;
            this.ItemTextEditLX2.Name = "ItemTextEditLX2";
            // 
            // ItemTextEditVol
            // 
            this.ItemTextEditVol.AutoHeight = false;
            this.ItemTextEditVol.DisplayFormat.FormatString = "n2";
            this.ItemTextEditVol.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditVol.EditFormat.FormatString = "n2";
            this.ItemTextEditVol.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditVol.Mask.EditMask = "n2";
            this.ItemTextEditVol.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditVol.Name = "ItemTextEditVol";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "新建",
            "扩建",
            "增容"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.SelectedIndexChanged += new System.EventHandler(this.repositoryItemComboBox1_SelectedIndexChanged);
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            this.repositoryItemComboBox2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBox2.SelectedIndexChanged += new System.EventHandler(this.repositoryItemComboBox2_SelectedIndexChanged);
            // 
            // repositoryItemDateEdit1
            // 
            this.repositoryItemDateEdit1.AutoHeight = false;
            this.repositoryItemDateEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit1.Mask.EditMask = "yyyy-MM";
            this.repositoryItemDateEdit1.Name = "repositoryItemDateEdit1";
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            // 
            // rowTitle
            // 
            this.rowTitle.Height = 25;
            this.rowTitle.Name = "rowTitle";
            this.rowTitle.Properties.Caption = "项目名称";
            this.rowTitle.Properties.FieldName = "Title";
            this.rowTitle.Properties.ImageIndex = 25;
            this.rowTitle.Properties.RowEdit = this.ItemTextEditTitle;
            // 
            // rowLX1
            // 
            this.rowLX1.Height = 25;
            this.rowLX1.Name = "rowLX1";
            this.rowLX1.Properties.Caption = "类型";
            this.rowLX1.Properties.FieldName = "LX1";
            this.rowLX1.Properties.ImageIndex = 25;
            this.rowLX1.Properties.RowEdit = this.repositoryItemComboBox1;
            // 
            // rowLX2
            // 
            this.rowLX2.Height = 25;
            this.rowLX2.Name = "rowLX2";
            this.rowLX2.Properties.Caption = "种类";
            this.rowLX2.Properties.FieldName = "LX2";
            this.rowLX2.Properties.ImageIndex = 25;
            this.rowLX2.Properties.RowEdit = this.repositoryItemComboBox2;
            // 
            // rowVol
            // 
            this.rowVol.Height = 25;
            this.rowVol.Name = "rowVol";
            this.rowVol.Properties.Caption = "容量";
            this.rowVol.Properties.FieldName = "Vol";
            this.rowVol.Properties.ImageIndex = 25;
            this.rowVol.Properties.RowEdit = this.ItemTextEditVol;
            // 
            // editorRow2
            // 
            this.editorRow2.Height = 50;
            this.editorRow2.Name = "editorRow2";
            this.editorRow2.Properties.Caption = "注";
            this.editorRow2.Properties.FieldName = "Col2";
            this.editorRow2.Properties.RowEdit = this.repositoryItemMemoEdit1;
            this.editorRow2.Visible = false;
            // 
            // editorRow1
            // 
            this.editorRow1.Height = 25;
            this.editorRow1.Name = "editorRow1";
            this.editorRow1.Properties.Caption = "时间";
            this.editorRow1.Properties.FieldName = "CreateTime";
            this.editorRow1.Properties.Format.FormatString = "yyyy-MM";
            this.editorRow1.Properties.Format.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.editorRow1.Properties.RowEdit = this.repositoryItemDateEdit1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(189, 239);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(293, 239);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // FrmPSP_VolumeBalance_CalcDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(392, 267);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPSP_VolumeBalance_CalcDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "供电负荷";
            this.Load += new System.EventHandler(this.FrmPSP_VolumeBalance_CalcDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            this.tabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLX2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditVol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
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

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditTitle;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTitle;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditLX1;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLX1;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditLX2;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLX2;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditVol;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowVol;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow editorRow1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
        private System.Windows.Forms.TextBox textBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit1;
        public DevExpress.XtraVerticalGrid.Rows.EditorRow editorRow2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;

	}
}
