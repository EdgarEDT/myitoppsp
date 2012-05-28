namespace Itop.Client
{
    partial class FrmSysDataAdd
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
            this.txtCityDesc = new DevExpress.XtraEditors.MemoEdit();
            this.sCityWd = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.sCityJd = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtCityName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCanser = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.btnDelData = new DevExpress.XtraEditors.SimpleButton();
            this.btnCreateData = new DevExpress.XtraEditors.SimpleButton();
            this.btnConnect = new DevExpress.XtraEditors.SimpleButton();
            this.txtServerPwd = new DevExpress.XtraEditors.TextEdit();
            this.txtServerUser = new DevExpress.XtraEditors.TextEdit();
            this.txtServerName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtServerAddress = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCityDesc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCityWd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCityJd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCityName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerPwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerAddress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtCityDesc);
            this.groupControl1.Controls.Add(this.sCityWd);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.sCityJd);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtCityName);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(10, 15);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(256, 209);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "城市信息";
            // 
            // txtCityDesc
            // 
            this.txtCityDesc.Location = new System.Drawing.Point(83, 123);
            this.txtCityDesc.Name = "txtCityDesc";
            this.txtCityDesc.Size = new System.Drawing.Size(160, 67);
            this.txtCityDesc.TabIndex = 8;
            // 
            // sCityWd
            // 
            this.sCityWd.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sCityWd.Location = new System.Drawing.Point(83, 89);
            this.sCityWd.Name = "sCityWd";
            this.sCityWd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.sCityWd.Size = new System.Drawing.Size(160, 21);
            this.sCityWd.TabIndex = 7;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(16, 89);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "纬度坐标：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(16, 122);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "城市描述：";
            // 
            // sCityJd
            // 
            this.sCityJd.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.sCityJd.Location = new System.Drawing.Point(83, 58);
            this.sCityJd.Name = "sCityJd";
            this.sCityJd.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.sCityJd.Size = new System.Drawing.Size(160, 21);
            this.sCityJd.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 58);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "经度坐标：";
            // 
            // txtCityName
            // 
            this.txtCityName.Location = new System.Drawing.Point(83, 27);
            this.txtCityName.Name = "txtCityName";
            this.txtCityName.Size = new System.Drawing.Size(160, 21);
            this.txtCityName.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 29);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "城市名称：";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(352, 234);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确 定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCanser
            // 
            this.btnCanser.Location = new System.Drawing.Point(443, 234);
            this.btnCanser.Name = "btnCanser";
            this.btnCanser.Size = new System.Drawing.Size(75, 23);
            this.btnCanser.TabIndex = 3;
            this.btnCanser.Text = "取 消";
            this.btnCanser.Click += new System.EventHandler(this.btnCanser_Click);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.btnDelData);
            this.groupControl2.Controls.Add(this.btnCreateData);
            this.groupControl2.Controls.Add(this.btnConnect);
            this.groupControl2.Controls.Add(this.txtServerPwd);
            this.groupControl2.Controls.Add(this.txtServerUser);
            this.groupControl2.Controls.Add(this.txtServerName);
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.labelControl8);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.txtServerAddress);
            this.groupControl2.Controls.Add(this.labelControl7);
            this.groupControl2.Location = new System.Drawing.Point(288, 15);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(269, 209);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "数据库信息";
            // 
            // btnDelData
            // 
            this.btnDelData.Location = new System.Drawing.Point(145, 181);
            this.btnDelData.Name = "btnDelData";
            this.btnDelData.Size = new System.Drawing.Size(66, 23);
            this.btnDelData.TabIndex = 17;
            this.btnDelData.Text = "删除数据库";
            this.btnDelData.Click += new System.EventHandler(this.btnDelData_Click);
            // 
            // btnCreateData
            // 
            this.btnCreateData.Location = new System.Drawing.Point(73, 181);
            this.btnCreateData.Name = "btnCreateData";
            this.btnCreateData.Size = new System.Drawing.Size(66, 23);
            this.btnCreateData.TabIndex = 16;
            this.btnCreateData.Text = "创建数据库";
            this.btnCreateData.Click += new System.EventHandler(this.btnCreateData_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(5, 181);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(62, 23);
            this.btnConnect.TabIndex = 15;
            this.btnConnect.Text = "连接测试";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtServerPwd
            // 
            this.txtServerPwd.Location = new System.Drawing.Point(91, 89);
            this.txtServerPwd.Name = "txtServerPwd";
            this.txtServerPwd.Size = new System.Drawing.Size(160, 21);
            this.txtServerPwd.TabIndex = 14;
            // 
            // txtServerUser
            // 
            this.txtServerUser.Location = new System.Drawing.Point(91, 59);
            this.txtServerUser.Name = "txtServerUser";
            this.txtServerUser.Size = new System.Drawing.Size(160, 21);
            this.txtServerUser.TabIndex = 13;
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(91, 119);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(160, 21);
            this.txtServerName.TabIndex = 12;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(13, 120);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(72, 14);
            this.labelControl6.TabIndex = 9;
            this.labelControl6.Text = "数据库名称：";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(25, 90);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(60, 14);
            this.labelControl8.TabIndex = 11;
            this.labelControl8.Text = "登录密码：";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(13, 62);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(72, 14);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "登录用户名：";
            // 
            // txtServerAddress
            // 
            this.txtServerAddress.Location = new System.Drawing.Point(91, 32);
            this.txtServerAddress.Name = "txtServerAddress";
            this.txtServerAddress.Size = new System.Drawing.Size(160, 21);
            this.txtServerAddress.TabIndex = 8;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(13, 34);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(72, 14);
            this.labelControl7.TabIndex = 7;
            this.labelControl7.Text = "服务器名称：";
            // 
            // FrmSysDataAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 266);
            this.Controls.Add(this.btnCanser);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmSysDataAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSysDataAdd";
            this.Load += new System.EventHandler(this.FrmSysDataAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCityDesc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCityWd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sCityJd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCityName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerPwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerAddress.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SpinEdit sCityWd;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SpinEdit sCityJd;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtCityName;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtCityDesc;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCanser;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnCreateData;
        private DevExpress.XtraEditors.SimpleButton btnConnect;
        private DevExpress.XtraEditors.TextEdit txtServerPwd;
        private DevExpress.XtraEditors.TextEdit txtServerUser;
        private DevExpress.XtraEditors.TextEdit txtServerName;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit txtServerAddress;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevExpress.XtraEditors.SimpleButton btnDelData;

    }
}