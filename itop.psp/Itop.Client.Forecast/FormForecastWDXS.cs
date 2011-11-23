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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using Dundas.Charting.WinControl;
using DevExpress.Utils;
using Itop.Domain.Table;

namespace Itop.Client.Forecast
{
    public partial class FormForecastWDXS : FormBase
    {
        DataTable dataTable = new DataTable();
        private Ps_forecast_list forecastReport = null;
       public IList<Ps_History> fucol =null;
        string type1 = "1";
        int type = 1;
      //  DataTable dataTable = new DataTable();
        bool bLoadingData = false;
        bool _canEdit = true;
        int firstyear = 2000;
        int endyear = 2008;
        public FormForecastWDXS(IList<Ps_History> _fucol)
        {
            InitializeComponent();
            fucol = _fucol;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            double Tavg = 0.0;                                      //求其多年的年平均温度的平均温度
            Ps_History wend= new Ps_History();
           for(int j=0;j<fucol.Count;j++)
           {

               if (fucol[j].Title.Contains("平均温度"))
               {
                   wend = fucol[j];
                   for (int i = firstyear; i <= endyear;i++ )
                   {
                       Tavg += Convert.ToDouble(fucol[j].GetType().GetProperty("y" + i.ToString()).GetValue(fucol[j], null));

                   }
               }
           }
           Tavg /= endyear - firstyear + 1;

            foreach (Ps_History ph in fucol)
            {
                if (!ph.Title.Contains("平均温度"))
                {
                    for (int i = firstyear; i <= endyear;i++ )
                    {
                        double tmax=Convert.ToDouble(wend.GetType().GetProperty("y"+i).GetValue(wend,null));
                      if (tmax<Tavg)
                      {
                          double sz =Convert.ToDouble( ph.GetType().GetProperty("y" + i).GetValue(ph, null) )* (1 + (Tavg - tmax) * temk1);
                          ph.GetType().GetProperty("y" + i).SetValue(ph, sz, null);
                      }
                        else if (tmax>Tavg)
                        {
                            double sz =Convert.ToDouble( ph.GetType().GetProperty("y" + i).GetValue(ph, null)) * (1 + (Tavg -tmax ) * temk2);
                            ph.GetType().GetProperty("y" + i).SetValue(ph, sz, null);
                        }
                    }
                }
            }
            this.DialogResult = DialogResult.OK;

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        public double temk1
        {
            get { return Convert.ToDouble(spinEdit1.Value); }
            set
            {
                spinEdit1.Value = (decimal)value;
           
            }
        }
        public double temk2
        {
            get { return Convert.ToDouble(spinEdit2.Value); }
            set
            {
                
                    spinEdit2.Value = (decimal)value;
               
            }
        }
        private void FormForecastWDXS_Load(object sender, EventArgs e)
        {

            Application.DoEvents();
            Ps_YearRange py = new Ps_YearRange();
            py.Col4 = "电力发展实绩";
            py.Col5 = Itop.Client.MIS.ProgUID;

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
            LoadData();
            RefreshChart();
        }
        private void LoadData()
        {
            //this.splitContainerControl1.SplitterPosition = (2 * this.splitterControl1.Width) / 3;
            //this.splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;
            treeList1.DataSource = null;
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

            ArrayList al = new ArrayList();
            //al.Add( "全地区GDP（亿元）");
            //al.Add( "全社会用电量（亿kWh）");
            //al.Add( "全社会供电量（亿kWh）");
            //al.Add( "全社会最大负荷（万kW）");
            //al.Add( "年末总人口（万人）");
            //al.Add( "总面积（平方公里）");


            //IList<Base_Data> li1 = Common.Services.BaseService.GetStrongList<Base_Data>();
            //foreach (Base_Data bd in li1)
            //    al.Add(bd.Title);




            //Ps_History psp_Type = new Ps_History();
            //psp_Type.Forecast = type;
            //psp_Type.Col4 = ProjectUID;
            //IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);

            //for (int c = 0; c < al.Count; c++)
            //{
            //    bool bl = true;
            //    foreach (Ps_History ph in listTypes)
            //    {
            //        if (al[c].ToString() == ph.Title)
            //            bl = false;
            //    }
            //    if (bl)
            //    {
            //        Ps_History pf = new Ps_History();
            //        pf.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;
            //        pf.Forecast = type;
            //        pf.ForecastID = type1;
            //        pf.Title = al[c].ToString();
            //        pf.Col4 = ProjectUID;
            //        object obj = Services.BaseService.GetObject("SelectPs_HistoryMaxID", pf);
            //        if (obj != null)
            //            pf.Sort = ((int)obj) + 1;
            //        else
            //            pf.Sort = 1;
            //        Services.BaseService.Create<Ps_History>(pf);
            //        listTypes.Add(pf);
            //    }
            //}




            dataTable = Itop.Common.DataConverter.ToDataTable((IList)fucol, typeof(Ps_History));
            treeList1.DataSource = dataTable;
            Application.DoEvents();
            treeList1.ExpandAll();

            bLoadingData = false;
        }

        //添加固定列
        private void AddFixColumn()
        {
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "分类名";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "Sort";
            column.VisibleIndex = -1;
            column.SortOrder = SortOrder.Ascending;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "ForecastID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "Forecast";
            column.VisibleIndex = -1;
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
        }
        //添加年份后，新增一列
        private void AddColumn(int year)
        {
            TreeListColumn column = new TreeListColumn();

            column.FieldName = "y" + year;
            column.Tag = year;
            column.Caption = year + "年";
            column.Name = year.ToString();
            column.Width = 100;
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
        private void CopyBaseColor(FORBaseColor bc1, FORBaseColor bc2)
        {
            bc1.UID = bc2.UID;
            bc1.Title = bc2.Title;
            bc1.Remark = bc2.Remark;
            bc1.Color = bc2.Color;
            bc1.Color1 = ColorTranslator.FromOle(bc2.Color);

        }
        private void RefreshChart()
        {

            IList<FORBaseColor> list = Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByWhere", "Remark='电力发展实绩-" + Itop.Client.MIS.ProgUID + "-1'");

            IList<FORBaseColor> li = new List<FORBaseColor>();
            bool bl = false;
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
                        m++;
                        bl = true;
                        FORBaseColor bc1 = new FORBaseColor();
                        Color cl = (Color)ht[m % 10];
                        bc1.Color1 = cl;
                        //bc1.Color = cl.ToArgb();
                        //bc1.Color1 = Color.Blue;

                        ////bc1.Color = 0;
                        ////bc1.Color1 = bc.Color1;
                        //bc1.Color1 = ColorTranslator.FromOle(bc.Color);
                        //CopyBaseColor(bc1, bc);
                        li.Add(bc1);

                    }


                }
                if (!bl)
                {
                    FORBaseColor bc1 = new FORBaseColor();
                    bc1.UID = Guid.NewGuid().ToString();
                    //bc1.Remark = "电力发展实绩" + "-" + ProjectUID + "-0";
                    bc1.Remark = "电力发展实绩-" + Itop.Client.MIS.ProgUID + "-1";
                    bc1.Title = row["Title"].ToString();
                    //bc1.Color = 16711680;
                    //bc1.Color1 = Color.Blue;
                    Color cl = (Color)ht[m % 10];
                    bc1.Color = 0;
                    bc1.Color1 = Color.Black;
                    Services.BaseService.Create<FORBaseColor>(bc1);
                    li.Add(bc1);
                    m++;
                }

            }

