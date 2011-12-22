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
    public partial class FrmSet : FormBase
    {
       

        public string Str_jj
        {
            get { return txtjj.Text; }
            //set { str_jj = value; }
        }
        private string str_rzb = "";

        public string Str_rzb
        {
            get { return str_rzb; }
            set { str_rzb = value; }
        }
        private string str_dj = "";

        public string Str_dj
        {
            get { return txtdy.Text; }
            set { str_dj = value; }
        }
        private string str_rl = "";

        public string Str_rl
        {
            get { return str_rl; }
            set { str_rl = value; }
        }
        private string str_num = "";

        public string Str_num
        {
            get { return txtnum.Text; }
            set { str_num = value; }
        }

        public decimal s = 0;
        public double sub_s = 0;

        public FrmSet()
        {
            InitializeComponent();
        }

        private void FrmSet_Load(object sender, EventArgs e)
        {
            if (sub_s * 0.9 >Convert.ToDouble(s)) {
                label8.Text = "当前区域内供电负荷满足要求，无需新建变电站。";
            } else
                label8.Text = "";
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (txtjj.Text=="")
            {
                MessageBox.Show("变电站最小间距不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtdy.Text == "")
            {
                MessageBox.Show("电压等级不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtnum.Text == "")
            {
                MessageBox.Show("新建变电站数量不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtrl_EditValueChanged(object sender, EventArgs e)
        {
            if (s == 0 || txtrl.Text=="")
            {
                return;
            }
            int nn =Convert.ToInt32( (s * Convert.ToDecimal( txtrzb.Text)-Convert.ToDecimal( sub_s)) / Convert.ToDecimal( txtrl.Text));
            txtnum.Text = nn.ToString();
            if (nn < 0)
            {
                label8.Text = "当前区域内供电负荷满足要求，无需新建变电站。";
            }
            else
            {
                label8.Text = "";
            }
        }

        private void txtnum_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtnum.Text) > 0)
            {
                label8.Text = "";
            }
        }
    }
}