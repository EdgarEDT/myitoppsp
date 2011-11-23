
namespace Itop.Client.Stutistics
{
	partial class CtrlPowerLine
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
			this.colPowerName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
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
			this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.colPowerName,this.colCreateDate,this.colRemark});
			this.gridView.GridControl = this.gridControl;
			this.gridView.GroupPanelText = "输变电项目";
			this.gridView.Name = "gridView";
			this.gridView.OptionsBehavior.Editable = false;
			this.gridView.OptionsPrint.UsePrintStyles = true;
			this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
			//
			// colPowerName
			//
			this.colPowerName.AppearanceHeader.Options.UseTextOptions = true;
			this.colPowerName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colPowerName.Caption = "项目名称";
			this.colPowerName.FieldName = "PowerName";
			this.colPowerName.Name = "colPowerName";
			this.colPowerName.Visible = true;
			this.colPowerName.VisibleIndex = 0;
			//
			// colCreateDate
			//
			this.colCreateDate.AppearanceHeader.Options.UseTextOptions = true;
			this.colCreateDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCreateDate.Caption = "创建日期";
			this.colCreateDate.DisplayFormat.FormatString = "yyyy-MM-dd";
			this.colCreateDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
			this.colCreateDate.FieldName = "CreateDate";
			this.colCreateDate.Name = "colCreateDate";
			this.colCreateDate.Visible = true;
			this.colCreateDate.VisibleIndex = 0;
			//
			// colRemark
			//
			this.colRemark.AppearanceHeader.Options.UseTextOptions = true;
			this.colRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colRemark.Caption = "说明";
			this.colRemark.FieldName = "Remark";
			this.colRemark.Name = "colRemark";
			this.colRemark.Visible = true;
			this.colRemark.VisibleIndex = 0;
			//
			// DepartmentCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridControl);
			this.Name = "PowerLineCtrl";
			this.Size = new System.Drawing.Size(534, 319);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
			this.ResumeLayout(false);
		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn colPowerName;		private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;		private DevExpress.XtraGrid.Columns.GridColumn colRemark;	}
}
