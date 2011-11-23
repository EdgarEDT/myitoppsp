using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Client.Common;
using Itop.Common;
using Itop.Domain.Table;
using Dundas.Charting.WinControl;

namespace Itop.Client.Stutistics
{
    public partial class FrmBurdenMaxYear : Itop.Client.Base.FormBase
    {
        string title = "";
        string unit = "";
        bool isSelect = false;
        private IList<PS_Table_AreaWH> AreaList = null;

        DevExpress.XtraGrid.GridControl gcontrol = null;

        public string Title
        {
            get { return title; }
        }

        public string Unit
        {
            get { return unit; }
        }

        public DevExpress.XtraGrid.GridControl Gcontrol
        {
            get { return gcontrol; }
        }

        public bool IsSelect
        {
            set { isSelect = value; }
        }






        IList<BurdenYear> li = new List<BurdenYear>();
        public FrmBurdenMaxYear()
        {
            InitializeComponent();
        }

        private void FrmBurdenMaxYear_Load(object sender, EventArgs e)
        {
            string pjt = " ProjectID='" + MIS.ProgUID + "'";
            AreaList = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", pjt);
           
            bandedGridView1.GroupPanelText = this.Text;


            //IList<BurdenLine> list = Services.BaseService.GetStrongList<BurdenLine>();
            IList<BurdenLine> list = Services.BaseService.GetList<BurdenLine>("SelectBurdenLineByWhere", " uid like '%" + Itop.Client.MIS.ProgUID + "%'  order by BurdenDate");      
                
            int i = 1;
            int yeardata = 0;
            
            //IList<BurdenYear> li = new List<BurdenYear>();
            BurdenYear by = new BurdenYear();
            foreach (BurdenLine bl in list)
            {
                double burmax = 0;
                burmax = Math.Max(bl.Hour1, bl.Hour2);
                burmax = Math.Max(burmax, bl.Hour3);
                burmax = Math.Max(burmax, bl.Hour4);
                burmax = Math.Max(burmax, bl.Hour5);
                burmax = Math.Max(burmax, bl.Hour6);
                burmax = Math.Max(burmax, bl.Hour7);
                burmax = Math.Max(burmax, bl.Hour8);
                burmax = Math.Max(burmax, bl.Hour9);
                burmax = Math.Max(burmax, bl.Hour10);
                burmax = Math.Max(burmax, bl.Hour11);
                burmax = Math.Max(burmax, bl.Hour12);
                burmax = Math.Max(burmax, bl.Hour13);
                burmax = Math.Max(burmax, bl.Hour14);
                burmax = Math.Max(burmax, bl.Hour15);
                burmax = Math.Max(burmax, bl.Hour16);
                burmax = Math.Max(burmax, bl.Hour17);
                burmax = Math.Max(burmax, bl.Hour18);
                burmax = Math.Max(burmax, bl.Hour19);
                burmax = Math.Max(burmax, bl.Hour20);
                burmax = Math.Max(burmax, bl.Hour21);
                burmax = Math.Max(burmax, bl.Hour22);
                burmax = Math.Max(burmax, bl.Hour23);
                burmax = Math.Max(burmax, bl.Hour24);

                if (yeardata != bl.BurdenDate.Year)
                {
                    if (yeardata == 0)
                    {
                        yeardata = bl.BurdenDate.Year;
                        by = new BurdenYear();
                        by.BurdenYears = yeardata;
                        by.Values = burmax;
                        by.BurdenDate = bl.BurdenDate;
                        by.AreaID = bl.AreaID;
                    }
                    else
                    {
                        li.Add(by);
                        yeardata = bl.BurdenDate.Year;
                        by = new BurdenYear();
                        by.BurdenYears = yeardata;
                        by.Values = burmax;
                        by.BurdenDate = bl.BurdenDate;
                        by.AreaID = bl.AreaID;
                    }

                }
                else
                {
                    if (by.Values < burmax)
                    {
                        by.Values = burmax;
                        by.BurdenDate = bl.BurdenDate;
                        by.AreaID = bl.AreaID;
                    }




                    //by.Values = Math.Max(by.Values,burmax);
                    //by.BurdenDate = bl.BurdenDate;
                }
                if(i==list.Count)
                    li.Add(by);
                i++;
            }



            DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            gridBand1.Caption = "时间";
            gridBand1.Width = 100;
            gridBand1.Name = "gridBand1";
            gridBand1.AppearanceHeader.Options.UseTextOptions = true;
            gridBand1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridView1.Bands.Add(gridBand1);

            //DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            //gridBand2.Caption = "时间";
            //gridBand2.Name = "gridBand2";
            //gridBand2.AppearanceHeader.Options.UseTextOptions = true;
            //gridBand2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //gridBand1.Children.Add(gridBand2);

            DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            gridColumn1.Caption = "时间";
            gridColumn1.FieldName = "s" ;
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandedGridView1.Columns.Add(gridColumn1);
            gridBand1.Columns.Add(gridColumn1);
            



            DataTable dt = new DataTable();
            dt.Columns.Add("s", typeof(string));




            int listCount = li.Count;


            int numi = 0;
            int numj = 0;
            int numk = 0;

            DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBand = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[listCount];
            DevExpress.XtraGrid.Views.BandedGrid.GridBand[] gridBandDate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand[listCount];
            DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] gridColumn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[listCount];


