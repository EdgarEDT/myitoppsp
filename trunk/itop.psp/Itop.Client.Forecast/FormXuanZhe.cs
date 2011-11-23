using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Domain.Forecast;
using Itop.Client.Base;
namespace Itop.Client.Forecast
{
    /// <summary>
    /// 结果比较设置
    /// </summary>
    public partial class FormXuanZhe : FormBase
    {
        private DataTable dt1 = new DataTable();
        private DataTable dt2 = new DataTable();
        private DataTable dt3 = new DataTable();
        private DataTable dt4 = new DataTable();
        private ArrayList a1 = new ArrayList();
        private ArrayList a2 = new ArrayList();
        private ArrayList a3 = new ArrayList();
        private ArrayList a4 = new ArrayList();
        private string ForecastID = "";
        private Ps_forecast_list forecastReport;
        public Ps_forecast_list ForecastReport
        {
            get { return forecastReport; }
            set { forecastReport = value; }
        }
        public string ReportForecastID
        {
            get { return ForecastID; }
            set { ForecastID = value; }
        }
        public DataTable DT1
        { 
            get{return dt1;}
        }

        public DataTable DT2
        {
            set { dt2=value; }
        }
      

        public DataTable DT3
        {
            set { dt3 = value; }
        }

        public ArrayList A1
        {
            get { return a1; }
        }
        public ArrayList A2
        {
            get { return a2; }
        }
        public ArrayList A3
        {
            get { return a3; }
        }
        public ArrayList A4
        {
            get { return a4; }
        }
        public FormXuanZhe()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
            string str="";
            int m = 0;
            a1.Clear();
            a2.Clear();
            a3.Clear();
            a4.Clear();
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                //if (System.DBNull.Value == gridView1.GetRowCellValue(i, "B"))
                //    continue;
                if ((bool)gridView1.GetRowCellValue(i, "B"))
                {
                    
                    switch (gridView1.GetRowCellValue(i, "A").ToString())
                    {
                        case "年增长率法":
                            m = 1;
                            break;
                        case "外推法":
                            m = 2;
                            break;
                        case "相关法":
                            m = 3;
                            break;
                        case "弹性系数法":
                            m = 4;
                            break;
                        case "指数平滑法":
                            m = 5;
                            break;
                        case "灰色模型法":
                            m = 6;
                            break;
                        case "专家决策法":
                            m = 7;
                            break;
                        case "复合算法":
                            //m = 9;
                            m = 20;
                            break;
                        case "定权组合系数法":
                            m = 10;
                            break;

                        case "常规增长率-大用户预测法":
                            m = 11;
                            break;
                    }
                  
                    a1.Add(m);
                 
                }
            }

            if (a1.Count < 2)
            {

                MessageBox.Show("请至少选择 2 个预测方法！");
                return;
            }

            
            foreach (DataRow dr in dt2.Rows)
            {
                if (System.DBNull.Value == dr["B"])
                    continue;
                if (Convert.ToBoolean( dr["B"]))
                {
                    a2.Add(dr["ID"].ToString());
                }
            
            }
            if (a2.Count < 1)
            {
                MessageBox.Show("请至少选择 1 个项目名称！");
               
                return;
            }

