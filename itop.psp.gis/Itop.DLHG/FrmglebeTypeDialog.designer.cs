
namespace Itop.DLGH{	partial class FrmglebeTypeDialog	{		/// <summary>		/// 必需的设计器变量。		/// </summary>		private System.ComponentModel.IContainer components = null;		/// <summary>		/// 清理所有正在使用的资源。		/// </summary>		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>		/// </summary>		protected override void Dispose(bool disposing)		{			if (disposing && (components != null))			{				components.Dispose();			}			base.Dispose(disposing);		}		#region Windows 窗体设计器生成的代码		/// <summary>		/// 设计器支持所需的方法 - 不要		/// 使用代码编辑器修改此方法的内容。		/// </summary>		private void InitializeComponent()		{
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.TypeColor = new DevExpress.XtraEditors.ColorEdit();
            this.TypeStyle = new DevExpress.XtraEditors.TextEdit();
            this.txtrjl = new DevExpress.XtraEditors.TextEdit();
            this.txtxs = new DevExpress.XtraEditors.TextEdit();
            this.TypeName = new DevExpress.XtraEditors.TextEdit();
            this.TypeID = new DevExpress.XtraEditors.TextEdit();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TypeColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeStyle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrjl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtxs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.TypeColor);
            this.panelControl.Controls.Add(this.TypeStyle);
            this.panelControl.Controls.Add(this.txtrjl);
            this.panelControl.Controls.Add(this.txtxs);
            this.panelControl.Controls.Add(this.TypeName);
            this.panelControl.Controls.Add(this.TypeID);
            this.panelControl.Controls.Add(this.label6);
            this.panelControl.Controls.Add(this.label5);
            this.panelControl.Controls.Add(this.label4);
            this.panelControl.Controls.Add(this.label3);
            this.panelControl.Controls.Add(this.label2);
            this.panelControl.Controls.Add(this.label1);
            this.panelControl.Controls.Add(this.btnOK);
            this.panelControl.Controls.Add(this.btnCancel);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(410, 220);
            this.panelControl.TabIndex = 0;
            this.panelControl.Text = "panelControl1";
            // 
            // TypeColor
            // 
            this.TypeColor.EditValue = System.Drawing.Color.Empty;
            this.TypeColor.Location = new System.Drawing.Point(160, 89);
            this.TypeColor.Name = "TypeColor";
            this.TypeColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TypeColor.Size = new System.Drawing.Size(238, 23);
            this.TypeColor.TabIndex = 5;
            // 
            // TypeStyle
            // 
            this.TypeStyle.Location = new System.Drawing.Point(160, 61);
            this.TypeStyle.Name = "TypeStyle";
            this.TypeStyle.Properties.Mask.EditMask = "####0.###";
            this.TypeStyle.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.TypeStyle.Properties.MaxLength = 8;
            this.TypeStyle.Size = new System.Drawing.Size(238, 23);
            this.TypeStyle.TabIndex = 4;
            // 
            // txtrjl
            // 
            this.txtrjl.EditValue = "1";
            this.txtrjl.Location = new System.Drawing.Point(160, 146);
            this.txtrjl.Name = "txtrjl";
            this.txtrjl.Properties.EditFormat.FormatString = "###.##";
            this.txtrjl.Properties.Mask.EditMask = "###.##";
            this.txtrjl.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtrjl.Properties.MaxLength = 5;
            this.txtrjl.Size = new System.Drawing.Size(238, 23);
            this.txtrjl.TabIndex = 4;
            // 
            // txtxs
            // 
            this.txtxs.EditValue = "1";
            this.txtxs.Location = new System.Drawing.Point(160, 117);
            this.txtxs.Name = "txtxs";
            this.txtxs.Properties.Mask.EditMask = "#0.#";
            this.txtxs.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtxs.Properties.MaxLength = 3;
            this.txtxs.Size = new System.Drawing.Size(238, 23);
            this.txtxs.TabIndex = 4;
            // 
            // TypeName
            // 
            this.TypeName.Location = new System.Drawing.Point(160, 34);
            this.TypeName.Name = "TypeName";
            this.TypeName.Size = new System.Drawing.Size(238, 23);
            this.TypeName.TabIndex = 4;
            // 
            // TypeID
            // 
            this.TypeID.Location = new System.Drawing.Point(160, 7);
            this.TypeID.Name = "TypeID";
            this.TypeID.Size = new System.Drawing.Size(238, 23);
            this.TypeID.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "容积率：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "需用系数：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "颜色：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "负荷密度指标（MW/KM²）：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "地块名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "地块编号：";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(232, 187);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(323, 187);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            // 
            // FrmglebeTypeDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(410, 220);
            this.Controls.Add(this.panelControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmglebeTypeDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地块负荷密度维护";
            this.Load += new System.EventHandler(this.FrmglebeTypeDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TypeColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeStyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtrjl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtxs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeID.Properties)).EndInit();
            this.ResumeLayout(false);

		}		#endregion

        private DevExpress.XtraEditors.PanelControl panelControl;		private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.TextEdit TypeID;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.ColorEdit TypeColor;
        private DevExpress.XtraEditors.TextEdit TypeStyle;
        private DevExpress.XtraEditors.TextEdit TypeName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtxs;
        private DevExpress.XtraEditors.TextEdit txtrjl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;	}}