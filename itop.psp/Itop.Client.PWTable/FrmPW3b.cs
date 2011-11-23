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
using Itop.Common;
using Itop.Domain.PWTable;
using Itop.Client.Base;
using Itop.Client.Common;

namespace Itop.Client.PWTable
{
    public partial class FrmPW3b :Itop.Client.Base.FormBase
    {
        string type = "";
        public bool addright = true;
        public bool editright = true;
        public bool deletetright = true;
        public bool printright = true;
        public string ProgUID = "";
        /// <summary>
        /// 获取GridControl对象
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        

        public FrmPW3b()
        {
            InitializeComponent();
        }

        private void FrmProject_Sum_Load(object sender, EventArgs e)
        {
            ProgUID = Itop.Client.MIS.ProgUID;
            this.ctrlProject_Sum1.Type = type;
            this.ctrlProject_Sum1.RefreshData();
            InitRight();
        }

        private void InitRight()
        {
            if (!addright)
            {
                barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!editright)
            {
                barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                ctrlProject_Sum1.editright = false;
            }

            if (!deletetright)
            {
                barButtonDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!printright)
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
            InsertInfo();
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
            for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            {
                bool bl = false;
                GridColumn gc = this.ctrlProject_Sum1.GridView.VisibleColumns[k - 1];
                dt.Columns.Add(gc.FieldName);
                h1.Add(aa.ToString(), gc.FieldName);
                aa++;
            }

            int m = 1;
            for (int i = m; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; i++)
            {
                DataRow dr = dt.NewRow();
                str = "";
                for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
                {
                    str = str + fpSpread1.Sheets[0].Cells[i, j].Text;
                    dr[h1[j.ToString()].ToString()] = fpSpread1.Sheets[0].Cells[i, j].Text;
                }
                if (str != "")
                    dt.Rows.Add(dr);

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
                    IList<PW_tb3b> lii = new List<PW_tb3b>();
                    DateTime s8 = DateTime.Now;
                    for (int i = 0; i < dts.Rows.Count; i++)
                    {


                        PW_tb3b l1 = new PW_tb3b();
                        foreach (DataColumn dc in dts.Columns)
                        {
                            columnname = dc.ColumnName;
                            //if (dts.Rows[i][dc.ColumnName].ToString() == "")
                            //    continue;
                            l1.GetType().GetProperty(dc.ColumnName).SetValue(l1, dts.Rows[i][dc.ColumnName].ToString(), null);


                        }
                        lii.Add(l1);
                    }

                    foreach (PW_tb3b lll in lii)
                    {

                        PW_tb3b l1 = new PW_tb3b();

                        IList<PW_tb3b> list = new List<PW_tb3b>();


                        //{
                        lll.UID = Guid.NewGuid().ToString();
                        lll.col7 = type;
                        Services.BaseService.Create<PW_tb3b>(lll);
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

    }
}