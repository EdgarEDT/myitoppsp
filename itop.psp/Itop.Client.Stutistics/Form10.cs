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
using System.Xml;
using System.IO;
using Itop.Domain.Stutistics;

namespace Itop.Client.Stutistics
{
    public partial class Form10 : FormBase
    {

        string title = "";
        string unit = "单位：兆伏安、公里";
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

        DataTable dt = new DataTable();


        DataTable dataTable;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;

        private string typeFlag2 = "-1";

        public Form10()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            Show();
            Application.DoEvents();

            InitRight();

            //LoadData();
            InitSodata();
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

                foreach (DataRow rw in dataTable.Rows)
                {
                    string ss = rw["Code"].ToString();
                    LineInfo li = Common.Services.BaseService.GetOneByKey<LineInfo>(ss);
                    if (li != null && li.EleID != "")
                    {
                        rw["Title"] = li.LineName;
                        rw["L3"] = li.Length;
                        rw["L4"] = li.LineType;
                    }
                    substation li1 = Common.Services.BaseService.GetOneByKey<substation>(ss);
                    if (li1 != null && li1.EleID != "")
                    {
                        rw["Title"] = li1.EleName;
                        rw["L5"] = li1.ObligateField2;
                        rw["L2"] = li1.Number;
                        rw["L6"] = li1.Burthen;
                    }

                }



                treeList1.DataSource = dataTable;

                treeList1.Columns["Title"].Caption = "项目名称";
                treeList1.Columns["Title"].Width = 180;
                treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
                treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
                treeList1.Columns["Flag"].VisibleIndex = -1;
                treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["Flag"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
                treeList1.Columns["Flag2"].VisibleIndex = -1;
                treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["CreateTime"].VisibleIndex = -1;
                treeList1.Columns["CreateTime"].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["CreateTime"].SortOrder = System.Windows.Forms.SortOrder.Ascending;


                treeList1.Columns["UpdateTime"].VisibleIndex = -1;
                treeList1.Columns["UpdateTime"].OptionsColumn.ShowInCustomizationForm = false;


                treeList1.Columns["IsConn"].VisibleIndex = -1;
                treeList1.Columns["IsConn"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["StartYear"].VisibleIndex = -1;
                treeList1.Columns["StartYear"].OptionsColumn.ShowInCustomizationForm = false;

                treeList1.Columns["EndYear"].VisibleIndex = -1;
                treeList1.Columns["EndYear"].OptionsColumn.ShowInCustomizationForm = false;


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

            IList<PowerProValues> listValues = Common.Services.BaseService.GetList<PowerProValues>("SelectPowerProValuesListByFlag2", typeFlag2);



            foreach (PowerProValues value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null)
                {
                    node.SetValue(value.Year, value.Value);
                }
            }


            //foreach (TreeListNode tln in treeList1.Nodes)
            //{
            //    string ss = tln["Code"].ToString();
            //    LineInfo li = Common.Services.BaseService.GetOneByKey<LineInfo>(ss);
            //    if (li != null)
            //    {
            //        tln.SetValue("L2", li.Length);
            //        tln.SetValue("L3", li.LineType);
            //    }
            //    substation li1 = Common.Services.BaseService.GetOneByKey<substation>(ss);
            //    if (li1 != null)
            //    {
            //        tln.SetValue("L4", li1.ObligateField1);
            //        tln.SetValue("L5", li1.Burthen);
            //    }
            //}
        }



