using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Chen;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FormPSP_VolumeBalanceVolumecalc : FormBase
    {
        private string type = "";
        private double yearValue = 0;
        public double YearValue
        {
            get { return yearValue; }
            set { yearValue = value; }
        }
        private string flag = "";
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public string Type
        {
            set { type = value; }
            get { return type; }
        }
        private string uid = "";
        public string UID
        {
            get { return uid; }
            set { uid = value; }
        }
        string formtitle = "";
        public string FormTitle
        {
            get { return formtitle; }
            set { formtitle = value; }
        }

        string ctrtitle = "";
        /// <summary>
        /// 获取flag对象
        /// </summary>
        public string CtrTitle
        {
            get { return ctrtitle; }
            set { ctrtitle = value; }
        }

        public double SetSpanText
        {
            set { this.spinEdit1.Value = (decimal)value; }
            get { return (double)spinEdit1.Value; }
        }

        public FormPSP_VolumeBalanceVolumecalc()
        {
            InitializeComponent();
        }

        private void FormPSP_VolumeBalanceVolumecalc_Load(object sender, EventArgs e)
        {
           // spinEdit1.Value =Convert.ToDecimal(yearValue);
            groupBox1.Text = ctrtitle;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //PSP_VolumeBalanceDataSource BaseYeartemp = new PSP_VolumeBalanceDataSource();
            //BaseYeartemp.TypeID = Convert.ToInt32(type);
            //BaseYeartemp.UID = uid;
            //BaseYeartemp.Flag = flag;
            //BaseYeartemp.Value =Convert.ToDouble( spinEdit1.Value);
            //Services.BaseService.Update<PSP_VolumeBalanceDataSource>(BaseYeartemp);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}