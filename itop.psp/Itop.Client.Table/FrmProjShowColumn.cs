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
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmProjShowColumn : FormBase
    {
        public FrmProjShowColumn()
        {
            InitializeComponent();
        }
       public  DataTable dt = new DataTable();
        public DevExpress.XtraTreeList.TreeList treelist;

        private void FrmProjShowColumn_Load(object sender, EventArgs e)
        {

            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Sort", typeof(int));
            dt.Columns.Add("width", typeof(int));
            int n= 0;
            for (int i = 0; i < treelist.Columns.Count; i++)
            {
                if (treelist.Columns[i].VisibleIndex>-1)
                {
                    n++;
                    DataRow temprow = dt.NewRow();
                    temprow["ID"] = treelist.Columns[i].FieldName;
                    temprow["Title"] = treelist.Columns[i].Caption;
                    if (n<=8)
                    {
                         temprow["Sort"] = 1;
                    }
                    else
                    {
                        temprow["Sort"] = 0;
                    }
                    
                    temprow["width"] = treelist.Columns[i].Width;
                    dt.Rows.Add(temprow);
                }
            }
            treeList1.DataSource = dt;
            for (int i = 0; i < treeList1.Columns.Count; i++)
            {
                treeList1.Columns[i].VisibleIndex = -1;
            }
            int m = 0;
           
            //treeList1.Columns["ID"].VisibleIndex =-1;

            treeList1.Columns["Title"].Caption = "列名称";
            treeList1.Columns["Title"].Width = 300;
            treeList1.Columns["Title"].MinWidth = 300;
            
            treeList1.Columns["Title"].VisibleIndex = m++;
            treeList1.Columns["Title"].OptionsColumn.AllowEdit = false;
            treeList1.Columns["Title"].OptionsColumn.AllowSort = false;

           

           
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
           
                DialogResult = DialogResult.OK;
                this.Close();
            
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Sort"] = 1;
            }
            treelist.RefreshDataSource();
        }

       

       
    }
}