        //添加年份后，新增一列
        private void AddColumn(string year)
        {
            try
            {

                dataTable.Columns.Add(year , typeof(string));

                TreeListColumn column = treeList1.Columns.Insert(100);


                column.OptionsColumn.AllowEdit = false;


                column.FieldName = year ;
                column.Tag = year;
                column.Caption = year ;
                column.Width = 70;
                column.OptionsColumn.AllowSort = false;
                column.VisibleIndex = 100;//有两列隐藏列

                column.ColumnEdit = this.repositoryItemMemoEdit2;

                treeList1.Columns["Remark"].VisibleIndex = 5000;

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
            treeList1.RefreshDataSource();
        }

        private void AddColumn1()//静态总投资
        {
            //TreeListColumn column = treeList1.Columns["StartYear"];
            //column.Caption = "计划开工时间";
            //column.Width = 70;
            //column.OptionsColumn.AllowSort = false;
            //column.VisibleIndex = -1;
            ////column.ColumnEdit = repositoryItemTextEdit1;
            //column.ColumnEdit = this.repositoryItemMemoEdit1;
            //column.OptionsColumn.AllowEdit = false;

            //column = treeList1.Columns["EndYear"];
            //column.Caption = "计划结束时间";
            //column.Width = 70;
            //column.OptionsColumn.AllowSort = false;
            //column.VisibleIndex = -1;
            ////column.ColumnEdit = repositoryItemTextEdit1;
            //column.ColumnEdit = this.repositoryItemMemoEdit1;
            //column.OptionsColumn.AllowEdit = false;




            TreeListColumn column = treeList1.Columns["Remark"];
            column.Caption = "备注";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 799;
            //column.ColumnEdit = repositoryItemTextEdit1;
            column.ColumnEdit = this.repositoryItemMemoEdit1;
            column.OptionsColumn.AllowEdit = false;

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
            column.Caption = "台数";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 800;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L2"];
            column.Caption = "容量(MVA)";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 801;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L5"];
            column.Caption = "负荷率(%)";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 802;
            //column.VisibleIndex = -1;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L6"];
            column.Caption = "最大负荷(MVA)";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 803;
            //column.VisibleIndex = -1;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L3"];
            column.Caption = "长度(KM)";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 804;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L4"];
            column.Caption = "型号";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 805;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            

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


            if (e.Column.FieldName == "Remark")
            {
                SaveRemarkValue(treeList1.FocusedNode.GetValue("ID").ToString(), e.Value.ToString());
            }


            if (e.Column.FieldName != "Remark" && e.Column.FieldName != "Title")
            {

                SaveCellValue(treeList1.FocusedColumn.Tag.ToString(), treeList1.FocusedNode.GetValue("ID").ToString(), e.Value.ToString());
            }


        }

        private bool SaveRemarkValue(string typeID, string value)
        {
            PowerProTypes ppt = new PowerProTypes();
            ppt.ID = typeID;
            ppt.Flag2=typeFlag2;


            PowerProTypes ps = Services.BaseService.GetOneByKey<PowerProTypes>(ppt);//<PowerProTypes>(typeID);

            ps.Remark = value;



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





        private void treeList1_ShowingEditor(object sender, CancelEventArgs e)
        {
            
            //if(treeList1.FocusedNode.HasChildren)
            //{
            //    e.Cancel = true;
            //}
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



            Form10Print frm = new Form10Print();
            frm.IsSelect = isSelect;
            frm.Text = "输变电设备情况表";
            frm.GridDataTable = ConvertTreeListToDataTable(treeList1);


            if (frm.ShowDialog() == DialogResult.OK && isSelect)
            {
                gcontrol = frm.gridControl1;
                title = frm.Title;
                unit = "单位：兆伏安、公里";
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
             if (node.Nodes.Count > 0 || node.ParentNode != null)
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
            LoadData();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormTitleEdit frm = new FormTitleEdit();
            frm.Text = "增加项目";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PowerProTypes psp_Type = new PowerProTypes();
                psp_Type.Title = frm.TypeTitle;
                psp_Type.Flag = frm.PowerType;
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


            FormTitleEdit frm = new FormTitleEdit();
            frm.Text = "增加" + focusedNode.GetValue("Title") + "的子项目";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PowerProTypes psp_Type = new PowerProTypes();
                psp_Type.Title = frm.TypeTitle;
                psp_Type.Flag = frm.PowerType;
                psp_Type.Flag2 = (string)focusedNode.GetValue("Flag2");
                psp_Type.ParentID = focusedNode.GetValue("ID").ToString();

                try
                {
                    Common.Services.BaseService.Create("InsertPowerProTypes", psp_Type);
                    LoadData();

                    FoucsLocation(psp_Type.ID, treeList1.Nodes); 
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

            if (treeList1.FocusedNode.ParentNode == null)
            {
                MsgBox.Show("一级项目名称不能修改！");
                return;
            }

            FormTitleEdit frm = new FormTitleEdit();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            try
            {
                if (treeList1.FocusedNode["Flag"].ToString() == "2")
                {
                    frm.PowerType = 2;
                }
                else
                {
                    frm.PowerType = 1;
                }
            }
            catch { }
            frm.Text = "修改项目";
            frm.Isupdate = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string id = treeList1.FocusedNode["ID"].ToString();
                    string flag22 = typeFlag2;

                    PowerProTypes pptss = new PowerProTypes();
                    pptss.ID = id;
                    pptss.Flag2 = flag22;


                    PowerProTypes psp_Type = Services.BaseService.GetOneByKey<PowerProTypes>(pptss);
                psp_Type.Title = frm.TypeTitle;
                psp_Type.Flag = frm.PowerType;


                if (psp_Type.Code != "")
                {
                    LineInfo li3 = Services.BaseService.GetOneByKey<LineInfo>(psp_Type.Code);
                    if (li3 != null)
                    {
                        li3.LineName = psp_Type.Title;
                        Common.Services.BaseService.Update<LineInfo>(li3);
                    }

                    substation sb3 = Services.BaseService.GetOneByKey<substation>(psp_Type.Code);
                    if (sb3 != null)
                    {
                        sb3.EleName = psp_Type.Title;
                        Common.Services.BaseService.Update<substation>(sb3);
                    }
                }
                
                    Common.Services.BaseService.Update<PowerProTypes>(psp_Type);
                    treeList1.FocusedNode.SetValue("Title", frm.TypeTitle);

                    //FoucsLocation(id, treeList1.Nodes);
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
                MsgBox.Show("一级项目不允许删除！");
                return;
            }

            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("此项目下有子项目，请先删除子项目！");
                return;
            }

            if (MsgBox.ShowYesNo("是否删除项目 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
            {

                
                PowerProValues PowerValues = new PowerProValues();
                PowerValues.TypeID = treeList1.FocusedNode["ID"].ToString();
                PowerValues.Year = typeFlag2;
                PowerProTypes ppss = new PowerProTypes();
                ppss.ID = treeList1.FocusedNode["ID"].ToString();
                ppss.Flag2 = typeFlag2;
                PowerProTypes ppss1 = Services.BaseService.GetOneByKey<PowerProTypes>(ppss);
                PowerValues.TypeID1 = ppss1.Code;
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
                frm.Type1 = typeFlag2;// ctrlPowerEachList1.FocusedObject.UID;

                

                if (frm.ShowDialog() == DialogResult.OK)
                {
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

            string fieldName = treeList1.FocusedColumn.FieldName;
            if (fieldName == "Title" || fieldName == "Remark" || fieldName == "L1" || fieldName == "L2" || fieldName == "L3" || fieldName == "L4" ||  fieldName == "L5" || fieldName == "L6" || fieldName == "StartYear" ||  fieldName == "EndYear")
            {
                return;
            }


            if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedColumn.FieldName + " 及该类别数据？") != DialogResult.Yes)
            {
                return;
            }

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
            fep.IsStuff = true;
            fep.FlagId = typeFlag2;
            fep.PowerUId = treeList1.FocusedNode["ID"].ToString();
            string uid1 = treeList1.FocusedNode["ID"].ToString();
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
                FoucsLocation(uid1, treeList1.Nodes);
            }
        }

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;

            FrmEditProject fep = new FrmEditProject();
            fep.IsStuff = true;
            fep.FlagId = typeFlag2;
            fep.PowerUId = treeList1.FocusedNode["ID"].ToString();
            string uid1 = treeList1.FocusedNode["ID"].ToString();
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
                FoucsLocation(uid1, treeList1.Nodes);
            }
        }


        private void InitSodata()
        {


            Hashtable hs = new Hashtable();
            Hashtable hs1 = new Hashtable();
            IList<LineInfo> listLineInfo = Services.BaseService.GetList<LineInfo>("SelectLineInfoByPowerIDStuff", typeFlag2);
            foreach (LineInfo l1 in listLineInfo)
            {
                hs.Add(Guid.NewGuid().ToString(), l1.UID);
            }



            IList<substation> listsubstation = Services.BaseService.GetList<substation>("SelectsubstationByPowerIDStuff", typeFlag2);
            foreach (substation s1 in listsubstation)
            {
                hs.Add(Guid.NewGuid().ToString(), s1.UID);
            }

            PowerProTypes psp_Type = new PowerProTypes();
            psp_Type.Flag2 = typeFlag2;
            IList<PowerProTypes> listProTypes = Common.Services.BaseService.GetList<PowerProTypes>("SelectPowerProTypesByFlag2", psp_Type);
            foreach (PowerProTypes ps in listProTypes)
            {
                hs1.Add(Guid.NewGuid().ToString(), ps.Code);
            }



            foreach (PowerProTypes p1 in listProTypes)
            {

                if (p1.Code != "" && !hs.ContainsValue(p1.Code))
                {
                    //删除
                    Services.BaseService.Delete<PowerProTypes>(p1);
                }
            }


            foreach (LineInfo l2 in listLineInfo)
            {
                if (!hs1.ContainsValue(l2.UID) && l2.ObligateField1 != "")
                {
                    //添加
                    try
                    {
                        PowerProTypes ps2 = new PowerProTypes();
                        ps2.ParentID = l2.Voltage.ToUpper().Replace("KV", "");
                        ps2.Title = l2.LineName;
                        ps2.Code = l2.UID;
                        ps2.Flag = 1;
                        ps2.L3 = double.Parse(l2.Length);
                        ps2.L4 = l2.LineType;

                        ps2.Flag2 = "-1";
                        Services.BaseService.Create<PowerProTypes>(ps2);
                    }
                    catch { }

                }
            }


            foreach (substation s2 in listsubstation)
            {
                if (!hs1.ContainsValue(s2.UID) && s2.ObligateField1 != "")
                {
                    //添加
                    try
                    {
                        PowerProTypes ps3 = new PowerProTypes();
                        ps3.ParentID = s2.ObligateField1;
                        ps3.Title = s2.EleName;
                        ps3.Code = s2.UID;
                        ps3.L2 = double.Parse(s2.Number.ToString());
                        ps3.L5 = double.Parse(s2.ObligateField2);
                        ps3.L6 = double.Parse(s2.Burthen.ToString());

                        ps3.Flag = 2;
                        ps3.Flag2 = "-1";
                        Services.BaseService.Create<PowerProTypes>(ps3);
                    }
                    catch { }

                }
            }



            LoadData();








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