using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Domain.Layouts;
using System.Collections;
using Itop.Common;
using System.Diagnostics;
using DevExpress.Utils;
using Itop.Domain.RightManager;
using Itop.Client.Base;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win;
using FarPoint.Win.Spread;
using System.IO;

namespace Itop.Client.Layouts
{
    public partial class FrmTypes_Langfang : FormBase
    {
        int i = 0;
        //Microsoft.Office.Interop.Excel.Workbook doc = dsoExcelControl1.AxFramerControl.ActiveDocument as Microsoft.Office.Interop.Excel.Workbook;
        Microsoft.Office.Interop.Excel.Application exc = new Microsoft.Office.Interop.Excel.Application(); //引用Excel对象
        string uid1 = "";
        string filename = "";
        string filepath = Path.GetTempPath();

        public string TYpe
        {
            get { return type; }
            set { type = value; }
        
        }
       
        private string type = "";
        private IList ilist = new ArrayList();
        DataTable dataTable = new DataTable();
        private bool isSelect = false;

        private bool editrights = true;
        byte[] bts=null;


        bool excelstate = false;

        string progName = "";

        public string ProgName
        {
            set { progName = value; }
        }

        public string TitleName
        {
            get
            {
                string tt = ""; 
                
                if(treeList1.FocusedNode!=null)
                    tt =treeList1.FocusedNode["Title"].ToString();
                return tt;
            }
        }

        public bool IsSelect
        {
            set { isSelect = value; }
           
        }



        public FrmTypes_Langfang()
        {
            InitializeComponent();
            
        }

        private void FrmLayoutContents_Load(object sender, EventArgs e)
        {

            InitForm();
            InitData();
      
            //fpSpread1.Sheets[0].SetColumnAllowAutoSort(-1, true);
            treeList1.MoveFirst();
            // Microsoft.Office.Interop.Excel.Application excelapp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        }

        private void InitData()
        {

            //if (!isSelect)
            //{
            //    switch (smmprog.ProgId)
            //    {
            //        case "64c9efcb-e6cc-402f-b2fc-f5f6f7d296f7":
            //            progName = "负荷特性分析";
            //            type = "fhtxfx";
            //            break;

            //        case "a50e1781-e470-4721-a6ee-c4b1294d6bd4":
            //            progName = "电网基础数据";
            //            type = "dwjcsj";
            //            break;

            //        case "3630adcc-9d4b-4059-b44e-4f88ccf76b43":
            //            progName = "电网规划基础表";
            //            type = "dwghjc";
            //            break;
            //    }


            //}

            PspType pt = new PspType();
            pt.Col1 = ProjectUID;
            ilist = Services.BaseService.GetList("SelectPspTypeList", pt);
            //ilist = Services.BaseService.GetList<PspType>();
            dataTable = DataConverter.ToDataTable(ilist, typeof(PspType));
            treeList1.DataSource = dataTable;

            Econ ed = new Econ();
            ed.UID = "Excel";
            try
            {
                bts = Services.BaseService.GetOneByKey<Econ>(ed).ExcelData;
            }
            catch { }
        
        }


        private void InitForm()
        {
            if (!isSelect)
            {
                barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                if (!EditRight)
                {
                    barEdititem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    
                    editrights = false;
                }

                if (!AddRight)
                {
                    barAdd1item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barAdditem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                if (!DeleteRight)
                {
                    barDelitem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }


                if(!(AddRight || EditRight))
                    barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                  
            }
            else
            {
                barList.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
 
            }

        }


        private void IsSave()
        {
            if (!editrights)
                return;

            if (MsgBox.ShowYesNo("数据已经发生改变，是否保存？") != DialogResult.Yes)
            {
                return;
            }


            WaitDialogForm wait = null;
            PspType obj = Services.BaseService.GetOneByKey<PspType>(uid1);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            try
            {
                wait = new WaitDialogForm("", "正在保存数据, 请稍候...");

                obj.Contents = ms.GetBuffer();
                Services.BaseService.Update("UpdatePspTypeBy", obj);
                wait.Close();
                MsgBox.Show("保存成功");
                excelstate = false;
            }
            catch
            {
                wait.Close();
                MsgBox.Show("保存失败");

            }
        }


        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

            if (excelstate)
            {
                IsSave();
            }

            if (treeList1.FocusedNode == null)
                return;

            //barCheckItem1.Checked = false;
            //barCheckItem1.Caption = "显示标题栏";
            //barCheckItem2.Checked = false;
            //barCheckItem2.Caption = "显示菜单栏";
            //barCheckItem3.Checked = false;
            //barCheckItem3.Caption = "显示工具栏";
            //barCheckItem4.Checked = false;
            //barCheckItem4.Caption = "显示公式栏";
            excelstate = false;
            string uid = treeList1.FocusedNode["UID"].ToString();
            uid1 = uid;
            PspType obj = Services.BaseService.GetOneByKey<PspType>(uid);

            this.ctrlRtfAttachFiles1.Category = uid;
            this.ctrlRtfAttachFiles1.RefreshData();

           // WaitDialogForm wait = null;
            try
            {
                //wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
                System.IO.MemoryStream ms = new System.IO.MemoryStream(obj.Contents);
                //by1 = obj.Contents;
                string str = "";
                str = filename;
                string fname = filepath + Guid.NewGuid().ToString() + ".xls";
                //string fname = filepath + "aaabbb.xls";
                filename = fname;
                dsoExcelControl1.saveStreamFile(fname, ms.ToArray());
                dsoExcelControl1.FileOpen(fname);
                //dsoExcelControl1.AxFramerControl.Titlebar = false;
                //dsoExcelControl1.Onisdispalymenubar(true);
                //dsoExcelControl1.Onisdispalytoolbar(true);
                //dsoExcelControl1.OnisdislayFormulaBar(true);
                try
                {
                    //dsoExcelControl2.FileNew();

                }
                catch { }

                if (i == 0)
                {
                    //Microsoft.Office.Interop.Excel.Workbook book = exc.Application.Workbooks.Add(fname); //引用Excel工作簿
                    //exc.Visible = false; //使Excel不可视
                    //i++;
                    
                }


                //doc.Application.DisplayFormulaBar = false;
                if (File.Exists(str))
                {
                    File.Delete(str);
                }
              
                //wait.Close();
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);//wait.Close();
            }

        }

