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
using Itop.Domain.HistoryValue;
using DevExpress.XtraTreeList;
using Itop.Client.Common;

namespace Itop.Client.Chen
{
    public partial class Form1_Fs_VolumeBalance : Itop.Client.Base.FormBase
    {

        DataTable dataTable;

        private string lastEditValue = string.Empty;
        private int typeFlag = 10000;
        private int typeFlag2 = 1;

        public string TYPEFLAG2
        {
            get { return type; }
            set { type = value; }
        }


        private TreeListNode tln=null;
        public TreeListNode TLN
        {
            get { return tln; }
        
        }




        Hashtable hs = new Hashtable();

        public Hashtable HS
        {
            set { hs = value; }
            get { return hs; }
        
        }

        bool isjingji = false;

        public bool IsJingJi
        {
            set { isjingji = value; }
            get { return isjingji; }

        }

        int fl2 = 999999;

        public int FL2
        {
            set { fl2 = value; }
            get { return fl2; }

        }


        IList<PSP_P_Types> li = new List<PSP_P_Types>();

        public IList<PSP_P_Types> LI
        {
            set { li = value; }
            get { return li; }

        }

        string type = "1";

        ////public string Type
        ////{
        ////    set { type = value; }
        ////    get { return type; }

        ////}

        public Form1_Fs_VolumeBalance()
        {
            InitializeComponent();
        }

        private void Form1_F_Load(object sender, EventArgs e)
        {

            DataTable dts = new DataTable();
            dts.Columns.Add("ID", typeof(int));
            dts.Columns.Add("Name", typeof(string));



            IList<PSP_ForecastReports> list = Services.BaseService.GetList<PSP_ForecastReports>("SelectPSP_ForecastReportsByFlag", type);
            if (list == null)
                return;
            
            foreach (PSP_ForecastReports pf in list)
            {
               DataRow dr = dts.NewRow();
                dr["ID"] = pf.ID;
                dr["Name"] = pf.Title;
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

            typeFlag2 = id1;
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;

        }












        private void LoadData()
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }

            //PSP_P_Types psp_Type = new PSP_P_Types();
            //psp_Type.Flag2 = typeFlag2;
            //IList listTypes = Common.Services.BaseService.GetList("SelectPSP_P_TypesByFlag2", psp_Type);

            //dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_P_Types));
            //////string str = " Flag!='" + typeFlag + "' and Flag2='" + typeFlag2+ "' ";

            //////IList listTypes = Common.Services.BaseService.GetList("SelectPSP_P_TypesByWhere", str);


            //////dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_P_Types));

