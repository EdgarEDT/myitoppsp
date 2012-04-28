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
    public partial class FormHisView : FormBase
    {
        Hashtable ht = new Hashtable();
        Hashtable ht1 = new Hashtable();
        Hashtable ht2 = new Hashtable();
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
        public FormHisView()
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
            psp_Type.Forecast = 1;
            psp_Type.Col4 = projectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            DataTable dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));

            DataRow[] rows1 = dataTable.Select("Title like '全地区GDP%'");
           // DataRow[] rows2 = dataTable.Select("Title like '全社会供电量%'");
            DataRow[] rows4 = dataTable.Select("Title like '全社会用电量%'");
            //DataRow[] rows5 = dataTable.Select("Title like '全社会最大负荷%'");
            DataRow[] rows7 = dataTable.Select("Title like '年末总人口%'");
            DataRow[] rows8 = dataTable.Select("Title='居民用电'");

            if (rows1.Length==0)
            {
                MessageBox.Show("缺少全地区GDP数据!");
                this.Close();
                return;
            }
            //if (rows2.Length == 0)
            //{
            //    MessageBox.Show("缺少全社会供电量数据!");
            //    this.Close();
            //}
            if (rows4.Length == 0)
            {
                MessageBox.Show("缺少全社会用电量数据!");
                this.Close();
                return;
            }
            //if (rows5.Length == 0)
            //{
            //    MessageBox.Show("缺少全社会最大负荷数据!");
            //    this.Close();
            //}
            if (rows7.Length == 0)
            {
                MessageBox.Show("缺少年末总人口数据!");
                this.Close();
                return;
            }
          


            GDPUnits = Historytool.FindUnits(rows1[0]["Title"].ToString());
            //全社会供电量单位
            //AGdlUnits = Historytool.FindUnits(rows2[0]["Title"].ToString());
            //全社会用电量单位
            AYdlUnits = Historytool.FindUnits(rows4[0]["Title"].ToString());
            //全社会最大负荷单位
           // AMaxFhUnits = Historytool.FindUnits(rows5[0]["Title"].ToString());
            //年末总人口单位
            NMARkUnits = Historytool.FindUnits(rows7[0]["Title"].ToString());

            string pid = rows1[0]["ID"].ToString();
            string sid = rows4[0]["ID"].ToString();

            ///全地区GDP子类
            DataRow[] rows3 = dataTable.Select("ParentID='"+pid+"'");
            ///会社会用电量子类
            DataRow[] rows6 = dataTable.Select("ParentID='" + sid + "'");
            int m=-1;
            this.gridControl1.BeginInit();
            this.gridControl1.BeginUpdate();
            for (int i = firstyear; i <= endyear; i++)
            {
                dt.Columns.Add("y" + i, typeof(double));
                if (!ht.ContainsValue(i))
                    continue;
                if (IsFist)
                {
                    RealFistYear = i;
                    IsFist = false;
                }
                m++;
                //dt.Columns.Add("y" + i, typeof(double));
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
                    gridColumn = new GridColumn();
                    gridColumn.Caption = "年均增长率(%)";
                    gridColumn.FieldName = "m" + i;
                    gridColumn.Visible = true;
                    gridColumn.Width = 130;
                    gridColumn.VisibleIndex = 2 * m + 11;
                    gridColumn.DisplayFormat.FormatString = "n2";
                    gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView1.Columns.Add(gridColumn);
                    dt.Columns.Add("m" + i, typeof(double));
                }

                if (ht2.ContainsValue(i))
                {

                    gridColumn = new GridColumn();
                    gridColumn.Caption = "逐年增长率(%)";
                    gridColumn.FieldName = "n" + i;
                    gridColumn.Visible = true;
                    gridColumn.Width = 130;
                    gridColumn.VisibleIndex = 2 * m + 12;
                    gridColumn.DisplayFormat.FormatString = "n2";
                    gridColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    gridView1.Columns.Add(gridColumn);
                    dt.Columns.Add("n" + i, typeof(double));

                }
            
            }

            this.gridControl1.EndUpdate();
            this.gridControl1.EndInit();
                double sum = 0;// 全地区GDP数据
                try { sum = Convert.ToDouble(rows1[0]["y" + firstyear]); }
                catch { }


                double sum51 = 0;// 全社会用电量数据
                try { sum51 = Convert.ToDouble(rows4[0]["y" + firstyear]); }
                catch { }
                double sum52 = 0;
                double sum53 = 0;

                double sum1 = 0;
                double sum2 = 0;
                double sum3 = 0;
                double sum4 = 0;
                double sum5 = 0;
                double sum6 = 0;
                double sum7 = 0; 
                double sum8 = 0;
                double sum9 = 0;
                double sum10 = 0;
                double sum11 = 0;
                double sum12 = 0;
                double sum13 = 0;
                DataRow row = dt.NewRow();
                DataRow row3 = dt.NewRow();
                DataRow row4 = dt.NewRow();
                DataRow row5 = dt.NewRow();
                DataRow row6 = dt.NewRow();
                DataRow row7 = dt.NewRow();
                DataRow row8 = dt.NewRow();
                DataRow row9 = dt.NewRow();
                DataRow row10 = dt.NewRow();
                DataRow row11 = dt.NewRow();
                row["ID"] = Guid.NewGuid().ToString();
                row["Title"] = "一、地区生产总值(GDP,"+GDPUnits+")";

                m = firstyear;

                for (int j = firstyear; j <= endyear; j++)
                {
                    //if (!ht.ContainsValue(j)) lyh
                    //    continue;
                    try { sum1 = Convert.ToDouble(rows1[0]["y" + j]); }
                    catch { }
                    row["y" + j] = sum1;

                    try { sum51 = Convert.ToDouble(rows4[0]["y" + j]); }
                    catch { }
                    
                    if (m != firstyear)//表示不是第一年，以后的年份要算增长
                    {
                        try { sum2 = Convert.ToDouble(rows1[0]["y" + (j - 1)]); }
                        catch { }

                        try { sum52 = Convert.ToDouble(rows4[0]["y" + (j - 1)]); }
                        catch { }
                        if (sum52 != 0)
                            sum53 = sum51 * 100 / sum52-100;//用电量增长


                        if (sum2 != 0)
                            sum3 = sum1 * 100 / sum2-100;//GDP增长 

                        row3["y" + j] = sum3;
                    }
                    else
                        row3["y" + j] = 1;

                    try { sum4 = Convert.ToDouble(rows4[0]["y" + j]); }
                    catch { }
                    row4["y" + j] = sum4;//用电量



                    //try { sum5 = Convert.ToDouble(rows5[0]["y" + j]); }
                    //catch { }
                    //row5["y" + j] = sum5;//最大负荷

                    //if (sum5 != 0)
                    //    sum6 = sum4 * 10000 / sum5;
                    //row6["y" + j] = sum6;// 计算全社会最大负荷利用小时数

                    //try { sum7 = Convert.ToDouble(rows2[0]["y" + j]); }
                    //catch { }


                    if (m != firstyear)
                    {
                        //if (sum53 != 0)
                        //    sum8 = sum3 / sum53;
                        //row7["y" + j] = sum8;//原计算
                        if (sum3 != 0)
                            sum8 = sum53 / sum3;
                        row7["y" + j] = sum8;//弹性系数，电力消费增长速度与国民经济增长的比值lgm
                    }
                    else
                        row7["y" + j] = 1;

                    try { sum9 = Convert.ToDouble(rows7[0]["y" + j]); }//年末人口
                    catch { }

                    if (sum9 != 0)
                    {
                        if (AYdlUnits.Contains("亿") && NMARkUnits.Contains("万人"))//亿kWh  万人
                        {
                            sum10 = sum51 * 10000 / sum9;//人均用电量
                        }
                        else if (AYdlUnits.Contains("万") && NMARkUnits.Contains("万人"))//万kWh  万人
	                    {
                            sum10 = sum51 / sum9;//人均用电量
	                    }
                        else
                        {
                            MessageBox.Show("全社会用电量或年末总人口的单位错误！请用默认类别管理较对！（亿kWh,万kWh,万人）");
                            this.Close();
                            return;
                        }
                        
                    }
                    row8["y" + j] = sum10;

                    if (sum1 != 0)
                    {
                        if (AYdlUnits.Contains("亿")&&GDPUnits.Contains("亿元"))//亿kWh 亿元
                        {
                            sum11 = sum4 * 10000 / sum1;//单产耗能
                        }
                        else if (AYdlUnits.Contains("万") && GDPUnits.Contains("亿元"))//万kWh  亿元
                        {
                            sum11 = sum4  / sum1;//单产耗能
                        }
                        else
                        {
                            MessageBox.Show("全社会用电量或全地区GDP的单位错误！请用默认类别管理较对！（亿kWh,万kWh,亿元）");
                            this.Close();
                            return;
                        }
                    }
                    row9["y" + j] = sum11;//单产耗能

                    if (rows8.Length > 0)
                    {
                        try { sum12 = Convert.ToDouble(rows8[0]["y" + j]); }
                        catch { }
                        row11["y" + j] = sum12;

                        if (sum9 != 0)
                        {
                            if (AYdlUnits.Contains("亿") && NMARkUnits.Contains("万人"))//亿kWh 万人
                            {
                                sum13 = sum12 * 10000 / sum9;//居民用电
                            }
                            else if (AYdlUnits.Contains("万") && NMARkUnits.Contains("万人"))//万kWh  万人
                            {
                                sum13 = sum12 / sum9;//居民用电
                            }
                            else
                            {
                                MessageBox.Show("全社会用电量或年末总人口的单位错误！请用默认类别管理较对！（亿kWh,万kWh,万人）");
                                this.Close();
                                return;
                            }

                        }
                        row10["y" + j] = sum13;
                    }
                    else
                    {
                        row10["y" + j] = sum13;
                    }
                    m++;
                }

                dt.Rows.Add(row);

            for (int k = 0; k < rows3.Length; k++)
            {
                double su1 = 0;
                double su2 = 0;
                double su3 = 0;
                
                

                DataRow ro1 = dt.NewRow();
                ro1["ID"] = Guid.NewGuid().ToString();
                ro1["Title"] = rows3[k]["Title"].ToString();

                DataRow ro2 = dt.NewRow();
                ro2["ID"] = Guid.NewGuid().ToString();
                ro2["Title"] = "比例（%）";

                for (int j = firstyear; j <= endyear; j++)
                {
                    if (!ht.ContainsValue(j))
                        continue;
                    try { su1 = Convert.ToDouble(rows1[0]["y" + j]); }
                    catch { }
                    su2 = 0;
                    su3 = 0;
                    try { su2 = Convert.ToDouble(rows3[k]["y" + j]); }
                    catch { }
                    ro1["y" + j] = su2;

                    if (su1 != 0)
                        su3 = su2 * 100 / su1;
                        ro2["y" + j] = su3;

                }

                dt.Rows.Add(ro1);
                dt.Rows.Add(ro2);

            }

            row3["ID"] = Guid.NewGuid().ToString();
            row3["Title"] = "地区生产总值增长率（％）";
            dt.Rows.Add(row3);


            row4["ID"] = Guid.NewGuid().ToString();
            row4["Title"] = "二、全社会用电量("+AYdlUnits+")";
            dt.Rows.Add(row4);


            //row5["ID"] = Guid.NewGuid().ToString();
            //row5["Title"] = "最大负荷（万千瓦）";
            //dt.Rows.Add(row5);

            //row6["ID"] = Guid.NewGuid().ToString();
            //row6["Title"] = "最大负荷利用小时数(小时)";
            //dt.Rows.Add(row6);



            for (int k = 0; k < rows6.Length; k++)
            {
                double su1 = 0;
                double su2 = 0;
                double su3 = 0;
                


                DataRow ro1 = dt.NewRow();
                ro1["ID"] = Guid.NewGuid().ToString();
                ro1["Title"] = rows6[k]["Title"].ToString();

                DataRow ro2 = dt.NewRow();
                ro2["ID"] = Guid.NewGuid().ToString();
                ro2["Title"] = "比例（%）";

                for (int j = firstyear; j <= endyear; j++)
                {
                    if (!ht.ContainsValue(j))
                        continue;
                    try { su1 = Convert.ToDouble(rows4[0]["y" + j]); }
                    catch { }
                    su2 = 0;
                    su3 = 0;
                    try { su2 = Convert.ToDouble(rows6[k]["y" + j]); }
                    catch { }
                    ro1["y" + j] = su2;

                    if (su1 != 0)
                        su3 = su2 * 100 / su1;
                        ro2["y" + j] = su3;

                }

                dt.Rows.Add(ro1);
                dt.Rows.Add(ro2);

            }

            row7["ID"] = Guid.NewGuid().ToString();
            row7["Title"] = "弹性系数";
            dt.Rows.Add(row7);

            row8["ID"] = Guid.NewGuid().ToString();
            row8["Title"] = "人均用电量（千瓦时/人）";
            dt.Rows.Add(row8);

            row9["ID"] = Guid.NewGuid().ToString();
            row9["Title"] = "GDP单耗（千瓦时/万元）";
            dt.Rows.Add(row9);

            for (int k = 0; k < rows6.Length; k++)
            {
                double su1 = 0;
                double su2 = 0;
                double su3 = 0;

                DataRow ro1 = dt.NewRow();
                ro1["ID"] = Guid.NewGuid().ToString();
                ro1["Title"] = rows6[k]["Title"].ToString();

                for (int j = firstyear; j <= endyear; j++)
                {
                    if (!ht.ContainsValue(j))
                        continue;
                    su1 = 0;
                    su2 = 0;
                    su3 = 0;
                    try { su1 = Convert.ToDouble(rows6[k]["y" + j]); }
                    catch { }
                    try { su2 = Convert.ToDouble(rows3[k]["y" + j]); }
                    catch { }

                    if (su2 != 0)
                    { su3 = su1 * 10000 / su2; }
                    else
                        su3 = su1;
                     ro1["y" + j] = su3; 
                }
                if (rows6[k]["Title"].ToString().IndexOf("居民") >= 0)
                    continue;
                dt.Rows.Add(ro1);

            }

            row10["ID"] = Guid.NewGuid().ToString();
            row10["Title"] = "居民用电（千瓦时/人）";
            dt.Rows.Add(row10);


            double d = 0;
            //年均增长率
            foreach(DataRow drw1 in dt.Rows)
            {
               
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.IndexOf("m") >= 0)
                    {
                        string s = dc.ColumnName.Replace("m", "");
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
                            if ( ht.ContainsValue(i))
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
                        drw1["m" + s]=sss;
                    }
                }
            }
            //逐年增长率
            double dd = 0;
            foreach (DataRow drw1 in dt.Rows)
            {
                
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.ColumnName.IndexOf("n") >= 0)
                    {
                        string s = dc.ColumnName.Replace("n", "");
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
                        drw1["n" + s] = sss;
                    }
                }
            }







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