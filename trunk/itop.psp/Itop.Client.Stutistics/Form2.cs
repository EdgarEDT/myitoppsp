using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Common;
using Itop.Client.Base;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Stutistics;
using Itop.Client.Common;

namespace Itop.Client.Stutistics
{
    public partial class Form2 : FormBase
    {

        string title = "";
        string unit = "";
        bool isSelect = false;
        string type = "XMXH";

        DevExpress.XtraGrid.GridControl gcontrol = null;

        bool st = false;



        public string Title
        {
            get { return title; }
        }

        public string Unit
        {
            get { return unit; }
        }

        public DevExpress.XtraGrid.GridControl Gcontrol
        {
            get { return gcontrol; }
        }

        public bool IsSelect
        {
            set { isSelect = value; }
        }




        DataTable dataTable;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;

        private string typeFlag2 ="";
        //int aindex = 0;
        
        public Form2()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ctrlPowerEachList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            this.ctrlPowerEachList1.RefreshData(type);

            Show();
            Application.DoEvents();

            InitRight();
        }


        private void InitRight()
        {
            if (!AddRight)
            {
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem12.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            
            }

            if (!EditRight)
            {
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem13.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                ctrlPowerEachList1.editright = false;
            }

            if (!DeleteRight)
            {
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            
            }
        
        }

