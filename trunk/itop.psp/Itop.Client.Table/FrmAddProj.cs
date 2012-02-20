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
    public partial class FrmAddProj : FormBase
    {
        public FrmAddProj()
        {
            InitializeComponent(); 
            string conn = "Col5='" + projectID + "' and Col4='" + OperTable.tzgs + "'";
            yAnge = oper.GetYearRange(conn);
          
        }
        private bool ISfirstload = true;
        public Ps_Table_BuildPro Currentptb = null;
        private Ps_YearRange yAnge = new Ps_YearRange();
        OperTable oper = new OperTable();
        private string projectID;
        private string stat="no";
        private bool line = false;
        private string strtype = "";
        private string _uid = "";


        public string Col1
        {
            get { return memoEdit1.Text; }
            set { memoEdit1.Text = value; }
        }
       
        public string Col2
        {
            get { return textEdit2.Text; }
            set
            {
                textEdit2.Text = value;
            }
        }
        public string Col3
        {
            get { return comboBoxEdit9.Text; }
            set {
                comboBoxEdit9.Text = value;
            }
        }
        public string Col4
        {
            get { return comboBoxEdit12.Text; }
            set
            {
                comboBoxEdit12.Text = value;
                if (comboBoxEdit12.Text == "线路")
                {


                    spinEdit2.Enabled = true;
                    combDXXH.Enabled = true;
                    spinEdit4.Enabled = false;
                    spinEdit1.Enabled = false;
                }
                if (comboBoxEdit12.Text == "变电站")
                {
                    spinEdit2.Enabled = false;
                    combDXXH.Enabled = false;
                    spinEdit4.Enabled = true;
                    spinEdit1.Enabled = true;

                }
                if (comboBoxEdit12.Text == "送变电")
                {
                    spinEdit1.Enabled = true;
                    combDXXH.Enabled = true;
                    spinEdit4.Enabled = true;
                    spinEdit2.Enabled = true;
                }
            }
        }
        public string Col5
        {
            get { return spinEdit4.Value.ToString(); }
            set
            {
                double tempdb = 0;
                if (double.TryParse(value,out tempdb))
                {
                    
                }
                spinEdit4.Value =Convert.ToDecimal(tempdb);
            }
        }
        public string Buildyear
        {
            get { return comboBoxEdit1.Text; }
            set { comboBoxEdit1.Text = value; }
        }
        public string BuildEd
        {
            get {
                return comboBoxEdit2.Text;
            }
            set { comboBoxEdit2.Text = value; }
        }
        //public double Length
        //{
        //    get
        //    {
        //        return double.Parse(spinEdit1.Text);
        //    }
        //    set
        //    {
        //        spinEdit1.Text = value.ToString();
        //    }
        //}
        public double Length
        {
            get
            {
                return double.Parse(spinEdit2.Text);
            }
            set
            {
                spinEdit2.Value = Convert.ToDecimal(value); ;
            }
        }
       
        
        public string ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }
        public double Volumn
        {
            get { return double.Parse(spinEdit1.Text); }
            set { spinEdit1.Value = Convert.ToDecimal(value); }
        }
        public string FromID
        {
            get { 
                    return comboBoxEdit5.Text;
            }
            set
            {
                comboBoxEdit5.Text = value;
            }
        }
      
       
        private int syear = 2010;
      
       
        public string AreaName
        {
            get { return comboBoxEdit10.Text; }
            set { comboBoxEdit10.Text = value; }
        }
        private string areaname = "";
      
        public string State
        {
            set { combState.Text=value; }
            get { return combState.Text; }
        }
        public string StateTime
        {
            get { return dateStateTime.Value.ToShortDateString(); }
            set
            {
                DateTime temptime=DateTime.Now;
                if (DateTime.TryParse((value == null||value=="") ? temptime.ToString() : value.ToString(), out temptime))
                {
                    
                }
                dateStateTime.Value = temptime;
            }
        }
        public double AllVol
        {
            get
            {
                return double.Parse(spinEdit3.Text);
            }
            set
            {
                spinEdit3.Text = value.ToString();
            }
        }
        public string Title
        {
            set
            {
                textEdit1.Text = value;
            }
            get
            {
                return textEdit1.Text;
            }
        }
        public string LineType
        {
            get { return combDXXH.Text; }
            set { combDXXH.Text = value; }
        }
        public string AreaType
        {
            get { return combWQFL.Text; }
            set { combWQFL.Text = value; }
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

            string conn = "ProjectID='" + projectID + "' order by Sort";
            IList<PS_Table_AreaWH> list1 = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            foreach (PS_Table_AreaWH area in list1)
            {
                comboBoxEdit10.Properties.Items.Add(area.Title);
            }
            //导线型号
            WireCategory wc = new WireCategory();
            wc.Type = "40";
            IList<WireCategory> list = Common.Services.BaseService.GetList<WireCategory>("SelectWireCategoryList", wc);
            for (int k = 0; k < list.Count; k++)
            {
                combDXXH.Properties.Items.Add(list[k].WireType);
            }
            //网区类型
            IList<string> areatyplist = null;
            if (Common.Services.BaseService.GetList<string>("SelectBuildProDifAreaType", "") != null)
            {
                areatyplist = Common.Services.BaseService.GetList<string>("SelectBuildProDifAreaType", "");

                for (int j = 0; j < areatyplist.Count; j++)
                {
                    combWQFL.Properties.Items.Add(areatyplist[j]);
                }
            }
        }

        private void FrmAddTzgs_Load(object sender, EventArgs e)
        {
            InitCom();
           
            if (Currentptb != null)
            {
                Title = Currentptb.Title;
                Col1 = Currentptb.Col1;
                Col2 = Currentptb.Col2;
                Col3 = Currentptb.Col3;
                Col4 = Currentptb.Col4;
                Col5 = Currentptb.Col5;
                Volumn =Currentptb. Volumn;
                 Buildyear =  Currentptb.BuildYear;
                BuildEd = Currentptb.BuildEd;
                FromID = Currentptb.FromID;
                AreaName = Currentptb.AreaName;
                AreaType = Currentptb.AreaType;
                AllVol = Currentptb.AllVolumn;
                State = Currentptb.State;
                StateTime = Currentptb.StateTime;
                LineType =Currentptb.Linetype;
                Length =Currentptb. Length;
            }
            ISfirstload = false;
      
        }

       
       
      
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == "" || textEdit1.Text == null )
            {
                MessageBox.Show("项目名称不能为空", "");
                return; 
            }
             int tempdb = 0;
             if (!int.TryParse(spinEdit4.Text, out tempdb))
            {
               MessageBox.Show("主变台数输入错误！", "");
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
            if (comboBoxEdit2.Text.Length == 0)
            {
                MessageBox.Show("竣工年不能为空", "");
                return;
            }
            if (combWQFL.Text.Length==0)
            {
                MessageBox.Show("网区分类不能为空", "");
                return;
            }
            if (Currentptb!=null)
            {
                Currentptb.Title = Title;
                Currentptb.Col1 = Col1;
                Currentptb.Col2 = Col2;
                Currentptb.Col3 = Col3;
                Currentptb.Col4 = Col4;
                Currentptb.Col5 = Col5;
                Currentptb.Volumn = Volumn;
                Currentptb.BuildYear = Buildyear;
                Currentptb.BuildEd = BuildEd;
                Currentptb.FromID = FromID;
                Currentptb.AreaName = AreaName;
                Currentptb.AreaType = AreaType;
                Currentptb.AllVolumn = AllVol;
                Currentptb.State = State;
                Currentptb.StateTime = StateTime;
                Currentptb.Linetype = LineType;
                Currentptb.Length = Length;
                 switch (combState.Text)
                {
                    case "规划审查":
                        Currentptb.Stime1 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "可行性研究":
                        Currentptb.Stime2 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "获取支持文件":
                        Currentptb.Stime3 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "初步设计":
                        Currentptb.Stime4 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "施工设计":
                        Currentptb.Stime5 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "施工":
                        Currentptb.Stime6 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "投产运行":
                        Currentptb.Stime7 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "移交":
                        Currentptb.Stime8 = dateStateTime.Value.ToShortDateString();
                        break;
                    default:
                        break;
                 }

            }
            DialogResult = DialogResult.OK;
        }

       

        private void comboBoxEdit12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEdit12.Text == "线路")
            {

               
                spinEdit2.Enabled = true;
                combDXXH.Enabled = true;
                spinEdit4.Enabled = false;
                spinEdit1.Enabled = false;
            }
            if (comboBoxEdit12.Text == "变电站")
            {

              

                spinEdit2.Enabled = false;
                combDXXH.Enabled = false;
                spinEdit4.Enabled = true;
                spinEdit1.Enabled = true;

            }
            if (comboBoxEdit12.Text == "送变电")
            {
                spinEdit1.Enabled = true;
                combDXXH.Enabled = true;
                spinEdit4.Enabled = true;
                spinEdit2.Enabled = true;
            }
        }
        private DateTime ChangeTime(string str)
        {
            DateTime temptime=DateTime.Now;
            if (DateTime.TryParse(str==""?temptime.ToString():str,out temptime))
	        {
        		 
	        }
            return temptime;
        }
        private bool ChangeStatetime = false;
        private decimal Changetz(string str)
        {
            decimal tempdl = 0;
            if (decimal.TryParse(str,out tempdl))
            {

            }
            return tempdl;
        }
        private bool Changetzflag = false;
        private void DelTz()
        {
             switch (combState.Text)
             {
                    case "可行性研究":
                        Changetzflag = true;
                     labtz.Visible=true;
                     sptz.Visible=true;
                        labtz.Text="可行投资";
                        sptz.Value= Changetz((Currentptb.Col6 == null || Currentptb.Col6 =="")? "" : Currentptb.Col6);
                        Changetzflag = false;
                        break;
                    case "初步设计":
                        Changetzflag = true;
                      labtz.Visible=true;
                     sptz.Visible=true;
                        labtz.Text = "初设投资";
                        sptz.Value= Changetz((Currentptb.Col7== null || Currentptb.Col7 =="")? "" : Currentptb.Col7);
                        Changetzflag = false;
                        break;
                    case "施工设计":
                        Changetzflag = true;
                      labtz.Visible=true;
                     sptz.Visible=true;
                        labtz.Text = "施工投资";
                        sptz.Value = Changetz((Currentptb.Col8 == null || Currentptb.Col8 == "") ? "" : Currentptb.Col8);
                        Changetzflag = false;
                        break;
                   case "移交":
                       Changetzflag = true;
                      labtz.Visible=true;
                     sptz.Visible=true;
                       labtz.Text = "移交投资";
                      sptz.Value = Changetz((Currentptb.Col9 == null || Currentptb.Col9 == "") ? "" : Currentptb.Col9);
                      Changetzflag = false;
                       break;
                       default:
                      labtz.Visible=false;
                     sptz.Visible=false;
                        break;
                }
             }

        private void sptz_EditValueChanged(object sender, EventArgs e)
        {
            if (!Changetzflag)
            {
                switch (combState.Text)
                {
                    case "可行性研究":
                        Currentptb.Col6 = sptz.Value.ToString();
                        break;
                    case "初步设计":
                        Currentptb.Col7 = sptz.Value.ToString();
                        break;
                    case "施工设计":
                        Currentptb.Col8 = sptz.Value.ToString();
                        break;
                    case "移交":
                        Currentptb.Col9 = sptz.Value.ToString();
                        break;
                    default:
                      
                        break;
                }
            }
          
        }
        
        private void combState_SelectedIndexChanged(object sender, EventArgs e)
        {
            DelTz();
            if (!ISfirstload)
            {
             
                ChangeStatetime = true;
                switch (combState.Text)
                {
                    case "规划审查":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime1 == null || Currentptb.Stime1 =="")? "" : Currentptb.Stime1);
                        Currentptb.Stime1 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "可行性研究":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime2 == null || Currentptb.Stime2 == "") ? "" : Currentptb.Stime2);
                        Currentptb.Stime2 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "获取支持文件":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime3 == null || Currentptb.Stime3 == "") ? "" : Currentptb.Stime3);
                        Currentptb.Stime3 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "初步设计":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime4 == null || Currentptb.Stime4 == "") ? "" : Currentptb.Stime4);
                        Currentptb.Stime4 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "施工设计":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime5 == null || Currentptb.Stime5 == "") ? "" : Currentptb.Stime5);
                        Currentptb.Stime5 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "施工":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime6 == null || Currentptb.Stime6 == "") ? "" : Currentptb.Stime6);
                        Currentptb.Stime6 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "投产运行":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime7 == null || Currentptb.Stime7 == "") ? "" : Currentptb.Stime7);
                        Currentptb.Stime7= dateStateTime.Value.ToShortDateString();
                        break;
                    case "移交":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime8 == null || Currentptb.Stime8 == "") ? "" : Currentptb.Stime8);
                        Currentptb.Stime8 = dateStateTime.Value.ToShortDateString();
                        break;
                    default:
                        break;
                }
                ChangeStatetime = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void dateStateTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            FrmProjTZEdit frm = new FrmProjTZEdit();
            frm.Currentptb = Currentptb;
            spinEdit3.Text = Currentptb.AllVolumn.ToString();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Currentptb = frm.Currentptb;
                ChangeStatetime = true;
                if (!Changetzflag)
                {
                    switch (combState.Text)
                    {
                        case "可行性研究":
                            sptz.Value = Changetz((Currentptb.Col6 == null || Currentptb.Col6 == "") ? "" : Currentptb.Col6);
                            break;
                        case "初步设计":
                            sptz.Value = Changetz((Currentptb.Col7 == null || Currentptb.Col7 == "") ? "" : Currentptb.Col7);
                            break;
                        case "施工设计":
                            sptz.Value = Changetz((Currentptb.Col8 == null || Currentptb.Col8 == "") ? "" : Currentptb.Col8);
                            break;
                        case "移交":
                            sptz.Value = Changetz((Currentptb.Col9 == null || Currentptb.Col9 == "") ? "" : Currentptb.Col9);
                            break;
                        default:

                            break;
                    }
                }

            }
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            FrmProjTimeEdit frm = new FrmProjTimeEdit();
            frm.Currentptb = Currentptb;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Currentptb = frm.Currentptb;
                ChangeStatetime = true;
                switch (combState.Text)
                {
                    case "规划审查":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime1 == null || Currentptb.Stime1 == "") ? "" : Currentptb.Stime1);
                        Currentptb.Stime1 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "可行性研究":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime2 == null || Currentptb.Stime2 == "") ? "" : Currentptb.Stime2);
                        Currentptb.Stime2 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "获取支持文件":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime3 == null || Currentptb.Stime3 == "") ? "" : Currentptb.Stime3);
                        Currentptb.Stime3 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "初步设计":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime4 == null || Currentptb.Stime4 == "") ? "" : Currentptb.Stime4);
                        Currentptb.Stime4 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "施工设计":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime5 == null || Currentptb.Stime5 == "") ? "" : Currentptb.Stime5);
                        Currentptb.Stime5 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "施工":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime6 == null || Currentptb.Stime6 == "") ? "" : Currentptb.Stime6);
                        Currentptb.Stime6 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "投产运行":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime7 == null || Currentptb.Stime7 == "") ? "" : Currentptb.Stime7);
                        Currentptb.Stime7 = dateStateTime.Value.ToShortDateString();
                        break;
                    case "移交":
                        dateStateTime.Value = ChangeTime((Currentptb.Stime8 == null || Currentptb.Stime8 == "") ? "" : Currentptb.Stime8);
                        Currentptb.Stime8 = dateStateTime.Value.ToShortDateString();
                        break;
                    default:
                        break;
                }
                ChangeStatetime = false;
            }
        }

       

       

      

       
    }
}