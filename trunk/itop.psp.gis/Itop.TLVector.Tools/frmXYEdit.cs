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
    public partial class frmXYEdit : FormBase
    {
        private string strValue = "";
        public string TextVal = "";
        public string StrValue
        {
            get { return strValue; }
            set { strValue = value; }
        }

        public frmXYEdit()
        {
            InitializeComponent();
        }
        public void Init()
        {
            string[] str = strValue.Split(',');
            //string[] str = ((string)IEnum.Current).Split(',');
            string[] JWD1 = str[0].Split(' ');
            d1.Text = JWD1[0];
            f1.Text = JWD1[1];
            m1.Text = JWD1[2];
            string[] JWD2 = str[1].Split(' ');
            d2.Text = JWD2[0];
            f2.Text = JWD2[1];
            m2.Text = JWD2[2];

        }

        private void ok_Click(object sender, EventArgs e)
        {
            if (d1.Text == "")
            {
                MessageBox.Show("经度： 度不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                d1.Focus();
                return;
            }
            if (Convert.ToInt32(d1.Text) > 360)
            {
                MessageBox.Show("经度： 度数值不合理。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                d1.Focus();
                return;
            }
            if (f1.Text == "")
            {
                MessageBox.Show("经度： 分不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                f1.Focus();
                return;
            }
            if (Convert.ToInt32(f1.Text) > 59)
            {
                MessageBox.Show("经度： 分数值不合理。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                f1.Focus();
                return;
            }
            if (m1.Text == "")
            {
                MessageBox.Show("经度： 秒不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m1.Focus();
                return;
            }
            if (Convert.ToDecimal(m1.Text) > 60)
            {
                MessageBox.Show("经度： 秒数值不合理。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m1.Focus();
                return;
            }

            if (d2.Text == "")
            {
                MessageBox.Show("纬度： 度不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                d2.Focus();
                return;
            }
            //if (Convert.ToInt32(d2.Text) != 30 && Convert.ToInt32(d2.Text) != 31)
            //{
            //    MessageBox.Show("纬度： 度数值不合理。\r\n铜陵地区纬度应该介于３０度至３１度之间。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    d2.Focus();
            //    return;
            //}
            if (f2.Text == "")
            {
                MessageBox.Show("纬度： 分不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                f2.Focus();
                return;
            }
            if (Convert.ToInt32(f2.Text) > 59)
            {
                MessageBox.Show("纬度： 分数值不合理。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                f2.Focus();
                return;
            }
            if (m2.Text == "")
            {
                MessageBox.Show("纬度： 秒不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m2.Focus();
                return;
            }
            if (Convert.ToDecimal(m2.Text) > 60)
            {
                MessageBox.Show("纬度： 秒数值不合理。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m2.Focus();
                return;
            }
            //if(d2.Text=="31" && Convert.ToInt32(f2.Text)>17){
            //    MessageBox.Show("纬度： 超出本图最大显示范围。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    f2.Focus();
            //    return;
            //}
            //if (d2.Text == "30" && Convert.ToInt32(f2.Text) < 30)
            //{
            //    MessageBox.Show("纬度： 超出本图最大显示范围。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    f2.Focus();
            //    return;
            //}
            strValue = d1.Text + " " + f1.Text + " " + m1.Text + "," + d2.Text + " " + f2.Text + " " + m2.Text;
            TextVal = d1.Text + "°" + f1.Text + "′" + m1.Text + "″," + d2.Text + "°" + f2.Text + "′" + m2.Text + "″";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}