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

using Itop.Client.Using;
namespace Itop.Client.Forecast
{
    public partial class FormForecastMaxOrBad_new : DevExpress.XtraEditors.XtraForm
    {
        int type = 1;
        DataTable dataTable2 = new DataTable();
        DataTable dataTable1 = new DataTable();
        DataTable dataTable3 = new DataTable();
        private Ps_forecast_list forecastReport = null;
        bool bLoadingData = false;
        bool _canEdit = true;
      
        bool selectdral = true;

        public int Type
        {
            get { return type; }
            set { type = value; }
        }
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
        public FormForecastMaxOrBad_new(Ps_forecast_list fr)
        {
        
            forecastReport = fr;
            InitializeComponent();
        }

        private void HideToolBarButton()
        {
            if (!CanEdit)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
          

        }

        private void FormForecast7_Load(object sender, EventArgs e)
        {
            splitContainerControl1.SplitterPosition = splitContainerControl1.Height / (2);
            splitContainerControl2.SplitterPosition = splitContainerControl2.Height / (2);
            splitContainerControl3.SplitterPosition = splitContainerControl3.Height / (2);
            HideToolBarButton();
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            //gridView2.BeginUpdate();
            LoadData();
            //gridView2.EndUpdate();
            RefreshChart();
            this.Cursor = Cursors.Default;

            string s = "";
            if (type == 1)
            {
                s = "坏数据点";
                //this.gridView1.GroupPanelText = "";
            }
            else
            {
                s = "大用户";
            }
            this.Text = s;
            //this.gridView1.GroupPanelText = s;



            foreach (DataColumn dc in dataTable2.Columns)
            {
                if (!dataTable3.Columns.Contains(dc.ColumnName))
                    dataTable3.Columns.Add(dc.ColumnName, dc.DataType);
            }

            InitData3();
            
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
            if (dataTable2 != null)
            {
                treeList3.Columns.Clear();
            }
            if (dataTable1 != null)
            {
                dataTable1.Columns.Clear();
            }
            AddFixColumn();
            AddFixColumn2();
            AddFixColumn3();
            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
            {
                AddColumn(i);
            }
            for (int i = forecastReport.StartYear; i <=forecastReport.EndYear; i++)
            {
                AddColumn2(i);
            }
            for (int i = forecastReport.StartYear; i <= forecastReport.EndYear; i++)
            {
                AddColumn3(i);
            }

            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = 0;
            IList<Ps_Forecast_Math> listTypes = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            dataTable2 = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_Forecast_Math));
            treeList2.DataSource = dataTable2;

