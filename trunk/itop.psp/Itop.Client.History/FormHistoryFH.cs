using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Itop.Common;
using Itop.Client.Base;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Common;
using Itop.Domain.Forecast;
using DevExpress.XtraEditors.Repository;
using Itop.Client.Using;
using Itop.Domain.Table;
using System.IO;
using DevExpress.Utils;
using Dundas.Charting.WinControl;
namespace Itop.Client.History
{
    public partial class FormHistoryFH : FormBase
    {
        //当使用默认分类管理时用于控制当前窗体
        public static FormHistoryFH Historyhome = new FormHistoryFH();
        public IList<Ps_History> listTypes = null;
        public string type1 = "7";
        public int type = 7;
        private string yearflag = "电力发展实绩负荷";
        public DataTable dataTable = new DataTable();
        bool bLoadingData = false;
        bool _canEdit = true;
        int firstyear = 2000;
        int endyear =2008;
        bool isdoubleedit = false;
        /// <summary>
        /// 图形类型
        /// line  线型
        /// 
        /// </summary>
        string charttypestring = "line";
        string isfirst = "yes";
        /// <summary>
        /// 图形背景色
        /// </summary>
        Color chartbackcolor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
        public FormHistoryFH()
        {
            InitializeComponent();
        }
        class chart_check
        {
           public  string keyname = string.Empty;
           public  bool keyvalue = false;
        }
        List<chart_check> chartchecklist = new List<chart_check>();

        private void HideToolBarButton()
        {

        }

        private void InitForm()
        {
            barButtonItem1.Glyph = Itop.ICON.Resource.新建;
            barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem3.Glyph = Itop.ICON.Resource.修改;
            barButtonItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem2.Glyph = Itop.ICON.Resource.删除;
            barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barSubItem1.Glyph = Itop.ICON.Resource.工具;
            barSubItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barSubItem3.Glyph = Itop.ICON.Resource.发送;
            barSubItem3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem22.Glyph = Itop.ICON.Resource.授权;
            barButtonItem22.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem4.Glyph = Itop.ICON.Resource.刷新;
            barButtonItem4.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem6.Glyph = Itop.ICON.Resource.关闭;
            barButtonItem6.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
        }


        private void Form8Forecast_Load(object sender, EventArgs e)
        {
            InitForm();
            Application.DoEvents();
            Ps_YearRange py=new Ps_YearRange();
            py.Col4=yearflag;
            py.Col5=ProjectUID;

            IList<Ps_YearRange> li = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);
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
                Services.BaseService.Create<Ps_YearRange>(py);
            }
            if (!AddRight)
            {
                barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!EditRight)
            {
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem22.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!DeleteRight)
            {
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

      
             LoadData();
             RefreshChart();
             Historyhome = this;
             
        }

        private void LoadData()
        {
            //this.splitContainerControl1.SplitterPosition = (2* this.splitterControl1.Width) / 3;
            //this.splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;
            bLoadingData = true;
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }
            AddFixColumn();
            for (int i = firstyear; i <= endyear; i++)
            {
                AddColumn(i);
            }

            Hashtable hasone = new Hashtable();

            Hashtable hastwo = new Hashtable();

            hasone.Add("全社会最大负荷（MW）", "全社会最大负荷（MW）");

