using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Domain.Table;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmAreaSel : FormBase
    {
        public frmAreaSel()
        {
            InitializeComponent();
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }
        public PS_Table_AreaWH FocusedObject
        {
            get { return this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as PS_Table_AreaWH; }
        }
        public void InitGrid2()
        {
            string conn = "ProjectID='" + Itop.Client.MIS.ProgUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            this.gridControl2.DataSource = list;

        }

        private void frmAreaSel_Load(object sender, EventArgs e)
        {
            InitGrid2();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (FocusedObject == null)
            {
                MessageBox.Show("请选择记录。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}