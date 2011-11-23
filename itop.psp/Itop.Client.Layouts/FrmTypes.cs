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

namespace Itop.Client.Layouts
{
    public partial class FrmTypes : FormBase
    {
        string uid1 = "";

        public FarPoint.Win.Spread.FpSpread FPSpread
        {
            get { return fpSpread1; }
        
        }

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



        public FrmTypes()
        {
            InitializeComponent();
            
        }

        private void FrmLayoutContents_Load(object sender, EventArgs e)
        {

            InitForm();
            InitData();
      
            //fpSpread1.Sheets[0].SetColumnAllowAutoSort(-1, true);
            treeList1.MoveFirst();      
        }

        private void InitData()
        {

            if (!isSelect)
            {
                switch (smmprog.ProgId)
                {
                    case "64c9efcb-e6cc-402f-b2fc-f5f6f7d296f7":
                        progName = "�������Է���";
                        type = "fhtxfx";
                        break;

                    case "a50e1781-e470-4721-a6ee-c4b1294d6bd4":
                        progName = "������������";
                        type = "dwjcsj";
                        break;

                    case "3630adcc-9d4b-4059-b44e-4f88ccf76b43":
                        progName = "�����滮������";
                        type = "dwghjc";
                        break;
                }


            }

            string s = " UID like '%|" + ProjectUID + "' and Col1='" + progName + "'";
       

           
            ilist = Services.BaseService.GetList("SelectPspTypeListByWhere", s);
            //ilist = Services.BaseService.GetList<PspType>();
            dataTable = DataConverter.ToDataTable(ilist, typeof(PspType));
            treeList1.DataSource = dataTable;

            Econ ed = new Econ();
            ed.UID = "yyy";
            try
            {
                bts = Services.BaseService.GetOneByKey<Econ>(ed).ExcelData;
            }
            catch { }
        
        }


        private void InitForm()
        {
            this.fpSpread1_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
          
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


            ////////else
            ////////{
            ////////    VsmdgroupProg vp = new VsmdgroupProg();
            ////////    try
            ////////    {
            ////////        vp = MIS.GetProgRight("a5ac2d77-e60e-4253-ae10-4a17abf5a89b", "", MIS.UserNumber);

            ////////    }
            ////////    catch { }
            ////////    if (vp.upd == "0")
            ////////    {
            ////////        barEdititem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            ////////        barSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            ////////        editrights = false; ;
            ////////    }

            ////////    if (vp.ins == "0")
            ////////    {
            ////////        barAdd1item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            ////////        barAdditem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            ////////    }

            ////////    if (vp.del == "0")
            ////////        barDelitem.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


            ////////    //barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            ////////}
        }


        private void IsSave()
        {
            if (!editrights)
                return;

            if (MsgBox.ShowYesNo("�����Ѿ������ı䣬�Ƿ񱣴棿") != DialogResult.Yes)
            {
                return;
            }


            WaitDialogForm wait = null;
            PspType obj = Services.BaseService.GetOneByKey<PspType>(uid1);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            try
            {
                wait = new WaitDialogForm("", "���ڱ�������, ���Ժ�...");
                fpSpread1.Save(ms, false);

                obj.Contents = ms.GetBuffer();
                Services.BaseService.Update("UpdatePspTypeBy", obj);
                wait.Close();
                MsgBox.Show("����ɹ�");
                excelstate = false;
            }
            catch
            {
                wait.Close();
                MsgBox.Show("����ʧ��");

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

            excelstate = false;
            string uid = treeList1.FocusedNode["UID"].ToString();
            uid1 = uid;
            PspType obj = Services.BaseService.GetOneByKey<PspType>(uid);

            WaitDialogForm wait = null;
            try
            {
                wait = new WaitDialogForm("", "���ڼ�������, ���Ժ�...");
                System.IO.MemoryStream ms = new System.IO.MemoryStream(obj.Contents);
                //by1 = obj.Contents;
                fpSpread1.Open(ms);
                wait.Close();
       
                fpSpread1.ActiveSheet.Columns[0, fpSpread1.ActiveSheet.Columns.Count - 1].AllowAutoFilter = true;//��ѯ
                fpSpread1.Sheets[0].SetColumnAllowAutoSort(-1, true);//����

            }
            catch{ wait.Close(); }

        }

    


        private void barAdditem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PspType obj = new PspType();
            obj.UID = obj.UID + "|" + ProjectUID;
            obj.Contents = bts;
            obj.ParentID = "";
            obj.CreateDate = DateTime.Now;
            obj.Col1 = progName;
            FrmPspTypeDialog dlg = new FrmPspTypeDialog();
            dlg.Text = progName;
            dlg.Object = obj;
            dlg.IsCreate = true;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
        }

        private void barAdd1item_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;
            string uid = treeList1.FocusedNode["UID"].ToString();


            PspType obj = new PspType();
            obj.UID = obj.UID + "|" + ProjectUID ;
            obj.Contents = bts;
            obj.ParentID = uid;
            obj.CreateDate = DateTime.Now;
            obj.Col1 = progName;
            FrmPspTypeDialog dlg = new FrmPspTypeDialog();
            dlg.Text = progName;
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
                MsgBox.Show("���¼�Ŀ¼������ɾ����");
                return;
            }
            string uid = treeList1.FocusedNode["UID"].ToString();

            //����ȷ��
            if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
            {
                return;
            }

            //ִ��ɾ������
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
                    fpSpread1.Open(ms);
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
            this.fpSpread1.Update();
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
                fpSpread1.Update();
                fpSpread1.Save(ms, false);

                obj.Contents = ms.GetBuffer();
                Services.BaseService.Update("UpdatePspTypeBy", obj);
                excelstate = false;
                MsgBox.Show("����ɹ�");


                

                
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
            Econ ec = new Econ();
            ec.UID = "yyy";



            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            try
            {
                fpSpread1.Save(ms, false);

                ec.ExcelData = ms.GetBuffer();
                Services.BaseService.Create<Econ>(ec);
                //Services.BaseService.Update<Econ>(ec);
            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;
            openFileDialog1.Filter = "Microsoft Excel�ļ� (*.xls)|*.xls";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fpSpread1.OpenExcel(openFileDialog1.FileName);
                    Initex(fpSpread1);
                    fpSpread1.ActiveSheet.Columns[0, fpSpread1.ActiveSheet.Columns.Count - 1].AllowAutoFilter = true;
                    fpSpread1.Sheets[0].SetColumnAllowAutoSort(-1, true);
                }
                catch
                {
                    MsgBox.Show("����ʧ�ܣ�");
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
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = saveFileDialog1.FileName;
                try
                {
                    fpSpread1.SaveExcel(fname);
                    if (MsgBox.ShowYesNo("�����ɹ����Ƿ�򿪸��ĵ���") != DialogResult.Yes)
                        return;

                    System.Diagnostics.Process.Start(fname);
                }
                catch
                {
                    MsgBox.Show("�޷�����" + fname + "�����������ļ��������ļ������ļ���������λ�á�");
                    return;
                }
            }
        }
    }
}