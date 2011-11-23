using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Domain.Stutistic;
using Itop.Client.Common;
using DevExpress.XtraEditors;
using Itop.Client.Base;

namespace ItopVector.Tools
{
    public partial class frmAddLine : FormBase
    {
        public ArrayList list = new ArrayList();
        public LineInfo line = new LineInfo();
        PowerProTypes p;
        public bool SubUpdate = false;
        public string LineWidth = "2";
        private string layerID = "";
        
        public frmAddLine()
        {
            InitializeComponent();
        }
        public void Init(string _layerID)
        {
            layerID = _layerID;
        }
        private void add_Click(object sender, EventArgs e)
        {
            
            try
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
                //if (d2.Text == "31" && Convert.ToInt32(f2.Text) > 17)
                //{
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
                //string temp=d1.Text+","+d2.Text;
                string temp = d1.Text + "��" + f1.Text + "��" + m1.Text + "��," + d2.Text + "��" + f2.Text + "��" + m2.Text + "��";
               // list.Add(temp, d1.Text + " " + f1.Text + " " + m1.Text + "," + d2.Text + " " + f2.Text + " " + m2.Text);
                for (int n = 0; n<list.Count; n++)
                {
                    if (d1.Text + " " + f1.Text + " " + m1.Text + "," + d2.Text + " " + f2.Text + " " + m2.Text==(string)list[n])
                    {
                        MessageBox.Show("��γ���ظ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                list.Add(d1.Text + " " + f1.Text + " " + m1.Text + "," + d2.Text + " " + f2.Text + " " + m2.Text);
                gtlist.Items.Add(temp);
                
                //System.Collections.Specialized.NameObjectCollection
                
            }
            catch(Exception e1){
                MessageBox.Show("��γ���ظ���","��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            //list.Add(gtlist.Items.Count,temp);
        }

        private void del_Click(object sender, EventArgs e)
        {
            if (gtlist.SelectedIndex==-1)
            {
                return;
            }
            if(gtlist.SelectedItem!=null){
               // list.Remove(gtlist.SelectedItem);
                list.RemoveAt(gtlist.SelectedIndex);
                gtlist.Items.Remove(gtlist.SelectedItem);
                list.TrimToSize();
               
            }
        }

        private void frmAddLine_Load(object sender, EventArgs e)
        {
            IList list = Services.BaseService.GetList<LineType>();
            DataTable dt = Itop.Common.DataConverter.ToDataTable(list, typeof(LineType));
            dj.Properties.DataSource = dt;
        }

        private void dj_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string uid = dj.EditValue.ToString();
                DataRowView rowView = (DataRowView)dj.Properties.GetDataSourceRowByKeyValue(uid);
                if (rowView != null)
                {
                    line.ObligateField2 = rowView.Row["Color"].ToString();
                    LineWidth = rowView.Row["ObligateField1"].ToString();
                    //line.ObligateField2 = lineType;
                }
            }
            catch { }
        }

        private void mc_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
           
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(mc.Text==""){
                MessageBox.Show("��·���Ʋ���Ϊ�ա�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mc.Focus();
                return;
            }
            if (gtlist.ItemCount<2)
            {
                MessageBox.Show("������Ŀ����С��2��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gtlist.Focus();
                return;
            }
            //if (dj.Text=="")
            //{
            //    MessageBox.Show("��ѹ�ȼ�����Ϊ�ա�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    dj.Focus();
            //    return;
            //}
            line.LayerID = layerID;
            line.LineName = mc.Text;
            //line.Length = cd.Text;
            //line.LineType =xh.Text;
            //line.Voltage = dj.Text;
            //line.ObligateField1 = comboBox1.Text;
           
            this.DialogResult = DialogResult.OK;
            this.Close();
            //OldUID = p.Code;
            //p.Code = line.UID;
        }

        private void d2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void d1_KeyUp(object sender, KeyEventArgs e)
        {
            ((TextEdit)sender).Text = ((TextEdit)sender).Text.Replace("-", "");
        }

        private void f2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gtlist_DoubleClick(object sender, EventArgs e)
        {
            if (gtlist.SelectedIndex == -1)
            {
                return;
            }
            
            frmXYEdit f = new frmXYEdit();
            f.StrValue = (string)list[gtlist.SelectedIndex];
            f.Init();
            if (f.ShowDialog() == DialogResult.OK)
            {
                list.RemoveAt(gtlist.SelectedIndex);
                list.Insert(gtlist.SelectedIndex,f.StrValue);
                gtlist.SetItemValue(f.TextVal,gtlist.SelectedIndex);
            }

            
        }

        private void simpleButton3_Click(object sender, EventArgs e) {
            foreach (string str in memoEdit1.Lines) {
                try {
                    string[] arr = str.Split('\t');
                    if (arr.Length < 2) continue;
                    double d1 = double.Parse(arr[0]);
                    double t1 = double.Parse(arr[1]);
                    int n1 = (int)d1;
                    double d2 = (d1 - n1) * 60.0;
                    int n2 = (int)d2;
                    double d3 = (d2 - n2) * 60.0;
                    int n3 = (int)d3;

                    int m1 = (int)t1;
                    double t2 = (t1 - m1) * 60.0;
                    int m2 = (int)t2;
                    double t3 = (t2 - m2) * 60.0;
                    int m3 = (int)t3;
                    list.Add(string.Format("{0} {1} {2},{3} {4} {5}", n1, n2, n3, m1, m2, m3));
                    gtlist.Items.Add(string.Format("{0}��{1}��{2}��,{3}��{4}��{5}��", n1, n2, n3, m1, m2, m3));
                } catch { }
            }
            memoEdit1.Text = "";
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            foreach (string str in memoEdit2.Lines)
            {
                try
                {
                    string[] arr = str.Split(new char[] { ' ', '\t' },StringSplitOptions.RemoveEmptyEntries);
                    double n1=Convert.ToDouble(arr[0]);
                    double n2 =Convert.ToDouble( arr[1]);
                    double n3 = Convert.ToDouble(arr[2]);
                    double m1 = Convert.ToDouble(arr[3]);
                    double m2 = Convert.ToDouble(arr[4]);
                    double m3 = Convert.ToDouble(arr[5]);
                    list.Add(string.Format("{0} {1} {2},{3} {4} {5}", n1, n2, n3, m1, m2, m3));
                    gtlist.Items.Add(string.Format("{0}��{1}��{2}��,{3}��{4}��{5}��", n1, n2, n3, m1, m2, m3));
                }
                catch { }
            }
            memoEdit2.Text = "";
        }      
    }
}