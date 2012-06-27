using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using Itop.Common;
using System.Diagnostics;
using ItopVector.Core.Figure;
using System.Xml;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Base;
namespace ItopVector.Tools {
    public partial class frmGrade : FormBase {
        private IList ilist = new ArrayList();
        DataTable dataTable = new DataTable();
        private string strSvgDataUid = "";
        private ItopVector.Core.Document.SvgDocument symbolDoc;
        public string type = "";
        public frmGrade() {
            InitializeComponent();

        }
        public void CK() {
            for (int i = 0; i < ilist.Count; i++) {
                if (((LayerGrade)ilist[i]).Type == type) {
                    treeList1.GetNodeByVisibleIndex(i).Tag = CheckState.Checked;
                }
            }
            treeList1.Refresh();
        }
        public void InitData() {
            DevExpress.XtraTreeList.Design.XViews xv = new DevExpress.XtraTreeList.Design.XViews(treeList1);
            treeList1.SelectImageList = null;
            try {
                SetCheckedNode(treeList1.Nodes[0].Nodes[0]);
            } catch { }
        }

        private CheckState GetCheckState(object obj) {
            if (obj != null) return (CheckState)obj;
            return CheckState.Unchecked;
        }
        //<treeList1>
        private void SetCheckedNode(DevExpress.XtraTreeList.Nodes.TreeListNode node) {
            CheckState check = GetCheckState(node.Tag);
            if (check == CheckState.Indeterminate || check == CheckState.Unchecked) {
                check = CheckState.Checked;

            } else check = CheckState.Unchecked;
            treeList1.BeginUpdate();
            node.Tag = check;
            SetCheckedChildNodes(node, check);
            SetCheckedParentNodes(node, check);
            treeList1.EndUpdate();
        }
        //</treeList1>

