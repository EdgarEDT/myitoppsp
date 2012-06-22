using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Itop.Domain.GM;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using System.Collections;
using DevExpress.XtraTreeList;
using Itop.Client.Common;
using Itop.Domain.Forecast;
using Itop.Domain.HistoryValue;
using Itop.Client.Base;
namespace Itop.Client.Forecast
{
    public partial class FormForecastLoadData2 : FormBase
    {
        //用于控制新旧版本数据选取，1为旧版本，主要是分区供电实绩数据旧版采用三个表存放
        //2为新版本，四个数据源存放在同一个表里
        //旧版
        //private string Flag = "1";
        ////新版

        public bool maxhour = false;
        public bool ISGDP = false;
      private string Flag = "2";

        DataTable dataTable;

        private string lastEditValue = string.Empty;

        private int typeFlag2 = 1;


        Hashtable hs = new Hashtable();

        public Hashtable HS
        {
            set { hs = value; }
            get { return hs; }
        
        }

        string pid = "";
        public string PID
        {
            set { pid = value; }
            get { return pid; }

        }
        string selectid = "";
        public string Selectid
        {
            set { selectid = value; }
            get { return selectid; }

        }
        int startyear = 0;

        public int StartYear
        {
            set { startyear = value; }
            get { return startyear; }

        }


        IList<Ps_History> li = new List<Ps_History>();

        public IList<Ps_History> LI
        {
            set { li = value; }
            get { return li; }

        }

        int endyear = 0;

        public int EndYear
        {
            set { endyear = value; }
            get { return endyear; }

        }

