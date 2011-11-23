
namespace Itop.Client.Stutistics
{
	partial class FrmPowerEachTotalDialog
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
            this.ItemTextEditStuffName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditLengths = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditLCount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditTotal = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditVolume = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditType = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditIsSum = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditItSum = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rowStuffName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLengths = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLCount = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTotal = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowVolume = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowIsSum = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowItSum = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditStuffName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLengths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditIsSum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditItSum)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.xtraTabControl1);
            this.panelControl.Controls.Add(this.btnOK);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(400, 294);
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
            this.xtraTabControl1.Size = new System.Drawing.Size(387, 252);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(381, 225);
            this.tabPage.Text = "资金需求汇总表";
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
            this.ItemTextEditLengths,
            this.ItemTextEditLCount,
            this.ItemTextEditTotal,
            this.ItemTextEditVolume,
            this.ItemTextEditType,
            this.ItemTextEditIsSum,
            this.ItemTextEditItSum});
            this.vGridControl.RowHeaderWidth = 129;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowStuffName,
            this.rowLengths,
            this.rowLCount,
            this.rowTotal,
            this.rowVolume,
            this.rowType,
            this.rowIsSum,
            this.rowItSum});
            this.vGridControl.Size = new System.Drawing.Size(374, 218);
            this.vGridControl.TabIndex = 0;
            this.vGridControl.FocusedRowChanged += new DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventHandler(this.vGridControl_FocusedRowChanged);
            // 
            // ItemTextEditStuffName
            // 
            this.ItemTextEditStuffName.AutoHeight = false;
            this.ItemTextEditStuffName.MaxLength = 100;
            this.ItemTextEditStuffName.Name = "ItemTextEditStuffName";
            // 
            // ItemTextEditLengths
            // 
            this.ItemTextEditLengths.AutoHeight = false;
            this.ItemTextEditLengths.MaxLength = 50;
            this.ItemTextEditLengths.Name = "ItemTextEditLengths";
            // 
            // ItemTextEditLCount
            // 
            this.ItemTextEditLCount.AutoHeight = false;
            this.ItemTextEditLCount.MaxLength = 50;
            this.ItemTextEditLCount.Name = "ItemTextEditLCount";
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
            // ItemTextEditType
            // 
            this.ItemTextEditType.AutoHeight = false;
            this.ItemTextEditType.MaxLength = 50;
            this.ItemTextEditType.Name = "ItemTextEditType";
            // 
            // ItemTextEditIsSum
            // 
            this.ItemTextEditIsSum.AutoHeight = false;
            this.ItemTextEditIsSum.DisplayFormat.FormatString = "n2";
            this.ItemTextEditIsSum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditIsSum.EditFormat.FormatString = "n2";
            this.ItemTextEditIsSum.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditIsSum.Mask.EditMask = "n2";
            this.ItemTextEditIsSum.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditIsSum.Name = "ItemTextEditIsSum";
            // 
            // ItemTextEditItSum
            // 
            this.ItemTextEditItSum.AutoHeight = false;
            this.ItemTextEditItSum.DisplayFormat.FormatString = "n2";
            this.ItemTextEditItSum.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditItSum.EditFormat.FormatString = "n2";
            this.ItemTextEditItSum.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditItSum.Mask.EditMask = "n2";
            this.ItemTextEditItSum.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditItSum.Name = "ItemTextEditItSum";
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
            // rowLengths
            // 
            this.rowLengths.Height = 25;
            this.rowLengths.Name = "rowLengths";
            this.rowLengths.Properties.Caption = "项目总长度";
            this.rowLengths.Properties.FieldName = "Lengths";
            this.rowLengths.Properties.ImageIndex = 25;
            this.rowLengths.Properties.RowEdit = this.ItemTextEditLengths;
            // 
            // rowLCount
            // 
            this.rowLCount.Height = 25;
            this.rowLCount.Name = "rowLCount";
            this.rowLCount.Properties.Caption = "座数";
            this.rowLCount.Properties.FieldName = "LCount";
            this.rowLCount.Properties.ImageIndex = 25;
            this.rowLCount.Properties.RowEdit = this.ItemTextEditLCount;
            // 
            // rowTotal
            // 
            this.rowTotal.Height = 25;
            this.rowTotal.Name = "rowTotal";
            this.rowTotal.Properties.Caption = "主变台数";
            this.rowTotal.Properties.FieldName = "Total";
            this.rowTotal.Properties.ImageIndex = 25;
            this.rowTotal.Properties.RowEdit = this.ItemTextEditTotal;
            // 
            // rowVolume
            // 
            this.rowVolume.Height = 25;
            this.rowVolume.Name = "rowVolume";
            this.rowVolume.Properties.Caption = "主变总容量";
            this.rowVolume.Properties.FieldName = "Volume";
            this.rowVolume.Properties.ImageIndex = 25;
            this.rowVolume.Properties.RowEdit = this.ItemTextEditVolume;
            // 
            // rowType
            // 
            this.rowType.Height = 25;
            this.rowType.Name = "rowType";
            this.rowType.Properties.Caption = "其他";
            this.rowType.Properties.FieldName = "Type";
            this.rowType.Properties.ImageIndex = 25;
            this.rowType.Properties.RowEdit = this.ItemTextEditType;
            // 
            // rowIsSum
            // 
            this.rowIsSum.Height = 25;
            this.rowIsSum.Name = "rowIsSum";
            this.rowIsSum.Properties.Caption = "静态投资合计";
            this.rowIsSum.Properties.FieldName = "IsSum";
            this.rowIsSum.Properties.ImageIndex = 25;
            this.rowIsSum.Properties.RowEdit = this.ItemTextEditIsSum;
            // 
            // rowItSum
            // 
            this.rowItSum.Height = 25;
            this.rowItSum.Name = "rowItSum";
            this.rowItSum.Properties.Caption = "动态投资合计";
            this.rowItSum.Properties.FieldName = "ItSum";
            this.rowItSum.Properties.ImageIndex = 25;
            this.rowItSum.Properties.RowEdit = this.ItemTextEditItSum;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(215, 265);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(319, 265);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // FrmPowerEachTotalDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 292);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPowerEachTotalDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "资金需求汇总表";
            this.Load += new System.EventHandler(this.FrmPowerEachTotalDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditStuffName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLengths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditLCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditIsSum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditItSum)).EndInit();
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

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditLengths;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLengths;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditLCount;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLCount;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditTotal;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTotal;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditVolume;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowVolume;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditType;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowType;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditIsSum;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowIsSum;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditItSum;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowItSum;

	}
}
