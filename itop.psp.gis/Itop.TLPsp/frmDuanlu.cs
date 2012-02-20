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
namespace Itop.TLPsp
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
            leel.SvgUID = pspDev.SvgUID;
            leel.Type = pspDev.Type;
            
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
            PSPDEV dev = new PSPDEV();
            dev.SvgUID = leel.SvgUID;
            dev.Type = "use";
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", dev);
          
            dev.Type = "polyline";
            dev.Lable = "支路";
            
            IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", dev);
           
        }
        

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            label5.Text = hscool;
        }
        private void visualChanged(object sender, EventArgs e)
        {
            if (leel.Type == "Use")
                hScrollBar1.Visible = false;
        }
        private void visualChanged2(object sender, EventArgs e)
        {
            if (leel.Type == "Use")
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

    }
}