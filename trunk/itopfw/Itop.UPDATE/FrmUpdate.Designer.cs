﻿namespace Itop.UPDATE {
    partial class FrmUpdate {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new Itop.UPDATE.XpProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btSetup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(268, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "开始升级";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.ColorBackGround = System.Drawing.Color.White;
            this.progressBar1.ColorBarBorder = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(240)))), ((int)(((byte)(170)))));
            this.progressBar1.ColorBarCenter = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(150)))), ((int)(((byte)(10)))));
            this.progressBar1.ColorText = System.Drawing.Color.Black;
            this.progressBar1.Location = new System.Drawing.Point(25, 40);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Position = 0;
            this.progressBar1.PositionMax = 100;
            this.progressBar1.PositionMin = 0;
            this.progressBar1.Size = new System.Drawing.Size(318, 21);
            this.progressBar1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 3;
            // 
            // btSetup
            // 
            this.btSetup.Location = new System.Drawing.Point(25, 84);
            this.btSetup.Name = "btSetup";
            this.btSetup.Size = new System.Drawing.Size(75, 23);
            this.btSetup.TabIndex = 1;
            this.btSetup.Text = "服务设置";
            this.btSetup.UseVisualStyleBackColor = true;
            this.btSetup.Click += new System.EventHandler(this.btSetup_Click);
            // 
            // FrmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 127);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btSetup);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.Name = "FrmUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智能升级";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private Itop.UPDATE.XpProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btSetup;
    }
}

