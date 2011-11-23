namespace Itop.Client.BaseData
{
    partial class FrmColorData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmColorData));
            this.ctrlBaseColor1 = new Itop.Client.BaseData.CtrlBaseColor();
            this.SuspendLayout();
            // 
            // il
            // 
            this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.il.ImageSize = new System.Drawing.Size(24, 24);
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.Images.SetKeyName(0, "");
            this.il.Images.SetKeyName(1, "修改.ico");
            this.il.Images.SetKeyName(2, "");
            this.il.Images.SetKeyName(3, "");
            this.il.Images.SetKeyName(4, "关闭.ico");
            // 
            // ctrlBaseColor1
            // 
            this.ctrlBaseColor1.AllowUpdate = true;
            this.ctrlBaseColor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlBaseColor1.Location = new System.Drawing.Point(0, 34);
            this.ctrlBaseColor1.Name = "ctrlBaseColor1";
            this.ctrlBaseColor1.Size = new System.Drawing.Size(547, 348);
            this.ctrlBaseColor1.TabIndex = 4;
            // 
            // FrmColorData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 382);
            this.Controls.Add(this.ctrlBaseColor1);
            this.Name = "FrmColorData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmColorData";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmColorData_Load);
            this.Controls.SetChildIndex(this.ctrlBaseColor1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlBaseColor ctrlBaseColor1;
    }
}