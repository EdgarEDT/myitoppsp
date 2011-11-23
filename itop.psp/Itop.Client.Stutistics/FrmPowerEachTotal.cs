using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Client.Common;
using System.IO;
using Itop.Common;
using DevExpress.XtraTreeList.Nodes;

namespace Itop.Client.Stutistics
{
    public partial class FrmPowerEachTotal : Itop.Client.Base.FormBase
    {
        string title = "";
        //string unit = "";
        bool isSelect = false;
        static int  b=0;

        DevExpress.XtraGrid.GridControl gcontrol = null;

        public string Title
        {
            get { return title; }
        }

        public string Unit
        {
            get { return "单位:万元、公里、万千伏安、万千乏"; }
        }

        public DevExpress.XtraGrid.GridControl Gcontrol
        {
            get { return gcontrol; }
        }

        public bool IsSelect
        {
            set { isSelect = value; }
        }


        public FrmPowerEachTotal()
        {
            InitializeComponent();
        }

        private void FrmPowerEachTotal_Load(object sender, EventArgs e)
        {

            barButtonItem12.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (!isSelect)
            {
                barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            
            this.ctrlPowerEachTotalList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            this.ctrlPowerEachTotalList1.GridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(GridView_CellValueChanged);
            this.ctrlPowerEachTotalList1.RefreshData();
            InitRight();
        }

        void GridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (this.ctrlPowerEachTotalList1.FocusedObject == null)
                return;
            this.ctrlPowerEachTotal1.LineUID = this.ctrlPowerEachTotalList1.FocusedObject.UID;
            this.ctrlPowerEachTotal1.LineName = this.ctrlPowerEachTotalList1.FocusedObject.ListName;
        }


        private void InitRight()
        {
            if (!AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!EditRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem14.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem15.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!DeleteRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem6.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (!PrintRight)
            {
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }


        }


        void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.ctrlPowerEachTotalList1.FocusedObject == null)
                return;
            this.ctrlPowerEachTotal1.LineUID = this.ctrlPowerEachTotalList1.FocusedObject.UID;
            this.ctrlPowerEachTotal1.LineName = this.ctrlPowerEachTotalList1.FocusedObject.ListName;
            this.ctrlPowerEachTotal1.RefreshData();
        }



        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachTotalList1.AddObject();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachTotalList1.UpdateObject();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachTotalList1.DeleteObject();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachTotal1.AddObject();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachTotal1.UpdateObject();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachTotal1.DeleteObject();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachTotalList1.PrintPreview();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachTotal1.PrintPreview();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctrlPowerEachTotalList1.FocusedObject == null)
                return;
            ctrlPowerEachTotal1.InitGrid();

            title = this.ctrlPowerEachTotalList1.FocusedObject.ListName;

            
            gcontrol = ctrlPowerEachTotal1.gridControl1;
            this.DialogResult = DialogResult.OK;
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachTotal1.AddObject1();
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachTotal1.InsertData();
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctrlPowerEachTotalList1.FocusedObject == null)
                return;
            ctrlPowerEachTotal1.InitGrid();
            title = this.ctrlPowerEachTotalList1.FocusedObject.ListName;
            
            //FileClass.ExportExcel(ctrlPowerEachTotal1.gridControl1);
            FileClass.ExportExcel(title, "", ctrlPowerEachTotal1.gridControl1);
           
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            InsertLineData1();
        }

        private void InsertLineData1()
        {
            if (ctrlPowerEachTotalList1.FocusedObject == null)
            {
                MsgBox.Show("没有项目存在，无法导入！");
                return;
            }

            string UID = "";
            try
            {
                TreeListNode tln = this.ctrlPowerEachTotal1.ZHJ.FocusedNode;
                UID = tln["UID"].ToString();
            }
            catch
            { }
           
            string a = "";
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
                        if (dts.Rows[i][0].ToString() != "")             
                        {
                            PowerEachTotal li = new PowerEachTotal();
                            //PowerEachTotalList li1 = (PowerEachTotalList)Itop.Client.Common.Services.BaseService.GetObject("SelectPowerEachTotalListList", "");
                            object obj = Services.BaseService.GetObject("SelectPowerEachTotalList1", "");

                            li.PowerLineUID = ctrlPowerEachTotal1.LineUID;
                            li.Remark = ctrlPowerEachTotal1.LineName;
                            li.StuffName = dts.Rows[i][0].ToString();
                            li.Lengths = dts.Rows[i][1].ToString();
                            li.LCount = dts.Rows[i][2].ToString();
                            li.Total = dts.Rows[i][3].ToString();
                            li.Volume = dts.Rows[i][4].ToString();
                            li.Type = dts.Rows[i][5].ToString();
                            li.IsSum = Convert.ToDouble(dts.Rows[i][6].ToString());
                            li.ItSum = Convert.ToDouble(dts.Rows[i][7].ToString());
                            li.ParentID = UID;

                        


                            Services.BaseService.Create<PowerEachTotal>(li);

                        }
                   
            }

            this.ctrlPowerEachTotal1.RefreshData(); 
        }
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
            for (int k =0; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
            {
                dt.Columns.Add("col" + k.ToString());
            }


            for (int i = 3; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
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

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctrlPowerEachTotalList1.FocusedObject == null)
            {
                MsgBox.Show("没有要删除的数据！");
                return;
            }
            if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
                return;

            string Flag2 = ctrlPowerEachTotalList1.FocusedObject.UID;
            Services.BaseService.Update("DeletePowerEachTotalByUID", Flag2);
            MsgBox.Show("清除成功！");
            // InitSodata2();
            this.ctrlPowerEachTotal1.LineUID = this.ctrlPowerEachTotalList1.FocusedObject.UID;
            this.ctrlPowerEachTotal1.LineName = this.ctrlPowerEachTotalList1.FocusedObject.ListName;
            this.ctrlPowerEachTotal1.RefreshData();
        }

    }
}