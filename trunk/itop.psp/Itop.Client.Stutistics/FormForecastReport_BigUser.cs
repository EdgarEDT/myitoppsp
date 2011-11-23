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
using Itop.Domain.Stutistic;

namespace Itop.Client.Stutistics
{
    /// <summary>
    /// Summary description for FormForecastReport.
    /// </summary>
    public class FormForecastReport_BigUser : DevExpress.XtraEditors.XtraForm
    {
        private Label label1;
        private Label label2;
        private Label label3;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;

        private bool _isEdit;
        private string  _typeFlag;
        private bool _noHistoryYears;
        private string _typeText;

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
        private PowerEachList  psp_ForecastReport;
        private DateEdit dateEdit1;
        private MemoEdit memoEdit1;
        private SpinEdit spinEdit1;
        private Label label4;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public FormForecastReport_BigUser()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            _isEdit = false;
         
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "年份：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "创建时间：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "备注：";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(171, 183);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 26);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "确定(&O)";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(264, 183);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 26);
            this.simpleButton2.TabIndex = 6;
            this.simpleButton2.Text = "取消(&C)";
            // 
            // dateEdit1
            // 
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(128, 71);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Appearance.BackColor = System.Drawing.SystemColors.Window;
            this.dateEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.dateEdit1.Size = new System.Drawing.Size(228, 23);
            this.dateEdit1.TabIndex = 7;
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(128, 115);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(228, 46);
            this.memoEdit1.TabIndex = 8;
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(128, 29);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.spinEdit1.Properties.Mask.EditMask = "g0";
            this.spinEdit1.Size = new System.Drawing.Size(82, 23);
            this.spinEdit1.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(216, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "年";
            // 
            // FormForecastReport_BigUser
            // 
            this.AcceptButton = this.simpleButton1;
            this.CancelButton = this.simpleButton2;
            this.ClientSize = new System.Drawing.Size(384, 235);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.spinEdit1);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.dateEdit1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormForecastReport_BigUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormForecastReport";
            this.Load += new System.EventHandler(this.FormForecastReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        public PowerEachList Psp_ForecastReport
        {
            get { return psp_ForecastReport; }
            set { psp_ForecastReport = value; }
        }

        public bool IsEdit
        {
            get { return _isEdit; }
            set { _isEdit = value; }
        }

        public string TypeFlag
        {
            get { return _typeFlag; }
            set { _typeFlag = value; }
        }

        private void FormForecastReport_Load(object sender, EventArgs e)
        {

            if (IsEdit)
            {
                spinEdit1.Text = psp_ForecastReport.ListName;
                memoEdit1.Text = psp_ForecastReport.Remark;
                dateEdit1.Text = psp_ForecastReport.CreateDate.ToShortDateString();
                spinEdit1.Enabled = false;
                dateEdit1.Enabled = false;
            }
            else
            {
                spinEdit1.Properties.MaxValue = DateTime.Now.Year + 100;
                spinEdit1.Properties.MinValue= DateTime.Now.Year -100;
                spinEdit1.Value = DateTime.Now.Year;
                dateEdit1.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //if(textEdit1.Text == string.Empty)
            //{
            //    MsgBox.Show("请输入计划表名称！");
            //    return;
            //}

            if(_isEdit)//修改
            {

            }
            else//新建
            {
                psp_ForecastReport = new PowerEachList();
                psp_ForecastReport.Types = _typeFlag;
            }
            psp_ForecastReport.ListName = spinEdit1.Text;
            DateTime dt = DateTime.Now;
            if (dateEdit1.Text == null || dateEdit1.Text == "")
                dateEdit1.Text = dt.ToString();
            if (Convert.ToDateTime(dateEdit1.Text) > dt)
            {
                psp_ForecastReport.CreateDate = dt;
            }
            else
            {
                psp_ForecastReport.CreateDate = Convert.ToDateTime(dateEdit1.Text);
            }
            psp_ForecastReport.Remark = memoEdit1.Text;

            if (_isEdit)
            {
                try
                {
                    Common.Services.BaseService.Update<PowerEachList>(psp_ForecastReport);
                }
                catch
                {
                    MsgBox.Show("修改计划表出错！");
                    return;
                }
            }
            else
            {
                try
                { 
              PowerEachList obj=(PowerEachList)Common.Services.BaseService.GetObject("SelectPowerEachListListByTypesAndListName", psp_ForecastReport);
              if (obj != null)
              {
                  MsgBox.Show("已经存在此项目表名称！");
                  return;
              }
            
              Common.Services.BaseService.Create<PowerEachList>(psp_ForecastReport);
              PowerEachList oo = (PowerEachList)Common.Services.BaseService.GetObject("SelectPowerEachListListByTypesAndListName", psp_ForecastReport); 
                }
                catch
                {
                    MsgBox.Show("新建计划表出错！");
                    return;
                }
            }

            DialogResult = DialogResult.OK;
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

      
    }
}