        private void barAdditem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PspType obj = new PspType();
            obj.UID = obj.UID + "|" + ProjectUID;
            obj.Contents = bts;
            obj.ParentID = "";
            obj.CreateDate = DateTime.Now;
            obj.Col1 = ProjectUID;
            FrmPspTypeDialog dlg = new FrmPspTypeDialog();
            dlg.Text = progName;
            dlg.Object = obj;
            dlg.IsCreate = true;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            //string uid = treeList1.FocusedNode["UID"].ToString();
            //uid1 = uid;
            //PspType obj1 = Services.BaseService.GetOneByKey<PspType>(uid);

            WaitDialogForm wait = null;
            try
            {
                wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
                //System.IO.MemoryStream ms = new System.IO.MemoryStream(obj1.Contents);
                ////  by1 = obj.Contents;
                string str = "";
                str = filename;
                string fname = filepath + Guid.NewGuid().ToString() + ".xls";
                filename = fname;
                dsoExcelControl1.AxFramerControl.Save(fname, false, null, null);
                //dsoExcelControl1.FileOpen(fname);

                //dsoExcelControl1.AxFramerControl.Titlebar = false;
                //dsoExcelControl1.Onisdispalymenubar(false);
                //dsoExcelControl1.Onisdispalytoolbar(false);

                wait.Close();
            }
            catch { wait.Close(); }

            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
        }

        private void barAdd1item_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;
            string uid = treeList1.FocusedNode["UID"].ToString();

