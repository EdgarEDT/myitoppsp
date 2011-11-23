
namespace Itop.Client.Chen
{
	partial class CtrlPSP_Calc_Spring
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
			this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colValue1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colValue2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colValue3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colValue4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colValue5 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colValue6 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colValue7 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colValue8 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colValue9 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colValue10 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCol1 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCol2 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCol3 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colCol4 = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colFlag = new DevExpress.XtraGrid.Columns.GridColumn();
			this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
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
			this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {this.colID,this.colName,this.colValue1,this.colValue2,this.colValue3,this.colValue4,this.colValue5,this.colValue6,this.colValue7,this.colValue8,this.colValue9,this.colValue10,this.colCol1,this.colCol2,this.colCol3,this.colCol4,this.colFlag,this.colType});
			this.gridView.GridControl = this.gridControl;
			this.gridView.GroupPanelText = "参数";
			this.gridView.Name = "gridView";
			this.gridView.OptionsBehavior.Editable = false;
			this.gridView.OptionsPrint.UsePrintStyles = true;
			this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
			//
			// colID
			//
			this.colID.AppearanceHeader.Options.UseTextOptions = true;
			this.colID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colID.Caption = "";
			this.colID.FieldName = "ID";
			this.colID.Name = "colID";
			this.colID.Visible = true;
			this.colID.VisibleIndex = 0;
			//
			// colName
			//
			this.colName.AppearanceHeader.Options.UseTextOptions = true;
			this.colName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colName.Caption = "";
			this.colName.FieldName = "Name";
			this.colName.Name = "colName";
			this.colName.Visible = true;
			this.colName.VisibleIndex = 0;
			//
			// colValue1
			//
			this.colValue1.AppearanceHeader.Options.UseTextOptions = true;
			this.colValue1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colValue1.Caption = "";
			this.colValue1.DisplayFormat.FormatString = "n2";
			this.colValue1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colValue1.FieldName = "Value1";
			this.colValue1.Name = "colValue1";
			this.colValue1.Visible = true;
			this.colValue1.VisibleIndex = 0;
			//
			// colValue2
			//
			this.colValue2.AppearanceHeader.Options.UseTextOptions = true;
			this.colValue2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colValue2.Caption = "";
			this.colValue2.DisplayFormat.FormatString = "n2";
			this.colValue2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colValue2.FieldName = "Value2";
			this.colValue2.Name = "colValue2";
			this.colValue2.Visible = true;
			this.colValue2.VisibleIndex = 0;
			//
			// colValue3
			//
			this.colValue3.AppearanceHeader.Options.UseTextOptions = true;
			this.colValue3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colValue3.Caption = "";
			this.colValue3.DisplayFormat.FormatString = "n2";
			this.colValue3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colValue3.FieldName = "Value3";
			this.colValue3.Name = "colValue3";
			this.colValue3.Visible = true;
			this.colValue3.VisibleIndex = 0;
			//
			// colValue4
			//
			this.colValue4.AppearanceHeader.Options.UseTextOptions = true;
			this.colValue4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colValue4.Caption = "";
			this.colValue4.DisplayFormat.FormatString = "n2";
			this.colValue4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colValue4.FieldName = "Value4";
			this.colValue4.Name = "colValue4";
			this.colValue4.Visible = true;
			this.colValue4.VisibleIndex = 0;
			//
			// colValue5
			//
			this.colValue5.AppearanceHeader.Options.UseTextOptions = true;
			this.colValue5.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colValue5.Caption = "";
			this.colValue5.DisplayFormat.FormatString = "n2";
			this.colValue5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colValue5.FieldName = "Value5";
			this.colValue5.Name = "colValue5";
			this.colValue5.Visible = true;
			this.colValue5.VisibleIndex = 0;
			//
			// colValue6
			//
			this.colValue6.AppearanceHeader.Options.UseTextOptions = true;
			this.colValue6.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colValue6.Caption = "";
			this.colValue6.DisplayFormat.FormatString = "n2";
			this.colValue6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colValue6.FieldName = "Value6";
			this.colValue6.Name = "colValue6";
			this.colValue6.Visible = true;
			this.colValue6.VisibleIndex = 0;
			//
			// colValue7
			//
			this.colValue7.AppearanceHeader.Options.UseTextOptions = true;
			this.colValue7.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colValue7.Caption = "";
			this.colValue7.DisplayFormat.FormatString = "n2";
			this.colValue7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colValue7.FieldName = "Value7";
			this.colValue7.Name = "colValue7";
			this.colValue7.Visible = true;
			this.colValue7.VisibleIndex = 0;
			//
			// colValue8
			//
			this.colValue8.AppearanceHeader.Options.UseTextOptions = true;
			this.colValue8.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colValue8.Caption = "";
			this.colValue8.DisplayFormat.FormatString = "n2";
			this.colValue8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colValue8.FieldName = "Value8";
			this.colValue8.Name = "colValue8";
			this.colValue8.Visible = true;
			this.colValue8.VisibleIndex = 0;
			//
			// colValue9
			//
			this.colValue9.AppearanceHeader.Options.UseTextOptions = true;
			this.colValue9.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colValue9.Caption = "";
			this.colValue9.DisplayFormat.FormatString = "n2";
			this.colValue9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colValue9.FieldName = "Value9";
			this.colValue9.Name = "colValue9";
			this.colValue9.Visible = true;
			this.colValue9.VisibleIndex = 0;
			//
			// colValue10
			//
			this.colValue10.AppearanceHeader.Options.UseTextOptions = true;
			this.colValue10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colValue10.Caption = "";
			this.colValue10.DisplayFormat.FormatString = "n2";
			this.colValue10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
			this.colValue10.FieldName = "Value10";
			this.colValue10.Name = "colValue10";
			this.colValue10.Visible = true;
			this.colValue10.VisibleIndex = 0;
			//
			// colCol1
			//
			this.colCol1.AppearanceHeader.Options.UseTextOptions = true;
			this.colCol1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCol1.Caption = "";
			this.colCol1.FieldName = "Col1";
			this.colCol1.Name = "colCol1";
			this.colCol1.Visible = true;
			this.colCol1.VisibleIndex = 0;
			//
			// colCol2
			//
			this.colCol2.AppearanceHeader.Options.UseTextOptions = true;
			this.colCol2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCol2.Caption = "";
			this.colCol2.FieldName = "Col2";
			this.colCol2.Name = "colCol2";
			this.colCol2.Visible = true;
			this.colCol2.VisibleIndex = 0;
			//
			// colCol3
			//
			this.colCol3.AppearanceHeader.Options.UseTextOptions = true;
			this.colCol3.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCol3.Caption = "";
			this.colCol3.FieldName = "Col3";
			this.colCol3.Name = "colCol3";
			this.colCol3.Visible = true;
			this.colCol3.VisibleIndex = 0;
			//
			// colCol4
			//
			this.colCol4.AppearanceHeader.Options.UseTextOptions = true;
			this.colCol4.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colCol4.Caption = "";
			this.colCol4.FieldName = "Col4";
			this.colCol4.Name = "colCol4";
			this.colCol4.Visible = true;
			this.colCol4.VisibleIndex = 0;
			//
			// colFlag
			//
			this.colFlag.AppearanceHeader.Options.UseTextOptions = true;
			this.colFlag.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colFlag.Caption = "";
			this.colFlag.FieldName = "Flag";
			this.colFlag.Name = "colFlag";
			this.colFlag.Visible = true;
			this.colFlag.VisibleIndex = 0;
			//
			// colType
			//
			this.colType.AppearanceHeader.Options.UseTextOptions = true;
			this.colType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.colType.Caption = "";
			this.colType.FieldName = "Type";
			this.colType.Name = "colType";
			this.colType.Visible = true;
			this.colType.VisibleIndex = 0;
			//
			// DepartmentCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gridControl);
			this.Name = "PSP_Calc_SpringCtrl";
			this.Size = new System.Drawing.Size(534, 319);
			((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
			this.ResumeLayout(false);
		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn colID;		private DevExpress.XtraGrid.Columns.GridColumn colName;		private DevExpress.XtraGrid.Columns.GridColumn colValue1;		private DevExpress.XtraGrid.Columns.GridColumn colValue2;		private DevExpress.XtraGrid.Columns.GridColumn colValue3;		private DevExpress.XtraGrid.Columns.GridColumn colValue4;		private DevExpress.XtraGrid.Columns.GridColumn colValue5;		private DevExpress.XtraGrid.Columns.GridColumn colValue6;		private DevExpress.XtraGrid.Columns.GridColumn colValue7;		private DevExpress.XtraGrid.Columns.GridColumn colValue8;		private DevExpress.XtraGrid.Columns.GridColumn colValue9;		private DevExpress.XtraGrid.Columns.GridColumn colValue10;		private DevExpress.XtraGrid.Columns.GridColumn colCol1;		private DevExpress.XtraGrid.Columns.GridColumn colCol2;		private DevExpress.XtraGrid.Columns.GridColumn colCol3;		private DevExpress.XtraGrid.Columns.GridColumn colCol4;		private DevExpress.XtraGrid.Columns.GridColumn colFlag;		private DevExpress.XtraGrid.Columns.GridColumn colType;	}
}
