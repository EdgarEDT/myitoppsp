using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Domain.Forecast;
using System.Collections;
using Itop.Client.Projects;

namespace Itop.Client.Forecast
{
    public partial class FormSetGDP2 : FormBase
    {
        public  Ps_forecast_list forecastReport = null;
        DataTable dt = new DataTable();
        DataRow newrow1 = null;
        DataRow newrow2 = null;
        DataRow newrow3 = null;
        IList<Ps_Calc> list1 = new List<Ps_Calc>();
        public int type=0;
     
        public FormSetGDP2()
        {
            InitializeComponent();
        }


        private void FormSetGDP_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
         
            string sql = " ForecastID='" + forecastReport.ID + "' and Forecast=" + type + " and Col1='dc' and Col2='GDP'";

            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByWhere", sql);

            DataTable   dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));
            DataRow[] rows1 = dataTable.Select("Title like '一产'");
            DataRow[] rows2 = dataTable.Select("Title like '二产'");
            DataRow[] rows3 = dataTable.Select("Title like '三产'");

            if (rows1.Length == 0 || rows2.Length == 0 || rows3.Length == 0)
            {
                MessageBox.Show("缺少‘GDP’数据！( 一产、二产、三产)");
                this.Close();
                return;
            }
       

            Ps_Calc pcs = new Ps_Calc();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);

          
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            newrow1 =  dt.NewRow();
            newrow2= dt.NewRow();
            newrow3 = dt.NewRow();
         
            newrow1["ID"] = "ID";
            newrow1["Name"] = "一产增长率(%)";
            newrow2["Name"] = "二产增长率(%)";
            newrow3["Name"] = "三产增长率(%)";



            for (int i = forecastReport.YcStartYear; i <= forecastReport.YcEndYear; i++)
            {
                dt.Columns.Add(i.ToString(), typeof(double));
                DevExpress.XtraVerticalGrid.Rows.EditorRow editorRow = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
                editorRow.Properties.FieldName = i.ToString().Trim();
                editorRow.Properties.Caption =i.ToString().Trim();
                editorRow.Height = 20;
                editorRow.Properties.RowEdit = this.repositoryItemCalcEdit1;
                this.vGridControl1.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] { editorRow });


                double s1 = 0;
                double s2 = 0;
                double s3 = 0;

                double s4 = 0;
                double s5 = 0;
                double s6 = 0;


                double s7 = 0;
                double s8 = 0;
                double s9 = 0;


                try { s1 = Convert.ToDouble(rows1[0]["y" + i]); }
                catch { }
                try { s2 = Convert.ToDouble(rows1[0]["y" + (i-1)]); }
                catch { }
                if (s2 != 0)
                    s3 = (s1 - s2) / s2;
                newrow1[i.ToString()] =Math.Round( s3*100,3);


                try { s4 = Convert.ToDouble(rows1[0]["y" + i]); }
                catch { }
                try { s5 = Convert.ToDouble(rows1[0]["y" + (i - 1)]); }
                catch { }
                if (s5 != 0)
                    s6= (s4 - s5) / s5;
                newrow2[i.ToString()] = Math.Round(s6 * 100, 3);

                try { s7 = Convert.ToDouble(rows1[0]["y" + i]); }
                catch { }
                try { s8 = Convert.ToDouble(rows1[0]["y" + (i - 1)]); }
                catch { }
                if (s8 != 0)
                    s9 = (s7 - s8) / s8;
                newrow3[i.ToString()] = Math.Round(s9 * 100, 3);

                foreach (Ps_Calc pcs1 in list1)
                {
                    if (i == pcs1.Year)
                    {
                        newrow1[i.ToString()] = Math.Round(pcs1.Value1 * 100, 3);
                        newrow2[i.ToString()] = Math.Round(pcs1.Value2 * 100, 3);
                        newrow3[i.ToString()] = Math.Round(pcs1.Value3 * 100, 3);

                    }
                }
            }
            dt.Rows.Add(newrow1);
            dt.Rows.Add(newrow2);
            dt.Rows.Add(newrow3);
            vGridControl1.DataSource = dt;
            }

        private void FormSetGDP_Load()
        {

        }
        bool edit = false;
        private void vGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            if (!edit)
            {
                int year = Convert.ToInt32(vGridControl1.FocusedRow.Properties.FieldName);
                if (year < forecastReport.YcEndYear)
                {
                    ChnageNextValue(year,vGridControl1.FocusedRecord+1, e.Value);
                }
            }
            
        }
        private void ChnageNextValue(int year,int row,object value)
        {
            edit = true;
            switch (row)
            {
                case 1:
                    for (int i = year + 1; i <= forecastReport.YcEndYear; i++)
                    {
                        newrow1[i.ToString()] = value;
                    }
                    break;
                case 2:
                    for (int i = year + 1; i <= forecastReport.YcEndYear; i++)
                    {
                        newrow2[i.ToString()] = value;
                    }
                    break;
                case 3:
                    for (int i = year + 1; i <= forecastReport.YcEndYear; i++)
                    {
                        newrow3[i.ToString()] = value;
                    }
                    break;
                default:
                    break;
            }
            edit = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
          
            int i = 1;
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName == "Name"||dc.ColumnName == "ID" )
                    continue;
                double value1 = 0;
                double value2 = 0;
                double value3 = 0;
                try
                {
                    value1= (double)newrow1[dc.ColumnName]/100;
                    value2 = (double)newrow2[dc.ColumnName]/100;
                    value3 = (double)newrow3[dc.ColumnName]/100;
 
                }
                catch { }


                bool bl = false;
                foreach (Ps_Calc pc11 in list1)
                {
                    if (pc11.Year == Convert.ToInt32(dc.ColumnName))
                    {
                        bl = true;
                        pc11.Value1 = value1;
                        pc11.Value2 = value2;
                        pc11.Value3 = value3;
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
                    pcs.Value2 = value2;
                    pcs.Value3 = value3;
                    Services.BaseService.Create<Ps_Calc>(pcs);
                   
                }
            }
            this.DialogResult = DialogResult.OK;
        }



        }
}
