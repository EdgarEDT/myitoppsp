namespace Itop.Client
{
    partial class UserBar
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserBar));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.labMin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "缩小无底色.png");
            this.imageList1.Images.SetKeyName(1, "缩小.png");
            this.imageList1.Images.SetKeyName(2, "无底色放大.png");
            this.imageList1.Images.SetKeyName(3, "放大.png");
            this.imageList1.Images.SetKeyName(4, "X无底色.png");
            this.imageList1.Images.SetKeyName(5, "X.png");
            // 
            // labMin
            // 
            this.labMin.BackColor = System.Drawing.Color.Transparent;
            this.labMin.ImageIndex = 3;
            this.labMin.ImageList = this.imageList1;
            this.labMin.Location = new System.Drawing.Point(0, -1);
            this.labMin.Name = "labMin";
            this.labMin.Size = new System.Drawing.Size(32, 20);
            this.labMin.TabIndex = 9;
            this.labMin.Text = "   ";
            this.labMin.MouseLeave += new System.EventHandler(this.labMin_MouseLeave);
            this.labMin.Click += new System.EventHandler(this.labMin_Click);
            this.labMin.MouseEnter += new System.EventHandler(this.labMin_MouseEnter);
            // 
            // UserBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.labMin);
            this.Name = "UserBar";
            this.Size = new System.Drawing.Size(32, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label labMin;

    }
}
