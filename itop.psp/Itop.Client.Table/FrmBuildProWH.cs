﻿using System;
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
using Itop.Domain.Graphics;
using DevExpress.Utils;

namespace Itop.Client.Table
{
    public partial class FrmBuildProWH : FormBase
    {
        DataTable dataTable;
        private bool IS_FirstLoad = true;
        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;
        private DataCommon dc = new DataCommon();
        TreeListNode treenode;
        private int typeFlag2 = 1;
        private OperTable oper = new OperTable();
        public Ps_YearRange yAnge = new Ps_YearRange();
        public Ps_YearRange yAngeXs = new Ps_YearRange();
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

        public FrmBuildProWH()
        {
            InitializeComponent();
            barSubItem1.Glyph = Itop.ICON.Resource.新建;
            barButtonItem2.Glyph = Itop.ICON.Resource.修改;
            barButtonItem10.Glyph = Itop.ICON.Resource.修改;
            barButtonItem15.Glyph = Itop.ICON.Resource.修改;
            barButtonItem7.Glyph = Itop.ICON.Resource.关闭;

            barButtonItem19.Glyph = Itop.ICON.Resource.打回重新编;
            barButtonItem20.Glyph = Itop.ICON.Resource.打回重新编;
            barButtonItem22.Glyph = Itop.ICON.Resource.布局;


        }
        string tong = "",g_col4="";
        public void ShowBian()
        {
            this.Show();
            barButtonItem18.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tong = "Col4='bian' and ";
            g_col4 = "bian";
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }
        public void ShowLine()
        {
            this.Show();
            barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            tong = "Col4='line' and ";
            g_col4 = "line";
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }
        private void HideToolBarButton()
        {
            if (!base.AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem18.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.EditRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.DeleteRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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
            yAnge = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.tzgs + "'");
            yAngeXs = oper.GetYearRange("Col5='" + GetProjectID + "' and Col4='" + OperTable.tzgsxs + "'");
            Show();
            initpasteCols();
            if (IS_FirstLoad)
            {
                IS_FirstLoad = false;
                ShowBian();
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
                        ((Ps_Table_BuildPro)listTypes[i]).BefVolumn += double.Parse(listTypes[i].GetType().GetProperty("y" + j).GetValue(listTypes[i], null).ToString());
                        ((Ps_Table_BuildPro)listTypes[i]).AftVolumn -= double.Parse(listTypes[i].GetType().GetProperty("y" + j).GetValue(listTypes[i], null).ToString());
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

            string con = tong + "ProjectID='" + GetProjectID + "'";
            listTypes = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", con);
           // CalcYearVol();
          //  AddTotalRow(ref listTypes);
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_BuildPro));
            //dataTable = dc.GetSortTable(dataTable, "Flag", true);
            treeList1.DataSource = dataTable;
            for (int i = 0; i < treeList1.Columns.Count; i++)
            {
                treeList1.Columns[i].VisibleIndex = -1;
            }
           
