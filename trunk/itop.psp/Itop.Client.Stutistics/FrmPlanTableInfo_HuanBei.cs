using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client;
using Itop.Common;
using DevExpress.XtraGrid.Columns;
using Itop.Domain.Stutistic;
using System.Collections;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.IO;
using DevExpress.XtraGrid.Views.BandedGrid;
namespace Itop.Client.Stutistics
{
    public partial class FrmPlanTableInfo_HuanBei : Itop.Client.Base.FormBase
    {
        string title = "";
        string unit = "";
        bool isSelect = false;
        public bool add = true;
        public bool edit = true;

        public bool delete = true;
        public bool print = true;

        string typeflag = "";
        private PowerEachList plantable = null;
        DateTime dt = new DateTime();
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
            set { gcontrol = value; }
        }

        public bool IsSelect
        {
            set { isSelect = value; }
        }
        public FrmPlanTableInfo_HuanBei(PowerEachList ptable)
        {
            InitializeComponent();
            plantable = ptable;
            typeflag = ptable.UID;
            dt =ptable.CreateDate ;
            this.Text = ptable.ListName;

        }
        private string type = "Plan";  //电厂
        private string flag = "Plan";//现状
        private string type2 = "PlanTable";//现状

      
        private void FrmPowerSubstationInfo_Load(object sender, EventArgs e)
        {
            if (isSelect)
            {
                //InitializeComponent();
                this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }

            //InitSodata2();

            this.ctrlItemPlanTable_HuaiBei1.GridView.GroupPanelText = this.ctrlItemPlanTable_HuaiBei1.GridView.GroupPanelText+"("+this.dt.ToShortDateString()+")";
            this.ctrlItemPlanTable_HuaiBei1.Type = type;
            this.ctrlItemPlanTable_HuaiBei1.Flag = typeflag;
            this.ctrlItemPlanTable_HuaiBei1.Type2 = type2;
            this.ctrlItemPlanTable_HuaiBei1.RefreshData1();

            try
            {
                foreach (GridColumn gc in this.ctrlItemPlanTable_HuaiBei1.GridView.Columns)
                {

                    if (gc.FieldName.Substring(0, 1) == "S")
                    {
                        gc.Visible = false;
                        gc.OptionsColumn.ShowInCustomizationForm = false;

                        if (gc.FieldName.Substring(0,2) == "SB")
                        {
                            gc.Visible = true;
                            gc.OptionsColumn.ShowInCustomizationForm = true;
                        }
                    }
                    if (gc.FieldName.IndexOf("Flag") >= 0)
                    {
                        gc.Visible = false;
                        gc.OptionsColumn.ShowInCustomizationForm = false;
                    }
                    if (gc.FieldName == "ParentID" || gc.FieldName == "CreateDate")
                    {
                        gc.Visible = false;
                        gc.OptionsColumn.ShowInCustomizationForm = false;
                    }
                }
            }
            catch (ExecutionEngineException ex)
            {
                MsgBox.Show(ex.Message);
            }
            InitValues();
            HideToolBarButton();
        }

