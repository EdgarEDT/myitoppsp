
namespace Itop.Client.Stutistics
{
	partial class CtrlPSP_Project_List
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
            this.colL3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colL15 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
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
            this.gridControl.Size = new System.Drawing.Size(534, 319);
            this.gridControl.TabIndex = 0;
            this.gridControl.UseEmbeddedNavigator = true;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            this.gridControl.Click += new System.EventHandler(this.gridControl_Click);
            // 
            // gridView
            // 
            this.gridView.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colL3,
            this.colL4,
            this.colL1,
            this.colL2,
            this.colL5,
            this.colL6,
            this.colL8,
            this.colL9,
            this.colL15});
            this.gridView.GridControl = this.gridControl;
            this.gridView.GroupPanelText = "建设项目表";
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsPrint.UsePrintStyles = true;
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // colL3
            // 
            this.colL3.AppearanceHeader.Options.UseTextOptions = true;
            this.colL3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL3.Caption = "项目名称";
            this.colL3.FieldName = "L3";
            this.colL3.Name = "colL3";
            this.colL3.Visible = true;
            this.colL3.VisibleIndex = 0;
            this.colL3.Width = 92;
            // 
            // colL4
            // 
            this.colL4.AppearanceHeader.Options.UseTextOptions = true;
            this.colL4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL4.Caption = "电压等级";
            this.colL4.FieldName = "L4";
            this.colL4.Name = "colL4";
            this.colL4.Visible = true;
            this.colL4.VisibleIndex = 1;
            this.colL4.Width = 92;
            // 
            // colL1
            // 
            this.colL1.AppearanceHeader.Options.UseTextOptions = true;
            this.colL1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL1.Caption = "区域";
            this.colL1.FieldName = "L1";
            this.colL1.Name = "colL1";
            this.colL1.Visible = true;
            this.colL1.VisibleIndex = 2;
            this.colL1.Width = 66;
            // 
            // colL2
            // 
            this.colL2.AppearanceHeader.Options.UseTextOptions = true;
            this.colL2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL2.Caption = "建设性质";
            this.colL2.FieldName = "L2";
            this.colL2.Name = "colL2";
            this.colL2.Visible = true;
            this.colL2.VisibleIndex = 3;
            this.colL2.Width = 95;
            // 
            // colL5
            // 
            this.colL5.AppearanceHeader.Options.UseTextOptions = true;
            this.colL5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL5.Caption = "台数";
            this.colL5.FieldName = "L5";
            this.colL5.Name = "colL5";
            this.colL5.Visible = true;
            this.colL5.VisibleIndex = 4;
            this.colL5.Width = 64;
            // 
            // colL6
            // 
            this.colL6.AppearanceHeader.Options.UseTextOptions = true;
            this.colL6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL6.Caption = "单台容量(MVA)";
            this.colL6.FieldName = "L6";
            this.colL6.Name = "colL6";
            this.colL6.Visible = true;
            this.colL6.VisibleIndex = 5;
            this.colL6.Width = 106;
            // 
            // colL8
            // 
            this.colL8.AppearanceHeader.Options.UseTextOptions = true;
            this.colL8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL8.Caption = "线路长度(KM)";
            this.colL8.FieldName = "L8";
            this.colL8.Name = "colL8";
            this.colL8.Visible = true;
            this.colL8.VisibleIndex = 6;
            this.colL8.Width = 119;
            // 
            // colL9
            // 
            this.colL9.AppearanceHeader.Options.UseTextOptions = true;
            this.colL9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL9.Caption = "导线型号";
            this.colL9.FieldName = "L9";
            this.colL9.Name = "colL9";
            this.colL9.Visible = true;
            this.colL9.VisibleIndex = 7;
            this.colL9.Width = 71;
            // 
            // colL15
            // 
            this.colL15.AppearanceHeader.Options.UseTextOptions = true;
            this.colL15.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colL15.Caption = "投产年限";
            this.colL15.FieldName = "L15";
            this.colL15.Name = "colL15";
            this.colL15.Visible = true;
            this.colL15.VisibleIndex = 8;
            this.colL15.Width = 131;
            // 
            // CtrlPSP_Project_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Name = "CtrlPSP_Project_List";
            this.Size = new System.Drawing.Size(534, 319);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn colL1;		private DevExpress.XtraGrid.Columns.GridColumn colL2;		private DevExpress.XtraGrid.Columns.GridColumn colL3;		private DevExpress.XtraGrid.Columns.GridColumn colL4;		private DevExpress.XtraGrid.Columns.GridColumn colL5;		private DevExpress.XtraGrid.Columns.GridColumn colL6;		private DevExpress.XtraGrid.Columns.GridColumn colL8;		private DevExpress.XtraGrid.Columns.GridColumn colL9;		private DevExpress.XtraGrid.Columns.GridColumn colL15;	}
}
