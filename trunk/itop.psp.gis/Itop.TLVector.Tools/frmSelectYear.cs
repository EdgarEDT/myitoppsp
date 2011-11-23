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
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmSelectYear : FormBase
    {
        public string year = "";
        public frmSelectYear()
        {
            InitializeComponent();
        }

        private void frmSelectYear_Load(object sender, EventArgs e)
        {
            LayerGrade lay = new LayerGrade();
            IList<LayerGrade> list = Services.BaseService.GetList<LayerGrade>("SelectLayerGradeList", lay);
            for (int i = 0; i < list.Count; i++)
            {
                TreeNode node = new TreeNode();
                node.Text = list[i].Name;
                node.Tag = list[i].SUID;
                treeView1.Nodes[0].Nodes.Add(node);
            }
            treeView1.ExpandAll();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(treeView1.SelectedNode==null){
                MessageBox.Show("请选择年份。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            year = treeView1.SelectedNode.Tag.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}