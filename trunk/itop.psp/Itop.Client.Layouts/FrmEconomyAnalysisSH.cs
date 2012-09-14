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
using System.Reflection;
namespace Itop.Client.Layouts
{
    public partial class FrmEconomyAnalysisSH : FormBase
    {

        System.IO.MemoryStream si1 = new System.IO.MemoryStream();
        System.IO.MemoryStream si2 = new System.IO.MemoryStream();
        byte[] by1 = null;

        string uid1 = "";

        public FarPoint.Win.Spread.FpSpread FPSpread
        {
            get { return fpSpread1; }
        
        }



        private IList<EconomyAnalysis> ilist = new List<EconomyAnalysis>();
        DataTable dataTable = new DataTable();
        private bool isSelect = false;
        private byte[] txtByte = null;

        private int nianshu=0;
        private int kaishinian = 0;
        private int xiangmujisuannian = 0;
        private int lixinian = 0;

        private int xiangmunian1 = 0;
        private int lixinian1 = 0;
        private int nianshu1 = 0;
        private IList<EconomyData> eds = new List<EconomyData>();

        private bool editrights = true;

        bool isloadstate = false;
        byte[] bts=null;


        bool excelstate = false;



        FarPoint.Win.Spread.CellType.NumberCellType numberCellTypes= new FarPoint.Win.Spread.CellType.NumberCellType();




        FarPoint.Win.Spread.CellType.NumberCellType numberCellTypes1 = new FarPoint.Win.Spread.CellType.NumberCellType();
        FarPoint.Win.Spread.CellType.NumberCellType numberCellTypes2 = new FarPoint.Win.Spread.CellType.NumberCellType();
        FarPoint.Win.Spread.CellType.NumberCellType numberCellTypes3 = new FarPoint.Win.Spread.CellType.NumberCellType();
        FarPoint.Win.LineBorder lineBorder = new FarPoint.Win.LineBorder(Color.Black, 1);
        FarPoint.Win.LineBorder lineBorder1 = new FarPoint.Win.LineBorder(Color.White, 0);  
        

        public byte[] TxtByte
        {
            set { txtByte = value; }
            get { return txtByte; }
        }

        public bool IsSelect
        {
            set { isSelect = value; }
           
        }



        public FrmEconomyAnalysisSH()
        {
            InitializeComponent();
            
        }

        private void FrmLayoutContents_Load(object sender, EventArgs e)
        {
            numberCellTypes2.DecimalPlaces = 0;
            //numberCellTypes2.FixedPoint = true;
            numberCellTypes3.DecimalPlaces = 1;

            numberCellTypes1.DecimalPlaces = 3;
            numberCellTypes1.FixedPoint = true;

            barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            Econ ed=new Econ();
                        ed.UID="xxx";
                        bts = Services.BaseService.GetOneByKey<Econ>(ed).ExcelData;
            //isload = 0;

            InitForm();
            InitData();

            

            isloadstate = true;
            treeList1.ExpandAll();
            treeList1.MoveFirst();
            
        }

        private void InitData()
        {
            ilist = Services.BaseService.GetList<EconomyAnalysis>("SelectEconomyAnalysisByWhere"," UID like '%"+ProjectUID+"%'");
            if (ilist.Count == 0)
            {
                EconomyAnalysis obj = new EconomyAnalysis();
                obj.UID = Guid.NewGuid().ToString() + "|" + ProjectUID;
                obj.Title="供电公司";
                if(bts.Length>0)
                obj.Contents = bts;
                obj.ParentID = "0";
                obj.CreateDate = DateTime.Now;
                Services.BaseService.Create<EconomyAnalysis>(obj);
                ilist.Add(obj);
            
            }


            dataTable = DataConverter.ToDataTable((IList)ilist, typeof(EconomyAnalysis));
            treeList1.DataSource = dataTable;
        
        }


        private void InitForm()
        {
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet2.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet3.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet4.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet5.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet6.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet7.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet8.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.fpSpread1_Sheet9.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;

            if (!isSelect)
            {
                barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                if (!EditRight)
                {
                    barEdititem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barCS.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
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

                

                //if (!PrintRight)
                //{
                //    barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
              //  }

                //barSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            else
            {
                VsmdgroupProg vp = new VsmdgroupProg();
                try
                {
                    vp = MIS.GetProgRight("a5ac2d77-e60e-4253-ae10-4a17abf5a89b", "", MIS.UserNumber);
                }
                catch { }
                if (vp.upd == "0")
                {
                    barEdititem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    editrights = false; ;
                }

                if (vp.ins == "0")
                {
                    barAdd1item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barAdditem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                if (vp.del == "0")
                    barDelitem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;



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
            EconomyAnalysis obj = Services.BaseService.GetOneByKey<EconomyAnalysis>(uid1);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            try
            {
                wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
                fpSpread1.Save(ms, false);

                obj.Contents = ms.GetBuffer();
                Services.BaseService.Update("UpdateEconomyAnalysisByContents", obj);
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
            InitData1();
            fpSpread1.Sheets[7].Cells[14, 1].Text = "电量不含税加价(还贷期间）";
            fpSpread1.Sheets[7].Cells[15, 1].Text = "电量不含税加价(还贷后）";
            

            

        }

    


        private void barAdditem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ////////EconomyAnalysis obj = new EconomyAnalysis();
            ////////obj.Contents = bts;
            ////////obj.ParentID = "0";
            ////////obj.CreateDate = DateTime.Now;


            if (treeList1.FocusedNode == null)
                return;
            string uid = treeList1.FocusedNode["UID"].ToString();


            EconomyAnalysis obj = new EconomyAnalysis();
            obj.UID = Guid.NewGuid().ToString() + "|" + ProjectUID;
            obj.Contents = bts;
            obj.ParentID = uid;
            obj.CreateDate = DateTime.Now;


            FrmEconomyAnalysisDialog dlg = new FrmEconomyAnalysisDialog();
            dlg.Object = obj;
            dlg.IsCreate = true;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
            MsgTitle();

            fpSpread1.Sheets[7].Cells[14, 1].Text = "电量不含税加价(还贷期间）";
            fpSpread1.Sheets[7].Cells[15, 1].Text = "电量不含税加价(还贷后）";

        }

        private void barAdd1item_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;
            string uid = treeList1.FocusedNode["UID"].ToString();


            EconomyAnalysis obj = new EconomyAnalysis();
            obj.UID = Guid.NewGuid().ToString() + "|" + ProjectUID;
            obj.Contents = bts;
            obj.ParentID = uid;
            obj.CreateDate = DateTime.Now;
            FrmEconomyAnalysisDialog dlg = new FrmEconomyAnalysisDialog();
            dlg.Object = obj;
            dlg.IsCreate = true;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
            //InitData1();
            MsgTitle();
            fpSpread1.Sheets[7].Cells[14, 1].Text = "电量不含税加价(还贷期间）";
            fpSpread1.Sheets[7].Cells[15, 1].Text = "电量不含税加价(还贷后）";
        }


        private void MsgTitle()
        {
            if (MsgBox.ShowYesNo("是否进入参数设置") == DialogResult.Yes)
            {
                EditData();
            
            }
        
        
        }



        private void barEdititem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;

            if (treeList1.FocusedNode.ParentNode == null)
                return;

            string uid = treeList1.FocusedNode["UID"].ToString();
            EconomyAnalysis obj = Services.BaseService.GetOneByKey<EconomyAnalysis>(uid);

            EconomyAnalysis objCopy = new EconomyAnalysis();
            DataConverter.CopyTo<EconomyAnalysis>(obj, objCopy);

            FrmEconomyAnalysisDialog dlg = new FrmEconomyAnalysisDialog();
            dlg.Object = objCopy;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DataConverter.CopyTo<EconomyAnalysis>(objCopy, obj);
            treeList1.FocusedNode.SetValue("Title", obj.Title);

        }

        private void barDelitem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;

            if (treeList1.FocusedNode.ParentNode == null)
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
                Services.BaseService.DeleteByKey<EconomyAnalysis>(uid);
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return;
            }
            this.treeList1.Nodes.Remove(this.treeList1.FocusedNode);
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }




        private void SetEconomyDatalist(IList<EconomyData> ed)
        {
            try
            {

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }

        private IList<EconomyData> GetEconomyDatalist(IList<EconomyData> ed)
        {
            try
            {
                
            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
            return ed;
        }




        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {



            
              if (treeList1.FocusedNode == null)
                return;
            string uid = treeList1.FocusedNode["UID"].ToString();
            EconomyAnalysis obj = Services.BaseService.GetOneByKey<EconomyAnalysis>(uid);
            System.IO.MemoryStream ms=new System.IO.MemoryStream();
            WaitDialogForm wait = null;
            try
            {
                wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
                textBox1.Focus();
                fpSpread1.Update();
                fpSpread1.Save(ms, false);

                obj.Contents = ms.GetBuffer();
                Services.BaseService.Update("UpdateEconomyAnalysisByContents", obj);
                excelstate = false;
                wait.Close();
                MsgBox.Show("保存成功");

                
            }
            catch (Exception ex) { wait.Close();
                MsgBox.Show("保存失败");}
        }




        private void barCS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            EditData();
            



        }


        private void EditData()
        {
            nianshu1 = int.Parse(fpSpread1.Sheets[8].GetValue(10, 3).ToString());
            FrmEconomyGuideSH feg = new FrmEconomyGuideSH();
            feg.ProjectID = ProjectUID;
            if (nianshu1 == 0)
            {
                feg.ObjectList = new List<EconomyData>();
            }
            else
            {
                IList<EconomyData> i1 = new List<EconomyData>();
                for (int q = 0; q < nianshu1; q++)
                {
                    EconomyData ed11 = new EconomyData();
                    try
                    {
                        ed11.S1 = (int)fpSpread1.Sheets[8].GetValue(15, 1 + q);
                    }
                    catch { }
                    try
                    {
                        ed11.S2 = Convert.ToDouble(fpSpread1.Sheets[8].GetValue(16, 1 + q).ToString());
                    }
                    catch { }

                    try
                    {
                        ed11.S3 = Convert.ToDouble(fpSpread1.Sheets[8].GetValue(17, 1 + q).ToString());
                    }
                    catch { }
                    i1.Add(ed11);
                       

                }
                feg.ObjectList = i1;

            }

            if (xiangmunian1 == 0)
            {
                feg.ObjectList1 = new List<EconomyData>();
            }
            else
            {
                IList<EconomyData> i2 = new List<EconomyData>();
                for (int w = 0; w < xiangmunian1; w++)
                {
                    EconomyData ed22 = new EconomyData();
                    try
                    {
                        ed22.S1 = kaishinian + w;
                    }
                    catch { }
                    try
                    {
                        ed22.S2 = (double)fpSpread1.Sheets[3].GetValue(3, 2 + w);
                    }
                    catch { }
                    try
                    {
                        ed22.S3 = Convert.ToDouble(fpSpread1.Sheets[8].GetValue(18, 1 + w).ToString());
                    }
                    catch { }
                    i2.Add(ed22);


                }
                feg.ObjectList1 = i2;
            }


            try
            {
                feg.S1 = int.Parse(fpSpread1.Sheets[8].GetValue(9, 3).ToString());
            }
            catch
            { }

            try
            {
                feg.S2 = int.Parse(fpSpread1.Sheets[8].GetValue(10, 3).ToString());
            }
            catch
            { }

            try
            {
                feg.S3 = int.Parse(fpSpread1.Sheets[8].GetValue(2, 1).ToString());
            }
            catch
            { }

            try
            {
                feg.S4 = int.Parse(fpSpread1.Sheets[8].GetValue(13, 3).ToString());
            }
            catch
            { }

            try
            {
                feg.S5 = double.Parse(fpSpread1.Sheets[8].GetValue(3, 1).ToString());
            }
            catch
            { }

            try
            {
                feg.S6 = double.Parse(fpSpread1.Sheets[8].GetValue(12, 3).ToString());
            }
            catch
            { }

            try
            {
                feg.S7 = double.Parse(fpSpread1.Sheets[8].GetValue(8, 3).ToString());
            }
            catch
            { }

            try
            {
                feg.S8 = double.Parse(fpSpread1.Sheets[8].GetValue(4, 1).ToString());
            }
            catch
            { }

            try
            {
                feg.S9 = double.Parse(fpSpread1.Sheets[8].GetValue(8, 1).ToString());
            }
            catch
            { }


            try
            {
                feg.S10 = double.Parse(fpSpread1.Sheets[8].GetValue(9, 1).ToString());
            }
            catch
            { }

            try
            {
                feg.S11 = double.Parse(fpSpread1.Sheets[8].GetValue(10, 1).ToString());
            }
            catch
            { }



            try
            {
                feg.S12 = double.Parse(fpSpread1.Sheets[8].GetValue(14, 3).ToString());
            }
            catch
            { }


            try
            {
                feg.S13 = int.Parse(fpSpread1.Sheets[8].GetValue(14, 1).ToString());
            }
            catch
            { }


            try
            {
                feg.S14 = double.Parse(fpSpread1.Sheets[8].GetValue(5, 1).ToString());
            }
            catch
            { }


            try
            {
                feg.S15 = double.Parse(fpSpread1.Sheets[8].GetValue(6, 1).ToString());
            }
            catch
            { }


            try
            {
                feg.S16 = double.Parse(fpSpread1.Sheets[8].GetValue(7, 1).ToString());
            }
            catch
            { }


            try
            {
                feg.S17 = double.Parse(fpSpread1.Sheets[8].GetValue(11, 1).ToString());
            }
            catch
            { }

            try
            {
                feg.S18 = double.Parse(fpSpread1.Sheets[8].GetValue(12, 1).ToString());
            }
            catch
            { }

            try
            {
                feg.S19 = double.Parse(fpSpread1.Sheets[8].GetValue(13, 1).ToString());
            }
            catch
            { }

            try
            {
                feg.S20 = double.Parse(fpSpread1.Sheets[8].GetValue(2, 3).ToString());
            }
            catch
            { }

            try
            {
                feg.S21 = double.Parse(fpSpread1.Sheets[8].GetValue(3, 3).ToString());
            }
            catch
            { }

            try
            {
                feg.S22 = double.Parse(fpSpread1.Sheets[8].GetValue(4, 3).ToString());
            }
            catch
            { }

            try
            {
                feg.S23 = double.Parse(fpSpread1.Sheets[8].GetValue(5, 3).ToString());
            }
            catch
            { }

            try
            {
                feg.S24 = double.Parse(fpSpread1.Sheets[8].GetValue(6, 3).ToString());
            }
            catch
            { }

            try
            {
                feg.S25 = double.Parse(fpSpread1.Sheets[8].GetValue(7, 3).ToString());
            }
            catch
            { }

            try
            {
                feg.S26 = double.Parse(fpSpread1.Sheets[8].GetValue(11, 3).ToString());
            }
            catch
            { }











            try
            {
                feg.S31 = double.Parse(fpSpread1.Sheets[8].GetValue(19, 1).ToString());
            }
            catch
            { }



            try
            {
                feg.S32 = double.Parse(fpSpread1.Sheets[8].GetValue(20, 1).ToString());
            }
            catch
            { }


            try
            {
                feg.S33 = double.Parse(fpSpread1.Sheets[8].GetValue(21, 1).ToString());
            }
            catch
            { }


            try
            {
                feg.S34 = double.Parse(fpSpread1.Sheets[8].GetValue(22, 1).ToString());
            }
            catch
            { }


            try
            {
                feg.S35 = double.Parse(fpSpread1.Sheets[8].GetValue(19, 3).ToString());
            }
            catch
            { }


            try
            {
                feg.S36 = double.Parse(fpSpread1.Sheets[8].GetValue(20, 3).ToString());
            }
            catch
            { }


            try
            {
                feg.S37 = double.Parse(fpSpread1.Sheets[8].GetValue(21, 3).ToString());
            }
            catch
            { }

            try
            {
                feg.S38 = double.Parse(fpSpread1.Sheets[8].GetValue(22, 3).ToString());
            }
            catch
            { }


            try
            {
                feg.S39 = double.Parse(fpSpread1.Sheets[8].GetValue(23, 1).ToString());
            }
            catch
            { }



            try
            {
                feg.S40 = double.Parse(fpSpread1.Sheets[8].GetValue(23, 3).ToString());
            }
            catch
            { }













            fpSpread1.Sheets[7].Cells[14, 1].Text = "电量不含税加价(还贷期间）";
            fpSpread1.Sheets[7].Cells[15, 1].Text = "电量不含税加价(还贷后）";


            try
            {
                fpSpread1.Sheets[7].Cells[16, 1].Text = "单位电量输配电成本（不含网费，不含税）";
                fpSpread1.Sheets[7].Cells[17, 1].Text = "单位电量输配电成本（不含网费，含税）";
                fpSpread1.Sheets[7].Cells[18, 1].Text = "单位电量输配电成本（含网费，含税）";
            }
            catch { }





            if (feg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(bts);
                    fpSpread1.Open(ms);
                }
                catch{}


                fpSpread1.Sheets[8].SetValue(9, 3, feg.S1);
                fpSpread1.Sheets[8].SetValue(10, 3, feg.S2);
                fpSpread1.Sheets[8].SetValue(2, 1, feg.S3);
                fpSpread1.Sheets[8].SetValue(3, 1, feg.S5);
                fpSpread1.Sheets[8].SetValue(13, 3, feg.S4);
                fpSpread1.Sheets[8].SetValue(12, 3, feg.S6);
                fpSpread1.Sheets[8].SetValue(8, 3, feg.S7);
                fpSpread1.Sheets[8].SetValue(4, 1, feg.S8);
                fpSpread1.Sheets[8].SetValue(8, 1, feg.S9);
                fpSpread1.Sheets[8].SetValue(9, 1, feg.S10);
                fpSpread1.Sheets[8].SetValue(10, 1, feg.S11);
                fpSpread1.Sheets[8].SetValue(14, 3, feg.S12);
                fpSpread1.Sheets[8].SetValue(14, 1, feg.S13);
                fpSpread1.Sheets[8].SetValue(5, 1, feg.S14);
                fpSpread1.Sheets[8].SetValue(6, 1, feg.S15);
                fpSpread1.Sheets[8].SetValue(7, 1, feg.S16);
                fpSpread1.Sheets[8].SetValue(11, 1, feg.S17);
                fpSpread1.Sheets[8].SetValue(12, 1, feg.S18);
                fpSpread1.Sheets[8].SetValue(13, 1, feg.S19);
                fpSpread1.Sheets[8].SetValue(2, 3, feg.S20);
                fpSpread1.Sheets[8].SetValue(3, 3, feg.S21);
                fpSpread1.Sheets[8].SetValue(4, 3, feg.S22);
                fpSpread1.Sheets[8].SetValue(5, 3, feg.S23);
                fpSpread1.Sheets[8].SetValue(6, 3, feg.S24);
                fpSpread1.Sheets[8].SetValue(7, 3, feg.S25);
                fpSpread1.Sheets[8].SetValue(11, 3, feg.S26);





                try
                {
                    fpSpread1.Sheets[8].SetValue(19, 1, feg.S31);
                    fpSpread1.Sheets[8].SetValue(20, 1, feg.S32);
                    fpSpread1.Sheets[8].SetValue(21, 1, feg.S33);
                    fpSpread1.Sheets[8].SetValue(22, 1, feg.S34);
                    fpSpread1.Sheets[8].SetValue(19, 3, feg.S35);
                    fpSpread1.Sheets[8].SetValue(20, 3, feg.S36);
                    fpSpread1.Sheets[8].SetValue(21, 3, feg.S37);
                    fpSpread1.Sheets[8].SetValue(22, 3, feg.S38);
                    fpSpread1.Sheets[8].SetValue(23, 1, feg.S39);
                    fpSpread1.Sheets[8].SetValue(23, 3, feg.S40);
                }
                catch { }









                IList<EconomyData> li = new List<EconomyData>();
                li = feg.ObjectList;

                InitList(li);
                InitList1(feg.ObjectList1);
                InitCompute();

                InitCom1();
                InitCom2();

                InitList1(feg.ObjectList1);


                InitComputeFY();

            }
        
        }



