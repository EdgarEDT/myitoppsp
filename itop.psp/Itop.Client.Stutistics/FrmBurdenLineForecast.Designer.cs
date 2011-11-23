namespace Itop.Client.Stutistics
{
    partial class FrmBurdenLineForecast
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBurdenLineForecast));
            this.ctrlBurdenLineForecast1 = new Itop.Client.Stutistics.CtrlBurdenLineForecast();
            this.barBurdenLineForecast = new DevExpress.XtraBars.BarButtonItem();
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
            this.il.Images.SetKeyName(5, "布局.ico");
            // 
            // ctrlBurdenLineForecast1
            // 
            this.ctrlBurdenLineForecast1.AllowUpdate = true;
            this.ctrlBurdenLineForecast1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlBurdenLineForecast1.Location = new System.Drawing.Point(0, 33);
            this.ctrlBurdenLineForecast1.Name = "ctrlBurdenLineForecast1";
            this.ctrlBurdenLineForecast1.Size = new System.Drawing.Size(573, 408);
            this.ctrlBurdenLineForecast1.TabIndex = 4;
            // 
            // barBurdenLineForecast
            // 
            this.barBurdenLineForecast.Caption = "典型日负荷曲线预测表";
            this.barBurdenLineForecast.Id = 7;
            this.barBurdenLineForecast.Name = "barBurdenLineForecast";
            this.barBurdenLineForecast.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBurdenLineForecast_ItemClick);
            // 
            // FrmBurdenLineForecast
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 441);
            this.Controls.Add(this.ctrlBurdenLineForecast1);
            this.Name = "FrmBurdenLineForecast";
            this.Text = "负荷曲线预测";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmBurdenLineForecast_Load);
            this.Controls.SetChildIndex(this.ctrlBurdenLineForecast1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlBurdenLineForecast ctrlBurdenLineForecast1;
        private DevExpress.XtraBars.BarButtonItem barBurdenLineForecast;
    }
}