            Ps_YearRange yr = yAnge;
            treeList1.Columns["AreaName"].Caption = "城区";
            treeList1.Columns["AreaName"].Width = 150;
            treeList1.Columns["AreaName"].MinWidth = 150;
            treeList1.Columns["AreaName"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["AreaName"].OptionsColumn.AllowSort = false;
            if(g_col4=="bian")
                treeList1.Columns["AreaName"].VisibleIndex = 1;
            else
                treeList1.Columns["AreaName"].VisibleIndex = -1;

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
            treeList1.Columns["BuildYear"].VisibleIndex = 4;
            treeList1.Columns["BuildEd"].Caption = "竣工年限";
            treeList1.Columns["BuildEd"].Width = 100;
            treeList1.Columns["BuildEd"].MinWidth = 100;
            treeList1.Columns["BuildEd"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["BuildEd"].OptionsColumn.AllowSort = false;
            treeList1.Columns["BuildEd"].VisibleIndex = 5;
            treeList1.Columns["Col3"].Caption = "建设性质";
            treeList1.Columns["Col3"].Width = 120;
            treeList1.Columns["Col3"].MinWidth = 120;
            treeList1.Columns["Col3"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Col3"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Col3"].VisibleIndex = 2;
            treeList1.Columns["Flag"].Caption = "导线型号";
            treeList1.Columns["Flag"].Width = 120;
            treeList1.Columns["Flag"].MinWidth = 120;
            treeList1.Columns["Flag"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Flag"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Flag"].VisibleIndex = 3;
            if(g_col4=="bian")
                treeList1.Columns["Flag"].VisibleIndex = -1;

            treeList1.Columns["Length"].Caption = "长度";
            treeList1.Columns["Length"].Width = 120;
            treeList1.Columns["Length"].MinWidth = 120;
            treeList1.Columns["Length"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Length"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Length"].VisibleIndex = -1;
            treeList1.Columns["Volumn"].Caption = "容量";
            treeList1.Columns["Volumn"].Width = 120;
            treeList1.Columns["Volumn"].MinWidth = 120;
            treeList1.Columns["Volumn"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Volumn"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Volumn"].VisibleIndex = -1;
            treeList1.Columns["AllVolumn"].Caption = "总投资";
            treeList1.Columns["AllVolumn"].Width = 100;
            treeList1.Columns["AllVolumn"].MinWidth = 100;
            treeList1.Columns["AllVolumn"].OptionsColumn.AllowEdit = true;
            treeList1.Columns["AllVolumn"].OptionsColumn.AllowSort = false;
            treeList1.Columns["AllVolumn"].VisibleIndex = -1;
            treeList1.Columns["BefVolumn"].Caption = Convert.ToString(yr.StartYear)+"年底投资";
            treeList1.Columns["BefVolumn"].Width = 100;
            treeList1.Columns["BefVolumn"].MinWidth = 100;
            treeList1.Columns["BefVolumn"].OptionsColumn.AllowEdit = true;
            treeList1.Columns["BefVolumn"].OptionsColumn.AllowSort = false;
            treeList1.Columns["BefVolumn"].VisibleIndex = -1;
            treeList1.Columns["AftVolumn"].Caption = Convert.ToString(yr.StartYear + 1) + "~" + Convert.ToString(yr.StartYear + 5) + "投资合计";
            treeList1.Columns["AftVolumn"].Width = 150;
            treeList1.Columns["AftVolumn"].MinWidth = 150;
            treeList1.Columns["AftVolumn"].OptionsColumn.AllowEdit = true;
            treeList1.Columns["AftVolumn"].OptionsColumn.AllowSort = false;
            treeList1.Columns["AftVolumn"].VisibleIndex = -1;
            CalcYearColumn();
            for (int i = 2; i <= 4; i++)
            {
                if (i != 3)
                {
                    treeList1.Columns["Col" + i.ToString()].VisibleIndex = -1;
                    treeList1.Columns["Col" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
                }
            }

            treeList1.Columns["Col1"].Caption = "备注";
            treeList1.Columns["Col1"].Width = 300;
            treeList1.Columns["Col1"].MinWidth = 300;
            treeList1.Columns["Col1"].OptionsColumn.AllowEdit = true;
            treeList1.Columns["Col1"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Col1"].VisibleIndex = 1000;
            treeList1.Columns["Sort"].VisibleIndex = -1;
            treeList1.Columns["ProjectID"].VisibleIndex = -1;
            treeList1.Columns["FromID"].VisibleIndex = -1;
            treeList1.Columns["BianInfo"].VisibleIndex = -1;
            treeList1.Columns["LineInfo"].VisibleIndex = -1;
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
            string con = tong + "ProjectID='" + GetProjectID + "'";
            listTypes = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", con);
           // CalcYearVol();
            //IList cloneList = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", con);
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
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_BuildPro));
            //dataTable = dc.GetSortTable(dataTable, "Flag", true);

            treeList1.DataSource = dataTable;
           // SetValueNull();
            //treeList1.ExpandAll();
        }
        string totoalParent;
        public void AddTotalRow(ref IList list)
        {
            //合计
            string conn = tong + "ParentID='0' and ProjectID='" + GetProjectID + "'";
            int[] year = GetYears();
            Ps_Table_BuildPro parent = new Ps_Table_BuildPro();
            parent.ID += "|" + GetProjectID;
            parent.ParentID = "0"; parent.Title = "合计"; parent.Sort = 10000;// OperTable.GetMaxSort() + 1;
            //
            IList<Ps_Table_BuildPro> teList = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", conn);
            double old=0.0,te=0.0;
            //for (int i = 0; i < teList.Count; i++)
            //{
            //    parent.AllVolumn += teList[i].AllVolumn;
            //    parent.BefVolumn += teList[i].BefVolumn;
            //    parent.AftVolumn += teList[i].AftVolumn;
            //    //for (int j = yAnge.StartYear + 1; j <= yAnge.StartYear + 5; j++)
            //    //{
            //    //    old=double.Parse(parent.GetType().GetProperty("y"+j.ToString()).GetValue(parent,null).ToString());
            //    //    te = double.Parse(teList[i].GetType().GetProperty("y"+j.ToString()).GetValue(teList[i],null).ToString());
            //    //    parent.GetType().GetProperty("y" + j.ToString()).SetValue(parent, old + te, null);
            //    //}
            //}
            list.Add(parent);
        }

        public void CalcYearColumn()
        {
            int[] year = GetYears();
            for (int i = year[0]; i < year[1]; i++)
            {
                treeList1.Columns["y" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
            }
            for (int i = year[2] + 1; i <= year[3]; i++)
            {
                treeList1.Columns["y" + i.ToString()].VisibleIndex = -1;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.ShowInCustomizationForm = false;
            }
            for (int i = year[1]; i <= year[2]; i++)
            {
                treeList1.Columns["y" + i.ToString()].Caption = i.ToString() + "年";
                
                treeList1.Columns["y" + i.ToString()].VisibleIndex = i;
                treeList1.Columns["y" + i.ToString()].Width = 60;
                if (i == year[1])
                {
                    treeList1.Columns["y" + i.ToString()].Caption += (g_col4=="bian"?"(MVA)":"(km)");
                    treeList1.Columns["y" + i.ToString()].Width = 100;
                }
                treeList1.Columns["y" + i.ToString()].OptionsColumn.AllowEdit = false;
                treeList1.Columns["y" + i.ToString()].OptionsColumn.AllowSort = false;

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
            if ( focusedNode.GetValue("ParentID").ToString()!="0")
            {
                //MsgBox.Show( focusedNode.GetValue("Title").ToString()+"不允许添加子分类！");
                //return;
                focusedNode = focusedNode.ParentNode;
            }

            FrmAddTzgsBBPW frm = new FrmAddTzgsBBPW();
            frm.Text = "增加" + focusedNode.GetValue("Title") + "的变电工程";
            frm.Stat = focusedNode.GetValue("Col2").ToString();
            frm.buildprortzgsflag = false;
            frm.Line = false;
            frm.StrType = "bian";
            frm.operatorflag = true;
            frm.TzgsXs = double.Parse(yAngeXs.Col1);
            frm.AreaName = focusedNode.GetValue("AreaName").ToString();
            try
            {
                frm.V = int.Parse(focusedNode.GetValue("FromID").ToString());
            }
            catch { }
            frm.ProjectID = ProjectUID;
           // frm.SetLabelName = "子分类名称";
            if(frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_BuildPro table1 = new Ps_Table_BuildPro();
                table1.ID += "|" + GetProjectID;
                table1.Title = frm.ParentName;
                table1.ParentID = focusedNode.GetValue("ID").ToString();
                table1.ProjectID = GetProjectID;
                table1.BuildYear = frm.StartYear;
                table1.BuildEd = frm.FinishYear;
                table1.FromID = frm.GetFromID;
                table1.Length = frm.LineLen;
                table1.Volumn = frm.Vol;
                table1.AllVolumn = frm.AllVol;
              //  table1.BefVolumn = frm.AllVol;
                table1.AftVolumn = frm.AllVol;
                table1.LineInfo = frm.LineInfo;
                table1.BianInfo = frm.BianInfo;
                table1.GetType().GetProperty("y" + Convert.ToString(frm.StartYear)).SetValue(table1, frm.Vol, null);
                table1.Col4 = "bian";
                table1.Sort = OperTable.GetBuildProMaxSort()+1;
                table1.Col3 = frm.Col3;
                table1.Col1 = frm.BieZhu;
                table1.AreaName = frm.AreaName;
                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_BuildPro", table1);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table1, dataTable.NewRow()));
                    AddChildVol(table1, true);
                    LoadData1();
                    FoucsLocation(table1.ID, treeList1.Nodes);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加变电站出错：" + ex.Message);
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
                FrmAddBuild frm = new FrmAddBuild();
                //frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
                frm.ParentName = treeList1.FocusedNode.GetValue("Title").ToString();
                frm.Text = "修改分类名";
                frm.SetLabelName = "分类名称";
                frm.GetV = treeList1.FocusedNode.GetValue("FromID").ToString();
                frm.Conn = tong + "ParentID='0' and ProjectID='" + GetProjectID + "' and FromID=";
                frm.BEdit = true;
                 
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Ps_Table_BuildPro table1 = new Ps_Table_BuildPro();
                    table1 = Common.Services.BaseService.GetOneByKey<Ps_Table_BuildPro>(treeList1.FocusedNode.GetValue("ID"));
                    table1.Title = frm.ParentName;
                    table1.FromID = frm.GetV;
                    table1.Col2 = "no";
                    try
                    {
                        Common.Services.BaseService.Update<Ps_Table_BuildPro>(table1);
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
                FrmAddTzgsBBPW frm = new FrmAddTzgsBBPW();
                frm.buildprortzgsflag = false;
                Ps_Table_BuildPro table = new Ps_Table_BuildPro();
                table = Common.Services.BaseService.GetOneByKey<Ps_Table_BuildPro>(treeList1.FocusedNode.GetValue("ID"));
                frm.StrType = table.Col4;
                frm.ParentName = treeList1.FocusedNode.GetValue("Title").ToString();
                frm.operatorflag = false;
                try
                {
                    frm.V = int.Parse(treeList1.FocusedNode.GetValue("FromID").ToString());
                }
                catch { }
                frm.AreaName = treeList1.FocusedNode.GetValue("AreaName").ToString();
                frm.ProjectID = ProjectUID;
                if (treeList1.FocusedNode.GetValue("Col4") != null)
                {
                    if (treeList1.FocusedNode.GetValue("Col4").ToString() == "line")
                        frm.Line = true;
                }
                frm.Text = "修改工程";
                frm.Stat = treeList1.FocusedNode.ParentNode.GetValue("Col2").ToString();
                frm.BianInfo = table.BianInfo;
                frm.LineInfo = table.LineInfo;
                frm.StartYear = table.BuildYear;
                frm.FinishYear = table.BuildEd;
               
                frm.LineLen = table.Length;
                frm.BieZhu = table.Col1;
                frm.Col3 = table.Col3;
                frm.Vol = table.Volumn;
               
                frm.TzgsXs = double.Parse(yAngeXs.Col1);
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
                    for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
                    {
                        table.GetType().GetProperty("y" + i).SetValue(table, 0, null);
                    }
                    double temp1 = 0.0;
                    if (g_col4 == "bian")
                        temp1 = frm.Vol;
                    else if (g_col4 == "line")
                        temp1 = frm.LineLen;
                    table.GetType().GetProperty("y" + Convert.ToString(frm.StartYear)).SetValue(table, temp1, null);
                    table.Col1 = frm.BieZhu;
                    table.Col3 = frm.Col3;
                    table.BianInfo = frm.BianInfo;
                    table.LineInfo = frm.LineInfo;
                    table.Flag = frm.GetFlag;
                    table.FromID = frm.GetFromID;
                    table.AreaName = frm.AreaName;
                    try
                    {
                        Common.Services.BaseService.Update<Ps_Table_BuildPro>(table);
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
                Ps_Table_BuildPro table1 = new Ps_Table_BuildPro();
               // Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList1.FocusedNode);
                table1.ID = treeList1.FocusedNode.GetValue("ID").ToString();

                try
                {
                    //DeletePSP_ValuesByType里面删除数据和分类
                    TreeListNode node1 = treeList1.FocusedNode.NextNode;
                    string teID = "";
                    if (node1 != null)
                        teID = node1.GetValue("ID").ToString();
                    AddChildVol(Common.Services.BaseService.GetOneByKey<Ps_Table_BuildPro>(table1.ID), false);
                    Common.Services.BaseService.Delete <Ps_Table_BuildPro>(table1);//("DeletePs_Table_BuildPro", table1);
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

        public void AddChildVol(Ps_Table_BuildPro child,bool bAdd)
        {
            Ps_Table_BuildPro pare = Common.Services.BaseService.GetOneByKey<Ps_Table_BuildPro>(child.ParentID);

            IList<Ps_Table_BuildPro> list = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", " ParentID='" + child.ParentID + "'");
           
            for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
            {
                double tempdb = 0;
                foreach (Ps_Table_BuildPro  ptb in list)
                {
                    tempdb += double.Parse(ptb.GetType().GetProperty("y" + i.ToString()).GetValue(ptb, null).ToString());
                }
                pare.GetType().GetProperty("y" + i.ToString()).SetValue(pare, tempdb, null);
            }
            Common.Services.BaseService.Update<Ps_Table_BuildPro>(pare);


           // IList<string> list=new List<string>();
           // list.Add("Length");list.Add("Volumn");//list.Add("AllVolumn");list.Add("BefVolumn");
           //// list.Add("AftVolumn");
           //// for (int i = 2008; i <= yAnge.StartYear + 5; i++)
           ////     list.Add("y" + i.ToString());
           // double old=0.0,cld=0.0;
           //// foreach (string str in list)
           // for(int i=yAnge.BeginYear;i<=yAnge.EndYear;i++)
           // {
           //     old=double.Parse(pare.GetType().GetProperty("y"+i.ToString()).GetValue(pare,null).ToString());
           //     cld = double.Parse(child.GetType().GetProperty("y" + i.ToString()).GetValue(child, null).ToString());
           //     if (bAdd)
           //         pare.GetType().GetProperty("y" + i.ToString()).SetValue(pare, old + cld, null);
           //     else
           //         pare.GetType().GetProperty("y" + i.ToString()).SetValue(pare, old - cld, null);
           // }
           // Common.Services.BaseService.Update<Ps_Table_BuildPro>(pare);
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
        public void CalcYearVol(TreeListNode node, double vol,string year)
        {
            double v = vol - double.Parse(lastEditValue);
            double temp = 0.0;
            double tot=double.Parse(node.GetValue("y"+Convert.ToString(yAnge.StartYear+1)).ToString());
            if (tot >= v)
            {
                node.SetValue("y" + Convert.ToString(yAnge.StartYear + 1), tot - v);
                //node.SetValue("AftVolumn", double.Parse(node.GetValue("AftVolumn").ToString())-v);
               // node.ParentNode.SetValue("AftVolumn", double.Parse(node.ParentNode.GetValue("AftVolumn").ToString()) - v);
                node.ParentNode.SetValue("y" + Convert.ToString(yAnge.StartYear + 1), double.Parse(node.ParentNode.GetValue("y" + Convert.ToString(yAnge.StartYear + 1)).ToString()) - v);
                node.SetValue(year, vol);
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
                CalcNotZero(node, double.Parse(node.GetValue(column.FieldName).ToString()));
            }
            else if (column.FieldName == "AllVolumn")
            {
                CalcNotAll(node, double.Parse(node.GetValue(column.FieldName).ToString()));
            }
            else if (column.FieldName.StartsWith("y") && column.FieldName!="y"+Convert.ToString(yAnge.StartYear+1))
            {
                CalcYearVol(node, double.Parse(node.GetValue(column.FieldName).ToString()), column.FieldName);
            }
            bPast = false;
        }
        public void CalcGong(TreeListNode node, TreeListColumn column,double cha)
        {
            node.ParentNode.SetValue(column.FieldName, double.Parse(node.ParentNode.GetValue(column.FieldName).ToString())+cha);
        }
        bool bPast = false;
      //  double totalvalue = 0.0;
        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (bPast)
                return;
            if (e.Value != null) 
            if ((e.Value.ToString() != lastEditValue
                || lastEditNode != e.Node
                || lastEditColumn != e.Column)
                && (e.Column.Caption.IndexOf("年") > 0 || e.Column.FieldName=="AllVolumn"))
            {

                if (double.Parse(e.Value.ToString()) > double.Parse(e.Node.GetValue("AllVolumn").ToString()))
                {
                    MessageBox.Show("不能大于总投资", "错误");
                    return;
                }
                CalcVolumn(e.Node, e.Column);
                DataRow row = (e.Node.TreeList.GetDataRecordByNode(e.Node) as DataRowView).Row;
                Ps_Table_BuildPro tzgs = DataConverter.RowToObject<Ps_Table_BuildPro>(row);
                Common.Services.BaseService.Update<Ps_Table_BuildPro>(tzgs);
                CalcGong(e.Node, e.Column, double.Parse(e.Value.ToString()) - double.Parse(lastEditValue));
                DataRow row1 = (e.Node.ParentNode.TreeList.GetDataRecordByNode(e.Node.ParentNode) as DataRowView).Row;
                Ps_Table_BuildPro tzgs1 = DataConverter.RowToObject<Ps_Table_BuildPro>(row1);
                Common.Services.BaseService.Update<Ps_Table_BuildPro>(tzgs1);
              
                  
            }

           
        }

        private bool SaveCellValue(string year, string typeID, double value)
        {
            Ps_Table_BuildPro psp = new Ps_Table_BuildPro();
            Ps_Table_BuildPro old = Common.Services.BaseService.GetOneByKey<Ps_Table_BuildPro>(typeID);
            psp = (Ps_Table_BuildPro)old.Clone();
            psp.GetType().GetProperty(year).SetValue(psp, Math.Round(value,1),null);

            try
            {
                Common.Services.BaseService.Update<Ps_Table_BuildPro>(psp);
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
            if (name == "一、负荷")
                name = "一、负荷合计";

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
            if (treeList1.FocusedNode.GetValue("ParentID").ToString() == "0")
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
                        case "4":
                            total += Convert.ToDouble(value);
                            break;
                        case "2":
                        case "3":
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
            //FormChooseYears frm = new FormChooseYears();
            //for (int i = yAnge.StartYear; i <= yAnge.FinishYear; i++)
            //{
            //    frm.ListYearsForChoose.Add(i);

            //}
            //frm.NoIncreaseRate = true;

            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            string con = tong + "ProjectID='" + GetProjectID + "' ORDER BY Sort";
                listTypes = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", con);

              //  CalcYearVol();
             //   AddTotalRow(ref listTypes);
                DataTable dt = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_BuildPro));
                

                FrmResultPrint frma = new FrmResultPrint();
                IList<string> strTe = new List<string>();
                if (xtraTabControl1.SelectedTabPageIndex == 0)
                    frma.Line = true;
                else
                    frma.Line = false;
                for (int k = yAnge.StartYear; k <= yAnge.FinishYear; k++)
                    strTe.Add(k.ToString());
                frma.YearList1 = strTe;
                frma.IsSelect = _isSelect;
                frma.Text = "电网建设项目表";
                frma.Dw1 = "单位:千米、万千伏安";
                treeList1.DataSource = dt;
                frma.bTzgs = true;
                frma.IsBand = false;
                frma.BBuild = true;
                frma.GridDataTable = ResultDataTable(ConvertTreeListToDataTable(treeList1, false), strTe);
                
                listTypes = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", con);

               // CalcYearVol();
            DataTable dt1 = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(Ps_Table_BuildPro));
                treeList1.DataSource = dt1;
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

            listColID.Add("Flag");
            dt.Columns.Add("Flag", typeof(string));
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
            for (int i = yAnge.StartYear; i <= yAnge.FinishYear; i++)
            {
                listColID.Add("y" + i.ToString());
                dt.Columns.Add("y" + i.ToString(), typeof(double));
            }
            for (int i = 1; i < 6; i++)
            {
                listColID.Add("Col" + i.ToString());
                dt.Columns.Add("Col" + i.ToString(), typeof(string));
            }
            myN = 1; youN = 0;
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


            FrmAddBuild frm = new FrmAddBuild();
            frm.Conn = tong + "ParentID='0' and ProjectID='" + GetProjectID + "' and FromID=";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_BuildPro table_yd = new Ps_Table_BuildPro();
                table_yd.ID += "|" + GetProjectID;
                table_yd.Title = frm.ParentName;
                table_yd.ParentID = "0";
                table_yd.Sort = OperTable.GetBuildProMaxSort() + 1;
                table_yd.ProjectID = GetProjectID;
                table_yd.Col2 = frm.ParentName;
                table_yd.Col4 = g_col4;
                table_yd.FromID = frm.GetV;
                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_BuildPro", table_yd);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加分类出错：" + ex.Message);
                }
                
                this.Cursor = Cursors.WaitCursor;
                treeList1.BeginUpdate();
                //treeList1.ExpandAll();
                LoadData1();
                FoucsLocation(table_yd.ID, treeList1.Nodes);
                treeList1.EndUpdate();
                this.Cursor = Cursors.Default;
            }
        }

        public void AddModelChild(string id,string pid)
        {

            Ps_Table_BuildPro table1 = new Ps_Table_BuildPro();
            table1.ID += "|" + GetProjectID;
            table1.Title = "其中：小水电";
            table1.ParentID = id;
            table1.ProjectID = GetProjectID;
            table1.Col1 = "child";
            table1.Col2 = "2";
            table1.Col3 = "shui";
            table1.Col4 = pid;
            table1.Sort = 0;
            Ps_Table_BuildPro table3 = new Ps_Table_BuildPro();
            table3.ID += "|" + GetProjectID;
            table3.Title = "其它";
            table3.Col1 = "child";
            table3.Col2 = "2";
            table3.ParentID = id;
            table3.ProjectID = GetProjectID;
            table3.Col3 = "other";
            table3.Col4 = pid;
            table3.Sort = 2;
            Ps_Table_BuildPro table2 = new Ps_Table_BuildPro();
            table2.ID += "|" + GetProjectID;
            table2.Title = "小火电";
            table2.Col1 = "child";
            table2.Col2 = "2";
            table2.ParentID = id;
            table2.ProjectID = GetProjectID;
            table2.Col3 = "huo";
            table2.Col4 = pid;
            table2.Sort = 1;
            Common.Services.BaseService.Create("InsertPs_Table_BuildPro", table1);
            Common.Services.BaseService.Create("InsertPs_Table_BuildPro", table3);
            Common.Services.BaseService.Create("InsertPs_Table_BuildPro", table2);
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
                Ps_Table_BuildPro table1 = new Ps_Table_BuildPro();
                // Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList1.FocusedNode);
                table1.ID = treeList1.FocusedNode.GetValue("ID").ToString();
                DelAll(table1.ID);
                try
                {
                    //DeletePSP_ValuesByType里面删除数据和分类
                    Common.Services.BaseService.Delete<Ps_Table_BuildPro>(table1);//("DeletePs_Table_BuildPro", table1);

                    
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
            IList<Ps_Table_BuildPro> list = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", conn);
            if (list.Count > 0)
            {
                foreach (Ps_Table_BuildPro var in list)
                {
                    string child = var.ID;
                    DelAll(child);
                    Ps_Table_BuildPro ny = new Ps_Table_BuildPro();
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
            frm.ClassName = "Ps_Table_BuildPro";
            //if(g_col4=="bian")
            //    frm.SelectString = "SelectPs_Table_BuildProByConn1";
            //else if (g_col4 == "line")
                frm.SelectString = "SelectPs_Table_BuildProByConn";
            frm.InsertString = "InsertPs_Table_BuildPro";
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
                string conn = tong + " ProjectID='" + GetProjectID + "'";
                Common.Services.BaseService.Update("DeletePs_Table_BuildProByConn", conn);
                LoadData1();
            }
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

        public void UpdateFuHe(string pid,string col1,Ps_Table_BuildPro oldrs,Ps_Table_BuildPro newrs)
        {
            string conn = tong + "ProjectID='" + GetProjectID + "' and Col1='" + col1 + "' and ParentID='" + pid + "'";
            IList list = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", conn);
            if (list.Count > 0)
            {
                for(int i=yAnge.BeginYear;i<=yAnge.EndYear;i++)
                {
                    double oldyf = double.Parse(oldrs.GetType().GetProperty("yf"+i.ToString()).GetValue(oldrs,null).ToString());
                    double oldyk = double.Parse(oldrs.GetType().GetProperty("yk" + i.ToString()).GetValue(oldrs, null).ToString());
                    double newyf = double.Parse(newrs.GetType().GetProperty("yf" + i.ToString()).GetValue(newrs, null).ToString());
                    double newyk = double.Parse(newrs.GetType().GetProperty("yk" + i.ToString()).GetValue(newrs, null).ToString());
                    double myyf = double.Parse(list[0].GetType().GetProperty("yf" + i.ToString()).GetValue(list[0], null).ToString());
                    double myyk = double.Parse(list[0].GetType().GetProperty("yk" + i.ToString()).GetValue(list[0], null).ToString());
                    list[0].GetType().GetProperty("yf" + i.ToString()).SetValue(list[0], Math.Round(myyf - oldyf + newyf,1), null);
                    list[0].GetType().GetProperty("yk" + i.ToString()).SetValue(list[0], Math.Round(myyk - oldyk + newyk,1), null);
                }
                Common.Services.BaseService.Update<Ps_Table_BuildPro>((Ps_Table_BuildPro)list[0]);
            }
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // string conn = "ProjectID='"+GetProjectID+"' and Col1='1' and ParentID in (Select ID from Ps_Table_BuildPro where ParentID='0')";
            //  IList list = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", conn);
            if (this.treeList1.FocusedNode != null)
            {
                string nTitle = this.treeList1.FocusedNode.GetValue("Title").ToString();
                string nId = this.treeList1.FocusedNode.GetValue("Col1").ToString();
                if (nTitle == "一、负荷" && nId == "1")
                {
                    Ps_Table_BuildPro psr = Common.Services.BaseService.GetOneByKey<Ps_Table_BuildPro>(this.treeList1.FocusedNode.GetValue("ID").ToString());
                    Ps_Table_BuildPro old = (Ps_Table_BuildPro)psr.Clone();
                    FormLoadForecastData frm = new FormLoadForecastData();
                    frm.ProjectUID = GetProjectID;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        FrmFKbi fkb = new FrmFKbi();
                        if (fkb.ShowDialog() == DialogResult.OK)
                        {
                            DataRow row = frm.ROW;
                            for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
                            {
                                psr.GetType().GetProperty("yk" + i.ToString()).SetValue(psr, Math.Round(double.Parse(row["y" + i.ToString()].ToString()),1), null);
                                psr.GetType().GetProperty("yf" + i.ToString()).SetValue(psr, Math.Round(double.Parse(row["y" + i.ToString()].ToString())*fkb.GetVal,1), null);
                            }
                            Common.Services.BaseService.Update<Ps_Table_BuildPro>(psr);
                            UpdateFuHe(psr.ParentID, "no", old, psr);
                            LoadData1();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("不是正确的负荷数据行！");
                }
            }
            else
            {
                MessageBox.Show("请选择要导入的负荷数据行！");
            }
        }
        //更新
        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshData();
        }
        public Ps_Table_BuildPro GetTZGSParent(string type)
        {
            try
            {
                IList list = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", tong + "Col2='" + type + "' and ProjectID='" + GetProjectID + "' and ParentID='0'");
                Ps_Table_BuildPro guid = new Ps_Table_BuildPro();
                guid = null;
                if (list.Count > 0)
                    guid = (Ps_Table_BuildPro)list[0];
                return guid;
            }
            catch { return null; }
        }
        public double GetBianQ(double type, string name,string s1,ref string n)
        {
            string Name=(name.IndexOf("扩建")==-1?"新建":"扩建");
            string t="1×"+type*10+"MVA";
            string t1 = "1×" + type * 10 + " MVA";
            string n1=Name+s1+"kV变电站";
            n = "常规@" + n1 + "@";
            if (type >= 10)
                n += t;
            else
                n += t1;
            IList<Project_Sum> list = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByValues", "(T5='"+t+"' or T5='"+t1+"') and Name='"+n1+"' and S5='2'");
            if (list.Count > 0)
                return list[0].Num;
            return 0;
        }
        public void RefreshData()
        {
            string n = "";
            string conn = "ProjectID='" + GetProjectID + "' and Status!='拆除' and FinishYear>'" + Convert.ToString(yAnge.StartYear) + "' and FinishYear<'" + Convert.ToString(yAnge.StartYear + 6) + "' and (Col4='" + OperTable.ph110 + "' or Col4='" + OperTable.ph220 + "' or Col4='" + OperTable.ph500 + "') and ParentID not in (Select FromID from Ps_Table_BuildPro where ProjectID = '" + GetProjectID + "')";
            IList<Ps_Table_Edit> list = Common.Services.BaseService.GetList<Ps_Table_Edit>("SelectPs_Table_EditListByConn", conn);
            for (int i = 0; i < list.Count; i++)
            {
                n = "";
                if (list[i].Col4 == OperTable.ph110)
                {
                    Ps_Table_100PH table = Common.Services.BaseService.GetOneByKey<Ps_Table_100PH>(list[i].ParentID);
                    if (table != null)
                    {
                        Ps_Table_BuildPro pare = new Ps_Table_BuildPro();
                        pare = GetTZGSParent("110");
                        Ps_Table_BuildPro tzgs = new Ps_Table_BuildPro();
                        if (pare != null)
                            tzgs.ParentID = pare.ID;
                        else
                        {
                            pare = new Ps_Table_BuildPro();
                            pare.ID += "|" + GetProjectID;
                            pare.Col2 = "110";
                            pare.Title = "110千伏送变电工程";
                            pare.ParentID = "0";
                            pare.ProjectID = GetProjectID;
                            pare.Sort = 2;
                            Common.Services.BaseService.Create<Ps_Table_BuildPro>(pare);
                            tzgs.ParentID = pare.ID;
                        }
                        tzgs.ID += "|" + GetProjectID; 
                        tzgs.FromID = table.ID;
                        tzgs.ProjectID = GetProjectID;
                        tzgs.Sort = OperTable.GetBuildProMaxSort() + 1;
                        tzgs.Title = table.Title+"工程";
                        tzgs.BuildYear = list[i].StartYear;
                        tzgs.BuildEd = list[i].FinishYear;
                        tzgs.Volumn = double.Parse(list[i].Volume);
                        tzgs.Col3 = list[i].Status;
                        tzgs.AftVolumn = GetBianQ(double.Parse(list[i].Volume), list[i].Status, "110",ref n);
                        tzgs.BianInfo = n;
                        tzgs.Col4 = list[i].Col4;
                        tzgs.AllVolumn = tzgs.AftVolumn;
                      //  tzgs.GetType().GetProperty("y" + Convert.ToString(yAnge.StartYear + 1)).SetValue(tzgs, tzgs.AllVolumn, null);
                        pare.AftVolumn += tzgs.AftVolumn;
                        pare.AllVolumn += tzgs.AllVolumn;
                        pare.Length += tzgs.Length;
                        pare.Volumn += tzgs.Volumn;
                     //   pare.GetType().GetProperty("y" + Convert.ToString(yAnge.StartYear + 1)).SetValue(pare, double.Parse(pare.GetType().GetProperty("y" + Convert.ToString(yAnge.StartYear + 1)).GetValue(pare, null).ToString()) + tzgs.AllVolumn, null);
                        Common.Services.BaseService.Update<Ps_Table_BuildPro>(pare);
                        Common.Services.BaseService.Create<Ps_Table_BuildPro>(tzgs);
                    }
                }
                else if (list[i].Col4 == OperTable.ph220)
                {
                    Ps_Table_200PH table = Common.Services.BaseService.GetOneByKey<Ps_Table_200PH>(list[i].ParentID);
                    if (table != null)
                    {
                        Ps_Table_BuildPro pare = new Ps_Table_BuildPro();
                        pare = GetTZGSParent("220");
                        Ps_Table_BuildPro tzgs = new Ps_Table_BuildPro();
                        if (pare != null)
                            tzgs.ParentID = pare.ID;
                        else
                        {
                            pare = new Ps_Table_BuildPro();
                            pare.ID += "|" + GetProjectID;
                            pare.Col2 = "220";
                            pare.Title = "220千伏送变电工程";
                            pare.ParentID = "0";
                            pare.ProjectID = GetProjectID;
                            pare.Sort = 1;
                            Common.Services.BaseService.Create<Ps_Table_BuildPro>(pare);
                            tzgs.ParentID = pare.ID;
                        }
                        tzgs.ID += "|" + GetProjectID; 
                        tzgs.FromID = table.ID;
                        tzgs.ProjectID = GetProjectID;
                        tzgs.Sort = OperTable.GetBuildProMaxSort() + 1;
                        tzgs.Title = table.Title + "工程";
                        tzgs.BuildYear = list[i].StartYear;
                        tzgs.BuildEd = list[i].FinishYear;
                        tzgs.Volumn = double.Parse(list[i].Volume);
                        tzgs.Col3 = list[i].Status;
                        tzgs.Col4 = list[i].Col4;
                        tzgs.AftVolumn = GetBianQ(double.Parse(list[i].Volume), list[i].Status, "220",ref n);
                        tzgs.AllVolumn = tzgs.AftVolumn;
                        tzgs.BianInfo = n;
                     //   tzgs.GetType().GetProperty("y" + Convert.ToString(yAnge.StartYear + 1)).SetValue(tzgs, tzgs.AllVolumn, null);
                        pare.AftVolumn += tzgs.AftVolumn;
                        pare.AllVolumn += tzgs.AllVolumn;
                        pare.Length += tzgs.Length;
                        pare.Volumn += tzgs.Volumn;
                      //  pare.GetType().GetProperty("y" + Convert.ToString(yAnge.StartYear + 1)).SetValue(pare, double.Parse(pare.GetType().GetProperty("y" + Convert.ToString(yAnge.StartYear + 1)).GetValue(pare, null).ToString()) + tzgs.AllVolumn, null);
                        Common.Services.BaseService.Update<Ps_Table_BuildPro>(pare);
                        Common.Services.BaseService.Create<Ps_Table_BuildPro>(tzgs);
                    }
                }
                else if (list[i].Col4 == OperTable.ph500)
                {
                    Ps_Table_500PH table = Common.Services.BaseService.GetOneByKey<Ps_Table_500PH>(list[i].ParentID);
                    if (table != null)
                    {
                        Ps_Table_BuildPro pare = new Ps_Table_BuildPro();
                        pare = GetTZGSParent("500");
                        Ps_Table_BuildPro tzgs = new Ps_Table_BuildPro();
                        if (pare != null)
                            tzgs.ParentID = pare.ID;
                        else
                        {
                            pare = new Ps_Table_BuildPro();
                            pare.ID += "|" + GetProjectID;
                            pare.Col2 = "500";
                            pare.Title = "500千伏送变电工程";
                            pare.ParentID = "0";
                            pare.ProjectID = GetProjectID;
                            pare.Sort = 0;
                            Common.Services.BaseService.Create<Ps_Table_BuildPro>(pare);
                            tzgs.ParentID = pare.ID;
                        }
                        tzgs.ID += "|" + GetProjectID; 
                        tzgs.FromID = table.ID;
                        tzgs.ProjectID = GetProjectID;
                        tzgs.Sort = OperTable.GetBuildProMaxSort()+1;
                        tzgs.Title = table.Title + "工程";
                        tzgs.BuildYear = list[i].StartYear;
                        tzgs.BuildEd = list[i].FinishYear;
                        tzgs.Volumn = double.Parse(list[i].Volume);
                        tzgs.Col3 = list[i].Status;
                        tzgs.Col4 = list[i].Col4;
                        tzgs.AftVolumn = GetBianQ(double.Parse(list[i].Volume), list[i].Status, "500",ref n);
                        tzgs.AllVolumn = tzgs.AftVolumn;
                        tzgs.BianInfo = n;
                  //      tzgs.GetType().GetProperty("y" + Convert.ToString(yAnge.StartYear + 1)).SetValue(tzgs, tzgs.AllVolumn, null);
                        pare.AftVolumn += tzgs.AftVolumn;
                        pare.AllVolumn += tzgs.AllVolumn;
                        pare.Length += tzgs.Length;
                        pare.Volumn += tzgs.Volumn;
               //         pare.GetType().GetProperty("y" + Convert.ToString(yAnge.StartYear + 1)).SetValue(pare, double.Parse(pare.GetType().GetProperty("y" + Convert.ToString(yAnge.StartYear + 1)).GetValue(pare,null).ToString())+tzgs.AllVolumn, null);
                        Common.Services.BaseService.Update<Ps_Table_BuildPro>(pare);
                        Common.Services.BaseService.Create<Ps_Table_BuildPro>(tzgs);
                    }
                }
            }
            LoadData1();
            //string con = "ProjectID='" + GetProjectID + "'";
            //IList<Ps_Table_BuildPro> list2 = Common.Services.BaseService.GetList<Ps_Table_BuildPro>("SelectPs_Table_BuildProByConn", con);
            //for (int i = 0; i < list1.Count; i++)
            //{
            //    for (int j = 0; j < list2.Count; j++)
            //    {
 
            //    }
            //}
        }
        //重新获取
        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        //变电站造价
        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmProject_Sum fsum = new FrmProject_Sum();
            fsum.Type = "2";
            fsum.Text = "变电站造价信息";
            fsum.ShowDialog();
        }
        //线路造价
        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmProject_Sum fsum = new FrmProject_Sum();
            fsum.Type = "1";
            fsum.Text = "线路造价信息";
            fsum.ShowDialog();
    
        }

        private void treeList1_AfterDragNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            if (e.Node.ParentNode.GetValue("ParentID").ToString() != "0")
            {
                MessageBox.Show("只能拖到根目录下");
                LoadData1();
                treeList1.SetFocusedNode(e.Node);
                return;
            }
            Ps_Table_BuildPro old = Common.Services.BaseService.GetOneByKey<Ps_Table_BuildPro>(e.Node.GetValue("ID").ToString());
            AddChildVol(old, false);
            DataRow obj = (e.Node.TreeList.GetDataRecordByNode(e.Node) as DataRowView).Row ;
            Ps_Table_BuildPro tzgs = DataConverter.RowToObject<Ps_Table_BuildPro>(obj);
            Common.Services.BaseService.Update<Ps_Table_BuildPro>(tzgs);
            AddChildVol(tzgs, true);
            LoadData1();
            treeList1.SetFocusedNode(e.Node);
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
            if (focusedNode.GetValue("ParentID").ToString() != "0")
            {
                //MsgBox.Show(focusedNode.GetValue("Title").ToString() + "不允许添加子分类！");
                //return;
                focusedNode = focusedNode.ParentNode;
            }

            FrmAddTzgsBBPW frm = new FrmAddTzgsBBPW();
            frm.Text = "增加" + focusedNode.GetValue("Title") + "的线路工程";
            frm.Stat = focusedNode.GetValue("Col2").ToString();
            frm.Line = true;
            frm.StrType = "line";
            frm.buildprortzgsflag = false;
            frm.ProjectID = ProjectUID;
            // frm.SetLabelName = "子分类名称";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_BuildPro table1 = new Ps_Table_BuildPro();
                table1.ID += "|" + GetProjectID;
                table1.Title = frm.ParentName;
                table1.ParentID = focusedNode.GetValue("ID").ToString();
                table1.ProjectID = GetProjectID;
                table1.BuildYear = frm.StartYear;
                table1.BuildEd = frm.FinishYear;
                table1.FromID = frm.GetFromID;
                table1.Length = frm.LineLen;
                table1.Volumn = frm.Vol;
                table1.AllVolumn = frm.AllVol;
                //  table1.BefVolumn = frm.AllVol;
                table1.AftVolumn = frm.AllVol;
                table1.LineInfo = frm.LineInfo;
                table1.BianInfo = frm.BianInfo;
                table1.GetType().GetProperty("y" + Convert.ToString(frm.StartYear)).SetValue(table1, frm.LineLen, null);
                //table1.Col2 = treeList1.FocusedNode.GetValue("Col1").ToString();
                table1.Sort = OperTable.GetBuildProMaxSort() + 1;
                table1.Col3 = frm.Col3;
                table1.Col1 = frm.BieZhu;
                table1.Flag = frm.GetFlag;
                table1.Col4 = "line";
                try
                {
                    Common.Services.BaseService.Create("InsertPs_Table_BuildPro", table1);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table1, dataTable.NewRow()));
                    AddChildVol(table1, true);
                    LoadData1();
                    FoucsLocation(table1.ID, treeList1.Nodes);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加线路出错：" + ex.Message);
                }
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPageIndex == 0)
            {
                barButtonItem18.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem19.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tong = "Col4='bian' and ";
                g_col4 = "bian";
                Application.DoEvents();
                this.Cursor = Cursors.WaitCursor;
                treeList1.BeginUpdate();
                LoadData();
                treeList1.EndUpdate();
                this.Cursor = Cursors.Default;
                if (!base.AddRight)
                {
                    barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                   
                }
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 1)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem19.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barButtonItem18.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                tong = "Col4='line' and ";
                g_col4 = "line";
                Application.DoEvents();
                this.Cursor = Cursors.WaitCursor;
                treeList1.BeginUpdate();
                LoadData();
                treeList1.EndUpdate();
                this.Cursor = Cursors.Default; 
                if (!base.AddRight)
                {
                    barButtonItem18.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
            }
        }
        //更新变电站
        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int year = yAnge.StartYear;
            string con = "Col4='bian' and ProjectID='" + GetProjectID + "' and ParentID='0'";
            IList list = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", con);
            DataTable dt = Itop.Common.DataConverter.ToDataTable(list, typeof(Ps_Table_BuildPro));

