using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Forecast;
using Itop.Common;
using Itop.Domain.Table;
using Itop.Client.Base;
namespace Itop.Client.Forecast
{
    public partial class FormForecastEditC : FormBase
    {
        string LoginUser = "";
        private bool _isEdit;
        private int _typeFlag;
        private bool _noHistoryYears;
        private string _typeText;
        private string projectUID = "";

        public string TypeText
        {
            get { return _typeText; }
            set { _typeText = value; }
        }

        public string ProjectUID
        {
            get { return projectUID; }
            set { projectUID = value; }
        }

        public bool NoHistoryYears
        {
            get { return _noHistoryYears; }
            set { _noHistoryYears = value; }
        }
        private Ps_forecast_list psp_ForecastReport;
        public Ps_forecast_list Psp_ForecastReport
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
        public FormForecastEditC()
        {
            InitializeComponent();
            _isEdit = false;
            //_typeFlag = 1;
            _noHistoryYears = false;
            _typeText = "����";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == string.Empty)
            {
                MsgBox.Show("������Ԥ�����ƣ�");
                return;
            }

            if (_isEdit)//�޸�
            {

            }
            else//�½�
            {
                psp_ForecastReport = new Ps_forecast_list();
                psp_ForecastReport.ID = Guid.NewGuid().ToString();
                psp_ForecastReport.Col1 = TypeFlag.ToString();
            }
            psp_ForecastReport.Title = textEdit1.Text;
            psp_ForecastReport.StartYear = (int)spinEdit1.Value;
            psp_ForecastReport.EndYear = (int)spinEdit2.Value;
            psp_ForecastReport.UserID = projectUID;


            if (psp_ForecastReport.EndYear < psp_ForecastReport.StartYear + 1)
            {
                MsgBox.Show("�������Ӧ�ô�����ʼ�������1�꣡");
                return;
            }

            if (_isEdit)
            {
                try
                {
                    Common.Services.BaseService.Update<Ps_forecast_list>(psp_ForecastReport);
                }
                catch
                {
                    MsgBox.Show("�޸�Ԥ�����");
                    return;
                }
            }
            else
            {
                try
                {
                    Common.Services.BaseService.Create<Ps_forecast_list>(psp_ForecastReport);
                }
                catch
                {
                    MsgBox.Show("�½�Ԥ�����");
                    return;
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void FormForecastEdit_Load(object sender, EventArgs e)
        {
             LoginUser = projectUID;
            if(IsEdit)
            {
                textEdit1.Text = psp_ForecastReport.Title;
                spinEdit1.Value = psp_ForecastReport.StartYear;
                spinEdit2.Value = psp_ForecastReport.EndYear;
            }
            else
            {
                spinEdit1.Value = DateTime.Now.Year;
                spinEdit2.Value = DateTime.Now.Year + 10;
                textEdit1.Text = "������" + spinEdit1.Value + "��" + spinEdit2.Value + "����" + _typeText + "Ԥ���������";
            }

            int firstyear = 0;
            int endyear = 0;
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "������չʵ��";
            py.Col5 = projectUID;

            IList<Ps_YearRange> li = Itop.Client.Common.Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0)
            {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;

                label4.Text = "������չʵ����ʼ��Ϊ" + firstyear + "��,������Ϊ" + endyear + "�ꡣ";
            }
            else
                label4.Text = "";


        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}