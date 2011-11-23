using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using System.Text.RegularExpressions;
using Itop.Domain.Stutistic;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.IO;
using Itop.Domain.Stutistics;
using Itop.Client.Base;

namespace Itop.Client.Table
{
    public partial class FrmAddTzgsWH2 : FormBase
    {
        public FrmAddTzgsWH2()
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
        private string strtype = "";
        private string tzgsid = "";
        private string _uid="";
        public string UID
        {
            get { return _uid; }
            set { _uid = value; }
        }
        public string StrType
        {
            get {
            //    if(comboBoxEdit12.Text=="变电站")
            //    return "bian";
            //if (comboBoxEdit12.Text == "线路")
            //    return "line";
            //if (comboBoxEdit12.Text == "开关")
            //    return "kg";
            //else
            //{
                return "";
            //}
            }
            set { strtype = value; }
        }

        public string TZGSID
        {
            get { return tzgsid; }
            set { tzgsid = value; }
        }

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
                //if (strtype=="line")
                //    return textEdit1.Text;
                //else
                    return comboBoxEdit8.Text;
            }
            set
            {
                //if (strtype=="line")
                //    textEdit1.Text = value;
                //else
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
        public double LineLen2
        {
            get { return double.Parse(spinEdit3.Text); }
            set { spinEdit3.Value = Convert.ToDecimal(value); }
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
        public string DQ
        {
            get { return comboBoxEdit11.Text; }
            set { comboBoxEdit11.Text = value; }
        }

        public double Num1
        {
            get
            {
                if (strtype=="line")
                    return Convert.ToDouble(txtnr.Text);
                if (strtype=="bian")
                    return  Convert.ToDouble(txtzb.Text);
                if (strtype == "kg")
                    return Convert.ToDouble(txtNum1.Text);
                else
                {
                    return 0;
                }
            }
            set
            {
                if (strtype == "line")
                    txtnr.Text = Convert.ToString(value);
                if (strtype == "bian")
                    txtzb.Text = Convert.ToString(value);
                if (strtype == "kg")
                    txtNum1.Text = Convert.ToString(value);
            }
        }
        public double Num2
        {
            get
            {
                if (strtype == "line")
                    return Convert.ToDouble(txtgt.Text);
                if (strtype == "bian")
                    return Convert.ToDouble(txtdlq.Text);
                if (strtype == "kg")
                    return Convert.ToDouble(txtNum2.Text);
                else
                {
                    return 0;
                }
            }
            set
            {
                if (strtype == "line")
                    txtgt.Text = Convert.ToString(value);
                if (strtype == "bian")
                    txtdlq.Text = Convert.ToString(value);
                if (strtype == "kg")
                    txtNum2.Text = Convert.ToString(value);
            }
        }
        public double Num3
        {
            get { return Convert.ToDouble(txtNum3.Text); }
            set { txtNum3.Text = Convert.ToString(value); }
        }
        public double Num4
        {
            get { return Convert.ToDouble(txtNum4.Text); }
            set { txtNum4.Text = Convert.ToString(value); }
        }
        public double Num6
        {
            get { return Convert.ToDouble(txtgd.Text); }
            set { txtgd.Text = Convert.ToString(value); }
        }
        public double Num5
        {
            get
            {
                if (strtype == "line")
                    return Convert.ToDouble(txtts.Text);
                if (strtype == "bian")
                    return Convert.ToDouble(txtzs.Text);
                //if (strtype == "kg")
                //    return Convert.ToDouble(txtNum2.Text);
                else
                {
                    return 0;
                }
            }
            set
            {
                if (strtype == "line")
                    txtts.Text = Convert.ToString(value);
                if (strtype == "bian")
                    txtzs.Text = Convert.ToString(value);
                //if (strtype == "kg")
                //    txtNum2.Text = Convert.ToString(value);
            }
        }
        public double WGNum
        {
            get { return Convert.ToDouble(txtwg.Text); }
            set { txtwg.Text = Convert.ToString(value); }
        }
        public double Amount
        {
            get {
                if (strtype == "line")
                    return Convert.ToDouble(txtje2.Text);
                if (strtype == "bian")
                    return Convert.ToDouble(txtje1.Text);
                //if (strtype == "kg")
                //    return Convert.ToDouble(txtNum2.Text);
                else
                {
                    return Convert.ToDouble(txttz.Text); 
                }
                
            }
            set {
                if (strtype == "line")
                    txtje2.Text = Convert.ToString(value);
                if (strtype == "bian")
                    txtje1.Text = Convert.ToString(value);
                if (strtype == "")
                {
                    txttz.Text = Convert.ToString(value);
                }
            }
        }
        public int JGNum
        {
            get { return Convert.ToInt32(txtjg.Text); }
            set { txtjg.Text = Convert.ToString(value); }
        }
        public string ProgType
        {
            get { return txtProg.Text; }
            set { txtProg.Text = Convert.ToString(value); }
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
            string conn = "ProjectID='" + Itop.Client.MIS.ProgUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            foreach (PS_Table_AreaWH area in list)
            {
                comboBoxEdit10.Properties.Items.Add(area.Title);
            }
            comboBoxEdit1.Text = DateTime.Now.Year.ToString();
            comboBoxEdit2.Text = DateTime.Now.AddYears(1).Year.ToString() ;
            IList<string> strList = oper.GetLineS1("S5='2'");
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
            //if (strtype=="line")
            //{
                
            //    comboBoxEdit8.Visible = false;
            //    label15.Visible = false;
            //    comboBoxEdit10.Visible = false;
            //    panelControl1.Visible = true;
            //    panelControl2.Visible = false;
            //    panelControl3.Visible = false;
            //    comboBoxEdit12.Text = "线路";
            //}
            //if(strtype=="bian")
            //{
            //    panelControl1.Visible = false;
            //    panelControl2.Visible = true;
            //    panelControl3.Visible = false;
            //    textEdit1.Visible = false;
            //    comboBoxEdit12.Text = "变电站";
            //}
            //if (strtype == "kg")
            //{
            //    panelControl1.Visible = false;
            //    panelControl2.Visible = false;
            //    panelControl3.Visible = true;
            //    textEdit1.Visible = false;
            //    comboBoxEdit12.Text = "开关";
            //}
            string conn = "ProjectID='" + projectID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            foreach (PS_Table_AreaWH area in list)
            {
                comboBoxEdit10.Properties.Items.Add(area.Title);
            }
            if (areaname != "")
                comboBoxEdit10.Text = areaname;
            else if (comboBoxEdit10.Properties.Items.Count > 0)
                comboBoxEdit10.SelectedIndex = 0;

            RefreshData(tzgsid);
        }

        private void comboBoxEdit5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string x = comboBoxEdit3.Text;
            //comboBoxEdit3.Properties.Items.Clear();
            string te="";
            //if (stat == "500" || stat == "220" || stat == "110")
            //    te = "S1='"+stat+"' and ";
            IList<string> strList=oper.GetLineType(te+"S5='1' and S1='"+comboBoxEdit5.Text+"'");
            //foreach(string str in strList)
            //    comboBoxEdit3.Properties.Items.Add(str);
            //comboBoxEdit3.SelectedIndex = 0;
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
           // comboBoxEdit7.Properties.Items.Clear();
            string te = "";
            //if (stat == "500" || stat == "220" || stat == "110")
            //    te = "S1='" + stat + "' and ";
            IList<string> strList = oper.GetLineName(te + "S5='1' and Type='" + comboBoxEdit3.Text + "' and S1='" + comboBoxEdit5.Text + "'");
            foreach (string str in strList)
                comboBoxEdit7.Properties.Items.Add(str);
            comboBoxEdit7.SelectedIndex = 0;

            //if (comboBoxEdit3.Text == "架空")
            //{
            //    label19.Text = "耐热导线：";
            //    label20.Text = "杆塔（基）：";
            //    txtgt.Visible = true;
            //}
            //else
            //{
            //    label19.Text = "沟道：";
            //    label20.Text = "";
            //    txtgt.Visible = false;
            //}
        }

       

      

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if ((line && (textEdit1.Text == "" || textEdit1.Text == null)) || (!line && (comboBoxEdit8.Text=="" || comboBoxEdit8.Text==null)))
            {
                MessageBox.Show("项目名称不能为空", "");
                return; 
            }
            if (comboBoxEdit5.Text!=comboBoxEdit6.Text){
                MessageBox.Show("线路与变电站电压应一致。", "");
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
                //comboBoxEdit8.Properties.Items.Clear();
                string conn = "AreaID='" + projectID + "' and  L1=" + V + " and AreaName='" + comboBoxEdit10.Text + "'";
                IList<Substation_Info> list = Common.Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByCon", conn);
                //foreach (Substation_Info area in list)
                //{
                //    comboBoxEdit8.Properties.Items.Add(area.Title);
                //}
                if (comboBoxEdit8.Properties.Items.Count > 0)
                    comboBoxEdit8.SelectedIndex = 0;
            }
        }

