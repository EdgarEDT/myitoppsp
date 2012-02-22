namespace ItopVector.Tools
{
    partial class frmSubstationManager
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
            this.button1 = new DevExpress.XtraEditors.SimpleButton();
            this.button6 = new DevExpress.XtraEditors.SimpleButton();
            this.button2 = new DevExpress.XtraEditors.SimpleButton();
            this.button3 = new DevExpress.XtraEditors.SimpleButton();
            this.button4 = new DevExpress.XtraEditors.SimpleButton();
            this.button5 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(31, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(174, 27);
            this.button1.TabIndex = 1;
            this.button1.Text = "模块管理";
            this.button1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(31, 45);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(174, 27);
            this.button6.TabIndex = 2;
            this.button6.Text = "变电站选址方案管理";
            this.button6.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(31, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(174, 27);
            this.button2.TabIndex = 3;
            this.button2.Text = "站址初选";
            this.button2.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(31, 113);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(174, 27);
            this.button3.TabIndex = 4;
            this.button3.Text = "资料阅读";
            this.button3.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(31, 147);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(174, 27);
            this.button4.TabIndex = 5;
            this.button4.Text = "在线评分";
            this.button4.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(31, 181);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(174, 27);
            this.button5.TabIndex = 6;
            this.button5.Text = "优选结果";
            this.button5.Click += new System.EventHandler(this.simpleButton6_Click);
            // 
            // frmSubstationManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 236);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSubstationManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "变电站站址优选";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton button1;
        private DevExpress.XtraEditors.SimpleButton button6;
        private DevExpress.XtraEditors.SimpleButton button2;
        private DevExpress.XtraEditors.SimpleButton button3;
        private DevExpress.XtraEditors.SimpleButton button4;
        private DevExpress.XtraEditors.SimpleButton button5;
    }
}