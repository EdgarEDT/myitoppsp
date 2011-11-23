namespace Itop.Client.Layouts
{
    partial class FrmGHBGdata
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCanser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cobGHBGYear = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(13, 61);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCanser
            // 
            this.btnCanser.Location = new System.Drawing.Point(108, 61);
            this.btnCanser.Name = "btnCanser";
            this.btnCanser.Size = new System.Drawing.Size(75, 23);
            this.btnCanser.TabIndex = 1;
            this.btnCanser.Text = "取消";
            this.btnCanser.UseVisualStyleBackColor = true;
            this.btnCanser.Click += new System.EventHandler(this.btnCanser_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择规划报告年份：";
            // 
            // cobGHBGYear
            // 
            this.cobGHBGYear.FormattingEnabled = true;
            this.cobGHBGYear.Location = new System.Drawing.Point(131, 18);
            this.cobGHBGYear.Name = "cobGHBGYear";
            this.cobGHBGYear.Size = new System.Drawing.Size(72, 20);
            this.cobGHBGYear.TabIndex = 3;
            this.cobGHBGYear.SelectedValueChanged += new System.EventHandler(this.cobGHBGYear_SelectedValueChanged);
            // 
            // FrmGHBGdata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 92);
            this.Controls.Add(this.cobGHBGYear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCanser);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGHBGdata";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "规划报告更新年份设置";
            this.Load += new System.EventHandler(this.FrmGHBGdata_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCanser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cobGHBGYear;
    }
}