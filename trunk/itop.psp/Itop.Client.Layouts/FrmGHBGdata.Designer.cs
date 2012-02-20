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
            this.label1 = new System.Windows.Forms.Label();
            this.cobGHBGYear = new System.Windows.Forms.ComboBox();
            this.btnCanser = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "选择规划报告年份：";
            // 
            // cobGHBGYear
            // 
            this.cobGHBGYear.FormattingEnabled = true;
            this.cobGHBGYear.Location = new System.Drawing.Point(153, 21);
            this.cobGHBGYear.Name = "cobGHBGYear";
            this.cobGHBGYear.Size = new System.Drawing.Size(83, 22);
            this.cobGHBGYear.TabIndex = 3;
            this.cobGHBGYear.SelectedValueChanged += new System.EventHandler(this.cobGHBGYear_SelectedValueChanged);
            // 
            // btnCanser
            // 
            this.btnCanser.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCanser.Location = new System.Drawing.Point(140, 72);
            this.btnCanser.Name = "btnCanser";
            this.btnCanser.Size = new System.Drawing.Size(62, 23);
            this.btnCanser.TabIndex = 17;
            this.btnCanser.Text = "取消";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(48, 72);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(62, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "确定";
            // 
            // FrmGHBGdata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 107);
            this.Controls.Add(this.btnCanser);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cobGHBGYear);
            this.Controls.Add(this.label1);
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

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cobGHBGYear;
        private DevExpress.XtraEditors.SimpleButton btnCanser;
        private DevExpress.XtraEditors.SimpleButton btnOK;
    }
}