            foreach (FORBaseColor bc2 in li)
            {
                //bc2.Color1 = Color.Black;
                hs.Add(bc2.Color1);
            }


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
                            v.Title = (i + 1).ToString() + "." + row["Title"].ToString();
                            v.Sort = Convert.ToInt32(col.FieldName.Replace("y", ""));
                            v.y1990 = (double)row[col.FieldName];

                            listValues.Add(v);
                        }
                    }
                }


            }

            chart1.Series.Clear();


            chart1.DataBindCrossTab(listValues, "Title", "Sort", "y1990", "");
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                //chart1.Series[i].Color = (Color)hs[i];
                //chart1.Series[i].Name = (i + 1).ToString() + "." + chart1.Series[i].Name;
                //chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;

                //chart1.Series[i].MarkerSize = 7;
                //chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(2);
                chart1.Series[i].Color = (Color)hs[i];

                chart1.Series[i].Name =chart1.Series[i].Name;
                chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;

                chart1.Series[i].MarkerSize = 7;
                chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(2);
                //chart1.Series[i].XValueIndexed = true;

            }

        }
        static public void TreeNodeToDataObject<T>(T dataObject, DevExpress.XtraTreeList.Nodes.TreeListNode treeNode)
        {
            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.Name.Substring(0, 1) == "y")
                    pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);
            }
        }

    }
}