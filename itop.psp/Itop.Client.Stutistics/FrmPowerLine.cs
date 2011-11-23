using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Domain.Stutistic;
using Itop.Common;
using System.Diagnostics;
using Itop.Domain.RightManager;


namespace Itop.Client.Stutistics
{
    public partial class FrmPowerLine : Itop.Client.Base.FormBase
    {

        DataTable dataTable = new DataTable();
        private bool isSelect = false;
        private string lineName = "";
        private string lineUID = "";
        private string lineParentUID = "";
        private bool lineState = false;

        public bool IsSelect
        {
            get { return isSelect; }
            set { isSelect = value; }
        }

        public string LineName
        {
            get { return lineName; }
            set { lineName = value; }
        }

        public string LineUID
        {
            get { return lineUID; }
            set { lineUID = value; }
        }


        public string LineParentUID
        {
            get { return lineParentUID; }
            set { lineParentUID = value; }
        }

        public bool LineState
        {
            get { return lineState; }
            set { lineState = value; }
        }


        public FrmPowerLine()
        {
            InitializeComponent();
        }

        private void FrmPowerLine_Load(object sender, EventArgs e)
        {
            try 
            {
                dataTable = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList<PowerLine>(), typeof(PowerLine));
                treeList1.DataSource = dataTable;
            }
            catch { }

            Right();
        }

        private void Right()
        {
            try
            {
                VsmdgroupProg vp = new VsmdgroupProg();
                try
                {
                    vp = MIS.GetProgRight("0cf61d9f-a4f4-464a-af23-574a90d2fbba", "", MIS.UserNumber);
                }
                catch { }
                if (vp.upd == "0")
                {
                    barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                if (vp.ins == "0")
                {
                    barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    barAdd1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                if (vp.del == "0")
                    barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                if (vp.prn == "0")
                    barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            catch { }
        
        
        }

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string parentid = "";
            if (treeList1.FocusedNode != null)
            {
                parentid = treeList1.FocusedNode["ParentID"].ToString();
            }

            PowerLine obj = new PowerLine();
            obj.ParentID = parentid;
            FrmPowerLineDialog dlg = new FrmPowerLineDialog();
            dlg.Object = obj;
            dlg.IsCreate = true;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
                
        }

        private void barAdd1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string parentid = "";
            if (treeList1.FocusedNode == null)
                return;
            parentid = treeList1.FocusedNode["UID"].ToString();
            PowerLine obj = new PowerLine();
            obj.ParentID = parentid;
            FrmPowerLineDialog dlg = new FrmPowerLineDialog();
            dlg.Object = obj;
            dlg.IsCreate = true;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            dataTable.Rows.Add(DataConverter.ObjectToRow(obj, dataTable.NewRow()));
            
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
                return;
            string uid = treeList1.FocusedNode["UID"].ToString();
            PowerLine obj = Services.BaseService.GetOneByKey<PowerLine>(uid);
            PowerLine objCopy = new PowerLine();
            DataConverter.CopyTo<PowerLine>(obj, objCopy);

            FrmPowerLineDialog dlg = new FrmPowerLineDialog();
            dlg.Object = objCopy;

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DataConverter.CopyTo<PowerLine>(objCopy, obj);
            treeList1.FocusedNode.SetValue("PowerName", obj.PowerName);
            treeList1.FocusedNode.SetValue("Remark", obj.Remark);
        }

        private void barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                Services.BaseService.DeleteByKey<PowerLine>(uid);
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return;
            }
            this.treeList1.Nodes.Remove(this.treeList1.FocusedNode);
        }

        private void barPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ComponentPrint.ShowPreview(this.treeList1, smmprog.ProgName);
        }

        private void barClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            if (!isSelect)
                return;

            if (treeList1.FocusedNode == null)
                return;
            if (treeList1.FocusedNode["ParentID"].ToString() == "")
                lineState = true;

            lineName = treeList1.FocusedNode["PowerName"].ToString();
            lineUID = treeList1.FocusedNode["UID"].ToString();
            lineParentUID = treeList1.FocusedNode["ParentID"].ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
}