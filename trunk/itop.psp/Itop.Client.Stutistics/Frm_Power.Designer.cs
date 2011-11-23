namespace Itop.Client.Stutistics
{
    partial class Frm_Power
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Power));
            this.ctrlPs_Power1 = new Itop.Client.Stutistics.CtrlPs_Power();
            this.SuspendLayout();
            // 
            // il
            // 
            this.il.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.il.ImageSize = new System.Drawing.Size(24, 24);
            this.il.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("il.ImageStream")));
            this.il.Images.SetKeyName(0, "");
            this.il.Images.SetKeyName(1, "");
            this.il.Images.SetKeyName(2, "");
            this.il.Images.SetKeyName(3, "");
            this.il.Images.SetKeyName(4, "");
            // 
            // ctrlPs_Power1
            // 
            this.ctrlPs_Power1.AllowUpdate = true;
            this.ctrlPs_Power1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlPs_Power1.Location = new System.Drawing.Point(0, 34);
            this.ctrlPs_Power1.Name = "ctrlPs_Power1";
            this.ctrlPs_Power1.Size = new System.Drawing.Size(640, 416);
            this.ctrlPs_Power1.TabIndex = 4;
            this.ctrlPs_Power1.TYPE = "";
            // 
            // Frm_Power
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 450);
            this.Controls.Add(this.ctrlPs_Power1);
            this.Name = "Frm_Power";
            this.Text = "Frm_Power";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_Power_Load);
            this.Controls.SetChildIndex(this.ctrlPs_Power1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlPs_Power ctrlPs_Power1;
    }
}