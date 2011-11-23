using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Domain.Forecast;
using Itop.Client.Common;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Base;
namespace Itop.Client.Forecast
{
    public partial class FormDyhptl : FormBase
    {
        public FormDyhptl()
        {
            InitializeComponent();
        }

        IList<Ps_Calc> list1 = new List<Ps_Calc>();
        Ps_Calc pc1 = new Ps_Calc();
        private bool isedit = false;
        public int type = 12;
        DataTable dt = null;

        DataRow newrow2 = null;

        public bool ISEdit
        {

            set { isedit = value; }
        }
        Ps_forecast_list forecastReport;
        public Ps_forecast_list PForecastReports
        {
            get { return forecastReport; }
            set { forecastReport = value; }
        }

        DataTable dataTable;
        public DataTable DTable
        {
            get { return dataTable; }
            set { dataTable = value; }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Ps_Calc pcs = new Ps_Calc();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            pcs =(Ps_Calc) Services.BaseService.GetObject("SelectPs_CalcByForecast", pcs);
            if (pcs!=null)
            {
                pcs.Value1 = Convert.ToDouble(spinEdit1.Value);
                Services.BaseService.Update<Ps_Calc>(pcs);
            }
            else
            {
                pcs = new Ps_Calc();
                pcs.ID = System.Guid.NewGuid().ToString();
                pcs.Forecast=type;
                pcs.ForecastID = forecastReport.ID;
                pcs.Value1 =Convert.ToDouble(spinEdit1.Value) ;
                Services.BaseService.Create<Ps_Calc>(pcs);
            }
        }

        private void FormDyhptl_Load(object sender, EventArgs e)
        {
            int firstyear = 0;
            int endyear = 0;

            Ps_Forecast_Setup pfs = new Ps_Forecast_Setup();
            pfs.Forecast = type;
            pfs.ForecastID = forecastReport.ID;

            IList<Ps_Forecast_Setup> li = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);

            if (li.Count != 0)
            {
                firstyear = li[0].StartYear;
                endyear = li[0].EndYear;
            }


            Ps_Calc pcs = new Ps_Calc();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
            if (list1.Count>0)
            {
                spinEdit1.Value = (decimal)list1[0].Value1;
            }
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}