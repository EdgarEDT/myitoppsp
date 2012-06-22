namespace Itop.Client.Forecast
{
    partial class FormForecastModelDSH
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.sbtnOK = new DevExpress.XtraEditors.SimpleButton();
            this.sbtnCansel = new DevExpress.XtraEditors.SimpleButton();
            this.loeModel = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.loeModel.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(28, 45);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(84, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "选择预测方法：";
            // 
            // sbtnOK
            // 
            this.sbtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtnOK.Location = new System.Drawing.Point(196, 121);
            this.sbtnOK.Name = "sbtnOK";
            this.sbtnOK.Size = new System.Drawing.Size(75, 23);
            this.sbtnOK.TabIndex = 1;
            this.sbtnOK.Text = "确定";
            this.sbtnOK.Click += new System.EventHandler(this.sbtnOK_Click);
            // 
            // sbtnCansel
            // 
            this.sbtnCansel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbtnCansel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbtnCansel.Location = new System.Drawing.Point(277, 121);
            this.sbtnCansel.Name = "sbtnCansel";
            this.sbtnCansel.Size = new System.Drawing.Size(75, 23);
            this.sbtnCansel.TabIndex = 2;
            this.sbtnCansel.Text = "取消";
            // 
            // loeModel
            // 
            this.loeModel.Location = new System.Drawing.Point(118, 42);
            this.loeModel.Name = "loeModel";
            this.loeModel.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.loeModel.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("title", "预测方法名"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default)});
            this.loeModel.Properties.DropDownRows = 10;
            this.loeModel.Size = new System.Drawing.Size(212, 21);
            this.loeModel.TabIndex = 3;
            this.loeModel.EditValueChanged += new System.EventHandler(this.loeModel_EditValueChanged);
            // 
            // FormForecastModelDSH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 156);
            this.Controls.Add(this.loeModel);
            this.Controls.Add(this.sbtnCansel);
            this.Controls.Add(this.sbtnOK);
            this.Controls.Add(this.labelControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormForecastModelDSH";
            this.Text = "FormForecastModelDSH";
            this.Load += new System.EventHandler(this.FormForecastModelDSH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.loeModel.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton sbtnOK;
        private DevExpress.XtraEditors.SimpleButton sbtnCansel;
        private DevExpress.XtraEditors.LookUpEdit loeModel;
    }
}