            string con2 = "Col4='bian' and ProjectID='" + GetProjectID + "'";
            IList list2 = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", con2);
            DataTable dt2 = Itop.Common.DataConverter.ToDataTable(list2, typeof(Ps_Table_BuildPro));

            WaitDialogForm wait = new WaitDialogForm("", "正在更新变电站数据，请稍后...");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string parentid = dt.Rows[i]["ID"].ToString();
                string dy = dt.Rows[i]["FromID"].ToString();

                string conn1 = " AreaID='" + GetProjectID + "' and L1=" + dy;
                IList<PSP_Substation_Info> listbdz = Common.Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", conn1);
                int mm = 0;

                foreach (PSP_Substation_Info psi in listbdz)
                {
                    mm++;
                    int perent=(i+1) * 100*mm/listbdz.Count / dt.Rows.Count;
                    wait.SetCaption( perent+ "%");

                    string dyid = psi.UID;
                    string connjz = " where SvgUID ='" + dyid + "' and Type='03'";
                    IList<PSPDEV> listatt = Common.Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", connjz);
                    DataTable dataTable = Itop.Common.DataConverter.ToDataTable((IList)listatt, typeof(PSPDEV));
                    if (listatt.Count>0)
                    {

                        DataRow[] xyrow=dataTable.Select(" Date1<="+year);
                        DataRow[] dyrow=dataTable.Select(" Date1>"+year ,"Date1");

                        if (dyrow.Length>0)
	                    {
                            if (xyrow.Length>0)
	                        {
                                int start=int.Parse(dyrow[0]["Date1"].ToString());
                                int end=int.Parse(dyrow[dyrow.Length-1]["Date1"].ToString());
                                //扩建
                                for (int j = start; j <= end; j++)
			                    {
                                    DataRow[] curow=dataTable.Select(" Date1="+j );
                                    if (curow.Length==0)
                                    {
                                        continue;
                                    }
                                    Ps_Table_BuildPro table1 = new Ps_Table_BuildPro();
                                    table1.ID += "|" + GetProjectID;
                                    table1.Title = psi.Title;
                                    table1.ParentID = parentid;
                                    table1.ProjectID = GetProjectID;
                                    table1.BuildYear =j.ToString(); 
                                    table1.BuildEd = curow[0]["OperationYear"].ToString();
                                    table1.FromID = dy;
                                    table1.Volumn = double .Parse( curow[0]["SiN"].ToString());
                                 
                                    table1.Col4 = "bian";
                                    table1.Sort = OperTable.GetBuildProMaxSort() + 1;
                                    table1.Col10 = curow[0]["SUID"].ToString();
                                    table1.Col3="扩建";
                                    table1.AreaName = psi.AreaName;
                                    for (int k = 1; k < curow.Length; k++)
			                        {
                        			    table1.Volumn += double .Parse( curow[k]["SiN"].ToString());
                                        
			                        }
                                    table1.GetType().GetProperty("y" + j).SetValue(table1, table1.Volumn, null);
                                    try
                                    {
                                         if (DTHave(dt2,curow[0]["SUID"].ToString()))
                                            {

                                                 UPDateOldBDZ(dt2,curow[0]["SUID"].ToString(),table1);
                                            }
                                            else
                                            {
                                                Common.Services.BaseService.Create("InsertPs_Table_BuildPro", table1);
                                                dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table1, dataTable.NewRow()));
                                                AddChildVol(table1, true);
                                            }
                                      
                                    }
                                      catch (Exception ee)
                                    {
                                        wait.Close();
                                    }
			                    }


	                        }
                            else
	                        {
                                //新建+扩建
                                int start=int.Parse(dyrow[0]["Date1"].ToString());
                                int end=int.Parse(dyrow[dyrow.Length-1]["Date1"].ToString());
                                for (int j = start; j <= end; j++)
			                    {
                                    DataRow[] curow=dataTable.Select(" Date1="+j );
                                    if (curow.Length==0)
                                    {
                                        continue;
                                    }
                                    if (j==start)//只有第一年需要新建，其后的都是扩建
	                                {
                                        Ps_Table_BuildPro table1 = new Ps_Table_BuildPro();
                                        table1.ID += "|" + GetProjectID;
                                        table1.Title = psi.Title;
                                        table1.ParentID = parentid;
                                        table1.ProjectID = GetProjectID;
                                        table1.BuildYear = j.ToString();
                                        table1.BuildEd = curow[0]["OperationYear"].ToString();
                                        table1.FromID = dy;
                                        table1.Volumn = double .Parse( curow[0]["SiN"].ToString());
                                       
                                        table1.Col4 = "bian";
                                        table1.Sort = OperTable.GetBuildProMaxSort() + 1;
                                        table1.Col10 = curow[0]["SUID"].ToString();
                                        table1.Col3="新建";
                                        table1.AreaName = psi.AreaName;
                                        for (int k = 1; k < curow.Length; k++)
                                        {
                                            table1.Volumn += double.Parse(curow[k]["SiN"].ToString());
                                            
                                        }
                                         table1.GetType().GetProperty("y" + j).SetValue(table1, table1.Volumn, null);
                                        try
                                        {
                                                if (DTHave(dt2,curow[0]["SUID"].ToString()))
                                                {

                                                     UPDateOldBDZ(dt2,curow[0]["SUID"].ToString(),table1);
                                                }
                                                else
                                                {
                                                     Common.Services.BaseService.Create("InsertPs_Table_BuildPro", table1);
                                                     dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table1, dataTable.NewRow()));
                                                     AddChildVol(table1, true);
                                                }
                                            
                                        }
                                          catch (Exception ee)
                                        {
                                            wait.Close();
                                        }
                                          
	                                }
                                    else
	                                {
                                        
                                             Ps_Table_BuildPro table2 = new Ps_Table_BuildPro();
                                            table2.ID += "|" + GetProjectID;
                                            table2.Title = psi.Title;
                                            table2.ParentID = parentid;
                                            table2.ProjectID = GetProjectID;
                                            table2.BuildYear = j.ToString();
                                            table2.BuildEd =  curow[0]["OperationYear"].ToString();
                                            table2.FromID = dy;
                                            table2.Volumn = double .Parse( curow[0]["SiN"].ToString());
                                            
                                            table2.Col4 = "bian";
                                            table2.Sort = OperTable.GetBuildProMaxSort() + 1;
                                            table2.Col10 = curow[0]["SUID"].ToString();
                                            table2.Col3="扩建";
                                            table2.AreaName = psi.AreaName;
                                            for (int k = 1; k < curow.Length; k++)
		                                    {
                    			                table2.Volumn += double .Parse( curow[k]["SiN"].ToString());
                                               
		                                    }
                                            table2.GetType().GetProperty("y" + j).SetValue(table2, table2.Volumn, null);
                                            try
                                            {
                                                 if (DTHave(dt2,curow[0]["SUID"].ToString()))
                                                {

                                                     UPDateOldBDZ(dt2,curow[0]["SUID"].ToString(),table2);
                                                }
                                                else
                                                {
                                                     Common.Services.BaseService.Create("InsertPs_Table_BuildPro", table2);
                                                     dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table2, dataTable.NewRow()));
                                                     AddChildVol(table2, true);
                                                }

                                                
                                            }
                                              catch (Exception ee)
                                            {
                                                wait.Close();
                                            }
	                                }
                                     
                                   
			                    }


	                        }
	                    }

                    }
                    else
                    {
                        int curyear = 0;
                        int.TryParse(psi.L28, out curyear);
                        if (curyear> year)
	                    {
                           
                    	    Ps_Table_BuildPro table1 = new Ps_Table_BuildPro();
                            table1.ID += "|" + GetProjectID;
                            table1.Title = psi.Title;
                            table1.ParentID = parentid;
                            table1.ProjectID = GetProjectID;
                            table1.BuildYear = psi.L28;
                            table1.BuildEd = psi.S2;
                            table1.FromID = dy;
                            table1.Volumn = psi.L2;
                            
                            //  table1.BefVolumn = frm.AllVol;
                            table1.GetType().GetProperty("y" + psi.L28).SetValue(table1, psi.L2, null);
                            table1.Col4 = "bian";
                            table1.Sort = OperTable.GetBuildProMaxSort() + 1;
                            table1.Col10 = psi.UID;
                            table1.Col3="新建";
                            table1.AreaName = psi.AreaName;
                            try
                            {
                                 if (DTHave(dt2,psi.UID))
                                {

                                     UPDateOldBDZ(dt2,psi.UID,table1);
                                }
                                else
                                {
                                     Common.Services.BaseService.Create("InsertPs_Table_BuildPro", table1);
                                     dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table1, dataTable.NewRow()));
                                     AddChildVol(table1, true);
                                }
                               
                            }
                            catch (Exception ee)
                            {
                                wait.Close();
                            }
	                    }
                      

                    }

                }

             



            }
           
            LoadData1();
            wait.Close(); 

        }
        private void UPDateOldBDZ(DataTable dt, string condition, Ps_Table_BuildPro value)
        {
            DataRow[] dtrow = dt.Select("Col10='"+condition+"'");
            Ps_Table_BuildPro ptb = Itop.Common.DataConverter.RowToObject<Ps_Table_BuildPro>(dtrow[0]);
            ptb.Volumn = value.Volumn;
          
            ptb.BuildYear=value.BuildYear;
            ptb.BuildEd=value.BuildEd;
            for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
            {
                ptb.GetType().GetProperty("y" + i).SetValue(ptb, 0, null);

            }
            ptb.GetType().GetProperty("y" + value.BuildYear).SetValue(ptb, value.Volumn, null);
            try
            {
                Common.Services.BaseService.Update<Ps_Table_BuildPro>(ptb);
                AddChildVol(ptb, true);
            }
            catch (Exception ee)
            {
                
                throw;
            }
           

        }
        private void UPDateOldXl(DataTable dt, string condition, Ps_Table_BuildPro value)
        {
            DataRow[] dtrow = dt.Select("Col10='" + condition + "'");
            Ps_Table_BuildPro ptb = Itop.Common.DataConverter.RowToObject<Ps_Table_BuildPro>(dtrow[0]);
            ptb.Length = value.Length;
            
            ptb.BuildYear = value.BuildYear;
            ptb.BuildEd = value.BuildEd;
            for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
            {
                ptb.GetType().GetProperty("y" + i).SetValue(ptb, 0, null);

            }

            ptb.GetType().GetProperty("y" + value.BuildYear).SetValue(ptb, value.Length, null);
            try
            {
                Common.Services.BaseService.Update<Ps_Table_BuildPro>(ptb);
                AddChildVol(ptb, true);
            }
            catch (Exception ee)
            {

                throw;
            }


        }
        private bool DTHave(DataTable dt, string condition)
        {
            DataRow[] dtrow = dt.Select(" Col10='"+condition+"'");
            if (dtrow.Length>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //更新线路
        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Hashtable area_key_id = new Hashtable();
            //初始化哈希表
            string areaall = " ProjectID='" + GetProjectID + "'";
            IList<PS_Table_AreaWH> tempPTA = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", areaall);

            if (tempPTA.Count != 0)
            {
                for (int i = 0; i < tempPTA.Count; i++)
                {
                    area_key_id.Add(tempPTA[i].ID, tempPTA[i].Title);
                }
            }
      
            int year = yAnge.StartYear;
            string con = "Col4='line' and ProjectID='" + GetProjectID + "' and ParentID='0'";
            IList list = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", con);
            DataTable dt = Itop.Common.DataConverter.ToDataTable(list, typeof(Ps_Table_BuildPro));

            string con2 = "Col4='line' and ProjectID='" + GetProjectID + "'";
            IList list2 = Common.Services.BaseService.GetList("SelectPs_Table_BuildProByConn", con2);
            DataTable dt2 = Itop.Common.DataConverter.ToDataTable(list2, typeof(Ps_Table_BuildPro));

            WaitDialogForm wait = new WaitDialogForm("", "正在更新线路数据，请稍后...");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string parentid = dt.Rows[i]["ID"].ToString();
                string dy = dt.Rows[i]["FromID"].ToString();

                string connjz = " where ProjectID ='" + GetProjectID + "' and Type='05' and RateVolt=" + dy + " and Cast(Date1 as int)>" + year;
                IList<PSPDEV> listatt = Common.Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", connjz);
                int mm = 0;

                foreach (PSPDEV psi in listatt)
                {
                    mm++;
                    int perent = (i + 1) * 100 * mm / listatt.Count / dt.Rows.Count;
                    wait.SetCaption(perent + "%");

                    Ps_Table_BuildPro table1 = new Ps_Table_BuildPro();
                    table1.ID += "|" + GetProjectID;
                    table1.Title = psi.Name;
                    table1.ParentID = parentid;
                    table1.ProjectID = GetProjectID;
                    table1.BuildYear = psi.Date1;
                    table1.BuildEd = psi.OperationYear;
                    table1.FromID = dy;
                    table1.Length = psi.LineLength+psi.Length2;
                    
                    //  table1.BefVolumn = frm.AllVol;
                    table1.GetType().GetProperty("y" + psi.Date1).SetValue(table1, table1.Length, null);
                    table1.Col4 = "line";
                    table1.Sort = OperTable.GetBuildProMaxSort() + 1;
                    table1.Col10 = psi.SUID;
                    table1.Col3 = "新建";
                    if (area_key_id[psi.AreaID]!=null)
                    {
                        table1.AreaName = area_key_id[psi.AreaID].ToString();
                    }
                    
                    try
                    {
                        if (DTHave(dt2, psi.SUID))
                        {

                            UPDateOldXl(dt2, psi.SUID, table1);
                        }
                        else
                        {
                            Common.Services.BaseService.Create("InsertPs_Table_BuildPro", table1);
                            dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(table1, dataTable.NewRow()));
                            AddChildVol(table1, true);
                        }

                    }
                    catch (Exception ee)
                    {
                        wait.Close();
                    }

                   
                }
            } 
            LoadData1();
            wait.Close(); 
        }
        // 基础年设置
        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

      


       

    }
}