        private void HideToolBarButton()
        {
            if (!add)
            {
                barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barAdd1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
              
            }

            if (!edit)
            {
                barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barEdit1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.ctrlItemPlanTable_HuaiBei1.editright = false;
             
            }
            if (!delete)
            {
                barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barDel1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
               
            }

            if (!print)
            {
                barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }
        }

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlItemPlanTable_HuaiBei1.AddObject(typeflag);
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlItemPlanTable_HuaiBei1.UpdateObject();
        }

        private void barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlItemPlanTable_HuaiBei1.DeleteObject();
        }

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlItemPlanTable_HuaiBei1.PrintPreview();
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          this.Close();
        }

        private void barAdd1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                FrmPlanTable_AddBands frm = new FrmPlanTable_AddBands();
                frm.Type = type;
                frm.Flag = flag;
                frm.Type2 = "PlanTable";
                frm.AddFlag = "PTFlag";


                if (frm.ShowDialog() == DialogResult.OK)
                {
                    InitValues();
                }

            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        foreach(GridColumn gc in this.ctrlItemPlanTable_HuaiBei1.GridView.Columns)
            //        {
            //            if (gc.FieldName == frm.ClassType)
            //            {
            //                gc.Caption = frm.ClassName;
            //                gc.Visible = true;
            //                gc.OptionsColumn.ShowInCustomizationForm = true;
            //            }
            //        }




            //    if (gc.Columns[0].FieldName.Substring(0, 1) == "S")
            //    {
            //        gc.Visible = false;
            //        foreach (PowerSubstationLine pss in li)
            //        {

            //            if (gc.Columns[0].FieldName == pss.ClassType)
            //            {
            //                gc.Visible = true;
            //                gc.Caption = pss.Title;
            //                gc.Columns[0].Caption = pss.Title;
            //                gc.Columns[0].Visible = true;
            //                gc.Columns[0].OptionsColumn.ShowInCustomizationForm = true;

            //            }
            //        }
            //    }
            //}
            }

            catch (Exception ex)
            { MsgBox.Show(ex.Message); }
        }
     
        private void InitValues()
        {
            PowerSubstationLine psl = new PowerSubstationLine();
            psl.Flag = flag;
            psl.Type = type;
            psl.Type2 = "PlanTable";

            IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psl);


            foreach (GridBand gc in this.ctrlItemPlanTable_HuaiBei1.bandedGridView1.Bands)
            {
                try
                {
                    if (gc.Columns[0].FieldName.Substring(0, 1) == "S")
                    {
                        gc.Visible = false;
                        if (gc.Columns[0].FieldName.Substring(0, 2) == "SB")
                        {
                            gc.Visible = true;
                        }
                        foreach (PowerSubstationLine pss in li)
                        {

                            if (gc.Columns[0].FieldName == pss.ClassType)
                            {
                                gc.Visible = true;
                                gc.Caption = pss.Title;
                                gc.Columns[0].Caption = pss.Title;
                                gc.Columns[0].Visible = true;
                                gc.Columns[0].OptionsColumn.ShowInCustomizationForm = true;

                            }
                        }
                    }
                }
                catch { }
            }
        }
        private void barEdit1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridColumn gc = this.ctrlItemPlanTable_HuaiBei1.GridView.FocusedColumn;
            if (gc == null)
                return;

            if (gc.FieldName.Substring(0, 1) != "S")
                return;

            FrmPlanTable_AddBands frm = new FrmPlanTable_AddBands();
            frm.ClassName = gc.Caption;
            frm.ClassType = gc.FieldName;
            frm.Type = type;
            frm.Flag = flag;
            frm.Type2 = "PlanTable";
            frm.IsUpdate = true;
            frm.Text = "修改分类";
            if (frm.ShowDialog() == DialogResult.OK)
            {
               // gc.Caption = frm.ClassName;
                InitValues();
            }
        }

        private void barDel1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
       {
           GridColumn gc = this.ctrlItemPlanTable_HuaiBei1.GridView.FocusedColumn;
           if (gc == null)
               return;

           if (gc.FieldName.Substring(0, 1) != "S")
           {
               MsgBox.Show("不能删除固定列");
               return;
           }
           bool bl = false;
           for (int i = 0; i < ctrlItemPlanTable_HuaiBei1.GridView.VisibleColumns.Count; i++)
            {
                if (gc.Caption == ctrlItemPlanTable_HuaiBei1.GridView.VisibleColumns[i].Caption)
                {
                    if (MsgBox.ShowYesNo("是否删除 " + gc.Caption + " 的所有数据？") != DialogResult.Yes)
                    {
                        return;
                    }
                    bl = true;
                    break;
                }
                else
                {
                    bl = false;
                }

            }
            if (bl == true)
            {
                int colIndex = ctrlItemPlanTable_HuaiBei1.GridView.FocusedColumn.VisibleIndex;

                foreach (GridBand gc1 in ctrlItemPlanTable_HuaiBei1.bandedGridView1.Bands)
                {
                    try
                    {
                        if (gc1.Columns[0].Name == gc.Name)
                        {
                            gc1.Visible = false;
                        }
                    }
                    catch { }
                }

                gc.Visible = false;
                gc.OptionsColumn.ShowInCustomizationForm = false;
                PSP_PlanTable_HuaiBei si = new PSP_PlanTable_HuaiBei();
                si.Title = gc.FieldName + "=''";
                si.Flag2 = flag;
                Itop.Client.Common.Services.BaseService.Update("UpdatePSP_PlanTable_HuaiBeiByFlag", si);

                PowerSubstationLine psl = new PowerSubstationLine();
                psl.ClassType = gc.FieldName;
                psl.Flag = flag;
                psl.Type = type;
                psl.Title = gc.Caption;
                psl.Type2 = "PlanTable";
                Itop.Client.Common.Services.BaseService.Update("DeletePowerSubstationLineByAll", psl);


                if (colIndex >= ctrlItemPlanTable_HuaiBei1.GridView.VisibleColumns.Count)
                {
                    colIndex--;
                }
                ctrlItemPlanTable_HuaiBei1.GridView.FocusedColumn = ctrlItemPlanTable_HuaiBei1.GridView.VisibleColumns[colIndex];

            }
            else
            {
                return;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gcontrol = this.ctrlItemPlanTable_HuaiBei1.GridControl;
            title = this.Text;
            unit = "";
            DialogResult = DialogResult.OK;
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.ctrlItemPlanTable_HuaiBei1.GridControl);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

                    IList<PSP_PlanTable_HuaiBei> lii = new List<PSP_PlanTable_HuaiBei>();
                    
                    DateTime dt= DateTime.Now;
                    for (int i =0; i < dts.Rows.Count; i++)
                    {
                        PSP_PlanTable_HuaiBei li = new PSP_PlanTable_HuaiBei();

                         //   li.Title = dts.Rows[i][0].ToString();
                            
                            li.Flag = typeflag;
                            li.UID = Guid.NewGuid().ToString();
                            li.CreateDate = dt.AddSeconds(i);
                            li.ParentID = ctrlItemPlanTable_HuaiBei1.FocusedObject.DY;
                           // li.S1 = dts.Rows[i][1].ToString();

                            foreach (DataColumn dc in dts.Columns)
                            {
                                columnname = dc.ColumnName;
                                if (dts.Rows[i][dc.ColumnName].ToString() == "")
                                    continue;

                                string LL2 = "";
                                try
                                {
                                    LL2 = dts.Rows[i][dc.ColumnName].ToString();
                                }
                                catch { }
                                li.GetType().GetProperty(dc.ColumnName).SetValue(li, LL2, null);
                              
                            }
                         
                            lii.Add(li);
                           
                   

                    }

                    foreach (PSP_PlanTable_HuaiBei lll in lii)
                    {
                        Services.BaseService.Create<PSP_PlanTable_HuaiBei>(lll);
                    
                    }
           
                    this.ctrlItemPlanTable_HuaiBei1.RefreshData1();


                }
            }
            catch { MessageBox.Show(columnname+"导入格式不正确！"); }
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
           for (int k = 1; k <= fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; k++)
           {
             //  dt.Columns.Add("col" + k.ToString());
              
               GridColumn gc = this.ctrlItemPlanTable_HuaiBei1.GridView.VisibleColumns[k - 1];
               dt.Columns.Add(gc.FieldName);
           }


           for (int i =3; i < fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; i++)
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

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MsgBox.ShowYesNo("该操作将清除所有数据，清除数据以后无法恢复,可能对其他用户的数据产生影响，请谨慎操作，你确定继续吗？") == DialogResult.No)
                return;

            Services.BaseService.Update("DeletePSP_PlanTable_HuaiBeiByFlag2AndParentID", typeflag);
            MsgBox.Show("清除成功！");
           // InitSodata2();
            this.ctrlItemPlanTable_HuaiBei1.RefreshData1();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //string filepath = Path.GetTempPath();
            //this.ctrlItemPlanTable_HuaiBei1.bandedGridView1.SaveLayoutToXml(filepath + "SubstationLayOut.xml");
        }



       
}

}
