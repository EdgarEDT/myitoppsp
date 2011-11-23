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
using Itop.Domain.Graphics;

namespace Itop.Client.Stutistics
{
    public partial class Form6 : FormBase
    {

        string title = "";
        string unit = "";
        bool isSelect = false;
        string type = "JSXM";

        DevExpress.XtraGrid.GridControl gcontrol = null;

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

        private string typeFlag2 = "-1";
        //int aindex = 0;

        public Form6()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //////this.ctrlPowerEachList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            //////this.ctrlPowerEachList1.RefreshData(type);

            Show();
            Application.DoEvents();
            //this.Cursor = Cursors.WaitCursor;
            //treeList1.BeginUpdate();


            LoadData();
            //treeList1.EndUpdate();
            //this.Cursor = Cursors.Default;


            InitRight();

            //this.ctrlPowerEachList1.colListName.Caption = "规划名称";
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

            
            }

            if (!DeleteRight)
            {
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            
            }
        
        }

        //////void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //////{
        //////    if (this.ctrlPowerEachList1.FocusedObject == null)
        //////        return;
        //////    //this.ctrlPowerEachList1.LineUID = this.ctrlPowerEachTotalList1.FocusedObject.UID;
        //////    //this.ctrlPowerEachList1.LineName = this.ctrlPowerEachTotalList1.FocusedObject.ListName;
        //////    //this.ctrlPowerEachList1.RefreshData();
        //////    typeFlag2 = this.ctrlPowerEachList1.FocusedObject.UID;

        //////    this.Cursor = Cursors.WaitCursor;
        //////    treeList1.BeginUpdate();
        //////    LoadData();
        //////    treeList1.EndUpdate();
        //////    this.Cursor = Cursors.Default;
        //////}


        //加载设备情况表列字段
        private void LoadData()
        {
            try
            {
                if (dataTable != null)
                {
                    dataTable.Columns.Clear();
                    treeList1.Columns.Clear();
                }

                PowerProTypes psp_Type = new PowerProTypes();
                psp_Type.Flag2 = typeFlag2;
                IList listTypes = new ArrayList();
                try
                {
                    listTypes = Common.Services.BaseService.GetList("SelectPowerProTypesByFlag2", psp_Type);

                }
                catch (Exception ex)
                { MsgBox.Show(ex.Message); }
                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PowerProTypes));

                treeList1.DataSource = dataTable;

                treeList1.Columns["Title"].Caption = "项目名称";
                treeList1.Columns["Title"].Width = 180;
                treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
                treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
                treeList1.Columns["Flag"].VisibleIndex = -1;
                treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["Flag2"].VisibleIndex = -1;
                treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;


                AddColumn2();


                PowerProYears psp_Year = new PowerProYears();
                psp_Year.Flag = typeFlag2;
                IList<PowerProYears> listYears = Common.Services.BaseService.GetList<PowerProYears>("SelectPowerProYearsListByFlag", psp_Year);

                foreach (PowerProYears item in listYears)
                {
                    AddColumn(item.Year);
                }

                AddColumn1();


                Application.DoEvents();

                LoadValues();

                treeList1.ExpandAll();
            }
            catch
            {
                
            
            }
        }

        //读取数据
        private void LoadValues()
        {
            //PowerProValues PowerValues = new PowerProValues();
            //PowerValues.Year = typeFlag2;//用ID字段存放Flag2的值

            //PowerValues.TypeID1 = typeFlag2;

            IList<PowerProValues> listValues = Common.Services.BaseService.GetList<PowerProValues>("SelectPowerProValuesListByFlag2", typeFlag2);



            foreach (PowerProValues value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null)
                {
                    node.SetValue(value.Year, value.Value);
                }
            }


            foreach (TreeListNode tln in treeList1.Nodes)
            {
                string ss = tln["Code"].ToString();
                LineInfo li = Common.Services.BaseService.GetOneByKey<LineInfo>(ss);
                if (li != null)
                {
                    tln.SetValue("L1", li.Length);
                    tln.SetValue("L2", li.LineType);
                    tln.SetValue("L3", li.Voltage);
                }
                substation li1 = Common.Services.BaseService.GetOneByKey<substation>(ss);
                if (li1 != null)
                {
                    tln.SetValue("L3", li1.ObligateField1);
                    tln.SetValue("L4", li1.Burthen);
                    tln.SetValue("L5", li1.ObligateField2);
                }
            
            
            
            }




            
            ////foreach(TreeListNode tln in treeList1.Nodes)
            ////{
            ////    CalChildNode(tln);
            ////}
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

        private void AddColumn(string year)
        {
            try
            {
                //int nInsertIndex = GetInsertIndex(year);

                dataTable.Columns.Add(year , typeof(string));


                //TreeListColumn column = treeList1.Columns.Add();
                TreeListColumn column = treeList1.Columns.Insert(100);


                column.OptionsColumn.AllowEdit = false;


                column.FieldName = year ;
                column.Tag = year;
                column.Caption = year ;
                column.Width = 70;
                column.OptionsColumn.AllowSort = false;
                column.VisibleIndex = 100;//有两列隐藏列
                //aindex = nInsertIndex - 2;
                //column.ColumnEdit = repositoryItemTextEdit1;
                column.ColumnEdit = this.repositoryItemMemoEdit2;

                //foreach (TreeListColumn col in treeList1.Columns)
                //{
                //    if (col.Caption.IndexOf("年") > 0)
                //    {
                //        col.VisibleIndex = (int)col.Tag;
                //    }
                //}
                treeList1.Columns["Remark"].VisibleIndex = 5000;
                //treeList1.Columns["Lixi"].VisibleIndex = 5000;
                //treeList1.Columns["Yubei"].VisibleIndex = 5000;
                //treeList1.Columns["Dongtai"].VisibleIndex = 5000;
            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
            treeList1.RefreshDataSource();
        }

        private void AddColumn1()//静态总投资

        {
            TreeListColumn column = treeList1.Columns["Remark"];
            column.Caption = "备注";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 799;
            //column.ColumnEdit = repositoryItemTextEdit1;
            column.ColumnEdit = this.repositoryItemMemoEdit1;
            column.OptionsColumn.AllowEdit = true;

            TreeListColumn column1 = treeList1.Columns["Code"];
            column1.Caption = "备注";
            column1.Width = 70;
            column1.OptionsColumn.AllowSort = false;
            column1.VisibleIndex = 799;
            //column1.ColumnEdit = repositoryItemTextEdit1;
            column.ColumnEdit = this.repositoryItemMemoEdit1;
            column1.OptionsColumn.AllowEdit = false;
            column1.VisibleIndex = -1;
            column1.OptionsColumn.ShowInCustomizationForm = false;
        }

        private void AddColumn2()//建设期间贷款利息
        {
            TreeListColumn column = treeList1.Columns["L1"];
            column.Caption = "长度";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 800;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L2"];
            column.Caption = "类型";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 801;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L3"];
            column.Caption = "电压";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 802;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L4"];
            column.Caption = "负荷率";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 803;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L5"];
            column.Caption = "所属区域";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 804;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

        }

        private void AddColumn3()//价格预备费

        {
            //dataTable.Columns.Add("Yubei", typeof(double));
            TreeListColumn column = treeList1.Columns["Yubei"];
            //column.FieldName = "Yubei";
            column.Caption = "价格预备费";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 801;
            column.ColumnEdit = repositoryItemTextEdit1;
        }

        private void AddColumn4()//动态投资

        {
            //dataTable.Columns.Add("Dongtai", typeof(double));
            TreeListColumn column = treeList1.Columns["Dongtai"];
            //column.FieldName = "Dongtai";
            column.Caption = "动态投资";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 802;
            column.ColumnEdit = repositoryItemTextEdit1;
        }





        private int GetInsertIndex(int year)
        {
            int nFixedColumns = typeof(PowerTypes).GetProperties().Length - 2;//ID和ParentID列不在treeList1.Columns中

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
            int nFixedColumns = typeof(PowerTypes).GetProperties().Length - 2;//ID和ParentID列不在treeList1.Columns中

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
            
            //////////if ((e.Value.ToString() != lastEditValue
            //////////    || lastEditNode != e.Node
            //////////    || lastEditColumn != e.Column)
            //////////    //&& e.Column.FieldName.IndexOf("年") > 0
            //////////    //&& e.Column.Tag != null
            //////////)
            //////////{

            if (e.Column.FieldName == "Remark")
            {
                SaveRemarkValue(treeList1.FocusedNode.GetValue("ID").ToString(), e.Value.ToString());
                //SaveCellValue((int)treeList1.FocusedColumn.Tag, (int)treeList1.FocusedNode.GetValue("ID"), Convert.ToDouble(e.Value));
            }


            if (e.Column.FieldName != "Remark" && e.Column.FieldName != "Title")
            {

                SaveCellValue(treeList1.FocusedColumn.Tag.ToString(), treeList1.FocusedNode.GetValue("ID").ToString(), e.Value.ToString());
            }


            //////////    if (e.Column.FieldName != "Lixi" && e.Column.FieldName != "Yubei" && e.Column.FieldName != "Dongtai" && e.Column.FieldName != "Dongtai")
            //////////    {
            //////////        SaveCellValue((int)treeList1.FocusedColumn.Tag, (int)treeList1.FocusedNode.GetValue("ID"), Convert.ToDouble(e.Value));
            //////////    }
            //////////    else
            //////////    {
            //////////        int id = (int)e.Node["ID"];

            //////////        SaveCellValue(e.Column.FieldName,id, Convert.ToDouble(e.Value));
            //////////    }
            //////////    //
            //////////    CalculateSum(e.Node, e.Column);
            //////////    //}
            //////////    //else
            //////////    //{
            //////////    //    treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, lastEditValue);
            //////////    //}
            //////////}

            //////////CalculateNodeSum(e.Node);
        }

        private bool SaveRemarkValue(string typeID, string value)
        {
            PowerProTypes ppt = new PowerProTypes();
            ppt.ID = typeID;
            ppt.Flag2=typeFlag2;


            PowerProTypes ps = Services.BaseService.GetOneByKey<PowerProTypes>(ppt);//<PowerProTypes>(typeID);

            ps.Remark = value;

            //PowerProValues PowerValues = new PowerProValues();
            //PowerValues.TypeID = typeID;
            //PowerValues.Value = value;
            //PowerValues.Year = year;

            try
            {
                Common.Services.BaseService.Update<PowerProTypes>(ps);
            }
            catch (Exception ex)
            {
                MsgBox.Show("保存数据出错：" + ex.Message);
                return false;
            }
            return true;
        }


        private bool SaveCellValue(string year, string typeID, string value)
        {
            PowerProValues PowerValues = new PowerProValues();
            PowerValues.TypeID = typeID;
            PowerValues.Value = value;
            PowerValues.Year = year;
            PowerValues.TypeID1 = typeFlag2;
            try
            {
                Common.Services.BaseService.Update<PowerProValues>(PowerValues);
            }
            catch(Exception ex)
            {
                MsgBox.Show("保存数据出错：" + ex.Message);
                return false;
            }
            return true;
        }


        ////////private bool SaveCellValue(int typeID,string year, string value)
        ////////{
        ////////    PowerProValues PowerValues = new PowerProValues();
        ////////    PowerValues.TypeID = typeID;
        ////////    PowerValues.Value = value;
        ////////    PowerValues.Year = year;

        ////////    try
        ////////    {
        ////////        Common.Services.BaseService.Update<PowerProValues>(PowerValues);
        ////////    }
        ////////    catch (Exception ex)
        ////////    {
        ////////        MsgBox.Show("保存数据出错：" + ex.Message);
        ////////        return false;
        ////////    }
        ////////    return true;
        ////////}



        private bool SaveCellValue(string fname,int id,double value)
        {
            //PowerValues PowerValues = new PowerValues();
            //PowerValues.TypeID = typeID;
            //PowerValues.Value = value;
            //PowerValues.Year = year;
            PowerTypes pt = Services.BaseService.GetOneByKey<PowerTypes>(id);

            switch (fname)
            {
                case "Lixi":
                    pt.Lixi = Convert.ToDouble(value);
                    break;

                case "Yubei":
                    pt.Yubei = Convert.ToDouble(value);
                    break;

                case "Dongtai":
                    pt.Dongtai = Convert.ToDouble(value);
                    break;
            }


            try
            {
                Common.Services.BaseService.Update<PowerTypes>(pt);
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
            //if(treeList1.FocusedNode.HasChildren)
            //{
            //    e.Cancel = true;
            //}
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


            Sumss(parentNode, column);
        }



        //计算静态总投资

        private void CalculateNodeSum(TreeListNode node)
        {
            double sum = 0.0;
            foreach (TreeListColumn col in treeList1.Columns)
            {
                if (col.Caption.IndexOf("年") > 0)
                {
                    try
                    {
                        sum += (double)node[col.FieldName];
                    }
                    catch { }
                }
            }
            node["JingTai"] = sum;

            if (node.ParentNode != null)
            {
                CalculateNodeSum(node.ParentNode);
            }
        }

        //当子分类数据改变时，计算其父分类的值

        ////////////private void CalculateSum(TreeListNode node, TreeListColumn column)
        ////////////{
        ////////////    TreeListNode parentNode = node.ParentNode;

        ////////////    if (parentNode == null)
        ////////////    {
        ////////////        return;
        ////////////    }

        ////////////    double sum = 0;
        ////////////    foreach(TreeListNode nd in parentNode.Nodes)
        ////////////    {
        ////////////        object value = nd.GetValue(column.FieldName);
        ////////////        if(value != null && value != DBNull.Value)
        ////////////        {
        ////////////            sum += Convert.ToDouble(value);
        ////////////        }
        ////////////    }

        ////////////    parentNode.SetValue(column.FieldName, sum);

        ////////////    if (column.FieldName != "Lixi" && column.FieldName != "Yubei" && column.FieldName != "Dongtai")
        ////////////    {
        ////////////        SaveCellValue((int)column.Tag, (int)parentNode.GetValue("ID"), sum);
        ////////////    }
        ////////////    else
        ////////////    {
        ////////////        SaveCellValue(column.FieldName,(int)parentNode.GetValue("ID"), sum);           
        ////////////    }

        ////////////    CalculateSum(parentNode, column);
        ////////////}

        private void treeList1_ShownEditor(object sender, EventArgs e)
        {
            lastEditColumn = treeList1.FocusedColumn;
            lastEditNode = treeList1.FocusedNode;
            lastEditValue = treeList1.FocusedNode.GetValue(lastEditColumn.FieldName).ToString();
        }

        //统计
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {



            Form3Print frm = new Form3Print();
            frm.IsSelect = isSelect;
            frm.Text = "输变电设备情况表";
            frm.GridDataTable = ConvertTreeListToDataTable(treeList1);


            if (frm.ShowDialog() == DialogResult.OK && isSelect)
            {
                gcontrol = frm.gridControl1;
                title = frm.Title;
                unit = "单位：万元";
                DialogResult = DialogResult.OK;
            }


        }

        //把树控件内容按显示顺序生成到DataTable中

        private DataTable ConvertTreeListToDataTable(DevExpress.XtraTreeList.TreeList xTreeList)
        {
            DataTable dt = new DataTable();
            List<string> listColID = new List<string>();

            foreach (TreeListColumn column in xTreeList.Columns)
            {

                    listColID.Add(column.FieldName);
                    dt.Columns.Add(column.FieldName, typeof(string));

            }


            foreach(TreeListNode node in xTreeList.Nodes)
            {
                AddNodeDataToDataTable(dt, node, listColID);
            }

            return dt;
        }

        private void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID)
        {
            DataRow newRow = dt.NewRow();
            foreach(string colID in listColID)
            {
                //分类名，第二层及以后在前面加空格
                if(colID == "Title" && node.ParentNode != null)
                {
                    newRow[colID] = "　" + node[colID];
                }
                else
                {
                        newRow[colID] = node[colID];
                }

             }

            dt.Rows.Add(newRow);

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
            //this.ctrlPowerEachList1.AddObjecta(type);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.ctrlPowerEachList1.UpdateObject();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.ctrlPowerEachList1.DeleteObject("sb");
            //LoadData();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加项目";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PowerProTypes psp_Type = new PowerProTypes();
                psp_Type.Title = frm.TypeTitle;
                psp_Type.Flag = 0;
                psp_Type.Flag2 = typeFlag2;
                psp_Type.ParentID = "0";

                try
                {
                    psp_Type.ID = Common.Services.BaseService.Create("InsertPowerProTypes", psp_Type).ToString();
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


            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加" + focusedNode.GetValue("Title") + "的子项目";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PowerProTypes psp_Type = new PowerProTypes();
                psp_Type.Title = frm.TypeTitle;
                psp_Type.Flag = (int)focusedNode.GetValue("Flag");
                psp_Type.Flag2 = (string)focusedNode.GetValue("Flag2");
                psp_Type.ParentID = focusedNode.GetValue("ID").ToString();

                try
                {
                    Common.Services.BaseService.Create("InsertPowerProTypes", psp_Type);
                    LoadData();
                    //treeList1.RefreshDataSource();
                    //dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
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

            //if (treeList1.FocusedNode.ParentNode == null)
            //{
            //    MsgBox.Show("一级分类名称不能修改！");
            //    return;
            //}

            FormTypeTitle frm = new FormTypeTitle();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "修改项目名";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {

                PowerProTypes psp_Type = Services.BaseService.GetOneByKey<PowerProTypes>(treeList1.FocusedNode["ID"].ToString());
                psp_Type.Title = frm.TypeTitle;

                
                    Common.Services.BaseService.Update<PowerProTypes>(psp_Type);
                    treeList1.FocusedNode.SetValue("Title", frm.TypeTitle);
                }
                catch (Exception ex)
                {
                    MsgBox.Show("修改出错：" + ex.Message);
                }
            }
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            if (treeList1.FocusedNode.ParentNode==null)
            {
                MsgBox.Show("根项目不允许删除！");
                return;
            }

            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("此项目下有子项目，请先删除子项目！");
                return;
            }

            if (MsgBox.ShowYesNo("是否删除项目 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
            {
                //PowerProTypes ppy = new PowerProTypes();
                //ppy.ID = treeList1.FocusedNode["ID"].ToString();
                //ppy.Flag2 = typeFlag2;


                //PowerProTypes psp_Type = Services.BaseService.GetOneByKey<PowerProTypes>(ppy);
                
                PowerProValues PowerValues = new PowerProValues();
                PowerValues.TypeID = treeList1.FocusedNode["ID"].ToString();
                PowerValues.Year = typeFlag2;

                try
                {
                    //DeletePowerValuesByType里面删除数据和分类

                    Common.Services.BaseService.Update("DeletePowerProValuesByType", PowerValues);

                    TreeListNode brotherNode = null;
                    try
                    {
                        if (treeList1.FocusedNode.ParentNode.Nodes.Count > 1)
                        {
                            brotherNode = treeList1.FocusedNode.PrevNode;
                            if (brotherNode == null)
                            {
                                brotherNode = treeList1.FocusedNode.NextNode;
                            }
                        }
                    }
                    catch { }
                    treeList1.DeleteNode(treeList1.FocusedNode);
                    LoadData();
                  
                }
                catch (Exception ex)
                {
                    MsgBox.Show("删除出错：" + ex.Message);
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FormNewType4 frm = new FormNewType4();
                frm.Flag2 = typeFlag2;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    AddColumn(frm.Type);
                }

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }




        private void barButtonItem4_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedColumn == null)
            {
                return;
            }


            if (treeList1.FocusedColumn.FieldName == "Title" || treeList1.FocusedColumn.FieldName == "Remark")
            {
                return;
            }

            try
            {
                FormNewType4 frm = new FormNewType4();


                frm.IsUpdate = true;
                frm.Type = treeList1.FocusedColumn.FieldName;
                frm.Flag2 = typeFlag2;
                frm.Type1 = typeFlag2;///////////////////

                

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //LoadData();
                    treeList1.FocusedColumn.Caption = frm.Type;
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


            if (treeList1.FocusedColumn.FieldName == "Title" || treeList1.FocusedColumn.FieldName == "Remark")
            {
                return;
            }


            if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedColumn.FieldName + " 及该类别数据？") != DialogResult.Yes)
            {
                return;
            }


            //PowerProValues PowerValues = new PowerProValues();
            //PowerValues.ID = typeFlag2;//借用ID属性存放Flag2
            //PowerValues.Year = treeList1.FocusedColumn.Tag.ToString();

            Hashtable hs = new Hashtable();
            hs.Add("ID", typeFlag2);
            hs.Add("Year", treeList1.FocusedColumn.Tag.ToString());

            try
            {
                //DeletePowerValuesByYear删除数据和年份

                int colIndex = treeList1.FocusedColumn.AbsoluteIndex;
                Common.Services.BaseService.Update("DeletePowerProValuesByYear", hs);
                dataTable.Columns.Remove(treeList1.FocusedColumn.FieldName);
                treeList1.Columns.Remove(treeList1.FocusedColumn);
                if (colIndex >= treeList1.Columns.Count)
                {
                    colIndex--;
                }
                treeList1.FocusedColumn = treeList1.Columns[colIndex];
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

        private void barButtonItem5_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmEditProject fep = new FrmEditProject();
            fep.FlagId = typeFlag2;
            fep.PowerUId = treeList1.FocusedNode["ID"].ToString();
            LineInfo li = Common.Services.BaseService.GetOneByKey<LineInfo>(treeList1.FocusedNode["Code"].ToString());
            if (li != null)
                fep.IsLine = true;
            substation li1 = Common.Services.BaseService.GetOneByKey<substation>(treeList1.FocusedNode["ID"].ToString());
            if (li1 != null)
                fep.IsPower = true;
            if (fep.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                treeList1.ExpandAll();
            }
        }

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;

            FrmEditProject fep = new FrmEditProject();
            fep.FlagId = typeFlag2;
            fep.PowerUId = treeList1.FocusedNode["ID"].ToString();
            LineInfo li = Common.Services.BaseService.GetOneByKey<LineInfo>(treeList1.FocusedNode["Code"].ToString());
            if (li != null)
                fep.IsLine = true;
            substation li1 = Common.Services.BaseService.GetOneByKey<substation>(treeList1.FocusedNode["ID"].ToString());
            if (li1 != null)
                fep.IsPower = true;

            if (fep.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                treeList1.ExpandAll();
            }
        }

        
    }
}