            foreach (BurdenYear bys in li)
            {
                gridBand[numi] = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                gridBand[numi].Caption = FindArea(bys.AreaID)+"_"+ bys.BurdenYears.ToString() + "年";
                gridBand[numi].Width = 100;
                gridBand[numi].Name = "gridBand" + bys.BurdenYears.ToString();
                gridBand[numi].AppearanceHeader.Options.UseTextOptions = true;
                gridBand[numi].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                this.bandedGridView1.Bands.Add(gridBand[numi]);

                gridBandDate[numj] = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                gridBandDate[numj].Caption = bys.BurdenDate.ToString("MM月dd日");
                gridBandDate[numj].Name = "gridBand" + bys.BurdenDate.ToString("yyyyMMdd");
                gridBandDate[numj].AppearanceHeader.Options.UseTextOptions = true;
                gridBandDate[numj].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gridBand[numi].Children.Add(gridBandDate[numj]);

                gridColumn[numk] = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
                gridColumn[numk].Caption = "Year" + bys.BurdenYears.ToString();
                gridColumn[numk].FieldName = "Year" + bys.BurdenYears.ToString();
                gridColumn[numk].Name = "Year" + bys.BurdenYears.ToString();
                gridColumn[numk].Visible = true;
                gridColumn[numk].Width = 130;
                gridColumn[numk].AppearanceCell.Options.UseTextOptions = true;
                gridColumn[numk].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                bandedGridView1.Columns.Add(gridColumn[numk]);
                gridBandDate[numj].Columns.Add(gridColumn[numk]);
                numi++;
                numj++;
                numk++;




                dt.Columns.Add("Year" + bys.BurdenYears.ToString(), typeof(double));
            }

            DataRow row = dt.NewRow();
            row["s"] = "负荷";
            foreach (BurdenYear bys1 in li)
            {
                row["Year" + bys1.BurdenYears.ToString()] = bys1.Values;
            }
            dt.Rows.Add(row);

            gridControl1.DataSource = dt;



            chart1.Series.Clear();


            string seriesName = "最大负荷";
            chart1.Series.Add(seriesName);
            chart1.Series[seriesName].Type = SeriesChartType.Line;
            chart1.Series[seriesName].BorderWidth = 2;
            chart1.Series[seriesName].MarkerSize = 7;
            chart1.Series[seriesName].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(2);

            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName == "s")
                    continue;

                try { if (int.Parse(dc.ColumnName.Replace("Year", "")) > 10000)continue; }
                catch { }

                string coname = dc.ColumnName.Replace("Year", "");

