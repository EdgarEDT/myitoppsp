
namespace Itop.Client.Stutistics
{
	partial class FrmPs_PowerDialog
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
            this.ItemTextEditFQ = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditTitle = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditDY = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditTS = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditRL = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditWGRL = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditCreateYear = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditUseYear = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rowFQ = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTitle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowDY = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTS = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowRL = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowWGRL = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowCreateYear = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowUseYear = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditFQ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditDY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditRL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditWGRL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditCreateYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditUseYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.xtraTabControl1);
            this.panelControl.Controls.Add(this.btnOK);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(395, 320);
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
            this.xtraTabControl1.Size = new System.Drawing.Size(382, 278);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(378, 251);
            this.tabPage.Text = "aa";
            // 
            // vGridControl
            // 
            this.vGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vGridControl.Location = new System.Drawing.Point(3, 3);
            this.vGridControl.Name = "vGridControl";
            this.vGridControl.RecordWidth = 192;
            this.vGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ItemTextEditFQ,
            this.ItemTextEditTitle,
            this.ItemTextEditDY,
            this.ItemTextEditTS,
            this.ItemTextEditRL,
            this.ItemTextEditWGRL,
            this.ItemTextEditCreateYear,
            this.ItemTextEditUseYear,
            this.repositoryItemComboBox1});
            this.vGridControl.RowHeaderWidth = 154;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowFQ,
            this.rowTitle,
            this.rowDY,
            this.rowTS,
            this.rowRL,
            this.rowWGRL,
            this.rowCreateYear,
            this.rowUseYear});
            this.vGridControl.Size = new System.Drawing.Size(371, 244);
            this.vGridControl.TabIndex = 0;
            // 
            // ItemTextEditFQ
            // 
            this.ItemTextEditFQ.AutoHeight = false;
            this.ItemTextEditFQ.MaxLength = 100;
            this.ItemTextEditFQ.Name = "ItemTextEditFQ";
            // 
            // ItemTextEditTitle
            // 
            this.ItemTextEditTitle.AutoHeight = false;
            this.ItemTextEditTitle.MaxLength = 100;
            this.ItemTextEditTitle.Name = "ItemTextEditTitle";
            // 
            // ItemTextEditDY
            // 
            this.ItemTextEditDY.AutoHeight = false;
            this.ItemTextEditDY.Name = "ItemTextEditDY";
            // 
            // ItemTextEditTS
            // 
            this.ItemTextEditTS.AutoHeight = false;
            this.ItemTextEditTS.Name = "ItemTextEditTS";
            // 
            // ItemTextEditRL
            // 
            this.ItemTextEditRL.AutoHeight = false;
            this.ItemTextEditRL.DisplayFormat.FormatString = "n2";
            this.ItemTextEditRL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditRL.EditFormat.FormatString = "n2";
            this.ItemTextEditRL.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditRL.Mask.EditMask = "n2";
            this.ItemTextEditRL.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditRL.Name = "ItemTextEditRL";
            // 
            // ItemTextEditWGRL
            // 
            this.ItemTextEditWGRL.AutoHeight = false;
            this.ItemTextEditWGRL.DisplayFormat.FormatString = "n2";
            this.ItemTextEditWGRL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditWGRL.EditFormat.FormatString = "n2";
            this.ItemTextEditWGRL.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditWGRL.Mask.EditMask = "n2";
            this.ItemTextEditWGRL.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditWGRL.Name = "ItemTextEditWGRL";
            // 
            // ItemTextEditCreateYear
            // 
            this.ItemTextEditCreateYear.AutoHeight = false;
            this.ItemTextEditCreateYear.Name = "ItemTextEditCreateYear";
            // 
            // ItemTextEditUseYear
            // 
            this.ItemTextEditUseYear.AutoHeight = false;
            this.ItemTextEditUseYear.Name = "ItemTextEditUseYear";
            // 
            // rowFQ
            // 
            this.rowFQ.Height = 25;
            this.rowFQ.Name = "rowFQ";
            this.rowFQ.Properties.Caption = "分区";
            this.rowFQ.Properties.FieldName = "FQ";
            this.rowFQ.Properties.ImageIndex = 25;
            this.rowFQ.Properties.RowEdit = this.ItemTextEditFQ;
            // 
            // rowTitle
            // 
            this.rowTitle.Height = 25;
            this.rowTitle.Name = "rowTitle";
            this.rowTitle.Properties.Caption = "名称";
            this.rowTitle.Properties.FieldName = "Title";
            this.rowTitle.Properties.ImageIndex = 25;
            this.rowTitle.Properties.RowEdit = this.ItemTextEditTitle;
            // 
            // rowDY
            // 
            this.rowDY.Height = 25;
            this.rowDY.Name = "rowDY";
            this.rowDY.Properties.Caption = "电压等级";
            this.rowDY.Properties.FieldName = "DY";
            this.rowDY.Properties.ImageIndex = 25;
            this.rowDY.Properties.RowEdit = this.repositoryItemComboBox1;
            // 
            // rowTS
            // 
            this.rowTS.Height = 25;
            this.rowTS.Name = "rowTS";
            this.rowTS.Properties.Caption = "台数";
            this.rowTS.Properties.FieldName = "TS";
            this.rowTS.Properties.ImageIndex = 25;
            this.rowTS.Properties.RowEdit = this.ItemTextEditTS;
            // 
            // rowRL
            // 
            this.rowRL.Height = 25;
            this.rowRL.Name = "rowRL";
            this.rowRL.Properties.Caption = "容量";
            this.rowRL.Properties.FieldName = "RL";
            this.rowRL.Properties.ImageIndex = 25;
            this.rowRL.Properties.RowEdit = this.ItemTextEditRL;
            // 
            // rowWGRL
            // 
            this.rowWGRL.Height = 25;
            this.rowWGRL.Name = "rowWGRL";
            this.rowWGRL.Properties.Caption = "备用容量";
            this.rowWGRL.Properties.FieldName = "WGRL";
            this.rowWGRL.Properties.ImageIndex = 25;
            this.rowWGRL.Properties.RowEdit = this.ItemTextEditWGRL;
            // 
            // rowCreateYear
            // 
            this.rowCreateYear.Height = 25;
            this.rowCreateYear.Name = "rowCreateYear";
            this.rowCreateYear.Properties.Caption = "建成时间";
            this.rowCreateYear.Properties.FieldName = "CreateYear";
            this.rowCreateYear.Properties.ImageIndex = 25;
            this.rowCreateYear.Properties.RowEdit = this.ItemTextEditCreateYear;
            // 
            // rowUseYear
            // 
            this.rowUseYear.Height = 25;
            this.rowUseYear.Name = "rowUseYear";
            this.rowUseYear.Properties.Caption = "可用年限";
            this.rowUseYear.Properties.FieldName = "UseYear";
            this.rowUseYear.Properties.ImageIndex = 25;
            this.rowUseYear.Properties.RowEdit = this.ItemTextEditUseYear;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(190, 289);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(294, 289);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "500",
            "220",
            "110",
            "66",
            "35",
            "10"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            this.repositoryItemComboBox1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // FrmPs_PowerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 321);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPs_PowerDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "aa";
            this.Load += new System.EventHandler(this.FrmPs_PowerDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditFQ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditDY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditRL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditWGRL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditCreateYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditUseYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private DevExpress.XtraEditors.PanelControl panelControl;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraTab.XtraTabPage tabPage;
		private DevExpress.XtraEditors.SimpleButton btnOK;
		private DevExpress.XtraEditors.SimpleButton btnCancel;
		private DevExpress.XtraVerticalGrid.VGridControl vGridControl;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditFQ;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowFQ;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditTitle;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTitle;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditDY;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowDY;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditTS;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTS;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditRL;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowRL;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditWGRL;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowWGRL;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditCreateYear;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowCreateYear;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditUseYear;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowUseYear;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;

	}
}
