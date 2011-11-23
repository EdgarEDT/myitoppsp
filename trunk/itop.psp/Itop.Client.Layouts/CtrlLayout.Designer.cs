
namespace Itop.Client.Layouts
{
	partial class CtrlLayout
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		/// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region �����������ɵĴ���

		/// <summary> 
		/// �����֧������ķ��� - ��Ҫ
		/// ʹ�ô���༭���޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLayoutName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreater = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemark = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.gridControl.Load += new System.EventHandler(this.gridControl_Load);
            //this.gridControl.Click += new System.EventHandler(this.gridControl_Click);
            // 
            // gridView
            // 
            this.gridView.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLayoutName,
            this.colCreateDate,
            this.colCreater,
            this.colRemark,
            this.gridColumn1});
            this.gridView.GridControl = this.gridControl;
            this.gridView.GroupPanelText = "�滮����";
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsPrint.UsePrintStyles = true;
            this.gridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn1, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // colLayoutName
            // 
            this.colLayoutName.AppearanceHeader.Options.UseTextOptions = true;
            this.colLayoutName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colLayoutName.Caption = "�滮����";
            this.colLayoutName.FieldName = "LayoutName";
            this.colLayoutName.Name = "colLayoutName";
            this.colLayoutName.Visible = true;
            this.colLayoutName.VisibleIndex = 0;
            // 
            // colCreateDate
            // 
            this.colCreateDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreateDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreateDate.Caption = "��������";
            this.colCreateDate.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.colCreateDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreateDate.FieldName = "CreateDate";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 1;
            // 
            // colCreater
            // 
            this.colCreater.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreater.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreater.Caption = "������";
            this.colCreater.FieldName = "CreaterName";
            this.colCreater.Name = "colCreater";
            this.colCreater.Visible = true;
            this.colCreater.VisibleIndex = 2;
            // 
            // colRemark
            // 
            this.colRemark.AppearanceHeader.Options.UseTextOptions = true;
            this.colRemark.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRemark.Caption = "��ע";
            this.colRemark.FieldName = "Remark";
            this.colRemark.Name = "colRemark";
            this.colRemark.Visible = true;
            this.colRemark.VisibleIndex = 3;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "CreateDate";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // CtrlLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Name = "CtrlLayout";
            this.Size = new System.Drawing.Size(534, 319);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn colLayoutName;		private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;		private DevExpress.XtraGrid.Columns.GridColumn colCreater;		private DevExpress.XtraGrid.Columns.GridColumn colRemark;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}
