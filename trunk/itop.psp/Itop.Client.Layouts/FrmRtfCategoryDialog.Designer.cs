
namespace Itop.Client.Layouts
{
	partial class FrmRtfCategoryDialog
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
            this.ItemTextEditTitle = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditSortNo = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rowSortNo = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowTitle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditSortNo)).BeginInit();
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
            this.panelControl.Size = new System.Drawing.Size(399, 142);
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
            this.xtraTabControl1.Size = new System.Drawing.Size(386, 100);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(382, 73);
            this.tabPage.Text = "文字资料";
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
            this.ItemTextEditTitle,
            this.ItemTextEditSortNo});
            this.vGridControl.RowHeaderWidth = 129;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowSortNo,
            this.rowTitle});
            this.vGridControl.Size = new System.Drawing.Size(373, 66);
            this.vGridControl.TabIndex = 0;
            // 
            // ItemTextEditTitle
            // 
            this.ItemTextEditTitle.AutoHeight = false;
            this.ItemTextEditTitle.MaxLength = 100;
            this.ItemTextEditTitle.Name = "ItemTextEditTitle";
            // 
            // ItemTextEditSortNo
            // 
            this.ItemTextEditSortNo.AutoHeight = false;
            this.ItemTextEditSortNo.DisplayFormat.FormatString = "n0";
            this.ItemTextEditSortNo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditSortNo.EditFormat.FormatString = "n0";
            this.ItemTextEditSortNo.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditSortNo.Mask.EditMask = "n0";
            this.ItemTextEditSortNo.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditSortNo.Name = "ItemTextEditSortNo";
            // 
            // rowSortNo
            // 
            this.rowSortNo.Appearance.Options.UseTextOptions = true;
            this.rowSortNo.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.rowSortNo.Height = 25;
            this.rowSortNo.Name = "rowSortNo";
            this.rowSortNo.Properties.Caption = "目录序号";
            this.rowSortNo.Properties.FieldName = "SortNo";
            this.rowSortNo.Properties.ImageIndex = 25;
            this.rowSortNo.Properties.RowEdit = this.ItemTextEditSortNo;
            this.rowSortNo.Visible = false;
            // 
            // rowTitle
            // 
            this.rowTitle.Height = 25;
            this.rowTitle.Name = "rowTitle";
            this.rowTitle.Properties.Caption = "目录名称";
            this.rowTitle.Properties.FieldName = "Title";
            this.rowTitle.Properties.ImageIndex = 25;
            this.rowTitle.Properties.RowEdit = this.ItemTextEditTitle;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(210, 111);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(314, 111);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // FrmRtfCategoryDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(399, 142);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRtfCategoryDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文字资料";
            this.Load += new System.EventHandler(this.FrmRtfCategoryDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditSortNo)).EndInit();
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

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditSortNo;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowSortNo;

	}
}
