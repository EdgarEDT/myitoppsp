
namespace ItopVector.Tools
{
    partial class FrmPSP_SubstationParDialog
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        /// </summary>
        protected override void Dispose(bool disposing)
        {

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabPage = new DevExpress.XtraTab.XtraTabPage();
            this.vGridControl = new DevExpress.XtraVerticalGrid.VGridControl();
            this.ItemTextEditUID = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditInfoName = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEdittype = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditcol1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditcol2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.ItemTextEditcol3 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.rowUID = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowInfoName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowtype = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowcol1 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowcol2 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowcol3 = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditUID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditInfoName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEdittype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol3)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.xtraTabControl1);
            this.panelControl.Controls.Add(this.btnOK);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(395, 132);
            this.panelControl.TabIndex = 0;
            this.panelControl.Text = "panelControl1";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraTabControl1.Location = new System.Drawing.Point(7, 7);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabPage;
            this.xtraTabControl1.Size = new System.Drawing.Size(382, 90);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabPage});
            this.xtraTabControl1.Text = "xtraTabControl1";
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.vGridControl);
            this.tabPage.Name = "tabPage";
            this.tabPage.Size = new System.Drawing.Size(378, 75);
            // 
            // vGridControl
            // 
            this.vGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.vGridControl.Location = new System.Drawing.Point(3, 3);
            this.vGridControl.Name = "vGridControl";
            this.vGridControl.RecordWidth = 234;
            this.vGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ItemTextEditUID,
            this.ItemTextEditInfoName,
            this.ItemTextEdittype,
            this.ItemTextEditcol1,
            this.ItemTextEditcol2,
            this.ItemTextEditcol3});
            this.vGridControl.RowHeaderWidth = 129;
            this.vGridControl.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowUID,
            this.rowInfoName,
            this.rowtype,
            this.rowcol1,
            this.rowcol2,
            this.rowcol3});
            this.vGridControl.Size = new System.Drawing.Size(371, 68);
            this.vGridControl.TabIndex = 0;
            // 
            // ItemTextEditUID
            // 
            this.ItemTextEditUID.AutoHeight = false;
            this.ItemTextEditUID.Name = "ItemTextEditUID";
            // 
            // ItemTextEditInfoName
            // 
            this.ItemTextEditInfoName.AutoHeight = false;
            this.ItemTextEditInfoName.MaxLength = 50;
            this.ItemTextEditInfoName.Name = "ItemTextEditInfoName";
            // 
            // ItemTextEdittype
            // 
            this.ItemTextEdittype.AutoHeight = false;
            this.ItemTextEdittype.Name = "ItemTextEdittype";
            // 
            // ItemTextEditcol1
            // 
            this.ItemTextEditcol1.AutoHeight = false;
            this.ItemTextEditcol1.MaxLength = 50;
            this.ItemTextEditcol1.Name = "ItemTextEditcol1";
            // 
            // ItemTextEditcol2
            // 
            this.ItemTextEditcol2.AutoHeight = false;
            this.ItemTextEditcol2.MaxLength = 50;
            this.ItemTextEditcol2.Name = "ItemTextEditcol2";
            // 
            // ItemTextEditcol3
            // 
            this.ItemTextEditcol3.AutoHeight = false;
            this.ItemTextEditcol3.MaxLength = 50;
            this.ItemTextEditcol3.Name = "ItemTextEditcol3";
            // 
            // rowUID
            // 
            this.rowUID.Height = 25;
            this.rowUID.Name = "rowUID";
            this.rowUID.Properties.FieldName = "UID";
            this.rowUID.Properties.ImageIndex = 25;
            this.rowUID.Properties.RowEdit = this.ItemTextEditUID;
            this.rowUID.Visible = false;
            // 
            // rowInfoName
            // 
            this.rowInfoName.Height = 25;
            this.rowInfoName.Name = "rowInfoName";
            this.rowInfoName.Properties.Caption = "����";
            this.rowInfoName.Properties.FieldName = "InfoName";
            this.rowInfoName.Properties.ImageIndex = 25;
            this.rowInfoName.Properties.RowEdit = this.ItemTextEditInfoName;
            // 
            // rowtype
            // 
            this.rowtype.Height = 25;
            this.rowtype.Name = "rowtype";
            this.rowtype.Properties.Caption = "(1��ʾ������أ�2��ʾһ�����أ� ";
            this.rowtype.Properties.FieldName = "type";
            this.rowtype.Properties.ImageIndex = 25;
            this.rowtype.Properties.RowEdit = this.ItemTextEdittype;
            this.rowtype.Visible = false;
            // 
            // rowcol1
            // 
            this.rowcol1.Height = 25;
            this.rowcol1.Name = "rowcol1";
            this.rowcol1.Properties.FieldName = "col1";
            this.rowcol1.Properties.ImageIndex = 25;
            this.rowcol1.Properties.RowEdit = this.ItemTextEditcol1;
            this.rowcol1.Visible = false;
            // 
            // rowcol2
            // 
            this.rowcol2.Height = 25;
            this.rowcol2.Name = "rowcol2";
            this.rowcol2.Properties.FieldName = "col2";
            this.rowcol2.Properties.ImageIndex = 25;
            this.rowcol2.Properties.RowEdit = this.ItemTextEditcol2;
            this.rowcol2.Visible = false;
            // 
            // rowcol3
            // 
            this.rowcol3.Height = 25;
            this.rowcol3.Name = "rowcol3";
            this.rowcol3.Properties.FieldName = "col3";
            this.rowcol3.Properties.ImageIndex = 25;
            this.rowcol3.Properties.RowEdit = this.ItemTextEditcol3;
            this.rowcol3.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(210, 102);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "ȷ��";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(314, 102);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "ȡ��";
            // 
            // FrmPSP_SubstationParDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(395, 132);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPSP_SubstationParDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmPSP_SubstationParDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditUID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditInfoName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEdittype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemTextEditcol3)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabPage;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraVerticalGrid.VGridControl vGridControl;

        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditUID;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowUID;

        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditInfoName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowInfoName;

        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEdittype;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowtype;

        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditcol1;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowcol1;

        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditcol2;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowcol2;

        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit ItemTextEditcol3;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowcol3;

    }
}
