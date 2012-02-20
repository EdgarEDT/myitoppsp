namespace ItopVector.Tools
{
    partial class frmPlanList
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
            this.ctrlPSP_PlanList1 = new ItopVector.Tools.CtrlPSP_PlanList();
            this.button4 = new DevExpress.XtraEditors.SimpleButton();
            this.button5 = new DevExpress.XtraEditors.SimpleButton();
            this.button1 = new DevExpress.XtraEditors.SimpleButton();
            this.button2 = new DevExpress.XtraEditors.SimpleButton();
            this.button3 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // ctrlPSP_PlanList1
            // 
            this.ctrlPSP_PlanList1.AllowUpdate = true;
            this.ctrlPSP_PlanList1.Location = new System.Drawing.Point(-1, 0);
            this.ctrlPSP_PlanList1.Name = "ctrlPSP_PlanList1";
            this.ctrlPSP_PlanList1.Size = new System.Drawing.Size(582, 266);
            this.ctrlPSP_PlanList1.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(131, 280);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(88, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "影响因素维护";
            this.button4.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(226, 280);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(88, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "线路评分";
            this.button5.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(321, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(88, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "新建线路规划";
            this.button1.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(416, 280);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "删除线路规划";
            this.button2.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(511, 280);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(65, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "确定";
            this.button3.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // frmPlanList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 315);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.ctrlPSP_PlanList1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPlanList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "线路走廊优化";
            this.Load += new System.EventHandler(this.frmPlanList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlPSP_PlanList ctrlPSP_PlanList1;
        private DevExpress.XtraEditors.SimpleButton button4;
        private DevExpress.XtraEditors.SimpleButton button5;
        private DevExpress.XtraEditors.SimpleButton button1;
        private DevExpress.XtraEditors.SimpleButton button2;
        private DevExpress.XtraEditors.SimpleButton button3;

    }
}