namespace Itop.Client.Layouts
{
    partial class FrmEditBookMark
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
            this.btnCanser = new DevExpress.XtraEditors.SimpleButton();
            this.btnAddMark = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "书签名称：";
            // 
            // txtMarkName
            // 
            this.txtMarkName.Location = new System.Drawing.Point(103, 22);
            this.txtMarkName.Name = "txtMarkName";
            this.txtMarkName.Size = new System.Drawing.Size(182, 22);
            this.txtMarkName.TabIndex = 2;
            // 
            // txtMarkDisc
            // 
            this.txtMarkDisc.Location = new System.Drawing.Point(103, 61);
            this.txtMarkDisc.Name = "txtMarkDisc";
            this.txtMarkDisc.Size = new System.Drawing.Size(182, 22);
            this.txtMarkDisc.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "书签描述：";
            // 
            // btnCanser
            // 
            this.btnCanser.Location = new System.Drawing.Point(188, 107);
            this.btnCanser.Name = "btnCanser";
            this.btnCanser.Size = new System.Drawing.Size(69, 23);
            this.btnCanser.TabIndex = 17;
            this.btnCanser.Text = "取 消";
            this.btnCanser.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btnAddMark
            // 
            this.btnAddMark.Location = new System.Drawing.Point(99, 107);
            this.btnAddMark.Name = "btnAddMark";
            this.btnAddMark.Size = new System.Drawing.Size(69, 23);
            this.btnAddMark.TabIndex = 16;
            this.btnAddMark.Text = "修 改";
            this.btnAddMark.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // FrmEditBookMark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 155);
            this.Controls.Add(this.btnCanser);
            this.Controls.Add(this.btnAddMark);
            this.Controls.Add(this.txtMarkDisc);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMarkName);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEditBookMark";
            this.Text = "修改书签标记";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmEditBookMark_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMarkName;
        private System.Windows.Forms.TextBox txtMarkDisc;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnCanser;
        private DevExpress.XtraEditors.SimpleButton btnAddMark;
    }
}