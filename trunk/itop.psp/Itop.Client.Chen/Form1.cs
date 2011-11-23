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

namespace Itop.Client.Chen
{
    public partial class Form1 : FormBase
    {
        DataTable dataTable;

        private TreeListNode lastEditNode = null;
        private TreeListColumn lastEditColumn = null;
        private string lastEditValue = string.Empty;
        private DataCommon dc = new DataCommon();
        TreeListNode treenode;
        private int typeFlag2 = 1;

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
        
        public Form1()
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

        private void Form1_Load(object sender, EventArgs e)
        {
            HideToolBarButton();

            Show();
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

            PSP_Types psp_Type = new PSP_Types();
            psp_Type.Flag2 = typeFlag2;
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByFlag2", psp_Type);

            dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Types));
            dataTable = dc.GetSortTable(dataTable, "Flag", true);

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

            foreach (PSP_Years item in listYears)
            {
                AddColumn(item.Year);
            }
            Application.DoEvents();

            LoadValues();
            foreach (PSP_Years item in listYears)
            {
                try
                {
                    //ComputeValue(item.Year);
                    ComputeElectricValue(item.Year);
                }
                catch { }
            }
            treeList1.ExpandAll();
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
            if (nodestr == "GDP(万元)" || nodestr == "全社会用电量(万千瓦时)" || nodestr == "单产耗能(万千瓦时/万元)")
            {
                MsgBox.Show( focusedNode.GetValue("Title").ToString()+"不允许添加子分类！");
                return;
            }

            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加" + focusedNode.GetValue("Title") + "的子分类";

            if(frm.ShowDialog() == DialogResult.OK)
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
    
            string nodestr = treeList1.FocusedNode.GetValue("Title").ToString();
            if (nodestr == "GDP(万元)" || nodestr == "全社会用电量(万千瓦时)" || nodestr == "单产耗能(万千瓦时/万元)" || nodestr == "人均生活用电量(千瓦时/人)" || nodestr == "B、城乡居民生活用电合计")
            {
                MsgBox.Show(treeList1.FocusedNode.GetValue("Title").ToString() + "不允许修改分类名称！");
                return;
            }
            //if (treeList1.FocusedNode.ParentNode == null)
            //{
            //    MsgBox.Show("一级分类名称不能修改！");
            //    return;
            //}

            string parentid = treeList1.FocusedNode["ParentID"].ToString();
            string flag = treeList1.FocusedNode["flag"].ToString();

            //if (parentid == "0" && (flag == "2" || flag == "3" || flag == "5" || flag == "7"))
            //{
            //    MsgBox.Show("固定分类，不能修改！");
            //    return;
            //}

            //if (treeList1.FocusedNode["Title"].ToString().IndexOf("居民") > -1 && parentid == "233")
            //{
            //    MsgBox.Show("固定分类，不能修改！");
            //    return;
            //}



