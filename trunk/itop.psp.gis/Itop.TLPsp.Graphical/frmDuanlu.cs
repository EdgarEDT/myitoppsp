using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using System.Collections;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.TLPsp.Graphical
{
    public partial class frmDuanlu : FormBase
    {
        PSPDEV leel = new PSPDEV();
        public frmDuanlu()
        {
            InitializeComponent();
        }
        public frmDuanlu(PSPDEV pspDev)

        {
            InitializeComponent();
            InitData(pspDev);
        }
        public void InitData(PSPDEV pspDev)
        {
            PSPDEV dev = new PSPDEV();
            dev.SvgUID = pspDev.SvgUID;
            leel = pspDev;
           
            
        }
       
        public string DuanluBigsmall
        {
            get
            {
                if (comboBoxEdit2.Text != "")
                    return comboBoxEdit2.Text;
                else
                    return "大方式电抗";
            }
            set
            {
                comboBoxEdit2.Text = value;
            }
        }
        private string _projectsuid;
        public string projectsuid
        {
            get { return _projectsuid; }
            set { _projectsuid = value; }
        }
        public string DuanluType
        {
            get
            {
                return comboBoxEdit3.Text;
            }
            set
            {
                comboBoxEdit3.Text = value;
            }
        }
        public string DuanluTuxing
        {
            get
            {
                return comboBoxEdit4.Text;
            }
            set
            {
                comboBoxEdit4.Text = value;
            }
        }
        public string DuanluBaobiao
        {
            get
            {
                return comboBoxEdit1.Text;
            }
            set
            {
                comboBoxEdit1.Text = value;
            }
        }
        public string hscool
        {
            get
            { return hScrollBar1.Value.ToString(); }
        }
        private void frmProperty_Load(object sender, EventArgs e)
        {
            string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectsuid + "'AND Type='01'";
            IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon1);
            strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectsuid + "'AND Type='05'";          
            IList list2 = Services.BaseService.GetList("SelectPSPDEVByCondition", strCon1);
           
        }
        

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            label5.Text = hscool;
        }
        private void visualChanged(object sender, EventArgs e)
        {
            if (leel.Type == "01")
                hScrollBar1.Visible = false;
        }
        private void visualChanged2(object sender, EventArgs e)
        {
            if (leel.Type == "01")
            {
                label3.Visible = false;
                label5.Visible = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

    }
}