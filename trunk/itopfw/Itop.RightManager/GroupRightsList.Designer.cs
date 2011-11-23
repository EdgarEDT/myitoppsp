namespace Itop.RightManager {
    partial class GroupRightsList {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMall = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMrun = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMadd = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMupd = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMdel = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMqry = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMprn = new System.Windows.Forms.ToolStripMenuItem();
            this.发送ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.审查ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.审批ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMnall = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMnrun = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMnadd = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMnupd = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMndel = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMnqry = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMnprn = new System.Windows.Forms.ToolStripMenuItem();
            this.发送ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.审查ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.审批ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ProgName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.run = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ins = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.修改 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.del = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.qry = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.prn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.send = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.exam = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.pass = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProgName,
            this.run,
            this.ins,
            this.修改,
            this.del,
            this.qry,
            this.prn,
            this.send,
            this.exam,
            this.pass});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(567, 281);
            this.dataGridView1.TabIndex = 0;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全选ToolStripMenuItem,
            this.取消ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(95, 48);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMall,
            this.TSMrun,
            this.TSMadd,
            this.TSMupd,
            this.TSMdel,
            this.TSMqry,
            this.TSMprn,
            this.发送ToolStripMenuItem,
            this.审查ToolStripMenuItem,
            this.审批ToolStripMenuItem});
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.全选ToolStripMenuItem.Text = "全选";
            // 
            // TSMall
            // 
            this.TSMall.Name = "TSMall";
            this.TSMall.Size = new System.Drawing.Size(152, 22);
            this.TSMall.Text = "全部";
            this.TSMall.Click += new System.EventHandler(this.TSMall_Click);
            // 
            // TSMrun
            // 
            this.TSMrun.Name = "TSMrun";
            this.TSMrun.Size = new System.Drawing.Size(152, 22);
            this.TSMrun.Text = "运行";
            this.TSMrun.Click += new System.EventHandler(this.TSMrun_Click);
            // 
            // TSMadd
            // 
            this.TSMadd.Name = "TSMadd";
            this.TSMadd.Size = new System.Drawing.Size(152, 22);
            this.TSMadd.Text = "添加";
            this.TSMadd.Click += new System.EventHandler(this.TSMadd_Click);
            // 
            // TSMupd
            // 
            this.TSMupd.Name = "TSMupd";
            this.TSMupd.Size = new System.Drawing.Size(152, 22);
            this.TSMupd.Text = "修改";
            this.TSMupd.Click += new System.EventHandler(this.TSMupd_Click);
            // 
            // TSMdel
            // 
            this.TSMdel.Name = "TSMdel";
            this.TSMdel.Size = new System.Drawing.Size(152, 22);
            this.TSMdel.Text = "删除";
            this.TSMdel.Click += new System.EventHandler(this.TSMdel_Click);
            // 
            // TSMqry
            // 
            this.TSMqry.Name = "TSMqry";
            this.TSMqry.Size = new System.Drawing.Size(152, 22);
            this.TSMqry.Text = "查询";
            this.TSMqry.Click += new System.EventHandler(this.TSMqry_Click);
            // 
            // TSMprn
            // 
            this.TSMprn.Name = "TSMprn";
            this.TSMprn.Size = new System.Drawing.Size(152, 22);
            this.TSMprn.Text = "打印";
            this.TSMprn.Click += new System.EventHandler(this.TSMprn_Click);
            // 
            // 发送ToolStripMenuItem
            // 
            this.发送ToolStripMenuItem.Name = "发送ToolStripMenuItem";
            this.发送ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.发送ToolStripMenuItem.Text = "发送";
            this.发送ToolStripMenuItem.Visible = false;
            this.发送ToolStripMenuItem.Click += new System.EventHandler(this.发送ToolStripMenuItem_Click);
            // 
            // 审查ToolStripMenuItem
            // 
            this.审查ToolStripMenuItem.Name = "审查ToolStripMenuItem";
            this.审查ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.审查ToolStripMenuItem.Text = "审核";
            this.审查ToolStripMenuItem.Visible = false;
            this.审查ToolStripMenuItem.Click += new System.EventHandler(this.审查ToolStripMenuItem_Click);
            // 
            // 审批ToolStripMenuItem
            // 
            this.审批ToolStripMenuItem.Name = "审批ToolStripMenuItem";
            this.审批ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.审批ToolStripMenuItem.Text = "审批";
            this.审批ToolStripMenuItem.Visible = false;
            this.审批ToolStripMenuItem.Click += new System.EventHandler(this.审批ToolStripMenuItem_Click);
            // 
            // 取消ToolStripMenuItem
            // 
            this.取消ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMnall,
            this.TSMnrun,
            this.TSMnadd,
            this.TSMnupd,
            this.TSMndel,
            this.TSMnqry,
            this.TSMnprn,
            this.发送ToolStripMenuItem1,
            this.审查ToolStripMenuItem1,
            this.审批ToolStripMenuItem1});
            this.取消ToolStripMenuItem.Name = "取消ToolStripMenuItem";
            this.取消ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.取消ToolStripMenuItem.Text = "取消";
            // 
            // TSMnall
            // 
            this.TSMnall.Name = "TSMnall";
            this.TSMnall.Size = new System.Drawing.Size(152, 22);
            this.TSMnall.Text = "全部";
            this.TSMnall.Click += new System.EventHandler(this.TSMnall_Click);
            // 
            // TSMnrun
            // 
            this.TSMnrun.Name = "TSMnrun";
            this.TSMnrun.Size = new System.Drawing.Size(152, 22);
            this.TSMnrun.Text = "运行";
            this.TSMnrun.Click += new System.EventHandler(this.TSMnrun_Click);
            // 
            // TSMnadd
            // 
            this.TSMnadd.Name = "TSMnadd";
            this.TSMnadd.Size = new System.Drawing.Size(152, 22);
            this.TSMnadd.Text = "添加";
            this.TSMnadd.Click += new System.EventHandler(this.TSMnadd_Click);
            // 
            // TSMnupd
            // 
            this.TSMnupd.Name = "TSMnupd";
            this.TSMnupd.Size = new System.Drawing.Size(152, 22);
            this.TSMnupd.Text = "修改";
            this.TSMnupd.Click += new System.EventHandler(this.TSMnupd_Click);
            // 
            // TSMndel
            // 
            this.TSMndel.Name = "TSMndel";
            this.TSMndel.Size = new System.Drawing.Size(152, 22);
            this.TSMndel.Text = "删除";
            this.TSMndel.Click += new System.EventHandler(this.TSMndel_Click);
            // 
            // TSMnqry
            // 
            this.TSMnqry.Name = "TSMnqry";
            this.TSMnqry.Size = new System.Drawing.Size(152, 22);
            this.TSMnqry.Text = "查询";
            this.TSMnqry.Click += new System.EventHandler(this.TSMnqry_Click);
            // 
            // TSMnprn
            // 
            this.TSMnprn.Name = "TSMnprn";
            this.TSMnprn.Size = new System.Drawing.Size(152, 22);
            this.TSMnprn.Text = "打印";
            this.TSMnprn.Click += new System.EventHandler(this.TSMnprn_Click);
            // 
            // 发送ToolStripMenuItem1
            // 
            this.发送ToolStripMenuItem1.Name = "发送ToolStripMenuItem1";
            this.发送ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.发送ToolStripMenuItem1.Text = "发送";
            this.发送ToolStripMenuItem1.Visible = false;
            this.发送ToolStripMenuItem1.Click += new System.EventHandler(this.发送ToolStripMenuItem1_Click);
            // 
            // 审查ToolStripMenuItem1
            // 
            this.审查ToolStripMenuItem1.Name = "审查ToolStripMenuItem1";
            this.审查ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.审查ToolStripMenuItem1.Text = "审核";
            this.审查ToolStripMenuItem1.Visible = false;
            this.审查ToolStripMenuItem1.Click += new System.EventHandler(this.审查ToolStripMenuItem1_Click);
            // 
            // 审批ToolStripMenuItem1
            // 
            this.审批ToolStripMenuItem1.Name = "审批ToolStripMenuItem1";
            this.审批ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.审批ToolStripMenuItem1.Text = "审批";
            this.审批ToolStripMenuItem1.Visible = false;
            this.审批ToolStripMenuItem1.Click += new System.EventHandler(this.审批ToolStripMenuItem1_Click);
            // 
            // ProgName
            // 
            this.ProgName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProgName.DataPropertyName = "progname";
            this.ProgName.HeaderText = "模块名称";
            this.ProgName.MinimumWidth = 100;
            this.ProgName.Name = "ProgName";
            this.ProgName.ReadOnly = true;
            // 
            // run
            // 
            this.run.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.run.DataPropertyName = "run";
            this.run.FalseValue = "0";
            this.run.HeaderText = "运行";
            this.run.Name = "run";
            this.run.TrueValue = "1";
            // 
            // ins
            // 
            this.ins.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ins.DataPropertyName = "ins";
            this.ins.FalseValue = "0";
            this.ins.HeaderText = "添加";
            this.ins.Name = "ins";
            this.ins.TrueValue = "1";
            // 
            // 修改
            // 
            this.修改.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.修改.DataPropertyName = "upd";
            this.修改.FalseValue = "0";
            this.修改.HeaderText = "修改";
            this.修改.Name = "修改";
            this.修改.TrueValue = "1";
            // 
            // del
            // 
            this.del.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.del.DataPropertyName = "del";
            this.del.FalseValue = "0";
            this.del.HeaderText = "删除";
            this.del.Name = "del";
            this.del.TrueValue = "1";
            // 
            // qry
            // 
            this.qry.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.qry.DataPropertyName = "qry";
            this.qry.FalseValue = "0";
            this.qry.HeaderText = "查询";
            this.qry.Name = "qry";
            this.qry.TrueValue = "1";
            // 
            // prn
            // 
            this.prn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.prn.DataPropertyName = "prn";
            this.prn.FalseValue = "0";
            this.prn.HeaderText = "打印";
            this.prn.Name = "prn";
            this.prn.TrueValue = "1";
            // 
            // send
            // 
            this.send.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.send.DataPropertyName = "send";
            this.send.FalseValue = "0";
            this.send.HeaderText = "发送";
            this.send.Name = "send";
            this.send.TrueValue = "1";
            this.send.Visible = false;
            // 
            // exam
            // 
            this.exam.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.exam.DataPropertyName = "exam";
            this.exam.FalseValue = "0";
            this.exam.HeaderText = "审核";
            this.exam.Name = "exam";
            this.exam.TrueValue = "1";
            this.exam.Visible = false;
            // 
            // pass
            // 
            this.pass.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.pass.DataPropertyName = "pass";
            this.pass.FalseValue = "0";
            this.pass.HeaderText = "审批";
            this.pass.Name = "pass";
            this.pass.TrueValue = "1";
            this.pass.Visible = false;
            // 
            // GroupRightsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "GroupRightsList";
            this.Size = new System.Drawing.Size(567, 281);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMrun;
        private System.Windows.Forms.ToolStripMenuItem TSMadd;
        private System.Windows.Forms.ToolStripMenuItem TSMupd;
        private System.Windows.Forms.ToolStripMenuItem TSMdel;
        private System.Windows.Forms.ToolStripMenuItem TSMqry;
        private System.Windows.Forms.ToolStripMenuItem TSMprn;
        private System.Windows.Forms.ToolStripMenuItem TSMall;
        private System.Windows.Forms.ToolStripMenuItem 取消ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMnrun;
        private System.Windows.Forms.ToolStripMenuItem TSMnadd;
        private System.Windows.Forms.ToolStripMenuItem TSMnupd;
        private System.Windows.Forms.ToolStripMenuItem TSMndel;
        private System.Windows.Forms.ToolStripMenuItem TSMnqry;
        private System.Windows.Forms.ToolStripMenuItem TSMnprn;
        private System.Windows.Forms.ToolStripMenuItem TSMnall;
        private System.Windows.Forms.ToolStripMenuItem 发送ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 审查ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 审批ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 发送ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 审查ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 审批ToolStripMenuItem1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProgName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn run;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ins;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 修改;
        private System.Windows.Forms.DataGridViewCheckBoxColumn del;
        private System.Windows.Forms.DataGridViewCheckBoxColumn qry;
        private System.Windows.Forms.DataGridViewCheckBoxColumn prn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn send;
        private System.Windows.Forms.DataGridViewCheckBoxColumn exam;
        private System.Windows.Forms.DataGridViewCheckBoxColumn pass;

    }
}
