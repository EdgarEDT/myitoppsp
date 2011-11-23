namespace Itop.Client.Layouts
{
    partial class FormExcel
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
            this.dsoExcelControl1 = new Itop.Client.Layouts.DSOExcelControl();
            this.SuspendLayout();
            // 
            // dsoExcelControl1
            // 
            this.dsoExcelControl1.IsDispalyMenuBar = true;
            this.dsoExcelControl1.IsDispalyTitleBar = true;
            this.dsoExcelControl1.IsDispalyToolBar = true;
            this.dsoExcelControl1.Location = new System.Drawing.Point(12, 12);
            this.dsoExcelControl1.Name = "dsoExcelControl1";
            this.dsoExcelControl1.Size = new System.Drawing.Size(641, 280);
            this.dsoExcelControl1.TabIndex = 0;
            // 
            // FormExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 323);
            this.Controls.Add(this.dsoExcelControl1);
            this.Name = "FormExcel";
            this.Text = "FormExcel";
            this.ResumeLayout(false);

        }

        #endregion

        private DSOExcelControl dsoExcelControl1;
    }
}