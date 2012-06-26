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
using Itop.Domain.HistoryValue;
using Itop.Client.Forecast.FormAlgorithm_New;
namespace Itop.Client.Forecast
{
    public partial class FormForecast9SH : FormBase
    {
        int type = 20;
        DataTable dataTable=new DataTable ();
        private Ps_forecast_list forecastReport = null;
        private PublicFunction m_pf = new PublicFunction();
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

        public FormForecast9SH(Ps_forecast_list fr)
        {
            InitializeComponent();
            forecastReport = fr;
            Text = fr.Title;
            chart_user1.SetColor += new chart_userSH.setcolor(chart_user1_SetColor);
        }

        void chart_user1_SetColor()
        {
            FormColor fc = new FormColor();
            fc.DT = dataTable;
            fc.ID = forecastReport.ID.ToString();
            fc.For = type;
            if (fc.ShowDialog() == DialogResult.OK)
                RefreshChart();
        }

        private void HideToolBarButton()
        {
            if (!CanEdit)
            {
                barButtonItem17.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem25.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barButtonItem22.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem26.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                simpleButton4.Enabled = false;
                simpleButton6.Enabled = false;
            }
            if (!AddRight)
            {
              
            }

        }

        private void FormForecast9_Load(object sender, EventArgs e)
        {


            HideToolBarButton();
            //chart1.Series.Clear();
            //Show();
            Application.DoEvents();
            //this.Cursor = Cursors.WaitCursor;
            //treeList1.BeginUpdate();
           // InitData1();
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

            SetHistoryYear();
            

             IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByWhere", "  Forecast = '" + type + "' and ForecastID = '" + forecastReport.ID + "'and  Value2 != '0' and Value3 != '0' and  Value4 != '0' and Value5 != '0' and (Col4 != '') order by Value2");
             
            foreach (Ps_Calc pcs2 in list1)
            {
                ha.Add(ha.Count, pcs2.CalcID + "@" + pcs2.Value4 + "-" + pcs2.Value5 + "@" + pcs2.Value2 + "-" + pcs2.Value3
                    +"@"+ pcs2.Col4);
               
            }
            
          
           
            if (ha.Count < 1)
            {
                simpleButton2.Enabled = false;

            }
            else
            {
                CalcPredictedValue();
            }
        }
        //设置历史年份
        private void SetHistoryYear()
        {
            Ps_Forecast_Setup pfs = new Ps_Forecast_Setup();
            pfs.ID = Guid.NewGuid().ToString();
            pfs.Forecast = type;
            pfs.ForecastID = forecastReport.ID;
            pfs.StartYear = forecastReport.StartYear;
            pfs.EndYear = forecastReport.EndYear;

            IList<Ps_Forecast_Setup> li = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);
            if (li.Count == 0)
                Services.BaseService.Create<Ps_Forecast_Setup>(pfs);
            else
                Services.BaseService.Update("UpdatePs_Forecast_SetupByForecast", pfs);

            IList<Ps_Forecast_Setup> li2 = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);

