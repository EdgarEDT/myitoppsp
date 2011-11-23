﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using System.Text.RegularExpressions;
using Itop.Domain.Stutistic;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmAddTzgsWH : FormBase
    {
        public FrmAddTzgsWH()
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
        private bool line = false;
        public bool Line
        {
            get { return line; }
            set { line = value; }
        }
        public string Stat
        {
            get { return stat; }
            set { stat = value; }
        }
        private int v = 110;
        public int V
        {
            get { return v; }
            set { v = value; }
        }
        public string Col3
        {
            get { return comboBoxEdit9.Text; }
            set {
                string str = value;
                if (str.IndexOf("扩建") != -1)
                    comboBoxEdit9.SelectedIndex = 1;
                else if (str.IndexOf("改造") != -1)
                    comboBoxEdit9.SelectedIndex = 2;
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
            get {
                if (line)
                    return textEdit1.Text;
                else
                    return comboBoxEdit8.Text;
            }
            set
            {
                if (line)
                    textEdit1.Text = value;
                else
                    comboBoxEdit8.Text = value;
            }
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
        public string GetFromID
        {
            get { 
                if(Line) 
                    return comboBoxEdit5.Text;
                else
                    return comboBoxEdit6.Text;
            }
        }
        private double tzgsXs=0.03;
        public double TzgsXs
        {
            set { tzgsXs = value; }
        }
        private int syear = 2010;
        public int Syear
        {
            set { syear = value; }
        }
        //public void SetText(string com6,string com
        public double Vol
        {
            get {
                //string str = comboBoxEdit8.Text;
                //int n = 1; int a = str.IndexOf('×'), b = str.IndexOf('X');
                //if (a != -1 || b != -1)
                //{
                //    int.TryParse((a == -1 ? str.Substring(b - 1, 1) : str.Substring(a - 1, 1)),out n);
                //}
                //if (str == "")
                //    return 0.0;
                //str = str.Replace(" ", "");
                //Regex reg = new Regex("[0-9]{2,3}");
                //return (n!=0?n:1)*double.Parse(reg.Match(str).Value)/10;
                return double.Parse(spinEdit2.Text); 
            }
            set { spinEdit2.Value = Convert.ToDecimal(value); }
        }
        public string AreaName
        {
            get { return comboBoxEdit10.Text; }
            set { areaname = value; }
        }
        private string areaname = "";
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
        public string GetFlag
        {
            get { return comboBoxEdit7.Text; }
        }
        public string BianInfo
        {
            get { return comboBoxEdit6.Text + "@" + comboBoxEdit4.Text; }
            set
            {
               try
                { 
                    string str = value;
                    string[] ary = str.Split('@');
                    if (ary.Length == 2)
                    {
                        comboBoxEdit6.Text = ary[0];
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
                int fy = 2010;
                try { fy = int.Parse(FinishYear); }
                catch { }
                if (fy < syear)
                    fy = syear;
                double pei = Math.Pow((1 + tzgsXs),(fy - syear)); 
                string conn="";
                if (Line)
                {
                    if (comboBoxEdit5.Text == "" || comboBoxEdit3.Text == "" || comboBoxEdit7.Text == "")
                        LineVol += 0.0;
                    else
                    {
                        conn = "S5='1' and S1='" + comboBoxEdit5.Text + "' and Type='" + comboBoxEdit3.Text + "' and Name='" + comboBoxEdit7.Text + "'";
                        double linevolPer = oper.GetAllVol(conn);
                        LineVol += linevolPer * double.Parse(spinEdit1.Text) * pei;
                    }
                }
                else
                {
                    if (comboBoxEdit6.Text == "" || comboBoxEdit4.Text == "")
                        LineVol += 0.0;
                    else
                    {
                        conn = "S5='2' and S1='" + comboBoxEdit6.Text + "' and Name='" + comboBoxEdit4.Text + "'";
                        double linevolPer = oper.GetAllVol(conn);
                        LineVol += linevolPer * double.Parse(spinEdit2.Text) * pei;
                    }
                }
                return LineVol;
            }
        }

        public void InitCom()
        {
            comboBoxEdit9.Properties.Items.Add("新建");
            comboBoxEdit9.Properties.Items.Add("扩建");
            comboBoxEdit9.Properties.Items.Add("改造");
            comboBoxEdit9.SelectedIndex = 0;
            for (int i = yAnge.BeginYear; i <= yAnge.EndYear; i++)
            {
                comboBoxEdit1.Properties.Items.Add(i.ToString());
                comboBoxEdit2.Properties.Items.Add(i.ToString());
            }
            comboBoxEdit1.Text = DateTime.Now.Year.ToString();
            comboBoxEdit2.Text = DateTime.Now.AddYears(1).Year.ToString() ;
            IList<string> strList = oper.GetLineS1("S5='1'");
            foreach(string str in strList)
                comboBoxEdit5.Properties.Items.Add(str);
            comboBoxEdit5.SelectedIndex = 0;
            strList = oper.GetLineS1("S5='2'");
            foreach (string str in strList)
                comboBoxEdit6.Properties.Items.Add(str);
            comboBoxEdit6.SelectedIndex = 0;
        }

        private void FrmAddTzgs_Load(object sender, EventArgs e)
        {
            if (line)
            {
                panelControl1.Visible = true;
                comboBoxEdit8.Visible = false;
                label15.Visible = false;
                comboBoxEdit10.Visible = false;
            }
            else
            {
                panelControl2.Visible = true;
                textEdit1.Visible = false;
            }
            string conn = "ProjectID='" + projectID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            foreach (PS_Table_AreaWH area in list)
            {
                comboBoxEdit10.Properties.Items.Add(area.Title);
            }
            conn = "ProjectID='" + projectID + "' order by Sort";
            IList<PS_Table_Area_TYPE> list1 = Common.Services.BaseService.GetList<PS_Table_Area_TYPE>("SelectPS_Table_Area_TYPEByConn", conn);
            foreach (PS_Table_Area_TYPE area in list1)
            {
                comboBoxEdit11.Properties.Items.Add(area.Title);
            }

            if (areaname != "")
                comboBoxEdit10.Text = areaname;
            else if (comboBoxEdit10.Properties.Items.Count > 0)
                comboBoxEdit10.SelectedIndex = 0;

            //conn = "AreaID='" + projectID + "' and  L1=" + V + " and AreaName='" + comboBoxEdit10.Text + "'";
            //IList<Substation_Info> list1 = Common.Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByCon", conn);
            //foreach (Substation_Info area in list1)
            //{
            //    comboBoxEdit8.Properties.Items.Add(area.Title);
            //}
            //if(ParentName!="")
            //    comboBoxEdit8.Text = ParentName;
            //else if (comboBoxEdit8.Properties.Items.Count > 0)
            //    comboBoxEdit8.SelectedIndex = 0;
        }

        private void comboBoxEdit5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string x = comboBoxEdit3.Text;
            comboBoxEdit3.Properties.Items.Clear();
            string te="";
            //if (stat == "500" || stat == "220" || stat == "110")
            //    te = "S1='"+stat+"' and ";
            IList<string> strList=oper.GetLineType(te+"S5='1' and S1='"+comboBoxEdit5.Text+"'");
            foreach(string str in strList)
                comboBoxEdit3.Properties.Items.Add(str);
            comboBoxEdit3.SelectedIndex = 0;
            if (x == comboBoxEdit3.Text)
            {
                comboBoxEdit7.Properties.Items.Clear();
                //if (stat == "500" || stat == "220" || stat == "110")
                //    te = "S1='" + stat + "' and ";
                IList<string> strList1 = oper.GetLineName(te + "S5='1' and S1='" + comboBoxEdit5.Text + "' and Type='" + comboBoxEdit3.Text + "'");
                foreach (string str in strList1)
                    comboBoxEdit7.Properties.Items.Add(str);
                comboBoxEdit7.SelectedIndex = 0;
            }
        }

        private void comboBoxEdit6_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEdit4.Properties.Items.Clear();
            string x = comboBoxEdit4.Text;
            string te = "";
            //if (stat == "500" || stat == "220" || stat == "110")
            //    te = "S1='" + stat + "' and ";
            IList<string> strList = oper.GetLineName(te + "S5='2' and S1='" + comboBoxEdit6.Text + "'");
            foreach (string str in strList)
                comboBoxEdit4.Properties.Items.Add(str);
            comboBoxEdit4.SelectedIndex = 0;
            
        }

        private void comboBoxEdit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEdit7.Properties.Items.Clear();
            string te = "";
            //if (stat == "500" || stat == "220" || stat == "110")
            //    te = "S1='" + stat + "' and ";
            IList<string> strList = oper.GetLineName(te + "S5='1' and Type='" + comboBoxEdit3.Text + "' and S1='" + comboBoxEdit5.Text + "'");
            foreach (string str in strList)
                comboBoxEdit7.Properties.Items.Add(str);
            comboBoxEdit7.SelectedIndex = 0;
        }

       

        //private void comboBoxEdit8_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    comboBoxEdit4.Properties.Items.Clear();
        //    string te = "";
        //    //if (stat == "500" || stat == "220" || stat == "110")
        //    //    te = "S1='" + stat + "' and ";
        //    IList<string> strList = oper.GetLineName(te+"S5='2' and Type='" + comboBoxEdit6.Text + "' and T5='" + comboBoxEdit8.Text + "'");
        //    foreach (string str in strList)
        //        comboBoxEdit4.Properties.Items.Add(str);
        //    comboBoxEdit4.SelectedIndex = 0;
        //}

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if ((line && (textEdit1.Text == "" || textEdit1.Text == null)) || (!line && (comboBoxEdit8.Text=="" || comboBoxEdit8.Text==null)))
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

        private void comboBoxEdit10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit9.Text != "新建")
            {
                comboBoxEdit8.Properties.Items.Clear();
                string conn = "AreaID='" + projectID + "' and  L1=" + V + " and AreaName='" + comboBoxEdit10.Text + "'";
                IList<Substation_Info> list = Common.Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByCon", conn);
                foreach (Substation_Info area in list)
                {
                    comboBoxEdit8.Properties.Items.Add(area.Title);
                }
                if (comboBoxEdit8.Properties.Items.Count > 0)
                    comboBoxEdit8.SelectedIndex = 0;
            }
        }

        private void comboBoxEdit9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit9.Text == "新建")
            {
                comboBoxEdit8.Properties.Items.Clear();
                comboBoxEdit8.Text = "";
            }
            else
            {
                comboBoxEdit8.Properties.Items.Clear();
                string conn = "AreaID='" + projectID + "' and  L1=" + V + " and AreaName='" + comboBoxEdit10.Text + "'";
                IList<Substation_Info> list = Common.Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByCon", conn);
                foreach (Substation_Info area in list)
                {
                    comboBoxEdit8.Properties.Items.Add(area.Title);
                }
                if (comboBoxEdit8.Properties.Items.Count > 0)
                    comboBoxEdit8.SelectedIndex = 0;
            }
        }
    }
}