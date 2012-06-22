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
using Itop.Domain.HistoryValue;
using Itop.Client.Common;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using Dundas.Charting.WinControl;

using Itop.Client.Using;
using Itop.Client.Base;
namespace Itop.Client.Forecast.FormAlgorithm_New
{
    public partial class FormExpertSH : FormBase
    {
        int type = 7;//专家指定法
        DataTable dataTable2 = new DataTable();
        DataTable dataTable1 = new DataTable();
       
        private Ps_forecast_list forecastReport = null;
        private PublicFunction m_pf = new PublicFunction();
        bool bLoadingData = false;
        bool _canEdit = true;
        bool ExpandOrCollpase = false;//节点展开还是收缩
        int intFocuses = 0;//焦点是否落在treeList1上intFouces=1还是treeList2上intFouces=2
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
        public FormExpertSH(Ps_forecast_list fr)
        {
        
            forecastReport = fr;
            InitializeComponent();
            chart_user1.SetColor += new chart_userSH.setcolor(chart_user1_SetColor);
        }

        void chart_user1_SetColor()
        {
            FormColor fc = new FormColor();
            fc.DT = dataTable2;
            fc.For = 7;
            fc.ID = forecastReport.ID.ToString();
            if (fc.ShowDialog() == DialogResult.OK)
                RefreshChart();
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
            //splitContainerControl1.SplitterPosition = splitContainerControl1.Height / 2;
            //splitContainerControl2.SplitterPosition = splitContainerControl2.Height  / 2;

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
        Hashtable ht = new Hashtable();
        private void checkfixedvalue()
        {
            ht.Clear();
            ht.Add("全社会用电量（亿kWh）", 1);


            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            dataTable2 = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Forecast_Math));

