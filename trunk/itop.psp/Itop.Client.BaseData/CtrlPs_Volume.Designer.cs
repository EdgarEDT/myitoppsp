
namespace Itop.Client.BaseData
{
	partial class CtrlPs_Volume
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
            this.colYears = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxPw = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colYearEndVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWaterVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFireVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBackupVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colToolsVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colbalkVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colbalkWaterVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colbalkFireVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBalanceVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFeedPw = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGetPw = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBreakPw = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGetPs = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // 
            // 
            this.gridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControl.EmbeddedNavigator.Name = "";
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(819, 421);
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
            this.colYears,
            this.colMaxPw,
            this.colYearEndVolume,
            this.colWaterVolume,
            this.colFireVolume,
            this.colBackupVolume,
            this.colToolsVolume,
            this.colMaxVolume,
            this.colbalkVolume,
            this.colbalkWaterVolume,
            this.colbalkFireVolume,
            this.colBalanceVolume,
            this.colFeedPw,
            this.colGetPw,
            this.colBreakPw,
            this.colGetPs});
            this.gridView.GridControl = this.gridControl;
            this.gridView.GroupPanelText = "电力平衡表";
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsPrint.UsePrintStyles = true;
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // colYears
            // 
            this.colYears.AppearanceHeader.Options.UseTextOptions = true;
            this.colYears.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colYears.Caption = "年份";
            this.colYears.FieldName = "Years";
            this.colYears.Name = "colYears";
            this.colYears.Visible = true;
            this.colYears.VisibleIndex = 0;
            // 
            // colMaxPw
            // 
            this.colMaxPw.AppearanceHeader.Options.UseTextOptions = true;
            this.colMaxPw.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMaxPw.Caption = "最大供电负荷需求";
            this.colMaxPw.DisplayFormat.FormatString = "n2";
            this.colMaxPw.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMaxPw.FieldName = "MaxPw";
            this.colMaxPw.Name = "colMaxPw";
            this.colMaxPw.Visible = true;
            this.colMaxPw.VisibleIndex = 1;
            // 
            // colYearEndVolume
            // 
            this.colYearEndVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colYearEndVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colYearEndVolume.Caption = "年末装机容量";
            this.colYearEndVolume.DisplayFormat.FormatString = "n2";
            this.colYearEndVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colYearEndVolume.FieldName = "YearEndVolume";
            this.colYearEndVolume.Name = "colYearEndVolume";
            this.colYearEndVolume.Visible = true;
            this.colYearEndVolume.VisibleIndex = 2;
            // 
            // colWaterVolume
            // 
            this.colWaterVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colWaterVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colWaterVolume.Caption = "水电装机容量";
            this.colWaterVolume.DisplayFormat.FormatString = "n2";
            this.colWaterVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colWaterVolume.FieldName = "WaterVolume";
            this.colWaterVolume.Name = "colWaterVolume";
            this.colWaterVolume.Visible = true;
            this.colWaterVolume.VisibleIndex = 3;
            // 
            // colFireVolume
            // 
            this.colFireVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colFireVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFireVolume.Caption = "火电装机容量";
            this.colFireVolume.DisplayFormat.FormatString = "n2";
            this.colFireVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFireVolume.FieldName = "FireVolume";
            this.colFireVolume.Name = "colFireVolume";
            this.colFireVolume.Visible = true;
            this.colFireVolume.VisibleIndex = 4;
            // 
            // colBackupVolume
            // 
            this.colBackupVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colBackupVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBackupVolume.Caption = "装机备用容量";
            this.colBackupVolume.DisplayFormat.FormatString = "n2";
            this.colBackupVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBackupVolume.FieldName = "BackupVolume";
            this.colBackupVolume.Name = "colBackupVolume";
            this.colBackupVolume.Visible = true;
            this.colBackupVolume.VisibleIndex = 5;
            // 
            // colToolsVolume
            // 
            this.colToolsVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colToolsVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colToolsVolume.Caption = "机组容量";
            this.colToolsVolume.DisplayFormat.FormatString = "n2";
            this.colToolsVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colToolsVolume.FieldName = "ToolsVolume";
            this.colToolsVolume.Name = "colToolsVolume";
            this.colToolsVolume.Visible = true;
            this.colToolsVolume.VisibleIndex = 6;
            // 
            // colMaxVolume
            // 
            this.colMaxVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colMaxVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMaxVolume.Caption = "最大单机容量";
            this.colMaxVolume.DisplayFormat.FormatString = "n2";
            this.colMaxVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMaxVolume.FieldName = "MaxVolume";
            this.colMaxVolume.Name = "colMaxVolume";
            this.colMaxVolume.Visible = true;
            this.colMaxVolume.VisibleIndex = 7;
            // 
            // colbalkVolume
            // 
            this.colbalkVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colbalkVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colbalkVolume.Caption = "受阻容量";
            this.colbalkVolume.DisplayFormat.FormatString = "n2";
            this.colbalkVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colbalkVolume.FieldName = "balkVolume";
            this.colbalkVolume.Name = "colbalkVolume";
            this.colbalkVolume.Visible = true;
            this.colbalkVolume.VisibleIndex = 8;
            // 
            // colbalkWaterVolume
            // 
            this.colbalkWaterVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colbalkWaterVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colbalkWaterVolume.Caption = "水电受阻容量";
            this.colbalkWaterVolume.DisplayFormat.FormatString = "n2";
            this.colbalkWaterVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colbalkWaterVolume.FieldName = "balkWaterVolume";
            this.colbalkWaterVolume.Name = "colbalkWaterVolume";
            this.colbalkWaterVolume.Visible = true;
            this.colbalkWaterVolume.VisibleIndex = 9;
            // 
            // colbalkFireVolume
            // 
            this.colbalkFireVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colbalkFireVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colbalkFireVolume.Caption = "火电受阻容量";
            this.colbalkFireVolume.DisplayFormat.FormatString = "n2";
            this.colbalkFireVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colbalkFireVolume.FieldName = "balkFireVolume";
            this.colbalkFireVolume.Name = "colbalkFireVolume";
            this.colbalkFireVolume.Visible = true;
            this.colbalkFireVolume.VisibleIndex = 10;
            // 
            // colBalanceVolume
            // 
            this.colBalanceVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colBalanceVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBalanceVolume.Caption = "平衡容量";
            this.colBalanceVolume.DisplayFormat.FormatString = "n2";
            this.colBalanceVolume.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBalanceVolume.FieldName = "BalanceVolume";
            this.colBalanceVolume.Name = "colBalanceVolume";
            this.colBalanceVolume.Visible = true;
            this.colBalanceVolume.VisibleIndex = 11;
            // 
            // colFeedPw
            // 
            this.colFeedPw.AppearanceHeader.Options.UseTextOptions = true;
            this.colFeedPw.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFeedPw.Caption = "可供电力";
            this.colFeedPw.DisplayFormat.FormatString = "n2";
            this.colFeedPw.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFeedPw.FieldName = "FeedPw";
            this.colFeedPw.Name = "colFeedPw";
            this.colFeedPw.Visible = true;
            this.colFeedPw.VisibleIndex = 12;
            // 
            // colGetPw
            // 
            this.colGetPw.AppearanceHeader.Options.UseTextOptions = true;
            this.colGetPw.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGetPw.Caption = "电网外受电力";
            this.colGetPw.DisplayFormat.FormatString = "n2";
            this.colGetPw.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGetPw.FieldName = "GetPw";
            this.colGetPw.Name = "colGetPw";
            this.colGetPw.Visible = true;
            this.colGetPw.VisibleIndex = 13;
            // 
            // colBreakPw
            // 
            this.colBreakPw.AppearanceHeader.Options.UseTextOptions = true;
            this.colBreakPw.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBreakPw.Caption = "电力盈（+）亏（-）平衡";
            this.colBreakPw.DisplayFormat.FormatString = "n2";
            this.colBreakPw.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colBreakPw.FieldName = "BreakPw";
            this.colBreakPw.Name = "colBreakPw";
            this.colBreakPw.Visible = true;
            this.colBreakPw.VisibleIndex = 14;
            // 
            // colGetPs
            // 
            this.colGetPs.AppearanceHeader.Options.UseTextOptions = true;
            this.colGetPs.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colGetPs.Caption = "外受电比例";
            this.colGetPs.DisplayFormat.FormatString = "n2";
            this.colGetPs.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGetPs.FieldName = "GetPs";
            this.colGetPs.Name = "colGetPs";
            this.colGetPs.Visible = true;
            this.colGetPs.VisibleIndex = 15;
            // 
            // CtrlPs_Volume
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Name = "CtrlPs_Volume";
            this.Size = new System.Drawing.Size(819, 421);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn colYears;		private DevExpress.XtraGrid.Columns.GridColumn colMaxPw;		private DevExpress.XtraGrid.Columns.GridColumn colYearEndVolume;		private DevExpress.XtraGrid.Columns.GridColumn colWaterVolume;		private DevExpress.XtraGrid.Columns.GridColumn colFireVolume;		private DevExpress.XtraGrid.Columns.GridColumn colBackupVolume;		private DevExpress.XtraGrid.Columns.GridColumn colToolsVolume;		private DevExpress.XtraGrid.Columns.GridColumn colMaxVolume;		private DevExpress.XtraGrid.Columns.GridColumn colbalkVolume;		private DevExpress.XtraGrid.Columns.GridColumn colbalkWaterVolume;		private DevExpress.XtraGrid.Columns.GridColumn colbalkFireVolume;		private DevExpress.XtraGrid.Columns.GridColumn colBalanceVolume;		private DevExpress.XtraGrid.Columns.GridColumn colFeedPw;		private DevExpress.XtraGrid.Columns.GridColumn colGetPw;		private DevExpress.XtraGrid.Columns.GridColumn colBreakPw;		private DevExpress.XtraGrid.Columns.GridColumn colGetPs;	}
}
