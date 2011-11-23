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
using System.IO;
using TONLI.BZH.UI;

namespace Itop.Client.Layouts
{
    public partial class FrmRtfCategory : Itop.Client.Base.FormBase
    {
        private IList<RtfCategory> ilist = new List<RtfCategory>();
        DataTable dataTable = new DataTable();
        //decimal count = 0;
        private bool isSelect = false;
        private byte[] txtByte = null;
        bool isstate = false;
        byte[] fb = null;

        public byte[] TxtByte
        {
            set { txtByte = value; }
            get { return txtByte; }
        }

        public bool IsSelect
        {
            set { isSelect = value; }
        }

        public FrmRtfCategory()
        {
            InitializeComponent();
        }

        string type = "";
        public void Init1()
        {
            type = "1";
            this.ShowDialog();
        }

        public void Init2()
        {
            type = "2";
            this.ShowDialog();
        }

        public void Init3()
        {
            type = "3";
            this.ShowDialog();
        }

        public void Init4()
        {
            type = "4";
            this.ShowDialog();
        }

        public void Init5()
        {
            type = "5";
            this.ShowDialog();
        }

        public void Init6()
        {
            type = "6";
            this.ShowDialog();
        }

        public void Init7()
        {
            type = "7";
            this.ShowDialog();
        }

        public void Init8()
        {
            type = "8";
            this.ShowDialog();
        }

        public void Init9()
        {
            type = "9";
            this.ShowDialog();
        }

        public void Init10()
        {
            type = "10";
            this.ShowDialog();
        }

        public void Init11()
        {
            type = "11";
            this.ShowDialog();
        }

        public void Init12()
        {
            type = "12";
            this.ShowDialog();
        }

        public void Init13()
        {
            type = "13";
            this.ShowDialog();
        }

        public void Init14()
        {
            type = "14";
            this.ShowDialog();
        }

        public void Init15()
        {
            type = "15";
            this.ShowDialog();
        }

        public void Inita()
        {
            type = "a";
            this.ShowDialog();
        }


        public void Initb()
        {
            type = "b";
            this.ShowDialog();
        }























        public void InitModule(string id)
        {
            try
            {
                //dsoFramerWordControl2.FileNew();
                LayoutType lt1 = Services.BaseService.GetOneByKey<LayoutType>(id);
                if (lt1 == null)
                {
                    lt1 = new LayoutType();
                    LayoutType lt2 = Services.BaseService.GetOneByKey<LayoutType>("LayoutModule");
                    lt1.UID = id;
                    Services.BaseService.Create<LayoutType>(lt1);
                }
                fb = lt1.ExcelData;
            }
            catch { }
        }

        private void FrmLayoutContents_Load(object sender, EventArgs e)
        {
            InitModule("Rtf");
            
           
            isstate = true;
            InitForm();
            InitData();

            
            treeList1.MoveFirst();




                if (AddRight || EditRight)
                dsoFramerWordControl2.OnFileSaved += new EventHandler(dsoFramerWordControl2_OnFileSaved);

            //txEdit1.saveData += new TxEditor.SaveToStream(txEdit1_saveData);
        }

        void dsoFramerWordControl2_OnFileSaved(object sender, EventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;
            string uid = treeList1.FocusedNode["UID"].ToString();
            RtfCategory obj = Services.BaseService.GetOneByKey<RtfCategory>(uid);
            obj.RtfContents = dsoFramerWordControl2.FileDataGzip;
            //obj.RtfContents = txEdit1.m_SaveStream();
            WaitDialogForm wait = null;
            try
            {
                wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
                Services.BaseService.Update("UpdateRtfCategoryByte", obj);
                wait.Close();
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                wait.Close();
                return;


            }
        }


