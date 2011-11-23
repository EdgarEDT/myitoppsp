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
using DevExpress.XtraGrid.Columns;

namespace Itop.Client.Stutistics
{
    public partial class Form9_Liao : FormBase
    {

        string title = "";
        string unit = "兆伏安、公里";
        bool isSelect = false;
        string type = "LiaoNingJSXM";

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

        private string typeFlag2 = "";

        public Form9_Liao()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ctrlPowerEachList11.IsJSXM = true;
            this.ctrlPowerEachList11.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            this.ctrlPowerEachList11.RefreshData(type);

            Show();
            Application.DoEvents();

            InitRight();

            this.ctrlPowerEachList11.colListName.Caption = "名称";
            this.ctrlPowerEachList11.colListName.Width = 60;
            this.ctrlPowerEachList11.colRemark.Width=5;



            //InitPicData();

        }


        private void InitPicData()
        {
        SVGFILE sf = Services.BaseService.GetOneByKey<SVGFILE>("26474eb6-cd92-4e84-a579-2f33946acf1a");
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(sf.SVGDATA);

            
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C", typeof(bool));


            XmlNodeList nli = xd.GetElementsByTagName("layer");


            foreach (XmlNode node in nli)
            {

                DataRow row =dt.NewRow();

                XmlElement xe=(XmlElement)node;

                row["A"] =xe.GetAttribute("id");
                row["B"] = xe.GetAttribute("label");
                row["C"] = false;
                dt.Rows.Add(row);
            }
        
        
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
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem16.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!DeleteRight)
            {
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            
            }
             if (!PrintRight)
            {
                barButtonItem6.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
              
            
            }
         
        
        }

        void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.ctrlPowerEachList11.FocusedObject == null)
                return;

            typeFlag2 = this.ctrlPowerEachList11.FocusedObject.UID;

            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            InitSodata();
            //LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
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
                DataTable ddt = new DataTable();
                //ddt.Columns.Clear();
                //ddt = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PowerProTypes));
                //ddt.DefaultView.Sort = "CreateTime asc";
                //DataView dv = new DataView(ddt);
                //dv.Sort = "CreateTime desc";
                //DataSet ds = new DataSet();
                //ds.Tables.Add(ddt);

                //ds.Tables[0].DefaultView.Sort = " CreateTime asc";

                //dataTable = ddt.DefaultView.Table;
          

               dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PowerProTypes));
               treeList1.DataSource = dataTable;

               treeList1.Columns["Title"].Caption = "区域名称";
               treeList1.Columns["Title"].Width = 180;
               treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
               treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
               treeList1.Columns["Flag"].VisibleIndex = -1;
               // treeList1.Columns["Flag"].SortOrder = System.Windows.Forms.SortOrder.Ascending;
               treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
               treeList1.Columns["Flag2"].VisibleIndex = -1;
               treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;

               treeList1.Columns["CreateTime"].VisibleIndex = -1;
               treeList1.Columns["CreateTime"].OptionsColumn.ShowInCustomizationForm = false;
               treeList1.Columns["CreateTime"].SortOrder = System.Windows.Forms.SortOrder.Ascending;


               treeList1.Columns["UpdateTime"].VisibleIndex = -1;
               treeList1.Columns["UpdateTime"].OptionsColumn.ShowInCustomizationForm = false;

               treeList1.Columns["UpdateTime"].VisibleIndex = -1;
               treeList1.Columns["UpdateTime"].OptionsColumn.ShowInCustomizationForm = false;

               //treeList1.Columns["IsConn"].VisibleIndex = -1;
               //treeList1.Columns["IsConn"].OptionsColumn.ShowInCustomizationForm = false;

               treeList1.Columns["ID"].VisibleIndex = -1;
               treeList1.Columns["ID"].OptionsColumn.ShowInCustomizationForm = false;
               treeList1.Columns["ParentID"].VisibleIndex = -1;
               treeList1.Columns["ParentID"].OptionsColumn.ShowInCustomizationForm = false;

               treeList1.Columns["StartYear"].VisibleIndex = -1;
               treeList1.Columns["StartYear"].OptionsColumn.ShowInCustomizationForm = false;

               treeList1.Columns["EndYear"].VisibleIndex = -1;
               treeList1.Columns["EndYear"].OptionsColumn.ShowInCustomizationForm = false;

               //treeList1.Columns["Remark"].VisibleIndex = -1;
               //treeList1.Columns["Remark"].OptionsColumn.ShowInCustomizationForm = false;

               treeList1.Columns["L1"].VisibleIndex = -1;
               treeList1.Columns["L1"].OptionsColumn.ShowInCustomizationForm = false;
               treeList1.Columns["L2"].VisibleIndex = -1;
               treeList1.Columns["L2"].OptionsColumn.ShowInCustomizationForm = false;
               treeList1.Columns["L3"].VisibleIndex = -1;
               treeList1.Columns["L3"].OptionsColumn.ShowInCustomizationForm = false;

                AddColumn2();


                PowerProYears psp_Year = new PowerProYears();
                psp_Year.Flag = typeFlag2;
                IList<PowerProYears> listYears = Common.Services.BaseService.GetList<PowerProYears>("SelectPowerProYearsListByFlag", psp_Year);

                foreach (PowerProYears item in listYears)
                {
                    AddColumn(item.Year);
                }

              //  AddColumn1();


                Application.DoEvents();

                LoadValues();

                treeList1.ExpandAll();
            }
            catch(Exception ex)
            {
                MsgBox.Show(ex.Message);
            
            }
        }

        private void UpLoadData()
        {
            try
            {
                if (dataTable != null)
                {
                    dataTable.Columns.Clear();
                    treeList1.Nodes.Clear();
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
                DataTable ddt = new DataTable();
                //ddt.Columns.Clear();
                //ddt = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PowerProTypes));
                //ddt.DefaultView.Sort = "CreateTime asc";
                //DataView dv = new DataView(ddt);
                //dv.Sort = "CreateTime desc";
                //DataSet ds = new DataSet();
                //ds.Tables.Add(ddt);

                //ds.Tables[0].DefaultView.Sort = " CreateTime asc";

                //dataTable = ddt.DefaultView.Table;


                dataTable = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PowerProTypes));
                treeList1.DataSource = dataTable;

                //PowerProYears psp_Year = new PowerProYears();
                //psp_Year.Flag = typeFlag2;
                //IList<PowerProYears> listYears = Common.Services.BaseService.GetList<PowerProYears>("SelectPowerProYearsListByFlag", psp_Year);

                //foreach (PowerProYears item in listYears)
                //{
                //    AddColumn(item.Year);
                //}
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

            IList<PowerProValues> listValues = Common.Services.BaseService.GetList<PowerProValues>("SelectPowerProValuesListByFlag2", typeFlag2);


 
                foreach (PowerProValues value in listValues)
                {
                    TreeListNode node = treeList1.FindNodeByFieldValue("ID", value.TypeID);
                    if (node != null)
                    { try
                       {
                        node.SetValue(value.Year, value.Value);
                    }
                        catch 
                         {
            
                        }
                    }
                }
            
            

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
                //treeList1.Columns["StartYear"].VisibleIndex = 4998;
                //treeList1.Columns["EndYear"].VisibleIndex = 4999;
               // treeList1.Columns["Remark"].VisibleIndex = 5000;

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
            treeList1.RefreshDataSource();
        }

        private void AddColumn1()//静态总投资

        {
            ////TreeListColumn column = treeList1.Columns["StartYear"];
            ////column.Caption = "计划开工时间";
            ////column.Width = 70;
            ////column.OptionsColumn.AllowSort = false;
            ////column.VisibleIndex = 760;
            //////column.ColumnEdit = repositoryItemTextEdit1;
            ////column.ColumnEdit = this.repositoryItemMemoEdit1;
            ////column.OptionsColumn.AllowEdit = false;

            ////column = treeList1.Columns["EndYear"];
            ////column.Caption = "计划结束时间";
            ////column.Width = 70;
            ////column.OptionsColumn.AllowSort = false;
            ////column.VisibleIndex = 761;
            //////column.ColumnEdit = repositoryItemTextEdit1;
            ////column.ColumnEdit = this.repositoryItemMemoEdit1;
            ////column.OptionsColumn.AllowEdit = false;

            ////column = treeList1.Columns["Remark"];
            ////column.Caption = "备注";
            ////column.Width = 70;
            ////column.OptionsColumn.AllowSort = false;
            ////column.VisibleIndex = 799;
            //////column.ColumnEdit = repositoryItemTextEdit1;
            ////column.ColumnEdit = this.repositoryItemMemoEdit1;
            ////column.OptionsColumn.AllowEdit = false;

        }

        private void AddColumn2()//建设期间贷款利息
        {
            TreeListColumn column = treeList1.Columns["Code"];

            column = treeList1.Columns["Code"];
            column.Caption = "项目名称";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 800;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L4"];
           column.Caption = "建设性质";
           column.Width = 70;
           column.OptionsColumn.AllowSort = false;
           column.VisibleIndex = 801;
           column.ColumnEdit = repositoryItemTextEdit1;
           column.OptionsColumn.AllowEdit = false;

           column = treeList1.Columns["IsConn"];
            column.Caption = "变电容量(MVA)";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 802;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["Remark"];
            column.Caption = "线路长度(KM)";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 803;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;

            column = treeList1.Columns["L5"];
            column.Caption = "投产年限";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 804;
            //column.VisibleIndex = -1;
            column.ColumnEdit = repositoryItemTextEdit1;
            column.OptionsColumn.AllowEdit = false;


            column = treeList1.Columns["L6"];
            column.Caption = "总投资(万元)";
            column.Width = 110;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = 805;
            //column.VisibleIndex = -1;
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


            PowerEachList pel = this.ctrlPowerEachList11.FocusedObject;
            if (pel == null)
                return;


            Form9Print_Liao frm = new Form9Print_Liao();
            frm.IsSelect = isSelect;
            frm.Text = this.ctrlPowerEachList11.FocusedObject.ListName;
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


            foreach (TreeListNode node in xTreeList.Nodes)
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


           //  if (node.Nodes.Count > 0 || node.Nodes.Count =0||node.ParentNode == null || node.ParentNode != null)
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
            this.ctrlPowerEachList11.AddObjecta(type,false);
            this.ctrlPowerEachList11.IsJSXM = true;
            this.ctrlPowerEachList11.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            this.ctrlPowerEachList11.RefreshData(type);

            Show();
            Application.DoEvents();

            InitRight();

            this.ctrlPowerEachList11.colListName.Caption = "名称";
            this.ctrlPowerEachList11.colListName.Width = 60;
            this.ctrlPowerEachList11.colRemark.Width = 5;

         //   InitSodata();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachList11.UpdateObject();
            if (this.ctrlPowerEachList11.FocusedObject == null)
                return;

            typeFlag2 = this.ctrlPowerEachList11.FocusedObject.UID;

            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            InitSodata();
            //LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachList11.DeleteObject("sb");
           if (this.ctrlPowerEachList11.FocusedObject == null)
                return;

            typeFlag2 = this.ctrlPowerEachList11.FocusedObject.UID;

            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            InitSodata();
            //LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctrlPowerEachList11.FocusedObject == null)
                return;
            FrmEditProject4 frm = new FrmEditProject4();
           
            frm.FlagId = typeFlag2;
            try
            {
                if (treeList1.FocusedNode["ID"].ToString() != null && treeList1.FocusedNode["ID"].ToString() == "")
                    frm.PowerUId = treeList1.FocusedNode["ID"].ToString();
            }
            catch { }
            frm.isupdate = false;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //PowerProTypes psp_Type = new PowerProTypes();
                //psp_Type.Title = frm.TypeTitle;
                //psp_Type.Flag = frm.PowerType;
                //psp_Type.Flag2 = typeFlag2;
                //psp_Type.ParentID = "0";

                //try
                //{
                //    psp_Type.ID = Common.Services.BaseService.Create("InsertPowerProTypes", psp_Type).ToString();
                //    // dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(psp_Type, dataTable.NewRow()));
                   
                //}
                //catch { }
                treeList1.BeginUpdate();
                InitSodata();

                treeList1.EndUpdate();
              //  FoucsLocation(uid1, treeList1.Nodes);

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
                string id=psp_Type.ID;
                psp_Type.Title = frm.TypeTitle;
                psp_Type.Flag = frm.PowerType;
                psp_Type.Flag2 = (string)focusedNode.GetValue("Flag2");
                psp_Type.ParentID = focusedNode.GetValue("ID").ToString();

                try
                {
                    Common.Services.BaseService.Create("InsertPowerProTypes", psp_Type);
                    

                    LoadData();
                    FoucsLocation(psp_Type.ID, treeList1.Nodes); 
                    treeList1.RefreshDataSource();
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
            //    MsgBox.Show("一级项目名称不能修改！");
            //    return;
            //}

            FrmEditProject4 frm = new FrmEditProject4();
            frm.FlagId = typeFlag2;
           
            frm.PowerUId=treeList1.FocusedNode["ID"].ToString();
           // frm.Isupdate = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    treeList1.BeginUpdate();
                    InitSodata();
                   
                    treeList1.EndUpdate();

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

            //if (treeList1.FocusedNode.ParentNode==null)
            //{
            //    MsgBox.Show("一级项目不允许删除！");
            //    return;
            //}

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
                ppss.ID=treeList1.FocusedNode["ID"].ToString();
                ppss.Flag2=typeFlag2;


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
                    treeList1.BeginUpdate();
                    InitSodata();

                    treeList1.EndUpdate();
                  
                }
                catch (Exception ex)
                {
                    MsgBox.Show("删除出错：" + ex.Message);
                }
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctrlPowerEachList11.FocusedObject == null)
            {
                MsgBox.Show("请先添加项目规划");
                return;
            }
               
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


            string fieldName = treeList1.FocusedColumn.FieldName;

            if (fieldName == "Title" || fieldName == "Remark" || fieldName == "IsConn" || fieldName == "L1" || fieldName == "L2" || fieldName == "L3" || fieldName == "L4" || fieldName == "L5" || fieldName == "L6" || fieldName == "Code" || fieldName == "EndYear")
            {
                return;
            }

            try
            {
                FormNewType4 frm = new FormNewType4();


                frm.IsUpdate = true;
                frm.Type = treeList1.FocusedColumn.FieldName;
                frm.Flag2 = typeFlag2;
                frm.Type1 = ctrlPowerEachList11.FocusedObject.UID;
                frm.Text = "修改分类";
                

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

            if (fieldName == "Title" || fieldName == "Remark" || fieldName == "IsConn" || fieldName == "L1" || fieldName == "L2" || fieldName == "L3" || fieldName == "L4" || fieldName == "L5" || fieldName == "L6" || fieldName == "StartYear" || fieldName == "EndYear")
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
            FrmEditProject2 fep = new FrmEditProject2();
            fep.FlagId = typeFlag2;
            fep.PowerUId = treeList1.FocusedNode["ID"].ToString();
            string uid1 = treeList1.FocusedNode["ID"].ToString();
            //LineInfo li = Common.Services.BaseService.GetOneByKey<LineInfo>(treeList1.FocusedNode["Code"].ToString());
            //if (li != null)
            //    fep.IsLine = true;
            //substation li1 = Common.Services.BaseService.GetOneByKey<substation>(treeList1.FocusedNode["ID"].ToString());
            //if (li1 != null)
            //    fep.IsPower = true;
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

            FrmEditProject4 fep = new FrmEditProject4();
            fep.FlagId = typeFlag2;
            fep.PowerUId = treeList1.FocusedNode["ID"].ToString();
            string uid1 = treeList1.FocusedNode["ID"].ToString();
            //LineInfo li = Common.Services.BaseService.GetOneByKey<LineInfo>(treeList1.FocusedNode["Code"].ToString());
            //if (li != null)
            //    fep.IsLine = true;
            //substation li1 = Common.Services.BaseService.GetOneByKey<substation>(treeList1.FocusedNode["ID"].ToString());
            //if (li1 != null)
            //    fep.IsPower = true;

            if (fep.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                treeList1.ExpandAll();
                FoucsLocation(uid1, treeList1.Nodes);
            }
        }

        private void 关联图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (ctrlPowerEachList11.FocusedObject == null)
            //    return;


            //foreach (DataRow rws in dt.Rows)
            //{
            //    rws["C"] = false;
            //}

            //string sid = ctrlPowerEachList11.FocusedObject.UID;

            //PowerPicSelect ppsn = new PowerPicSelect();
            //ppsn.EachListID = sid;

            //IList<PowerPicSelect> liss = Services.BaseService.GetList<PowerPicSelect>("SelectPowerPicSelectList", ppsn);

            //foreach (PowerPicSelect pps in liss)
            //{
            //    foreach (DataRow rw in dt.Rows)
            //    {
            //        if (pps.PicSelectID == rw["A"].ToString())
            //            rw["C"] = true;
            //    }
            //}
            //FrmPicTypeSelect fpt = new FrmPicTypeSelect();
            //fpt.DT = dt;
            //if (fpt.ShowDialog() == DialogResult.OK)
            //{
            //    dt = fpt.DT;

            //    int c=0;
            //    foreach (PowerPicSelect pps1 in liss)
            //    {
            //        c=0;
            //        foreach (DataRow rw in dt.Rows)
            //        {
            //            if (pps1.PicSelectID == rw["A"].ToString() && (bool)rw["C"])
            //                c = 1;    
            //        }
            //        if (c == 0)
            //        {
            //            Services.BaseService.Delete<PowerPicSelect>(pps1);
            //        }
            //    }


            //    foreach (DataRow rw1 in dt.Rows)
            //    {
            //        c = 0;
            //        if ((bool)rw1["C"])
            //        {
            //            foreach (PowerPicSelect pps2 in liss)
            //            {
            //                if (pps2.PicSelectID == rw1["A"].ToString())
            //                    c = 1;
            //            }
            //            if (c == 0)
            //            {
            //                PowerPicSelect pp3 = new PowerPicSelect();
            //                pp3.EachListID = sid;
            //                pp3.PicSelectID = rw1["A"].ToString();

            //                Services.BaseService.Create<PowerPicSelect>(pp3);
            //            }
            //        }
            //    }
            //    InitSodata();  
            //}
        }
        private void InitSodata()
        {

            LoadData();       
        
        }

        private void FoucsLocation(string id)
        {
            foreach (TreeListNode tln in treeList1.Nodes)
            {
                if (tln["ID"].ToString() == id)
                    treeList1.FocusedNode = tln;
                    tln.Selected = true;
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

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctrlPowerEachList11.FocusedObject == null)
            {
                MsgBox.Show("请先添加项目规划");
                return;
            }

                  InsertLineData2();                  
        }






        private void InsertLineData2()
        {


            PowerEachList pel = this.ctrlPowerEachList11.FocusedObject;
            if (pel==null)
            {
                MsgBox.Show("请先添加项目规划");
                return;
            }
            TreeListNode tln = treeList1.FocusedNode;
            
            //try
            //{
            //    string parentid = tln["ParentID"].ToString();
            //    string id = tln["ID"].ToString();
            //    string flag2 = tln["Flag2"].ToString();
            //}
            //catch { }
            string typeid = pel.UID;
      

            PowerProTypes z = new PowerProTypes();
            PowerProYears h = new PowerProYears();
            PowerProValues j = new PowerProValues();


            PowerProYears ppy1=new PowerProYears();
            ppy1.Flag=typeid;

            //IList<PowerProYears> listppy = Services.BaseService.GetList<PowerProYears>("SelectPowerProYearsListByFlag", ppy1);

            try
            {
                DataTable dts = new DataTable();
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel文件(*.xls)|*.xls";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    dts = GetExcel(op.FileName);

                    for (int i = 0; i < dts.Rows.Count; i++)
                    {
                        //if (dts.Rows[i][1].ToString().IndexOf("合计") >= 0)
                        //    continue;
                        z = new PowerProTypes();
                        string guid=Guid.NewGuid().ToString();
                        z.ID = guid;
                        z.Flag2 = typeid;
                       // z.ParentID = id;


                        string strflag = "";
                        foreach (DataColumn dc in dts.Columns)
                        {
                            strflag = dc.Caption.ToString();
                            try
                            {
                                switch (strflag)
                                {
                                    case "区域名称":
                                        z.Title = dts.Rows[i][dc.ColumnName].ToString();
                                        break;
                                    case "项目名称":
                                        z.Code = dts.Rows[i][dc.ColumnName].ToString();
                                        break;
                                    case "建设性质":
                                        z.L4 = dts.Rows[i][dc.ColumnName].ToString();
                                        break;

                                    case "变电容量(MVA)":
                                        try
                                        {
                                            z.IsConn =dts.Rows[i][dc.ColumnName].ToString();
                                        }
                                        catch { z.L2 = 0; }
                                        break;

                                    case "线路长度(KM)":
                                        try
                                        {
                                            z.Remark = dts.Rows[i][dc.ColumnName].ToString();
                                        }
                                        catch { z.L3 = 0; }
                                        break;

                                    case "投产年限":
                                        try
                                        {
                                            z.L5 = Convert.ToDouble(dts.Rows[i][dc.ColumnName].ToString());
                                        }
                                        catch { z.L5 = 0; }
                                        break;

                                    case "总投资(万元)":
                                        try
                                        {
                                            z.L6 = Convert.ToDouble(dts.Rows[i][dc.ColumnName].ToString());
                                        }
                                        catch { z.L6 = 0; }
                                        break;

                                    default:
                                        try
                                        {
                                            PowerProYears ppy2 = new PowerProYears();
                                            ppy2.Year = strflag;
                                            ppy2.Flag = typeid;

                                            IList<PowerProYears> listyear2 = Services.BaseService.GetList<PowerProYears>("SelectPowerProYearsByYearFlag", ppy2);
                                            if (listyear2.Count == 0)
                                            {
                                                Services.BaseService.Create<PowerProYears>(ppy2);
                                            }
                                            PowerProTypes pvalues = (PowerProTypes)Services.BaseService.GetObject("SelectPowerProTypesByCodeAndTitle", z);
                                            if (pvalues != null)
                                                z.ID = pvalues.ID;
                                            j = new PowerProValues();
                                            j.TypeID = z.ID;
                                            j.TypeID1 = typeid;
                                            j.Year = strflag;
                                            j.Value = dts.Rows[i][dc.ColumnName].ToString();
                                            Services.BaseService.Update<PowerProValues>(j);
                                        }
                                        catch { }
                                        break;
                                }
                            }
                            catch { }
                        }
                        Services.BaseService.Update("UpdatePowerProTypesByCodeAndTitle", z);
                    }
                }
                ReLoadData();
            }
            catch { MsgBox.Show("导入格式不正确！"); }
        }


        private void InsertLineData1()
        {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
                return;
            PowerProTypes z = new PowerProTypes();
            PowerProYears h = new PowerProYears();
            PowerProValues j = new PowerProValues();
            string parentid = tln["ParentID"].ToString();
            string id = tln["ID"].ToString();
            string flag2 = tln["Flag2"].ToString();
            object obj = Services.BaseService.GetObject("SelectPowerProTypesList", "");
          
            try
            {               
                DataTable dts = new DataTable();
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel文件(*.xls)|*.xls";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    dts = GetExcel(op.FileName); 
              
                    for (int i = 1; i < dts.Rows.Count; i++)
                    {
                        if (dts.Rows[i][1].ToString().IndexOf("合计") >= 0)
                            continue;
                        z.ID = Guid.NewGuid().ToString();
                        string strflag = "";
                        foreach (DataColumn dc in dts.Columns)
                        {
                           strflag = dc.Caption.ToString();
                            try {
                            switch (strflag)
                            {
                                case "项目名称":
                                   
                                        z.Title = dts.Rows[i][dc.ColumnName].ToString(); 
                                     
                                   
                                    break;
                                case "计划开工时间":
                                    if (dts.Rows[i][dc.ColumnName].ToString() == "")
                                    { z.StartYear = 0; }
                                    else
                                        z.StartYear = int.Parse(dts.Rows[i][dc.ColumnName].ToString());
                                    break;
                                case "计划结束时间":
                                    if (dts.Rows[i][dc.ColumnName].ToString() == "")
                                    { z.EndYear = 0; }
                                    else
                                        z.EndYear = int.Parse(dts.Rows[i][dc.ColumnName].ToString());
                                    break;
                                case "备注":
                                    z.IsConn = dts.Rows[i][dc.ColumnName].ToString();
                                    break;
                                case "备注2":
                                    break;
                                case "台数":
                                    if (dts.Rows[i][dc.ColumnName].ToString() == "")
                                    { z.L1 = 0; }
                                    else
                                        z.L1 = double.Parse(dts.Rows[i][dc.ColumnName].ToString());
                                    break;
                                case "容量(MVA)":
                                    if (dts.Rows[i][dc.ColumnName].ToString() == "")
                                    { z.L2 = 0; }
                                    else
                                        z.L2 = double.Parse(dts.Rows[i][dc.ColumnName].ToString());
                                    break;
                                case "负荷率(%)":
                                    if (dts.Rows[i][dc.ColumnName].ToString() == "")
                                    { z.L5 = 0; }
                                    else
                                        z.L5 = double.Parse(dts.Rows[i][dc.ColumnName].ToString());
                                    break;
                                case "最大负荷(MVA)":
                                    if (dts.Rows[i][dc.ColumnName].ToString() == "")
                                    { z.L6 = 0; }
                                    else
                                        z.L6 = double.Parse(dts.Rows[i][dc.ColumnName].ToString());
                                    break;
                                case "长度(KM)":
                                    if (dts.Rows[i][dc.ColumnName].ToString() == "")
                                    { z.L3 = 0; }
                                    else
                                        z.L3 = double.Parse(dts.Rows[i][dc.ColumnName].ToString());
                                    break;
                                case "型号":
                                    z.L4 = dts.Rows[i][dc.ColumnName].ToString();
                                    break;
                                default:
                                    j.Value = dts.Rows[i][dc.ColumnName].ToString();
                                    j.TypeID = z.ID;
                                    j.Year = dc.ColumnName;
                                  //  j.TypeID1 = flag2;
                                    Services.BaseService.Create<PowerProValues>(j);
                                    break;
                            }
                        }
                        catch { }
                            ////if (dc.Caption.IndexOf("自定义列") >= 0)
                            ////{
                                
                            ////    j.Value = dts.Rows[i][dc.ColumnName].ToString();
                            ////    j.TypeID = z.ID;
                            ////    j.Year = dc.ColumnName;
                            ////    j.TypeID1 = flag2;
                            ////    Services.BaseService.Create<PowerProValues>(j);
                            ////}
                            ////if (dc.Caption.IndexOf("项目名称") >= 0)
                            ////{
                            ////    z.Title = dts.Rows[i][dc.ColumnName].ToString();
                            ////}
                            ////if (dc.Caption.IndexOf("台数") >= 0)
                            ////{
                            ////    if (dts.Rows[i][dc.ColumnName].ToString() == "")
                            ////    { z.L1 = 0; }
                            ////    else
                            ////    z.L1 = double.Parse(dts.Rows[i][dc.ColumnName].ToString());
                            ////}
                            ////if (dc.Caption.IndexOf("容量") >= 0)
                            ////{       if (dts.Rows[i][dc.ColumnName].ToString() == "")
                            ////        { z.L2 = 0; }
                            ////    else
                            ////    z.L2 = double.Parse(dts.Rows[i][dc.ColumnName].ToString());
                            ////}
                            ////if (dc.Caption.IndexOf("负荷率") >= 0)
                            ////{
                            ////    if (dts.Rows[i][dc.ColumnName].ToString() == "")
                            ////    { z.L5 = 0; }
                            ////    else
                            ////    z.L5 = double.Parse(dts.Rows[i][dc.ColumnName].ToString());
                            ////}
                            ////if (dc.Caption.IndexOf("最大负荷") >= 0)
                            ////{
                            ////    if (dts.Rows[i][dc.ColumnName].ToString() == "")
                            ////    { z.L6 = 0; }
                            ////    else
                            ////    z.L6 = double.Parse(dts.Rows[i][dc.ColumnName].ToString());
                            ////}
                            ////if (dc.Caption.IndexOf("长度") >= 0)
                            ////{
                            ////    if (dts.Rows[i][dc.ColumnName].ToString() == "")
                            ////    { z.L3 = 0; }
                            ////    else
                            ////    z.L3 = double.Parse(dts.Rows[i][dc.ColumnName].ToString());
                            ////}
                            ////if (dc.Caption.IndexOf("型号") >= 0)
                            ////{
                            ////    z.L4 = dts.Rows[i][dc.ColumnName].ToString();
                            ////}
                            ////if (dc.Caption.IndexOf("计划开工") >= 0)
                            ////{
                            ////    if (dts.Rows[i][dc.ColumnName].ToString() == "")
                            ////    { z.StartYear =0 ; }
                            ////    else
                            ////        z.StartYear = int.Parse(dts.Rows[i][dc.ColumnName].ToString());
                            ////}
                            ////if (dc.Caption.IndexOf("计划结束") >= 0)
                            ////{
                            ////    if (dts.Rows[i][dc.ColumnName].ToString() == "")
                            ////    { z.EndYear = 0; }
                            ////    else
                            ////        z.EndYear = int.Parse(dts.Rows[i][dc.ColumnName].ToString());
                            ////}
                            ////if (dc.Caption.IndexOf("备注") >= 0)
                            ////{
                            ////    z.IsConn = dts.Rows[i][dc.ColumnName].ToString();
                            ////}
 
                        }   
                        z.Flag2 = flag2;
                        z.ParentID = id;
                        z.Flag = 1;  
                        Services.BaseService.Create<PowerProTypes>(z);
                    }
            }
            ReLoadData();
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
             Hashtable h1 = new Hashtable();
            for (int k = 0; k < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            {
                //dt.Columns.Add("col" + k.ToString());
                //string aa = fpSpread1.Sheets[0].Cells[0, k].Value.ToString();
                //dt.Columns.Add(aa);
                foreach (TreeListColumn tlc in this.treeList1.Columns)
                {
                    if (tlc.Caption == fpSpread1.Sheets[0].Cells[2, k].Text)
                    {
                        if (!h1.Contains(tlc.Caption))
                        {
                            dt.Columns.Add(tlc.Caption);

                            h1.Add(tlc.Caption, tlc.FieldName);
                        }
                        //aa++;
                    }
                }
            }


            for (int i =3; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
            {
                DataRow dr = dt.NewRow();
                str = "";
                for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
                {
                    str = str + fpSpread1.Sheets[0].Cells[i, j].Text;
                      if (h1[fpSpread1.Sheets[0].Cells[2, j].Text]!=null)
                        dr[fpSpread1.Sheets[0].Cells[2, j].Text] = fpSpread1.Sheets[0].Cells[i, j].Text;
                   // dr[j] = fpSpread1.Sheets[0].Cells[i, j].Text;
                }
                if (str != "")
                    dt.Rows.Add(dr);

            }
            return dt;
        }
        private void ReLoadData()
        {
            if (this.ctrlPowerEachList11.FocusedObject == null)
                return;

            typeFlag2 = this.ctrlPowerEachList11.FocusedObject.UID;

            this.Cursor = Cursors.WaitCursor;
            treeList1.BeginUpdate();
            InitSodata();
            //LoadData();
            treeList1.EndUpdate();
            this.Cursor = Cursors.Default;
        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        
            if (this.ctrlPowerEachList11.FocusedObject == null)
                return;
            FormClass fc = new FormClass();
            gridControl1.DataSource = fc.ConvertTreeListToDataTable(treeList1);

            //for (int i = 0; i < gridView1.Columns.Count; i++)
            //{
            //    if (gridView1.Columns[i].Caption.IndexOf("自定义列") >= 0)
            //    {
            //        gridView1.Columns[i].Caption = gridView1.Columns[i].Caption.Substring(0, gridView1.Columns[i].Caption.Length - 4);
            //    }
            //}
            gridView1.Columns["Title"].Caption = "城区名称";
            gridView1.Columns["Title"].VisibleIndex = 0;
            gridView1.Columns["Code"].Caption = "项目名称";
            gridView1.Columns["Code"].VisibleIndex = 800;

            gridView1.Columns["L4"].Caption = "建设性质";
            gridView1.Columns["L4"].VisibleIndex = 801;
            gridView1.Columns["IsConn"].Caption = "变电容量(MVA)";
            gridView1.Columns["IsConn"].VisibleIndex = 802;
            gridView1.Columns["Remark"].Caption = "线路长度(KM)";
            gridView1.Columns["Remark"].VisibleIndex = 803;
          
            gridView1.Columns["L5"].Caption = "投产年限";
            gridView1.Columns["L5"].VisibleIndex = 804;
            gridView1.Columns["L6"].Caption = "总投资（万元）";
            gridView1.Columns["L6"].VisibleIndex = 805;

            gridView1.Columns["StartYear"].Visible = false;
            gridView1.Columns["EndYear"].Visible = false;
            gridView1.Columns["L2"].Visible = false;
            gridView1.Columns["ParentID"].Visible = false;
           
            gridView1.Columns["L1"].Visible = false;
            gridView1.Columns["Flag"].Visible = false;
            gridView1.Columns["Flag2"].Visible = false;
            gridView1.Columns["CreateTime"].Visible = false;
            gridView1.Columns["UpdateTime"].Visible = false;
            gridView1.Columns["L3"].Visible = false;
            title = this.ctrlPowerEachList11.FocusedObject.ListName;
            FileClass.ExportExcel("111222","",gridControl1);

        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ctrlPowerEachList11.FocusedObject == null)
            {
                MsgBox.Show("没有要删除的数据！");
                return;
            }
            if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
                return;
     
            string  Flag2=ctrlPowerEachList11.FocusedObject.UID;
            Services.BaseService.Update("DeletePowerProTypesByFlag2", Flag2);
            MsgBox.Show("清除成功！");
            // InitSodata2();
            ReLoadData();

        }
    }
}