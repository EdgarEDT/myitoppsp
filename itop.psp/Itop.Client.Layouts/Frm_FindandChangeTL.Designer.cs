namespace Itop.Client.Layouts
{
    partial class Frm_FindandChangeTL
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
            this.txtold = new System.Windows.Forms.TextBox();
            this.gropold = new System.Windows.Forms.GroupBox();
            this.gropnew = new System.Windows.Forms.GroupBox();
            this.txtnew = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.UID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MarkName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MarkDisc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MarkText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.StartP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MarkType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnChangeandNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnChange = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCanser = new DevExpress.XtraEditors.SimpleButton();
            this.gropold.SuspendLayout();
            this.gropnew.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtold
            // 
            this.txtold.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtold.Location = new System.Drawing.Point(8, 16);
            this.txtold.Multiline = true;
            this.txtold.Name = "txtold";
            this.txtold.ReadOnly = true;
            this.txtold.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtold.Size = new System.Drawing.Size(210, 81);
            this.txtold.TabIndex = 2;
            // 
            // gropold
            // 
            this.gropold.Controls.Add(this.txtold);
            this.gropold.Location = new System.Drawing.Point(272, 3);
            this.gropold.Name = "gropold";
            this.gropold.Size = new System.Drawing.Size(231, 107);
            this.gropold.TabIndex = 3;
            this.gropold.TabStop = false;
            this.gropold.Text = "当前内容";
            // 
            // gropnew
            // 
            this.gropnew.Controls.Add(this.txtnew);
            this.gropnew.Location = new System.Drawing.Point(271, 118);
            this.gropnew.Name = "gropnew";
            this.gropnew.Size = new System.Drawing.Size(231, 112);
            this.gropnew.TabIndex = 4;
            this.gropnew.TabStop = false;
            this.gropnew.Text = "替换内容";
            // 
            // txtnew
            // 
            this.txtnew.Location = new System.Drawing.Point(10, 21);
            this.txtnew.Multiline = true;
            this.txtnew.Name = "txtnew";
            this.txtnew.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtnew.Size = new System.Drawing.Size(210, 83);
            this.txtnew.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gridControl2);
            this.panel1.Location = new System.Drawing.Point(12, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 215);
            this.panel1.TabIndex = 10;
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(252, 215);
            this.gridControl2.TabIndex = 5;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.UID,
            this.MarkName,
            this.MarkDisc,
            this.MarkText,
            this.StartP,
            this.MarkType,
            this.CreateDate});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.gridView2.GroupPanelText = "书签列表";
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView2_FocusedRowChanged);
            // 
            // UID
            // 
            this.UID.Caption = "书签编号";
            this.UID.FieldName = "UID";
            this.UID.Name = "UID";
            // 
            // MarkName
            // 
            this.MarkName.Caption = "名称";
            this.MarkName.FieldName = "MarkName";
            this.MarkName.Name = "MarkName";
            this.MarkName.Visible = true;
            this.MarkName.VisibleIndex = 0;
            this.MarkName.Width = 144;
            // 
            // MarkDisc
            // 
            this.MarkDisc.Caption = "描述";
            this.MarkDisc.FieldName = "MarkDisc";
            this.MarkDisc.Name = "MarkDisc";
            this.MarkDisc.Visible = true;
            this.MarkDisc.VisibleIndex = 1;
            this.MarkDisc.Width = 144;
            // 
            // MarkText
            // 
            this.MarkText.Caption = "书签文本";
            this.MarkText.FieldName = "MarkText";
            this.MarkText.Name = "MarkText";
            this.MarkText.Width = 144;
            // 
            // StartP
            // 
            this.StartP.Caption = "书签起位";
            this.StartP.FieldName = "StartP";
            this.StartP.Name = "StartP";
            // 
            // MarkType
            // 
            this.MarkType.Caption = "书签类型";
            this.MarkType.FieldName = "MarkType";
            this.MarkType.Name = "MarkType";
            this.MarkType.Width = 60;
            // 
            // CreateDate
            // 
            this.CreateDate.Caption = "创建日期";
            this.CreateDate.FieldName = "CreateDate";
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.Width = 118;
            // 
            // btnChangeandNext
            // 
            this.btnChangeandNext.Location = new System.Drawing.Point(12, 245);
            this.btnChangeandNext.Name = "btnChangeandNext";
            this.btnChangeandNext.Size = new System.Drawing.Size(110, 23);
            this.btnChangeandNext.TabIndex = 11;
            this.btnChangeandNext.Text = "替换并到下一个";
            this.btnChangeandNext.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(128, 245);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(91, 23);
            this.btnChange.TabIndex = 12;
            this.btnChange.Text = "替 换";
            this.btnChange.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(225, 245);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(91, 23);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = "下一个";
            this.btnNext.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(322, 245);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(91, 23);
            this.btnOk.TabIndex = 14;
            this.btnOk.Text = "保 存";
            this.btnOk.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // btnCanser
            // 
            this.btnCanser.Location = new System.Drawing.Point(419, 245);
            this.btnCanser.Name = "btnCanser";
            this.btnCanser.Size = new System.Drawing.Size(91, 23);
            this.btnCanser.TabIndex = 15;
            this.btnCanser.Text = "取 消";
            this.btnCanser.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // Frm_FindandChangeTL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 286);
            this.Controls.Add(this.btnCanser);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.btnChangeandNext);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gropnew);
            this.Controls.Add(this.gropold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_FindandChangeTL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "规划报告编制助手";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Frm_FindandChangeTL_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_FindandChangeTL_FormClosing);
            this.gropold.ResumeLayout(false);
            this.gropold.PerformLayout();
            this.gropnew.ResumeLayout(false);
            this.gropnew.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtold;
        private System.Windows.Forms.GroupBox gropold;
        private System.Windows.Forms.GroupBox gropnew;
        private System.Windows.Forms.TextBox txtnew;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gridControl2;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn UID;
        private DevExpress.XtraGrid.Columns.GridColumn MarkName;
        private DevExpress.XtraGrid.Columns.GridColumn MarkDisc;
        private DevExpress.XtraGrid.Columns.GridColumn MarkText;
        private DevExpress.XtraGrid.Columns.GridColumn StartP;
        private DevExpress.XtraGrid.Columns.GridColumn MarkType;
        private DevExpress.XtraGrid.Columns.GridColumn CreateDate;
        private DevExpress.XtraEditors.SimpleButton btnChangeandNext;
        private DevExpress.XtraEditors.SimpleButton btnChange;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCanser;
    }
}