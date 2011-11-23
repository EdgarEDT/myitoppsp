namespace ItopVector.Tools
{
    partial class frmGlebeTypeList
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
            this.ctrlglebeType11 = new ItopVector.Tools.CtrlglebeType1();
            this.SuspendLayout();
            // 
            // ctrlglebeType11
            // 
            this.ctrlglebeType11.AllowUpdate = true;
            this.ctrlglebeType11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlglebeType11.Location = new System.Drawing.Point(0, 0);
            this.ctrlglebeType11.Name = "ctrlglebeType11";
            this.ctrlglebeType11.Size = new System.Drawing.Size(540, 401);
            this.ctrlglebeType11.TabIndex = 0;
            this.ctrlglebeType11.Load += new System.EventHandler(this.ctrlglebeType11_Load);
            // 
            // frmGlebeTypeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 401);
            this.Controls.Add(this.ctrlglebeType11);
            this.Name = "frmGlebeTypeList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图例";
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlglebeType1 ctrlglebeType11;
    }
}