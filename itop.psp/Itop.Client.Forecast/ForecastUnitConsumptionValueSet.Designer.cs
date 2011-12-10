namespace Itop.Client.Forecast
{
    partial class ForecastUnitConsumptionValueSet
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
            this.rbdhtwo = new System.Windows.Forms.RadioButton();
            this.rbdhone = new System.Windows.Forms.RadioButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbdlone = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbgpdone = new System.Windows.Forms.RadioButton();
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
            this.groupControl1.Size = new System.Drawing.Size(263, 267);
            this.groupControl1.TabIndex = 0;
            // 
            // simpleButton6
            // 
            this.simpleButton6.Location = new System.Drawing.Point(173, 224);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(76, 26);
            this.simpleButton6.TabIndex = 5;
            this.simpleButton6.Text = "取消";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbdhtwo);
            this.groupBox3.Controls.Add(this.rbdhone);
            this.groupBox3.Location = new System.Drawing.Point(33, 145);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(192, 68);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "单耗";
            // 
            // rbdhtwo
            // 
            this.rbdhtwo.Location = new System.Drawing.Point(20, 42);
            this.rbdhtwo.Name = "rbdhtwo";
            this.rbdhtwo.Size = new System.Drawing.Size(59, 16);
            this.rbdhtwo.TabIndex = 1;
            this.rbdhtwo.Text = "kWh/元";
            this.rbdhtwo.UseVisualStyleBackColor = true;
            this.rbdhtwo.CheckedChanged += new System.EventHandler(this.rbdhtwo_CheckedChanged);
            // 
            // rbdhone
            // 
            this.rbdhone.Location = new System.Drawing.Point(20, 20);
            this.rbdhone.Name = "rbdhone";
            this.rbdhone.Size = new System.Drawing.Size(59, 16);
            this.rbdhone.TabIndex = 0;
            this.rbdhone.Text = "kWh/元";
            this.rbdhone.UseVisualStyleBackColor = true;
            this.rbdhone.CheckedChanged += new System.EventHandler(this.rbdhone_CheckedChanged);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Location = new System.Drawing.Point(85, 224);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(76, 26);
            this.simpleButton5.TabIndex = 4;
            this.simpleButton5.Text = "确定";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbdlone);
            this.groupBox2.Location = new System.Drawing.Point(33, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(192, 49);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "电量单位";
            // 
            // tbdlone
            // 
            this.tbdlone.AutoSize = true;
            this.tbdlone.Checked = true;
            this.tbdlone.Location = new System.Drawing.Point(20, 20);
            this.tbdlone.Name = "tbdlone";
            this.tbdlone.Size = new System.Drawing.Size(53, 16);
            this.tbdlone.TabIndex = 0;
            this.tbdlone.TabStop = true;
            this.tbdlone.Text = "亿kWh";
            this.tbdlone.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbgpdone);
            this.groupBox1.Location = new System.Drawing.Point(33, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GDP单位";
            // 
            // rbgpdone
            // 
            this.rbgpdone.AutoSize = true;
            this.rbgpdone.Checked = true;
            this.rbgpdone.Location = new System.Drawing.Point(20, 20);
            this.rbgpdone.Name = "rbgpdone";
            this.rbgpdone.Size = new System.Drawing.Size(47, 16);
            this.rbgpdone.TabIndex = 0;
            this.rbgpdone.TabStop = true;
            this.rbgpdone.Text = "亿元";
            this.rbgpdone.UseVisualStyleBackColor = true;
        
            // 
            // ForecastUnitConsumptionValueSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 265);
            this.Controls.Add(this.groupControl1);
            this.Name = "ForecastUnitConsumptionValueSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "产值单耗法-计算单位设置";
            this.Load += new System.EventHandler(this.ForecastMaxHourSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbgpdone;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbdhone;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton tbdlone;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private System.Windows.Forms.RadioButton rbdhtwo;


    }
}