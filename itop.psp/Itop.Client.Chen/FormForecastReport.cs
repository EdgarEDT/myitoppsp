using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Domain.HistoryValue;
using Itop.Common;
using Itop.Client.Base;
using System.Text.RegularExpressions;


namespace Itop.Client.Chen
{
    /// <summary>
    /// Summary description for FormForecastReport.
    /// </summary>
    public class FormForecastReport : DevExpress.XtraEditors.XtraForm
    {
        private Label label1;
        private SpinEdit spinEdit1;
        private Label label2;
        private TextEdit textEdit1;
        private Label label3;
        private SpinEdit spinEdit2;
        private Label label4;
        private SpinEdit spinEdit3;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;

        private bool _isEdit;
        private int _typeFlag;
        private bool _noHistoryYears;
        private string _typeText;
        private string _projectid;
        public string TypeText
        {
            get { return _typeText; }
            set { _typeText = value; }
        }

        public bool NoHistoryYears
        {
            get { return _noHistoryYears; }
            set { _noHistoryYears = value; }
        }
        private PSP_ForecastReports psp_ForecastReport;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public FormForecastReport()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            _isEdit = false;
            _typeFlag = 0;
            _noHistoryYears = false;
            _typeText = "电量";
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
            this.label1 = new System.Windows.Forms.Label();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.spinEdit2 = new DevExpress.XtraEditors.SpinEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.spinEdit3 = new DevExpress.XtraEditors.SpinEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "预测名称：";
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(128, 71);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit1.Properties.IsFloatValue = false;
            this.spinEdit1.Properties.Mask.EditMask = "####";
            this.spinEdit1.Properties.MaxValue = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.spinEdit1.Properties.MinValue = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.spinEdit1.Size = new System.Drawing.Size(228, 21);
            this.spinEdit1.TabIndex = 2;
            this.spinEdit1.EditValueChanged += new System.EventHandler(this.spinEdit1_EditValueChanged);
            this.spinEdit1.Enter += new System.EventHandler(this.spinEdit1_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "起始年份：";
            // 
            // textEdit1
            // 
            this.textEdit1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textEdit1.Location = new System.Drawing.Point(128, 30);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.MaxLength = 100;
            this.textEdit1.Size = new System.Drawing.Size(228, 21);
            this.textEdit1.TabIndex = 1;
            this.textEdit1.EditValueChanged += new System.EventHandler(this.textEdit1_EditValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "结束年份：";
            // 
            // spinEdit2
            // 
            this.spinEdit2.EditValue = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.spinEdit2.Location = new System.Drawing.Point(128, 114);
            this.spinEdit2.Name = "spinEdit2";
            this.spinEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit2.Properties.IsFloatValue = false;
            this.spinEdit2.Properties.Mask.EditMask = "####";
            this.spinEdit2.Properties.MaxValue = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.spinEdit2.Properties.MinValue = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.spinEdit2.Size = new System.Drawing.Size(228, 21);
            this.spinEdit2.TabIndex = 3;
            this.spinEdit2.Enter += new System.EventHandler(this.spinEdit2_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "历史数据年数：";
            // 
            // spinEdit3
            // 
            this.spinEdit3.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spinEdit3.Location = new System.Drawing.Point(128, 160);
            this.spinEdit3.Name = "spinEdit3";
            this.spinEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit3.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit3.Properties.IsFloatValue = false;
            this.spinEdit3.Properties.Mask.EditMask = "##";
            this.spinEdit3.Properties.MaxValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.spinEdit3.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit3.Size = new System.Drawing.Size(228, 21);
            this.spinEdit3.TabIndex = 4;
            this.spinEdit3.Enter += new System.EventHandler(this.spinEdit3_Enter);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(181, 221);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 26);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "确定(&O)";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(281, 221);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 26);
            this.simpleButton2.TabIndex = 6;
            this.simpleButton2.Text = "取消(&C)";
            // 
            // FormForecastReport
            // 
            this.AcceptButton = this.simpleButton1;
            this.CancelButton = this.simpleButton2;
            this.ClientSize = new System.Drawing.Size(384, 267);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.spinEdit3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.spinEdit2);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.spinEdit1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormForecastReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormForecastReport";
            this.Load += new System.EventHandler(this.FormForecastReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit3.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        public PSP_ForecastReports Psp_ForecastReport
        {
            get { return psp_ForecastReport; }
            set { psp_ForecastReport = value; }
        }

        public bool IsEdit
        {
            get { return _isEdit; }
            set { _isEdit = value; }
        }

        public int TypeFlag
        {
            get { return _typeFlag; }
            set { _typeFlag = value; }
        }
        public string ProjectID
        {
            get { return _projectid; }
            set { _projectid = value; }
        }
        private void FormForecastReport_Load(object sender, EventArgs e)
        {
            if(IsEdit)
            {
                textEdit1.Text = psp_ForecastReport.Title;
                spinEdit1.Value = psp_ForecastReport.StartYear;
                spinEdit2.Value = psp_ForecastReport.EndYear;
                spinEdit3.Value = psp_ForecastReport.HistoryYears;
            }
            else
            {
                spinEdit1.Value = DateTime.Now.Year;
                spinEdit2.Value = DateTime.Now.Year + 10;
                textEdit1.Text = "本地区" + spinEdit1.Value + "～" + spinEdit2.Value + "年需" + _typeText + "预测表（方案）";
            }

            if(_noHistoryYears)
            {
                label4.Visible = spinEdit3.Visible = false;
                simpleButton1.Location = new Point(simpleButton1.Location.X, simpleButton1.Location.Y - 60);
                simpleButton2.Location = new Point(simpleButton2.Location.X, simpleButton2.Location.Y - 60);
                this.Height -= 60;
                if (!IsEdit)
                {
                    spinEdit2.Value = DateTime.Now.Year + 5;
                    textEdit1.Text = "本地区" + spinEdit1.Value.ToString() + "年/" + spinEdit2.Value.ToString() + "年年负荷曲线预测表";
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEdit1.Text == string.Empty)
            {
                MsgBox.Show("请输入预测名称！");
                return;
            }

            if(_isEdit)//修改
            {

            }
            else//新建
            {
                psp_ForecastReport = new PSP_ForecastReports();
                psp_ForecastReport.Flag = _typeFlag;
                psp_ForecastReport.ProjectID = _projectid;
            }
            psp_ForecastReport.Title = textEdit1.Text;
            psp_ForecastReport.StartYear = (int)spinEdit1.Value;
            psp_ForecastReport.EndYear = (int)spinEdit2.Value;
            psp_ForecastReport.HistoryYears = (int)spinEdit3.Value;

            if (psp_ForecastReport.EndYear < psp_ForecastReport.StartYear + 1)
            {
                MsgBox.Show("结束年份应该大于起始年份至少1年！");
                return;
            }

            if (_isEdit)
            {
                try
                {
                    Common.Services.BaseService.Update<PSP_ForecastReports>(psp_ForecastReport);
                }
                catch
                {
                    MsgBox.Show("修改预测出错！");
                    return;
                }
            }
            else
            {
                try
                {
                    psp_ForecastReport.ID = (int)Common.Services.BaseService.Create<PSP_ForecastReports>(psp_ForecastReport);
                }
                catch
                {
                    MsgBox.Show("新建预测出错！");
                    return;
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if(spinEdit2.Value < spinEdit1.Value)
            {
                spinEdit2.Value = spinEdit1.Value;
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
        }
        private InputLanguage oldInput = null;
        private void spinEdit1_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
        }

        private void spinEdit2_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
        }

        private void spinEdit3_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
        }
    }
}

