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
    public partial class FormAddInfo_TouZiGuSuan : FormBase
    {
        public FormAddInfo_TouZiGuSuan()
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
         PSP_Project_List objec = new PSP_Project_List();
        public PSP_Project_List OBJ = new PSP_Project_List();
        PSP_Project_List obj = new PSP_Project_List();
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
               
                PSP_Project_List ppt = new PSP_Project_List();
                ppt.ID = poweruid;
                ppt.Flag2 = flag;
                //obj = (PSP_Project_List)Common.Services.BaseService.GetObject("SelectPSP_Project_ListByKey", ppt);
                obj = Common.Services.BaseService.GetOneByKey<PSP_Project_List>(ppt);
                OBJ = obj;
                powerid =obj.Code;
                if (OBJ.Flag == 2)
                {
                    OBJ.L19 = "变电站";
                }
                else
                {
                    OBJ.L19 = "线路";
                }
                isupdate = true;
                UpDateInit();
                IList<PSP_Project_List> list = new List<PSP_Project_List>();
                list.Add(OBJ);
                this.vGridControl1.DataSource = list;
              
            }
              
                if (isupdate == false)
                {
                    OBJ.ID = Guid.NewGuid().ToString();
                    OBJ.CreateTime = DateTime.Now;
                    OBJ.UpdateTime = DateTime.Now;
                    OBJ.ParentID = parentID;
                    OBJ.Flag2 = flag;

                    OBJ.Flag = 2;
                    OBJ.L4 = "220";
                    OBJ.L5 = "1";
                    OBJ.L7 = "户内站";
                    OBJ.L11= "0";
                    OBJ.L12 = 0;
                    OBJ.L10 = 0;
                    OBJ.L19 = "变电站";
                    CreateInit();
                    IList<PSP_Project_List> list = new List<PSP_Project_List>();
                    list.Add(OBJ);
                    this.vGridControl1.DataSource = list;

                }

           
             this.电压等级.Properties.RowEdit.Click += new EventHandler(RowEdit_Click);
             this.类型.Properties.RowEdit.EditValueChanged += new EventHandler(RowEdit_EditValueChanged);
          
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

        void RowEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (OBJ.L19 == "变电站")
            {
                OBJ.Flag = 2;  }
             if (OBJ.L19 == "线路")
             {
                 OBJ.Flag = 1;  }
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

                this.主变台数.Enabled = false;
                this.单台容量.Enabled = false;
            }
            if (OBJ.Flag == 2)
            {
                this.主变台数.Enabled = true;
                this.单台容量.Enabled = true;
            }
        }
      
        private void simpleButton1_Click(object sender, EventArgs e)
        {

           if (OBJ.L4 == "" || OBJ.L4 == null)
            {
                MsgBox.Show("电压等级为必填项！");
                return;
            }
            if (OBJ.L3 == "" || OBJ.L3 == null)
            {
                MsgBox.Show("项目名称为必填项！");
                return;
            }
            if (OBJ.L5.ToString() != null && OBJ.L5.ToString() != "" && OBJ.L6.ToString() != null && OBJ.L6.ToString() != "")
                OBJ.IsConn = Convert.ToString(double.Parse(OBJ.L5) * double.Parse(OBJ.L6));
           try
           {
               Common.Services.BaseService.Update("UpdatePSP_Project_ListByFlag2", OBJ);
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
            if (e.Row.Properties.FieldName.ToString() == "L19" || e.Row.Properties.FieldName.ToString() == "L4" )
            {
                repositoryItemComboBox3.Items.Clear();
                this.repositoryItemComboBox10.Items.Clear();
                if (OBJ.L4 == "220")
                {
                    Project_Sum pjt = new Project_Sum();
                    if (OBJ.Flag == 2)
                    {
                        pjt.S1 = "220";
                        pjt.S5 = "2";

                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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
                
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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
                    
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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
                  
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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
                   
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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

            if (e.Row.Properties.FieldName.ToString() == "L19")
            {
                Project_Sum pjt = new Project_Sum();
                if (e.Value.ToString() == "变电站")
                {
                    this.repositoryItemComboBox3.Items.Clear();
                    OBJ.Flag = 2;
                    pjt.S1 = OBJ.L4;
                    pjt.S5 = "2";

                    IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                    foreach (Project_Sum ps in lt)
                    {
                        if (ps.T5 != null && ps.T5 != "")
                            if (!this.repositoryItemComboBox3.Items.Contains(ps.T5))
                            {
                                this.repositoryItemComboBox3.Items.Add(ps.T5);
                            }
                    }
                }
                else
                {
                    this.repositoryItemComboBox3.Items.Clear();
                    OBJ.Flag = 1;
                    pjt.S1 = OBJ.L4;
                    pjt.S5 = "1";

                    IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

                    foreach (Project_Sum ps in lt)
                    {
                        if (ps.T5 != null && ps.T5 != "")
                            if (!this.repositoryItemComboBox10.Items.Contains(ps.T5))
                            {
                                this.repositoryItemComboBox10.Items.Add(ps.T5);
                            }
                    }
                   
                }

                if (OBJ.Flag == 1)
                {
                    OBJ.L5 = "";
                    OBJ.L6 = "";
                    OBJ.L7 = "";
                    this.单台容量.Enabled = false;
                    this.主变台数.Enabled = false;
                }
                if (OBJ.Flag == 2)
                {
                    this.单台容量.Enabled = true;
                    this.主变台数.Enabled = true;
                }
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
                        //pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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
                        //pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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
                        //pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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
                        //pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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
                        //pjt.Type = OBJ.L7;
                        IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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
          
            if (OBJ.Flag == 1)
            {

                this.单台容量.Enabled = false;
                this.主变台数.Enabled = false;
            }
            if (OBJ.Flag == 2)
            {
                this.单台容量.Enabled = true;
                this.主变台数.Enabled = true;
            }

            PSP_Project_List sf = (PSP_Project_List)Services.BaseService.GetObject("SelectPSP_Project_ListByMaxCreateTime", null);
            IList sf1 = Services.BaseService.GetList("selectPag2", null);

            foreach (PSP_Project_List kk in sf1)
            {
                if (kk.L1 != "" && kk.L1 != null)
                    this.repositoryItemComboBox5.Items.Add(kk.L1);
            }
            //if (sf != null)
            //    if (sf.L1 != "" && sf.L1 != null)
            //        OBJ.L1 = sf.L1;
            //    else if (this.repositoryItemComboBox5.Items.Count > 0)
            //        OBJ.L1 = this.repositoryItemComboBox5.Items[0].ToString();
            //    else if (this.repositoryItemComboBox5.Items.Count > 0)
            //        OBJ.L1 = this.repositoryItemComboBox5.Items[0].ToString();

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
                    IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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
                if (OBJ.Flag == 1)
                {

                    if (this.repositoryItemComboBox10.Items.Count > 0)
                    {
                        OBJ.L9 = this.repositoryItemComboBox10.Items[0].ToString();
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

                    IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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
                if (OBJ.Flag == 1)
                {

                    if (this.repositoryItemComboBox10.Items.Count > 0)
                    {
                        OBJ.L9 = this.repositoryItemComboBox10.Items[0].ToString();
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

                    IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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
                if (OBJ.Flag == 1)
                {
 
                    if (this.repositoryItemComboBox10.Items.Count > 0)
                    {
                        OBJ.L9 = this.repositoryItemComboBox10.Items[0].ToString();
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

                    IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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
                if (OBJ.Flag == 1)
                {

                    if (this.repositoryItemComboBox10.Items.Count > 0)
                    {
                        OBJ.L9 = this.repositoryItemComboBox10.Items[0].ToString();
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

                    IList<Project_Sum> lt = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", pjt);

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
                if (OBJ.Flag == 1)
                {

                    if (this.repositoryItemComboBox10.Items.Count > 0)
                    {
                        OBJ.L9 = this.repositoryItemComboBox10.Items[0].ToString();
                    }
                }

            }        
            if (OBJ.Flag == 2)
            {
                this.单台容量.Enabled = true;
                this.主变台数.Enabled = true;
            }


            PSP_Project_List sf = (PSP_Project_List)Services.BaseService.GetObject("SelectPSP_Project_ListByMaxCreateTime", null);
            IList sf1 = Services.BaseService.GetList("selectPag2", null);

            foreach (PSP_Project_List kk in sf1)
            {
                if (kk.L1 != "" && kk.L1 != null)
                  this.repositoryItemComboBox5.Items.Add(kk.L1);
            }
            if (sf!=null)
                if (sf.L1 != "" && sf.L1!=null)
                  OBJ.L1 = sf.L1;
                else if (this.repositoryItemComboBox5.Items.Count > 0)
                  OBJ.L1 = this.repositoryItemComboBox5.Items[0].ToString();
           else if (this.repositoryItemComboBox5.Items.Count>0)
               OBJ.L1 = this.repositoryItemComboBox5.Items[0].ToString();
      
        }
        private void vGridControl1_Click(object sender, EventArgs e)
        {

        }  
    }
}

