using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using System.Text.RegularExpressions;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmAddTzgs : FormBase
    {
        public FrmAddTzgs()
        {
            InitializeComponent(); 
            string conn = "Col5='" + projectID + "' and Col4='" + OperTable.tzgs + "'";
            yAnge = oper.GetYearRange(conn);
            InitCom();
        }
        private Ps_YearRange yAnge = new Ps_YearRange();
        OperTable oper = new OperTable();
        private string projectID;
        private string stat="no";
        public string Stat
        {
            get { return stat; }
            set { stat = value; }
        }
        public string Col3
        {
            get { return comboBoxEdit9.Text; }
            set {
                string str = value;
                if (str.IndexOf("扩建") != -1)
                    comboBoxEdit9.SelectedIndex = 1;
                else
                    comboBoxEdit9.Text = "新建";
            }
        }
        public string StartYear
        {
            get { return comboBoxEdit1.Text; }
            set { comboBoxEdit1.Text = value; }
        }
        public string FinishYear
        {
            get {
                return comboBoxEdit2.Text;
            }
            set { comboBoxEdit2.Text = value; }
        }
        public string BieZhu
        {
            get { return memoEdit1.Text; }
            set { memoEdit1.Text = value; }
        }
        public string ParentName
        {
            get { return textEdit1.Text; }
            set { textEdit1.Text = value; }
        }
        public string ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }
        public double LineLen
        {
            get { return double.Parse(spinEdit1.Text); }
            set { spinEdit1.Value = Convert.ToDecimal(value); }
        }
        //public void SetText(string com6,string com
        public double Vol
        {
            get {
                string str = comboBoxEdit8.Text;
                int n = 1; int a = str.IndexOf('×'), b = str.IndexOf('X');
                if (a != -1 || b != -1)
                {
                    int.TryParse((a == -1 ? str.Substring(b - 1, 1) : str.Substring(a - 1, 1)),out n);
                }
                if (str == "")
                    return 0.0;
                str = str.Replace(" ", "");
                Regex reg = new Regex("[0-9]{2,3}");
                return (n!=0?n:1)*double.Parse(reg.Match(str).Value)/10;
            }
        }
        public string LineInfo
        {
            get { return comboBoxEdit5.Text + "@" + comboBoxEdit3.Text + "@" + comboBoxEdit7.Text; }
            set {
                try
                {
                    string str = value;
                    string[] ary = str.Split('@');
                
                    if (ary.Length == 3)
                    {
                        comboBoxEdit5.Text = ary[0];
                        comboBoxEdit3.Text = ary[1];
                        comboBoxEdit7.Text = ary[2];
                    }
                }
                catch { }
            }
        }
        public string BianInfo
        {
            get { return comboBoxEdit6.Text + "@" + comboBoxEdit4.Text + "@" + comboBoxEdit8.Text; }
            set
            {
               try
                { 
                    string str = value;
                    string[] ary = str.Split('@');
                    if (ary.Length == 3)
                    {
                        comboBoxEdit6.Text = ary[0];
                        comboBoxEdit8.Text = ary[2];
                        comboBoxEdit4.Text = ary[1];
                    }
                }
                catch { }
            }
        }
        public double AllVol
        {
            get
            {
                double LineVol=0.0,BianVol=0.0;
                string conn="";
                if (comboBoxEdit5.Text == "" || comboBoxEdit3.Text == "" || comboBoxEdit7.Text == "")
                    LineVol += 0.0;
                else
                {
                    conn = "S5='1' and Type='" + comboBoxEdit5.Text + "' and Name='" + comboBoxEdit3.Text + "' and T5='" + comboBoxEdit7.Text + "'";
                    double linevolPer = oper.GetAllVol(conn);
                    LineVol += linevolPer * double.Parse(spinEdit1.Text);
                }
                if (comboBoxEdit6.Text == "" || comboBoxEdit8.Text == "" || comboBoxEdit4.Text == "")
                    LineVol += 0.0;
                else
                {
                    conn = "S5='2' and Type='" + comboBoxEdit6.Text + "' and Name='" + comboBoxEdit4.Text + "' and T5='" + comboBoxEdit8.Text + "'";
                    double linevolPer = oper.GetAllVol(conn);
                    LineVol += linevolPer;
                }
                return LineVol;
            }
        }

        public void InitCom()
        {
            comboBoxEdit9.Properties.Items.Add("新建");
            comboBoxEdit9.Properties.Items.Add("扩建");
            comboBoxEdit9.SelectedIndex = 0;
            for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
            {
                comboBoxEdit1.Properties.Items.Add(i.ToString());
                comboBoxEdit2.Properties.Items.Add(i.ToString());
            }
            comboBoxEdit1.Text = DateTime.Now.Year.ToString();
            comboBoxEdit2.Text = DateTime.Now.AddYears(1).Year.ToString() ;
            IList<string> strList = oper.GetLineType("S5='1'");
            foreach(string str in strList)
                comboBoxEdit5.Properties.Items.Add(str);
            comboBoxEdit5.SelectedIndex = 0;
            strList = oper.GetLineType("S5='2'");
            foreach (string str in strList)
                comboBoxEdit6.Properties.Items.Add(str);
            comboBoxEdit6.SelectedIndex = 0;
        }

        private void FrmAddTzgs_Load(object sender, EventArgs e)
        {
            
        }

        private void comboBoxEdit5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string x = comboBoxEdit3.Text;
            comboBoxEdit3.Properties.Items.Clear();
            string te="";
            //if (stat == "500" || stat == "220" || stat == "110")
            //    te = "S1='"+stat+"' and ";
            IList<string> strList=oper.GetLineName(te+"S5='1' and Type='"+comboBoxEdit5.Text+"'");
            foreach(string str in strList)
                comboBoxEdit3.Properties.Items.Add(str);
            comboBoxEdit3.SelectedIndex = 0;
            if (x == comboBoxEdit3.Text)
            {
                comboBoxEdit7.Properties.Items.Clear();
                //if (stat == "500" || stat == "220" || stat == "110")
                //    te = "S1='" + stat + "' and ";
                IList<string> strList1 = oper.GetLineT5(te + "S5='1' and Type='" + comboBoxEdit5.Text + "' and Name='" + comboBoxEdit3.Text + "'");
                foreach (string str in strList1)
                    comboBoxEdit7.Properties.Items.Add(str);
                comboBoxEdit7.SelectedIndex = 0;
            }
        }

        private void comboBoxEdit6_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEdit8.Properties.Items.Clear();
            string x = comboBoxEdit8.Text;
            string te = "";
            //if (stat == "500" || stat == "220" || stat == "110")
            //    te = "S1='" + stat + "' and ";
            IList<string> strList = oper.GetLineT5(te + "S5='2' and Type='" + comboBoxEdit6.Text + "'");
            foreach (string str in strList)
                comboBoxEdit8.Properties.Items.Add(str);
            comboBoxEdit8.SelectedIndex = 0;
            if (x == comboBoxEdit8.Text)
            {
                comboBoxEdit4.Properties.Items.Clear();
                //if (stat == "500" || stat == "220" || stat == "110")
                //    te = "S1='" + stat + "' and ";
                IList<string> strList1 = oper.GetLineName(te + "S5='2' and Type='" + comboBoxEdit6.Text + "' and T5='" + comboBoxEdit8.Text + "'");
                foreach (string str in strList1)
                    comboBoxEdit4.Properties.Items.Add(str);
                comboBoxEdit4.SelectedIndex = 0;
            }
        }

        private void comboBoxEdit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEdit7.Properties.Items.Clear();
            string te = "";
            //if (stat == "500" || stat == "220" || stat == "110")
            //    te = "S1='" + stat + "' and ";
            IList<string> strList = oper.GetLineT5(te + "S5='1' and Type='" + comboBoxEdit5.Text + "' and Name='" + comboBoxEdit3.Text + "'");
            foreach (string str in strList)
                comboBoxEdit7.Properties.Items.Add(str);
            comboBoxEdit7.SelectedIndex = 0;
        }

       

        private void comboBoxEdit8_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEdit4.Properties.Items.Clear();
            string te = "";
            //if (stat == "500" || stat == "220" || stat == "110")
            //    te = "S1='" + stat + "' and ";
            IList<string> strList = oper.GetLineName(te+"S5='2' and Type='" + comboBoxEdit6.Text + "' and T5='" + comboBoxEdit8.Text + "'");
            foreach (string str in strList)
                comboBoxEdit4.Properties.Items.Add(str);
            comboBoxEdit4.SelectedIndex = 0;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == "" || textEdit1.Text == null)
            {
                MessageBox.Show("项目名称不能为空", "");
                return; 
            }
            try
            {
                if (int.Parse(comboBoxEdit2.Text) < int.Parse(comboBoxEdit1.Text))
                {
                    MessageBox.Show("竣工年必须大于开工年", "");
                    return;
                }
            }
            catch { }
            DialogResult = DialogResult.OK;
        }
    }
}