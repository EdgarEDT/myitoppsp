namespace Itop.Client.Login
{
    partial class loginsetting
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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sbtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnCanser = new DevExpress.XtraEditors.SimpleButton();
            this.txtServer = new DevExpress.XtraEditors.TextEdit();
            this.txtPort = new DevExpress.XtraEditors.TextEdit();
            this.txtProtocol = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProtocol.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label5.Location = new System.Drawing.Point(24, 87);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "端  口";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4.Location = new System.Drawing.Point(24, 59);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "协  议";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label3.Location = new System.Drawing.Point(24, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "服务器";
            // 
            // sbtnOk
            // 
            this.sbtnOk.Location = new System.Drawing.Point(177, 134);
            this.sbtnOk.Name = "sbtnOk";
            this.sbtnOk.Size = new System.Drawing.Size(75, 23);
            this.sbtnOk.TabIndex = 17;
            this.sbtnOk.Text = "确定";
            this.sbtnOk.Click += new System.EventHandler(this.sbtnOk_Click);
            // 
            // sbtnCanser
            // 
            this.sbtnCanser.Location = new System.Drawing.Point(258, 134);
            this.sbtnCanser.Name = "sbtnCanser";
            this.sbtnCanser.Size = new System.Drawing.Size(75, 23);
            this.sbtnCanser.TabIndex = 17;
            this.sbtnCanser.Text = "取消";
            this.sbtnCanser.Click += new System.EventHandler(this.sbtnCanser_Click);
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(82, 25);
            this.txtServer.Name = "txtServer";
            this.txtServer.Properties.Appearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.txtServer.Properties.Appearance.Options.UseBorderColor = true;
            this.txtServer.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtServer.Size = new System.Drawing.Size(232, 21);
            this.txtServer.TabIndex = 18;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(82, 87);
            this.txtPort.Name = "txtPort";
            this.txtPort.Properties.Appearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.txtPort.Properties.Appearance.Options.UseBorderColor = true;
            this.txtPort.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPort.Size = new System.Drawing.Size(232, 21);
            this.txtPort.TabIndex = 19;
            // 
            // txtProtocol
            // 
            this.txtProtocol.Location = new System.Drawing.Point(82, 57);
            this.txtProtocol.Name = "txtProtocol";
            this.txtProtocol.Properties.Appearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.txtProtocol.Properties.Appearance.Options.UseBorderColor = true;
            this.txtProtocol.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtProtocol.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtProtocol.Properties.Items.AddRange(new object[] {
            "TCP",
            "HTTP"});
            this.txtProtocol.Size = new System.Drawing.Size(232, 21);
            this.txtProtocol.TabIndex = 20;
            // 
            // loginsetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 169);
            this.Controls.Add(this.txtProtocol);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.sbtnCanser);
            this.Controls.Add(this.sbtnOk);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "loginsetting";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.loginsetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtServer.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProtocol.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton sbtnOk;
        private DevExpress.XtraEditors.SimpleButton sbtnCanser;
        private DevExpress.XtraEditors.TextEdit txtServer;
        private DevExpress.XtraEditors.TextEdit txtPort;
        private DevExpress.XtraEditors.ComboBoxEdit txtProtocol;
    }
}