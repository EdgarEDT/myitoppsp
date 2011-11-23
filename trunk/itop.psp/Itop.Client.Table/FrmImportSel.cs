using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Common;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmImportSel : FormBase
    {
        public FrmImportSel()
        {
            InitializeComponent();
        }
        private IList bindList=new ArrayList();
        private bool bRst = true;
        private string rz = "1.9";
        public bool BRst
        {
            get { return bRst; }
            set { bRst = value; }
        }
        public string RZ
        {
            get { return rz; }
            set { rz = value; }
        }
        public IList BindList
        {
            get { return bindList; }
            set { bindList = value; }
        }
        private IList<string> outList = new List<string>();
        public IList<string> OutList
        {
            get { return outList; }
        }
        private void FrmRZ_Load(object sender, EventArgs e)
        {
            InitData();
        }

        public void InitData()
        {
            if (bindList.Count > 0)
            {
                InitCol();
            }
        }

        public void InitCol()
        {
            DataTable dt = DataConverter.ToDataTable(bindList);
            dt.Columns.Add("B", typeof(bool));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["B"] = false;
            }
            gridControl1.DataSource = dt;
        }

       
        private void ok_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                bool check = Convert.ToBoolean(gridView1.GetRowCellValue(i,"B"));//.SetRowCellValue(i, "B", checkEdit1.Checked);
                if (check)
                    outList.Add(gridView1.GetRowCellValue(i, "ID").ToString());
            }
            if (outList.Count == 0)
            {
                MessageBox.Show("至少选择一个城区！"); return;
            }
            DialogResult = DialogResult.OK;
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                gridView1.SetRowCellValue(i, "B", checkEdit1.Checked);
            }
        }


    }
}