            PSP_P_Types psp_Type = new PSP_P_Types();
            psp_Type.Flag2 = typeFlag2;
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_P_TypesByFlag2", psp_Type);
            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_P_Types));
            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "分类名";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;

            PSP_ForecastReports pf = Services.BaseService.GetOneByKey<PSP_ForecastReports>(typeFlag2);

            //string str1 = " flag=" + typeFlag2;
            //IList<PSP_P_Years> listyears = Common.Services.BaseService.GetList<PSP_P_Years>("SelectPSP_P_YearsByWhere", str1);

            //foreach (PSP_P_Years pppy in listyears)
            //{
            //    if (pppy.Year <= pf.EndYear)
            //    {
            //        AddColumn(pppy.Year);
            //    }
            //}

            for (int i = pf.StartYear; i <= pf.EndYear; i++)
            {
                AddColumn(i);
            }


            Application.DoEvents();
            try
                {
                    LoadValues();
                }
                catch { }

            treeList1.ExpandAll();
        }

        //读取数据
        private void LoadValues()
        {
            PSP_ForecastReports pf = Services.BaseService.GetOneByKey<PSP_ForecastReports>(typeFlag2);
            PSP_ForecastValues psp_Value = new PSP_ForecastValues();
            psp_Value.ForecastID = typeFlag2;

            IList<PSP_ForecastValues> listValues = Common.Services.BaseService.GetList<PSP_ForecastValues>("SelectPSP_ForecastValuesByForecastID", psp_Value);

            foreach (PSP_ForecastValues value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null && value.Year >= pf.StartYear && value.Year <= pf.EndYear)
                {
                    node.SetValue(value.Year + "年", value.Value);
                }
            }



            ////////PSP_ForecastReports pf = Services.BaseService.GetOneByKey<PSP_ForecastReports>(typeFlag2);


            ////////string str = " TypeID not in(80001,80002,80003,80004,80005,80006) and Flag2=" + typeFlag2;
            ////////IList<PSP_P_Values> listValues = Common.Services.BaseService.GetList<PSP_P_Values>("SelectPSP_P_ValuesByWhere", str);

            ////////foreach (PSP_P_Values value in listValues)
            ////////{
            ////////    TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
            ////////    if (node != null)
            ////////    {
            ////////        if (value.Year <= pf.EndYear)
            ////////        {
            ////////            value.Value = Math.Round(value.Value);
            ////////            node.SetValue(value.Year + "年", value.Value);
            ////////        }
            ////////    }
            ////////}
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
            //column.ColumnEdit = repositoryItemTextEdit1;
            //treeList1.RefreshDataSource();
        }

        private int GetInsertIndex(int year)
        {
            int nFixedColumns = typeof(PSP_P_Types).GetProperties().Length - 2;//ID和ParentID列不在treeList1.Columns中
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






        private void LoadData1(int id)
        {
            if (dataTable != null)
            {
                dataTable.Columns.Clear();
                treeList1.Columns.Clear();
            }

            switch (id)
            { 
                case 999999:
                    typeFlag2 = 1;
                    break;

                case 888888:
                    typeFlag2 = 2;
                    break;
            
            
            }
            PSP_Types psp_Type = new PSP_Types();
            psp_Type.Flag2 = typeFlag2;
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByFlag2", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Types));

            treeList1.DataSource = dataTable;

            treeList1.Columns["Title"].Caption = "分类名";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;

            PSP_Years psp_Year = new PSP_Years();
            psp_Year.Flag = typeFlag2;
            IList<PSP_Years> listYears = Common.Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlag", psp_Year);

            Hashtable htt = new Hashtable();
            foreach (PSP_Years item in listYears)
            {
                if(!htt.ContainsValue(item.Year))
                    htt.Add(Guid.NewGuid().ToString(),item.Year);
                AddColumn1(item.Year);
            }
            Application.DoEvents();

            LoadValues1(htt);

            treeList1.ExpandAll();
        }

        //读取数据
        private void LoadValues1(Hashtable htt)
        {
            
            PSP_Values psp_Values = new PSP_Values();
            psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值

            IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesListByFlag2", psp_Values);

            foreach (PSP_Values value in listValues)
            {
                if (!htt.ContainsValue(value.Year))
                    continue;

                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null)
                {
                    node.SetValue(value.Year + "年", value.Value);
                }
            }
        }

        //添加年份后，新增一列
        private void AddColumn1(int year)
        {
            int nInsertIndex = GetInsertIndex1(year);

            dataTable.Columns.Add(year + "年", typeof(double));

            TreeListColumn column = treeList1.Columns.Insert(nInsertIndex);
            column.FieldName = year + "年";
            column.Tag = year;
            column.Caption = year + "年";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = nInsertIndex - 2;//有两列隐藏列
            //column.ColumnEdit = repositoryItemTextEdit1;
            //treeList1.RefreshDataSource();
        }

        private int GetInsertIndex1(int year)
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            

        }

        private void UpdateData(PSP_Types pt, PSP_P_Types pt1)
        {
            pt1.Flag = pt.Flag;
            pt1.Flag2 = pt.Flag2;
            pt1.ID = pt.ID;
            pt1.ParentID = pt.ParentID;
            pt1.Title = pt.Title;
        }

        private void Get(PSP_Types pt, IList<PSP_P_Types> li,Hashtable hs)
        {
            IList<PSP_Types> li1 = Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByParentID", pt);
            foreach (PSP_Types pt1 in li1)
            {
                if (hs.ContainsValue(pt1))
                    continue;

                hs.Add(Guid.NewGuid().ToString(), pt1);
                //PSP_P_Types ppt=new PSP_P_Types();
                //UpdateData(pt1,ppt);
                //li.Add(ppt);
                Get(pt1, li, hs);
            }
        }


        private void Get1(PSP_P_Types pt, IList<PSP_P_Types> li, Hashtable hs)
        {
            IList<PSP_P_Types> li1 = Services.BaseService.GetList<PSP_P_Types>("SelectPSP_P_TypesByParentID", pt);
            foreach (PSP_P_Types pt1 in li1)
            {
                if (hs.ContainsValue(pt1))
                    continue;

                hs.Add(Guid.NewGuid().ToString(), pt1);
                //li.Add(pt1);
                Get1(pt1, li, hs);
            }
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
            TreeListNode tlnn = treeList1.FocusedNode;
            if (tlnn == null)
                return;
            tln = tlnn;
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


    }
}