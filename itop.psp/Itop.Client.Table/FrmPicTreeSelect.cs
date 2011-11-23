using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using System.Xml;
using System.IO;
using Itop.Client.Common;
using Itop.Domain.Stutistic;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmPicTreeSelect : FormBase
    {
        public FrmPicTreeSelect()
        {
            InitializeComponent();
        }

        private DataTable dt = new DataTable();

        public DataTable DT
        {
            set { dt = value; }
            get { return dt; }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void FrmPicTypeSelect_Load(object sender, EventArgs e)
        {
            
            if(!DesignMode)
            treeList1.DataSource = dt;


        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            //for (int i = 0; i < gridView1.RowCount; i++)
            //{
            //    gridView1.SetRowCellValue(i, "C", checkEdit1.Checked);
            //    if (!checkEdit1.Checked)
            //    {
            //        gridView1.SetRowCellValue(i, "C", false);
            //    }
            //}
        }
    }
}