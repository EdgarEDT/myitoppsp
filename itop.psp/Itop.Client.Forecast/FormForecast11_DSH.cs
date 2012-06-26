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
using DevExpress.XtraEditors.Repository;
using Itop.Client.Forecast;
using Itop.Domain.Table;
using DevExpress.Utils;
using Itop.Client;
using Itop.Client.History;
using Itop.Client.Forecast.FormAlgorithm_New;
namespace Itop.Client.Forecast
{
    /// <summary>
    /// 分区预测方案
    /// </summary>
    public partial class FormForecast11_DSH : FormBase
    {
        string type1 = "12";
        int type = 12;
        string type31 = "13";
        int type32 = 13;
        DataTable dataTable = new DataTable();
        DataTable dataTable2 = new DataTable();
        DataTable chartdataTable = new DataTable();
        bool bLoadingData = false;
        bool _canEdit = false;
        int firstyear = 2000;
        int endyear = 2008;
        string jqbyear = "2000";
        string jqeyear = "2010";
        string splitstr = "-";
        string where1 = "";
        string where2 = "";
        private Ps_forecast_list forecastReport = null;
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
        public FormForecast11_DSH(Ps_forecast_list fr)
        {
            InitializeComponent();
            forecastReport = fr;
            treeList1.OptionsView.AutoWidth = false;
          
            barButtonItem6.Glyph = Itop.ICON.Resource.关闭;

            btGetHistory.Glyph = Itop.ICON.Resource.修改;
            barButtonItem10.Glyph = Itop.ICON.Resource.打回重新编;
        }

        private void HideToolBarButton()
        {

            if (!base.AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.EditRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.DeleteRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }


        }

        private void Form8Forecast_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            firstyear = forecastReport.StartYear;
            endyear = forecastReport.YcEndYear;
            Ps_Forecast_Setup pfs = new Ps_Forecast_Setup();
            pfs.Forecast = type;
            pfs.ForecastID = forecastReport.ID;
            //pfs.StartYear = int.Parse(jqbyear.Replace("y", ""));
            //pfs.EndYear = int.Parse(jqeyear.Replace("y", ""));

            IList<Ps_Forecast_Setup> li = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);

            if (li.Count == 0)
            {
                Ps_YearRange py = new Ps_YearRange();
                py.Col4 = "区县发展实绩";
                py.Col5 = Itop.Client.MIS.ProgUID;

                IList<Ps_YearRange> li1 = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);