        private void InitList(IList<EconomyData> li)
        {
            int syear = li.Count;
            if (syear >= 4)
            {
                fpSpread1.Sheets[8].ColumnCount = syear + 1;
            }
            else
            {
                fpSpread1.Sheets[8].ColumnCount = 4;
            }
            fpSpread1.Sheets[8].Cells[15, 1, 18, syear].Border = lineBorder;

            int j = 0;
            foreach (EconomyData ed in li)
            {
                try
                {
                    fpSpread1.Sheets[8].SetValue(15, 1 + j, ed.S1);
                }
                catch { }
                try
                {
                    fpSpread1.Sheets[8].SetValue(16, 1 + j, ed.S2);
                }
                catch { }

                try
                {
                    fpSpread1.Sheets[8].SetValue(17, 1 + j, ed.S3);
                }
                catch { }
                j++;
            }
        
        
        }



        private void InitList1(IList<EconomyData> li)
        {
            int u = 2;
            int syear = li.Count;
            if (syear >= 4)
            {
                fpSpread1.Sheets[8].ColumnCount = syear + 1;
            }
            else
            {
                fpSpread1.Sheets[8].ColumnCount = 4;
            }
            fpSpread1.Sheets[8].Cells[15, 1, 18, syear].Border = lineBorder;


            fpSpread1.Sheets[3].Cells.Get(3, 2, 3, 1+li.Count).CellType = null;
            foreach (EconomyData ed in li)
            {
                fpSpread1.Sheets[3].Cells[3, u].Value = ed.S2;
                //fpSpread1.Sheets[3].SetValue(3, u, ed.S2);
                //fpSpread1.Sheets[3].Cells.Get(3, u).CellType = numberCellTypes2;

                fpSpread1.Sheets[8].SetValue(18,  u-1, ed.S3);
                u++;
            }

            fpSpread1.Sheets[3].Cells.Get(3, 2, 3, 1 + li.Count).CellType = numberCellTypes2;




        }


