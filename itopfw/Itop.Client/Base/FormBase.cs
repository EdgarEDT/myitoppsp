using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.RightManager;
using System.Collections;
using DevExpress.XtraEditors;
//using Itop.Domain.Projects;

namespace Itop.Client.Base
{
    public partial class FormBase : XtraForm
    {

        public IList<Project> project = new List<Project>();
        public VsmdgroupProg smdgroup = new VsmdgroupProg();
        public Smmprog smmprog = new Smmprog();

        private string projectuid = "";
        public string ProjectUID
        {
            get { return project[0].UID; }
        }


        #region 属性
        public bool AddRight {
            get {
                try {
                    return (int.Parse(smdgroup.ins) > 0) ? true : false;
                } catch { }

                return false;
            }
            //set {
            //    smdgroup.ins = value?"1":"0" ;
            //}
        }
        

        public bool EditRight {
            get {
                try {
                    return (int.Parse(smdgroup.upd) > 0) ? true : false;
                } catch { }

                return false;
            }
            //set {
            //    smdgroup.upd = value?"1":"0";
            //}
        }
        

        public bool DeleteRight {
            get {
                try {
                    return (int.Parse(smdgroup.del) > 0) ? true : false;
                } catch { }

                return false;
            }
            set {
                smdgroup.del = value?"1":"0";
            }
        }
        

        public bool QueryRight {
            get {
                try {
                    return (int.Parse(smdgroup.qry) > 0) ? true : false;
                } catch { }

                return false;
            }
            set {
                smdgroup.qry = value?"1":"0";
            }
        }
        

        public bool PrintRight {
            get {
                try {
                    return (int.Parse(smdgroup.prn) > 0) ? true : false;
                } catch { }

                return false;
            }
            set {
                smdgroup.prn = value?"1":"0";
            }
        }
       

        public bool SendRight {
            get {
                try {
                    return (int.Parse(smdgroup.send) > 0) ? true : false;
                } catch { }

                return false;
            }
            set {
                smdgroup.send = value?"1":"0";
            }
        }
        public bool ExamRight {
            get {
                try {
                    return (int.Parse(smdgroup.exam) > 0) ? true : false;
                } catch { }

                return false;
            }
            set {
                smdgroup.exam = value?"1":"0";
            }
        }
        public bool PassRight {
            get {
                try {
                    return (int.Parse(smdgroup.pass) > 0) ? true : false;
                } catch { }

                return false;
            }
            set {
                smdgroup.pass = value?"1":"0";
            }
        }

        #endregion

        public FormBase()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 设置权限方法
        /// </summary>
        /// <param name="progid"></param>
        public void  SetRightByProgid(string progid) {

            SetRightByProgid(progid, "");
        }
        /// <summary>
        /// 设置权限方法
        /// </summary>
        /// <param name="progid"></param>
        /// <param name="projectuid"></param>
        public void  SetRightByProgid(string progid,string projectuid) {

            smdgroup = MIS.GetProgRight(progid, projectuid);
        }
        /// <summary>
        /// 初始标准数据方法
        /// </summary>
        /// <param name="pl"></param>
        /// <param name="sm"></param>
        /// <param name="smmp"></param>
        public virtual bool InitData(List<Project> pl, VsmdgroupProg sm, Smmprog smmp)
        {
            project = pl;
            smdgroup = sm;
            smmprog = smmp;
            
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            if(this.Owner!=null)
                this.StartPosition = FormStartPosition.CenterParent;
            this.Text = pl[1].ProjectName + " - " + pl[0].ProjectName + " - " + smmprog.ProgName;
            this.Owner = (System.Windows.Forms.Form)Itop.Client.MIS.MainForm;

            return true;

        }
        /// <summary>
        /// 标准打开方法
        /// </summary>
        /// <returns></returns>
        public virtual bool Execute() {
            
            OpenForm();
            MIS.SaveLog(smmprog.ProgName, "打开" + smmprog.ProgName);
            return true;
        }
        /// <summary>
        ///  标准打开方法
        /// </summary>
        /// <returns></returns>
        public virtual bool ExecuteShow()
        {
           
            Show();
            MIS.SaveLog(smmprog.ProgName, "打开" + smmprog.ProgName);
            return true;
        }

        public virtual void OpenForm()
        {

            ShowDialog();
        }


        private void FormBase_Load(object sender, EventArgs e)
        {
            //this.Text = smmprog.ProgName;



        }
    }
}