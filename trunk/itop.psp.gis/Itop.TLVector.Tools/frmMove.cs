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
    public partial class frmMove : FormBase
    {
        private string strValue = "";

        public string StrValue
        {
            get { return strValue; }
            set { strValue = value; }
        }

        public frmMove()
        {
            InitializeComponent();
        }
        public void Init(string _j1,string _w1,string _d1,string _j2,string _w2,string _d2)
        {
            d1.Text = _j1;
            f1.Text = _w1;
            m1.Text = _d1;
            d2.Text = _j2;
            f2.Text = _w2;
            m2.Text= _d2;
        }

        private void ok_Click(object sender, EventArgs e)
        {
            if (d1.Text == "")
            {
                MessageBox.Show("���ȣ� �Ȳ���Ϊ�ա�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                d1.Focus();
                return;
            }
            if (Convert.ToInt32(d1.Text) > 360)
            {
                MessageBox.Show("���ȣ� ����ֵ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                d1.Focus();
                return;
            }
            if (f1.Text == "")
            {
                MessageBox.Show("���ȣ� �ֲ���Ϊ�ա�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                f1.Focus();
                return;
            }
            if (Convert.ToInt32(f1.Text) > 59)
            {
                MessageBox.Show("���ȣ� ����ֵ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                f1.Focus();
                return;
            }
            if (m1.Text == "")
            {
                MessageBox.Show("���ȣ� �벻��Ϊ�ա�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m1.Focus();
                return;
            }
            if (Convert.ToDecimal(m1.Text) > 60)
            {
                MessageBox.Show("���ȣ� ����ֵ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m1.Focus();
                return;
            }

            if (d2.Text == "")
            {
                MessageBox.Show("γ�ȣ� �Ȳ���Ϊ�ա�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                d2.Focus();
                return;
            }
            //if (Convert.ToInt32(d2.Text) != 30 && Convert.ToInt32(d2.Text) != 31)
            //{
            //    MessageBox.Show("γ�ȣ� ����ֵ������\r\nͭ�����γ��Ӧ�ý��ڣ�������������֮�䡣", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    d2.Focus();
            //    return;
            //}
            if (f2.Text == "")
            {
                MessageBox.Show("γ�ȣ� �ֲ���Ϊ�ա�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                f2.Focus();
                return;
            }
            if (Convert.ToInt32(f2.Text) > 59)
            {
                MessageBox.Show("γ�ȣ� ����ֵ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                f2.Focus();
                return;
            }
            if (m2.Text == "")
            {
                MessageBox.Show("γ�ȣ� �벻��Ϊ�ա�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m2.Focus();
                return;
            }
            if (Convert.ToDecimal(m2.Text) > 60)
            {
                MessageBox.Show("γ�ȣ� ����ֵ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m2.Focus();
                return;
            }
            //if (d1.Text == "117" && Convert.ToInt32(f1.Text) < 25)
            //{
            //    MessageBox.Show("���ȣ� ������ͼ�����ʾ��Χ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    f1.Focus();
            //    return;
            //}
            //if (d1.Text == "118" && Convert.ToInt32(f1.Text) > 15)
            //{
            //    MessageBox.Show("���ȣ� ������ͼ�����ʾ��Χ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    f1.Focus();
            //    return;
            //}
            //if(d2.Text=="31" && Convert.ToInt32(f2.Text)>16){
            //    MessageBox.Show("γ�ȣ� ������ͼ�����ʾ��Χ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    f2.Focus();
            //    return;
            //}
            //if (d2.Text == "30" && Convert.ToInt32(f2.Text) < 30)
            //{
            //    MessageBox.Show("γ�ȣ� ������ͼ�����ʾ��Χ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    f2.Focus();
            //    return;
            //}
            strValue = d1.Text + " " + f1.Text + " " + m1.Text + "," + d2.Text + " " + f2.Text + " " + m2.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmMove_Load(object sender, EventArgs e)
        {
            
        }
    }
}