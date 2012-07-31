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
using Itop.Domain.Table;
using Itop.Client.Base;
namespace Itop.Client.Forecast
{
    public partial class FormForecastCalc4SH : FormBase
    {
        public FormForecastCalc4SH()
        {
            InitializeComponent();
        }

        IList<Ps_Calc> list1 = new List<Ps_Calc>();
        Ps_Calc pc1 = new Ps_Calc();
        private bool isedit=false;
        int type = 4;
        DataTable dt = null;
        int firstyear = 0;
        int endyear = 0;
        int firstyear1 = 0;
        int endyear1 = 0;
        DataRow newrow2 = null;
        DataRow newrow1 = null;
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

        private void FormForecastCalc4_Load(object sender, EventArgs e)
        {
            #region 电力发展实绩



            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "电力发展实绩经济";
            py.Col5 = Itop.Client.MIS.ProgUID;

            IList<Ps_YearRange> li2 = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li2.Count > 0)
            {
                firstyear1 = li2[0].StartYear;
                endyear1 = li2[0].FinishYear;
            }
            //新经济数据
            //Ps_History psp_Type = new Ps_History();
            //psp_Type.Forecast = 5;
            //psp_Type.Col4 = Itop.Client.MIS.ProgUID;
            //IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            //DataTable dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
            //DataRow[] rows1 = dataTable.Select("Title like '全地区GDP%'");


            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));
            DataRow[] rows1 = dataTable.Select("Title like '全地区GDP%'");

            DataRow[] rows4 = dataTable.Select("Title like '全社会用电量%'");


            //新电量数据
            //Ps_History psp_Type2 = new Ps_History();
            //psp_Type2.Forecast = 6;
            //psp_Type2.Col4 = Itop.Client.MIS.ProgUID;
            //IList<Ps_History> listTypes2 = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type2);
            //DataTable dataTable2 = Itop.Common.DataConverter.ToDataTable((IList)listTypes2, typeof(Ps_History));
            //DataRow[] rows4 = dataTable2.Select("Title like '全社会用电量%'");


            if (rows1.Length == 0 )
            {
                MessageBox.Show("缺少‘全地区GDP’数据！");
                this.Close();
                return;
            }
            

            if (rows4.Length==0)
            {
                MessageBox.Show("电量数据中缺少‘全社会用电量’数据！");
                this.Close();
                return;
            }

            #endregion


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
    
            dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
    
            newrow1 = dt.NewRow();
            newrow1["ID"] = "ID";
            newrow1["Name"] = "弹性系数";
            //newrow2 = dt.NewRow();
            //newrow2["Name"] = "GDP增长率";
          
            for (int i = firstyear; i <= forecastReport.YcEndYear; i++)
            {
                dt.Columns.Add(i.ToString(), typeof(double));
                DevExpress.XtraVerticalGrid.Rows.EditorRow editorRow = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
                editorRow.Properties.FieldName = i.ToString().Trim();
                editorRow.Properties.Caption =i.ToString().Trim();
                editorRow.Height = 20;
                editorRow.Properties.RowEdit = this.repositoryItemCalcEdit4;
                this.vGridControl2.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] { editorRow });


                double s1 = 0;
                double s2 = 0;
                double s3 = 0;
                double s4 = 0;
                double s5 = 0;
                double s6 = 0;
                double s7 = 0;
                try { s1 = Convert.ToDouble(rows1[0]["y" + i]); }
                catch { }
                try { s2 = Convert.ToDouble(rows1[0]["y" + (i-1)]); }
                catch { }

                try { s3 = Convert.ToDouble(rows4[0]["y" + i]); }
                catch { }
                try { s4 = Convert.ToDouble(rows4[0]["y" + (i - 1)]); }
                catch { }

                if (s2 != 0)
                    s5 = (s1 - s2) / s2;

                if (s4 != 0)
                    s6 = (s3 - s4) / s4;

                if (s5 != 0)
                    s7 = s6 / s5;

                newrow1[i.ToString()] = Math.Round(s7,3);
                //newrow2[i.ToString()] =Math.Round( s5,3);

                if (i>=forecastReport.YcStartYear)
                {
                    foreach (Ps_Calc pcs2 in list1)
                    {
                        if (i == pcs2.Year)
                        {
                            newrow1[i.ToString()] = Math.Round(pcs2.Value1, 3);
                        }
                    }

                }
               
           
              
            }
            dt.Rows.Add(newrow1);
            //dt.Rows.Add(newrow2);
     //       gridControl1.DataSource = dt;

            vGridControl2.DataSource = dt;


         
         
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
                if (dc.ColumnName == "Name" || dc.ColumnName == "ID" || Convert.ToInt32(dc.ColumnName)<=endyear)
                    continue;
                double value1 = 0;
                //double value2 = 0;
                try
                {
                    value1 = (double)newrow1[dc.ColumnName];
                    //value2 = (double)newrow2[dc.ColumnName];

                }
                catch { }


                bool bl = false;
                foreach (Ps_Calc pc11 in list1)
                {
                    if (pc11.Year ==  Convert.ToInt32( dc.ColumnName))
                    {
                        bl = true;
                        pc11.Value1 = value1;
                        //pc11.Value2 = value2;
                        Services.BaseService.Update<Ps_Calc>(pc11);
                    }
                }
                if (!bl)
                {
                    Ps_Calc pcs = new Ps_Calc();
                    pcs.ID = Guid.NewGuid().ToString();
                    pcs.Forecast = type;
                    pcs.ForecastID = forecastReport.ID;
                    pcs.Year = Convert.ToInt32(dc.ColumnName);
                    pcs.Value1 = value1;
                    //pcs.Value2 = value2;
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

        private void vGridControl2_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (Convert.ToInt32(vGridControl2.FocusedRow.Properties.FieldName)<=endyear)
            e.Cancel = true;
            if (vGridControl2.FocusedRow.Properties.FieldName!=null)
            {

            }
        }
        bool edit = false;
        private void vGridControl2_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            if (!edit)
            {
                int year = Convert.ToInt32(vGridControl2.FocusedRow.Properties.FieldName);
                if (year < forecastReport.YcEndYear)
                {
                    ChnageNextValue(year, e.Value);
                }
            }

        }
        private void ChnageNextValue(int year, object value)
        {
            edit = true;
            for (int i = year + 1; i <= forecastReport.YcEndYear; i++)
            {
                newrow1[i.ToString()] = value;
            }
            edit = false;
        }






    }
}