        private void comboBoxEdit9_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBoxEdit9.Text == "新建")
            //{
            //    comboBoxEdit8.Properties.Items.Clear();
            //    comboBoxEdit8.Text = "";
            //}
            //else
            //{
                //comboBoxEdit8.Properties.Items.Clear();
                string conn = "AreaID='" + projectID + "' and  L1=" + V + " and AreaName='" + comboBoxEdit10.Text + "'";
                IList<Substation_Info> list = Common.Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByCon", conn);
                foreach (Substation_Info area in list)
                {
                    comboBoxEdit8.Properties.Items.Add(area.Title);
                }
                if (comboBoxEdit8.Properties.Items.Count > 0)
                    comboBoxEdit8.SelectedIndex = 0;
            //}
        }

        private void txtdlq_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBoxEdit12_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(comboBoxEdit12.Text == "线路"){
            //    panelControl1.Visible = true;
            //    panelControl2.Visible = false;
            //    panelControl3.Visible = false;
            //}
            //if (comboBoxEdit12.Text == "变电站")
            //{
            //    panelControl1.Visible = false;
            //    panelControl2.Visible = true;
            //    panelControl3.Visible = false;
            //}
            //if (comboBoxEdit12.Text == "开关")
            //{
            //    panelControl1.Visible = false;
            //    panelControl2.Visible = false;
            //    panelControl3.Visible = true;
            //}
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            FormTZGSXX tzxx = new FormTZGSXX();
            tzxx.pid = TZGSID;
            tzxx.type = "sub";
            tzxx.devicenum =Convert.ToInt32(txtzs.Text) ;
            tzxx.buildyear = StartYear;
            tzxx.buildend = FinishYear;
            if (comboBoxEdit6.Text!=null)
            {
                tzxx.volt = Convert.ToDouble(comboBoxEdit6.Text);
            }
            
            tzxx.ShowDialog();
            if (tzxx.DialogResult==DialogResult.OK)
            {
                txtzs.Text = tzxx.ObjectList.Count.ToString();
                double rl = 0.0; int tash = 0;
                foreach (Ps_Table_TZMX pt in tzxx.ObjectList)
                {
                    rl+=pt.Vol;
                    tash += pt.Num;
                }
                spinEdit2.Value = (decimal)rl;
                txtzb.Text = tash.ToString();

            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FormTZGSXX tzxx = new FormTZGSXX();
            tzxx.pid = TZGSID;
            tzxx.type = "line";
            tzxx.devicenum = Convert.ToInt32(txtts.Text);
            tzxx.buildyear = StartYear;
            tzxx.buildend = FinishYear;
            if (comboBoxEdit5.Text!=null)
            {
                tzxx.volt = Convert.ToDouble(comboBoxEdit5.Text);
            }
           
            tzxx.ShowDialog();
            if (tzxx.DialogResult == DialogResult.OK)
            {
                txtts.Text = tzxx.ObjectList.Count.ToString();
               // double rl = 0.0; int tash = 0;
                //foreach (Ps_Table_TZMX pt in tzxx.ObjectList)
                //{
                //    r1 += pt.Vol;
                //    tash += pt.Num;
                //}
                //spinEdit2.Value = (decimal)r1;
                //txtzb.Text = tash.ToString();

            }
        }

        private void 增加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImgAdd f = new frmImgAdd();
            f.Uid = tzgsid;
            if (f.ShowDialog() == DialogResult.OK)
            {
                RefreshData(tzgsid);
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Itop.Domain.Graphics.PSP_ImgInfo obj = FocusedObject;
            if (obj == null)
            {
                return;
            }
            if (MessageBox.Show("确定要删除么？", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                Services.BaseService.Update("DeletePSP_ImgInfo", obj);
                RefreshData(tzgsid);

            }
        }

        private void 查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        public Itop.Domain.Graphics.PSP_ImgInfo FocusedObject
        {
            get { return this.gridView2.GetRow(this.gridView2.FocusedRowHandle) as Itop.Domain.Graphics.PSP_ImgInfo; }
        }
        public bool RefreshData(string id)
        {
            try
            {
                Itop.Domain.Graphics.PSP_ImgInfo img = new Itop.Domain.Graphics.PSP_ImgInfo();
                img.TreeID = id;
                IList<Itop.Domain.Graphics.PSP_ImgInfo> list = Services.BaseService.GetList<Itop.Domain.Graphics.PSP_ImgInfo>("SelectPSP_ImgInfoList", img);

                this.gridControl.DataSource = list;
            }
            catch (Exception exc)
            {
                //HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }

        public void OpenFile()
        {
            Itop.Domain.Graphics.PSP_ImgInfo obj = FocusedObject;
            if (obj == null)
            {
                return;
            }
            obj = Services.BaseService.GetOneByKey<Itop.Domain.Graphics.PSP_ImgInfo>(obj.UID);
            BinaryWriter bw;
            FileStream fs;
            try
            {
                byte[] bt = obj.Image;
                string filename = obj.Name;
                fs = new FileStream("C:\\" + filename, FileMode.Create, FileAccess.Write);
                bw = new BinaryWriter(fs);
                bw.Write(bt);
                bw.Flush();
                bw.Close();
                fs.Close();
                System.Diagnostics.Process.Start("C:\\" + filename);

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void spinEdit2_EditValueChanged(object sender, EventArgs e)
        {
            string sql = " S1='" + comboBoxEdit6.Text + "' and Name='" + comboBoxEdit4 .Text+ "'";
            Project_Sum p= (Project_Sum)Services.BaseService.GetObject("SelectProject_SumByValues",sql);
            if(p!=null){
                txtje1.Text =Convert.ToString( Convert.ToDecimal( p.Num) * spinEdit2.Value);
                txttz.Text = Convert.ToString(Convert.ToDecimal(txtje1.Text) + Convert.ToDecimal(txtje2.Text));
            }
        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            string sql = " S1='" + comboBoxEdit5.Text + "' and Type='架空'";
            Project_Sum p = (Project_Sum)Services.BaseService.GetObject("SelectProject_SumByValues", sql);
            if (p != null)
            {
                txtje2.Text = Convert.ToString(Convert.ToDecimal(p.Num) * spinEdit1.Value);
                txttz.Text =Convert.ToString( Convert.ToDecimal(txtje1.Text) + Convert.ToDecimal(txtje2.Text));
            }
        }

        private void spinEdit3_EditValueChanged(object sender, EventArgs e)
        {
            string sql = " S1='" + comboBoxEdit5.Text + "' and Type='电缆'";
            Project_Sum p = (Project_Sum)Services.BaseService.GetObject("SelectProject_SumByValues", sql);
            if (p != null)
            {
                txtje2.Text = Convert.ToString(Convert.ToDecimal(p.Num) * spinEdit3.Value);
                txttz.Text = Convert.ToString(Convert.ToDecimal(txtje1.Text) + Convert.ToDecimal(txtje2.Text));
            }
        }
    }
}