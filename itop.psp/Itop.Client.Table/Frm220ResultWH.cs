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

namespace Itop.Client.Table
{
    public partial class Frm220ResultWH : FormBase
    {
        DataTable dataTable;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;
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

        public Frm220ResultWH()
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
                barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem12.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem13.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.DeleteRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.rst220 + "'");
            Show();
            initpasteCols();
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }
        private IList listTypes;
        private void LoadData()
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }
            string conn = "ProjectID='" + GetProjectID + "' and ParentID='0'";
            IList list = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", conn);
            for (int i = 0; i < list.Count; i++)
            {
                Update110(((Ps_Table_220Result)list[i]).Title, ((Ps_Table_220Result)list[i]).ID);
            }
            string con = "ProjectID='" + GetProjectID + "'";
            listTypes = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", con);
          //  AddTotalRow(ref listTypes);
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_220Result));
            //dataTable = dc.GetSortTable(dataTable, "Flag", true);

            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "项目名称";
            treeList1.Columns["Title"].Width = 250;
            treeList1.Columns["Title"].MinWidth = 250;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Title"].VisibleIndex = 0;
            CalcYearColumn();
            viewForK("yf", barCheckItem1.Checked); viewForK("yk", barCheckItem2.Checked);
            for (int i = 1; i <= 4; i++)
            {
                treeList1.Columns["Col" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["Col" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
            }
            treeList1.Columns["Sort"].VisibleIndex = -1;
            treeList1.Columns["ProjectID"].VisibleIndex = -1;
            treeList1.Columns["BuildIng"].VisibleIndex = -1;
            treeList1.Columns["BuildEd"].VisibleIndex = -1;
            treeList1.Columns["BuildYear"].VisibleIndex = -1;
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
                        node.SetValue("yf" + i.ToString(), 1);
                        node.SetValue("yk" + i.ToString(), 1);
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
            listTypes = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", con);
            //IList cloneList = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", con);
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
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_220Result));
            //dataTable = dc.GetSortTable(dataTable, "Flag", true);

            treeList1.DataSource = dataTable;
            //SetValueNull();
            //treeList1.ExpandAll();
        }
        string totoalParent;
        public void AddTotalRow(ref IList list)
        {
            //合计
            string conn = "ParentID='0' and ProjectID='" + GetProjectID + "'";

            int[] year = GetYears();
            Ps_Table_220Result parent = new Ps_Table_220Result();
            parent.ID += "|" + GetProjectID;
            parent.ParentID = "0"; parent.Title = "220千伏合计"; parent.Sort = 1000;// OperTable.GetMaxSort() + 1;
            list.Add(parent);
            totoalParent = parent.ID;
            string[] lei = new string[7] { "一、110千伏供电负荷合计", "二、110千伏直供负荷", "三、220千伏变电站低压侧供电负荷", "四、110千伏及以下地方电源出力", "五、外网110千伏及以下送入电力", "六、外网110千伏及以下送出电力", "七、220千伏供电负荷" };
            for (int i = 0; i < lei.Length; i++)
            {
                conn = "Col1='"+Convert.ToString(i+1)+"' and ProjectID='" + GetProjectID + "'";
                string conn1 = "ProjectID='" + GetProjectID + "' and Col2='"+Convert.ToString(i+1)+"'";
                if (i == 6)
                {
                    conn = "Col1='no' and ProjectID='" + GetProjectID + "'";
                    conn1 = "ProjectID='" + GetProjectID + "' and Col2='no'";
                }
                Ps_Table_220Result table1 = new Ps_Table_220Result();
                table1.ID += "|" + GetProjectID;
                table1.Title = lei[i];
                table1.ParentID = parent.ID;
                table1.ProjectID = GetProjectID;
                table1.Col1 = Convert.ToString(i + 1);
                table1.BuildEd = "total";
                if (i == 6)
                    table1.Col1 = "no";
                IList tList = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", conn);
                for (int j = year[1]; j <= year[2]; j++)
                {
                    double first = 0.0, sec = 0.0;
                    for (int k = 0; k < tList.Count; k++)
                    {
                        first += double.Parse(((Ps_Table_220Result)tList[k]).GetType().GetProperty("yf" + j).GetValue((Ps_Table_220Result)tList[k], null).ToString());
                        sec += double.Parse(((Ps_Table_220Result)tList[k]).GetType().GetProperty("yk" + j).GetValue((Ps_Table_220Result)tList[k], null).ToString());
                    }
                    table1.GetType().GetProperty("yf" + j).SetValue(table1,first, null);
                    table1.GetType().GetProperty("yk" + j).SetValue(table1, sec, null);
                }
                table1.Sort = i + 1;
                list.Add(table1);
                //IList cList = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", conn1);
                //for (int j = 0; j < cList.Count; j++)
                //{
                //    Ps_Table_220Result tablex = new Ps_Table_220Result();
                //    tablex = (Ps_Table_220Result)((Ps_Table_220Result)cList[j]).Clone();
                //    tablex.BuildEd = "total";
                //    tablex.ParentID = table1.ID;
                //    list.Add(tablex);
                //}
            }
            
        }

        public void CalcYearColumn()
        {
            int[] year = GetYears();
            for (int i = year[0]; i < year[1]; i++)
            {
                treeList1.Columns["yf" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["yf" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["yk" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["yk" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["yk" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yk" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["yk" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;
                treeList1.Columns["yf" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yf" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["yf" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;

            }
            for (int i = year[2] + 1; i <= year[3]; i++)
            {
                treeList1.Columns["yf" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["yf" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["yk" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["yk" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["yk" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yk" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["yk" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;
                treeList1.Columns["yf" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yf" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["yf" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;

            }
            for (int i = year[1]; i <= year[2]; i++)
            {
                treeList1.Columns["yf" + i.ToString()].Caption = i.ToString() + "年丰";
                treeList1.Columns["yf" + i.ToString()].VisibleIndex = i;
                treeList1.Columns["yf" + i.ToString()].Width = 70;
               // treeList1.Columns["yf" + i.ToString()].OptionsColumn.AllowEdit = false;
                treeList1.Columns["yf" + i.ToString()].OptionsColumn.AllowSort = false;
                treeList1.Columns["yf" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yk" + i.ToString()].Caption = i.ToString() + "年枯";
                treeList1.Columns["yk" + i.ToString()].VisibleIndex = i;
                treeList1.Columns["yk" + i.ToString()].Width = 70;
              //  treeList1.Columns["yk" + i.ToString()].OptionsColumn.AllowEdit = false;
                treeList1.Columns["yk" + i.ToString()].OptionsColumn.AllowSort = false;
                treeList1.Columns["yk" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yk" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yk" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["yk" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;
                treeList1.Columns["yf" + i.ToString()].ColumnEdit = repositoryItemTextEdit1;
                treeList1.Columns["yf" + i.ToString()].Format.FormatString = "#####################0.##";
                treeList1.Columns["yf" + i.ToString()].Format.FormatType = DevExpress.Utils.FormatType.Numeric;

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
        //计算单产耗能
        public void ComputeValue(int year)
        {
            TreeListNode node= treeList1.FindNodeByFieldValue("Title", "全社会用电量(万千瓦时)");
            TreeListNode node1 = treeList1.FindNodeByFieldValue("Title", "GDP(万元)");
            TreeListNode node2 = treeList1.FindNodeByFieldValue("Title", "单产耗能(万千瓦时/万元)");//全社会供电量(万千瓦时)
          
            try
            {

                if (node.Nodes[0].Nodes[0][year + "年"].ToString() == "" || node1.Nodes[0][year + "年"].ToString() == "" || node1.Nodes[0][year + "年"].ToString() == "0")
                {
                    node2.Nodes[0].SetValue(year + "年",null);
                }
                else
                    node2.Nodes[0].SetValue(year + "年", Math.Round(Convert.ToDouble(node.Nodes[0].Nodes[0][year + "年"].ToString()) / Convert.ToDouble(node1.Nodes[0][year + "年"].ToString()), 4));
                

                //if (node.Nodes[0].Nodes[0].Nodes[0][year + "年"].ToString() != "" && node1.Nodes[0][year + "年"].ToString() != "" && node1.Nodes[0][year + "年"].ToString() != "0")
                //{
                //    node2.Nodes[0].SetValue(year + "年", Math.Round(Convert.ToDouble(node.Nodes[0].Nodes[0].Nodes[0][year + "年"].ToString()) / Convert.ToDouble(node1.Nodes[0][year + "年"].ToString()), 4));
                //}

            }
            catch { }
            try
            {
                if (node.Nodes[0].Nodes[1][year + "年"].ToString() == "" || node1.Nodes[1][year + "年"].ToString() == ""||node1.Nodes[1][year + "年"].ToString() == "0")
                {
                    node2.Nodes[1].SetValue(year + "年",null);
                }
                else
                    node2.Nodes[1].SetValue(year + "年", Math.Round(Convert.ToDouble(node.Nodes[0].Nodes[1][year + "年"].ToString()) / Convert.ToDouble(node1.Nodes[1][year + "年"].ToString()), 4));
  
                //if (node.Nodes[0].Nodes[0].Nodes[1][year + "年"].ToString() != "" && node1.Nodes[1][year + "年"].ToString() != "" && node1.Nodes[1][year + "年"].ToString() != "0")
                //{
                //    node2.Nodes[1].SetValue(year + "年", Math.Round(Convert.ToDouble(node.Nodes[0].Nodes[0].Nodes[1][year + "年"].ToString()) / Convert.ToDouble(node1.Nodes[1][year + "年"].ToString()), 4));
                //}
            }
            catch { }
            try
            {
                if (node.Nodes[0].Nodes[2][year + "年"].ToString() == "" || node1.Nodes[2][year + "年"].ToString() == "" || node1.Nodes[2][year + "年"].ToString() == "0")
                {
                    node2.Nodes[2].SetValue(year + "年", null);
                }
                else
                    node2.Nodes[2].SetValue(year + "年", Math.Round(Convert.ToDouble(node.Nodes[0].Nodes[2][year + "年"].ToString()) / Convert.ToDouble(node1.Nodes[2][year + "年"].ToString()), 4));
              
                //if (node.Nodes[0].Nodes[0].Nodes[2][year + "年"].ToString() != "" && node1.Nodes[2][year + "年"].ToString() != "" && node1.Nodes[2][year + "年"].ToString() != "0")
                //{
                //    node2.Nodes[2].SetValue(year + "年", Math.Round(Convert.ToDouble(node.Nodes[0].Nodes[0].Nodes[2][year + "年"].ToString()) / Convert.ToDouble(node1.Nodes[2][year + "年"].ToString()), 4));
                //}
            }
            catch { }

            double n1 = 0;
            double n2 = 0;
            double n3 = 0;
            
            if (node2.Nodes[0][year + "年"].ToString() != "")
            { n1 = Convert.ToDouble(node2.Nodes[0][year + "年"].ToString()); }

            if (node2.Nodes[1][year + "年"].ToString() != "")
            { n2 = Convert.ToDouble(node2.Nodes[1][year + "年"].ToString()); }

            if (node2.Nodes[2][year + "年"].ToString() != "")
            { n3 = Convert.ToDouble(node2.Nodes[2][year + "年"].ToString()); }

            if (n1 == 0 && n2 == 0 && n3 == 0)
            { node2.SetValue(year + "年", null); }
            else
            {
                node2.SetValue(year + "年", Math.Round(Convert.ToDouble(n1 + n2 + n3), 4));
            }

        }

        //计算人均用电量,人均生活用电量
        public void ComputeElectricValue(int year)
        {
            TreeListNode node = treeList1.FindNodeByFieldValue("Title", "全社会用电量(万千瓦时)");
            TreeListNode node1 = treeList1.FindNodeByFieldValue("Title", "人口(万人)");
            TreeListNode node2 = treeList1.FindNodeByFieldValue("Title", "人均用电量");
          //  TreeListNode node2 = treeList1.FindNodeByFieldValue("Title", "人均用电量(千瓦时/人)");
            TreeListNode node3 = treeList1.FindNodeByFieldValue("Title", "B、城乡居民生活用电合计");
            TreeListNode node4 = treeList1.FindNodeByFieldValue("Title", "人均生活用电量(千瓦时/人)");

            TreeListNode node6 = treeList1.FindNodeByFieldValue("Title", "全社会供电量(万千瓦时)"); //供电最大负荷(万千瓦)
            TreeListNode node7 = treeList1.FindNodeByFieldValue("Title", "供电最大负荷(万千瓦)");
            TreeListNode node8 = treeList1.FindNodeByFieldValue("Title", "Tmax");
            try
            {
               // double value1=Math.Round(Convert.ToDouble(node[year + "年"].ToString()) / Convert.ToDouble(node1[year + "年"].ToString()), 0);
                if (node[year + "年"].ToString() == ""|| node1[year + "年"].ToString() == ""|| node1[year + "年"].ToString() == "0" )
                {
                    node2.SetValue(year + "年", null);
                }
                else
                    node2.SetValue(year + "年", Math.Round(Convert.ToDouble(node[year + "年"].ToString()) / Convert.ToDouble(node1[year + "年"].ToString()), 0));
                //if (node[year + "年"].ToString() != "" && node1[year + "年"].ToString() != "" && node1[year + "年"].ToString() != "0" && value1!=0)
                //{
                //    node2.SetValue(year + "年", value1);
                //}
            }
            catch { }
            try
            {
                
                if (node3[year + "年"].ToString() == "" || node1[year + "年"].ToString() == "" || node1[year + "年"].ToString() == "0")
                {
                    node4.SetValue(year + "年", null);
                }
                else
                    node4.SetValue(year + "年", Math.Round(Convert.ToDouble(node3[year + "年"].ToString()) / Convert.ToDouble(node1[year + "年"].ToString()), 0));

            }
            catch { }


            try
            {

                if (node6[year + "年"].ToString() == "" || node7[year + "年"].ToString() == "" || node7[year + "年"].ToString() == "0")
                {
                    node8.SetValue(year + "年", null);
                }
                else
                    node8.SetValue(year + "年", Math.Round(Convert.ToDouble(node6[year + "年"].ToString()) / Convert.ToDouble(node7[year + "年"].ToString()), 0));

            }
            catch { }

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
            if (treeList1.FocusedNode.GetValue("Col1").ToString() == "no" || focusedNode.GetValue("ParentID").ToString()=="0")
            {
                MsgBox.Show( focusedNode.GetValue("Title").ToString()+"不允许添加子分类！");
                return;
            }

            FrmAddPN frm = new FrmAddPN();
            frm.Text = "增加" + focusedNode.GetValue("Title") + "的子分类";
            frm.SetLabelName = "子分类名称";
            if(frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_220Result table1 = new Ps_Table_220Result();
                table1.ID += "|" + GetProjectID;
                table1.Title = frm.ParentName;
                table1.ParentID = focusedNode.GetValue("ID").ToString();
                table1.ProjectID = GetProjectID;
                table1.Col1 = "child";
                table1.Col2 = treeList1.FocusedNode.GetValue("Col1").ToString();
                table1.Sort = OperTable.Get220ResultMaxSort()+1;

                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_220Result", table1);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table1, dataTable.NewRow()));
                }
                catch(Exception ex)
                {
                    MsgBox.Show("增加子分类出错：" + ex.Message);
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
    
            string nodestr = treeList1.FocusedNode.GetValue("Col1").ToString();
            

            string parentid = treeList1.FocusedNode["ParentID"].ToString();

            if (!base.EditRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            if (nodestr == "1" || nodestr == "2" || nodestr == "3" || nodestr == "4" || nodestr == "5" || nodestr == "6" || nodestr == "no")
            {
                MsgBox.Show("固定分类不许修改！");
                return;
            }
            FrmAddPN frm = new FrmAddPN();
            //frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.ParentName = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "修改分类名";
            frm.SetLabelName = "分类名称";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_220Result table1 = new Ps_Table_220Result();
                table1 = Common.Services.BaseService.GetOneByKey<Ps_Table_220Result>(treeList1.FocusedNode.GetValue("ID"));
                table1.Title = frm.ParentName;

                try
                {
                    Common.Services.BaseService.Update<Ps_Table_220Result>(table1);
                    treeList1.FocusedNode.SetValue("Title", frm.ParentName);
                }
                catch { }
                //catch(Exception ex)
                //{
                //    MsgBox.Show("修改出错：" + ex.Message);
                //}
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
            if (nodestr == "1" || nodestr == "2" || nodestr == "3" || nodestr == "4" || nodestr == "5" || nodestr == "6" || nodestr == "no")
            {
                MsgBox.Show("固定分类不许删除！");
                return;
            }
            string parentid = treeList1.FocusedNode["ParentID"].ToString();
          

         
            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("此分类下有子分类，请先删除子分类！");
                return;
            }



            

            if(MsgBox.ShowYesNo("是否删除分类 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
            {
                Ps_Table_220Result table1 = new Ps_Table_220Result();
               // Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList1.FocusedNode);
                table1.ID = treeList1.FocusedNode.GetValue("ID").ToString();

                try
                {
                    //DeletePSP_ValuesByType里面删除数据和分类
                    Common.Services.BaseService.Delete <Ps_Table_220Result>(table1);//("DeletePs_Table_220Result", table1);

                    TreeListNode brotherNode = null;
                    if(treeList1.FocusedNode.ParentNode.Nodes.Count > 1)
                    {
                        brotherNode = treeList1.FocusedNode.PrevNode;
                        if(brotherNode == null)
                        {
                            brotherNode = treeList1.FocusedNode.NextNode;
                        }
                    }
                    treeList1.DeleteNode(treeList1.FocusedNode);

                    //删除后，如果同级还有其它分类，则重新计算此类的所有年份数据
                    if(brotherNode != null)
                    {
                        foreach(TreeListColumn column in treeList1.Columns)
                        {
                            if (column.Caption.IndexOf("年") > 0)
                            {
                                CalculateSum(brotherNode, column);
                            }
                        }
                    }
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

        //增加年份
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            FormNewYear frm = new FormNewYear();
            frm.Flag2 = typeFlag2;
            int nFixedColumns = typeof(PSP_Types).GetProperties().Length;
            int nColumns = treeList1.Columns.Count;
            if (nFixedColumns == nColumns + 2)//相等时，表示还没有年份，新年份默认为当前年减去15年
            {
                frm.YearValue = DateTime.Now.Year - 15;
            }
            else
            {
                //有年份时，默认为最大年份加1年
                frm.YearValue = (int)treeList1.Columns[nColumns - 1].Tag + 1;
            }

            if (frm.ShowDialog() == DialogResult.OK)
            {
                AddColumn(frm.YearValue);
            }
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
        bool bPast = false;
        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (bPast)
                return;
            if (e.Value != null) 
            if ((e.Value.ToString() != lastEditValue
                || lastEditNode != e.Node
                || lastEditColumn != e.Column)
                && e.Column.Caption.IndexOf("年") > 0)
            {
                
                
                    if (SaveCellValue((string)treeList1.FocusedColumn.FieldName, (string)treeList1.FocusedNode.GetValue("ID"), Convert.ToDouble(e.Value)))
                    {
                        CalculateSum(e.Node, e.Column);
                        if (e.Value.ToString() == "0" && isdel)
                        {

                            treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, null);
                            //string flagid = " year=" + treeList1.FocusedColumn.FieldName.Replace("Y","").Replace("年","")+ " and TypeID=" + treeList1.FocusedNode["ID"];
                            //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                            //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                          
                        }
                    }
                    else
                    {
                        treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, lastEditValue);
                    }
                
              
                  
            }

            FindNodes(treeList1.FocusedNode);
            string nodestr = treenode.GetValue("Title").ToString();
            if (nodestr == "GDP(万元)")
            {
                ComputeValue((int)treeList1.FocusedColumn.Tag);
            }
            if (nodestr == "全社会用电量(万千瓦时)")
            {
                ComputeValue((int)treeList1.FocusedColumn.Tag);
            }
            if (nodestr == "人口(万人)")
            {
                ComputeElectricValue((int)treeList1.FocusedColumn.Tag);
          
            }
            isdel = false;
        }

        private bool SaveCellValue(string year, string typeID, double value)
        {
            Ps_Table_220Result psp = new Ps_Table_220Result();
            Ps_Table_220Result old = Common.Services.BaseService.GetOneByKey<Ps_Table_220Result>(typeID);
            psp = (Ps_Table_220Result)old.Clone();
            psp.GetType().GetProperty(year).SetValue(psp, Math.Round(value,1),null);

            try
            {
                Common.Services.BaseService.Update<Ps_Table_220Result>(psp);
            }
            catch(Exception ex)
            {
                MsgBox.Show("保存数据出错：" + ex.Message);
                return false;
            }
            return true;
        }

        public void CalcTotalChange(string year, string name, double value)
        {
            if (name == "一、110千伏以下负荷")
                name = "一、110千伏供电负荷合计";

        }

        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if(treeList1.FocusedNode.HasChildren
                || !base.EditRight)
            {
                e.Cancel = true;
            }
            FindNodes(treeList1.FocusedNode);
            string nodestr = treenode.GetValue("Title").ToString();
          if (nodestr == "单产耗能(万千瓦时/万元)")
            {
                e.Cancel = true;
            }
            if (nodestr == "人均用电量(千瓦时/人)")
            {
                e.Cancel = true;
            }
            if (nodestr == "Tmax")
            {
                e.Cancel = true;
            }
            if (treeList1.FocusedNode.GetValue("Col1").ToString() == "no")
                e.Cancel = true;
            if (treeList1.FocusedNode.GetValue("BuildEd").ToString() == "total")
                e.Cancel = true;
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
            if (parentNode.GetValue("ParentID").ToString() == "0")
            {
                CalcTotal(node, column);
                return;
            }
            double sum = 0;
            foreach(TreeListNode nd in parentNode.Nodes)
            {
                object value = nd.GetValue(column.FieldName);
                if(value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }
           
            if(sum!=0)
            parentNode.SetValue(column.FieldName, sum);
            else
            parentNode.SetValue(column.FieldName, 0.0);
        if (sum != 0)
        {
            if (!SaveCellValue((string)column.FieldName, (string)parentNode.GetValue("ID"), sum))
            {
                return;
            }
        }
        else
        {
            //if (parentNode.HasChildren)
            //{
            //    string flagid = " year=" + treeList1.FocusedColumn.FieldName.Replace("Y", "").Replace("年", "") + " and TypeID=" + parentNode["ID"];
            //    //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
            //    //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
            //    IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
            //    if (listValues.Count == 1)
            //    {
            //        Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
            //    }
            //}
            if (!SaveCellValue((string)column.FieldName, (string)parentNode.GetValue("ID"), 0.0))
            {

            }
        }
        

            CalculateSum(parentNode, column);
        }
        List<string> pasteCols = new List<string>();
        private void initpasteCols()
        {
            pasteCols.Add("Title");
            for (int i = yAnge.StartYear; i <= yAnge.FinishYear; i++)
            {
                pasteCols.Add("yf" + i);
                pasteCols.Add("yk" + i);
            }
        }

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
                    catch (Exception e) { string ddd = e.Message;  }
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
            double total = 0.0; string col1 = "";
            TreeListNode to = null;
            foreach (TreeListNode nd in node.ParentNode.Nodes)
            {
                col1 = nd.GetValue("Col1").ToString();
                if (col1 == "no")
                    to = nd;
                object value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value)
                {
                    switch (col1)
                    {
                        case "1":
                        case "2":
                        case "6":
                        case "3":
                            total += Convert.ToDouble(value);
                            break;
                        case "4":
                        case "5":
                            total -= Convert.ToDouble(value);
                            break;
                        default:
                            total += 0.0;
                            break;
                    }
                }
            }
            if (total != 0)
                to.SetValue(column.FieldName, Math.Round(total,1));
            else
                to.SetValue(column.FieldName, 0.0);
            SaveCellValue((string)column.FieldName, (string)to.GetValue("ID"), total);
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
            FormChooseYears frm = new FormChooseYears();
            for (int i = yAnge.StartYear; i <= yAnge.FinishYear; i++)
            {
                frm.ListYearsForChoose.Add(i);

            }
            frm.NoIncreaseRate = true;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                string con = "ProjectID='" + GetProjectID + "' ORDER BY Sort";
                listTypes = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", con);
                  AddTotalRow(ref listTypes);
                 
                DataTable dt = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_220Result));
                

                FrmResultPrint frma = new FrmResultPrint();
                frma.IsSelect = _isSelect;
                frma.Text = "220千伏电力平衡结果表";
                frma.Dw1 = "单位：万千瓦";
                treeList1.DataSource = dt;
                frma.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1,false), frm.ListChoosedYears);
                
                listTypes = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", con);
                DataTable dt1 = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_220Result));
                treeList1.DataSource = dt1;
              //  SetValueNull();
                //treeList1.ExpandAll();
                frma.YearList = frm.ListChoosedYears;
                if (frma.ShowDialog() == DialogResult.OK && _isSelect)
                {
                    //gcontrol = frm.gridControl1;
                    //title = frm.Title;
                    //unit = "单位：万元";
                    DialogResult = DialogResult.OK;
                }





            }
        }

        //把树控件内容按显示顺序生成到DataTable中
        private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList, bool bRemove)
        {
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();
            listColID.Add("BuildIng");
            dt.Columns.Add("BuildIng", typeof(string));
            listColID.Add("BuildYear");
            dt.Columns.Add("BuildYear", typeof(string));

            listColID.Add("BuildEd");
            dt.Columns.Add("BuildEd", typeof(string));
            listColID.Add("Sort");
            dt.Columns.Add("Sort", typeof(int));
            listColID.Add("ProjectID");
            dt.Columns.Add("ProjectID", typeof(string));

            listColID.Add("ParentID");
            dt.Columns.Add("ParentID", typeof(string));

            listColID.Add("Title");
            dt.Columns.Add("Title", typeof(string));
            dt.Columns["Title"].Caption = "分类";
            for (int i = yAnge.StartYear; i <= yAnge.FinishYear; i++)
            {
                listColID.Add("yf" + i.ToString());
                dt.Columns.Add("yf" + i.ToString(), typeof(double));
                listColID.Add("yk" + i.ToString());
                dt.Columns.Add("yk" + i.ToString(), typeof(double));
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
            }
            if (bRemove)
            {
                if (newRow["Col1"].ToString() != "1" && newRow["BuildEd"].ToString() != "total")
                    dt.Rows.Add(newRow);
            }
            else
                dt.Rows.Add(newRow);

            foreach (TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID, bRemove);
            }
        }


        public string ConvertYear(string year)
        {
            if (year.IndexOf("年") == -1)
                return year;
            else if (year.IndexOf("丰") != -1)
                return "yf" + year.Substring(0, 4);
            else if (year.IndexOf("枯") != -1)
                return "yk" + year.Substring(0, 4);
            else
                return "";
        }

        //根据选择的统计年份，生成统计结果数据表
        private DataTable ResultDataTable(DataTable sourceDataTable, List<Itop.Client.Chen.ChoosedYears> listChoosedYears)
        {
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("Title", typeof(string));
            foreach (Itop.Client.Chen.ChoosedYears choosedYear in listChoosedYears)
            {
                dtReturn.Columns.Add(choosedYear.Year + "年丰", typeof(double));
                dtReturn.Columns.Add(choosedYear.Year + "年枯", typeof(double));
            }

            int nRowMaxLoad = 0;//地区最高负荷所在行
            int nRowMaxPower = 0;//地区电网供电能力所在行
            int nRowMaxPowerLow = 0;//枯水期地区电网供电能力所在行

            #region 填充数据
            for (int i = 0; i < sourceDataTable.Rows.Count; i++)
            {
                DataRow newRow = dtReturn.NewRow();
                DataRow sourceRow = sourceDataTable.Rows[i];
                foreach (DataColumn column in dtReturn.Columns)
                {
                    newRow[column.ColumnName] = sourceRow[ConvertYear(column.ColumnName)];
                }
                dtReturn.Rows.Add(newRow);


            }
            #endregion

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
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            Ps_YearRange range = yAnge;
            TreeListNode focusedNode = treeList1.FocusedNode;

            //if (focusedNode == null)
            //{
            //    return;
            //}


            FrmAddPN frm = new FrmAddPN();
            string ParId = "",Heid="";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_220Result table_yd = new Ps_Table_220Result();
                table_yd.ID += "|" + GetProjectID;
                table_yd.Title = frm.ParentName;
                table_yd.ParentID = "0";
                table_yd.Sort = OperTable.Get220ResultMaxSort() + 1;
                table_yd.ProjectID = GetProjectID;
                for (int i = range.BeginYear; i <= range.EndYear; i++)
                {
                    table_yd.GetType().GetProperty("yf" + i.ToString()).SetValue(table_yd, null, null);
                    table_yd.GetType().GetProperty("yk" + i.ToString()).SetValue(table_yd, null, null);
                }
                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_220Result", table_yd);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加城区出错：" + ex.Message);
                }
                string[] lei = new string[7] { "一、110千伏以下负荷", "二、110千伏直供负荷","三、220千伏变电站低压侧供电负荷","四、110千伏及以下地方电源出力", "五、外网110千伏及以下送入电力", "六、外网110千伏及以下送出电力", "七、220千伏供电负荷" };
                for (int i = 0; i < lei.Length; i++)
                {
                    Ps_Table_220Result table1 = new Ps_Table_220Result();
                    table1.ID += "|" + GetProjectID;
                    table1.Title = lei[i];
                    table1.ParentID = table_yd.ID;
                    table1.ProjectID = GetProjectID;
                    table1.Col1 = Convert.ToString(i+1);
                    if (i == 6)
                    { 
                        table1.Col1 = "no";
                        Heid = table1.ID;
                    }
                    if (i == 3)
                        ParId = table1.ID;//,table_yd.Title);
                    table1.Sort = i+1;
                    try
                    {
                        Common.Services.BaseService.Create("InsertPs_Table_220Result", table1);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("增加项目出错：" + ex.Message);
                    }
                }
                AddModelChild(ParId, table_yd.Title, Heid);
                this.Cursor = Cursors.WaitCursor;
                treeList1.BeginUpdate();
                //treeList1.ExpandAll();
                LoadData1();
                FoucsLocation(table_yd.ID, treeList1.Nodes);
                treeList1.EndUpdate();
                this.Cursor = Cursors.Default;
            }
        }
        public void Update110(string chen,string pid)
        {
            try
            {
                int[] year = GetYears();
                string con1 = "", con2 = "", con3 = "", con4 = "", con7 = "";
                double oldshuiyf = 0.0, old4yf = 0.0, oldshuiyk = 0.0, old4yk = 0.0, oldyf = 0.0, oldyk = 0.0, newyf = 0.0, newyk = 0.0,
                    oldheyf = 0.0, oldheyk = 0.0, oldhuoyf = 0.0, oldhuoyk = 0.0, newhuoyf = 0.0, newhuoyk = 0.0;
                con1 = "ProjectID='" + GetProjectID + "' and Col3='shui' and Col4='" + chen + "'";
                IList listup = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", con1);
                con4 = "ProjectID='" + GetProjectID + "' and Col3='huo' and Col4='" + chen + "'";
                IList listdown = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", con4);
                if (listup.Count > 0)
                {
                    con1 = "ProjectID='" + GetProjectID + "' and Col1='4' and ParentID='" + pid + "'";
                    IList list4 = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", con1);
                    Ps_Table_220Result res4 = (Ps_Table_220Result)((Ps_Table_220Result)list4[0]).Clone();

                    con2 = "ProjectID='" + GetProjectID + "' and Col3='shui' and ParentID='" + res4.ID + "'";
                    IList listshui = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", con2);
                    Ps_Table_220Result reshui = (Ps_Table_220Result)((Ps_Table_220Result)listshui[0]).Clone();

                    con3 = "ProjectID='" + GetProjectID + "' and Col3='35xia' and ParentID='" + reshui.ID + "'";
                    IList list35x = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", con3);

                    con4 = "ProjectID='" + GetProjectID + "' and Col3='huo' and ParentID='" + res4.ID + "'";
                    IList listhuo = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", con4);
                    Ps_Table_220Result reshuo = (Ps_Table_220Result)((Ps_Table_220Result)listhuo[0]).Clone();

                    con7 = "ProjectID='" + GetProjectID + "' and Col1='no' and ParentID='" + pid + "'";
                    IList list7 = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", con7);
                    Ps_Table_220Result reshe = (Ps_Table_220Result)((Ps_Table_220Result)list7[0]).Clone();
                    Ps_Table_220Result res35 = (Ps_Table_220Result)((Ps_Table_220Result)list35x[0]).Clone();
                    for (int i = year[0]; i <= year[3]; i++)
                    {
                        oldyf = double.Parse(list35x[0].GetType().GetProperty("yf" + i.ToString()).GetValue(list35x[0], null).ToString());
                        oldyk = double.Parse(list35x[0].GetType().GetProperty("yk" + i.ToString()).GetValue(list35x[0], null).ToString());
                        newyf = double.Parse(listup[0].GetType().GetProperty("yf" + i.ToString()).GetValue(listup[0], null).ToString());
                        newyk = double.Parse(listup[0].GetType().GetProperty("yk" + i.ToString()).GetValue(listup[0], null).ToString());
                        oldhuoyf = double.Parse(listhuo[0].GetType().GetProperty("yf" + i.ToString()).GetValue(listhuo[0], null).ToString());
                        oldhuoyk = double.Parse(listhuo[0].GetType().GetProperty("yk" + i.ToString()).GetValue(listhuo[0], null).ToString());
                        newhuoyf = double.Parse(listdown[0].GetType().GetProperty("yf" + i.ToString()).GetValue(listdown[0], null).ToString());
                        newhuoyk = double.Parse(listdown[0].GetType().GetProperty("yk" + i.ToString()).GetValue(listdown[0], null).ToString());
                        if (oldyf != newyf || oldhuoyf != newhuoyf)
                        {
                            res35.GetType().GetProperty("yf" + i.ToString()).SetValue(res35, newyf, null);
                            reshuo.GetType().GetProperty("yf" + i.ToString()).SetValue(reshuo, newhuoyf, null);
                            oldshuiyf = double.Parse(listshui[0].GetType().GetProperty("yf" + i.ToString()).GetValue(listshui[0], null).ToString());
                            reshui.GetType().GetProperty("yf" + i.ToString()).SetValue(reshui, oldshuiyf - oldyf + newyf, null);
                            old4yf = double.Parse(list4[0].GetType().GetProperty("yf" + i.ToString()).GetValue(list4[0], null).ToString());
                            res4.GetType().GetProperty("yf" + i.ToString()).SetValue(res4, old4yf - oldyf + newyf - oldhuoyf + newhuoyf, null);
                            oldheyf = double.Parse(list7[0].GetType().GetProperty("yf" + i.ToString()).GetValue(list7[0], null).ToString());
                            reshe.GetType().GetProperty("yf" + i.ToString()).SetValue(reshe, oldheyf + oldyf - newyf + oldhuoyf - newhuoyf, null);
                        }
                        if (oldyk != newyk || oldhuoyk != newhuoyk)
                        {
                            res35.GetType().GetProperty("yk" + i.ToString()).SetValue(res35, newyk, null);
                            reshuo.GetType().GetProperty("yk" + i.ToString()).SetValue(reshuo, newhuoyk, null);
                            oldshuiyk = double.Parse(listshui[0].GetType().GetProperty("yk" + i.ToString()).GetValue(listshui[0], null).ToString());
                            reshui.GetType().GetProperty("yk" + i.ToString()).SetValue(reshui, oldshuiyk - oldyk + newyk, null);
                            old4yk = double.Parse(list4[0].GetType().GetProperty("yk" + i.ToString()).GetValue(list4[0], null).ToString());
                            res4.GetType().GetProperty("yk" + i.ToString()).SetValue(res4, old4yk - oldyk + newyk - oldhuoyk + newhuoyk, null);
                            oldheyk = double.Parse(list7[0].GetType().GetProperty("yk" + i.ToString()).GetValue(list7[0], null).ToString());
                            reshe.GetType().GetProperty("yk" + i.ToString()).SetValue(reshe, oldheyk + oldyk - newyk + oldhuoyk - newhuoyk, null);
                        }
                    }
                    Common.Services.BaseService.Update<Ps_Table_220Result>(res35);
                    Common.Services.BaseService.Update<Ps_Table_220Result>(reshui);
                    Common.Services.BaseService.Update<Ps_Table_220Result>(reshuo);
                    Common.Services.BaseService.Update<Ps_Table_220Result>(res4);
                    Common.Services.BaseService.Update<Ps_Table_220Result>(reshe);
                }
            }
            catch { }
        }
        public void AddModelChild(string id,string pid,string heid)
        {
            string conn = "ProjectID='"+GetProjectID+"' and Col3='shui' and Col4='"+pid+"'";
            IList listshui = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", conn);
            conn = "ProjectID='" + GetProjectID + "' and Col3='huo' and Col4='" + pid + "'";
            IList listhuo = Common.Services.BaseService.GetList("SelectPs_Table_110ResultByConn", conn);
            Ps_Table_220Result res1 = Common.Services.BaseService.GetOneByKey<Ps_Table_220Result>(id);
            Ps_Table_220Result reshe = Common.Services.BaseService.GetOneByKey<Ps_Table_220Result>(heid);
            Ps_Table_220Result table1 = new Ps_Table_220Result();
            table1.ID += "|" + GetProjectID;
            table1.Title = "其中：小水电";
            table1.ParentID = id;
            table1.ProjectID = GetProjectID;
            table1.Col3 = "shui";
            table1.Col1 = "child";
            table1.Col2 = "no";
            table1.Sort = 0;
            Ps_Table_220Result table3 = new Ps_Table_220Result();
            table3.ID += "|" + GetProjectID;
            table3.Title = "35千伏及以下地方小水电出力";
            table3.ParentID = table1.ID;
            table3.ProjectID = GetProjectID;
            table3.Col3 = "35xia";
            table3.Col1 = "child";
            table3.Col2 = "no";
            table3.Sort = 0;
            double temp = 0.0,temp1=0.0,temp2=0.0,temp3=0.0;
            if (listshui.Count > 0)
            {
                for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
                {
                    temp=double.Parse(listshui[0].GetType().GetProperty("yf"+i.ToString()).GetValue(listshui[0],null).ToString());
                    temp1 = double.Parse(listshui[0].GetType().GetProperty("yk" + i.ToString()).GetValue(listshui[0], null).ToString());
                    table1.GetType().GetProperty("yf" + i.ToString()).SetValue(table1, temp, null);
                    table1.GetType().GetProperty("yk" + i.ToString()).SetValue(table1, temp1, null);
                    table3.GetType().GetProperty("yf" + i.ToString()).SetValue(table3, temp, null);
                    table3.GetType().GetProperty("yk" + i.ToString()).SetValue(table3, temp1, null);
                    res1.GetType().GetProperty("yf" + i.ToString()).SetValue(res1, temp, null);
                    res1.GetType().GetProperty("yk" + i.ToString()).SetValue(res1, temp1, null);
                    reshe.GetType().GetProperty("yf" + i.ToString()).SetValue(reshe, 0.0-temp, null);
                    reshe.GetType().GetProperty("yk" + i.ToString()).SetValue(reshe, 0.0-temp1, null);
                }
            }
            Ps_Table_220Result table4 = new Ps_Table_220Result();
            table4.ID += "|" + GetProjectID;
            table4.Title = "其它";
            table4.ParentID = id;
            table4.ProjectID = GetProjectID;
            table4.Col3 = "other";
            table4.Col1 = "child";
            table4.Col2 = "no";
            table4.Sort = 3;
            Ps_Table_220Result table2 = new Ps_Table_220Result();
            table2.ID += "|" + GetProjectID;
            table2.Title = "小火电";
            table2.ParentID = id;
            table2.ProjectID = GetProjectID;
            table2.Col3 = "huo";
            table2.Col1 = "child";
            table2.Col2 = "no";
            table2.Sort = 1;
            if (listhuo.Count > 0)
            {
                for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
                {
                    temp = double.Parse(listhuo[0].GetType().GetProperty("yf" + i.ToString()).GetValue(listhuo[0], null).ToString());
                    temp1 = double.Parse(listhuo[0].GetType().GetProperty("yk" + i.ToString()).GetValue(listhuo[0], null).ToString());
                    table2.GetType().GetProperty("yf" + i.ToString()).SetValue(table2, temp, null);
                    table2.GetType().GetProperty("yk" + i.ToString()).SetValue(table2, temp1, null);
                    if (listshui.Count > 0)
                    {
                        temp2 = double.Parse(listshui[0].GetType().GetProperty("yf" + i.ToString()).GetValue(listshui[0], null).ToString());
                        temp3 = double.Parse(listshui[0].GetType().GetProperty("yk" + i.ToString()).GetValue(listshui[0], null).ToString());
                        res1.GetType().GetProperty("yf" + i.ToString()).SetValue(res1, temp+temp2, null);
                        res1.GetType().GetProperty("yk" + i.ToString()).SetValue(res1, temp1+temp3, null);
                        reshe.GetType().GetProperty("yf" + i.ToString()).SetValue(reshe, 0.0 - temp-temp2, null);
                        reshe.GetType().GetProperty("yk" + i.ToString()).SetValue(reshe, 0.0 - temp1-temp3, null);
                    }
                }
                
            }
            Common.Services.BaseService.Create("InsertPs_Table_220Result", table1);
            Common.Services.BaseService.Create("InsertPs_Table_220Result", table3);
            Common.Services.BaseService.Create("InsertPs_Table_220Result", table2);
            Common.Services.BaseService.Create("InsertPs_Table_220Result", table4);
            Common.Services.BaseService.Update<Ps_Table_220Result>(res1);
            Common.Services.BaseService.Update<Ps_Table_220Result>(reshe);
        }

        private void treeList1_EditorKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyData == Keys.Back || e.KeyData == Keys.Space || e.KeyData == Keys.Delete)
            //{
            //    isdel = true;
            //}
            //else
            //    isdel = false;
            //if (treeList1.FocusedNode!=null)
            //if (treeList1.FocusedNode[treeList1.FocusedColumn.FieldName].ToString() == "0" && isdel)
            //{

            //    treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, null);
            //    string flagid = " year=" + treeList1.FocusedColumn.FieldName.Replace("Y", "").Replace("年", "") + " and TypeID=" + treeList1.FocusedNode["ID"];
            //    //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
            //    //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
            //    IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
            //    if (listValues.Count == 1)
            //    {
            //        Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
            //    }

            //}
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            if (treeList1.FocusedNode.GetValue("ParentID").ToString() != "0")
            {
                MsgBox.Show("此分类不是总分类！");
                return;
            }

            if (MsgBox.ShowYesNo("总分类及其下属分类都将删除，是否删除总分类 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
            {
                Ps_Table_220Result table1 = new Ps_Table_220Result();
                // Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList1.FocusedNode);
                table1.ID = treeList1.FocusedNode.GetValue("ID").ToString();
                DelAll(table1.ID);
                try
                {
                    //DeletePSP_ValuesByType里面删除数据和分类
                    Common.Services.BaseService.Delete<Ps_Table_220Result>(table1);//("DeletePs_Table_220Result", table1);

                    
                    treeList1.DeleteNode(treeList1.FocusedNode);
                    //treeList1.ExpandAll();
                    //删除后，如果同级还有其它分类，则重新计算此类的所有年份数据
                    
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

        public void DelAll(string suid)
        {
            string conn = "ParentId='" + suid + "'";
            IList<Ps_Table_220Result> list = Common.Services.BaseService.GetList<Ps_Table_220Result>("SelectPs_Table_220ResultByConn", conn);
            if (list.Count > 0)
            {
                foreach (Ps_Table_220Result var in list)
                {
                    string child = var.ID;
                    DelAll(child);
                    Ps_Table_220Result ny = new Ps_Table_220Result();
                    ny.ID = child;
                    Common.Services.BaseService.Delete(ny);
                }
            }
            else
                return;
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCopy frm = new FrmCopy();
            frm.CurID = GetProjectID;
            frm.ClassName = "Ps_Table_220Result";
            frm.SelectString = "SelectPs_Table_220ResultByConn";
            frm.InsertString = "InsertPs_Table_220Result";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("导入成功");
                LoadData1();
            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("该操作将清除当前项目的所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string conn = "ProjectID='" + GetProjectID + "'";
                Common.Services.BaseService.Update("DeletePs_Table_220ResultByConn", conn);
                LoadData1();
            }
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormYearSet fys = new FormYearSet();
            fys.TYPE = OperTable.rst220;
            fys.PID = ProjectUID;
            if (fys.ShowDialog() != DialogResult.OK)
                return;
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.rst220 + "'");
            LoadData();
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string conn = "ProjectID='"+GetProjectID+"' and ParentID='0'";
            IList list = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", conn);
            for (int i = 0; i < list.Count; i++)
            {
                Update110(((Ps_Table_220Result)list[i]).Title, ((Ps_Table_220Result)list[i]).ID);
            }
            LoadData1();
        }

        public void UpdateFuHe(string pid, string col1, Ps_Table_220Result oldrs, Ps_Table_220Result newrs)
        {
            string conn = "ProjectID='" + GetProjectID + "' and Col1='" + col1 + "' and ParentID='" + pid + "'";
            IList list = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", conn);
            if (list.Count > 0)
            {
                for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
                {
                    double oldyf = double.Parse(oldrs.GetType().GetProperty("yf" + i.ToString()).GetValue(oldrs, null).ToString());
                    double oldyk = double.Parse(oldrs.GetType().GetProperty("yk" + i.ToString()).GetValue(oldrs, null).ToString());
                    double newyf = double.Parse(newrs.GetType().GetProperty("yf" + i.ToString()).GetValue(newrs, null).ToString());
                    double newyk = double.Parse(newrs.GetType().GetProperty("yk" + i.ToString()).GetValue(newrs, null).ToString());
                    double myyf = double.Parse(list[0].GetType().GetProperty("yf" + i.ToString()).GetValue(list[0], null).ToString());
                    double myyk = double.Parse(list[0].GetType().GetProperty("yk" + i.ToString()).GetValue(list[0], null).ToString());
                    list[0].GetType().GetProperty("yf" + i.ToString()).SetValue(list[0], Math.Round(myyf - oldyf + newyf, 1), null);
                    list[0].GetType().GetProperty("yk" + i.ToString()).SetValue(list[0], Math.Round(myyk - oldyk + newyk, 1), null);
                }
                Common.Services.BaseService.Update<Ps_Table_220Result>((Ps_Table_220Result)list[0]);
            }
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (this.treeList1.FocusedNode != null)
            //{
            //    string nTitle = this.treeList1.FocusedNode.GetValue("Title").ToString();
            //    string nId = this.treeList1.FocusedNode.GetValue("Col1").ToString();
            //    if (nTitle == "一、110千伏以下负荷" && nId == "1")
            //    {
            //        Ps_Table_220Result psr = Common.Services.BaseService.GetOneByKey<Ps_Table_220Result>(this.treeList1.FocusedNode.GetValue("ID").ToString());
            //        Ps_Table_220Result old = (Ps_Table_220Result)psr.Clone();
            //        FormLoadForecastData frm = new FormLoadForecastData();
            //        frm.ProjectUID = GetProjectID;
            //        if (frm.ShowDialog() == DialogResult.OK)
            //        {
            //            FrmFKbi fkb = new FrmFKbi();
            //            if (fkb.ShowDialog() == DialogResult.OK)
            //            {
            //                DataRow row = frm.ROW;
            //                for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
            //                {
            //                    psr.GetType().GetProperty("yk" + i.ToString()).SetValue(psr, Math.Round(double.Parse(row["y" + i.ToString()].ToString()), 1), null);
            //                    psr.GetType().GetProperty("yf" + i.ToString()).SetValue(psr, Math.Round(double.Parse(row["y" + i.ToString()].ToString()) * fkb.GetVal, 1), null);
            //                }
            //                Common.Services.BaseService.Update<Ps_Table_220Result>(psr);
            //                UpdateFuHe(psr.ParentID, "no", old, psr);
            //                LoadData1();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("不是正确的负荷数据行,请选择正确的行！");
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("请选择要导入的负荷数据行！");
            //}
            string conn = "ParentID='0' and ProjectID='" + GetProjectID + "'";
            IList cList = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", conn);
            if (cList.Count > 0)
            {
                FrmImportSel frm1 = new FrmImportSel();
                frm1.BindList = cList;
                if (frm1.ShowDialog() == DialogResult.OK)
                {
                    FormLoadForecastData frm = new FormLoadForecastData();
                    frm.ProjectUID = GetProjectID;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        FrmFKbi fkb = new FrmFKbi();
                        if (fkb.ShowDialog() == DialogResult.OK)
                        {
                            DataRow row = frm.ROW;
                            foreach (string str in frm1.OutList)
                            {
                                IList tempList = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", "ParentID='" + str + "' and ProjectID='" + GetProjectID + "' and Col1='1' and Title='一、110千伏以下负荷'");
                                if (tempList.Count > 0)
                                {
                                    Ps_Table_220Result psr = tempList[0] as Ps_Table_220Result;
                                    Ps_Table_220Result old = (Ps_Table_220Result)psr.Clone();
                                    for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
                                    {
                                        psr.GetType().GetProperty("yk" + i.ToString()).SetValue(psr, Math.Round(double.Parse(row["y" + i.ToString()].ToString()), 1), null);
                                        psr.GetType().GetProperty("yf" + i.ToString()).SetValue(psr, Math.Round(double.Parse(row["y" + i.ToString()].ToString()) * fkb.GetVal, 1), null);
                                    }
                                    Common.Services.BaseService.Update<Ps_Table_220Result>(psr);
                                    UpdateFuHe(psr.ParentID, "no", old, psr);
                                }
                            }
                            LoadData1();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("没有数据！");
            }
        }
        public void ClearChen()
        {
            IList pareList = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", "ProjectID='" + GetProjectID +
                            "' and ParentID='0' and BuildIng not in (Select ID From PS_Table_AreaWH where ProjectID='" +
                            GetProjectID + "')");
            for (int i = 0; i < pareList.Count; i++)
            {
                Common.Services.BaseService.Delete<Ps_Table_220Result>(pareList[i] as Ps_Table_220Result);
                DelAll((pareList[i] as Ps_Table_220Result).ID);
            }
            LoadData1();
        }
        public void UpdateChen()
        {
            IList pareList = Common.Services.BaseService.GetList("SelectPS_Table_AreaWHByConn", "ProjectID='" + GetProjectID +
                "'");
            for (int i = 0; i < pareList.Count; i++)
            {
                IList pareList1 = Common.Services.BaseService.GetList("SelectPs_Table_220ResultByConn", "ProjectID='" + GetProjectID +
                                "' and ParentID='0' and BuildIng='" + (pareList[i] as PS_Table_AreaWH).ID + "'");
                if (pareList1.Count > 0)
                {
                    if ((pareList1[0] as Ps_Table_220Result).Title != (pareList[i] as PS_Table_AreaWH).Title)
                    {
                        (pareList1[0] as Ps_Table_220Result).Title = (pareList[i] as PS_Table_AreaWH).Title;
                        Common.Services.BaseService.Update<Ps_Table_220Result>(pareList1[0]);
                        LoadData1();
                    }
                }
            }

        }
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ClearChen();
            UpdateChen();
            IList pareList = Common.Services.BaseService.GetList("SelectPS_Table_AreaWHByConn", "ProjectID='" + GetProjectID +
                "' and ID not in (Select BuildIng From Ps_Table_220Result where ProjectID='" +
                GetProjectID + "' and ParentID='0') order by sort");
            for (int i = 0; i < pareList.Count; i++)
            {
                Ps_Table_220Result table_yd = new Ps_Table_220Result();
                table_yd.ID += "|" + GetProjectID;
                table_yd.Title = ((PS_Table_AreaWH)pareList[i]).Title;
                table_yd.ParentID = "0";
                table_yd.Sort = OperTable.Get220ResultMaxSort() + 1;
                table_yd.ProjectID = GetProjectID;
                table_yd.BuildIng = ((PS_Table_AreaWH)pareList[i]).ID;
                for (int j = yAnge.BeginYear; j <= yAnge.EndYear; j++)
                {
                    table_yd.GetType().GetProperty("yf" + j.ToString()).SetValue(table_yd, null, null);
                    table_yd.GetType().GetProperty("yk" + j.ToString()).SetValue(table_yd, null, null);
                }
                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_220Result", table_yd);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加城区出错：" + ex.Message);
                }
                string ParId = "", Heid = "";
                string[] lei = new string[7] { "一、110千伏以下负荷", "二、110千伏直供负荷", "三、220千伏变电站低压侧供电负荷", "四、110千伏及以下地方电源出力", "五、外网110千伏及以下送入电力", "六、外网110千伏及以下送出电力", "七、220千伏供电负荷" };
                for (int k = 0; k < lei.Length; k++)
                {
                    Ps_Table_220Result table1 = new Ps_Table_220Result();
                    table1.ID += "|" + GetProjectID;
                    table1.Title = lei[k];
                    table1.ParentID = table_yd.ID;
                    table1.ProjectID = GetProjectID;
                    table1.Col1 = Convert.ToString(k + 1);
                    if (k == 6)
                    {
                        table1.Col1 = "no";
                        Heid = table1.ID;
                    }
                    if (k == 3)
                        ParId = table1.ID;//,table_yd.Title);
                    table1.Sort = k + 1;
                    try
                    {
                        Common.Services.BaseService.Create("InsertPs_Table_220Result", table1);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("增加项目出错：" + ex.Message);
                    }
                }
                AddModelChild(ParId, table_yd.Title, Heid);
                this.Cursor = Cursors.WaitCursor;
                treeList1.BeginUpdate();
                //treeList1.ExpandAll();
                LoadData1();
                treeList1.EndUpdate();
                this.Cursor = Cursors.Default;
            }
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewForK("yf", barCheckItem1.Checked);
        }
        public void viewForK(string str, bool check)
        {
            int[] year = GetYears();
            if (check)
            {
                for (int i = year[1]; i <= year[2]; i++)
                {
                    treeList1.Columns[str + i.ToString()].VisibleIndex = i;
                }
            }
            else
            {
                for (int i = year[1]; i <= year[2]; i++)
                {
                    treeList1.Columns[str + i.ToString()].VisibleIndex = -1;
                }
            }
        }
        private void barCheckItem2_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            viewForK("yk", barCheckItem2.Checked);
        }
    }
}