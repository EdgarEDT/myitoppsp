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
    public partial class FormPowerView : FormBase
    {
        Hashtable ht = new Hashtable();
        string projectUID = ""; 
        int firstyear = 2000;
        int endyear = 2008;
        //单位
        string Untis = string.Empty;
        public string ProjectUID
        {
            set { projectUID = value; }
        }

        public Hashtable HT
        {
            set { ht = value; }
        }

        public FormPowerView()
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
            py.Col4 = "电力发展实绩";
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
            dt.Columns.Add("Title");
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = 1;
            psp_Type.Col4 = projectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            DataTable dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));

            DataRow[] rows1 = dataTable.Select("Title like '全社会用电量%'");
            if (rows1.Length==0)
            {
                MessageBox.Show("缺少‘全社会用电量’ 数据,无法进行统计!");
                this.Close();
                return;
            }
            string pid = rows1[0]["ID"].ToString();
            string tempTite=rows1[0]["Title"].ToString();
            //取出标题中的单位
            Untis = Historytool.FindUnits(tempTite);
            DataRow[] rows3 = dataTable.Select("ParentID='"+pid+"'");
            if (rows3.Length==0)
            {
                MessageBox.Show("缺少‘全社会用电量’下的分行业用电数据,无法进行统计!");
                this.Close();
                return;
            }

            int m=-1;
            for (int i = firstyear; i <= endyear; i++)
            {
                if (!ht.ContainsValue(i))
                    continue;

                m++;
                dt.Columns.Add("y" + i, typeof(double));
                dt.Columns.Add("n" + i, typeof(double));

                GridBand gb = new GridBand();
                gb.Caption = i + "年";
                gb.AppearanceHeader.Options.UseTextOptions = true;
                gb.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
               
                this.bandedGridView1.Bands.Add(gb);

                GridBand gb1 = new GridBand();
                if (Untis.Length>0)
                {
                    gb1.Caption = "用电量("+Untis+")";
                }
                else
                {
                    gb1.Caption = "用电量";
                }
                gb1.AppearanceHeader.Options.UseTextOptions = true;
                gb1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gb.Children.Add(gb1);

                GridBand gb2 = new GridBand();
                gb2.Caption = "百分比";
                gb2.AppearanceHeader.Options.UseTextOptions = true;
                gb2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gb.Children.Add(gb2);


                BandedGridColumn gridColumn = new BandedGridColumn();
                gridColumn.Caption = i+"年用电量";
                gridColumn.FieldName = "y" + i;
                gridColumn.Visible = true;
                gridColumn.VisibleIndex = 2*m+10;
                gridColumn.DisplayFormat.FormatString = "n2";
                gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridColumn.Width = 95;
                gb1.Columns.Add(gridColumn);

                BandedGridColumn gridColumn1 = new BandedGridColumn();
                gridColumn1.Caption = i+"年百分比";
                gridColumn1.FieldName = "n" + i;
                gridColumn1.Visible = true;
                gridColumn1.VisibleIndex = 2 * m + 11;
                gridColumn1.DisplayFormat.FormatString = "n2";
                gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridColumn1.Width = 80;
                gb2.Columns.Add(gridColumn1);
            }

                double sum = 0;
                try { sum = Convert.ToDouble(rows1[0]["y" + firstyear]); }
                    catch { }


                DataRow row1 = dt.NewRow();
                row1["ID"] = Guid.NewGuid().ToString();
                row1["Title"] = "用电量总计";//rows1[0]["Title"].ToString();
            for (int k = 0; k < rows3.Length; k++)
            {

                

                DataRow row = dt.NewRow();
                row["ID"] = Guid.NewGuid().ToString();
                row["Title"] = rows3[k]["Title"].ToString();

                for (int j = firstyear; j <= endyear; j++)
                {
                    if (!ht.ContainsValue(j))
                        continue;
                    sum = 0;
                    try {
                        sum = Convert.ToDouble(rows1[0]["y" + j]); }
                    catch { }

                    row1["y" + j] = sum;
                    row1["n" + j] = 1;
                    double sum1 = 0;
                    double sum2 = 0;
                    try { sum1 = Convert.ToDouble(rows3[k]["y" + j]); }
                    catch { }
                    row["y" + j] = sum1;
                    if (sum != 0)
                        sum2 = sum1 / sum;
                    row["n" + j] = sum2;
                }

                dt.Rows.Add(row);
            }
            dt.Rows.Add(row1);



            this.gridControl1.DataSource = dt;


        
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
            ComponentPrint.ShowPreview(this.gridControl1, this.bandedGridView1.GroupPanelText);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportToExcelOld(this.bandedGridView1.GroupPanelText, Untis, this.gridControl1);
        }
    }
}