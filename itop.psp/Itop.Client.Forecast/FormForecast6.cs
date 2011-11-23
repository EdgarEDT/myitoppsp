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

using Itop.Client.Using;
namespace Itop.Client.Forecast
{
    public partial class FormForecast6 : FormBase
    {
        int type = 6;
        DataTable dataTable=new DataTable ();
        private Ps_forecast_list forecastReport = null;
        private PublicFunction m_pf = new PublicFunction();
        IList<Ps_Calc> list1 = new List<Ps_Calc>();
        Ps_Calc pc1 = new Ps_Calc();
        bool bLoadingData = false;
        bool _canEdit = true;
        string firstyear = "0";
        string endyear ="0";
        bool selectdral = true;

        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; EditRight = value; }
        }

        private bool EditRight = false;

        public FormForecast6(Ps_forecast_list fr)
        {
            InitializeComponent();
            forecastReport = fr;
            Text = fr.Title;
        }

        private void HideToolBarButton()
        {
            if (!CanEdit)
            {
                barButtonItem17.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barButtonItem25.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem26.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem22.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                simpleButton6.Enabled = false;
            }
            if (!AddRight)
            {
              
            }

        }

        private void FormForecast6_Load(object sender, EventArgs e)
        {

            HideToolBarButton();
            //chart1.Series.Clear();
            //Show();
            Application.DoEvents();
            //this.Cursor = Cursors.WaitCursor;
            //treeList1.BeginUpdate();
            LoadData();
            //treeList1.EndUpdate();
            RefreshChart();
            //this.Cursor = Cursors.Default;



            Ps_Forecast_Setup pfs = new Ps_Forecast_Setup();
            pfs.Forecast = type;
            pfs.ForecastID = forecastReport.ID;
            pfs.StartYear = int.Parse(firstyear.Replace("y", ""));
            pfs.EndYear = int.Parse(endyear.Replace("y", ""));

            IList<Ps_Forecast_Setup> li = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);

            if (li.Count != 0)
            {
                if ((li[0].StartYear >= forecastReport.StartYear && forecastReport.EndYear >= li[0].EndYear))
                {
                    firstyear = li[0].StartYear.ToString();
                    endyear = li[0].EndYear.ToString();
                }
                else
                {
                    endyear = "0";
                    firstyear = "0";
                }
            }



        }

        private void LoadData()
        {
            this.splitContainerControl1.SplitterPosition = (2 * this.splitterControl1.Width) / 3;
            this.splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;
            treeList1.DataSource = null;
            bLoadingData = true;
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }
            AddFixColumn();
            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
            {
                AddColumn(i);
            }
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));

            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;

            Application.DoEvents();
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

            IList<FORBaseColor> list = Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByWhere", "Remark='" + this.forecastReport.ID.ToString() + "-" + type + "'");

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
                    if (row["Title"].ToString() == bc.Title)
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
                    bc1.Remark = this.forecastReport.ID.ToString() + "-" + type;
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
            al.Add(Application.StartupPath + "/img/1.ico");
            al.Add(Application.StartupPath + "/img/2.ico");
            al.Add(Application.StartupPath + "/img/3.ico");
            al.Add(Application.StartupPath + "/img/4.ico");
            al.Add(Application.StartupPath + "/img/5.ico");
            al.Add(Application.StartupPath + "/img/6.ico");
            al.Add(Application.StartupPath + "/img/7.ico");


            chart1.DataBindCrossTab(listValues, "Title", "Sort", "y1990", "");
            //for (int i = 0; i < chart1.Series.Count; i++)
            //{
            //    chart1.Series[i].Color = (Color)hs[i];
            //    chart1.Series[i].Name = chart1.Series[i].Name;
            //    chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;
            //    chart1.Series[i].MarkerImage = al[i % 7].ToString();
            //    chart1.Series[i].MarkerSize = 7;
            //    chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(2);

            //    chart1.Series[i].XValueIndexed = false;

            //}

            //chart1.ChartAreas["Default"].AxisX.MinorGrid.Enabled = false;
            //chart1.ChartAreas["Default"].AxisY.MinorGrid.Enabled = true;
            //chart1.ChartAreas["Default"].AxisY.MinorGrid.LineStyle = ChartDashStyle.Dash;
            //chart1.ChartAreas["Default"].AxisY.MinorGrid.LineColor = Color.Gray;
            m_pf.SetChart(chart1, checkBox1, hs, al);
        }


        private InputLanguage oldInput = null;

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            // psp_Type.ForecastID = forecastReport.ID;
            // psp_Type.Forecast = type;
            // Common.Services.BaseService.Update("DeletePs_Forecast_MathForecastIDAndForecast", psp_Type);
            // psp_Type.Forecast = 0;
            // IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
            // foreach (Ps_Forecast_Math psp_Typetemp in listTypes)
            //{
            //    psp_Type = new Ps_Forecast_Math();
            //    psp_Type = psp_Typetemp;
            //    psp_Type.ID = Guid.NewGuid().ToString();
            //    psp_Type.Forecast = type;
            //    Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type);
            //}

            ForecastClass fc = new ForecastClass();
            fc.BadForecast(type, forecastReport);

            LoadData();

            RefreshChart();
        }






        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "JPEG文件(*.jpg)|*.jpg|BMP文件(*.bmp)|*.bmp|PNG文件(*.png)|*.png";
            if (sf.ShowDialog() != DialogResult.OK)
                return;

            Dundas.Charting.WinControl.ChartImageFormat ci = new Dundas.Charting.WinControl.ChartImageFormat();
            switch (sf.FilterIndex)
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
            this.chart1.SaveAsImage(sf.FileName, ci);
        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormColor fc = new FormColor();
            fc.DT = dataTable;
            fc.ID = forecastReport.ID.ToString();
            fc.For = type;
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
            fr.Text = forecastReport.Title;
            fr.ShowDialog();
        }

        private void FormAverageForecast_Resize(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                JS(2);
            }
            catch { MsgBox.Show("历史数据不正确，请重新设定"); }

        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            Brush brush = null;
            Rectangle r = e.Bounds;
            Color c3 = Color.FromArgb(152, 122, 254);
            Color c4 = Color.FromArgb(152, 122, 254);
            //foreach (GridCell cell in alist)
            //{

            if (e.Column.FieldName.IndexOf("y") > -1 && firstyear != "Title" && endyear != "Title")
                {
                    if (Convert.ToInt32(e.Column.FieldName.Replace("y", "")) >= Convert.ToInt32(firstyear.Replace("y", "")) && Convert.ToInt32(endyear.Replace("y", "")) >= Convert.ToInt32(e.Column.FieldName.Replace("y", "")))
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c3, c4, 180);
                    if (brush != null)
                    {
                        e.Graphics.FillRectangle(brush, r);
                    }
                }
        }

        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barButtonItem26.Caption == "开始截取历史数据")
            {
                barButtonItem26.Caption = "结束截取历史数据";
                firstyear = "0";
                endyear = "0";
                selectdral = false;
                this.simpleButton2.Enabled = false;
                this.barButtonItem17.Enabled = false;
                //this.simpleButton4.Enabled = false;


                treeList1.OptionsSelection.MultiSelect = true;
                treeList1.OptionsBehavior.Editable = false;
                treeList1.Refresh();
            }
            else if (barButtonItem26.Caption == "结束截取历史数据")
            {
                barButtonItem26.Caption = "开始截取历史数据";
                selectdral = true;
                this.simpleButton2.Enabled = true;
                this.barButtonItem17.Enabled = true;
                //this.simpleButton4.Enabled = true;
                if (firstyear != "Title")
                {
                    Ps_Forecast_Setup pfs = new Ps_Forecast_Setup();
                    pfs.ID = Guid.NewGuid().ToString();
                    pfs.Forecast = type;
                    pfs.ForecastID = forecastReport.ID;
                    pfs.StartYear = int.Parse(firstyear.Replace("y", ""));
                    pfs.EndYear = int.Parse(endyear.Replace("y", ""));

                    IList<Ps_Forecast_Setup> li = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);

                    if (li.Count == 0)
                        Services.BaseService.Create<Ps_Forecast_Setup>(pfs);
                    else
                        Services.BaseService.Update("UpdatePs_Forecast_SetupByForecast", pfs);
                }

                treeList1.OptionsSelection.MultiSelect = false;
                treeList1.OptionsBehavior.Editable = true;
            }
        }



        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            Save();
            
        }

        private void Save()
        {

            //保存

            foreach (DataRow dataRow in dataTable.Rows)
            {

                TreeListNode row = treeList1.FindNodeByKeyID(dataRow["ID"]);

                //for (int i = 0; i < this.treeList1.Nodes.Count; i++)
                //{
                //    TreeListNode row = this.treeList1.Nodes[i];
                Ps_Forecast_Math v = new Ps_Forecast_Math();
                v.ID = row["ID"].ToString();
                foreach (TreeListColumn col in this.treeList1.Columns)
                {
                    if (col.FieldName.IndexOf("y") > -1)
                    {
                        object obj = row[col.FieldName];
                        if (obj != DBNull.Value)
                        {
                            v.GetType().GetProperty(col.FieldName).SetValue(v, obj, null);
                        }
                    }
                }

                try
                {
                    Services.BaseService.Update("UpdatePs_Forecast_MathByID", v);

                }
                catch { }
            }
            MsgBox.Show("保存成功！");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //保存按钮
            Save();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //表格数据发生变化
            RefreshChart();
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ////参数设置

            CS();

        }



        //根据节点返回此行的历史数据
        private double[] GenerateHistoryValue(DataRow node, int syear, int eyear)
        {
            double[] rt = new double[eyear - syear + 1];
            for (int i = 0; i < eyear - syear + 1; i++)
            {
                object obj = node["y" + (syear + i)];
                if (obj == null || obj == DBNull.Value)
                {
                    rt[i] = 0;
                }
                else
                {
                    rt[i] = (double)obj;
                }
            }
            return rt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CS();
        }

        private void CS()
        {
            if (firstyear == "0" || endyear == "0")
            {
                MsgBox.Show("请设置历史数据起始年结束年后再点参数设置");
                return;
            }


            FormForecastCalc1 fc = new FormForecastCalc1();
            fc.DTable = dataTable;
            fc.ISEdit = EditRight;
            fc.PForecastReports = forecastReport;
            if (fc.ShowDialog() != DialogResult.OK)
                return;
            JS(2);
      
        }

        private void JS(int index)
        {
            //计算预测值

            int fyear = int.Parse(firstyear.Replace("y", ""));
            int syear = int.Parse(endyear.Replace("y", ""));
            int eyear = forecastReport.EndYear;

            foreach (DataRow dataRow in dataTable.Rows)
            {


                double zzl = 0;
              
                double value1 = 0;
                try { value1 = (double)dataRow["y" + syear]; }
                catch { }



                if (fyear != 0 && syear != 0)
                {
                         if (fyear != 0 && syear != 0)
                {
                    if (syear - fyear < 2)
                    {
                        MessageBox.Show("历史年份必须大于两年！");
                        return;
                    }
                }
                    double[] historyValues = GenerateHistoryValue(dataRow, fyear, syear);

                    Ps_Calc pcs = new Ps_Calc();
                   
                         
                            switch (index)
                            {
                                case 1:
                                   
                                     double[,] u = Calculator.GrayMethodAU(historyValues, eyear - syear);
                                     u[0, 0]=Math.Round(u[0,0],2);
                                     u[1, 0] = Math.Round(u[1, 0], 2);

                                     bool bl = false;
                                     foreach (Ps_Calc pc11 in list1)
                                     {
                                         if (pc11.CalcID == dataRow["ID"].ToString())
                                         {
                                             bl = true;
                                             pc11.Value1 = u[0,0];
                                             pc11.Value2 = u[1, 0];

                                             Services.BaseService.Update<Ps_Calc>(pc11);
                                         }
                                     }
                                     if (!bl)
                                     {
                                         
                                         pcs.ID = Guid.NewGuid().ToString();
                                         pcs.Forecast = type;
                                         pcs.ForecastID = forecastReport.ID;
                                         pcs.CalcID = dataRow["ID"].ToString();
                                         pcs.Value1 = u[0, 0];
                                         pcs.Value2 = u[1, 0];
                                         Services.BaseService.Create<Ps_Calc>(pcs);

                                     }
                                   
                                    
                                    break;
                                case 2:
                                    double[] yn = new double[eyear - syear];
                                    //double[] au = new double[2];
                                    // pcs = new Ps_Calc();
                                    //pcs.Forecast = type;
                                    //pcs.ForecastID = forecastReport.ID;
                                    //pcs.CalcID=dataRow["ID"].ToString();

                                    //pcs = (Ps_Calc)Services.BaseService.GetObject("SelectPs_CalcByForecastCalcID", pcs);
                                    //au[0] = pcs.Value1;
                                    //au[1] = pcs.Value2;
                                    if (this.treeList1.FocusedNode !=null)
                                    {
                                        if (this.treeList1.FocusedNode["ID"].ToString() == dataRow["ID"].ToString())
                                        {
                                            double[,] au = Calculator.GrayMethodAU(historyValues, eyear - syear);
                                            au[0, 0] = Math.Round(au[0, 0], 2);
                                            au[1, 0] = Math.Round(au[1, 0], 2);
                                            spinEdit1.Value = (decimal)au[0, 0];
                                            spinEdit2.Value = (decimal)au[1, 0];
                                        }
                                    }


                                    simpleButton2.Enabled = true;
                                    yn = Calculator.GrayMethod(historyValues, eyear - syear);
                                    for (int i = 1; i <= eyear - syear; i++)
                                    {
                                        dataRow["y" + (syear + i)] = yn[i - 1];
                                    }
                                    ForecastClass fc = new ForecastClass();
                                    fc.MaxForecast(forecastReport, dataTable);
                                    RefreshChart();
                                    break;
                               

                            }
                 

                 
                }
            }
        
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int syear=int.Parse(firstyear.Replace("y",""));
            int eyear=int.Parse(endyear.Replace("y",""));
            if (eyear >= forecastReport.StartYear)
                RefreshChart(syear, eyear);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int syear = int.Parse(firstyear.Replace("y", ""));
            int eyear = int.Parse(endyear.Replace("y", ""));
            if (eyear >= forecastReport.StartYear)
                RefreshChart(eyear + 1, forecastReport.EndYear);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RefreshChart();
        }



        private void RefreshChart(int syear, int eyear)
        {

            IList<FORBaseColor> list = Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByWhere", "Remark='" + this.forecastReport.ID.ToString() + "-" + type + "'");

            IList<FORBaseColor> li = new List<FORBaseColor>();
            bool bl = false;
            foreach (DataRow row in dataTable.Rows)
            {
                bl = false;
                foreach (FORBaseColor bc in list)
                {
                    if (row["Title"].ToString() == bc.Title)
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
                    bc1.Remark = this.forecastReport.ID.ToString() + "-" + type;
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

            for (int i = 0; i < this.treeList1.Nodes.Count; i++)
            {
                TreeListNode row = treeList1.Nodes[i];
                foreach (TreeListColumn col in treeList1.Columns)
                {
                    if (col.FieldName.IndexOf("y") > -1)
                    {
                        int yyear = int.Parse(col.FieldName.Replace("y", ""));
                        if (yyear >= syear && yyear <= eyear)
                        {
                            object obj = row[col.FieldName];
                            if (obj != DBNull.Value)
                            {
                                Ps_Forecast_Math v = new Ps_Forecast_Math();
                                v.ForecastID = forecastReport.ID;
                                v.ID = (string)row["ID"];
                                v.Title = row["Title"].ToString();
                                v.Sort = Convert.ToInt32(col.FieldName.Replace("y", ""));
                                v.y1990 = (double)row[col.FieldName];

                                listValues.Add(v);
                            }
                        }
                    }
                }


            }

            chart1.Series.Clear();


            chart1.DataBindCrossTab(listValues, "Title", "Sort", "y1990", "");
            //for (int i = 0; i < chart1.Series.Count; i++)
            //{
            //    chart1.Series[i].Color = (Color)hs[i];
            //    chart1.Series[i].Name = (i + 1).ToString() + "." + chart1.Series[i].Name;
            //    chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;

            //    chart1.Series[i].MarkerSize = 7;
            //    chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(2);

            //}
            m_pf.SetChart(chart1, checkBox1, hs, null);
        }

        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!selectdral)
            {
                if (firstyear == "0")
                {
                    if (treeList1.FocusedColumn.FieldName.Contains("y"))
                    {
                        firstyear = treeList1.FocusedColumn.FieldName;
                    }
                }
                else
                {
                    if (treeList1.FocusedColumn.FieldName.Contains("y"))
                    {
                        endyear = treeList1.FocusedColumn.FieldName;
                    }

                    if (Convert.ToInt32(firstyear.Replace("y", "")) > Convert.ToInt32(endyear.Replace("y", "")))
                    {
                        string itemp = firstyear;
                        firstyear = endyear;
                        endyear = itemp;
                    }
                }
                treeList1.Refresh();
            }
        }

        private void treeList1_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Column.FieldName.IndexOf("y") > -1 && firstyear != "Title" && endyear != "Title")
            {
                if (Convert.ToInt32(e.Column.FieldName.Replace("y", "")) >= Convert.ToInt32(firstyear.Replace("y", "")) && Convert.ToInt32(endyear.Replace("y", "")) >= Convert.ToInt32(e.Column.FieldName.Replace("y", "")))

                    e.Appearance.BackColor = Color.FromArgb(152, 122, 254);

            }
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            RefreshChart();
        }

        private void button7_Click(object sender, EventArgs e)
        {
           if( firstyear!="0"&&endyear!="0")
           {
               JS(1);
           }
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!CanEdit)
            {
                e.Cancel = true;
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            RefreshChart();
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult result = chart1.HitTest(e.X, e.Y);
            if (result != null && result.Object != null)
            {
                if (result.Object is LegendItem)
                {
                    LegendItem legend = (LegendItem)result.Object;
                    Series SelectedSeries = (Series)legend.Tag;
                    if (SelectedSeries != null)
                    {
                        if (SelectedSeries.Enabled)
                        {
                            SelectedSeries.Enabled = false;
                            legend.Cells[0].Image = string.Format(Application.StartupPath + @"/img/chk_unchecked.png");
                        }
                        else
                        {
                            SelectedSeries.Enabled = true;
                            legend.Cells[0].Image = string.Format(Application.StartupPath + @"/img/chk_checked.png");
                        }
                        chart1.ResetAutoValues();
                    }
                }
            }
        }



    }
}