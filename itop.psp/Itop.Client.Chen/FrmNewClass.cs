using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Chen;
using Itop.Common;
using Itop.Client.Base;
using Itop.Domain.Chen;

namespace Itop.Client.Chen
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
        PSP_35KVStyle psp_sl = null;

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

            classname = psp_sl.Title = spinEdit2.Value.ToString();
            if (classname.ToString() == "")
            {
                MsgBox.Show("年度不能为空");
                return;
            }
            try
            {
                if (Common.Services.BaseService.GetObject("SelectPSP_35KVStyleByTitleTypeClass", psp_sl) == null)
                {
                    try
                    {
                        if (!IsUpdate)
                        {
                            bool bz = false;
                            string f = "";
                            foreach (string ss in s)
                            {
                                PSP_35KVStyle ps = new PSP_35KVStyle();
                                ps.Title = classname;
                                ps.Type = type;
                                ps.ClassType = ss;

                                object obj = Common.Services.BaseService.GetObject("SelectPSP_35KVStyleByTitleTypeClass1", ps);
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
                                Common.Services.BaseService.Create<PSP_35KVStyle>(psp_sl);
                            }


                        }
                        else
                        {
                            Common.Services.BaseService.Update<PSP_35KVStyle>(psp_sl);
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
            s.Add("L1");
            s.Add("L2");
            s.Add("L3");
            s.Add("L4");
            s.Add("L5");
            s.Add("L6");
            s.Add("L7");
            s.Add("L8");
            s.Add("L9");
            s.Add("L10");
            s.Add("L11");
            s.Add("L12");
            s.Add("L13");
            s.Add("L14");
            s.Add("L15");
            s.Add("L16");
            s.Add("L17");
            s.Add("L18");
            s.Add("L19");
            s.Add("L20");
            s.Add("L21");
            s.Add("L22");
            s.Add("L23");
            s.Add("L24");
            s.Add("L25");
            s.Add("L26");
            s.Add("L27");
            s.Add("L28");
            s.Add("L29");
            s.Add("L30");
            s.Add("L31");
            s.Add("L32");
            s.Add("L33");
            s.Add("L34");
            s.Add("L35");
            s.Add("L36");
            s.Add("L37");
            s.Add("L38");
            s.Add("L39");
            s.Add("L40");

            //spinEdit2.Text = classname;
            classname1 = classname;
            if (!IsUpdate)
            {
                psp_sl = new PSP_35KVStyle();
                psp_sl.Type = type;
            }
            else
            {
                PSP_35KVStyle ps = new PSP_35KVStyle();
                ps.Title = classname;
                ps.Type = type;
                ps.ClassType = classtype;

                psp_sl = (PSP_35KVStyle)Common.Services.BaseService.GetObject("SelectPSP_35KVStyleByAll", ps);

            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }



    }
}