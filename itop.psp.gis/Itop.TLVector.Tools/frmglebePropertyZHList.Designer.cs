namespace ItopVector.Tools
{
    partial class frmglebePropertyZHList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmglebePropertyList));
            this.ctrlglebeProperty1 = new ItopVector.Tools.CtrlglebePropertyZH();
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
            // ctrlglebeProperty1
            // 
            this.ctrlglebeProperty1.AllowUpdate = true;
            this.ctrlglebeProperty1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlglebeProperty1.Location = new System.Drawing.Point(0, 34);
            this.ctrlglebeProperty1.Name = "ctrlglebeProperty1";
            this.ctrlglebeProperty1.Size = new System.Drawing.Size(544, 342);
            this.ctrlglebeProperty1.TabIndex = 0;
            // 
            // frmglebePropertyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 376);
            this.Controls.Add(this.ctrlglebeProperty1);
            this.Name = "frmglebePropertyList";
            this.Text = "";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmglebePropertyList_Load);
            this.Controls.SetChildIndex(this.ctrlglebeProperty1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlglebePropertyZH ctrlglebeProperty1;

    }
}