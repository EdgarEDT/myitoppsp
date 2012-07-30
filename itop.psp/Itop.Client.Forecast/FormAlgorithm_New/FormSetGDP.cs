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
    public partial class FormSetGDP : FormBase
    {
        public  Ps_forecast_list forecastReport = null;
        DataTable dt = new DataTable();
        DataRow newrow1 = null;
        IList<Ps_Calc> list1 = new List<Ps_Calc>();
        Ps_Calc result = null;
        public  Ps_Calc pclResult
        {
            get
            {
                return result;
            }
        }
        public int type=0;
        public bool GetGDP
        {
            get
            {
                return checkGdp.Checked;
            }
        }
       
        public FormSetGDP()
        {
            InitializeComponent();
        }


        private void FormSetGDP_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
         


            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            DataTable   dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));
            DataRow[] rows1 = dataTable.Select("Title like '全地区GDP%'");

            if (rows1.Length == 0 )
            {
                MessageBox.Show("缺少‘全地区GDP’数据！");
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
         
            newrow1["ID"] = "ID";
            newrow1["Name"] = "GDP增长率";

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
                double s5 = 0;
                try { s1 = Convert.ToDouble(rows1[0]["y" + i]); }
                catch { }
                try { s2 = Convert.ToDouble(rows1[0]["y" + (i-1)]); }
                catch { }

                if (s2 != 0)
                    s5 = (s1 - s2) / s2;
                newrow1[i.ToString()] =Math.Round( s5,3);


                foreach (Ps_Calc pcs2 in list1)
                {
                    if (i == pcs2.Year)
                    {
                        newrow1[i.ToString()] = Math.Round(pcs2.Value2,3);

                    }
                }
            }
            dt.Rows.Add(newrow1);
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
                    ChnageNextValue(year, e.Value);
                }
            }
            
        }
        private void ChnageNextValue(int year,object value)
        {
            edit = true;
            for (int i = year+1; i <= forecastReport.YcEndYear; i++)
            {
                newrow1[i.ToString()] = value;
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
                double value2 = 0;
                try
                {
                    value2= (double)newrow1[dc.ColumnName];
 
                }
                catch { }


                bool bl = false;
                foreach (Ps_Calc pc11 in list1)
                {
                    if (pc11.Year == Convert.ToInt32(dc.ColumnName))
                    {
                        bl = true;
                        pc11.Value2 = value2;
                        Services.BaseService.Update<Ps_Calc>(pc11);
                    }
                    result = pc11;

                }
                if (!bl)
                {
                    Ps_Calc pcs = new Ps_Calc();
                    pcs.ID = Guid.NewGuid().ToString();
                    pcs.Forecast = type;
                    pcs.ForecastID = forecastReport.ID;
                    pcs.Year = Convert.ToInt32(dc.ColumnName);
                    pcs.Value2 = value2;
                    Services.BaseService.Create<Ps_Calc>(pcs);
                    result = pcs;
                }
            }
            this.DialogResult = DialogResult.OK;
        }



        }
}
