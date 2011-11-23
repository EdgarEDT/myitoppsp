
namespace Itop.Client.Stutistics
{
	partial class CtrlBurdenMonth
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
            this.colBurdenYear = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMonth1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMonth2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMonth3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMonth4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMonth5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMonth6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMonth7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMonth8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMonth9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMonth10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMonth11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMonth12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
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
            this.repositoryItemLookUpEdit1});
            this.gridControl.Size = new System.Drawing.Size(534, 319);
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
            this.colBurdenYear,
            this.colMonth1,
            this.colMonth2,
            this.colMonth3,
            this.colMonth4,
            this.colMonth5,
            this.colMonth6,
            this.colMonth7,
            this.colMonth8,
            this.colMonth9,
            this.colMonth10,
            this.colMonth11,
            this.colMonth12});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsPrint.UsePrintStyles = true;
            this.gridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colBurdenYear, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "地区";
            this.gridColumn1.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.gridColumn1.FieldName = "AreaID";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // colBurdenYear
            // 
            this.colBurdenYear.AppearanceHeader.Options.UseTextOptions = true;
            this.colBurdenYear.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBurdenYear.Caption = "年度";
            this.colBurdenYear.FieldName = "BurdenYear";
            this.colBurdenYear.Name = "colBurdenYear";
            this.colBurdenYear.Visible = true;
            this.colBurdenYear.VisibleIndex = 1;
            // 
            // colMonth1
            // 
            this.colMonth1.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth1.Caption = "1月";
            this.colMonth1.DisplayFormat.FormatString = "n2";
            this.colMonth1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMonth1.FieldName = "Month1";
            this.colMonth1.Name = "colMonth1";
            this.colMonth1.Visible = true;
            this.colMonth1.VisibleIndex = 2;
            // 
            // colMonth2
            // 
            this.colMonth2.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth2.Caption = "2月";
            this.colMonth2.DisplayFormat.FormatString = "n2";
            this.colMonth2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMonth2.FieldName = "Month2";
            this.colMonth2.Name = "colMonth2";
            this.colMonth2.Visible = true;
            this.colMonth2.VisibleIndex = 3;
            // 
            // colMonth3
            // 
            this.colMonth3.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth3.Caption = "3月";
            this.colMonth3.DisplayFormat.FormatString = "n2";
            this.colMonth3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMonth3.FieldName = "Month3";
            this.colMonth3.Name = "colMonth3";
            this.colMonth3.Visible = true;
            this.colMonth3.VisibleIndex = 4;
            // 
            // colMonth4
            // 
            this.colMonth4.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth4.Caption = "4月";
            this.colMonth4.DisplayFormat.FormatString = "n2";
            this.colMonth4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMonth4.FieldName = "Month4";
            this.colMonth4.Name = "colMonth4";
            this.colMonth4.Visible = true;
            this.colMonth4.VisibleIndex = 5;
            // 
            // colMonth5
            // 
            this.colMonth5.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth5.Caption = "5月";
            this.colMonth5.DisplayFormat.FormatString = "n2";
            this.colMonth5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMonth5.FieldName = "Month5";
            this.colMonth5.Name = "colMonth5";
            this.colMonth5.Visible = true;
            this.colMonth5.VisibleIndex = 6;
            // 
            // colMonth6
            // 
            this.colMonth6.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth6.Caption = "6月";
            this.colMonth6.DisplayFormat.FormatString = "n2";
            this.colMonth6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMonth6.FieldName = "Month6";
            this.colMonth6.Name = "colMonth6";
            this.colMonth6.Visible = true;
            this.colMonth6.VisibleIndex = 7;
            // 
            // colMonth7
            // 
            this.colMonth7.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth7.Caption = "7月";
            this.colMonth7.DisplayFormat.FormatString = "n2";
            this.colMonth7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMonth7.FieldName = "Month7";
            this.colMonth7.Name = "colMonth7";
            this.colMonth7.Visible = true;
            this.colMonth7.VisibleIndex = 8;
            // 
            // colMonth8
            // 
            this.colMonth8.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth8.Caption = "8月";
            this.colMonth8.DisplayFormat.FormatString = "n2";
            this.colMonth8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMonth8.FieldName = "Month8";
            this.colMonth8.Name = "colMonth8";
            this.colMonth8.Visible = true;
            this.colMonth8.VisibleIndex = 9;
            // 
            // colMonth9
            // 
            this.colMonth9.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth9.Caption = "9月";
            this.colMonth9.DisplayFormat.FormatString = "n2";
            this.colMonth9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMonth9.FieldName = "Month9";
            this.colMonth9.Name = "colMonth9";
            this.colMonth9.Visible = true;
            this.colMonth9.VisibleIndex = 10;
            // 
            // colMonth10
            // 
            this.colMonth10.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth10.Caption = "10月";
            this.colMonth10.DisplayFormat.FormatString = "n2";
            this.colMonth10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMonth10.FieldName = "Month10";
            this.colMonth10.Name = "colMonth10";
            this.colMonth10.Visible = true;
            this.colMonth10.VisibleIndex = 11;
            // 
            // colMonth11
            // 
            this.colMonth11.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth11.Caption = "11月";
            this.colMonth11.DisplayFormat.FormatString = "n2";
            this.colMonth11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMonth11.FieldName = "Month11";
            this.colMonth11.Name = "colMonth11";
            this.colMonth11.Visible = true;
            this.colMonth11.VisibleIndex = 12;
            // 
            // colMonth12
            // 
            this.colMonth12.AppearanceHeader.Options.UseTextOptions = true;
            this.colMonth12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMonth12.Caption = "12月";
            this.colMonth12.DisplayFormat.FormatString = "n2";
            this.colMonth12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMonth12.FieldName = "Month12";
            this.colMonth12.Name = "colMonth12";
            this.colMonth12.Visible = true;
            this.colMonth12.VisibleIndex = 13;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Title","地区")});
            this.repositoryItemLookUpEdit1.DisplayMember = "Title";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.ValueMember = "ID";
            // 
            // CtrlBurdenMonth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Name = "CtrlBurdenMonth";
            this.Size = new System.Drawing.Size(534, 319);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn colBurdenYear;		private DevExpress.XtraGrid.Columns.GridColumn colMonth1;		private DevExpress.XtraGrid.Columns.GridColumn colMonth2;		private DevExpress.XtraGrid.Columns.GridColumn colMonth3;		private DevExpress.XtraGrid.Columns.GridColumn colMonth4;		private DevExpress.XtraGrid.Columns.GridColumn colMonth5;		private DevExpress.XtraGrid.Columns.GridColumn colMonth6;		private DevExpress.XtraGrid.Columns.GridColumn colMonth7;		private DevExpress.XtraGrid.Columns.GridColumn colMonth8;		private DevExpress.XtraGrid.Columns.GridColumn colMonth9;		private DevExpress.XtraGrid.Columns.GridColumn colMonth10;		private DevExpress.XtraGrid.Columns.GridColumn colMonth11;		private DevExpress.XtraGrid.Columns.GridColumn colMonth12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
    }
}