                if (li1.Count > 0)
                {
                    jqbyear = li1[0].StartYear.ToString();
                    jqeyear = li1[0].FinishYear.ToString();
                }
            }
            //Services.BaseService.Create<Ps_Forecast_Setup>(pfs);
            else
            {
                jqbyear = li[0].StartYear.ToString();
                jqeyear = li[0].EndYear.ToString();
            }

            LoadData();
            SelectDaYongHu();

            //RefreshChart();
            where1 = string.Format(" Forecast ={0} and ForecastID ='{1}' ", type, forecastReport.ID);
            where2 = string.Format(" Forecast ={0} and ForecastID ='{1}' ", type32, forecastReport.ID);
            initpasteCols();



            SetHistoryYear();
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
                jqbyear = li2[0].StartYear.ToString();
                jqeyear = li2[0].EndYear.ToString();

            }
        }
        /// <summary>
        /// 初始化粘贴列
        /// </summary>
        private void initpasteCols()
        {
            pasteCols.Add("Title");
            for (int i = firstyear; i <= endyear; i++)
            {
                pasteCols.Add("y" + i);
            }
        }
        List<string> pasteCols = new List<string>();//数据粘贴列
        private void LoadData()
        {
            //this.splitContainerControl1.SplitterPosition = (2* this.splitterControl1.Width) / 3;
            
            treeList1.DataSource = null;
            bLoadingData = true;
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
                //dataTable2.Columns.Clear();
               
                chartdataTable.Columns.Clear();

            }

            //chartdataTable.Columns.Add("Title", typeof(string));
            //chartdataTable.Columns.Add("ID", typeof(string));
            //chartdataTable.Columns.Add("ParentID", typeof(string));
            //chartdataTable.Columns.Add("Forecast", typeof(int));
            //chartdataTable.Columns.Add("ForecastID", typeof(string));
            //chartdataTable.Columns.Add("Sort", typeof(int));
            AddFixColumn();
            for (int i = firstyear; i <= endyear; i++)
            {
                AddColumn(i);

            }
            //treeList1.Columns.AssignTo(treeList2.Columns);

            TreeListColumn col = AddColumn(1993);
            col.VisibleIndex = -1;
            col.Caption = "常规增长率";
            col.Width = 80;
            load1();
            //load2();
            //foreach (DataColumn dc in dataTable2.Columns)
            //{
            //    if (!chartdataTable.Columns.Contains(dc.ColumnName))
            //        chartdataTable.Columns.Add(dc.ColumnName, dc.DataType);

            //}
            bLoadingData = false;
            treeList1.CollapseAll();
           
            if (treeList1.Nodes.Count > 1)
            {
                treeList1.MoveFirst();
                treeList1.Nodes[1].Expanded = true;
            }

            
        }

        private void load1()
        {

            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.Forecast = type;
            psp_Type.ForecastID = forecastReport.ID;
            IList<Ps_Forecast_Math> listTypes = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_Forecast_Math));

            treeList1.DataSource = dataTable;
            treeList1.Columns["Sort"].SortOrder = SortOrder.Ascending;
            Application.DoEvents();
            //if(treeList1.Nodes.Count>0)
            //treeList1.Nodes[0].Expanded = true;

        }
        private void load2()
        {
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.Forecast = type32;
            psp_Type.ForecastID = forecastReport.ID;
            IList<Ps_Forecast_Math> listTypes = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);

            //dataTable2 = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_Forecast_Math));
            //treeList2.DataSource = dataTable2;
            //treeList2.Columns["Sort"].SortOrder = SortOrder.Ascending;
            Application.DoEvents();
            //if (treeList2.Nodes.Count > 0)
            //treeList2.Nodes[0].Expanded = true;

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
            //column = new TreeListColumn();
            //column.FieldName = "Col2";
            //column.Caption = "产品生产能力";
            //column.VisibleIndex = 1;
            //column.Width = 150;
            //column.OptionsColumn.AllowEdit = false;
            //column.OptionsColumn.AllowSort = false;
            //this.treeList1.Columns.AddRange(new TreeListColumn[] {
            //column});
            column = new TreeListColumn();
            column.FieldName = "Sort";
            column.Caption = "序号";
            column.VisibleIndex = -1;
            column.Width = 50;
            column.OptionsColumn.AllowEdit = false;
            //column.SortOrder = SortOrder.Ascending;
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
        private TreeListColumn AddColumn(int year)
        {
            TreeListColumn column = new TreeListColumn();

            column.FieldName = "y" + year;
            column.Tag = year;
            column.Caption = year + "年";
            column.Name = year.ToString();
            column.Width = 60;
            //column.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            column.VisibleIndex = year;//

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
            return column;
        }

        private void CopyBaseColor(FORBaseColor bc1, FORBaseColor bc2)
        {
            bc1.UID = bc2.UID;
            bc1.Title = bc2.Title;
            bc1.Remark = bc2.Remark;
            bc1.Color = bc2.Color;
            bc1.Color1 = ColorTranslator.FromOle(bc2.Color);
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
            //this.chart1.SaveAsImage(sf.FileName, ci);
        }


        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //FileClass.ExportToExcelOld(this.forecastReport.Title, "", this.gridControl1);
        }

        private void Save()
        {
            //保存

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    List<int> li = new List<int>();
        //    for (int i = firstyear; i <= endyear; i++)
        //    {
        //        li.Add(i);
        //    }

        //    FormChooseYears cy = new FormChooseYears();
        //    cy.ListYearsForChoose = li;
        //    if (cy.ShowDialog() != DialogResult.OK)
        //        return;
        //    Hashtable ht = new Hashtable();

        //    foreach (DataRow a in cy.DT.Rows)
        //    {
        //        if (a["B"].ToString() == "True")
        //            ht.Add(Guid.NewGuid().ToString(), Convert.ToInt32(a["A"].ToString().Replace("年", "")));
        //    }
        //    FormGdpView fgv = new FormGdpView();
        //    fgv.ProjectUID = ProjectUID;
        //    fgv.HT = ht;
        //    fgv.ShowDialog();
        //}
        private void cacsumsehui(string strname)
        {

            Ps_Forecast_Math v;
            DataRow[] drlist = dataTable.Select("Title like '%常规%'");
            //foreach (DataColumn dc in dataTable.Columns)
            //{
            double value = 0;
            //if (dc.ColumnName.IndexOf("y") != 0)
            //{
            //    continue;
            //}
            foreach (DataRow dr in drlist)
            {

                if (treeList1.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode == null)
                    continue;
                try
                {
                    value += Math.Round(Convert.ToDouble(dr[strname]), 2);
                }
                catch { }
            }
            foreach (DataRow dr in drlist)
            {
                TreeListNode node = treeList1.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode;
                if (node == null)
                {
                    int i = dataTable.Rows.IndexOf(dr);


                    dataTable.Rows[i][strname] = value;
                    //DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
                    v = DataConverter.RowToObject<Ps_Forecast_Math>(dataTable.Rows[i]);
                    commonhelp.ResetValue(v.ID, strname);
                    v.Col4 = "yes";
                    Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
                }
            }



            DataRow[] drlist2 = dataTable.Select("Title like '%大用户%'");

            value = 0;

            foreach (DataRow dr in drlist2)
            {
                if (treeList1.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode == null)
                    continue;

                try
                {
                    value += Math.Round(Convert.ToDouble(dr[strname]), 2);
                }
                catch { }
            }
            foreach (DataRow dr in drlist2)
            {
                TreeListNode node = treeList1.FindNodeByKeyID(dr["ID"]).ParentNode.ParentNode;
                if (node == null)
                {
                    int i = dataTable.Rows.IndexOf(dr);

                    dataTable.Rows[i][strname] = value;
                    v = DataConverter.RowToObject<Ps_Forecast_Math>(dataTable.Rows[i]);
                    v.Col4 = "yes";
                    commonhelp.ResetValue(v.ID, strname);
                    Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
                }
            }

            //  dataTable.Rows[2][dc.ColumnName] = value;
            //           }

        }
        
        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //表格数据发生变化
            if (e.Column.FieldName.Substring(0, 1) != "y") return;
            //if ((e.Node.ParentNode.ParentNode == null && !e.Node.HasChildren))
            //{
            //  ;
            //      Common.Services.BaseService.Update<Ps_Forecast_Math>( DataConverter.RowToObject<Ps_Forecast_Math>( (e.Node.TreeList.GetDataRecordByNode(e.Node) as DataRowView).Row));
            //      return;
            //}
            double d = 0;
            if(e.Value!=null)
            {
                if (!double.TryParse(e.Value.ToString(), out d)) return;
            }
            
            treeList1.BeginInit();
            if (e.Column.FieldName == "y1993")
            {
                try { jsdl(e.Node, e.Column); }
                catch { }

            }
            else
            {
                try
                {
                    CalculateSum(e.Node, e.Column);
                    if (e.Node.ParentNode!=null)
                    {
                        if (!e.Node.ParentNode["Title"].ToString().Contains("全地区"))
                            cacsumsehui(e.Column.FieldName);
                    }
                   
                }
                catch { }
            }
            treeList1.EndInit();
            //Save();
            //RefreshChart();
        }
       
        #region 分类管理
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //TreeListNode focusedNode = treeList1.FocusedNode;

            //if (focusedNode == null)
            //{
            //    return;
            //}

            ////if (focusedNode.ParentNode == null)
            ////    return;

            //FormHistory2_add frm = new FormHistory2_add();
            //frm.Text = "增加分类";

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    Ps_Forecast_Math pf = new Ps_Forecast_Math();
            //    pf.ID = createID();
            //    pf.Forecast = type;
            //    pf.ForecastID = type1;
            //    pf.Title = frm.TypeTitle;
            //    pf.Col2 = frm.COL10;
            //    //pf.y1990 =frm.COL11;
            //    //pf.y1991 = frm.COL12;
            //    pf.ParentID = focusedNode["ID"].ToString();
            //    pf.Col4 = ProjectUID;
            //    pf.Sort = frm.Sortid;
            //    //object obj = Services.BaseService.GetObject("SelectPs_HistoryMaxID", null);
            //    //if (obj != null)
            //    //    pf.Sort = ((int)obj) + 1;
            //    //else
            //    //    pf.Sort = 1;

            //    Ps_Forecast_Math pf2 = new Ps_Forecast_Math();
            //    pf2.ID = pf.ID + splitstr;
            //    pf2.Forecast = type32;
            //    pf2.ForecastID = type31;
            //    pf2.Title = pf.Title;
            //    pf2.ParentID = pf.ParentID + splitstr;
            //    pf2.Col4 = ProjectUID;
            //    pf2.Sort = frm.Sortid;
            //    try
            //    {
            //        Services.BaseService.Create<Ps_Forecast_Math>(pf);
            //        Services.BaseService.Create<Ps_Forecast_Math>(pf2);
            //        //dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, dataTable.NewRow()));
            //        LoadData();
            //    }
            //    catch (Exception ex) { MsgBox.Show("增加分类出错：" + ex.Message); }
            //}

        }

        private string createID()
        {
            string str = Guid.NewGuid().ToString();
            return str.Substring(str.Length - 12);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
                return;
            if (tln.HasChildren)
                return;


            if (MsgBox.ShowYesNo("是否删除分类 " + tln.GetValue("Title") + "？") == DialogResult.Yes)
            {
                Ps_Forecast_Math pf = new Ps_Forecast_Math();
                pf.ID = tln["ID"].ToString();

                try
                {
                    //DeletePSP_ValuesByType里面删除数据和分类
                    Common.Services.BaseService.Delete<Ps_Forecast_Math>(pf);
                    pf.ID = pf.ID + splitstr;
                    Common.Services.BaseService.Delete<Ps_Forecast_Math>(pf);
                    //TreeListNode node = treeList2.FindNodeByKeyID(pf.ID);
                    //if (node != null)
                    //    treeList2.DeleteNode(node);
                    treeList1.DeleteNode(treeList1.FocusedNode);

                }
                catch { }
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //修改
            //if (treeList1.FocusedNode == null)
            //    return;

            //if (treeList1.FocusedNode.ParentNode == null)
            //{
            //    MsgBox.Show("该目录不可修改");
            //    return;
            //}


            //FormHistory2_add frm = new FormHistory2_add();
            //frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            //frm.Text = "修改分类";
            //Ps_Forecast_Math v = new Ps_Forecast_Math();
            //v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(treeList1.FocusedNode["ID"].ToString());

            //frm.COL10 = v.Col2;
            //frm.Sortid = v.Sort;
            ////frm.COL11 = v.y1990;
            ////frm.COL12 = v.y1991;

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    //TreeNodeToDataObject<Ps_Forecast_Math>(v, treeList1.FocusedNode);
            //    v.Title = frm.TypeTitle;
            //    Ps_Forecast_Math v2 = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(v.ID + splitstr);
            //    v2.Title = v.Title;
            //    if (treeList1.FocusedNode.Nodes.Count == 0)
            //    {
            //        v.Col2 = frm.COL10;
            //        //v.y1990 = frm.COL11;
            //        //v.y1991 = frm.COL12;
            //    }
            //    v.Sort = frm.Sortid;
            //    v2.Sort = v.Sort;
            //    try
            //    {
            //        try { treeList1.FocusedNode["Col2"] = v.Col2; }
            //        catch { }
            //        //try {treeList1.FocusedNode.SetValue("y1990", v.y1990);} catch { }
            //        //try {treeList1.FocusedNode.SetValue("y1991", v.y1991);} catch { }
            //        try { treeList1.FocusedNode.SetValue("Title", v.Title); }
            //        catch { }
            //        try { treeList1.FocusedNode.SetValue("Sort", v.Sort); }
            //        catch { }
            //        Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
            //        Common.Services.BaseService.Update<Ps_Forecast_Math>(v2);
            //        TreeListNode node = treeList2.FindNodeByKeyID(v2.ID);
            //        try { node["Title"] = v2.Title; }
            //        catch { }
            //        try { node["Sort"] = v2.Sort; }
            //        catch { }

            //    }
            //    catch (Exception ex) { MsgBox.Show("修改分类出错：" + ex.Message); }

            //}
        }
        #endregion
        static public void TreeNodeToDataObject<T>(T dataObject, DevExpress.XtraTreeList.Nodes.TreeListNode treeNode)
        {
            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.Name.Substring(0, 1) == "y")
                {
                    object obj = treeNode.GetValue(pi.Name);
                    try
                    {
                        pi.SetValue(dataObject, obj != DBNull.Value ? obj : 0, null);
                    }
                    catch (Exception err) { MessageBox.Show(err.Message); }
                }
                //if (pi.Name == "COL10" || pi.Name == "y1991" || pi.Name == "y1991") {
                //    pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);
                //}
            }
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (treeList1.FocusedNode.HasChildren)
            {
                if (treeList1.FocusedNode["Title"].ToString().Contains("常规"))
                {
                    if (treeList1.FocusedColumn.FieldName != "y1993")
                    {
                        e.Cancel = true;
                    }
                }
                else
                    e.Cancel = true;
            }
            if (!EditRight)
            {
                e.Cancel = true;
            }
        }
        //private void treeList2_ShowingEditor(object sender, CancelEventArgs e)
        //{
        //    if (treeList2.FocusedNode.HasChildren)
        //    {
        //        e.Cancel = true;
        //    }
        //    if (!EditRight)
        //    {
        //        e.Cancel = true;
        //    }
        //}
        //把起始年和终止年都进行计算；
        private void CalaculateSumnew1(DataRow row, DataTable dat)
        {
            // DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_Forecast_Math hs = DataConverter.RowToObject<Ps_Forecast_Math>(row);
            //Common.Services.BaseService.Update<Ps_History>(hs);
            string parentid = hs.ParentID;
            DataRow[] parentNode = dat.Select("ID='" + parentid + "'");
            for (int j = firstyear; j <= endyear; j++)
            {
                string field = "y" + j.ToString();


                if (string.IsNullOrEmpty(parentid))
                {
                    if (hs.Title.Contains("各县合计"))
                    {
                        DataRow[] drlist = dat.Select("Title like '%全社会%'");
                        double value = 0;
                        foreach (DataRow dr in drlist)
                        {
                            if (dr["Title"].ToString().Contains("全地区"))
                            {
                                int i = dat.Rows.IndexOf(dr);
                                dat.Rows[i][field] = hs.GetType().GetProperty(field).GetValue(hs, null);
                                Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(dat.Rows[i]);


                                Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
                            }


                        }
                    }
                    return;
                }

                DataRow[] drlist1 = dat.Select("ParentID='" + parentid + "'");
                double sum = 0;
                bool TSL_falg = false;
                double Tsl_double = 0;
                foreach (DataRow nd in drlist1)
                {
                    DataRow[] childrennode = dat.Select("ParentID='" + nd["ID"].ToString() + "'");
                    if (nd["Title"].ToString() == "同时率")
                    {
                        //记录同时率
                        if (Convert.ToDouble(nd[field].ToString()) != 0)
                        {
                            TSL_falg = true;
                            Tsl_double = Convert.ToDouble(nd[field].ToString());
                        }
                        continue;
                    }
                    if (parentNode[0]["Title"].ToString().Contains("合计") && childrennode.Length == 0) continue;
                    object value = nd[field].ToString();
                    if (value != null && value != DBNull.Value)
                    {
                        sum += Convert.ToDouble(value);
                    }
                }
                if (sum != 0)
                {
                    //如果同时率不为0，则乘以同时率
                    if (TSL_falg)
                    {
                        sum = sum * Tsl_double;
                    }
                    parentNode[0][field] = sum;
                    Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(parentNode[0]);
                    //row = (parentNode.TreeList.GetDataRecordByNode(parentNode) as DataRowView).Row;
                    // v = DataConverter.RowToObject<Ps_History>(row);

                    //v = Services.BaseService.GetOneByKey<Ps_History>(parentNode["ID"].ToString());
                    //TreeNodeToDataObject<Ps_History>(v, parentNode);

                    Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
                }
                //else
                //    //parentNode.SetValue(column.FieldName, null);
                //    parentNode[0][field] = 0;
                //parentNode.SetValue(column.FieldName, 0);
                //Ps_History vparent = DataConverter.RowToObject<Ps_History>(parentNode[0]);
            }


            CalaculateSumnew1(parentNode[0], dat);

            ///////
            //TreeListNode parentNode = node.ParentNode;

        }
        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            //Ps_Forecast_Math v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(node["ID"].ToString());
            //TreeNodeToDataObject<Ps_Forecast_Math>(v, node);
            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(row);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(v);

            commonhelp.SetValue(v.ID, column.FieldName, 1);

            TreeListNode parentNode = node.ParentNode;
            if (parentNode == null)
            {
                if (node["Title"].ToString().Contains("各县合计"))
                {
                    DataRow[] drlist = dataTable.Select("Title like '%全社会%'");
                    double value = 0;
                    foreach (DataRow dr in drlist)
                    {
                        if (dr["Title"].ToString().Contains("全地区"))
                        {
                            int i = dataTable.Rows.IndexOf(dr);
                            dataTable.Rows[i][column.FieldName] = node[column.FieldName];
                            v = DataConverter.RowToObject<Ps_Forecast_Math>(dataTable.Rows[i]);


                            Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
                        }


                    }
                }
                return;
            }
            //if (node["Title"].ToString() == "同时率") return;

            double sum = 0;
            bool TSL_falg = false;
            double Tsl_double = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            {
                if (nd["Title"].ToString() == "同时率")
                {
                    //记录同时率
                    if (Convert.ToDouble(nd[column].ToString()) != 0)
                    {
                        TSL_falg = true;
                        Tsl_double = Convert.ToDouble(nd[column].ToString());
                    }
                    continue;
                }
                if (parentNode["Title"].ToString().Contains("合计") && !nd.HasChildren) continue;
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
                row = (parentNode.TreeList.GetDataRecordByNode(parentNode) as DataRowView).Row;
                v = DataConverter.RowToObject<Ps_Forecast_Math>(row);

                //v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(parentNode["ID"].ToString());
                //TreeNodeToDataObject<Ps_Forecast_Math>(v, parentNode);

                Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
            }
            else
                parentNode.SetValue(column.FieldName, 0);
            CalculateSum(parentNode, column);
        }
        //对全社会的值进行统计 根据全社会的常规电量和大用户电量来进行统计
        private void Calqushehui1()
        {
            DataRow[] rows1 = dataTable.Select("Title like '%全社会%'");
            if (rows1.Length>0)
            {
                foreach (DataRow dr in rows1)
                {
                    DataRow[] rows2 = dataTable.Select("Title like '%常规%' and ParentID='" + dr["ID"] + "'");
                    DataRow[] rows3 = dataTable.Select("Title like '%同时率%' and ParentID='" + dr["ID"] + "'");
                    DataRow[] rows4 = dataTable.Select("Title like '%大用户%' and ParentID='" + dr["ID"] + "'");
                    double sum = 0.0, tsl = 1.0;
                    for (int i = firstyear; i < endyear; i++)
                    {
                        sum = 0.0; tsl = 1.0;
                        string y = "y" + i.ToString();
                        sum += Convert.ToDouble(rows2[0][y]);
                        sum += Convert.ToDouble(rows4[0][y]);
                        tsl = Convert.ToDouble(rows3[0][y]) != 0 ? Convert.ToDouble(rows3[0][y]) : 1.0;
                        dr[y] = sum * tsl;
                    }
                    Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(dr);
                    Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
                }
            }
           
        }
        private void Calqushehui2()
        {
            //DataRow[] rows1 = dataTable2.Select("Title like '%全社会%'");
            //if (rows1.Length>0)
            //{
            //    foreach (DataRow dr in rows1)
            //    {
            //        DataRow[] rows2 = dataTable2.Select("Title like '%常规%' and ParentID='" + dr["ID"] + "'");
            //        DataRow[] rows3 = dataTable2.Select("Title like '%同时率%' and ParentID='" + dr["ID"] + "'");
            //        DataRow[] rows4 = dataTable2.Select("Title like '%大用户%' and ParentID='" + dr["ID"] + "'");
            //        double sum = 0.0, tsl = 1.0;
            //        for (int i = firstyear; i < endyear; i++)
            //        {
            //            sum = 0.0; tsl = 1.0;
            //            string y = "y" + i.ToString();
            //            sum += Convert.ToDouble(rows2[0][y]);
            //            sum += Convert.ToDouble(rows4[0][y]);
            //            tsl = Convert.ToDouble(rows3[0][y]) != 0 ? Convert.ToDouble(rows3[0][y]) : 1.0;
            //            dr[y] = sum * tsl;
            //        }
            //        Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(dr);
            //        Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
            //    }
            //}
           
        }
        private void CalculateSum3(TreeListNode node, TreeListColumn column)
        {
            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(row);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(v);

            commonhelp.SetValue(v.ID, column.FieldName, 1);

            TreeListNode parentNode = node.ParentNode;
            if (parentNode == null)
            {
                if (node["Title"].ToString().Contains("各县合计"))
                {
                    //DataRow[] drlist = dataTable2.Select("Title like '%全社会%'");
                    double value = 0;
                    //foreach (DataRow dr in drlist)
                    //{
                    //    if (dr["Title"].ToString().Contains("全地区"))
                    //    {
                    //        int i = dataTable2.Rows.IndexOf(dr);
                    //        dataTable2.Rows[i][column.FieldName] = node[column.FieldName];
                    //        v = DataConverter.RowToObject<Ps_Forecast_Math>(dataTable2.Rows[i]);


                    //        Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
                    //    }


                    //}
                }
                return;
            }
            //if (node["Title"].ToString() == "同时率") return;

            double sum = 0;
            bool TSL_falg = false;
            double Tsl_double = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            {
                if (nd["Title"].ToString() == "同时率")
                {
                    //记录同时率
                    if (Convert.ToDouble(nd[column].ToString()) != 0)
                    {
                        TSL_falg = true;
                        Tsl_double = Convert.ToDouble(nd[column].ToString());
                    }
                    continue;
                }
                if (parentNode["Title"].ToString().Contains("合计") && !nd.HasChildren) continue;
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
                row = (parentNode.TreeList.GetDataRecordByNode(parentNode) as DataRowView).Row;
                v = DataConverter.RowToObject<Ps_Forecast_Math>(row);

                //v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(parentNode["ID"].ToString());
                //TreeNodeToDataObject<Ps_Forecast_Math>(v, parentNode);

                Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
            }
            else
                parentNode.SetValue(column.FieldName, 0);
            CalculateSum3(parentNode, column);
        }
        private void CalculateSum2(TreeListNode node, TreeListColumn column)
        {
            //Ps_Forecast_Math v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(node["ID"].ToString());
            //TreeNodeToDataObject<Ps_Forecast_Math>(v, node);
            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(row);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
            TreeListNode parentNode = node.ParentNode;
            if (parentNode == null)
            {
                return;
            }
            //if (node["Title"].ToString() == "同时率") return;
            //if (node["Title"].ToString() == "同时率") 
            //{ 


            //}
            double sum = 0;
            double valuetemp = 0;
            bool istsl = false;
            foreach (TreeListNode nd in parentNode.Nodes)
            {

                object value = null;
                if (nd["Title"].ToString() == "同时率")
                {
                    istsl = true;
                    value = nd.GetValue(column.FieldName);
                    if (value != null && value != DBNull.Value)
                    {
                        valuetemp = Convert.ToDouble(value);
                        continue;
                    }

                }
                if (!nd.HasChildren && istsl && parentNode["Title"].ToString().Contains("合计"))
                    continue;
                value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value)
                {
                    if (istsl)
                        sum += valuetemp * Convert.ToDouble(value);
                    else
                        sum += Convert.ToDouble(value);
                }
            }
            if (sum != 0)
            {
                parentNode.SetValue(column.FieldName, sum);
                row = (parentNode.TreeList.GetDataRecordByNode(parentNode) as DataRowView).Row;
                v = DataConverter.RowToObject<Ps_Forecast_Math>(row);

                //v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(parentNode["ID"].ToString());
                //TreeNodeToDataObject<Ps_Forecast_Math>(v, parentNode);

                Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
            }
            else
                parentNode.SetValue(column.FieldName, null);
            CalculateSum2(parentNode, column);
        }
        private void jsdl(TreeListNode node, TreeListColumn column)
        {
            double nl = (double)node["y1993"];
            nl /= 100;

            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            int byear = firstyear;
            double d1 = (double)node["y" + byear];
            while (d1 == 0 && byear < endyear)
            {
                byear++;
                d1 = (double)node["y" + byear];
            }
            for (int i = byear + 1; i <= endyear; i++)
            {
                //node.SetValue("y" + i, Math.Round(d1 * Math.Pow((1 + nl), i - firstyear), 2));
                row["y" + i] = Math.Round(d1 * Math.Pow((1 + nl), i - byear), 2);

            }
            Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(row);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(v);

            sumDL(node.ParentNode, byear, endyear);

        }

        private void sumDL(TreeListNode parentNode, int byear, int endyear)
        {

            if (parentNode == null || byear >= endyear) return;
            DataRow row = (parentNode.TreeList.GetDataRecordByNode(parentNode) as DataRowView).Row;

            for (int i = byear; i <= endyear; i++)
            {
                string fname = "y" + i;
                double sum = 0;
                foreach (TreeListNode nd in parentNode.Nodes)
                {
                    object value = nd.GetValue(fname);
                    if (value != null && value != DBNull.Value)
                    {
                        sum += Convert.ToDouble(value);
                    }
                }
                row[fname] = sum;
            }
            Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(row);
            Common.Services.BaseService.Update<Ps_Forecast_Math>(v);
            sumDL(parentNode.ParentNode, byear, endyear);
        }

        //private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        //{
        //    if (e.Node != null)
        //    {
        //        string id = e.Node["ID"] + splitstr;
        //        TreeListNode node = treeList2.FindNodeByKeyID(id);
        //        if (node != null)
        //        {
        //            treeList2.Selection.Clear();
        //            treeList2.SetFocusedNode(node);
        //        }
        //    }
        //}
        private void reloadTree2()
        {
            //dataTable2.Clear();
            //load2();
        }
        private void treeList1_AfterDragNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            string id = e.Node["ID"].ToString();

            Ps_Forecast_Math v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(id);
            if (v != null)
            {
                v.ParentID = e.Node["ParentID"].ToString();
                Services.BaseService.Update<Ps_Forecast_Math>(v);
                v = Services.BaseService.GetOneByKey<Ps_Forecast_Math>(id + splitstr);
                if (v != null)
                {
                    v.ParentID = e.Node["ParentID"].ToString() + splitstr;
                    Services.BaseService.Update<Ps_Forecast_Math>(v);
                    reloadTree2();
                }
            }
        }
        #region 统计表
        /// <summary>
        /// 现有大工业用户产能及用电情况
        /// </summary>
        private void tj432()
        {
            //FormChooseYears frm = new FormChooseYears();
            //foreach (TreeListColumn column in treeList1.Columns) {
            //    if (column.Caption.IndexOf("年") > 0) {
            //        frm.ListYearsForChoose.Add((int)column.Tag);
            //    }
            //}

            //if (frm.ShowDialog() == DialogResult.OK) {
            Formtj432 f = new Formtj432();
            f.Text = "现有大工业用户产能及用电情况";
            f.CanPrint = base.EditRight;
            //f.Text = Title = "本地区" + frm.ListChoosedYears[0].Year + "～" + frm.ListChoosedYears[frm.ListChoosedYears.Count - 1].Year + "年分区县供电实绩";
            //f.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1), frm.ListYearsForChoose);
            List<int> list = new List<int>();
            list.Add(endyear);
            setcols432(list);
            f.GridDataTable = ConvertTreeListToDataTable(treeList1);
            f.ShowDialog();
            //}
        }
        /// <summary>
        /// 全市常规增长率+大用户法预测结果
        /// </summary>
        private void tj433()
        {
            FormChooseYears1 frm = new FormChooseYears1();
            foreach (TreeListColumn column in treeList1.Columns)
            {
                if (column.Caption.IndexOf("年") > 0)
                {
                    frm.ListYearsForChoose.Add((int)column.Tag);
                }
            }

            if (frm.ShowDialog() == DialogResult.OK)
            {

                Formtj432 f = new Formtj432();
                f.Text = "全市常规增长率+大用户法预测结果";
                f.CanPrint = base.EditRight;
                //f.Text = Title = "本地区" + frm.ListChoosedYears[0].Year + "～" + frm.ListChoosedYears[frm.ListChoosedYears.Count - 1].Year + "年分区县供电实绩";
                //f.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1), frm.ListYearsForChoose);
                setcols433(frm.DT.Rows);
                f.GridDataTable = ConvertTreeListToDataTable433(treeList1);
                DialogResult dr = f.ShowDialog();
            }

        }
        /// <summary>
        /// 区县常规增长率+大用户法预测结果
        /// </summary>
        private void tj532()
        {
            FormChooseYears1 frm = new FormChooseYears1();
            foreach (TreeListColumn column in treeList1.Columns)
            {
                if (column.Caption.IndexOf("年") > 0)
                {
                    frm.ListYearsForChoose.Add((int)column.Tag);
                }
            }
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Formtj432 f = new Formtj432();
                f.Text = treeList1.FocusedNode["Title"].ToString() + "常规增长率+大用户法预测结果";
                f.CanPrint = base.EditRight;
                setcols433(frm.DT.Rows);
                f.GridDataTable = ConvertTreeListToDataTable532(treeList1);
                DialogResult dr = f.ShowDialog();
            }

        }

        private DataTable ConvertTreeListToDataTable532(DevExpress.XtraTreeList.TreeList treeList1)
        {

            TreeListNode node = treeList1.FocusedNode;
            DataRow row1 = ((DataRowView)node.TreeList.GetDataRecordByNode(node)).Row;
            DataRow newrow = createRow(showtable, row1);
            newrow["Title"] = "全社会用电量";
            setFh(newrow, node["ID"].ToString());
            AddNodeDataToDataTable433(showtable, node, cols, "");

            #region 计算增长率
            DataTable dtReturn = showtable;
            DataColumn columnBegin = null;
            foreach (DataColumn column in dtReturn.Columns)
            {
                if (column.Caption.IndexOf("年") > 0)
                {
                    if (columnBegin == null)
                    {
                        columnBegin = column;
                    }
                }
                else if (column.ColumnName.IndexOf("增长率") > 0)
                {
                    DataColumn columnEnd = dtReturn.Columns[column.Ordinal - 2];

                    foreach (DataRow row in dtReturn.Rows)
                    {
                        object numerator = row[columnEnd];
                        object denominator = row[columnBegin];

                        if (numerator != DBNull.Value && denominator != DBNull.Value)
                        {
                            try
                            {
                                int intervalYears = Convert.ToInt32(columnEnd.ColumnName.Replace("y", ""))
                                    - Convert.ToInt32(columnBegin.ColumnName.Replace("y", ""));
                                object obj = Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);
                                row[column] = obj;
                                if (double.IsNaN((double)row[column]) || double.IsInfinity((double)row[column]))
                                    row[column] = 0;
                            }
                            catch
                            {
                                row[column] = 0;
                            }
                        }
                    }

                    //本次计算增长率的列作为下次的起始列

                    columnBegin = columnEnd;
                }
            }
            #endregion
            return showtable;
        }
        private void AddNodeDataToDataTable433(DataTable dt, TreeListNode node, List<string> listColID, string padstr)
        {
            padstr += "　";
            foreach (TreeListNode nd in node.Nodes)
            {
                //if (nd["Title"].ToString().Contains("新增")) continue;
                DataRow newRow = dt.NewRow();
                string space = string.Copy(padstr);
                foreach (string colID in listColID)
                {
                    //分类名，第二层及以后在前面加空格
                    if (colID == "Title" && nd.ParentNode != null)
                    {
                        newRow[colID] = space + nd[colID];
                    }
                    else
                    {
                        newRow[colID] = nd[colID];
                    }
                }
                setFh(newRow, nd["ID"].ToString());//负荷
                dt.Rows.Add(newRow);

                AddNodeDataToDataTable433(dt, nd, listColID, padstr);
            }
        }
        List<DataRow> dtman1 = new List<DataRow>();//现有用户
        List<DataRow> dtman2 = new List<DataRow>();//新增用户
        private void AddNodeDataToDataTable4332(DataTable dt, TreeListNode node, List<string> listColID, string padstr, string city)
        {
            padstr += "　";
            DataTable dt1 = dt;
            List<DataRow> listman = null;
            List<DataRow> listman2 = null;
            string title = node["Title"].ToString();
            if (title.Contains("现有"))
            {
                listman = dtman1;
            }
            else if (title.Contains("新增"))
            {
                listman = dtman2;
            }

            foreach (TreeListNode nd in node.Nodes)
            {
                //if (nd["Title"].ToString().Contains("新增")) continue;
                if (listman == null)
                {
                    title = nd["Title"].ToString();
                    if (title.Contains("现有"))
                    {
                        listman2 = dtman1;
                    }
                    else if (title.Contains("新增"))
                    {
                        listman2 = dtman2;
                    }
                    else
                        listman2 = null;
                }
                DataRow newRow = dt1.NewRow();
                string space = string.Copy(padstr);
                foreach (string colID in listColID)
                {
                    //分类名，第二层及以后在前面加空格
                    if (colID == "Title" && nd.ParentNode != null)
                    {
                        newRow[colID] = space + nd[colID];
                    }
                    else
                    {
                        newRow[colID] = nd[colID];
                    }
                }
                if (city != "") newRow["Title"] = city;
                setFh(newRow, nd["ID"].ToString());//负荷
                if (listman != null)
                    listman.Add(newRow);
                else if (listman2 != null)
                    listman2.Add(newRow);
                else
                    dt1.Rows.Add(newRow);

                AddNodeDataToDataTable4332(dt, nd, listColID, padstr, "");
            }
        }
        private void createHj433(TreeListNode node, IList<Ps_Forecast_Math> list1, IList<Ps_Forecast_Math> list2)
        {
            DataRow newrow = showtable.NewRow();
            string title = node["Title"].ToString();
            Ps_Forecast_Math ps1 = null;//电量
            Ps_Forecast_Math ps2 = null;//负荷
            foreach (Ps_Forecast_Math ps in list1)
            {
                if (ps.Title == title)
                {
                    ps1 = ps;
                    break;
                }
            }
            if (ps1 == null) return;
            foreach (Ps_Forecast_Math ps in list2)
            {
                if (ps.Title == title)
                {
                    ps2 = ps;
                    break;
                }
            }
            if (ps1 != null)
            {
                newrow = Itop.Common.DataConverter.ObjectToRow(ps1, newrow);
                newrow = createRow(showtable, newrow);
            }
            if (ps2 != null)
            {
                DataRow row = showtable.NewRow();
                row = Itop.Common.DataConverter.ObjectToRow(ps2, row);
                setData2(newrow, row);
            }
            if (title == "常规用电量")
            {
                foreach (TreeListNode n1 in node.Nodes)
                {
                    createHj433(n1, list1, list2);
                }
            }

        }
        private DataTable ConvertTreeListToDataTable433(DevExpress.XtraTreeList.TreeList treeList1)
        {
            IList<Ps_Forecast_Math> list = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathGroupList", where1);
            IList<Ps_Forecast_Math> list2 = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathGroupList", where2);
            dtman1.Clear();
            dtman2.Clear();
            foreach (TreeListNode node in treeList1.Nodes)
            {
                DataRow row = ((DataRowView)node.TreeList.GetDataRecordByNode(node)).Row;
                DataRow newrow1 = createRow(showtable, row);
                setFh(newrow1, row["ID"].ToString());
                newrow1["Title"] = "全社会用电量";
                bool flag = true;
                foreach (TreeListNode node2 in node.Nodes)
                {//区县
                    if (flag)
                    {
                        flag = false;
                        foreach (TreeListNode node3 in node2.Nodes)
                        {//
                            if (node3["Title"].ToString().Contains("同时率"))
                            {
                                DataRow row2 = ((DataRowView)node3.TreeList.GetDataRecordByNode(node3)).Row;
                                DataRow newrow2 = createRow(showtable, row2);
                                setFh(newrow2, row2["ID"].ToString());
                            }
                            else
                            {
                                createHj433(node3, list, list2);
                                if (node3["Title"].ToString().Contains("大用户"))
                                    AddNodeDataToDataTable4332(showtable, node3, cols, "", node2["Title"].ToString());
                            }
                        }
                    }
                    else
                    {
                        foreach (TreeListNode node4 in node2.Nodes)
                        {
                            if (node4["Title"].ToString().Contains("大用户"))
                                AddNodeDataToDataTable4332(showtable, node4, cols, "", node2["Title"].ToString());
                        }
                    }
                    //DataRow row22 = findrow(showtable, " trim(title) like '现有%' ","");
                    //if (row22 != null)
                    //    row22["Title"] = "　"+node2["Title"]+"现有";
                    //row22 = findrow(showtable, " trim(title) like '新增%' ", "");
                    //if (row22 != null)
                    //    row22["Title"] = "　" + node2["Title"] + "新增";
                }

            }
            DataRow newrow = showtable.NewRow();
            newrow["Title"] = "现有";
            showtable.Rows.Add(newrow);
            foreach (DataRow row in dtman1)
            {
                showtable.Rows.Add(row);
            }
            newrow = showtable.NewRow();
            newrow["Title"] = "新增";
            showtable.Rows.Add(newrow);
            foreach (DataRow row in dtman2)
            {
                showtable.Rows.Add(row);
            }
            #region 计算增长率
            DataTable dtReturn = showtable;
            DataColumn columnBegin = null;
            DataColumn columnPre = null;
            foreach (DataColumn column in dtReturn.Columns)
            {

                if (column.Caption.IndexOf("年电量") > 0)
                {
                    if (columnBegin == null)
                    {
                        columnBegin = column;
                    }
                }
                else if (column.ColumnName.IndexOf("增长率") > 0)
                {
                    int n = 1;
                    if (columnPre.Caption.IndexOf("年负荷") >= 0)
                        n = 2;
                    DataColumn columnEnd = dtReturn.Columns[column.Ordinal - n];

                    foreach (DataRow row in dtReturn.Rows)
                    {
                        object numerator = row[columnEnd];
                        object denominator = row[columnBegin];

                        if (numerator != DBNull.Value && denominator != DBNull.Value)
                        {
                            try
                            {
                                int intervalYears = Convert.ToInt32(columnEnd.ColumnName.Replace("y", ""))
                                    - Convert.ToInt32(columnBegin.ColumnName.Replace("y", ""));
                                object obj = Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);
                                row[column] = obj;
                                if (double.IsNaN((double)row[column]) || double.IsInfinity((double)row[column]))
                                    row[column] = 0;
                            }
                            catch
                            {
                                row[column] = 0;
                            }
                        }
                    }

                    //本次计算增长率的列作为下次的起始列

                    columnBegin = columnEnd;
                }
                columnPre = column;
            }
            #endregion
            return showtable;
        }
        private void setcols433(ICollection rows)
        {
            showtable = new DataTable();

            cols.Clear();
            colyears.Clear();
            //showtable.Columns.Add("Col1", typeof(string)).Caption = "序号";

            //cols.Add("Col1");
            showtable.Columns.Add("Title", typeof(string)).Caption = "项目名称";
            cols.Add("Title");
            string col = "";
            int lastyear = 0;
            foreach (DataRow row in rows)
            {
                int year = 0;
                if (row["B"].ToString() == "True")
                    year = Convert.ToInt32(row["A"].ToString().Replace("年", ""));
                else
                    continue;
                lastyear = year;
                col = "y" + year;
                cols.Add(col);

                showtable.Columns.Add(col, typeof(double)).Caption = year + "年电量";
                colyears.Add(col);
                showtable.Columns.Add(col + "_", typeof(double)).Caption = year + "年负荷";
                if (row["C"].ToString() == "True")
                {
                    showtable.Columns.Add(col + "_增长率", typeof(double)).Caption = "增长率";
                }

            }
            //if (col != "") {//最后一年加负荷列
            //    colyears.Add(col);
            //    showtable.Columns.Add(col + "_", typeof(double)).Caption = lastyear + "年负荷";
            //}


        }
        private DataRow findrow(DataTable table, string filter, string sort)
        {
            DataRow[] rows = table.Select(filter, sort);
            if (rows != null && rows.Length > 0)
            {

                return rows[0];
            }
            return null;

        }
        private DataRow copyrow(DataTable dt, DataRow row)
        {
            DataRow newrow = dt.NewRow();
            newrow.ItemArray = row.ItemArray.Clone() as object[];
            return newrow;
        }
        List<string> cols = new List<string>();//复制列
        List<string> colyears = new List<string>();//负荷列
        List<string> colsum = new List<string>();//合计列
        private DataTable showtable = new DataTable();
        private void setcols432(List<int> list)
        {
            showtable = new DataTable();

            cols.Clear();
            colyears.Clear();
            colsum.Clear();
            showtable.Columns.Add("Col1", typeof(string)).Caption = "序号";

            cols.Add("Col1");
            showtable.Columns.Add("Title", typeof(string)).Caption = "用户名";

            cols.Add("Title");
            //showtable.Columns.Add("Col2", typeof(string)).Caption = "产品生产能力";
            //cols.Add("Col2");


            if (list.Count > 0)
            {
                string col = "y" + list[0];
                cols.Add(col);
                colyears.Add(col);
                colsum.Add(col);
                colsum.Add(col + "_");
                showtable.Columns.Add(col, typeof(double)).Caption = list[0] + "年电量";
                showtable.Columns.Add(col + "_", typeof(double)).Caption = list[0] + "年负荷";

            }
        }
        private DataRow createRow(DataTable dt, DataRow row)
        {
            DataRow newrow = dt.NewRow();
            foreach (string col in cols)
            {
                if (dt.Columns[col].DataType == dataTable.Columns[col].DataType)
                    newrow[col] = row[col];
                else
                    newrow[col] = double.Parse(row[col].ToString());
            }
            dt.Rows.Add(newrow);
            return newrow;
        }
        private DataRow setData1(DataRow newrow, DataRow row)
        {
            foreach (string col in cols)
            {
                if (showtable.Columns[col].DataType == dataTable.Columns[col].DataType)
                    newrow[col] = row[col];
                else
                    newrow[col] = double.Parse(row[col].ToString());
            }
            return newrow;
        }
        /// <summary>
        /// row2的负荷到newrow
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        private void setData2(DataRow newrow, DataRow row2)
        {
            foreach (string col in colyears)
            {
                newrow[col + "_"] = row2[col];
            }
        }
        /// <summary>
        /// 设置负荷
        /// </summary>
        /// <param name="newrow"></param>
        /// <param name="row2"></param>
        private void setFh(DataRow newrow, string id)
        {
            //DataRow row22 = findrow(dataTable2, "ID='" + id + splitstr + "'", "");
            //if (row22 != null)
            //    setData2(newrow, row22);
        }
        string dxs = "零一二三四五六七八九十";
        private string getDX(int num)
        {
            if (num > 19) return num.ToString();
            string str = num.ToString();
            string ret = "";
            int count = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                string s = str[i].ToString();
                if (count == 0)
                {
                    ret = dxs[int.Parse(s)].ToString();
                }
                else if (count == 1)
                {
                    ret = "十" + ret;
                }
                count++;
            }
            return ret;
        }
        /// <summary>
        /// 查找大用户节点
        /// </summary>
        /// <param name="node"></param>
        private TreeListNode findDYHParent(TreeListNode root)
        {
            TreeListNode retNode = null;
            foreach (TreeListNode node in root.Nodes)
            {
                if (node["Title"].ToString().Contains("大用户"))
                {
                    retNode = node;
                    break;
                }
                retNode = findDYHParent(node);
            }
            return retNode;
        }
        /// <summary>
        /// 生成合计行
        /// </summary>
        /// <param name="dt"></param>
        private DataRow createSumRow(DataTable dt)
        {
            DataRow newrow = dt.NewRow();
            foreach (DataRow row in dt.Rows)
            {
                foreach (string col in colsum)
                {
                    double d1 = 0;
                    try { d1 = Convert.ToDouble(newrow[col]); }
                    catch { }

                    double d2 = 0;
                    try { d2 = Convert.ToDouble(row[col]); }
                    catch { }
                    newrow[col] = d1 + d2;
                }
            }
            return newrow;
        }
        private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList treeList1)
        {
            if (treeList1.Nodes.Count == 0) return showtable;
            int count1 = 0;//区县计数
            foreach (TreeListNode node in treeList1.Nodes[0].Nodes)
            {
                TreeListNode pnode = findDYHParent(node);
                if (pnode != null && pnode.HasChildren)
                {
                    count1++;
                    DataRow newrow = showtable.NewRow();
                    newrow["Title"] = node["Title"];
                    newrow["Col1"] = getDX(count1);
                    showtable.Rows.Add(newrow);
                    int count2 = 0;//区县大用户计数
                    TreeListNode node1 = pnode.Nodes[0];
                    TreeListNode findnode = null;
                    while (node1 != null)
                    {
                        if (node1["Title"].ToString().Contains("现有"))
                        {
                            findnode = node1;
                            break;
                        }
                        node1 = node1.NextNode;
                    }
                    if (findnode == null) continue;
                    foreach (TreeListNode node2 in findnode.Nodes)
                    {
                        count2++;
                        DataRowView row2 = node2.TreeList.GetDataRecordByNode(node2) as DataRowView;
                        newrow = createRow(showtable, row2.Row);
                        newrow["Col1"] = count2;
                        setFh(newrow, row2["ID"].ToString());
                    }
                }
            }
            //现有小计
            DataRow row33 = createSumRow(showtable);
            row33["Title"] = "小计";
            row33["Col1"] = getDX(count1 + 1);
            showtable.Rows.Add(row33);
            return showtable;
        }

        private DataTable ResultDataTable(object p, object p_2)
        {

            return null;
        }
        #endregion

        private void bt432_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tj432();
        }

        private void bt433_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tj433();
        }
        #region 复制节点
        TreeListNode cloneNode(TreeListNode cnode)
        {
            TreeListNode retnode = cnode.Clone() as TreeListNode;
            for (int i = cnode.Nodes.Count - 1; i >= 0; i--)
            {
                retnode.Nodes.Add(cloneNode(cnode.Nodes[i]));
            }
            return retnode;
        }
        private void setdatazero(Ps_Forecast_Math ps)
        {
            Type t = ps.GetType();
            foreach (PropertyInfo infor in t.GetProperties())
            {
                if (infor.Name.StartsWith("y") && infor.Name.Length == 5)
                {
                    try { infor.SetValue(ps, 0, null); }
                    catch { }
                }
            }
        }
        private void copyNode(TreeListNode cnode)
        {
            if (cnode.Level > 3) return;
            DataRow row = (cnode.TreeList.GetDataRecordByNode(cnode) as DataRowView).Row;
            Ps_Forecast_Math ps = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(row);
            ps.ID = createID();
            setdatazero(ps);
            Common.Services.BaseService.Create<Ps_Forecast_Math>(ps);
            string pid = ps.ID;
            ps.ID = pid + splitstr;
            ps.ParentID = ps.ParentID + splitstr;
            ps.Forecast = type32;
            ps.ForecastID = type31;
            Common.Services.BaseService.Create<Ps_Forecast_Math>(ps);

            TreeListNode nd1 = null;
            for (int i = cnode.Nodes.Count - 1; i >= 0; i--)
            {
                nd1 = cnode.Nodes[i];
                nd1["ParentID"] = pid;
                copyNode(nd1);
            }
        }
        private void btCopy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //复制节点及子节点
            TreeListNode tln = treeList1.FocusedNode;

            if (tln == null || tln.Level == 0)
                return;
            //if (MsgBox.ShowYesNo("请确认复制["+tln.GetDisplayText("Title")+"]及子类？") != DialogResult.Yes) return;
            FormHistory2_add frm = new FormHistory2_add();
            frm.Text = "复制分类";
            frm.TypeTitle = tln["Title"].ToString();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                TreeListNode copy = cloneNode(tln);
                copy["Title"] = frm.TypeTitle;
                copy["Sort"] = frm.Sortid;
                copyNode(copy);
                LoadData();
            }
        }
        #endregion
        private void bt532_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null || tln.Level != 1)
            {
                MsgBox.Show("请先选择分区后重试！");
                return;
            }
            tj532();
        }
        #region 粘贴剪切板数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pnode">父节点</param>
        /// <param name="column">列名</param>
        /// <param name="findstr">查找字符</param>
        /// <param name="f1">是否深度查找</param>
        /// <returns></returns>
        ///         
        private TreeListNode findByColumnValue(TreeListNode pnode, string column, string findstr, bool f1)
        {
            TreeListNode retnode = null;
            foreach (TreeListNode node in pnode.Nodes)
            {
                if (node[column].ToString().Contains(findstr))
                {
                    retnode = node;
                    break;
                }
                if (f1)
                {
                    retnode = findByColumnValue(node, column, findstr, f1);
                    if (retnode != null) break;
                }
            }
            return retnode;
        }
        private void createRow(string title)
        {

        }
        private object getSummaryValue(string columnid, TreeListNodes nodes)
        {
            double ret = 0;
            foreach (TreeListNode node in nodes)
            {
                double d1 = 0;
                double.TryParse(node[columnid].ToString(), out d1);
                ret += d1;
            }
            return ret;
        }
        private void updateSummaryNode(TreeListNode pnode)
        {
            foreach (string col in pasteCols)
            {
                if (col.StartsWith("y"))
                {
                    try
                    {
                        object obj = getSummaryValue(col, pnode.Nodes);
                        pnode.SetValue(col, obj);
                    }
                    catch { }
                }
            }
            updateNode(pnode);
            if (pnode.ParentNode != null)
                updateSummaryNode(pnode.ParentNode);
        }

        private void updateNode(TreeListNode node)
        {
            DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
            Ps_Forecast_Math v = DataConverter.RowToObject<Ps_Forecast_Math>(row);
            Services.BaseService.Update<Ps_Forecast_Math>(v);
        }
        private void pasteData(TreeListNode tln)
        {
            string s1 = tln["Title"].ToString();
            //if (!s1.StartsWith("现有")) return;

            IDataObject obj1 = Clipboard.GetDataObject();
            string text = obj1.GetData("Text").ToString();
            string[] lines = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            tln.TreeList.BeginInit();
            if (tln.HasChildren)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] items = lines[i].Split(new string[] { "\t" }, StringSplitOptions.None);
                    if (items.Length != pasteCols.Count) continue;
                    TreeListNode fnode = findByColumnValue(tln, "Title", items[0], false);
                    if (fnode == null) continue;
                    for (int j = 0; j < pasteCols.Count; j++)
                    {
                        try
                        {
                            if (items[j] == "") items[j] = "0";
                            fnode.SetValue(pasteCols[j], items[j]);
                        }
                        catch { }
                    }
                    updateNode(fnode);
                }
                updateSummaryNode(tln);
            }
            else if (lines.Length > 0)
            {
                string[] items = lines[0].Split(new string[] { "\t" }, StringSplitOptions.None);
                if (items.Length == pasteCols.Count && items[0] == tln["Title"].ToString())
                {
                    TreeListNode fnode = tln;
                    for (int j = 0; j < pasteCols.Count; j++)
                    {
                        try
                        {
                            if (items[j] == "") items[j] = "0";
                            fnode.SetValue(pasteCols[j], items[j]);
                        }
                        catch { }
                    }
                    updateNode(tln);
                    updateSummaryNode(tln.ParentNode);
                }
            }

            tln.TreeList.EndInit();
        }
        #endregion
        private void treeList1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                TreeListNode tln = treeList1.FocusedNode;
                if (tln == null)
                {
                    return;
                }
                pasteData(tln);
            }
        }
        //private void treeList2_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Control && e.KeyCode == Keys.V)
        //    {
        //        TreeListNode tln = treeList2.FocusedNode;
        //        if (tln == null)
        //        {
        //            return;
        //        }
        //        pasteData(tln);
        //    }
        //    //if (e.Control && e.KeyCode == Keys.C)
        //    //{
        //    //    TreeListNode tln = treeList2.FocusedNode;

        //    //    if (tln == null)
        //    //    {
        //    //        return;
        //    //    }
        //    //    copyDatatoPaste(treeList2.Selection);
        //    //}
        //}

        private void copyDatatoPaste(DevExpress.XtraTreeList.TreeListMultiSelection treeListMultiSelection)
        {
            StringBuilder sb1 = new StringBuilder();
            foreach (TreeListNode node in treeListMultiSelection)
            {
                string line = "";
                foreach (string col in pasteCols)
                {
                    if (line == "")
                        line = node[col].ToString();
                    else
                        line += "\t" + node[col].ToString();
                }
                sb1.AppendLine(line);
            }
            Clipboard.SetText(sb1.ToString());
        }

        private void bt434_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            tj433();
        }
        //更新历史数据
        void GetHistory(WaitDialogForm wait)
        {
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = 2; //type;
            psp_Type.Col4 = Itop.Client.MIS.ProgUID;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            DataTable dt1 = DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));

            psp_Type.Forecast = 3;// type32;
            IList<Ps_History> listTypes2 = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            DataTable dt2 = DataConverter.ToDataTable((IList)listTypes2, typeof(Ps_History));
            DataRow[] drfh = dt2.Select("Title like '全社会%'");
            DataRow[] dr = dt1.Select("Title like '全社会%'");
            string strparuid = createID();
            //if (dataTable.Select("Title like '全社会%'").Length == 1 && dataTable2.Select("Title like '全社会%'").Length == 1)
            if (dr.Length == 1 && drfh.Length == 1)
            {

                int i = dt2.Rows.IndexOf(drfh[0]);
                if (!drfh[0]["Title"].ToString().Contains("各县合计"))
                    dt2.Rows[i]["Title"] = drfh[0]["Title"].ToString() + "(各县合计)";

                //int i = dt1.Rows.IndexOf(dr[0]);
                if (!dr[0]["Title"].ToString().Contains("各县合计"))
                    dt1.Rows[i]["Title"] = dr[0]["Title"].ToString() + "(各县合计)";
                string uidtemp = "";
                string uidtemp2 = "";

                string uidtemp3 = "";
                uidtemp2 = dt1.Rows[i]["ID"].ToString();
                DataRow[] drtemp = dataTable.Select("Title like '%各县合计%'");
                if (drtemp.Length > 0)
                {
                    i = dataTable.Rows.IndexOf(drtemp[0]);

                    uidtemp3 = dataTable.Rows[i]["ID"].ToString();
                }
                //else
                //{
                //    //i = dt1.Rows.IndexOf(dt1.Select("Title like '%各县合计%'")[0]);

                //}


                DataRow dr2 = dt1.NewRow();

                if (dataTable.Select("Title like '同时率%' and ParentID='" + uidtemp3 + "'").Length == 0 && dt1.Select("Title like '同时率%' and ParentID='" + uidtemp2 + "'").Length == 0)
                {
                    dr2["ForecastID"] = dt1.Rows[i]["ForecastID"];
                    dr2["Forecast"] = dt1.Rows[i]["Forecast"];
                    uidtemp = createID();
                    dr2["ID"] = uidtemp;
                    dr2["Col4"] = dt1.Rows[i]["Col4"];
                    dr2["ParentID"] = dt1.Rows[i]["ID"];
                    dr2["Sort"] = -3;
                    dr2["Title"] = "同时率";
                    for (int j= firstyear; j <= endyear; j++)
                    {
                        string y = "y" + j.ToString();
                        dr2[y] = 1;
                    }
                    dt1.Rows.Add(dr2);




                    dr2 = dt2.NewRow();
                    dr2["ForecastID"] = dt2.Rows[i]["ForecastID"];
                    dr2["Forecast"] = dt2.Rows[i]["Forecast"];
                    dr2["ID"] = uidtemp + "-";
                    dr2["Col4"] = dt2.Rows[i]["Col4"];
                    dr2["ParentID"] = dt2.Rows[i]["ID"];
                    dr2["Sort"] = -3;
                    dr2["Title"] = "同时率";
                    for (int j = firstyear; j <= endyear; j++)
                    {
                        string y = "y" + j.ToString();
                        dr2[y] = 1;
                    }
                    dt2.Rows.Add(dr2);

                }
                else
                {
                    int j = 0;
                    if (dt1.Select("Title like '同时率%' and ParentID='" + uidtemp2 + "'").Length == 1)
                    {
                        j = dt1.Rows.IndexOf(dt1.Select("Title like '同时率%' and ParentID='" + uidtemp2 + "'")[0]);
                        dt1.Rows[j]["Sort"] = -3;
                    }


                    if (dt1.Select("Title like '同时率%' and ParentID='" + uidtemp2 + "'").Length == 1 && dataTable.Select("Title like '同时率%' and ParentID='" + uidtemp3 + "'").Length == 1)
                    {
                        dt1.Rows[j]["Sort"] = -3;
                        foreach (DataColumn dc in dt1.Columns)
                        {
                            //if(dc.ColumnName.IndexOf("y")>0)
                            dt1.Rows[j][dc.ColumnName] = dataTable.Select("Title like '同时率%' and ParentID='" + uidtemp2 + "'")[0][dc.ColumnName];
                            //dt1.Rows[j][dc.ColumnName] = dataTable2.Select("Title like '同时率%' and ParentID='" + uidtemp2 + "'")[0][dc.ColumnName];
                        }
                        dt1.Rows[i]["Sort"] = -3;
                        dt2.Rows[i]["Sort"] = -3;
                    }

                }
                if (dataTable.Select("Title like '常规%' and ParentID='" + uidtemp3 + "'").Length == 0 && dt1.Select("Title like '常规%' and ParentID='" + uidtemp2 + "'").Length == 0)
                {
                    dr2 = dt1.NewRow();
                    dr2["ForecastID"] = dt1.Rows[i]["ForecastID"];
                    dr2["Forecast"] = dt1.Rows[i]["Forecast"];
                    uidtemp = createID();
                    dr2["ID"] = uidtemp;
                    dr2["Col4"] = dt1.Rows[i]["Col4"];
                    dr2["ParentID"] = dt1.Rows[i]["ID"];
                    dr2["Sort"] = -2;
                    dr2["Title"] = "常规用电量";
                    dt1.Rows.Add(dr2);


                    dr2 = dt2.NewRow();
                    dr2["ForecastID"] = dt2.Rows[i]["ForecastID"];
                    dr2["Forecast"] = dt2.Rows[i]["Forecast"];
                    dr2["ID"] = uidtemp + "-";
                    dr2["Col4"] = dt2.Rows[i]["Col4"];
                    dr2["ParentID"] = dt2.Rows[i]["ID"];
                    dr2["Sort"] = -2;
                    dr2["Title"] = "常规负荷";
                    dt2.Rows.Add(dr2);
                }
                else
                {
                    int j = 0;
                    if (dt1.Select("Title like '常规%' and ParentID='" + uidtemp2 + "'").Length == 1)
                    {
                        j = dt1.Rows.IndexOf(dt1.Select("Title like '常规%' and ParentID='" + uidtemp2 + "'")[0]);
                        dt1.Rows[j]["Sort"] = -2;
                    }


                    if (dt1.Select("Title like '常规%' and ParentID='" + uidtemp2 + "'").Length == 1 && dataTable.Select("Title like '常规%' and ParentID='" + uidtemp3 + "'").Length == 1)
                    {

                        foreach (DataColumn dc in dt1.Columns)
                        {
                            //if(dc.ColumnName.IndexOf("y")>0)
                            dt1.Rows[j][dc.ColumnName] = dataTable.Select("Title like '常规%' and ParentID='" + uidtemp2 + "'")[0][dc.ColumnName];
                            //dt2.Rows[j][dc.ColumnName] = dataTable2.Select("Title like '常规%' and ParentID='" + uidtemp2 + "'")[0][dc.ColumnName];
                        }
                        dt1.Rows[j]["Sort"] = -2;
                        dt2.Rows[j]["Sort"] = -2;
                    }
                }

                if (dataTable.Select("Title like '大用户%' and ParentID='" + uidtemp3 + "'").Length == 0 && dt2.Select("Title like '大用户%' and ParentID='" + uidtemp2 + "'").Length == 0)
                {
                    dr2 = dt1.NewRow();
                    dr2["ForecastID"] = dt1.Rows[i]["ForecastID"];
                    dr2["Forecast"] = dt1.Rows[i]["Forecast"];
                    uidtemp = createID();
                    dr2["ID"] = uidtemp;
                    dr2["Col4"] = dt1.Rows[i]["Col4"];
                    dr2["ParentID"] = dt1.Rows[i]["ID"];
                    dr2["Sort"] = -1;
                    dr2["Title"] = "大用户电量";
                    dt1.Rows.Add(dr2);

                    dr2 = dt2.NewRow();
                    dr2["ForecastID"] = dt2.Rows[i]["ForecastID"];
                    dr2["Forecast"] = dt2.Rows[i]["Forecast"];
                    dr2["ID"] = uidtemp + "-";
                    dr2["Col4"] = dt2.Rows[i]["Col4"];
                    dr2["ParentID"] = dt2.Rows[i]["ID"];
                    dr2["Sort"] = -1;
                    dr2["Title"] = "大用户负荷";
                    dt2.Rows.Add(dr2);
                }
                else
                {
                    int j = 0;
                    if (dt1.Select("Title like '大用户%' and ParentID='" + uidtemp2 + "'").Length == 1)
                    {
                        j = dt1.Rows.IndexOf(dt1.Select("Title like '大用户%' and ParentID='" + uidtemp2 + "'")[0]);
                        dt1.Rows[j]["Sort"] = -1;
                    }


                    if (dt1.Select("Title like '大用户%' and ParentID='" + uidtemp2 + "'").Length == 1 && dataTable.Select("Title like '大用户%' and ParentID='" + uidtemp3 + "'").Length == 1)
                    {

                        foreach (DataColumn dc in dt1.Columns)
                        {
                            //if(dc.ColumnName.IndexOf("y")>0)
                            dt1.Rows[j][dc.ColumnName] = dataTable.Select("Title like '大用户%' and ParentID='" + uidtemp2 + "'")[0][dc.ColumnName];
                            //dt2.Rows[j][dc.ColumnName] = dataTable2.Select("Title like '大用户%' and ParentID='" + uidtemp2 + "'")[0][dc.ColumnName];
                        }
                        dt1.Rows[j]["Sort"] = -1;
                        dt2.Rows[j]["Sort"] = -1;
                    }
                }

                if (dataTable.Select("Title like '%全地区%' and ParentID=''").Length == 0)
                {
                    dr2 = dt1.NewRow();

                    foreach (DataColumn dc in dt1.Columns)
                    {
                        dr2[dc.ColumnName] = dr[0][dc.ColumnName];
                    }
                    dr2["Title"] = dr[0]["Title"].ToString().Replace("(各县合计)", "") + "(全地区)";
                    dr2["ParentID"] = "";
                    dr2["ID"] = strparuid;
                    dr2["Sort"] = -1;
                    dt1.Rows.Add(dr2);

                    dr2 = dt2.NewRow();

                    foreach (DataColumn dc in dt2.Columns)
                    {
                        dr2[dc.ColumnName] = drfh[0][dc.ColumnName];
                    }
                    dr2["Title"] = drfh[0]["Title"].ToString().Replace("(各县合计)", "") + "(全地区)";
                    dr2["ParentID"] = "";
                    dr2["ID"] = strparuid + "-";
                    dr2["Sort"] = -1;
                    dt2.Rows.Add(dr2);



                    if (dataTable.Select("Title like '同时率%' and ParentID='" + strparuid + "'").Length == 0)
                    {

                        dr2 = dt1.NewRow();
                        dr2["ForecastID"] = dt1.Rows[i]["ForecastID"];
                        dr2["Forecast"] = dt1.Rows[i]["Forecast"];
                        uidtemp = createID();
                        dr2["ID"] = uidtemp;
                        dr2["Col4"] = dt1.Rows[i]["Col4"];
                        dr2["ParentID"] = strparuid;
                        dr2["Sort"] = 0;
                        dr2["Title"] = "同时率";
                        for (int j = firstyear; j <= endyear;j++ )
                        {
                            string y = "y" + j.ToString();
                            dr2[y] = 1;
                        }
                        dt1.Rows.Add(dr2);


                        dr2 = dt2.NewRow();
                        dr2["ForecastID"] = dt2.Rows[i]["ForecastID"];
                        dr2["Forecast"] = dt2.Rows[i]["Forecast"];
                        dr2["ID"] = uidtemp + "-";
                        dr2["Col4"] = dt2.Rows[i]["Col4"];
                        dr2["ParentID"] = strparuid + "-";
                        dr2["Sort"] = 0;
                        dr2["Title"] = "同时率";
                        for (int j = firstyear; j <= endyear; j++)
                        {
                            string y = "y" + j.ToString();
                            dr2[y] = 1;
                        }
                        dt2.Rows.Add(dr2);
                    }

                    if (dataTable.Select("Title like '常规%' and ParentID='" + strparuid + "'").Length == 0)
                    {
                        dr2 = dt1.NewRow();
                        dr2["ForecastID"] = dt1.Rows[i]["ForecastID"];
                        dr2["Forecast"] = dt1.Rows[i]["Forecast"];
                        uidtemp = createID();
                        dr2["ID"] = uidtemp;
                        dr2["Col4"] = dt1.Rows[i]["Col4"];
                        dr2["ParentID"] = strparuid;
                        dr2["Sort"] = 1;
                        dr2["Title"] = "常规用电量";
                        dt1.Rows.Add(dr2);


                        dr2 = dt2.NewRow();
                        dr2["ForecastID"] = dt2.Rows[i]["ForecastID"];
                        dr2["Forecast"] = dt2.Rows[i]["Forecast"];
                        dr2["ID"] = uidtemp + "-";
                        dr2["Col4"] = dt2.Rows[i]["Col4"];
                        dr2["ParentID"] = strparuid + "-";
                        dr2["Sort"] = 1;
                        dr2["Title"] = "常规负荷";
                        dt2.Rows.Add(dr2);
                    }
                    if (dataTable.Select("Title like '大用户%' and ParentID='" + strparuid + "'").Length == 0)
                    {
                        dr2 = dt1.NewRow();
                        dr2["ForecastID"] = dt1.Rows[i]["ForecastID"];
                        dr2["Forecast"] = dt1.Rows[i]["Forecast"];
                        uidtemp = createID();
                        dr2["ID"] = uidtemp;
                        dr2["Col4"] = dt1.Rows[i]["Col4"];
                        dr2["ParentID"] = strparuid;
                        dr2["Sort"] = 2;
                        dr2["Title"] = "大用户电量";
                        dt1.Rows.Add(dr2);

                        dr2 = dt2.NewRow();
                        dr2["ForecastID"] = dt2.Rows[i]["ForecastID"];
                        dr2["Forecast"] = dt2.Rows[i]["Forecast"];
                        dr2["ID"] = uidtemp + "-";
                        dr2["Col4"] = dt2.Rows[i]["Col4"];
                        dr2["ParentID"] = strparuid + "-";
                        dr2["Sort"] = 2;
                        dr2["Title"] = "大用户负荷";
                        dt2.Rows.Add(dr2);

                    }
                }

            }
            DataRow[] rows = dt1.Select("ParentID=''", "sort asc");
            foreach (DataRow row in rows)
            {
                createMath(row, dt1, dt2, "");
            }
            
            LoadData();
            //大用户数值统计(如果想求和的话 新添加的)
            //jsdayh1(2001,2008);
            int m = 1;
            foreach (DataColumn dc in dt2.Columns)
            {
                m++;
                wait.SetCaption((10 + m * 90 / dt2.Columns.Count) + "%");
                if (dc.ColumnName.Substring(0, 1) != "y")
                    continue;
                cacsumsehui(dc.ColumnName);
                //cacsumsehui2(dc.ColumnName);
            }
        }

        private void createMath(DataRow row, DataTable dt1, DataTable dt2, string pid)
        {
            string id = row["ID"].ToString();
            DataRow frow = findrow(dataTable, "Col1='" + id + "'", "");//电量
            Type mathType = typeof(Ps_Forecast_Math);
            string _pid = "";
            Ps_History v = DataConverter.RowToObject<Ps_History>(row);
            if (frow == null)
            {//create

                v.ID = createID();
                v.Col1 = id;
                v.ForecastID = forecastReport.ID;
                v.Forecast = type;
                v.ParentID = pid;
                Services.BaseService.Create("InsertPs_Forecast_MathbyPs_History", v);
                _pid = v.ID;
            }
            else
            {//update
                Ps_Forecast_Math py = DataConverter.RowToObject<Ps_Forecast_Math>(frow);
                py.Title = row["Title"].ToString();
                py.Col2 = row["Col2"].ToString();
                py.ParentID = pid;
                for (int i = 2000; i <= endyear; i++)
                {
                    mathType.GetProperty("y" + i).SetValue(py, row["y" + i], null);
                }
                Services.BaseService.Update<Ps_Forecast_Math>(py);
                _pid = py.ID;
            }
           // DataRow frow2 = findrow(dataTable2, "Col1='" + id + splitstr + "'", "");//负荷
            DataRow row2 = findrow(dt2, "ID='" + id + splitstr + "'", "");
            //if (frow2 == null)
            //{
            //    if (row2 != null)
            //    {
            //        v = DataConverter.RowToObject<Ps_History>(row2);
            //        v.ID = _pid + splitstr;
            //        v.Col1 = id + splitstr;
            //        v.ForecastID = forecastReport.ID;
            //        v.Forecast = type32;
            //        v.ParentID = pid == "" ? "" : pid + splitstr;
            //        Services.BaseService.Create("InsertPs_Forecast_MathbyPs_History", v);
            //    }
            //}
            //else
            //{
            //    Ps_Forecast_Math py = DataConverter.RowToObject<Ps_Forecast_Math>(frow2);
            //    py.Title = row["Title"].ToString();
            //    py.Col2 = row["Col2"].ToString();
            //    py.ParentID = pid == "" ? "" : pid + splitstr;
            //    object value = 0;
            //    if (row2 != null)
            //    {
            //        //value = row2["y" + firstyear];
            //        //mathType.GetProperty("y" + firstyear).SetValue(py, value, null);
            //        for (int i = 2000; i <= endyear; i++)
            //        {
            //            mathType.GetProperty("y" + i).SetValue(py, row2["y" + i], null);
            //        }
            //    }
            //    Services.BaseService.Update<Ps_Forecast_Math>(py);
            //}
            DataRow[] rows = dt1.Select("ParentID='" + id + "'", "sort asc");
            foreach (DataRow r in rows)
            {
                createMath(r, dt1, dt2, _pid);
            }
            
        }

        /// <summary>
        /// 更新历史数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btGetHistory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm wait = null;
            wait = new WaitDialogForm("", "正在更新数据请稍后...");
            Ps_Forecast_Math psp_Type = new Ps_Forecast_Math();
            psp_Type.Forecast = type;
            psp_Type.ForecastID = forecastReport.ID;
            IList<Ps_Forecast_Math> listTypes = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
            for (int i = 0; i < listTypes.Count; i++)
            {
                Common.Services.BaseService.Delete<Ps_Forecast_Math>(listTypes[i]);
                wait.SetCaption(((i + 1) * 10 / (listTypes.Count * 2)) + "%");

            }
            psp_Type.Forecast = type32;
            psp_Type.ForecastID = forecastReport.ID;
            IList<Ps_Forecast_Math> listTypes32 = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByForecastIDAndForecast", psp_Type);
            for (int i = 0; i < listTypes32.Count; i++)
            {
                Common.Services.BaseService.Delete<Ps_Forecast_Math>(listTypes32[i]);
                wait.SetCaption((5 + (i + 1) * 10 / (listTypes.Count * 2)) + "%");
            }

            LoadData();
            barButtonItem5.Enabled = false;
            GetHistory(wait);
            barButtonItem5.Enabled = true;
            if (dataTable.Rows.Count > 0)
            {
                SelectDaYongHu();
            }
            wait.Close();
            MessageBox.Show("更新已完成！");
        }
        private void jschgfhanddl(int byear, int eyear)
        {
            DataRow[] rows = dataTable.Select("Title like '%常规%'");
            //DataRow[] rows1 = dataTable2.Select("Title like '%常规%'");
            string columnEnd = "y" + eyear;
            string columnBegin = "y" + byear;
            int intervalYears = eyear - byear;
            //常规电量
            foreach (DataRow row in rows)
            {
                //原先自然增长率
                object numerator = row[columnEnd];
                object denominator = row[columnBegin];
                double nl = 0;
                //if (numerator != DBNull.Value && denominator != DBNull.Value)
                //{
                //    try
                //    {
                //        nl = (double)Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);

                //        if (double.IsNaN((double)nl) || double.IsInfinity((double)nl))
                //            nl = 0;
                //    }
                //    catch
                //    {
                //        nl = 0;
                //    }
                //}
                double d1 = 0;
                try
                {
                    d1 = (double)numerator;
                }
                catch { }
                Ps_Calc pcs = new Ps_Calc();
                pcs.Forecast = type;
                pcs.ForecastID = forecastReport.ID;
                pcs.CalcID = row["ID"].ToString().Trim();
                int startyear = Convert.ToInt32(jqeyear.Replace("y", "").Trim()) + 1;
               IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByWhere", "Forecast = '" + type + "' and ForecastID ='" + forecastReport.ID + "' and  CalcID = '" + row["ID"].ToString().Trim() + "'and Value2='"+startyear.ToString()+"' order by Value2");
               int pcycbyear = eyear + 1;
               int pcyceyear = endyear;
                //如果在预测表里有预测值
                if (list1.Count>0)
                {
                    nl = ((Ps_Calc)list1[0]).Value4;
                    pcycbyear = Convert.ToInt32(((Ps_Calc)list1[0]).Value2);
                    pcyceyear = Convert.ToInt32(((Ps_Calc)list1[0]).Value3);
                }
                   //如果在预测表里没有预测值则采用自然增长率法
                else
                {
                    if (numerator != DBNull.Value && denominator != DBNull.Value)
                    {
                        try
                        {
                            nl = (double)Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);

                            if (double.IsNaN((double)nl) || double.IsInfinity((double)nl))
                                nl = 0;
                        }
                        catch
                        {
                            nl = 0;
                        }
                    }
                }
                for (int i = pcycbyear; i <= pcyceyear; i++)
                {
                    row["y" + i] = Math.Round(d1 * Math.Pow((1 + nl), i - eyear), 2);
                }
                row["y1993"] = nl * 100;
                Ps_Forecast_Math ps = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(row);
                Services.BaseService.Update<Ps_Forecast_Math>(ps);
                TreeListNode node = treeList1.FindNodeByKeyID(row["ID"]);
                //if (node != null) sumDL(node.ParentNode, firstyear, endyear);
            }
            ////常规负荷
            //foreach (DataRow row in rows1)
            //{
            //    //原先自然增长率
            //    object numerator = row[columnEnd];
            //    object denominator = row[columnBegin];
            //    double nl = 0;
            //    //if (numerator != DBNull.Value && denominator != DBNull.Value)
            //    //{
            //    //    try
            //    //    {
            //    //        nl = (double)Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);

            //    //        if (double.IsNaN((double)nl) || double.IsInfinity((double)nl))
            //    //            nl = 0;
            //    //    }
            //    //    catch
            //    //    {
            //    //        nl = 0;
            //    //    }
            //    //}
            //    double d1 = 0;
            //    try
            //    {
            //        d1 = (double)numerator;
            //    }
            //    catch { }
            //    Ps_Calc pcs = new Ps_Calc();
            //    pcs.Forecast = type;
            //    pcs.ForecastID = forecastReport.ID;
            //    pcs.CalcID = row["ID"].ToString().Trim();
            //    int startyear = Convert.ToInt32(jqeyear.Replace("y", "").Trim()) + 1;
            //    IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByWhere", "Forecast = '" + type + "' and ForecastID ='" + forecastReport.ID + "' and  CalcID = '" + row["ID"].ToString().Trim() + "'and Value2='"+startyear.ToString()+"' order by Value2");
            //    int pcycbyear = eyear + 1;
            //    int pcyceyear = endyear;
            //    //如果在预测表里有预测值
            //    if (list1.Count > 0)
            //    {
            //        nl = ((Ps_Calc)list1[0]).Value4;
            //        pcycbyear = Convert.ToInt32(((Ps_Calc)list1[0]).Value2);
            //        pcyceyear = Convert.ToInt32(((Ps_Calc)list1[0]).Value3);
            //    }
            //    //如果在预测表里没有预测值则采用自然增长率法
            //    else
            //    {
            //        if (numerator != DBNull.Value && denominator != DBNull.Value)
            //        {
            //            try
            //            {
            //                nl = (double)Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);

            //                if (double.IsNaN((double)nl) || double.IsInfinity((double)nl))
            //                    nl = 0;
            //            }
            //            catch
            //            {
            //                nl = 0;
            //            }
            //        }
            //    }
            //    for (int i = pcycbyear; i <= pcyceyear; i++)
            //    {
            //        row["y" + i] = Math.Round(d1 * Math.Pow((1 + nl), i - eyear), 2);
            //    }
            //    row["y1993"] = nl * 100;
            //    Ps_Forecast_Math ps = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(row);
            //    Services.BaseService.Update<Ps_Forecast_Math>(ps);
            //    TreeListNode node = treeList1.FindNodeByKeyID(row["ID"]);
            //    //if (node != null) sumDL(node.ParentNode, firstyear, endyear);
            //}
        }
        private void js(int byear, int eyear)
        {
            DataRow[] rows = dataTable.Select("Title like '%常规%'");

            string columnEnd = "y" + eyear;
            string columnBegin = "y" + byear;
            int intervalYears = eyear - byear;
            foreach (DataRow row in rows)
            {
                object numerator = row[columnEnd];
                object denominator = row[columnBegin];
                double nl = 0;
                if (numerator != DBNull.Value && denominator != DBNull.Value)
                {
                    try
                    {
                        nl = (double)Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);

                        if (double.IsNaN((double)nl) || double.IsInfinity((double)nl))
                            nl = 0;
                    }
                    catch
                    {
                        nl = 0;
                    }
                }
                double d1 = 0;
                try
                {
                    d1 = (double)numerator;
                }
                catch { }

                for (int i = eyear + 1; i <= endyear; i++)
                {
                    row["y" + i] = Math.Round(d1 * Math.Pow((1 + nl), i - eyear), 2);
                }
                row["y1993"] = nl * 100;
                Ps_Forecast_Math ps = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(row);
                Services.BaseService.Update<Ps_Forecast_Math>(ps);
                TreeListNode node = treeList1.FindNodeByKeyID(row["ID"]);
                if (node != null) sumDL(node.ParentNode, firstyear, endyear);
            }
        }
       
        //返回电量现有的用户
        private DataRow[] jsdlxydyh(DataRow row)
        {
            
            DataRow[] rows1 = dataTable.Select("Title like '%现有%' and ParentID='" + row["ID"].ToString() + "'");
            if (rows1.Length > 0)
            {
                DataRow[] rows2 = dataTable.Select("ParentID='" + rows1[0]["ID"].ToString() + "'");
                return rows2;
            }
            else
                return null;
            
         }
        //f返回电量新增大用户
        private DataRow[] jsdlxzdyh(DataRow row)
        {
            DataRow[] rows1 = dataTable.Select("Title like '%新增%' and ParentID='" + row["ID"].ToString() + "'");
            if (rows1.Length > 0)
            {
                DataRow[] rows2 = dataTable.Select("ParentID='" + rows1[0]["ID"].ToString() + "'");
                return rows2;
            }
            else
                return null;
        }
        //返回负荷现有的大用户
        //private DataRow[] jsxyfhdyh(DataRow row)
        //{
        //    DataRow[] rows1 = dataTable2.Select("Title like '%现有%' and ParentID='" + row["ID"].ToString() + "'");
        //    if (rows1.Length > 0)
        //    {
        //        DataRow[] rows2 = dataTable2.Select("ParentID='" + rows1[0]["ID"].ToString() + "'");
        //        return rows2;
        //    }
        //    else
        //        return null;
        //}
        //返回负荷新增大用户
        private DataRow[] jsxzfhdyh(DataRow row)
        {
            //DataRow[] rows1 = dataTable2.Select("Title like '%新增%' and ParentID='" + row["ID"].ToString() + "'");
            //if (rows1.Length > 0)
            //{
            //    DataRow[] rows2 = dataTable2.Select("ParentID='" + rows1[0]["ID"].ToString() + "'");
            //    return rows2;
            //}
            //else
               return null;
        }
        //平摊数值
        private void PtDatarow(DataRow dr,string type,string yhtype,int byear,int eyear,int lastyear,double ptlv)
        {
            Ps_History v = new Ps_History();
            v = Services.BaseService.GetOneByKey<Ps_History>(dr["Col1"].ToString());
            if (v!=null)
            {
                if (type=="电量")
                {
                    int startyear=(string.IsNullOrEmpty(v.Col11)?0:Convert.ToInt32(v.Col11));
                    int finshyear = (string.IsNullOrEmpty(v.Col12) ? 0 : Convert.ToInt32(v.Col12));
                    double value = Convert.ToDouble(dr["y1990"].ToString());
                    if (yhtype=="现有")
                    {
                        if (finshyear<=byear)
                        {
                            for (int i = byear; i <= lastyear;i++ )
                            {
                                dr["y" + i.ToString()] = value;
                            }
                        }
                        else if (finshyear>byear)
                        {
                            for (int i = finshyear; i <=lastyear;i++ )
                            {
                                dr["y" + i.ToString()] = value;
                            }
                        }
                        Ps_Forecast_Math ps = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(dr);
                        Services.BaseService.Update<Ps_Forecast_Math>(ps);
                        TreeListNode node = treeList1.FindNodeByKeyID(dr["ID"]);
                        if (node != null) sumDL(node.ParentNode, firstyear, endyear);

                    }
                    else if (yhtype=="新增")
                    {
                        int intervalYears = finshyear - startyear;
                        double basex = (double)Math.Round(value / (Math.Pow(1+ptlv,intervalYears)),2);
                        for (int i = startyear; i <= finshyear;i++ )
                        {
                            double zhi = Math.Round((Math.Pow(1 + ptlv, i-startyear)*basex), 2);
                            dr["y" + i.ToString()] = zhi;
                        }
                        for (int i = finshyear + 1; i <= lastyear;i++ )
                        {
                            dr["y" + i.ToString()] = value;
                        }
                        Ps_Forecast_Math ps = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(dr);
                        Services.BaseService.Update<Ps_Forecast_Math>(ps);
                        TreeListNode node = treeList1.FindNodeByKeyID(dr["ID"]);
                        if (node != null) sumDL(node.ParentNode, firstyear, endyear);

                    }
                }
                else if (type == "负荷")
                {
                    int startyear = (string.IsNullOrEmpty(v.Col11) ? 0 : Convert.ToInt32(v.Col11));
                    int finshyear = (string.IsNullOrEmpty(v.Col12) ? 0 : Convert.ToInt32(v.Col12));
                    double value = Convert.ToDouble(dr["y1991"].ToString());
                    if (yhtype == "现有")
                    {
                        if (finshyear <= byear)
                        {
                            for (int i = byear; i <= lastyear; i++)
                            {
                                dr["y" + i.ToString()] = value;
                            }
                        }
                        else if (finshyear > byear)
                        {
                            for (int i = finshyear; i <= lastyear; i++)
                            {
                                dr["y" + i.ToString()] = value;
                            }
                        }
                        Ps_Forecast_Math ps = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(dr);
                        Services.BaseService.Update<Ps_Forecast_Math>(ps);
                        //TreeListNode node = treeList2.FindNodeByKeyID(dr["ID"]);
                        //if (node != null) sumDL(node.ParentNode, firstyear, endyear);

                    }
                    else if (yhtype == "新增")
                    {
                        int intervalYears = finshyear - startyear;
                        double basex = (double)Math.Round(value / (Math.Pow(1 + ptlv, intervalYears)), 2);
                        for (int i = startyear; i <= finshyear; i++)
                        {
                            double zhi = Math.Round((Math.Pow(1 + ptlv, i - startyear) * basex), 2);
                            dr["y" + i.ToString()] = zhi;
                        }
                        for (int i = finshyear + 1; i <= lastyear; i++)
                        {
                            dr["y" + i.ToString()] = value;
                        }
                        Ps_Forecast_Math ps = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(dr);
                        Services.BaseService.Update<Ps_Forecast_Math>(ps);
                        //TreeListNode node = treeList2.FindNodeByKeyID(dr["ID"]);
                        //if (node != null) sumDL(node.ParentNode, firstyear, endyear);

                    }
                }
            }
        }

        private void jstjaddyear1()
        {
            //jstjflag = true;
            DataRow[] rows1 = dataTable.Select("Title like '%新增%'");
            //DataRow[] rows2 = dataTable2.Select("Title like '%新增%'");

            //获得平摊率
            //double ftlv = 0.2;
            //Ps_Calc pcs = new Ps_Calc();
            //pcs.Forecast = type;
            //pcs.ForecastID = forecastReport.ID;
            //IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
            //if (list1.Count > 0)
            //{
            //    ftlv = list1[0].Value1;
            //}
            //获取所有的大用户
            //wf.SetCaption("正在统计新增大用户电量");
            foreach (DataRow row in rows1)
            {
                DataRow[] rows = dataTable.Select("ParentID='" + row["ID"].ToString() + "'");
                if (rows != null && rows.Length != 0)
                {
                    CalaculateSumnew1(rows[rows.Length - 1], dataTable);
                    //for (int i = firstyear; i <= endyear; i++)
                    //{

                    //    string y = "y" + i.ToString();
                    //    //CalculateSum(treeList1.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList1.Columns[y]);
                    //    CalaculateSumnew(rows[rows.Length - 1], y, dataTable);

                    //}

                }
            }
           // wf.SetCaption("正在统计新增大用户负荷");
            //foreach (DataRow row in rows2)
            //{
            //    DataRow[] rows = dataTable2.Select("ParentID='" + row["ID"].ToString() + "'");
            //    if (rows != null && rows.Length != 0)
            //    {
            //        CalaculateSumnew1(rows[rows.Length - 1], dataTable2);
            //        //for (int i = firstyear; i <= endyear; i++)
            //        //{
            //        //    string y = "y" + i.ToString();
            //        //    //CalculateSum(treeList2.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList2.Columns[y]);
            //        //    CalaculateSumnew(rows[rows.Length - 1], y, dataTable2);
            //        //}


            //    }
            //}
            rows1 = dataTable.Select("Title like '%现有%'");
            //rows2 = dataTable2.Select("Title like '%现有%'");

            //获得平摊率
            //double ftlv = 0.2;
            //Ps_Calc pcs = new Ps_Calc();
            //pcs.Forecast = type;
            //pcs.ForecastID = forecastReport.ID;
            //IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
            //if (list1.Count > 0)
            //{
            //    ftlv = list1[0].Value1;
            //}
            //获取所有的大用户
           // wf.SetCaption("正在统计现有大用户电量");
            foreach (DataRow row in rows1)
            {
                DataRow[] rows = dataTable.Select("ParentID='" + row["ID"].ToString() + "'");
                if (rows != null && rows.Length != 0)
                {
                    CalaculateSumnew1(rows[rows.Length - 1], dataTable);
                    //for (int i = firstyear; i <= endyear; i++)
                    //{
                    //    string y = "y" + i.ToString();
                    //    //CalculateSum(treeList1.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList1.Columns[y]);
                    //    CalaculateSumnew(rows[rows.Length - 1], y, dataTable);
                    //}

                }
            }
           // wf.SetCaption("正在统计现有大用户负荷");
            //foreach (DataRow row in rows2)
            //{
            //    DataRow[] rows = dataTable2.Select("ParentID='" + row["ID"].ToString() + "'");
            //    if (rows != null && rows.Length != 0)
            //    {
            //        CalaculateSumnew1(rows[rows.Length - 1], dataTable2);
            //        //for (int i = firstyear; i <= endyear; i++)
            //        //{
            //        //    string y = "y" + i.ToString();
            //        //   // CalculateSum(treeList2.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList2.Columns[y]);
            //        //    CalaculateSumnew(rows[rows.Length - 1], y, dataTable2);
            //        //}


            //    }
            //}
        }
        //根据总得来统计 找到最底层的目录
        private void jstjaddyear()
        {
            DataRow[] rows1 = dataTable.Select("Title like '%新增%'");
            //DataRow[] rows2 = dataTable2.Select("Title like '%新增%'");

            //获得平摊率
            //double ftlv = 0.2;
            //Ps_Calc pcs = new Ps_Calc();
            //pcs.Forecast = type;
            //pcs.ForecastID = forecastReport.ID;
            //IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
            //if (list1.Count > 0)
            //{
            //    ftlv = list1[0].Value1;
            //}
            //获取所有的大用户
            foreach (DataRow row in rows1)
            {
                DataRow[] rows = dataTable.Select("ParentID='" + row["ID"].ToString() + "'");
                if (rows != null&&rows.Length!=0)
                {
                    for (int i = firstyear; i <= endyear; i++)
                    {

                        string y = "y" + i.ToString();
                        CalculateSum(treeList1.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList1.Columns[y]);

                    }

                }
            }

            //foreach (DataRow row in rows2)
            //{
            //    DataRow[] rows = dataTable2.Select("ParentID='" + row["ID"].ToString() + "'");
            //    if (rows != null && rows.Length != 0)
            //    {
            //        for (int i = firstyear; i <= endyear; i++)
            //        {
            //            string y = "y" + i.ToString();
            //            CalculateSum(treeList2.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList2.Columns[y]);

            //        }


            //    }
            //}
            rows1 = dataTable.Select("Title like '%现有%'");
            //rows2 = dataTable2.Select("Title like '%现有%'");

            //获得平摊率
            //double ftlv = 0.2;
            //Ps_Calc pcs = new Ps_Calc();
            //pcs.Forecast = type;
            //pcs.ForecastID = forecastReport.ID;
            //IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
            //if (list1.Count > 0)
            //{
            //    ftlv = list1[0].Value1;
            //}
            //获取所有的大用户
            foreach (DataRow row in rows1)
            {
                DataRow[] rows = dataTable.Select("ParentID='" + row["ID"].ToString() + "'");
                if (rows != null && rows.Length != 0)
                {
                    for (int i = firstyear; i <= endyear; i++)
                    {
                        string y = "y" + i.ToString();
                        CalculateSum(treeList1.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList1.Columns[y]);

                    }

                }
            }

            //foreach (DataRow row in rows2)
            //{
            //    DataRow[] rows = dataTable2.Select("ParentID='" + row["ID"].ToString() + "'");
            //    if (rows != null && rows.Length != 0)
            //    {
            //        for (int i = firstyear; i <= endyear; i++)
            //        {
            //            string y = "y" + i.ToString();
            //            CalculateSum(treeList2.FindNodeByKeyID(rows[rows.Length - 1]["ID"].ToString()), treeList2.Columns[y]);

            //        }


            //    }
            //}
        }
        //大用户在基础数据中操作
        private void jsdayh1(int byear,int eyear)
        {
            DataRow[] rows1 = dataTable.Select("Title like '%大用户%'");
            //DataRow[] rows2 = dataTable2.Select("Title like '%大用户%'");
            string columnEnd = "y" + eyear;
            string columnBegin = "y" + byear;
            //获得平摊率
            //double ftlv = 0.2;
            //Ps_Calc pcs = new Ps_Calc();
            //pcs.Forecast = type;
            //pcs.ForecastID = forecastReport.ID;
            //IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
            //if (list1.Count > 0)
            //{
            //    ftlv = list1[0].Value1;
            //}
            //获取所有的大用户
            foreach (DataRow row in rows1)
            {
                DataRow[] rows = dataTable.Select("ParentID='" + row["ID"].ToString() + "'");
                if (rows != null && rows.Length != 0)
                {
                    foreach (DataRow drw in rows)
                    {
                        Ps_Forecast_Math ps = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(drw);
                        // Services.BaseService.Update<Ps_Forecast_Math>(ps);
                        TreeListNode node = treeList1.FindNodeByKeyID(drw["ID"]);
                        if (node != null) sumDL(node.ParentNode, firstyear, endyear);
                    }
                }
            }

            //foreach (DataRow row in rows2)
            //{
            //    DataRow[] rows = dataTable2.Select("ParentID='" + row["ID"].ToString() + "'");
            //    if (rows != null && rows.Length != 0)
            //    {
            //        foreach (DataRow drw in rows)
            //        {
            //            Ps_Forecast_Math ps = Itop.Common.DataConverter.RowToObject<Ps_Forecast_Math>(drw);
            //            // Services.BaseService.Update<Ps_Forecast_Math>(ps);
            //            //TreeListNode node = treeList2.FindNodeByKeyID(drw["ID"]);
            //            //if (node != null) sumDL(node.ParentNode, firstyear, endyear);
            //        }
                   
            //    }
            //}
         
        }
       //为以前大用户平摊在负荷预测中进行的操作
        private void jsdayh(int byear, int eyear)
        {
            DataRow[] rows1 = dataTable.Select("Title like '%大用户%'");
            //DataRow[] rows2 = dataTable2.Select("Title like '%大用户%'");
            string columnEnd = "y" + eyear;
            string columnBegin = "y" + byear;
            //获得平摊率
            double ftlv=0.2;
              Ps_Calc pcs = new Ps_Calc();
            pcs.Forecast = type;
            pcs.ForecastID = forecastReport.ID;
           IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByForecast", pcs);
            if (list1.Count>0)
            {
                ftlv = list1[0].Value1;
            }
            // 平摊大用户的电量
            foreach (DataRow dr in rows1)
            {
                DataRow[] xydyhdlrows=jsdlxydyh(dr);
                DataRow[] xzdyhdlrows=jsdlxzdyh(dr);
               //现有大用户只是将其值 根据要建成的那一年来的值来确定后面的值都为此值
                if (xydyhdlrows!=null)
                {
                    foreach (DataRow drw in xydyhdlrows)
                    {
                        PtDatarow(drw, "电量", "现有", byear, eyear, endyear, ftlv);

                    }
                }
               
                //新增大用户要在建设期间进行平摊，建成以后则不变化
                if (xzdyhdlrows!=null)
                {
                    foreach (DataRow drw in xzdyhdlrows)
                    {
                        PtDatarow(drw, "电量", "新增", byear, eyear, endyear, ftlv);

                    }
                }
               
            }
            ////平摊大用户的负荷
            //foreach (DataRow dr in rows2)
            //{
            //     DataRow[] xydyhfhrows=jsxyfhdyh(dr);
            //    DataRow[] xzdyhfhrows=jsxzfhdyh(dr);
            //    //同电量
            //    if (xydyhfhrows!=null)
            //    {
            //        foreach (DataRow drw in xydyhfhrows)
            //        {
            //            PtDatarow(drw, "负荷", "现有", byear, eyear, endyear, ftlv);

            //        }
            //    }
            //    if (xzdyhfhrows!=null)
            //    {
            //        foreach (DataRow drw in xzdyhfhrows)
            //        {
            //            PtDatarow(drw, "负荷", "新增", byear, eyear, endyear, ftlv);

            //        }
            //    }
               
            //}
            //int intervalYears = eyear - byear;

            //foreach (DataRow row in rows)
            //{
            //    object numerator = row[columnEnd];
            //    object denominator = row[columnBegin];
            //    double nl = 0;
            //    if (numerator != DBNull.Value && denominator != DBNull.Value)
            //    {
            //        try
            //        {
            //            nl = (double)Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);

            //            if (double.IsNaN((double)nl) || double.IsInfinity((double)nl))
            //                nl = 0;
            //        }
            //        catch
            //        {
            //            nl = 0;
            //        }
            //    }
            //    double d1 = 0;
            //    try
            //    {
            //        d1 = (double)numerator;
            //    }
            //    catch { }

            //    for (int i = eyear + 1; i <= endyear; i++)
            //    {
            //        row["y" + i] = Math.Round(d1 * Math.Pow((1 + nl), i - eyear), 2);
            //    }
            //    row["y1993"] = nl * 100;

            //////////////////////////////////////////////////////////////////////////
               
       
        }
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm wait = null;
            wait = new WaitDialogForm("", "正在计算数据请稍后...");
            wait.Show();
            int byear = 2001;
            int eyear = 2008;
            Ps_Forecast_Setup pfs = new Ps_Forecast_Setup();
            pfs.Forecast = type;
            pfs.ForecastID = forecastReport.ID;
            //pfs.StartYear = int.Parse(jqbyear.Replace("y", ""));
            //pfs.EndYear = int.Parse(jqeyear.Replace("y", ""));

            IList<Ps_Forecast_Setup> li = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);

            if (li.Count == 0)
            {
                Ps_YearRange py = new Ps_YearRange();
                py.Col4 = "区县发展实绩";
                py.Col5 = Itop.Client.MIS.ProgUID;

                IList<Ps_YearRange> li1 = Services.BaseService.GetList<Ps_YearRange>("SelectPs_YearRangeByCol5andCol4", py);

                if (li1.Count > 0)
                {
                    byear = li1[0].StartYear;
                    eyear = li1[0].FinishYear;
                }
            }
            //Services.BaseService.Create<Ps_Forecast_Setup>(pfs);
            else
            {
                byear = li[0].StartYear;
                eyear = li[0].EndYear;
            }
                //Services.BaseService.Update("UpdatePs_Forecast_SetupByForecast", pfs);
          
            treeList1.BeginInit();
            //treeList2.BeginInit();
            //js(byear, eyear);
            if (eyear<firstyear||byear>endyear)
            {
                MessageBox.Show("设定的历史年份有问题！请重新设定");
                return;
            }
            wait.SetCaption("计算常规用户");
            jschgfhanddl(byear, eyear);
            wait.SetCaption("计算大用户");
            jstjaddyear1();
           // LoadData();
            //jsdayh1(byear, eyear);
            //jsdayh(byear, eyear);
            wait.SetCaption("统计常规以及大用户值");
            //使统计量为子类的常规以及大用户值的和
            int m = 1;
            foreach (DataColumn dc in dataTable.Columns)
            {
               
                if (dc.ColumnName.Substring(0, 1) != "y")
                    continue;
                cacsumsehui(dc.ColumnName);
                //cacsumsehui2(dc.ColumnName);
                m++;
                wait.SetCaption((10 + m * 90 / dataTable.Columns.Count) + "%");
             
            }
            //对全社会进行统计
            Calqushehui1();
            Calqushehui2();
            //treeList2.EndInit();
            treeList1.EndInit();
            wait.Close();
            MessageBox.Show("计算成功！");
            //LoadData();
        }
        void copyrow(DataTable dataTable, string filter, ref DataTable DesdataTable)
        {
            DataRow[] rows = dataTable.Select(filter);
            foreach (DataRow row in rows)
            {
                DataRow dt = DesdataTable.NewRow();
                foreach (DataColumn dc in DesdataTable.Columns)
                {
                    dt[dc.ColumnName] = row[dc.ColumnName];

                }
                DesdataTable.Rows.Add(dt);
            }
        }
        void copyrow(DataTable dataTable, string filter, ref DataTable DesdataTable, string destitle, string addstr)
        {
            DataRow[] rows = dataTable.Select(filter);
            foreach (DataRow row in rows)
            {
                DataRow dt = DesdataTable.NewRow();
                foreach (DataColumn dc in DesdataTable.Columns)
                {
                    dt[dc.ColumnName] = row[dc.ColumnName];
                    if (dc.ColumnName == destitle)
                    {
                        dt[dc.ColumnName] = dt[dc.ColumnName].ToString() + addstr;
                    }
                }
                DesdataTable.Rows.Add(dt);
            }
        }
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string parenrID1 = "";
            string parenrID2 = "";
            chartdataTable.Rows.Clear();
            //copyrow(dataTable, "ParentID=''", ref chartdataTable, "Title", "（电量）");
            //if (chartdataTable.Rows.Count > 0 && chartdataTable.Columns.Count>0)
            //{
            //   // parenrID1 = chartdataTable.Rows[0]["ID"].ToString();
            //    parenrID1 = chartdataTable.Rows[0][0].ToString();
            //    copyrow(dataTable, "ParentID='" + parenrID1 + "'", ref  chartdataTable,"Title","（电量）");
            //}
            //int ilen = chartdataTable.Rows.Count;

            //copyrow(dataTable2, "ParentID=''", ref chartdataTable, "Title", "（负荷）");
            //if (chartdataTable.Rows.Count > ilen && chartdataTable.Columns.Count > 0)
            //{
            //   // parenrID2 = chartdataTable.Rows[ilen]["ID"].ToString();
            //    parenrID2 = chartdataTable.Rows[ilen][0].ToString();
            //    copyrow(dataTable2, "ParentID='" + parenrID2 + "'", ref  chartdataTable, "Title", "（负荷）");
            //}
            //DataRow row = (treeList1.Nodes[0].TreeList.GetDataRecordByNode(treeList1.Nodes[0]) as DataRowView).Row;

            //DataRow dt = chartdataTable.NewRow();
            //foreach (DataColumn dc in chartdataTable.Columns)
            //{
            //    dt[dc.ColumnName] = row[dc.ColumnName];
            //    if (dc.ColumnName == "Title")
            //    {
            //        dt[dc.ColumnName] = dt[dc.ColumnName].ToString() + "（电量）";
            //    }
            //}
            //dt["ParentID"] = 2;
            //chartdataTable.Rows.Add(dt);
            //DataRow row2 = (treeList1.Nodes[1].TreeList.GetDataRecordByNode(treeList1.Nodes[1]) as DataRowView).Row;

            //dt = chartdataTable.NewRow();
            //foreach (DataColumn dc in chartdataTable.Columns)
            //{
            //    dt[dc.ColumnName] = row2[dc.ColumnName];
            //    if (dc.ColumnName == "Title")
            //    {
            //        dt[dc.ColumnName] = dt[dc.ColumnName].ToString() + "（电量）";
            //    }
            //}
            //dt["ParentID"] = 2;
            //chartdataTable.Rows.Add(dt);
            //foreach (TreeListNode node in treeList1.Nodes[1].Nodes)
            //{
            //    if (node.HasChildren)
            //    {
            //         row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;

            //         dt = chartdataTable.NewRow();
            //        foreach (DataColumn dc in chartdataTable.Columns)
            //        {
            //            dt[dc.ColumnName] = row[dc.ColumnName];
            //            if (dc.ColumnName == "Title")
            //            {
            //                dt[dc.ColumnName] = dt[dc.ColumnName].ToString() + "（电量）";
            //            }
            //        }
            //        dt["ParentID"] = 2;
            //        chartdataTable.Rows.Add(dt);
            //    }

            //}


            // row = (treeList2.Nodes[0].TreeList.GetDataRecordByNode(treeList2.Nodes[0]) as DataRowView).Row;

            // dt = chartdataTable.NewRow();
            //foreach (DataColumn dc in chartdataTable.Columns)
            //{
            //    dt[dc.ColumnName] = row[dc.ColumnName];
            //    if (dc.ColumnName == "Title")
            //    {
            //        dt[dc.ColumnName] = dt[dc.ColumnName].ToString() + "（负荷）";
            //    }
            //}
            //dt["ParentID"] = 3;
            //chartdataTable.Rows.Add(dt);
            // row2 = (treeList2.Nodes[1].TreeList.GetDataRecordByNode(treeList2.Nodes[1]) as DataRowView).Row;

            //dt = chartdataTable.NewRow();
            //foreach (DataColumn dc in chartdataTable.Columns)
            //{
            //    dt[dc.ColumnName] = row2[dc.ColumnName];
            //    if (dc.ColumnName == "Title")
            //    {
            //        dt[dc.ColumnName] = dt[dc.ColumnName].ToString() + "（负荷）";
            //    }
            //}
            //dt["ParentID"] = 3;
            //chartdataTable.Rows.Add(dt);
            //foreach (TreeListNode node in treeList2.Nodes[1].Nodes)
            //{
            //    if (node.HasChildren)
            //    {
            //        row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;

            //        dt = chartdataTable.NewRow();
            //        foreach (DataColumn dc in chartdataTable.Columns)
            //        {
            //            dt[dc.ColumnName] = row[dc.ColumnName];
            //            if (dc.ColumnName == "Title")
            //            {
            //                dt[dc.ColumnName] = dt[dc.ColumnName].ToString() + "（负荷）";
            //            }
            //        }
            //        dt["ParentID"] =3;
            //        chartdataTable.Rows.Add(dt);
            //    }

            //}
            DataRow row;

            DataRow dt;
            foreach (TreeListNode nd in treeList1.Nodes)
            {
                row = (nd.TreeList.GetDataRecordByNode(nd) as DataRowView).Row;
                dt = chartdataTable.NewRow();
                foreach (DataColumn dc in chartdataTable.Columns)
                {
                    dt[dc.ColumnName] = row[dc.ColumnName];
                    if (dc.ColumnName == "Title")
                    {
                        dt[dc.ColumnName] = dt[dc.ColumnName].ToString() + "（负荷）";
                    }
                }
                dt["ParentID"] = 3;
                chartdataTable.Rows.Add(dt);
                foreach (TreeListNode nd2 in nd.Nodes)
                {
                    if (nd2.HasChildren)
                    {
                        row = (nd2.TreeList.GetDataRecordByNode(nd2) as DataRowView).Row;

                        dt = chartdataTable.NewRow();
                        foreach (DataColumn dc in chartdataTable.Columns)
                        {
                            dt[dc.ColumnName] = row[dc.ColumnName];
                            if (dc.ColumnName == "Title")
                            {
                                dt[dc.ColumnName] = dt[dc.ColumnName].ToString() + "（负荷）";
                            }
                        }
                        dt["ParentID"] = 3;
                        chartdataTable.Rows.Add(dt);


                    }

                }

            }
            //foreach (TreeListNode nd in treeList2.Nodes)
            //{
            //    row = (nd.TreeList.GetDataRecordByNode(nd) as DataRowView).Row;
            //    dt = chartdataTable.NewRow();
            //    foreach (DataColumn dc in chartdataTable.Columns)
            //    {
            //        dt[dc.ColumnName] = row[dc.ColumnName];
            //        if (dc.ColumnName == "Title")
            //        {
            //            dt[dc.ColumnName] = dt[dc.ColumnName].ToString() + "（电量）";
            //        }
            //    }
            //    dt["ParentID"] = 2;
            //    chartdataTable.Rows.Add(dt);
            //    foreach (TreeListNode nd2 in nd.Nodes)
            //    {
            //        if (nd2.HasChildren)
            //        {
            //            row = (nd2.TreeList.GetDataRecordByNode(nd2) as DataRowView).Row;

            //            dt = chartdataTable.NewRow();
            //            foreach (DataColumn dc in chartdataTable.Columns)
            //            {
            //                dt[dc.ColumnName] = row[dc.ColumnName];
            //                if (dc.ColumnName == "Title")
            //                {
            //                    dt[dc.ColumnName] = dt[dc.ColumnName].ToString() + "（电量）";
            //                }
            //            }
            //            dt["ParentID"] = 2;
            //            chartdataTable.Rows.Add(dt);


            //        }

            //    }

            //}
            //            copyrow(dataTable2, "ParentID='" + parenrID2 + "' or ID='" + parenrID2 + "'", ref  chartdataTable);
            FormForecastTX frm = new FormForecastTX();
            frm.Text = this.Text + "- " + "查看图形";
            frm.dt = chartdataTable;
            frm.PFL = forecastReport;
            frm.ParenrID1 = parenrID1;
            frm.ParenrID2 = parenrID2;
            frm.ShowDialog();
        }
        /// <summary>
        /// 查找对应电量预测
        /// </summary>
        private void SelectDaYongHu()
        {
            //"UserID='" + Itop.Client.MIS.ProgUID + "' and Col1='2' and Title='" + forecastReport.Title + "'  and StartYear='" + forecastReport.StartYear + "'" + "'  and EndYear='" + forecastReport.EndYear + "'"
            IList<Ps_forecast_list> listReports = Common.Services.BaseService.GetList<Ps_forecast_list>("SelectPs_forecast_listByWhere", "UserID='" + Itop.Client.MIS.ProgUID + "' and Col1='1' and Title='" + forecastReport.Title + "'");
            if (listReports.Count < 1)
            {
                barButtonItem8.Caption = "无对应电量预测方案";
                barButtonItem8.Enabled = false;


            }
            else if (listReports.Count == 1)
            {
                object obj = Common.Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere",
                            "ForecastID='" + listReports[0].ID + "' and Forecast='11'");
                if (obj == null)
                {

                    barButtonItem8.Caption = "对应电量预测方案无数据";
                    barButtonItem8.Enabled = false;
                }
                else
                {
                    barButtonItem8.Enabled = true;
                    barButtonItem8.Caption = "获取对应预测值";
                }

            }
            else
            {

                barButtonItem8.Caption = "有多个同名电量预测方案";
                barButtonItem8.Enabled = false;

            }
            if (dataTable.Rows.Count < 1)
                barButtonItem8.Enabled = false;
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
                          "Forecast='" + psp_TypePan.Forecast + "' and ForecastID='" + psp_TypePan.ForecastID + "' and ParentID='" + psp_TypePan.ID + "'" + strtemp + " order by sort desc");
            Ps_Forecast_Math matcgui;
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
                        value2 = value2 * Math.Round(Convert.ToDouble(mat.GetType().GetProperty("y" + i.ToString()).GetValue(mat, null)), 2);
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
                        value2 = Math.Round(Convert.ToDouble(mattsl.GetType().GetProperty("y" + i.ToString()).GetValue(mattsl, null)), 2);
                        if (psp_TypePan.Forecast == 3)
                        {
                            if (value2 != 0)
                            {
                                value2 = Math.Round(Convert.ToDouble(psp_TypePan.GetType().GetProperty("y" + i.ToString()).GetValue(psp_TypePan, null)), 2) / value2;

                                value = value2 - Math.Round(Convert.ToDouble(matdyh.GetType().GetProperty("y" + i.ToString()).GetValue(matdyh, null)), 2);
                                matcgui.GetType().GetProperty("y" + i.ToString()).SetValue(matcgui, value, null);
                                Common.Services.BaseService.Update<Ps_Forecast_Math>(matcgui);
                            }

                        }
                        else
                            if (psp_TypePan.Forecast == 2)
                            {
                                value2 = Math.Round(Convert.ToDouble(psp_TypePan.GetType().GetProperty("y" + i.ToString()).GetValue(psp_TypePan, null)), 2);
                                value = value2 - Math.Round(Convert.ToDouble(matdyh.GetType().GetProperty("y" + i.ToString()).GetValue(matdyh, null)), 2);
                                matcgui.GetType().GetProperty("y" + i.ToString()).SetValue(matcgui, value, null);
                                Common.Services.BaseService.Update<Ps_Forecast_Math>(matcgui);

                            }

                    }


                }

        }
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<Ps_forecast_list> listReports = Common.Services.BaseService.GetList<Ps_forecast_list>("SelectPs_forecast_listByWhere", "UserID='" + Itop.Client.MIS.ProgUID + "' and Col1='1' and Title='" + forecastReport.Title + "'");
            if (listReports.Count > 0)
            {
                bool iserror = false;
                WaitDialogForm wait = new WaitDialogForm("", "正在更新数据, 请稍候...");
                try
                {

                    IList<Ps_Forecast_Math> list = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere",
                              "ForecastID='" + listReports[0].ID + "' and Forecast='11'");
                    int typetemp = 0;
                    foreach (Ps_Forecast_Math pf in list)
                    {
                        string id = pf.ID;
                        Ps_Forecast_Math psp_TypePan = new Ps_Forecast_Math();
                        string[] str = new string[2];
                        if (pf.Title.Contains("电量"))
                        {
                            typetemp = 2;
                            str[1] = pf.Title;
                            if (pf.Title.Substring(pf.Title.Length - 2, 2) == "电量")
                                str[1] = pf.Title.Substring(0, pf.Title.Length - 2);
                        }
                        else if (pf.Title.Contains("负荷"))
                        {
                            typetemp = 3;
                            if (pf.Title.Substring(pf.Title.Length - 2, 2) == "负荷")
                                str[1] = pf.Title.Substring(0, pf.Title.Length - 2);
                        }
                        if (str[1] == null)
                        {
                            continue;
                        }

                        if (str[1].Contains("全社会") && !str[1].Contains("(全地区)"))
                        {
                            str[1] = str[1] + "(全地区)";
                        }
                        //psp_Type.Forecast = 2;
                        //psp_Type.ForecastID = listReports[0].ID;
                        //psp_Type.Title=str[1];
                        object obj = Common.Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere",
                            "Forecast='" + typetemp + "' and ForecastID='" + forecastReport.ID + "' and Title='" + str[1] + "'");
                        if (obj == null)
                            continue;
                        Ps_Forecast_Math psp_Type = obj as Ps_Forecast_Math;
                        psp_TypePan = obj as Ps_Forecast_Math;


                        Ps_Forecast_Math v = pf;
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




                        IList<Ps_Forecast_Math> list2 = Common.Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere",
                       "Forecast='" + typetemp + "' and ForecastID='" + forecastReport.ID + "' and ParentID='" + psp_Type.ID + "'" + " and ( Title like '常规%' or Title  like '大用户%' or Title  like '同时率%'   ) order by sort");
                        if (list.Count == 0)
                            continue;
                        Ps_Forecast_Math pfscgui = new Ps_Forecast_Math();
                        Ps_Calc pcs = new Ps_Calc();
                        pcs.Forecast = 11;
                        pcs.ForecastID = listReports[0].ID;
                        pcs.CalcID = id;
                        IList<Ps_Calc> list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByWhere", "Forecast = '11' and ForecastID ='" + listReports[0].ID + "' and  CalcID = '" + id + "' order by Value2");
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
                                foreach (Ps_Forecast_Math ms in list2)
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
                                        foreach (Ps_Forecast_Math ms in list2)
                                        {
                                            // value1 = (double)dataRow["y" + (i )]; 

                                            // value1 = (double)dataRow["y" + (i )]; 
                                            if (ms.Title.Contains("常规"))
                                            {
                                                value1 = Math.Round(Convert.ToDouble(ms.GetType().GetProperty("y" + (i - 1)).GetValue(ms, null)), 2);
                                                pfscgui = ms;
                                            }

                                            else
                                                if (ms.Title.Contains("大用户"))
                                                    value2 = Math.Round(Convert.ToDouble(ms.GetType().GetProperty("y" + i).GetValue(ms, null)), 2);

                                                else
                                                    if (ms.Title.Contains("同时率"))
                                                        value3 = Math.Round(Convert.ToDouble(ms.GetType().GetProperty("y" + i).GetValue(ms, null)), 2);
                                        }

                                    }
                                    catch { }
                                    value1 = Math.Round(value1 * (1 + zzl * 0.01), 2);
                                    pfscgui.GetType().GetProperty("y" + i).SetValue(pfscgui, value1, null);
                                    Common.Services.BaseService.Update<Ps_Forecast_Math>(pfscgui);
                                }
                            }


                        }

                        if (!v.Title.Contains("全社会"))
                        {
                            v = Common.Services.BaseService.GetOneByKey<Ps_Forecast_Math>(v.ParentID);


                            updateAllPan(v, forecastReport);
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
                        LoadData();
                    }
                }
                catch
                {

                    iserror = true;

                }
                wait.Close();
                if (!iserror)
                    MessageBox.Show("更新成功！");
                else
                    MessageBox.Show("更新失败！");

            }
        }
        bool selectdral = true;
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.Nodes.Count == 0)
            {
                MessageBox.Show("无数据，不能操作！");
                return;
            }
            if (barButtonItem9.Caption == "开始截取历史数据")
            {
                barButtonItem9.Caption = "结束截取历史数据";
                jqbyear = "0";
                jqeyear = "0";
                selectdral = false;
               


                treeList1.OptionsSelection.MultiSelect = true;
                treeList1.OptionsBehavior.Editable = false;
                treeList1.Refresh();
                //treeList2.OptionsSelection.MultiSelect = true;
                //treeList2.OptionsBehavior.Editable = false;
                //treeList2.Refresh();
            }
            else if (barButtonItem9.Caption == "结束截取历史数据")
            {
                barButtonItem9.Caption = "开始截取历史数据";
                selectdral = true;

                if (jqbyear != "Title")
                {
                    Ps_Forecast_Setup pfs = new Ps_Forecast_Setup();
                    pfs.ID = Guid.NewGuid().ToString();
                    pfs.Forecast = type;
                    pfs.ForecastID = forecastReport.ID;
                    pfs.StartYear = int.Parse(jqbyear.Replace("y", ""));
                    pfs.EndYear = int.Parse(jqeyear.Replace("y", ""));

                    IList<Ps_Forecast_Setup> li = Services.BaseService.GetList<Ps_Forecast_Setup>("SelectPs_Forecast_SetupByForecast", pfs);

                    if (li.Count == 0)
                        Services.BaseService.Create<Ps_Forecast_Setup>(pfs);
                    else
                        Services.BaseService.Update("UpdatePs_Forecast_SetupByForecast", pfs);
                }

                treeList1.OptionsSelection.MultiSelect = false;
                treeList1.OptionsBehavior.Editable = true;
                //treeList2.OptionsSelection.MultiSelect = false;
                //treeList2.OptionsBehavior.Editable = true;
            }
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormDyhptl fd = new FormDyhptl();
            //fd.PForecastReports = forecastReport;
            //fd.ISEdit = EditRight;
            //if (fd.ShowDialog()!=DialogResult.OK)
            //{
            //    return;
            //}
            CS();


        }
        DataTable yctbs = new DataTable();
        private void CS()
        {
            if (jqbyear == "0" || jqeyear == "0")
            {
                MsgBox.Show("请设置历史数据起始年结束年后再点参数设置");
                return;
            }

            yctbs = dataTable.Clone();
            DataRow[] rows1 = dataTable.Select("Title like '%常规%'");
            //DataRow[] rows2 = dataTable2.Select("Title like '%常规%'");
            foreach (DataRow dr in rows1)
            {
                Ps_Forecast_Math ps=new Ps_Forecast_Math();
                ps.ID=dr["ParentID"].ToString();
                ps=Services.BaseService.GetOneByKey<Ps_Forecast_Math>(ps);
                DataRow ndr = yctbs.NewRow();
                ndr.ItemArray = dr.ItemArray.Clone() as object[];
                //DataRow ndr = dr;
                ndr["Title"] = ps.Title + dr["Title"].ToString();
                yctbs.Rows.Add(ndr);
            }
            //foreach (DataRow dr in rows2)
            //{
            //    Ps_Forecast_Math ps=new Ps_Forecast_Math();
            //    ps.ID=dr["ParentID"].ToString();
            //    ps=Services.BaseService.GetOneByKey<Ps_Forecast_Math>(ps);
            //    DataRow ndr = yctbs.NewRow();
            //    ndr.ItemArray = dr.ItemArray.Clone() as object[];
            //    //DataRow ndr = dr;
            //    ndr["Title"] = ps.Title + dr["Title"].ToString();
            //    yctbs.Rows.Add(ndr);
            //}

            // FormForecastCalc1 fc = new FormForecastCalc1();
            FormForecastCalc1ThirdSH fc = new FormForecastCalc1ThirdSH();
            //fc.Text = this.Text + "- 参数设置";
            fc.Text = "常规用户参数设置";
            fc.DTable = yctbs;
            fc.ISEdit = EditRight;
            fc.PForecastReports = forecastReport;
            fc.Firstyear = Convert.ToInt32(jqbyear.Replace("y", ""));
            fc.Endyear = Convert.ToInt32(jqeyear.Replace("y", ""));
            fc.Type = type;
            if (fc.ShowDialog() != DialogResult.OK)
                return;
            //  JS();
            JS2();
        }
        private void JS2()
        {
            if (jqbyear == "0" || jqeyear == "0")
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

            foreach (DataRow dataRow in yctbs.Rows)
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
                int startyear = Convert.ToInt32(jqeyear.Replace("y", "").Trim()) + 1;
                pcs.Value2 =Convert.ToDouble(jqeyear.Replace("y", "").Trim())+1 ;

                list1 = Services.BaseService.GetList<Ps_Calc>("SelectPs_CalcByWhere", "Forecast = '" + type + "' and ForecastID ='" + forecastReport.ID + "' and  CalcID = '" + dataRow["ID"].ToString().Trim() + "'and Value2='"+startyear.ToString()+"' order by Value2");

                foreach (Ps_Calc pc11 in list1)
                {


                    bl = true;

                    zzl = pc11.Value4;
                    syear = Convert.ToInt32(pc11.Value2);
                    eyear = Convert.ToInt32(pc11.Value3);
     

                }
                if (!bl)
                {
                    if (jqbyear != "0" && jqeyear != "0")
                    {
                        double value2 = 0;
                        syear = Convert.ToInt32(jqeyear.Replace("y", ""));
                        eyear = Convert.ToInt32(forecastReport.EndYear) - 1;
                        double[] historyValues = GenerateHistoryValue(dataRow, Convert.ToInt32(jqbyear.Replace("y", "")), syear);
                        zzl = Math.Round(Calculator.AverageIncreasing(historyValues) * 100, 2);
                        Ps_Calc pcs1 = new Ps_Calc();
                        pcs1.ID = Guid.NewGuid().ToString();
                        pcs1.Forecast = type;
                        pcs1.ForecastID = forecastReport.ID;
                        pcs1.CalcID = dataRow["ID"].ToString();
                        pcs1.Col1 = dataRow["Title"].ToString();
                        pcs1.Value2 = Convert.ToInt32(jqeyear.Replace("y", "")) + 1;
                        pcs1.Value3 = Convert.ToInt32(forecastReport.YcEndYear);
                        pcs1.Value4 = zzl;


                        Services.BaseService.Create<Ps_Calc>(pcs1);
               
                    }

                }
             
            }
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

        private void treeList1_MouseUp(object sender, MouseEventArgs e)
        {
            if (!selectdral)
            {
                if (jqbyear == "0")
                {
                    if (treeList1.FocusedColumn.FieldName.Contains("y"))
                    {
                        jqbyear = treeList1.FocusedColumn.FieldName;
                    }
                }
                else
                {
                    if (treeList1.FocusedColumn.FieldName.Contains("y"))
                    {
                        jqeyear= treeList1.FocusedColumn.FieldName;
                    }

                    if (Convert.ToInt32(jqbyear.Replace("y", "")) > Convert.ToInt32(jqeyear.Replace("y", "")))
                    {
                        string itemp = jqbyear;
                        jqbyear = jqeyear;
                        jqeyear = jqbyear;

                    }
                }
                treeList1.Refresh();
                //treeList2.Refresh();
            }
        }

        private void treeList1_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Column.FieldName.IndexOf("y") > -1 && jqbyear != "Title" && jqeyear != "Title")
            {
                if (Convert.ToInt32(e.Column.FieldName.Replace("y", "")) >= Convert.ToInt32(jqbyear.Replace("y", "")) && Convert.ToInt32(jqeyear.Replace("y", "")) >= Convert.ToInt32(e.Column.FieldName.Replace("y", "")))

                    e.Appearance.BackColor = Color.FromArgb(152, 122, 254);
                if (commonhelp.HasValue(e.Node["ID"].ToString(), e.Column.FieldName))
                {
                    e.Appearance.ForeColor = Color.Salmon;
                }
            }
        }

        private void treeList2_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            if (e.Column.FieldName.IndexOf("y") > -1 && jqbyear != "Title" && jqeyear != "Title")
            {
                if (Convert.ToInt32(e.Column.FieldName.Replace("y", "")) >= Convert.ToInt32(jqbyear.Replace("y", "")) && Convert.ToInt32(jqeyear.Replace("y", "")) >= Convert.ToInt32(e.Column.FieldName.Replace("y", "")))

                    e.Appearance.BackColor = Color.FromArgb(152, 122, 254);
                if (commonhelp.HasValue(e.Node["ID"].ToString(), e.Column.FieldName))
                {
                    e.Appearance.ForeColor = Color.Salmon;
                }
            }
        }


    }
}