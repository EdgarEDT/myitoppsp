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
using Itop.Domain.HistoryValue;
using Dundas.Charting.WinControl;
using Itop.Client.Forecast.FormAlgorithm_New;

using Itop.Client.Using;

//using Microsoft.Office.Interop.Excel;
namespace Itop.Client.Forecast
{
    public partial class FormForecastF : FormBase
    {
        //标识新版还是旧版开关
        bool New_Flag = true;

        DataTable dataTable=new DataTable ();
        string com = "";
        private int typeFlag2 = 2;
        private int typeFlag =0;
        Hashtable hash = new Hashtable();
        private Ps_forecast_list forecastReport = null;
        bool bLoadingData = false;

        bool _canEdit = true;

        bool _isSelect = false; 
        
        string pid = "";
        public string PID
        {
            set { pid = value; }
            get { return pid; }

        }

        DevExpress.XtraGrid.GridControl _gridControl;

        public DevExpress.XtraGrid.GridControl GridControl
        {
            get { return _gridControl; }
            set { _gridControl = value; }
        }

        public bool IsSelect
        {
            get { return _isSelect; }
            set { _isSelect = value; }
        }

        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; EditRight = value; }
        }
        bool _canPrint = true;

        public bool CanPrint
        {
            get { return _canPrint; }
            set { _canPrint = value; }
        }
        private bool AddRight = false;
        private bool EditRight = false;
        public bool ADdRight
        {
            get { return AddRight; }
            set { AddRight = value; }
        }
        private bool DeleteRight = false;
        public bool DEleteRight
        {
            get { return DeleteRight; }
            set { DeleteRight = value; }
        }
        public FormForecastF(Ps_forecast_list fr, int tf)
        {
            InitializeComponent();
            forecastReport = fr;
          //  typeFlag = forecastReport.ID;
            Text = fr.Title;
        }

        private void HideToolBarButton()
        {
            //if (!CanEdit)
            //{
            //    barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //  //  barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    barSubItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    barButtonItem17.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //    barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //}
            if (!AddRight)
            {
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem15.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
            if (!EditRight)
            {
                barButtonItem23.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem27.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem22.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!DeleteRight)
            {
                barButtonItem24.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }

        }

        private void Form8Forecast_Load(object sender, EventArgs e)
        {
            barButtonItem16.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
             //hash.Add("ArverageIncreasingMethod","年增长率法");
             //hash.Add("SpringCoefficientMethod","弹性系数法");
             //hash.Add("TwiceMoveArverageMethod","移动平均法");
             //hash.Add("GrayMethod", "灰色模型法");
             //hash.Add("LinearlyRegressMethod", "线性回归法");
             //hash.Add("IndexIncreaseMethod", "指数增长法");
             //hash.Add("IndexSmoothMethod", "指数平滑法");
             //hash.Add("LinearlyTrend","线性趋势法");
             //hash.Add("ProfessionalLV", "专家决策法");
             HideToolBarButton();
             //chart1.Series.Clear();
             Show();
             Application.DoEvents();
             this.Cursor = Cursors.WaitCursor;
             //treeList1.BeginUpdate();
             barButtonItem16.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
             LoadData();
             InitSum();
             //treeList1.EndUpdate();
             RefreshChart();
             this.Cursor = Cursors.Default;
          
            
        }

        private void LoadData()
        {
            treeList1.DataSource = null;
            bLoadingData = true;
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                this.treeList1.Columns.Clear();

                //treeList1.Columns.Clear();
            }
            //((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            AddFixColumn();
            
            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
            {
                AddColumn(i);
            }
            //((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = 0;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));

            this.treeList1.DataSource = dataTable;


       
            Application.DoEvents();

            bLoadingData = false;
            InitMenu();

            
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
            column.Width = 70;
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
  

       



        private void InitMenu()
        {
            barButtonItem1.ImageIndex = -1;
            barButtonItem25.ImageIndex = -1;
            barButtonItem3.ImageIndex = -1;
            barButtonItem2.ImageIndex = -1;
            barButtonItem8.ImageIndex = -1;
            barButtonItem4.ImageIndex = -1;
            barButtonItem19.ImageIndex = -1;
            //barButtonItem12.ImageIndex = -1;
            //barButtonItem16.ImageIndex = -1;
            //barButtonItem19.ImageIndex = -1;
            barButtonItem30.ImageIndex = -1;



            Ps_Forecast_Math psp_Typen = new Ps_Forecast_Math();
            psp_Typen.ForecastID = forecastReport.ID;
            psp_Typen.Forecast = 1;
            IList listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
                barButtonItem1.ImageIndex = 0;

            psp_Typen.Forecast = 2;
            listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
                barButtonItem25.ImageIndex = 0;

            psp_Typen.Forecast = 3;
            listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
                barButtonItem3.ImageIndex = 0;

            psp_Typen.Forecast = 4;
            listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
                barButtonItem2.ImageIndex = 0;

            psp_Typen.Forecast = 5;
            listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
                barButtonItem8.ImageIndex = 0;

            psp_Typen.Forecast = 6;
            listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
                barButtonItem4.ImageIndex = 0;

            psp_Typen.Forecast = 7;
            listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
                barButtonItem19.ImageIndex = 0;
            psp_Typen.Forecast = 11;
            listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
                barButtonItem30.ImageIndex = 0;
            psp_Typen.Forecast = 15;
            listTypesn = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Typen);
            if (listTypesn.Count > 0)
                barButtonItem35.ImageIndex = 0;
        }





        //年增长率法
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //InitMenu();
            //barButtonItem1.ImageIndex = 8;
            com = "ArverageIncreasingMethod";
            //FormForecast1 FMA = new FormForecast1(forecastReport);
            FormAverageGrowthRate FMA = new FormAverageGrowthRate(forecastReport);
            FMA.CanEdit = CanEdit;
            FMA.Text = this.Text + "- 年增长率法";
            FMA.Show();
            InitMenu();
        }

        //弹性系数法
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //InitMenu();
            //barButtonItem2.ImageIndex = 8;
            com = "SpringCoefficientMethod";
         //   Forecast("SpringCoefficientMethod");
            //FormForecast4 fm = new FormForecast4(forecastReport);
            FormCoefficientOfElasticity fm = new FormCoefficientOfElasticity(forecastReport);
            fm.Text = this.Text + "- 弹性系数法";
            fm.CanEdit = CanEdit;
            fm.Show();
            InitMenu();
        }

        //移动平均法
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormForecast3 FMA = new FormForecast3(forecastReport);
            FormCorrelationMethod FMA = new FormCorrelationMethod(forecastReport);
            FMA.Text = this.Text + "- 相关法";
            FMA.CanEdit = CanEdit;
            FMA.ADdRight = AddRight;
            FMA.DEleteRight = DeleteRight;
            FMA.Show();
            InitMenu();
        }

        //灰色模型
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //InitMenu();
            //barButtonItem4.ImageIndex = 8;
            com = "GrayMethod";
        //    Forecast("GrayMethod");
            //FormForecast6 FMA = new FormForecast6(forecastReport);
            GrayModel FMA = new GrayModel(forecastReport);
            FMA.Text = this.Text + "- 灰色模型法";
            FMA.CanEdit = CanEdit;
            FMA.Show();
            InitMenu();
        }

        //线性回归
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //InitMenu();
            //barButtonItem5.ImageIndex = 8;
            com ="LinearlyRegressMethod";
          //  Forecast("LinearlyRegressMethod");
            InitMenu();
        }

        //指数增长
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitMenu();
            barButtonItem7.ImageIndex = 8;
            com = "IndexIncreaseMethod";
      //      Forecast("IndexIncreaseMethod");

        }

        //指数平滑
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //InitMenu();
            //barButtonItem8.ImageIndex = 8;
            com ="IndexSmoothMethod";
          //  Forecast("IndexSmoothMethod");
            InitMenu();
        }

        //线性趋势法
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //InitMenu();
            //barButtonItem12.ImageIndex = 8;
            com = "LinearlyTrend";
        //    Forecast("LinearlyTrend");
            InitMenu(); 
        }

     
        //保存数据
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!CanEdit)
            
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

        //    Save();
        }




        private void Save(TreeListNode node, TreeListColumn column)
        {
            Cursor = Cursors.WaitCursor;
            //List<PSP_ForecastValues> listValues = new List<PSP_ForecastValues>();
            string str = "";
            str = "set " + column.FieldName + "=" + node.GetValue(column.FieldName) + " where ID='" + node.GetValue("ID") + "'";

           
                        try
            {
                Common.Services.BaseService.Update("UpdatePs_Forecast_MathbyIDAndYear", str);
                //MsgBox.Show("数据保存成功！");
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            }

            Cursor = Cursors.Default;

        }



        private void CopyBaseColor(FORBaseColor bc1, FORBaseColor bc2)
        {
            bc1.UID = bc2.UID;
            bc1.Title = bc2.Title;
            bc1.Remark = bc2.Remark;
            bc1.Color = bc2.Color;
            bc1.Color1 = ColorTranslator.FromOle(bc2.Color);
        }
        /*
        private void RefreshChart()
        {

            IList<FORBaseColor> list = Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByWhere", "Remark='" + this.forecastReport.ID.ToString() + "-" + typeFlag + "'");

            IList<FORBaseColor> li = new List<FORBaseColor>();
            bool bl = false;
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
                    if (row["Title"].ToString() == bc.Title && row["ParentID"]!=DBNull.Value)
                    {
                        bl = true;
                        FORBaseColor bc1 = new FORBaseColor();
                        bc1.Color1 = Color.Blue;
                        CopyBaseColor(bc1, bc);
                        li.Add(bc1);
                    }


                }
                if (!bl)
                {
                    FORBaseColor bc1 = new FORBaseColor();
                    bc1.UID = Guid.NewGuid().ToString();
                    bc1.Remark = this.forecastReport.ID.ToString() + "-" + typeFlag;
                    bc1.Title = row["Title"].ToString();
                    bc1.Color = 16711680;
                    bc1.Color1 = Color.Blue;
                    Services.BaseService.Create<FORBaseColor>(bc1);
                    li.Add(bc1);
                }

            }
            ArrayList hs = new ArrayList();
            foreach (FORBaseColor bc2 in li)
            {
                hs.Add(bc2.Color1);
            }











            List<Ps_Forecast_Math> listValues = new List<Ps_Forecast_Math>();

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
                            Ps_Forecast_Math v = new Ps_Forecast_Math();
                            v.ForecastID = forecastReport.ID;
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

            ArrayList al = new ArrayList();
            al.Add(Application.StartupPath+"/img/1.ico");
            al.Add(Application.StartupPath + "/img/2.ico");
            al.Add(Application.StartupPath + "/img/3.ico");
            al.Add(Application.StartupPath + "/img/4.ico");
            al.Add(Application.StartupPath + "/img/5.ico");
            al.Add(Application.StartupPath + "/img/6.ico");
            al.Add(Application.StartupPath + "/img/7.ico");
            //al.Add("img/8.ico");
            //al.Add("img/9.ico");
            //al.Add("img/10.ico");

            LegendItem legendItem;
            LegendCell legendCell1;
            LegendCell legendCell2;
            LegendCell legendCell3;
            Legend legend = new Legend();
            legend.AutoFitText = false;

            chart1.DataBindCrossTab(listValues, "Title", "Sort", "y1990", "");
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                legendItem = new Dundas.Charting.WinControl.LegendItem();
                legendCell1 = new Dundas.Charting.WinControl.LegendCell();
                legendCell1.CellType = Dundas.Charting.WinControl.LegendCellType.Image;
                legendCell1.Name = "Cell1";

                legendCell2 = new Dundas.Charting.WinControl.LegendCell();
                legendCell3 = new Dundas.Charting.WinControl.LegendCell();
                legendCell2.CellType = Dundas.Charting.WinControl.LegendCellType.Image;
                legendCell2.Name = "Cell2";
                legendCell3.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
                legendCell3.Name = "Cell3";
                legendCell3.Text = chart1.Series[i].Name;
                legendCell3.TextColor = (Color)hs[i];
                /*默认是柱状图，下面代码是变成line
               // chart1.Series[i].Color = (Color)hs[i];
               // chart1.Series[i].Name =chart1.Series[i].Name;
               // chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;

               // chart1.Series[i].MarkerImage = al[i % 7].ToString();
               // chart1.Series[i].MarkerSize = 7;
               // chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(2);
                
               //// chart1.Series[i].XValueIndexed = true;
                chart1.Series[i].Color = (Color)hs[i];
                chart1.Series[i].Name = (i + 1).ToString() + "." + chart1.Series[i].Name;
                chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;
                chart1.Series[i].MarkerImage = al[i % 7].ToString();
                chart1.Series[i].MarkerSize = 7;
                chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(2);

                chart1.Series[i].ShowInLegend = false;

                legendItem.Cells.Add(legendCell1);
                legendItem.Cells.Add(legendCell3);
                legendItem.Tag = chart1.Series[i];
                if (checkBox1.Checked)
                {
                    legendItem.Cells[0].Image = string.Format(Application.StartupPath + @"/img/chk_checked.png");

                }
                else
                {
                    legendItem.Cells[0].Image = string.Format(Application.StartupPath + @"/img/chk_unchecked.png");
                }
                legend.CustomItems.Add(legendItem);
                chart1.Series[i].Enabled = checkBox1.Checked;
            }

            chart1.ChartAreas["Default"].AxisX.MinorGrid.Enabled = false;
            chart1.ChartAreas["Default"].AxisY.MinorGrid.Enabled = true;
            chart1.ChartAreas["Default"].AxisY.MinorGrid.LineStyle = ChartDashStyle.Dash;
            chart1.ChartAreas["Default"].AxisY.MinorGrid.LineColor = Color.Gray;
            legend.Name = "Default";
            chart1.Legends.Add(legend);

        }
        */
        private void RefreshChart()
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
            int m = 0;
            IList<FORBaseColor> list = Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByWhere", "Remark='" + this.forecastReport.ID.ToString() + "-" + typeFlag + "'");

            IList<FORBaseColor> li = new List<FORBaseColor>();
            bool bl = false;
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
                    if (row["Title"].ToString() == bc.Title && row["ParentID"] != DBNull.Value)
                    {
                        bl = true;
                        FORBaseColor bc1 = new FORBaseColor();
                        bc1.Color1 = bc.Color1;
                        //bc1.Color1 = Color.Blue;
                        CopyBaseColor(bc1, bc);
                        li.Add(bc1);
                    }


                }
                if (!bl)
                {
                    FORBaseColor bc1 = new FORBaseColor();
                    bc1.UID = Guid.NewGuid().ToString();
                    bc1.Remark = this.forecastReport.ID.ToString() + "-" + typeFlag;
                    bc1.Title = row["Title"].ToString();
                    //bc1.Color = 16711680;
                    if (m == 0)
                    {
                        Random rd = new Random();
                        m = rd.Next(100);
                    }
                    Color cl = (Color)ht[m % 13];
                    bc1.Color = ColorTranslator.ToOle(cl);
                    bc1.Color1 =cl;
                    //bc1.Color1 = Color.Blue;
                    Services.BaseService.Create<FORBaseColor>(bc1);
                    li.Add(bc1);
                }
                m++;
            }
            ArrayList hs = new ArrayList();
            foreach (FORBaseColor bc2 in li)
            {
                hs.Add(bc2.Color1);
            }

            List<Ps_Forecast_Math> listValues = new List<Ps_Forecast_Math>();

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
                            Ps_Forecast_Math v = new Ps_Forecast_Math();
                            v.ForecastID = forecastReport.ID;
                            v.ID = (string)row["ID"];
                            v.Title =  row["Title"].ToString();
                            v.Sort = Convert.ToInt32(col.FieldName.Replace("y", ""));
                            v.y1990 = (double)row[col.FieldName];

                            listValues.Add(v);
                        }
                    }
                }


            }

            this.chart_user1.RefreshChart(listValues, "Title", "Sort", "y1990", hs);

        }

    

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private InputLanguage oldInput = null;

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormForecastLoadData ffs = new FormForecastLoadData();
          //  ffs.Type = type;
            ffs.PID = pid;
            ffs.StartYear = forecastReport.StartYear;
            ffs.EndYear = forecastReport.EndYear;
            if (ffs.ShowDialog() != DialogResult.OK)
                return;
           
         
            Hashtable hs = ffs.HS;

            if (hs.Count == 0)
                return;
            string id = Guid.NewGuid().ToString();
            if (New_Flag)
            {
                foreach (Ps_History de3 in hs.Values)
                {
                    Ps_Forecast_Math py = new Ps_Forecast_Math();
                    py.Col1 = de3.ID;
                    py.Forecast = 0;
                    py.ForecastID = forecastReport.ID;

                    py = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByCol1", py);
                    if (py == null)
                    {

                        Ps_Forecast_Math ForecastMath = new Ps_Forecast_Math();
                        ForecastMath.Title = de3.Title;

                        for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
                        {
                            ForecastMath.GetType().GetProperty("y" + i).SetValue(ForecastMath, de3.GetType().GetProperty("y" + i).GetValue(de3, null), null);
                        }


                        id = id.Substring(0, 8);

                        ForecastMath.Col1 = de3.ID;
                        ForecastMath.ID = id + "|" + de3.ID;
                        ForecastMath.ParentID = id + "|" + de3.ParentID;
                        ForecastMath.Forecast = 0;
                        ForecastMath.ForecastID = forecastReport.ID;
                        ForecastMath.Sort = de3.Sort;
                        Services.BaseService.Create("InsertPs_Forecast_MathbyPs_History", ForecastMath);
                    }
                    else
                    {

                        for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
                        {
                            py.GetType().GetProperty("y" + i).SetValue(py, de3.GetType().GetProperty("y" + i).GetValue(de3, null), null);
                        }
                        Services.BaseService.Update<Ps_Forecast_Math>(py);

                    }
                }
            }
            else
            {
                if (ffs.Selectid != "4")
                {
                    foreach (Ps_History de3 in hs.Values)
                    {
                        Ps_Forecast_Math py = new Ps_Forecast_Math();
                        py.Col1 = de3.ID;
                        py.Forecast = 0;
                        py.ForecastID = forecastReport.ID;

                        py = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByCol1", py);
                        if (py == null)
                        {

                            Ps_Forecast_Math ForecastMath = new Ps_Forecast_Math();
                            ForecastMath.Title = de3.Title;

                            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
                            {
                                ForecastMath.GetType().GetProperty("y" + i).SetValue(ForecastMath, de3.GetType().GetProperty("y" + i).GetValue(de3, null), null);
                            }


                            id = id.Substring(0, 8);

                            ForecastMath.Col1 = de3.ID;
                            ForecastMath.ID = id + "|" + de3.ID;
                            ForecastMath.ParentID = id + "|" + de3.ParentID;
                            ForecastMath.Forecast = 0;
                            ForecastMath.ForecastID = forecastReport.ID;
                            ForecastMath.Sort = de3.Sort;
                            Services.BaseService.Create("InsertPs_Forecast_MathbyPs_History", ForecastMath);
                        }
                        else
                        {

                            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
                            {
                                py.GetType().GetProperty("y" + i).SetValue(py, de3.GetType().GetProperty("y" + i).GetValue(de3, null), null);
                            }
                            Services.BaseService.Update<Ps_Forecast_Math>(py);

                        }
                    }

                }
                else if (ffs.Selectid == "4")
                {
                    foreach (PSP_Types de3 in hs.Values)
                    {
                        Ps_Forecast_Math py = new Ps_Forecast_Math();

                        //py = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", "Title='" + de3.Title + "'" + " and Forecast='0' and ForecastID='" + forecastReport.ID + "' and Col1='"+de3.ID+"'");
                        py = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", "  Forecast='0' and ForecastID='" + forecastReport.ID + "' and Col1='" + de3.ID + "'");
                        if (py == null)
                        {

                            Ps_Forecast_Math ForecastMath = new Ps_Forecast_Math();

                            IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", "TypeID='" + de3.ID + "'");

                            foreach (PSP_Values value in listValues)
                            {

                                ForecastMath.GetType().GetProperty("y" + value.Year).SetValue(ForecastMath, value.Value, null);
                            }


                            id = id.Substring(0, 8);
                            ForecastMath.Title = de3.Title;
                            ForecastMath.Col1 = de3.ID.ToString();
                            ForecastMath.ID = id + "|" + de3.ID;
                            ForecastMath.ParentID = id + "|" + de3.ParentID;
                            ForecastMath.Forecast = 0;
                            ForecastMath.ForecastID = forecastReport.ID;
                            object obj = Services.BaseService.GetObject("SelectPs_Forecast_MathMaxID", null);
                            if (obj != null)
                                ForecastMath.Sort = ((int)obj) + 1;
                            else
                                ForecastMath.Sort = 1;
                            Services.BaseService.Create("InsertPs_Forecast_MathbyPs_History", ForecastMath);
                        }
                        else
                        {


                            IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", " TypeID='" + de3.ID + "'");

                            foreach (PSP_Values value in listValues)
                            {

                                py.GetType().GetProperty("y" + value.Year).SetValue(py, value.Value, null);
                            }

                            Services.BaseService.Update<Ps_Forecast_Math>(py);

                        }
                    }

                }

            }
           


            LoadData();

            //ArrayList al = M1();
            //for (int i = 0; i < al.Count; i++)
            //{
            //    TreeListNode tlnn = (TreeListNode)al[i];
            //    aaa(tlnn);
            //}


            this.chart_user1.All_Select(true);
            RefreshChart();
        }



        private void InitSum()
        {
            foreach (TreeListNode tlnn in treeList1.Nodes)
            {
                foreach (TreeListNode tlnn1 in tlnn.Nodes)
                {
                    CalculateSum2(tlnn1);
                }
            }
        }








        private void b()
        {
            foreach (TreeListNode tlnn in treeList1.Nodes)
            {
                aaa(tlnn);
            }
        
        }


        private ArrayList M1()
        {
            ArrayList al = new ArrayList();
            foreach (TreeListNode tlnn in treeList1.Nodes)
            {
                al.Add(M2(tlnn));
            }
            return al;

        }

        private TreeListNode M2(TreeListNode tln)
        {
            if (tln.Nodes.Count == 0)
                return tln;
            else
            {
                foreach (TreeListNode tl in tln.Nodes)
                {
                    return M2(tl);
                }
                return tln;
            }
        }



        private void aaa(TreeListNode tln)
        {
            if(tln.Nodes.Count==0)
                CalculateSum2(tln);
            else
            {
                foreach (TreeListNode tl in tln.Nodes)
                {
                    aaa(tl);
                }
            }
        }


        private DataTable ConvertXtrTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList)
        {
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();

            listColID.Add("Title");
            dt.Columns.Add("分类名", typeof(string));

            foreach (TreeListColumn column in xTreeList.Columns)
            {
                if (column.FieldName.IndexOf("y") > -1)
                {
                    listColID.Add(column.FieldName);
                    dt.Columns.Add(column.FieldName.Replace("y", "") + "年", typeof(double));
                }
            }

            foreach (TreeListNode node in xTreeList.Nodes)
            {
                AddNodesDataToDataTable(dt, node, listColID);
            }

            return dt;
        }

        private void AddNodesDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID)
        {
            DataRow newRow = dt.NewRow();
            foreach (string colID in listColID)
            {
                //分类名，第二层及以后在前面加空格
                if (colID == "Title" && node.ParentNode == null)
                {
                    newRow["分类"] = node[colID];
                }
                else
                {
                    if (dt.Columns.Contains(colID.Replace("y", "") + "年"))
                    newRow[colID.Replace("y", "") + "年"] = node[colID];
                }
            }

            dt.Rows.Add(newRow);

            foreach (TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID);
            }
        }
        private void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID)
        {
            DataRow newRow = dt.NewRow();
            foreach (string colID in listColID)
            {
                //分类名，第二层及以后在前面加空格
                if (colID == "Title" && node.ParentNode != null)
                {
                    newRow["分类"] = "　　" + node[colID];
                }
                else
                {
                    if (dt.Columns.Contains(colID.Replace("y", "") + "年"))
                    newRow[colID.Replace("y", "") + "年"] = node[colID];
                }
            }

            dt.Rows.Add(newRow);

            foreach (TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID);
            }
        }
        private void copydata(DataTable dt1,DataTable dt2/*,ArrayList a2*/)
        {
            foreach (DataColumn dc in dt1.Columns)
                dt2.Columns.Add(dc.ColumnName, dc.DataType);
            foreach (DataRow row1 in dt1.Rows)
            {
                DataRow row2 = dt2.NewRow();
                foreach (DataColumn dc in dt1.Columns)
                {
                    

                    row2[dc.ColumnName] = row1[dc.ColumnName];
                }
               /* if(a2.Contains(row2["Title"].ToString()))*/
                    dt2.Rows.Add(row2);
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
             bool TSL_falg = false;
            double Tsl_double = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            { 
                if (nd["Title"].ToString().Contains("同时率"))
                {
                    //记录同时率
                    if (Convert.ToDouble(nd[column].ToString()) != 0)
                    {
                        TSL_falg = true;
                        Tsl_double = Convert.ToDouble(nd[column].ToString());
                    }
                    continue;
                }
                object value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }
            if (TSL_falg)
            {
                sum = sum * Tsl_double;
            }
            parentNode.SetValue(column.FieldName, sum);
            Save(node, column);
            CalculateSum2(parentNode, column);
          
        }

        //计算指定节点的各列各
        private void CalculateSum2(TreeListNode node)
        {
            foreach (TreeListColumn column in treeList1.Columns)
            {
                if (column.FieldName.IndexOf("y") >= 0)
                {
                    CalculateSum2(node, column);
                }
            }
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

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormColor fc = new FormColor();
            fc.DT = dataTable;
            fc.For = typeFlag;
            fc.ID = forecastReport.ID.ToString();
            if (fc.ShowDialog() == DialogResult.OK)
                RefreshChart();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem9_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加分类";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
             
                psp_Type.ID = Guid.NewGuid().ToString();

                psp_Type.Forecast = 0;
                psp_Type.ForecastID = forecastReport.ID;

                psp_Type.Title = frm.TypeTitle;
                object obj = Services.BaseService.GetObject("SelectPs_Forecast_MathMaxID", null);
                if (obj != null)
                    psp_Type.Sort = ((int)obj) + 1;
                else
                    psp_Type.Sort = 1;
              

                try
                {
                    Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type);
                    //psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_P_Types", psp_Type);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));

                    
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加分类出错：" + ex.Message);
                }
                RefreshChart();
            }
        }

        private void barButtonItem15_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode row = this.treeList1.FocusedNode;
            if (row == null)
            {
                return;
            }


            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加子分类";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                psp_Type.ParentID = row["ID"].ToString();

                psp_Type.ID = Guid.NewGuid().ToString();

                psp_Type.Forecast = 0;
                psp_Type.ForecastID = forecastReport.ID;

                psp_Type.Title = frm.TypeTitle;
                object obj = Services.BaseService.GetObject("SelectPs_Forecast_MathMaxID", null);
                if (obj != null)
                    psp_Type.Sort = ((int)obj) + 1;
                else
                    psp_Type.Sort = 1;


                try
                {
                    Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type);
                    //psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_P_Types", psp_Type);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));


                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加分类出错：" + ex.Message);
                }
                RefreshChart();
            }
        }

        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode row = this.treeList1.FocusedNode;
            if (row == null)
            {
                return;
            }


            string parentid = row["ParentID"].ToString();


            FormTypeTitle frm = new FormTypeTitle();
            frm.TypeTitle = row["Title"].ToString();
            frm.Text = "修改分类名";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                ForecastClass1.TreeNodeToDataObject(psp_Type, row);


                //psp_Type = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(row);
                psp_Type.Title = frm.TypeTitle;

                try
                {
                    Common.Services.BaseService.Update<Ps_Forecast_Math>(psp_Type);
                    row.SetValue("Title",frm.TypeTitle);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("修改出错：" + ex.Message);
                }
                RefreshChart();
            }
        }

        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode row = this.treeList1.FocusedNode;
            if (row == null)
            {
                return;
            }

            if (row.Nodes.Count > 0)
            {
                MsgBox.Show("有下级分类，不可删除");
                return;
            }

            string parentid = row["ParentID"].ToString();



            if (MsgBox.ShowYesNo("是否删除分类 " + row["Title"].ToString() + "？") == DialogResult.Yes)
            {
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                ForecastClass1.TreeNodeToDataObject(psp_Type, row);
                //psp_Type = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(row);
                Ps_Forecast_Math psp_Values = new Ps_Forecast_Math();
                psp_Values.ID = psp_Type.ID;

                try
                {
                    //DeletePSP_ValuesByType里面删除数据和分类
                    Common.Services.BaseService.Delete<Ps_Forecast_Math>(psp_Values);
                    FORBaseColor bc1 = new FORBaseColor();

                    bc1.Remark = forecastReport.ID+ "-" + typeFlag;
                    bc1.Title = row["Title"].ToString();
                    Common.Services.BaseService.Update("DeleteFORBaseColorByTitleRemark", bc1);

                    this.treeList1.Nodes.Remove(row);
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.WaitCursor;
                    LoadData();
                    this.Cursor = Cursors.Default;
                }
                RefreshChart();
            }
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //ForecastFileClass.ExportExcel(dataTable, forecastReport.Title, false, true);
            //GridTreePrint.Exception(this.treeList1, forecastReport.Title);
            FormResult fr = new FormResult();
            fr.LI = this.treeList1;
            fr.Text = forecastReport.Title;
            fr.ShowDialog();

        }

        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormForecast2 FMA = new FormForecast2(forecastReport);
            FormExtrapolationMethod FMA = new FormExtrapolationMethod(forecastReport);
            FMA.Text = this.Text + "- 外推法";
            FMA.CanEdit = CanEdit;
            FMA.Show();
            InitMenu();
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormForecast7 FMA = new FormForecast7(forecastReport);
            FormExpert FMA = new FormExpert(forecastReport);
            FMA.Text = this.Text + "- 专家决策法";
            FMA.CanEdit = CanEdit;
            FMA.Show();
            InitMenu(); 
        }

        private void barButtonItem8_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormForecast5 FMA = new FormForecast5(forecastReport);
            FormExponentialSmoothing FMA = new FormExponentialSmoothing(forecastReport);
            FMA.Text = this.Text + "- 指数平滑法";
            FMA.CanEdit = CanEdit;
            FMA.Show();
            InitMenu();
        }
        //最大小时数法
        private void barButtonItem35_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormForecast5 FMA = new FormForecast5(forecastReport);
            FormMaxHour FMA = new FormMaxHour(forecastReport);
            FMA.Text = this.Text + "- 最大小时数法";
            FMA.CanEdit = CanEdit;
            FMA.Show();
            InitMenu();
        }



        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormForecast10 FMA = new FormForecast10(forecastReport);
            FMA.Text = this.Text + "- 定权组合系数";
            FMA.CanEdit = CanEdit;
            FMA.Show();
        }

        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormForecast9 FMA = new FormForecast9(forecastReport);
            FMA.Text = this.Text + "- 复合算法";
            FMA.CanEdit = CanEdit;
            FMA.Show();
        }

        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormForecastMaxOrBad_new frm = new FormForecastMaxOrBad_new(forecastReport);
            frm.Type = 1;
            frm.ShowDialog();
            LoadData(); 
        }

        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormForecastMaxOrBad frm = new FormForecastMaxOrBad(forecastReport);
            frm.Type = 2;
            frm.ShowDialog();
        }
        /// <summary>
        /// 结果比较
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //dataTable
            ArrayList a1 = new ArrayList();
            ArrayList a2 = new ArrayList();
            ArrayList a3 = new ArrayList();
            ArrayList a4 = new ArrayList();
            DataTable dt3 = new DataTable();
            dt3.Columns.Add("A", typeof(int));
            dt3.Columns.Add("B",typeof(bool));
            dt3.Columns.Add("C", typeof(bool));
            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
            {
                DataRow dr = dt3.NewRow();
                dr["A"] = i;
                //dr["B"] = true;
                //dr["C"] = false;
                dt3.Rows.Add(dr);
            }


            FormXuanZhe form = new FormXuanZhe();
            //form.Text = this.Text + "- 结果比较设置";
            form.DT2 = dataTable;
            form.DT3 = dt3;
            form.ReportForecastID = forecastReport.ID;
            form.ForecastReport = forecastReport;
            if (form.ShowDialog() != DialogResult.OK)
                return;
            a1 = form.A1;
            a2 = form.A2;
            a3 = form.A3;
            a4 = form.A4;
            DataTable dt = new DataTable();
            foreach (DataColumn dc in dataTable.Columns)
            {
                dt.Columns.Add(dc.ColumnName, dc.DataType);
            }

            IList<Ps_Forecast_Math> li = new List<Ps_Forecast_Math>();
            ArrayList al = new ArrayList();
            foreach (DataRow dr in dataTable.Rows)
            {
                if (!(bool)dr["B"])
                    continue;
                string id = dr["ID"].ToString();
                Ps_Forecast_Math pfvalue = new Ps_Forecast_Math();//平均值
                pfvalue.Title = dr["Title"].ToString() + "<平均值>";
                pfvalue.Title = pfvalue.Title.Replace("常规用电量-", "");
                pfvalue.ID = "999|" + Itop.Client.MIS.ProgUID;
                pfvalue.ForecastID = forecastReport.ID;
                pfvalue.Forecast = Convert.ToInt32(dr["Forecast"]);
                int icount = 0;
                for (int i = 0; i < a1.Count; i++)
                {
                    int m = Convert.ToInt32(a1[i]);
                   
                    Ps_Forecast_Math pf2 = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(id);
                    Ps_Forecast_Math pf1 = new Ps_Forecast_Math();
                    Ps_Forecast_Math pf = new Ps_Forecast_Math();
                    //pf.Col4 = pid;
                    pf.Col1 = id;
                    pf.ForecastID = forecastReport.ID;
                    pf.Forecast = m;
                    IList<Ps_Forecast_Math> li2 = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByCol1", pf);
                    if (li2.Count == 0)
                    {
                        pf1 = pf2;
                        pf1.Forecast = m;
                        pf1.ID = Guid.NewGuid().ToString();

                    }
                    else
                    {
                        pf1 = li2[0];
                    }
                    string str = "";
                    switch (m)
                    {
                        case 1:
                            str = "年增长率法";
                            break;
                        case 2:
                            str = "外推法";
                            break;
                        case 3:
                            str = "相关法";
                            break;
                        case 4:
                            str = "弹性系数法";
                            break;
                        case 5:
                            str = "指数平滑法";
                            break;
                        case 6:
                            str = "灰色模型法";
                            break;
                        case 7:
                            str = "专家决策法";

                            break;
                        case 20:
                            str = "复合算法";

                            break;
                        case 10:
                            str = "定权组合系数法";

                            break;
                        case 11:
                            str = "常规增长率-大用户预测法";

                            break;

                    }
                    //if (m != 11)
                    //{
                    //    if ( pf1.Title.Contains("-") && pf1.Title.Contains("常规"))
                    //    {
                    //        continue;
                    //    }
                    pf1.Title = pf1.Title + "<" + str + ">";
                    for (int j = forecastReport.StartYear; j <= forecastReport.EndYear; j++)
                    {

                        double value2 = Math.Round(Convert.ToDouble(pfvalue.GetType().GetProperty("y" + j.ToString()).GetValue(pfvalue, null)), 2);
                        double value = Math.Round(Convert.ToDouble(pf1.GetType().GetProperty("y" + j.ToString()).GetValue(pf1, null)), 2);
                        value = Math.Round((value + value2), 2);
                        pfvalue.GetType().GetProperty("y" + j.ToString()).SetValue(pfvalue, value, null);
                    }
                    pf1.Sort = li.Count;
                    icount++;
                    al.Add(pf1.Title);
                    li.Add(pf1);
                }
                //else
                //{
                //    if (pf1.Title.Contains("-")||true)
                //    {
                //         IList<Ps_forecast_list> listReports = Common.Services.BaseService.GetList<Ps_forecast_list>("SelectPs_forecast_listByWhere", "UserID='" + Itop.Client.MIS.ProgUID + "' and Col1='2' and Title='" + forecastReport.Title + "'");
                //         if (listReports.Count > 0)
                //         {
                //             pf1.Title = dr["Title"].ToString();
                //             string[] strtemp = new string [2];
                //             if (pf1.Title.Contains("电量"))
                //             {
                //                 strtemp[0] = "电量";
                //                 strtemp[1] = pf1.Title;
                //                 if(pf1.Title.Substring(pf1.Title.Length-2,2)=="电量")
                //                     strtemp[1] = pf1.Title.Substring(0, pf1.Title.Length - 2);

                //             }
                //             else if (pf1.Title.Contains("负荷"))
                //             {
                //                 strtemp[0] = "负荷";
                //                 strtemp[1] = pf1.Title;
                //                 if (pf1.Title.Substring(pf1.Title.Length - 2, 2) == "负荷")
                //                     strtemp[1] = pf1.Title.Substring(0, pf1.Title.Length - 2);
                //             }

                //             int typetemp = 0;

                //             if (strtemp[0].Contains("电量"))
                //                 typetemp = 2;
                //             else
                //                 typetemp = 3;
                //             if (typetemp == 2)
                //             {
                //                 str = "电量";
                //             }
                //             else
                //             {
                //                 str = "负荷";
                //             }
                //             //if (strtemp[1].Contains("全社会"))
                //             //{
                //             //    IList<Ps_Forecast_Math> li3 = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere",
                //             //         "Forecast='" + typetemp + "' and ForecastID='" + listReports[0].ID + "' and Title like '全社会%'");

                //             //    foreach (Ps_Forecast_Math ps in li3)
                //             //    {

                //             //        ps.Title = ps.Title + str + "<常规增长率-大用户预测法>";

                //             //        if (al.Contains(ps.Title))
                //             //            continue;
                //             //           for (int j = forecastReport.StartYear; j <= forecastReport.EndYear; j++)
                //             //           {

                //             //               double value2 =Math.Round(Convert.ToDouble(pfvalue.GetType().GetProperty("y" + j.ToString()).GetValue(pfvalue, null)),2 );
                //             //               double value = Math.Round(Convert.ToDouble(ps.GetType().GetProperty("y" + j.ToString()).GetValue(ps, null)), 2);
                //             //               value = Math.Round((value + value2) , 2);
                //             //               pfvalue.GetType().GetProperty("y" +j.ToString()).SetValue(pfvalue, value, null);
                //             //           }
                //             //           icount++;
                //             //           ps.Sort = li.Count;

                //             //           al.Add(ps.Title);
                //             //        li.Add(ps);
                //             //    }
                //             //}
                //             //else
                //             {
                //                 object obj = Common.Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere",
                //                         "Forecast='" + typetemp + "' and ForecastID='" + listReports[0].ID + "' and Title='" + strtemp[1] + "'");
                //                 if (obj == null)
                //                     continue;
                //                 pf1 = obj as Ps_Forecast_Math;
                //                 pf1.ParentID = "";
                //                 pf1.Title = pf1.Title + str + "<常规增长率-大用户预测法>";

                //                 if (al.Contains(pf1.Title))
                //                     continue;
                //                    for (int j = forecastReport.StartYear; j <= forecastReport.EndYear; j++)
                //                    {

                //                        double value2 =Math.Round(Convert.ToDouble(pfvalue.GetType().GetProperty("y" + j.ToString()).GetValue(pfvalue, null)),2 );
                //                        double value =Math.Round(Convert.ToDouble(pf1.GetType().GetProperty("y" + j.ToString()).GetValue(pf1, null)),2 );
                //                        value = Math.Round((value + value2) , 2);
                //                        pfvalue.GetType().GetProperty("y" + j.ToString()).SetValue(pfvalue, value, null);
                //                    }
                //                    icount++;
                //                    pf1.Sort = li.Count;


                //                                 li.Add(pf1);
                //             }
                //         }
                //    }

                //}



                for (int j = forecastReport.StartYear; j <= forecastReport.EndYear; j++)
                {

                    double value = Math.Round(Convert.ToDouble(pfvalue.GetType().GetProperty("y" + j.ToString()).GetValue(pfvalue, null)), 2);

                    value = Math.Round((value / icount), 2);
                    pfvalue.GetType().GetProperty("y" + j.ToString()).SetValue(pfvalue, value, null);
                }

                li.Add(pfvalue);
            }
    


            FormForecastJG ffjg = new FormForecastJG();
            ffjg.Text = this.Text + "- 结果比较";
            ffjg.CanEdit = CanEdit;
            ffjg.LI = li;
            ffjg.A1 = a3;
            ffjg.A2 = a4;
            ffjg.PFL = forecastReport;
            ffjg.ShowDialog();


        }



        private void DataCopy(DataRow row1,DataRow row2)
        {
            foreach (DataColumn dc in row1.Table.Columns)
            {
                try
                {
                    switch (dc.DataType.ToString())
                    { 
                        case "int" :
                            row2[dc.ColumnName] = Convert.ToInt32(row1[dc.ColumnName].ToString());
                            break;
                        case "double":
                            row2[dc.ColumnName] = Convert.ToDouble(row1[dc.ColumnName].ToString());
                            break;
                        case "string":
                            row2[dc.ColumnName] = row1[dc.ColumnName].ToString();
                            break;
                        default:
                            row2[dc.ColumnName] = row1[dc.ColumnName].ToString();
                            break;
                    }
                }
                catch { }
            }
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            TreeListNode row = this.treeList1.FocusedNode;
            if (row == null)
                return;
            Ps_Forecast_Math pf = new Ps_Forecast_Math();
            ForecastClass1.TreeNodeToDataObject<Ps_Forecast_Math>(pf,row);
            Services.BaseService.Update<Ps_Forecast_Math>(pf);
            CalculateSum2(row);
            //aaa(row);
            RefreshChart();
        }
        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(row);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
            TreeListNode parentNode = node.ParentNode;
            if (parentNode == null)
            {
                return;
            }
            double sum = 0;
            bool TSL_falg = false;
            double Tsl_double = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            {
                if (nd["Title"].ToString().Contains("同时率"))
                {
                    //记录同时率
                    if (Convert.ToDouble(nd[column].ToString()) != 0)
                    {
                        TSL_falg = true;
                        Tsl_double = Convert.ToDouble(nd[column].ToString());
                    }
                    continue;
                }
                object value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }
            if (sum != 0)
            {
                if (TSL_falg)
                {
                    sum = sum * Tsl_double;
                }
                parentNode.SetValue(column.FieldName, sum);
            }
            else
            {
                parentNode.SetValue(column.FieldName, null);
            }
            CalculateSum(parentNode, column);
        }
        

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if(!CanEdit)
            {
                e.Cancel = true;
            }
        }

        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormForecast11 FMA = new FormForecast11(forecastReport);
            //FMA.Text = this.Text + "- 常规增长率-大用户预测法";
            //FMA.CanEdit = CanEdit;
            //FMA.Show();
            //InitMenu();

            FormForecast11_hc frm = new FormForecast11_hc(forecastReport);
            frm.CanEdit = EditRight;
            //frm.project = Itop.Client.MIS.ProgUID;
            frm.smdgroup = smdgroup;
            frm.Text = this.Text + "- 常规增长率-大用户预测法";
            DialogResult dr = frm.ShowDialog();
        }

        private void barButtonItem31_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        public double temtk1 = -0.0001, temtk2 = -0.0001;
        private void barButtonItem32_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = 0;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
            IList<Ps_History> fhcol = new List<Ps_History>();
            bool flag = false;                   //判断有没有 最大负荷这条数据 和年平均温度
            foreach (Ps_Forecast_Math pm in listTypes)
            {
                if (pm.Title.Contains("最大负荷"))
                {
                   
                    Ps_History ph = new Ps_History();
                    ph.ID = pm.Col1;
                    ph = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByKey", ph);
                    if (ph!=null)
                    {
                        flag = true;
                        Ps_History pr = new Ps_History();
                        pr.ID = ph.ParentID;
                        pr = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByKey", pr);
                        if (pr!=null)
                        {
                            ph.Title = pr.Title + ph.Title;
                        }
                        fhcol.Add(ph);
                    }
                }
            }
            if (!flag)
            {
                MsgBox.Show("预测数据中不存在负荷数据，请导入数据后再操作！");
                return;
            }
           
            //string con = "Title='年平均温度' AND Col4='" + ProjectUID + "'AND Forecast='1'";
            Ps_History wd = (Ps_History)Services.BaseService.GetObject("SelectPs_HistoryByCondition", "Title like'%年平均温度%' AND Col4='" + Itop.Client.MIS.ProgUID + "'AND Forecast='1'");
            if (wd!=null)
            {
                fhcol.Add(wd);
            }
            else
            {
                MsgBox.Show("在电力发展实际中请设定年平均温度！");
                return;
            }

            FormForecastWDXS wdxs = new FormForecastWDXS(fhcol);
            wdxs.temk1 = temtk1;
            wdxs.temk2 = temtk2;
            if (wdxs.ShowDialog()==DialogResult.OK)
            {
                foreach(Ps_History de3 in wdxs.fucol)
                {
                    temtk1 = wdxs.temk1;
                    temtk2 = wdxs.temk2;
                    Ps_Forecast_Math py = new Ps_Forecast_Math();
                    py.Col1 = de3.ID;
                    py.Forecast = 0;
                    py.ForecastID = forecastReport.ID;
                    py = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByCol1", py);
                    if (py != null)                   
                    {

                        for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
                        {
                            py.GetType().GetProperty("y" + i).SetValue(py, de3.GetType().GetProperty("y" + i).GetValue(de3, null), null);
                        }
                        Services.BaseService.Update<Ps_Forecast_Math>(py);

                    }
                    
                }
               
            }
            LoadData();
            RefreshChart();
            
        }

        private void barButtonItem33_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormXiaoshi frm = new FormXiaoshi(forecastReport, typeFlag2);
            ////frm.PF = forecastReport;
            //frm.ShowDialog();
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            RefreshChart();
        }

        private void barButtonItem34_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormForecast11_hc frm = new FormForecast11_hc(forecastReport);
            frm.CanEdit = EditRight;
            //frm.project = Itop.Client.MIS.ProgUID;
            frm.smdgroup = smdgroup;
            frm.Text = this.Text + "- " + forecastReport.Title;
            DialogResult dr = frm.ShowDialog();
        }
        //添加空间负荷预测功能
        private void barButtonItem36_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MessageBox.Show("请添写空间负荷预测功能!");
        }
       
       
       


    }
}