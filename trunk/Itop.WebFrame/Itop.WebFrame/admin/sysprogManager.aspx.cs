using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Itop.Frame.Model;

namespace Itop.WebFrame {
    public partial class sysprogManager : GridPageBase<sysprog> {
        protected override void Page_Load(object sender, EventArgs e) {
            DataStore = Store1;
            base.Page_Load(sender, e);
            
        }
        //private void BindTreeData() {
        //    if (!X.IsAjaxRequest) {
        //        refreshTree();
        //    }
        //}
        //protected void refreshTree() {
        //    List<sysprog> list = this.CurrentData;
        //    IEnumerable<sysprog> list2= list.Where(prog=>prog.ParentID=="root" && prog.IsGroup=="1" );
        //    createTree((TreePanel1.Root[0] as Ext.Net.TreeNode).Nodes,list2);
        //}

        //private void createTree(Ext.Net.TreeNodeCollection tc, IEnumerable<sysprog> list2) {
        //    foreach (sysprog item in list2) {
        //        Ext.Net.TreeNode node = new Ext.Net.TreeNode(item.ProgName) { NodeID=item.id };
        //        tc.Add(node);
        //        createTree(node.Nodes,this.CurrentData.Where(prog=>prog.ParentID==item.id && prog.IsGroup=="1" ));
        //    }
        //}
        protected void GetNodes(object sender, NodeLoadEventArgs e) {
            e.Nodes = getNodes(e.NodeID);
        }
        private Ext.Net.TreeNodeCollection getNodes( string pid) {
             Ext.Net.TreeNodeCollection nodes=new Ext.Net.TreeNodeCollection();
             IEnumerable<sysprog> list2 = CurrentData.Where(prog => prog.ParentID == pid);
             foreach (sysprog item in list2) {
                 Ext.Net.TreeNode node = new Ext.Net.TreeNode(item.ProgName) { NodeID = item.id };
                 if (item.IsGroup != "1") {
                     node.Leaf = true;
                 }
                 nodes.Add(node);
             }
             return nodes;
        }
        protected override void Data_Refresh(object sender, StoreRefreshDataEventArgs e) {
            string pid=e.Parameters["ParentID"];

            Store1.DataSource = CurrentData.Where(p => p.ParentID == pid);
            Store1.DataBind();
           
        }
        protected override void InitGridColumn() {
            //GridPanel1.ColumnModel.Columns.Clear();
            ColumnCollection columns = GridPanel1.ColumnModel.Columns;
            ColumnBase col = new RowNumbererColumn();
            //columns.Add(col);
            col = new NumberColumn() { Header = "排序", DataIndex = "orderID", Width = 50, Fixed = true };
            (col as NumberColumn).Format = "0";
            col.Editor.Add(new NumberField());            
            columns.Add(col);
            col = new Column() { Header = "代码", DataIndex = "ProgCode", Width = 80, Fixed = true  };
            col.Editor.Add(new TextField());
            columns.Add(col);
            col = new Column() { Header = "名称", DataIndex = "ProgName" };
            col.Editor.Add(new TextField());
            columns.Add(col);
            col = new Column() { Header = "页面", DataIndex = "ProgClass" };
            col.Editor.Add(new TextField());
            columns.Add(col);
            col = new Column() { Header = "小图标", DataIndex = "ProgIcon1", Width = 50, Fixed = true };
            col.Editor.Add(new TextField());
            columns.Add(col);
            col = new Column() { Header = "大图标", DataIndex = "ProgIcon2", Width = 50, Fixed = true };
            col.Editor.Add(new TextField());
            columns.Add(col);
            col = new Column() { Header = "分类", DataIndex = "IsCore", Width = 40, Fixed = true };
            col.Editor.Add(new TextField());
            columns.Add(col);
            col = new Column() { Header = "组", DataIndex = "IsGroup", Width = 30, Fixed = true };
            col.Editor.Add(new TextField());
            columns.Add(col);
            

        }
    }
}
