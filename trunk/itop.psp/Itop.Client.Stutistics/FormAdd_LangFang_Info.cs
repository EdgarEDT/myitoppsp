using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using DevExpress.XtraEditors;
using Itop.Common;
using Itop.Domain.Graphics;
using System.Collections;
using Itop.Domain.Stutistics;
using Itop.Client.Common;
using DevExpress.XtraEditors.Controls;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class FormAdd_LangFang_Info : FormBase
    {
        public FormAdd_LangFang_Info()
        {
            InitializeComponent();
        }
        double sumvaluedata = 0;
        double sumvalueLine = 0;
        private bool isupdate = false;
        private string flag = "";
        private string poweruid = "";
        private string powerid = "";
        private Hashtable hs = new Hashtable();
        private string islineflag = "";
         PSP_PowerProValues_LangFang objec = new PSP_PowerProValues_LangFang();
        public PSP_PowerProValues_LangFang OBJ = new PSP_PowerProValues_LangFang();
        PSP_PowerProValues_LangFang obj = new PSP_PowerProValues_LangFang();
        private bool isstuff = false;

        public bool IsStuff
        {
            set { isstuff = value; }
            get { return isstuff; }
        }

        /// <summary>
        /// 规划ID
        /// </summary>
        public string FlagId
        {
            set { flag = value; }
            get { return flag; }
        }

        /// <summary>
        /// 变电站，线路ID
        /// </summary>
        //public string PowerId
        //{
        //    set { powerid = value; }
        //    get { return powerid; }
        //}


        public string PowerUId
        {
            set { poweruid = value; }
            get { return poweruid; }
        }

        public string IsLineFlag
        {
            set { islineflag = value; }
            get { return islineflag; }
        }
        private bool isline = false;
        public bool IsLine
        {
            set { isline = value; }
            get { return isline; }
        }

        private bool isPower = false;
        public bool IsPower
        {
            set { isPower = value; }
            get { return isPower; }
        }

        private string parentID ="";
        public string ParentID
        {
            set { parentID = value; }
            get { return parentID; }
        }
 
       public bool Isupdate
        {
            set { isupdate = value; }
            get { return isupdate; }
        }
        private void FormAddInfo_LangFang_Load(object sender, EventArgs e)
        {
            if (isupdate == true)
            {
               
                PSP_PowerProValues_LangFang ppt = new PSP_PowerProValues_LangFang();
                ppt.ID = poweruid;
                ppt.Flag2 = flag;
                obj = Common.Services.BaseService.GetOneByKey<PSP_PowerProValues_LangFang>(ppt);
                OBJ = obj;
                isupdate = true;
                IList<PSP_PowerProValues_LangFang> list = new List<PSP_PowerProValues_LangFang>();
                list.Add(OBJ);
                this.vGridControl1.DataSource = list;
                UpDateInit();
            }
              
                if (isupdate == false)
                {
                    OBJ.ID = Guid.NewGuid().ToString();
                    OBJ.CreateTime = DateTime.Now;
                    OBJ.UpdateTime = DateTime.Now;
                    OBJ.ParentID = parentID;
                    OBJ.Flag2 = flag;
                    OBJ.L12 = 0;
                    OBJ.Flag = 2;
                    OBJ.L4 = "220";
                    OBJ.L5 = "1";
                    OBJ.L7 = "户内站";
                    OBJ.L11= "0";
                    OBJ.L10 = 0;
                    CreateInit();
                    IList<PSP_PowerProValues_LangFang> list = new List<PSP_PowerProValues_LangFang>();
                    list.Add(OBJ);
                    this.vGridControl1.DataSource = list;

                }

           
          //  this.电压等级.Properties.RowEdit.EditValueChanging+=new ChangingEventHandler(RowEdit_EditValueChanging1);
            this.电压等级.Properties.RowEdit.Click += new EventHandler(RowEdit_Click);
          //  this.电压等级.Properties.RowEdit.Click += new EventHandler(RowEdit_Click);
            this.主变台数及容量.Properties.RowEdit.Click +=new EventHandler(RowEdit_Click1);
            this.出线规模.Properties.RowEdit.Click += new EventHandler(RowEdit_Click2);
            this.接线形式.Properties.RowEdit.Click += new EventHandler(RowEdit_Click3);
            this.无功配置.Properties.RowEdit.Click += new EventHandler(RowEdit_Click4);
            this.投资造价.Properties.RowEdit.Click += new EventHandler(RowEdit_Click5);
            this.造价比例.Properties.RowEdit.Click += new EventHandler(RowEdit_Click6);
            this.投资造价.Properties.RowEdit.EditValueChanged += new EventHandler(RowEdit_EditValueChanged);
            this.造价比例.Properties.RowEdit.EditValueChanging += new ChangingEventHandler(RowEdit_EditValueChanging);

            LineInfo li22 = Common.Services.BaseService.GetOneByKey<LineInfo>(powerid);
            if (li22 != null || OBJ.Flag == 1)
            {
                isline = true;
            }
            substation sb = Common.Services.BaseService.GetOneByKey<substation>(powerid);
            if (sb != null || OBJ.Flag == 2)
            {
                isPower = true;
            }
        }

        void RowEdit_Click(object sender, EventArgs e)
        {

            if (OBJ.Flag != 1 && OBJ.Flag != 2)
            {
                MsgBox.Show("类型的值无效！请先选择类型值，在继续操作。");
                return;
            }
            if (OBJ.Flag == 1)
            {
                this.categoryRow1.Visible = true;
                this.主变台数.Enabled = false;
                this.单台容量.Enabled = false;
                this.出线规模.Enabled = false;
                this.接线形式.Enabled = false;
                this.无功配置.Enabled = false;
                this.主变台数及容量.Enabled = false;

            }
            if (OBJ.Flag == 2)
            {
                this.categoryRow1.Visible = true;
                this.主变台数.Enabled = true;
                this.单台容量.Enabled = true;
                this.出线规模.Enabled = true;
                this.接线形式.Enabled = true;
                this.无功配置.Enabled = true;
                this.主变台数及容量.Enabled = true;

            }
        }

        void RowEdit_Click1(object sender, EventArgs e)
        {
            if (OBJ.L4 == ""||OBJ.L4==null)
            {
                MsgBox.Show("电压等级为必填项！");
                return;
            }
        }
        void RowEdit_Click2(object sender, EventArgs e)
        {
            if (OBJ.L4 == ""||OBJ.L4==null)
            {
                MsgBox.Show("电压等级为必填项！");
                return;
            }
        }
        void RowEdit_Click3(object sender, EventArgs e)
        {
           
            if (OBJ.L4 == ""||OBJ.L4==null)
            {
                MsgBox.Show("电压等级为必填项！");
                return;
            }
        }
        void RowEdit_Click4(object sender, EventArgs e)
        {
                 
            if (OBJ.L4 == ""||OBJ.L4==null)
            {
                MsgBox.Show("电压等级为必填项！");
                return;
            }
        }
        void RowEdit_Click5(object sender, EventArgs e)
        {

          
        }
        void RowEdit_Click6(object sender, EventArgs e)
        {
          
        }
        void RowEdit_EditValueChanging(object sender, ChangingEventArgs e)
        {
           
        }

        void RowEdit_EditValueChanged(object sender, EventArgs e)
        {
           
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

           //for (int i = 0; i < te.Length; i++)
           //{
           //    SaveCellValue(te[i].Name.Replace("Text", ""), poweruid, te[i].Text.Trim());
           //}
            if (OBJ.L4 == "" || OBJ.L4 == null)
            {
                MsgBox.Show("电压等级为必填项！");
                return;
            }
            if (OBJ.L3 == "" || OBJ.L3 == null)
            {
                MsgBox.Show("工程名称为必填项！");
                return;
            }
           if (isline)
           {
               try
               {

                   LineInfo li = Common.Services.BaseService.GetOneByKey<LineInfo>(powerid);

                   if (li != null)
                   {
                       li.LineName = OBJ.L3;
                       li.Voltage = OBJ.L4;
                       li.Length = OBJ.L8;
                       li.LineType = OBJ.L9;
                       Common.Services.BaseService.Update<LineInfo>(li);
                   }


               }
               catch { }
           }
           if (isPower)
           {
               try
               {
                   substation sb = Common.Services.BaseService.GetOneByKey<substation>(powerid);
                   Substation_Info sub = new Substation_Info();
                   sub.Code = powerid;
                   Substation_Info sbinfo = (Substation_Info)Common.Services.BaseService.GetObject("SelectSubstation_InfoByCode", sub);
                   if (sbinfo != null)
                   {
                       sbinfo.L2 = double.Parse(OBJ.L6);
                       sbinfo.L3 = int.Parse(OBJ.L5);
                       Common.Services.BaseService.Update("UpdateSubstation_InfoByUID", sub);
                   }

                   if (sb != null)
                   {
                       try
                       {
                           sb.EleName = OBJ.L3;
                           sb.ObligateField1 = OBJ.L4;
                       }
                       catch { }

                       Common.Services.BaseService.Update<substation>(sb);
                   }
               }
               catch { }
           }

           try
           {
               Common.Services.BaseService.Update("UpdatePSP_PowerProValues_LangFangBy", OBJ);
           }
           catch { }

           this.DialogResult = DialogResult.OK;
       }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void vGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
 
            
            repositoryItemComboBox3.Items.Clear();
            this.repositoryItemComboBox10.Items.Clear();
            if (e.Row.Properties.FieldName.ToString() == "Flag" || e.Row.Properties.FieldName.ToString() == "L4" || e.Row.Properties.FieldName.ToString() == "L7")
            {
                if (OBJ.L4 == "220")
                {
                    Project_Sum pjt = new Project_Sum();
                    if (OBJ.Flag == 2)
                    {
                        pjt.S1 = "220";
                        pjt.S5 = "2";
                        pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                        foreach (Project_Sum ps in lt)
                        {
                            if (ps.T5 != null && ps.T5 != "")
                                if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                                {
                                    this.repositoryItemComboBox3.Items.Add(ps.T5);
                                }
                        }
                    }
                    pjt.S1 = "220";
                    pjt.S5 = "1";

                    IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                    foreach (Project_Sum pss in ltt)
                    {
                        if (pss.L1 != null && pss.L1 != "")
                            if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                            {
                                this.repositoryItemComboBox10.Items.Add(pss.L1);
                            }
                    }

                }
                if (OBJ.L4 == "110")
                {

                    Project_Sum pjt = new Project_Sum();
                    if (OBJ.Flag == 2)
                    {
                        pjt.S1 = "110";
                        pjt.S5 = "2";
                        pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                        foreach (Project_Sum ps in lt)
                        {
                            if (ps.T5 != null && ps.T5 != "")
                                if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                                {
                                    this.repositoryItemComboBox3.Items.Add(ps.T5);
                                }
                        }

                    }
                    pjt.S1 = "110";
                    pjt.S5 = "1";
                    IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                    foreach (Project_Sum pss in ltt)
                    {
                        if (pss.L1 != null && pss.L1 != "")
                            if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                            {
                                this.repositoryItemComboBox10.Items.Add(pss.L1);
                            }
                    }
                }
                if (OBJ.L4 == "35")
                {

                    Project_Sum pjt = new Project_Sum();
                    if (OBJ.Flag == 2)
                    {
                        pjt.S1 = "35";
                        pjt.S5 = "2";
                        pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                        foreach (Project_Sum ps in lt)
                        {
                            if (ps.T5 != null && ps.T5 != "")
                                if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                                {
                                    this.repositoryItemComboBox3.Items.Add(ps.T5);
                                }
                        }

                    }

                    pjt.S1 = "35";
                    pjt.S5 = "1";
                    IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                    foreach (Project_Sum pss in ltt)
                    {
                        if (pss.L1 != null && pss.L1 != "")
                            if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                            {
                                this.repositoryItemComboBox10.Items.Add(pss.L1);
                            }
                    }

                }
                if (OBJ.L4 == "66")
                {

                    Project_Sum pjt = new Project_Sum();

                    if (OBJ.Flag == 2)
                    {
                        pjt.S1 = "66";
                        pjt.S5 = "2";
                        pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                        foreach (Project_Sum ps in lt)
                        {
                            if (ps.T5 != null && ps.T5 != "")
                                if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                                {
                                    this.repositoryItemComboBox3.Items.Add(ps.T5);
                                }
                        }
                    }
                    pjt.S1 = "66";
                    pjt.S5 = "1";
                    IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                    foreach (Project_Sum pss in ltt)
                    {
                        if (pss.L1 != null && pss.L1 != "")
                            if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                            {
                                this.repositoryItemComboBox10.Items.Add(pss.L1);
                            }
                    }
                }
                if (OBJ.L4 == "500")
                {

                    Project_Sum pjt = new Project_Sum();
                    if (OBJ.Flag == 2)
                    {
                        pjt.S1 = "500";
                        pjt.S5 = "2";
                        pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                        foreach (Project_Sum ps in lt)
                        {
                            if (ps.T5 != null && ps.T5 != "")
                                if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                                {
                                    this.repositoryItemComboBox3.Items.Add(ps.T5);
                                }
                        }

                    }
                    pjt.S1 = "500";
                    pjt.S5 = "1";
                    IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                    foreach (Project_Sum pss in ltt)
                    {
                        if (pss.L1 != null && pss.L1 != "")
                            if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                            {
                                this.repositoryItemComboBox10.Items.Add(pss.L1);
                            }
                    }

                }
            }
            if (e.Row.Properties.FieldName.ToString() == "Flag")
            {
                OBJ.Flag =int.Parse(e.Value.ToString());
                if (OBJ.Flag == 1)
                {
                    OBJ.L1 = "";
                    OBJ.L2 = "";
                    OBJ.L13 = "";
                    OBJ.L14 = "";
                    this.categoryRow1.Visible = true;
                    this.建设形式.Enabled = false;
                    this.单台容量.Enabled = false;
                    this.主变台数.Enabled = false;
                    this.出线规模.Enabled = false;
                    this.接线形式.Enabled = false;
                    this.无功配置.Enabled = false;
                    this.主变台数及容量.Enabled = false;

                }
                if (OBJ.Flag == 2)
                {

                    this.categoryRow1.Visible = true;
                    this.建设形式.Enabled = true;
                    this.单台容量.Enabled = true;
                    this.主变台数.Enabled = true;
                    this.出线规模.Enabled = true;
                    this.接线形式.Enabled = true;
                    this.无功配置.Enabled = true;
                    this.主变台数及容量.Enabled = true;

                }
            }
            if (e.Row.Properties.FieldName.ToString() == "L6")
            {
                if (OBJ.Flag == 2)
                {
                    this.repositoryItemComboBox5.Items.Clear();

                    if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != "" && OBJ.L7 != "")
                    {
                        string str = "S5=" + OBJ.Flag + "and S1=" + OBJ.L4 + "and T1 like '" + OBJ.L5 + "%'" + "and T5 =" + OBJ.L6 + "and Type =" + OBJ.L7;
                        IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByValues", str);
                        if (OBJ.L11 == null || OBJ.L11 == "")
                            OBJ.L11 = "0";
                        if (sum.Count == 0)
                        {
                            OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                        }

                        if (sum.Count == 1)
                        {
                            foreach (Project_Sum pp in sum)
                                OBJ.L11 =  Convert.ToString(pp.Num + int.Parse(OBJ.L11));
                                OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                        }

                        foreach (Project_Sum li in sum)
                        {
                            this.repositoryItemComboBox5.Items.Add(li.T1);
                        }
                    }
                }
            }
            if (e.Row.Properties.FieldName.ToString() == "L1")
            {
                if (OBJ.Flag == 2)
                {
                    this.repositoryItemComboBox6.Items.Clear();
                    if (OBJ.L1 != null && OBJ.L1 != "" && OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != ""&& OBJ.L7 != "")
                    {
                        Project_Sum ps = new Project_Sum();
                        ps.T1 = OBJ.L1;
                        ps.S5 = OBJ.Flag.ToString();
                        ps.S1 = OBJ.L4;
                        ps.T5 = OBJ.L6;
                        ps.Type=OBJ.L7;
                        IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByvalue", ps);
                        if (OBJ.L11 == null || OBJ.L11 == "")
                            OBJ.L11 = "0";
                        if (sum.Count == 0)
                        {
                            OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                        }

                        //if (sum.Count == 1)
                        //{
                        //    foreach (Project_Sum pp in sum)
                        //        OBJ.L11 = Convert.ToString(pp.Num + int.Parse(OBJ.L11));
                        //    OBJ.L10 = Convert.ToString(double.Parse(OBJ.L11) * OBJ.L12);
                        //}
                        foreach (Project_Sum li in sum)
                        {
                            this.repositoryItemComboBox6.Items.Add(li.T2);
                        }
                    }
                }
                
                return;
           
            }
            if (e.Row.Properties.FieldName.ToString() == "L2")
            {
                if (OBJ.Flag == 2)
                {
                    this.repositoryItemComboBox7.Items.Clear();
                    if (OBJ.L2 != null && OBJ.L2 != "" && OBJ.L1 != null && OBJ.L1 != "" && OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != "")
                    {
                        Project_Sum ps = new Project_Sum();
                        ps.T1 = OBJ.L1;
                        ps.T2 = OBJ.L2;
                        ps.S5 = OBJ.Flag.ToString();
                        ps.S1 = OBJ.L4;
                        ps.T5 = OBJ.L6;
                        ps.Type=OBJ.L7;
                        IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByvalue1", ps);
                        if (OBJ.L11 == null || OBJ.L11 == "")
                            OBJ.L11 = "0";
                        if (sum.Count == 0)
                        {
                            OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                        }

                        //if (sum.Count == 1)
                        //{
                        //    foreach (Project_Sum pp in sum)
                        //        OBJ.L11 = Convert.ToString(pp.Num + int.Parse(OBJ.L11));
                        //    OBJ.L10 = Convert.ToString(double.Parse(OBJ.L11) * OBJ.L12);
                        //}
                        foreach (Project_Sum li in sum)
                        {
                            this.repositoryItemComboBox7.Items.Add(li.T3);
                        }
                    }
                }
               
                return;
            }
            if (e.Row.Properties.FieldName.ToString() == "L13")
            {
                if (OBJ.Flag == 2)
                {
                    this.repositoryItemComboBox8.Items.Clear();
                    if (OBJ.L13 != null && OBJ.L13 != "" && OBJ.L2 != null && OBJ.L2 != "" && OBJ.L1 != null && OBJ.L1 != "" && OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != "")
                    {
                        Project_Sum ps = new Project_Sum();
                        ps.T1 = OBJ.L1;
                        ps.T2 = OBJ.L2;
                        ps.T3 = OBJ.L13;
                        ps.S5 = OBJ.Flag.ToString();
                        ps.S1 = OBJ.L4;
                        ps.T5 = OBJ.L6;
                        ps.Type=OBJ.L7;
                        IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByvalue2", ps);
                        if (OBJ.L11 == null || OBJ.L11 == "")
                            OBJ.L11 = "0";
                        if (sum.Count == 0)
                        {
                            OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                        }

                        //if (sum.Count == 1)
                        //{
                        //    foreach (Project_Sum pp in sum)
                        //        OBJ.L11 = Convert.ToString(pp.Num + int.Parse(OBJ.L11));
                        //    OBJ.L10 = Convert.ToString(double.Parse(OBJ.L11) * OBJ.L12);
                        //}
                        foreach (Project_Sum li in sum)
                        {
                             this.repositoryItemComboBox8.Items.Add(li.T4);//暂时没有确定字段
                        }
                    }
                }
                
                return;  
            }
           if (e.Row.Properties.FieldName.ToString() == "L7")//建设形式
            {
                if (OBJ.Flag == 2)
                {
                    this.repositoryItemComboBox3.Items.Clear();
                    if (OBJ.L13 != null && OBJ.L13 != "" && OBJ.L2 != null && OBJ.L2 != "" && OBJ.L1 != null && OBJ.L1 != "" && OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != "")
                    {
                        Project_Sum ps = new Project_Sum();
                        ps.S5 = OBJ.Flag.ToString();
                        ps.S1 = OBJ.L4;
                        ps.Type=OBJ.L7;
                        IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByvalueL7", ps);
                        if (OBJ.L11 == null || OBJ.L11 == "")
                            OBJ.L11 = "0";
                        if (sum.Count == 0)
                        {
                            OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                        }

                        //if (sum.Count == 1)
                        //{
                        //    foreach (Project_Sum pp in sum)
                        //        OBJ.L11 = Convert.ToString(pp.Num + int.Parse(OBJ.L11));
                        //    OBJ.L10 = Convert.ToString(double.Parse(OBJ.L11) * OBJ.L12);
                        //}
                        foreach (Project_Sum li in sum)
                        {
                             this.repositoryItemComboBox3.Items.Add(li.T4);//暂时没有确定字段
                        }
                    }
                }
                
                return;  
            }

            if (e.Row.Properties.FieldName.ToString() == "L9")
            {
                
                    this.repositoryItemComboBox11.Items.Clear();
                    if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L9 != null && OBJ.L9 != "")
                    {
                        Project_Sum ps = new Project_Sum();
                        ps.S5 = "1";
                        ps.S1 = OBJ.L4;
                        ps.L1 = OBJ.L9;
                        IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue", ps);

                        foreach (Project_Sum li in sum)
                        {
                            this.repositoryItemComboBox11.Items.Add(li.L1);
                        }


                    }
                
            }

            if (e.Row.Properties.FieldName.ToString() == "L15")
            {

                this.repositoryItemComboBox12.Items.Clear();
                if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L9 != null && OBJ.L9 != "" && OBJ.L15 != null && OBJ.L15 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.S5 = "1";
                    ps.S1 = OBJ.L4;
                    ps.L1 = OBJ.L9;
                    ps.L2 = OBJ.L15;

                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue1", ps);

                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox12.Items.Add(li.L1);
                    }
                }
               
            }

            if (e.Row.Properties.FieldName.ToString() == "Flag" || e.Row.Properties.FieldName.ToString() == "L4" || e.Row.Properties.FieldName.ToString() == "L8" || e.Row.Properties.FieldName.ToString() == "L9" || e.Row.Properties.FieldName.ToString() == "L15" || e.Row.Properties.FieldName.ToString() == "L16")
            {
                if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L9 != null && OBJ.L9 != "" && OBJ.L15 != null && OBJ.L15 != "")//变电站下的线路，线路信息
                {
                    Project_Sum ps = new Project_Sum();
                    ps.S5 = "1";
                    ps.S1 = OBJ.L4;
                    ps.L1 = OBJ.L9;
                    ps.L2 = OBJ.L15;
                    ps.L3 = OBJ.L16;
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue2", ps);
                    if (sum.Count == 1)
                    {
                        if (OBJ.L8 == null || OBJ.L8 == "")
                            OBJ.L8 = "0";
                        foreach (Project_Sum pp in sum)
                        {
                            if (pp.Num.ToString() == null || pp.Num.ToString() == "")
                                pp.Num = 0;

                            sumvalueLine = pp.Num * double.Parse(OBJ.L8);
                        }
                        OBJ.L11 = Convert.ToString(sumvalueLine + sumvaluedata);
                        OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                    }
                }
            }
            if (e.Row.Properties.FieldName.ToString() == "Flag" || e.Row.Properties.FieldName.ToString() == "L4" || e.Row.Properties.FieldName.ToString() == "L5" || e.Row.Properties.FieldName.ToString() == "L6" || e.Row.Properties.FieldName.ToString() == "L7" || e.Row.Properties.FieldName.ToString() == "L1" || e.Row.Properties.FieldName.ToString() == "L2" || e.Row.Properties.FieldName.ToString() == "L13" || e.Row.Properties.FieldName.ToString() == "L14")
            {
                if (OBJ.Flag == 2)//计算最终造价
                {
                    if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != "")
                    {
                        Project_Sum ps = new Project_Sum();
                        ps.T1 = OBJ.L1;
                        ps.T2 = OBJ.L2;
                        //ps.T3 = OBJ.L13;
                        //ps.T4 = OBJ.L14;
                        ps.S5 = OBJ.Flag.ToString();
                        ps.S1 = OBJ.L4;
                        ps.T5 = OBJ.L6;
                        ps.Type = OBJ.L7;
                        Project_Sum sum = (Project_Sum)Common.Services.BaseService.GetObject("SelectProject_SumByvalue3", ps);
                        if (OBJ.L11 == null || OBJ.L11 == "")
                            OBJ.L11 = "0";
                        if (sum == null)
                        {
                            OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                        }
                        if (sum != null)
                        {
                            sumvaluedata = sum.Num;

                            OBJ.L11 = Convert.ToString(sum.Num + sumvalueLine);
                            OBJ.L10 =double.Parse(OBJ.L11) * OBJ.L12;

                        }

                    }
                }
            }
            if (e.Row.Properties.FieldName.ToString() == "L11")
            {
                OBJ.L11 = e.Value.ToString();
                if (OBJ.L11 != null && OBJ.L11 != "" && OBJ.L12 != null && OBJ.L12.ToString() != "")
                    OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
            }

            if (e.Row.Properties.FieldName.ToString() == "L12")
            {
                OBJ.L12 =double.Parse(e.Value.ToString());
                if (OBJ.L11 != null && OBJ.L11 != "" && OBJ.L12 != null && OBJ.L12.ToString() != "")
                    OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
            }
        this.vGridControl1.Refresh();
           
            return;
        }
        private void UpDateInit()
        {

            double sumvalue = 0;
           
                if (OBJ.L4 == "220")
                {
                    Project_Sum pjt = new Project_Sum();
                    if (OBJ.Flag == 2)
                    {
                        pjt.S1 = "220";
                        pjt.S5 = "2";
                        pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                        foreach (Project_Sum ps in lt)
                        {
                            if (ps.T5 != null && ps.T5 != "")
                            if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                            {
                                this.repositoryItemComboBox3.Items.Add(ps.T5);
                            }
                        }
                      
                    }
                        pjt.S1 = "220";
                        pjt.S5 = "1";

                        IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                        foreach (Project_Sum pss in ltt)
                        {
                            if (pss.L1 != null && pss.L1 != "")
                            if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                            {
                                this.repositoryItemComboBox10.Items.Add(pss.L1);
                            }
                        }

                }
                if (OBJ.L4 == "110")
                {

                    Project_Sum pjt = new Project_Sum();
                    if (OBJ.Flag == 2)
                    {
                        pjt.S1 = "110";
                        pjt.S5 = "2";
                        pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                        foreach (Project_Sum ps in lt)
                        {
                            if (ps.T5 != null && ps.T5 != "")
                            if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                            {
                                this.repositoryItemComboBox3.Items.Add(ps.T5);
                            }
                        }
                       
                    }
                        pjt.S1 = "110";
                        pjt.S5 = "1";
                        IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                        foreach (Project_Sum pss in ltt)
                        {
                            if (pss.L1 != null && pss.L1 != "")
                            if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                            {
                                this.repositoryItemComboBox10.Items.Add(pss.L1);
                            }
                        }
                }
                if (OBJ.L4 == "35")
                {

                    Project_Sum pjt = new Project_Sum();
                    if (OBJ.Flag == 2)
                    {
                        pjt.S1 = "35";
                        pjt.S5 = "2";
                        pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                        foreach (Project_Sum ps in lt)
                        {
                            if (ps.T5 != null && ps.T5 != "")
                            if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                            {
                                this.repositoryItemComboBox3.Items.Add(ps.T5);
                            }
                        }
                       
                    }

                        pjt.S1 = "35";
                        pjt.S5 = "1";
                        IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                        foreach (Project_Sum pss in ltt)
                        {
                            if (pss.L1 != null && pss.L1 != "")
                            if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                            {
                                this.repositoryItemComboBox10.Items.Add(pss.L1);
                            }
                        }
                }
                if (OBJ.L4 == "66")
                {

                    Project_Sum pjt = new Project_Sum();

                    if (OBJ.Flag == 2)
                    {
                        pjt.S1 = "66";
                        pjt.S5 = "2";
                        pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                        foreach (Project_Sum ps in lt)
                        {
                            if (ps.T5 != null && ps.T5 != "")
                            if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                            {
                                this.repositoryItemComboBox3.Items.Add(ps.T5);
                            }
                        }
                       
                    }

                        pjt.S1 = "66";
                        pjt.S5 = "1";
                        IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                        foreach (Project_Sum pss in ltt)
                        {
                            if (pss.L1 != null && pss.L1 != "")
                            if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                            {
                                this.repositoryItemComboBox10.Items.Add(pss.L1);
                            }
                        }
                }
                if (OBJ.L4 == "500")
                {

                    Project_Sum pjt = new Project_Sum();
                    if (OBJ.Flag == 2)
                    {
                        pjt.S1 = "500";
                        pjt.S5 = "2";
                        pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                        foreach (Project_Sum ps in lt)
                        {
                            if (ps.T5 != null && ps.T5 != "")
                            if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                            {
                                this.repositoryItemComboBox3.Items.Add(ps.T5);
                            }
                        }
                       
                    }

                        pjt.S1 = "500";
                        pjt.S5 = "1";
                        IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                        foreach (Project_Sum pss in ltt)
                        {
                            if (pss.L1 != null && pss.L1 != "")
                            if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                            {
                                this.repositoryItemComboBox10.Items.Add(pss.L1);
                            }
                        }
                }
          
          
          

            if (OBJ.Flag == 2)//主变台数
            {
                this.repositoryItemComboBox5.Items.Clear();
                if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != "" && OBJ.L7 != "")
                {
                    string str = "S5='" + OBJ.Flag + "'and S1='" + OBJ.L4 + "'and T1 like '" + OBJ.L5 + "%'" + "and T5 ='" + OBJ.L6 + "'and Type ='" + OBJ.L7 +"'";
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByValues", str);
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox5.Items.Add(li.T1);
                    }
                }
            }
            

            if (OBJ.Flag == 2)
            {
                this.repositoryItemComboBox6.Items.Clear();
                if (OBJ.L1 != null && OBJ.L1 != "" && OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != "" && OBJ.L7 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.T1 = OBJ.L1;
                    ps.S5 = OBJ.Flag.ToString();
                    ps.S1 = OBJ.L4;
                    ps.T5 = OBJ.L6;
                    ps.Type = OBJ.L7;
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByvalue", ps);
                   
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox6.Items.Add(li.T2);
                    }
                }


            }

            if (OBJ.Flag == 2)
            {
                this.repositoryItemComboBox7.Items.Clear();
                if (OBJ.L2 != null && OBJ.L2 != "" && OBJ.L1 != null && OBJ.L1 != "" && OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != "" && OBJ.L7 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.T1 = OBJ.L1;
                    ps.T2 = OBJ.L2;
                    ps.S5 = OBJ.Flag.ToString();
                    ps.S1 = OBJ.L4;
                    ps.T5 = OBJ.L6;
                    ps.Type = OBJ.L7;
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByvalue1", ps);
                    
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox7.Items.Add(li.T3);
                    }
                }
            }

            if (OBJ.Flag == 2)
            {
                this.repositoryItemComboBox8.Items.Clear();
                if (OBJ.L13 != null && OBJ.L13 != "" && OBJ.L2 != null && OBJ.L2 != "" && OBJ.L1 != null && OBJ.L1 != "" && OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != "" && OBJ.L7 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.T1 = OBJ.L1;
                    ps.T2 = OBJ.L2;
                    ps.T3 = OBJ.L13;
                    ps.S5 = OBJ.Flag.ToString();
                    ps.S1 = OBJ.L4;
                    ps.T5 = OBJ.L6;
                    ps.Type = OBJ.L7;
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByvalue2", ps);
                  
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox8.Items.Add(li.T4);
                    }
                }
            }

            if (OBJ.Flag == 2)//变电站下的线路
            {
                this.repositoryItemComboBox11.Items.Clear();
                if ( OBJ.L4 != null && OBJ.L4 != "" && OBJ.L9!= null && OBJ.L9 != "" )
                {
                    Project_Sum ps = new Project_Sum();
                    ps.S5 = "1";
                    ps.S1 = OBJ.L4;
                    ps.L1 = OBJ.L9;
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue", ps);
                   
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox11.Items.Add(li.L1);
                    }
                }

                this.repositoryItemComboBox12.Items.Clear();
                if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L9 != null && OBJ.L9 != "" && OBJ.L15 != null && OBJ.L15 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.S5 = "1";
                    ps.S1 = OBJ.L4;
                    ps.L1 = OBJ.L9;
                    ps.L2 = OBJ.L15;

                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue1", ps);
                    
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox12.Items.Add(li.L1);
                    }
                }
               
              }

            if (OBJ.Flag == 1)//线路信息
            {
                this.repositoryItemComboBox11.Items.Clear();
                if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L9 != null && OBJ.L9 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.S5 = "1";
                    ps.S1 = OBJ.L4;
                    ps.L1 = OBJ.L9;
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue", ps);
                   
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox11.Items.Add(li.L1);
                    }
                }

                this.repositoryItemComboBox12.Items.Clear();
                if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L9 != null && OBJ.L9 != "" && OBJ.L15 != null && OBJ.L15 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.S5 = "1";
                    ps.S1 = OBJ.L4;
                    ps.L1 = OBJ.L9;
                    ps.L2 = OBJ.L15;

                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue1", ps);
                   
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox12.Items.Add(li.L1);
                    }
                }
            }

            if (OBJ.Flag == 1)
            {
                OBJ.L1 = "";
                OBJ.L2 = "";
                OBJ.L13 = "";
                OBJ.L14 = "";
                this.categoryRow1.Visible = true;
                this.建设形式.Enabled = false;
                this.单台容量.Enabled = false;
                this.主变台数.Enabled = false;
                this.出线规模.Enabled = false;
                this.接线形式.Enabled = false;
                this.无功配置.Enabled = false;
                this.主变台数及容量.Enabled = false;

            }
            if (OBJ.Flag == 2)
            {
             
                this.categoryRow1.Visible = true;
                this.建设形式.Enabled = true;
                this.单台容量.Enabled = true;
                this.主变台数.Enabled = true;
                this.出线规模.Enabled = true;
                this.接线形式.Enabled = true;
                this.无功配置.Enabled = true;
                this.主变台数及容量.Enabled = true;

            }
        }
        private void CreateInit()
        {

            double sumvalue = 0;
            OBJ.L11 = "0";
            if (OBJ.L4 == "220")
            {
                Project_Sum pjt = new Project_Sum();
                if (OBJ.Flag == 2)
                {
                    pjt.S1 = "220";
                    pjt.S5 = "2";
                    pjt.Type = OBJ.L7;
                    IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                    foreach (Project_Sum ps in lt)
                    {
                        if (ps.T5 != null && ps.T5!="")
                        if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                        {
                            this.repositoryItemComboBox3.Items.Add(ps.T5);
                        }
                    }
                    if (this.repositoryItemComboBox3.Items.Count > 0)
                    {
                        OBJ.L6 = this.repositoryItemComboBox3.Items[0].ToString();
                    }

                }
                    pjt.S1 = "220";
                    pjt.S5 = "1";
                    IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                    foreach (Project_Sum pss in ltt)
                    {
                        if (pss.L1 != null && pss.L1 != "")
                        if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                        {
                            this.repositoryItemComboBox10.Items.Add(pss.L1);
                        }
                    }
                    if (this.repositoryItemComboBox10.Items.Count > 0)
                    {
                        OBJ.L9 = this.repositoryItemComboBox10.Items[0].ToString();
                    }
            }
            if (OBJ.L4 == "110")
            {

                Project_Sum pjt = new Project_Sum();
                if (OBJ.Flag == 2)
                {
                    pjt.S1 = "110";
                    pjt.S5 = "2";
                    pjt.Type = OBJ.L7;
                    IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                    foreach (Project_Sum ps in lt)
                    {
                        if (ps.T5 != null && ps.T5 != "")
                        if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                        {
                            this.repositoryItemComboBox3.Items.Add(ps.T5);
                        }
                    }
                    if (this.repositoryItemComboBox3.Items.Count > 0)
                    {
                        OBJ.L6 = this.repositoryItemComboBox3.Items[0].ToString();
                    }

                }
                    pjt.S1 = "110";
                    pjt.S5 = "1";
                    IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                    foreach (Project_Sum pss in ltt)
                    {
                        if (pss.L1 != null && pss.L1 != "")
                        if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                        {
                            this.repositoryItemComboBox10.Items.Add(pss.L1);
                        }
                    }
                    if (this.repositoryItemComboBox10.Items.Count > 0)
                    {
                        OBJ.L9 = this.repositoryItemComboBox10.Items[0].ToString();
                    }
            }
            if (OBJ.L4 == "35")
            {

                Project_Sum pjt = new Project_Sum();
                if (OBJ.Flag == 2)
                {
                    pjt.S1 = "35";
                    pjt.S5 = "2";
                    pjt.Type = OBJ.L7;
                    IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                    foreach (Project_Sum ps in lt)
                    {
                        if (ps.T5 != null && ps.T5 != "")
                        if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                        {
                            this.repositoryItemComboBox3.Items.Add(ps.T5);
                        }
                    }
                    if (this.repositoryItemComboBox3.Items.Count > 0)
                    {
                        OBJ.L6 = this.repositoryItemComboBox3.Items[0].ToString();
                    }

                }
                    pjt.S1 = "35";
                    pjt.S5 = "1";
                    IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                    foreach (Project_Sum pss in ltt)
                    {
                        if (pss.L1 != null && pss.L1 != "")
                        if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                        {
                            this.repositoryItemComboBox10.Items.Add(pss.L1);
                        }
                    }
                    if (this.repositoryItemComboBox10.Items.Count > 0)
                    {
                        OBJ.L9 = this.repositoryItemComboBox10.Items[0].ToString();
                    }

            }
            if (OBJ.L4 == "66")
            {

                Project_Sum pjt = new Project_Sum();

                if (OBJ.Flag == 2)
                {
                    pjt.S1 = "66";
                    pjt.S5 = "2";
                    pjt.Type = OBJ.L7;
                    IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                    foreach (Project_Sum ps in lt)
                    {
                        if (ps.T5 != null && ps.T5 != "")
                        if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                        {
                            this.repositoryItemComboBox3.Items.Add(ps.T5);
                        }
                    }
                    if (this.repositoryItemComboBox3.Items.Count > 0)
                    {
                        OBJ.L6 = this.repositoryItemComboBox3.Items[0].ToString();
                    }

                }
                    pjt.S1 = "66";
                    pjt.S5 = "1";
                    IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                    foreach (Project_Sum pss in ltt)
                    {
                        if (pss.L1 != null && pss.L1 != "")
                        if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                        {
                            this.repositoryItemComboBox10.Items.Add(pss.L1);
                        }
                    }
                    if (this.repositoryItemComboBox10.Items.Count > 0)
                    {
                        OBJ.L9 = this.repositoryItemComboBox10.Items[0].ToString();
                    }
            }
            if (OBJ.L4 == "500")
            {

                Project_Sum pjt = new Project_Sum();
                if (OBJ.Flag == 2)
                {
                    pjt.S1 = "500";
                    pjt.S5 = "2";
                    pjt.Type = OBJ.L7;
                    IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5andType", pjt);

                    foreach (Project_Sum ps in lt)
                    {
                        if (ps.T5 != null && ps.T5 != "")
                        if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                        {
                            this.repositoryItemComboBox3.Items.Add(ps.T5);
                        }
                    }
                    if (this.repositoryItemComboBox3.Items.Count > 0)
                    {
                        OBJ.L6 = this.repositoryItemComboBox3.Items[0].ToString();
                    }

                }
                    pjt.S1 = "500";
                    pjt.S5 = "1";
                    IList<Project_Sum> ltt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                    foreach (Project_Sum pss in ltt)
                    {
                        if (pss.L1 != null && pss.L1 != "")
                        if (!this.repositoryItemComboBox10.Items.Contains(pss.L1))
                        {
                            this.repositoryItemComboBox10.Items.Add(pss.L1);
                        }
                    }
                    if (this.repositoryItemComboBox10.Items.Count > 0)
                    {
                        OBJ.L9 = this.repositoryItemComboBox10.Items[0].ToString();
                    }
            }
          
          

            if (OBJ.Flag == 2)//主变台数
            {
                this.repositoryItemComboBox5.Items.Clear();
                if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != "" && OBJ.L7 != "")
                {
                    string str = "S5='" + OBJ.Flag + "' and S1 = '" + OBJ.L4 + "' and T5 = '" + OBJ.L6 + "' and Type = '" + OBJ.L7 + "' and T1 like '" + OBJ.L5 + "%" +"'";
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByValues", str);

                    if (sum.Count == 0)
                    {
                        if (isupdate == false)
                            OBJ.L11 = "0";
                          OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                    }
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox5.Items.Add(li.T1);
                    }
                    if (sum.Count > 0)
                    {
                        OBJ.L1 = this.repositoryItemComboBox5.Items[0].ToString();
                    }
                }
            }


            if (OBJ.Flag == 2)
            {
                this.repositoryItemComboBox6.Items.Clear();
                if (OBJ.L1 != null && OBJ.L1 != "" && OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != "" && OBJ.L7 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.T1 = OBJ.L1;
                    ps.S5 = OBJ.Flag.ToString();
                    ps.S1 = OBJ.L4;
                    ps.T5 = OBJ.L6;
                    ps.Type = OBJ.L7;
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByvalue", ps);
                    if (sum.Count == 0)
                    {
                        OBJ.L11 = "0";
                        if (OBJ.L11 != null && OBJ.L11 != "")
                            OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                    }
                    if (sum.Count == 1)
                    {
                        foreach (Project_Sum pp in sum)
                            OBJ.L11 = pp.Num.ToString();
                        if (OBJ.L11 != null && OBJ.L11 != "")
                            OBJ.L10 =double.Parse(OBJ.L11) * OBJ.L12;
                        else
                        {
                            OBJ.L11 = "0";
                            OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                        }
                    }
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox6.Items.Add(li.T2);
                    }
                    if (sum.Count > 0)
                    {
                        OBJ.L2 = this.repositoryItemComboBox6.Items[0].ToString();
                    }
                }


            }

            if (OBJ.Flag == 2)
            {
                this.repositoryItemComboBox7.Items.Clear();
                if (OBJ.L2 != null && OBJ.L2 != "" && OBJ.L1 != null && OBJ.L1 != "" && OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != "" && OBJ.L7 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.T1 = OBJ.L1;
                    ps.T2 = OBJ.L2;
                    ps.S5 = OBJ.Flag.ToString();
                    ps.S1 = OBJ.L4;
                    ps.T5 = OBJ.L6;
                    ps.Type = OBJ.L7;
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByvalue1", ps);
                    if (sum.Count == 0)
                    {
                        OBJ.L11 = "0";
                        if (OBJ.L11 != null && OBJ.L11 != "")
                            OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                    }
                    if (sum.Count == 1)
                    {
                        foreach (Project_Sum pp in sum)
                            OBJ.L11 = pp.Num.ToString();
                        if (OBJ.L11 != null && OBJ.L11 != "")
                            OBJ.L10 =double.Parse(OBJ.L11) * OBJ.L12;
                        else
                        {
                            OBJ.L11 = "0";
                            OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                        }
                    }
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox7.Items.Add(li.T3);
                    }
                    if (sum.Count > 0)
                    {
                        OBJ.L13 = this.repositoryItemComboBox7.Items[0].ToString();
                    }
                }
            }

            if (OBJ.Flag == 2)
            {
                this.repositoryItemComboBox8.Items.Clear();
                if (OBJ.L13 != null && OBJ.L13 != "" && OBJ.L2 != null && OBJ.L2 != "" && OBJ.L1 != null && OBJ.L1 != "" && OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != "" && OBJ.L7 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.T1 = OBJ.L1;
                    ps.T2 = OBJ.L2;
                    ps.T3 = OBJ.L13;
                    ps.S5 = OBJ.Flag.ToString();
                    ps.S1 = OBJ.L4;
                    ps.T5 = OBJ.L6;
                    ps.Type = OBJ.L7;
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByvalue2", ps);
                    if (sum.Count == 0)
                    {
                        OBJ.L11 = "0";
                        if (OBJ.L11 != null && OBJ.L11 != "")
                            OBJ.L10 =double.Parse(OBJ.L11) * OBJ.L12;
                    }
                    if (sum.Count == 1)
                    {
                        foreach (Project_Sum pp in sum)
                            OBJ.L11 = pp.Num.ToString();
                        if (OBJ.L11 != null && OBJ.L11 != "")
                            OBJ.L10 =double.Parse(OBJ.L11) * OBJ.L12;
                        else
                        {
                            OBJ.L11 = "0";
                            OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                        }
                    }
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox8.Items.Add(li.T4);
                    }
                    if (sum.Count > 0)
                    {
                        OBJ.L14 = this.repositoryItemComboBox8.Items[0].ToString();
                    }
                }
            }
            if (OBJ.L14 != null && OBJ.L14 != "" && OBJ.L13 != null && OBJ.L13 != "" && OBJ.L2 != null && OBJ.L2 != "" && OBJ.L1 != null && OBJ.L1 != "" && OBJ.L4 != null && OBJ.L4 != "" && OBJ.L5 != null && OBJ.L5 != "" && OBJ.L6 != null && OBJ.L6 != "" && OBJ.L7 != "")
            {
                Project_Sum ps = new Project_Sum();
                ps.T1 = OBJ.L1;
                ps.T2 = OBJ.L2;
                ps.T3 = OBJ.L13;
                ps.T4 = OBJ.L14;
                ps.S5 = OBJ.Flag.ToString();
                ps.S1 = OBJ.L4;
                ps.T5 = OBJ.L6;
                ps.Type = OBJ.L7;
                IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByvalue3", ps);
                if (sum.Count == 0)
                {
                    OBJ.L11 = "0";
                    if (OBJ.L11 != null && OBJ.L11 != "")
                        OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                }
                if (sum.Count == 1)
                {
                    foreach (Project_Sum pp in sum)
                    {
                        OBJ.L11 = pp.Num.ToString();
                        sumvaluedata = pp.Num;
                    }
                    if (OBJ.L11 != null && OBJ.L11 != "")
                        OBJ.L10 =double.Parse(OBJ.L11) * OBJ.L12;
                    else
                    {
                        OBJ.L11 = "0";
                        OBJ.L10 =double.Parse(OBJ.L11) * OBJ.L12;
                    }
                }
               
            }
            if (OBJ.Flag == 2)//变电站下的线路
            {
                this.repositoryItemComboBox11.Items.Clear();
                if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L9 != null && OBJ.L9 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.S5 = "1";
                    ps.S1 = OBJ.L4;
                    ps.L1 = OBJ.L9;
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue", ps);
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox11.Items.Add(li.L2);
                    }
                    if (sum.Count > 0)
                    {
                        OBJ.L15 = this.repositoryItemComboBox11.Items[0].ToString();
                    }
                }

                this.repositoryItemComboBox12.Items.Clear();
                if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L9 != null && OBJ.L9 != "" && OBJ.L15 != null && OBJ.L15 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.S5 = "1";
                    ps.S1 = OBJ.L4;
                    ps.L1 = OBJ.L9;
                    ps.L2 = OBJ.L15;

                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue1", ps);

                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox12.Items.Add(li.L3);
                    }

                    if (sum.Count > 0)
                    {
                        OBJ.L16 = this.repositoryItemComboBox12.Items[0].ToString();
                    }
                }
                if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L9 != null && OBJ.L9 != "" && OBJ.L15 != null && OBJ.L15 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.S5 = "1";
                    ps.S1 = OBJ.L4;
                    ps.L1 = OBJ.L9;
                    ps.L2 = OBJ.L15;
                    ps.L3 = OBJ.L16;
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue2", ps);

                    if (OBJ.L8 == null || OBJ.L8 == "")
                        OBJ.L8 = "0";
                    if (sum.Count == 1)
                    {
                        foreach (Project_Sum pp in sum)
                        {
                            if (pp.Num.ToString() == null || pp.Num.ToString() == "")
                                pp.Num = 0;
                            sumvalue = pp.Num * double.Parse(OBJ.L8);
                        }
                            OBJ.L11 = Convert.ToString(sumvalue + double.Parse(OBJ.L11));
                            OBJ.L10 = double.Parse(OBJ.L11) * OBJ.L12;
                    }

                }


            }

            if (OBJ.Flag == 1)//线路信息
            {
                this.repositoryItemComboBox11.Items.Clear();
                if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L9 != null && OBJ.L9 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.S5 = "1";
                    ps.S1 = OBJ.L4;
                    ps.L1 = OBJ.L9;
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue", ps);
                    //if (sum.Count == 0)
                    //{
                    //    OBJ.L11 = "0";
                    //    OBJ.L10 = Convert.ToString(double.Parse(OBJ.L11) * OBJ.L12);
                    //}
                    //if (sum.Count == 1)
                    //{
                    //    foreach (Project_Sum pp in sum)
                    //        OBJ.L11 = pp.Num.ToString();
                    //    if (OBJ.L11 != null && OBJ.L11 != "")
                    //    {
                    //        OBJ.L10 = Convert.ToString(double.Parse(OBJ.L11) * OBJ.L12);
                    //    }
                    //    else
                    //    {
                    //        OBJ.L11 = "0";
                    //        OBJ.L10 = Convert.ToString(double.Parse(OBJ.L11) * OBJ.L12);
                    //    }
                    // }
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox11.Items.Add(li.L2);
                    }

                    if (sum.Count > 0)
                    {
                        OBJ.L15 = this.repositoryItemComboBox11.Items[0].ToString();
                    }
                }

                this.repositoryItemComboBox12.Items.Clear();
                if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L9 != null && OBJ.L9 != "" && OBJ.L15 != null && OBJ.L15 != "")
                {
                    Project_Sum ps = new Project_Sum();
                    ps.S5 = "1";
                    ps.S1 = OBJ.L4;
                    ps.L1 = OBJ.L9;
                    ps.L2 = OBJ.L15;

                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue1", ps);
                    //if (sum.Count == 0)
                    //{
                    //    OBJ.L11 = "0";
                    //    OBJ.L10 = Convert.ToString(double.Parse(OBJ.L11) * OBJ.L12);
                    //}
                    //if (sum.Count == 1)
                    //{
                    //    foreach (Project_Sum pp in sum)
                    //        OBJ.L11 = pp.Num.ToString();
                    //    if (OBJ.L11 != null && OBJ.L11 != "")
                    //    {
                    //        OBJ.L10 = Convert.ToString(double.Parse(OBJ.L11) * OBJ.L12);
                    //    }
                    //    else
                    //    {
                    //        OBJ.L11 = "0";
                    //        OBJ.L10 = Convert.ToString(double.Parse(OBJ.L11) * OBJ.L12);
                    //    }
                    //}
                    foreach (Project_Sum li in sum)
                    {
                        this.repositoryItemComboBox12.Items.Add(li.L3);
                    }
                    if (sum.Count > 0)
                    {
                        OBJ.L16 = this.repositoryItemComboBox12.Items[0].ToString();
                    }

                }
                OBJ.L8 = "0";
                OBJ.L11 = "0";
                OBJ.L10 = 0;
                //if (OBJ.L4 != null && OBJ.L4 != "" && OBJ.L9 != null && OBJ.L9 != "" && OBJ.L15 != null && OBJ.L15 != "")
                //{
                //    Project_Sum ps = new Project_Sum();
                //    ps.S5 = "1";
                //    ps.S1 = OBJ.L4;
                //    ps.L1 = OBJ.L9;
                //    ps.L2 = OBJ.L15;
                //    ps.L3 = OBJ.L16;
                //    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue2", ps);
                //    if (sum.Count == 0)
                //    {
                //        OBJ.L11 = "0";
                    
                //            OBJ.L10 = Convert.ToString(double.Parse(OBJ.L11) * OBJ.L12);
                //    }
                //    if (sum.Count == 1)
                //    {
                //        foreach (Project_Sum pp in sum)
                //            OBJ.L11 = pp.Num.ToString();
                //        if (OBJ.L11 != null && OBJ.L11 != "")
                //        {
                //            OBJ.L10 = Convert.ToString(double.Parse(OBJ.L11) * OBJ.L12);
                //        }
                //        else
                //        {
                //            OBJ.L11 = "0";
                //            OBJ.L10 = Convert.ToString(double.Parse(OBJ.L11) * OBJ.L12);
                //        }
                //    }

                //}


            }

            if (OBJ.Flag == 1)
            {
                OBJ.L1 = "";
                OBJ.L2 = "";
                OBJ.L13 = "";
                OBJ.L14 = "";
                this.categoryRow1.Visible = true;
                this.建设形式.Enabled = false;
                this.单台容量.Enabled = false;
                this.主变台数.Enabled = false;
                this.出线规模.Enabled = false;
                this.接线形式.Enabled = false;
                this.无功配置.Enabled = false;
                this.主变台数及容量.Enabled = false;

            }
            if (OBJ.Flag == 2)
            {

                this.categoryRow1.Visible = true;
                this.建设形式.Enabled = true;
                this.单台容量.Enabled = true;
                this.主变台数.Enabled = true;
                this.出线规模.Enabled = true;
                this.接线形式.Enabled = true;
                this.无功配置.Enabled = true;
                this.主变台数及容量.Enabled = true;

            }



        }
        private void vGridControl1_Click(object sender, EventArgs e)
        {

        }

    }
}