        private void SetEx8()
        {


            fpSpread1.Sheets[7].Cells[3, 3].Formula = "筹措表!R5C" + (3 + nianshu).ToString();
            fpSpread1.Sheets[7].Cells[4, 3].Formula = "筹措表!R6C" + (3 + nianshu).ToString();
            fpSpread1.Sheets[7].Cells[5, 3].Formula = "筹措表!R7C" + (3 + nianshu).ToString();
            fpSpread1.Sheets[7].Cells[6, 3].Formula = "全投资!R20C2";
            fpSpread1.Sheets[7].Cells[7, 3].Formula = "全投资!R20C5";
            fpSpread1.Sheets[7].Cells[8, 3].Formula = "全投资!R20C9";
            fpSpread1.Sheets[7].Cells[9, 3].Formula = "资本金!R20C2";
            fpSpread1.Sheets[7].Cells[10, 3].Formula = "资本金!R20C5";
            fpSpread1.Sheets[7].Cells[11, 3].Formula = "AVERAGE(损益表!R9C" + (3 + nianshu).ToString() + ":R9C" + (2 + xiangmujisuannian).ToString() + ")/筹措表!R7C" + (3 + nianshu).ToString();
            fpSpread1.Sheets[7].Cells[12, 3].Formula = "(AVERAGE(损益表!R4C" + (3 + nianshu).ToString() + ":R4C" + (2 + xiangmujisuannian).ToString() + ")-AVERAGE(损益表!R8C" + (3 + nianshu).ToString() + ":R8C" + (2 + xiangmujisuannian).ToString() + "))/筹措表!R7C" + (3 + nianshu).ToString();
            fpSpread1.Sheets[7].Cells[13, 3].Formula = "(AVERAGE(损益表!R9C" + (3 + nianshu).ToString() + ":R9C" + (2 + xiangmujisuannian).ToString() + ")-AVERAGE(损益表!R10C" + (3 + nianshu).ToString() + ":R10C" + (2 + xiangmujisuannian).ToString() + "))/筹措表!R11C" + (3 + nianshu).ToString();
            fpSpread1.Sheets[7].Cells[14, 3].Formula = "AVERAGE(损益表!R5C3:R5C" + (2 + nianshu+lixinian).ToString() + ")";


            if(xiangmujisuannian>nianshu +lixinian)
            fpSpread1.Sheets[7].Cells[15, 3].Formula = "AVERAGE(损益表!R5C" + (nianshu + 3+lixinian).ToString() + ":R5C" + (2 + xiangmujisuannian).ToString() + ")";
            if(xiangmujisuannian==nianshu +lixinian)
                fpSpread1.Sheets[7].Cells[15, 3].Formula = "AVERAGE(损益表!R5C" + (2 + xiangmujisuannian).ToString() + ")";
            //fpSpread1.Sheets[7].Cells[3,3,15,3].Column.Width=.Get(3, 3, 15, 3)..CellType = numberCellTypes;
        }
        private void SetEx7()
        {
            fpSpread1.Sheets[6].ColumnCount = 300;

            try{
            fpSpread1.Sheets[6].Cells[2, 2, 12, 1+xiangmunian1].Border = lineBorder1;
            fpSpread1.Sheets[6].Cells[2, 2, 12, 1 + xiangmunian1].ResetText();
            }
            catch
            { }
            int listmax = 0;
            try
            {
                for (int k = 2; k < 2 + xiangmujisuannian; k++)
                {
                    #region 项目年度
                    fpSpread1.Sheets[6].SetValue(2, k, kaishinian + k - 2);
                    #endregion


                    #region 销售收入
                    if (k > 2)
                        fpSpread1.Sheets[6].Cells[3, k].Formula = "SUM(R7C" + (k + 1).ToString() + ":R9C" + (k + 1).ToString() + ")";
                    #endregion 
 

                    #region 电量加价(不含税）
                    if (k > 2)
                        fpSpread1.Sheets[6].Cells[4, k].Formula = "R4C" + (k + 1).ToString() + "/R6C" + (k + 1).ToString();
                    #endregion 


                    #region 售电量
                        fpSpread1.Sheets[6].Cells[5, k].Formula = "成本费用表!R4C" + (k + 1).ToString();
                    #endregion 
 

                    #region 销售税金及附加
                        if (k > 2)
                        fpSpread1.Sheets[6].Cells[6, k].Formula = "SUM(R8C" + (k + 1).ToString() + ":R9C" + (k + 1).ToString() + ")*0.017/0.983";
                    #endregion  


                    
                    #region 总成本费用
                    if (k > 2)
                        fpSpread1.Sheets[6].Cells[7, k].Formula = "成本费用表!R9C" + (k + 1).ToString();
                    #endregion  


                    #region 销售利润
                    if (k > 2)
                        fpSpread1.Sheets[6].Cells[8, k].Formula = "SUM(R12C" + (k + 1).ToString()+":R13C" + (k + 1).ToString()+")/((1-基本数据!R10C2)*(1-基本数据!R9C2))";
                    #endregion  


                    #region 所得税
                    if (k > 2)
                        fpSpread1.Sheets[6].Cells[9, k].Formula = "R9C" + (k + 1).ToString() + "*参数!R9C2";
                    #endregion  

                    #region 公积金、公益金
                    if (k > 2)
                        fpSpread1.Sheets[6].Cells[10, k].Formula = "(R9C" + (k + 1).ToString() + "-R10C" + (k + 1).ToString() + ")*参数!R8C2";
                    #endregion  


                    #region 偿还建设投资借款
                    if (k > 2 && k < 1 + nianshu+lixinian)
                        fpSpread1.Sheets[6].Cells[11, k].Formula = "还本付息表!R"+(10+nianshu).ToString()+"C" + (k).ToString();

                    if (k >= 1 + nianshu + lixinian)
                        fpSpread1.Sheets[6].Cells[11, k].Formula = "0";
                    #endregion  


                    #region 资本金分利
                    if (k > 2)
                        fpSpread1.Sheets[6].Cells[12, k].Formula = "资本金!R6C" + (k + 1).ToString();
                    #endregion  


                    listmax = k;

                }


            fpSpread1.Sheets[6].Cells.Get(3, 2, 12, listmax).CellType = numberCellTypes;
            fpSpread1.Sheets[6].Cells.Get(4, 2, 4, listmax).CellType = numberCellTypes1;

            fpSpread1.Sheets[6].Cells[2, 0, 12, listmax].Border = lineBorder;

            fpSpread1.Sheets[6].Cells[3, 2, 12, listmax].Column.Width = 100;////
            fpSpread1.Sheets[6].Cells.Get(0, 0).ColumnSpan = listmax + 1;

            fpSpread1.Sheets[6].ColumnCount = listmax + 1;

        }
        catch (Exception ex) { MsgBox.Show(ex.Message); }
        }
        private void SetEx6()
        {
            fpSpread1.Sheets[5].ColumnCount = 300;

            try{
            fpSpread1.Sheets[5].Cells[2, 2, 17, 1 + xiangmunian1].Border = lineBorder1;
            fpSpread1.Sheets[5].Cells[2, 2, 17, 1 + xiangmunian1].ResetText();
            }
            catch
            { }
            int listmax = 0;
            try
            {
                for (int k = 2; k < 2 + xiangmujisuannian; k++)
                {
                    #region 项目年度
                    fpSpread1.Sheets[5].SetValue(2, k, kaishinian + k - 2);
                    #endregion

                    #region 现金流入
                    if (k>2)
                        fpSpread1.Sheets[5].Cells[4, k].Formula = "SUM(R6C" + (k + 1).ToString() + ":R10C" + (k + 1).ToString() + ")";
                    #endregion  

                    #region 资本金分利
                    if (k == 3)
                        fpSpread1.Sheets[5].Cells[5, k].Formula = "筹措表!R11C" + (k).ToString() + "*基本数据!R12C2";

                    if (k > 3 && k<=2+nianshu)
                        fpSpread1.Sheets[5].Cells[5, k].Formula = "SUM(筹措表!R11C3:R11C" + (k).ToString() + ")*基本数据!R12C2";//"筹措表!R11C" + (k).ToString() + "*基本数据!R12C2+R6C"+(k).ToString();
                    if (k > 2 + nianshu)
                        fpSpread1.Sheets[5].Cells[5, k].Formula = "R6C"+k.ToString();
                        //fpSpread1.Sheets[5].Cells[5, k].Formula = "R6C" + (k).ToString();

                    #endregion 


                    #region 回收1，2，3，4
                    if (k == 1 + xiangmujisuannian)
                    {
                        fpSpread1.Sheets[5].Cells[6, k].Formula = "筹措表!R13C" + (eds.Count + 3).ToString();
                        fpSpread1.Sheets[5].Cells[7, k].Formula = "筹措表!R8C" + (eds.Count + 3).ToString()+"*基本数据!R7C2";

                        if(xiangmujisuannian>nianshu+lixinian)
                        fpSpread1.Sheets[5].Cells[8, k].Formula = "SUM(还本付息表!R" + (8 + nianshu).ToString() + "C3:R" + (8 + nianshu).ToString() + "C" + (lixinian + nianshu + 1).ToString() + ")-SUM(还本付息表!R" + (9 + nianshu).ToString() + "C3:R" + (9 + nianshu).ToString() + "C" + (lixinian + nianshu + 1).ToString() + ")+SUM(成本费用表!R6C" + (3 + nianshu + lixinian).ToString() + ":R6C" + (2 + xiangmujisuannian).ToString() + ")";

                    if (xiangmujisuannian == nianshu + lixinian)
                        fpSpread1.Sheets[5].Cells[8, k].Formula = "SUM(还本付息表!R" + (8 + nianshu).ToString() + "C3:R" + (8 + nianshu).ToString() + "C" + (lixinian + nianshu + 1).ToString() + ")-SUM(还本付息表!R" + (9 + nianshu).ToString() + "C3:R" + (9 + nianshu).ToString() + "C" + (lixinian + nianshu + 1).ToString() + ")+R6C" + (2 + xiangmujisuannian).ToString();
                        fpSpread1.Sheets[5].Cells[9, k].Formula = "SUM(损益表!R11C4:R11C"+(2+xiangmujisuannian).ToString()+")*2/3";//"筹措表!R13C" + (eds.Count + 3).ToString();

                    }
                    #endregion 








                    #region 现金流出
                    if(k<2+nianshu)
                    fpSpread1.Sheets[5].Cells[10, k].Formula = "SUM(R12C" + (k + 1).ToString() + ":R13C" + (k + 1).ToString() + ")";
                    #endregion  

                    #region 建设投资
                if (k < 2 + nianshu)
                    fpSpread1.Sheets[5].Cells[11, k].Formula = "筹措表!R11C" + (k + 1).ToString();
                    #endregion 

                    #region 流动资金
                if (k < 2 + nianshu)
                    fpSpread1.Sheets[5].Cells[12, k].Formula = "筹措表!R13C" + (k + 1).ToString();
                    #endregion 



                    #region 净现金流量
                    fpSpread1.Sheets[5].Cells[14, k].Formula = "R5C" + (k + 1).ToString() + "-R11C" + (k + 1).ToString();
                    #endregion  


                    #region 累计净现金流量
                    if (k == 2)
                        fpSpread1.Sheets[5].Cells[15, k].Formula = "R15C" + (k + 1).ToString();
                    if (k > 2)
                        fpSpread1.Sheets[5].Cells[15, k].Formula = "R15C" + (k + 1).ToString() + "+R16C" + k.ToString();
                    #endregion  


                    #region 净现金流量现值
                    if (k == 2)
                        fpSpread1.Sheets[5].Cells[16, k].Formula = "NPV(参数!R15C4,R15C" + (k + 1).ToString() + ")";
                    if (k > 2)
                        fpSpread1.Sheets[5].Cells[16, k].Formula = "NPV(参数!R15C4,R15C3:R15C" + (k + 1).ToString() + ")-R18C" + k.ToString();
                    //fpSpread1_Sheet5.Cells[17, k].Formula = "NPV(8%,$R16C4:R16C" + (k + 1).ToString() + ")-R19C" + k.ToString();
                    #endregion  


                    #region 净现金流量现值累计
                    if (k == 2)
                        fpSpread1.Sheets[5].Cells[17, k].Formula = "R17C" + (k + 1).ToString();
                    if (k > 2)
                        fpSpread1.Sheets[5].Cells[17, k].Formula = "R17C" + (k + 1).ToString() + "+R18C" + k.ToString();
                    #endregion  

                    listmax = k;
                }



            #region 最底下
                fpSpread1.Sheets[5].Cells[19, 1].Formula = "IRR(R15C3:R15C" + (3 + listmax).ToString() + ")"; ;
                fpSpread1.Sheets[5].Cells[19, 4].Formula = "R18C" + (2 + xiangmujisuannian).ToString();
                fpSpread1.Sheets[5].Cells[19, 8].Formula = "";


            #endregion


            fpSpread1.Sheets[5].Cells.Get(4, 2, 17, listmax).CellType = numberCellTypes;

            fpSpread1.Sheets[5].Cells[4, 2, 17, listmax].Column.Width = 100;////


            fpSpread1.Sheets[5].Cells[2, 0, 17, listmax].Border = lineBorder;
            fpSpread1.Sheets[5].Cells.Get(0, 0).ColumnSpan = listmax + 1;

            fpSpread1.Sheets[5].ColumnCount = listmax + 1;
        }
        catch (Exception ex) { MsgBox.Show(ex.Message); }
        }
        private void SetEx5()
        {
            fpSpread1.Sheets[4].ColumnCount = 300;

            try 
	            {	        

            fpSpread1.Sheets[4].Cells[2, 2, 17, 1 + xiangmunian1].Border = lineBorder1;
            fpSpread1.Sheets[4].Cells[2, 2, 17, 1 + xiangmunian1].ResetText();

            }
            catch
            { }
            int listmax = 0;

            int g1 = 0;
            int g2 = 0;
            int g3 = 0;

            try
            {
                for (int k = 2; k < 2 + xiangmujisuannian; k++)
                {
                    #region 项目年度
                    fpSpread1.Sheets[4].SetValue(2, k, kaishinian + k - 2);
                    #endregion

                    #region 现金流入
                    fpSpread1.Sheets[4].Cells[4, k].Formula = "SUM(R6C" + (k + 1).ToString() + ":R8C" + (k + 1).ToString() + ")";
                    #endregion

                    #region 销售加价收入
                    fpSpread1.Sheets[4].Cells[5, k].Formula = "损益表!R4C" + (k + 1).ToString();
                    #endregion

                    #region ----
                    if(k==1 + xiangmujisuannian)
                        fpSpread1.Sheets[4].Cells[6, k].Formula = "筹措表!R13C" + (nianshu + 3).ToString();
                    #endregion

                    #region ----
                    if (k == 1 + xiangmujisuannian)
                        fpSpread1.Sheets[4].Cells[7, k].Formula = "筹措表!R8C" + (nianshu + 3).ToString() + "*参数!R6C2";
                    #endregion

                    #region 现金流出
                    fpSpread1.Sheets[4].Cells[8, k].Formula = "SUM(R10C" + (k + 1).ToString() + ":R14C" + (k + 1).ToString() + ")";
                    #endregion

                    #region 建设投资
                    if(k<2+nianshu)
                    fpSpread1.Sheets[4].Cells[9, k].Formula = "筹措表!R8C" + (k + 1).ToString();
                    #endregion

                    #region 流动资金
                    if (k < 2 + nianshu)
                    fpSpread1.Sheets[4].Cells[10, k].Formula = "筹措表!R13C" + (k + 1).ToString();
                    #endregion

                    if (k > 2)
                    {
                        #region 经营成本
                        fpSpread1.Sheets[4].Cells[11, k].Formula = "成本费用表!R5C" + (k + 1).ToString();
                        #endregion

                        #region 销售税金
                        fpSpread1.Sheets[4].Cells[12, k].Formula = "损益表!R7C" + (k + 1).ToString();
                        #endregion

                        #region 所得税
                        fpSpread1.Sheets[4].Cells[13, k].Formula = "损益表!R10C" + (k + 1).ToString();
                        #endregion
                    }

                    #region 净现金流量
                    fpSpread1.Sheets[4].Cells[14, k].Formula = "R5C" + (k + 1).ToString() + "-R9C" + (k + 1).ToString();
                    #endregion


                    #region 累计净现金流量
                    if (k == 2)
                        fpSpread1.Sheets[4].Cells[15, k].Formula = "R15C" + (k + 1).ToString();
                    if(k>2)
                        fpSpread1.Sheets[4].Cells[15, k].Formula = "R15C" + (k + 1).ToString() + "+R16C" + k.ToString();
                    #endregion  

                    #region 净现金流量现值
                    try
                    {
                        if (((double)fpSpread1.Sheets[4].Cells[4, k].Value > 0 || (double)fpSpread1.Sheets[4].Cells[8, k].Value > 0) && g1==0 )
                            g1 = (int)fpSpread1.Sheets[4].Cells[2, k].Value;
                    }catch{}

                    if (k == 2)
                        fpSpread1.Sheets[4].Cells[16, k].Formula = "NPV(参数!R15C4,R15C" + (k + 1).ToString() + ")";
                    if (k > 2)
                        fpSpread1.Sheets[4].Cells[16, k].Formula = "NPV(参数!R15C4,R15C3:R15C" + (k + 1).ToString() + ")-R18C" + k.ToString();



                        //fpSpread1_Sheet5.Cells[17, k].Formula = "NPV(8%,$R16C4:R16C" + (k + 1).ToString() + ")-R19C" + k.ToString();
                    #endregion  

                    #region 净现金流量现值累计
                    if (k == 2)
                        fpSpread1.Sheets[4].Cells[17, k].Formula = "R17C" + (k + 1).ToString();
                    if (k > 2)
                        fpSpread1.Sheets[4].Cells[17, k].Formula = "R17C" + (k + 1).ToString() + "+R18C" + k.ToString();
                    #endregion  


                    try
                    {
                        if ((double)fpSpread1.Sheets[4].Cells[15, k].Value > 0 && g2 == 0)
                        {
                            g2 = (int)fpSpread1.Sheets[4].Cells[2, k].Value;
                            g3 = k;
                            MsgBox.Show("g1=" + g1.ToString());
                            MsgBox.Show("g2=" + g2.ToString());

                            MsgBox.Show("g3=" + g3.ToString());
                        }
                    }
                    catch { }

                    listmax = k;

                }


            #region 最底下
            fpSpread1.Sheets[4].Cells[19, 1].Formula = "IRR(R15C3:R15C" + (1 + listmax).ToString() + ")"; ;
            fpSpread1.Sheets[4].Cells[19, 4].Formula = "R18C" + (2 + xiangmujisuannian).ToString();
            


            fpSpread1.Sheets[4].Cells.Get(4, 2, 17, listmax).CellType = numberCellTypes;

            fpSpread1.Sheets[4].Cells[4, 2, 17, listmax].Column.Width = 100;////


            fpSpread1.Sheets[4].Cells[2, 0, 17, listmax].Border = lineBorder;
            fpSpread1.Sheets[4].Cells.Get(0, 0).ColumnSpan = listmax + 1;

            fpSpread1.Sheets[4].ColumnCount = listmax + 1;
            #endregion


        }
        catch (Exception ex) { MsgBox.Show(ex.Message); }
        }



