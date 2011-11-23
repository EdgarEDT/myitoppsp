using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmPlanList : FormBase
    {
        private string key = "";

        public string Key
        {
            get { return key; }
            set { key = value; }
        }


        public frmPlanList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ctrlPSP_PlanList1.AddObject();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ctrlPSP_PlanList1.DeleteObject();
        }

        private void frmPlanList_Load(object sender, EventArgs e)
        {
            ctrlPSP_PlanList1.RefreshData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //if (ctrlPSP_PlanList1.FocusedObject == null)
            //{
            //    MessageBox.Show("请选择记录。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //    return;
            //}
            //key = ctrlPSP_PlanList1.FocusedObject.UID;
            //this.DialogResult = DialogResult.Yes;
            //this.Close();
            frmLineParMng par = new frmLineParMng();
            par.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ctrlPSP_PlanList1.FocusedObject == null)
            {
                MessageBox.Show("请选择记录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmLineList1 frm = new frmLineList1();
            frm.Eleid = ctrlPSP_PlanList1.FocusedObject.UID;
            frm.Show();
            //frmPengFenLine f1 = new frmPengFenLine();
            //f1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ctrlPSP_PlanList1.FocusedObject == null)
            {
                MessageBox.Show("请选择记录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            key = ctrlPSP_PlanList1.FocusedObject.UID;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}