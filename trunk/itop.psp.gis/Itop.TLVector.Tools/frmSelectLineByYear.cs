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
    public partial class frmSelectLineByYear : FormBase
    {
        private string yearid = "";
        private string power = "";
        public LineInfo line=null;
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

        public frmSelectLineByYear()
        {
            InitializeComponent();
        }

        private void frmSelectLineByYear_Load(object sender, EventArgs e)
        {
            InitDate();
        }
        public void InitDate()
        {
            LayerGrade lay=new LayerGrade();
            IList<LayerGrade> list= Services.BaseService.GetList<LayerGrade>("SelectLayerGradeList",lay);
            for (int i = 0; i < list.Count;i++ )
            {
                TreeNode node = new TreeNode();
                node.Text = list[i].Name;
                node.Tag = list[i].SUID;
                treeView1.Nodes[0].Nodes.Add(node);
            }
            treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                yearid = treeView1.SelectedNode.Tag.ToString();
                LineInfo line = new LineInfo();
                line.UID = yearid;
                line.EleID = power;
                // line.LayerID = sid;
                //DataTable dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectLineInfoByPowerKvAndYear", line), typeof(LineInfo));
                gridControl.DataSource = Services.BaseService.GetList("SelectLineInfoByPowerKvAndYear", line);//dt;
                this.Text = power + "kV 线路列表";
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
        public IList<LineInfo> ObjectList
        {
            get { return this.gridControl.DataSource as IList<LineInfo>; }
        }

        /// <summary>
        /// 获取焦点对象，即FocusedRow
        /// </summary>
        public LineInfo FocusedObject
        {
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as LineInfo; }
        }

        private void gridControl_DoubleClick(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            line = this.gridView.GetRow(this.gridView.FocusedRowHandle) as LineInfo;
            //line = ObjectList[this.gridView.FocusedRowHandle];
            string aa = "";
            this.Close();
        }

    }
}