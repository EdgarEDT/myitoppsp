namespace Itop.DLGH
{
    partial class frmglebeType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmglebeType));
            this.ctrlglebeType1 = new Itop.DLGH.CtrlglebeType();
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
            // ctrlglebeType1
            // 
            this.ctrlglebeType1.AllowUpdate = true;
            this.ctrlglebeType1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlglebeType1.Location = new System.Drawing.Point(0, 34);
            this.ctrlglebeType1.Name = "ctrlglebeType1";
            this.ctrlglebeType1.Size = new System.Drawing.Size(708, 385);
            this.ctrlglebeType1.TabIndex = 4;
            // 
            // frmglebeType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 419);
            this.Controls.Add(this.ctrlglebeType1);
            this.Name = "frmglebeType";
            this.Text = "frmglebeType";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmglebeType_Load);
            this.Controls.SetChildIndex(this.ctrlglebeType1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlglebeType ctrlglebeType1;

    }
}