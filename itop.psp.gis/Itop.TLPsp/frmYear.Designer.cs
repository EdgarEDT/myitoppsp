﻿namespace Itop.TLPsp
{
    partial class frmYear
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
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(13, 43);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(169, 29);
            this.simpleButton2.TabIndex = 22;
            this.simpleButton2.Text = "中期网架年份";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(13, 7);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(169, 29);
            this.simpleButton1.TabIndex = 21;
            this.simpleButton1.Text = "近期网架年份";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(13, 80);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(169, 29);
            this.simpleButton3.TabIndex = 23;
            this.simpleButton3.Text = "远期网架年份";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // frmYear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 116);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmYear";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "网架年份设定";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
    }
}