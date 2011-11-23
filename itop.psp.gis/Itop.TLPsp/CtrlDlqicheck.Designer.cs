namespace Itop.TLPsp
{
    partial class CtrlDlqicheck
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
            this.components = new System.ComponentModel.Container();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.belongsubstation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.outI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.busshortI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.transhege = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ZLshort = new DevExpress.XtraGrid.Columns.GridColumn();
            this.buszl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ZLhege = new DevExpress.XtraGrid.Columns.GridColumn();
            this.endhege = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pSPDEVBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSPDEVBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.DataSource = this.pSPDEVBindingSource;
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControl.Location = new System.Drawing.Point(0, 0);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(802, 222);
            this.gridControl.TabIndex = 1;
            this.gridControl.UseEmbeddedNavigator = true;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.gridView.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.name,
            this.belongsubstation,
            this.outI,
            this.busshortI,
            this.transhege,
            this.ZLshort,
            this.buszl,
            this.ZLhege,
            this.endhege});
            this.gridView.GridControl = this.gridControl;
            this.gridView.GroupPanelText = "断路器开断能力校核表";
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsPrint.UsePrintStyles = true;
            // 
            // name
            // 
            this.name.Caption = "断路器名";
            this.name.FieldName = "Name";
            this.name.Name = "name";
            this.name.Visible = true;
            this.name.VisibleIndex = 0;
            // 
            // belongsubstation
            // 
            this.belongsubstation.Caption = "所属变电站";
            this.belongsubstation.FieldName = "HuganLine1";
            this.belongsubstation.Name = "belongsubstation";
            this.belongsubstation.Visible = true;
            this.belongsubstation.VisibleIndex = 1;
            // 
            // outI
            // 
            this.outI.Caption = "额定短路开断电流（kA）";
            this.outI.FieldName = "HuganTQ1";
            this.outI.Name = "outI";
            this.outI.Visible = true;
            this.outI.VisibleIndex = 2;
            // 
            // busshortI
            // 
            this.busshortI.Caption = "变电站母线短路电流（kA）";
            this.busshortI.FieldName = "OutQ";
            this.busshortI.Name = "busshortI";
            this.busshortI.Visible = true;
            this.busshortI.VisibleIndex = 3;
            // 
            // transhege
            // 
            this.transhege.Caption = "交流分量是否合格";
            this.transhege.FieldName = "HuganLine3";
            this.transhege.Name = "transhege";
            this.transhege.Visible = true;
            this.transhege.VisibleIndex = 4;
            // 
            // ZLshort
            // 
            this.ZLshort.Caption = "额定短路开断电流的直流分量（%）";
            this.ZLshort.FieldName = "HuganTQ3";
            this.ZLshort.Name = "ZLshort";
            this.ZLshort.Visible = true;
            this.ZLshort.VisibleIndex = 5;
            // 
            // buszl
            // 
            this.buszl.Caption = "母线短路直流分量（%）";
            this.buszl.FieldName = "HuganTQ5";
            this.buszl.Name = "buszl";
            this.buszl.Visible = true;
            this.buszl.VisibleIndex = 6;
            // 
            // ZLhege
            // 
            this.ZLhege.Caption = "直流分量是否合格";
            this.ZLhege.FieldName = "HuganLine4";
            this.ZLhege.Name = "ZLhege";
            this.ZLhege.Visible = true;
            this.ZLhege.VisibleIndex = 7;
            // 
            // endhege
            // 
            this.endhege.Caption = "开断能力是否合格";
            this.endhege.FieldName = "KName";
            this.endhege.Name = "endhege";
            this.endhege.Visible = true;
            this.endhege.VisibleIndex = 8;
            // 
            // pSPDEVBindingSource
            // 
            this.pSPDEVBindingSource.DataSource = typeof(Itop.Domain.Graphics.PSPDEV);
            // 
            // CtrlDlqicheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Name = "CtrlDlqicheck";
            this.Size = new System.Drawing.Size(802, 222);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pSPDEVBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Columns.GridColumn name;
        private DevExpress.XtraGrid.Columns.GridColumn belongsubstation;
        private DevExpress.XtraGrid.Columns.GridColumn outI;
        private DevExpress.XtraGrid.Columns.GridColumn busshortI;
        private DevExpress.XtraGrid.Columns.GridColumn transhege;
        private DevExpress.XtraGrid.Columns.GridColumn ZLshort;
        private DevExpress.XtraGrid.Columns.GridColumn buszl;
        private DevExpress.XtraGrid.Columns.GridColumn ZLhege;
        private DevExpress.XtraGrid.Columns.GridColumn endhege;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private System.Windows.Forms.BindingSource pSPDEVBindingSource;
    }
}
