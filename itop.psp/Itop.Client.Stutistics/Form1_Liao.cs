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

namespace Itop.Client.Stutistics
{
    public partial class Form1_Liao : FormBase
    {

        string title = "";
        string unit = "";
        bool isSelect = false;
        string type = "LiaoNingZJXQ";

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

        private string typeFlag2 = "";
        //int aindex = 0;

        public Form1_Liao()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ctrlPowerEachList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);

            this.ctrlPowerEachList1.RefreshData(type);
            this.ctrlPowerEachList1.colListName.Width = 70;
            this.ctrlPowerEachList1.colRemark.Width = 5;

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

                ctrlPowerEachList1.editright = false;
            }

            if (!DeleteRight)
            {
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem16.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            
            }
            if (!PrintRight)
            {
                barButtonItem6.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
        
        }

        void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ReLoad();

        }
        private void ReLoad()
        {
            if (this.ctrlPowerEachList1.FocusedObject == null)
                return;

            typeFlag2 = this.ctrlPowerEachList1.FocusedObject.UID;

            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();

            for (int i = 0; i < treeList1.Columns.Count; i++)
            {
                if (treeList1.Columns[i].FieldName.ToString().Substring(0, 1).ToString() == "S")
                {
                    treeList1.Columns[i].VisibleIndex = -1;
                }

            }

            InitValues();

            treeList1.ExpandAll();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }
        private void InitValues()
        {  
          
            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Flag = "1";
            psl.Type = typeFlag2;
            psl.Type2 = type;

            IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psl);


            //foreach (GridBand gc in this.ctrlPSP_PowerSubstationInfo1.bandedGridView1.Bands)
            //{
            //    try
            //    {
            //        if (gc.Columns[0].FieldName.Substring(0, 1) == "S")
            //        {
            //            gc.Visible = false;
                        foreach (PowerSubstationLine pss in li)
                        {
                            ReAddColumn(pss.ClassType,pss.Title);
                           
                        }
                   // }
            //    }
            //    catch { }
            //}
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
          
                PSP_PowerTypes_Liao psp_Type = new PSP_PowerTypes_Liao();
                psp_Type.Flag2 = typeFlag2;
                IList listTypes = new ArrayList();
                try
                {
                    listTypes = Common.Services.BaseService.GetList("SelectPSP_PowerTypes_LiaoByFlag2", psp_Type);

                }
                catch (Exception ex)
                { MsgBox.Show(ex.Message); }
                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_PowerTypes_Liao));

                treeList1.DataSource = dataTable;

                treeList1.Columns["Title"].Caption = "项目名称";
                treeList1.Columns["Title"].Width = 180;
                treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
                treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
                treeList1.Columns["Flag"].VisibleIndex = -1;
                treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["Flag2"].VisibleIndex = -1;
                treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["CreatTime"].VisibleIndex = -1;
                treeList1.Columns["CreatTime"].OptionsColumn.ShowInCustomizationForm = false;
                treeList1.Columns["CreatTime"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
              
                PowerYears psp_Year = new PowerYears();
                psp_Year.Flag = typeFlag2;
                IList<PowerYears> listYears = Common.Services.BaseService.GetList<PowerYears>("SelectPowerYearsListByFlag", psp_Year);

                foreach (PowerYears item in listYears)
                {
                    AddColumn(item.Year);
                }

                AddColumn1();
                AddColumn2();
                AddColumn3();
                AddColumn4();

                ////PowerProYears psp_Years = new PowerProYears();
                ////psp_Years.Flag = typeFlag2;
                ////IList<PowerProYears> listYear = Common.Services.BaseService.GetList<PowerProYears>("SelectPowerProYearsListByFlag", psp_Years);

                ////foreach (PowerProYears item in listYear)
                ////{
                ////    ReAddColumn(item.Year);
                ////}

                Application.DoEvents();
             
                LoadValues();

               
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message);
            
            }
        }

        //读取数据
        private void LoadValues()
        {
            //PowerValues PowerValues = new PowerValues();
            //PowerValues.ID = typeFlag2;//用ID字段存放Flag2的值


            IList<PowerValues> listValues = Common.Services.BaseService.GetList<PowerValues>("SelectPowerValuesListFromTypes_LiaoByFlag2", typeFlag2);
            foreach(PowerValues value in listValues)
            {
                TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                if (node != null)
                {
                    try
                    {
                        node.SetValue(value.Year + "年", value.Value);
                    }
                    catch
                    { }

                }
            }


            //IList<PowerProValues> listValue = Common.Services.BaseService.GetList<PowerProValues>("SelectPowerProValuesListByFlag2", typeFlag2);
            //foreach (PowerProValues value in listValue)
            //{
            //    TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
            //    if (node != null)
            //    {
            //        node.SetValue(value.Year, value.Value);
            //    }
            //}
            
            //foreach(TreeListNode tln in treeList1.Nodes)
            //{
            //    CalChildNode(tln);
            //}
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
            try
            {
                //ArrayList al = new ArrayList();
                //al.Clear();
                int nInsertIndex = GetInsertIndex(year);

                //for (int i = 0; i < treeList1.Columns.Count; i++)
                //{
                //    if (treeList1.Columns[i].VisibleIndex != -1)
                //        al.Add(treeList1.Columns[i].Caption);
                //}

                //if (al.Contains(year + "年") == true)
                //{
                //    MsgBox.Show("已经存在此年份，要想添加，请先删除已存在的" + year + "年!");
                //    return;
                //}
                dataTable.Columns.Add(year + "年", typeof(double));


                //TreeListColumn column = treeList1.Columns.Add();
                TreeListColumn column = treeList1.Columns.Insert(nInsertIndex);
                column.FieldName = year + "年";
                column.Tag = year;
                column.Caption = year + "年";
                column.Width = 70;
                column.OptionsColumn.AllowSort = false;
                column.VisibleIndex = nInsertIndex - 2;//有两列隐藏列
                //aindex = nInsertIndex - 2;
                column.ColumnEdit = repositoryItemTextEdit1;

                foreach (TreeListColumn col in treeList1.Columns)
                {
                    if (col.Caption.IndexOf("年") > 0)
                    {
                        col.VisibleIndex = Convert.ToInt32(col.Tag.ToString().Substring(0,4));
                    }
                }
           
            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
            treeList1.RefreshDataSource();
        }

        private void AddColumn1()
        {

            TreeListColumn column = treeList1.Columns["JianSheXingZhi"];
            //column.FieldName = "Jingtai";
            column.Caption = "建设性质";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
          
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;
        }

        private void AddColumn2()//建设期间贷款利息
        {
            //dataTable.Columns.Add("Lixi", typeof(double));
            TreeListColumn column = treeList1.Columns["RongLiang"];
            //column.FieldName = "Lixi";
            column.Caption = "变电容量";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
           
            column.ColumnEdit = repositoryItemTextEdit1;
        }

        private void AddColumn3()//价格预备费

        {
            //dataTable.Columns.Add("Yubei", typeof(double));
            TreeListColumn column = treeList1.Columns["ChangDu"];
          //  column.FieldName = "价格预备";
            column.Caption = "线路长度";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
          
            column.ColumnEdit = repositoryItemTextEdit1;
        }

        private void AddColumn4()//动态投资

        {
            //dataTable.Columns.Add("Dongtai", typeof(double));
            TreeListColumn column = treeList1.Columns["TouZiZongEr"];
            //column.FieldName = "Dongtai";
            column.Caption = "总投资（万元）";
            column.Width = 110;
            column.OptionsColumn.AllowSort = false;
      
            column.ColumnEdit = repositoryItemTextEdit1;
        }





        private int GetInsertIndex(int year)
        {
            int nFixedColumns = typeof(PSP_PowerTypes_Liao).GetProperties().Length - 2;//ID和ParentID列不在treeList1.Columns中
 
            //int nColumns = treeList1.Columns.Count;
            //int nIndex = nFixedColumns;

            ////还没有年份

            //if (nColumns == nFixedColumns)
            //{
            //}
            //else//已经有年份

            //{
            //    bool bFind = false;

            //    //查找此年的位置

            //    for (int i = nFixedColumns + 1; i < nColumns; i++)
            //    {
            //        //Tag存放年份
            //        int tagYear1 = (int)treeList1.Columns[i - 1].Tag;
            //        int tagYear2 = (int)treeList1.Columns[i].Tag;
            //        if (tagYear1 < year && tagYear2 > year)
            //        {
            //            nIndex = i;
            //            bFind = true;
            //            break;
            //        }
            //    }

            //    //不在最大和最小之间

            //    if (!bFind)
            //    {
            //        int tagYear1 = (int)treeList1.Columns[nFixedColumns].Tag;

            //        if (tagYear1 > year)//比第一个年份小
            //        {
            //            nIndex = nFixedColumns;
            //        }
            //        else
            //        {
            //            nIndex = nColumns;
            //        }
            //    }
            //}

            return nFixedColumns;
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
                //&& e.Column.FieldName.IndexOf("年") > 0
                //&& e.Column.Tag != null
            )
            {

                if (e.Column.FieldName != "Lixi" && e.Column.FieldName != "Yubei" && e.Column.FieldName != "Dongtai" && e.Column.FieldName != "Dongtai")
                {
                    SaveCellValue((int)treeList1.FocusedColumn.Tag, (int)treeList1.FocusedNode.GetValue("ID"), Convert.ToDouble(e.Value));
                }
                else
                {
                    int id = (int)e.Node["ID"];

                  //  SaveCellValue(e.Column.FieldName,id, Convert.ToDouble(e.Value));
                }
                //
                CalculateSum(e.Node, e.Column);
                //}
                //else
                //{
                //    treeList1.FocusedNode.SetValue(treeList1.FocusedColumn.FieldName, lastEditValue);
                //}
            }

          //  CalculateNodeSum(e.Node);
        }

        private bool SaveCellValue(int year, int typeID, double value)
        {
            PowerValues PowerValues = new PowerValues();
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



        //private bool SaveCellValue(string fname,int id,double value)
        //{
        //    //PowerValues PowerValues = new PowerValues();
        //    //PowerValues.TypeID = typeID;
        //    //PowerValues.Value = value;
        //    //PowerValues.Year = year;
        //    PSP_PowerTypes_Liao pt = Services.BaseService.GetOneByKey<PSP_PowerTypes_Liao>(id);

        //    switch (fname)
        //    {
        //        case "Lixi":
        //            pt. = Convert.ToDouble(value);
        //            break;

        //        case "Yubei":
        //            pt.Yubei = Convert.ToDouble(value);
        //            break;

        //        case "Dongtai":
        //            pt.Dongtai = Convert.ToDouble(value);
        //            break;
        //    }


        //    try
        //    {
        //        Common.Services.BaseService.Update<PSP_PowerTypes_Liao>(pt);
        //    }
        //    catch (Exception ex)
        //    {
        //        MsgBox.Show("保存数据出错：" + ex.Message);
        //        return false;
        //    }
        //    return true;
        //}

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
            e.Cancel = true;


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

            if (this.ctrlPowerEachList1.FocusedObject == null)
            {
                MsgBox.Show("没有项目存在，请先添加项目分类管理！");
                return; 
            }
            ////PSP_PowerTypes_Liao ppt = new PSP_PowerTypes_Liao();
            ////ppt.Flag2 = this.ctrlPowerEachList1.FocusedObject.UID;
            ////IList<PSP_PowerTypes_Liao> ps = Common.Services.BaseService.GetList<PSP_PowerTypes_Liao>("SelectPSP_PowerTypes_LiaoByFlag2", ppt);

            ////if (ps.Count == 0)
            ////{
            ////    MsgBox.Show("没有记录存在，无法统计！");
            ////    return;
            ////}
            Form1Print_Liao frm = new Form1Print_Liao();
            
            frm.IsSelect = isSelect;
            frm.Text = this.ctrlPowerEachList1.FocusedObject.ListName;
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
            listColID.Add("Flag");
            dt.Columns.Add("Flag", typeof(int));

            listColID.Add("ParentID");
            dt.Columns.Add("ParentID", typeof(int));

            listColID.Add("Title");
            dt.Columns.Add("Title", typeof(string));
            dt.Columns["Title"].Caption = "项目名称";

            listColID.Add("JianSheXingZhi");
            dt.Columns.Add("JianSheXingZhi", typeof(string));
            dt.Columns["JianSheXingZhi"].Caption = "建设性质";

            listColID.Add("RongLiang");
            dt.Columns.Add("RongLiang", typeof(string));
            dt.Columns["RongLiang"].Caption = "容量";

            listColID.Add("ChangDu");
            dt.Columns.Add("ChangDu", typeof(string));
            dt.Columns["ChangDu"].Caption = "线路长度";

            listColID.Add("TouZiZongEr");
            dt.Columns.Add("TouZiZongEr", typeof(string));
            dt.Columns["TouZiZongEr"].Caption = "投资总额（万元）";
            int i = 0;
            foreach (TreeListColumn column in xTreeList.Columns)
            {
                if (column.FieldName.IndexOf("年") > 0)
                {
                    listColID.Add(column.FieldName);
                    dt.Columns.Add(column.FieldName, typeof(string));
                }
                if (column.FieldName.Substring(0, 1) == "S" && column.VisibleIndex!=-1)
                {
                    listColID.Add(column.FieldName);
                    dt.Columns.Add(column.FieldName, typeof(string));
                   // dt.Columns[column.FieldName].Caption = column.Caption+ "@" +column.VisibleIndex.ToString();
                    dt.Columns[column.FieldName].Caption = column.Caption;
                }
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
            this.ctrlPowerEachList1.AddObjecta(type,false);

          //  this.ctrlPowerEachList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);

            this.ctrlPowerEachList1.RefreshData(type);
            this.ctrlPowerEachList1.colListName.Width = 70;
            this.ctrlPowerEachList1.colRemark.Width = 5;

            Show();
            Application.DoEvents();

            InitRight();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachList1.UpdateObject();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachList1.DeleteObject("xuqiu");
            if (this.ctrlPowerEachList1.bl)
                ReLoad();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //typeFlag2 = this.ctrlPowerEachList1.FocusedObject.UID;
            if (this.ctrlPowerEachList1.FocusedObject == null)
            {
                MsgBox.Show("没有指定的项目存在，请先添加项目分类管理！");
                return;
            }
            FrmEditProject5 frm = new FrmEditProject5();
           
            frm.FlagId = typeFlag2;
            frm.Type = type;
            try
            {
                if (treeList1.FocusedNode["ID"].ToString() != null && treeList1.FocusedNode["ID"].ToString() == "")
                    frm.PowerUId = treeList1.FocusedNode["ID"].ToString();
            }
            catch { }
            //treeList1.FocusedNode.GetValue("ID");
           
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //PSP_PowerTypes_Liao psp_Type = new PSP_PowerTypes_Liao();
                //psp_Type.Title = frm.TypeTitle;
                //psp_Type.Flag = 0;
                //psp_Type.Flag2 = typeFlag2;
                //psp_Type.ParentID = 0;

                //try
                //{
                //    psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_PowerTypes_Liao", psp_Type);
                //    dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                //}
                //catch (Exception ex)
                //{
                //    MsgBox.Show("增加项目出错：" + ex.Message);
                //}
                
            }
            ReLoad();
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
                PSP_PowerTypes_Liao psp_Type = new PSP_PowerTypes_Liao();
                psp_Type.Title = frm.TypeTitle;
                psp_Type.Flag = (int)focusedNode.GetValue("Flag");
                psp_Type.Flag2 = (string)focusedNode.GetValue("Flag2");
                psp_Type.ParentID = (int)focusedNode.GetValue("ID");

                try
                {
                    psp_Type.ID = (int)Common.Services.BaseService.Create("InsertPSP_PowerTypes_Liao", psp_Type);
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

            //if (treeList1.FocusedNode.ParentNode == null)
            //{
            //    MsgBox.Show("一级分类名称不能修改！");
            //    return;
            //}
            FrmEditProject5 frm = new FrmEditProject5();
           
            frm.FlagId = typeFlag2;
            frm.Type = type;
            frm.PowerUId = treeList1.FocusedNode["ID"].ToString();
            frm.Isupdate = true;
            ////FormTypeTitle frm = new FormTypeTitle();
            ////frm.TypeTitle = treeList1.FocusedNode.GetValue("Title").ToString();
            ////frm.Text = "修改项目名";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                ////PSP_PowerTypes_Liao psp_Type = new PSP_PowerTypes_Liao();
                ////Class1.TreeNodeToDataObject<PSP_PowerTypes_Liao>(psp_Type, treeList1.FocusedNode);
                ////psp_Type.Title = frm.TypeTitle;

                ////try
                ////{
                ////    Common.Services.BaseService.Update<PSP_PowerTypes_Liao>(psp_Type);
                ////    treeList1.FocusedNode.SetValue("Title", frm.TypeTitle);
                ////}
                ////catch (Exception ex)
                ////{
                ////    //MsgBox.Show("修改出错：" + ex.Message);
                ////}
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
                PowerValues PowerValues = new PowerValues();
                PowerValues.TypeID = int.Parse(treeList1.FocusedNode["ID"].ToString());

                try
                {
                    //DeletePowerValuesByType里面删除数据和分类

                    Common.Services.BaseService.Update("DeletePowerValuesANDPSP_PowerTypes_LiaoByType", PowerValues);
                   
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
            if (this.ctrlPowerEachList1.FocusedObject == null)
            {
                MsgBox.Show("请先添加项目分类管理");
                return;
            }

            try
            {
                FormNewYear frm = new FormNewYear();
                frm.Flag2 = typeFlag2;
                int nFixedColumns = typeof(PSP_PowerTypes_Liao).GetProperties().Length;
                int nColumns = treeList1.Columns.Count;
                //if (nFixedColumns == nColumns + 2)//相等时，表示还没有年份，新年份默认为当前年减去15年

                //{
                //    frm.YearValue = DateTime.Now.Year - 15;
                //}
                //else
                //{
                    //有年份时，默认为最大年份加1年
                PowerYears ps = new PowerYears();
                ps.Flag = typeFlag2;

                PowerYears pg = (PowerYears)Common.Services.BaseService.GetObject("SelectPowerYearsByFlag", ps);
                if (pg != null)
                {
                    frm.YearValue = pg.Year + 1;
                }
                else
                {
                    frm.YearValue = DateTime.Now.Year;
                }
             

               // }

                ArrayList al = new ArrayList();
                al.Clear();

                for (int i = 0; i < treeList1.Columns.Count; i++)
                {
                    if (treeList1.Columns[i].VisibleIndex != -1)
                        al.Add(treeList1.Columns[i].Caption);
                }

                frm.Al = al;
              
               
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    AddColumn(frm.YearValue);
                }

                ReLoad();

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
                MsgBox.Show("不能删除此列！");
                return;
            }

            if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedColumn.FieldName + " 及该年的所有数据？") != DialogResult.Yes)
            {
                return;
            }


            //PowerValues PowerValues = new PowerValues();
            //PowerValues.ID = typeFlag2;//借用ID属性存放Flag2
            //PowerValues.Year = (int)treeList1.FocusedColumn.Tag;


            Hashtable hs = new Hashtable();
            hs.Add("ID", treeList1.FocusedNode["ID"].ToString());
            hs.Add("Year", (int)treeList1.FocusedColumn.Tag);
            hs.Add("Flag",typeFlag2);

            try
            {
                //DeletePowerValuesByYear删除数据和年份

                int colIndex = treeList1.FocusedColumn.AbsoluteIndex;
                Common.Services.BaseService.Update("DeletePowerValuesByValue", hs);
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
            //ctrlPowerProject1.InitGrid();
            //gcontrol = ctrlPowerProject1.gridControl1;
            


            //this.DialogResult = DialogResult.OK;
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
                if (this.ctrlPowerEachList1.FocusedObject == null)
                {
                    MsgBox.Show("请先添加项目分类管理！");
                    return;
                }

            InsertLineData1();
        }

        private void InsertLineData1()
        {
            string columnname = "";

            IList<PSP_PowerTypes_Liao> psplist = new List<PSP_PowerTypes_Liao>();
            psplist.Clear();
          
            IList<PowerValues> pvlist=new List<PowerValues>();
            pvlist.Clear();

            PowerYears z1 = new PowerYears();
            z1.Flag=this.ctrlPowerEachList1.FocusedObject.UID;

               ArrayList al = new ArrayList();
               int pycount = 0;
               IList<PowerYears> py = Common.Services.BaseService.GetList<PowerYears>("SelectPowerYearsListByFlag", z1);
               pycount = py.Count;
               for (int i = 0; i < 20; i++)
               {
                   al.Add("");
               }
               int j = 0;
                   try
                   {
                       DataTable dts = new DataTable();
                       OpenFileDialog op = new OpenFileDialog();
                       op.Filter = "Excel文件(*.xls)|*.xls";
                       if (op.ShowDialog() == DialogResult.OK)
                       {
                           dts = GetExcel(op.FileName);
                           //if (treeList1.Columns.Count != dts.Columns.Count) 
                           //  {
                           //      MsgBox.Show("导入的格式不正确！");
                           //      return;
                           //  }
                           DateTime dt = DateTime.Now;
                           for (int i =0; i < dts.Rows.Count; i++)
                           {

                               PSP_PowerTypes_Liao m2 = new PSP_PowerTypes_Liao();
                               m2.Flag2 = this.ctrlPowerEachList1.FocusedObject.UID;
                               //if (dts.Rows[i][1].ToString().IndexOf("合计") >= 0)
                               //    continue;
                               m2.CreatTime = dt.AddSeconds(i);

                               foreach (DataColumn dc in dts.Columns)
                               {
                                   columnname = dc.ColumnName;
                                   if (dts.Rows[i][dc.ColumnName].ToString() == "")
                                       continue;
                                   try
                                   {
                                       if (dc.ColumnName.ToString().Substring(4,1)=="年" &&dc.ColumnName.ToString().Length == 5)
                                           continue;
                                   }
                                   catch { }

                                   string LL2 = "";
                                   try
                                   {
                                       LL2 = dts.Rows[i][dc.ColumnName].ToString();
                                   }
                                   catch { }
                                   m2.GetType().GetProperty(dc.ColumnName).SetValue(m2, LL2, null);

                               }
                               //try
                               //{
                               //    m2.Title = dts.Rows[i][0].ToString();
                               //}
                               //catch { }

                               //try
                               //{
                               //    m2.JianSheXingZhi =dts.Rows[i][1].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.RongLiang =dts.Rows[i][2].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.ChangDu = dts.Rows[i][3].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.TouZiZongEr =dts.Rows[i][4].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S1 = dts.Rows[i][pycount+5].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S2 = dts.Rows[i][pycount + 6].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S3 = dts.Rows[i][pycount + 7].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S4 = dts.Rows[i][pycount + 8].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S5 = dts.Rows[i][pycount + 9].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S6 = dts.Rows[i][pycount +10].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S7 = dts.Rows[i][pycount + 11].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S8 = dts.Rows[i][pycount + 12].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S9 = dts.Rows[i][pycount + 13].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S10 = dts.Rows[i][pycount + 14].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S11 = dts.Rows[i][pycount + 15].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S12 = dts.Rows[i][pycount + 16].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S13 = dts.Rows[i][pycount + 17].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S14 = dts.Rows[i][pycount + 18].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S15 = dts.Rows[i][pycount + 19].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S16 = dts.Rows[i][pycount + 10].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S17 = dts.Rows[i][pycount + 21].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S18 = dts.Rows[i][pycount + 22].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S19 = dts.Rows[i][pycount + 23].ToString();
                               //}
                               //catch { }
                               //try
                               //{
                               //    m2.S20 = dts.Rows[i][pycount + 24].ToString();
                               //}
                               //catch { }
                 
                               psplist.Add(m2);
                               
                           }
                           for (int i = 0; i < dts.Rows.Count; i++)
                           {
                               //for (int k = 1; k <= pycount; k++)
                               //{
                               //    PowerValues m1 = new PowerValues();

                               //    m1.Value = double.Parse(dts.Rows[i][4 + k].ToString());
                               //    //m1.TypeID = m2.ID;
                               //    m1.Year = int.Parse(dts.Columns[4 + k].ColumnName.Substring(0, 4));
                               //    pvlist.Add(m1);
                               //}


                               foreach (DataColumn dc in dts.Columns)
                               {
                                   columnname = dc.ColumnName;
                                   PowerValues m1 = new PowerValues();
                                   try
                                   {
                                       if (dc.ColumnName.ToString().Substring(4,1)=="年" &&dc.ColumnName.ToString().Length == 5)
                                       {
                                           if (dts.Rows[i][dc.ColumnName].ToString() == null || dts.Rows[i][dc.ColumnName].ToString() == "")
                                           {
                                               m1.Year = int.Parse(dc.ColumnName.ToString().Substring(0, 4));

                                           }
                                           else
                                           {
                                               m1.Value = double.Parse(dts.Rows[i][dc.ColumnName].ToString());
                                               m1.Year = int.Parse(dc.ColumnName.ToString().Substring(0, 4));
                                           }
                                           pvlist.Add(m1); 
                                       }
                                       else
                                       {
                                           continue;
                                         
                                       }
                                   }
                                   catch { }
                                  
                               }
                               
                           }
                           int index = 0;
                           foreach (PSP_PowerTypes_Liao ptl in psplist)
                           {  
                               int z_typeID = (int)Common.Services.BaseService.Create("InsertPSP_PowerTypes_Liao", ptl);
                               for (int L = 1; L <= pycount; L++)
                               {
                                   pvlist[index].TypeID = z_typeID;
                                   PowerValues pv = (PowerValues)pvlist[index];
                                   index++;
                                  Common.Services.BaseService.Create("InsertPowerValues", pv);
                               }

                           }

                         
                       }
                       ReLoad();
                       
                   }
                   catch { MsgBox.Show("导入格式不正确！"); }
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
            //for (int k = 1; k < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            //{
            // //dt.Columns.Add("col" + k.ToString());
               


            //}

            for (int k = 0; k <=fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) ; k++)
            {
                foreach (TreeListColumn tlc in this.treeList1.Columns)
                {
                    if (tlc.Caption == fpSpread1.Sheets[0].Cells[3, k].Text && (tlc.FieldName == "RongLiang" || tlc.FieldName == "ChangDu"))
                    {
                        dt.Columns.Add(tlc.FieldName);
                    }
                    else if (tlc.Caption == fpSpread1.Sheets[0].Cells[3, k].Text && tlc.FieldName.IndexOf("年") >= 0)
                    {
                        dt.Columns.Add(tlc.FieldName);
                    }
                    else if (tlc.Caption == fpSpread1.Sheets[0].Cells[2, k].Text)
                    {
                        dt.Columns.Add(tlc.FieldName);
                    }
                
                
                }

                ////TreeListColumn gc = this.treeList1.Columns[k];
                ////if (gc.VisibleIndex != -1)
                ////    dt.Columns.Add(gc.FieldName);
            }
            for (int i =4; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
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
            if (this.ctrlPowerEachList1.FocusedObject == null)
                return;

            typeFlag2 = this.ctrlPowerEachList1.FocusedObject.UID;

            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }
        private void barButtonItem5_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (this.ctrlPowerEachList1.FocusedObject == null)
                return;
            FormClass fc = new FormClass();
            gridControl1.DataSource = fc.ConvertTreeListToDataTable(treeList1);
            gridView1.Columns["Title"].Caption = "项目名称";
            gridView1.Columns["Title"].VisibleIndex = 0;
            gridView1.Columns["JianSheXingZhi"].Caption = "建设性质";
            gridView1.Columns["RongLiang"].Caption = "变电容量";
            gridView1.Columns["ChangDu"].Caption = "线路长度";
            gridView1.Columns["TouZiZongEr"].Caption = "总投资（万元）";
            gridView1.Columns["CreatTime"].Visible = false;
            gridView1.Columns["Flag"].Visible = false;
            gridView1.Columns["Flag2"].Visible = false;
            title = this.ctrlPowerEachList1.FocusedObject.ListName;
            FileClass.ExportExcel(gridControl1);

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctrlPowerEachList1.FocusedObject == null)
            {
                MsgBox.Show("请先添加项目分类管理！");
                return;
            }
            try
            {
               
                ArrayList al = new ArrayList();
                al.Clear();
                for (int i = 0; i < treeList1.Columns.Count; i++)
                {
                    if (treeList1.Columns[i].VisibleIndex != -1)
                        al.Add(treeList1.Columns[i].Caption);
                }

               
                FrmPower_AddBands frm = new FrmPower_AddBands();

                //psl.Flag = "1";
                //psl.Type = typeFlag2;
                //psl.Type2 = type;

                frm.Type = typeFlag2;
                frm.Flag = "1";
                frm.Type2 = type;
                frm.AddFlag = "form1_liaoflag";
                frm.Al = al;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    ReAddColumn(frm.ClassType,frm.textBox1.Text);
                }

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedColumn == null)
            {
                return;
            }

            string fieldName = treeList1.FocusedColumn.FieldName;

            if (fieldName.Substring(0, 1) != "S")
            {
                MessageBox.Show("此列，无权修改！");
                return;
            }
            try
            {
                FrmPower_AddBands frm = new FrmPower_AddBands();
                frm.ClassName = treeList1.FocusedColumn.Caption;
                frm.ClassType = treeList1.FocusedColumn.FieldName;
                frm.Type = typeFlag2;
                frm.Flag ="1";
                frm.Type2 = type;
                frm.IsUpdate = true;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // gc.Caption = frm.ClassName;
                    EditColumn(frm.ClassType, frm.textBox1.Text, treeList1.FocusedColumn.VisibleIndex);
                }
            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }

        }
        private void EditColumn(string filename, string year,int visibleindex)
        {
            try
            {
                TreeListColumn column = treeList1.Columns[filename];

                dataTable.Columns.Add(year, typeof(string));

                //   TreeListColumn column = treeList1.Columns.Insert(100);


                column.OptionsColumn.AllowEdit = false;


                // column.FieldName = filename;
                column.Tag = year;
                column.Caption = year;
                column.Width = 70;
                column.OptionsColumn.AllowSort = false;
                column.VisibleIndex = visibleindex;//有两列隐藏列

                column.ColumnEdit = this.repositoryItemTextEdit1;
                //treeList1.Columns["StartYear"].VisibleIndex = 4998;
                //treeList1.Columns["EndYear"].VisibleIndex = 4999;
                // treeList1.Columns["Remark"].VisibleIndex = 5000;

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
            treeList1.RefreshDataSource();
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedColumn == null)
            {
                return;
            }

            string fieldName = treeList1.FocusedColumn.FieldName;
            string caption = treeList1.FocusedColumn.Caption;
            if (fieldName.Substring(0, 1) != "S")
            {
                MsgBox.Show("不能删除此列！");
                return;
            }

            if (MsgBox.ShowYesNo("是否删除 " + treeList1.FocusedColumn.Caption + " 的所有数据？") != DialogResult.Yes)
            {
                return;
            }
           

            try
            {
                int colIndex = treeList1.FocusedColumn.AbsoluteIndex;
               
                Hashtable ha = new Hashtable();
                ha.Add("Title", fieldName + "=''");
                ha.Add("Flag2", typeFlag2);
                Itop.Client.Common.Services.BaseService.Update("UpdatePSP_PowerTypes_LiaoByFlag2", ha);

                PowerSubstationLine psl = new PowerSubstationLine();
           
                psl.Flag = "1";
                psl.ClassType = fieldName;
             
                psl.Type = typeFlag2;
                psl.Title = caption;
                psl.Type2 = type;
                Itop.Client.Common.Services.BaseService.Update("DeletePowerSubstationLineByAll", psl);

               // Common.Services.BaseService.Update("DeletePowerValuesByYear", hs);
               // dataTable.Columns.Remove(treeList1.FocusedColumn.Caption);
               // treeList1.Columns.Remove(treeList1.FocusedColumn);
                treeList1.FocusedColumn.VisibleIndex = -1;
                if (colIndex >= treeList1.VisibleColumnCount)
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

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmEditProject3 fep = new FrmEditProject3();
            fep.FlagId = typeFlag2;
            fep.PowerUId = treeList1.FocusedNode["ID"].ToString();
           string  uid1= treeList1.FocusedNode["ID"].ToString();
            fep.Type = type;

           
            if (fep.ShowDialog() == DialogResult.OK)
            {
               ReLoad();
                //LoadData();
                treeList1.ExpandAll();
                FoucsLocation(uid1, treeList1.Nodes);
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

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
         //  FrmEditProject3 fep = new FrmEditProject3();
            if (!EditRight)
                return;
            if (treeList1.FocusedNode == null)
            {
                return;
            }
            FrmEditProject5 fep = new FrmEditProject5();
            string uid1="不存在 ";
            try
            {
                fep.FlagId = typeFlag2;
                fep.PowerUId = treeList1.FocusedNode["ID"].ToString();
                uid1 = treeList1.FocusedNode["ID"].ToString();
                fep.Type = type;
                fep.Isupdate = true;
            }
            catch { }
            if (uid1 == "不存在 ")
            {
                MsgBox.Show("没有记录存在，无法修改！");
                return;
            }


            if (fep.ShowDialog() == DialogResult.OK)
            {
                ReLoad();
                //LoadData();
                treeList1.ExpandAll();
                FoucsLocation(uid1, treeList1.Nodes);
            }
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ctrlPowerEachList1.FocusedObject == null)
            {
                MsgBox.Show("没有要删除的数据！");
                return;
            }
            if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
                return;

            string Flag2 = ctrlPowerEachList1.FocusedObject.UID;
            Services.BaseService.Update("DeletePSP_PowerTypes_LiaoByFlag2", Flag2);
            MsgBox.Show("清除成功！");
            // InitSodata2();
            ReLoad();


        }

        private void ctrlPowerEachList1_Load(object sender, EventArgs e)
        {

        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

        }
     
    }
}