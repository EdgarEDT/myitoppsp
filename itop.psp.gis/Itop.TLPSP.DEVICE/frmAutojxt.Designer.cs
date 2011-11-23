namespace Itop.TLPSP.DEVICE
{
    partial class frmAutojxt
    {
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.ucGraph1 = new Itop.TLPSP.DEVICE.UCGraph();
            this.SuspendLayout();
            // 
            // ucGraph1
            // 
            this.ucGraph1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGraph1.Location = new System.Drawing.Point(0, 0);
            this.ucGraph1.Name = "ucGraph1";
            this.ucGraph1.Showdeep = false;
            this.ucGraph1.ShowSave = false;
            this.ucGraph1.Showsbtop = true;
            this.ucGraph1.Size = new System.Drawing.Size(685, 394);
            this.ucGraph1.TabIndex = 1;
            // 
            // frmAutojxt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 394);
            this.Controls.Add(this.ucGraph1);
            this.Name = "frmAutojxt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统结线图";
            this.ResumeLayout(false);

        }

        #endregion

        public UCGraph ucGraph1;
    }
}