            if (!base.EditRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            FormTypeTitle frm = new FormTypeTitle();
            frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "修改分类名";

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

            if (treeList1.FocusedNode.ParentNode == null)
            {
                MsgBox.Show("一级分类为固定内容，不能删除！");
                return;
            }
            string parentid = treeList1.FocusedNode["ParentID"].ToString();
            string flag = treeList1.FocusedNode["flag"].ToString();

            if (parentid == "0" && (flag == "2" || flag == "3" || flag == "5" || flag == "7"))
            {
                MsgBox.Show("固定分类，不能删除！");
                return;
            }

            if (treeList1.FocusedNode["Title"].ToString().IndexOf("居民") > -1 && parentid == "233")
            {
                MsgBox.Show("固定分类，不能删除！");
                return;
            }


            string nodestr = treeList1.FocusedNode.GetValue("Title").ToString();
            if (nodestr == "B、城乡居民生活用电合计")
            {
                MsgBox.Show("不允许删除" + treeList1.FocusedNode.GetValue("Title").ToString() + "名称！");
                return;
            }

            if (treeList1.FocusedNode.HasChildren)
            {
                MsgBox.Show("此分类下有子分类，请先删除子分类！");
                return;
            }



            if (!base.DeleteRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }

            if(MsgBox.ShowYesNo("是否删除分类 " + treeList1.FocusedNode.GetValue("Title") + "？") == DialogResult.Yes)
            {
                PSP_Types psp_Type = new PSP_Types();
                Class1.TreeNodeToDataObject<PSP_Types>(psp_Type, treeList1.FocusedNode);
                PSP_Values psp_Values = new PSP_Values();
                psp_Values.TypeID = psp_Type.ID;

                try
                {
                    //DeletePSP_ValuesByType里面删除数据和分类
                    Common.Services.BaseService.Update("DeletePSP_ValuesByType", psp_Values);

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
                            if (column.FieldName.IndexOf("年") > 0)
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

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {
            if (e.Value != null) 
            if ((e.Value.ToString() != lastEditValue
                || lastEditNode != e.Node
                || lastEditColumn != e.Column)
                && e.Column.FieldName.IndexOf("年") > 0
                && e.Column.Tag != null)
            {
                
                
                    if (SaveCellValue((int)treeList1.FocusedColumn.Tag, (int)treeList1.FocusedNode.GetValue("ID"), Convert.ToDouble(e.Value)))
                    {
                        CalculateSum(e.Node, e.Column);
                        if (e.Value.ToString() == "0" && isdel)
                        {

                            treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, null);
                            string flagid = " year=" + treeList1.FocusedColumn.FieldName.Replace("Y","").Replace("年","")+ " and TypeID=" + treeList1.FocusedNode["ID"];
                            //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                            //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                          IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                            if (listValues.Count == 1)
                            {
                                Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                            }
                            
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

        private bool SaveCellValue(int year, int typeID, double value)
        {
            PSP_Values psp_values = new PSP_Values();
            psp_values.TypeID = typeID;
            psp_values.Value = value;
            psp_values.Year = year;

            try
            {
                Common.Services.BaseService.Update<PSP_Values>(psp_values);
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
            parentNode.SetValue(column.FieldName, null);
        if (sum != 0)
        {
            if (!SaveCellValue((int)column.Tag, (int)parentNode.GetValue("ID"), sum))
            {
                return;
            }
        }
        else
        {
            if (parentNode.HasChildren)
            {
                string flagid = " year=" + treeList1.FocusedColumn.FieldName.Replace("Y", "").Replace("年", "") + " and TypeID=" + parentNode["ID"];
                //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                if (listValues.Count == 1)
                {
                    Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                }
            }
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
            FormChooseYears frm = new FormChooseYears();
            foreach (TreeListColumn column in treeList1.Columns)
            {
                if (column.FieldName.IndexOf("年") > 0)
                {
                    frm.ListYearsForChoose.Add((int)column.Tag);
                }
            }

            if(frm.ShowDialog() == DialogResult.OK)
            {
                Form1Result f = new Form1Result();
                f.CanPrint = base.PrintRight;
                f.Text = Title = "本地区" + frm.ListChoosedYears[0].Year + "～" + frm.ListChoosedYears[frm.ListChoosedYears.Count - 1].Year + "年经济和电力发展实绩";
                f.GridDataTable = ResultDataTable1(ConvertTreeListToDataTable(treeList1), frm.ListChoosedYears);
                f.IsSelect = IsSelect;
                DialogResult dr = f.ShowDialog();
                //f.ShowDialog();
                if (IsSelect && dr == DialogResult.OK)
                {
                    Gcontrol = f.gridControl1;
                    Unit = "单位：万元、小时、万千瓦时、万千瓦、万人";
                    DialogResult = DialogResult.OK;
                }
                try
                {

                    //barManager1.Bars.Clear();
                    //    this.barManager1.Bars.Add(bar1);
                }
                catch { }
            }

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

            listColID.Add("Title");
            dt.Columns.Add("Title", typeof(string));
            dt.Columns["Title"].Caption = "分类";
            foreach (TreeListColumn column in xTreeList.Columns)
            {
                if (column.FieldName.IndexOf("年") > 0)
                {
                    listColID.Add(column.FieldName);
                    dt.Columns.Add(column.FieldName, typeof(double));
                }
            }

            foreach(TreeListNode node in xTreeList.Nodes)
            {
                //if((int)node.GetValue("Flag") == 5)
                //{
                //    continue;
                //}
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
                    newRow[colID] = "　　" + node[colID];
                }
                else
                {
                    newRow[colID] = node[colID];
                }
            }

            //根分类结束后加空行
            if (node.ParentNode == null && dt.Rows.Count > 0)
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

            int nRowSumPower = 0;//总用电量所在行
            int nRowTotalPower = 0;//供电量所在行
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
                    if ((int)sourceRow["ParentID"] == 0)
                    {
                        switch ((int)sourceRow["Flag"])
                        {
                            case 2://总用电量
                                nRowSumPower = i;
                                break;

                            case 3://最高负荷，后面加一行Tmax
                                nRowTotalPower = i;
                                break;

                            case 5://最高负荷，后面加一行Tmax
                                nRowMaxLoad = i;
                                dtReturn.Rows.Add(new object[] { "Tmax" });
                                break;

                            case 7://总人口
                                nRowPopulation = i +2;//由于之前加了一行TMax，此处行加1
                                dtReturn.Rows.Add(new object[] { "人均用电量(千瓦时/人)" });
                                dtReturn.Rows.Add(new object[] { "人均生活用电量(千瓦时/人)" });
                                break;


                            default:
                                break;
                        }




                    }
                    else if (sourceRow["Title"].ToString().IndexOf("居民") > -1 && sourceRow["ParentID"].ToString() == "233")
                    {
                        nRowDenizen = i;
                    }





                    //if (sourceRow["Title"].ToString().IndexOf("全社会供电量") > -1)
                    //{
                    //    nRowTotalPower = i;
                    //}
                    //else if (sourceRow["Title"].ToString().IndexOf("居民") > -1 && sourceRow["ParentID"].ToString()=="2")
                    //{
                    //    nRowDenizen = i;
                    //}
                    //else if (sourceRow["Title"].ToString().IndexOf("供电量") > -1 && sourceRow["ParentID"].ToString()=="26")
                    //else if (sourceRow["Title"].ToString().IndexOf("全社会供电量") > -1)
                    //{
                    //    nRowTotalPower = i;
                    //}

                }
            }
            #endregion

            #region 计算TMax和人均用电量
            foreach (ChoosedYears choosedYear in listChoosedYears)
            {
                object sumPower = dtReturn.Rows[nRowSumPower+1][choosedYear.Year + "年"];//总用电量
                object numerator = dtReturn.Rows[nRowTotalPower-2][choosedYear.Year + "年"];//供电量
                object  denominator = dtReturn.Rows[nRowMaxLoad+1][choosedYear.Year + "年"];//最高负荷


                if (denominator != DBNull.Value && denominator.ToString()!="")
                {


                    if (Convert.ToInt32(denominator) != 0)
                    {
                        try
                        {
                            dtReturn.Rows[nRowMaxLoad + 2][choosedYear.Year + "年"] = (int)((double)numerator / (double)(denominator));
                        }
                        catch
                        {
                        }
                    }

                }
                    denominator = dtReturn.Rows[nRowPopulation][choosedYear.Year + "年"];  //人口
                    if (denominator != DBNull.Value)
                    {
                        try
                        {
                            dtReturn.Rows[nRowPopulation + 1][choosedYear.Year + "年"] = System.Math.Round((double)sumPower / (double)denominator, 3);
                        }
                        catch
                        {
                        }
                    }
               // }

                //if (nRowDenizen != 0)
                numerator = dtReturn.Rows[nRowDenizen][choosedYear.Year + "年"];  //人口
                
                denominator = dtReturn.Rows[nRowPopulation][choosedYear.Year + "年"];  //总人口
                if (denominator != DBNull.Value && numerator != DBNull.Value)
                {
                    try
                    {
                        if (nRowDenizen != 0)
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
                            if ((double)denominator == 0)
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
                    }

                    //本次计算增长率的列作为下次的起始列
                    columnBegin = columnEnd;
                }

            }
            #endregion
            //dtReturn.Rows.RemoveAt(nRowMaxLoad - 1);//移除多余的TMAX

            ////移除不需要显示的城市和郊县
            //dtReturn.Rows.RemoveAt(nRowPopulation + 2);
            //dtReturn.Rows.RemoveAt(nRowPopulation + 2);
            
            return dtReturn;
        }

        //潮阳府新统计表

        //根据选择的统计年份，生成统计结果数据表
        private DataTable ResultDataTable1(DataTable sourceDataTable, List<ChoosedYears> listChoosedYears)
        {
            int cityflag = -1;
            int countyflag = -1;
            DataTable dtReturn = new DataTable();
            dtReturn.Columns.Add("Title", typeof(string));
            foreach (ChoosedYears choosedYear in listChoosedYears)
            {
                dtReturn.Columns.Add(choosedYear.Year + "年", typeof(double));
                if (choosedYear.WithIncreaseRate)
                {
                    dtReturn.Columns.Add(choosedYear.Year + "增长率", typeof(double)).Caption = "增长率";
                }
            }

          //  int nRowSumPower = 0;//总用电量所在行
            int nRowTotalPower = 0;//供电量所在行
            int nRowMaxLoad = 0;//最高负荷所在行
            int nRowPopulation = 0;//人口所在行
            int nRowDenizen = 0;//居民用电量所在行

            #region 填充数据，获取总用电量所在行、最高负荷所在行、人口所在行
            for (int i = 0; i < sourceDataTable.Rows.Count; i++)
            {
                DataRow newRow = dtReturn.NewRow();
                DataRow sourceRow = sourceDataTable.Rows[i];
                foreach (DataColumn column in dtReturn.Columns)
                {
                    if (column.Caption != "增长率")
                    {
                        newRow[column.ColumnName] = sourceRow[column.ColumnName];
                    }
                }

////                1.TMAX值
////＝全社会供电量/供电最大负荷
////2.人均用电量
////＝全社会供电量/人口
////3.人均生活用电量 
////＝居民/人口                 （居民是属于会社会用电量下的二级分类）
////全社会供电量这种，建议判断时，用包含“全社会”并且包含“供电量”的方式。

////数据见附图
////1. 统计中，第二行应为分割行，不应有数据。
////2.目前有4个Tmax，应该只有一个，位于供电最大负荷下。目前没有统计出数据。
////3.统计缺少人均用电量 人均生活用电量。
////人均用电量＝全社会供电量/人口

                dtReturn.Rows.Add(newRow);

                //if (sourceRow["Flag"] != DBNull.Value)
                //{
                //    if ((int)sourceRow["ParentID"] == 0)
                //    {
                        string str = sourceRow["Title"].ToString();
                        if (str.Contains("全社会") == true && str.Contains("供电量") == true)
                            nRowTotalPower = i;
                        //if (str.Contains("供电最大负荷") == true)
                        //{

                        //    nRowMaxLoad = i;
                        //    dtReturn.Rows.Add(new object[] { "Tmax" });
                        //}
                        ////////if (str.Contains("人口") == true )
                        ////////{ 
                        ////////        nRowPopulation = i + 1;//由于之前加了一行TMax，此处行加1
                        ////////        dtReturn.Rows.Add(new object[] { "人均用电量(千瓦时/人)" });
                        ////////        dtReturn.Rows.Add(new object[] { "人均生活用电量(千瓦时/人)" });
                       
                        ////////}
                    //    }

                    //}
                   ////////if (sourceRow["Title"].ToString().IndexOf("居民") > -1)
                   //////// {
                   ////////     nRowDenizen = i;
                   //////// }
                   //////// if (dtReturn.Rows[dtReturn.Rows.Count-1]["Title"].ToString().IndexOf("城区") > -1)
                   //////// {
                   ////////     cityflag = dtReturn.Rows.Count - 1;
                   //////// }
                   //////// if (dtReturn.Rows[dtReturn.Rows.Count - 1]["Title"].ToString().IndexOf("郊县") > -1)
                   //////// {
                   ////////     countyflag = dtReturn.Rows.Count - 1;
                   //////// }
                    //if (sourceRow["Title"].ToString().IndexOf("全社会供电量") > -1)
                    //{
                    //    nRowTotalPower = i;
                    //}
                    //else if (sourceRow["Title"].ToString().IndexOf("居民") > -1 && sourceRow["ParentID"].ToString()=="2")
                    //{
                    //    nRowDenizen = i;
                    //}
                    //else if (sourceRow["Title"].ToString().IndexOf("供电量") > -1 && sourceRow["ParentID"].ToString()=="26")
                    //else if (sourceRow["Title"].ToString().IndexOf("全社会供电量") > -1)
                    //{
                    //    nRowTotalPower = i;
                    //}

                
            }
            #endregion

            #region 计算TMax和人均用电量
            ////foreach (ChoosedYears choosedYear in listChoosedYears)
            ////{
            ////   // object sumPower = dtReturn.Rows[nRowSumPower + 1][choosedYear.Year + "年"];//总用电量
            ////    object numerator = dtReturn.Rows[nRowTotalPower][choosedYear.Year + "年"];//供电量
            ////    object denominator = dtReturn.Rows[nRowMaxLoad][choosedYear.Year + "年"];//最高负荷


            ////    if (denominator != DBNull.Value && denominator.ToString() != "")
            ////    {


            ////        if (Convert.ToInt32(denominator) != 0)
            ////        {
            ////            try
            ////            {
            ////                dtReturn.Rows[nRowMaxLoad + 1][choosedYear.Year + "年"] = (int)((double)numerator / (double)(denominator));//tmax
            ////            }
            ////            catch
            ////            {
            ////            }
            ////        }

            ////    }
            ////    denominator = dtReturn.Rows[nRowPopulation][choosedYear.Year + "年"];  //人口
            ////    if (denominator != DBNull.Value)
            ////    {
            ////        if (Convert.ToInt32(denominator) > 0)
            ////        {
            ////            try
            ////            {
            ////                dtReturn.Rows[nRowPopulation + 1][choosedYear.Year + "年"] = System.Math.Round((double)numerator / (double)denominator, 3);//人均用电量
            ////            }
            ////            catch
            ////            {
            ////            }
            ////        }
            ////    }
            ////    // }

            ////    //if (nRowDenizen != 0)
            ////    numerator = dtReturn.Rows[nRowDenizen][choosedYear.Year + "年"];  //居民用电量

            ////    denominator = dtReturn.Rows[nRowPopulation][choosedYear.Year + "年"];  //总人口
            ////    if (denominator != DBNull.Value && numerator != DBNull.Value)
            ////    {
            ////        if (Convert.ToInt32(denominator) > 0)
            ////        {
            ////            try
            ////            {
            ////                if (nRowDenizen != 0)
            ////                    dtReturn.Rows[nRowPopulation + 2][choosedYear.Year + "年"] = System.Math.Round((double)numerator / (double)denominator, 3);//居民生活用电量
            ////            }
            ////            catch
            ////            {
            ////            }
            ////        }
            ////    }
            ////}
            #endregion

            #region 计算增长率
            DataColumn columnBegin = null;
            DataColumn column2000 = null;
            foreach (DataColumn column in dtReturn.Columns)
            {

                if (column.ColumnName.IndexOf("年") > 0)
                {
                    if (int.Parse(column.ColumnName.ToString().Substring(0, 4)) == 2000)
                        column2000 = column;
                    if (columnBegin == null)
                    {
                        columnBegin = column;
                    }
                }
                else if (column.ColumnName.IndexOf("增长率") > 0)
                {
                    DataColumn columnEnd = dtReturn.Columns[column.Ordinal - 1];

                    foreach (DataRow row in dtReturn.Rows)
                    {
                        if (row["Title"].ToString() == "Tmax")
                        {
                            continue;
                        }

                        object numerator = row[columnEnd];
                        object denominator = row[columnBegin];
                        //if (System.Configuration.ConfigurationSettings.AppSettings["HuaiBei"].ToString() == "淮北")
                        //{
                            if (row["Title"].ToString() == "人口(万人)" || row["Title"].ToString() == "人均用电量(千瓦时/人)" || row["Title"].ToString() == "人均生活用电量(千瓦时/人)")
                            {
                                if (int.Parse(columnEnd.ColumnName.ToString().Substring(0, 4)) > 2000)
                                {
                                    object deno = row[columnBegin];
                                    //deno = row[columnBegin];
                                    if (int.Parse(columnBegin.ColumnName.ToString().Substring(0, 4)) < 2000)
                                    {
                                        deno = row[column2000];
                                    }
                                    try
                                    {
                                        int intervalYears = Convert.ToInt32(columnEnd.ColumnName.Replace("年", ""))
                                             - Convert.ToInt32(columnBegin.ColumnName.Replace("年", ""));
                                        row[column] = Math.Round(Math.Pow((double)numerator / (double)deno, 1.0 / intervalYears) - 1, 4);
                                    }
                                    catch
                                    {
                                    }

                                }
                            }
                            else
                            {
                                if (numerator != DBNull.Value && denominator != DBNull.Value)
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
                        //}
                        //else
                        //{
                        //    if (numerator != DBNull.Value && denominator != DBNull.Value)
                        //    {
                        //        try
                        //        {
                        //            int intervalYears = Convert.ToInt32(columnEnd.ColumnName.Replace("年", ""))
                        //                - Convert.ToInt32(columnBegin.ColumnName.Replace("年", ""));
                        //            row[column] = Math.Round(Math.Pow((double)numerator / (double)denominator, 1.0 / intervalYears) - 1, 4);
                        //        }
                        //        catch
                        //        {
                        //        }
                        //    }
                        //}
                    }
                        //本次计算增长率的列作为下次的起始列
                        columnBegin = columnEnd;
                    }


                }

            #endregion
            //dtReturn.Rows.RemoveAt(nRowMaxLoad - 1);//移除多余的TMAX
      

            ////移除不需要显示的城市和郊县
            ArrayList al = new ArrayList();
            al.Clear();
            if (cityflag != -1)
            {
                al.Add(cityflag);
            }
            if (countyflag != -1)
            {
                al.Add(countyflag);
            }

          
            foreach (int m in al)
            {
                if (m == -1)
                    al.Remove(m);
                    
            }
                  al.Sort();
            for (int i = al.Count; i > 0;i--)
                dtReturn.Rows.RemoveAt(int.Parse(al[i-1].ToString()));

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

            TreeListNode focusedNode = treeList1.FocusedNode;

            if (focusedNode == null)
            {
                return;
            }


            FormTypeTitle frm = new FormTypeTitle();
            frm.Text = "增加分类";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                PSP_Types psp_Type = new PSP_Types();
                psp_Type.Title = frm.TypeTitle;

                int flag = psp_Type.ID + 1;
                if (focusedNode != null)
                {
                    flag = (int)focusedNode.GetValue("Flag");
                }
                psp_Type.Flag = flag;
                psp_Type.Flag2 = typeFlag2;
                psp_Type.ParentID = 0;

                try
                {
                    psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_Types", psp_Type);
                    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));

                    this.Cursor = Cursors.WaitCursor;
                    treeList1.BeginUpdate();
                    LoadData();
                    treeList1.EndUpdate();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加分类出错：" + ex.Message);
                }
            }
        }

        private void treeList1_EditorKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Back || e.KeyData == Keys.Space || e.KeyData == Keys.Delete)
            {
                isdel = true;
            }
            else
                isdel = false;
            if (treeList1.FocusedNode!=null)
            if (treeList1.FocusedNode[treeList1.FocusedColumn.FieldName].ToString() == "0" && isdel)
            {

                treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, null);
                string flagid = " year=" + treeList1.FocusedColumn.FieldName.Replace("Y", "").Replace("年", "") + " and TypeID=" + treeList1.FocusedNode["ID"];
                //psp_Values.ID = typeFlag2;//用ID字段存放Flag2的值
                //psp_Values.TypeID =Convert.ToInt32(treeList1.FocusedNode["ID"]);
                IList<PSP_Values> listValues = Common.Services.BaseService.GetList<PSP_Values>("SelectPSP_ValuesByWhere", flagid);
                if (listValues.Count == 1)
                {
                    Common.Services.BaseService.Delete<PSP_Values>(listValues[0]);
                }

            }
        }
    }
}