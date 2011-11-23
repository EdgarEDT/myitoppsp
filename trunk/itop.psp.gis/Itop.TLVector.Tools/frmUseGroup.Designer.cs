namespace ItopVector.Tools
{
    partial class frmUseGroup
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.ctrlUseGroup1 = new ItopVector.Tools.CtrlUseGroup();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(5, 2);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(54, 23);
            this.simpleButton1.TabIndex = 14;
            this.simpleButton1.Text = "删除";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(62, 2);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(54, 23);
            this.simpleButton2.TabIndex = 14;
            this.simpleButton2.Text = "确定";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // ctrlUseGroup1
            // 
            this.ctrlUseGroup1.AllowUpdate = true;
            this.ctrlUseGroup1.Location = new System.Drawing.Point(0, 29);
            this.ctrlUseGroup1.Name = "ctrlUseGroup1";
            this.ctrlUseGroup1.Size = new System.Drawing.Size(534, 312);
            this.ctrlUseGroup1.TabIndex = 4;
            this.ctrlUseGroup1.DoubleClick += new System.EventHandler(this.ctrlUseGroup1_DoubleClick);
            // 
            // frmUseGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 341);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.ctrlUseGroup1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUseGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图元组";
            this.Load += new System.EventHandler(this.frmUseGroup_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlUseGroup ctrlUseGroup1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}