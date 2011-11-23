using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.HistoryValue;
using System.Collections;
using Itop.Common;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
    public partial class Form7_BaseYearRate : FormBase
    {
        DataTable dt1 = new DataTable();
        private bool bl1 = true;
        private bool bl2 = false;
        private int typeFlag2 = 7;
        private string baseyearFlag = "4132ae36-36b3-47ed-a9b9-163a6479d5d3";
        private int endyear = 0;
        public Form7_BaseYearRate()
        {
            InitializeComponent();
        }
        private string EnsureBaseYear(string baseyear)
        {
            PSP_BaseYearRate BaseYearrate = (PSP_BaseYearRate)Common.Services.BaseService.GetObject("SelectPSP_BaseYearRateByKey", baseyearFlag);
            if (BaseYearrate.BaseYear != "0")
            {
                Hashtable ha = new Hashtable();
                ha.Add("Flag", typeFlag2);
                ha.Add("Year", Convert.ToInt32(BaseYearrate.BaseYear));
                PSP_Years baseyearlistYears = (PSP_Years)Common.Services.BaseService.GetObject("SelectPSP_YearsByYearFlag", ha);
                if (baseyearlistYears != null)
                {

                    baseyear = baseyearlistYears.Year.ToString();

                }
            }
            return baseyear;
        }
        private void Form7_BaseYearRate_Load(object sender, EventArgs e)
        {
            if (dt1 != null)
            {
                dt1.Columns.Clear();
                treeList1.Columns.Clear();
            }
            //this.gridColumn2.ColumnEdit = this.repositoryItemTextEdit1;
            PSP_Types psp_Type = new PSP_Types();
            psp_Type.Flag2 = typeFlag2;
            IList listTypes = Common.Services.BaseService.GetList("SelectPSP_TypesByFlag2", psp_Type);

            dt1 = Itop.Common.DataConverter.ToDataTable(listTypes, typeof(PSP_Types));

            treeList1.DataSource = dt1;

            treeList1.Columns["Title"].Caption = "分类名";
            treeList1.Columns["Title"].Width = 180;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Flag"].VisibleIndex = -1;
            treeList1.Columns["Flag"].OptionsColumn.ShowInCustomizationForm = false;
            treeList1.Columns["Flag2"].VisibleIndex = -1;
            treeList1.Columns["Flag2"].OptionsColumn.ShowInCustomizationForm = false;
            string baseyear = "";
            baseyear=EnsureBaseYear(baseyear);
            PSP_Years psp_Year = new PSP_Years();
            IList<PSP_Years> listYears;
            if (baseyear != "")
            {
                psp_Year.Flag = typeFlag2;
                psp_Year.Year = Convert.ToInt32(baseyear);
                listYears = Common.Services.BaseService.GetList<PSP_Years>("SelectPSP_YearsListByFlagAndYear", psp_Year);
            }
            else
            {
                MsgBox.Show("没有添加基准年，请添加后再操作！");
                return;
            }
            if (listYears.Count > 1)
                endyear = listYears[listYears.Count - 1].Year;
            else
            {
                MsgBox.Show("没有添加预测年，请添加后再操作！");
                this.DialogResult = DialogResult.OK;
                return;
            }
            foreach (PSP_Years item in listYears)
            {
                
                if (item.Year.ToString() == baseyear)
                    continue;
                AddColumn(item.Year);
                 PSP_BaseYearRate psp_Yeartemp = new PSP_BaseYearRate();
                 foreach (PSP_Types psp_Typetemp in listTypes)
                 {

                     psp_Yeartemp.BaseYear = item.Year.ToString();
                     psp_Yeartemp.TypeID = psp_Typetemp.ID;
                     try
                     {
                         IList<PSP_BaseYearRate> list1 = Common.Services.BaseService.GetList<PSP_BaseYearRate>("SelectPSP_BaseYearRateByYear", psp_Yeartemp);

                         if (list1.Count == 1)
                         {
                             TreeListNode node = treeList1.FindNodeByFieldValue("ID",Convert.ToInt32(list1[0].TypeID));
                             if (node != null)
                             {
                                 if (list1[0].YearRate!="")
                                 node.SetValue(list1[0].BaseYear + "年", Convert.ToDouble(list1[0].YearRate));
                             else node.SetValue(psp_Yeartemp.BaseYear + "年", "0");

                             }


                         }
                         else if (list1.Count == 0)
                         {
                          try
                            {



                                psp_Yeartemp.UID = Guid.NewGuid().ToString();
                                psp_Yeartemp.YearRate = "0";
                                Common.Services.BaseService.Create<PSP_BaseYearRate>(psp_Yeartemp);
                                TreeListNode node = treeList1.FindNodeByFieldValue("ID", psp_Yeartemp.TypeID);
                                if (node != null)
                                {

                                    node.SetValue(psp_Yeartemp.BaseYear + "年", "0");
                                }
                               
                            }
                            catch (Exception ex)
                            {
                               // MsgBox.Show("出错啦：" + ex.Message);
                            }
                         }

                     }
                     catch (Exception ex)
                     {
                       //  MsgBox.Show(ex.ToString());
                     }
                 }
            }
            treeList1.ExpandAll();
           

        }
        //添加年份后，新增一列

        private void AddColumn(int year)
        {
            int nInsertIndex = GetInsertIndex(year);

            dt1.Columns.Add(year + "年", typeof(double));

            TreeListColumn column = treeList1.Columns.Insert(nInsertIndex);
            column.FieldName = year + "年";
            column.Tag = year;
            column.Caption = year + "年";
            column.Width = 70;
            column.OptionsColumn.AllowSort = false;
            column.VisibleIndex = nInsertIndex - 2;//有两列隐藏列
            column.ColumnEdit = repositoryItemTextEdit1;
            column.Format.FormatType = DevExpress.Utils.FormatType.Custom;
            column.Format.FormatString = "p2";
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
           

                savedate();
                bl2 = false;
                this.Close();
        }

        private void treeList1_CellValueChanged(object sender, DevExpress.XtraTreeList.CellValueChangedEventArgs e)
        {

            if (!bl1)
                return;
           
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
                return;

            TreeListColumn tnc = treeList1.FocusedColumn;
            if (tnc.Caption.IndexOf("年") < 0)
                return;
            bl1 = false;
            double fuhe = 0;
            try
            {
                fuhe = double.Parse(tln[tnc.FieldName].ToString());
            }
            catch { return; }
              int a = 0;
            try { a = int.Parse(tnc.Caption.Replace("年", "")); }
            catch { }
            
            int b = endyear;
            double fh = 0;
            for (int i = a; i <= b; i++)
            {
                fh = 0;
                try
                {
                    tln.SetValue(i + "年", fh + fuhe);
                }
                catch { }
            }
            bl1 = true;
            bl2 = true;
        }

        private void Form7_BaseYearRate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bl2)
            {
                if (MessageBox.Show("是否保存数据?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    savedate();
                bl2 = false;
            }
        }
        private void savedate()
        {
            string baseyear = "";
            baseyear = EnsureBaseYear(baseyear);
            if (baseyear != "")
            {
                PSP_BaseYearRate yearrate = new PSP_BaseYearRate();
                IList listTypes = Common.Services.BaseService.GetList("SelectPSP_BaseYearRateList", yearrate);
                try
                {
                    foreach (PSP_BaseYearRate yearratetemp in listTypes)
                    {
                        if (yearratetemp.BaseYear == baseyear) continue;
                        TreeListNode node = treeList1.FindNodeByFieldValue("ID", yearratetemp.TypeID);
                        if (node != null)
                        {
                            yearrate.BaseYear = yearratetemp.BaseYear;
                            yearrate.YearRate = Convert.ToString(node[yearratetemp.BaseYear + "年"]);
                            yearrate.TypeID = yearratetemp.TypeID;
                            yearrate.UID = Guid.NewGuid().ToString();
                            Common.Services.BaseService.Update("UpdatePSP_BaseYearRateByYear", yearrate);
                        }
                    }
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                   // MsgBox.Show("出错啦：" + ex.Message);
                }

            }
        }
    }
}