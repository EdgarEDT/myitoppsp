namespace Itop.JournalSheet.From
{
    partial class FormWait
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
            this.prcBar = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // prcBar
            // 
            this.prcBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prcBar.Location = new System.Drawing.Point(0, 0);
            this.prcBar.Name = "prcBar";
            this.prcBar.Size = new System.Drawing.Size(292, 20);
            this.prcBar.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormWait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 20);
            this.ControlBox = false;
            this.Controls.Add(this.prcBar);
            this.Name = "FormWait";
            this.Text = "正在更新请稍等...";
            this.Load += new System.EventHandler(this.FormWait_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar prcBar;
        private System.Windows.Forms.Timer timer1;
    }
}