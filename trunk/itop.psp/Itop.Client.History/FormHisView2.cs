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
using DevExpress.XtraGrid.Views.BandedGrid;
using Itop.Client.Using;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.Client.History
{
    public partial class FormHisView2 : FormBase
    {
        Hashtable ht = new Hashtable();
        Hashtable ht1 = new Hashtable();
        Hashtable ht2 = new Hashtable();
        public int pstype = 0;
        public string yearflag = string.Empty;
        bool IsFist = true;
        int  RealFistYear = 0;
        string projectUID = ""; 
        int firstyear = 1990;
        int endyear = 2020;
        //GDP单位
        string GDPUnits = string.Empty;
        //全社会供电量单位
        string AGdlUnits = string.Empty;
        //全社会用电量单位
        string AYdlUnits = string.Empty;
        //全社会最大负荷单位
        string AMaxFhUnits = string.Empty;
        //年末总人口单位
        string NMARkUnits = string.Empty;
        public string ProjectUID
        {
            set { projectUID = value; }
        }

        public Hashtable HT
        {
            set { ht = value; }
        }
        public Hashtable HT1
        {
            set { ht1 = value; }
        }
        public Hashtable HT2
        {
            set { ht2 = value; }
        }
        public FormHisView2()
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
                firstyear = 1990;
                endyear = 2020;
                py.BeginYear = 1990;
                py.FinishYear = endyear;
                py.StartYear = firstyear;
                py.EndYear = 2060;
                py.ID = Guid.NewGuid().ToString();
                Itop.Client.Common.Services.BaseService.Create<Ps_YearRange>(py);
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Title");
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = pstype;
            psp_Type.Col4 = projectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            DataTable dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));

            int m=-1;
            this.gridControl1.BeginInit();
            this.gridControl1.BeginUpdate();
            bool isfirst = true;
            for (int i = firstyear; i <= endyear; i++)
            {
                
                if (!ht.ContainsValue(i))
                    continue;
                if (IsFist)
                {
                    RealFistYear = i;
                    IsFist = false;
                }
                m++;
                
                GridColumn gridColumn = new GridColumn();
                gridColumn.Caption = i+"年";
                gridColumn.FieldName = "y" + i;
                gridColumn.Visible = true;
                gridColumn.VisibleIndex = 2*m+10;
                gridColumn.Width = 70;
                gridColumn.DisplayFormat.FormatString = "n2";
                gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView1.Columns.Add(gridColumn);

                if (ht1.ContainsValue(i))
                {
                    if (isfirst)
                    {
                        isfirst = false;
                    }
                    else
                    {
                        gridColumn = new GridColumn();
                        gridColumn.Caption = "年均增长率(%)";
                        gridColumn.FieldName = "mm" + i;
                        gridColumn.Visible = true;
                        gridColumn.Width = 130;
                        gridColumn.VisibleIndex = 2 * m + 11;
                        gridColumn.DisplayFormat.FormatString = "n2";
                        gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        gridView1.Columns.Add(gridColumn);
                        dataTable.Columns.Add("mm" + i, typeof(double));
                    }
                   
                }

                if (ht2.ContainsValue(i))
                {

                    gridColumn = new GridColumn();
                    gridColumn.Caption = "逐年增长率(%)";
                    gridColumn.FieldName = "nn" + i;
                    gridColumn.Visible = true;
                    gridColumn.Width = 130;
                    gridColumn.VisibleIndex = 2 * m + 12;
                    gridColumn.DisplayFormat.FormatString = "n2";
                    gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView1.Columns.Add(gridColumn);
                    dataTable.Columns.Add("nn" + i, typeof(double));

                }
            
            }

            this.gridControl1.EndUpdate();
            this.gridControl1.EndInit();


            double d = 0;
            //年均增长率
            foreach (DataRow drw1 in dataTable.Rows)
            {

                foreach (DataColumn dc in dataTable.Columns)
                {
                    if (dc.ColumnName.IndexOf("mm") >= 0)
                    {
                        string s = dc.ColumnName.Replace("mm", "");
                        int y1 = int.Parse(s);
                        double d1 = 0;
                        try
                        {
                            d1 = (double)drw1["y" + s];
                        }
                        catch { }
                        int peryear = 0;
                        for (int i = y1-1; i >0; i--)
                        {
                            if ( ht1.ContainsValue(i))
                            {
                                peryear = i;
                                break;
                            }
                        }
                        try
                        {
                            d = (double)drw1["y" + peryear];
                        }
                        catch { }


                        double sss = Math.Round(Math.Pow(d1 / d, 1.0 / (y1 - peryear)) - 1, 4);
                        sss *= 100;

                        if (sss.ToString() == "非数字" || sss.ToString() == "正无穷大")
                            sss = 0;
                        drw1["mm" + s]=sss;
                    }
                }
            }
            //逐年增长率
            double dd = 0;
            foreach (DataRow drw1 in dataTable.Rows)
            {

                foreach (DataColumn dc in dataTable.Columns)
                {
                    if (dc.ColumnName.IndexOf("nn") >= 0)
                    {
                        string s = dc.ColumnName.Replace("nn", "");
                        int y1 = int.Parse(s);
                        double d1 = 0;
                        try
                        {
                            d1 = (double)drw1["y" + s];
                        }
                        catch { }
                        try
                        {
                            dd = (double)drw1["y" + (y1-1)];
                        }
                        catch { }

                        double sss = Math.Round(Math.Pow(d1 / dd, 1.0 / 1) - 1, 4);
                        sss *=100;
                        if (sss.ToString() == "非数字" || sss.ToString() == "正无穷大")
                            sss = 0;
                        drw1["nn" + s] = sss;
                    }
                }
            }







            this.gridControl1.DataSource = dataTable;


        
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
            
            FileClass.ExportToExcelOld(this.gridView1.GroupPanelText, "", this.gridControl1);
        }
    }
}