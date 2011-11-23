using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Forecast;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Projects;
using Dundas.Charting.WinControl;
using Itop.Client.Base;
using Itop.Client.Using;
namespace Itop.Client.Forecast
{
    /// <summary>
    /// 结果比较
    /// </summary>
    public partial class FormForecastJG : FormBase
    {
        DataTable dataTable = new DataTable();
        bool bLoadingData = false;
        private ArrayList a1 = new ArrayList();
        private ArrayList a2 = new ArrayList();
        private int typeFlag = 99;
        IList<Ps_Forecast_Math> li = new List<Ps_Forecast_Math>();
        bool _canEdit = true;
        private bool EditRight = false;
        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; EditRight = value; }
        }
        public IList<Ps_Forecast_Math> LI
        {
            set { li = value; }
        }

        public ArrayList A1
        {
            get { return a1; }
            set { a1 = value; }
        }
        public ArrayList A2
        {
            get { return a2; }
            set { a2 = value; }
        }
        Ps_forecast_list forecastReport = new Ps_forecast_list();
        public Ps_forecast_list PFL
        {
            set{forecastReport= value;}
        
        }


        public FormForecastJG()
        {
            InitializeComponent();
        }

        private void FormForecastJG_Load(object sender, EventArgs e)
        {
            InitForm();
            LoadData();
            RefreshChart();
        }

        private void InitForm()
        {
            barButtonItem1.Glyph = Itop.ICON.Resource.审核;
            barButtonItem1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            barButtonItem2.Glyph = Itop.ICON.Resource.关闭;
            barButtonItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            if (!EditRight)
            {
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
               
            }
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
            dataTable = Itop.Common.DataConverter.ToDataTable((IList)li, typeof(Ps_Forecast_Math));
            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
            {
                if(a1.Contains(i))
                AddColumn(i);
                if (a2.Contains(i))
                AddColumn1(i);
            }
            //((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();

            

            double d = 0;
            int yeartemp = 0;
        
            foreach (DataRow drw1 in dataTable.Rows)
            {
                     try
                {
                    d = (double)drw1["y" + forecastReport.StartYear];
                    yeartemp = forecastReport.StartYear;
                }
                catch { }
                foreach (DataColumn dc in dataTable.Columns)
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

                        double sss = Math.Round(Math.Pow(d1 / d, 1.0 / (y1 - yeartemp))*100 - 100, 2);
                        try
                        {
                            yeartemp = y1;
                            d = (double)drw1["y" + yeartemp];
                        }
                        catch { }
                        if (sss.ToString() == "非数字")
                            sss = 0;
                        drw1["m" + s] = sss+"%";
                       
                    }
                }
            }







            this.treeList1.DataSource = dataTable;


            //treeList1.Columns["Title"].Caption = "分类名";
            //treeList1.Columns["Title"].Width = 180;
            //treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            //treeList1.Columns["Title"].OptionsColumn.AllowSort = false;

            Application.DoEvents();

            //  LoadValues();

            //       LoadHistoryValue();
            //treeList1.ExpandAll();
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



        //添加年份后，新增一列
        private void AddColumn1(int year)
        {
            // treeList1.Columns.Add(year + "年", typeof(double));
            //    TreeListColumn column = treeList1.Columns.Add();
            //DevExpress.XtraTreeList.Columns.TreeListColumn column = new DevExpress.XtraTreeList.Columns.TreeListColumn();

            dataTable.Columns.Add("m" + year, typeof(string));

            TreeListColumn column = new TreeListColumn();

            column.FieldName = "m" + year;
            column.Tag = year;
            column.Caption = "年均增长率";
            column.Name = year.ToString();
            column.Width = 100;
            column.VisibleIndex = year;//有两列隐藏列
            //column.ColumnEdit = repositoryItemTextEdit1;

            // 
            // repositoryItemTextEdit1
            //
            //RepositoryItemTextEdit repositoryItemTextEdit1 = new RepositoryItemTextEdit();
            //repositoryItemTextEdit1.AutoHeight = false;
            //repositoryItemTextEdit1.DisplayFormat.FormatString = "P2";
            //repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //repositoryItemTextEdit1.Mask.EditMask = "P2";
            //repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            //column.ColumnEdit = repositoryItemTextEdit1;
            //column.DisplayFormat.FormatString = "P2";
            //column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;

            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});


        }

        private DataTable ConvertXtrTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList)
        {
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();
            //listColID.Add("Flag");
            // dt.Columns.Add("Flag", typeof(int));

            // listColID.Add("ParentID");
            // dt.Columns.Add("ParentID", typeof(int));

            listColID.Add("Title");
            dt.Columns.Add("分类名", typeof(string));
            dt.Columns["分类名"].Caption = "分类名";
            int i = forecastReport.StartYear;
            foreach (TreeListColumn column in xTreeList.Columns)
            {
                if (column.FieldName.IndexOf("y") >= 0)
                {
                    listColID.Add(column.FieldName.Replace("y", "") + "年");
                    dt.Columns.Add(column.FieldName.Replace("y", "") + "年", typeof(string));
                }
                else if ( column.FieldName.IndexOf("m") >= 0)
                {
                    listColID.Add(column.FieldName.Replace("m", "")+"年增长率");
                    dt.Columns.Add(i+"年至" + column.FieldName.Replace("m", "") + "年增长率", typeof(string));
                    i = Convert.ToInt32(column.FieldName.Replace("m","")) + 1;
                   
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
           
            //foreach (string colID in listColID)
            foreach (DataColumn dc in dt.Columns)
            {
                string colID = dc.ColumnName;
                //分类名，第二层及以后在前面加空格
                if (colID == "分类名" && node.ParentNode != null)
                {
                    newRow["分类名"] = node["Title"];
                }
                else
                {
                    if (colID == "分类名")
                        newRow["分类名"] = node["Title"];
                    else if (colID.Contains("年增长率"))
                    {

                        newRow[colID] = node["m" + colID.Substring(6, 4)];

                    }
                    else
                    {
                        newRow[colID] = node["y" + colID.Replace("年", "")];
                    
                    }

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
                    newRow["分类名"] = "　　" + node[colID];
                }
                else
                {
                    newRow[colID] = node["y" + colID.Replace("年", "")];
                }
            }

            dt.Rows.Add(newRow);

            foreach (TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID);
            }
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormTypeTitle ftt = new FormTypeTitle();
            //ftt.Text = "报表名称";
            //ftt.TypeTitle = "报表名称";
            //if (ftt.ShowDialog() == DialogResult.OK)
            //{
            //    //FileClass.ExportToExcelOld(ftt.TypeTitle, "", this.gridControl1);



            
            //}
            DataTable dt = ConvertXtrTreeListToDataTable(treeList1);
            FileClass.ExportExcel(dt, forecastReport.Title, false, false);
            //FileClass.ExportExcel(dataTable, forecastReport.Title, false, false);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
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

            //ArrayList ht = new ArrayList();
            //ht.Add(Color.Red);
            //ht.Add(Color.Blue);
            //ht.Add(Color.Green);
            //ht.Add(Color.Yellow);
            //ht.Add(Color.HotPink);
            //ht.Add(Color.LawnGreen);
            //ht.Add(Color.Khaki);
            //ht.Add(Color.LightSlateGray);
            //ht.Add(Color.LightSeaGreen);
            //ht.Add(Color.Lime);

            IList<FORBaseColor> list = Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByWhere", "Remark='" + this.forecastReport.ID.ToString() + "-" + typeFlag + "'");

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
                    bc1.Remark = this.forecastReport.ID.ToString() + "-" + typeFlag;
                    bc1.Title = row["Title"].ToString();
                    bc1.Color = 16711680;
                    bc1.Color1 = Color.Blue;
                    Services.BaseService.Create<FORBaseColor>(bc1);
                    li.Add(bc1);
                }

            }
            ArrayList ht = new ArrayList();
            foreach (FORBaseColor bc2 in li)
            {
                ht.Add(bc2.Color1);
            }


            List<Ps_Forecast_Math> listValues = new List<Ps_Forecast_Math>();

            for (int i = 0; i < this.dataTable.Rows.Count; i++)
            {
                DataRow row = this.dataTable.Rows[i];
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
                            v.Title = row["Title"].ToString();
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
            LegendItem legendItem;
            LegendCell legendCell1;
            LegendCell legendCell2;
            LegendCell legendCell3;
            Legend legend = new Legend();
            legend.AutoFitText = false;
            //legend.BackColor = System.Drawing.Color.Transparent;
            //legend.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)), ((System.Byte)(64)));
            for (int i = 0; i < chart1.Series.Count; i++)
            {
                int j = i % 10;
                legendItem = new Dundas.Charting.WinControl.LegendItem();
                legendCell1 = new Dundas.Charting.WinControl.LegendCell();
                legendCell2 = new Dundas.Charting.WinControl.LegendCell();
                legendCell3 = new Dundas.Charting.WinControl.LegendCell();
                legendCell1.CellType = Dundas.Charting.WinControl.LegendCellType.Image;
                legendCell2.CellType = Dundas.Charting.WinControl.LegendCellType.Image;
                legendCell1.Name = "Cell1";
                legendCell2.Name = "Cell2";
                legendCell3.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
                legendCell3.Name = "Cell3";
                legendCell3.Text = chart1.Series[i].Name;
                legendCell3.TextColor = (Color)ht[i];
                chart1.Series[i].Color = (Color)ht[i];
                chart1.Series[i].Name = (i + 1).ToString() + "." + chart1.Series[i].Name;
                chart1.Series[i].Type = Dundas.Charting.WinControl.SeriesChartType.Line;
                chart1.Series[i].MarkerImage = al[i % 7].ToString();
                chart1.Series[i].MarkerSize = 7;
                chart1.Series[i].MarkerStyle = (Dundas.Charting.WinControl.MarkerStyle)(2);
                chart1.Series[i].ShowInLegend = false;
                chart1.Series[i].Enabled = true;
                chart1.Series[i].XValueIndexed = true;
                legendItem.Cells.Add(legendCell1);
                legendItem.Cells.Add(legendCell2);
                legendItem.Cells.Add(legendCell3);
                legendItem.Tag = chart1.Series[i];
                legendItem.Cells[1].Image = al[i % 7].ToString();
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
            chart1.ChartAreas["Default"].AxisY.MinorGrid.Enabled = true;
            chart1.ChartAreas["Default"].AxisY.MinorGrid.LineStyle = ChartDashStyle.Dash;
            chart1.ChartAreas["Default"].AxisY.MinorGrid.LineColor = Color.Gray;
         
            legend.Name = "Default";

            this.chart1.Legends.Add(legend);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormColor fc = new FormColor();
            fc.DT = dataTable;
            fc.ID = forecastReport.ID.ToString();
            fc.For = typeFlag;
            if (fc.ShowDialog() == DialogResult.OK)
                RefreshChart();
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!CanEdit || treeList1.FocusedColumn.FieldName=="Title")
            {
                e.Cancel = true;
            }
        }

        private void chart1_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult result = chart1.HitTest(e.X, e.Y);
            if (result != null && result.Object != null)
            {
                // When user hits the LegendItem
                if (result.Object is LegendItem)
                {
                    // Legend item result
                    LegendItem legendItem = (LegendItem)result.Object;

                    // series item selected
                    Series selectedSeries = (Series)legendItem.Tag;

                    if (selectedSeries != null)
                    {



                        if (selectedSeries.Enabled)
                        {
                            selectedSeries.Enabled = false;
                            legendItem.Cells[0].Image = string.Format(Application.StartupPath + @"/img/chk_unchecked.png");
                            //  legendItem.Cells[0].ImageTranspColor = Color.Red;
                        }

                        else
                        {
                            selectedSeries.Enabled = true;
                            legendItem.Cells[0].Image = string.Format(Application.StartupPath + @"/img/chk_checked.png");
                            // legendItem.Cells[0].ImageTranspColor = Color.Red;
                        }
                        chart1.ResetAutoValues();
                    }
                }
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            RefreshChart();
        }

     
    }
}