namespace Itop.Client.Forecast
{
    partial class ForecastMaxHourSet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbhourone = new System.Windows.Forms.RadioButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbfhone = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbdltwo = new System.Windows.Forms.RadioButton();
            this.rbdlone = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.simpleButton6);
            this.groupControl1.Controls.Add(this.groupBox3);
            this.groupControl1.Controls.Add(this.simpleButton5);
            this.groupControl1.Controls.Add(this.groupBox2);
            this.groupControl1.Controls.Add(this.groupBox1);
            this.groupControl1.Location = new System.Drawing.Point(0, 1);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(263, 305);
            this.groupControl1.TabIndex = 0;
            // 
            // simpleButton6
            // 
            this.simpleButton6.Location = new System.Drawing.Point(149, 256);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(76, 26);
            this.simpleButton6.TabIndex = 5;
            this.simpleButton6.Text = "取消";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbhourone);
            this.groupBox3.Location = new System.Drawing.Point(33, 182);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(192, 68);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "最大小时数";
            // 
            // rbhourone
            // 
            this.rbhourone.AutoSize = true;
            this.rbhourone.Checked = true;
            this.rbhourone.Location = new System.Drawing.Point(20, 20);
            this.rbhourone.Name = "rbhourone";
            this.rbhourone.Size = new System.Drawing.Size(47, 16);
            this.rbhourone.TabIndex = 0;
            this.rbhourone.TabStop = true;
            this.rbhourone.Text = "小时";
            this.rbhourone.UseVisualStyleBackColor = true;
            // 
            // simpleButton5
            // 
            this.simpleButton5.Location = new System.Drawing.Point(63, 256);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(76, 26);
            this.simpleButton5.TabIndex = 4;
            this.simpleButton5.Text = "确定";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbfhone);
            this.groupBox2.Location = new System.Drawing.Point(33, 108);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(192, 68);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "负荷单位";
            // 
            // tbfhone
            // 
            this.tbfhone.AutoSize = true;
            this.tbfhone.Checked = true;
            this.tbfhone.Location = new System.Drawing.Point(20, 20);
            this.tbfhone.Name = "tbfhone";
            this.tbfhone.Size = new System.Drawing.Size(35, 16);
            this.tbfhone.TabIndex = 0;
            this.tbfhone.TabStop = true;
            this.tbfhone.Text = "MW";
            this.tbfhone.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbdltwo);
            this.groupBox1.Controls.Add(this.rbdlone);
            this.groupBox1.Location = new System.Drawing.Point(33, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用电量单位";
            // 
            // rbdltwo
            // 
            this.rbdltwo.AutoSize = true;
            this.rbdltwo.Location = new System.Drawing.Point(20, 42);
            this.rbdltwo.Name = "rbdltwo";
            this.rbdltwo.Size = new System.Drawing.Size(53, 16);
            this.rbdltwo.TabIndex = 1;
            this.rbdltwo.TabStop = true;
            this.rbdltwo.Text = "万kWh";
            this.rbdltwo.UseVisualStyleBackColor = true;
            this.rbdltwo.CheckedChanged += new System.EventHandler(this.rbdltwo_CheckedChanged);
            // 
            // rbdlone
            // 
            this.rbdlone.AutoSize = true;
            this.rbdlone.Location = new System.Drawing.Point(20, 20);
            this.rbdlone.Name = "rbdlone";
            this.rbdlone.Size = new System.Drawing.Size(53, 16);
            this.rbdlone.TabIndex = 0;
            this.rbdlone.TabStop = true;
            this.rbdlone.Text = "亿kWh";
            this.rbdlone.UseVisualStyleBackColor = true;
            this.rbdlone.CheckedChanged += new System.EventHandler(this.rbdlone_CheckedChanged);
            // 
            // ForecastMaxHourSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 292);
            this.Controls.Add(this.groupControl1);
            this.Name = "ForecastMaxHourSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "最大小时数-计算单位设置";
            this.Load += new System.EventHandler(this.ForecastMaxHourSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbdlone;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbhourone;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton tbfhone;
        private System.Windows.Forms.RadioButton rbdltwo;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;


    }
}