            foreach (DataRow dr in dt3.Rows)
            {
                if (Convert.ToBoolean(dr["B"]))
                {
                    a3.Add(Convert.ToInt32(dr["A"]));
                }
                if (Convert.ToBoolean(dr["C"]))
                {
                    a4.Add(Convert.ToInt32(dr["A"]));
                }

            }
            if (a3.Count < 1)
            {
                MessageBox.Show("请至少选择1年！");

                return;
            }
            this.DialogResult = DialogResult.OK;
        }
        private bool SelectDaYongHu(int i)
        {
            //"UserID='" + Itop.Client.MIS.ProgUID + "' and Col1='2' and Title='" + forecastReport.Title + "'  and StartYear='" + forecastReport.StartYear + "'" + "'  and EndYear='" + forecastReport.EndYear + "'"
            IList<Ps_forecast_list> listReports = Common.Services.BaseService.GetList<Ps_forecast_list>("SelectPs_forecast_listByWhere", "UserID='" + Itop.Client.MIS.ProgUID + "' and Col1='2' and Title='" + forecastReport.Title + "'");
            if (listReports.Count < 1)
            {

                return false;

            }
            else if(i==1)
            {
                object obj = Common.Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere",
                            "ForecastID='" + listReports[0].ID + "'");
                if (obj == null)
                    return false;
                Ps_Forecast_Math psp_Typen = new Ps_Forecast_Math();
                psp_Typen.ForecastID = ForecastID;
                psp_Typen.Forecast = 11;
                IList listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
                if (listTypesn.Count < 1)
                {
                    return false;
                }
            }
            return true;
        }
        private void Form8XuanZhe_Load(object sender, EventArgs e)
        {
            dt1.Columns.Add("A", typeof(string));
            dt1.Columns.Add("B", typeof(bool));
            dt1.Columns.Add("C", typeof(string));

            Ps_Forecast_Math psp_Typen = new Ps_Forecast_Math();
            psp_Typen.ForecastID = ForecastID;
            psp_Typen.Forecast = 1;
            IList listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
           
            DataRow row = dt1.NewRow();
            row["A"] = "年增长率法";
            row["B"] = true;
            if (listTypesn.Count > 0)
            {
                row["C"] = "有";
            }
            else
                
            {
                row["C"] = "无";
            }
            dt1.Rows.Add(row);

            row = dt1.NewRow();
            row["A"] = "常规增长率-大用户预测法";
            row["B"] = false;
            //psp_Typen.Forecast = 2;
            //listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (SelectDaYongHu(1))
            {
                row["C"] = "有";
            }
            else
            {
                row["C"] = "无";
            }

            if (SelectDaYongHu(0))
            dt1.Rows.Add(row);

            row = dt1.NewRow();
            row["A"] = "外推法";
            row["B"] = false;
            psp_Typen.Forecast = 2;
            listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
            {
                row["C"] = "有";
            }
            else
            {
                row["C"] = "无";
            }
            dt1.Rows.Add(row);

            //row = dt1.NewRow();
            //row["A"] = "相关法";
            //row["B"] = false;
            //psp_Typen.Forecast = 3;
            //listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            //if (listTypesn.Count > 0)
            //{
            //    row["C"] = "有";
            //}
            //else
            //{
            //    row["C"] = "无";
            //}
            //dt1.Rows.Add(row);

            row = dt1.NewRow();
            row["A"] = "弹性系数法";
            row["B"] = false;
            psp_Typen.Forecast = 4;
            listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
            {
                row["C"] = "有";
            }
            else
            {
                row["C"] = "无";
            }
            dt1.Rows.Add(row);

            row = dt1.NewRow();
            row["A"] = "指数平滑法";
            row["B"] = false;
            psp_Typen.Forecast = 5;
            listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
            {
                row["C"] = "有";
            }
            else
            {
                row["C"] = "无";
            }
            dt1.Rows.Add(row);

            row = dt1.NewRow();
            row["A"] = "灰色模型法";
            row["B"] = false;
            psp_Typen.Forecast = 6;
            listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
            {
                row["C"] = "有";
            }
            else
            {
                row["C"] = "无";
            }
            dt1.Rows.Add(row);

            row = dt1.NewRow();
            row["A"] = "专家决策法";
            row["B"] = false;
            psp_Typen.Forecast = 7;
            listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
            {
                row["C"] = "有";
            }
            else
            {
                row["C"] = "无";
            }
            dt1.Rows.Add(row);

            row = dt1.NewRow();
            row["A"] = "复合算法";
            row["B"] = false;
           // psp_Typen.Forecast =9;
            psp_Typen.Forecast = 20;
            listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
            {
                row["C"] = "有";
            }
            else
            {
                row["C"] = "无";
            }
            dt1.Rows.Add(row);

            //row = dt1.NewRow();
            //row["A"] = "定权组合系数法";
            //row["B"] = false;
            //psp_Typen.Forecast = 10;
            //listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            //if (listTypesn.Count > 0)
            //{
            //    row["C"] = "有";
            //}
            //else
            //{
            //    row["C"] = "无";
            //}
            //dt1.Rows.Add(row);
           
            gridControl1.DataSource = dt1;

            foreach (DataColumn dc in dt4.Columns)
            {
                dt2.Columns.Add(dc.ColumnName, dc.DataType);

            }
            if(!dt2.Columns.Contains("B"))
            dt2.Columns.Add("B", typeof(bool));
        if (!dt2.Columns.Contains("A"))
            dt2.Columns.Add("A");
        if (!dt4.Columns.Contains("B"))
            dt4.Columns.Add("B", typeof(bool));
        if (!dt4.Columns.Contains("A"))
            dt4.Columns.Add("A");
            //foreach (DataRow dr2 in dt2.Rows)
            //{
            //    DataRow dr = dttemp.NewRow();
            //    foreach (DataColumn dc in dt2.Columns)
            //        dr[dc.ColumnName] = dr2[dc.ColumnName];
            //    dttemp.Rows.Add(dr);
            //}
            ArrayList alist = new ArrayList();
            //foreach (DataRow dr in dt4.Rows)
            //{
                
            //    DataRow defind = dt2.NewRow();
            //    if (dr["Title"].ToString().Contains("-"))
            //    {
            //        string[] str = dr["Title"].ToString().Split('-');
            //        if (str[0].Contains("电量"))
            //        str[1] = str[1] + "电量";
            //            else if (str[0].Contains("负荷"))
            //        str[1] = str[1] + "负荷";

            //        if (alist.Contains(str[1]))
            //        {
            //            continue;
                    
            //        }
                    
                  
            //        foreach (DataColumn dc in dt2.Columns)
            //            defind[dc.ColumnName] = dr[dc.ColumnName];
            //        defind["Title"] = str[1];
            //        defind["A"] = str[1];
            //        alist.Add(str[1]);
            //        dt2.Rows.Add(defind);

            //    }
            //    else
            //    {
            //        if (alist.Contains(dr["Title"]))
            //        {
            //            continue;

            //        }


            //        foreach (DataColumn dc in dt2.Columns)
            //            defind[dc.ColumnName] = dr[dc.ColumnName];

            //        alist.Add(dr["Title"]);
            //        dt2.Rows.Add(defind);
            //    }
            //}


            foreach (DataRow dr in dt2.Rows)
            {
                dr["A"] = dr["Title"].ToString();
                dr["B"] = false;

            }
            
          
            // gridControl2.DataSource = dt2;
            gridControl2.DataSource = dt2;
            foreach (DataRow dr in dt3.Rows)
            {
                dr["B"] = true;
                dr["C"] = false;

            }




            gridControl3.DataSource = dt3;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in dt1.Rows)
                dr["B"] = checkEdit1.Checked;
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in dt2.Rows)
                dr["B"] = checkEdit2.Checked;
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in dt3.Rows)
                dr["B"] = checkEdit3.Checked;
        }

        private void checkEdit4_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in dt3.Rows)
                dr["C"] = checkEdit4.Checked;
        }

   

       
    }
}