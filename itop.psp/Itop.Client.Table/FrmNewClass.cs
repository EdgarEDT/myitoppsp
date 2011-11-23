using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistic;
using Itop.Common;
using Itop.Client.Base;

namespace Itop.Client.Table
{
    public partial class FrmNewClass : DevExpress.XtraEditors.XtraForm
    {
        private string classname = "";
        private string classname1 = "";
        private string type = "";
        private string type2 = "";
        private string flag = "";
        private string classtype = "";
        private bool isUpdate = false;
        PowerSubstationLine psp_sl = null;

        private List<string> s = new List<string>();
        

        



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


        /// <summary>
        /// 是否更新
        /// </summary>
        public bool IsUpdate
        {
            get { return isUpdate; }
            set { isUpdate = value; }
        }

        public FrmNewClass()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            classname = psp_sl.Title = spinEdit1.Text;
            if (classname.ToString() == "")
            {
                MsgBox.Show("类别名称不能为空");
                return;
            }

            try
            {
                if (Common.Services.BaseService.GetObject("SelectPowerSubstationLineByTitleTypeClass", psp_sl) == null)
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
                                ps.Title = classname;
                                ps.Flag = flag;
                                ps.Type = type;
                                ps.Type2 = type2;
                                ps.ClassType = ss;

                                object obj = Common.Services.BaseService.GetObject("SelectPowerSubstationLineByTitleTypeClass1", ps);
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
                               // psp_sl.UID+="|"+proj
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

        private void FormNewYear_Load(object sender, EventArgs e)
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

            spinEdit1.Text = classname;
            classname1 = classname;
            if (!IsUpdate)
            {
                psp_sl = new PowerSubstationLine();
                psp_sl.Type = type;
                psp_sl.Type2 = type2;
                psp_sl.Flag = flag;
            }
            else
            {
                PowerSubstationLine ps = new PowerSubstationLine();
                ps.Title = classname;
                ps.ClassType = classtype;
                ps.Flag = flag;
                ps.Type = type;
                ps.Type2 = type2;

                psp_sl = (PowerSubstationLine)Common.Services.BaseService.GetObject("SelectPowerSubstationLineByAll", ps);

            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }



    }
}