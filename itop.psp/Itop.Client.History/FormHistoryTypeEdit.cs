using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.History
{
    public partial class FormHistoryTypeEdit : DevExpress.XtraEditors.XtraForm
    {
        private string typeTitle = string.Empty;
        private string _remark = string.Empty;
        private string _Units = string.Empty;
        //默认单位
        private string[] UStr ={ "亿元", "MW", "万kW", "亿kWh", "万kWh", "万人", "km²", "万元/人" };
        public string TypeTitle
        {
            get { return typeTitle; }
            set { typeTitle = value; }
        }
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        public string Units
        {
            get { return _Units; }
            set { _Units = value; }
        }
        public FormHistoryTypeEdit()
        {
            InitializeComponent();
        }

        private void FormHistoryTypeEdit_Load(object sender, EventArgs e)
        {
            textEdit1.Text = TypeTitle.Trim();
            textEdit2.Text = Units.Trim() ;
            memoEdit1.Text = Remark.Trim();
            combu.Items.Clear();
            for (int i = 0; i < UStr.Length; i++)
            {
                combu.Items.Add(UStr[i]);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(textEdit1.Text == string.Empty)
            {
                Itop.Common.MsgBox.Show("请输入名称！");
                return;
            }
            if (textEdit1.Text.Contains("（")||textEdit1.Text.Contains("("))
            {
                Itop.Common.MsgBox.Show("类名中不能包含括号，单位请添加在下方！");
                return;
            }
          
            if (textEdit2.Text.Trim().Length>0&&(textEdit2.Text.Trim().IndexOf("#")==0||textEdit2.Text.Trim().IndexOf("#")==(textEdit2.Text.Trim().Length-1)))
            {
                Itop.Common.MsgBox.Show("单位不能以'#'开头或结尾！");
                return;
            }
            if (memoEdit1.Text.Length>=200)
            {
                Itop.Common.MsgBox.Show("说明文本过长！");
                return;
            }

            if (textEdit1.Text == typeTitle && memoEdit1.Text == Remark&&textEdit2.Text==Units)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                typeTitle = textEdit1.Text.Trim();
                Remark = memoEdit1.Text.Trim();
                Units = textEdit2.Text.Trim();
                DialogResult = DialogResult.OK;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                combu.Enabled = true;
            }
            else
            {
                combu.Enabled = false;
            }
        }

        private void combu_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tempstr = textEdit2.Text.Trim();
            if (tempstr.Length==0)
            {
                textEdit2.Text = combu.SelectedItem.ToString();
                return;
            }
            if (tempstr.Contains(combu.SelectedItem.ToString()))
            {
                MessageBox.Show("单位中已包含 " + combu.SelectedItem.ToString() + " 不能重复添加!");
            }
            else
            {
                if (tempstr.Substring(tempstr.Length-1,1)!="#")
                {
                    textEdit2.Text = textEdit2.Text.Trim() + "#" + combu.SelectedItem.ToString();
                }
                else
                {
                    textEdit2.Text = textEdit2.Text.Trim() + combu.SelectedItem.ToString();
                }
            }
        }
    }
}