            hasone.Add("网供电大负荷（MW）", "网供电大负荷（MW）");


           

            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = type;
            psp_Type.Col4 = ProjectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);

            foreach (string key in hasone.Keys)
            {
                bool bl = true;
                foreach (Ps_History ph in listTypes)
                {
                    if (key == ph.Title)
                        bl = false;
                }
                if (bl)
                {
                    Ps_History pf = new Ps_History();
                    pf.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;
                    pf.Forecast = type;
                    pf.ForecastID = type1;
                    pf.Title = key;
                    pf.Col4 = ProjectUID;
                    object obj = Services.BaseService.GetObject("SelectPs_HistoryMaxID", pf);
                    if (obj != null)
                        pf.Sort = ((int)obj) + 1;
                    else
                        pf.Sort = 1;
                    Services.BaseService.Create<Ps_History>(pf);
                    listTypes.Add(pf);
                    int m = 0;
                    if (hastwo.ContainsKey(key))
                    {
                        ArrayList temlist = (ArrayList)hastwo[key];
                        foreach (string title in temlist)
                        {
                            m++;
                            foreach (Ps_History ph in listTypes)
                            {
                                if (title == ph.Title)
                                    bl = false;
                            }
                            if (bl)
                            {
                                Ps_History pfchild = new Ps_History();
                                pfchild.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;
                                pfchild.Forecast = type;
                                pfchild.ForecastID = type1;
                                pfchild.Title = title;
                                pfchild.Col4 = ProjectUID;
                                pfchild.ParentID = pf.ID;
                                object objchild = Services.BaseService.GetObject("SelectPs_HistoryMaxID", pfchild);
                                if (obj != null)
                                    pfchild.Sort = ((int)obj) + 1 + m;
                                else
                                    pfchild.Sort = 1;
                                Services.BaseService.Create<Ps_History>(pfchild);
                                listTypes.Add(pfchild);
                            }

                        }

                    }
                }


            }
            dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));

            treeList1.BeginInit();
            treeList1.DataSource = dataTable;
            treeList1.Columns["Sort"].SortOrder = SortOrder.Ascending;
            treeList1.EndInit();
            Application.DoEvents();
            treeList1.ExpandAll();

            bLoadingData = false;
        }
        public void LoadData1()
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
            }

            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = type;
            psp_Type.Col4 = ProjectUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
            treeList1.DataSource = dataTable;

        }
        //添加固定列
        private void AddFixColumn()
        {
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "分类名";
            column.VisibleIndex = 0;
            column.Width = 210;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "Sort";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "ForecastID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "Forecast";
            column.VisibleIndex =-1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "ParentID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "Col10";
            column.VisibleIndex = -1;

            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
        }
        //添加年份后，新增一列
        private void AddColumn(int year)
        {
            TreeListColumn column = new TreeListColumn();

            column.FieldName ="y" +year ;
            column.Tag = year;
            column.Caption = year + "年";
            column.Name = year.ToString();
            column.Width = 80;
            //column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            column.VisibleIndex = year;//有两列隐藏列
            
            // 
            // repositoryItemTextEdit1
            //
            RepositoryItemTextEdit repositoryItemTextEdit1 = new RepositoryItemTextEdit();
            repositoryItemTextEdit1.AutoHeight = false;
            repositoryItemTextEdit1.DisplayFormat.FormatString = "n2";
            repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            repositoryItemTextEdit1.Mask.EditMask = "n2";
            repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            column.ColumnEdit = repositoryItemTextEdit1;
            //column.DisplayFormat.FormatString = "#####################0.##";
            //column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            column.Format.FormatString = "#####################0.##";
            column.Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

        }

        private bool chartcheck_has(string tempvalue)
        {
            bool has = false;
            for (int i = 0; i < chartchecklist.Count; i++)
            {
                if (chartchecklist[i].keyname==tempvalue)
                {
                    has = chartchecklist[i].keyvalue;
                    break;
                }
            }
            return has;
        }
        private int int_chart_check()
        {
            int m = 0;
            for (int i = 0; i < chartchecklist.Count; i++)
            {
                if (chartchecklist[i].keyvalue)
                {
                    m++;
                }
            }
            return m;
        }
        private void chartcheck_value(string tempvalue, bool tempbool)
        {
            bool has = false;
            for (int i = 0; i < chartchecklist.Count; i++)
            {
                if (chartchecklist[i].keyname == tempvalue)
                {
                    chartchecklist[i].keyvalue = tempbool ;
                    has = true;
                    break;
                }
            }
            if (!has)
            {
                chart_check tempcc = new chart_check();
                tempcc.keyname = tempvalue;
                tempcc.keyvalue = tempbool;
                chartchecklist.Add(tempcc);
            }
        }
        private void CopyBaseColor(FORBaseColor bc1, FORBaseColor bc2)
        {
            bc1.UID = bc2.UID;
            bc1.Title = bc2.Title;
            bc1.Remark = bc2.Remark;
            bc1.Color = bc2.Color;
            bc1.Color1 = ColorTranslator.FromOle(bc2.Color);
    
        }
        private ArrayList colorlist()
        {
            ArrayList ht = new ArrayList();
            ht.Add(Color.Red);
            ht.Add(Color.Blue);
            ht.Add(Color.Green);
            ht.Add(Color.Yellow);
            ht.Add(Color.HotPink);
            ht.Add(Color.LawnGreen);
            ht.Add(Color.Khaki);
            ht.Add(Color.LightSlateGray);
            ht.Add(Color.LightSeaGreen);
            ht.Add(Color.Lime);
            ht.Add(Color.Black);
            ht.Add(Color.Brown);
            ht.Add(Color.Crimson);
            return ht;
        }
      
        private ArrayList chartbasecolor ()
        {
            ArrayList basecolor = colorlist();
            IList<FORBaseColor> list = Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByWhere", "Remark='电力发展实绩-" + ProjectUID + "-1'");
            IList<FORBaseColor> li = new List<FORBaseColor>();
            bool bl = false;
            ArrayList hs = new ArrayList();
            int m = 0;
            ArrayList aldatablr = new ArrayList();
            foreach (DataRow row in dataTable.Rows)
            {
                aldatablr.Add(row["ID"].ToString());
            }
            foreach (DataRow row in dataTable.Rows)
            {
                if (aldatablr.Contains(row["ParentID"].ToString()))
                    continue;
                bl = false;
                foreach (FORBaseColor bc in list)
                {
                    if (row["Title"].ToString() == bc.Title)
                    {
                        bl = true;
                        FORBaseColor bc1 = new FORBaseColor();
                        bc1.Color = 0;
                        //bc1.Color1 = bc.Color1;
                        bc1.Color1 = ColorTranslator.FromOle(bc.Color);
                        //CopyBaseColor(bc1, bc);
                        li.Add(bc1);

                    }


                }
                if (!bl)
                {
                    FORBaseColor bc1 = new FORBaseColor();
                    bc1.UID = Guid.NewGuid().ToString();

                    bc1.Remark = "电力发展实绩-" + ProjectUID + "-1";
                    bc1.Title = row["Title"].ToString();
                    if (m == 0)
                    {
                        Random rd = new Random();
                        m = rd.Next(100);
                    }
                    Color cl = (Color)basecolor[m % 13];
                    bc1.Color = ColorTranslator.ToOle(cl);

                    bc1.Color1 = cl;
                    Services.BaseService.Create<FORBaseColor>(bc1);
                    li.Add(bc1);
                }
                m++;

            }

            foreach (FORBaseColor bc2 in li)
            {
                 hs.Add(bc2.Color1);
            }

            return hs;
           
            //ArrayList al = new ArrayList();
            //al.Add(Application.StartupPath + "/img/1.ico");
            //al.Add(Application.StartupPath + "/img/2.ico");
            //al.Add(Application.StartupPath + "/img/3.ico");
            //al.Add(Application.StartupPath + "/img/4.ico");
            //al.Add(Application.StartupPath + "/img/5.ico");
            //al.Add(Application.StartupPath + "/img/6.ico");
            //al.Add(Application.StartupPath + "/img/7.ico");
        }
        private List<Ps_History> chartlistvalue()
        {
            List<Ps_History> listValues = new List<Ps_History>();

            for (int i = 0; i < treeList1.Nodes.Count; i++)
            {
                TreeListNode row = treeList1.Nodes[i];
                foreach (TreeListColumn col in treeList1.Columns)
                {
                    if (col.FieldName.IndexOf("y") > -1)
                    {
                        object obj = row[col.FieldName];
                        if (obj != DBNull.Value)
                        {
                            Ps_History v = new Ps_History();
                            v.ForecastID = type1;
                            v.ID = (string)row["ID"];
                            v.Title = row["Title"].ToString();
                            v.Sort = Convert.ToInt32(col.FieldName.Replace("y", ""));
                            v.y1990 = (double)row[col.FieldName];
                            listValues.Add(v);
                        }
                    }
                }
            }
            return listValues;
        }
        //更新图形
        public void RefreshChart()
        {
            ArrayList chartcolor = chartbasecolor();
            this.chart_user1.RefreshChart(chartlistvalue(), "Title", "Sort", "y1990", chartcolor);
        }
        
       
        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf=new SaveFileDialog();
            sf.Filter = "JPEG文件(*.jpg)|*.jpg|BMP文件(*.bmp)|*.bmp|PNG文件(*.png)|*.png";
            if(sf.ShowDialog()!=DialogResult.OK)
                return;

            Dundas.Charting.WinControl.ChartImageFormat ci = new Dundas.Charting.WinControl.ChartImageFormat();
            switch(sf.FilterIndex)
            {
                case 0:
                    ci = Dundas.Charting.WinControl.ChartImageFormat.Jpeg;
                    break;

                    case 1:
                        ci = Dundas.Charting.WinControl.ChartImageFormat.Bmp;
                    break;

                    case 2:
                        ci = Dundas.Charting.WinControl.ChartImageFormat.Png;
                    break;
            
            
            
            }
            this.chart_user1.chart1.SaveAsImage(sf.FileName, ci);
        }
        //图表颜色管理
        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormColor fc = new FormColor();
            fc.DT = dataTable;
            fc.ID = "电力发展实绩" + "-" + ProjectUID;
            fc.For =1;
            if (fc.ShowDialog() == DialogResult.OK)
                RefreshChart();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //FileClass.ExportToExcelOld(this.forecastReport.Title, "", this.gridControl1);
            FormResult fr = new FormResult();
            fr.LI = this.treeList1;
            fr.Text = this.Text;
            fr.ShowDialog();
        }

        private void Save()
        {
            
            //保存

            foreach (DataRow dataRow in dataTable.Rows)
            {

                try
                {
                    Ps_History v = Itop.Common.DataConverter.RowToObject<Ps_History>(dataRow);
                    Services.BaseService.Update("UpdatePs_HistoryByID", v);
                    
                }
                catch { }
            }
        }

       

    
        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //表格数据发生变化
            if (e.Column.FieldName.IndexOf("y") >= 0)
            {
                CalculateSum(e.Node, e.Column);
                RefreshChart();
                //loadGrid();
            }
        }
        public void loadGrid()
        {

            treeList1.DataSource = null;

            IList<Base_Data> li1 = Common.Services.BaseService.GetStrongList<Base_Data>();
            ArrayList al = new ArrayList();
            foreach (Base_Data bd in li1)
                al.Add(bd.Title);

            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = type;
            psp_Type.Col4 = ProjectUID;
            //IList<Ps_History> 
            listTypes.Clear();    
                listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);


            for (int c = 0; c < al.Count; c++)
            {
                bool bl = true;
                foreach (Ps_History ph in listTypes)
                {
                    if (al[c].ToString() == ph.Title)
                        bl = false;
                }
                if (bl)
                {
                    Ps_History pf = new Ps_History();
                    pf.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;
                    pf.Forecast = type;
                    pf.ForecastID = type1;
                    pf.Title = al[c].ToString();
                    pf.Col4 = ProjectUID;
                    object obj = Services.BaseService.GetObject("SelectPs_HistoryMaxID", pf);
                    if (obj != null)
                        pf.Sort = ((int)obj) + 1;
                    else
                        pf.Sort = 1;
                    Services.BaseService.Create<Ps_History>(pf);
                    listTypes.Add(pf);
                }
            }
            string sql00 = " Title like '全社会最大负荷利用小时%' and Col4='" + ProjectUID + "' and Forecast=" + type;
            Ps_History p00 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql00);
            string sql01 = " Title like '网供最大负荷利用小时%' and Col4='" + ProjectUID + "' and Forecast=" + type;
            Ps_History p01 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql01);

            if (p00 == null || p01 == null)
            {
                string sql = " Title like '全社会用电量%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql);
                string sql2 = " Title like '全社会最大负荷%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p2 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql2);
                string sql3 = " Title like '网供电量%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p3 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql3);
                string sql4 = " Title like '网供最大负荷%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p4 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql4);
                if (p1 != null && p2 != null && p3 != null && p4 != null)
                {
                    p00 = new Ps_History();
                    p00.ID = Guid.NewGuid().ToString();
                    p00.Title = "全社会最大负荷利用小时";
                    p00.Sort = 90;
                    p00.Forecast = type;
                    p00.ForecastID = type.ToString();
                    p00.ParentID = "";
                    p00.y2005 = p1.y2005 / p2.y2005;
                    p00.y2006 = p1.y2006 / p2.y2006;
                    p00.y2007 = p1.y2007 / p2.y2007;
                    p00.y2008 = p1.y2008 / p2.y2008;
                    p00.y2009 = p1.y2009 / p2.y2009;
                    p00.Col4 = ProjectUID;
                    Services.BaseService.Create<Ps_History>(p00);
                    p01 = new Ps_History();
                    p01.ID = Guid.NewGuid().ToString();
                    p01.Title = "网供最大负荷利用小时";
                    p01.Sort = 91;
                    p01.Forecast = type;
                    p01.ForecastID = type.ToString();
                    p01.ParentID = "";
                    p01.y2005 = p3.y2005 / p4.y2005;
                    p01.y2006 = p3.y2006 / p4.y2006;
                    p01.y2007 = p3.y2007 / p4.y2007;
                    p01.y2008 = p3.y2008 / p4.y2008;
                    p01.y2009 = p3.y2009 / p4.y2009;
                    p01.Col4 = ProjectUID;
                    Services.BaseService.Create<Ps_History>(p01);
                }
                listTypes.Add(p00);
                listTypes.Add(p01);
            }
            else
            {
                string sql = " Title like '全社会用电量%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p1 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql);
                string sql2 = " Title like '全社会最大负荷%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p2 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql2);
                string sql3 = " Title like '网供电量%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p3 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql3);
                string sql4 = " Title like '网供最大负荷%' and Col4='" + ProjectUID + "' and Forecast=" + type;
                Ps_History p4 = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", sql4);
                if (p1 != null && p2 != null && p3 != null && p4 != null)
                {
                    p00 = new Ps_History();
                    p00.ID = Guid.NewGuid().ToString();
                    p00.Title = "全社会最大负荷利用小时";
                    p00.Sort = 90;
                    p00.Forecast = type;
                    p00.ForecastID = type.ToString();
                    p00.ParentID = "";
                    p00.y2005 = p1.y2005 / p2.y2005;
                    p00.y2006 = p1.y2006 / p2.y2006;
                    p00.y2007 = p1.y2007 / p2.y2007;
                    p00.y2008 = p1.y2008 / p2.y2008;
                    p00.y2009 = p1.y2009 / p2.y2009;
                    p00.Col4 = ProjectUID;
                    Services.BaseService.Update<Ps_History>(p00);
                    p01 = new Ps_History();
                    p01.ID = Guid.NewGuid().ToString();
                    p01.Title = "网供最大负荷利用小时";
                    p01.Sort = 91;
                    p01.Forecast = type;
                    p01.ForecastID = type.ToString();
                    p01.ParentID = "";
                    p01.y2005 = p3.y2005 / p4.y2005;
                    p01.y2006 = p3.y2006 / p4.y2006;
                    p01.y2007 = p3.y2007 / p4.y2007;
                    p01.y2008 = p3.y2008 / p4.y2008;
                    p01.y2009 = p3.y2009 / p4.y2009;
                    p01.Col4 = ProjectUID;
                    Services.BaseService.Update<Ps_History>(p01);
                }
                //listTypes.Add(p00);
                //listTypes.Add(p01);
            }
            /**/


            dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
            treeList1.DataSource = dataTable;
            Application.DoEvents();
            treeList1.ExpandAll();
        }
        //增加子分类
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }

            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加分类";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_History pf = new Ps_History();
                pf.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;
                pf.Forecast = type;
                pf.ForecastID = type1;
                pf.Title = frm.TypeTitle;
                pf.ParentID = focusedNode["ID"].ToString();
                pf.Col4 = ProjectUID;
                object obj = Services.BaseService.GetObject("SelectPs_HistoryMaxID", pf);
                if (obj != null)
                    pf.Sort = ((int)obj) + 1;
                else
                    pf.Sort = 1;

                try
                {
                    Services.BaseService.Create<Ps_History>(pf);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()));
                    
                }
                catch (Exception ex) { MsgBox.Show("增加分类出错：" + ex.Message); }
             RefreshChart();
            }
            
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
                return;
            if (tln.GetValue("Col10").ToString()=="1")
            {
                if (MessageBox.Show(tln.GetValue("Title").ToString() + "  分类为默认类别，删除将会对数据统计及报表产生重大影响！！\n\n                               请确认","警告",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
                
            }
            if (tln.HasChildren)
            {
                if (MsgBox.ShowYesNo(tln.GetValue("Title") + "该分类有子类，如果删除将会同子类一起删除" + "？") == DialogResult.Yes)
                {
                    DeleteNode(tln);
                }
                else
                {
                    return;
                }

            }
            else
            {
                if (MsgBox.ShowYesNo("是否删除分类 " + tln.GetValue("Title") + "？") == DialogResult.Yes)
                {
                    DeleteNode(tln);
                }
                else
                {
                    return;
                }
            }

        }
        //删除结点
        public void DeleteNode(TreeListNode tln)
        {
            if (tln.HasChildren)
            {
                for (int i = 0; i < tln.Nodes.Count; i++)
                {
                    DeleteNode(tln.Nodes[i]);
                }
                DeleteNode(tln);
            }
            else
            {
                Ps_History pf = new Ps_History();
                pf.ID = tln["ID"].ToString();
                 string nodestr = tln["Title"].ToString();
                try
                {
                    TreeListNode node = tln.TreeList.FindNodeByKeyID(pf.ID);
                    if (node != null)
                        tln.TreeList.DeleteNode(node);
                    RemoveDataTableRow(dataTable, pf.ID);
                    Common.Services.BaseService.Delete<Ps_History>(pf);
                    //如果是一类结点，删除颜色列表中对应的值
                    if (node.ParentNode==null)
                    {
                        string constr = "  Title='" + nodestr + "'  and Remark='电力发展实绩-" + ProjectUID + "-1'";
                        IList<FORBaseColor> fbclist=Common.Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByConn",constr);
                        if (fbclist.Count>0)
                        {
                            for (int i = 0; i < fbclist.Count; i++)
			                {
                             Common.Services.BaseService.Delete<FORBaseColor>(fbclist[i]);
			                }
                       
                        }
                    }
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message + "删除结点出错！");
                }
                RefreshChart();
            }


        }
        public void RemoveDataTableRow(DataTable dt, string ID)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ID"].ToString()==ID)
                {
                    dt.Rows.RemoveAt(i);
                    break;
                }
            }
        }

        


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //修改
            if (treeList1.FocusedNode == null)
                return;

            if (treeList1.FocusedNode.GetValue("Col10").ToString() == "1")
            {
                MsgBox.Show("该类别为默认类别，不可修改！");
                return;
            }

            string nodestr = treeList1.FocusedNode.GetValue("Title").ToString();
            FormTypeTitle frm = new FormTypeTitle();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "修改分类";
            string strid=treeList1.FocusedNode["ID"].ToString();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //Ps_History v = new Ps_History();
                //TreeNodeToDataObject<Ps_History>(v, treeList1.FocusedNode);
                Ps_History v = Common.Services.BaseService.GetOneByKey<Ps_History>(strid);
                v.Title = frm.TypeTitle;
                v.ID = strid;
                try
                {
                    //如果修改一类结点，则改变相应的颜色列表
                    if (treeList1.FocusedNode.ParentNode==null)
                    {
                        string constr = "  Title='" + nodestr + "'  and Remark='电力发展实绩-" + ProjectUID + "-1'";
                        IList<FORBaseColor> fbclist=Common.Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByConn",constr);
                        if (fbclist.Count>0)
                        {       
                            fbclist[0].Title=frm.TypeTitle;
                            Common.Services.BaseService.Update<FORBaseColor>(fbclist[0]);
                        }
                    }
                    Common.Services.BaseService.Update<Ps_History>(v);
                    treeList1.FocusedNode.SetValue("Title", frm.TypeTitle);
                }
                catch { }
             RefreshChart();

            }




        }

        static public void TreeNodeToDataObject<T>(T dataObject, DevExpress.XtraTreeList.Nodes.TreeListNode treeNode)
        {
            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if(pi.Name.Substring(0,1)=="y")
                pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);
            }
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList1.FocusedNode.HasChildren)
            {
                e.Cancel = true;
            }
            if (!EditRight)
            {
                e.Cancel = true;
            }
        }


        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            Ps_History v = Services.BaseService.GetOneByKey<Ps_History>(node["ID"].ToString());
            TreeNodeToDataObject<Ps_History>(v, node);

            Common.Services.BaseService.Update<Ps_History>(v);


            TreeListNode parentNode = node.ParentNode;

            if (parentNode == null)
            {
                return;
            }

            double sum = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            {
                object value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }
            if (sum != 0)
            {
                parentNode.SetValue(column.FieldName, sum);
                v = Services.BaseService.GetOneByKey<Ps_History>(parentNode["ID"].ToString());
                TreeNodeToDataObject<Ps_History>(v, parentNode);

                Common.Services.BaseService.Update<Ps_History>(v);
            }
            else
                parentNode.SetValue(column.FieldName, 0);
            CalculateSum(parentNode, column);
        }
       
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormYearSet fys = new FormYearSet();
            fys.TYPE = yearflag;
            fys.PID = ProjectUID;
            if (fys.ShowDialog() != DialogResult.OK)
                return;
            firstyear = fys.SYEAR;
            endyear = fys.EYEAR;
            LoadData();
            RefreshChart();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormProjectDataCopy fpd = new FormProjectDataCopy();
            fpd.ProjectUID = ProjectUID;

            if (fpd.ShowDialog() != DialogResult.OK)
                return;

            string pid = fpd.ProjectUID;

            Hashtable ht1 = new Hashtable();
            Hashtable ht2 = new Hashtable();
            Hashtable ht3 = new Hashtable();



            Ps_History psp_Type1 = new Ps_History();
            psp_Type1.Forecast = type;
            psp_Type1.Col4 = pid;
            IList<Ps_History> li1 = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type1);



            foreach (Ps_History ph in li1)
            {

                string tempid = ph.ID;
                if (ht1.ContainsKey(tempid))
                {
                    ph.ID = ht1[tempid].ToString();
                }
                else
                {
                    ph.ID = Guid.NewGuid().ToString();
                    ht1.Add(tempid, ph.ID);
                }
                if (ph.ParentID != "0")
                {
                    if (ht1.ContainsKey(ph.ParentID))
                    {
                        ph.ParentID = ht1[ph.ParentID].ToString();
                    }
                    else
                    {
                        string tempid2 = Guid.NewGuid().ToString();
                        ht1.Add(ph.ParentID, tempid2);
                        ph.ParentID = tempid2;
                    }
                }
                ph.Col4 = ProjectUID;
                Services.BaseService.Create<Ps_History>(ph);
            }
            //IList<Ps_History> li2 = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type2);
            LoadData1();
            //b();
            RefreshChart();

        }




        private void b()
        {
            foreach (TreeListNode tlnn in treeList1.Nodes)
            {
                aaa(tlnn);
            }

        }



        private void aaa(TreeListNode tln)
        {
           
                if (tln.Nodes.Count == 0)
                    CalculateSum2(tln);
                else
                {
                    foreach (TreeListNode tl in tln.Nodes)
                    {
                        aaa(tl);
                    }
                }
            

         
           
        }


        //当子分类数据改变时，计算其父分类的值
        private void CalculateSum2(TreeListNode node, TreeListColumn column)
        {
            TreeListNode parentNode = node.ParentNode;

            if (parentNode == null)
            {
                return;
            }

            double sum = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            {
                object value = nd.GetValue(column.FieldName);
                try
                {
                    if (value != null && value != DBNull.Value)
                    {
                        sum += Convert.ToDouble(value);
                    }
                }

                catch (Exception ex)
                {
                    string str = ex.Message;
                }
            }
            parentNode.SetValue(column.FieldName, sum);

            CalculateSum2(parentNode, column);
        }

        //计算指定节点的各列各
        private void CalculateSum2(TreeListNode node)
        {
            foreach (TreeListColumn column in treeList1.Columns)
            {
                if (column.FieldName.IndexOf("y") >= 0)
                {
                    //CalculateSum2(node, column);
                    CalculateSum(node, column);
                }
            }
        }
        //添加一级分类
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加分类";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_History pf = new Ps_History();
                pf.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;
                pf.Forecast = type;
                pf.ForecastID = type1;
                pf.Title = frm.TypeTitle;
                pf.Col4 = ProjectUID;
                object obj = Services.BaseService.GetObject("SelectPs_HistoryMaxID", pf);
                if (obj != null)
                    pf.Sort = ((int)obj) + 1;
                else
                    pf.Sort = 1;
                //一级分类图表的颜色
                ArrayList ht = new ArrayList();
                ht.Add(Color.Red);
                ht.Add(Color.Blue);
                ht.Add(Color.Green);
                ht.Add(Color.Yellow);
                ht.Add(Color.HotPink);
                ht.Add(Color.LawnGreen);
                ht.Add(Color.Khaki);
                ht.Add(Color.LightSlateGray);
                ht.Add(Color.LightSeaGreen);
                ht.Add(Color.Lime);
                FORBaseColor bc1 = new FORBaseColor();
                bc1.UID = Guid.NewGuid().ToString();

                bc1.Remark = "电力发展实绩-" + ProjectUID + "-1";
                bc1.Title = pf.Title;
                Random rd=new Random ();
                int m = rd.Next(100);
                Color cl = (Color)ht[m % 10];
                bc1.Color = cl.ToArgb();
                bc1.Color1 = cl;
                try
                {
                    Services.BaseService.Create<Ps_History>(pf);
                    Services.BaseService.Create<FORBaseColor>(bc1);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()));
                    RefreshChart();
                    
                }
                catch (Exception ex) { MsgBox.Show("增加分类出错：" + ex.Message); }
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
                return;
            Ps_History psp_Type2 = new Ps_History();
            psp_Type2.Forecast = type;
            psp_Type2.Col4 = ProjectUID;
            Services.BaseService.Update("DeletePs_HistoryBy", psp_Type2);
            dataTable.Clear();
            treeList1.Nodes.Clear();
            
            //LoadData();
            //b();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Insertdata();
           
        }
        private DataTable GetExcel(string filepach)
        {
            
            FarPoint.Win.Spread.FpSpread fpSpread1 = new FarPoint.Win.Spread.FpSpread();

            try
            {
                fpSpread1.OpenExcel(filepach);
            }
            catch
            {
                string filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName(filepach);
                File.Copy(filepach, filepath1);
                fpSpread1.OpenExcel(filepath1);
                File.Delete(filepath1);
            }
            DataTable dt = new DataTable();
            Hashtable h1 = new Hashtable();
            int aa = 0;
           
            IList<string> filedList = new List<string>();
            IList<string> capList = new List<string>();
            for (int i = 0; i < treeList1.Columns.Count; i++)
            {

                if (treeList1.Columns[i].VisibleIndex < 0)
                {
                    if (treeList1.Columns[i].FieldName == "ParentID")
                        capList.Add("父ID");
                    else
                    {
                        capList.Add(treeList1.Columns[i].FieldName);
                    }
                }
                else
                {
                    if (treeList1.Columns[i].FieldName != "Title")
                    capList.Add(treeList1.Columns[i].Caption);
                    else
                    {
                        capList.Add("项目");
                    }
                }
                
                filedList.Add(treeList1.Columns[i].FieldName);
            }

         
            int c = 0;
          
            IList<string> fie = new List<string>();
            int cn = 0;
            int gong = 65;
            int m = 3;
            string L1 = "";
            string S4 = "";
            string L2 = "";
            string AreaName = "";
            string temStr = "";
            for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
            {
                if (capList.Contains(fpSpread1.Sheets[0].Cells[2, j].Text))
            
                    fie.Add(filedList[capList.IndexOf(fpSpread1.Sheets[0].Cells[2, j].Text)]);
              
            }

            for (int k = 0; k < fie.Count; k++)
            {
                dt.Columns.Add(fie[k]);
            }
            for (int i = m; i <fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
            {

                DataRow dr = dt.NewRow();
                
                for (int j = 0,fiej =0;fiej<fie.Count&& j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
                {
             
                  //  dr[fie[j]] = fpSpread1.Sheets[0].Cells[i, j].Text.Trim();
                    if (capList.IndexOf(fpSpread1.Sheets[0].Cells[2, j].Text) < 0)
                    {
                       // fiej = j - 1;
                        continue;

                    }
                   
                   // if (fie.Contains(filedList[capList.IndexOf(fpSpread1.Sheets[0].Cells[2, j].Text)]))
                    //    dr[fie[fie.IndexOf(filedList[capList.IndexOf(fpSpread1.Sheets[0].Cells[2, j].Text)])]] = fpSpread1.Sheets[0].Cells[i, j].Text.Trim();
                    dr[fie[fiej]] = fpSpread1.Sheets[0].Cells[i, j].Text.Trim();
                    fiej ++;
                 
                }
                // GetL2(dr["L4"].ToString(), out L2);
                // dr["AreaName"] = AreaName; dr["L1"] = L1; dr["S4"] = S4; dr["L2"] = L2; dr["S3"] = ""; dr["S5"] = i.ToString();
                // if (str != "")
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private void Insertdata()
        {

            //LayoutList ll1 = this.ctrlLayoutList1.FocusedObject;
            //if (ll1 == null)
            //    return;

            string columnname = "";
           
           
            
                DataTable dts = new DataTable();
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel文件(*.xls)|*.xls";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    dts = GetExcel(op.FileName);
                    IList<Ps_History> lii = new List<Ps_History>();
                    DateTime s8 = DateTime.Now;
                    int x = 0;
                    WaitDialogForm wait = new WaitDialogForm("", "正在导入数据, 请稍候...");
                    try
            {
                    for (int i = 0; i < dts.Rows.Count; i++)
                    {

                       
                              this.Cursor = Cursors.WaitCursor;
                        Ps_History l1 = new Ps_History();
                        foreach (DataColumn dc in dts.Columns)
                        {
                            columnname = dc.ColumnName;
                            if (dts.Rows[i][dc.ColumnName].ToString() == "")
                                continue;

                            switch (l1.GetType().GetProperty(dc.ColumnName).PropertyType.Name)
                            {
                                case "Double":
                                    if (dts.Rows[i][dc.ColumnName] == null || dts.Rows[i][dc.ColumnName] == DBNull.Value || dts.Rows[i][dc.ColumnName].ToString() == "")
                                    {
                                        l1.GetType().GetProperty(dc.ColumnName).SetValue(l1,0, null);
                                        break;
                                    }
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, Convert.ToDouble(dts.Rows[i][dc.ColumnName]), null);
                                    break;
                                case "Int32":
                                    if (dts.Rows[i][dc.ColumnName] == null || dts.Rows[i][dc.ColumnName] == DBNull.Value || dts.Rows[i][dc.ColumnName].ToString() == "")
                                    {
                                        l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, 0, null);
                                        break;
                                    }
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, Convert.ToInt32(dts.Rows[i][dc.ColumnName]), null);
                                    break;

                                default:
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, dts.Rows[i][dc.ColumnName], null);
                                    break;
                            }
                        }
                       
                        l1.Forecast = type;
                        l1.Col4 = ProjectUID;
                        l1.ForecastID = type1;
                        lii.Add(l1);
                    }
                    int parenti = -4;
                    //foreach (Ps_History l1 in lii)
                    Ps_History psl1;
                    for (int i = 0; i < lii.Count; i++)
                    {



                        psl1 = lii[i];
                        psl1.Sort = i;
                        string con = "Col4='" + ProjectUID + "' and Title='" + psl1.Title + "' and Forecast='" + type + "' and ParentID='"+ psl1.ParentID +"'";
                        object obj = Services.BaseService.GetObject("SelectPs_HistoryByCondition", con);
                        if (obj != null)
                        {
                            psl1.ID = ((Ps_History)obj).ID;
                           // psl1.Sort = ((Ps_History)obj).Sort;
                           
                            if (psl1.ParentID.Contains("-"))
                            psl1.ParentID = ((Ps_History)obj).ParentID;
                            Services.BaseService.Update<Ps_History>(psl1);
                        }
                        else
                        {
                            psl1.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;

                            //object obj2 = Services.BaseService.GetObject("SelectPs_HistoryMaxID", psl1);
                            //if (obj2 != null)
                            //    psl1.Sort = ((int)obj2) + 1;
                            //else
                            //    psl1.Sort = 1;
                           // psl1.Sort = i;
                            Services.BaseService.Create<Ps_History>(psl1);
                        }
                        for (int j = i + 1; j < lii.Count;j++ )
                        {
                            if (lii[j].ParentID == parenti.ToString())
                            {
                                lii[j].ParentID = psl1.ID;
                            }
                        }

                        parenti--;
                    }
                    LoadData();
                    wait.Close();
                    wait = new WaitDialogForm("", "正在重新计算数据, 请稍候...");
                    for (int i = 0; i < lii.Count; i++)
                        {
                          TreeListNode nd=  treeList1.FindNodeByKeyID(lii[i].ID);
                          if (nd != null)
                          {
                              foreach(TreeListColumn tr  in treeList1.Columns)
                                  if (tr.FieldName.IndexOf("y") >= 0)
                              {
                                  CalculateSum(nd,tr);
                                
                              }
                          
                          }
                        
                        }
                        RefreshChart();
                       

                   // b();
                    this.Cursor = Cursors.Default;
                    wait.Close();
                    MsgBox.Show("导入成功！");
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    wait.Close();
                    MsgBox.Show(columnname + ex.Message); MsgBox.Show("导入格式不正确！");
                }
                }

           
        }

       

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
          //isdoubleedit = true;
        }

        private void treeList1_FocusedColumnChanged(object sender, DevExpress.XtraTreeList.FocusedColumnChangedEventArgs e)
        {
            if (e.Column==null)
            {
                isdoubleedit = false;
                return;
            }
            if (e.Column.FieldName.IndexOf("y")==0)
            {
                isdoubleedit = true;
            }
            else
            {
                isdoubleedit = false;
            }
           
        }

        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

      

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             //默认类别管理
       
            FormHistoryType frm = new FormHistoryType("6");
            List<string> templist = new List<string>();
            //把当前类别ID放入templist
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                templist.Add(dataTable.Rows[i]["ID"].ToString());
            }
            frm.ValueList = templist;
            frm.ForecastID = type1;
            frm.Forecast = type;
   
            frm.ShowDialog();
        
        }
        

        //向上一个
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            TreeListNode node = treeList1.FocusedNode;
            int i = 0, sortj = 0, sortj2 = 0;

            if (treeList1.FocusedNode.PrevNode != null)
            {
                string ID1 = treeList1.FocusedNode.PrevNode["ID"].ToString();
                string ID2 = treeList1.FocusedNode["ID"].ToString();
                if (ID1 != ID2)
                {
                    sortj = Convert.ToInt32(treeList1.FocusedNode.PrevNode["Sort"]);
                    sortj2 = Convert.ToInt32(treeList1.FocusedNode["Sort"]);
                    if (sortj2 == sortj)
                    {
                        sortj2 = sortj2 - 1;

                    }
                    else
                    {
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }
                    Ps_History ph = Common.Services.BaseService.GetOneByKey<Ps_History>(ID1);
                    Ps_History ph2 = Common.Services.BaseService.GetOneByKey<Ps_History>(ID2);
                    if (ph != null)
                    {
                        ph.Sort = sortj;
                        Common.Services.BaseService.Update<Ps_History>(ph);
                    }
                    if (ph2 != null)
                    {
                        ph2.Sort = sortj2;
                        Common.Services.BaseService.Update<Ps_History>(ph2);
                    }
                   
                    treeList1.FocusedNode.PrevNode["Sort"] = sortj;
                    treeList1.FocusedNode["Sort"] = sortj2;
                    treeList1.BeginSort();
                    treeList1.EndSort();
                   
                }

            }
        }
        //向下一个
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            TreeListNode node = treeList1.FocusedNode;
            int i = 0, sortj = 0, sortj2 = 0;

            if (treeList1.FocusedNode.NextNode != null)
            {
                string ID1 = treeList1.FocusedNode.NextNode["ID"].ToString();
                string ID2 = treeList1.FocusedNode["ID"].ToString();
                if (ID1 != ID2)
                {
                    sortj = Convert.ToInt32(treeList1.FocusedNode.NextNode["Sort"]);
                    sortj2 = Convert.ToInt32(treeList1.FocusedNode["Sort"]);
                    if (sortj2 == sortj)
                    {
                        sortj2 = sortj2 + 1;
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }
                    else
                    {
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }

                  
                    Ps_History ph = Common.Services.BaseService.GetOneByKey<Ps_History>(ID1);
                    Ps_History ph2 = Common.Services.BaseService.GetOneByKey<Ps_History>(ID2);
                    if (ph != null)
                    {
                        ph.Sort = sortj;
                        Common.Services.BaseService.Update<Ps_History>(ph);
                    }
                    if (ph2 != null)
                    {
                        ph2.Sort = sortj2;
                        Common.Services.BaseService.Update<Ps_History>(ph2);
                    }
                    treeList1.FocusedNode.NextNode["Sort"] = sortj;
                    treeList1.FocusedNode["Sort"] = sortj2;
                    treeList1.BeginSort();
                    treeList1.EndSort();
                    treeList1.Refresh();
                }

            }
        }

      
      
      
        //社会及经济用电情况

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            

            List<int> li = new List<int>();
            for (int i = firstyear; i <= endyear; i++)
            {
                li.Add(i);
            }

            FormChooseYears1 cy = new FormChooseYears1();
            cy.ListYearsForChoose = li;
            if (cy.ShowDialog() != DialogResult.OK)
                return;
            Hashtable ht = new Hashtable();
            Hashtable ht1 = new Hashtable();
            Hashtable ht2 = new Hashtable();
            foreach (DataRow a in cy.DT.Rows)
            {
                if (a["B"].ToString() == "True")
                    ht.Add(Guid.NewGuid().ToString(), Convert.ToInt32(a["A"].ToString().Replace("年", "")));

                if (a["C"].ToString() == "True")
                    ht1.Add(Guid.NewGuid().ToString(), Convert.ToInt32(a["A"].ToString().Replace("年", "")));

                if (a["D"].ToString() == "True")
                    ht2.Add(Guid.NewGuid().ToString(), Convert.ToInt32(a["A"].ToString().Replace("年", "")));
            }
            FormHisView2 fgv = new FormHisView2();
            fgv.pstype = type;
            fgv.Text = "负荷数据统计";
            fgv.ProjectUID = ProjectUID;
            fgv.HT = ht;
            fgv.HT1 = ht1;
            fgv.HT2 = ht2;
            fgv.ShowDialog();
        

        }


      
       

        

     
    }
}   
