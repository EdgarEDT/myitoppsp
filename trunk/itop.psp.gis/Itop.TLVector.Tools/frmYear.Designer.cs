namespace ItopVector.Tools
{
    partial class frmYear
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
            this.button2 = new DevExpress.XtraEditors.SimpleButton();
            this.button3 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "近期网架年份";
            this.button1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(23, 61);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "中期网架年份";
            this.button2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(23, 95);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(139, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "远期网架年份";
            this.button3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // frmYear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 136);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmYear";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "网架年份设定";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton button1;
        private DevExpress.XtraEditors.SimpleButton button2;
        private DevExpress.XtraEditors.SimpleButton button3;
    }
}