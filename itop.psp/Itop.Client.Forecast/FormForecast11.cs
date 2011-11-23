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
using Itop.Client.Using;
namespace Itop.Client.Forecast
{
    public partial class FormForecast11 : FormBase
    {
        int type = 11;
        DataTable dataTable=new DataTable ();
        private Ps_forecast_list forecastReport = null;
        private PublicFunction m_pf = new PublicFunction();
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

        public FormForecast11(Ps_forecast_list fr)
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
                barButtonItem22.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem26.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem25.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                simpleButton6.Enabled = false;
                simpleButton4.Enabled = false;
            }
            if (!AddRight)
            {
              
            }
            SelectDaYongHu();
        }
        /// <summary>
        /// 查找对应大用户
        /// </summary>
        private void SelectDaYongHu()
        {
            //"UserID='" + Itop.Client.MIS.ProgUID + "' and Col1='2' and Title='" + forecastReport.Title + "'  and StartYear='" + forecastReport.StartYear + "'" + "'  and EndYear='" + forecastReport.EndYear + "'"
            IList<Ps_forecast_list> listReports = Common.Services.BaseService.GetList<Ps_forecast_list>("SelectPs_forecast_listByWhere", "UserID='" + Itop.Client.MIS.ProgUID + "' and Col1='2' and Title='" + forecastReport.Title + "'");
            if(listReports.Count<1)
            {
                barButtonItem1.Caption = "无对应大用户方案";
                barButtonItem1.Enabled = false;
                simpleButton2.Enabled = false;
                simpleButton4.Enabled = false;
                barButtonItem17.Enabled = false;
                barButtonItem26.Enabled = false;
                
            }
            else if (listReports.Count == 1)
            {
                object obj = Common.Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere",
                            "ForecastID='" + listReports[0].ID + "'");
                if (obj == null)
                {

                    barButtonItem1.Caption = "对应大用户方案无数据";
                    barButtonItem17.Enabled = false;
                    barButtonItem26.Enabled = false;
                    barButtonItem1.Enabled = false;
                    simpleButton2.Enabled = false;
                    simpleButton4.Enabled = false;

                }

            }
            else
            {

                barButtonItem1.Caption = "有多个同名大用户方案";
                barButtonItem1.Enabled = false;
                simpleButton2.Enabled = false;
                simpleButton4.Enabled = false;
                barButtonItem17.Enabled = false;
                barButtonItem26.Enabled = false;
                
            }
           
        }
     
        private void Form11Forecast_Load(object sender, EventArgs e)
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
                firstyear=li[0].StartYear.ToString();
                endyear=li[0].EndYear.ToString();
                //if (int.Parse(firstyear.Replace("y", "")) > int.Parse(endyear.Replace("y", "")))
                // {
                //     string itemp = firstyear;
                //     firstyear = endyear;
                //     endyear = itemp;

                // }
             }
            
          
            
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

            
            this.chart_user1.RefreshChart(listValues, "Title", "Sort", "y1990", hs);
           
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
            this.chart_user1.All_Select(true);
            RefreshChart();
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
            fc.ID = forecastReport.ID.ToString();
            fc.For =type;
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

            //JS();
            JS2();
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            Brush brush = null;
            Rectangle r = e.Bounds;
            Color c3 = Color.FromArgb(152, 122, 254);
            Color c4 = Color.FromArgb(152, 122, 254);
            //foreach (GridCell cell in alist)
            //{

            if (e.Column.FieldName.IndexOf("y") > -1 && firstyear!="Title" && endyear!="Title")
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
                this.simpleButton4.Enabled = false;


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
                this.simpleButton4.Enabled = true;
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

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            
         
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            //if (!selectdral)
            //{
            //    if (firstyear == "0")
            //    {
            //        firstyear = gridView1.FocusedColumn.FieldName;
            //    }
            //    else
            //    {
            //        endyear = gridView1.FocusedColumn.FieldName;
            //    }
            //    gridView1.RefreshData();
            //}
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


           // FormForecastCalc1 fc = new FormForecastCalc1();
            FormForecastCalc1Third fc = new FormForecastCalc1Third();
            //fc.Text = this.Text + "- 参数设置";
            fc.Text =  "参数设置";
            fc.DTable = dataTable;
            fc.ISEdit = EditRight;
            fc.PForecastReports = forecastReport;
            fc.Firstyear = Convert.ToInt32(firstyear.Replace("y",""));
            fc.Endyear = Convert.ToInt32(endyear.Replace("y", ""));
            fc.Type = type;
            if (fc.ShowDialog() != DialogResult.OK)
                return;
          //  JS();
            JS2();
        }

           private void JS()
        {
            //计算预测值
            Ps_Calc pcs = new Ps_Calc();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);


            int fyear = int.Parse(firstyear.Replace("y", ""));
            int syear = int.Parse(endyear.Replace("y", ""));
            int eyear = forecastReport.EndYear;

            foreach (DataRow dataRow in dataTable.Rows)
            {


                double zzl = 0;
                bool bl = false;
                double value1 = 0;
                try { value1 = (double)dataRow["y" + syear]; }
                catch { }


                foreach (Ps_Calc pc11 in list1)
                {
                    if (pc11.CalcID == dataRow["ID"].ToString().Trim())
                    {
                        bl = true;
                        zzl = pc11.Value1;
                    }
                }


                if (!bl)
                {
                    if (fyear != 0 && syear != 0)
                    {
                        double[] historyValues = GenerateHistoryValue(dataRow, fyear, syear);
                        zzl = Calculator.AverageIncreasing(historyValues);
                        Ps_Calc pcs1 = new Ps_Calc();
                        pcs1.ID = Guid.NewGuid().ToString();
                        pcs1.Forecast = type;
                        pcs1.ForecastID = forecastReport.ID;
                        pcs1.CalcID = dataRow["ID"].ToString();
                        pcs1.Value1 = zzl;
                        Services.BaseService.Create<Ps_Calc>(pcs1);
                    }
                    
                }
                if (fyear != 0 && syear != 0)
                {
                    for (int i = 1; i <= eyear - syear; i++)
                    {
                        dataRow["y" + (syear + i)] = value1 * Math.Pow(1 + zzl, i);
                    }
                }


            }
            ForecastClass fc=new ForecastClass();
            fc.MaxForecast(forecastReport, dataTable);
            RefreshChart();
        
        }

        private void JS2()
        {
            //if (firstyear == "0" || endyear == "0")
            //{
            //    MsgBox.Show("请设置历史数据起始年结束年后再点参数设置");
            //    return;
            //}

            ////计算预测值
            //Ps_Calc pcs = new Ps_Calc();
            //pcs.Forecast = type;
            //pcs.ForecastID = forecastReport.ID;
            //IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);


            //int fyear = 0;
            //int syear = 0;
            //int eyear = 0;
            //IList<Ps_forecast_list> listReports = Common.Services.BaseService.GetList<Ps_forecast_list>("SelectPs_forecast_listByWhere", "UserID='" + Itop.Client.MIS.ProgUID + "' and Col1='2' and Title='" + forecastReport.Title + "'");
            //if (listReports.Count < 1)
            //{
            //    return;
            //}
            //foreach (DataRow dataRow in dataTable.Rows)
            //{

            //     syear = 0;
            //     eyear = 0;
            //    double zzl = 0;
            //    bool bl = false;
            //    double value1 = 0;
            //    //try { value1 = (double)dataRow["y" + syear]; }
            //    //catch { }

            //    pcs = new Ps_Calc();
            //    pcs.Forecast = type;
            //    pcs.ForecastID = forecastReport.ID;
            //    pcs.CalcID = dataRow["ID"].ToString().Trim();
            //    list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByWhere", "Forecast = '" + type + "' and ForecastID ='" + forecastReport.ID + "' and  CalcID = '" + dataRow["ID"].ToString().Trim() + "' order by Value2");

            //    Ps_Forecast_Math psp_TypePan = new Ps_Forecast_Math();
            //    string[] str = new string[2] { "",""};
            //    int typetemp = 0;
            //    if (dataRow["Title"].ToString().Contains("电量"))
            //    {
            //        typetemp = 2;
            //        str[1] = dataRow["Title"].ToString();
            //        if (dataRow["Title"].ToString().Substring(dataRow["Title"].ToString().Length - 2, 2) == "电量")
            //            str[1] = dataRow["Title"].ToString().Substring(0, dataRow["Title"].ToString().Length - 2);
            //    }
            //    else if (dataRow["Title"].ToString().Contains("负荷"))
            //    {
            //        typetemp = 3;

            //        str[1] = dataRow["Title"].ToString();
            //        if (dataRow["Title"].ToString().Substring(dataRow["Title"].ToString().Length - 2, 2) == "负荷")
            //            str[1] = dataRow["Title"].ToString().Substring(0, dataRow["Title"].ToString().Length - 2);
            //    }
                
            //    if (str[1].Contains("全社会") && !str[1].Contains("(全地区)"))
            //    {
            //        str[1] = str[1] + "(全地区)";
            //    }
            //    //psp_Type.Forecast = 2;
            //    //psp_Type.ForecastID = listReports[0].ID;
            //    //psp_Type.Title=str[1];
            //    object obj = Common.Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere",
            //        "Forecast='" + typetemp + "' and ForecastID='" + listReports[0].ID + "' and Title='" + str[1] + "'");
            //    if (obj == null)
            //        continue;
            //    Ps_Forecast_Math psp_Type = obj as Ps_Forecast_Math;
            //    psp_TypePan = obj as Ps_Forecast_Math;
            //    string mathstr="";

            //    IList<Ps_Forecast_Math> list = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere",
            //        "Forecast='" + typetemp + "' and ForecastID='" + listReports[0].ID + "' and ParentID='" + psp_Type.ID + "'" + " and ( Title like '常规%' or Title  like '大用户%' or Title  like '同时率%'   ) order by sort");
            //    if (list.Count == 0)
            //        continue;
            //    Ps_Forecast_Math pfscgui = new Ps_Forecast_Math();
         

            //    foreach (Ps_Calc pc11 in list1)
            //    {


            //        //if (pc11.CalcID == dataRow["ID"].ToString().Trim())
            //        //{
            //           bl = true;

            //        zzl = pc11.Value4;
            //        syear = Convert.ToInt32(pc11.Value2);
            //        eyear = Convert.ToInt32(pc11.Value3);
            //        //}
            //        //else
            //        //    continue;



            //        //if (!bl)
            //        //{
            //        //    if (fyear != 0 && syear != 0)
            //        //    {
            //        //        double[] historyValues = GenerateHistoryValue(dataRow, fyear, syear);
            //        //        zzl = Calculator.AverageIncreasing(historyValues);
            //        //        Ps_Calc pcs1 = new Ps_Calc();
            //        //        pcs1.ID = Guid.NewGuid().ToString();
            //        //        pcs1.Forecast = type;
            //        //        pcs1.ForecastID = forecastReport.ID;
            //        //        pcs1.CalcID = dataRow["ID"].ToString();
            //        //        pcs1.Value1 = zzl;
            //        //        pcs.Value2 = Convert.ToDouble(comboBox4.SelectedItem.ToString().Replace("年", ""));
            //        //        pcs.Value3 = Convert.ToDouble(comboBox5.SelectedItem.ToString().Replace("年", ""));
            //        //        pcs.Value4 = Convert.ToDouble(dr["B"].ToString().Replace("%", ""));
            //        //        pcs.Col1 = comboBox1.SelectedItem.ToString();
            //        //        Services.BaseService.Create<Ps_Calc>(pcs1);
            //        //    }

            //        //}
            //        if (syear != 0 && eyear != 0)
            //        {
            //            double value2 = 0;
            //            double value3 = 0;
            //            foreach (Ps_Forecast_Math ms in list)
            //            {
            //                // value1 = (double)dataRow["y" + (i )]; 

            //                // value1 = (double)dataRow["y" + (i )]; 
            //                if (ms.Title.Contains("常规"))
            //                {
                               
            //                    pfscgui = ms;
            //                }
            //            }
            //            for (int i = syear; i <= eyear; i++)
            //            {
            //                try {
            //                    foreach(Ps_Forecast_Math ms in list){
            //                   // value1 = (double)dataRow["y" + (i )]; 
                                   
            //                            // value1 = (double)dataRow["y" + (i )]; 
            //                            if (ms.Title.Contains("常规"))
            //                            {
            //                                value1 = Math.Round(Convert.ToDouble(ms.GetType().GetProperty("y" + (i-1)).GetValue(ms, null)), 2);
            //                                //pfscgui = ms;
            //                            }

            //                            else
            //                    if (ms.Title.Contains("大用户"))
            //                        value2 = Math.Round(Convert.ToDouble(ms.GetType().GetProperty("y" + i).GetValue(ms, null)), 2);

            //                    else
            //                        if (ms.Title.Contains("同时率"))
            //                            value3 = Math.Round(Convert.ToDouble(ms.GetType().GetProperty("y" + i).GetValue(ms, null)), 2);
            //                    }
                            
            //                }
            //                catch { }
            //                value1 =Math.Round( value1 * (1 + zzl * 0.01),2);
            //                pfscgui.GetType().GetProperty("y" + i ).SetValue(pfscgui, value1, null);
                        
            //                if (pfscgui.Forecast==2)
            //                dataRow["y" + i ] = value1 + value2;
            //                else
            //                if (pfscgui.Forecast == 3)
            //                    dataRow["y" + i ] = (value1 + value2)*value3;
                         
            //            }
            //        }


            //    }
            //    if(!bl)
            //    {
            //        if (firstyear != "0" && endyear != "0")
            //        {
            //            double value2 = 0;
            //            double value3 = 0;
            //            syear = Convert.ToInt32(endyear.Replace("y", ""));
            //            eyear = Convert.ToInt32(forecastReport.EndYear)-1;
            //            double[] historyValues = GenerateHistoryValue(dataRow, Convert.ToInt32(firstyear.Replace("y", "")), syear);
            //            zzl = Math.Round( Calculator.AverageIncreasing(historyValues)*100,2);
            //            Ps_Calc pcs1 = new Ps_Calc();
            //            pcs1.ID = Guid.NewGuid().ToString();
            //            pcs1.Forecast = type;
            //            pcs1.ForecastID = forecastReport.ID;
            //            pcs1.CalcID = dataRow["ID"].ToString();
            //            pcs1.Col1 = dataRow["Title"].ToString();
            //            pcs1.Value2 = Convert.ToInt32(endyear.Replace("y", ""))+1;
            //            pcs1.Value3 = Convert.ToInt32(forecastReport.EndYear);
            //            pcs1.Value4 = zzl;
                       

            //            Services.BaseService.Create<Ps_Calc>(pcs1);
            //            for (int i = syear; i <= eyear; i++)
            //            {
            //                try
            //                {
            //                    foreach (Ps_Forecast_Math ms in list)
            //                    {
            //                        // value1 = (double)dataRow["y" + (i )]; 

            //                        // value1 = (double)dataRow["y" + (i )]; 
            //                        if (ms.Title.Contains("常规"))
            //                        {
            //                            value1 = Math.Round(Convert.ToDouble(ms.GetType().GetProperty("y" +( i-1)).GetValue(ms, null)), 2);
            //                            pfscgui = ms;
            //                        }

            //                        else
            //                            if (ms.Title.Contains("大用户"))
            //                                value2 = Math.Round(Convert.ToDouble(ms.GetType().GetProperty("y" + i ).GetValue(ms, null)), 2);

            //                            else
            //                                if (ms.Title.Contains("同时率"))
            //                                    value3 = Math.Round(Convert.ToDouble(ms.GetType().GetProperty("y" + i).GetValue(ms, null)), 2);
            //                    }

            //                }
            //                catch { }
            //                value1 = Math.Round(value1 * (1 + zzl * 0.01), 2);
            //                pfscgui.GetType().GetProperty("y" + (i )).SetValue(pfscgui, value1, null);
            //                if (pfscgui.Forecast == 2)
            //                    dataRow["y" + (i )] = value1 + value2;
            //                else
            //                    if (pfscgui.Forecast == 3)
            //                        dataRow["y" + (i)] = (value1 + value2) * value3;
                            
            //            }

            //        }

            //    }
            //    ForecastClass fc = new ForecastClass();
            //    fc.MaxForecast(forecastReport, dataTable);
            //    RefreshChart();
            //    treeList1.Refresh();
            //}
            //针对蚌埠
            if (firstyear == "0" || endyear == "0")
            {
                MsgBox.Show("请设置历史数据起始年结束年后再点参数设置");
                return;
            }

            //计算预测值
            Ps_Calc pcs = new Ps_Calc();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecastCalcID", pcs);


            int fyear = 0;
            int syear = 0;
            int eyear = 0;

            foreach (DataRow dataRow in dataTable.Rows)
            {

                syear = 0;
                eyear = 0;
                double zzl = 0;
                bool bl = false;
                double value1 = 0;
                //try { value1 = (double)dataRow["y" + syear]; }
                //catch { }
                pcs = new Ps_Calc();
                pcs.Forecast = type;
                pcs.ForecastID = forecastReport.ID;
                pcs.CalcID = dataRow["ID"].ToString().Trim();
                list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByWhere", "Forecast = '" + type + "' and ForecastID ='" + forecastReport.ID + "' and  CalcID = '" + dataRow["ID"].ToString().Trim() + "' order by Value2");

                foreach (Ps_Calc pc11 in list1)
                {


                    //if (pc11.CalcID == dataRow["ID"].ToString().Trim())
                    //{
                    bl = true;

                    zzl = pc11.Value4;
                    syear = Convert.ToInt32(pc11.Value2);
                    eyear = Convert.ToInt32(pc11.Value3);
                    //}
                    //else
                    //    continue;



                    //if (!bl)
                    //{
                    //    if (fyear != 0 && syear != 0)
                    //    {
                    //        double[] historyValues = GenerateHistoryValue(dataRow, fyear, syear);
                    //        zzl = Calculator.AverageIncreasing(historyValues);
                    //        Ps_Calc pcs1 = new Ps_Calc();
                    //        pcs1.ID = Guid.NewGuid().ToString();
                    //        pcs1.Forecast = type;
                    //        pcs1.ForecastID = forecastReport.ID;
                    //        pcs1.CalcID = dataRow["ID"].ToString();
                    //        pcs1.Value1 = zzl;
                    //        pcs.Value2 = Convert.ToDouble(comboBox4.SelectedItem.ToString().Replace("年", ""));
                    //        pcs.Value3 = Convert.ToDouble(comboBox5.SelectedItem.ToString().Replace("年", ""));
                    //        pcs.Value4 = Convert.ToDouble(dr["B"].ToString().Replace("%", ""));
                    //        pcs.Col1 = comboBox1.SelectedItem.ToString();
                    //        Services.BaseService.Create<Ps_Calc>(pcs1);
                    //    }

                    //}
                    if (syear != 0 && eyear != 0)
                    {
                        for (int i = syear; i <= eyear; i++)
                        {
                            try { value1 = (double)dataRow["y" + (i - 1)]; }
                            catch { }
                            dataRow["y" + i] = value1 * (1 + zzl * 0.01);
                        }
                    }


                }
                if (!bl)
                {
                    if (firstyear != "0" && endyear != "0")
                    {
                        double value2 = 0;
                        syear = Convert.ToInt32(endyear.Replace("y", ""));
                        eyear = Convert.ToInt32(forecastReport.EndYear) - 1;
                        double[] historyValues = GenerateHistoryValue(dataRow, Convert.ToInt32(firstyear.Replace("y", "")), syear);
                        zzl = Math.Round(Calculator.AverageIncreasing(historyValues) * 100, 2);
                        Ps_Calc pcs1 = new Ps_Calc();
                        pcs1.ID = Guid.NewGuid().ToString();
                        pcs1.Forecast = type;
                        pcs1.ForecastID = forecastReport.ID;
                        pcs1.CalcID = dataRow["ID"].ToString();
                        pcs1.Col1 = dataRow["Title"].ToString();
                        pcs1.Value2 = Convert.ToInt32(endyear.Replace("y", "")) + 1;
                        pcs1.Value3 = Convert.ToInt32(forecastReport.EndYear);
                        pcs1.Value4 = zzl;


                        Services.BaseService.Create<Ps_Calc>(pcs1);
                        for (int i = syear; i <= eyear; i++)
                        {
                            try { value1 = (double)dataRow["y" + (i - 1)]; }
                            catch { }
                            dataRow["y" + i] = value1 * (1 + zzl * 0.01);
                        }

                    }

                }
                ForecastClass fc = new ForecastClass();
                fc.MaxForecast(forecastReport, dataTable);
                RefreshChart();
                treeList1.Refresh();
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
            this.chart_user1.RefreshChart(listValues, "Title", "Sort", "y1990", hs);
           
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
            try
            {
                if (e.Column.FieldName.IndexOf("y") > -1 && firstyear != "Title" && endyear != "Title")
                {
                    if (Convert.ToInt32(e.Column.FieldName.Replace("y", "")) >= Convert.ToInt32(firstyear.Replace("y", "")) && Convert.ToInt32(endyear.Replace("y", "")) >= Convert.ToInt32(e.Column.FieldName.Replace("y", "")))

                        e.Appearance.BackColor = Color.FromArgb(152, 122, 254);

                }
            }
            catch (System.Exception ex)
            {
            	
            }
           
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //表格数据发生变化
            if (e.Column.FieldName.Substring(0, 1) != "y") return;
            double d = 0;
            if (!double.TryParse(e.Value.ToString(), out d)) return;
            treeList1.BeginInit();
            try
            {
                CalculateSum(e.Node, e.Column);
            }
            catch
            { }
            treeList1.EndInit();
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
            if (!CanEdit)
            {
                e.Cancel = true;
            }
        }
        private void updateAllPan(Ps_Forecast_Math psp_TypePan, Ps_forecast_list listReports)
        {
            string strtemp = "";
            if (psp_TypePan.Forecast == 2)
            {
                strtemp = " and Title!='同时率'";
            }
            else
                if (psp_TypePan.Forecast == 3)
                {
                    strtemp = "";
                }
            IList<Ps_Forecast_Math> mathlist = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere",
                          "Forecast='" + psp_TypePan.Forecast + "' and ForecastID='" + psp_TypePan.ForecastID + "' and ParentID='" + psp_TypePan.ID + "'" + strtemp+" order by sort desc");
            Ps_Forecast_Math matcgui ;
            Ps_Forecast_Math matdyh;
            Ps_Forecast_Math mattsl;


            //if (psp_TypePan.Title.Contains("各县合计"))
            //{
                matcgui = new Ps_Forecast_Math();//常规
                matdyh = new Ps_Forecast_Math();//大用户
                mattsl = new Ps_Forecast_Math();//同时率
            //}
            double value = 0;
            double value2 = 0;
            for (int i = listReports.StartYear; i <= listReports.EndYear; i++)
            {
                value = 0;
                 value2 = 0;
                foreach (Ps_Forecast_Math mat in mathlist)
                {
                    if (psp_TypePan.Title.Contains("各县合计") && (mat.Title.Contains("常规") || mat.Title.Contains("大用户") || mat.Title.Contains("同时率")))
                    {
                        if (mat.Title.Contains("常规"))
                        {
                            matcgui = mat;
                        }
                        else
                        if (mat.Title.Contains("大用户"))
                        {
                            matdyh = mat;
                        }
                        else
                            if (mat.Title.Contains("同时率"))
                            {
                                mattsl = mat;
                            }
                    if (!mat.Title.Contains("同时率"))
                        continue;
                    }
                    if (mat.Title == "同时率")
                        value2 = value2 * Math.Round(Convert.ToDouble( mat.GetType().GetProperty("y" + i.ToString()).GetValue(mat, null)),2);
                    else
                        value2 += Math.Round(Convert.ToDouble(mat.GetType().GetProperty("y" + i.ToString()).GetValue(mat, null)), 2);

                }
                value += value2;
                psp_TypePan.GetType().GetProperty("y" + i.ToString()).SetValue(psp_TypePan, value, null);
                Common.Services.BaseService.Update<Ps_Forecast_Math>(psp_TypePan);
            }
            if (psp_TypePan.ParentID != "")
            {
                psp_TypePan = Common.Services.BaseService.GetOneByKey<Ps_Forecast_Math>(psp_TypePan.ParentID);
                updateAllPan(psp_TypePan, listReports);
            }
            else
                if (psp_TypePan.Title.Contains("各县合计"))
                {
                    value = 0;
                    value2 = 0;
                    for (int i = listReports.StartYear; i <= listReports.EndYear; i++)
                    {
                        value2 =  Math.Round(Convert.ToDouble(mattsl.GetType().GetProperty("y" + i.ToString()).GetValue(mattsl, null)),2);
                        if ( psp_TypePan.Forecast==3)
                        {
                            if(value2 != 0 )
                            {
                            value2=Math.Round(Convert.ToDouble(psp_TypePan.GetType().GetProperty("y" + i.ToString()).GetValue(psp_TypePan, null)),2)/value2;
                            
                             value=value2-Math.Round(Convert.ToDouble(matdyh.GetType().GetProperty("y" + i.ToString()).GetValue(matdyh, null)),2);
                            matcgui.GetType().GetProperty("y" + i.ToString()).SetValue(matcgui, value, null);
                                 Common.Services.BaseService.Update<Ps_Forecast_Math>(matcgui);
                            }
                           
                        }
                        else
                            if (psp_TypePan.Forecast == 2)
                            {
                                value2 =Math.Round(Convert.ToDouble(psp_TypePan.GetType().GetProperty("y" + i.ToString()).GetValue(psp_TypePan, null)),2 );
                                value = value2 -Math.Round( Convert.ToDouble(matdyh.GetType().GetProperty("y" + i.ToString()).GetValue(matdyh, null)),2);
                                matcgui.GetType().GetProperty("y" + i.ToString()).SetValue(matcgui, value, null);
                                Common.Services.BaseService.Update<Ps_Forecast_Math>(matcgui);
                            
                            }
                        
                    }

                
                }
        
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            IList<Ps_forecast_list> listReports = Common.Services.BaseService.GetList<Ps_forecast_list>("SelectPs_forecast_listByWhere", "UserID='" + Itop.Client.MIS.ProgUID + "' and Col1='2' and Title='" + forecastReport.Title + "'");
            if (listReports.Count > 0)
            {
                WaitDialogForm wait = new WaitDialogForm("", "正在更新数据, 请稍候...");
              //  DataRow[] drlist = dataTable.Select("Title like '%-%'");
                int typetemp = 0;
                foreach (DataRow dr in dataTable.Rows)
                {
                    Ps_Forecast_Math psp_TypePan = new Ps_Forecast_Math();
                    string[] str = new string[2];
                    if (dr["Title"].ToString().Contains("电量"))
                    {
                        typetemp = 2;
                        str[1] = dr["Title"].ToString();
                        if (dr["Title"].ToString().Substring(dr["Title"].ToString().Length - 2, 2) == "电量")
                            str[1] = dr["Title"].ToString().Substring(0, dr["Title"].ToString().Length - 2);
                    }
                    else if (dr["Title"].ToString().Contains("负荷"))
                    {
                        typetemp = 3;

                        str[1] = dr["Title"].ToString();
                        if (dr["Title"].ToString().Substring(dr["Title"].ToString().Length - 2, 2) == "负荷")
                            str[1] = dr["Title"].ToString().Substring(0, dr["Title"].ToString().Length - 2);
                    }
                    //psp_Type.Forecast = 2;
                    //psp_Type.ForecastID = listReports[0].ID;
                    //psp_Type.Title=str[1];
                    if (str[1]!=null&&str[1].Contains("全社会") && !str[1].Contains("(全地区)"))
                    {
                        str[1]=str[1]+"(全地区)";
                    }
                    object obj = Common.Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere",
                        "Forecast='" + typetemp + "' and ForecastID='" + listReports[0].ID + "' and Title='" + str[1] + "'");
                    if (obj == null)
                        continue;
                    Ps_Forecast_Math psp_Type = obj as Ps_Forecast_Math;
                    psp_TypePan = obj as Ps_Forecast_Math;
                   
                   Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(dr);
                   v.ID = psp_Type.ID;
                   v.ParentID = psp_Type.ParentID;
                   v.Forecast = psp_Type.Forecast;
                   v.ForecastID = psp_Type.ForecastID;
                   v.Sort = psp_Type.Sort;
                   v.Title = psp_Type.Title;
                   v.Col1 = psp_Type.Col1;
                   v.Col2 = psp_Type.Col2;
                   v.Col3 = psp_Type.Col3;
                   v.Col4 = psp_Type.Col4;
                   Common.Services.BaseService.Update<Ps_Forecast_Math>(v);


                   IList<Ps_Forecast_Math> list = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere",
                  "Forecast='" + typetemp + "' and ForecastID='" + listReports[0].ID + "' and ParentID='" + psp_Type.ID + "'" + " and ( Title like '常规%' or Title  like '大用户%' or Title  like '同时率%'   ) order by sort");
                   if (list.Count == 0)
                       continue;
                   Ps_Forecast_Math pfscgui = new Ps_Forecast_Math();
                   Ps_Calc pcs = new Ps_Calc();
                   pcs.Forecast = type;
                   pcs.ForecastID = forecastReport.ID;
                   pcs.CalcID = dr["ID"].ToString().Trim();
                   IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByWhere", "Forecast = '" + type + "' and ForecastID ='" + forecastReport.ID + "' and  CalcID = '" + dr["ID"].ToString().Trim() + "' order by Value2");
                   double zzl = 0;
                   int syear = 0, eyear = 0;
                   foreach (Ps_Calc pc11 in list1)
                   {


                       //if (pc11.CalcID == dataRow["ID"].ToString().Trim())
                       //{
                     

                       zzl = pc11.Value4;
                       syear = Convert.ToInt32(pc11.Value2);
                       eyear = Convert.ToInt32(pc11.Value3);
                       //}
                       //else
                       //    continue;



                       //if (!bl)
                       //{
                       //    if (fyear != 0 && syear != 0)
                       //    {
                       //        double[] historyValues = GenerateHistoryValue(dataRow, fyear, syear);
                       //        zzl = Calculator.AverageIncreasing(historyValues);
                       //        Ps_Calc pcs1 = new Ps_Calc();
                       //        pcs1.ID = Guid.NewGuid().ToString();
                       //        pcs1.Forecast = type;
                       //        pcs1.ForecastID = forecastReport.ID;
                       //        pcs1.CalcID = dataRow["ID"].ToString();
                       //        pcs1.Value1 = zzl;
                       //        pcs.Value2 = Convert.ToDouble(comboBox4.SelectedItem.ToString().Replace("年", ""));
                       //        pcs.Value3 = Convert.ToDouble(comboBox5.SelectedItem.ToString().Replace("年", ""));
                       //        pcs.Value4 = Convert.ToDouble(dr["B"].ToString().Replace("%", ""));
                       //        pcs.Col1 = comboBox1.SelectedItem.ToString();
                       //        Services.BaseService.Create<Ps_Calc>(pcs1);
                       //    }

                       //}
                       if (syear != 0 && eyear != 0)
                       {
                           double value1 = 0;
                           double value2 = 0;
                           double value3 = 0;
                           foreach (Ps_Forecast_Math ms in list)
                           {
                               // value1 = (double)dataRow["y" + (i )]; 

                               // value1 = (double)dataRow["y" + (i )]; 
                               if (ms.Title.Contains("常规"))
                               {

                                   pfscgui = ms;
                               }
                           }
                           for (int i = syear; i <= eyear; i++)
                           {
                               try
                               {
                                   foreach (Ps_Forecast_Math ms in list)
                                   {
                                       // value1 = (double)dataRow["y" + (i )]; 

                                       // value1 = (double)dataRow["y" + (i )]; 
                                       if (ms.Title.Contains("常规"))
                                       {
                                           value1 = Math.Round(Convert.ToDouble(ms.GetType().GetProperty("y" + (i-1)).GetValue(ms, null)), 2);
                                           pfscgui = ms;
                                       }

                                       else
                                           if (ms.Title.Contains("大用户"))
                                               value2 = Math.Round(Convert.ToDouble(ms.GetType().GetProperty("y" + (i )).GetValue(ms, null)), 2);

                                           else
                                               if (ms.Title.Contains("同时率"))
                                                   value3 = Math.Round(Convert.ToDouble(ms.GetType().GetProperty("y" + (i )).GetValue(ms, null)), 2);
                                   }

                               }
                               catch { }
                               value1 = Math.Round(value1 * (1 + zzl * 0.01), 2);
                               pfscgui.GetType().GetProperty("y" + (i)).SetValue(pfscgui, value1, null);
                               Common.Services.BaseService.Update<Ps_Forecast_Math>(pfscgui);
                           }
                       }


                   }



                   if (!v.Title.Contains("全社会"))
                   {
                       v = Common.Services.BaseService.GetOneByKey<Ps_Forecast_Math>(v.ParentID);

                       updateAllPan(v, listReports[0]);
                   }
                    //////////////计算
                    //string strtemp = "";
                    //if (typetemp==2)
                    //{
                    //   strtemp = " and Title!='同时率'";
                    //}
                    //else
                    //    if (typetemp == 3)
                    //    {
                    //        strtemp = ""; 
                    //    }
                    //IList<Ps_Forecast_Math> mathlist = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere",
                    //   "Forecast='" + typetemp + "' and ForecastID='" + listReports[0].ID + "' and ParentID='" + psp_TypePan.ID + "'" + strtemp);
                    //double value = 0;
                    //for (int i = listReports[0].StartYear; i < listReports[0].EndYear; i++)
                    //{
                    //    value = 0;
                    //    foreach (Ps_Forecast_Math mat in mathlist)
                    //    {
                    //        if (mat.Title=="同时率")
                    //        value*= (double)mat.GetType().GetProperty("y" + i.ToString()).GetValue(mat, null);
                    //        else
                    //        value += (double)mat.GetType().GetProperty("y" + i.ToString()).GetValue(mat, null);

                    //    }
                    //    psp_TypePan.GetType().GetProperty("y" + i.ToString()).SetValue(psp_TypePan, value, null);
                    //    Common.Services.BaseService.Update<Ps_Forecast_Math>(psp_TypePan);
                    //}
                }
                wait.Close();
                MessageBox.Show("更新成功！");
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            RefreshChart();
        }

       

    }
}