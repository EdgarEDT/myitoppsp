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
    public partial class UcPdtype : DevExpress.XtraEditors.XtraUserControl {
        public UcPdtype() {
            InitializeComponent();
        }
        public delegate void SendDataEventHandler<T>(object sender,T obj);
        public event SendDataEventHandler<PDrelregion> FocusedNodeChanged;
       
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e) {
           
            //if (FocusedNodeChanged != null)
            //{
            //    TreeListNode tn = treeList1.FocusedNode;
            //    PDrelregion pdr = new PDrelregion();
            //    pdr.ID = tn["ID"].ToString();
            //    pdr = Services.BaseService.GetOneByKey<PDrelregion>(pdr);
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
                PDrelregion pr = new PDrelregion();
                pr.ProjectID = Itop.Client.MIS.ProgUID;
                IList<PDrelregion> listTypes = Services.BaseService.GetList<PDrelregion>("SelectPDrelregionByProjectID", pr);



                dataTable = Itop.Common.DataConverter.ToDataTable((IList)listTypes, typeof(PDrelregion));
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
            column.FieldName = "AreaName";
            column.Caption = "地区名";
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
            column = new TreeListColumn();
            column.FieldName = "PeopleSum";
            column.Caption = "人口总数";
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});

           
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            PDtypefrmedit PDT = new PDtypefrmedit();
            if (PDT.ShowDialog()==DialogResult.OK)
            {
                PDrelregion pdr = new PDrelregion();
                pdr.ProjectID = Itop.Client.MIS.ProgUID;
                pdr.PeopleSum = PDT.Peplesum;
                pdr.AreaName = PDT.Areaname;
                pdr.Year = PDT.Year;
                Services.BaseService.Create<PDrelregion>(pdr);
                dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pdr, dataTable.NewRow()));
               
                //init();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
             
             if (treeList1.FocusedNode == null)
                return;
              TreeListNode tn=treeList1.FocusedNode;
            DataRow[] dr=dataTable.Select("ID='"+tn["ID"].ToString()+"'");
            PDtypefrmedit PDT = new PDtypefrmedit();
            if (dr[0]!=null)
            {
                PDT.Pdtype = Itop.Common.DataConverter.RowToObject<PDrelregion>(dr[0]);
            }
            if (PDT.ShowDialog() == DialogResult.OK) {
                
                Services.BaseService.Update<PDrelregion>(PDT.Pdtype);
                treeList1.FocusedNode.SetValue("AreaName", PDT.Areaname);
                treeList1.FocusedNode.SetValue("PeopleSum", PDT.Peplesum);
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
                PDrelregion pf = new PDrelregion();
                pf.ID = tln["ID"].ToString();
                string nodestr = tln["AreaName"].ToString();
                try {
                    TreeListNode node = tln.TreeList.FindNodeByKeyID(pf.ID);
                    if (node != null)
                        tln.TreeList.DeleteNode(node);
                    RemoveDataTableRow(dataTable, pf.ID);
                    Itop.Client.Common.Services.BaseService.Delete<PDrelregion>(pf);
                    
                    
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
                PDrelregion pdr = new PDrelregion();
                pdr.ID = tn["ID"].ToString();
                pdr = Services.BaseService.GetOneByKey<PDrelregion>(pdr);
                FocusedNodeChanged(treeList1, pdr);
            }

        }

    }
}
