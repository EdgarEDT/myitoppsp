using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Client.Base;

namespace Itop.Client
{
    public partial class FrmDirTree : FormBase
    {
        public string SelectPaht;
        public   SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
        public FrmDirTree()
        {
            InitializeComponent();
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            SelectPaht = txtpath.Text.Trim();
            if (SelectPaht=="")
            {
                MessageBox.Show("请选择路径！");
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void FrmDirTree_Load(object sender, EventArgs e)
        {
            IntiData();
        }
        private void IntiData()
        {
        //     EnumAvailableMedia(SQLDMO.SQLDMO_MEDIA_TYPE)：列举对SQL Server可见的媒介质，参数是一个枚举类型，用于指定方法返回的类型，分别是
        //_All                            返回所有类型
        //SQLDMOMedia_CDROM                   返回光驱列表
        //SQLDMOMedia_FixedDisk                 返回硬盘列表
          SQLDMO.QueryResults disk=svr.EnumAvailableMedia(SQLDMO.SQLDMO_MEDIA_TYPE.SQLDMOMedia_FixedDisk);
          for (int i = 1; i < disk.Rows; i++)
          {
              TreeNode newnode = new TreeNode();
              newnode.Text = disk.GetColumnString(i, 1);
              newnode.Tag = newnode.Text;
              newnode.StateImageIndex = 0;
              treeView1.Nodes.Add(newnode);
              
          }
        }
        private void AddTree(TreeNode node, SQLDMO.QueryResults result)
        {
            for (int i = 1; i < result.Rows; i++)
            {
                TreeNode newnode = new TreeNode();
                newnode.Text = result.GetColumnString(i, 1);
                if (!node.Tag.ToString().EndsWith("\\"))
                {
                    newnode.Tag = node.Tag + "\\" + newnode.Text;
                }
                else
                {
                    newnode.Tag = node.Tag + newnode.Text;
                }
                newnode.StateImageIndex = 1;
                node.Nodes.Add(newnode);
            }

        }
        private void AddTreeAll(TreeNode node, SQLDMO.QueryResults result)
        {
            for (int i = 1; i < result.Rows; i++)
            {
                TreeNode newnode = new TreeNode();
                newnode.Text = result.GetColumnString(i, 1);
                if (!node.Tag.ToString().EndsWith("\\"))
                {
                     newnode.Tag = node.Tag +"\\"+ newnode.Text;
                }
                else
                {
                    newnode.Tag = node.Tag + newnode.Text;
                }
                newnode.StateImageIndex = 1;
                AddTree(newnode, svr.EnumDirectories(newnode.Tag.ToString()));
                node.Nodes.Add(newnode);
            }
           
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
                try
                {
                    SQLDMO.QueryResults result = svr.EnumDirectories(e.Node.Tag.ToString());
                    AddTree(e.Node, result);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            txtpath.Text = treeView1.SelectedNode.Tag.ToString();
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count==0)
            {
                try
                {
                    SQLDMO.QueryResults result = svr.EnumDirectories(e.Node.Tag.ToString());
                    AddTree(e.Node, result);
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
            }

        }
    }
}