            commonhelp.CheckHasFixValue(ht, dataTable2, forecastReport.ID, type);

        }
        Hashtable OldHt = new Hashtable();
        private void LoadData()
        {
            checkfixedvalue();
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
            for (int i = forecastReport.StartYear; i <= forecastReport.YcEndYear; i++)
            {
                AddColumn(i);
            }
            for (int i = forecastReport.StartYear; i <forecastReport.YcEndYear; i++)
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
            OldHt.Clear();
            foreach (DataRow row in dataTable2.Rows)
            {
                if (!OldHt.ContainsKey(row["Title"].ToString()))
                {
                    OldHt.Add(row["Title"].ToString(), row["ID"].ToString());
                }
            }


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
                    if (m == 0)
                    {
                        Random rd = new Random();
                        m = rd.Next(100);
                    }
                    Color cl = (Color)ht[m % 13];
                    bc1.Color = ColorTranslator.ToOle(cl);
                    bc1.Color1 = cl;
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

       private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
        

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          //  this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// 读取原始数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormForecastLoadData2 ffs = new FormForecastLoadData2();
            ffs.PID = MIS.ProgUID;
            ffs.StartYear = forecastReport.StartYear;
            ffs.EndYear = forecastReport.EndYear;
            if (ffs.ShowDialog() != DialogResult.OK)
                return;


            Hashtable hs = ffs.HS;

            if (hs.Count == 0)
                return;
            string id = Guid.NewGuid().ToString();
            if (ffs.Selectid != "4")
            {
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

            }
            else if (ffs.Selectid == "4")
            {
                foreach (PSP_Types de3 in hs.Values)
                {
                    Ps_Forecast_Math py = new Ps_Forecast_Math();

                    //py = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", "Title='" + de3.Title + "'" + " and Forecast='0' and ForecastID='" + forecastReport.ID + "' and Col1='"+de3.ID+"'");
                    py = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", "  Forecast = '" + type + "' and ForecastID='" + forecastReport.ID + "' and Col1='" + de3.ID + "'");
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
                        ForecastMath.Forecast = type;
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
            LoadData();

            this.chart_user1.All_Select(true);
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
            this.chart_user1.chart1.SaveAsImage(sf.FileName, ci);
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

       
        /// <summary>
        /// 添加分类名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加分类";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();

                psp_Type.ID = Guid.NewGuid().ToString();

                psp_Type.Forecast = type;
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
                    dataTable1.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable1.NewRow()));
                    dataTable2.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable2.NewRow()));

                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加分类出错：" + ex.Message);
                }
                RefreshChart();
            }
        }
        /// <summary>
        /// 添加子分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode row = null;
            if (intFocuses == 1) 
            {
                row = this.treeList1.FocusedNode;
                intFocuses = 0;
            }
            else if (intFocuses==2)
            {
                row = this.treeList2.FocusedNode;
                intFocuses = 0;
            }
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

                psp_Type.Forecast = type;
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
                    dataTable1.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable1.NewRow()));
                    dataTable2.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable2.NewRow()));

                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加分类出错：" + ex.Message);
                }
                RefreshChart();
            }
        }
        /// <summary>
        /// 修改分类名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode row = null;
            if(intFocuses==1)
            {
                row = this.treeList1.FocusedNode;
                intFocuses = 0;
            }
            else if(intFocuses==2)
            {
                row = this.treeList2.FocusedNode;
                intFocuses = 0;
            }
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
                    row.SetValue("Title", frm.TypeTitle);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("修改出错：" + ex.Message);
                }
                LoadData();
                RefreshChart();
                
            }
        }
        /// <summary>
        /// 删除分类名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode row = null;
            if (intFocuses==1)
            {
                row = this.treeList1.FocusedNode;
                intFocuses = 0;
            }
            else if(intFocuses ==2)
            {
                row = this.treeList2.FocusedNode;
                intFocuses = 0;
            }
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

                    bc1.Remark = forecastReport.ID + "-" + type;
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
                LoadData();
                RefreshChart();
            }
        }
        /// <summary>
        /// treeList1和treeList2同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if(e.Node!=null)
            {
                treeList2.Selection.Clear();
                this.treeList2.SetFocusedNode(this.treeList2.FindNodeByKeyID(e.Node["ID"]));
                intFocuses = 1;
            }
        }
        /// <summary>
        /// treeList1和treeList2同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList2_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if(e.Node!=null)
            {
                treeList1.Selection.Clear();
                this.treeList1.SetFocusedNode(this.treeList1.FindNodeByKeyID(e.Node["ID"]));
                intFocuses = 2;
            }
        }

        private void treeList1_AfterExpand(object sender, NodeEventArgs e)
        {
            if(ExpandOrCollpase)
            {
                return;
            }
            ExpandOrCollpase = true;
            TreeListNode node = this.treeList2.FindNodeByKeyID(e.Node["ID"]);
            if(node!=null)
            {
                node.Expanded = e.Node.Expanded;
            }
            ExpandOrCollpase = false;
        }

        private void treeList1_AfterCollapse(object sender, NodeEventArgs e)
        {
            if(ExpandOrCollpase)
            {
                return;
            }
            ExpandOrCollpase = true;
            TreeListNode node = this.treeList2.FindNodeByKeyID(e.Node["ID"]);
            if(node!=null)
            {
                node.Expanded = e.Node.Expanded;
            }
            ExpandOrCollpase = false;
        }

        private void treeList2_AfterCollapse(object sender, NodeEventArgs e)
        {
            if(ExpandOrCollpase)
            {
                return;
            }
            ExpandOrCollpase = true;
            TreeListNode node = this.treeList1.FindNodeByKeyID(e.Node["ID"]);
            if(node!=null)
            {
                node.Expanded = e.Node.Expanded;
            }
            ExpandOrCollpase = false;
        }

        private void treeList2_AfterExpand(object sender, NodeEventArgs e)
        {
            if(ExpandOrCollpase)
            {
                return;
            }
            ExpandOrCollpase = true;
            TreeListNode node = treeList1.FindNodeByKeyID(e.Node["ID"]);
            if(node!=null)
            {
                node.Expanded = e.Node.Expanded;
            }
            ExpandOrCollpase = false;
        }

        private void treeList2_Enter(object sender, EventArgs e)
        {
            intFocuses = 2;
        }

        private void treeList1_Enter(object sender, EventArgs e)
        {
            intFocuses = 1;
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Hashtable temphs = new Hashtable();
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.ForecastID = forecastReport.ID;
            psp_Type.Forecast = type;
            Common.Services.BaseService.Update("DeletePs_Forecast_MathForecastIDAndForecast", psp_Type);
            psp_Type.Forecast = 0;
            IList listTypes = Common.Services.BaseService.GetList("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
            //foreach (Ps_Forecast_Math psp_Typetemp in listTypes)
            //{
            //    temphs.Add(psp_Typetemp.ID, Guid.NewGuid().ToString());
            //}
            foreach (Ps_Forecast_Math psp_Typetemp in listTypes)
            {
                string id = psp_Typetemp.ID;
                psp_Type = new Ps_Forecast_Math();
                psp_Type = psp_Typetemp;
                psp_Type.ID = Guid.NewGuid().ToString();
                psp_Type.Col1 = id;
                psp_Type.Forecast = type;
                if (psp_Type.ParentID != "")
                {
                    if (temphs.ContainsKey(psp_Type.ParentID))
                    {
                        psp_Type.ParentID = temphs[psp_Type.ParentID].ToString();
                    }
                }

                Common.Services.BaseService.Create<Ps_Forecast_Math>(psp_Type);
            }
            LoadData();
            this.chart_user1.All_Select(true);
            RefreshChart();
        }

       



 


    }
}