            Hashtable ht = new Hashtable();
            IList<Ps_BadData> list = new List<Ps_BadData>();
            foreach (Ps_Forecast_Math pfm in listTypes)
            {
                Ps_BadData pb1 = new Ps_BadData();
                pb1.ForecastID = pfm.ForecastID;
                pb1.Forecast = type;
                pb1.Col1 = pfm.ID;
                Ps_BadData pb = new Ps_BadData();
                IList<Ps_BadData> li = Common.Services.BaseService.GetList<Ps_BadData>("SelectPs_BadDataByCol1", pb1);
                if (li.Count == 0)
                {
                    pb = new Ps_BadData();
                    pb.ID = pfm.ID+"|m|"+type;
                    pb.Title = pfm.Title;
                    pb.ForecastID = pfm.ForecastID;
                    pb.Forecast = type;
                    pb.Col1 = pfm.ID;
                    pb.ParentID = pfm.ParentID + "|m|" + type;
                    Services.BaseService.Create<Ps_BadData>(pb);
                }
                else
                    pb = li[0];
                list.Add(pb);
            }
            dataTable1 = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(Ps_BadData));

            treeList2.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.DataSource = dataTable1;
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
            column.OptionsColumn.AllowEdit = false;
            this.treeList2.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "Sort";
            column.VisibleIndex = -1; column.OptionsColumn.AllowEdit = false;
            this.treeList2.Columns.AddRange(new TreeListColumn[] {
            column});


            column = new TreeListColumn();
            column.FieldName = "ForecastID";
            column.VisibleIndex = -1; column.OptionsColumn.AllowEdit = false;
            this.treeList2.Columns.AddRange(new TreeListColumn[] {
            column});


            column = new TreeListColumn();
            column.FieldName = "Forecast";
            column.VisibleIndex = -1; column.OptionsColumn.AllowEdit = false;
            this.treeList2.Columns.AddRange(new TreeListColumn[] {
            column});


            column = new TreeListColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1; column.OptionsColumn.AllowEdit = false;
            this.treeList2.Columns.AddRange(new TreeListColumn[] {
            column});


            column = new TreeListColumn();
            column.FieldName = "ParentID";
            column.VisibleIndex = -1; column.OptionsColumn.AllowEdit = false;

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

        //添加固定列

        private void AddFixColumn3()
        {
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "分类名";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            this.treeList3.Columns.AddRange(new TreeListColumn[] {
            column});

            column = new TreeListColumn();
            column.FieldName = "Sort";
            column.VisibleIndex = -1; column.OptionsColumn.AllowEdit = false;
            this.treeList3.Columns.AddRange(new TreeListColumn[] {
            column});


            column = new TreeListColumn();
            column.FieldName = "ForecastID";
            column.VisibleIndex = -1; column.OptionsColumn.AllowEdit = false;
            this.treeList3.Columns.AddRange(new TreeListColumn[] {
            column});


            column = new TreeListColumn();
            column.FieldName = "Forecast";
            column.VisibleIndex = -1; column.OptionsColumn.AllowEdit = false;
            this.treeList3.Columns.AddRange(new TreeListColumn[] {
            column});


            column = new TreeListColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1; column.OptionsColumn.AllowEdit = false;
            this.treeList3.Columns.AddRange(new TreeListColumn[] {
            column});


            column = new TreeListColumn();
            column.FieldName = "ParentID";
            column.VisibleIndex = -1; column.OptionsColumn.AllowEdit = false;

            this.treeList3.Columns.AddRange(new TreeListColumn[] {
            column});

        }

        //添加年份后，新增一列

        private void AddColumn(int year)
        {
            TreeListColumn column = new TreeListColumn();
            column.OptionsColumn.AllowEdit = false;
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


            this.treeList2.Columns.AddRange(new TreeListColumn[] {
            column});


        }
        //添加年份后，新增一列

        private void AddColumn2(int year)
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
            //column.DisplayFormat.FormatString = "#####################0.##%";
            //column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;


            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});


        }
        //添加年份后，新增一列

        private void AddColumn3(int year)
        {
            TreeListColumn column = new TreeListColumn();
            column.OptionsColumn.AllowEdit = false;
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


            this.treeList3.Columns.AddRange(new TreeListColumn[] {
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

            IList<FORBaseColor> list = Services.BaseService.GetList<FORBaseColor>("SelectFORBaseColorByWhere", "Remark='" + this.forecastReport.ID.ToString() + "-1" + type + "'");

            IList<FORBaseColor> li = new List<FORBaseColor>();
            bool bl = false;
            foreach (DataRow row in dataTable2.Rows)
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
                    bc1.Remark = this.forecastReport.ID.ToString() + "-1" + type;
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

            for (int i = 0; i < treeList2.Nodes.Count; i++)
            {
                TreeListNode row = treeList2.Nodes[i];

                
                TreeListNode row1 = treeList1.Nodes[i];
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
                            v.y1990 = (double)row[col.FieldName]+(double)row1[col.FieldName];


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

            }

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

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            Save();
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

          
        }

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
            if (MessageBox.Show("保存后修改数据将替换原始数据？","提示",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                //清楚坏数据点的修正值

                foreach (DataRow dataRow in dataTable1.Rows)
                {

                    TreeListNode row = treeList1.FindNodeByKeyID(dataRow["ID"]);
                    Ps_BadData v = Services.BaseService.GetOneByKey<Ps_BadData>(row["ID"].ToString());
                    foreach (TreeListColumn col in this.treeList1.Columns)
                    {
                        if (col.FieldName.IndexOf("y") > -1)
                        {
                            object obj = row[col.FieldName];
                            if (obj != DBNull.Value)
                            {
                                v.GetType().GetProperty(col.FieldName).SetValue(v, 0, null);
                            }
                        }
                    }

                    try
                    {
                        Services.BaseService.Update<Ps_BadData>(v);

                    }
                    catch { }
                }
                //保存修改后的数据
                foreach (DataRow dataRow in dataTable3.Rows)
                {

                    TreeListNode row = treeList3.FindNodeByKeyID(dataRow["ID"]);
                    Ps_Forecast_Math v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(row["ID"].ToString());
                    foreach (TreeListColumn col in this.treeList3.Columns)
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
                        Services.BaseService.Update<Ps_Forecast_Math>(v);

                    }
                    catch { }
                }
                this.Close();
                MessageBox.Show("更新完成!");
            }
            
        }


        private void Save()
        {

            //保存

            //foreach (DataRow dataRow in dataTable3.Rows)
            //{

            //    TreeListNode row = treeList3.FindNodeByKeyID(dataRow["ID"]);
            //    Ps_BadData v = Services.BaseService.GetOneByKey<Ps_BadData>(row["ID"].ToString());
            //    foreach (TreeListColumn col in this.treeList3.Columns)
            //    {
            //        if (col.FieldName.IndexOf("y") > -1)
            //        {
            //            object obj = row[col.FieldName];
            //            if (obj != DBNull.Value)
            //            {
            //                v.GetType().GetProperty(col.FieldName).SetValue(v, obj, null);
            //            }
            //        }
            //    }

            //    try
            //    {
            //        Services.BaseService.Update<Ps_BadData>(v);

            //    }
            //    catch { }
            //}
            foreach (DataRow dataRow in dataTable1.Rows)
            {
                TreeListNode row = treeList1.FindNodeByKeyID(dataRow["ID"]);
                Ps_BadData v = Services.BaseService.GetOneByKey<Ps_BadData>(row["ID"].ToString());
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
                    Services.BaseService.Update<Ps_BadData>(v);

                }
                catch { }
            }
            //MsgBox.Show("保存成功！");
        }
        private void gridView2_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormResult fr = new FormResult();
            fr.LI = this.treeList1;
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

        private void treeList1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            Save();
            RefreshChart();
            InitData3();
        }

        private void treeList1_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            
        }

        private void treeList2_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            
        }

        private void treeList2_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            RefreshChart();
            InitData3();
        }

        private void InitData3()
        {
            dataTable3.Rows.Clear();

            for(int i=0;i< dataTable2.Rows.Count;i++)
            {
                DataRow row1 = dataTable3.NewRow();
                row1["ID"] = dataTable2.Rows[i]["ID"].ToString();
                row1["Title"]=dataTable2.Rows[i]["Title"].ToString();
                row1["ParentID"] = dataTable2.Rows[i]["ParentID"].ToString();

                foreach(DataColumn col in dataTable2.Columns)
                {
                    if (col.ColumnName.IndexOf("y") > -1)
                    {
                        double s1 = 0;
                        double s2 = 0;
                        object obj1 = dataTable1.Rows[i][col.ColumnName];
                        if (obj1 != DBNull.Value)
                            s1 = (double)obj1;
                        object obj2 = dataTable2.Rows[i][col.ColumnName];
                        if (obj2 != DBNull.Value)
                            s2 = (double)obj2;
                        row1[col.ColumnName] = s1 + s2;
                    }
                }
                dataTable3.Rows.Add(row1);
            
            }
            this.treeList3.DataSource = dataTable3;
        }
        /// <summary>
        /// 手动剔除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tln = treeList3.FocusedNode;
            if (tln == null)
                return;

            TreeListColumn tlc = treeList3.FocusedColumn;
            if (tlc == null)
                return;

            if (tlc.FieldName.IndexOf("y") < 0)
                return;

            int year=int.Parse(tlc.FieldName.Replace("y",""));

            double s1=0;
            double s2=0;

            if (year == forecastReport.StartYear)
            {
                try { s1 = (double)tln["y" + (year + 1)]; }
                catch { }
                try { s2 = (double)tln["y" + (year + 2)]; }
                catch { }

                tln.SetValue(tlc.FieldName, 2 * s1 - s2);
            }
            else
            {
                try { s1 = (double)tln["y" + (year - 1)]; }
                catch { }
                try { s2 = (double)tln["y" + (year + 1)]; }
                catch { }

                tln.SetValue(tlc.FieldName, (s1 + s2)/2);
            
            
            }
            Save();

        }

    }
}