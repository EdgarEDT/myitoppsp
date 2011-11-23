using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.HistoryValue;
using Itop.Common;
using Itop.Client.Base;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Chen;
using Itop.Domain.Table;
using Itop.Client.Table;
using Itop.Client.Forecast;
using Itop.Client.Stutistics;
using Itop.Domain.Stutistics;
using Itop.Domain.Table;
using System.Drawing.Drawing2D;


namespace Itop.Client.Table
{
    public partial class FrmTzgsAHTL : FormBase
    {
        DataTable dataTable;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;
        private string OldEditValue = string.Empty;
        private DataCommon dc = new DataCommon();
        TreeListNode treenode;
        private int typeFlag2 = 1;
        private OperTable oper = new OperTable();
        public Ps_YearRange yAnge = new Ps_YearRange();
        private bool _isSelect = false;
        bool isdel = false;
        public bool IsSelect
        {
            get { return _isSelect; }
            set { _isSelect = value; }
        }
        private string _title = "";

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _unit = "";

        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }
        DevExpress.XtraGrid.GridControl _gcontrol = null;

        public DevExpress.XtraGrid.GridControl Gcontrol
        {
            get { return _gcontrol; }
            set { _gcontrol = value; }
        }

        public FrmTzgsAHTL()
        {
            InitializeComponent();
        }

        private void HideToolBarButton()
        {
            if (!base.AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.EditRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.DeleteRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        public string GetProjectID
        {
            get { return ProjectUID; }
        }

        public int[] GetYears()
        {
            Ps_YearRange yr = yAnge;
            int[] year = new int[4] { yr.BeginYear, yr.StartYear, yr.FinishYear, yr.EndYear };
            return year;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HideToolBarButton();
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.tzgs + "'");
            Show();
            initpasteCols();
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }
        private void initpasteCols()
        {
            pasteCols.Add("Title");
            for (int i = yAnge.StartYear; i <= yAnge.FinishYear; i++)
            {
                pasteCols.Add("yf" + i);
                pasteCols.Add("yk" + i);
            }
        }
        private IList listTypes;
        public void CalcYearVol()
        {
            if (yAnge.StartYear > 2008)
            {
                for (int i = 0; i < listTypes.Count; i++)
                {
                    for (int j = 2009; j < yAnge.StartYear + 1; j++)
                    {
                        ((Ps_Table_TZGS)listTypes[i]).BefVolumn += double.Parse(listTypes[i].GetType().GetProperty("y" + j).GetValue(listTypes[i], null).ToString());
                        ((Ps_Table_TZGS)listTypes[i]).AftVolumn -= double.Parse(listTypes[i].GetType().GetProperty("y" + j).GetValue(listTypes[i], null).ToString());
                    }
                }
            }
        }
        private void LoadData()
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.tzgs + "'");
            int startyear = yAnge.StartYear;
            int endyear = yAnge.StartYear + 5;
            string con = "ProjectID='" + GetProjectID + "' and convert(int,BuildYear) >=" + startyear + "  and convert(int,BuildEd) <=" + endyear+"   Order by Title";
            listTypes = Common.Services.BaseService.GetList("SelectPs_Table_TZGSByConn", con);
           
