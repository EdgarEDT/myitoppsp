using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using Itop.Domain.Forecast;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using Itop.Client.Projects;
using Itop.Client.Using;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.Client.History
{
    public partial class FormGdpView : FormBase
    {
        public int pstype = 0;
        Hashtable ht = new Hashtable();
        public string yearflag = string.Empty;
        string projectUID = ""; 
        int firstyear = 2000;
        int endyear = 2008;
        public string ProjectUID
        {
            set { projectUID = value; }
        }

        public Hashtable HT
        {
            set { ht = value; }
        }

        public FormGdpView()
        {
            InitializeComponent();
        }

        private void FormGdpView_Load(object sender, EventArgs e)
        {
            InitData();
            InitForm();
        }

        private void InitData()
        {
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = yearflag;
            py.Col5 = projectUID;

            IList<Ps_YearRange> li = Itop.Client.Common.Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
            if (li.Count > 0)
            {
                firstyear = li[0].StartYear;
                endyear = li[0].FinishYear;
            }
            else
            {

                firstyear = 2000;
                endyear = 2008;
                py.BeginYear = 1990;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Itop.Client.Common.Services.BaseService.Create<Ps_YearRange>(py);
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Year");
            dt.Columns.Add("GDP", typeof(double));
            dt.Columns.Add("A", typeof(double));
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = pstype;
            psp_Type.Col4 = projectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            DataTable dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));

            DataRow[] rows1 = dataTable.Select("Title like '全地区GDP%'");
            DataRow[] rows2 = dataTable.Select("Title like '年末总人口%'");
            //找不到数据时给出提示
            if (rows1.Length == 0 || rows2.Length == 0)
            {
                MessageBox.Show("缺少‘全地区GDP’或‘年末总人口’ 数据,无法进行统计!");
                this.Close();
                return;
            }
            string pid = rows1[0]["ID"].ToString();
            DataRow[] rows3 = dataTable.Select("ParentID='"+pid+"'");

            int m=-1;
            for(int k=0;k<rows3.Length;k++)
            {
                m++;
                dt.Columns.Add("m" + m, typeof(double));
                dt.Columns.Add("n" + m, typeof(double));
                GridColumn gridColumn = new GridColumn();
                gridColumn.Caption = rows3[k]["Title"].ToString();
                gridColumn.FieldName = "m" + m;
                gridColumn.Visible = true;
                gridColumn.VisibleIndex = 2*m+10;
                gridColumn.DisplayFormat.FormatString = "n2";
                gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridColumn.Width = 70;
                
                this.gridView1.Columns.Add(gridColumn);

                GridColumn gridColumn1 = new GridColumn();
                gridColumn1.Caption = "比例(%)";
                gridColumn1.FieldName = "n" + m;
                gridColumn1.Visible = true;
                gridColumn1.VisibleIndex = 2 * m + 11;
                gridColumn1.DisplayFormat.FormatString = "n0";
                gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridColumn1.Width = 80;
                this.gridView1.Columns.Add(gridColumn1);
            }

            GridColumn gridColumn2 = new GridColumn();
            gridColumn2.Caption = "人口(万人)";
            gridColumn2.FieldName = "RK";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 2 * m+12;
            gridColumn2.DisplayFormat.FormatString = "n2";
            gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridColumn2.Width = 90;
            dt.Columns.Add("RK", typeof(double));
            this.gridView1.Columns.Add(gridColumn2);
            GridColumn gridColumn3 = new GridColumn();
            gridColumn3.Caption = "人均GDP（万元/人）";
            gridColumn3.FieldName = "RJGDP";
            gridColumn3.Visible = true;
            gridColumn3.VisibleIndex = 2 * m + 13;
            gridColumn3.DisplayFormat.FormatString = "n4";
            gridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridColumn3.Width = 120;
         
            dt.Columns.Add("RJGDP", typeof(double));
            this.gridView1.Columns.Add(gridColumn3);

            double sum1 = 0;
            for (int i = firstyear; i <= endyear; i++)
            {
                if(!ht.ContainsValue(i))
                    continue;

                DataRow row = dt.NewRow();
                row["ID"] = Guid.NewGuid().ToString();
                row["Year"] = i;
                double sum = 0;
                try { sum = Convert.ToDouble(rows1[0]["y" + i]); }
                catch { }
                row["GDP"] = sum.ToString();

                if (i == firstyear)
                    sum1 = sum;

                if (sum1 != 0)
                    sum1 = sum * 100 / sum1;
                row["A"] = sum1.ToString("n2");
                sum1 = sum;
                for (int j = 0; j <= m; j++)
                {
                    double s = 0;
                    double y = 0;
                    try { s = Convert.ToDouble(rows3[j]["y" + i]); }
                    catch { }
                    row["m" + j] = s.ToString();
                    if (sum != 0)
                        y = s*100 / sum;

                    row["n" + j] =detel_jd(y,2);
                }
                double rk = 0;
                double rjgdp = 0;
                try { rk = Convert.ToDouble(rows2[0]["y" + i]); }
                catch { }
                row["RK"] = rk.ToString();

                if (rk != 0)
                    rjgdp = sum / rk;

                row["RJGDP"] = detel_jd(rjgdp, 4);
                dt.Rows.Add(row);
                
            }

            this.gridControl1.DataSource = dt;


        
        }
        private double detel_jd(double tempdouble,int tempint)
        {
            return Math.Round(tempdouble, tempint);
        }
        private void InitForm()
        {
            barButtonItem1.Glyph = Itop.ICON.Resource.打印;
            barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem2.Glyph = Itop.ICON.Resource.关闭;
            barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem3.Glyph = Itop.ICON.Resource.发送;
            barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ComponentPrint.ShowPreview(this.gridControl1, this.gridView1.GroupPanelText);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportToExcelOld(this.gridView1.GroupPanelText, "亿元、%、万人、万元/人", this.gridControl1);
        }
    }
}