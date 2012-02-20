namespace Itop.Client.Layouts
{
    partial class FrmAddBookMark
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
            this.txtMarkName = new System.Windows.Forms.TextBox();
            this.txtMarkDisc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chbType = new System.Windows.Forms.CheckBox();
            this.btnCanser = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddMark = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "书签名称：";
            // 
            // txtMarkName
            // 
            this.txtMarkName.Location = new System.Drawing.Point(97, 10);
            this.txtMarkName.Name = "txtMarkName";
            this.txtMarkName.Size = new System.Drawing.Size(182, 22);
            this.txtMarkName.TabIndex = 2;
            // 
            // txtMarkDisc
            // 
            this.txtMarkDisc.Location = new System.Drawing.Point(97, 49);
            this.txtMarkDisc.Name = "txtMarkDisc";
            this.txtMarkDisc.Size = new System.Drawing.Size(182, 22);
            this.txtMarkDisc.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "书签描述：";
            // 
            // chbType
            // 
            this.chbType.AutoSize = true;
            this.chbType.Location = new System.Drawing.Point(16, 80);
            this.chbType.Name = "chbType";
            this.chbType.Size = new System.Drawing.Size(62, 18);
            this.chbType.TabIndex = 8;
            this.chbType.Text = "程序员";
            this.chbType.UseVisualStyleBackColor = true;
            this.chbType.Visible = false;
            // 
            // btnCanser
            // 
            this.btnCanser.Location = new System.Drawing.Point(195, 97);
            this.btnCanser.Name = "btnCanser";
            this.btnCanser.Size = new System.Drawing.Size(62, 23);
            this.btnCanser.TabIndex = 15;
            this.btnCanser.Text = "取 消";
            this.btnCanser.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnAddMark
            // 
            this.btnAddMark.Location = new System.Drawing.Point(106, 97);
            this.btnAddMark.Name = "btnAddMark";
            this.btnAddMark.Size = new System.Drawing.Size(62, 23);
            this.btnAddMark.TabIndex = 14;
            this.btnAddMark.Text = "添 加";
            this.btnAddMark.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // FrmAddBookMark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 150);
            this.Controls.Add(this.btnCanser);
            this.Controls.Add(this.btnAddMark);
            this.Controls.Add(this.chbType);
            this.Controls.Add(this.txtMarkDisc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMarkName);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAddBookMark";
            this.Text = "添加书签标记";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmAddBookMark_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMarkName;
        private System.Windows.Forms.TextBox txtMarkDisc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chbType;
        private DevExpress.XtraEditors.SimpleButton btnCanser;
        private DevExpress.XtraEditors.SimpleButton btnAddMark;
    }
}