        private void SetCheckedChildNodes(DevExpress.XtraTreeList.Nodes.TreeListNode node, CheckState check) {
            for (int i = 0; i < node.Nodes.Count; i++) {
                node.Nodes[i].Tag = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }
        private void SetCheckedParentNodes(DevExpress.XtraTreeList.Nodes.TreeListNode node, CheckState check) {
            if (node.ParentNode != null) {
                bool b = false;
                CheckState state;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++) {
                    if (node.ParentNode.Nodes[i].Tag == null) state = CheckState.Unchecked;
                    else state = (CheckState)node.ParentNode.Nodes[i].Tag;
                    if (!check.Equals(state)) {
                        b = !b;
                        break;
                    }
                }
                node.ParentNode.Tag = b ? CheckState.Indeterminate : check;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }


        public string GetSelectNode() {
            string id = "'',";
            for (int i = 0; i < treeList1.Nodes.Count; i++) {
                //if ( treeList1.Nodes[i].Tag!=null && treeList1.Nodes[i].Tag.ToString()=="Checked")
                //{
                for (int j = 0; j < treeList1.Nodes[i].Nodes.Count; j++) {
                    if (treeList1.Nodes[i].Nodes[j].Tag != null && treeList1.Nodes[i].Nodes[j].Tag.ToString() == "Checked") { id = id + treeList1.Nodes[i].Nodes[j].GetValue(treeListColumn2).ToString() + ","; }
                }
                if (treeList1.Nodes[i].Tag != null && treeList1.Nodes[i].Tag.ToString() == "Checked") {
                    id = id + treeList1.Nodes[i].GetValue(treeListColumn2).ToString() + ",";
                }
                //}

            }
            //for (int i = 0; i<treeList1.Selection.Count; i++)
            //{
            //    id =id+ "'"+treeList1.Selection[i].GetValue(treeListColumn2).ToString()+"',";
            //}
            return id.Substring(0, id.Length - 1);
        }
        public frmGrade(string svgDataUid) {
            strSvgDataUid = svgDataUid;
            treeList1.BeginUpdate();
            InitializeComponent();
            InitData(svgDataUid);
            treeList1.EndUpdate();

        }
        public void InitData(string svgDataUid) {
            strSvgDataUid = svgDataUid;
            LayerGrade lg = new LayerGrade();
            lg.SvgDataUid = svgDataUid;
            lg.ParentID = "SUID";
            ilist = Services.BaseService.GetList("SelectLayerGradeListBySvgDataUid2", lg);
            //ilist = Services.BaseService.GetList<PspType>();
            dataTable = DataConverter.ToDataTable(ilist, typeof(LayerGrade));


            treeList1.DataSource = dataTable;
            Application.DoEvents();

        }
        private void LayerGradeAdd(object sender, EventArgs e) {
            string uid = "";
            if (treeList1.FocusedNode != null) {
                uid = treeList1.FocusedNode[treeListColumn2].ToString();
            }
            frmLayerGradeInPut dlg = new frmLayerGradeInPut(strSvgDataUid);
            LayerGrade objnew = new LayerGrade();
            objnew.SUID = Guid.NewGuid().ToString();
            objnew.SvgDataUid = strSvgDataUid;
            //objnew.ParentID = uid;            

            if (dlg.ShowDialog() == DialogResult.OK) {
                objnew.Name = dlg.TextInPut;
                objnew.ParentID = dlg.ParentID;
            } else {
                return;
            }
            if (objnew.Name == "") {
                MessageBox.Show("分级名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (LayerGrade lay in ilist) {
                if (objnew.Name == lay.Name) {
                    MessageBox.Show("分级已经存在。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            dataTable.Rows.Add(DataConverter.ObjectToRow(objnew, dataTable.NewRow()));
            Services.BaseService.Create<LayerGrade>(objnew);
            InitData(strSvgDataUid);
        }
        private void LayerGradeChange(object sender, EventArgs e) {
            if (treeList1.FocusedNode == null) {
                return;
            }
            string uid = treeList1.FocusedNode[treeListColumn2].ToString();
            LayerGrade obj = Services.BaseService.GetOneByKey<LayerGrade>(uid);
            frmLayerGradeInPut dlg = new frmLayerGradeInPut(strSvgDataUid);
            dlg.TextInPut = treeList1.FocusedNode[treeListColumn1].ToString();
            //LayerGrade objParent = Services.BaseService.GetOneByKey<LayerGrade>((string)treeList1.FocusedNode[treeListColumn4]);
            dlg.ParentID = treeList1.FocusedNode[treeListColumn4].ToString();
            if (obj != null) {
                dlg.textBoxEnabled = true;
            } else {
                dlg.textBoxEnabled = false;
            }

            if (dlg.ShowDialog() == DialogResult.OK) {
                if (dlg.TextInPut == "") {
                    MessageBox.Show("分级名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                SVG_LAYER lar = new SVG_LAYER();
                lar.svgID = strSvgDataUid;
                lar.SUID = uid;
                lar = (SVG_LAYER)Services.BaseService.GetObject("SelectSVG_LAYERByKey", lar);
                if (lar != null) {
                    lar.YearID = dlg.ParentID;
                    Services.BaseService.Update<SVG_LAYER>(lar);
                }
                if (obj != null) {
                    obj.Name = dlg.TextInPut;
                    obj.ParentID = dlg.ParentID;

                    foreach (LayerGrade lay in ilist) {
                        if (obj.Name == lay.Name && obj.Name != treeList1.FocusedNode[treeListColumn1].ToString()) {
                            MessageBox.Show("分级已经存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    if (obj.ParentID == obj.SUID) {
                        MessageBox.Show("不能将分级设置成自己的子级！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    treeList1.FocusedNode.SetValue("Name", obj.Name);
                    treeList1.FocusedNode.SetValue("ParentID", obj.ParentID);
                    Services.BaseService.Update("UpdateLayerGrade", obj);
                } else {
                    Layer lay = SymbolDoc.GetLayerByID(uid);
                    treeList1.FocusedNode.SetValue("Name", dlg.TextInPut);
                    treeList1.FocusedNode.SetValue("ParentID", dlg.ParentID);
                    //lay.Label = dlg.TextInPut;              
                    lay.SetAttribute("ParentID", dlg.ParentID);

                }
            } else {
                return;
            }
            InitData(strSvgDataUid);
        }
        private void LayerGradeDel(object sender, EventArgs e) {
            if (treeList1.FocusedNode == null)
                return;
            IList list = this.SymbolDoc.getLayerList();
            foreach (Layer lay in list) {
                if (treeList1.FocusedNode[treeListColumn2].ToString() == lay.ID) {
                    return;
                }
            }
            if (treeList1.FocusedNode.Nodes.Count > 0) {
                MsgBox.Show("有下级目录，不能删除！");
                return;
            }
            string uid = treeList1.FocusedNode[treeListColumn2].ToString();
            SVG_LAYER lar = new SVG_LAYER();
            lar.svgID = strSvgDataUid;
            lar.YearID = " YearID='" + uid + "' ";
            IList<SVG_LAYER> list4 = Services.BaseService.GetList<SVG_LAYER>("SelectSVG_LAYERByWhere", lar);
            if (list4.Count > 0) {
                MsgBox.Show("当前分级下有图形数据，不能删除！");
                return;
            }

            //请求确认
            if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes) {
                return;
            }

            //执行删除操作
            try {
                Services.BaseService.DeleteByKey<LayerGrade>(uid);
            } catch (Exception exc) {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return;
            }
            this.treeList1.Nodes.Remove(this.treeList1.FocusedNode);
            InitData(strSvgDataUid);
        }
        public void SetLayerAttribute() {
            foreach (Layer layer in SymbolDoc.getLayerList()) {
                layer.SetAttribute("IsSelect", "false");
            }
            for (int i = 0; i < treeList1.Selection.Count; i++) {
                foreach (Layer lay in SymbolDoc.getLayerList()) {
                    if (((lay.GetAttribute("ParentID") == treeList1.Selection[i].GetValue(treeListColumn2).ToString()) || lay.GetAttribute("id") == treeList1.Selection[i].GetValue(treeListColumn2).ToString()) && (lay.GetAttribute("layerType") == "电网规划层")) {
                        lay.SetAttribute("IsSelect", "true");
                    } else {
                        if (lay.GetAttribute("layerType") == "电网规划层") {
                            lay.Visible = false;
                        }
                    }
                }
            }
        }
        //public IList layerList
        //{            
        //    get
        //    {   
        //        IList list=new ArrayList();               
        //        for (int i=0;i<treeList1.Selection.Count;i++)
        //        {
        //            list.Add(treeList1.Selection[i].GetValue(treeListColumn1));
        //        }                
        //        return list; 
        //    }
        //}
        public ItopVector.Core.Document.SvgDocument SymbolDoc {
            get {
                return this.symbolDoc;
            }
            set {
                this.symbolDoc = value;
            }
        }

        private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e) {
            CheckState check = GetCheckState(e.Node.Tag);
            if (check == CheckState.Unchecked)
                e.NodeImageIndex = 0;
            else if (check == CheckState.Checked)
                e.NodeImageIndex = 1;
            else e.NodeImageIndex = 2;
        }

        private void treeList1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.Space)
                SetCheckedNode(treeList1.FocusedNode);
        }

        private void treeList1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                DevExpress.XtraTreeList.TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
                if (hInfo.HitInfoType == HitInfoType.StateImage || hInfo.HitInfoType == HitInfoType.Cell)

                    SetCheckedNode(hInfo.Node);
            }
        }

        private void simpleButtonOK_Click(object sender, EventArgs e) {
            //if (f.ShowDialog() == DialogResult.OK)
            //{
            string sel = GetSelectNode();
            LayerGrade lg = new LayerGrade();
            lg.SvgDataUid = strSvgDataUid;
            lg.Type = type;
            IList list1 = Services.BaseService.GetList("SelectLayerGradeList5", lg);
            if (list1.Count > 0) {
                foreach (LayerGrade ld in list1) {
                    ld.Type = "";
                    Services.BaseService.Update<LayerGrade>(ld);
                }
            }
            string[] id = sel.Split(",".ToCharArray());
            //消除原来的


            if (id.Length > 1) {
                for (int i = 1; i < id.Length; i++) {
                    LayerGrade _l = Services.BaseService.GetOneByKey<LayerGrade>(id[i]);
                    _l.Type = type;
                    Services.BaseService.Update<LayerGrade>(_l);
                }
            }
            //}
            this.Close();
        }

        private void simpleButtonCancle_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}