using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.Collections;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmSelectSubAllByYear : FormBase
    {
        private string yearid = "";
        private string power = "";
        public substation substat=null;
        public string Power
        {
            get { return power; }
            set { power = value; }
        }
        public string Yearid
        {
            get { return yearid; }
            set { yearid = value; }
        }

        public frmSelectSubAllByYear()
        {
            InitializeComponent();
        }

        private void frmSelectLineByYear_Load(object sender, EventArgs e)
        {
            InitDate();
        }
        public void InitDate()
        {
            IList numlist = Services.BaseService.GetList<LineType>();
            TreeNode tn = new TreeNode();
            tn.Text = "原有变电站";
            tn.Tag = "old";
            for (int j = 0; j < numlist.Count; j++)
            {
                TreeNode n1 = new TreeNode();
                n1.Text = ((LineType)numlist[j]).TypeName;
                n1.Tag = ((LineType)numlist[j]).TypeName.Replace("kV", ""); ;
                tn.Nodes.Add(n1);
            }
            treeView1.Nodes[0].Nodes.Add(tn);
           
            LayerGrade lay=new LayerGrade();
            IList<LayerGrade> list= Services.BaseService.GetList<LayerGrade>("SelectLayerGradeList",lay);
            for (int i = 0; i < list.Count;i++ )
            {
                TreeNode node = new TreeNode();
                node.Text = list[i].Name;
                node.Tag = list[i].SUID;
                for (int j = 0; j < numlist.Count; j++)
                {
                    TreeNode n1 = new TreeNode();
                    n1.Text = ((LineType)numlist[j]).TypeName;
                    n1.Tag = ((LineType)numlist[j]).TypeName.Replace("kV",""); ;
                    node.Nodes.Add(n1);
                }
                treeView1.Nodes[0].Nodes.Add(node);
              

            }
            treeView1.Nodes[0].Expand();
        }
     
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Text!="年份")
            {
                string tempid = treeView1.SelectedNode.Tag.ToString();

                
                if (tempid.Length < 10)
                {
                    power = tempid;
                    yearid = treeView1.SelectedNode.Parent.Tag.ToString();
                    if (yearid == "old")
                    {
                        substation sub2 = new substation();
                        sub2.EleName = " ObligateField5<'2008' and ObligateField1='" + power + "'"; //power;
                        gridControl.DataSource = Services.BaseService.GetList("SelectsubstationByWhere", sub2);//dt;
                        this.Text = "变电站列表";
                        return;
                    }
                }
                substation sub = new substation();
                sub.UID = yearid;
                sub.ObligateField1 = power;
                // line.LayerID = sid;
                //DataTable dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectLineInfoByPowerKvAndYear", line), typeof(LineInfo));
                gridControl.DataSource = Services.BaseService.GetList("SelectSubInfoByPowerKvAndYear", sub);//dt;
                this.Text ="变电站列表";
            }
        }
        public GridControl GridControl
        {
            get { return gridControl; }
        }

        /// <summary>
        /// 获取GridView对象
        /// </summary>
        public GridView GridView
        {
            get { return gridView; }
        }

        /// <summary>
        /// 获取GridControl的数据源，即对象的列表
        /// </summary>
        public IList<substation> ObjectList
        {
            get { return this.gridControl.DataSource as IList<substation>; }
        }

        /// <summary>
        /// 获取焦点对象，即FocusedRow
        /// </summary>
        public substation FocusedObject
        {
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as substation; }
        }

        private void gridControl_DoubleClick(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            substat = this.gridView.GetRow(this.gridView.FocusedRowHandle) as substation;
            //line = ObjectList[this.gridView.FocusedRowHandle];
            string aa = "";
            this.Close();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.gridView.FocusedRowHandle != -1)
            {
                substat = this.gridView.GetRow(this.gridView.FocusedRowHandle) as substation;
                Services.BaseService.Delete<substation>(substat);
                gridView.DeleteRow(this.gridView.FocusedRowHandle);
            }
        }

    }
}