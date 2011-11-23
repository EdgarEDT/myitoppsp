using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Common;
using Itop.Domain.HistoryValue;

namespace Itop.Client.Chen
{
    /// <summary>
    /// Summary description for FormWGBC.
    /// </summary>
    public class FormWGBC : DevExpress.XtraEditors.XtraForm
    {
        private GroupBox groupBox1;
        private TextEdit textEdit1;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;

        private bool _isEdit = false;

        public bool IsEdit
        {
            get { return _isEdit; }
            set { _isEdit = value; }
        }
        private PSP_WGBCReports _wgbcReport = null;

        public PSP_WGBCReports WgbcReport
        {
            get { return _wgbcReport; }
            set { _wgbcReport = value; }
        }
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public FormWGBC()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textEdit1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标题";
            // 
            // textEdit1
            // 
            this.textEdit1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textEdit1.Location = new System.Drawing.Point(27, 50);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.MaxLength = 100;
            this.textEdit1.Size = new System.Drawing.Size(227, 21);
            this.textEdit1.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(127, 142);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 27);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "确定(&O)";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(214, 142);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 27);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "取消(&C)";
            // 
            // FormWGBC
            // 
            this.AcceptButton = this.simpleButton1;
            this.CancelButton = this.simpleButton2;
            this.ClientSize = new System.Drawing.Size(303, 179);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormWGBC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "无功补偿容量配置表";
            this.Load += new System.EventHandler(this.FormWGBC_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEdit1.Text == string.Empty)
            {
                MsgBox.Show("请输入标题！");
                return;
            }

            if(IsEdit)
            {
            }
            else
            {
                _wgbcReport = new PSP_WGBCReports();
            }
            _wgbcReport.Title = textEdit1.Text;

            if (IsEdit)
            {
                try
                {
                    Common.Services.BaseService.Update<PSP_WGBCReports>(_wgbcReport);
                }
                catch
                {
                    MsgBox.Show("修改出错！");
                    return;
                }
            }
            else
            {
                try
                {
                    _wgbcReport.ID = (int)Common.Services.BaseService.Create<PSP_WGBCReports>(_wgbcReport);
                }
                catch
                {
                    MsgBox.Show("新建出错！");
                    return;
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void FormWGBC_Load(object sender, EventArgs e)
        {

            if (IsEdit)
            {
                textEdit1.Text = _wgbcReport.Title;
            }
            else
            {
            }
        }
    }
}

