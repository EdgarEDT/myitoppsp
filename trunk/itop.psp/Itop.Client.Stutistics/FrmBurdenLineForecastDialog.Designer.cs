
namespace Itop.Client.Stutistics
{
	partial class FrmBurdenLineForecastDialog
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
            this.ItemTextEditBurdenYear = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditSummerDayAverage = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditSummerMinAverage = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditWinterDayAverage = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditWinterMinAverage = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rowBurdenYear = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryRow1 = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowSummerDayAverage = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowSummerMinAverage = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.editorRow1 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryRow2 = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowWinterDayAverage = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowWinterMinAverage = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.editorRow2 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditBurdenYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditSummerDayAverage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditSummerMinAverage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditWinterDayAverage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditWinterMinAverage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.xtraTabControl1);
            this.panelControl.Controls.Add(this.btnOK);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(410, 319);
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
            this.xtraTabControl1.Size = new System.Drawing.Size(397, 277);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(391, 250);
            this.tabPage.Text = "冬夏典型日负荷曲线预测";
            // 
            // vGridControl
            // 
            this.vGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vGridControl.Location = new System.Drawing.Point(3, 3);
            this.vGridControl.Name = "vGridControl";
            this.vGridControl.RecordWidth = 165;
            this.vGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ItemTextEditBurdenYear,
            this.ItemTextEditSummerDayAverage,
            this.ItemTextEditSummerMinAverage,
            this.ItemTextEditWinterDayAverage,
            this.ItemTextEditWinterMinAverage,
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
            this.vGridControl.RowHeaderWidth = 201;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowBurdenYear,
            this.categoryRow1,
            this.categoryRow2});
            this.vGridControl.Size = new System.Drawing.Size(384, 243);
            this.vGridControl.TabIndex = 0;
            this.vGridControl.FocusedRowChanged += new DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventHandler(this.vGridControl_FocusedRowChanged);
            // 
            // ItemTextEditBurdenYear
            // 
            this.ItemTextEditBurdenYear.AutoHeight = false;
            this.ItemTextEditBurdenYear.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditBurdenYear.EditFormat.FormatString = "###0";
            this.ItemTextEditBurdenYear.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditBurdenYear.Mask.EditMask = "###0";
            this.ItemTextEditBurdenYear.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditBurdenYear.Name = "ItemTextEditBurdenYear";
            this.ItemTextEditBurdenYear.Validating += new System.ComponentModel.CancelEventHandler(this.ItemTextEditBurdenYear_Validating);
            // 
            // ItemTextEditSummerDayAverage
            // 
            this.ItemTextEditSummerDayAverage.AutoHeight = false;
            this.ItemTextEditSummerDayAverage.DisplayFormat.FormatString = "n2";
            this.ItemTextEditSummerDayAverage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditSummerDayAverage.EditFormat.FormatString = "n2";
            this.ItemTextEditSummerDayAverage.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditSummerDayAverage.Mask.EditMask = "n2";
            this.ItemTextEditSummerDayAverage.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditSummerDayAverage.Name = "ItemTextEditSummerDayAverage";
            // 
            // ItemTextEditSummerMinAverage
            // 
            this.ItemTextEditSummerMinAverage.AutoHeight = false;
            this.ItemTextEditSummerMinAverage.DisplayFormat.FormatString = "n2";
            this.ItemTextEditSummerMinAverage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditSummerMinAverage.EditFormat.FormatString = "n2";
            this.ItemTextEditSummerMinAverage.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditSummerMinAverage.Mask.EditMask = "n2";
            this.ItemTextEditSummerMinAverage.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditSummerMinAverage.Name = "ItemTextEditSummerMinAverage";
            // 
            // ItemTextEditWinterDayAverage
            // 
            this.ItemTextEditWinterDayAverage.AutoHeight = false;
            this.ItemTextEditWinterDayAverage.DisplayFormat.FormatString = "n2";
            this.ItemTextEditWinterDayAverage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditWinterDayAverage.EditFormat.FormatString = "n2";
            this.ItemTextEditWinterDayAverage.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditWinterDayAverage.Mask.EditMask = "n2";
            this.ItemTextEditWinterDayAverage.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditWinterDayAverage.Name = "ItemTextEditWinterDayAverage";
            // 
            // ItemTextEditWinterMinAverage
            // 
            this.ItemTextEditWinterMinAverage.AutoHeight = false;
            this.ItemTextEditWinterMinAverage.DisplayFormat.FormatString = "n2";
            this.ItemTextEditWinterMinAverage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditWinterMinAverage.EditFormat.FormatString = "n2";
            this.ItemTextEditWinterMinAverage.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditWinterMinAverage.Mask.EditMask = "n2";
            this.ItemTextEditWinterMinAverage.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditWinterMinAverage.Name = "ItemTextEditWinterMinAverage";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.DisplayFormat.FormatString = "n2";
            this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.Mask.EditMask = "n2";
            this.repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.DisplayFormat.FormatString = "n2";
            this.repositoryItemTextEdit2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit2.Mask.EditMask = "n2";
            this.repositoryItemTextEdit2.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // rowBurdenYear
            // 
            this.rowBurdenYear.Height = 25;
            this.rowBurdenYear.Name = "rowBurdenYear";
            this.rowBurdenYear.Properties.Caption = "年度";
            this.rowBurdenYear.Properties.FieldName = "BurdenYear";
            this.rowBurdenYear.Properties.ImageIndex = 25;
            this.rowBurdenYear.Properties.RowEdit = this.ItemTextEditBurdenYear;
            // 
            // categoryRow1
            // 
            this.categoryRow1.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowSummerDayAverage,
            this.rowSummerMinAverage,
            this.editorRow1});
            this.categoryRow1.Name = "categoryRow1";
            this.categoryRow1.Properties.Caption = "夏季";
            // 
            // rowSummerDayAverage
            // 
            this.rowSummerDayAverage.Height = 25;
            this.rowSummerDayAverage.Name = "rowSummerDayAverage";
            this.rowSummerDayAverage.Properties.Caption = "夏季日平均负荷率(%)";
            this.rowSummerDayAverage.Properties.FieldName = "SummerDayAverage";
            this.rowSummerDayAverage.Properties.ImageIndex = 25;
            this.rowSummerDayAverage.Properties.RowEdit = this.ItemTextEditSummerDayAverage;
            // 
            // rowSummerMinAverage
            // 
            this.rowSummerMinAverage.Height = 25;
            this.rowSummerMinAverage.Name = "rowSummerMinAverage";
            this.rowSummerMinAverage.Properties.Caption = "夏季日最小负荷率(%)";
            this.rowSummerMinAverage.Properties.FieldName = "SummerMinAverage";
            this.rowSummerMinAverage.Properties.ImageIndex = 25;
            this.rowSummerMinAverage.Properties.RowEdit = this.ItemTextEditSummerMinAverage;
            // 
            // editorRow1
            // 
            this.editorRow1.Height = 25;
            this.editorRow1.Name = "editorRow1";
            this.editorRow1.Properties.Caption = "Tmax";
            this.editorRow1.Properties.FieldName = "SummerData";
            this.editorRow1.Properties.RowEdit = this.repositoryItemTextEdit1;
            // 
            // categoryRow2
            // 
            this.categoryRow2.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowWinterDayAverage,
            this.rowWinterMinAverage,
            this.editorRow2});
            this.categoryRow2.Name = "categoryRow2";
            this.categoryRow2.Properties.Caption = "冬季";
            // 
            // rowWinterDayAverage
            // 
            this.rowWinterDayAverage.Height = 25;
            this.rowWinterDayAverage.Name = "rowWinterDayAverage";
            this.rowWinterDayAverage.Properties.Caption = "冬季日平均负荷率(%)";
            this.rowWinterDayAverage.Properties.FieldName = "WinterDayAverage";
            this.rowWinterDayAverage.Properties.ImageIndex = 25;
            this.rowWinterDayAverage.Properties.RowEdit = this.ItemTextEditWinterDayAverage;
            // 
            // rowWinterMinAverage
            // 
            this.rowWinterMinAverage.Height = 25;
            this.rowWinterMinAverage.Name = "rowWinterMinAverage";
            this.rowWinterMinAverage.Properties.Caption = "冬季日最小负荷率(%)";
            this.rowWinterMinAverage.Properties.FieldName = "WinterMinAverage";
            this.rowWinterMinAverage.Properties.ImageIndex = 25;
            this.rowWinterMinAverage.Properties.RowEdit = this.ItemTextEditWinterMinAverage;
            // 
            // editorRow2
            // 
            this.editorRow2.Height = 25;
            this.editorRow2.Name = "editorRow2";
            this.editorRow2.Properties.Caption = "Tmax";
            this.editorRow2.Properties.FieldName = "WinterData";
            this.editorRow2.Properties.RowEdit = this.repositoryItemTextEdit2;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(209, 289);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定(&O)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(313, 289);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消(&C)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(290, -1);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 1;
            // 
            // FrmBurdenLineForecastDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(412, 331);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBurdenLineForecastDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "冬夏典型日负荷曲线预测";
            this.Load += new System.EventHandler(this.FrmBurdenLineForecastDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditBurdenYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditSummerDayAverage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditSummerMinAverage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditWinterDayAverage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditWinterMinAverage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private DevExpress.XtraEditors.PanelControl panelControl;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
		private DevExpress.XtraTab.XtraTabPage tabPage;
		private DevExpress.XtraEditors.SimpleButton btnOK;
		private DevExpress.XtraEditors.SimpleButton btnCancel;
		private DevExpress.XtraVerticalGrid.VGridControl vGridControl;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditBurdenYear;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowBurdenYear;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditSummerDayAverage;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowSummerDayAverage;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditSummerMinAverage;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowSummerMinAverage;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditWinterDayAverage;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowWinterDayAverage;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditWinterMinAverage;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowWinterMinAverage;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryRow1;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow editorRow1;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryRow2;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow editorRow2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private System.Windows.Forms.TextBox textBox1;

	}
}
