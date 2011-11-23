namespace Itop.TLPsp
{
    partial class chaoliuResult
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(chaoliuResult));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.pSPDEVBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoltV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLineLength = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLineType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.colLineStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLineR = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLineTQ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLineGNDC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLineChange = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.wireCategoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.schedulerStorage1 = new DevExpress.XtraScheduler.SchedulerStorage(this.components);
            this.xtraVertGridBlending1 = new DevExpress.XtraVerticalGrid.Blending.XtraVertGridBlending();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSPDEVBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wireCategoryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerStorage1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.DataSource = this.pSPDEVBindingSource;
            this.gridControl1.EmbeddedNavigator.Name = "";
            this.gridControl1.Location = new System.Drawing.Point(12, 12);
            this.gridControl1.LookAndFeel.SkinName = "Liquid Sky";
            this.gridControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1,
            this.repositoryItemComboBox1,
            this.repositoryItemComboBox2});
            this.gridControl1.Size = new System.Drawing.Size(599, 293);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // pSPDEVBindingSource
            // 
            this.pSPDEVBindingSource.DataSource = typeof(Itop.Domain.Graphics.PSPDEV);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colName,
            this.colVoltV,
            this.colLineLength,
            this.colLineType,
            this.colLineStatus,
            this.colLineR,
            this.colLineTQ,
            this.colLineGNDC,
            this.colLineChange});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // colName
            // 
            this.colName.Caption = "名称";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsFilter.AllowAutoFilter = false;
            this.colName.OptionsFilter.AllowFilter = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            this.colName.Width = 78;
            // 
            // colVoltV
            // 
            this.colVoltV.Caption = "电压";
            this.colVoltV.FieldName = "VoltV";
            this.colVoltV.Name = "colVoltV";
            this.colVoltV.OptionsFilter.AllowAutoFilter = false;
            this.colVoltV.OptionsFilter.AllowFilter = false;
            this.colVoltV.Visible = true;
            this.colVoltV.VisibleIndex = 1;
            this.colVoltV.Width = 48;
            // 
            // colLineLength
            // 
            this.colLineLength.Caption = "线路长度";
            this.colLineLength.FieldName = "LineLength";
            this.colLineLength.Name = "colLineLength";
            this.colLineLength.OptionsFilter.AllowAutoFilter = false;
            this.colLineLength.OptionsFilter.AllowFilter = false;
            this.colLineLength.Visible = true;
            this.colLineLength.VisibleIndex = 2;
            this.colLineLength.Width = 61;
            // 
            // colLineType
            // 
            this.colLineType.Caption = "线路型号";
            this.colLineType.ColumnEdit = this.repositoryItemComboBox2;
            this.colLineType.FieldName = "LineType";
            this.colLineType.Name = "colLineType";
            this.colLineType.OptionsFilter.AllowAutoFilter = false;
            this.colLineType.OptionsFilter.AllowFilter = false;
            this.colLineType.Visible = true;
            this.colLineType.VisibleIndex = 3;
            this.colLineType.Width = 78;
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            // 
            // colLineStatus
            // 
            this.colLineStatus.Caption = "线路状态";
            this.colLineStatus.FieldName = "LineStatus";
            this.colLineStatus.Name = "colLineStatus";
            this.colLineStatus.OptionsFilter.AllowAutoFilter = false;
            this.colLineStatus.OptionsFilter.AllowFilter = false;
            this.colLineStatus.Visible = true;
            this.colLineStatus.VisibleIndex = 4;
            this.colLineStatus.Width = 74;
            // 
            // colLineR
            // 
            this.colLineR.Caption = "电阻";
            this.colLineR.FieldName = "LineR";
            this.colLineR.Name = "colLineR";
            this.colLineR.OptionsFilter.AllowAutoFilter = false;
            this.colLineR.OptionsFilter.AllowFilter = false;
            this.colLineR.Visible = true;
            this.colLineR.VisibleIndex = 5;
            this.colLineR.Width = 48;
            // 
            // colLineTQ
            // 
            this.colLineTQ.Caption = "电抗";
            this.colLineTQ.FieldName = "LineTQ";
            this.colLineTQ.Name = "colLineTQ";
            this.colLineTQ.OptionsFilter.AllowAutoFilter = false;
            this.colLineTQ.OptionsFilter.AllowFilter = false;
            this.colLineTQ.Visible = true;
            this.colLineTQ.VisibleIndex = 6;
            this.colLineTQ.Width = 48;
            // 
            // colLineGNDC
            // 
            this.colLineGNDC.Caption = "电纳";
            this.colLineGNDC.FieldName = "LineGNDC";
            this.colLineGNDC.Name = "colLineGNDC";
            this.colLineGNDC.OptionsFilter.AllowAutoFilter = false;
            this.colLineGNDC.OptionsFilter.AllowFilter = false;
            this.colLineGNDC.Visible = true;
            this.colLineGNDC.VisibleIndex = 7;
            this.colLineGNDC.Width = 51;
            // 
            // colLineChange
            // 
            this.colLineChange.Caption = "电流限值";
            this.colLineChange.FieldName = "LineChange";
            this.colLineChange.Name = "colLineChange";
            this.colLineChange.OptionsFilter.AllowAutoFilter = false;
            this.colLineChange.OptionsFilter.AllowFilter = false;
            this.colLineChange.Visible = true;
            this.colLineChange.VisibleIndex = 8;
            this.colLineChange.Width = 80;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.DataSource = this.wireCategoryBindingSource;
            this.repositoryItemLookUpEdit1.DisplayMember = "WireType";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            // 
            // wireCategoryBindingSource
            // 
            this.wireCategoryBindingSource.DataSource = typeof(Itop.Domain.Graphics.WireCategory);
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // chaoliuResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(623, 317);
            this.Controls.Add(this.gridControl1);
            this.Name = "chaoliuResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "chaoliuResult";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSPDEVBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wireCategoryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.schedulerStorage1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource pSPDEVBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colVoltV;
        private DevExpress.XtraGrid.Columns.GridColumn colLineR;
        private DevExpress.XtraGrid.Columns.GridColumn colLineTQ;
        private DevExpress.XtraGrid.Columns.GridColumn colLineGNDC;
        private DevExpress.XtraGrid.Columns.GridColumn colLineChange;
        private DevExpress.XtraGrid.Columns.GridColumn colLineLength;
        private DevExpress.XtraGrid.Columns.GridColumn colLineType;
        private DevExpress.XtraGrid.Columns.GridColumn colLineStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private System.Windows.Forms.BindingSource wireCategoryBindingSource;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraScheduler.SchedulerStorage schedulerStorage1;
        private DevExpress.XtraVerticalGrid.Blending.XtraVertGridBlending xtraVertGridBlending1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox2;
    }
}