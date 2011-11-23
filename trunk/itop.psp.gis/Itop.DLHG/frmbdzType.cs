using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Domain.Graphics;
using Itop.Client.Common;

namespace Itop.DLGH
{
    public partial class frmbdzType : FormModuleBase
    {
        string typeid = "";
        public frmbdzType()
        {
            InitializeComponent();
        }
        protected override void Add()
        {
            if (!AddRight)
            {
                MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FomInput f = new FomInput();
            if (f.ShowDialog() == DialogResult.OK)
            {
                PSP_bdz_type p = new PSP_bdz_type();
                p.id = Guid.NewGuid().ToString();
                p.Name = f.strName;
                p.col3 = typeid;
                Services.BaseService.Create<PSP_bdz_type>(p);
                TreeNode node = new TreeNode(p.Name);
                node.Tag = p.id;
                treeView1.SelectedNode.Nodes.Add(node);
                ctrlLineType1.Typeid = typeid;
                ctrlLineType1.RefreshData();
            }
        }
        protected override void Edit()
        {
            if (!EditRight)
            {
                MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            PSP_bdz_type p = new PSP_bdz_type();
            p.id = treeView1.SelectedNode.Tag.ToString();
            p = (PSP_bdz_type)Services.BaseService.GetObject("SelectPSP_bdz_typeByKey", p);

            FomInput f = new FomInput();
            f.strName = p.Name;
            if (f.ShowDialog() == DialogResult.OK)
            {
                p.Name = f.strName;
                Services.BaseService.Update("UpdatePSP_bdz_typeNM", p);
                treeView1.SelectedNode.Text = p.Name;
                ctrlLineType1.Typeid = typeid;
                ctrlLineType1.RefreshData();
            }
        }
        protected override void Del()
        {
            if (!DeleteRight)
            {
                MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PSP_interface ins = new PSP_interface();
            ins.col1 = " BdzId like '%"+treeView1.SelectedNode.Tag.ToString()+"%' ";
            IList<PSP_interface> l2 = Services.BaseService.GetList<PSP_interface>("SelectPSP_interfaceByWhere", ins);
            if(l2.Count>0){
                MessageBox.Show("分类下包含数据，不能删除。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(treeView1.SelectedNode.Nodes.Count>0){
                MessageBox.Show("分类下包含子分类，不能删除。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("确定删除么?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                PSP_bdz_type p = new PSP_bdz_type();
                p.id = treeView1.SelectedNode.Tag.ToString();
                Services.BaseService.Update("DeletePSP_bdz_type", p);
                treeView1.Nodes.Remove(treeView1.SelectedNode);
                ctrlLineType1.Typeid = typeid;
                ctrlLineType1.RefreshData();
            }
        }
        protected override void Print()
        {
            ctrlLineType1.PrintPreview();
        }
        public void InitData()
        {
            ctrlLineType1.RefreshData();
        }
        public void LoadTree(TreeNode _root, string id)
        {
            PSP_bdz_type p = new PSP_bdz_type();
            p.col1 = " col3='" + id + "' order by Name";
            IList<PSP_bdz_type> list = Services.BaseService.GetList<PSP_bdz_type>("SelectPSP_bdz_typeByWhere", p);
            for (int i = 0; i < list.Count; i++)
            {
                PSP_bdz_type _type = list[i];
                TreeNode node = new TreeNode(_type.Name);
                node.Tag = _type.id;
                LoadTree(node, _type.id);
                _root.Nodes.Add(node);
               
            }
        }
        private void frmglebeType_Load(object sender, EventArgs e)
        {
            LoadTree(treeView1.Nodes[0], "0");
            
            InitData();
            ctrlLineType1.AllowUpdate = EditRight;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            typeid = treeView1.SelectedNode.Tag.ToString();
            ctrlLineType1.Typeid = typeid;
            ctrlLineType1.RefreshData();
        }

        private void 增加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            FomInput f = new FomInput();
            if (f.ShowDialog() == DialogResult.OK)
            {
                PSP_bdz_type p = new PSP_bdz_type();
                p.id = Guid.NewGuid().ToString();
                p.Name = f.strName;
                p.col3 = typeid;
                Services.BaseService.Create<PSP_bdz_type>(p);
                TreeNode node = new TreeNode(p.Name);
                node.Tag = p.id;
                treeView1.SelectedNode.Nodes.Add(node);
                ctrlLineType1.Typeid = typeid;
                ctrlLineType1.RefreshData();
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除么?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information)==DialogResult.Yes)
            {
                PSP_bdz_type p=new PSP_bdz_type();
                p.id=treeView1.SelectedNode.Tag.ToString();
                Services.BaseService.Update("DeletePSP_bdz_type",p);
                treeView1.Nodes.Remove(treeView1.SelectedNode);
                ctrlLineType1.Typeid = typeid;
                ctrlLineType1.RefreshData();
            }
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PSP_bdz_type p = new PSP_bdz_type();
            p.id=treeView1.SelectedNode.Tag.ToString();
            p = (PSP_bdz_type)Services.BaseService.GetObject("SelectPSP_bdz_typeByKey", p);

            FomInput f = new FomInput();
            f.strName = p.Name;
            if (f.ShowDialog() == DialogResult.OK)
            {      
                p.Name = f.strName;
                Services.BaseService.Update<PSP_bdz_type>(p);
                treeView1.SelectedNode.Text = p.Name;
                ctrlLineType1.Typeid = typeid;
                ctrlLineType1.RefreshData();
            }
        }
    }
}