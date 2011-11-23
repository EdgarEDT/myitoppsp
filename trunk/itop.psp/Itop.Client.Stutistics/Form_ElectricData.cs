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
using System.IO;
using DevExpress.XtraGrid.Columns;
using Itop.Domain.HistoryValue;

namespace Itop.Client.Stutistics
{
    public partial class Form_ElectricData : FormBase
    {

        string title = "";
        string unit = "";
        bool isSelect = false;
        //string type = "辽宁电量数据";

        DevExpress.XtraGrid.GridControl gcontrol = new DevExpress.XtraGrid.GridControl();

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

        private int typeFlag2 = 100000000;
        //int aindex = 0;

        public Form_ElectricData()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.ctrlPSP_EachList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
          
            //this.ctrlPSP_EachList1.RefreshData(type);
            //this.ctrlPSP_EachList1.colListName.Width = 70;
            //this.ctrlPSP_EachList1.colRemark.Width = 5;

            //Show();
            //Application.DoEvents();

            InitRight();

            ReLoad();
        }


        private void InitRight()
        {

            if (!PrintRight)
            {
                barButtonItem6.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
                if (!AddRight)
                {
                    barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem12.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                if (!EditRight)
                {
                    barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem13.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem15.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                    barButtonItem17.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem18.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                }

                if (!DeleteRight)
                {
                    barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barButtonItem16.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                }
        
        }

        //void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{
        

        //}
        private void ReLoad()
        {
            //if (this.ctrlPSP_EachList1.FocusedObject == null)
            //    return;

            //typeFlag2 =(100000000+ this.ctrlPSP_EachList1.FocusedObject.ID);

            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();

            treeList1.ExpandAll();
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

                PSP_Types psp_Type = new PSP_Types();
                psp_Type.Flag2 = typeFlag2;
                IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByFlag2", psp_Type);

                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Types));

                treeList1.DataSource = dataTable;

                treeList1.Columns["Title"].Caption = "项目名称";
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

                foreach (PSP_Years item in listYears)
                {
                    AddColumn(item.Year);
                }
                Application.DoEvents();

                LoadValues();

                treeList1.ExpandAll();

               
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            
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
                if(node != null)
                {
                    node.SetValue(value.Year + "年", value.Value);
                }
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
              //  CalculateNodeSum(tln);
            }
        }

       
        private void ReAddColumn(string filename,string year)
        {
            try
            {
                TreeListColumn column = treeList1.Columns[filename];
                //ArrayList al = new ArrayList();
                //al.Clear();
                //for (int i = 0; i < treeList1.Columns.Count; i++)
                //{
                //    if(treeList1.Columns[i].VisibleIndex!=-1)
                //     al.Add(treeList1.Columns[i].Caption);
                //}

                //if (al.Contains(year) == true)
                //{
                //    MsgBox.Show("已经存在此年份，要想添加，请先删除已存在的" + year+"!");
                //    return;
                //}
               // dataTable.Columns.Add(year, typeof(string));

             //   TreeListColumn column = treeList1.Columns.Insert(100);


                //column.OptionsColumn.AllowEdit = false;


               // column.FieldName = filename;
                column.Tag = year;
                column.Caption = year;
                column.Width = 70;
                column.OptionsColumn.AllowSort = false;
                column.VisibleIndex = 100;//有两列隐藏列

                column.ColumnEdit = this.repositoryItemTextEdit1;
                //treeList1.Columns["StartYear"].VisibleIndex = 4998;
                //treeList1.Columns["EndYear"].VisibleIndex = 4999;
                // treeList1.Columns["Remark"].VisibleIndex = 5000;

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
            treeList1.RefreshDataSource();
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
            treeList1.RefreshDataSource();
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


        private int GetInsertIndex2(int year)
        {
            int nFixedColumns = typeof(PSP_PowerTypes_Liao).GetProperties().Length - 2;//ID和ParentID列不在treeList1.Columns中

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
            if ((e.Value.ToString() != lastEditValue
           || lastEditNode != e.Node
           || lastEditColumn != e.Column)
           && e.Column.FieldName.IndexOf("年") > 0
           && e.Column.Tag != null)
            {
                SaveCellValue((int)treeList1.FocusedColumn.Tag, (int)treeList1.FocusedNode.GetValue("ID"), Convert.ToDouble(e.Value));
                ////if (SaveCellValue((int)treeList1.FocusedColumn.Tag, (int)treeList1.FocusedNode.GetValue("ID"), Convert.ToDouble(e.Value)))
                ////{
                ////    CalculateSum(e.Node, e.Column);
                ////}
                ////else
                ////{
                ////    treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, lastEditValue);
                ////}
            }
        }

        private bool SaveCellValue(int year, int typeID, double value)
        {
            PSP_Values PowerValues = new PSP_Values();
            PowerValues.TypeID = typeID;
            PowerValues.Value = value;
            PowerValues.Year = year;

            try
            {
                Common.Services.BaseService.Update<PowerValues>(PowerValues);
            }
            catch(Exception ex)
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

            //if (treeList1.FocusedNode.HasChildren
            //    || !base.EditRight)
            //{
            //    e.Cancel = true;
            //}
            e.Cancel = false;


        }


        //////计算静态总投资列表

        ////private void Sumss(TreeListNode node, TreeListColumn column)
        ////{
        ////    TreeListNode parentNode = node.ParentNode;

        ////    if (parentNode == null)
        ////    {
        ////        return;
        ////    }

        ////    double sum = 0;
        ////    foreach (TreeListNode nd in parentNode.Nodes)
        ////    {
        ////        object value = nd.GetValue(column.FieldName);
        ////        if (value != null && value != DBNull.Value)
        ////        {
        ////            sum += Convert.ToDouble(value);
        ////        }
        ////    }

        ////    parentNode.SetValue(column.FieldName, sum);


        ////    Sumss(parentNode, column);
        ////}



        //////计算静态总投资

        ////private void CalculateNodeSum(TreeListNode node)
        ////{
        ////    double sum = 0.0;
        ////    foreach (TreeListColumn col in treeList1.Columns)
        ////    {
        ////        if (col.Caption.IndexOf("年") > 0)
        ////        {
        ////            try
        ////            {
        ////                sum += (double)node[col.FieldName];
        ////            }
        ////            catch { }
        ////        }
        ////    }
        ////    node["JingTai"] = sum;

        ////    if (node.ParentNode != null)
        ////    {
        ////        CalculateNodeSum(node.ParentNode);
        ////    }
        ////}

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

            if (column.FieldName != "Lixi" && column.FieldName != "Yubei" && column.FieldName != "Dongtai")
            {
                SaveCellValue((int)column.Tag, (int)parentNode.GetValue("ID"), sum);
            }
            else
            {
              //  SaveCellValue(column.FieldName,(int)parentNode.GetValue("ID"), sum);           
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

            //if (this.ctrlPSP_EachList1.FocusedObject == null)
            //{
            //    MsgBox.Show("没有项目存在，请先添加项目分类管理！");
            //    return; 
            //}
            ////PSP_PowerTypes_Liao ppt = new PSP_PowerTypes_Liao();
            ////ppt.Flag2 = this.ctrlPSP_EachList1.FocusedObject.UID;
            ////IList<PSP_PowerTypes_Liao> ps = Common.Services.BaseService.GetList<PSP_PowerTypes_Liao>("SelectPSP_PowerTypes_LiaoByFlag2", ppt);

            ////if (ps.Count == 0)
            ////{
            ////    MsgBox.Show("没有记录存在，无法统计！");
            ////    return;
            ////}
           
            FormElectricPrint frm = new FormElectricPrint();
            
            frm.IsSelect = isSelect;
           //// frm.Text = this.ctrlPSP_EachList1.FocusedObject.ListName;
            frm.Text = "行业电量历史数据";
            frm.GridDataTable = ConvertTreeListToDataTable(treeList1);


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
            //////listColID.Add("Flag");
            //////dt.Columns.Add("Flag", typeof(int));

            //////listColID.Add("ParentID");
            //////dt.Columns.Add("ParentID", typeof(int));

            listColID.Add("Title");
            dt.Columns.Add("Title", typeof(string));
            dt.Columns["Title"].Caption = "项目名称";

            int i = 0;
            foreach (TreeListColumn column in xTreeList.Columns)
            {
                if (column.FieldName.IndexOf("年") > 0)
                {
                    listColID.Add(column.FieldName);
                    dt.Columns.Add(column.FieldName, typeof(string));
                }
                //////if (column.FieldName.Substring(0, 1) == "S" && column.VisibleIndex!=-1)
                //////{
                //////    listColID.Add(column.FieldName);
                //////    dt.Columns.Add(column.FieldName, typeof(string));
                //////   // dt.Columns[column.FieldName].Caption = column.Caption+ "@" +column.VisibleIndex.ToString();
                //////    dt.Columns[column.FieldName].Caption = column.Caption;
                //////}
            }

            foreach(TreeListNode node in xTreeList.Nodes)
            {
                AddNodeDataToDataTable(dt, node, listColID);
            }

            ////DataRow[] rows1 = dt.Select("ParentID=0");
            ////DataRow d1 = dt.NewRow();
            ////foreach (DataRow rowss1 in rows1)
            ////{
            ////    compute(dt, d1,rowss1);
            ////}
            ////d1["Title"] = "全地区合计";
            ////dt.Rows.Add(d1);
            return dt;
        }

        private void compute(DataTable dt, DataRow row1, DataRow row2)
        {
            double ss = 0;
            foreach (DataColumn dc in dt.Columns)
            {

                if (dc.ColumnName.IndexOf("年") > 0  || dc.ColumnName == "Lixi" || dc.ColumnName == "Yubei" || dc.ColumnName == "Dongtai")
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

                    if(dc.ColumnName.IndexOf("年") > 0){
                        ss += (double)row1[dc];
                        }
                }

                row1["Jingtai"] = ss;
            }
        }



        private void AddNodeDataToDataTable(DataTable dt, TreeListNode node, List<string> listColID)
        {
            DataRow newRow = dt.NewRow();
            foreach(string colID in listColID)
            {
                //分类名，第二层及以后在前面加空格
                if(colID == "Title" && node.ParentNode != null)
                {
                    newRow[colID] = "　　" + node[colID];
                   
                }
                else
                {
                    newRow[colID] = node[colID];
                }
            }

            //根分类结束后加空行

            if(node.ParentNode == null && dt.Rows.Count > 0 && node.Nodes.Count>0)
            {
                dt.Rows.Add(new object[] { });
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
          ////  this.ctrlPSP_EachList1.AddObjecta(type,false);

          //////  this.ctrlPSP_EachList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);

          ////  this.ctrlPSP_EachList1.RefreshData(type);
          ////  this.ctrlPSP_EachList1.colListName.Width = 70;
          ////  this.ctrlPSP_EachList1.colRemark.Width = 5;

          ////  Show();
          ////  Application.DoEvents();

          ////  InitRight();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.ctrlPSP_EachList1.UpdateObject();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //this.ctrlPSP_EachList1.DeleteObject("xuqiu");
            //if (this.ctrlPSP_EachList1.bl)
            //    ReLoad();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //typeFlag2 = this.ctrlPSP_EachList1.FocusedObject.UID;
            //if (this.ctrlPSP_EachList1.FocusedObject == null)
            //{
            //    MsgBox.Show("没有指定的项目存在，请先添加项目分类管理！");
            //    return;
            //}
            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加项目";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PSP_Types psp_Type = new PSP_Types();
                psp_Type.Title = frm.TypeTitle;
                psp_Type.Flag = 0;
                psp_Type.Flag2 = typeFlag2;
                psp_Type.ParentID = 0;

                try
                {
                    psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_Types", psp_Type);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加项目出错：" + ex.Message);
                }
            }
          //  ReLoad();
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
                PSP_Types psp_Type = new PSP_Types();
                psp_Type.Title = frm.TypeTitle;
                psp_Type.Flag = (int)focusedNode.GetValue("Flag");
                psp_Type.Flag2 = (int)focusedNode.GetValue("Flag2");
                psp_Type.ParentID = (int)focusedNode.GetValue("ID");

                try
                {
                    psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_Types", psp_Type);
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

            if (treeList1.FocusedNode.ParentNode == null)
            {
                MsgBox.Show("一级分类名称不能修改！");
                return;
            }
            ////FrmEditProject5 frm = new FrmEditProject5();
           
            ////frm.FlagId = typeFlag2.ToString();
            ////frm.Type = type;
            ////frm.PowerUId = treeList1.FocusedNode["ID"].ToString();
            ////frm.Isupdate = true;
            FormTypeTitle frm = new FormTypeTitle();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "修改项目名";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PSP_Types psp_Type = new PSP_Types();
                Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList1.FocusedNode);
                psp_Type.Title = frm.TypeTitle;

                try
                {
                    Common.Services.BaseService.Update<PSP_Types>(psp_Type);
                    treeList1.FocusedNode.SetValue("Title", frm.TypeTitle);
                }
                catch (Exception ex)
                {
                    //MsgBox.Show("修改出错：" + ex.Message);
                }
            }
            ReLoad();
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }
            
            //if(treeList1.FocusedNode.ParentNode == null)
            //{
            //    MsgBox.Show("一级分类为固定内容，不能删除！");
            //    return;
            //}

            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("此项目下有子项目，请先删除子项目！");
                return;
            }

            if (MsgBox.ShowYesNo("是否删除项目 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
            {
               // PSP_PowerTypes_Liao psp_Type = new PSP_PowerTypes_Liao();
               // Class1.TreeNodeToDataObject<PSP_PowerTypes_Liao>(psp_Type, treeList1.FocusedNode);
                PSP_Values PowerValues = new PSP_Values();
                PowerValues.TypeID = int.Parse(treeList1.FocusedNode["ID"].ToString());

                try
                {
                    //DeletePowerValuesByType里面删除数据和分类

                    Common.Services.BaseService.Update("DeletePSP_ValuesByType", PowerValues);
                   
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

                    //删除后，如果同级还有其它分类，则重新计算此类的所有年份数据

                    if (brotherNode != null)
                    {
                        foreach (TreeListColumn column in treeList1.Columns)
                        {
                            if (column.FieldName.IndexOf("年") > 0)
                            {
                                CalculateSum(brotherNode, column);
                            }
                        }
                    }
                }
                catch //(Exception ex)
                {
                    //MsgBox.Show("删除出错：" + ex.Message);
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            //if (this.ctrlPSP_EachList1.FocusedObject == null)
            //{
            //    MsgBox.Show("请先添加项目分类管理");
            //    return;
            //}

            FormNewYear2 frm = new FormNewYear2();
            frm.Flag2 = typeFlag2.ToString();
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

        //private void vGridControl_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        //{
        //    if (e.Row.Properties.Format.FormatType == DevExpress.Utils.FormatType.Numeric)
        //    {
        //        oldInput = InputLanguage.CurrentInputLanguage;
        //        InputLanguage.CurrentInputLanguage = null;
        //    }
        //    else
        //    {
        //        if (oldInput != null && oldInput != InputLanguage.CurrentInputLanguage)
        //        {
        //            InputLanguage.CurrentInputLanguage = oldInput;
        //        }
        //    }
        //}
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

            private void barButtonItem4_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
            {
                //if (this.ctrlPSP_EachList1.FocusedObject == null)
                //{
                //    MsgBox.Show("请先添加项目分类管理！");
                //    return;
                //}

            InsertLineData1();
        }

        private void InsertLineData1()
        {
            string columnname = "";

            if (treeList1.Nodes.Count == 0)
            {
                return;
            }

            IList<PSP_Values> pvlist = new List<PSP_Values>();
            pvlist.Clear();
                   try
                   {
                       DataTable dts = new DataTable();
                       OpenFileDialog op = new OpenFileDialog();
                       op.Filter = "Excel文件(*.xls)|*.xls";
                       if (op.ShowDialog() == DialogResult.OK)
                       {
                           dts = GetExcel(op.FileName);
                               //////    string LL2 = "";
                               //////    try
                               //////    {
                               //////        LL2 = dts.Rows[i][dc.ColumnName].ToString();
                               //////    }
                               //////    catch { }
                               //////m2.GetType().GetProperty(dc.ColumnName).SetValue(m2, LL2, null);
                           ArrayList al = new ArrayList();
                           al.Clear();
                           foreach (TreeListColumn tcl in treeList1.Columns)
                           {
                               al.Add(tcl.Caption);
                           }
                          
                           
                             foreach (DataColumn dc in dts.Columns)
                             {
                                 int j = 0;
                                   columnname = dc.ColumnName;
                                 
                                   try
                                   {
                                       if (dc.ColumnName.ToString().Substring(4,1)=="年" &&dc.ColumnName.ToString().Length == 5)
                                       {
                                           if (!al.Contains(dc.ColumnName))
                                           {
                                               NewYear(int.Parse(dc.ColumnName.ToString().Substring(0, 4)));
                                           }
                                           foreach (TreeListColumn tcl in treeList1.Columns)
                                           {
                                               try
                                               {
                                                   if (tcl.Caption.ToString().Substring(4, 1) == "年" && tcl.Caption.ToString().Length == 5)
                                                   {
                                                       if (tcl.Caption == dc.ColumnName)
                                                       {
                                                           if (MessageBox.Show(tcl.Caption + "列已经存在！是否更新此列的数据！", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                                               continue;
                                                           for (int i = 0; i < dts.Rows.Count; i++)
                                                          {
                                                              PSP_Values m1 = new PSP_Values();
                                                              if (dts.Rows[i][dc.ColumnName].ToString() == null || dts.Rows[i][dc.ColumnName].ToString() == "")
                                                              {
                                                                  m1.Year = int.Parse(dc.ColumnName.ToString().Substring(0, 4));
                                                              }
                                                              else
                                                              {
                                                                 m1.Value = double.Parse(dts.Rows[i][dc.ColumnName].ToString());
                                                                 m1.Year = int.Parse(dc.ColumnName.ToString().Substring(0, 4));
                                                              }
                                                                m1.TypeID = int.Parse(treeList1.Nodes[j]["ID"].ToString());
                                                                pvlist.Add(m1);
                                                             if (treeList1.Nodes[j].HasChildren == true)
                                                             {
                                                                for (int m = 0; m < treeList1.Nodes[j].Nodes.Count; m++)
                                                                 {
                                                                    CalChildNode1(treeList1.Nodes[j].Nodes[m], ref i, dts, dc, ref pvlist);
                                                                 }
                                                            }
                                                           j++;
                                                          }
                                                        }
                                                    }
                                               }
                                               catch { }
                                             }
                                       }
                                       else
                                       {
                                           continue;
                                       }
                                   }
                                   catch  { }
                               }
                               foreach (PSP_Values pv in pvlist)
                                   Common.Services.BaseService.Update("UpdatePSP_Values", pv);
                       }
                       ReLoad();
                       
                   }
                   catch { MsgBox.Show("导入格式不正确！"); }
        }
        private void NewYear( int year)
        {
            if (!base.AddRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            FormNewYear2 frm = new FormNewYear2();
            frm.Flag2 = typeFlag2.ToString();
            int nFixedColumns = typeof(PSP_Types).GetProperties().Length;
            int nColumns = treeList1.Columns.Count;
            if (nFixedColumns == nColumns + 2)//相等时，表示还没有年份，新年份默认为当前年减去15年
            {
                frm.YearValue = year;
            }
            else
            {
                //有年份时，默认为最大年份加1年
                frm.YearValue = year;
            }
            frm.AddYear();
           AddColumn(frm.YearValue);
        }
        private void CalChildNode1(TreeListNode node,ref int i,DataTable dt,DataColumn dc, ref IList<PSP_Values> psplist)
        {
            i++;
            PSP_Values m1=new PSP_Values();
            if (dt.Rows[i][dc.ColumnName].ToString() == null || dt.Rows[i][dc.ColumnName].ToString() == "")
             {  
                 m1.Year = int.Parse(dc.ColumnName.ToString().Substring(0, 4));
             }  
            else
             {
                 m1.Value = double.Parse(dt.Rows[i][dc.ColumnName].ToString());
                 m1.Year = int.Parse(dc.ColumnName.ToString().Substring(0, 4));
             }
             m1.TypeID =int.Parse(node["ID"].ToString());
             psplist.Add(m1);
             for (int m = 0; m < node.Nodes.Count; m++)
                {
                    CalChildNode1(node.Nodes[m], ref i, dt, dc,ref psplist);
                }
        }
       

        private DataTable GetExcel(string filepach)
        {
            string str;
            FarPoint.Win.Spread.FpSpread fpSpread1 = new FarPoint.Win.Spread.FpSpread();

            try
            {
                fpSpread1.OpenExcel(filepach);
            }
            catch
            {
                string filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName(filepach);
                File.Copy(filepach, filepath1);
                fpSpread1.OpenExcel(filepath1);
                File.Delete(filepath1);
            }
            DataTable dt = new DataTable();
            for (int k = 0; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            {
                dt.Columns.Add(fpSpread1.Sheets[0].Cells[0, k].Text);
            }

             for (int i =1; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; i++)
            {
                DataRow dr = dt.NewRow();
                str = "";
                for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
                {
                    str = str + fpSpread1.Sheets[0].Cells[i, j].Text;
                    dr[j] = fpSpread1.Sheets[0].Cells[i, j].Text;
                }
                if (str != "")
                    dt.Rows.Add(dr);

            }
            return dt;
        }
        private void ReLaodData()
        {
            //if (this.ctrlPSP_EachList1.FocusedObject == null)
            //    return;

            //typeFlag2 = this.ctrlPSP_EachList1.FocusedObject.ID;

            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }
        private void barButtonItem5_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            ////if (this.ctrlPSP_EachList1.FocusedObject == null)
            ////    return;
            ////FormClass fc = new FormClass();
            ////gridControl1.DataSource = fc.ConvertTreeListToDataTable(treeList1);
            ////gridView1.Columns["Title"].Caption = "项目名称";
            ////gridView1.Columns["Title"].VisibleIndex = 0;
            ////gridView1.Columns["JianSheXingZhi"].Caption = "建设性质";
            ////gridView1.Columns["RongLiang"].Caption = "变电容量";
            ////gridView1.Columns["ChangDu"].Caption = "线路长度";
            ////gridView1.Columns["TouZiZongEr"].Caption = "总投资（万元）";
            ////gridView1.Columns["CreatTime"].Visible = false;
            ////gridView1.Columns["Flag"].Visible = false;
            ////gridView1.Columns["Flag2"].Visible = false;
            ////title = this.ctrlPSP_EachList1.FocusedObject.ListName;
            ////FileClass.ExportExcel(gridControl1);

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (this.ctrlPSP_EachList1.FocusedObject == null)
            //{
            //    MsgBox.Show("请先添加项目分类管理！");
            //    return;
            //}
            ////try
            ////{
               
            ////    ArrayList al = new ArrayList();
            ////    al.Clear();
            ////    for (int i = 0; i < treeList1.Columns.Count; i++)
            ////    {
            ////        if (treeList1.Columns[i].VisibleIndex != -1)
            ////            al.Add(treeList1.Columns[i].Caption);
            ////    }

               
            ////    FrmPower_AddBands frm = new FrmPower_AddBands();

            ////    frm.Type = typeFlag2.ToString();
            ////    frm.Flag = "1";
            ////    frm.Type2 = type;
            ////    frm.AddFlag = "form1_liaoflag";
            ////    frm.Al = al;
            ////    if (frm.ShowDialog() == DialogResult.OK)
            ////    {
            ////        ReAddColumn(frm.ClassType,frm.textBox1.Text);
            ////    }

            ////}
            ////catch (Exception ex) { MsgBox.Show(ex.Message); }
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ////if (treeList1.FocusedColumn == null)
            ////{
            ////    return;
            ////}

            ////string fieldName = treeList1.FocusedColumn.FieldName;

            ////if (fieldName.Substring(0, 1) != "S")
            ////    return;
            ////try
            ////{
            ////    FrmPower_AddBands frm = new FrmPower_AddBands();
            ////    frm.ClassName = treeList1.FocusedColumn.Caption;
            ////    frm.ClassType = treeList1.FocusedColumn.FieldName;
            ////    frm.Type = typeFlag2.ToString();
            ////    frm.Flag ="1";
            ////    frm.Type2 = type;
            ////    frm.IsUpdate = true;

            ////    if (frm.ShowDialog() == DialogResult.OK)
            ////    {
            ////        // gc.Caption = frm.ClassName;
            ////        EditColumn(frm.ClassType, frm.textBox1.Text, treeList1.FocusedColumn.VisibleIndex);
            ////    }
            ////}
            ////catch (Exception ex) { MsgBox.Show(ex.Message); }

        }
        private void EditColumn(string filename, string year,int visibleindex)
        {
            try
            {
                TreeListColumn column = treeList1.Columns[filename];
                dataTable.Columns.Add(year, typeof(string));
                column.OptionsColumn.AllowEdit = false;
                column.Tag = year;
                column.Caption = year;
                column.Width = 70;
                column.OptionsColumn.AllowSort = false;
                column.VisibleIndex = visibleindex;//有两列隐藏列
                column.ColumnEdit = this.repositoryItemTextEdit1;
            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
            treeList1.RefreshDataSource();
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ////if (treeList1.FocusedColumn == null)
            ////{
            ////    return;
            ////}

            ////string fieldName = treeList1.FocusedColumn.FieldName;
            ////string caption = treeList1.FocusedColumn.Caption;
            ////if (fieldName.Substring(0, 1) != "S")
            ////{
            ////    MsgBox.Show("不能删除此列！");
            ////    return;
            ////}

            ////if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedColumn.Caption + " 的所有数据？") != DialogResult.Yes)
            ////{
            ////    return;
            ////}
           

            ////try
            ////{
            ////    int colIndex = treeList1.FocusedColumn.AbsoluteIndex;
               
            ////    Hashtable ha = new Hashtable();
            ////    ha.Add("Title", fieldName + "=''");
            ////    ha.Add("Flag2", typeFlag2);
            ////    Itop.Client.Common.Services.BaseService.Update("UpdatePSP_PowerTypes_LiaoByFlag2", ha);

            ////    PowerSubstationLine psl = new PowerSubstationLine();
           
            ////    psl.Flag = "1";
            ////    psl.ClassType = fieldName;
             
            ////    psl.Type = typeFlag2.ToString();
            ////    psl.Title = caption;
            ////    psl.Type2 = type;
            ////    Itop.Client.Common.Services.BaseService.Update("DeletePowerSubstationLineByAll", psl);

            ////   // Common.Services.BaseService.Update("DeletePowerValuesByYear", hs);
            ////   // dataTable.Columns.Remove(treeList1.FocusedColumn.Caption);
            ////   // treeList1.Columns.Remove(treeList1.FocusedColumn);
            ////    treeList1.FocusedColumn.VisibleIndex = -1;
            ////    if (colIndex >= treeList1.VisibleColumnCount)
            ////    {
            ////        colIndex--;
            ////    }
            ////    treeList1.FocusedColumn = treeList1.Columns[colIndex];

            ////}
            ////catch (Exception ex)
            ////{
            ////    MsgBox.Show("删除出错：" + ex.Message);
            ////}
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           //// FrmEditProject3 fep = new FrmEditProject3();
           //// fep.FlagId = typeFlag2.ToString();
           //// fep.PowerUId = treeList1.FocusedNode["ID"].ToString();
           ////string  uid1= treeList1.FocusedNode["ID"].ToString();
           //// fep.Type = type;

           
           //// if (fep.ShowDialog() == DialogResult.OK)
           //// {
           ////    ReLoad();
           ////     //LoadData();
           ////     treeList1.ExpandAll();
           ////     FoucsLocation(uid1, treeList1.Nodes);
           //// }

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

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            //////if (treeList1.FocusedNode == null)
            //////{
            //////    return;
            //////}
            //////FrmEditProject5 fep = new FrmEditProject5();
            //////string uid1="不存在 ";
            //////try
            //////{
            //////    fep.FlagId = typeFlag2.ToString();
            //////    fep.PowerUId = treeList1.FocusedNode["ID"].ToString();
            //////    uid1 = treeList1.FocusedNode["ID"].ToString();
            //////    fep.Type = type;
            //////    fep.Isupdate = true;
            //////}
            //////catch { }
            //////if (uid1 == "不存在 ")
            //////{
            //////    MsgBox.Show("没有记录存在，无法修改！");
            //////    return;
            //////}


            //////if (fep.ShowDialog() == DialogResult.OK)
            //////{
            //////    ReLoad();
            //////    //LoadData();
            //////    treeList1.ExpandAll();
            //////    FoucsLocation(uid1, treeList1.Nodes);
            //////}
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (ctrlPSP_EachList1.FocusedObject == null)
            //{
            //    MsgBox.Show("没有要删除的数据！");
            //    return;
            //}
            ////if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
            ////    return;

            ////string Flag2 = ctrlPSP_EachList1.FocusedObject.ID.ToString();
            ////Services.BaseService.Update("DeletePSP_PowerTypes_LiaoByFlag2", Flag2);
            ////MsgBox.Show("清除成功！");
            ////// InitSodata2();
            ////ReLoad();


        }

    }
}