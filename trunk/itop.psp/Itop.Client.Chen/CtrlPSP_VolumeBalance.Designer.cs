
namespace Itop.Client.Chen
{
	partial class CtrlPSP_VolumeBalance
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoExEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.repositoryItemMemoExEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemMemoEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControl.EmbeddedNavigator.Name = "";
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemMemoExEdit1,
            this.repositoryItemMemoExEdit2,
            this.repositoryItemMemoEdit1,
            this.repositoryItemMemoEdit2,
            this.repositoryItemMemoEdit3});
            this.gridControl.Size = new System.Drawing.Size(746, 412);
            this.gridControl.TabIndex = 0;
            this.gridControl.UseEmbeddedNavigator = true;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.colL1,
            this.colL2,
            this.colL15,
            this.colL3,
            this.colL4,
            this.colL16,
            this.colL5,
            this.colL17,
            this.colL6,
            this.colL7,
            this.colL8,
            this.colL9,
            this.colL10,
            this.colL11,
            this.colL12,
            this.colL13,
            this.colL14});
            this.gridView.GridControl = this.gridControl;
            this.gridView.GroupPanelText = "110千伏变电容量平衡表 ";
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsPrint.UsePrintStyles = true;
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.OptionsView.RowAutoHeight = true;
            this.gridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "年度";
            this.gridColumn1.FieldName = "Year";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // colL1
            // 
            this.colL1.AppearanceHeader.Options.UseTextOptions = true;
            this.colL1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL1.Caption = "分区综合最高负荷";
            this.colL1.DisplayFormat.FormatString = "n2";
            this.colL1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL1.FieldName = "L1";
            this.colL1.Name = "colL1";
            this.colL1.Visible = true;
            this.colL1.VisibleIndex = 1;
            this.colL1.Width = 121;
            // 
            // colL2
            // 
            this.colL2.AppearanceHeader.Options.UseTextOptions = true;
            this.colL2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL2.Caption = "220kV主变35kV侧可供负荷";
            this.colL2.DisplayFormat.FormatString = "n2";
            this.colL2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL2.FieldName = "L2";
            this.colL2.Name = "colL2";
            this.colL2.Visible = true;
            this.colL2.VisibleIndex = 2;
            this.colL2.Width = 109;
            // 
            // colL15
            // 
            this.colL15.Caption = "注";
            this.colL15.ColumnEdit = this.repositoryItemMemoEdit1;
            this.colL15.FieldName = "S2";
            this.colL15.Name = "colL15";
            this.colL15.Visible = true;
            this.colL15.VisibleIndex = 3;
            // 
            // colL3
            // 
            this.colL3.AppearanceHeader.Options.UseTextOptions = true;
            this.colL3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL3.Caption = "小电厂需备用容量";
            this.colL3.DisplayFormat.FormatString = "n2";
            this.colL3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL3.FieldName = "L3";
            this.colL3.Name = "colL3";
            this.colL3.Width = 120;
            // 
            // colL4
            // 
            this.colL4.AppearanceHeader.Options.UseTextOptions = true;
            this.colL4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL4.Caption = "110kV及以下小电源直接供电负荷";
            this.colL4.DisplayFormat.FormatString = "n2";
            this.colL4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL4.FieldName = "L4";
            this.colL4.Name = "colL4";
            this.colL4.Visible = true;
            this.colL4.VisibleIndex = 4;
            this.colL4.Width = 130;
            // 
            // colL16
            // 
            this.colL16.Caption = "注";
            this.colL16.ColumnEdit = this.repositoryItemMemoEdit2;
            this.colL16.FieldName = "S3";
            this.colL16.Name = "colL16";
            this.colL16.Visible = true;
            this.colL16.VisibleIndex = 5;
            // 
            // colL5
            // 
            this.colL5.AppearanceHeader.Options.UseTextOptions = true;
            this.colL5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL5.Caption = "需110kV降压供电负荷";
            this.colL5.DisplayFormat.FormatString = "n2";
            this.colL5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL5.FieldName = "L5";
            this.colL5.Name = "colL5";
            this.colL5.Visible = true;
            this.colL5.VisibleIndex = 6;
            this.colL5.Width = 138;
            // 
            // colL17
            // 
            this.colL17.Caption = "注";
            this.colL17.ColumnEdit = this.repositoryItemMemoEdit3;
            this.colL17.FieldName = "S4";
            this.colL17.Name = "colL17";
            this.colL17.Visible = true;
            this.colL17.VisibleIndex = 7;
            // 
            // colL6
            // 
            this.colL6.AppearanceHeader.Options.UseTextOptions = true;
            this.colL6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL6.Caption = "现有110kV降压变电容量";
            this.colL6.DisplayFormat.FormatString = "n2";
            this.colL6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL6.FieldName = "L6";
            this.colL6.Name = "colL6";
            this.colL6.Visible = true;
            this.colL6.VisibleIndex = 8;
            this.colL6.Width = 201;
            // 
            // colL7
            // 
            this.colL7.AppearanceHeader.Options.UseTextOptions = true;
            this.colL7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL7.Caption = "110kV容载比";
            this.colL7.DisplayFormat.FormatString = "n2";
            this.colL7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL7.FieldName = "L7";
            this.colL7.Name = "colL7";
            this.colL7.Visible = true;
            this.colL7.VisibleIndex = 9;
            this.colL7.Width = 100;
            // 
            // colL8
            // 
            this.colL8.AppearanceHeader.Options.UseTextOptions = true;
            this.colL8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL8.Caption = "需110kV变电容量";
            this.colL8.DisplayFormat.FormatString = "n2";
            this.colL8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL8.FieldName = "L8";
            this.colL8.Name = "colL8";
            this.colL8.Visible = true;
            this.colL8.VisibleIndex = 10;
            this.colL8.Width = 130;
            // 
            // colL9
            // 
            this.colL9.AppearanceHeader.Options.UseTextOptions = true;
            this.colL9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL9.Caption = "变电容量盈亏";
            this.colL9.DisplayFormat.FormatString = "n2";
            this.colL9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL9.FieldName = "L9";
            this.colL9.Name = "colL9";
            this.colL9.Visible = true;
            this.colL9.VisibleIndex = 11;
            this.colL9.Width = 100;
            // 
            // colL10
            // 
            this.colL10.AppearanceHeader.Options.UseTextOptions = true;
            this.colL10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL10.Caption = "已立项的变电容量";
            this.colL10.DisplayFormat.FormatString = "n2";
            this.colL10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL10.FieldName = "L10";
            this.colL10.Name = "colL10";
            this.colL10.Visible = true;
            this.colL10.VisibleIndex = 12;
            this.colL10.Width = 140;
            // 
            // colL11
            // 
            this.colL11.AppearanceHeader.Options.UseTextOptions = true;
            this.colL11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL11.Caption = "规划新增变电容量";
            this.colL11.DisplayFormat.FormatString = "n2";
            this.colL11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL11.FieldName = "L11";
            this.colL11.Name = "colL11";
            this.colL11.Visible = true;
            this.colL11.VisibleIndex = 13;
            this.colL11.Width = 140;
            // 
            // colL12
            // 
            this.colL12.AppearanceHeader.Options.UseTextOptions = true;
            this.colL12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL12.Caption = "变电容量合计";
            this.colL12.DisplayFormat.FormatString = "n2";
            this.colL12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL12.FieldName = "L12";
            this.colL12.Name = "colL12";
            this.colL12.Visible = true;
            this.colL12.VisibleIndex = 14;
            this.colL12.Width = 100;
            // 
            // colL13
            // 
            this.colL13.AppearanceHeader.Options.UseTextOptions = true;
            this.colL13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL13.Caption = "容载比";
            this.colL13.DisplayFormat.FormatString = "n2";
            this.colL13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL13.FieldName = "L13";
            this.colL13.Name = "colL13";
            this.colL13.Visible = true;
            this.colL13.VisibleIndex = 15;
            // 
            // colL14
            // 
            this.colL14.AppearanceHeader.Options.UseTextOptions = true;
            this.colL14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL14.Caption = "备注";
            this.colL14.FieldName = "L14";
            this.colL14.Name = "colL14";
            this.colL14.Visible = true;
            this.colL14.VisibleIndex = 16;
            this.colL14.Width = 100;
            // 
            // repositoryItemMemoExEdit1
            // 
            this.repositoryItemMemoExEdit1.AutoHeight = false;
            this.repositoryItemMemoExEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit1.Name = "repositoryItemMemoExEdit1";
            // 
            // repositoryItemMemoExEdit2
            // 
            this.repositoryItemMemoExEdit2.AutoHeight = false;
            this.repositoryItemMemoExEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemMemoExEdit2.Name = "repositoryItemMemoExEdit2";
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemMemoEdit2
            // 
            this.repositoryItemMemoEdit2.Name = "repositoryItemMemoEdit2";
            // 
            // repositoryItemMemoEdit3
            // 
            this.repositoryItemMemoEdit3.Name = "repositoryItemMemoEdit3";
            // 
            // CtrlPSP_VolumeBalance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Name = "CtrlPSP_VolumeBalance";
            this.Size = new System.Drawing.Size(746, 412);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoExEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit3)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;  private DevExpress.XtraGrid.Columns.GridColumn colL10; private DevExpress.XtraGrid.Columns.GridColumn colL11;  private DevExpress.XtraGrid.Columns.GridColumn colL13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        public DevExpress.XtraGrid.Columns.GridColumn colL1;
        public DevExpress.XtraGrid.Columns.GridColumn colL2;
        public DevExpress.XtraGrid.Columns.GridColumn colL3;
        public DevExpress.XtraGrid.Columns.GridColumn colL4;
        public DevExpress.XtraGrid.Columns.GridColumn colL5;
        public DevExpress.XtraGrid.Columns.GridColumn colL6;
        public DevExpress.XtraGrid.Columns.GridColumn colL7;
        public DevExpress.XtraGrid.Columns.GridColumn colL8;
        public DevExpress.XtraGrid.Columns.GridColumn colL9;
        public DevExpress.XtraGrid.Columns.GridColumn colL12;
        public DevExpress.XtraGrid.Columns.GridColumn colL14;
        private DevExpress.XtraGrid.Columns.GridColumn colL17;
        public DevExpress.XtraGrid.Columns.GridColumn colL15;
        public DevExpress.XtraGrid.Columns.GridColumn colL16;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoExEdit repositoryItemMemoExEdit2;
    }
}