        public FormForecastLoadData2()
        {
            InitializeComponent();
        }
        //添加固定列
        private void AddFixColumn()
        {
            // treeList1.Columns.Add(year + "年", typeof(double));

            DevExpress.XtraTreeList.Columns.TreeListColumn column = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "分类名";
            column.VisibleIndex = 0;
            column.Width = 180;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            column});
            column = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            column.FieldName = "Sort";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            column});
            column = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            column.FieldName = "ForecastID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            column});

            column = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            column.FieldName = "Forecast";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            column});

            column = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            column});

            column = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            column.FieldName = "ParentID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            column});
        }
        //添加年份后，新增一列
        private void AddColumn(int year)
        {
            // treeList1.Columns.Add(year + "年", typeof(double));
            //    TreeListColumn column = treeList1.Columns.Add();
            DevExpress.XtraTreeList.Columns.TreeListColumn column = new DevExpress.XtraTreeList.Columns.TreeListColumn();



            column.FieldName = "y" + year;
            column.Tag = year;
            column.Caption = year + "年";
            column.Name = year.ToString();
            column.Width = 100;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = year;//有两列隐藏列
         //   column.ColumnEdit = repositoryItemTextEdit1;
            this.treeList1.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            column});

        }
        private void Form1_F_Load(object sender, EventArgs e)
        {
            DataTable dts = new DataTable();
            dts.Columns.Add("ID", typeof(int));
            dts.Columns.Add("Name", typeof(string));

            DataRow dr = dts.NewRow();
            dr["ID"] = 5;
            dr["Name"] = "经济人口数据";
            dts.Rows.Add(dr);
            if (!ISGDP)
            {
                dr = dts.NewRow();
                dr["Name"] = "电量数据";
                dr["ID"] = 6;
                dts.Rows.Add(dr);

                dr = dts.NewRow();
                dr["Name"] = "负荷数据";
                dr["ID"] = 7;
                dts.Rows.Add(dr);

                dr = dts.NewRow();
                dr["Name"] = "分区域电力数据";
                dr["ID"] = 4;
                dts.Rows.Add(dr);

                if (!maxhour)
                {
                    dr = dts.NewRow();
                    dr["Name"] = "分区县用电情况电量";
                    dr["ID"] = 2;
                    dts.Rows.Add(dr);
                }


                dr = dts.NewRow();
                dr["Name"] = "分区县用电情况负荷";
                dr["ID"] = 3;
                dts.Rows.Add(dr);
            }
       

            gridControl1.DataSource = dts;

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            if (dr == null)
                return;

            int id1 = (int)dr["ID"];
            Selectid = id1.ToString();
            typeFlag2 = id1;
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();

            if (Flag=="1")
            {
                if (id1 == 4)
                {
                    LoadData2();
                }
                else
                {
                    LoadData1(id1);
                }
            }
            else if (Flag=="2")
            {
                LoadData1(id1);
            }
            
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;

        }
        private int GetInsertIndex(int year)
        {
            int nFixedColumns = typeof(PSP_Types).GetProperties().Length - 2;//ID和ParentID列不在treeList1.Columns中

            int nColumns = treeList1.Columns.Count;
            int nIndex = nFixedColumns;

            //还没有年份

            if (nColumns == nFixedColumns)
            {
            }
            else//已经有年份
            {
                bool bFind = false;

                //查找此年的位置

                for (int i = nFixedColumns + 1; i < nColumns; i++)
                {
                    //Tag存放年份
                    int tagYear1 = (int)treeList1.Columns[i - 1].Tag;
                    int tagYear2 = (int)treeList1.Columns[i].Tag;
                    if (tagYear1 < year && tagYear2 > year)
                    {
                        nIndex = i;
                        bFind = true;
                        break;
                    }
                }

                //不在最大和最小之间

                if (!bFind)
                {
                    int tagYear1 = (int)treeList1.Columns[nFixedColumns].Tag;
                    int tagYear2 = (int)treeList1.Columns[nColumns - 1].Tag;

                    if (tagYear1 > year)//比第一个年份小
                    {
                        nIndex = nFixedColumns;
                    }
                    else
                    {
                        nIndex = nColumns;
                    }
                }
            }

            return nIndex;
        }
        private void AddColumn2(int year)
        {
            int nInsertIndex = GetInsertIndex(year);

            dataTable.Columns.Add("y" + year, typeof(double));

            TreeListColumn column = treeList1.Columns.Insert(nInsertIndex);
            column.FieldName ="y"+ year;
            column.Tag = year;
            column.Caption = year + "年";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = nInsertIndex - 2;//有两列隐藏列
            //column.ColumnEdit = repositoryItemTextEdit1;
            //treeList1.RefreshDataSource();
        }
        private void LoadData2()
        {
            //显示旧版分区县供电实绩（三个表合一）
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }

            PSP_Types psp_Type = new PSP_Types();
            psp_Type.Flag2 = 2;
            psp_Type.ProjectID = Itop.Client.MIS.ProgUID;
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByFlag2AndProjectID", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Types));

            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "分类名";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["ProjectID"].VisibleIndex = -1;
            treeList1.Columns["ProjectID"].OptionsColumn.ShowInCustomizationForm = false;

            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;

            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;
            //treeList1.Columns["ParentID"].VisibleIndex = -1;
            //treeList1.Columns["ParentID"].OptionsColumn.ShowInCustomizationForm = false;
            PSP_Years psp_Year = new PSP_Years();
            psp_Year.Flag = 2;
            IList<PSP_Years> listYears = Common.Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlag", psp_Year);

            foreach (PSP_Years item in listYears)
            {
                AddColumn2(item.Year);
            }
            Application.DoEvents();
            LoadValues();
            treeList1.ExpandAll();
        }
        //读取数据（旧分区县供电实绩）
        private void LoadValues()
        {
            PSP_Values psp_Values = new PSP_Values();
            psp_Values.ID = 2;//用ID字段存放Flag2的值


            IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesListByFlag2", psp_Values);

            foreach (PSP_Values value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null)
                {

                    node.SetValue("y" + value.Year, value.Value);
                }
            }
        }


        private void LoadData1(int id)
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            AddFixColumn();
            for (int i = startyear; i <= endyear; i++)
            {
                AddColumn(i);
            }
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
          
            Ps_History psp_Type = new Ps_History();
            psp_Type.Forecast = id;
            psp_Type.Col4 = pid;
            IList<Ps_History> listTypes = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryByForecast", psp_Type);
            dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_History));
            treeList1.DataSource = dataTable;
            treeList1.Columns["Sort"].SortOrder = SortOrder.Ascending;
            treeList1.Columns["Title"].Caption = "分类名";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;

            Application.DoEvents();
            treeList1.CollapseAll();
            //LoadValues1(htt);
            //treeList1.Nodes[0].Expanded = true;
            //treeList1.ExpandAll();
        }

   

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            

        }

    
        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListMultiSelection tm = treeList1.Selection;
            if (tm.Count == 0)
                return;

            DataRow dr = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            if (dr == null)
                return;
            int id1 = (int)dr["ID"];
            hs.Clear();
            li.Clear();
            if (Flag=="1")//旧版分区县供电实绩
            {
                if (id1 != 4)
                {
                    foreach (TreeListNode tln in tm)
                    {

                        Ps_History pt1 = new Ps_History();
                        Ps_History pt = Services.BaseService.GetOneByKey<Ps_History>((string)tln["ID"]);
                        if (tln["Title"].ToString() == "常规用电量" || tln["Title"].ToString() == "常规负荷")
                        {
                            pt.Title = pt.Title + "-" + tln.ParentNode["Title"];
                        }
                        else
                            if (!tln["Title"].ToString().Contains("电量") && !tln["Title"].ToString().Contains("负荷"))
                            {
                                if (id1 == 2)
                                    pt.Title = pt.Title + "电量";
                                else if (id1 == 3)
                                    pt.Title = pt.Title + "负荷";


                            }
                        if (!hs.ContainsValue(pt))
                            hs.Add(Guid.NewGuid().ToString(), pt);

                    }
                }
                else
                {
                    foreach (TreeListNode tln in tm)
                    {

                        PSP_Types pt = Services.BaseService.GetOneByKey<PSP_Types>((int)tln["ID"]);

                        if (!hs.ContainsValue(pt))
                            hs.Add(Guid.NewGuid().ToString(), pt);

                    }

                }
            }
            else//新版分区县供电实绩
            {
                foreach (TreeListNode tln in tm)
                {

                    Ps_History pt1 = new Ps_History();
                    Ps_History pt = Services.BaseService.GetOneByKey<Ps_History>((string)tln["ID"]);
                    if (tln["Title"].ToString() == "常规用电量" || tln["Title"].ToString() == "常规负荷")
                    {
                        pt.Title = pt.Title + "-" + tln.ParentNode["Title"];
                    }
                    else
                        if (!tln["Title"].ToString().Contains("电量") && !tln["Title"].ToString().Contains("负荷"))
                        {
                            if (id1 == 2)
                                pt.Title = pt.Title + "电量";
                            else if (id1 == 3)
                                pt.Title = pt.Title + "负荷";


                        }
                    if (!hs.ContainsValue(pt))
                        hs.Add(Guid.NewGuid().ToString(), pt);

                }

            }

           
           

            if (hs.Count > 0)
                this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


    }
}