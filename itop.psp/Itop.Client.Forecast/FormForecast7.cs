using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;

using System.Collections;
using Itop.Common;
using Itop.Domain.Forecast;
using Itop.Client.Common;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using Dundas.Charting.WinControl;

using Itop.Client.Using;
namespace Itop.Client.Forecast
{
    public partial class FormForecast7 : DevExpress.XtraEditors.XtraForm
    {
        int type = 7;//专家指定法
        DataTable dataTable2 = new DataTable();
        DataTable dataTable1 = new DataTable();
        private Ps_forecast_list forecastReport = null;
        private PublicFunction m_pf = new PublicFunction();
        bool bLoadingData = false;
        bool _canEdit = true;
      
        bool selectdral = true;

        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; EditRight = value; }
        }
        public DataTable DataTable2
        {
            get { return dataTable2; }
            set { dataTable2 = value; }
        }
        private bool EditRight = false;
        public FormForecast7( Ps_forecast_list fr)
        {
        
            forecastReport = fr;
            InitializeComponent();
        }

        private void HideToolBarButton()
        {
            if (!CanEdit)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
              
            }
          

        }

        private void FormForecast7_Load(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = splitContainerControl1.Height / 2;
            splitContainerControl2.SplitterPosition = splitContainerControl2.Height  / 2;

            HideToolBarButton();
            //chart1.Series.Clear();
            //Show();
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            //gridView2.BeginUpdate();
            LoadData();
            //gridView2.EndUpdate();
            RefreshChart();
            this.Cursor = Cursors.Default;



            //Ps_Forecast_Setup pfs = new Ps_Forecast_Setup();
            //pfs.Forecast = type;
            //pfs.ForecastID = forecastReport.ID;
            //pfs.StartYear = int.Parse(firstyear.Replace("y", ""));
            //pfs.EndYear = int.Parse(endyear.Replace("y", ""));

            //IList<Ps_Forecast_Setup> li = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);

            //if (li.Count != 0)
            //{
            //    firstyear = li[0].StartYear.ToString();
            //    endyear = li[0].EndYear.ToString();
            //}
        }
        private void LoadData()
        {
            treeList1.DataSource = null;
            bLoadingData = true;
            if (dataTable2 != null)
            {
                dataTable1.Columns.Clear();
                dataTable2.Columns.Clear();
                treeList2.Columns.Clear();
                treeList1.Columns.Clear();
               
            }
            if (dataTable1 != null)
            {
                dataTable1.Columns.Clear();
            }
            AddFixColumn();
            AddFixColumn2();
            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
            {
                AddColumn(i);
            }
            for (int i = forecastReport.StartYear; i <forecastReport.EndYear; i++)
            {
                AddColumn2(i);
            }
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

      
            dataTable2 = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));
            dataTable1 = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));
            treeList2.DataSource = dataTable2;



            treeList2.Columns["Title"].OptionsColumn.AllowEdit = false;
            Calculatordata1();
            treeList1.DataSource = dataTable1;
            Application.DoEvents();
            bLoadingData = false;
        }
        private void Calculatordata1()
          {

           foreach (DataRow row in dataTable1.Rows)
           {
               for (int i = forecastReport.StartYear; i < forecastReport.EndYear; i++)
               {
                   
                   
                       double firstvalue=0;
                     double secvalue=0;
                     if (DBNull.Value != row["y" + i.ToString()] && row["y" + i.ToString()].ToString() != "")
                         firstvalue = Convert.ToDouble(row["y" + i.ToString()]);
                           else
                               firstvalue=0;
                           if (DBNull.Value != row["y" + i.ToString()] && row["y" + ((int)(i + 1)).ToString()].ToString() != "")
                               secvalue = Convert.ToDouble(row["y" + (i + 1)]);
                           else
                               secvalue=0;
                           //if (secvalue != 0 || firstvalue != 0)
                           if (secvalue != 0 && firstvalue != 0)
                               row["y" + i.ToString()] = Math.Round((secvalue - firstvalue)  / firstvalue,2);
                           else
                           {
                              
                               row["y" + i.ToString()] = 0;
                           }
                   
               }
          
           }
          }

        //添加固定列
        private void AddFixColumn()
        {
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "分类名";
            column.VisibleIndex = 0;
            column.Width = 180;
            this.treeList2.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "Sort";
            column.VisibleIndex = -1;
            this.treeList2.Columns.AddRange(new TreeListColumn[] {
            column});


            column = new TreeListColumn();
            column.FieldName = "ForecastID";
            column.VisibleIndex = -1;
            this.treeList2.Columns.AddRange(new TreeListColumn[] {
            column});


            column = new TreeListColumn();
            column.FieldName = "Forecast";
            column.VisibleIndex = -1;
            this.treeList2.Columns.AddRange(new TreeListColumn[] {
            column});


            column = new TreeListColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1;
            this.treeList2.Columns.AddRange(new TreeListColumn[] {
            column});


            column = new TreeListColumn();
            column.FieldName = "ParentID";
            column.VisibleIndex = -1;

            this.treeList2.Columns.AddRange(new TreeListColumn[] {
            column});
           
        }
        private void AddFixColumn2()
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
            column.Caption =year + "年";
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


            this.treeList2.Columns.AddRange(new TreeListColumn[] {
            column});
           

        }
        //添加年份后，新增一列
        private void AddColumn2(int year)
        {
            TreeListColumn column = new TreeListColumn();

            column.FieldName = "y" + year;
            column.Tag = year;
            column.Caption = (year + 1) + "年";
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
           // repositoryItemTextEdit1.Mask.EditMask = "n2";
            repositoryItemTextEdit1.Mask.EditMask = "#####################0.00%";
            repositoryItemTextEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            column.ColumnEdit = repositoryItemTextEdit1;
            //column.DisplayFormat.FormatString = "#####################0.##%";
            //column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;


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
            foreach (DataRow row in dataTable2.Rows)
            {
                aldatablr.Add(row["ID"].ToString());
            }
            foreach (DataRow row in dataTable2.Rows)
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

            //for (int i = 0; i < treeList1.Nodes.Count; i++)
            //{
            //    TreeListNode row = treeList1.Nodes[i];
            //    foreach (TreeListColumn col in treeList1.Columns)
            //    {
            //        if (col.FieldName.IndexOf("y") > -1)
            //        {
            //            object obj = row[col.FieldName];
            //            if (obj != DBNull.Value)
            //            {
            //                Ps_Forecast_Math v = new Ps_Forecast_Math();
            //                v.ForecastID = forecastReport.ID;
            //                v.ID = (string)row["ID"];
            //                v.Title = row["Title"].ToString();
            //                v.Sort = Convert.ToInt32(col.FieldName.Replace("y", ""));
            //                v.y1990 = (double)row[col.FieldName];

            //                listValues.Add(v);
            //            }
            //        }
            //    }


            //}
            for (int i = 0; i < treeList2.Nodes.Count; i++)
            {
                TreeListNode row = treeList2.Nodes[i];
                foreach (TreeListColumn col in treeList2.Columns)
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
            m_pf.SetChart(chart1, checkBox1, hs, null);
        }

      
      

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
        

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          //  this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            //psp_Type.ForecastID = forecastReport.ID;
            //psp_Type.Forecast = type;
            //Common.Services.BaseService.Update("DeletePs_Forecast_MathForecastIDAndForecast", psp_Type);
            //psp_Type.Forecast = 0;
            //IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
            //foreach (Ps_Forecast_Math psp_Typetemp in listTypes)
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

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
           
        }

        //////////private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //////////{
        ////////////  DataRow dr=    gridView1.GetDataRow();
        //////////    if (e.Column.FieldName.IndexOf("y") > -1)
        //////////  {
        //////////             double firstvalue=0;
        //////////             double secvalue=0;
          
        //////////             if (DBNull.Value != dataTable2.Rows[e.RowHandle][e.Column.FieldName] && dataTable2.Rows[e.RowHandle][e.Column.FieldName].ToString() != "")
        //////////                 firstvalue = Convert.ToDouble(dataTable2.Rows[e.RowHandle][e.Column.FieldName]);
        //////////                   else
        //////////                       firstvalue=0;
        //////////                   if (DBNull.Value != e.Value&& e.Value.ToString() != "")
        //////////                       secvalue = Convert.ToDouble(e.Value)*0.01;
        //////////                   else
        //////////                       secvalue=0;
                     
        //////////                       dataTable2.Rows[e.RowHandle]["y" + (int.Parse(e.Column.FieldName.Replace("y", "")) + 1)] = Math.Round(firstvalue * (1 + secvalue), 2);
        //////////      firstvalue= Math.Round(firstvalue * (1 + secvalue), 3);
        //////////        for (int i =int.Parse( e.Column.FieldName.Replace("y",""))+1; i < forecastReport.EndYear; i++)
        //////////        {



        //////////            //if (DBNull.Value != dataTable2.Rows[e.RowHandle]["y" + i.ToString()] && dataTable2.Rows[e.RowHandle]["y" + i.ToString()].ToString() != "")
        //////////            //    firstvalue = Convert.ToDouble(dataTable2.Rows[e.RowHandle]["y" + i.ToString()]);
        //////////            //else
        //////////            //    firstvalue = 0;
        //////////            if (DBNull.Value != dataTable1.Rows[e.RowHandle]["y" + i.ToString()] && dataTable1.Rows[e.RowHandle]["y" + i ].ToString() != "")
        //////////                secvalue = Convert.ToDouble(dataTable1.Rows[e.RowHandle]["y" + i ]);
        //////////            else
        //////////                secvalue = 0;
        //////////            if (secvalue.ToString() == "非数字" || secvalue.ToString() == "正无穷大")
        //////////                continue;
        //////////            dataTable2.Rows[e.RowHandle]["y" + (i + 1)] = Math.Round(firstvalue * (1 + secvalue), 2);
        //////////            firstvalue = Math.Round(firstvalue * (1 + secvalue), 3);

        //////////        }
        //////////        //    gridView2.RefreshData();
        //////////        RefreshChart();


        //////////    }
          
        //////////}

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormColor fc = new FormColor();
            fc.DT = dataTable2;
            fc.For = 7;
            fc.ID = forecastReport.ID.ToString();
            if (fc.ShowDialog() == DialogResult.OK)
                RefreshChart();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Save();
        }

        private void Save()
        {

            //保存

            foreach (DataRow dataRow in dataTable2.Rows)
            {

                TreeListNode row = treeList2.FindNodeByKeyID(dataRow["ID"]);

                //for (int i = 0; i < this.treeList1.Nodes.Count; i++)
                //{
                //    TreeListNode row = this.treeList1.Nodes[i];
                Ps_Forecast_Math v = new Ps_Forecast_Math();
                v.ID = row["ID"].ToString();
                foreach (TreeListColumn col in this.treeList2.Columns)
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

        //////////private void gridView2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        //////////{
        //////////    if (e.Column.FieldName.IndexOf("y") > -1)
        //////////    {
        //////////        double firstvalue = 0;
        //////////        double secvalue = 0;
        //////////        if (e.Value.ToString() != "")
        //////////            firstvalue = Convert.ToDouble(e.Value);
        //////////        else
        //////////            firstvalue = 0;
        //////////        if (int.Parse(e.Column.FieldName.Replace("y", "")) > forecastReport.StartYear)
        //////////        {
        //////////            int i = int.Parse(e.Column.FieldName.Replace("y", ""));

        //////////            if (DBNull.Value != dataTable2.Rows[e.RowHandle]["y" + (i - 1)] && dataTable2.Rows[e.RowHandle]["y" + (i - 1)].ToString() != "")
        //////////                secvalue = Convert.ToDouble(dataTable2.Rows[e.RowHandle]["y" + (i - 1)]);
        //////////            else
        //////////                secvalue = 0;
        //////////            if (secvalue != 0 || firstvalue != 0)
        //////////                dataTable1.Rows[e.RowHandle]["y" + (i - 1)] = Math.Round((firstvalue - secvalue) / secvalue, 2);
        //////////            else
        //////////            {

        //////////                dataTable1.Rows[e.RowHandle]["y" + (i - 1)] = 0;
        //////////            }
                   
        //////////        }
               
        //////////        for (int i =int.Parse( e.Column.FieldName.Replace("y","")); i < forecastReport.EndYear; i++)
        //////////        {



        //////////            //if (DBNull.Value != dataTable2.Rows[e.RowHandle]["y" + i.ToString()] && dataTable2.Rows[e.RowHandle]["y" + i.ToString()].ToString() != "")
        //////////            //    firstvalue = Convert.ToDouble(dataTable2.Rows[e.RowHandle]["y" + i.ToString()]);
        //////////            //else
        //////////            //    firstvalue = 0;
        //////////            if (DBNull.Value != dataTable1.Rows[e.RowHandle]["y" + i.ToString()] && dataTable1.Rows[e.RowHandle]["y" + i ].ToString() != "")
        //////////                secvalue = Convert.ToDouble(dataTable1.Rows[e.RowHandle]["y" + i ]);
        //////////            else
        //////////                secvalue = 0;
        //////////            if (secvalue.ToString() == "非数字" || secvalue.ToString() == "正无穷大")
        //////////               continue;
        //////////            dataTable2.Rows[e.RowHandle]["y" + (i + 1)] = Math.Round(firstvalue * (1 + secvalue), 2);
        //////////            firstvalue = Math.Round(firstvalue * (1 + secvalue), 2);

        //////////        }
        //////////        //    gridView2.RefreshData();
        //////////        RefreshChart();


        //////////    }
        //////////}

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FileClass.ExportToExcelOld(this.forecastReport.Title, "", this.gridControl2);
            FormResult fr = new FormResult();
            fr.LI = this.treeList2;
            fr.Text = forecastReport.Title;
            fr.ShowDialog();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void treeList1_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            
        }

        private void treeList2_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            
        }

        private void treeList2_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            string id = e.Node["ID"].ToString();
            DataRow[] row2 = dataTable2.Select("ID='" + id + "'");
            DataRow[] row1 = dataTable1.Select("ID='" + id + "'");


            if (e.Column.FieldName.IndexOf("y") > -1)
            {
                double firstvalue = 0;
                double secvalue = 0;
                if (e.Value.ToString() != "")
                    firstvalue = Convert.ToDouble(e.Value);
                else
                    firstvalue = 0;
                if (int.Parse(e.Column.FieldName.Replace("y", "")) > forecastReport.StartYear)
                {
                    int i = int.Parse(e.Column.FieldName.Replace("y", ""));

                    if (DBNull.Value != row2[0]["y" + (i - 1)] && row2[0]["y" + (i - 1)].ToString() != "")
                        secvalue = Convert.ToDouble(row2[0]["y" + (i - 1)]);
                    else
                        secvalue = 0;
                    if (secvalue != 0 || firstvalue != 0)
                        row1[0]["y" + (i - 1)] = Math.Round((firstvalue - secvalue) / secvalue, 2);
                    else
                    {

                        row1[0]["y" + (i - 1)] = 0;
                    }

                }

                for (int i = int.Parse(e.Column.FieldName.Replace("y", "")); i < forecastReport.EndYear; i++)
                {

                    if (DBNull.Value != row1[0]["y" + i.ToString()] && row1[0]["y" + i].ToString() != "")
                        secvalue = Convert.ToDouble(row1[0]["y" + i]);
                    else
                        secvalue = 0;
                    if (secvalue.ToString() == "非数字" || secvalue.ToString() == "正无穷大")
                        continue;
                    row2[0]["y" + (i + 1)] = Math.Round(firstvalue * (1 + secvalue), 2);
                    firstvalue = Math.Round(firstvalue * (1 + secvalue), 2);

                }

                RefreshChart();


            }
        }

        private void treeList1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            string id = e.Node["ID"].ToString();
            DataRow[] row2 = dataTable2.Select("ID='" + id + "'");
            DataRow[] row1 = dataTable1.Select("ID='" + id + "'");

            if (e.Column.FieldName.IndexOf("y") > -1)
            {
                double firstvalue = 0;
                double secvalue = 0;



                if (DBNull.Value != row2[0][e.Column.FieldName] && row2[0][e.Column.FieldName].ToString() != "")
                    firstvalue = Convert.ToDouble(row2[0][e.Column.FieldName]);
                else
                    firstvalue = 0;
                if (DBNull.Value != e.Value && e.Value.ToString() != "")
                   // secvalue = Convert.ToDouble(e.Value) * 0.01;
                    secvalue = Convert.ToDouble(e.Value);
                else
                    secvalue = 0;

                row2[0]["y" + (int.Parse(e.Column.FieldName.Replace("y", "")) + 1)] = Math.Round(firstvalue * (1 + secvalue), 2);
                firstvalue = Math.Round(firstvalue * (1 + secvalue), 3);
                for (int i = int.Parse(e.Column.FieldName.Replace("y", "")) + 1; i < forecastReport.EndYear; i++)
                {

                    if (DBNull.Value != row1[0]["y" + i.ToString()] && row1[0]["y" + i].ToString() != "")
                        secvalue = Convert.ToDouble(row1[0]["y" + i]);
                    else
                        secvalue = 0;
                    if (secvalue.ToString() == "非数字" || secvalue.ToString() == "正无穷大")
                        continue;
                    row2[0]["y" + (i + 1)] = Math.Round(firstvalue * (1 + secvalue), 2);
                    firstvalue = Math.Round(firstvalue * (1 + secvalue), 3);

                }
                RefreshChart();


            }
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (!CanEdit)
            {
                e.Cancel = true;
            }
        }

        private void treeList2_ShowingEditor(object sender, CancelEventArgs e)
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