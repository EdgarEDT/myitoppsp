using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.HistoryValue;
using Itop.Common;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
    public partial class FormGDP_AddYears : FormBase
    {
        private int _flag2=1;
        private int _getvalue=2000;

        public int Flag2
        {
            get { return _flag2; }
            set { _flag2 = value; }
        }

        public int GetValue
        {
            get { return _getvalue; }
            set { _getvalue = value; }
        }

        public FormGDP_AddYears()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void FormGDP_AddYears_Load(object sender, EventArgs e)
        {
            spinEdit1.Value = _getvalue;
        }
        private InputLanguage output = null;
        private void spinEdit1_Enter(object sender, EventArgs e)
        {
            output = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            PSP_GDPYeas psp_Year = new PSP_GDPYeas();
            psp_Year.Flag = _flag2;
            psp_Year.Year = _getvalue = (int)spinEdit1.Value;
            try
            {
                if (Common.Services.BaseService.GetObject("SelectPSP_YeasByFlagYear", psp_Year) == null)
                {
                    try
                    {
                        Common.Services.BaseService.Create<PSP_GDPYeas>(psp_Year);
                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("出错啦：" + ex.Message);
                    }
                }
                else
                {
                    MsgBox.Show("此年份已经存在，请重新输入！");
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show("出错啦：" + ex.Message);
            }
        }
    }
}