                double YVal = 0;
                try
                {
                    YVal = (double)dt.Rows[0][dc.ColumnName];
                }
                catch { }
                chart1.Series[seriesName].Points.AddXY(coname, YVal);
            }

            if(!PrintRight)
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

        }
        //根据areaid返回Areaname
        private string FindArea(string Areaid)
        {
            string value = "无地区";
            if (AreaList.Count > 0)
            {
                for (int i = 0; i < AreaList.Count; i++)
                {
                    if (AreaList[i].ID == Areaid)
                    {
                        value = AreaList[i].Title.ToString();
                        break;
                    }
                }
            }
            return value;
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("A", typeof(string));
            dataTable.Columns.Add("B", typeof(bool));
            dataTable.Columns.Add("C", typeof(bool));
            dataTable.Rows.Clear();
            foreach (BurdenYear bl in li)
            {
                DataRow newRow = dataTable.NewRow();
                newRow["A"] = bl.BurdenYears;
                newRow["B"] = false;
                newRow["C"] = false;
                dataTable.Rows.Add(newRow);
            }

            FormYears cy = new FormYears();
            cy.DT = dataTable;
            if (cy.ShowDialog() != DialogResult.OK)
                return;

            IList<BurdenYear> li1 = new List<BurdenYear>();

            double values = 0;
            int years = 0;

            bool iszzl = false;
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (BurdenYear bl1 in li)
                {
                    if ((bool)row["B"] && row["A"].ToString() == bl1.BurdenYears.ToString())
                    {
                        
                        li1.Add(bl1);
                        if ((bool)row["C"])
                        {
                            BurdenYear by = new BurdenYear();
                            by.BurdenYears = 10000 + bl1.BurdenYears;
                            by.Values = Math.Round(Math.Pow(bl1.Values / values, 1.0 / (bl1.BurdenYears-years)) - 1, 4);
                           
                            by.Values=Math.Round(by.Values, 3);
                            li1.Add(by);

                            iszzl = true;
                            values = bl1.Values;
                            years = bl1.BurdenYears;
                        }
                        if (!iszzl)
                        {
                            values = bl1.Values;
                            years = bl1.BurdenYears;
                        }
                    }
                }
            }


            FrmBurdenMaxYear1 frm = new FrmBurdenMaxYear1();
            if (!PrintRight)
            {
                frm.print = false;
            }
            frm.Text = this.Text;
            frm.ObjList = li1;
            frm.IsSelect = isSelect;
            if (frm.ShowDialog() == DialogResult.OK && isSelect)
            {
                gcontrol = frm.Gcontrol;
                title = frm.Title;
                //unit = "单位：万千瓦";
                DialogResult = DialogResult.OK;
            }

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ComponentPrint.ShowPreview(this.gridControl1, this.bandedGridView1.GroupPanelText);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FileClass.ExportExcel(this.gridControl1);
            }
            catch { }
        }

        private void bandedGridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            Brush brush = null;
            Rectangle r = e.Bounds;
            int year = 0;
            Color c1 = Color.FromArgb(255, 121, 121);
            Color c2 = Color.FromArgb(255, 121, 121);
            object dr = this.bandedGridView1.GetDataRow(e.RowHandle);
            if (dr == null)
                return;
            if (gridControl1.DataSource == null)
                return;
            DataTable datrmp = gridControl1.DataSource as DataTable;

            //BurdenMonth bl = (BurdenMonth)dr;
            double imax = 0;
            double j = 0;
           foreach(DataColumn comstr in datrmp.Columns) 
            {
                if (!comstr.ColumnName.Contains("Year"))
                    continue;
                j = Convert.ToDouble(datrmp.Rows[0][comstr.ColumnName]);
                if (imax < j)
                    imax = j;
            }
            if (e.Column.FieldName.Contains("Year"))
            {
                if (imax.ToString() == e.CellValue.ToString())
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c1, c2, 180);
                    if (brush != null)
                    {
                        e.Graphics.FillRectangle(brush, r);
                    }
                }
            }
        }


    }
}