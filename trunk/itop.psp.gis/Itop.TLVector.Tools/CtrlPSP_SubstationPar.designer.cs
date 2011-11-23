
namespace ItopVector.Tools
{
	partial class CtrlPSP_SubstationPar
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
            this.colUID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInfoName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coltype = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcol1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcol2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcol3 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            // 
            // gridView
            // 
            this.gridView.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUID,
            this.colInfoName,
            this.coltype,
            this.colcol1,
            this.colcol2,
            this.colcol3});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsPrint.UsePrintStyles = true;
            this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // colUID
            // 
            this.colUID.AppearanceHeader.Options.UseTextOptions = true;
            this.colUID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUID.FieldName = "UID";
            this.colUID.Name = "colUID";
            // 
            // colInfoName
            // 
            this.colInfoName.AppearanceHeader.Options.UseTextOptions = true;
            this.colInfoName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colInfoName.Caption = "名称";
            this.colInfoName.FieldName = "InfoName";
            this.colInfoName.Name = "colInfoName";
            this.colInfoName.Visible = true;
            this.colInfoName.VisibleIndex = 0;
            // 
            // coltype
            // 
            this.coltype.AppearanceHeader.Options.UseTextOptions = true;
            this.coltype.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.coltype.Caption = "(1表示否决因素，2表示一般因素） ";
            this.coltype.FieldName = "type";
            this.coltype.Name = "coltype";
            // 
            // colcol1
            // 
            this.colcol1.AppearanceHeader.Options.UseTextOptions = true;
            this.colcol1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcol1.FieldName = "col1";
            this.colcol1.Name = "colcol1";
            // 
            // colcol2
            // 
            this.colcol2.AppearanceHeader.Options.UseTextOptions = true;
            this.colcol2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcol2.FieldName = "col2";
            this.colcol2.Name = "colcol2";
            // 
            // colcol3
            // 
            this.colcol3.AppearanceHeader.Options.UseTextOptions = true;
            this.colcol3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcol3.FieldName = "col3";
            this.colcol3.Name = "colcol3";
            // 
            // CtrlPSP_SubstationPar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Name = "CtrlPSP_SubstationPar";
            this.Size = new System.Drawing.Size(534, 319);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn colUID;		private DevExpress.XtraGrid.Columns.GridColumn colInfoName;		private DevExpress.XtraGrid.Columns.GridColumn coltype;		private DevExpress.XtraGrid.Columns.GridColumn colcol1;		private DevExpress.XtraGrid.Columns.GridColumn colcol2;		private DevExpress.XtraGrid.Columns.GridColumn colcol3;	}
}
