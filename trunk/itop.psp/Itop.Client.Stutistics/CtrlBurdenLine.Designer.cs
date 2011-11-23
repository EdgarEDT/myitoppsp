
namespace Itop.Client.Stutistics
{
	partial class CtrlBurdenLine
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
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSeason = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHour24 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gridColumn3,
            this.colSeason,
            this.colHour1,
            this.colHour2,
            this.colHour3,
            this.colHour4,
            this.colHour5,
            this.colHour6,
            this.colHour7,
            this.colHour8,
            this.colHour9,
            this.colHour10,
            this.colHour11,
            this.colHour12,
            this.colHour13,
            this.colHour14,
            this.colHour15,
            this.colHour16,
            this.colHour17,
            this.colHour18,
            this.colHour19,
            this.colHour20,
            this.colHour21,
            this.colHour22,
            this.colHour23,
            this.colHour24,
            this.colIsType,
            this.gridColumn1,
            this.gridColumn2});
            this.gridView.GridControl = this.gridControl;
            this.gridView.GroupPanelText = "负荷曲线表";
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsPrint.UsePrintStyles = true;
            this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn2, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "地区";
            this.gridColumn3.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.gridColumn3.FieldName = "AreaID";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // colSeason
            // 
            this.colSeason.AppearanceHeader.Options.UseTextOptions = true;
            this.colSeason.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSeason.Caption = "季节";
            this.colSeason.FieldName = "Season";
            this.colSeason.Name = "colSeason";
            this.colSeason.Visible = true;
            this.colSeason.VisibleIndex = 2;
            // 
            // colHour1
            // 
            this.colHour1.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour1.Caption = "1时";
            this.colHour1.DisplayFormat.FormatString = "n2";
            this.colHour1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour1.FieldName = "Hour1";
            this.colHour1.Name = "colHour1";
            this.colHour1.Visible = true;
            this.colHour1.VisibleIndex = 3;
            // 
            // colHour2
            // 
            this.colHour2.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour2.Caption = "2时";
            this.colHour2.DisplayFormat.FormatString = "n2";
            this.colHour2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour2.FieldName = "Hour2";
            this.colHour2.Name = "colHour2";
            this.colHour2.Visible = true;
            this.colHour2.VisibleIndex = 4;
            // 
            // colHour3
            // 
            this.colHour3.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour3.Caption = "3时";
            this.colHour3.DisplayFormat.FormatString = "n2";
            this.colHour3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour3.FieldName = "Hour3";
            this.colHour3.Name = "colHour3";
            this.colHour3.Visible = true;
            this.colHour3.VisibleIndex = 5;
            // 
            // colHour4
            // 
            this.colHour4.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour4.Caption = "4时";
            this.colHour4.DisplayFormat.FormatString = "n2";
            this.colHour4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour4.FieldName = "Hour4";
            this.colHour4.Name = "colHour4";
            this.colHour4.Visible = true;
            this.colHour4.VisibleIndex = 6;
            // 
            // colHour5
            // 
            this.colHour5.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour5.Caption = "5时";
            this.colHour5.DisplayFormat.FormatString = "n2";
            this.colHour5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour5.FieldName = "Hour5";
            this.colHour5.Name = "colHour5";
            this.colHour5.Visible = true;
            this.colHour5.VisibleIndex = 7;
            // 
            // colHour6
            // 
            this.colHour6.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour6.Caption = "6时";
            this.colHour6.DisplayFormat.FormatString = "n2";
            this.colHour6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour6.FieldName = "Hour6";
            this.colHour6.Name = "colHour6";
            this.colHour6.Visible = true;
            this.colHour6.VisibleIndex = 8;
            // 
            // colHour7
            // 
            this.colHour7.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour7.Caption = "7时";
            this.colHour7.DisplayFormat.FormatString = "n2";
            this.colHour7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour7.FieldName = "Hour7";
            this.colHour7.Name = "colHour7";
            this.colHour7.Visible = true;
            this.colHour7.VisibleIndex = 9;
            // 
            // colHour8
            // 
            this.colHour8.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour8.Caption = "8时";
            this.colHour8.DisplayFormat.FormatString = "n2";
            this.colHour8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour8.FieldName = "Hour8";
            this.colHour8.Name = "colHour8";
            this.colHour8.Visible = true;
            this.colHour8.VisibleIndex = 10;
            // 
            // colHour9
            // 
            this.colHour9.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour9.Caption = "9时";
            this.colHour9.DisplayFormat.FormatString = "n2";
            this.colHour9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour9.FieldName = "Hour9";
            this.colHour9.Name = "colHour9";
            this.colHour9.Visible = true;
            this.colHour9.VisibleIndex = 11;
            // 
            // colHour10
            // 
            this.colHour10.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour10.Caption = "10时";
            this.colHour10.DisplayFormat.FormatString = "n2";
            this.colHour10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour10.FieldName = "Hour10";
            this.colHour10.Name = "colHour10";
            this.colHour10.Visible = true;
            this.colHour10.VisibleIndex = 12;
            // 
            // colHour11
            // 
            this.colHour11.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour11.Caption = "11时";
            this.colHour11.DisplayFormat.FormatString = "n2";
            this.colHour11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour11.FieldName = "Hour11";
            this.colHour11.Name = "colHour11";
            this.colHour11.Visible = true;
            this.colHour11.VisibleIndex = 13;
            // 
            // colHour12
            // 
            this.colHour12.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour12.Caption = "12时";
            this.colHour12.DisplayFormat.FormatString = "n2";
            this.colHour12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour12.FieldName = "Hour12";
            this.colHour12.Name = "colHour12";
            this.colHour12.Visible = true;
            this.colHour12.VisibleIndex = 14;
            // 
            // colHour13
            // 
            this.colHour13.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour13.Caption = "13时";
            this.colHour13.DisplayFormat.FormatString = "n2";
            this.colHour13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour13.FieldName = "Hour13";
            this.colHour13.Name = "colHour13";
            this.colHour13.Visible = true;
            this.colHour13.VisibleIndex = 15;
            // 
            // colHour14
            // 
            this.colHour14.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour14.Caption = "14时";
            this.colHour14.DisplayFormat.FormatString = "n2";
            this.colHour14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour14.FieldName = "Hour14";
            this.colHour14.Name = "colHour14";
            this.colHour14.Visible = true;
            this.colHour14.VisibleIndex = 16;
            // 
            // colHour15
            // 
            this.colHour15.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour15.Caption = "15时";
            this.colHour15.DisplayFormat.FormatString = "n2";
            this.colHour15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour15.FieldName = "Hour15";
            this.colHour15.Name = "colHour15";
            this.colHour15.Visible = true;
            this.colHour15.VisibleIndex = 17;
            // 
            // colHour16
            // 
            this.colHour16.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour16.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour16.Caption = "16时";
            this.colHour16.DisplayFormat.FormatString = "n2";
            this.colHour16.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour16.FieldName = "Hour16";
            this.colHour16.Name = "colHour16";
            this.colHour16.Visible = true;
            this.colHour16.VisibleIndex = 18;
            // 
            // colHour17
            // 
            this.colHour17.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour17.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour17.Caption = "17时";
            this.colHour17.DisplayFormat.FormatString = "n2";
            this.colHour17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour17.FieldName = "Hour17";
            this.colHour17.Name = "colHour17";
            this.colHour17.Visible = true;
            this.colHour17.VisibleIndex = 19;
            // 
            // colHour18
            // 
            this.colHour18.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour18.Caption = "18时";
            this.colHour18.DisplayFormat.FormatString = "n2";
            this.colHour18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour18.FieldName = "Hour18";
            this.colHour18.Name = "colHour18";
            this.colHour18.Visible = true;
            this.colHour18.VisibleIndex = 20;
            // 
            // colHour19
            // 
            this.colHour19.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour19.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour19.Caption = "19时";
            this.colHour19.DisplayFormat.FormatString = "n2";
            this.colHour19.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour19.FieldName = "Hour19";
            this.colHour19.Name = "colHour19";
            this.colHour19.Visible = true;
            this.colHour19.VisibleIndex = 21;
            // 
            // colHour20
            // 
            this.colHour20.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour20.Caption = "20时";
            this.colHour20.DisplayFormat.FormatString = "n2";
            this.colHour20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour20.FieldName = "Hour20";
            this.colHour20.Name = "colHour20";
            this.colHour20.Visible = true;
            this.colHour20.VisibleIndex = 22;
            // 
            // colHour21
            // 
            this.colHour21.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour21.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour21.Caption = "21时";
            this.colHour21.DisplayFormat.FormatString = "n2";
            this.colHour21.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour21.FieldName = "Hour21";
            this.colHour21.Name = "colHour21";
            this.colHour21.Visible = true;
            this.colHour21.VisibleIndex = 23;
            // 
            // colHour22
            // 
            this.colHour22.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour22.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour22.Caption = "22时";
            this.colHour22.DisplayFormat.FormatString = "n2";
            this.colHour22.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour22.FieldName = "Hour22";
            this.colHour22.Name = "colHour22";
            this.colHour22.Visible = true;
            this.colHour22.VisibleIndex = 24;
            // 
            // colHour23
            // 
            this.colHour23.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour23.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour23.Caption = "23时";
            this.colHour23.DisplayFormat.FormatString = "n2";
            this.colHour23.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour23.FieldName = "Hour23";
            this.colHour23.Name = "colHour23";
            this.colHour23.Visible = true;
            this.colHour23.VisibleIndex = 25;
            // 
            // colHour24
            // 
            this.colHour24.AppearanceHeader.Options.UseTextOptions = true;
            this.colHour24.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colHour24.Caption = "24时";
            this.colHour24.DisplayFormat.FormatString = "n2";
            this.colHour24.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colHour24.FieldName = "Hour24";
            this.colHour24.Name = "colHour24";
            this.colHour24.Visible = true;
            this.colHour24.VisibleIndex = 26;
            // 
            // colIsType
            // 
            this.colIsType.AppearanceHeader.Options.UseTextOptions = true;
            this.colIsType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIsType.Caption = "是否为典型日";
            this.colIsType.FieldName = "IsType";
            this.colIsType.Name = "colIsType";
            this.colIsType.Visible = true;
            this.colIsType.VisibleIndex = 27;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "是否为最大日";
            this.gridColumn1.FieldName = "IsMaxDate";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 28;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "日期";
            this.gridColumn2.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn2.FieldName = "BurdenDate";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
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
            // CtrlBurdenLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Name = "CtrlBurdenLine";
            this.Size = new System.Drawing.Size(534, 319);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colSeason; private DevExpress.XtraGrid.Columns.GridColumn colHour1; private DevExpress.XtraGrid.Columns.GridColumn colHour2; private DevExpress.XtraGrid.Columns.GridColumn colHour3; private DevExpress.XtraGrid.Columns.GridColumn colHour4; private DevExpress.XtraGrid.Columns.GridColumn colHour5; private DevExpress.XtraGrid.Columns.GridColumn colHour6; private DevExpress.XtraGrid.Columns.GridColumn colHour7; private DevExpress.XtraGrid.Columns.GridColumn colHour8; private DevExpress.XtraGrid.Columns.GridColumn colHour9; private DevExpress.XtraGrid.Columns.GridColumn colHour10; private DevExpress.XtraGrid.Columns.GridColumn colHour11; private DevExpress.XtraGrid.Columns.GridColumn colHour12; private DevExpress.XtraGrid.Columns.GridColumn colHour13; private DevExpress.XtraGrid.Columns.GridColumn colHour14; private DevExpress.XtraGrid.Columns.GridColumn colHour15; private DevExpress.XtraGrid.Columns.GridColumn colHour16; private DevExpress.XtraGrid.Columns.GridColumn colHour17; private DevExpress.XtraGrid.Columns.GridColumn colHour18; private DevExpress.XtraGrid.Columns.GridColumn colHour19; private DevExpress.XtraGrid.Columns.GridColumn colHour20; private DevExpress.XtraGrid.Columns.GridColumn colHour21; private DevExpress.XtraGrid.Columns.GridColumn colHour22; private DevExpress.XtraGrid.Columns.GridColumn colHour23; private DevExpress.XtraGrid.Columns.GridColumn colHour24; private DevExpress.XtraGrid.Columns.GridColumn colIsType;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
    }
}
