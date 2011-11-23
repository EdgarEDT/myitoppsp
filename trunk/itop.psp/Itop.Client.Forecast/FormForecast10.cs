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
    public partial class FormForecast10 : FormBase
    {
        int type = 10;
        DataTable dataTable=new DataTable ();
        private Ps_forecast_list forecastReport = null;
        bool bLoadingData = false;
        bool _canEdit = true;
        string firstyear = "0";
        string endyear ="0";
        bool selectdral = true;
        Hashtable ha=new Hashtable ();
        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; EditRight = value; }
        }

        private bool EditRight = false;

        public FormForecast10(Ps_forecast_list fr)
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
                barButtonItem25.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                simpleButton4.Enabled = false;
                simpleButton6.Enabled = false;
            }
            if (!AddRight)
            {
              
            }

        }

        private void FormForecast10_Load(object sender, EventArgs e)
        {
            
            
             HideToolBarButton();
             ////chart1.Series.Clear();
             ////Show();
             Application.DoEvents();
             //this.Cursor = Cursors.WaitCursor;
             //gridView1.BeginUpdate();




             Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
             psp_Type.ForecastID = forecastReport.ID;
             psp_Type.Forecast = type;
             Common.Services.BaseService.Update("DeletePs_Forecast_MathForecastIDAndForecast", psp_Type);
             psp_Type.Forecast = 0;
             IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
             foreach (Ps_Forecast_Math psp_Typetemp in listTypes)
             {
                 psp_Type = new Ps_Forecast_Math();
                 psp_Type = psp_Typetemp;
                 psp_Type.ID = psp_Type.ID+"|10";
                 psp_Type.Forecast = type;
                 psp_Type.ParentID = psp_Type.ParentID + "|10";
                 Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type);
             }
             LoadData();
             //gridView1.EndUpdate();
             RefreshChart();
             this.Cursor = Cursors.Default;



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
             }
             ha.Clear();
            Ps_Calc pcs = new Ps_Calc();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
            foreach (Ps_Calc pcs2 in list1)
            {

                if ("年增长率法" == pcs2.CalcID)
                {
                    if (pcs2.Value1 > 0)
                    {
                        if (!ha.ContainsKey("年增长率法"))
                            ha.Add("年增长率法", pcs2.Value2);
                    }
                }
                else
                if ("弹性系数法" == pcs2.CalcID)
                {
                    if (pcs2.Value1 > 0)
                    {
                        if (!ha.ContainsKey("弹性系数法"))
                        ha.Add("弹性系数法", pcs2.Value2);
                    }
                    

                   

                }
                else
                    if ("指数平滑法" == pcs2.CalcID)
                    {
                        if (pcs2.Value1 > 0)
                        {
                            if (!ha.ContainsKey("指数平滑法"))
                                ha.Add("指数平滑法", pcs2.Value2);
                        }
                    }
                    else
                        if ("灰色模型法" == pcs2.CalcID)
                        {
                            if (pcs2.Value1 > 0)
                            {
                                if (!ha.ContainsKey("灰色模型法"))
                                    ha.Add("灰色模型法", pcs2.Value2);
                            }
                        }
                        else
                            if ("专家指定法" == pcs2.CalcID)
                            {
                                if (pcs2.Value1 > 0)
                                {
                                    if (!ha.ContainsKey("专家指定法"))
                                        ha.Add("专家指定法", pcs2.Value2);
                                }
                            }
                            else
                                if ("外推法" == pcs2.CalcID)
                                {
                                    if (pcs2.Value1 > 0)
                                    {
                                        if (!ha.ContainsKey("外推法"))
                                            ha.Add("外推法", pcs2.Value2);
                                    }
                                }
                                else
                                    if ("相关法" == pcs2.CalcID)
                                    {
                                        if (pcs2.Value1 > 0)
                                        {
                                            if (!ha.ContainsKey("相关法"))
                                                ha.Add("相关法", pcs2.Value2);
                                        }
                                    }
                                    else
                                        if ("人工神经网络法" == pcs2.CalcID)
                                        {
                                            if (pcs2.Value1 > 0)
                                            {
                                                if (!ha.ContainsKey("人工神经网络法"))
                                                    ha.Add("人工神经网络法", pcs2.Value2);
                                            }
                                        }
           
                                        
            }
            if (ha.Count < 1)
            {
                simpleButton2.Enabled = false;

            }
            else
            {
                JS(); 
            }
        }

        private void LoadData()
        {
            this.splitContainerControl1.SplitterPosition = (2* this.splitterControl1.Width) / 3;
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
                            v.Title =(i + 1).ToString() + "." + row["Title"].ToString();
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
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                chart1.Series[i].Color = (Color)hs[i];
                chart1.Series[i].Name = chart1.Series[i].Name;
                chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;
                chart1.Series[i].MarkerImage = al[i % 7].ToString();
                chart1.Series[i].MarkerSize = 7;
                chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(2);

                chart1.Series[i].XValueIndexed = false;
            }

            chart1.ChartAreas["Default"].AxisX.MinorGrid.Enabled = false;
            chart1.ChartAreas["Default"].AxisY.MinorGrid.Enabled = true;
            chart1.ChartAreas["Default"].AxisY.MinorGrid.LineStyle = ChartDashStyle.Dash;
            chart1.ChartAreas["Default"].AxisY.MinorGrid.LineColor = Color.Gray;
        }

        private InputLanguage oldInput = null;

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            Common.Services.BaseService.Update("DeletePs_Forecast_MathForecastIDAndForecast", psp_Type);
            psp_Type.Forecast = 0;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
            foreach (Ps_Forecast_Math psp_Typetemp in listTypes)
           {
               psp_Type = new Ps_Forecast_Math();
               psp_Type = psp_Typetemp;
               psp_Type.ID = Guid.NewGuid().ToString();
               psp_Type.Forecast = type;
               Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type);
           }
         

            LoadData();

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
            this.chart1.SaveAsImage(sf.FileName, ci);
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

            JS();

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
            
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            
         
        }

        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {

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
            //if (firstyear == "0" && endyear == "0")
            //{
            //    MsgBox.Show("请设置历史数据起始年结束年后再点参数设置");
            //    return;
            //}


            FormForecastCalc10 fc = new FormForecastCalc10();
            fc.DTable = dataTable;
            fc.ISEdit = EditRight;
            fc.PForecastReports = forecastReport;
            if (fc.ShowDialog() != DialogResult.OK)
                return;
            ha = fc.Ha;
            if (ha!=null&&ha.Count < 1)
            {
                simpleButton2.Enabled = false;

            }
            else
            {
                simpleButton2.Enabled = true;
            }
            JS();
      
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
            if (ha == null)
                return;
            if (ha.Count <1)
                return;

            IList<Ps_Forecast_Math> list = new List<Ps_Forecast_Math>();
            Ps_Forecast_Math psmtemp=new Ps_Forecast_Math ();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                //if (fyear != 0 && syear != 0)
                //{
                 //   double[] historyValues = GenerateHistoryValue(dataRow, fyear, syear);
                //    double[] yn = new double[eyear - syear];
                double[] yn = new double[forecastReport.EndYear - forecastReport.StartYear+1];
                    foreach (string index in ha.Keys)
                    {
                        switch (index)
                        {
                            case "年增长率法":
                                list.Clear();
                                psmtemp = new Ps_Forecast_Math();
                                psmtemp.ForecastID = forecastReport.ID;
                                psmtemp.Forecast = 1;
                              
                              
                                break;
                            case "弹性系数法":
                                list.Clear();
                                psmtemp = new Ps_Forecast_Math();
                                psmtemp.ForecastID = forecastReport.ID;
                                psmtemp.Forecast =4;
                               
                                break;
                           
                            case "指数平滑法":
                                list.Clear();
                                psmtemp = new Ps_Forecast_Math();
                                psmtemp.ForecastID = forecastReport.ID;
                                psmtemp.Forecast = 5;
                                break;
                            case "灰色模型法":
                                list.Clear();
                                psmtemp = new Ps_Forecast_Math();
                                psmtemp.ForecastID = forecastReport.ID;
                                psmtemp.Forecast = 6;
                                break;
                            case "专家指定法":
                                list.Clear();
                                psmtemp = new Ps_Forecast_Math();
                                psmtemp.ForecastID = forecastReport.ID;
                                psmtemp.Forecast = 7;
                                break;
                            case "外推法":
                                list.Clear();
                                psmtemp = new Ps_Forecast_Math();
                                psmtemp.ForecastID = forecastReport.ID;
                                psmtemp.Forecast = 2;
                                break;
                            case "相关法":
                                list.Clear();
                                psmtemp = new Ps_Forecast_Math();
                                psmtemp.ForecastID = forecastReport.ID;
                                psmtemp.Forecast = 3;
                                break;
                            
                            case "人工神经网络法":
                                list.Clear();
                                psmtemp = new Ps_Forecast_Math();
                                psmtemp.ForecastID = forecastReport.ID;
                                psmtemp.Forecast = 8;
                                break;
                        }
                        psmtemp.Title = dataRow["Title"].ToString();
                        list = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByForecastIDAndForecastTitle", psmtemp);
                               
                        if(list!=null&&list.Count>0)
                        {
                            for (int i = 0; i <= forecastReport.EndYear - forecastReport.StartYear; i++)
                        {
                            yn[i] += Convert.ToDouble(ha[index]) * Convert.ToDouble(list[0].GetType().GetProperty("y" + (forecastReport.StartYear + i)).GetValue(list[0], null));
                        
                        }
                        }
                    }

                    for (int i = 0; i <= forecastReport.EndYear - forecastReport.StartYear; i++)
                    {
                        dataRow["y" + (forecastReport.StartYear + i)] = yn[i];
                    }
          //      }

            
            }
            RefreshChart();
        
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
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                chart1.Series[i].Color = (Color)hs[i];
                chart1.Series[i].Name = (i + 1).ToString() + "." + chart1.Series[i].Name;
                chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;

                chart1.Series[i].MarkerSize = 7;
                chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(2);

            }

        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!CanEdit)
            {
                e.Cancel = true;
            }
        }


    }
}