        void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.ctrlPowerEachList1.FocusedObject == null)
                return;
            typeFlag2 = this.ctrlPowerEachList1.FocusedObject.UID;

            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }

        private void LoadData()
        {
            try
            {
                if (dataTable != null)
                {
                    dataTable.Columns.Clear();
                    treeList1.Columns.Clear();
                }

                PowerProjectTypes psp_Type = new PowerProjectTypes();
                psp_Type.Flag2 = typeFlag2;
                IList listTypes = new ArrayList();
                try
                {
                    listTypes = Common.Services.BaseService.GetList("SelectPowerProjectTypesByFlag2", psp_Type);
                }
                catch (Exception ex)
                { MsgBox.Show(ex.Message); }
                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PowerProjectTypes));

                
                foreach (DataRow rw in dataTable.Rows)
                {
                    try
                    {
                        if((int)rw["ParentID"]!=0)
                        rw["Dy1"] = (int)rw["Dy"];
                    }
                    catch { }
                
                }

                



                treeList1.DataSource = dataTable;

                treeList1.Columns["Title"].Caption = "项目名称";
                treeList1.Columns["Title"].Width = 180;
                treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
                treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
                treeList1.Columns["Title"].ColumnEdit = this.repositoryItemMemoEdit1;
                treeList1.Columns["Title"].VisibleIndex = 0;



                treeList1.Columns["Flag"].VisibleIndex = -1;
                treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["Flag2"].VisibleIndex = -1;
                treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;


                treeList1.Columns["TypeName"].VisibleIndex = -1;
                treeList1.Columns["TypeName"].OptionsColumn.ShowInCustomizationForm = false;

                try
                {
                    treeList1.Columns["Dy"].VisibleIndex = -1;
                    treeList1.Columns["Dy"].OptionsColumn.ShowInCustomizationForm = false;
                }
                catch { }
                

                PowerProjectYears psp_Year = new PowerProjectYears();
                psp_Year.Flag = typeFlag2;
                IList<PowerProjectYears> listYears = Common.Services.BaseService.GetList<PowerProjectYears>("SelectPowerProjectYearsListByFlag", psp_Year);
                AddColumn1();

                foreach (PowerProjectYears item in listYears)
                {
                    AddColumn(item.Year);
                }


                AddColumn2();
                foreach (PowerProjectYears item in listYears)
                {
                    AddColumns(item.Year);
                }


                AddColumn3();

                Application.DoEvents();

                LoadValues();

                treeList1.ExpandAll();
            }
            catch(Exception edx)
            {
                MsgBox.Show(edx.Message);
            
            }
        }

        //读取数据
        private void LoadValues()
        {
            //PowerProjectValues PowerValues = new PowerProjectValues();
            //PowerValues.ID = typeFlag2;//用ID字段存放Flag2的值

            IList<PowerProjectValues> listValues = Common.Services.BaseService.GetList<PowerProjectValues>("SelectPowerProjectValuesListByFlag2", typeFlag2);

            foreach (PowerProjectValues value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null)
                {
                    node.SetValue(value.Year + "年", value.Value);
                    node.SetValue(value.Year + "度", value.Value1);
                }
            }


            
            foreach(TreeListNode tln in treeList1.Nodes)
            {
                CalChildNode(tln);
            }
        }

        private void CalChildNode(TreeListNode tln)
        {
            if (tln.HasChildren)
            {
                foreach (TreeListNode nd in tln.Nodes)
                {
                    CalChildNode(nd);
                }
            }
            else
            {
                CalculateNodeSum(tln);
            }
        }

        //添加年份后，新增一列

        private void AddColumn(int year)
        {
            try
            {
                int nInsertIndex = GetInsertIndex(year);

                dataTable.Columns.Add(year + "年", typeof(double));

                //TreeListColumn column = treeList1.Columns.Add();
                TreeListColumn column = treeList1.Columns.Insert(nInsertIndex);
                column.FieldName = year + "年";
                column.Tag = year;
                column.Caption = year + "年规模";
                column.Width = 70;
                column.OptionsColumn.AllowSort = false;
                column.VisibleIndex = nInsertIndex - 2;//有两列隐藏列
                //aindex = nInsertIndex - 2;
                column.ColumnEdit = repositoryItemTextEdit1;

                foreach (TreeListColumn col in treeList1.Columns)
                {
                    if (col.Caption.IndexOf("年") > 0)
                    {
                        col.VisibleIndex = (int)col.Tag;
                    }
                }

            }
            catch{  }
            treeList1.RefreshDataSource();
        }


        private void AddColumns(int year)
        {
            try
            {
                int nInsertIndex = GetInsertIndex(year);

                dataTable.Columns.Add(year + "度", typeof(double));

                //TreeListColumn column = treeList1.Columns.Add();
                TreeListColumn column = treeList1.Columns.Insert(nInsertIndex);
                column.FieldName = year + "度";
                column.Tag = year;
                column.Caption = year + "年投资";
                column.Width = 70;
                column.OptionsColumn.AllowSort = false;
                column.VisibleIndex = nInsertIndex - 2;//有两列隐藏列
                //aindex = nInsertIndex - 2;
                column.ColumnEdit = repositoryItemTextEdit1;

                foreach (TreeListColumn col in treeList1.Columns)
                {
                    if (col.Caption.IndexOf("度") > 0)
                    {
                        col.VisibleIndex = (int)col.Tag;
                    }
                }

            }
            catch { }
            treeList1.RefreshDataSource();
        }



        private int GetInsertIndex(int year)
        {
            int nFixedColumns = typeof(PowerProjectTypes).GetProperties().Length - 2;//ID和ParentID列不在treeList1.Columns中
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


        private void AddColumn1()//静态总投资
        {
            TreeListColumn column = treeList1.Columns["Dy1"];
            column.Caption = "电压等级";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 1;
            //column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = true;
            column.ColumnEdit = this.repositoryItemMemoEdit2;

            column = treeList1.Columns["Jsxz"];
            column.Caption = "建设性质";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 2;
            //column.ColumnEdit = repositoryItemTextEdit2;
            column.OptionsColumn.AllowEdit = true;
            column.ColumnEdit = this.repositoryItemMemoEdit2;

            column = treeList1.Columns["Gznr"];
            column.Caption = "工程内容";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 3;
            //column.ColumnEdit = repositoryItemTextEdit2;
            column.OptionsColumn.AllowEdit = true;
            column.ColumnEdit = this.repositoryItemMemoEdit2;

            column = treeList1.Columns["Gm"];
            column.Caption = "规模";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 4;
            //column.ColumnEdit = repositoryItemTextEdit2;
            column.OptionsColumn.AllowEdit = true;
            column.ColumnEdit = this.repositoryItemMemoEdit2;

            column = treeList1.Columns["Gcfl"];
            column.Caption = "工程分类";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 5;
            //column.ColumnEdit = repositoryItemTextEdit2;
            column.OptionsColumn.AllowEdit = true;
            column.ColumnEdit = this.repositoryItemMemoEdit2;


        }


        private void AddColumn2()//静态总投资
        {
            TreeListColumn column = treeList1.Columns["Sgm"];
            column.Caption = "总规模";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 1000;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            
        }

        private void AddColumn3()//静态总投资
        {

            TreeListColumn column = treeList1.Columns["Stz"];
            column.Caption = "总投资";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 3000;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["Remark"];
            column.Caption = "备注";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 5000;
            //column.ColumnEdit = repositoryItemTextEdit2;
            column.OptionsColumn.AllowEdit = true;
            column.ColumnEdit = this.repositoryItemMemoEdit1;

        }
        private int GetInsertIndex3(int year)
        {
            int nFixedColumns = typeof(PowerProjectTypes).GetProperties().Length - 2;//ID和ParentID列不在treeList1.Columns中
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


        private int GetInsertIndex2(int year)
        {
            int nFixedColumns = typeof(PowerProjectTypes).GetProperties().Length - 2;//ID和ParentID列不在treeList1.Columns中
            int nColumns = treeList1.Columns.Count - 4;
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



        //增加年份
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            
            try
            {
                if (e.Column.FieldName == "Dy" || e.Column.FieldName == "Jsxz" || e.Column.FieldName == "Gznr" || e.Column.FieldName == "Gm" || e.Column.FieldName == "Gcfl" || e.Column.FieldName == "Remark")
                {
                    SaveCellValue(e.Column.FieldName, (int)e.Node["ID"], e.Value.ToString());
                }
                else
                {
                    SaveCellValue((int)e.Column.Tag, (int)e.Node["ID"], Convert.ToDouble(e.Value), e.Column.Caption);
                    CalculateSum(e.Node, e.Column);

                    CalculateNodeSum(e.Node);

                }
            }

            catch { }

        }

        



        private bool SaveCellValue(int year, int typeID, double value,string columnname)
        {
            PowerProjectValues pv=new PowerProjectValues();
            pv.TypeID=typeID;
            pv.Year=year;

            PowerProjectValues PowerValues = Services.BaseService.GetOneByKey<PowerProjectValues>(pv);

            if (PowerValues != null)
            {
                if(columnname.IndexOf("规模")>0)
                    PowerValues.Value=value;

                if(columnname.IndexOf("投资")>0)
                    PowerValues.Value1=value;
            }
            else
            {
                PowerValues=new PowerProjectValues();
                PowerValues.TypeID = typeID;
                PowerValues.Year=year;
                if(columnname.IndexOf("规模")>0)
                    PowerValues.Value=value;

                if(columnname.IndexOf("投资")>0)
                    PowerValues.Value1=value;
            }

            try
            {
                Common.Services.BaseService.Update<PowerProjectValues>(PowerValues);
            }
            catch(Exception ex)
            {
                MsgBox.Show("保存数据出错：" + ex.Message);
                return false;
            }
            return true;
        }



        private bool SaveCellValue(string fname, int id, string value)
        {

            PowerProjectTypes pt = Services.BaseService.GetOneByKey<PowerProjectTypes>(id);

            switch (fname)
            {
                case "Dy":
                    pt.Dy = int.Parse(value);
                    break;

                case "Gm":
                    pt.Gm = value;
                    break;

                case "Gznr":
                    pt.Gznr = value;
                    break;

                case "Jsxz":
                    pt.Jsxz = value;
                    break;

                case "Remark":
                    pt.Remark = value;
                    break;

                case "Gcfl":
                    pt.Gcfl = value;
                    break;
            }


                try
                {
                    Common.Services.BaseService.Update<PowerProjectTypes>(pt);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("保存数据出错：" + ex.Message);
                    return false;
                }
                return true;

        }


        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {

            if (treeList1.FocusedNode.HasChildren
                || !base.EditRight)
            {
                e.Cancel = true;
            }
        }


        //计算静态总投资列表
        private void Sumss(TreeListNode node, TreeListColumn column)
        {
            TreeListNode parentNode = node.ParentNode;

            if (parentNode == null)
            {
                return;
            }

            double sum = 0;
            foreach (TreeListNode nd in parentNode.Nodes)
            {
                object value = nd.GetValue(column.FieldName);
                if (value != null && value != DBNull.Value)
                {
                    sum += Convert.ToDouble(value);
                }
            }

            parentNode.SetValue(column.FieldName, sum);
            
            //CalculateSum(e.Node, e.Column);

            Sumss(parentNode, column);
        }



        //计算静态总投资
        private void CalculateNodeSum(TreeListNode node)
        {
            double sum = 0.0;
            double sum1 = 0.0;
            foreach (TreeListColumn col in treeList1.Columns)
            {
                if (col.Caption.IndexOf("年规模") > 0)
                {
                    try
                    {
                        sum += (double)node[col.FieldName];
                    }
                    catch { }
                }


                if (col.Caption.IndexOf("年投资") > 0)
                {
                    try
                    {
                        sum1 += (double)node[col.FieldName];
                    }
                    catch { }
                }
            }
            node["Sgm"] = sum;
            node["Stz"] = sum1;
            if (node.ParentNode != null)
            {
                CalculateNodeSum(node.ParentNode);
            }
        }

        //当子分类数据改变时，计算其父分类的值
        private void CalculateSum(TreeListNode node, TreeListColumn column)
        {
            TreeListNode parentNode = node.ParentNode;

            if (parentNode == null)
            {
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

            parentNode.SetValue(column.FieldName, sum);

            if (column.FieldName.IndexOf("年") > 0 || column.FieldName.IndexOf("度") > 0)
            {
                SaveCellValue((int)column.Tag, (int)parentNode["ID"], sum, column.Caption);
                //SaveCellValue((int)column.Tag, (int)parentNode.GetValue("ID"), sum);
            }
            else
            {
                //SaveCellValue(column.FieldName,(int)parentNode.GetValue("ID"), sum);           
            }

            CalculateSum(parentNode, column);
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
            //FrmBurdenLineMaxDate frm = new FrmBurdenLineMaxDate();
            ////frm.ShowDialog();

            //frm.IsSelect = isSelect;

            //if (isSelect && frm.ShowDialog() == DialogResult.OK)
            //{
            //    gcontrol = frm.gridControl1;
            //    title = frm.Title;
            //    unit = "单位：小时";
            //    DialogResult = DialogResult.OK;
            //}



            Form2Print frm = new Form2Print();
            if (!PrintRight)
            {
                frm.print = false;
            }
            frm.IsSelect = isSelect;
            if (this.ctrlPowerEachList1.FocusedObject!=null)
                frm.Text = this.ctrlPowerEachList1.FocusedObject.ListName;
            DataTable dt = ConvertTreeListToDataTable(treeList1);

            frm.GridDataTable = dt;


            if (frm.ShowDialog() == DialogResult.OK && isSelect)
            {
                gcontrol = frm.gridControl1;
                title = frm.Title;
                unit = "单位：万元";
                DialogResult = DialogResult.OK;
            }

                //f.ShowDialog();
            //}

        }

        //把树控件内容按显示顺序生成到DataTable中
        private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList)
        {
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();
            listColID.Add("Flag");
            dt.Columns.Add("Flag", typeof(int));

            listColID.Add("ParentID");
            dt.Columns.Add("ParentID", typeof(int));

            listColID.Add("TypeName");
            dt.Columns.Add("TypeName", typeof(string));

            listColID.Add("Title");
            dt.Columns.Add("Title", typeof(string));
            dt.Columns["Title"].Caption = "项目名称";

            listColID.Add("Dy1");
            dt.Columns.Add("Dy1", typeof(string));
            dt.Columns["Dy1"].Caption = "电压等级";

            listColID.Add("Dy");
            dt.Columns.Add("Dy", typeof(string));

            listColID.Add("Jsxz");
            dt.Columns.Add("Jsxz", typeof(string));
            dt.Columns["Jsxz"].Caption = "建设性质";

            listColID.Add("Gznr");
            dt.Columns.Add("Gznr", typeof(string));
            dt.Columns["Gznr"].Caption = "工程内容";

            listColID.Add("Gm");
            dt.Columns.Add("Gm", typeof(string));
            dt.Columns["Gm"].Caption = "规模";

            listColID.Add("Gcfl");
            dt.Columns.Add("Gcfl", typeof(string));
            dt.Columns["Gcfl"].Caption = "工程分类";

            foreach (TreeListColumn column in xTreeList.Columns)
            {
                if (column.FieldName.IndexOf("年") > 0)
                {
                    listColID.Add(column.FieldName);
                    dt.Columns.Add(column.FieldName, typeof(double));
                }

                if (column.FieldName.IndexOf("度") > 0)
                {
                    listColID.Add(column.FieldName);
                    dt.Columns.Add(column.FieldName, typeof(double));
                }
            }

            

            listColID.Add("Sgm");
            dt.Columns.Add("Sgm", typeof(double));
            dt.Columns["Sgm"].Caption = "总规模";

            listColID.Add("Stz");
            dt.Columns.Add("Stz", typeof(double));
            dt.Columns["Stz"].Caption = "总投资";

            listColID.Add("Remark");
            dt.Columns.Add("Remark", typeof(string));
            dt.Columns["Remark"].Caption = "备注";

            foreach(TreeListNode node in xTreeList.Nodes)
            {
                AddNodeDataToDataTable(dt, node, listColID);
            }

            //DataRow[] rows1 = dt.Select("TypeName='变电站' and Dy=66 and ParentID<>0 and Title<>'小计'");
            //DataRow[] rows2 = dt.Select("TypeName='线路' and Dy=66 and ParentID<>0 and Title<>'小计'");
            DataRow[] rows1 = dt.Select("TypeName='变电站' and Dy=110 and ParentID<>0 and Title<>'小计'");
            DataRow[] rows2 = dt.Select("TypeName='线路' and Dy=110 and ParentID<>0 and Title<>'小计'");
            DataRow[] rows3 = dt.Select("TypeName='变电站' and Dy=220 and ParentID<>0 and Title<>'小计'");
            DataRow[] rows4 = dt.Select("TypeName='线路' and Dy=220 and ParentID<>0 and Title<>'小计'");
            DataRow[] rows5 = dt.Select("TypeName='变电站' and Dy=500 and ParentID<>0 and Title<>'小计'");
            DataRow[] rows6 = dt.Select("TypeName='线路' and Dy=500 and ParentID<>0 and Title<>'小计'");


            DataRow d1 = dt.NewRow();
            foreach (DataRow rowss1 in rows1)
            {
                compute(dt,d1,rowss1); 
            }

            DataRow d2 = dt.NewRow();
            foreach (DataRow rowss2 in rows2)
            {
                compute(dt, d2, rowss2); 
            }

            DataRow d3 = dt.NewRow();
            foreach (DataRow rowss3 in rows3)
            {
                compute(dt, d3, rowss3); 
            }

            DataRow d4 = dt.NewRow();
            foreach (DataRow rowss4 in rows4)
            {
                compute(dt, d4, rowss4); 
            }
            DataRow d5 = dt.NewRow();
            foreach (DataRow rowss5 in rows5)
            {
                compute(dt, d5, rowss5);
            }
            DataRow d6 = dt.NewRow();
            foreach (DataRow rowss6 in rows6)
            {
                compute(dt, d6, rowss6);
            }
            DataRow rs1 = dt.NewRow();
            compute(dt, d1, d2, rs1);//110kv
            rs1["Dy1"] = "110千伏总投资";
            rs1["Title"] = "总计";
            //rs1["Dy1"] = "66千伏总投资";
            //rs1["Title"] = "总计";

            DataRow rs2 = dt.NewRow();
            compute(dt, d3, d4, rs2);//220kv
            rs2["Dy1"] = "220千伏总投资";
            rs2["Title"] = "总计";

            DataRow rs6 = dt.NewRow();
            compute(dt, d5, d6, rs6);//500kv
            rs6["Dy1"] = "500千伏总投资";
            rs6["Title"] = "总计";

            DataRow rs3 = dt.NewRow();
            compute(dt, d1, d3,d5, rs3);//变电站
            rs3["Dy1"] = "变电总投资";
            rs3["Title"] = "总计";

            DataRow rs4 = dt.NewRow();
            compute(dt, d2, d4,d6 , rs4);//线路
            rs4["Dy1"] = "线路总投资";
            rs4["Title"] = "总计";

            DataRow rs5 = dt.NewRow();
            compute(dt, rs3, rs4, rs5);//总计
            rs5["Dy"] = "总投资";
            rs5["Title"] = "总计";
         
            dt.Rows.Add(rs4);
            dt.Rows.Add(rs3);

            dt.Rows.Add(rs1);
            dt.Rows.Add(rs2);
            dt.Rows.Add(rs6);
            dt.Rows.Add(rs5);
            return dt;
        }



        private void compute(DataTable dt, DataRow row1, DataRow row2)
        {
            foreach (DataColumn dc in dt.Columns)
            {

                if (dc.ColumnName.IndexOf("度") > 0 || dc.ColumnName == "Stz" )
                {
                    double d1 = 0;
                    try
                    {
                        d1 = (double)row1[dc.ColumnName];
                    }
                    catch { }
                    double d2 = 0;
                    try
                    {
                        d2 = (double)row2[dc.ColumnName];
                    }
                    catch { }

                    row1[dc] = d1 + d2;
                }
                if (dc.ColumnName.IndexOf("年") > 0|| dc.ColumnName == "Sgm")
                {
                    double d1 = 0;
                    try
                    {
                        d1 = (double)row1[dc.ColumnName];
                    }
                    catch { }
                    double d2 = 0;
                    try
                    {
                        d2 = (double)row2[dc.ColumnName];
                    }
                    catch { }

                    row1[dc] = d1 + d2;
                }
            }
        }
        private void compute(DataTable dt, DataRow row1, DataRow row2,DataRow row)
        {
                foreach (DataColumn dc in dt.Columns)
                {

                    if (dc.ColumnName.IndexOf("度") > 0 || dc.ColumnName == "Stz" )
                    {
                        double d1 = 0;
                        try
                        {
                            d1 = (double)row1[dc.ColumnName];
                        }
                        catch { }
                        double d2 = 0;
                        try
                        {
                            d2 = (double)row2[dc.ColumnName];
                        }
                        catch { }

                        row[dc] = d1 + d2;                    
                    }
                    if (dc.ColumnName.IndexOf("年") > 0 || dc.ColumnName == "Sgm")
                    {
                        double d1 = 0;
                        try
                        {
                            d1 = (double)row1[dc.ColumnName];
                        }
                        catch { }
                        double d2 = 0;
                        try
                        {
                            d2 = (double)row2[dc.ColumnName];
                        }
                        catch { }

                        row[dc] = d1 + d2;
                    }
                }
        }

        private void compute(DataTable dt, DataRow row1, DataRow row2,DataRow row3, DataRow row)
        {
            foreach (DataColumn dc in dt.Columns)
            {

                if (dc.ColumnName.IndexOf("度") > 0 || dc.ColumnName == "Stz" )
                {
                    double d1 = 0;
                    try
                    {
                        d1 = (double)row1[dc.ColumnName];
                    }
                    catch { }
                    double d2 = 0;
                    try
                    {
                        d2 = (double)row2[dc.ColumnName];
                    }
                    catch { }
                    double d3 = 0;
                    try
                    {
                        d3 = (double)row3[dc.ColumnName];
                    }
                    catch { }

                    row[dc] = d1 + d2 +d3;
                }
                if (dc.ColumnName.IndexOf("年") > 0 || dc.ColumnName == "Sgm")
                {
                    double d1 = 0;
                    try
                    {
                        d1 = (double)row1[dc.ColumnName];
                    }
                    catch { }
                    double d2 = 0;
                    try
                    {
                        d2 = (double)row2[dc.ColumnName];
                    }
                    catch { }
                    double d3 = 0;
                    try
                    {
                        d3 = (double)row3[dc.ColumnName];
                    }
                    catch { }

                    row[dc] = d1 + d2 + d3;
                }
            }
        }



        private void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID)
        {
            bool sd=false;
            DataRow newRow = dt.NewRow();
            DataRow newRow1 = dt.NewRow();
            foreach (string colID in listColID)
            {

                if (node.ParentNode==null)
                {
                    if (colID.IndexOf("年") < 0 && colID.IndexOf("度") < 0 && colID.IndexOf("Sgm") < 0 && colID.IndexOf("Stz") < 0)
                    {
                        newRow[colID] = node[colID];
                    }
                    else
                    {
                        newRow1["Title"] = "小计";
                        newRow1[colID] = node[colID];
                    }

                }
                else
                {
                    newRow[colID] = node[colID];
                    if (colID.IndexOf("年") >= 0 || colID.IndexOf("度") >= 0 || colID.IndexOf("Sgm") >= 0 || colID.IndexOf("Stz") >= 0)
                    {
                        newRow1["Title"] = "小计";
                        newRow1[colID] = node.ParentNode[colID];
                    }
                }

                if (node.ParentNode != null && node == node.ParentNode.LastNode) sd = true;
                if (node.ParentNode == null && node.Nodes.Count==0) sd = true; 
            }


            

            //if (node.ParentNode == null && dt.Rows.Count > 0)
            //{
            //    dt.Rows.Add(newRow1);
            //}
            dt.Rows.Add(newRow);
            if (sd) dt.Rows.Add(newRow1);




            
            foreach(TreeListNode nd in node.Nodes)
            {
                AddNodeDataToDataTable(dt, nd, listColID);
            }
        }





        //根据选择的统计年份，生成统计结果数据表
        private DataTable ResultDataTable(DataTable sourceDataTable, List<ChoosedYears> listChoosedYears)
        {
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("Title", typeof(string));
            foreach (ChoosedYears choosedYear in listChoosedYears)
            {
                dtReturn.Columns.Add(choosedYear.Year + "年", typeof(double));
                if(choosedYear.WithIncreaseRate)
                {
                    dtReturn.Columns.Add(choosedYear.Year + "增长率", typeof(double)).Caption = "增长率";
                }
            }

            int nRowTotalPower = 0;//总用电量所在行
            int nRowMaxLoad = 0;//最高负荷所在行
            int nRowPopulation = 0;//人口所在行
            int nRowDenizen = 0;//居民用电量所在行

            #region 填充数据，获取总用电量所在行、最高负荷所在行、人口所在行
            for (int i = 0; i < sourceDataTable.Rows.Count; i++)
            {
                DataRow newRow = dtReturn.NewRow();
                DataRow sourceRow = sourceDataTable.Rows[i];
                foreach(DataColumn column in dtReturn.Columns)
                {
                    if(column.Caption != "增长率")
                    {
                        newRow[column.ColumnName] = sourceRow[column.ColumnName];
                    }
                }
                dtReturn.Rows.Add(newRow);

                if (sourceRow["Flag"] != DBNull.Value)
                {
                    if((int)sourceRow["ParentID"] == 0)
                    {
                        switch((int)sourceRow["Flag"])
                        {
                            case 2://总用电量
                                nRowTotalPower = i;
                                break;

                            case 4://最高负荷，后面加一行Tmax
                                nRowMaxLoad = i;
                                dtReturn.Rows.Add(new object[] { "Tmax" });
                                break;

                            case 7://总人口
                                nRowPopulation = i + 1;//由于之前加了一行TMax，此处行加1
                                dtReturn.Rows.Add(new object[] { "人均用电量" });
                                dtReturn.Rows.Add(new object[] { "人均生活用电量" });
                                break;

                            default:
                                break;
                        }
                    }
                    else if (sourceRow["Title"].ToString().IndexOf("居民") > -1)
                    {
                        nRowDenizen = i;
                    }
                }
            }
            #endregion

            #region 计算TMax和人均用电量
            foreach (ChoosedYears choosedYear in listChoosedYears)
            {
                object numerator = dtReturn.Rows[nRowTotalPower][choosedYear.Year + "年"];
                object denominator = dtReturn.Rows[nRowMaxLoad][choosedYear.Year + "年"];
                if(numerator != DBNull.Value)
                {
                    if(denominator != DBNull.Value)
                    {
                        try
                        {
                            dtReturn.Rows[nRowMaxLoad + 1][choosedYear.Year + "年"] = (int)((double)numerator / (double)denominator);
                        }
                        catch
                        {
                        }
                    }

                    denominator = dtReturn.Rows[nRowPopulation][choosedYear.Year + "年"];
                    if (denominator != DBNull.Value)
                    {
                        try
                        {
                            dtReturn.Rows[nRowPopulation + 1][choosedYear.Year + "年"] = System.Math.Round((double)numerator / (double)denominator, 3);
                        }
                        catch
                        {
                        }
                    }
                }

                numerator = dtReturn.Rows[nRowDenizen][choosedYear.Year + "年"];
                denominator = dtReturn.Rows[nRowPopulation][choosedYear.Year + "年"];
                if (denominator != DBNull.Value && numerator != DBNull.Value)
                {
                    try
                    {
                        dtReturn.Rows[nRowPopulation + 2][choosedYear.Year + "年"] = System.Math.Round((double)numerator / (double)denominator, 3);
                    }
                    catch
                    {
                    }
                }
            }
            #endregion

            #region 计算增长率
            DataColumn columnBegin = null;
            foreach(DataColumn column in dtReturn.Columns)
            {
                if(column.ColumnName.IndexOf("年") > 0)
                {
                    if(columnBegin == null)
                    {
                        columnBegin = column;
                    }
                }
                else if(column.ColumnName.IndexOf("增长率") > 0)
                {
                    DataColumn columnEnd = dtReturn.Columns[column.Ordinal - 1];

                    foreach(DataRow row in dtReturn.Rows)
                    {
                        if(row["Title"].ToString() == "Tmax")
                        {
                            continue;
                        }

                        object numerator = row[columnEnd];
                        object denominator = row[columnBegin];

                        if(numerator != DBNull.Value && denominator != DBNull.Value)
                        {
                            try
                            {
                                int intervalYears = Convert.ToInt32(columnEnd.ColumnName.Replace("年", ""))
                                    - Convert.ToInt32(columnBegin.ColumnName.Replace("年", ""));
                                row[column] = Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);
                            }
                            catch
                            {
                            }
                        }
                    }

                    //本次计算增长率的列作为下次的起始列
                    columnBegin = columnEnd;
                }

            }
            #endregion

            return dtReturn;
        }



        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachList1.AddObject(type);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachList1.UpdateObject();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachList1.DeleteObject("xihua");
            LoadData();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormTypeTitle1 frm = new FormTypeTitle1();
            frm.Text = "增加项目";
            frm.IsParent = true;

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PowerProjectTypes psp_Type = new PowerProjectTypes();
                psp_Type.Title = frm.TypeTitle;
                psp_Type.TypeName = frm.TypeName;
                psp_Type.Dy = frm.Dydj;
                //psp_Type.Dy1 = ;
                psp_Type.Flag = 0;
                psp_Type.Flag2 = typeFlag2;
                psp_Type.ParentID = 0;

                try
                {
                    psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPowerProjectTypes", psp_Type);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加项目出错：" + ex.Message);
                }
            }
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }


            if (focusedNode.ParentNode != null)
            {
                focusedNode = focusedNode.ParentNode;
            }


            FormTypeTitle1 frm = new FormTypeTitle1();
            frm.Text = "增加" + focusedNode.GetValue("Title") + "的子项目";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PowerProjectTypes psp_Type = new PowerProjectTypes();
                psp_Type.Title = frm.TypeTitle;
                psp_Type.TypeName = (string)focusedNode.GetValue("TypeName");
                psp_Type.Flag = (int)focusedNode.GetValue("Flag");
                psp_Type.Flag2 = (string)focusedNode.GetValue("Flag2");
                psp_Type.ParentID = (int)focusedNode.GetValue("ID");
                psp_Type.Dy = (int)focusedNode.GetValue("Dy");
                psp_Type.Dy1 = (int)focusedNode.GetValue("Dy");

                try
                {
                    psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPowerProjectTypes", psp_Type);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加子项目出错：" + ex.Message);
                }
            }
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }




            FormTypeTitle1 frm = new FormTypeTitle1();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            if (treeList1.FocusedNode.ParentNode == null)
            {
                frm.IsParent = true;
                frm.TypeName = treeList1.FocusedNode.GetValue("TypeName").ToString();
                frm.Dydj = (int)treeList1.FocusedNode.GetValue("Dy");
            }
            else
            {
                frm.TypeName = treeList1.FocusedNode.ParentNode.GetValue("TypeName").ToString();
                frm.Dydj = (int)treeList1.FocusedNode.ParentNode.GetValue("Dy");
            }

            
            frm.Text = "修改项目";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PowerProjectTypes psp_Type = Services.BaseService.GetOneByKey<PowerProjectTypes>(treeList1.FocusedNode["ID"].ToString());
                psp_Type.Title = frm.TypeTitle;
                if (treeList1.FocusedNode.ParentNode == null)
                {
                    psp_Type.TypeName = frm.TypeName;
                    psp_Type.Dy = frm.Dydj;
                    psp_Type.Dy1 = frm.Dydj;

                    treeList1.FocusedNode.SetValue("Dy", frm.Dydj);
                    foreach (TreeListNode tll in treeList1.FocusedNode.Nodes)
                    {
                        if (tll.ParentNode != null)
                        tll.SetValue("Dy1", frm.Dydj);
                    }
                }


                try
                {
                    Common.Services.BaseService.Update<PowerProjectTypes>(psp_Type);
                    treeList1.FocusedNode.SetValue("Title", frm.TypeTitle);
                }
                catch
                {
                    //MsgBox.Show("修改出错：" + ex.Message);
                }
            }
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }


            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("此项目下有子项目，请先删除子项目！");
                return;
            }

            if (MsgBox.ShowYesNo("是否删除项目 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
            {
                PowerProjectTypes psp_Type = Services.BaseService.GetOneByKey<PowerProjectTypes>(treeList1.FocusedNode["ID"].ToString());
                PowerProjectValues PowerValues = new PowerProjectValues();
                PowerValues.TypeID = psp_Type.ID;


                //PowersTypes psp_Type = Services.BaseService.GetOneByKey<PowersTypes>(treeList1.FocusedNode["ID"].ToString());


                try
                {
                    //DeletePowerValuesByType里面删除数据和分类
                    Common.Services.BaseService.Update("DeletePowerProjectValuesByType", PowerValues);
                    treeList1.Nodes.Remove(treeList1.FocusedNode);
                    //LoadData();
                }
                catch //(Exception ex)
                {
                    //MsgBox.Show("删除出错：" + ex.Message);
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FormNewProjectYear frm = new FormNewProjectYear();
                frm.Flag2 = typeFlag2;
                int nFixedColumns = typeof(PowerProjectTypes).GetProperties().Length;
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
                    LoadData();
                    //AddColumn(frm.YearValue);
                }

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedColumn == null)
            {
                return;
            }

            //不是年份列
            if (treeList1.FocusedColumn.FieldName.IndexOf("年") == -1)
            {
                return;
            }

            if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedColumn.FieldName + " 及该年的所有数据？") != DialogResult.Yes)
            {
                return;
            }


            //PowerProjectValues PowerValues = new PowerProjectValues();
            //PowerValues.ID = typeFlag2;//借用ID属性存放Flag2
            //PowerValues.Year = (int)treeList1.FocusedColumn.Tag;

            Hashtable hs = new Hashtable();
            hs.Add("ID", typeFlag2);
            hs.Add("Year", (int)treeList1.FocusedColumn.Tag);
            try
            {
                //DeletePowerValuesByYear删除数据和年份
                int colIndex = treeList1.FocusedColumn.AbsoluteIndex;
                Common.Services.BaseService.Update("DeletePowerProjectValuesByYear", hs);

                LoadData();
            }
            catch (Exception ex)
            {
                MsgBox.Show("删除出错：" + ex.Message);
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
    }
}