        private void InitForm()
        {
            //Itop.Common.ImeController.SetIme(this);
            //txEdit1.editok_state(false);

            if (!isSelect)
            {
                if (!EditRight)
                {
                    barSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //txEdit1.btn_editok_visible = DevExpress.XtraBars.BarItemVisibility.Never;
                    barEdititem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.ctrlRtfAttachFiles1.GridControl.EmbeddedNavigator.Buttons.CustomButtons[2].Visible = false;
                    this.ctrlRtfAttachFiles1.修改ToolStripMenuItem.Visible = false;
                    
                }

                if (!AddRight)
                {
                    barAdditem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barAdd1item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.ctrlRtfAttachFiles1.GridControl.EmbeddedNavigator.Buttons.CustomButtons[0].Visible = false;
                    this.ctrlRtfAttachFiles1.添加ToolStripMenuItem.Visible = false;
                }

                if (!DeleteRight)
                {
                    barDelitem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.ctrlRtfAttachFiles1.GridControl.EmbeddedNavigator.Buttons.CustomButtons[1].Visible = false;
                    this.ctrlRtfAttachFiles1.删除ToolStripMenuItem.Visible = false;
                }

                if (!PrintRight)
                {
                    barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    this.ctrlRtfAttachFiles1.打印ToolStripMenuItem.Visible = false;
                    this.ctrlRtfAttachFiles1.GridControl.EmbeddedNavigator.Buttons.CustomButtons[3].Visible = false;
                }

               // barSelect.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


                //if (!(AddRight || EditRight))
                //    dsoFramerWordControl2.IsReadOnly = true;
            }

            else
            {
                VsmdgroupProg vp=new VsmdgroupProg();
                try
                {
                    vp = MIS.GetProgRight("a5ac2d77-e60e-4253-ae10-4a17abf5a89b", "", MIS.UserNumber);
                }
                catch { }
                if (vp.upd == "0")
                {
                    barEdititem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //txEdit1.btn_editok_visible = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                if (vp.ins == "0")
                {
                    barAdd1item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barAdditem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                if (vp.del == "0")
                    barDelitem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                if (vp.prn == "0")
                    barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            
            
            }
        }

        private void InitData()
        {
            try
            {
                string s = " UID like '%"+ProjectUID+"|"+type+"%'";
                ilist = Services.BaseService.GetList<RtfCategory>("SelectRtfCategoryByWhere",s);
            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
            dataTable = DataConverter.ToDataTable((IList)ilist, typeof(RtfCategory));
            treeList1.DataSource = dataTable;
        
        }




        void txEdit1_saveData(byte[] byteStream)
        {
            
        }


        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;

            if (!isstate)
                return;

            string uid = treeList1.FocusedNode["UID"].ToString();
            RtfCategory obj = Services.BaseService.GetOneByKey<RtfCategory>(uid);
            WaitDialogForm wait=null;

            this.ctrlRtfAttachFiles1.Category = uid;
            this.ctrlRtfAttachFiles1.RefreshData();


            WordBuilder wb = new WordBuilder(); 
            if(fb!=null)
            wb.InsertFromStreamGzip(fb);


            try
            {
                wait = new WaitDialogForm("", "正在下载数据, 请稍候...");
                //txEdit1.ResetContents();
                //txEdit1.m_LoadStream(obj.RtfContents);
                if (obj.RtfContents != null && obj.RtfContents.Length > 0)
                {
                    if (fb != null)
                    {
                        wb.InsertFromStreamGzip(obj.RtfContents);
                        dsoFramerWordControl2.FileData = wb.FileData;
                    }
                    else
                    {
                        dsoFramerWordControl2.FileDataGzip = obj.RtfContents;
                    }

                    //////dsoFramerWordControl2.FileDataGzip = obj.RtfContents;
                }
                else
                {
                    ////////LayoutType lt1 = Services.BaseService.GetOneByKey<LayoutType>("LayoutModule");
                    ////////dsoFramerWordControl2.FileDataGzip = lt1.ExcelData;
                    dsoFramerWordControl2.FileNew();
                }
                
                //if (!(AddRight || EditRight))
                //    dsoFramerWordControl2.IsReadOnly = true;
                dsoFramerWordControl2.AxFramerControl.Menubar = true;
                wait.Close();
            }
            catch (Exception ex) 
            { 
                //dsoFramerWordControl2.FileDataGzip = wb.FileDataGzip; 
                wait.Close(); 
                MessageBox.Show(ex.Message); }//MsgBox.Show("程序发生异常，请检查Office是否安装正确！"); }
        }

        


        //添加同级
        private void barAdditem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {



            decimal count = 0;
            string parentid = "";
            if (treeList1.FocusedNode != null)
            {
                parentid = treeList1.FocusedNode["ParentID"].ToString();
            }

            object objs = Services.BaseService.GetObject("SelectRtfCategorySortNo", parentid);
            if (objs != null)
            count = (decimal)objs;

            RtfCategory obj = new RtfCategory();
            obj.UID = obj.UID + "|" + ProjectUID + "|" + type;
            obj.SortNo = count + 1;
            obj.ParentID = parentid;
           
            FrmRtfCategoryDialog dlg = new FrmRtfCategoryDialog();
            dlg.Object = obj;
            dlg.IsCreate = true;


            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
        }
        //添加下级
        private void barAdd1item_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            decimal count = 0;
            string parentid = "";
            if (treeList1.FocusedNode == null)
                return;
            parentid = treeList1.FocusedNode["UID"].ToString();

            object objs = Services.BaseService.GetObject("SelectRtfCategorySortNo", parentid);
            if (objs != null)
                count = (decimal)objs;

            RtfCategory obj = new RtfCategory();
            obj.UID = obj.UID + "|" + ProjectUID + "|" + type;
            obj.ParentID = parentid;
            obj.SortNo = count + 1;
            FrmRtfCategoryDialog dlg = new FrmRtfCategoryDialog();
            dlg.Object = obj;
            dlg.IsCreate = true;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
        }

        private void barEdititem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;
            string uid = treeList1.FocusedNode["UID"].ToString();
            RtfCategory obj = Services.BaseService.GetOneByKey<RtfCategory>(uid);
            RtfCategory objCopy = new RtfCategory();
            DataConverter.CopyTo<RtfCategory>(obj, objCopy);

            FrmRtfCategoryDialog dlg = new FrmRtfCategoryDialog();
            dlg.Object = objCopy;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DataConverter.CopyTo<RtfCategory>(objCopy, obj);
            treeList1.FocusedNode.SetValue("Title", obj.Title);
            treeList1.FocusedNode.SetValue("SortNo", obj.SortNo);
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
                Services.BaseService.DeleteByKey<RtfCategory>(uid);
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

        private void barSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;


            string uid = treeList1.FocusedNode["UID"].ToString();
            RtfCategory obj = Services.BaseService.GetOneByKey<RtfCategory>(uid);
            txtByte = obj.RtfContents;
            DeleteFile();
            this.DialogResult = DialogResult.OK;
        }

        private void FrmRtfCategory_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DeleteFile();
        }

        private void DeleteFile()
        {
            string path = Application.StartupPath + "\\BlogData";
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch
            { }
        }

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //txEdit1.m_Preview();
            dsoFramerWordControl2.FilePrintDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {




            //dsoFramerWordControl2.FilePrintDialog();
            //dsoFramerWordControl2.DoInsertOleObject(@"C:\进度安排.xls");
            //dsoFramerWordControl2.DoInsertPicture(@"C:\back.jpg");

            //Font font=new Font("宋体",18);
            //dsoFramerWordControl2.DoInsert("bbbbbb\n\r", font, TONLI.BZH.UI.WdParagraphAlignment.Center);
            //dsoFramerWordControl2.DoInsertFromFile(@"C:\电网规划计算机辅助决策系统》软件修改意见.doc");
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                dsoFramerWordControl2.DoPageSetupDialog();
            }
            catch { }
        }

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormLayoutType flt = new FormLayoutType();
            flt.Type = "Rtf";
            flt.FRC = this;
            flt.ShowDialog();
        }





    }
}