            if (li2.Count != 0)
            {
                firstyear = li2[0].StartYear.ToString();
                endyear = li2[0].EndYear.ToString();

            }
        }
        Hashtable ht = new Hashtable();
        private void checkfixedvalue()
        {
            ht.Clear();
            if (forecastReport.Col1 == "1")
            {
                ht.Add("全社会最大负荷（MW）", 1);
            }
            else
            {
                ht.Add("全社会用电量（亿kWh）", 1);
            }
           

            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));

            commonhelp.CheckHasFixValue(ht, dataTable, forecastReport.ID, type);

        }
        Hashtable OldHt = new Hashtable();
        private void LoadData()
        {
            //this.splitContainerControl1.SplitterPosition = (2 * this.splitterControl1.Width) / 3;
           // this.splitContainerControl2.SplitterPosition = splitContainerControl2.Height / 2;
            checkfixedvalue();
            treeList1.DataSource = null;
            bLoadingData = true;
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }
            AddFixColumn();
            for (int i = forecastReport.StartYear; i <= forecastReport.YcEndYear; i++)
            {
                AddColumn(i);
            }
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));
            OldHt.Clear();
            foreach (DataRow row in dataTable.Rows)
            {
                if (!OldHt.ContainsKey(row["Title"].ToString()))
                {
                    OldHt.Add(row["Title"].ToString(), row["ID"].ToString());
                }
            }
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
        /*
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

            //    chart1.Series[i].XValueIndexed = true;
            //}
            //chart1.ChartAreas["Default"].AxisX.MinorGrid.Enabled = false;
            //chart1.ChartAreas["Default"].AxisY.MinorGrid.Enabled = true;
            //chart1.ChartAreas["Default"].AxisY.MinorGrid.LineStyle = ChartDashStyle.Dash;
            //chart1.ChartAreas["Default"].AxisY.MinorGrid.LineColor = Color.Gray;
            m_pf.SetChart(chart1, checkBox1, hs, al);
        }
        */
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
                            v.Title =row["Title"].ToString();
                            v.Sort = Convert.ToInt32(col.FieldName.Replace("y", ""));
                            v.y1990 = (double)row[col.FieldName];

                            listValues.Add(v);
                        }
                    }
                }


            }

            this.chart_user1.RefreshChart(listValues, "Title", "Sort", "y1990", hs);
           
           // m_pf.SetChart(chart1, checkBox1, hs, al);
        }


        private InputLanguage oldInput = null;

        private void InitData1()
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
               psp_Type.ID = psp_Type.ID + "|9";
               psp_Type.Forecast = type;
               psp_Type.ParentID = psp_Type.ParentID + "|9";
               Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type);
           }
         

            //LoadData();

            //RefreshChart();
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

         //   JS();
            //JS2();
            CalcPredictedValue();
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            Brush brush = null;
            Rectangle r = e.Bounds;
            Color c3 = Color.FromArgb(152, 122, 254);
            Color c4 = Color.FromArgb(152, 122, 254);
            //foreach (GridCell cell in alist)
            //{
          
                if (e.Column.FieldName.IndexOf("y")>-1)
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
            if (treeList1.Nodes.Count == 0)
            {
                MessageBox.Show("无数据，不能操作！");
                return;
            }
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
                v.Col4 = "yes";
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
            ////原来的参数设置

            //CS();
            //现在用的界面
            ArgumentSet();

        }
        /// <summary>
        /// 设置参数的新界面
        /// </summary>
        private void ArgumentSet()
        {
            FormArgumentSetNewSH W_ArgumentSet = new FormArgumentSetNewSH();
            W_ArgumentSet.Text = this.Text + "参数设置";
            W_ArgumentSet.DTable = dataTable;
            W_ArgumentSet.ISEdit = EditRight;
            W_ArgumentSet.PForecastReports = forecastReport;
            if (W_ArgumentSet.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            ha = W_ArgumentSet.Ha;
            if (ha != null && ha.Count < 1)
            {
                simpleButton2.Enabled = false;

            }
            else
            {
                simpleButton2.Enabled = true;
            }
            CalcPredictedValue();
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
            //CS();
            ArgumentSet();
        }

        private void CS()
        {
            //if (firstyear == "0" && endyear == "0")
            //{
            //    MsgBox.Show("请设置历史数据起始年结束年后再点参数设置");
            //    return;
            //}


           // FormForecastCalc9 fc = new FormForecastCalc9();
            FormForecastCalc9Third fc = new FormForecastCalc9Third();
            fc.Text = this.Text + "- 参数设置";
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
            JS2();
           
        }

         private void JS2()
         {
            //计算预测值
             Ps_Calc pcs = new Ps_Calc();
            
             IList<Ps_Calc> list1;


           
            int syear = forecastReport.StartYear;
            int eyear = forecastReport.EndYear;
            int histsyear = 0;
            int histeyear = 0;
            if (ha == null)
                return;
            if (ha.Count <1)
                return;
            bool isfalse = false;
            IList<Ps_Forecast_Math> list = new List<Ps_Forecast_Math>();
            Ps_Forecast_Math psmtemp=new Ps_Forecast_Math ();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                //if (fyear != 0 && syear != 0)
                //{
                 //   double[] historyValues = GenerateHistoryValue(dataRow, fyear, syear);
               //     double[] yn = new double[eyear - syear];


                for (int intextemp = 0; intextemp < ha.Count; intextemp++)
                {
                    string index = ha[intextemp].ToString();
                    string name = index.Split('@')[0];
                    string strhistyear = index.Split('@')[1];
                    string strforecastyear = index.Split('@')[2];
                    histsyear = Convert.ToInt32(strhistyear.Split('-')[0].ToString());
                    histeyear = Convert.ToInt32(strhistyear.Split('-')[1].ToString());
                    syear = Convert.ToInt32(strforecastyear.Split('-')[0].ToString());
                    eyear = Convert.ToInt32(strforecastyear.Split('-')[1].ToString());

                    double[] yn = new double[eyear - syear + 1];
                    double zzl = 0;
                    bool bl = false;
                    double value1 = 0;
                    try { value1 = (double)dataRow["y" + histeyear]; }
                    catch { }
                    int M = histeyear - histsyear;
                    double[] yn1 = new double[M];

                    double M1 = 0;
                    double M2 = 0;
                    double M3 = 0;
                    double M4 = 0;
                    double[] historyValues = GenerateHistoryValue(dataRow, histsyear, histeyear);
                    switch (name)
                    {
                        case "外推法(年增长率法)":
                        case "年增长率法":
                            zzl = Calculator.AverageIncreasing(historyValues);
                            if (syear != 0 && eyear != 0)
                            {
                                for (int i = 1; i <= eyear - syear + 1; i++)
                                {
                                    pcs = new Ps_Calc();
                                    pcs.Forecast = 1;
                                    pcs.ForecastID = forecastReport.ID;
                                    pcs.CalcID = dataRow["ID"].ToString().Trim();
                                    list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByWhere", "Forecast = '" + type + "' and ForecastID ='" + forecastReport.ID + "' and  CalcID = '" + dataRow["ID"].ToString().Trim() + "' order by Value2");

                                    foreach (Ps_Calc pc11 in list1)
                                    {
                                        if (syear + i - 1 >= pc11.Value2 && syear + i - 1 <= pc11.Value3)
                                        {
                                            zzl = pc11.Value4;
                                        }
                                       
                                    }
                                    dataRow["y" + (syear + i - 1)] = value1 * Math.Pow(1 + zzl, i);
                                }
                            }


                            break;



                        case "外推法(直线)": 
                        case "相关法(直线)":
                            yn = Calculator.One(historyValues, eyear - syear + 1, ref M1, ref M2);
                            yn1 = Calculator.One1(M, M1, M2);
                            if (M1 == -999999 && M2 == -999999)
                            {
                                isfalse = true;
                                MsgBox.Show("此算法模拟失败，方程无解");
                                break;
                            }
                            for (int i = 1; i <= eyear - syear+1; i++)
                            {
                                dataRow["y" + (syear + i-1)] = yn[i - 1];
                            }
                            break;
                        case "外推法(抛物线)":
                        case "相关法(抛物线)":
                            yn = Calculator.Second(historyValues, eyear - syear + 1, ref M1, ref M2, ref M3);
                            yn1 = Calculator.Second1(M, M1, M2, M3);
                            if (M1 == -999999 && M2 == -999999)
                            {
                                isfalse = true;
                                MsgBox.Show("此算法模拟失败，方程无解");
                                break;
                            }
                            for (int i = 1; i <= eyear - syear + 1; i++)
                            {
                                dataRow["y" + (syear + i - 1)] = yn[i - 1];
                            }

                            break;
                        case "外推法(三阶)":
                        case "相关法(三阶)":
                            yn = Calculator.Three(historyValues, eyear - syear + 1, ref M1, ref M2, ref M3, ref M4);
                            yn1 = Calculator.Three1(M, M1, M2, M3, M4);
                            if (M1 == -999999 && M2 == -999999)
                            {
                                isfalse = true;
                                MsgBox.Show("此算法模拟失败，方程无解");
                                break;
                            }
                            for (int i = 1; i <= eyear - syear + 1; i++)
                            {
                                dataRow["y" + (syear + i - 1)] = yn[i - 1];
                            }

                            break;
                        case "外推法(指数)":
                        case "相关法(指数)":
                            yn = Calculator.Index(historyValues, eyear - syear + 1, ref M1, ref M2);
                            yn1 = Calculator.Index1(M, M1, M2);
                            if (M1 == -999999 && M2 == -999999)
                            {
                                isfalse = true;
                                MsgBox.Show("此算法模拟失败，方程无解");
                                break;
                            }
                            for (int i = 1; i <= eyear - syear + 1; i++)
                            {
                                dataRow["y" + (syear + i - 1)] = yn[i - 1];
                            }

                            break;
                        case "外推法(几何曲线)":
                        case "相关法(几何曲线)":
                            yn = Calculator.LOG(historyValues, eyear - syear + 1, ref M1, ref M2);
                            yn1 = Calculator.LOG1(M, M1, M2);
                            if (M1 == -999999 && M2 == -999999)
                            {
                                isfalse = true;
                                MsgBox.Show("此算法模拟失败，方程无解");
                                break;
                            }
                            for (int i = 1; i <= eyear - syear + 1; i++)
                            {
                                dataRow["y" + (syear + i - 1)] = yn[i - 1];
                            }

                            break;
                        case "弹性系数法":
                            Calculator.StartYear = syear;
                            Calculator.Type = type.ToString();
                            yn = Calculator.SpringCoefficientMethod((double)dataRow["y" + (syear-1)], eyear - syear+1, forecastReport.ID);

                            for (int i = 1; i <= eyear - syear+1; i++)
                            {
                                dataRow["y" + (syear + i-1)] = yn[i - 1];
                            }
                            break;

                        case "指数平滑法":
                            pcs = new Ps_Calc();
                            pcs.Forecast = 5;
                            pcs.ForecastID = forecastReport.ID;
                            list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
                            if (list1.Count > 0)
                            {
                             
                                yn = Calculator.IndexSmoothMethod(historyValues, eyear - syear + 1,list1[0].Value1 );
                            }
                            else
                            {
                                yn = Calculator.IndexSmoothMethod(historyValues, eyear - syear + 1, 0.1);
                            }
                         


                         
                            
                                for (int i = 1; i <= eyear - syear+1; i++)
                                {
                                    dataRow["y" + (syear + i - 1)] = yn[i - 1];
                                }
                            

                            break;
                        case "灰色模型法":
                            yn = Calculator.GrayMethod(historyValues, eyear - syear + 1);
                            for (int i = 1; i <= eyear - syear + 1; i++)
                            {
                                dataRow["y" + (syear + i - 1)] = yn[i - 1];
                            }
                            break;
                        case "专家决策法":
                            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                            psp_Type.ForecastID = forecastReport.ID;
                            psp_Type.Forecast = 7;
                            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
                            foreach (Ps_Forecast_Math psp_Typetemp in listTypes)
                            {
                                if (psp_Typetemp.Title == dataRow["Title"].ToString())
                                {
                                    double firstvalue = 0;
                                    double secvalue = 0;
                                    for (int i = histsyear; i <= eyear-1; i++)
                                    {
                                        firstvalue=(double)psp_Typetemp.GetType().GetProperty("y" + i).GetValue(psp_Typetemp,null);
                                        secvalue = (double)psp_Typetemp.GetType().GetProperty("y" + (i+1)).GetValue(psp_Typetemp, null);
                                          double dbtemp=0;
                                        if(dataRow["y"+i]!=DBNull.Value&&dataRow["y"+i]!=null&&dataRow["y"+i].ToString()!="")
                                         dbtemp=Convert.ToDouble(dataRow["y"+i]);
                                        else
                                            dbtemp=0;
                                        if (firstvalue != 0)
                                             dataRow["y" + (i + 1)] = dbtemp *(1+ Math.Round((secvalue - firstvalue) / firstvalue, 2));
                                        else
                                        {
                                            dataRow["y" + (i + 1)] = 0;
                                        }
                                    }
                                }
                            }
                            break;
                       
                        //case "相关法":
                        //    list.Clear();
                        //    psmtemp = new Ps_Forecast_Math();
                        //    psmtemp.ForecastID = forecastReport.ID;
                        //    psmtemp.Forecast = 3;
                        //    break;

                        //case "人工神经网络法":
                        //    list.Clear();
                        //    psmtemp = new Ps_Forecast_Math();
                        //    psmtemp.ForecastID = forecastReport.ID;
                        //    psmtemp.Forecast = 8;
                        //    break;
                    }
                   
                }

                   
             //   }
                if(isfalse)
                {
                    break;
                }
            
            }
            ForecastClass fc = new ForecastClass();
            fc.MaxForecast(forecastReport, dataTable);

            RefreshChart();
         }
        private void JS()
        {
            //计算预测值
            Ps_Calc pcs = new Ps_Calc();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
            IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);


            //int fyear = int.Parse(firstyear.Replace("y", ""));
            //int syear = int.Parse(endyear.Replace("y", ""));
            int syear = forecastReport.StartYear;
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
               //     double[] yn = new double[eyear - syear];
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
                            
                           string[] strvalueyear= ha[index].ToString().Split('@');
                           if (strvalueyear.Length < 2) continue;
                           for (int i = Convert.ToInt32(strvalueyear[0]); i <= Convert.ToInt32(strvalueyear[1]); i++)
                           {
                               yn[i - syear] = Convert.ToDouble(list[0].GetType().GetProperty("y" + (i)).GetValue(list[0], null));
                               
                               //yn[Convert.ToInt32(strvalueyear[0]) - syear] = Convert.ToDouble(list[0].GetType().GetProperty("y" + (i)).GetValue(list[0], null));
                               //dataRow["y" + i] = yn[i - syear];
                           }
                        
                        }
                    }

                    for (int i = 0; i <= eyear - syear; i++)
                    {
                        dataRow["y" + (syear + i)] = yn[i];
                    }
             //   }

            
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

            for (int i = 0; i < treeList1.Nodes.Count; i++)
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

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //ForecastClass fc = new ForecastClass();
            //fc.BadForecast(type, forecastReport);

            //LoadData();

            //RefreshChart();


            FormForecastLoadData2 ffs = new FormForecastLoadData2();
            ffs.PID = MIS.ProgUID;
            ffs.StartYear = forecastReport.StartYear;
            ffs.EndYear = forecastReport.YcEndYear;
            if (ffs.ShowDialog() != DialogResult.OK)
                return;


            Hashtable hs = ffs.HS;

            if (hs.Count == 0)
                return;
            string id = Guid.NewGuid().ToString();
            
                foreach (Ps_History de3 in hs.Values)
                {
                    if (OldHt.ContainsKey(de3.Title))
                    {
                        Ps_Forecast_Math py = Common.Services.BaseService.GetOneByKey<Ps_Forecast_Math>(OldHt[de3.Title]);
                        for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
                        {
                            commonhelp.ResetValue(py.ID, "y" + i);
                            py.GetType().GetProperty("y" + i).SetValue(py, de3.GetType().GetProperty("y" + i).GetValue(de3, null), null);
                        }

                        Services.BaseService.Update<Ps_Forecast_Math>(py);
                    }
                    else
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
                        if (de3.ParentID == "")
                        {
                            ForecastMath.ParentID = "";
                        }
                        else
                        {
                            ForecastMath.ParentID = id + "|" + de3.ParentID;
                        }

                        ForecastMath.Forecast = type;
                        ForecastMath.ForecastID = forecastReport.ID;
                        ForecastMath.Sort = de3.Sort;
                        Services.BaseService.Create("InsertPs_Forecast_MathbyPs_History", ForecastMath);
                    }
                }

          

            LoadData();
            this.chart_user1.All_Select(true);
            RefreshChart();
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if(!CanEdit)
            {
                e.Cancel=true;
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            RefreshChart();
        }

       
        /// <summary>
        /// 最新的预测值方法给“复合预测”中的“参数设置”调用
        /// </summary>
        private void CalcPredictedValue()
        {
            Ps_Calc pcs = new Ps_Calc();

            IList<Ps_Calc> list1;

            //权重值
            string m_StrWeightedValue = "";
            double m_nWeightedValue = 0.0;

            //临时存放计算好的每组预测数据,例如TempData[1,0],一维：用了哪些算法，二维：每个算法有哪些年份数据
            //
            double[,] TempData = null;
            ////历史年份
            //string strHYears = ha[0].ToString().Split('@')[1];
            ////历史年份的起始年
            //int nHYears = Convert.ToInt32(strHYears.Split('-')[0].ToString());

            //预测年份
            string strFYears = ha[0].ToString().Split('@')[2];
            //预测年份的结束年
            int nFYears = Convert.ToInt32(strFYears.Split('-')[1].ToString());

            int syear = Convert.ToInt32(strFYears.Split('-')[0].ToString()); ;
            int eyear = forecastReport.EndYear;
            TempData = new double[(ha.Count), (eyear - syear + 1)];
            //历史年份的起始年
            int histsyear = 0;
            //历史年份的结束年
            int histeyear = 0;
            if (ha == null)
                return;
            if (ha.Count < 1)
                return;
            bool isfalse = false;
            IList<Ps_Forecast_Math> list = new List<Ps_Forecast_Math>();
            Ps_Forecast_Math psmtemp = new Ps_Forecast_Math();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                //if (fyear != 0 && syear != 0)
                //{
                //   double[] historyValues = GenerateHistoryValue(dataRow, fyear, syear);
                //     double[] yn = new double[eyear - syear];

                int m_Index = 0;//TempData的一维下标
                for (int intextemp = 0; intextemp < ha.Count; intextemp++)
                {
                    string index = ha[intextemp].ToString();
                    string name = index.Split('@')[0];
                    string strhistyear = index.Split('@')[1];
                    string strforecastyear = index.Split('@')[2];
                    //权重值
                    m_StrWeightedValue = index.Split('@')[3];
                    m_nWeightedValue = Convert.ToDouble(m_StrWeightedValue.Split('%')[0].ToString());
                    //历史年份的起始年
                    histsyear = Convert.ToInt32(strhistyear.Split('-')[0].ToString());
                    //历史年份的结束年
                    histeyear = Convert.ToInt32(strhistyear.Split('-')[1].ToString());
                    //预测年份的结束年
                    syear = Convert.ToInt32(strforecastyear.Split('-')[0].ToString());
                    
                    //eyear = Convert.ToInt32(strforecastyear.Split('-')[1].ToString());

                    double[] yn = new double[eyear - syear + 1];
                    double zzl = 0;
                    bool bl = false;
                    double value1 = 0;
                    try { value1 = (double)dataRow["y" + histeyear]; }
                    catch { }
                    int M = histeyear - histsyear;
                    double[] yn1 = new double[M];

                    double M1 = 0;
                    double M2 = 0;
                    double M3 = 0;
                    double M4 = 0;
                    double[] historyValues = GenerateHistoryValue(dataRow, histsyear, histeyear);
                    
                    switch (name)
                    {
                        case "外推法(年增长率法)":
                        case "年增长率法":
                            //计算年均增长率
                            //zzl = Calculator.AverageIncreasing(historyValues);
                            try
                            {
                                zzl = Math.Round(Calculator.AverageIncreasing(historyValues) * 100, 2);
                            }
                            catch (System.Exception e)
                            {
                            	
                            }
                            if (syear != 0 && eyear != 0)
                            {
                                for (int i = 1; i <= eyear - syear + 1; i++)
                                {
                                    //pcs = new Ps_Calc();
                                    //pcs.Forecast = 1;
                                    //pcs.ForecastID = forecastReport.ID;
                                    //pcs.CalcID = dataRow["ID"].ToString().Trim();
                                    //list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByWhere", "Forecast = '" + type + "' and ForecastID ='" + forecastReport.ID + "' and  CalcID = '" + dataRow["ID"].ToString().Trim() + "' order by Value2");

                                    //foreach (Ps_Calc pc11 in list1)
                                    //{
                                    //    if (syear + i - 1 >= pc11.Value2 && syear + i - 1 <= pc11.Value3)
                                    //    {
                                    //        zzl = pc11.Value4;
                                    //    }

                                    //}
                                    try {
                                            if(i==1)
                                            {
                                                value1 = (double)dataRow["y" + (i + syear - 2)];
                                                TempData[m_Index, (i - 1)] = 0;
                                                TempData[m_Index, (i - 1)] = value1 * (1 + zzl * 0.01) * (m_nWeightedValue / 100);
                                            }
                                            else
                                            {
                                                value1 = TempData[m_Index, (i - 2)];
                                                TempData[m_Index, (i - 1)] = 0;
                                                TempData[m_Index, (i - 1)] = value1 * (1 + zzl * 0.01) ;
                                            }
                                        }
                                    catch { }
                                        //dataRow["y" + (syear + i - 1)] = value1 * Math.Pow(1 + zzl, i);
                                  
                                }
                            }


                            break;



                        case "外推法(直线)":
                        case "相关法(直线)":
                            try
                            {
                                yn = Calculator.One(historyValues, eyear - syear + 1, ref M1, ref M2);
                                yn1 = Calculator.One1(M, M1, M2);
                            }
                            catch (System.Exception e)
                            {
                            	
                            }
                            if (M1 == -999999 && M2 == -999999)
                            {
                                isfalse = true;
                                MsgBox.Show("此算法模拟失败，方程无解");
                                break;
                            }
                            for (int i = 1; i <= eyear - syear + 1; i++)
                            {
                                //dataRow["y" + (syear + i - 1)] = yn[i - 1];
                                TempData[m_Index, (i - 1)] = 0;
                                TempData[m_Index, (i - 1)] = yn[i - 1] * (m_nWeightedValue / 100);
                            }
                            break;
                        case "外推法(抛物线)":
                        case "相关法(抛物线)":
                            try
                            {
                                yn = Calculator.Second(historyValues, eyear - syear + 1, ref M1, ref M2, ref M3);
                                yn1 = Calculator.Second1(M, M1, M2, M3);
                            }
                            catch (System.Exception e)
                            {
                            	
                            }
                            if (M1 == -999999 && M2 == -999999)
                            {
                                isfalse = true;
                                MsgBox.Show("此算法模拟失败，方程无解");
                                break;
                            }
                            for (int i = 1; i <= eyear - syear + 1; i++)
                            {
                                //dataRow["y" + (syear + i - 1)] = yn[i - 1];
                                TempData[m_Index, (i - 1)] = 0;
                                TempData[m_Index, (i - 1)] = yn[i - 1] * (m_nWeightedValue / 100);
                            }

                            break;
                        case "外推法(三阶)":
                        case "相关法(三阶)":
                            try
                            {
                                yn = Calculator.Three(historyValues, eyear - syear + 1, ref M1, ref M2, ref M3, ref M4);
                                yn1 = Calculator.Three1(M, M1, M2, M3, M4);
                            }
                            catch (System.Exception e)
                            {
                            	
                            }
                            if (M1 == -999999 && M2 == -999999)
                            {
                                isfalse = true;
                                MsgBox.Show("此算法模拟失败，方程无解");
                                break;
                            }
                            for (int i = 1; i <= eyear - syear + 1; i++)
                            {
                                //dataRow["y" + (syear + i - 1)] = yn[i - 1];
                                TempData[m_Index, (i - 1)] = 0;
                                TempData[m_Index, (i - 1)] = yn[i - 1] * (m_nWeightedValue / 100);
                            }

                            break;
                        case "外推法(指数)":
                        case "相关法(指数)":
                            try
                            {
                                yn = Calculator.Index(historyValues, eyear - syear + 1, ref M1, ref M2);
                                yn1 = Calculator.Index1(M, M1, M2);
                            }
                            catch (System.Exception e)
                            {
                            	
                            }
                            if (M1 == -999999 && M2 == -999999)
                            {
                                isfalse = true;
                                MsgBox.Show("此算法模拟失败，方程无解");
                                break;
                            }
                            for (int i = 1; i <= eyear - syear + 1; i++)
                            {
                                //dataRow["y" + (syear + i - 1)] = yn[i - 1];
                                TempData[m_Index, (i - 1)] = 0;
                                TempData[m_Index, (i - 1)] = yn[i - 1] * (m_nWeightedValue / 100);
                            }

                            break;
                        case "外推法(几何曲线)":
                        case "相关法(几何曲线)":
                            try
                            {
                                yn = Calculator.LOG(historyValues, eyear - syear + 1, ref M1, ref M2);
                                yn1 = Calculator.LOG1(M, M1, M2);
                            }
                            catch (System.Exception e)
                            {
                            	
                            }
                            if (M1 == -999999 && M2 == -999999)
                            {
                                isfalse = true;
                                MsgBox.Show("此算法模拟失败，方程无解");
                                break;
                            }
                            for (int i = 1; i <= eyear - syear + 1; i++)
                            {
                                //dataRow["y" + (syear + i - 1)] = yn[i - 1];
                                TempData[m_Index, (i - 1)] = 0;
                                TempData[m_Index, (i - 1)] = yn[i - 1] * (m_nWeightedValue / 100);
                            }

                            break;
                        case "弹性系数法":
                            Calculator.StartYear = syear;
                            //Calculator.Type = type.ToString();
                            Calculator.Type = "24";//在复合算法的弹性系数法中参数设置的类型是24
                            historyValues = GenerateHistoryValue(dataRow, histsyear, syear);
                            try
                            {
                                yn = Calculator.SpringCoefficientMethod((double)dataRow["y" + (syear-1)], eyear - syear+1, forecastReport.ID);
                            }
                            catch (System.Exception e)
                            {
                            	
                            }

                            for (int i = 1; i <= eyear - syear+1; i++)
                            {
                                //dataRow["y" + (syear + i - 1)] = yn[i - 1];
                                TempData[m_Index, (i - 1)] = 0;
                                TempData[m_Index, (i - 1)] = yn[i - 1] * (m_nWeightedValue / 100);
                            }
                            break;

                        case "指数平滑法":
                            pcs = new Ps_Calc();
                            //pcs.Forecast = 5;
                            pcs.Forecast = 25;
                            pcs.ForecastID = forecastReport.ID;
                            list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
                            if (list1.Count > 0)
                            {

                                yn = Calculator.IndexSmoothMethod(historyValues, eyear - syear + 1, list1[0].Value1);
                            }
                            else
                            {
                                yn = Calculator.IndexSmoothMethod(historyValues, eyear - syear + 1, 0.2);
                            }




                            ////计算结果值
                            for (int i = 1; i <= eyear - syear + 1; i++)
                            {
                                //dataRow["y" + (syear + i - 1)] = yn[i - 1];
                                TempData[m_Index, (i - 1)] = 0;
                                TempData[m_Index, (i - 1)] = yn[i - 1] * Convert.ToDouble(m_nWeightedValue / 100.00);
                            }


                            break;
                        case "灰色模型法":
                            try
                            {
                                yn = Calculator.GrayMethod(historyValues, eyear - syear + 1);
                            }
                            catch (System.Exception e)
                            {
                            	
                            }
                            for (int i = 1; i <= eyear - syear + 1; i++)
                            {
                                //dataRow["y" + (syear + i - 1)] = yn[i - 1];
                                TempData[m_Index, (i - 1)] = 0;
                                TempData[m_Index, (i - 1)] = yn[i - 1] * (m_nWeightedValue / 100);
                            }
                            break;
                        case "专家决策法":
                            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
                            psp_Type.ForecastID = forecastReport.ID;
                            psp_Type.Forecast = 7;
                            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
                            foreach (Ps_Forecast_Math psp_Typetemp in listTypes)
                            {
                                if (psp_Typetemp.Title == dataRow["Title"].ToString())
                                {
                                    double firstvalue = 0;
                                    double secvalue = 0;
                                    for (int i = syear; i <= eyear ; i++)
                                    {
                                        firstvalue = (double)psp_Typetemp.GetType().GetProperty("y" + i).GetValue(psp_Typetemp, null);
                                        secvalue = (double)psp_Typetemp.GetType().GetProperty("y" + (i + 1)).GetValue(psp_Typetemp, null);
                                        double dbtemp = 0;
                                        if (dataRow["y" + i] != DBNull.Value && dataRow["y" + i] != null && dataRow["y" + i].ToString() != "")
                                            dbtemp = Convert.ToDouble(dataRow["y" + i]);
                                        else
                                            dbtemp = 0;
                                        if (firstvalue != 0)
                                        {
                                            //dataRow["y" + (i + 1)] = dbtemp * (1 + Math.Round((secvalue - firstvalue) / firstvalue, 2));
                                            TempData[m_Index, (i - syear)] = 0;
                                            TempData[m_Index, (i - syear)] = (dbtemp * (1 + Math.Round((secvalue - firstvalue) / firstvalue, 2))) * (m_nWeightedValue / 100);
                                        }
                                        else
                                        {
                                            //dataRow["y" + (i + 1)] = 0;
                                            TempData[m_Index, (i - syear)] = 0;
                                        }
                                    }
                                }
                            }
                            break;

                        //case "相关法":
                        //    list.Clear();
                        //    psmtemp = new Ps_Forecast_Math();
                        //    psmtemp.ForecastID = forecastReport.ID;
                        //    psmtemp.Forecast = 3;
                        //    break;

                        //case "人工神经网络法":
                        //    list.Clear();
                        //    psmtemp = new Ps_Forecast_Math();
                        //    psmtemp.ForecastID = forecastReport.ID;
                        //    psmtemp.Forecast = 8;
                        //    break;

                    }
                    ++m_Index;
                }

                double m_CalcWeight = 0.0;
                for (int i = 1; i <= eyear - syear + 1; ++i)
                {
                    m_CalcWeight = 0.0;
                    for (int j = 0; j < ha.Count; ++j)
                    {
                        if(TempData != null)
                        {
                            m_CalcWeight += TempData[j, (i - 1)];
                        }
                    }
                    dataRow["y" + (syear + i-1)] = m_CalcWeight;
                    commonhelp.ResetValue(dataRow["ID"].ToString(), "y" + (syear + i - 1));
                }
                //   }
                if (isfalse)
                {
                    break;
                }

            }


            ForecastClass fc = new ForecastClass();
            fc.MaxForecast(forecastReport, dataTable);

            RefreshChart();
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
                if (value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }

            parentNode.SetValue(column.FieldName, sum);
            
            CalculateSum2(parentNode, column);
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            TreeListNode row = this.treeList1.FocusedNode;
            if (row == null)
                return;
            //Ps_Forecast_Math pf = new Ps_Forecast_Math();
            //ForecastClass1.TreeNodeToDataObject<Ps_Forecast_Math>(pf, row);
            //Services.BaseService.Update<Ps_Forecast_Math>(pf);
            CalculateSum2(row);
            if (e.Column.FieldName.Contains("y"))
            {
                commonhelp.SetValue(e.Node["ID"].ToString(), e.Column.FieldName, 1);
            }
            //aaa(row);
            RefreshChart();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormForecastMaxOrBad_SH frm = new FormForecastMaxOrBad_SH(forecastReport);
            frm.Type = type;
            frm.ShowDialog();
            LoadData(); 
        }
    }
}