namespace ItopVector.Tools
{
    partial class frmLayerTreeManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLayerTreeManager));
            this.btAdd = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btDel = new DevExpress.XtraEditors.SimpleButton();
            this.btEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btOK = new DevExpress.XtraEditors.SimpleButton();
            this.btCel = new DevExpress.XtraEditors.SimpleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmClearLink = new System.Windows.Forms.ToolStripMenuItem();
            this.btUp = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.checkedListBoxControl2 = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.dateEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.button2 = new DevExpress.XtraEditors.SimpleButton();
            this.button1 = new DevExpress.XtraEditors.SimpleButton();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // btAdd
            // 
            this.btAdd.Image = ((System.Drawing.Image)(resources.GetObject("btAdd.Image")));
            this.btAdd.Location = new System.Drawing.Point(2, 3);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(36, 27);
            this.btAdd.TabIndex = 1;
            this.btAdd.Text = "增加";
            this.btAdd.ToolTip = "增加";
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Location = new System.Drawing.Point(231, -20);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(2, 336);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "groupControl1";
            this.groupControl1.Visible = false;
            // 
            // btDel
            // 
            this.btDel.Image = ((System.Drawing.Image)(resources.GetObject("btDel.Image")));
            this.btDel.Location = new System.Drawing.Point(86, 3);
            this.btDel.Name = "btDel";
            this.btDel.Size = new System.Drawing.Size(37, 27);
            this.btDel.TabIndex = 1;
            this.btDel.Text = "删除";
            this.btDel.ToolTip = "删除";
            this.btDel.Click += new System.EventHandler(this.btDel_Click);
            // 
            // btEdit
            // 
            this.btEdit.Image = ((System.Drawing.Image)(resources.GetObject("btEdit.Image")));
            this.btEdit.Location = new System.Drawing.Point(43, 3);
            this.btEdit.Name = "btEdit";
            this.btEdit.Size = new System.Drawing.Size(38, 27);
            this.btEdit.TabIndex = 1;
            this.btEdit.Text = "修改";
            this.btEdit.ToolTip = "修改";
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // btOK
            // 
            this.btOK.Location = new System.Drawing.Point(244, 286);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(28, 27);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "确定";
            this.btOK.Visible = false;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // btCel
            // 
            this.btCel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCel.Location = new System.Drawing.Point(244, 286);
            this.btCel.Name = "btCel";
            this.btCel.Size = new System.Drawing.Size(27, 27);
            this.btCel.TabIndex = 1;
            this.btCel.Text = "取消";
            this.btCel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(241, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "被选中图层为\r\n当前操作图层";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label2.Location = new System.Drawing.Point(244, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "打钩为显示,\r\n不打钩为隐藏";
            this.label2.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全选ToolStripMenuItem,
            this.全消ToolStripMenuItem,
            this.tmClearLink});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 70);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.全选ToolStripMenuItem.Text = "全选";
            this.全选ToolStripMenuItem.Click += new System.EventHandler(this.全选ToolStripMenuItem_Click);
            // 
            // 全消ToolStripMenuItem
            // 
            this.全消ToolStripMenuItem.Name = "全消ToolStripMenuItem";
            this.全消ToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.全消ToolStripMenuItem.Text = "全消";
            this.全消ToolStripMenuItem.Click += new System.EventHandler(this.全消ToolStripMenuItem_Click);
            // 
            // tmClearLink
            // 
            this.tmClearLink.Name = "tmClearLink";
            this.tmClearLink.Size = new System.Drawing.Size(118, 22);
            this.tmClearLink.Text = "清除关联";
            this.tmClearLink.Click += new System.EventHandler(this.清除关联ToolStripMenuItem_Click);
            // 
            // btUp
            // 
            this.btUp.Location = new System.Drawing.Point(244, 286);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(28, 27);
            this.btUp.TabIndex = 1;
            this.btUp.Text = "复制";
            this.btUp.Visible = false;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(198, 3);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(37, 27);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "上移";
            this.simpleButton1.ToolTip = "上移";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(240, 3);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(37, 27);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "下移";
            this.simpleButton2.ToolTip = "下移";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.Image")));
            this.simpleButton3.Location = new System.Drawing.Point(128, 3);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(30, 27);
            this.simpleButton3.TabIndex = 1;
            this.simpleButton3.Text = "复制";
            this.simpleButton3.ToolTip = "复制";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton4.Image")));
            this.simpleButton4.Location = new System.Drawing.Point(163, 3);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(30, 27);
            this.simpleButton4.TabIndex = 6;
            this.simpleButton4.Text = "合并";
            this.simpleButton4.ToolTip = "合并";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // checkedListBoxControl2
            // 
            this.checkedListBoxControl2.CheckOnClick = true;
            this.checkedListBoxControl2.Items.AddRange(new DevExpress.XtraEditors.Controls.CheckedListBoxItem[] {
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("单层复制"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("多层复制"),
            new DevExpress.XtraEditors.Controls.CheckedListBoxItem("年图层复制")});
            this.checkedListBoxControl2.Location = new System.Drawing.Point(147, 26);
            this.checkedListBoxControl2.Name = "checkedListBoxControl2";
            this.checkedListBoxControl2.Size = new System.Drawing.Size(110, 65);
            this.checkedListBoxControl2.TabIndex = 8;
            this.checkedListBoxControl2.Visible = false;
            this.checkedListBoxControl2.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.checkedListBoxControl2_ItemCheck);
            this.checkedListBoxControl2.Leave += new System.EventHandler(this.checkedListBoxControl2_Leave);
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(8, 36);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "数据有效时间";
            this.checkEdit1.Size = new System.Drawing.Size(121, 19);
            this.checkEdit1.TabIndex = 10;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 14);
            this.label3.TabIndex = 13;
            this.label3.Text = "起";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 14);
            this.label4.TabIndex = 13;
            this.label4.Text = "止";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(86, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 14);
            this.label5.TabIndex = 15;
            this.label5.Text = "年";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(86, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 14);
            this.label6.TabIndex = 15;
            this.label6.Text = "年";
            // 
            // dateEdit1
            // 
            this.dateEdit1.Location = new System.Drawing.Point(27, 57);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Mask.EditMask = "f0";
            this.dateEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.dateEdit1.Properties.MaxLength = 4;
            this.dateEdit1.Size = new System.Drawing.Size(56, 21);
            this.dateEdit1.TabIndex = 17;
            // 
            // dateEdit2
            // 
            this.dateEdit2.Location = new System.Drawing.Point(27, 82);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Mask.EditMask = "f0";
            this.dateEdit2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.dateEdit2.Properties.MaxLength = 4;
            this.dateEdit2.Size = new System.Drawing.Size(56, 21);
            this.dateEdit2.TabIndex = 17;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(106, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(43, 27);
            this.button2.TabIndex = 18;
            this.button2.Text = "确定";
            this.button2.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(246, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 27);
            this.button1.TabIndex = 19;
            this.button1.Text = "参";
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.simpleButton5_Click_1);
            // 
            // treeList1
            // 
            this.treeList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeList1.Location = new System.Drawing.Point(4, 109);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.OptionsView.ShowCheckBoxes = true;
            this.treeList1.Size = new System.Drawing.Size(272, 293);
            this.treeList1.TabIndex = 20;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "图层名称";
            this.treeListColumn1.FieldName = "NAME";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(247, 286);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(120, 89);
            this.checkedListBox1.TabIndex = 21;
            this.checkedListBox1.Visible = false;
            // 
            // frmLayerTreeManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 414);
            this.Controls.Add(this.treeList1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dateEdit2);
            this.Controls.Add(this.dateEdit1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.checkedListBoxControl2);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btCel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btUp);
            this.Controls.Add(this.btEdit);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btDel);
            this.Controls.Add(this.btAdd);
            this.Controls.Add(this.checkedListBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLayerTreeManager";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "图层管理";
            this.Load += new System.EventHandler(this.frmLayerManager_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLayerManager_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btAdd;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btDel;
        private DevExpress.XtraEditors.SimpleButton btEdit;
        private DevExpress.XtraEditors.SimpleButton btOK;
        private DevExpress.XtraEditors.SimpleButton btCel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btUp;       
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全消ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tmClearLink;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.TextEdit dateEdit1;
        private DevExpress.XtraEditors.TextEdit dateEdit2;
        private DevExpress.XtraEditors.SimpleButton button2;
        private DevExpress.XtraEditors.SimpleButton button1;
        private DevExpress.XtraTreeList.TreeList treeList1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
    }
}