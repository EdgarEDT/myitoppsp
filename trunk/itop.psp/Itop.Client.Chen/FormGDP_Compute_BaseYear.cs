using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.HistoryValue;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
    public partial class FormGDP_Compute_BaseYear : FormBase
    {
        public FormGDP_Compute_BaseYear()
        {
            InitializeComponent();
        }
        private string _baseyear=string.Empty;

        public string BaseYear
        {
            get { return _baseyear; }
            set { _baseyear = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void FormGDP_Compute_BaseYear_Load(object sender, EventArgs e)
        {
            PSP_GDPBaseYear BaseYear = (PSP_GDPBaseYear)Common.Services.BaseService.GetObject("SelectPSP_GDPBaseYearList", null);
            if (BaseYear != null)
            {
                spinEdit1.Text = BaseYear.BaseYear;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (spinEdit1.Text == "")
            {
                Itop.Common.MsgBox.Show("请选择指定的年份！");
                return;
            }
            if (Convert.ToInt32(spinEdit1.Text) < 1990 || Convert.ToInt32(spinEdit1.Text) > DateTime.Now.Year)
            {
                Itop.Common.MsgBox.Show("您输入的年份不在有效范围！请输入1990至" + DateTime.Now.Year + "范围内的有效值！");
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