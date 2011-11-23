
namespace Itop.Client.Stutistics
{
	partial class CtrlPs_Power
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
			this.colFQ = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colTitle = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colDY = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colTS = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colRL = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colWGRL = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCreateYear = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colUseYear = new DevExpress.XtraGrid.Columns.GridColumn();
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
			this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {this.gridView});
			//
			// gridView
			//
			this.gridView.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
			this.gridView.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.colFQ,this.colTitle,this.colDY,this.colTS,this.colRL,this.colWGRL,this.colCreateYear,this.colUseYear});
			this.gridView.GridControl = this.gridControl;
			this.gridView.GroupPanelText = "aa";
			this.gridView.Name = "gridView";
			this.gridView.OptionsBehavior.Editable = false;
			this.gridView.OptionsPrint.UsePrintStyles = true;
			this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
			//
			// colFQ
			//
			this.colFQ.AppearanceHeader.Options.UseTextOptions = true;
			this.colFQ.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colFQ.Caption = "分区";
			this.colFQ.FieldName = "FQ";
			this.colFQ.Name = "colFQ";
			this.colFQ.Visible = true;
			this.colFQ.VisibleIndex = 0;
			//
			// colTitle
			//
			this.colTitle.AppearanceHeader.Options.UseTextOptions = true;
			this.colTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colTitle.Caption = "名称";
			this.colTitle.FieldName = "Title";
			this.colTitle.Name = "colTitle";
			this.colTitle.Visible = true;
			this.colTitle.VisibleIndex = 0;
			//
			// colDY
			//
			this.colDY.AppearanceHeader.Options.UseTextOptions = true;
			this.colDY.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colDY.Caption = "电压等级";
			this.colDY.FieldName = "DY";
			this.colDY.Name = "colDY";
			this.colDY.Visible = true;
			this.colDY.VisibleIndex = 0;
			//
			// colTS
			//
			this.colTS.AppearanceHeader.Options.UseTextOptions = true;
			this.colTS.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colTS.Caption = "台数";
			this.colTS.FieldName = "TS";
			this.colTS.Name = "colTS";
			this.colTS.Visible = true;
			this.colTS.VisibleIndex = 0;
			//
			// colRL
			//
			this.colRL.AppearanceHeader.Options.UseTextOptions = true;
			this.colRL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colRL.Caption = "容量";
			this.colRL.DisplayFormat.FormatString = "n2";
			this.colRL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colRL.FieldName = "RL";
			this.colRL.Name = "colRL";
			this.colRL.Visible = true;
			this.colRL.VisibleIndex = 0;
			//
			// colWGRL
			//
			this.colWGRL.AppearanceHeader.Options.UseTextOptions = true;
			this.colWGRL.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colWGRL.Caption = "备用容量";
			this.colWGRL.DisplayFormat.FormatString = "n2";
			this.colWGRL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colWGRL.FieldName = "WGRL";
			this.colWGRL.Name = "colWGRL";
			this.colWGRL.Visible = true;
			this.colWGRL.VisibleIndex = 0;
			//
			// colCreateYear
			//
			this.colCreateYear.AppearanceHeader.Options.UseTextOptions = true;
			this.colCreateYear.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCreateYear.Caption = "建成时间";
			this.colCreateYear.FieldName = "CreateYear";
			this.colCreateYear.Name = "colCreateYear";
			this.colCreateYear.Visible = true;
			this.colCreateYear.VisibleIndex = 0;
			//
			// colUseYear
			//
			this.colUseYear.AppearanceHeader.Options.UseTextOptions = true;
			this.colUseYear.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colUseYear.Caption = "可用年限";
			this.colUseYear.FieldName = "UseYear";
			this.colUseYear.Name = "colUseYear";
			this.colUseYear.Visible = true;
			this.colUseYear.VisibleIndex = 0;
			//
			// DepartmentCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridControl);
			this.Name = "Ps_PowerCtrl";
			this.Size = new System.Drawing.Size(534, 319);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
			this.ResumeLayout(false);
		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn colFQ;		private DevExpress.XtraGrid.Columns.GridColumn colTitle;		private DevExpress.XtraGrid.Columns.GridColumn colDY;		private DevExpress.XtraGrid.Columns.GridColumn colTS;		private DevExpress.XtraGrid.Columns.GridColumn colRL;		private DevExpress.XtraGrid.Columns.GridColumn colWGRL;		private DevExpress.XtraGrid.Columns.GridColumn colCreateYear;		private DevExpress.XtraGrid.Columns.GridColumn colUseYear;	}
}
