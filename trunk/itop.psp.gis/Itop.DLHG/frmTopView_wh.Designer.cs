﻿namespace Itop.DLGH
{
    partial class frmTopView_wh
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
            this.ctrlSvgView1 = new ItopVector.Tools.CtrlSvgView_wh();
            this.SuspendLayout();
            // 
            // ctrlSvgView1
            // 
            this.ctrlSvgView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlSvgView1.Location = new System.Drawing.Point(0, 0);
            this.ctrlSvgView1.Name = "ctrlSvgView1";
            this.ctrlSvgView1.Size = new System.Drawing.Size(580, 445);
            this.ctrlSvgView1.TabIndex = 0;
            // 
            // frmTopView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 445);
            this.Controls.Add(this.ctrlSvgView1);
            this.Name = "frmTopView";
            this.Text = "frmTopView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTopView_FormClosing);
            this.Load += new System.EventHandler(this.frmTopView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ItopVector.Tools.CtrlSvgView_wh ctrlSvgView1;
    }
}