            PspType obj = new PspType();
            obj.UID = obj.UID + "|" + ProjectUID;
            obj.Contents = bts;
            obj.ParentID = uid;
            obj.CreateDate = DateTime.Now;
            obj.Col1 = ProjectUID;
            FrmPspTypeDialog dlg = new FrmPspTypeDialog();
            dlg.Text = progName;
            dlg.Object = obj;
            dlg.IsCreate = true;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            WaitDialogForm wait = null;
            try
            {
                wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
                //System.IO.MemoryStream ms = new System.IO.MemoryStream(obj1.Contents);
                ////  by1 = obj.Contents;
                string str = "";
                str = filename;
                string fname = filepath + Guid.NewGuid().ToString() + ".xls";
                filename = fname;
                dsoExcelControl1.AxFramerControl.Save(fname, false, null, null);
                //dsoExcelControl1.FileOpen(fname);

                //dsoExcelControl1.AxFramerControl.Titlebar = false;
                //dsoExcelControl1.Onisdispalymenubar(false);
                //dsoExcelControl1.Onisdispalytoolbar(false);

                wait.Close();
            }
            catch { wait.Close(); }
            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
        }

        private void barEdititem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;
            string uid = treeList1.FocusedNode["UID"].ToString();
            PspType obj = Services.BaseService.GetOneByKey<PspType>(uid);

            PspType objCopy = new PspType();
            DataConverter.CopyTo<PspType>(obj, objCopy);

            FrmPspTypeDialog dlg = new FrmPspTypeDialog();
            dlg.Text = progName;
            dlg.Object = objCopy;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DataConverter.CopyTo<PspType>(objCopy, obj);
            treeList1.FocusedNode.SetValue("Title", obj.Title);

        }

        private void barDelitem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;
            if (treeList1.FocusedNode.Nodes.Count > 0)
            {
                MsgBox.Show("有下级目录，不能删除！");
                return;
            }
            string uid = treeList1.FocusedNode["UID"].ToString();

            //请求确认
            if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
            {
                return;
            }

            //执行删除操作
            try
            {
                Services.BaseService.DeleteByKey<PspType>(uid);
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return;
            }
            this.treeList1.Nodes.Remove(this.treeList1.FocusedNode);

            if (treeList1.Nodes.Count == 0)
            {

                try
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(bts);
                  //  fpSpread1.Open(ms);
                }
                catch {}
            
            }

            InitData();
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // this.fpSpread1.Update();
            this.DialogResult = DialogResult.OK;
        }



        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
              if (treeList1.FocusedNode == null)
                return;
            string uid = treeList1.FocusedNode["UID"].ToString();
            PspType obj = Services.BaseService.GetOneByKey<PspType>(uid);
            System.IO.MemoryStream ms=new System.IO.MemoryStream();
            try
            {
                textBox1.Focus();
                dsoExcelControl1.AxFramerControl.Save(filename, true, null, null);
                obj.Contents = dsoExcelControl1.FileData1(filename);
                Services.BaseService.Update("UpdatePspTypeBy", obj);
                excelstate = false;
                MsgBox.Show("保存成功");

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }

        }



        private void FrmEconomyAnalysis_FormClosing(object sender, FormClosingEventArgs e)
        {
            treeList1.Focus();
            if (excelstate && treeList1.FocusedNode!=null)
            {
                IsSave();
            }
            excelstate = false;   
           
        }




        private void fpSpread1_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            excelstate = true;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Econ ec = new Econ();
            //ec.UID = "yyy";



            //System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //try
            //{
            //  //  fpSpread1.Save(ms, false);

            //    ec.ExcelData = ms.GetBuffer();
            //    Services.BaseService.Create<Econ>(ec);
            //    //Services.BaseService.Update<Econ>(ec);
            //}
            //catch (Exception ex) { MsgBox.Show(ex.Message); }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;
            openFileDialog1.Filter = "Microsoft Excel文件 (*.xls)|*.xls";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dsoExcelControl1.FileOpen(openFileDialog1.FileName);

                    //dsoExcelControl1.Onisdispalymenubar(false);
                    //dsoExcelControl1.Onisdispalytoolbar(false);
                    //dsoExcelControl1.AxFramerControl.Titlebar = false;

                    //fpSpread1.ActiveSheet.Columns[0, fpSpread1.ActiveSheet.Columns.Count - 1].AllowAutoFilter = true;
                    //fpSpread1.Sheets[0].SetColumnAllowAutoSort(-1, true);
                }
                catch
                {
                    MsgBox.Show("导入失败！");
                }
            }
        }


        private void Initex(FarPoint.Win.Spread.FpSpread fp)
        {
            foreach (FarPoint.Win.Spread.SheetView she in fp.Sheets)
            {
                she.ColumnCount = she.NonEmptyColumnCount;
                she.RowCount = she.NonEmptyRowCount;
                //txEdit1.ExportRangeToHTML(she, 0, 0, she.NonEmptyRowCount, she.NonEmptyColumnCount, "11", "22", 1);
            }
       
        
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            
            saveFileDialog1.FileName = treeList1.FocusedNode["Title"].ToString();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = saveFileDialog1.FileName;
                try
                {
                    dsoExcelControl1.AxFramerControl.Save(saveFileDialog1.FileName, true, null, null);
                    dsoExcelControl1.Dispose();
                    //dsoExcelControl1.AxFramerControl.Save(fname,true,null,null);
                    // dsoExcelControl1.FileSaveAs(out fname);
                    //if (MsgBox.Show("导出成功!") != DialogResult.Yes)
                    //////MsgBox.Show("导出成功!");
                    //////dsoExcelControl1.AxFramerControl.Dispose();
                    //////return;
                
                    if (MsgBox.ShowYesNo("导出成功，是否打开该文档？") != DialogResult.Yes)
                    {
                        //dsoExcelControl1.AxFramerControl.Dispose();
                        //dsoExcelControl1.Dispose();
                        Microsoft.Office.Interop.Excel.Application exc = new Microsoft.Office.Interop.Excel.Application(); //引用Excel对象
                        Microsoft.Office.Interop.Excel.Workbook book = exc.Application.Workbooks.Add(fname); //引用Excel工作簿
                        exc.Visible = true; //使Excel可视
                        return;
                    }
                    //System.Diagnostics.Process.Start(fname);

                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application(); //引用Excel对象
                    Microsoft.Office.Interop.Excel.Workbook bo = excel.Application.Workbooks.Add(fname); //引用Excel工作簿
                    excel.Visible = true; //使Excel可视




                    //  System.Diagnostics.Process.Start(fname);
                }
                catch
                {
                    MsgBox.Show("无法保存" + fname + "。请用其他文件名保存文件，或将文件存至其他位置。");
                    return;
                }
            }
        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barCheckItem1.Checked == true)
            {
                dsoExcelControl1.AxFramerControl.Titlebar = true;
                barCheckItem1.Caption = "隐藏标题栏";
            }
            else
            {
                dsoExcelControl1.AxFramerControl.Titlebar = false;
                barCheckItem1.Caption = "显示标题栏";
            }
        }

        private void barCheckItem2_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barCheckItem2.Checked == true)
            {
                dsoExcelControl1.Onisdispalymenubar(true);
                barCheckItem2.Caption = "隐藏菜单栏";
            }
            else
            {
                dsoExcelControl1.Onisdispalymenubar(false);
                barCheckItem2.Caption = "显示菜单栏";
            }
        }

        private void barCheckItem3_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barCheckItem3.Checked == true)
            {
                dsoExcelControl1.Onisdispalytoolbar(true);
                barCheckItem3.Caption = "隐藏工具栏";
            }
            else
            {
                dsoExcelControl1.Onisdispalytoolbar(false);
                barCheckItem3.Caption = "显示工具栏";
            }
        }

        private void barCheckItem4_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Workbook doc = dsoExcelControl1.AxFramerControl.ActiveDocument as Microsoft.Office.Interop.Excel.Workbook;
               
            if (barCheckItem4.Checked == true)
            {
                doc.Application.DisplayFormulaBar = true;
                barCheckItem4.Caption = "隐藏公式栏";
            }
            else
            {
                doc.Application.DisplayFormulaBar = false;
                barCheckItem4.Caption = "显示公式栏";
            }
        }


    }
}