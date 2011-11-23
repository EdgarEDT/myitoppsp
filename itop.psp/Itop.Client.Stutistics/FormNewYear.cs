using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Common;
using Itop.Client.Base;

namespace Itop.Client.Stutistics
{
    public partial class FormNewYear : DevExpress.XtraEditors.XtraForm
    {
        private int yearValue = 2000;
        private string flag2 = "";
        private bool _getYearOnly = false;

        private ArrayList al = new ArrayList();


        public ArrayList Al
        {
            get { return al; }
            set { al = value; }
        }
        public bool GetYearOnly
        {
            get { return _getYearOnly; }
            set { _getYearOnly = value; }
        }

        public string Flag2
        {
            get { return flag2; }
            set { flag2 = value; }
        }

        public int YearValue
        {
            get { return yearValue; }
            set { yearValue = value; }
        }

        public FormNewYear()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            PowerYears psp_Year = new PowerYears();
            psp_Year.Flag = flag2;
            yearValue = psp_Year.Year = (int)spinEdit1.Value;
            if(_getYearOnly)
            {
                this.DialogResult = DialogResult.OK;
                return;
            }

            if (al.Contains(yearValue + "年") == true)
            {
                MsgBox.Show("已经存在此年份，要想添加，请先删除已存在的" + yearValue + "年!");
                return;
            }
            try
            {
                if (Common.Services.BaseService.GetObject("SelectPowerYearsByYearFlag", psp_Year) == null)
                {
                    try
                    {
                        Common.Services.BaseService.Create<PowerYears>(psp_Year);
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
            catch(Exception ex)
            {
                MsgBox.Show("出错啦：" + ex.Message);
            }

        }

        private void FormNewYear_Load(object sender, EventArgs e)
        {
            spinEdit1.Value = yearValue;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
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