        private void InitCom1()
        { 
            try
            {
                int g1 = 0;
                int g2 = 0;
                int g3 = 0; 

                for (int k = 2; k < 2 + xiangmujisuannian; k++)
                {
                    try
                    {
                        if (((double)fpSpread1.Sheets[4].Cells[4, k].Value > 0 || (double)fpSpread1.Sheets[4].Cells[8, k].Value > 0) && g1 == 0)
                            g1 = (int)fpSpread1.Sheets[4].Cells[2, k].Value;
                    }
                    catch { }

                    try
                    {
                        if ((double)fpSpread1.Sheets[4].Cells[15, k].Value > 0 && g2 == 0)
                        {
                            g2 = (int)fpSpread1.Sheets[4].Cells[2, k].Value;
                            g3 = k;
                        }
                    }
                    catch { }
                }

                try
                {
                    fpSpread1.Sheets[4].SetValue(19, 8, g2 - g1 - ((double)fpSpread1.Sheets[4].Cells[15, g3 - 1].Value) / (double)fpSpread1.Sheets[4].Cells[14, g3].Value);
                    fpSpread1.Sheets[4].Cells[19, 8].CellType = numberCellTypes3;
                }
                catch { }
            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }



        private void InitCom2()
        {
            try
            {
                int g1 = 0;
                int g2 = 0;
                int g3 = 0;

                for (int k = 2; k < 2 + xiangmujisuannian; k++)
                {
                    try
                    {
                        if (((double)fpSpread1.Sheets[5].Cells[4, k].Value > 0 || (double)fpSpread1.Sheets[5].Cells[11, k].Value > 0) && g1 == 0)
                            g1 = (int)fpSpread1.Sheets[5].Cells[2, k].Value;
                    }
                    catch { }

                    try
                    {
                        if ((double)fpSpread1.Sheets[5].Cells[15, k].Value > 0 && g2 == 0)
                        {
                            g2 = (int)fpSpread1.Sheets[5].Cells[2, k].Value;
                            g3 = k;
                        }
                    }
                    catch { }
                }

                try
                {
                    fpSpread1.Sheets[5].SetValue(19, 8, g2 - g1 - ((double)fpSpread1.Sheets[5].Cells[15, g3 - 1].Value) / (double)fpSpread1.Sheets[5].Cells[14, g3].Value);
                    fpSpread1.Sheets[5].Cells[19, 8].CellType = numberCellTypes3;
                }
                catch { }
            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }








        private void SetEx4()
        {

            bool bl = false;
            fpSpread1.Sheets[3].ColumnCount = 300;

            try
            {
                fpSpread1.Sheets[3].Cells[2, 2, 8, 1 + xiangmunian1].Border = lineBorder1;
                fpSpread1.Sheets[3].Cells[2, 2, 8, 1 + xiangmunian1].ResetText();
            }
            catch
            { }

            int listmax = 0;
            try
            {
                for (int k = 2; k < 2 + xiangmujisuannian; k++)
                {
                    #region 项目年度
                    fpSpread1.Sheets[3].SetValue(2, k, kaishinian + k - 2);


                    #endregion

                    //#region 售电量
                    //if (k == 2)
                    //    fpSpread1.Sheets[3].Cells[3, k].Formula = "参数!R12C4";
                    //if (k > 2 && k<2+nianshu)
                    //    fpSpread1.Sheets[3].Cells[3, k].Formula = "R4C" + (k).ToString() + "*参数!R13C4";
                    //if (k >= 2 + nianshu)
                    //    fpSpread1.Sheets[3].Cells[3, k].Formula = "R4C" + (k).ToString();
                    //#endregion

                    #region 经营成本
                        if (k == 3)
                            fpSpread1.Sheets[3].Cells[4, k].Formula = "筹措表!R8C3*参数!R9C4";

                        if (k > 3 && k<3+nianshu)
                            fpSpread1.Sheets[3].Cells[4, k].Formula = "SUM(筹措表!R8C3:R8C" + k.ToString() + ")*参数!R9C4";

                        if (k >= 3 + nianshu)
                            fpSpread1.Sheets[3].Cells[4, k].Formula = "R5C"+k.ToString();
                    #endregion

                    #region 折旧费
                    int g1=0;
                    try
                    {
                        g1 = (int)fpSpread1.Sheets[8].GetValue(14, 1);
                    }
                    catch { }


                        if (k > 2 && k < 3 + nianshu)
                            fpSpread1.Sheets[3].Cells[5, k].Formula = "还本付息表!R" + (8 + nianshu).ToString() + "C"+k.ToString();
                        if (k >= 3 + nianshu && k<2+xiangmujisuannian-g1)
                            fpSpread1.Sheets[3].Cells[5, k].Formula = "R6C" + k.ToString();
                        if (k >= 2 + xiangmujisuannian - g1)
                        {
                            
                            fpSpread1.Sheets[3].Cells[5, k].Formula = "R6C" + k.ToString() + "-筹措表!R8C" + (k + g1 - xiangmujisuannian + 1).ToString() + "*基本数据!R6C2";
                            try{
                                
                                if(bl)
                                {
                                    fpSpread1.Sheets[3].Cells[5, k].Formula = "0";
                                }
                                else
                                {
                                    if ((double)fpSpread1.Sheets[3].Cells[5, k].Value <= 0.0)
                                    {
                                        fpSpread1.Sheets[3].Cells[5, k].Formula = "0";
                                        bl = true;
                                    }
                                }

                                
                            }catch{}

                            
                        }

                    #endregion


                    #region 财务费用
                        if (k > 2 && k<2+lixinian+nianshu)
                            fpSpread1.Sheets[3].Cells[6, k].Formula = "还本付息表!R" + (7 + nianshu).ToString() + "C" + k.ToString();

                    
                        //if (k == 2 + lixinian + nianshu)
                        //    fpSpread1.Sheets[3].Cells[6, k].Formula = "";

                    #endregion



                    #region 生产成本,输变电总成本
                    if (k > 2)
                    {
                        fpSpread1.Sheets[3].Cells[7, k].Formula = "R5C" + (k + 1).ToString() + "+R6C" + (k + 1).ToString();
                        fpSpread1.Sheets[3].Cells[8, k].Formula = "SUM(R5C" + (k + 1).ToString() + ":R7C" + (k + 1).ToString() + ")";
                    }
                    #endregion
                    listmax = k;

                }


            fpSpread1.Sheets[3].Cells.Get(4, 3, 8, listmax).CellType = numberCellTypes;


            fpSpread1.Sheets[3].Cells[3, 2, 8, listmax].Column.Width = 100;////


            fpSpread1.Sheets[3].Cells[2, 0, 8, listmax].Border = lineBorder;
            fpSpread1.Sheets[3].Cells.Get(0, 0).ColumnSpan = listmax + 1;

            fpSpread1.Sheets[3].ColumnCount = listmax + 1;


        }
        catch (Exception ex) { MsgBox.Show(ex.Message); }
        }
        private void SetEx3()
        {
            int i = 2;
            int m = nianshu + lixinian ;
            fpSpread1.Sheets[2].ColumnCount = 300;
            fpSpread1.Sheets[2].RowCount = 300;

            try
            {
                fpSpread1.Sheets[2].Cells[2, 2, nianshu1 + 9, nianshu1 + lixinian1].Border = lineBorder1;
                fpSpread1.Sheets[2].Cells[2, 2, nianshu1 + 9, nianshu1 + lixinian1].ResetText();
            }
            catch
            {

            
            }


            try
            {
                for (int k = 0; k < m; k++)
                {
                    fpSpread1.Sheets[2].SetValue(i, k + 1, kaishinian + k);
                    if (k < nianshu)
                        fpSpread1.Sheets[2].Cells[3, k + 1].Formula = "筹措表!R12C" + (k + 3).ToString();

                    for (int n = 0; n <nianshu; n++)
                    {
                        if (k == n + 1)
                            fpSpread1.Sheets[2].Cells[n + 4, k + 1].Formula = "-PMT(基本数据!R5C2,基本数据!R4C2,还本付息表!R4C"+(k+1).ToString()+",0,0)";
                        if (k > n + 1 && k < lixinian+n+1)
                            fpSpread1.Sheets[2].Cells[n + 4, k + 1].Formula = "还本付息表!R"+(n+5).ToString()+"C"+(k+1).ToString();
                    }

                    if (k > 0)
                    {
                        fpSpread1.Sheets[2].Cells[4 + nianshu, k+1].Formula = "SUM(R5C" + (k + 2).ToString() + ":R" + (4 +nianshu).ToString() + "C" + (k + 2).ToString() + ")";
                        fpSpread1.Sheets[2].Cells[5 + nianshu, k + 1].Formula = "R" + (5 + nianshu).ToString() + "C" + (k + 2).ToString() + "-R" + (7 + nianshu).ToString() + "C" + (k+2).ToString();


                        if (k == 2)
                        {
                            fpSpread1.Sheets[2].Cells[6 + nianshu, k + 1].Formula = "(SUM(R4C2:R4C" + (k + 1).ToString() + ")-R" + (6 + nianshu).ToString() + "C" + (k + 1).ToString() + "+R4C" + (k + 2).ToString() + "/2)*基本数据!R5C2";
                        }
                        else if (k == 1)
                        {
                            fpSpread1.Sheets[2].Cells[6 + nianshu, k + 1].Formula = "(R4C2+R4C3/2)*基本数据!R5C2";
                        }
                        else
                        {
                            fpSpread1.Sheets[2].Cells[6 + nianshu, k + 1].Formula = "(SUM(R4C2:R4C" + (k + 1).ToString() + ")-SUM(R11C3:R11C" + (k + 1).ToString() + ")+R4C" + (k + 2).ToString() + "/2)*基本数据!R5C2";

                        }


                        
                        
                        if (k == 1)
                        {
                            fpSpread1.Sheets[2].Cells[7 + nianshu, k + 1].Formula = "筹措表!R8C3*基本数据!R6C2";
                        }
                        else if(k>1 && k<nianshu)
                        {
                            fpSpread1.Sheets[2].Cells[7 + nianshu, k + 1].Formula = "SUM(筹措表!R8C3:R8C"+(k+2).ToString()+")*基本数据!R6C2";                       
                        }
                        else if (k == nianshu)
                        {
                            fpSpread1.Sheets[2].Cells[7 + nianshu, k + 1].Formula = "筹措表!R8C"+(3+k).ToString() + "*基本数据!R6C2";    
                        
                        }

                        else if (k > nianshu)
                        {
                            fpSpread1.Sheets[2].Cells[7 + nianshu, k + 1].Formula = "R"+(8+nianshu).ToString()+"C" + (k+1).ToString();

                        }



                        
                        fpSpread1.Sheets[2].Cells[8 + nianshu, k + 1].Formula = "IF((R" + (8 + nianshu).ToString() + "C" + (k + 2).ToString() + "-R" + (6 + nianshu).ToString() + "C" + (k + 2).ToString() + ")>0,R" + (6 + nianshu).ToString() + "C" + (k + 2).ToString() + ",R" + (8 + nianshu).ToString() + "C" + (k + 2).ToString()+")";
                        fpSpread1.Sheets[2].Cells[9 + nianshu, k + 1].Formula = "IF((R" + (6 + nianshu).ToString() + "C" + (k + 2).ToString() + "-R" + (9 + nianshu).ToString() + "C" + (k + 2).ToString() + ")>0,R" + (6 + nianshu).ToString() + "C" + (k + 2).ToString() + "-R" + (9 + nianshu).ToString() + "C" + (k + 2).ToString()+",0)";
                        //if(j>2)
                        //    fpSpread1.Sheets[3.Cells[i + list.Count + 4, j].Formula = "SUM(R5C3:R5C" + j.ToString() + ")+R5C" + (j + 1).ToString() + "/2-SUM(R" + (7 + yearcount).ToString() + "C3:R" + (7 + yearcount).ToString() + "C" + (j).ToString() + ")";
                        //fpSpread1.Sheets[3.Cells[i + list.Count + 5, j].Formula = "SUM(R5C3:R5C" + j.ToString() + ")+R5C" + (j + 1).ToString() + "/2-SUM(R" + (7 + yearcount).ToString() + "C3:R" + (7 + yearcount).ToString() + "C" + (j).ToString() + ")";
                    }
                    //j++;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);}

            fpSpread1.Sheets[2].Cells.Get(3, 1, nianshu+9, m).CellType = numberCellTypes;
            fpSpread1.Sheets[2].Cells[3, 1, nianshu + 9, m].Column.Width = 100;////


            fpSpread1.Sheets[2].Cells[2, 0, nianshu + 9, m].Border = lineBorder;
            fpSpread1.Sheets[2].Cells.Get(0, 0).ColumnSpan = m+1;

            fpSpread1.Sheets[2].SetValue(4 + nianshu, 0, "应还本金和利息");
            fpSpread1.Sheets[2].SetValue(5 + nianshu, 0, "      应还本金");
            fpSpread1.Sheets[2].SetValue(6 + nianshu, 0, "      应还利息");
            fpSpread1.Sheets[2].SetValue(7 + nianshu, 0, "固定资产折旧费");
            fpSpread1.Sheets[2].SetValue(8 + nianshu, 0, "其中:还款折旧");
            fpSpread1.Sheets[2].SetValue(9 + nianshu, 0, "其它还款资金");

            fpSpread1.Sheets[2].ColumnCount = m + 1;
            fpSpread1.Sheets[2].RowCount = 10 + nianshu;

            //fpSpread1.Sheets[3.Cells.Get(4, 2, i + list.Count + 5, j).CellType = numberCellTypes;


        }
        private void SetEx2()
        {
            //fpSpread1.Sheets[1].AddColumns(3, eds.Count);

            fpSpread1.Sheets[1].ColumnCount = 300;

            fpSpread1.Sheets[1].Cells[2, 2, 14, 2+nianshu1].ResetText();
            fpSpread1.Sheets[1].Cells[2, 2, 14, 2 + nianshu1].Border = lineBorder1;

            int i = 3;
            int j = 2;
            int m = eds.Count;
            foreach (EconomyData ed in eds)
            {
                fpSpread1.Sheets[1].SetValue(i, j, ed.S1);
                fpSpread1.Sheets[1].SetValue(i + 1, j, ed.S2);
                fpSpread1.Sheets[1].Cells[i + 3, j].Formula = "R" + (i + 2).ToString() + "C" + (j + 1).ToString() + "/(1-参数!R4C4*基本数据!R5C2*参数!R8C4)";
                fpSpread1.Sheets[1].Cells[i + 2, j].Formula = "R" + (i + 4).ToString() + "C" + (j + 1).ToString() + "-R" + (i + 2).ToString() + "C" + (j + 1).ToString();
                fpSpread1.Sheets[1].Cells[i + 4, j].Formula = "R" + (i + 4).ToString() + "C" + (j + 1).ToString();
                fpSpread1.Sheets[1].Cells[i + 5, j].Formula = "R" + (i + 5).ToString() + "C" + (j + 1).ToString() + "*参数!R5C4";
                fpSpread1.Sheets[1].Cells[i + 6, j].Formula = "R" + (i + 4).ToString() + "C" + (j + 1).ToString();
                fpSpread1.Sheets[1].Cells[i + 7, j].Formula = "R" + (i + 7).ToString() + "C" + (j + 1).ToString() + "*参数!R6C4";
                fpSpread1.Sheets[1].Cells[i + 8, j].Formula = "R" + (i + 7).ToString() + "C" + (j + 1).ToString() + "-R" + (i + 8).ToString() + "C" + (j + 1).ToString();
                fpSpread1.Sheets[1].Cells[i + 9, j].Formula = "R" + (i + 6).ToString() + "C" + (j + 1).ToString();
                fpSpread1.Sheets[1].Cells[i + 10, j].Formula = "R" + (i + 10).ToString() + "C" + (j + 1).ToString() + "*参数!R7C4";
                fpSpread1.Sheets[1].Cells[i + 11, j].Formula = "R" + (i + 10).ToString() + "C" + (j + 1).ToString() + "-R" + (i + 11).ToString() + "C" + (j + 1).ToString();

                j++;
            }


            //fpSpread1_Sheet2.SetValue(i, j, ed.S1);
            fpSpread1.Sheets[1].Cells[i + 1, j].Formula = "SUM(R" + (i + 2).ToString() + "C3:R" + (i + 2).ToString() + "C" + j.ToString() + ")";
            fpSpread1.Sheets[1].Cells[i + 2, j].Formula = "SUM(R" + (i + 3).ToString() + "C3:R" + (i + 3).ToString() + "C" + j.ToString() + ")";
            fpSpread1.Sheets[1].Cells[i + 3, j].Formula = "SUM(R" + (i + 4).ToString() + "C3:R" + (i + 4).ToString() + "C" + j.ToString() + ")";
            fpSpread1.Sheets[1].Cells[i + 4, j].Formula = "SUM(R" + (i + 5).ToString() + "C3:R" + (i + 5).ToString() + "C" + j.ToString() + ")";
            fpSpread1.Sheets[1].Cells[i + 5, j].Formula = "SUM(R" + (i + 6).ToString() + "C3:R" + (i + 6).ToString() + "C" + j.ToString() + ")";
            fpSpread1.Sheets[1].Cells[i + 6, j].Formula = "SUM(R" + (i + 7).ToString() + "C3:R" + (i + 7).ToString() + "C" + j.ToString() + ")";
            fpSpread1.Sheets[1].Cells[i + 7, j].Formula = "SUM(R" + (i + 8).ToString() + "C3:R" + (i + 8).ToString() + "C" + j.ToString() + ")";
            fpSpread1.Sheets[1].Cells[i + 8, j].Formula = "SUM(R" + (i + 9).ToString() + "C3:R" + (i + 9).ToString() + "C" + j.ToString() + ")";
            fpSpread1.Sheets[1].Cells[i + 9, j].Formula = "SUM(R" + (i + 10).ToString() + "C3:R" + (i + 10).ToString() + "C" + j.ToString() + ")";
            fpSpread1.Sheets[1].Cells[i + 10, j].Formula = "SUM(R" + (i + 11).ToString() + "C3:R" + (i + 11).ToString() + "C" + j.ToString() + ")";
            fpSpread1.Sheets[1].Cells[i + 11, j].Formula = "SUM(R" + (i + 12).ToString() + "C3:R" + (i + 12).ToString() + "C" + j.ToString() + ")";

            fpSpread1.Sheets[1].Cells.Get(4, 2, i + 11, j).CellType = numberCellTypes;
            fpSpread1.Sheets[1].Cells[4, 2, i + 11, j].Column.Width = 100;////


            fpSpread1.Sheets[1].Cells[2, 0, i + 11, j].Border = lineBorder;

            fpSpread1.Sheets[1].Cells.Get(2, 2).ColumnSpan = eds.Count + 1;
            fpSpread1.Sheets[1].Cells.Get(0, 0).ColumnSpan = eds.Count + 3;
            fpSpread1.Sheets[1].SetValue(2, 2, "建设及投产年度");
            fpSpread1.Sheets[1].SetValue(i, j, "合计");

            fpSpread1.Sheets[1].ColumnCount = 1 + j;
        }
        private void SetEx1()
        {
            try
            {
                fpSpread1.Sheets[0].SetValue(2, 1, "新增固定资产原值的" + (double)fpSpread1.Sheets[8].GetValue(8, 3)*100+"%");
            }
            catch { }
            fpSpread1.Sheets[0].Cells[3, 1].Formula = "参数!R3C2";
            fpSpread1.Sheets[0].Cells[4, 1].Formula = "参数!R4C2";
            fpSpread1.Sheets[0].Cells[5, 1].Formula = "参数!R5C2";
            fpSpread1.Sheets[0].Cells[6, 1].Formula = "参数!R6C2";
            fpSpread1.Sheets[0].Cells[7, 1].Formula = "参数!R7C2";
            fpSpread1.Sheets[0].Cells[8, 1].Formula = "参数!R8C2";
            fpSpread1.Sheets[0].Cells[9, 1].Formula = "参数!R9C2";
            fpSpread1.Sheets[0].Cells[10, 1].Formula = "参数!R10C2";
            fpSpread1.Sheets[0].Cells[11, 1].Formula = "参数!R11C2";

            try
            {
                fpSpread1.Sheets[0].SetValue(13, 1, "市区" + (double)fpSpread1.Sheets[8].GetValue(11, 1) * 100 + "%" + ",县镇" + (double)fpSpread1.Sheets[8].GetValue(12, 1) * 100 + "%" + ",其它地区" + (double)fpSpread1.Sheets[8].GetValue(13, 1) * 100 + "%" + ";教育费附加" + (double)fpSpread1.Sheets[8].GetValue(2, 3) * 100 + "%");
            }
            catch { }
        }

        private bool Check()
        {
            try
            {
                kaishinian = int.Parse(fpSpread1.Sheets[8].GetValue(9, 3).ToString());
                nianshu = int.Parse(fpSpread1.Sheets[8].GetValue(10, 3).ToString());


            }
            catch 
            {
                MsgBox.Show("开始年度或者年数未输入");
                return false; 
            }

            return true;
        
        
        }




        private void InitList()
        {
            //fpSpread1.Sheets[8].Cells[15, 1, 16, 50].ResetText();

            if (!Check())
                return;

            fpSpread1.Sheets[8].ColumnCount = 200;
            fpSpread1.Sheets[8].Cells[15, 1, 16, nianshu+1].ResetText();

            try
            {
                kaishinian = int.Parse(fpSpread1.Sheets[8].GetValue(9, 3).ToString());
                nianshu = int.Parse(fpSpread1.Sheets[8].GetValue(10, 3).ToString());

                //fpSpread1.Sheets[8].Cells[15, 1, 16, 19].ResetText();

                for (int i = 0; i < nianshu; i++)
                {
                    fpSpread1.Sheets[8].SetValue(15, 1 + i, kaishinian + i);
                }

            }
            catch { }

            try
            {

                xiangmujisuannian = int.Parse(fpSpread1.Sheets[8].GetValue(13, 3).ToString());


            }
            catch { }

            try
            {
                lixinian = int.Parse(fpSpread1.Sheets[8].GetValue(2, 1).ToString());
            }
            catch { }

            


            if (nianshu >=4)
            {
                fpSpread1.Sheets[8].ColumnCount = nianshu + 1;
            }
            else
            {
                fpSpread1.Sheets[8].ColumnCount = 4;
            }
            fpSpread1.Sheets[8].Cells[15, 1, 16, nianshu].Border = lineBorder;
        
        }



        private bool Check1()
        {
            try
            {
                lixinian = int.Parse(fpSpread1.Sheets[8].GetValue(2, 1).ToString());
            }
            catch
            {
                MsgBox.Show("还贷期未输入!");
                return false;
            }


            try
            {
                kaishinian = int.Parse(fpSpread1.Sheets[8].GetValue(9, 3).ToString());
                nianshu = int.Parse(fpSpread1.Sheets[8].GetValue(10, 3).ToString());
            }
            catch
            {
                MsgBox.Show("开始建设年度或建设年限未输入!");
                return false;
            }

            try
            {
                xiangmujisuannian = int.Parse(fpSpread1.Sheets[8].GetValue(13, 3).ToString());
            }
            catch
            {   
                MsgBox.Show("项目计算期未输入!");
                return false;
            }

            if (xiangmujisuannian > 50)
            {
                MsgBox.Show("项目计算期太大，不要超过50年");
                return false;

            }

            if (nianshu + lixinian > xiangmujisuannian)
            {
                MsgBox.Show("项目计算期应该大于还贷期和建设年限之和");
                return false;

            }


            return true;


        }




        private void InitCompute()
        {
            if (!Check1())
                return;


            try
            {
                eds.Clear();
                for (int i = 0; i < nianshu; i++)
                {
                    EconomyData ed = new EconomyData();
                    try
                    {
                        ed.S1 = int.Parse(fpSpread1.Sheets[8].GetValue(15, 1 + i).ToString());
                    }
                    catch { ed.S1 = 0; }
                    try
                    {
                        ed.S2 = int.Parse(fpSpread1.Sheets[8].GetValue(16, 1 + i).ToString());
                    }

                    catch { ed.S2 = 0; }
                    eds.Add(ed);
                }

            }

            catch { }



            try
            {
            SetEx1();
            SetEx2();
            SetEx3();
            SetEx4();
            SetEx5();
            SetEx6();
            SetEx7();
            SetEx8();
            fpSpread1.ActiveSheetIndex = 0;

            xiangmunian1 = xiangmujisuannian;
            lixinian1 = lixinian;
            nianshu1 = nianshu;
        }

        catch { }
        }




        private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            ////////////if (e.Column == 1 && e.Row == 18)
            ////////////{
            ////////////    InitList();
            ////////////}

            ////////////if (e.Column == 2 && e.Row == 18)
            ////////////{
            ////////////    InitCompute();
            ////////////}

            ////////////if (e.Column == 3 && e.Row == 18)
            ////////////{
            ////////////    fpSpread1.ActiveSheetIndex = 0;
            ////////////}

            ////////////if (e.Column == 4 && e.Row == 18)
            ////////////{
            ////////////    isnew();
            ////////////}
        }

        private void isnew()
        {
        }



        private void fpSpread1_Sheet9_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void FrmEconomyAnalysis_FormClosing(object sender, FormClosingEventArgs e)
        {
            treeList1.Focus();
            if (excelstate)
            {
                //MsgBox.Show("2222");
                IsSave();
            }

            excelstate = false;
           
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Econ ec = new Econ();
            ec.UID = "xxx";



            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            try
            {

                fpSpread1.Save(ms, false);

                ec.ExcelData = ms.GetBuffer();

                Services.BaseService.Update<Econ>(ec);
            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }



        private void fpSpread1_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            excelstate = true;
            
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;


            //string uid = treeList1.FocusedNode["UID"].ToString();
            //EconomyAnalysis obj = Services.BaseService.GetOneByKey<EconomyAnalysis>(uid);




            //System.IO.MemoryStream ms = new System.IO.MemoryStream(obj.Contents);

            //FarPoint.Win.Spread.FpSpread fps=new FarPoint.Win.Spread.FpSpread();
            //fps.Open(ms);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                string fname = "";
                saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fname = saveFileDialog1.FileName;
                    
                    try
                    {
                        fpSpread1.SaveExcel(fname, FarPoint.Excel.ExcelSaveFlags.NoFormulas);
                        //fps.SaveExcel(fname);
                        // 定义要使用的Excel 组件接口
                        // 定义Application 对象,此对象表示整个Excel 程序
                        Microsoft.Office.Interop.Excel.Application excelApp = null;
                        // 定义Workbook对象,此对象代表工作薄
                        Microsoft.Office.Interop.Excel.Workbook workBook;
                        // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                        Microsoft.Office.Interop.Excel.Worksheet ws = null;
                        Microsoft.Office.Interop.Excel.Range range = null;
                        excelApp = new Microsoft.Office.Interop.Excel.Application();

                        workBook = excelApp.Workbooks.Open(fname, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                        for (int i = 1; i <= workBook.Worksheets.Count; i++)
                        {

                            ws = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[i];
                            //取消保护工作表
                            ws.Unprotect(Missing.Value);
                            //有数据的行数
                            int row = ws.UsedRange.Rows.Count;
                            //有数据的列数
                            int col = ws.UsedRange.Columns.Count;
                            //创建一个区域
                            range = ws.get_Range(ws.Cells[1, 1], ws.Cells[row, col]);
                            //设区域内的单元格自动换行
                            range.Select();
                            range.NumberFormatLocal = "G/通用格式";

                            //保护工作表
                            ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                        }
                        //保存工作簿
                        workBook.Save();
                        //关闭工作簿
                        excelApp.Workbooks.Close();
                        if (MsgBox.ShowYesNo("导出成功，是否打开该文档？") != DialogResult.Yes)
                            return;

                        System.Diagnostics.Process.Start(fname);
                    }
                    catch
                    {
                        MsgBox.Show("无法保存" + fname + "。请用其他文件名保存文件，或将文件存至其他位置。");
                        return;
                    }
                }

        }

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            //InitData1();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitData1();
        }


        private void InitData1()
        {
            if (excelstate)
            {
                IsSave();
            }

            if (treeList1.FocusedNode == null)
                return;

            if (treeList1.FocusedNode.ParentNode == null)
            {
                barEdititem.Enabled=false;
                barDelitem.Enabled = false;
                barCS.Enabled = false;
                barButtonItem5.Enabled = false;
                barSave.Enabled = false;
            }
            else
            {
                barEdititem.Enabled = true ;
                barDelitem.Enabled = true;
                barCS.Enabled = true;
                barButtonItem5.Enabled = true;
                barSave.Enabled = true;
            }


            excelstate = false;

            if (!isloadstate)
                return;

            string uid = treeList1.FocusedNode["UID"].ToString();
            uid1 = uid;
            EconomyAnalysis obj = Services.BaseService.GetOneByKey<EconomyAnalysis>(uid);

            WaitDialogForm wait = null;
            try
            {
                wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
                System.IO.MemoryStream ms = new System.IO.MemoryStream(obj.Contents);
                by1 = obj.Contents;
                fpSpread1.Open(ms);

                try
                {
                    fpSpread1.Sheets[0].Cells[0, 0].Text = "附表1 " + treeList1.FocusedNode["Title"].ToString() + "基础数据";
                }
                catch { }

                wait.Close();

                try
                {
                    nianshu1 = int.Parse(fpSpread1.Sheets[8].GetValue(10, 3).ToString());
                }
                catch { }



                try
                {
                    kaishinian = int.Parse(fpSpread1.Sheets[8].GetValue(9, 3).ToString());
                    xiangmunian1 = int.Parse(fpSpread1.Sheets[8].GetValue(13, 3).ToString());
                    lixinian1 = int.Parse(fpSpread1.Sheets[8].GetValue(2, 1).ToString());
                }
                catch { }

            }
            catch { wait.Close(); }
        
        }


        private void InitComputeFY()
        {
            int y = int.Parse(fpSpread1.Sheets[8].GetValue(10, 3).ToString()); //年数

            int gz = int.Parse(fpSpread1.Sheets[8].GetValue(20, 1).ToString()); //工资
            int dy = int.Parse(fpSpread1.Sheets[8].GetValue(23, 1).ToString()); //定员

            double jycb = double.Parse(fpSpread1.Sheets[8].GetValue(8, 3).ToString()); //成本
            double cl = double.Parse(fpSpread1.Sheets[8].GetValue(22, 1).ToString());
            double wh = double.Parse(fpSpread1.Sheets[8].GetValue(19, 3).ToString());
            double bx = double.Parse(fpSpread1.Sheets[8].GetValue(20, 3).ToString());
            double qt = double.Parse(fpSpread1.Sheets[8].GetValue(21, 3).ToString());

            double xs = double.Parse(fpSpread1.Sheets[8].GetValue(19, 1).ToString());
            double gdj = double.Parse(fpSpread1.Sheets[8].GetValue(23, 3).ToString());

            double fl = double.Parse(fpSpread1.Sheets[8].GetValue(21, 1).ToString());

            double cbzh = 0;//成本综合
            double dlcbzh = 0;//电力成本综合
            double ygcbzh = 0;//员工成本综合
            ygcbzh = gz * dy * (1 + fl);


            double lxsum=cl+wh+bx+qt;//费用比例总和

            double sds = 0;//所得税

            double ws = 0;//网损

            ws = xs * gdj;

            double[,] d = new double[y, y];
            double[] e = new double[y];
            double[] f = new double[y];

            IList<EconomyData> i1 = new List<EconomyData>();
            double tzsum = 0;//总投资
            for (int q = 0; q < y; q++)
            {
                EconomyData ed11 = new EconomyData();
                try
                {
                    ed11.S1 = (int)fpSpread1.Sheets[8].GetValue(15, 1 + q);
                }
                catch { }
                try
                {
                    ed11.S2 = Convert.ToDouble(fpSpread1.Sheets[8].GetValue(16, 1 + q).ToString());
                }
                catch { }

                try
                {
                    ed11.S3 = Convert.ToDouble(fpSpread1.Sheets[8].GetValue(17, 1 + q).ToString());
                }
                catch { }
                tzsum += ed11.S2;
                i1.Add(ed11);
            }

            IList<EconomyData> i2 = new List<EconomyData>();
            double dlsum = 0;//总电量
            for (int w = 0; w < y; w++)
            {
                EconomyData ed22 = new EconomyData();
                try
                {
                    ed22.S1 = kaishinian + w;
                }
                catch { }
                try
                {
                    ed22.S2 = (double)fpSpread1.Sheets[3].GetValue(3, 2 + w);
                }
                catch { }

                try
                {
                    ed22.S3 = Convert.ToDouble(fpSpread1.Sheets[8].GetValue(18, 1 + w).ToString());
                }
                catch { }

                dlsum += ed22.S3;
                i2.Add(ed22);
            }



            for (int i = 0; i < y; i++)
            {       
                double d2 = i1[i].S2;
                for (int j = i; j < y; j++)
                {
                    if (i == j)
                    {
                        d[i, j] = d2 * lxsum;
                    }
                    else
                    {
                        d[i, j] = d2 * (lxsum+jycb);
                    }

                    dlcbzh += d[i, j];
                }
            }

         
            cbzh = dlcbzh + ygcbzh*y/10000;





            for (int i = 3; i < 3 + y - 1; i++)
            {
                double d1 = 0;
                if (fpSpread1.Sheets[6].Cells[6, i].Text != "")
                    d1 = double.Parse(fpSpread1.Sheets[6].Cells[6, i].Text);

                double d2 = 0;
                if (fpSpread1.Sheets[6].Cells[9, i].Text != "")
                    d2 = double.Parse(fpSpread1.Sheets[6].Cells[9, i].Text);

                double d3 = 0;
                if (fpSpread1.Sheets[6].Cells[9, i].Text != "")
                    d3 = double.Parse(fpSpread1.Sheets[6].Cells[10, i].Text);

                double d4 = 0;
                if (fpSpread1.Sheets[6].Cells[9, i].Text != "")
                    d4 = double.Parse(fpSpread1.Sheets[6].Cells[12, i].Text);

                sds += d1 + d2 + d3 + d4;
            }

            double m1 = 0;
            double m2 = 0;
            double m3 = 0;

            m1 = cbzh*1000 / dlsum ;
            m2 = (cbzh + sds) * 1000 / dlsum;
            m3 = m2 + ws;

            fpSpread1.Sheets[7].Cells[16, 3].Value = m1.ToString("n2");
            fpSpread1.Sheets[7].Cells[17, 3].Value = m2.ToString("n2");
            fpSpread1.Sheets[7].Cells[18, 3].Value = m3.ToString("n2");

        }

    }
}