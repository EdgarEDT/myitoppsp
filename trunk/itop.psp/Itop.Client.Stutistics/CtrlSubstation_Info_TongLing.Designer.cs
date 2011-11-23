
namespace Itop.Client.Stutistics
{
	partial class CtrlSubstation_Info_TongLing
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
            this.colAreaID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.S1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.S2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.S3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.S4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.S5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.S6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.S7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.S8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.S9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.S10 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.colAreaID,
            this.colL1,
            this.colTitle,
            this.colL2,
            this.colL9,
            this.colL10,
            this.colL18,
            this.colL20,
            this.S1,
            this.S2,
            this.S3,
            this.S4,
            this.S5,
            this.S6,
            this.S7,
            this.S8,
            this.S9,
            this.S10});
            this.gridView.GridControl = this.gridControl;
            this.gridView.GroupPanelText = "变电站情况";
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsPrint.UsePrintStyles = true;
            this.gridView.OptionsView.ColumnAutoWidth = false;
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // colAreaID
            // 
            this.colAreaID.AppearanceHeader.Options.UseTextOptions = true;
            this.colAreaID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAreaID.Caption = "序号";
            this.colAreaID.FieldName = "AreaID";
            this.colAreaID.Name = "colAreaID";
            this.colAreaID.Visible = true;
            this.colAreaID.VisibleIndex = 0;
            // 
            // colL1
            // 
            this.colL1.AppearanceHeader.Options.UseTextOptions = true;
            this.colL1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL1.Caption = "电压等级";
            this.colL1.FieldName = "L1";
            this.colL1.Name = "colL1";
            this.colL1.Visible = true;
            this.colL1.VisibleIndex = 1;
            // 
            // colTitle
            // 
            this.colTitle.AppearanceHeader.Options.UseTextOptions = true;
            this.colTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTitle.Caption = "变电所名称";
            this.colTitle.FieldName = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.Visible = true;
            this.colTitle.VisibleIndex = 2;
            this.colTitle.Width = 200;
            // 
            // colL2
            // 
            this.colL2.AppearanceHeader.Options.UseTextOptions = true;
            this.colL2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL2.Caption = "容量(MVA)";
            this.colL2.DisplayFormat.FormatString = "n2";
            this.colL2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL2.FieldName = "L2";
            this.colL2.Name = "colL2";
            this.colL2.Visible = true;
            this.colL2.VisibleIndex = 3;
            // 
            // colL9
            // 
            this.colL9.AppearanceHeader.Options.UseTextOptions = true;
            this.colL9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL9.Caption = "最大负荷(MW)";
            this.colL9.DisplayFormat.FormatString = "n2";
            this.colL9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL9.FieldName = "L9";
            this.colL9.Name = "colL9";
            this.colL9.Visible = true;
            this.colL9.VisibleIndex = 4;
            // 
            // colL10
            // 
            this.colL10.AppearanceHeader.Options.UseTextOptions = true;
            this.colL10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL10.Caption = "负载率(%)";
            this.colL10.DisplayFormat.FormatString = "n2";
            this.colL10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colL10.FieldName = "L10";
            this.colL10.Name = "colL10";
            this.colL10.Visible = true;
            this.colL10.VisibleIndex = 5;
            // 
            // colL18
            // 
            this.colL18.AppearanceHeader.Options.UseTextOptions = true;
            this.colL18.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL18.Caption = "年供电量";
            this.colL18.FieldName = "L18";
            this.colL18.Name = "colL18";
            this.colL18.Visible = true;
            this.colL18.VisibleIndex = 6;
            // 
            // colL20
            // 
            this.colL20.AppearanceHeader.Options.UseTextOptions = true;
            this.colL20.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL20.Caption = "备    注";
            this.colL20.FieldName = "L20";
            this.colL20.Name = "colL20";
            this.colL20.Visible = true;
            this.colL20.VisibleIndex = 7;
            this.colL20.Width = 200;
            // 
            // S1
            // 
            this.S1.Caption = "S1";
            this.S1.FieldName = "S1";
            this.S1.Name = "S1";
            this.S1.Visible = true;
            this.S1.VisibleIndex = 8;
            // 
            // S2
            // 
            this.S2.Caption = "S2";
            this.S2.FieldName = "S2";
            this.S2.Name = "S2";
            this.S2.Visible = true;
            this.S2.VisibleIndex = 9;
            // 
            // S3
            // 
            this.S3.Caption = "S3";
            this.S3.FieldName = "S3";
            this.S3.Name = "S3";
            this.S3.Visible = true;
            this.S3.VisibleIndex = 10;
            // 
            // S4
            // 
            this.S4.Caption = "S4";
            this.S4.FieldName = "S4";
            this.S4.Name = "S4";
            this.S4.Visible = true;
            this.S4.VisibleIndex = 11;
            // 
            // S5
            // 
            this.S5.Caption = "S5";
            this.S5.FieldName = "S5";
            this.S5.Name = "S5";
            this.S5.Visible = true;
            this.S5.VisibleIndex = 12;
            // 
            // S6
            // 
            this.S6.Caption = "S6";
            this.S6.FieldName = "S6";
            this.S6.Name = "S6";
            this.S6.Visible = true;
            this.S6.VisibleIndex = 13;
            // 
            // S7
            // 
            this.S7.Caption = "S7";
            this.S7.FieldName = "S7";
            this.S7.Name = "S7";
            this.S7.Visible = true;
            this.S7.VisibleIndex = 14;
            // 
            // S8
            // 
            this.S8.Caption = "S8";
            this.S8.FieldName = "S8";
            this.S8.Name = "S8";
            this.S8.Visible = true;
            this.S8.VisibleIndex = 15;
            // 
            // S9
            // 
            this.S9.Caption = "S9";
            this.S9.FieldName = "S9";
            this.S9.Name = "S9";
            this.S9.Visible = true;
            this.S9.VisibleIndex = 16;
            // 
            // S10
            // 
            this.S10.Caption = "S10";
            this.S10.FieldName = "S10";
            this.S10.Name = "S10";
            this.S10.Visible = true;
            this.S10.VisibleIndex = 17;
            // 
            // CtrlSubstation_Info_TongLing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Name = "CtrlSubstation_Info_TongLing";
            this.Size = new System.Drawing.Size(534, 319);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colAreaID; private DevExpress.XtraGrid.Columns.GridColumn colTitle; private DevExpress.XtraGrid.Columns.GridColumn colL1; private DevExpress.XtraGrid.Columns.GridColumn colL2; private DevExpress.XtraGrid.Columns.GridColumn colL9; private DevExpress.XtraGrid.Columns.GridColumn colL10; private DevExpress.XtraGrid.Columns.GridColumn colL18; private DevExpress.XtraGrid.Columns.GridColumn colL20;
        private DevExpress.XtraGrid.Columns.GridColumn S1;
        private DevExpress.XtraGrid.Columns.GridColumn S2;
        private DevExpress.XtraGrid.Columns.GridColumn S3;
        private DevExpress.XtraGrid.Columns.GridColumn S4;
        private DevExpress.XtraGrid.Columns.GridColumn S5;
        private DevExpress.XtraGrid.Columns.GridColumn S6;
        private DevExpress.XtraGrid.Columns.GridColumn S7;
        private DevExpress.XtraGrid.Columns.GridColumn S8;
        private DevExpress.XtraGrid.Columns.GridColumn S9;
        private DevExpress.XtraGrid.Columns.GridColumn S10;
    }
}
