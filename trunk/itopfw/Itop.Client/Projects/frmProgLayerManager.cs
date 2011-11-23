using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Projects;
using Itop.Domain.Graphics;
using System.Configuration;
using Itop.Client.Base;

namespace Itop.Client.Projects
{
    public partial class frmProgLayerManager : FormBase
    {
        string suid = "";
        public string progid = "";
        IList<LayerGrade> list = null;
        public frmProgLayerManager()
        {
            InitializeComponent();
        }

        private void frmProgLayerManager_Load(object sender, EventArgs e)
        {
            suid = ConfigurationSettings.AppSettings.Get("SvgID");
            LayerGrade g = new LayerGrade();
            g.ParentID = "SUID";
            g.SvgDataUid = suid;
            list= Services.BaseService.GetList<LayerGrade>("SelectLayerGradeListBySvgDataUid2", g);

            for (int i = 0; i < list.Count;i++ )
            {
                TreeNode node = new TreeNode();
                node.Tag = list[i].SUID;
                node.Text = list[i].Name;
                LayerGrade _g = new LayerGrade();
                _g.ParentID = list[i].SUID;
                _g.SvgDataUid = suid;
                IList<LayerGrade> _list = Services.BaseService.GetList<LayerGrade>("SelectLayerGradeListBySvgDataUid2", _g);
                for (int j = 0; j < _list.Count; j++)
                {
                    TreeNode _node = new TreeNode();
                    _node.Tag = _list[j].SUID;
                    _node.Text = _list[j].Name;
                    node.Nodes.Add(_node);
                }
                treeView1.Nodes.Add(node);

            }
            Psp_ProgLayerList p2 = new Psp_ProgLayerList();
            p2.ProgUID = progid;
            IList<Psp_ProgLayerList> plist = Services.BaseService.GetList<Psp_ProgLayerList>("SelectPsp_ProgLayerListListByProgUID", p2);
            treeView1.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterCheck);
            foreach(TreeNode tnode in treeView1.Nodes){
                if (GetNodeCheck(tnode, plist))
                {
                    tnode.Checked = true;
                    foreach(TreeNode tnode2 in tnode.Nodes){
                        if (GetNodeCheck(tnode2, plist))
                        {
                            tnode2.Checked = true;
                        }
                    }
                }
            }
            treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterCheck);
        }
        public bool GetNodeCheck(TreeNode node,IList<Psp_ProgLayerList> plist)
        {
            bool ck = false;
            for (int i = 0; i < plist.Count;i++ )
            {
                if (node.Tag.ToString() == plist[i].LayerGradeID)
                {
                    ck = true;
                    break;
                }
            }
            return ck;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Psp_ProgLayerList p1=new Psp_ProgLayerList();
            p1.ProgUID=progid;
            Services.BaseService.Update("DeletePsp_ProgLayerListByPID",p1);
            
            foreach(TreeNode node in treeView1.Nodes){
                if (node.Checked == true)
                {
                    
                    Psp_ProgLayerList obj = new Psp_ProgLayerList();
                    obj.UID = Guid.NewGuid().ToString();
                    obj.ProgUID = progid;
                    obj.LayerGradeID = node.Tag.ToString();
                    obj.col1 = node.Text;
                    Services.BaseService.Create<Psp_ProgLayerList>(obj);
                    foreach (TreeNode node2 in node.Nodes)
                    {
                        if (node2.Checked == true)
                        {
                            Psp_ProgLayerList obj2 = new Psp_ProgLayerList();
                            obj2.UID = Guid.NewGuid().ToString();
                            obj2.ProgUID = progid;
                            obj2.LayerGradeID = node2.Tag.ToString();
                            obj2.col1 = node2.Text;
                            Services.BaseService.Create<Psp_ProgLayerList>(obj2);
                        }
                    }
                }
            }
          
            this.Close();
        }
        private void tree_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {

            TreeView tree = (TreeView)sender;
            tree.AfterCheck -= new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterCheck);

            //折叠其他同级节点   
            TreeNode parentNode = e.Node.Parent;
            if (parentNode != null)
            {
                foreach (TreeNode tn in parentNode.Nodes)
                {
                    if (tn != e.Node)
                        tn.Collapse();
                }
            }
            else
            {
                foreach (TreeNode tn in tree.Nodes)
                {
                    if (tn != e.Node)
                        tn.Collapse();
                }
            }

            //标记该节点的所有子节点的选中状态与该节点一致   
            foreach (TreeNode tn in e.Node.Nodes)
                tn.Checked = e.Node.Checked;

            //if   (e.Node.Checked)   
            e.Node.ExpandAll();  
        
            if (e.Node.Checked == false && e.Node.Parent != null)
            {
                bool found = false; //父节点的子节点中至少有一个节点被选中，则found   =   true   
                foreach (TreeNode tn in e.Node.Parent.Nodes)
                {
                    if (tn.Checked == true)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false) //没有找到任何被选中的子节点   
                {
                    e.Node.Parent.Checked = false;
                   // e.Node.Parent.Collapse();
                }
            }
            if (e.Node.Checked == true && e.Node.Parent != null)
            {
                e.Node.Parent.Checked = true;
            }
            tree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterCheck);
        }   

    }
}