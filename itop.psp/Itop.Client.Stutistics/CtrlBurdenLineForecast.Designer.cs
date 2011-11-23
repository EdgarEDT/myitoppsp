
namespace Itop.Client.Stutistics
{
	partial class CtrlBurdenLineForecast
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
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colBurdenYear = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colSummerDayAverage = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colSummerMinAverage = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colWinterDayAverage = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand8 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colWinterMinAverage = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand9 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
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
            this.gridControl.MainView = this.bandedGridView1;
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
            this.gridControl.Size = new System.Drawing.Size(534, 319);
            this.gridControl.TabIndex = 0;
            this.gridControl.UseEmbeddedNavigator = true;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView1});
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.bandedGridView1.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2,
            this.gridBand3});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colBurdenYear,
            this.colSummerDayAverage,
            this.colSummerMinAverage,
            this.colWinterDayAverage,
            this.colWinterMinAverage,
            this.bandedGridColumn1,
            this.bandedGridColumn2});
            this.bandedGridView1.GridControl = this.gridControl;
            this.bandedGridView1.GroupPanelText = "负荷曲线预测";
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsBehavior.Editable = false;
            this.bandedGridView1.OptionsPrint.PrintHeader = false;
            this.bandedGridView1.OptionsPrint.UsePrintStyles = true;
            this.bandedGridView1.OptionsView.ShowColumnHeaders = false;
            this.bandedGridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colBurdenYear, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.bandedGridView1.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // gridBand1
            // 
            this.gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand1.Caption = "年度";
            this.gridBand1.Columns.Add(this.colBurdenYear);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.Width = 119;
            // 
            // colBurdenYear
            // 
            this.colBurdenYear.AppearanceHeader.Options.UseTextOptions = true;
            this.colBurdenYear.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBurdenYear.Caption = "年度";
            this.colBurdenYear.FieldName = "BurdenYear";
            this.colBurdenYear.Name = "colBurdenYear";
            this.colBurdenYear.Visible = true;
            this.colBurdenYear.Width = 119;
            // 
            // gridBand2
            // 
            this.gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand2.Caption = "夏季";
            this.gridBand2.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand4,
            this.gridBand5,
            this.gridBand6});
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.Width = 337;
            // 
            // gridBand4
            // 
            this.gridBand4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand4.Caption = "夏季日平均负荷率(%)";
            this.gridBand4.Columns.Add(this.colSummerDayAverage);
            this.gridBand4.Name = "gridBand4";
            this.gridBand4.Width = 111;
            // 
            // colSummerDayAverage
            // 
            this.colSummerDayAverage.AppearanceHeader.Options.UseTextOptions = true;
            this.colSummerDayAverage.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSummerDayAverage.Caption = "夏季日平均负荷率(%)";
            this.colSummerDayAverage.DisplayFormat.FormatString = "n2";
            this.colSummerDayAverage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSummerDayAverage.FieldName = "SummerDayAverage";
            this.colSummerDayAverage.Name = "colSummerDayAverage";
            this.colSummerDayAverage.Visible = true;
            this.colSummerDayAverage.Width = 111;
            // 
            // gridBand5
            // 
            this.gridBand5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand5.Caption = "夏季日最小负荷率(%)";
            this.gridBand5.Columns.Add(this.colSummerMinAverage);
            this.gridBand5.Name = "gridBand5";
            this.gridBand5.Width = 111;
            // 
            // colSummerMinAverage
            // 
            this.colSummerMinAverage.AppearanceHeader.Options.UseTextOptions = true;
            this.colSummerMinAverage.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSummerMinAverage.Caption = "夏季日最小负荷率(%)";
            this.colSummerMinAverage.DisplayFormat.FormatString = "n2";
            this.colSummerMinAverage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSummerMinAverage.FieldName = "SummerMinAverage";
            this.colSummerMinAverage.Name = "colSummerMinAverage";
            this.colSummerMinAverage.Visible = true;
            this.colSummerMinAverage.Width = 111;
            // 
            // gridBand6
            // 
            this.gridBand6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand6.Caption = "Tmax";
            this.gridBand6.Columns.Add(this.bandedGridColumn1);
            this.gridBand6.Name = "gridBand6";
            this.gridBand6.Width = 115;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "Tmax";
            this.bandedGridColumn1.ColumnEdit = this.repositoryItemTextEdit1;
            this.bandedGridColumn1.FieldName = "SummerData";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.Width = 115;
            // 
            // gridBand3
            // 
            this.gridBand3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand3.Caption = "冬季";
            this.gridBand3.Children.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand7,
            this.gridBand8,
            this.gridBand9});
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.Width = 361;
            // 
            // gridBand7
            // 
            this.gridBand7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand7.Caption = "冬季日平均负荷率(%)";
            this.gridBand7.Columns.Add(this.colWinterDayAverage);
            this.gridBand7.Name = "gridBand7";
            this.gridBand7.Width = 119;
            // 
            // colWinterDayAverage
            // 
            this.colWinterDayAverage.AppearanceHeader.Options.UseTextOptions = true;
            this.colWinterDayAverage.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colWinterDayAverage.Caption = "冬季日平均负荷率(%)";
            this.colWinterDayAverage.DisplayFormat.FormatString = "n2";
            this.colWinterDayAverage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWinterDayAverage.FieldName = "WinterDayAverage";
            this.colWinterDayAverage.Name = "colWinterDayAverage";
            this.colWinterDayAverage.Visible = true;
            this.colWinterDayAverage.Width = 119;
            // 
            // gridBand8
            // 
            this.gridBand8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand8.Caption = "冬季日最小负荷率(%)";
            this.gridBand8.Columns.Add(this.colWinterMinAverage);
            this.gridBand8.Name = "gridBand8";
            this.gridBand8.Width = 119;
            // 
            // colWinterMinAverage
            // 
            this.colWinterMinAverage.AppearanceHeader.Options.UseTextOptions = true;
            this.colWinterMinAverage.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colWinterMinAverage.Caption = "冬季日最小负荷率(%)";
            this.colWinterMinAverage.DisplayFormat.FormatString = "n2";
            this.colWinterMinAverage.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWinterMinAverage.FieldName = "WinterMinAverage";
            this.colWinterMinAverage.Name = "colWinterMinAverage";
            this.colWinterMinAverage.Visible = true;
            this.colWinterMinAverage.Width = 119;
            // 
            // gridBand9
            // 
            this.gridBand9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBand9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBand9.Caption = "Tmax";
            this.gridBand9.Columns.Add(this.bandedGridColumn2);
            this.gridBand9.Name = "gridBand9";
            this.gridBand9.Width = 123;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "Tmax";
            this.bandedGridColumn2.ColumnEdit = this.repositoryItemTextEdit2;
            this.bandedGridColumn2.FieldName = "WinterData";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.Width = 123;
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
            // CtrlBurdenLineForecast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Name = "CtrlBurdenLineForecast";
            this.Size = new System.Drawing.Size(534, 319);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colBurdenYear;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSummerDayAverage;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand5;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSummerMinAverage;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand6;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand7;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colWinterDayAverage;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand8;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colWinterMinAverage;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand9;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
    }
}
