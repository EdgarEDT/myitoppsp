
namespace Itop.Client.Forecast
{
	partial class FrmBaseColorDialog
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
            this.ItemTextEditColor = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditMaxValue = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditMinValue = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.rowTitle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowColor = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowMaxValue = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowMinValue = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditMaxValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditMinValue)).BeginInit();
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
            this.panelControl.Size = new System.Drawing.Size(403, 193);
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
            this.xtraTabControl1.Size = new System.Drawing.Size(390, 151);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(386, 124);
            this.tabPage.Text = "颜色";
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
            this.ItemTextEditColor,
            this.ItemTextEditMaxValue,
            this.ItemTextEditMinValue,
            this.repositoryItemColorEdit1});
            this.vGridControl.RowHeaderWidth = 129;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowTitle,
            this.rowColor,
            this.rowMaxValue,
            this.rowMinValue});
            this.vGridControl.Size = new System.Drawing.Size(377, 117);
            this.vGridControl.TabIndex = 0;
            // 
            // ItemTextEditTitle
            // 
            this.ItemTextEditTitle.AutoHeight = false;
            this.ItemTextEditTitle.MaxLength = 100;
            this.ItemTextEditTitle.Name = "ItemTextEditTitle";
            // 
            // ItemTextEditColor
            // 
            this.ItemTextEditColor.AutoHeight = false;
            this.ItemTextEditColor.Name = "ItemTextEditColor";
            // 
            // ItemTextEditMaxValue
            // 
            this.ItemTextEditMaxValue.AutoHeight = false;
            this.ItemTextEditMaxValue.DisplayFormat.FormatString = "n2";
            this.ItemTextEditMaxValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditMaxValue.EditFormat.FormatString = "n2";
            this.ItemTextEditMaxValue.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditMaxValue.Mask.EditMask = "n2";
            this.ItemTextEditMaxValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditMaxValue.Name = "ItemTextEditMaxValue";
            // 
            // ItemTextEditMinValue
            // 
            this.ItemTextEditMinValue.AutoHeight = false;
            this.ItemTextEditMinValue.DisplayFormat.FormatString = "n2";
            this.ItemTextEditMinValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditMinValue.EditFormat.FormatString = "n2";
            this.ItemTextEditMinValue.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ItemTextEditMinValue.Mask.EditMask = "n2";
            this.ItemTextEditMinValue.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.ItemTextEditMinValue.Name = "ItemTextEditMinValue";
            // 
            // repositoryItemColorEdit1
            // 
            this.repositoryItemColorEdit1.AutoHeight = false;
            this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            this.repositoryItemColorEdit1.Validating += new System.ComponentModel.CancelEventHandler(this.repositoryItemColorEdit1_Validating);
            // 
            // rowTitle
            // 
            this.rowTitle.Height = 25;
            this.rowTitle.Name = "rowTitle";
            this.rowTitle.Properties.Caption = "标题";
            this.rowTitle.Properties.FieldName = "Title";
            this.rowTitle.Properties.ImageIndex = 25;
            this.rowTitle.Properties.RowEdit = this.ItemTextEditTitle;
            // 
            // rowColor
            // 
            this.rowColor.Height = 25;
            this.rowColor.Name = "rowColor";
            this.rowColor.Properties.Caption = "颜色";
            this.rowColor.Properties.FieldName = "Color1";
            this.rowColor.Properties.ImageIndex = 25;
            this.rowColor.Properties.RowEdit = this.repositoryItemColorEdit1;
            // 
            // rowMaxValue
            // 
            this.rowMaxValue.Height = 25;
            this.rowMaxValue.Name = "rowMaxValue";
            this.rowMaxValue.Properties.Caption = "最大值";
            this.rowMaxValue.Properties.FieldName = "MaxValue";
            this.rowMaxValue.Properties.ImageIndex = 25;
            this.rowMaxValue.Properties.RowEdit = this.ItemTextEditMaxValue;
            this.rowMaxValue.Visible = false;
            // 
            // rowMinValue
            // 
            this.rowMinValue.Height = 25;
            this.rowMinValue.Name = "rowMinValue";
            this.rowMinValue.Properties.Caption = "最小值";
            this.rowMinValue.Properties.FieldName = "MinValue";
            this.rowMinValue.Properties.ImageIndex = 25;
            this.rowMinValue.Properties.RowEdit = this.ItemTextEditMinValue;
            this.rowMinValue.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(209, 161);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(313, 161);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            // 
            // FrmBaseColorDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(402, 193);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBaseColorDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "颜色";
            this.Load += new System.EventHandler(this.FrmBaseColorDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditMaxValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditMinValue)).EndInit();
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

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditTitle;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTitle;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditColor;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowColor;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditMaxValue;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowMaxValue;

		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditMinValue;
		private DevExpress.XtraVerticalGrid.Rows.EditorRow rowMinValue;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;

	}
}
