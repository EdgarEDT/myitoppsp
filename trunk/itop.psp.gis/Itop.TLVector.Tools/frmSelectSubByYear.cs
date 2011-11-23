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
    public partial class frmSelectSubByYear : FormBase
    {
        private string yearid = "";
        private string power = "";
        private string str_year = "";

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
        public string Str_year
        {
            get { return str_year; }
            set { str_year = value; }
        }
        public frmSelectSubByYear()
        {
            InitializeComponent();
        }
        private int flag=0;
        public frmSelectSubByYear(int _flag)
        {
         InitializeComponent();
         flag = _flag;
        }
        private void frmSelectLineByYear_Load(object sender, EventArgs e)
        {
            if (flag==0)
            {
                InitDate();
            }
            
            if (flag==1)
            {
                initDate();
            }
        }
        public void InitDate()
        {
            LayerGrade lay=new LayerGrade();
            //lay.Name = str_year;
            IList<LayerGrade> list= Services.BaseService.GetList<LayerGrade>("SelectLayerGradeList",lay);
            for (int i = 0; i < list.Count;i++ )
            {
                TreeNode node = new TreeNode();
                node.Text = list[i].Name;
                node.Tag = list[i].SUID;
                treeView1.Nodes[0].Nodes.Add(node);
            }
            treeView1.ExpandAll();
            foreach(TreeNode node in treeView1.Nodes[0].Nodes){
                if (node.Text.Length > 4)
                {
                    if (node.Text.Substring(0,4) == str_year)
                    {
                        treeView1.SelectedNode = node;
                    }
                }
            }
        }
        public void initDate()
        {
            LayerGrade lay = new LayerGrade();
            lay.Name = str_year+"%";
            IList<LayerGrade> list = Services.BaseService.GetList<LayerGrade>("SelectLayerGradeByYear", lay);
            for (int i = 0; i < list.Count; i++)
            {
                TreeNode node = new TreeNode();
                node.Text = list[i].Name;
                node.Tag = list[i].SUID;
                treeView1.Nodes[0].Nodes.Add(node);
            }
            treeView1.ExpandAll();
            foreach (TreeNode node in treeView1.Nodes[0].Nodes)
            {
                if (node.Text.Length > 4)
                {
                    if (node.Text.Substring(0, 4) == str_year)
                    {
                        treeView1.SelectedNode = node;
                    }
                }
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                yearid = treeView1.SelectedNode.Tag.ToString();
                substation sub = new substation();
                sub.UID = yearid;
                sub.ObligateField1 = power;
                // line.LayerID = sid;
                //DataTable dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectLineInfoByPowerKvAndYear", line), typeof(LineInfo));
                gridControl.DataSource = Services.BaseService.GetList("SelectSubInfoByPowerKvAndYear", sub);//dt;
                this.Text = power + "kV 变电站列表";
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

    }
}