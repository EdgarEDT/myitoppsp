
namespace Itop.Client.Layouts
{
    partial class CtrlFiles
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
            this.components = new System.ComponentModel.Container();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.���ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.�޸�ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ɾ��ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.��ӡToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDes = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFileSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.ContextMenuStrip = this.contextMenuStrip1;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControl.EmbeddedNavigator.CustomButtons.AddRange(new DevExpress.XtraEditors.NavigatorCustomButton[] {
            new DevExpress.XtraEditors.NavigatorCustomButton(6, "���"),
            new DevExpress.XtraEditors.NavigatorCustomButton(7, "ɾ��"),
            new DevExpress.XtraEditors.NavigatorCustomButton(8, "�޸�"),
            new DevExpress.XtraEditors.NavigatorCustomButton(12, "��ӡ")});
            this.gridControl.EmbeddedNavigator.Name = "";
            this.gridControl.EmbeddedNavigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.gridControl_EmbeddedNavigator_ButtonClick);
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(534, 327);
            this.gridControl.TabIndex = 0;
            this.gridControl.UseEmbeddedNavigator = true;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            this.gridControl.Load += new System.EventHandler(this.gridControl_Load);
            this.gridControl.Click += new System.EventHandler(this.gridControl_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.���ToolStripMenuItem,
            this.�޸�ToolStripMenuItem,
            this.ɾ��ToolStripMenuItem,
            this.��ӡToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(95, 92);
            // 
            // ���ToolStripMenuItem
            // 
            this.���ToolStripMenuItem.Name = "���ToolStripMenuItem";
            this.���ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.���ToolStripMenuItem.Text = "���";
            this.���ToolStripMenuItem.Click += new System.EventHandler(this.���ToolStripMenuItem_Click);
            // 
            // �޸�ToolStripMenuItem
            // 
            this.�޸�ToolStripMenuItem.Name = "�޸�ToolStripMenuItem";
            this.�޸�ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.�޸�ToolStripMenuItem.Text = "�޸�";
            this.�޸�ToolStripMenuItem.Click += new System.EventHandler(this.�޸�ToolStripMenuItem_Click);
            // 
            // ɾ��ToolStripMenuItem
            // 
            this.ɾ��ToolStripMenuItem.Name = "ɾ��ToolStripMenuItem";
            this.ɾ��ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.ɾ��ToolStripMenuItem.Text = "ɾ��";
            this.ɾ��ToolStripMenuItem.Click += new System.EventHandler(this.ɾ��ToolStripMenuItem_Click);
            // 
            // ��ӡToolStripMenuItem
            // 
            this.��ӡToolStripMenuItem.Name = "��ӡToolStripMenuItem";
            this.��ӡToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.��ӡToolStripMenuItem.Text = "��ӡ";
            this.��ӡToolStripMenuItem.Click += new System.EventHandler(this.��ӡToolStripMenuItem_Click);
            // 
            // gridView
            // 
            this.gridView.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDes,
            this.colFileType,
            this.colFileSize,
            this.colCreateDate,
            this.gridColumn1});
            this.gridView.GridControl = this.gridControl;
            this.gridView.GroupPanelText = "����";
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsPrint.UsePrintStyles = true;
            this.gridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCreateDate, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // colDes
            // 
            this.colDes.AppearanceHeader.Options.UseTextOptions = true;
            this.colDes.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDes.Caption = "����";
            this.colDes.FieldName = "Des";
            this.colDes.Name = "colDes";
            this.colDes.Visible = true;
            this.colDes.VisibleIndex = 0;
            // 
            // colFileType
            // 
            this.colFileType.AppearanceHeader.Options.UseTextOptions = true;
            this.colFileType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFileType.Caption = "�ļ�����";
            this.colFileType.FieldName = "FileType";
            this.colFileType.Name = "colFileType";
            this.colFileType.Visible = true;
            this.colFileType.VisibleIndex = 2;
            // 
            // colFileSize
            // 
            this.colFileSize.AppearanceHeader.Options.UseTextOptions = true;
            this.colFileSize.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFileSize.Caption = "�ļ���С(Byte)";
            this.colFileSize.DisplayFormat.FormatString = "n0";
            this.colFileSize.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colFileSize.FieldName = "FileSize";
            this.colFileSize.Name = "colFileSize";
            this.colFileSize.Visible = true;
            this.colFileSize.VisibleIndex = 3;
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
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "�ļ�";
            this.gridColumn1.FieldName = "FileName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            // 
            // CtrlFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Name = "CtrlFiles";
            this.Size = new System.Drawing.Size(534, 327);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.GridControl gridControl;
		private DevExpress.XtraGrid.Views.Grid.GridView gridView;
		private DevExpress.XtraGrid.Columns.GridColumn colDes;		private DevExpress.XtraGrid.Columns.GridColumn colFileType;		private DevExpress.XtraGrid.Columns.GridColumn colFileSize;		private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.ToolStripMenuItem ���ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem �޸�ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem ɾ��ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem ��ӡToolStripMenuItem;
    }
}
