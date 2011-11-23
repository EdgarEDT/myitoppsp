using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using System.Text.RegularExpressions;
using Itop.Domain.Stutistic;
using Itop.Domain.Graphics;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using Itop.Client.Common;
using DevExpress.XtraEditors.Repository;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmProjShow : FormBase
    {
        public FrmProjShow()
        {
            InitializeComponent();
        }
        public List<Ps_Table_BuildPro> ptblist = new List<Ps_Table_BuildPro>();
        private void FrmProjShow_Load(object sender, EventArgs e)
        {
            txtRemark.Text = "说明：\r\n";
            txtRemark.Text += "A类问题：如果被勾选，更新时将在项目管理中添加相应内容\r\n";
            txtRemark.Text += "B类问题：如果被勾选，更新时将以建设项目表中的数据更新对应数据\r\n";
            txtRemark.Text += "C类问题：如果被勾选，更新时将从项目管理删除相应数据\r\n";

            treeList1.DataSource = ptblist;
            int m = 0;
            for (int i = 0; i < treeList1.Columns.Count; i++)
            {
                treeList1.Columns[i].VisibleIndex = -1;
            }
            treeList1.Columns["Title"].Caption = "项目名称";
            treeList1.Columns["Title"].Width = 280;
            treeList1.Columns["Title"].MinWidth = 250;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Title"].VisibleIndex = m++;

            treeList1.Columns["FromID"].Caption = "电压等级";
            treeList1.Columns["FromID"].Width = 70;
            treeList1.Columns["FromID"].VisibleIndex = m++;
            treeList1.Columns["FromID"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["FromID"].OptionsColumn.AllowSort = false;


            treeList1.Columns["AreaName"].Caption = "区域";
            treeList1.Columns["AreaName"].Width = 70;
            treeList1.Columns["AreaName"].MinWidth = 70;
            treeList1.Columns["AreaName"].VisibleIndex = m++;
            treeList1.Columns["AreaName"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["AreaName"].OptionsColumn.AllowSort = false;

            treeList1.Columns["Col3"].Caption = "建设性质";
            treeList1.Columns["Col3"].Width = 80;
            treeList1.Columns["Col3"].MinWidth = 80;
            treeList1.Columns["Col3"].VisibleIndex = 4;
            treeList1.Columns["Col3"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Col3"].OptionsColumn.AllowSort = false;
            treeList1.Columns["Col3"].VisibleIndex = m++;

            treeList1.Columns["AllVolumn"].Caption = "投资金额";
            treeList1.Columns["AllVolumn"].Width = 80;
            treeList1.Columns["AllVolumn"].MinWidth = 80;
            treeList1.Columns["AllVolumn"].Format.FormatString = "n2";
            treeList1.Columns["AllVolumn"].Format.FormatType = DevExpress.Utils.FormatType.Numeric;
            treeList1.Columns["AllVolumn"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["AllVolumn"].OptionsColumn.AllowSort = false;
            treeList1.Columns["AllVolumn"].VisibleIndex = m++;


            RepositoryItemTextEdit repositoryItemTextEdit1 = new RepositoryItemTextEdit();
            repositoryItemTextEdit1.AutoHeight = false;
            repositoryItemTextEdit1.DisplayFormat.FormatString = "n2";
            treeList1.Columns["AllVolumn"].ColumnEdit = repositoryItemTextEdit1;

            treeList1.ExpandAll();
        }
        private void treeList1_GetStateImage(object sender, DevExpress.XtraTreeList.GetStateImageEventArgs e)
        {
            CheckState check = (CheckState)e.Node.GetValue("Sort");
         
            if (check == CheckState.Unchecked)
                e.NodeImageIndex = 0;
            else if (check == CheckState.Checked)
                e.NodeImageIndex = 1;
            else e.NodeImageIndex = 2;
        }

        private void treeList1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
                if (hInfo.HitInfoType == HitInfoType.StateImage)
                {
                    SetCheckedNode(hInfo.Node);
                  
                }
            }
        }
        //当选中项目时改变结点的状态同时变父结点和子结点的状态
        private void SetCheckedNode(TreeListNode node)
        {
            CheckState check = (CheckState)node.GetValue("Sort");
            if (check == CheckState.Indeterminate || check == CheckState.Unchecked) check = CheckState.Checked;
            else check = CheckState.Unchecked;
            treeList1.BeginUpdate();
            node["Sort"] = check;
            //StatusBarDisplayText(treeList1.FocusedNode);
            SetCheckedChildNodes(node, check);
            SetCheckedParentNodes(node, check);
            treeList1.Refresh();
            treeList1.EndUpdate();
        }
        //改变子结点状态
        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
              
                node.Nodes[i]["Sort"] = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }
        //改变父结点状态
        private void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                  
                    if (!CheckState.Checked.Equals((CheckState)node.ParentNode.Nodes[i]["Sort"]))
                    {
                        b = !b;
                        break;
                    }
                }
                node.ParentNode["Sort"] = b ? CheckState.Indeterminate : check;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要执行更新操作？此操作不可恢复","询问",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FrmResultPrintHC2 frm = new FrmResultPrintHC2();
            frm.treelist = treeList1;
            frm.Title = "更新数据列表";
            frm.ShowDialog();
			
		
        }

       
    }
}