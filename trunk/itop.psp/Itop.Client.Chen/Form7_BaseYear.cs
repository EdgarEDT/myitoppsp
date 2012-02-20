using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.HistoryValue;
using Itop.Common;
using System.Collections;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
    public partial class Form7_BaseYear : FormBase
    {
        private int typeFlag2 = 7;
        private string baseyearFlag = "4132ae36-36b3-47ed-a9b9-163a6479d5d3";
        private string _baseyear = string.Empty;

        public string BaseYear
        {
            get { return _baseyear; }
            set { _baseyear = value; }
        }

        public Form7_BaseYear()
        {
            InitializeComponent();
        }
        //private string EnsureBaseYear(string baseyear)
        //{
        //    PSP_BaseYearRate BaseYearrate = (PSP_BaseYearRate)Common.Services.BaseService.GetObject("SelectPSP_BaseYearRateByKey", baseyearFlag);
        //    if (BaseYearrate.BaseYear != "0")
        //    {
        //        Hashtable ha = new Hashtable();
        //        ha.Add("Flag", typeFlag2);
        //        ha.Add("Year", Convert.ToInt32(BaseYearrate.BaseYear));
        //        PSP_Years baseyearlistYears = (PSP_Years)Common.Services.BaseService.GetObject("SelectPSP_YearsByYearFlag", ha);
        //        if (baseyearlistYears != null)
        //        {

        //            baseyear = baseyearlistYears.Year.ToString();

        //        }
        //    }
        //    return baseyear;
        //}
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Form7_BaseYear_Load(object sender, EventArgs e)
        {

            spinEdit1.Text = _baseyear;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (spinEdit1.Text == "")
            {
                Itop.Common.MsgBox.Show("请选择指定的年份！");
                return;
            }
            PSP_Years psp_Year = new PSP_Years();
            psp_Year.Flag = typeFlag2;
            Hashtable ha = new Hashtable();
            ha.Add("Flag", typeFlag2);
            ha.Add("Year", Convert.ToInt32(spinEdit1.Text));
            PSP_Years listYears = (PSP_Years)Common.Services.BaseService.GetObject("SelectPSP_YearsByYearFlag", ha);

            if (listYears == null)
            {
                MsgBox.Show("年份数据中不存在此记录，请添加后再操作！");
                return;
            }
            else
            {
                _baseyear = spinEdit1.Text;
                DialogResult = DialogResult.OK;
            }
        }

      
    }
}