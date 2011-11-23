using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class FrmFQ : FormBase
    {

        DataTable dt = new DataTable();
        public DataTable DT
        {
            set { dt = value; }
            get { return dt; }
        
        }

        string fq="";
        public string FQ
        {
            set { fq = value; }
            get { return fq; }

        }

        public FrmFQ()
        {
            InitializeComponent();
        }

        private void repositoryItemCheckEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //DataRow row = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            //if (row == null)
            //    return;

            //bool bl = (bool)row["B"];
            //if (bl)
            //    fq = row["A"].ToString();
            //else
            //    fq = "MMMMMMMMMMNNNNNNNNNNN";
            //foreach (DataRow row1 in dt.Rows)
            //{
            //    if (row1["A"].ToString() != fq)
            //        row1["B"] = false;
            //}

        }

        private void FrmFQ_Load(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = dt;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            foreach (DataRow row1 in dt.Rows)
            {
                if ((bool)row1["B"] == true)
                    fq = row1["A"].ToString();
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}