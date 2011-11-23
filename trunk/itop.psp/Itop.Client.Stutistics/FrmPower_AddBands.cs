using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Common;
using Itop.Client.Base;
using System.Collections;


namespace Itop.Client.Stutistics
{
    public partial class FrmPower_AddBands : FormBase
    {
        private string classname = "";
        private string classname1 = "";
        private string type = "";
        private string type2 = "";
        private string flag = "";
        private string classtype = "";
        private bool isUpdate = false;
        private string addflag = "";

        private ArrayList al = new ArrayList(); 
        PowerSubstationLine psp_sl = new PowerSubstationLine();

        private List<string> s = new List<string>();
        public ArrayList Al
        {
            get { return al; }
            set { al = value; }
        }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string ClassName
        {
            get { return classname; }
            set { classname = value; }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string ClassType
        {
            get { return classtype; }
            set { classtype = value; }
        }


        /// <summary>
        /// 类型 1变电站 2线路
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }


        /// <summary>
        /// 类型 1变电站 2线路
        /// </summary>
        public string Type2
        {
            get { return type2; }
            set { type2 = value; }
        }

        /// <summary>
        /// 1现状，2规划
        /// </summary>
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        public string AddFlag
        {
            get { return addflag; }
            set { addflag = value; }
        }

        /// <summary>
        /// 是否更新
        /// </summary>
        public bool IsUpdate
        {
            get { return isUpdate; }
            set { isUpdate = value; }
        }
        public FrmPower_AddBands()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            PowerSubstationLine pl = new PowerSubstationLine();
            classname = textBox1.Text;
            pl.Type2 = type2;
            pl.Flag = flag;
            pl.Title = classname;
//            pl.Type = type;
            ////PowerSubstationLine psp_l = new PowerSubstationLine();
            ////psp_l = (PowerSubstationLine)Common.Services.BaseService.GetObject("SelectPowerSubstationLineByTitleTypeFlag", pl);
      
            ////psp_l.Type2 = type2;
            ////psp_l.Flag = flag;

            psp_sl.Title = textBox1.Text;
            if (classname.ToString() == "")
            {
                MsgBox.Show("类别名称不能为空");
                return;
            }

            if (al.Contains(textBox1.Text) == true)
            {
                MsgBox.Show("已经存在此分类，要想添加，请先删除已存在的" + textBox1.Text + "!");
            
                return;
            }
            try
            {
                PowerSubstationLine plobj = new PowerSubstationLine();
                plobj = (PowerSubstationLine)Common.Services.BaseService.GetObject("SelectPowerSubstationLineByTitleTypeFlag", pl);
                if (plobj == null)
                {
                    try
                    {
                        if (!IsUpdate)
                        {
                            bool bz = false;
                            string f = "";
                            foreach (string ss in s)
                            {
                                PowerSubstationLine ps = new PowerSubstationLine();
                                //ps.Title = classname;
                                ps.Flag = flag;
                                //ps.Type = type;
                                ps.Type2 = type2;
                                ps.ClassType = ss;
                                
 //                               object obj = Common.Services.BaseService.GetObject("SelectPowerSubstationLineByTitleTypeClass1", ps);
                                object obj = Common.Services.BaseService.GetObject("SelectPowerSubstationLineByClassTypeType2Class1", ps);
                                if (obj == null)
                                {
                                    classtype = ss;
                                    bz = true;
                                    f = ss;
                                    break;
                                }

                            }
                            if (bz)
                            {
                             
                                psp_sl.ClassType = f;
                                psp_sl.Type = type;
                                Common.Services.BaseService.Create<PowerSubstationLine>(psp_sl);
                            }


                        }
                        else
                        {
                            Common.Services.BaseService.Update<PowerSubstationLine>(psp_sl);
                        }

                        this.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("出错啦：" + ex.Message);
                    }
                }
                else
                {
                    MsgBox.Show("此分类已经存在，请重新输入！");
                }

            }
            catch { }
        }

        private void FrmPower_AddBands_Load(object sender, EventArgs e)
        {
            s.Add("S1");
            s.Add("S2");
            s.Add("S3");
            s.Add("S4");
            s.Add("S5");
            s.Add("S6");
            s.Add("S7");
            s.Add("S8");
            s.Add("S9");
            s.Add("S10");
            s.Add("S11");
            s.Add("S12");
            s.Add("S13");
            s.Add("S14");
            s.Add("S15");
            s.Add("S16");
            s.Add("S17");
            s.Add("S18");
            s.Add("S19");
            s.Add("S20");

            textBox1.Text = classname;
            classname1 = classname;

            if (!IsUpdate)
            {  

                psp_sl = new PowerSubstationLine();
                //psp_sl.Type = type;
                psp_sl.Type2 = type2;
                psp_sl.Flag = flag;
                //IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType2", psp_sl);          
                IList<PowerSubstationLine> li = Itop.Client.Common.Services.BaseService.GetList<PowerSubstationLine>("SelectPowerSubstationLineByFlagType", psp_sl);          
           
                if (addflag == "powerflag")
                    {

                        if (li.Count >= 30)
                        {
                            MsgBox.Show("本模块最多允许添加30个自定义列！");
                            this.Close();
                            return;
                        }
                        s.Add("S21");
                        s.Add("S22");
                        s.Add("S23");
                        s.Add("S24");
                        s.Add("S25");
                        s.Add("S26");
                        s.Add("S27");
                        s.Add("S28");
                        s.Add("S29");
                        s.Add("S30");
                    }
                    if (addflag == "form1_liaoflag")
                    {
                        if (li.Count >= 20)
                        {
                            MsgBox.Show("本模块最多允许添加20个自定义列！");
                            this.Close();
                            return;
                        }
                    }


             
            }
            else
            {
                PowerSubstationLine ps = new PowerSubstationLine();
                ps.Title = classname;
               // ps.ClassType = classtype;
                ps.Flag = flag;
                //ps.Type = type;
                ps.Type2 = type2;
                
                //psp_sl = (PowerSubstationLine)Common.Services.BaseService.GetObject("SelectPowerSubstationLineByTitleTypeFlag", ps);
                psp_sl = (PowerSubstationLine)Common.Services.BaseService.GetObject("SelectPowerSubstationLineByTitleType2Flag", ps);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //classname = psp_sl.Title = textBox1.Text;
            //if (classname.ToString() == "")
            //{
            //    MsgBox.Show("类别名称不能为空");
            //    return;
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}