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
using Itop.Domain.Graphics;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmAddTzgsHC2 : FormBase
    {
        public FrmAddTzgsHC2()
        {
            InitializeComponent(); 
            string conn = "Col5='" + projectID + "' and Col4='" + OperTable.tzgs + "'";
            yAnge = oper.GetYearRange(conn);
            InitCom();
        }
        bool ISFirstLoad = true;
        //计算方式 true为按容量 false为按个数
        bool Js_Flag = FrmTzgsWH1.Js_Flag;
       public string strFlag = "";

        private Ps_YearRange yAnge = new Ps_YearRange();
        OperTable oper = new OperTable();
        private string projectID;
        private string stat="no";
        private bool line = false;
         
        private string strtype = "";
        private string _uid = "";
        private bool _operatorflag;
        private bool _buildprortzgsflag=true;   //当为TRUE时为投资估算，当为FALSE时为建设项目模块
        
        public string AddName
        {
            get { return txtAddName.Text; }
            set { txtAddName.Text = value; }
        }
        public string StrType
        {
            get {
            if(comboBoxEdit12.Text=="变电站")
                return "bian";
            if (comboBoxEdit12.Text == "线路")
                return "line";
            if (comboBoxEdit12.Text == "送变电")
                return "sbd";
            else
            {
                return "";
            }
            }
            set { strtype = value; }
        }
        public bool operatorflag
        {
            get { return _operatorflag; }
            set { _operatorflag = value; }
        }
        public bool buildprortzgsflag
        {
            get { return _buildprortzgsflag; }
            set { _buildprortzgsflag = value; }
        }
        public string uid
        {
            get { return _uid; }
            set { _uid = value; }  
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
                {
                    comboBoxEdit9.SelectedIndex = 1;
                    combSub.Visible = true;
                    SelectSUB();
                }
                else if (str.IndexOf("改造") != -1)
                {
                    comboBoxEdit9.SelectedIndex = 2;
                    combSub.Visible = true;
                    SelectSUB();
                }
                else
                {
                    comboBoxEdit9.Text = "新建";
                    combSub.Visible = false;
                }
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
        public string AreaType
        {
            get { return comboBoxEdit11.Text; }
            set { comboBoxEdit11.Text = value; }
        }
        public string Title
        {
            get {
                
                    return comboBoxEdit8.Text;
            }
            set
            {
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
        public string LineType
        {
            get { return comboBoxEdit14.Text; }
            set { comboBoxEdit14.Text = value; }
        }
        public string GetFromID
        {
            get { 
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
      

        public int Num1
        {
            get
            {
                int temmpint=0;
                if (int.TryParse(txtzb.Text,out temmpint))
                {

                }
                return temmpint;
              
            }
            set
            {
                
                txtzb.Text = Convert.ToString(value);
                
            }
        }
       
       

        private string areaname = "";
        public string LineInfo
        {
            get { return comboBoxEdit6.Text + "@" + comboBoxEdit3.Text + "@" + comboBoxEdit7.Text; }
            set {
                try
                {
                    strFlag = "line";
                    string str = value;
                    string[] ary = str.Split('@');
                
                    if (ary.Length == 3)
                    {
                        comboBoxEdit6.Text = ary[0];
                        comboBoxEdit3.Text = ary[1];
                     
                        comboBoxEdit7.Properties.Items.Clear();
                       
                        IList<string> strList1 = oper.GetLineName(" " + "S5='1' and S1='" + comboBoxEdit6.Text + "' and Type='" + comboBoxEdit3.Text + "'");
                        foreach (string str2 in strList1)
                            comboBoxEdit7.Properties.Items.Add(str2);
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
            get { return comboBoxEdit6.Text + "@" +comboBoxEdit13.Text + "@"+comboBoxEdit4.Text; }
            set
            {
               try
                {
                    strFlag = "bian";
                    string str = value;
                    string[] ary = str.Split('@');
                    if (ary.Length == 3)
                    {
                        comboBoxEdit6.Text = ary[0];
                        comboBoxEdit13.Text = ary[1];

                        IList<string> strList = oper.GetLineName(" " + "S5='2' and S1='" + comboBoxEdit6.Text + "' and Type='" + comboBoxEdit13.Text + "'");
                        comboBoxEdit4.Properties.Items.Clear();
                        foreach (string str2 in strList)
                            comboBoxEdit4.Properties.Items.Add(str2);
                        comboBoxEdit4.Text = ary[2];


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
               
                    if (comboBoxEdit6.Text == "" || comboBoxEdit3.Text == "" || comboBoxEdit7.Text == "")
                        LineVol += 0.0;
                    else
                    {
                        conn = "S5='1' and S1='" + comboBoxEdit6.Text + "' and Type='" + comboBoxEdit3.Text + "' and Name='" + comboBoxEdit7.Text + "'";
                        double linevolPer = oper.GetAllVol(conn);
                        LineVol += linevolPer * double.Parse(spinEdit1.Text) * pei;
                    }
              
                    if (comboBoxEdit6.Text == "" || comboBoxEdit4.Text == "")
                        LineVol += 0.0;
                    else
                    {
                        conn = "S5='2' and S1='" + comboBoxEdit6.Text + "' and Type='" + comboBoxEdit13.Text + "' and Name='" + comboBoxEdit4.Text + "'";
                        double linevolPer = oper.GetAllVol(conn);
                        //按容量

                        if (Js_Flag)
                        {
                            LineVol += linevolPer * double.Parse(spinEdit2.Text) * pei;
                        }
                        //按台数据
                        else
                        {
                            LineVol += linevolPer * int.Parse(txtzb.Text) * pei;
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
          
            strList = oper.GetLineS1("S5='2'");
            foreach (string str in strList)
                comboBoxEdit6.Properties.Items.Add(str);
            comboBoxEdit6.SelectedIndex = 0;
            strList = oper.GetLineName("S5='2' and S1='" + comboBoxEdit6.Text + "' and name like'%" + comboBoxEdit9.Text + "%'");
            if (strList.Count != 0)
            {
                comboBoxEdit4.Text = strList[0].ToString();
            }
            IList<string> areatyplist=null;
             if (Common.Services.BaseService.GetList<string>("SelectBuildProDifAreaType", "")!=null)
            {
                 areatyplist=Common.Services.BaseService.GetList<string>("SelectBuildProDifAreaType", "");

                 for (int j = 0; j < areatyplist.Count; j++)
                 {
                     comboBoxEdit11.Properties.Items.Add(areatyplist[j]);
                 }
            }
             //导线型号
            WireCategory wc = new WireCategory();
            wc.Type = "40";
            IList<WireCategory> list = Common.Services.BaseService.GetList<WireCategory>("SelectWireCategoryList", wc);
            for (int k = 0; k < list.Count; k++)
			{
                comboBoxEdit14.Properties.Items.Add(list[k].WireType);
			}
            
        }

        private void FrmAddTzgs_Load(object sender, EventArgs e)
        {
            
            string conn = "ProjectID='" + projectID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            foreach (PS_Table_AreaWH area in list)
            {
                comboBoxEdit10.Properties.Items.Add(area.Title);
            }
            conn = "ProjectID='" + projectID + "' order by Sort";
            IList<PS_Table_Area_TYPE> list1 = Common.Services.BaseService.GetList<PS_Table_Area_TYPE>("SelectPS_Table_Area_TYPEByConn", conn);
           
            if (areaname != "")
                comboBoxEdit10.Text = areaname;
            else if (comboBoxEdit10.Properties.Items.Count > 0)
                comboBoxEdit10.SelectedIndex = 0;

            ISFirstLoad = false;
        }

        private void comboBoxEdit6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (strFlag=="line")
            {
                return;

            }
            else if (strFlag=="bian")
            {
                return;
            }
            else if (strFlag=="sbd")
            {
                string x = comboBoxEdit3.Text;
                //comboBoxEdit3.Properties.Items.Clear();
                string te = "";
                //if (stat == "500" || stat == "220" || stat == "110")
                //    te = "S1='"+stat+"' and ";
                IList<string> strList = oper.GetLineType(te + "S5='1' and S1='" + comboBoxEdit6.Text + "' and Type='" + x + "'");
                //foreach(string str in strList)
                //    comboBoxEdit3.Properties.Items.Add(str);
                //comboBoxEdit3.SelectedIndex = 0;
                //if (x == comboBoxEdit3.Text)
                //{
                    comboBoxEdit7.Properties.Items.Clear();
                    //if (stat == "500" || stat == "220" || stat == "110")
                    //    te = "S1='" + stat + "' and ";
                    IList<string> strList1 = oper.GetLineName(te + "S5='1' and S1='" + comboBoxEdit6.Text + "' and Type='" + comboBoxEdit3.Text + "'");
                    foreach (string str in strList1)
                        comboBoxEdit7.Properties.Items.Add(str);
                    comboBoxEdit7.SelectedIndex = 0;
                //}

                comboBoxEdit4.Properties.Items.Clear();
                x = comboBoxEdit13.Text;
                te = "";
                //if (stat == "500" || stat == "220" || stat == "110")
                //    te = "S1='" + stat + "' and ";
                strList = oper.GetLineName(te + "S5='2' and S1='" + comboBoxEdit6.Text + "' and Type='" + x + "'");
                foreach (string str in strList)
                    comboBoxEdit4.Properties.Items.Add(str);
                comboBoxEdit4.SelectedIndex = 0;

            }
           
            
            
        }
        private void comboBoxEdit13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (strFlag=="bian")
            {
                return;
            }
            comboBoxEdit4.Properties.Items.Clear();
            string x = comboBoxEdit13.Text;
            string te = "";
            //if (stat == "500" || stat == "220" || stat == "110")
            //    te = "S1='" + stat + "' and ";
            IList<string> strList = oper.GetLineName(te + "S5='2' and S1='" + comboBoxEdit6.Text + "' and Type='" + x + "'");
            foreach (string str in strList)
                comboBoxEdit4.Properties.Items.Add(str);
            comboBoxEdit4.SelectedIndex = 0;
        }
        private void comboBoxEdit3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (strFlag=="line")
            {
                return;
            }
            comboBoxEdit7.Properties.Items.Clear();
            string te = "";
            //if (stat == "500" || stat == "220" || stat == "110")
            //    te = "S1='" + stat + "' and ";
            IList<string> strList = oper.GetLineName(te + "S5='1' and Type='" + comboBoxEdit3.Text + "' and S1='" + comboBoxEdit6.Text + "'");
            foreach (string str in strList)
                comboBoxEdit7.Properties.Items.Add(str);
            comboBoxEdit7.SelectedIndex = 0;
        }

       

      

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (comboBoxEdit8.Text=="" || comboBoxEdit8.Text==null)
            {
                MessageBox.Show("项目名称不能为空", "");
                return; 
            }
            int temmpint = 0;
            if (!int.TryParse(txtzb.Text, out temmpint))
            {
                MessageBox.Show("主变台数格式有误", "");
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
            SelectSUB();
        }

        private void comboBoxEdit9_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBoxEdit9.Text == "新建"&&operatorflag)
            //{
            //    comboBoxEdit8.Properties.Items.Clear();
            //    comboBoxEdit8.Text = "";
            //}
            //if (comboBoxEdit9.Text != "新建" && operatorflag)
            //{
            //    //comboBoxEdit8.Properties.Items.Clear();
            //    string conn = "AreaID='" + projectID + "' and  L1=" + V + " and AreaName='" + comboBoxEdit10.Text + "'";
            //    IList<Substation_Info> list = Common.Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByCon", conn);
            //    foreach (Substation_Info area in list)
            //    {
            //        if (area.UID != uid)
            //        {
            //            comboBoxEdit8.Properties.Items.Add(area.Title);
            //        }

            //    }
            //    if (comboBoxEdit8.Properties.Items.Count > 0)
            //        comboBoxEdit8.SelectedIndex = 0;
            //}
            if ((comboBoxEdit9.Text == "扩建" || comboBoxEdit9.Text == "改造") && ISFirstLoad == false)
            {
                //FrmProShoSub frm = new FrmProShoSub();
                //frm.AreaName = AreaName;
                //if (frm.ShowDialog() == DialogResult.OK)
                //{
                //    txtAddName.Text = frm.Subname + comboBoxEdit9.Text;
                //    comboBoxEdit6.Text = frm.subl1.ToString();
                //}
                combSub.Visible = true;
                SelectSUB();
                combSub.Focus();
            }

          
        }

        private void txtdlq_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBoxEdit12_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        public void SelectSUB()
        {
            string sqlwhere = "";
            if (AreaName != "")
            {
                sqlwhere = "AreaID='" + MIS.ProgUID + "' and AreaName='" + AreaName + "' and  Flag='1' ";
            }
            else
            {
                sqlwhere = "AreaID='" + MIS.ProgUID + "' and  Flag='1' ";

            }
            IList<Substation_Info> pspList = Common.Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByCon", sqlwhere);
            SetComboBoxData2(combSub, "Title", "L1", "选择变电站", "值一", pspList);

        }
        public void SetComboBoxData2(DevExpress.XtraEditors.LookUpEdit comboBox, string displayMember, string valueMember, string nullTest, string cnStr, IList<Substation_Info> post)
        {
            comboBox.Properties.Columns.Clear();
            comboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            comboBox.Properties.DataSource = post;
            comboBox.Properties.DisplayMember = displayMember;
            comboBox.Properties.ValueMember = valueMember;
            comboBox.Properties.NullText = nullTest;
            comboBox.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(valueMember, "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(displayMember,"变电站" ,40),
             new DevExpress.XtraEditors.Controls.LookUpColumnInfo("L1", "电压", 25, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("AreaName", "区域", 35, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default)});
        }

        private void combSub_EditValueChanged(object sender, EventArgs e)
        {
            if (combSub.Text != null)
            {
                txtAddName.Text = combSub.Text + comboBoxEdit9.Text;
                comboBoxEdit6.Text = combSub.EditValue.ToString();
            }
        }
      

       
    }
}