using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using System.Collections;
using Itop.Common;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.IO;
using Itop.Client.Stutistics;
using System.Xml;
using ItopVector.Tools;
using Itop.Client.Base;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
namespace Itop.TLPsp.Graphical {
    public partial class UcPdreltype : DevExpress.XtraEditors.XtraUserControl {
        public UcPdreltype()
        {
            InitializeComponent();
        }
        public delegate void SendDataEventHandler<T>(object sender,T obj);
        public event SendDataEventHandler<Ps_pdreltype> FocusedNodeChanged;
       
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e) {
           
            //if (FocusedNodeChanged != null)
            //{
            //    TreeListNode tn = treeList1.FocusedNode;
            //    Ps_pdreltype pdr = new Ps_pdreltype();
            //    pdr.ID = tn["ID"].ToString();
            //    pdr = Services.BaseService.GetOneByKey<Ps_pdreltype>(pdr);
            //    FocusedNodeChanged(treeList1, pdr);
            //}

               
        }

        DataTable dataTable = new DataTable();
        public void init()
        {
            try {
                if (dataTable != null) {
                    dataTable.Columns.Clear();
                    treeList1.Columns.Clear();
                }
                AddFixColumn();
                Ps_pdreltype pr = new Ps_pdreltype();
                pr.ProjectID = Itop.Client.MIS.ProgUID;
                IList<Ps_pdreltype> listTypes = Services.BaseService.GetList<Ps_pdreltype>("SelectPs_pdreltypeByProjectID", pr);



                dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(Ps_pdreltype));
                treeList1.BeginInit();
                treeList1.DataSource = dataTable;

                //treeList1.Columns["Sort"].SortOrder = SortOrder.Ascending;
                treeList1.EndInit();
                Application.DoEvents();
                treeList1.ExpandAll();
            } catch (System.Exception ex) {

            }
            
            
          
        }
        private void AddFixColumn()
        {
            TreeListColumn column = new TreeListColumn();
            column.FieldName = "Title";
            column.Caption = "项目名称";
            column.VisibleIndex = 0;
            column.Width = 210;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            column = new TreeListColumn();
            column.FieldName = "ID";
            column.VisibleIndex = -1;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            

           
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            PDreltypefrmedit PDT = new PDreltypefrmedit();
            if (PDT.ShowDialog()==DialogResult.OK)
            {
                Ps_pdreltype pdr = new Ps_pdreltype();
                pdr.ProjectID = Itop.Client.MIS.ProgUID;
                pdr.Createtime = DateTime.Now;
                pdr.Title = PDT.Title;
                pdr.S1 = PDT.S1;
                //pdr.PeopleSum = PDT.Peplesum;
                //pdr.AreaName = PDT.Areaname;
                //pdr.Year = PDT.Year;
                Services.BaseService.Create<Ps_pdreltype>(pdr);
                //创建电源
                Ps_pdtypenode pn = new Ps_pdtypenode();
                pn.pdreltypeid = pdr.ID;
                pn.devicetype = "01";
                PSPDEV devzx = new PSPDEV();
                devzx.SUID =pdr. S1;
                devzx = Services.BaseService.GetOneByKey<PSPDEV>(devzx);
                if (devzx != null)
                {
                    pn.title = devzx.Name;
                    pn.DeviceID = devzx.SUID;
                }
                else
                {
                    pn.title = pdr.Title;

                }
                pn.Code = "0";
                Services.BaseService.Create<Ps_pdtypenode>(pn);
                dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, dataTable.NewRow()));
               
                //init();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
             
             if (treeList1.FocusedNode == null)
                return;
              TreeListNode tn=treeList1.FocusedNode;
            DataRow[] dr=dataTable.Select("ID='"+tn["ID"].ToString()+"'");
            PDreltypefrmedit PDT = new PDreltypefrmedit();
            if (dr[0]!=null)
            {
                PDT.Pdtype = Itop.Common.DataConverter.RowToObject<Ps_pdreltype>(dr[0]);
            }
            if (PDT.ShowDialog() == DialogResult.OK) {
                
                Services.BaseService.Update<Ps_pdreltype>(PDT.Pdtype);
                treeList1.FocusedNode.SetValue("Title", PDT.Title);
                  IList<Ps_pdtypenode>  list1 = Services.BaseService.GetList<Ps_pdtypenode>("SelectPs_pdtypenodeByCon", "pdreltypeid='" + PDT.Pdtype.ID+ "'and devicetype='01'");
               if (list1.Count>0)
               {
                   Ps_pdtypenode pn = list1[0];
                   PSPDEV devzx = new PSPDEV();
                   devzx.SUID = PDT.Pdtype.S1;
                   devzx = Services.BaseService.GetOneByKey<PSPDEV>(devzx);
                   if (devzx != null)
                   {
                       pn.title = devzx.Name;
                       pn.DeviceID = devzx.SUID;
                   }
                   else
                   {
                       pn.title = PDT.Pdtype.Title;

                   }
                   Services.BaseService.Create<Ps_pdtypenode>(pn);
               }
               
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            TreeListNode tln = treeList1.FocusedNode;
            if (tln == null)
                return;
           
            if (tln.HasChildren) {
                if (MsgBox.ShowYesNo(tln.GetValue("AreaName") + "该分类有子类，如果删除将会同子类一起删除" + "？") == DialogResult.Yes) {
                    DeleteNode(tln);
                } else {
                    return;
                }

            } else {
                if (MsgBox.ShowYesNo("是否删除分类 " + tln.GetValue("Title") + "？") == DialogResult.Yes) {
                    DeleteNode(tln);
                } else {
                    return;
                }
            }
        }
        //删除结点
        public void DeleteNode(TreeListNode tln) {
            if (tln.HasChildren) {
                for (int i = 0; i < tln.Nodes.Count; i++) {
                    DeleteNode(tln.Nodes[i]);
                }
                DeleteNode(tln);
            } else {
                Ps_pdreltype pf = new Ps_pdreltype();
                pf.ID = tln["ID"].ToString();
                string nodestr = tln["Title"].ToString();
                try {
                    TreeListNode node = tln.TreeList.FindNodeByKeyID(pf.ID);
                    if (node != null)
                        tln.TreeList.DeleteNode(node);
                    RemoveDataTableRow(dataTable, pf.ID);
                    Ps_pdtypenode pn = new Ps_pdtypenode();
                    pn.pdreltypeid = pf.ID;
                    Itop.Client.Common.Services.BaseService.Update("DeletePs_pdtypepdreltypeid", pn);
                    Itop.Client.Common.Services.BaseService.Delete<Ps_pdreltype>(pf);
                    
                    
                    
                    
                } catch (Exception e) {

                    MessageBox.Show(e.Message + "删除结点出错！");
                }
               
            }


        }
        public void RemoveDataTableRow(DataTable dt, string ID) {
            for (int i = 0; i < dt.Rows.Count; i++) {
                if (dt.Rows[i]["ID"].ToString() == ID) {
                    dt.Rows.RemoveAt(i);
                    break;
                }
            }
        }

        //private void UcPdtype_Load(object sender, EventArgs e) {
        //    init();
        //}

        private void treeList1_MouseClick(object sender, MouseEventArgs e) {
            if (FocusedNodeChanged != null) {
                TreeListNode tn = treeList1.FocusedNode;
                Ps_pdreltype pdr = new Ps_pdreltype();
                pdr.ID = tn["ID"].ToString();
                pdr = Services.BaseService.GetOneByKey<Ps_pdreltype>(pdr);
                FocusedNodeChanged(treeList1, pdr);
            }

        }

    }
}
