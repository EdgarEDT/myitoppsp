
namespace Itop.Client.Stutistics
{
	partial class FrmPowerProjectDialog
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
            this.ItemTextEditTotal = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditVolume = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditLengths = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditType = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditPlanStartYear = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditPlanEndYear = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rowStuffName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTotal = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowVolume = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLengths = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowPlanStartYear = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowPlanEndYear = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditStuffName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLengths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditPlanStartYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditPlanEndYear)).BeginInit();
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
            this.panelControl.Size = new System.Drawing.Size(403, 270);
            this.panelControl.TabIndex = 0;
            this.panelControl.Text = "panelControl1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(26, -240);
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
            this.xtraTabControl1.Size = new System.Drawing.Size(390, 228);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(384, 201);
            this.tabPage.Text = "输变电建设项目明细表";
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
            this.ItemTextEditTotal,
            this.ItemTextEditVolume,
            this.ItemTextEditLengths,
            this.ItemTextEditType,
            this.ItemTextEditPlanStartYear,
            this.ItemTextEditPlanEndYear,
            this.repositoryItemButtonEdit1});
            this.vGridControl.RowHeaderWidth = 129;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowStuffName,
            this.rowTotal,
            this.rowVolume,
            this.rowLengths,
            this.rowType,
            this.rowPlanStartYear,
            this.rowPlanEndYear});
            this.vGridControl.Size = new System.Drawing.Size(377, 194);
            this.vGridControl.TabIndex = 0;
            this.vGridControl.FocusedRowChanged += new DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventHandler(this.vGridControl_FocusedRowChanged);
            // 
            // ItemTextEditStuffName
            // 
            this.ItemTextEditStuffName.AutoHeight = false;
            this.ItemTextEditStuffName.MaxLength = 100;
            this.ItemTextEditStuffName.Name = "ItemTextEditStuffName";
            // 
            // ItemTextEditTotal
            // 
            this.ItemTextEditTotal.AutoHeight = false;
            this.ItemTextEditTotal.MaxLength = 50;
            this.ItemTextEditTotal.Name = "ItemTextEditTotal";
            // 
            // ItemTextEditVolume
            // 
            this.ItemTextEditVolume.AutoHeight = false;
            this.ItemTextEditVolume.MaxLength = 50;
            this.ItemTextEditVolume.Name = "ItemTextEditVolume";
            // 
            // ItemTextEditLengths
            // 
            this.ItemTextEditLengths.AutoHeight = false;
            this.ItemTextEditLengths.MaxLength = 50;
            this.ItemTextEditLengths.Name = "ItemTextEditLengths";
            // 
            // ItemTextEditType
            // 
            this.ItemTextEditType.AutoHeight = false;
            this.ItemTextEditType.MaxLength = 50;
            this.ItemTextEditType.Name = "ItemTextEditType";
            // 
            // ItemTextEditPlanStartYear
            // 
            this.ItemTextEditPlanStartYear.AutoHeight = false;
            this.ItemTextEditPlanStartYear.EditFormat.FormatString = "####";
            this.ItemTextEditPlanStartYear.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditPlanStartYear.Mask.EditMask = "####";
            this.ItemTextEditPlanStartYear.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditPlanStartYear.MaxLength = 50;
            this.ItemTextEditPlanStartYear.Name = "ItemTextEditPlanStartYear";
            // 
            // ItemTextEditPlanEndYear
            // 
            this.ItemTextEditPlanEndYear.AutoHeight = false;
            this.ItemTextEditPlanEndYear.EditFormat.FormatString = "####";
            this.ItemTextEditPlanEndYear.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditPlanEndYear.Mask.EditMask = "####";
            this.ItemTextEditPlanEndYear.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditPlanEndYear.MaxLength = 50;
            this.ItemTextEditPlanEndYear.Name = "ItemTextEditPlanEndYear";
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
            this.rowStuffName.Properties.Caption = "项目名称";
            this.rowStuffName.Properties.FieldName = "StuffName";
            this.rowStuffName.Properties.ImageIndex = 25;
            this.rowStuffName.Properties.RowEdit = this.ItemTextEditStuffName;
            // 
            // rowTotal
            // 
            this.rowTotal.Height = 25;
            this.rowTotal.Name = "rowTotal";
            this.rowTotal.Properties.Caption = "台数";
            this.rowTotal.Properties.FieldName = "Total";
            this.rowTotal.Properties.ImageIndex = 25;
            this.rowTotal.Properties.RowEdit = this.ItemTextEditTotal;
            // 
            // rowVolume
            // 
            this.rowVolume.Height = 25;
            this.rowVolume.Name = "rowVolume";
            this.rowVolume.Properties.Caption = "容量";
            this.rowVolume.Properties.FieldName = "Volume";
            this.rowVolume.Properties.ImageIndex = 25;
            this.rowVolume.Properties.RowEdit = this.ItemTextEditVolume;
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
            // rowType
            // 
            this.rowType.Height = 25;
            this.rowType.Name = "rowType";
            this.rowType.Properties.Caption = "型号";
            this.rowType.Properties.FieldName = "Type";
            this.rowType.Properties.ImageIndex = 25;
            this.rowType.Properties.RowEdit = this.ItemTextEditType;
            // 
            // rowPlanStartYear
            // 
            this.rowPlanStartYear.Height = 25;
            this.rowPlanStartYear.Name = "rowPlanStartYear";
            this.rowPlanStartYear.Properties.Caption = "计划开工时间";
            this.rowPlanStartYear.Properties.FieldName = "PlanStartYear";
            this.rowPlanStartYear.Properties.ImageIndex = 25;
            // 
            // rowPlanEndYear
            // 
            this.rowPlanEndYear.Height = 25;
            this.rowPlanEndYear.Name = "rowPlanEndYear";
            this.rowPlanEndYear.Properties.Caption = "预计投产时间";
            this.rowPlanEndYear.Properties.FieldName = "PlanEndYear";
            this.rowPlanEndYear.Properties.ImageIndex = 25;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(216, 240);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(320, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // FrmPowerProjectDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 267);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPowerProjectDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "输变电建设项目明细表";
            this.Load += new System.EventHandler(this.FrmPowerProjectDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditStuffName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLengths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditPlanStartYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditPlanEndYear)).EndInit();
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

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditTotal;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTotal;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditVolume;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowVolume;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditLengths;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLengths;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditType;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowType;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditPlanStartYear;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowPlanStartYear;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditPlanEndYear;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowPlanEndYear;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private System.Windows.Forms.TextBox textBox1;

	}
}
