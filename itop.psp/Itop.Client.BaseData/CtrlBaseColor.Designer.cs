
namespace Itop.Client.BaseData
{
	partial class CtrlBaseColor
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
            this.colTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.colColor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMinValue = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
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
            this.repositoryItemColorEdit1});
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
            this.colTitle,
            this.gridColumn1,
            this.colColor,
            this.colMaxValue,
            this.colMinValue});
            this.gridView.GridControl = this.gridControl;
            this.gridView.GroupPanelText = "��ɫ";
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsPrint.UsePrintStyles = true;
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // colTitle
            // 
            this.colTitle.AppearanceHeader.Options.UseTextOptions = true;
            this.colTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTitle.Caption = "����";
            this.colTitle.FieldName = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.Visible = true;
            this.colTitle.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "��ɫ";
            this.gridColumn1.ColumnEdit = this.repositoryItemColorEdit1;
            this.gridColumn1.FieldName = "Color1";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // repositoryItemColorEdit1
            // 
            this.repositoryItemColorEdit1.AutoHeight = false;
            this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            // 
            // colColor
            // 
            this.colColor.AppearanceHeader.Options.UseTextOptions = true;
            this.colColor.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colColor.Caption = "��ɫ";
            this.colColor.FieldName = "Color";
            this.colColor.Name = "colColor";
            // 
            // colMaxValue
            // 
            this.colMaxValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colMaxValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMaxValue.Caption = "���ֵ";
            this.colMaxValue.DisplayFormat.FormatString = "n2";
            this.colMaxValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMaxValue.FieldName = "MaxValue";
            this.colMaxValue.Name = "colMaxValue";
            // 
            // colMinValue
            // 
            this.colMinValue.AppearanceHeader.Options.UseTextOptions = true;
            this.colMinValue.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMinValue.Caption = "��Сֵ";
            this.colMinValue.DisplayFormat.FormatString = "n2";
            this.colMinValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colMinValue.FieldName = "MinValue";
            this.colMinValue.Name = "colMinValue";
            // 
            // CtrlBaseColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Name = "CtrlBaseColor";
            this.Size = new System.Drawing.Size(534, 319);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn colTitle;		private DevExpress.XtraGrid.Columns.GridColumn colColor;		private DevExpress.XtraGrid.Columns.GridColumn colMaxValue;		private DevExpress.XtraGrid.Columns.GridColumn colMinValue;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorEdit repositoryItemColorEdit1;
    }
}
