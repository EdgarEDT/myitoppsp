namespace Itop.RightManager.UI {
    partial class FrmSmmuserEdit_new {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSmmuserEdit_new));
            this.label1 = new System.Windows.Forms.Label();
            this.tbUserid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbExpireDate = new System.Windows.Forms.TextBox();
            this.tbDisableflg = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbRemark = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageComboBoxEdit1 = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btn_DeleteGroup = new DevExpress.XtraEditors.SimpleButton();
            this.btn_addgroup = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "用户号";
            // 
            // tbUserid
            // 
            this.tbUserid.Location = new System.Drawing.Point(99, 31);
            this.tbUserid.MaxLength = 20;
            this.tbUserid.Name = "tbUserid";
            this.tbUserid.Size = new System.Drawing.Size(270, 22);
            this.tbUserid.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户名";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(99, 63);
            this.tbUserName.MaxLength = 30;
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(270, 22);
            this.tbUserName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "密码";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 282);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "过期时间";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 14);
            this.label5.TabIndex = 2;
            this.label5.Text = "用户头像";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(99, 94);
            this.tbPassword.MaxLength = 20;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(270, 22);
            this.tbPassword.TabIndex = 2;
            // 
            // tbExpireDate
            // 
            this.tbExpireDate.Location = new System.Drawing.Point(99, 279);
            this.tbExpireDate.Name = "tbExpireDate";
            this.tbExpireDate.Size = new System.Drawing.Size(270, 22);
            this.tbExpireDate.TabIndex = 3;
            this.tbExpireDate.Visible = false;
            // 
            // tbDisableflg
            // 
            this.tbDisableflg.AutoSize = true;
            this.tbDisableflg.Location = new System.Drawing.Point(99, 161);
            this.tbDisableflg.Name = "tbDisableflg";
            this.tbDisableflg.Size = new System.Drawing.Size(15, 14);
            this.tbDisableflg.TabIndex = 5;
            this.tbDisableflg.UseVisualStyleBackColor = true;
            this.tbDisableflg.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 164);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 14);
            this.label6.TabIndex = 2;
            this.label6.Text = "是否禁用";
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 14);
            this.label7.TabIndex = 9;
            this.label7.Text = "描述";
            // 
            // tbRemark
            // 
            this.tbRemark.Location = new System.Drawing.Point(99, 126);
            this.tbRemark.MaxLength = 50;
            this.tbRemark.Name = "tbRemark";
            this.tbRemark.Size = new System.Drawing.Size(270, 22);
            this.tbRemark.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 218);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 14);
            this.label8.TabIndex = 11;
            this.label8.Text = "所属分组";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(100, 213);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(171, 67);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 15;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Windows Messenger.png");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "411214019201153424.jpg");
            this.imageList2.Images.SetKeyName(1, "411213517201116566.jpg");
            this.imageList2.Images.SetKeyName(2, "411153517201194678.jpg");
            this.imageList2.Images.SetKeyName(3, "411144019201118803.jpg");
            this.imageList2.Images.SetKeyName(4, "320521016201164845.jpg");
            this.imageList2.Images.SetKeyName(5, "320351016201136457.jpg");
            this.imageList2.Images.SetKeyName(6, "320231016201139934.jpg");
            this.imageList2.Images.SetKeyName(7, "320171016201169252.jpg");
            this.imageList2.Images.SetKeyName(8, "320111016201145884.jpg");
            this.imageList2.Images.SetKeyName(9, "320101116201195738.gif");
            this.imageList2.Images.SetKeyName(10, "41114411201110771.jpg");
            this.imageList2.Images.SetKeyName(11, "41103517201128217.jpg");
            this.imageList2.Images.SetKeyName(12, "35554410201158450.jpg");
            this.imageList2.Images.SetKeyName(13, "35454510201194671.jpg");
            this.imageList2.Images.SetKeyName(14, "35454410201181867.jpg");
            this.imageList2.Images.SetKeyName(15, "35424410201173094.jpg");
            this.imageList2.Images.SetKeyName(16, "35414510201178758.jpg");
            this.imageList2.Images.SetKeyName(17, "35384510201147551.jpg");
            this.imageList2.Images.SetKeyName(18, "35384410201145964.jpg");
            this.imageList2.Images.SetKeyName(19, "35274510201124305.jpg");
            this.imageList2.Images.SetKeyName(20, "35184410201119097.jpg");
            this.imageList2.Images.SetKeyName(21, "32071016201140511.jpg");
            this.imageList2.Images.SetKeyName(22, "32058916201135885.jpg");
            this.imageList2.Images.SetKeyName(23, "32054916201161968.jpg");
            this.imageList2.Images.SetKeyName(24, "32051116201123163.gif");
            this.imageList2.Images.SetKeyName(25, "32049916201188663.jpg");
            this.imageList2.Images.SetKeyName(26, "32046916201159189.jpg");
            this.imageList2.Images.SetKeyName(27, "32032916201159747.gif");
            this.imageList2.Images.SetKeyName(28, "32001116201111804.gif");
            this.imageList2.Images.SetKeyName(29, "22854011201186031.jpg");
            this.imageList2.Images.SetKeyName(30, "22849011201180230.jpg");
            this.imageList2.Images.SetKeyName(31, "22836011201161010.jpg");
            this.imageList2.Images.SetKeyName(32, "22825211201114363.jpg");
            this.imageList2.Images.SetKeyName(33, "22816211201166746.jpg");
            this.imageList2.Images.SetKeyName(34, "3564410201164488.jpg");
            this.imageList2.Images.SetKeyName(35, "3514410201153870.jpg");
            this.imageList2.Images.SetKeyName(36, "2286211201162170.jpg");
            this.imageList2.Images.SetKeyName(37, "1604[1].bmp");
            this.imageList2.Images.SetKeyName(38, "1603[1].bmp");
            this.imageList2.Images.SetKeyName(39, "1602[1].bmp");
            this.imageList2.Images.SetKeyName(40, "1599[1].bmp");
            this.imageList2.Images.SetKeyName(41, "1598[1].bmp");
            this.imageList2.Images.SetKeyName(42, "1273[1].bmp");
            this.imageList2.Images.SetKeyName(43, "1270[1].bmp");
            this.imageList2.Images.SetKeyName(44, "1043[1].bmp");
            this.imageList2.Images.SetKeyName(45, "1037[1].bmp");
            this.imageList2.Images.SetKeyName(46, "1026[1].bmp");
            this.imageList2.Images.SetKeyName(47, "1024[1].bmp");
            // 
            // imageComboBoxEdit1
            // 
            this.imageComboBoxEdit1.Location = new System.Drawing.Point(100, 157);
            this.imageComboBoxEdit1.Name = "imageComboBoxEdit1";
            this.imageComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageComboBoxEdit1.Properties.LargeImages = this.imageList2;
            this.imageComboBoxEdit1.Size = new System.Drawing.Size(78, 34);
            this.imageComboBoxEdit1.TabIndex = 16;
            this.imageComboBoxEdit1.EditValueChanged += new System.EventHandler(this.imageComboBoxEdit1_EditValueChanged);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(283, 307);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 29);
            this.simpleButton2.TabIndex = 18;
            this.simpleButton2.Text = "取消(&C)";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(193, 307);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 29);
            this.simpleButton1.TabIndex = 17;
            this.simpleButton1.Text = "确认(&O)";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btn_DeleteGroup
            // 
            this.btn_DeleteGroup.Location = new System.Drawing.Point(287, 246);
            this.btn_DeleteGroup.Name = "btn_DeleteGroup";
            this.btn_DeleteGroup.Size = new System.Drawing.Size(75, 29);
            this.btn_DeleteGroup.TabIndex = 20;
            this.btn_DeleteGroup.Text = "册除组";
            this.btn_DeleteGroup.Click += new System.EventHandler(this.btn_DeleteGroup_Click_1);
            // 
            // btn_addgroup
            // 
            this.btn_addgroup.Location = new System.Drawing.Point(287, 205);
            this.btn_addgroup.Name = "btn_addgroup";
            this.btn_addgroup.Size = new System.Drawing.Size(75, 29);
            this.btn_addgroup.TabIndex = 19;
            this.btn_addgroup.Text = "添加组";
            this.btn_addgroup.Click += new System.EventHandler(this.btn_addgroup_Click_1);
            // 
            // FrmSmmuserEdit_new
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 341);
            this.Controls.Add(this.btn_DeleteGroup);
            this.Controls.Add(this.btn_addgroup);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.imageComboBoxEdit1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbRemark);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbDisableflg);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbExpireDate);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbUserid);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmSmmuserEdit_new";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "用户编辑";
            this.Load += new System.EventHandler(this.FrmSmmuserEdit_new_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUserid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbExpireDate;
        private System.Windows.Forms.CheckBox tbDisableflg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbRemark;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private DevExpress.XtraEditors.ImageComboBoxEdit imageComboBoxEdit1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton btn_DeleteGroup;
        private DevExpress.XtraEditors.SimpleButton btn_addgroup;
    }
}