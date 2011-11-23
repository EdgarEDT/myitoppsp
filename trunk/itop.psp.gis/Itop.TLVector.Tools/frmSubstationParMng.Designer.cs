namespace ItopVector.Tools
{
    partial class frmSubstationParMng
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSubstationParMng));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ctrlPSP_SubstationPar1 = new ItopVector.Tools.CtrlPSP_SubstationPar();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ctrlPSP_SubstationPar2 = new ItopVector.Tools.CtrlPSP_SubstationPar();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 33);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(612, 330);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ctrlPSP_SubstationPar1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(604, 305);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "否决因素";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ctrlPSP_SubstationPar1
            // 
            this.ctrlPSP_SubstationPar1.AllowUpdate = true;
            this.ctrlPSP_SubstationPar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlPSP_SubstationPar1.Location = new System.Drawing.Point(3, 3);
            this.ctrlPSP_SubstationPar1.Name = "ctrlPSP_SubstationPar1";
            this.ctrlPSP_SubstationPar1.Size = new System.Drawing.Size(598, 299);
            this.ctrlPSP_SubstationPar1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ctrlPSP_SubstationPar2);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(604, 305);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "一般因素";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ctrlPSP_SubstationPar2
            // 
            this.ctrlPSP_SubstationPar2.AllowUpdate = true;
            this.ctrlPSP_SubstationPar2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlPSP_SubstationPar2.Location = new System.Drawing.Point(3, 3);
            this.ctrlPSP_SubstationPar2.Name = "ctrlPSP_SubstationPar2";
            this.ctrlPSP_SubstationPar2.Size = new System.Drawing.Size(598, 299);
            this.ctrlPSP_SubstationPar2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.checkBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(604, 305);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "评分标准";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(29, 23);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(204, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "去除每种候选站址的最高和最低分";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // frmSubstationParMng
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 363);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmSubstationParMng";
            this.Text = "评分因素维护";
            this.Load += new System.EventHandler(this.frmSubstationParMng_Load);
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private CtrlPSP_SubstationPar ctrlPSP_SubstationPar2;
        private CtrlPSP_SubstationPar ctrlPSP_SubstationPar1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}