            CalcYearVol();
          //  AddTotalRow(ref listTypes);
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_TZGS));
            //dataTable = dc.GetSortTable(dataTable, "Flag", true);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dataTable.Rows[i]["Length"] = dataTable.Rows[i]["y1990"];
                
            }
            string[] year={"","","","",""};
            for (int i =0; i < 5; i++)
			{
                year[i]="y"+(startyear+i+1);
			}
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i][year[0]].ToString() == "0" && dataTable.Rows[i][year[1]].ToString() == "0" && dataTable.Rows[i][year[2]].ToString() == "0" && dataTable.Rows[i][year[3]].ToString() == "0" && dataTable.Rows[i][year[4]].ToString() == "0")
                {
                    if (dataTable.Rows[i]["Col4"].ToString() == "")
                    {
                       
                        string conn = "ProjectID='" + dataTable.Rows[i]["ID"] + "'";
                        IList listvalue = Common.Services.BaseService.GetList("SelectPs_Table_TZMXByValue", conn);
                        DataTable temptable = Itop.Common.DataConverter.ToDataTable(listvalue, typeof(Ps_Table_TZMX));
                        double tempdb = 0.0;
                        for (int j = 0; j < temptable.Rows.Count; j++)
                        {
                           
                            for (int k = 0; k < 5; k++)
                            {
                                
                                double tempdb1 = double.Parse(dataTable.Rows[i][year[k]].ToString());
                                double tempdb2 = tempdb1 + double.Parse(temptable.Rows[j][year[k]].ToString());
                                dataTable.Rows[i][year[k]] = tempdb2.ToString();
                                tempdb += tempdb2;
                            }
                        }
                        dataTable.Rows[i]["AftVolumn"] = tempdb.ToString();
                        double berdouble=double.Parse(dataTable.Rows[i]["Amount"].ToString()) - tempdb;
                        dataTable.Rows[i]["BefVolumn"] = berdouble.ToString();
                    }
                    if (dataTable.Rows[i]["Col4"].ToString() == "pw")
                    {
                        
                        string conn = "ProjectID='" + dataTable.Rows[i]["ID"] + "'";
                        IList listvalue = Common.Services.BaseService.GetList("SelectPs_Table_TZMXByValue", conn);
                        DataTable temptable = Itop.Common.DataConverter.ToDataTable(listvalue, typeof(Ps_Table_TZMX));
                        double tempdb = 0.0;
                        for (int j = 0; j < temptable.Rows.Count; j++)
                        {

                            for (int k = 0; k < 5; k++)
                            {

                                double tempdb1 = double.Parse(dataTable.Rows[i][year[k]].ToString());
                                double tempdb2 = tempdb1 + double.Parse(temptable.Rows[j][year[k]].ToString());
                                dataTable.Rows[i][year[k]] = tempdb2.ToString();
                                tempdb += tempdb2;
                            }
                        }
                        dataTable.Rows[i]["AftVolumn"] = tempdb.ToString();
                        double berdouble = double.Parse(dataTable.Rows[i]["Amount"].ToString()) - tempdb;
                        dataTable.Rows[i]["BefVolumn"] = berdouble.ToString();

                    }
                   if (dataTable.Rows[i]["Col4"].ToString() == "line")
	                {
                        string type = "line"; 
                        string conn = "ProjectID='" + dataTable.Rows[i]["ParentID"] + "'and Typeqf='" + type + "'";
                        IList listvalue = Common.Services.BaseService.GetList("SelectPs_Table_TZMXByValue", conn);
                        DataTable temptable = Itop.Common.DataConverter.ToDataTable(listvalue, typeof(Ps_Table_TZMX));
                        double tempdb = 0.0;
                        for (int j = 0; j < temptable.Rows.Count; j++)
                        {

                            for (int k = 0; k < 5; k++)
                            {

                                double tempdb1 = double.Parse(dataTable.Rows[i][year[k]].ToString());
                                double tempdb2 = tempdb1 + double.Parse(temptable.Rows[j][year[k]].ToString());
                                dataTable.Rows[i][year[k]] = tempdb2.ToString();
                                tempdb += tempdb2;
                            }
                        }
                        dataTable.Rows[i]["AftVolumn"] = tempdb.ToString();
                        double berdouble = double.Parse(dataTable.Rows[i]["Amount"].ToString()) - tempdb;
                        dataTable.Rows[i]["BefVolumn"] = Math.Round(berdouble, 3);
                    
	                }
                    if (dataTable.Rows[i]["Col4"].ToString() == "bian")
                    {
                        string type = "sub";
                        string conn = "ProjectID='" + dataTable.Rows[i]["ParentID"] + "'and Typeqf='" + type + "'";
                        IList listvalue = Common.Services.BaseService.GetList("SelectPs_Table_TZMXByValue", conn);
                        DataTable temptable = Itop.Common.DataConverter.ToDataTable(listvalue, typeof(Ps_Table_TZMX));
                        double tempdb = 0.0;
                        for (int j = 0; j < temptable.Rows.Count; j++)
                        {
                            for (int k = 0; k < 5; k++)
                            {

                                double tempdb1 = double.Parse(dataTable.Rows[i][year[k]].ToString());
                                double tempdb2 = tempdb1 + double.Parse(temptable.Rows[j][year[k]].ToString());
                                dataTable.Rows[i][year[k]] = tempdb2.ToString();
                                tempdb += tempdb2;
                            }
                        }
                        dataTable.Rows[i]["AftVolumn"] = tempdb.ToString();
                        double berdouble = double.Parse(dataTable.Rows[i]["Amount"].ToString()) - tempdb;
                        dataTable.Rows[i]["BefVolumn"] = berdouble.ToString();

                    }
                    if (dataTable.Rows[i]["Col4"].ToString() == "pw-line")
                    {
                        string type = "pw-line";
                        string conn = "ProjectID='" + dataTable.Rows[i]["ParentID"] + "'and Typeqf='" + type + "'";
                        IList listvalue = Common.Services.BaseService.GetList("SelectPs_Table_TZMXByValue", conn);
                        DataTable temptable = Itop.Common.DataConverter.ToDataTable(listvalue, typeof(Ps_Table_TZMX));
                        double tempdb = 0.0;
                        for (int j = 0; j < temptable.Rows.Count; j++)
                        {

                            for (int k = 0; k < 5; k++)
                            {

                                double tempdb1 = double.Parse(dataTable.Rows[i][year[k]].ToString());
                                double tempdb2 = tempdb1 + double.Parse(temptable.Rows[j][year[k]].ToString());
                                dataTable.Rows[i][year[k]] = tempdb2.ToString();
                                tempdb += tempdb2;
                            }
                        }
                        dataTable.Rows[i]["AftVolumn"] = tempdb.ToString();
                        double berdouble = double.Parse(dataTable.Rows[i]["Amount"].ToString()) - tempdb;
                        dataTable.Rows[i]["BefVolumn"] = berdouble.ToString();

                    }
                    if (dataTable.Rows[i]["Col4"].ToString() == "pw-pb")
                    {
                        string type = "pw-pb";
                        string conn = "ProjectID='" + dataTable.Rows[i]["ParentID"] + "'and Typeqf='" + type + "'";
                        IList listvalue = Common.Services.BaseService.GetList("SelectPs_Table_TZMXByValue", conn);
                        DataTable temptable = Itop.Common.DataConverter.ToDataTable(listvalue, typeof(Ps_Table_TZMX));
                        double tempdb = 0.0;
                        for (int j = 0; j < temptable.Rows.Count; j++)
                        {

                            for (int k = 0; k < 5; k++)
                            {

                                double tempdb1 = double.Parse(dataTable.Rows[i][year[k]].ToString());
                                double tempdb2 = tempdb1 + double.Parse(temptable.Rows[j][year[k]].ToString());
                                dataTable.Rows[i][year[k]] = tempdb2.ToString();
                                tempdb += tempdb2;
                            }
                        }
                        dataTable.Rows[i]["AftVolumn"] = tempdb.ToString();
                        double berdouble = double.Parse(dataTable.Rows[i]["Amount"].ToString()) - tempdb;
                        dataTable.Rows[i]["BefVolumn"] = berdouble.ToString();

                    }
                    if (dataTable.Rows[i]["Col4"].ToString() == "pw-kg")
                    {
                        string type = "pw-kg";
                        string conn = "ProjectID='" + dataTable.Rows[i]["ParentID"] + "'and Typeqf='" + type + "'";
                        IList listvalue = Common.Services.BaseService.GetList("SelectPs_Table_TZMXByValue", conn);
                        DataTable temptable = Itop.Common.DataConverter.ToDataTable(listvalue, typeof(Ps_Table_TZMX));
                        double tempdb = 0.0;
                        for (int j = 0; j < temptable.Rows.Count; j++)
                        {

                            for (int k = 0; k < 5; k++)
                            {

                                double tempdb1 = double.Parse(dataTable.Rows[i][year[k]].ToString());
                                double tempdb2 = tempdb1 + double.Parse(temptable.Rows[j][year[k]].ToString());
                                dataTable.Rows[i][year[k]] = tempdb2.ToString();
                                tempdb += tempdb2;
                            }
                        }
                        dataTable.Rows[i]["AftVolumn"] = tempdb.ToString();
                        double berdouble = double.Parse(dataTable.Rows[i]["Amount"].ToString()) - tempdb;
                        dataTable.Rows[i]["BefVolumn"] = berdouble.ToString();

                    } 
               }
            }
            treeList1.DataSource = dataTable;
            
            Ps_YearRange yr = yAnge;
            treeList1.Columns["Title"].Caption = "项目名称";
            treeList1.Columns["Title"].Width = 250;
            treeList1.Columns["Title"].MinWidth = 250;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Title"].VisibleIndex = 0;
            treeList1.Columns["BuildYear"].Caption = "开工年限";
            treeList1.Columns["BuildYear"].Width = 100;
            treeList1.Columns["BuildYear"].MinWidth = 100;
            treeList1.Columns["BuildYear"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["BuildYear"].OptionsColumn.AllowSort = false;
            treeList1.Columns["BuildYear"].VisibleIndex = 1;
            treeList1.Columns["BuildEd"].Caption = "竣工年限";
            treeList1.Columns["BuildEd"].Width = 100;
            treeList1.Columns["BuildEd"].MinWidth = 100;
            treeList1.Columns["BuildEd"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["BuildEd"].OptionsColumn.AllowSort = false;
            treeList1.Columns["BuildEd"].VisibleIndex = 2;
            treeList1.Columns["AreaName"].VisibleIndex = -1;
            treeList1.Columns["Length"].Caption = "长度";
            treeList1.Columns["Length"].Width = 70;
            treeList1.Columns["Length"].MinWidth = 70;
            treeList1.Columns["Length"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Length"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Length"].VisibleIndex = 3;

           

            treeList1.Columns["Volumn"].Caption = "容量";
            treeList1.Columns["Volumn"].Width = 70;
            treeList1.Columns["Volumn"].MinWidth = 70;
            treeList1.Columns["Volumn"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Volumn"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Volumn"].VisibleIndex = 4;
            treeList1.Columns["Amount"].Caption = "总投资";
            treeList1.Columns["Amount"].Width = 100;
            treeList1.Columns["Amount"].MinWidth = 100;
            treeList1.Columns["Amount"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Amount"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Amount"].VisibleIndex = 5;
            treeList1.Columns["BefVolumn"].Caption = Convert.ToString(yr.StartYear)+"年底投资";
            treeList1.Columns["BefVolumn"].Width = 100;
            treeList1.Columns["BefVolumn"].MinWidth = 100;
            treeList1.Columns["BefVolumn"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["BefVolumn"].OptionsColumn.AllowSort = false;
            treeList1.Columns["BefVolumn"].VisibleIndex = 6;
            treeList1.Columns["AftVolumn"].Caption = Convert.ToString(yr.StartYear + 1) + "~" + Convert.ToString(yr.StartYear + 5) + "投资合计";
            treeList1.Columns["AftVolumn"].Width = 150;
            treeList1.Columns["AftVolumn"].MinWidth = 150;
            treeList1.Columns["AftVolumn"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["AftVolumn"].OptionsColumn.AllowSort = false;
            treeList1.Columns["AftVolumn"].VisibleIndex = 7;
            CalcYearColumn();
            for (int i = 2; i <= 4; i++)
            {
                treeList1.Columns["Col" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["Col" + i.ToString()].Width = 0;
                treeList1.Columns["Col" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
            }

            treeList1.Columns["Col1"].Caption = "备注";
            treeList1.Columns["Col1"].Width = 300;
            treeList1.Columns["Col1"].MinWidth = 300;
            treeList1.Columns["Col1"].OptionsColumn.AllowEdit = true;
            treeList1.Columns["Col1"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Col1"].VisibleIndex = 3000;
            treeList1.Columns["Sort"].VisibleIndex = -1;
            treeList1.Columns["ProjectID"].VisibleIndex = -1;
            treeList1.Columns["FromID"].VisibleIndex = -1;
            treeList1.Columns["BianInfo"].VisibleIndex = -1;
            treeList1.Columns["LineInfo"].VisibleIndex = -1;
            treeList1.Columns["DQ"].VisibleIndex = -1;
            treeList1.Columns["Num1"].VisibleIndex = -1;
            treeList1.Columns["Num2"].VisibleIndex = -1;
            treeList1.Columns["Num3"].VisibleIndex = -1;
            treeList1.Columns["Num4"].VisibleIndex = -1;
            treeList1.Columns["Num5"].VisibleIndex = -1;
            treeList1.Columns["Num6"].VisibleIndex = -1;
            treeList1.Columns["WGNum"].VisibleIndex = -1;
            treeList1.Columns["AllVolumn"].VisibleIndex = -1;
            treeList1.Columns["JGNum"].VisibleIndex = -1;
            treeList1.Columns["ProgType"].VisibleIndex = -1;
            treeList1.Columns["Length2"].VisibleIndex = -1;
            
            treeList1.Columns["Sort"].SortOrder = SortOrder.Ascending;
            Application.DoEvents();
           // SetValueNull();
            //treeList1.ExpandAll();
            treeList1.CollapseAll();
        }

        public void SetValueNull()
        {
            int[] year = GetYears();
            foreach(TreeListNode node in treeList1.Nodes)
            {
                if (node.GetValue("ParentID").ToString() == "0")
                {
                    for (int i = year[1]; i <= year[2]; i++)
                    {
                        node.SetValue("y" + i.ToString(), null);
                    }
                }
            }
        }

        public void LoadData1()
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                //treeList1.Columns.Clear();
            }
            string con = "ProjectID='" + GetProjectID + "'";
            listTypes = Common.Services.BaseService.GetList("SelectPs_Table_TZGSByConn", con);
            CalcYearVol();
            //IList cloneList = Common.Services.BaseService.GetList("SelectPs_Table_TZGSByConn", con);
            //cloneList.Clear();
            //for (int i = 0; i < listTypes.Count; i++)
            //{
            //    if (((Ps_Table_200PH)listTypes[i]).BuildEd != "total")// == fuID || ((Ps_Table_200PH)listTypes[i]).ParentID == fuID || ((Ps_Table_200PH)listTypes[i]).ID == totoalParent || ((Ps_Table_200PH)listTypes[i]).ParentID == totoalParent)
            //    {
            //        cloneList.Add(listTypes[i]);//listTypes.Remove(listTypes[i]);
            //    }
            //}
            //listTypes.Clear();
            //listTypes = cloneList;
          //  AddTotalRow(ref listTypes);
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_TZGS));
            //dataTable = dc.GetSortTable(dataTable, "Flag", true);

            treeList1.DataSource = dataTable;
           // SetValueNull();
            //treeList1.ExpandAll();
        }
        string totoalParent;
        public void AddTotalRow(ref IList list)
        {
            //合计
            string conn = "ParentID='0' and ProjectID='" + GetProjectID + "'";
            int[] year = GetYears();
            Ps_Table_TZGS parent = new Ps_Table_TZGS();
            parent.ID += "|" + GetProjectID;
            parent.ParentID = "0"; parent.Title = "合计"; parent.Sort = 10000;// OperTable.GetMaxSort() + 1;
            //
            IList<Ps_Table_TZGS> teList = Common.Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByConn", conn);
            double old=0.0,te=0.0;
            for (int i = 0; i < teList.Count; i++)
            {
                parent.AllVolumn += teList[i].AllVolumn;
                parent.BefVolumn += teList[i].BefVolumn;
                parent.AftVolumn += teList[i].AftVolumn;
                for (int j = yAnge.StartYear + 1; j <= yAnge.StartYear + 5; j++)
                {
                    old=double.Parse(parent.GetType().GetProperty("y"+j.ToString()).GetValue(parent,null).ToString());
                    te = double.Parse(teList[i].GetType().GetProperty("y"+j.ToString()).GetValue(teList[i],null).ToString());
                    parent.GetType().GetProperty("y" + j.ToString()).SetValue(parent, old + te, null);
                }
            }
            list.Add(parent);
        }

        public void CalcYearColumn()
        {
            int[] year = GetYears();
            for (int i = year[0]; i < year[1]+1; i++)
            {
                treeList1.Columns["y" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
            }
            for (int i = year[1] + 6; i <= year[3]; i++)
            {
                treeList1.Columns["y" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
            }
            for (int i = year[1]+1; i <= year[1]+5; i++)
            {
                treeList1.Columns["y" + i.ToString()].Caption = i.ToString() + "年";
                treeList1.Columns["y" + i.ToString()].VisibleIndex = i;
                treeList1.Columns["y" + i.ToString()].Width = 70;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.AllowEdit = true;
                //if(year[1]==i)
                //    treeList1.Columns["y" + i.ToString()].OptionsColumn.AllowEdit = false;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.AllowSort = false;
                treeList1.Columns["y" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;

            }
        }

        //读取数据
        private void LoadValues()
        {
            PSP_Values psp_Values = new PSP_Values();
            psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值


            IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesListByFlag2", psp_Values);

            foreach(PSP_Values value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null)
                {
                    node.SetValue(value.Year + "年", value.Value);
                }
            }
        }
       
       
        //添加年份后，新增一列

        private void AddColumn(int year)
        {
            int nInsertIndex = GetInsertIndex(year);

            dataTable.Columns.Add(year + "年", typeof(double));

            TreeListColumn column = treeList1.Columns.Insert(nInsertIndex);
            column.FieldName = year + "年";
            column.Tag = year;
            column.Caption = year + "年";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = nInsertIndex - 2;//有两列隐藏列
            column.ColumnEdit = repositoryItemTextEdit1;
            //treeList1.RefreshDataSource();
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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }

            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            FindNodes(treeList1.FocusedNode);
            string nodestr = treenode.GetValue("Title").ToString();
            if ( focusedNode.GetValue("ParentID").ToString()!="0")
            {
                //MsgBox.Show( focusedNode.GetValue("Title").ToString()+"不允许添加子分类！");
                // return;
                focusedNode = focusedNode.ParentNode;
            }

            FrmAddTzgsWH2 frm = new FrmAddTzgsWH2();
            frm.Text = "增加" + focusedNode.GetValue("Title") + "的子分类";
            frm.Stat = focusedNode.GetValue("Col2").ToString();
           // frm.SetLabelName = "子分类名称";
            if(frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_TZGS table1 = new Ps_Table_TZGS();
                table1.ID += "|" + GetProjectID;
                table1.Title = frm.ParentName;
                table1.ParentID = focusedNode.GetValue("ID").ToString();
                table1.ProjectID = GetProjectID;
                table1.BuildYear = frm.StartYear;
                table1.BuildEd = frm.FinishYear;
                table1.FromID = "0";
                //table1.Length = frm.LineLen;
                //table1.Volumn = frm.Vol;
                table1.AllVolumn = frm.AllVol;
              //  table1.BefVolumn = frm.AllVol;
                table1.AftVolumn = frm.AllVol;
                table1.LineInfo = frm.LineInfo;
                table1.BianInfo = frm.BianInfo;
                table1.AreaName = frm.AreaName;

                table1.GetType().GetProperty("y" + Convert.ToString(yAnge.StartYear + 1)).SetValue(table1, frm.AllVol, null);
                //table1.Col2 = treeList1.FocusedNode.GetValue("Col1").ToString();
                table1.Sort = OperTable.GetTZGSMaxSort()+1;
                table1.Col3 = frm.Col3;
                table1.Col1 = frm.BieZhu;
                table1.DQ = frm.DQ;
                table1.JGNum = frm.JGNum;
                table1.WGNum = frm.WGNum;
                table1.ProgType = frm.ProgType;
                table1.Amount = frm.Amount;
                //table1.Num1 = frm.Num1;
                //table1.Num2 = frm.Num2;
                //table1.Num3 = frm.Num3;
                //table1.Num4 = frm.Num4;

                try
                {
                    string pid = table1.ID;
                    string tit = table1.Title;
                    Common.Services.BaseService.Create("InsertPs_Table_TZGS", table1);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table1, dataTable.NewRow()));
                    frm.StrType = "bian";
                    table1.Title = tit+"-变电";
                    table1.Col4 = "bian";
                    table1.Volumn = frm.Vol;
                    table1.ParentID =pid;
                    table1.ID = Guid.NewGuid().ToString();
                    table1.BianInfo = frm.BianInfo;
                    table1.Num1 = frm.Num1;
                    table1.Num2 = frm.Num2;
                    table1.Num5 = frm.Num5;
                
                    
                    Common.Services.BaseService.Create("InsertPs_Table_TZGS", table1);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table1, dataTable.NewRow()));
                    frm.StrType = "line";
                    table1.Title = tit + "-线路";
                    table1.Col4 = "line";
                    table1.Length = frm.LineLen;
                    table1.ParentID = pid;
                    table1.ID = Guid.NewGuid().ToString();
                    table1.LineInfo = frm.LineInfo;
                    table1.Num1 = frm.Num1;
                    table1.Num2 = frm.Num2;
                    table1.Num5 = frm.Num5;

                    Common.Services.BaseService.Create("InsertPs_Table_TZGS", table1);

                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table1, dataTable.NewRow()));
                    AddChildVol(table1, true);
                    LoadData1();
                    FoucsLocation(table1.ID, treeList1.Nodes);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加工程出错：" + ex.Message);
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }
            //FindNodes(treeList1.FocusedNode);
    
            

            string parentid = treeList1.FocusedNode["ParentID"].ToString();

            if (!base.EditRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            if (parentid == "0")
            {
                FrmAddPN frm = new FrmAddPN();
                //frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
                frm.ParentName = treeList1.FocusedNode.GetValue("Title").ToString();
                frm.Text = "修改分类名";
                frm.SetLabelName = "分类名称";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Ps_Table_TZGS table1 = new Ps_Table_TZGS();
                    table1 = Common.Services.BaseService.GetOneByKey<Ps_Table_TZGS>(treeList1.FocusedNode.GetValue("ID"));
                    table1.Title = frm.ParentName;
                    table1.Col2 = "no";
                    try
                    {
                        Common.Services.BaseService.Update<Ps_Table_TZGS>(table1);
                        treeList1.FocusedNode.SetValue("Title", frm.ParentName);
                    }
                    catch { }
                    //catch(Exception ex)
                    //{
                    //    MsgBox.Show("修改出错：" + ex.Message);
                    //}
                }
            }
            else
            {
                FrmAddTzgsWH2 frm = new FrmAddTzgsWH2();
                Ps_Table_TZGS table = new Ps_Table_TZGS();
                table = Common.Services.BaseService.GetOneByKey<Ps_Table_TZGS>(treeList1.FocusedNode.GetValue("ID"));
                frm.ParentName = table.Title; //treeList1.FocusedNode.GetValue("Title").ToString();
                frm.Text = "修改工程";
                frm.Stat = treeList1.FocusedNode.ParentNode.GetValue("Col2").ToString();

                frm.AreaName = table.AreaName;
                frm.BianInfo = table.BianInfo;
                frm.LineInfo = table.LineInfo;
                frm.LineLen = table.Length;
                frm.Vol = table.Volumn;
                frm.StartYear = table.BuildYear;
                frm.FinishYear = table.BuildEd;
                frm.LineLen = table.Length;
                frm.BieZhu = table.Col1;
                frm.Col3 = table.Col3;
                frm.StrType = table.Col4;
                frm.JGNum = table.JGNum;
                frm.Amount = table.Amount;
                frm.ProgType = table.ProgType;
                frm.WGNum = table.WGNum;
                //frm.TzgsXs = double.Parse(yAngeXs.Col1);
                frm.DQ = table.DQ;

                frm.StrType = "bian";
                Ps_Table_TZGS t1 = new Ps_Table_TZGS();
                string sql1 = " ParentID='" + table.ID + "' and Col4='bian'";
                t1 =(Ps_Table_TZGS) Common.Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", sql1);
                frm.Num1 = t1.Num1;
                frm.Num2 = t1.Num2;
                frm.Num5 = t1.Num5;
                frm.StrType = "line";
                Ps_Table_TZGS t2 = new Ps_Table_TZGS();
                string sql2 = " ParentID='" + table.ID + "' and Col4='line'";
                t2 = (Ps_Table_TZGS)Common.Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", sql2);
                frm.Num1 = t2.Num1;
                frm.Num2 = t2.Num2;
                frm.Num5 = t2.Num5;
       
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    AddChildVol(table, false);
                    table.Title = frm.ParentName;
                    table.BuildYear = frm.StartYear;
                    table.BuildEd = frm.FinishYear;
                    table.Length = frm.LineLen;
                    table.Volumn = frm.Vol;
                    double temp = frm.AllVol - table.AllVolumn;
                    table.AllVolumn = frm.AllVol;
                    table.AftVolumn += temp;
                    table.GetType().GetProperty("y" + Convert.ToString(yAnge.StartYear + 1)).SetValue(table, temp + double.Parse(table.GetType().GetProperty("y" + Convert.ToString(yAnge.StartYear + 1)).GetValue(table,null).ToString()), null);
                    table.Col1 = frm.BieZhu;
                    table.Col3 = frm.Col3;
                    table.Col4 = frm.StrType;
                    table.BianInfo = frm.BianInfo;
                    table.LineInfo = frm.LineInfo;
                    table.JGNum =frm.JGNum;
                    table.Amount=frm.Amount;
                    table.ProgType=frm.ProgType;
                    table.WGNum = frm.WGNum;
                    table.DQ = frm.DQ;
                    frm.StrType = "bian";
                    t1.Num1 = frm.Num1;
                    t1.Num2 = frm.Num2;
                    t1.Num5 = frm.Num5;
                    t1.WGNum = frm.WGNum;
                    frm.StrType = "line";
                    t2.Num1 = frm.Num1;
                    t2.Num2 = frm.Num2;
                    t2.Num5 = frm.Num5;
                    try
                    {
                        Common.Services.BaseService.Update<Ps_Table_TZGS>(table);
                        Common.Services.BaseService.Update<Ps_Table_TZGS>(t1);
                        Common.Services.BaseService.Update<Ps_Table_TZGS>(t2);
                        AddChildVol(table, true);
                        LoadData1();
                        FoucsLocation(table.ID, treeList1.Nodes);
                    }
                    catch { }
                }
            }
        }
        private void FoucsLocation(string id, TreeListNodes tlns)
        {
            foreach (TreeListNode tln in tlns)
            {
                if (tln["ID"].ToString() == id)
                {
                    treeList1.FocusedNode = tln;
                    return;
                }
                FoucsLocation(id, tln.Nodes);
            }

        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }
            if (!base.DeleteRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            string nodestr = treeList1.FocusedNode.GetValue("Col1").ToString();
          
            string parentid = treeList1.FocusedNode["ParentID"].ToString();
          

         
            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("此工程下有子工程，请先删除子工程！");
                return;
            }
            if (treeList1.FocusedNode.GetValue("ParentID").ToString() == "0")
            {
                MsgBox.Show("此工程是分类，请用“删除分类”功能！");
                return;
            }
            if (MsgBox.ShowYesNo("是否删除工程 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
            {
                Ps_Table_TZGS table1 = new Ps_Table_TZGS();
               // Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList1.FocusedNode);
                table1.ID = treeList1.FocusedNode.GetValue("ID").ToString();

                try
                {
                    //DeletePSP_ValuesByType里面删除数据和分类

                    TreeListNode node1 = treeList1.FocusedNode.NextNode;
                    string teID = "";
                    if (node1 != null)
                        teID = node1.GetValue("ID").ToString();
                    AddChildVol(Common.Services.BaseService.GetOneByKey<Ps_Table_TZGS>(table1.ID), false);
                    Common.Services.BaseService.Delete <Ps_Table_TZGS>(table1);//("DeletePs_Table_TZGS", table1);
                    treeList1.DeleteNode(treeList1.FocusedNode);
                    LoadData1();
                    FoucsLocation(teID, treeList1.Nodes);
                }
                catch (Exception ex)
                {
                    //MsgBox.Show("删除出错：" + ex.Message);
                    this.Cursor = Cursors.WaitCursor;
                    treeList1.BeginUpdate();
                    LoadData();
                    treeList1.EndUpdate();
                    this.Cursor = Cursors.Default;
                }
            }
        }

        public void AddChildVol(Ps_Table_TZGS child,bool bAdd)
        {
            Ps_Table_TZGS pare = Common.Services.BaseService.GetOneByKey<Ps_Table_TZGS>(child.ParentID);
            IList<string> list=new List<string>();
            list.Add("Length");list.Add("Volumn");list.Add("AllVolumn");list.Add("BefVolumn");
            list.Add("AftVolumn");
            for (int i = 2008; i <= yAnge.StartYear + 5; i++)
                list.Add("y" + i.ToString());
            double old=0.0,cld=0.0;
            foreach (string str in list)
            {
                old=double.Parse(pare.GetType().GetProperty(str).GetValue(pare,null).ToString());
                cld=double.Parse(child.GetType().GetProperty(str).GetValue(child,null).ToString());
                if (bAdd)
                    pare.GetType().GetProperty(str).SetValue(pare, old + cld, null);
                else
                    pare.GetType().GetProperty(str).SetValue(pare, old - cld, null);
            }
            Common.Services.BaseService.Update<Ps_Table_TZGS>(pare);
        }

        //增加年份
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedColumn == null)
            {
                return;
            }

            //不是年份列

            if(treeList1.FocusedColumn.FieldName.IndexOf("年") == -1)
            {
                return;
            }

            if (!base.DeleteRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedColumn.FieldName + " 及该年的所有数据？") != DialogResult.Yes)
            {
                return;
            }


            PSP_Values psp_Values = new PSP_Values();
            psp_Values.ID = typeFlag2;//借用ID属性存放Flag2
            psp_Values.Year = (int)treeList1.FocusedColumn.Tag;

            try
            {
                //DeletePSP_ValuesByYear删除数据和年份

                int colIndex = treeList1.FocusedColumn.AbsoluteIndex;
                Common.Services.BaseService.Update("DeletePSP_ValuesByYear", psp_Values);
                dataTable.Columns.Remove(treeList1.FocusedColumn.FieldName);
                treeList1.Columns.Remove(treeList1.FocusedColumn);
                if(colIndex >= treeList1.Columns.Count)
                {
                    colIndex--;
                }
                treeList1.FocusedColumn = treeList1.Columns[colIndex];
            }
            catch(Exception ex)
            {
                MsgBox.Show("删除出错：" + ex.Message);
            }
        }
        public void CalcNotZero(TreeListNode node,double vol)
        {
            double v=vol-double.Parse(lastEditValue);
            double temp=0.0,temp1=0.0,temp2=0.0;
            node.SetValue("AftVolumn", double.Parse(node.GetValue("AftVolumn").ToString()) - v);
            node.ParentNode.SetValue("AftVolumn", double.Parse(node.ParentNode.GetValue("AftVolumn").ToString()) - v);
            for (int i = yAnge.StartYear + 5; i >= yAnge.StartYear + 1 ; i--)
            {
                temp2=temp = double.Parse(node.GetValue("y" + i).ToString());
                temp1 = double.Parse(node.ParentNode.GetValue("y" + i).ToString());
                if (temp != 0.0)
                {
                    temp = temp - v;
                   // temp1=temp1-v;
                    v = 0 - temp;
                    if (temp < 0)
                        temp = 0;
                  //  if (temp1 < 0)
                  //      temp1 = 0;
                    node.SetValue("y" + i, temp);
                    node.ParentNode.SetValue("y" + i, temp1-temp2+temp);
                    if (temp > 0)
                        return;
                }
            }
        }
        public void CalcYearVol(TreeListNode node, double vol,string year,TreeListColumn column)
        {
            //double v = vol - double.Parse(lastEditValue);
            //double temp = 0.0;
            //double tot=double.Parse(node.GetValue("y"+Convert.ToString(yAnge.StartYear+1)).ToString());
            //if (tot >= v)
            //{
            //    node.SetValue("y" + Convert.ToString(yAnge.StartYear + 1), tot - v);
            //    //node.SetValue("AftVolumn", double.Parse(node.GetValue("AftVolumn").ToString())-v);
            //   // node.ParentNode.SetValue("AftVolumn", double.Parse(node.ParentNode.GetValue("AftVolumn").ToString()) - v);
            //    node.ParentNode.SetValue("y" + Convert.ToString(yAnge.StartYear + 1), double.Parse(node.ParentNode.GetValue("y" + Convert.ToString(yAnge.StartYear + 1)).ToString()) - v);
            //    node.SetValue(year, vol);
            //}

            double tot = 0.0;
            for (int i = 1; i < 6; i++)
            {
                tot += double.Parse(node.GetValue("y" + Convert.ToString(yAnge.StartYear + i)).ToString());
            }
            if (double.Parse(node.GetValue("Amount").ToString()) <tot)
            {
                flag = true;
                MessageBox.Show("分年投资总额不能大于总投资", "错误");
                node.SetValue(year, double.Parse(OldEditValue));
                flag = false;
                 return;
            }

            //重新计算当前列的父结点值
            CalculateSum(node, column);

            //改变分项n-m年投资合计
            node.SetValue("AftVolumn", tot);
            //改变分项n—m年投资合计的总值父结点
            CalcTotal(node.ParentNode, "AftVolumn");
           
            node.SetValue("BefVolumn", Math.Round(double.Parse(node.GetValue("Amount").ToString()) - tot,3));
            //改变分项m年低投资合计的总值父结点
            CalcTotal(node.ParentNode, "BefVolumn");

            //保存数据
            try
            {
                DataRow row = (node.TreeList.GetDataRecordByNode(node) as DataRowView).Row;
                Ps_Table_TZGS tzgs = DataConverter.RowToObject<Ps_Table_TZGS>(row);
                Common.Services.BaseService.Update<Ps_Table_TZGS>(tzgs);

                DataRow row1 = (node.ParentNode.TreeList.GetDataRecordByNode(node.ParentNode) as DataRowView).Row;
                Ps_Table_TZGS tzgs1 = DataConverter.RowToObject<Ps_Table_TZGS>(row1);
                Common.Services.BaseService.Update<Ps_Table_TZGS>(tzgs1); 
            }
            catch (Exception)
            {
                
                throw;
            }
            
            
        }
        public void CalcNotAll(TreeListNode node, double vol)
        {

            double v = vol - double.Parse(lastEditValue);
           // node.ParentNode.SetValue("AllVolumn", double.Parse(node.ParentNode.GetValue("AllVolumn").ToString()) + v);
            double temp = 0.0, temp1 = 0.0, temp2 = 0.0;
            double tot = double.Parse(node.GetValue("y" + Convert.ToString(yAnge.StartYear + 1)).ToString());
            node.SetValue("AftVolumn", double.Parse(node.GetValue("AftVolumn").ToString()) + v);
            node.ParentNode.SetValue("AftVolumn", double.Parse(node.ParentNode.GetValue("AftVolumn").ToString()) + v);
            if (v > 0)
            {
                node.SetValue("y" + Convert.ToString(yAnge.StartYear + 1), tot + v);
                node.ParentNode.SetValue("y" + Convert.ToString(yAnge.StartYear + 1), double.Parse(node.ParentNode.GetValue("y" + Convert.ToString(yAnge.StartYear + 1)).ToString()) + v);
            }
            else
            {
                for (int i = yAnge.StartYear + 5; i >= yAnge.StartYear + 1; i--)
                {
                    temp2 = temp = double.Parse(node.GetValue("y" + i).ToString());
                    temp1 = double.Parse(node.ParentNode.GetValue("y" + i).ToString());
                    if (temp != 0.0)
                    {
                        temp = temp + v;
                        // temp1=temp1-v;
                        v = 0 - temp;
                        if (temp < 0)
                            temp = 0;
                        //  if (temp1 < 0)
                        //      temp1 = 0;
                        node.SetValue("y" + i, temp);
                        node.ParentNode.SetValue("y" + i, temp1 - temp2 + temp);
                        if (temp > 0)
                            return;
                    }
                }
            }
        }
        public void CalcVolumn(TreeListNode node, TreeListColumn column)
        {
            bPast = true;
            if (column.FieldName == "BefVolumn")
            {
                //CalcNotZero(node, double.Parse(node.GetValue(column.FieldName).ToString()));
            }
            else if (column.FieldName == "Amount")
            {
                //CalcNotAll(node, double.Parse(node.GetValue(column.FieldName).ToString()));
            }
            else if (column.FieldName.StartsWith("y") && column.FieldName!="y"+Convert.ToString(yAnge.StartYear+1))
            {
                CalcYearVol(node, double.Parse(node.GetValue(column.FieldName).ToString()), column.FieldName,column);
            }
            bPast = false;
        }
        public void CalcGong(TreeListNode node, TreeListColumn column,double cha)
        {
            node.ParentNode.SetValue(column.FieldName, double.Parse(node.ParentNode.GetValue(column.FieldName).ToString())+cha);
        }
        bool bPast = false;
      //  double totalvalue = 0.0;
        bool flag = false;
        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            //if (bPast)
            //    return;
            //if (e.Value != null) 
            //if ((e.Value.ToString() != lastEditValue
            //    || lastEditNode != e.Node
            //    || lastEditColumn != e.Column)
            //    && (e.Column.Caption.IndexOf("年") > 0 || e.Column.FieldName == "AftVolumn"))
            //{

            //    if (double.Parse(e.Value.ToString()) > double.Parse(e.Node.GetValue("Amount").ToString()))
            //    {
            //        MessageBox.Show("不能大于总投资", "错误");
            //        return;
            //    }
            //    CalcVolumn(e.Node, e.Column);
            //    DataRow row = (e.Node.TreeList.GetDataRecordByNode(e.Node) as DataRowView).Row;
            //    Ps_Table_TZGS tzgs = DataConverter.RowToObject<Ps_Table_TZGS>(row);
            //    Common.Services.BaseService.Update<Ps_Table_TZGS>(tzgs);
            //    CalcGong(e.Node, e.Column, double.Parse(e.Value.ToString()) - double.Parse(lastEditValue));
            //    DataRow row1 = (e.Node.ParentNode.TreeList.GetDataRecordByNode(e.Node.ParentNode) as DataRowView).Row;
            //    Ps_Table_TZGS tzgs1 = DataConverter.RowToObject<Ps_Table_TZGS>(row1);
            //    Common.Services.BaseService.Update<Ps_Table_TZGS>(tzgs1);   
            //}

            if (flag)
            {
                return;
            }
            if (e.Column.Caption.IndexOf("年")>0)
            {
               
                JSvalue(e.Node, e.Column);
                  
            }
        }
        public void JSvalue(TreeListNode node, TreeListColumn column)
        {
            //flag = true;
            if (column.FieldName == "BefVolumn")
            {
                //CalcNotZero(node, double.Parse(node.GetValue(column.FieldName).ToString()));
            }
            else if (column.FieldName == "Amount")
            {
                //CalcNotAll(node, double.Parse(node.GetValue(column.FieldName).ToString()));
            }
            else if (column.FieldName.StartsWith("y") )
            {
                CalcYearVol(node, double.Parse(node.GetValue(column.FieldName).ToString()), column.FieldName,column);
            }
            //flag = false;
        }
        private bool SaveCellValue(string year, string typeID, double value)
        {
            Ps_Table_TZGS psp = new Ps_Table_TZGS();
            Ps_Table_TZGS old = Common.Services.BaseService.GetOneByKey<Ps_Table_TZGS>(typeID);
            psp = (Ps_Table_TZGS)old.Clone();
            psp.GetType().GetProperty(year).SetValue(psp, Math.Round(value,1),null);

            try
            {
                Common.Services.BaseService.Update<Ps_Table_TZGS>(psp);
            }
            catch(Exception ex)
            {
                MsgBox.Show("保存数据出错：" + ex.Message);
                return false;
            }
            return true;
        }

       
       
        private void FindNodes(TreeListNode node)
        {
            if (node.ParentNode == null)
            {
                treenode = node;
                return ;

            }
           
            FindNodes(node.ParentNode);
            return ;
        }
        //当子分类数据改变时，计算其父分类的值

        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            TreeListNode parentNode = node.ParentNode;

            if (parentNode == null)
            {
                return;
            }
            else
            {
                CalcTotal(parentNode, column);
            }
           
        
        }
        List<string> pasteCols = new List<string>();
       

        private void pasteData(TreeListNode tln)
        {
            string s1 = tln["Title"].ToString();
            bPast = true;

            IDataObject obj1 = Clipboard.GetDataObject();
            string text = obj1.GetData("Text").ToString();
            string[] lines = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            double x = 0.0;
            tln.TreeList.BeginInit();
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
                        if (j != 0 && !double.TryParse(items[j], out x))
                            continue;
                        if (j > 0)
                            fnode.SetValue(pasteCols[j], double.Parse(items[j]));
                        else
                            fnode.SetValue(pasteCols[j], items[j]);
                        if (j > 0)
                        {
                            SaveCellValue(pasteCols[j], fnode.GetValue("ID").ToString(), double.Parse(items[j]));
                            CalculateSum(fnode, fnode.TreeList.Columns[pasteCols[j]]);
                        }
                    }
                    catch (Exception e) { string ddd = e.Message; }
                }
                //  updateNode(fnode);
            }
            // updateSummaryNode(tln);
            tln.TreeList.EndInit();
            bPast = false;
        }
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
                    retnode = findByColumnValue(node, column, findstr, f1);
            }
            return retnode;
        }
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
        public void CalcTotal(TreeListNode node, TreeListColumn column)
        {
            double total = 0.0; 
           
            foreach (TreeListNode nd in node.Nodes)
            {
                string temp = column.Name.ToString().Substring(4, column.Name.ToString().Length-4);
                total += double.Parse(nd.GetValue(temp).ToString());
            }
            node.SetValue(column.Name.ToString().Substring(4, column.Name.ToString().Length - 4), total);
        }
        //分项改变时计算父类值
        public void CalcTotal(TreeListNode node, string columnname)
        {
            double total = 0.0;

            foreach (TreeListNode nd in node.Nodes)
            {

                total += double.Parse(nd.GetValue(columnname).ToString());
            }
            node.SetValue(columnname, total);
        }
        private void treeList1_ShownEditor(object sender, EventArgs e)
        {
            lastEditColumn = treeList1.FocusedColumn;
            lastEditNode = treeList1.FocusedNode;
            lastEditValue = treeList1.FocusedNode.GetValue(lastEditColumn.FieldName).ToString();
        }

        //统计
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //FormChooseYears frm = new FormChooseYears();
            //for (int i = yAnge.StartYear; i <= yAnge.FinishYear; i++)
            //{
            //    frm.ListYearsForChoose.Add(i);

            //}
            //frm.NoIncreaseRate = true;

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
                string con = "ProjectID='" + GetProjectID + "' ORDER BY Sort";
                listTypes = Common.Services.BaseService.GetList("SelectPs_Table_TZGSByConn", con);

                CalcYearVol();
                AddTotalRow(ref listTypes);
                DataTable dt = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_TZGS));
                

                FrmResultPrint frma = new FrmResultPrint();
                IList<string> strTe = new List<string>();
                
                for (int k = yAnge.StartYear + 1; k <= yAnge.StartYear + 5; k++)
                    strTe.Add(k.ToString());
                frma.YearList1 = strTe;
                frma.IsSelect = _isSelect;
                frma.Text = "电网建设项目投资估算表";
                frma.Dw1 = "单位:千米、万千伏安、万元";
               // treeList1.DataSource = dt;
                treeList1.DataSource = dataTable;
                frma.bTzgs = true;
                frma.IsBand = false;
                frma.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1, false), strTe);
                
                listTypes = Common.Services.BaseService.GetList("SelectPs_Table_TZGSByConn", con);

                CalcYearVol();
            DataTable dt1 = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_TZGS));
                //treeList1.DataSource = dt1;
                treeList1.DataSource = dataTable;
              //  SetValueNull();
                //treeList1.ExpandAll();
                if (frma.ShowDialog() == DialogResult.OK)
                {
                    //gcontrol = frm.gridControl1;
                    //title = frm.Title;
                    //unit = "单位：万元";
                    DialogResult = DialogResult.OK;
                }





          //  }
        }
        private DataTable ResultDataTable(DataTable sourceDataTable, IList<string> listChoosedYears)
        {
            DataTable dtReturn = new DataTable();
            dtReturn = sourceDataTable;
            //dtReturn.Columns.Add("Title", typeof(string));
            //foreach (string choosedYear in listChoosedYears)
            //{
            //    dtReturn.Columns.Add(choosedYear.Year + "年", typeof(double));
            //}

            //int nRowMaxLoad = 0;//地区最高负荷所在行
            //int nRowMaxPower = 0;//地区电网供电能力所在行
            //int nRowMaxPowerLow = 0;//枯水期地区电网供电能力所在行

            //#region 填充数据
            //for (int i = 0; i < sourceDataTable.Rows.Count; i++)
            //{
            //    DataRow newRow = dtReturn.NewRow();
            //    DataRow sourceRow = sourceDataTable.Rows[i];
            //    foreach (DataColumn column in dtReturn.Columns)
            //    {
            //        newRow[column.ColumnName] = sourceRow[ConvertYear(column.ColumnName)];
            //    }
            //    dtReturn.Rows.Add(newRow);


            //}
            //#endregion

            //#region 计算电力盈亏和枯水期地区电力盈亏
            //foreach (ChoosedYears choosedYear in listChoosedYears)
            //{
            //    object maxLoad = dtReturn.Rows[nRowMaxLoad][choosedYear.Year + "年"];
            //    if (maxLoad != DBNull.Value)
            //    {
            //        object maxPower = dtReturn.Rows[nRowMaxPower][choosedYear.Year + "年"];
            //        if (maxPower != DBNull.Value)
            //        {
            //            dtReturn.Rows[nRowMaxPower + 1][choosedYear.Year + "年"] = (double)maxPower - (double)maxLoad;
            //        }

            //        maxPower = dtReturn.Rows[nRowMaxPowerLow][choosedYear.Year + "年"];
            //        if (maxPower != DBNull.Value)
            //        {
            //            dtReturn.Rows[nRowMaxPowerLow + 1][choosedYear.Year + "年"] = (double)maxPower - (double)maxLoad;
            //        }
            //    }
            //}
            //#endregion

            return dtReturn;
        }

        //把树控件内容按显示顺序生成到DataTable中

        private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList, bool bRemove)
        {
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();
            listColID.Add("FromID");
            dt.Columns.Add("FromID", typeof(string));
            listColID.Add("BuildYear");
            dt.Columns.Add("BuildYear", typeof(string));

            listColID.Add("BuildEd");
            dt.Columns.Add("BuildEd", typeof(string));

            listColID.Add("Length");
            dt.Columns.Add("Length", typeof(double));
            listColID.Add("Volumn");
            dt.Columns.Add("Volumn", typeof(double));
            listColID.Add("AllVolumn");
            dt.Columns.Add("AllVolumn", typeof(double));
            listColID.Add("BefVolumn");
            dt.Columns.Add("BefVolumn", typeof(double));
            listColID.Add("AftVolumn");
            dt.Columns.Add("AftVolumn", typeof(double));
            listColID.Add("Title");
            dt.Columns.Add("Title", typeof(string));
            dt.Columns["Title"].Caption = "项目名称";
            for (int i = yAnge.StartYear + 1; i <= yAnge.StartYear + 5; i++)
            {
                listColID.Add("y" + i.ToString());
                dt.Columns.Add("y" + i.ToString(), typeof(double));
            }
            for (int i = 1; i < 6; i++)
            {
                listColID.Add("Col" + i.ToString());
                dt.Columns.Add("Col" + i.ToString(), typeof(string));
            }
            foreach (TreeListNode node in xTreeList.Nodes)
            {
                AddNodeDataToDataTable(dt, node, listColID, bRemove);
            }

            return dt;
        }
        int myN = 1,youN=0;
        private void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID, bool bRemove)
        {
            DataRow newRow = dt.NewRow();
            foreach (string colID in listColID)
            {
                //分类名，第二层及以后在前面加空格
                if (colID == "Title" && node.ParentNode != null)
                {
                    newRow[colID] = "　　" + node[colID];
                }
                else
                {
                    newRow[colID] = node[colID];
                }
                if (colID == "FromID")
                {
                    if (node["ParentID"].ToString() == "0")
                    {
                        myN = 1;
                        newRow["FromID"] = OperTable.often[youN];
                        youN++;
                    }
                    else
                    {
                        newRow["FromID"] = myN.ToString();
                        myN++;
                    }

                }
            }
            //if (bRemove)
            //{
            //    if (newRow["Col1"].ToString() != "1" && newRow["BuildEd"].ToString() != "total")
            //        dt.Rows.Add(newRow);
            //}
            //else
                dt.Rows.Add(newRow);

            foreach (TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID, bRemove);
            }
        }


        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private InputLanguage oldInput = null;
        private void treeList1_FocusedColumnChanged(object sender, DevExpress.XtraTreeList.FocusedColumnChangedEventArgs e)
        {
            try
            {
                DevExpress.XtraEditors.Repository.RepositoryItemTextEdit edit = e.Column.ColumnEdit as DevExpress.XtraEditors.Repository.RepositoryItemTextEdit;
                if (edit != null && edit.Mask.MaskType == DevExpress.XtraEditors.Mask.MaskType.Numeric)
                {
                    oldInput = InputLanguage.CurrentInputLanguage;
                    InputLanguage.CurrentInputLanguage = null;
                }
                else
                {
                    if (oldInput != null && oldInput != InputLanguage.CurrentInputLanguage)
                    {
                        InputLanguage.CurrentInputLanguage = oldInput;
                    }
                }
            }
            catch { }
        }

     

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormYearSet fys = new FormYearSet();
            fys.TYPE = OperTable.tzgs;
            fys.PID = ProjectUID;
            fys.SetTzgs();
            if (fys.ShowDialog() != DialogResult.OK)
                return;
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.tzgs + "'");
            LoadData();
        }

      

       
       
        public Ps_Table_TZGS GetTZGSParent(string type)
        {
            try
            {
                IList list = Common.Services.BaseService.GetList("SelectPs_Table_TZGSByConn", "Title like '"+type+"%' and ProjectID='"+GetProjectID+"' and ParentID='0'");
                Ps_Table_TZGS guid = new Ps_Table_TZGS();
                guid = null;
                if (list.Count > 0)
                    guid = (Ps_Table_TZGS)list[0];
                return guid;
            }
            catch { return null; }
        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if ( treeList1.FocusedColumn.Name.IndexOf("y")<0)
            {
                return;
            }
           double endyear= double.Parse(treeList1.FocusedNode.GetValue("BuildEd").ToString());
           string tempstr = treeList1.FocusedColumn.Name.Substring(4, treeList1.FocusedColumn.Name.ToString().Length - 4);

           double nowyear = double.Parse(tempstr.Substring(1,4));
           
            if (treeList1.FocusedNode.HasChildren||endyear<nowyear)
            {
                e.Cancel = true;
            }
            else
            {
                OldEditValue = treeList1.FocusedNode.GetValue(tempstr).ToString();
            }
        }

        

        private void treeList1_CustomDrawNodeCell(object sender, DevExpress.XtraTreeList.CustomDrawNodeCellEventArgs e)
        {
            Brush backBrush, foreBrush;
            if (e.Node.ParentNode == null)
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.Bisque, Color.Bisque, LinearGradientMode.Horizontal);
                        foreBrush = new SolidBrush(Color.Black);
                   
                       
               
                    // filling the background
                    e.Graphics.FillRectangle(backBrush, e.Bounds);
                    // painting node value
                    e.Graphics.DrawString(e.CellText, e.Appearance.Font, foreBrush, e.Bounds, e.Appearance.GetStringFormat());

                    // prohibiting default painting
                    e.Handled = true;

                    //zwcxTreeList1.Canvas.Font.Color 
                    
                
            }
            else
            {
                backBrush = new SolidBrush(Color.DarkBlue);
                foreBrush = new SolidBrush(Color.PeachPuff);

            }
        }

      

      


       

    }
}