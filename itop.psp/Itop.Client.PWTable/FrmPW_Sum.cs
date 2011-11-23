using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using Itop.Client.Base;
using Itop.Common;
using Itop.Domain.PWTable;
using Itop.Client.Common;
using DevExpress.XtraGrid;

namespace Itop.Client.PWTable
{ 
    public partial class FrmPW_Sum : FormBase
    {  
        string type = "";
        public bool addright = true;
        public bool editright = true;
        public bool deletetright = true;
        public bool printright = true;
        /// <summary>
        /// 获取GridControl对象
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        

        public FrmPW_Sum()
        {
            InitializeComponent();
        }

        private void FrmProject_Sum_Load(object sender, EventArgs e)
        {
            this.ctrlProject_Sum1.Proguid = Itop.Client.MIS.ProgUID;
            this.ctrlProject_Sum1.Type = type;
            this.ctrlProject_Sum1.RefreshData();
            InitRight();
        }

        private void InitRight()
        {
            if (!AddRight)
            {
                barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!EditRight)
            {
                barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                ctrlProject_Sum1.editright = false;
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!DeleteRight)
            {
                barButtonDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!PrintRight)
            {
                barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                
            }
        }
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlProject_Sum1.AddObject();
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlProject_Sum1.UpdateObject();
        }

        private void barButtonDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlProject_Sum1.DeleteObject();
        }

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlProject_Sum1.PrintPreview();
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.ctrlProject_Sum1.GridControl);
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ctrlProject_Sum1.SubAdd();
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
            int aa = 0;
            //for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            //{
            //    bool bl = false;
            //    GridColumn gc = this.ctrlProject_Sum1.GridView.VisibleColumns[k - 1];
            //    dt.Columns.Add(gc.FieldName);
            //    h1.Add(aa.ToString(), gc.FieldName);
            //    aa++;
            //}
            {
                dt.Columns.Add("PQName");
                h1.Add("0", "PQName");
                dt.Columns.Add("PQtype");
                h1.Add("1", "PQtype");
                dt.Columns.Add("SubName");
                h1.Add("2", "SubName");

                dt.Columns.Add("LineName");
                h1.Add("3", "LineName");
                dt.Columns.Add("LineType");
                h1.Add("4", "LineType");
                dt.Columns.Add("LineLength");
                h1.Add("5", "LineLength");
                dt.Columns.Add("Num1");
                h1.Add("6", "Num1");
                dt.Columns.Add("Num2");
                h1.Add("7", "Num2");
                dt.Columns.Add("Num3");
                h1.Add("8", "Num3");
                dt.Columns.Add("Num4");
                h1.Add("9", "Num4");
                dt.Columns.Add("Num5");
                h1.Add("10", "Num5");
                dt.Columns.Add("Num6");
                h1.Add("11", "Num6");
                dt.Columns.Add("WG1");
                h1.Add("12", "WG1");
                dt.Columns.Add("Num7");
                h1.Add("13", "Num7");
                dt.Columns.Add("Num8");
                h1.Add("14", "Num8");
                dt.Columns.Add("WG2");
                h1.Add("15", "WG2");
                dt.Columns.Add("LineSX");
                h1.Add("16", "LineSX");
                dt.Columns.Add("col1");
                h1.Add("17", "col1");
                dt.Columns.Add("KBS");
                h1.Add("18", "KBS");
                dt.Columns.Add("KG");
                h1.Add("19", "KG");
                dt.Columns.Add("JXMS");
                h1.Add("20", "JXMS");
                dt.Columns.Add("LLXMC");
                h1.Add("21", "LLXMC");
                dt.Columns.Add("MaxFH");
                h1.Add("22", "MaxFH");
                dt.Columns.Add("SafeFH");
                h1.Add("23", "SafeFH");
                dt.Columns.Add("FZL");
                h1.Add("24", "FZL");
              
            }
            int m = 1;
            for (int i = m; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; i++)
            {
                DataRow dr = dt.NewRow();
                str = "";
                for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
                {
                    str = str + fpSpread1.Sheets[0].Cells[3 + i, j].Text;
                    dr[h1[j.ToString()].ToString()] = fpSpread1.Sheets[0].Cells[3 + i, j].Text;
                }
                if (str != "")
                {
                    if (fpSpread1.Sheets[0].Cells[3 + i, 22].Text != "" && fpSpread1.Sheets[0].Cells[3 + i, 23].Text != "")
                    {
                        dr[24] = (Convert.ToDecimal(fpSpread1.Sheets[0].Cells[3 + i, 22].Text) / Convert.ToDecimal(fpSpread1.Sheets[0].Cells[3 + i, 23].Text)*100).ToString("##.##");
                    }
                    dt.Rows.Add(dr);

                }

            }
            return dt;
        }
        private void InsertInfo()
        {
            string columnname = "";

            try
            {
                DataTable dts = new DataTable();
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel文件(*.xls)|*.xls";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    dts = GetExcel(op.FileName);
                    IList<PW_tb3a> lii = new List<PW_tb3a>();
                    DateTime s8 = DateTime.Now;
                    for (int i = 0; i < dts.Rows.Count; i++)
                    {


                        PW_tb3a l1 = new PW_tb3a();
                        foreach (DataColumn dc in dts.Columns)
                        {
                            columnname = dc.ColumnName;
                            //if (dts.Rows[i][dc.ColumnName].ToString() == "")
                            //    continue;

                            switch (dc.ColumnName)
                            {

                                case "LineLength":
                                case "Num1":
                                case "Num2":
                                case "Num3":
                                case "Num4":                           
                                case "Num6":
                                case "Num8":
                                case "MaxFH":
                                case "SafeFH":
                                case "WG1":
                                case "WG2":
                                case "FZL":
                                    decimal LL2 = 0;
                                    try
                                    {
                                        LL2 = Convert.ToDecimal(dts.Rows[i][dc.ColumnName].ToString());
                                    }
                                    catch { }
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, LL2, null);
                                    break;
                                case "Num5":
                                case "Num7":
                                case "KBS":
                                case "KG":
                                    int l5 = 0;
                                    try
                                    {
                                        l5 = Convert.ToInt32(dts.Rows[i][dc.ColumnName].ToString());
                                    }
                                    catch { }
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, l5, null);
                                    break;
                                default:
                                    l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, dts.Rows[i][dc.ColumnName].ToString(), null);
                                    break;
                            }
                        }
                        lii.Add(l1);
                    }

                    foreach (PW_tb3a lll in lii)
                    {

                        PW_tb3a l1 = new PW_tb3a();

                        IList<PW_tb3a> list = new List<PW_tb3a>();
                    
                        //if (type == "2")
                        //{
                        //    l1.S1 = lll.S1;
                        //    l1.T1 = lll.T1;
                        //    l1.T5 = lll.T5;
                        //    l1.S5 = type;
                        //    list = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByvalue3", l1);
                        //}


                        //if (list.Count > 0)
                        //{
                        //    lll.UID = list[0].UID;
                        //    Services.BaseService.Update<PW_tb3a>(lll);
                        //}
                        //else
                        //{
                            lll.UID = Guid.NewGuid().ToString();
                            lll.col2 = Itop.Client.MIS.ProgUID;
                            Services.BaseService.Create<PW_tb3a>(lll);
                        //}
                    }
                    this.ctrlProject_Sum1.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(columnname + ex.Message);
                MsgBox.Show("导入格式不正确！");

            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertInfo();
        }

    }
}