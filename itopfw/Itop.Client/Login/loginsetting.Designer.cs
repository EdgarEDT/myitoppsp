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
            this.combCity = new DevExpress.XtraEditors.LookUpEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefreshCity = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtServer.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtProtocol.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combCity.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label5.Location = new System.Drawing.Point(28, 101);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 14);
            this.label5.TabIndex = 14;
            this.label5.Text = "端  口:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label4.Location = new System.Drawing.Point(28, 69);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 14);
            this.label4.TabIndex = 13;
            this.label4.Text = "协  议:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label3.Location = new System.Drawing.Point(28, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 14);
            this.label3.TabIndex = 15;
            this.label3.Text = "服务器:";
            // 
            // sbtnOk
            // 
            this.sbtnOk.Location = new System.Drawing.Point(195, 193);
            this.sbtnOk.Name = "sbtnOk";
            this.sbtnOk.Size = new System.Drawing.Size(87, 27);
            this.sbtnOk.TabIndex = 17;
            this.sbtnOk.Text = "确定";
            this.sbtnOk.Click += new System.EventHandler(this.sbtnOk_Click);
            // 
            // sbtnCanser
            // 
            this.sbtnCanser.Location = new System.Drawing.Point(290, 193);
            this.sbtnCanser.Name = "sbtnCanser";
            this.sbtnCanser.Size = new System.Drawing.Size(87, 27);
            this.sbtnCanser.TabIndex = 17;
            this.sbtnCanser.Text = "取消";
            this.sbtnCanser.Click += new System.EventHandler(this.sbtnCanser_Click);
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(96, 29);
            this.txtServer.Name = "txtServer";
            this.txtServer.Properties.Appearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.txtServer.Properties.Appearance.Options.UseBorderColor = true;
            this.txtServer.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.txtServer.Size = new System.Drawing.Size(271, 23);
            this.txtServer.TabIndex = 18;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(96, 101);
            this.txtPort.Name = "txtPort";
            this.txtPort.Properties.Appearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.txtPort.Properties.Appearance.Options.UseBorderColor = true;
            this.txtPort.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.txtPort.Size = new System.Drawing.Size(271, 23);
            this.txtPort.TabIndex = 19;
            // 
            // txtProtocol
            // 
            this.txtProtocol.Location = new System.Drawing.Point(96, 66);
            this.txtProtocol.Name = "txtProtocol";
            this.txtProtocol.Properties.Appearance.BorderColor = System.Drawing.SystemColors.Desktop;
            this.txtProtocol.Properties.Appearance.Options.UseBorderColor = true;
            this.txtProtocol.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.txtProtocol.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtProtocol.Properties.Items.AddRange(new object[] {
            "TCP",
            "HTTP"});
            this.txtProtocol.Size = new System.Drawing.Size(271, 23);
            this.txtProtocol.TabIndex = 20;
            // 
            // combCity
            // 
            this.combCity.Location = new System.Drawing.Point(96, 140);
            this.combCity.Name = "combCity";
            this.combCity.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combCity.Size = new System.Drawing.Size(227, 21);
            this.combCity.TabIndex = 39;
            this.combCity.EditValueChanged += new System.EventHandler(this.combCity_EditValueChanged);
       
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label1.Location = new System.Drawing.Point(28, 143);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 14);
            this.label1.TabIndex = 40;
            this.label1.Text = "城  市:";
            // 
            // btnRefreshCity
            // 
            this.btnRefreshCity.Location = new System.Drawing.Point(329, 141);
            this.btnRefreshCity.Name = "btnRefreshCity";
            this.btnRefreshCity.Size = new System.Drawing.Size(38, 23);
            this.btnRefreshCity.TabIndex = 41;
            this.btnRefreshCity.Text = "更新";
            this.btnRefreshCity.Click += new System.EventHandler(this.btnRefreshCity_Click);
            // 
            // loginsetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 232);
            this.Controls.Add(this.btnRefreshCity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.combCity);
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
            ((System.ComponentModel.ISupportInitialize)(this.combCity.Properties)).EndInit();
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
        private DevExpress.XtraEditors.LookUpEdit combCity;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnRefreshCity;
    }
}