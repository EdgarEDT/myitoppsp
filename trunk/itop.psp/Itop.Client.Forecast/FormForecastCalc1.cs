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
    public partial class FormForecastCalc1 : FormBase
    {
        public FormForecastCalc1()
        {
            InitializeComponent();
        }

        IList<Ps_Calc> list1 = new List<Ps_Calc>();
        Ps_Calc pc1 = new Ps_Calc();
        private bool isedit=false;
        public int type = 1;
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

        private void HideToolBarButton()
        {

            if (!isedit)
            {
                //vGridControl2.Enabled = false;
                //simpleButton1.Visible = false;
            }
          
        }

        private void FormCalculator_Load(object sender, EventArgs e)
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
            



            HideToolBarButton();
            #region 年平均s
            dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            DataRow newrow1 = dt.NewRow();
            newrow1["ID"] = "ID";
            newrow1["Name"] = "历史增长率";
            newrow2 = dt.NewRow();
            newrow2["Name"] = "当前增长率";

            foreach (DataRow dataRow in dataTable.Rows)
            {
                dt.Columns.Add(dataRow["ID"].ToString().Trim(), typeof(double));
                DevExpress.XtraVerticalGrid.Rows.EditorRow editorRow = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
                editorRow.Properties.FieldName = dataRow["ID"].ToString().Trim();
                editorRow.Properties.Caption = dataRow["Title"].ToString().Trim();
                editorRow.Height = 20;
                editorRow.Properties.RowEdit = this.repositoryItemCalcEdit4;
                this.vGridControl2.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] { editorRow });

                int forecastYears = forecastReport.EndYear - forecastReport.StartYear;
                double[] historyValues = GenerateHistoryValue(dataRow,firstyear,endyear);
                newrow1[dataRow["ID"].ToString().Trim()] = Calculator.AverageIncreasing(historyValues);

                bool bl = false;
                foreach (Ps_Calc pc11 in list1)
                {
                    if (pc11.CalcID == editorRow.Properties.FieldName)
                    {
                        bl = true;
                        newrow2[dataRow["ID"].ToString().Trim()] = pc11.Value1;
                    }
                }
                if (!bl)
                {
                    newrow2[dataRow["ID"].ToString().Trim()] = Calculator.AverageIncreasing(historyValues);
                    
                    Ps_Calc pcs1 = new Ps_Calc();
                    pcs1.ID = Guid.NewGuid().ToString();
                    pcs1.Forecast = type;
                    pcs1.ForecastID = forecastReport.ID;
                    pcs1.CalcID = dataRow["ID"].ToString().Trim();
                    pcs1.Value1 = Calculator.AverageIncreasing(historyValues);
                    Services.BaseService.Create<Ps_Calc>(pcs1);
                }

            }
            


            dt.Rows.Add(newrow1);      
            dt.Rows.Add(newrow2);
            vGridControl2.DataSource = dt;

            


            #endregion

         
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            


            int i = 1;
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName == "Name" || dc.ColumnName == "ID")
                    continue;

                double value1 = 0;
                try
                {
                    value1 = (double)newrow2[dc.ColumnName];
                }
                catch { }


                bool bl = false;
                foreach (Ps_Calc pc11 in list1)
                {
                    if (pc11.CalcID == dc.ColumnName)
                    {
                        bl = true;
                        pc11.Value1 = value1;
                        Services.BaseService.Update<Ps_Calc>(pc11);
                    }
                }
                if (!bl)
                {
                    Ps_Calc pcs = new Ps_Calc();
                    pcs.ID = Guid.NewGuid().ToString();
                    pcs.Forecast = type;
                    pcs.ForecastID = forecastReport.ID;
                    pcs.CalcID = dc.ColumnName;
                    pcs.Value1 = value1;
                    Services.BaseService.Create<Ps_Calc>(pcs);

                }
            }
            this.DialogResult = DialogResult.OK;
        }





        //根据节点返回此行的历史数据
        private double[] GenerateHistoryValue(DataRow node,int syear,int eyear)
        {
            double[] rt = new double[eyear-syear+1];
            for (int i = 0; i < eyear - syear+1; i++)
            {
                object obj = node["y"+(syear+i)];
                if (obj == null || obj == DBNull.Value)
                {
                    rt[i] = 0;
                }
                else
                {
                    rt[i] = (double)obj;
                }
            }
            return rt;
        }






    }
}