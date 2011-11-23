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
    public partial class frmMuLian2 : FormBase
    {
        private string UID;
        public frmMuLian2(string svgDocumentUID)
        {
            svgUID = svgDocumentUID;
            InitializeComponent();
            InitCom();
        }
        public frmMuLian2(PSPDEV dev,string svgDocumentUID)
        {
            svgUID = svgDocumentUID;
            InitializeComponent();
            InitCom();
            InitData(dev);
        }
        public string svgUID
        {
            get
            {
                return UID;
            }
            set
            {
                UID = value;
            }
        }
        private void InitData(PSPDEV dev)
        {
            Name = dev.Name;
            INodeName = dev.HuganLine1;
            JNodeName = dev.HuganLine2;
            ILineName = dev.HuganLine3;
            JLineName = dev.HuganLine4;
            ILoadName = dev.KName;
            JLoadName = dev.KSwitchStatus;
            SwitchStatus1 = dev.LineLevel;
            SwitchStatus2 = dev.LineType;
            SwitchStatus3 = dev.LineStatus;
        }
        public string Name
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }
        public string INodeName
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
        public string JNodeName
        {
            get
            {
                return comboBoxEdit6.Text;
            }
            set
            {
                comboBoxEdit6.Text = value;
            }
        }
        public string ILineName
        {
            get
            {
                return comboBoxEdit2.Text;
            }
            set
            {
                comboBoxEdit2.Text = value;
            }
        }
        public string JLineName
        {
            get
            {
                return comboBoxEdit5.Text;
            }
            set
            {
                comboBoxEdit5.Text = value;
            }
        }
        public string ILoadName
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
        public string JLoadName
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
        public string SwitchStatus1
        {
            get
            {
                return comboBoxEdit7.Text;
            }
            set
            {
                comboBoxEdit7.Text = value;
            }
        }
        public string SwitchStatus2
        {
            get
            {
                return comboBoxEdit8.Text;
            }
            set
            {
                comboBoxEdit8.Text = value;
            }
        }
        public string SwitchStatus3
        {
            get
            {
                return comboBoxEdit9.Text;
            }
            set
            {
                comboBoxEdit9.Text = value;
            }
        }
        private void InitCom()
        {
            PSPDEV psp = new PSPDEV();
            psp.SvgUID = UID;
            psp.Lable = "母线节点";
            psp.Type = "Use";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", psp);
            psp.SvgUID = UID;
            psp.Lable = "负荷支路";
            psp.Type = "loadline";
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", psp);
            psp.SvgUID = UID;
            psp.Lable = "支路";
            psp.Type = "Polyline";
            IList list2 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", psp);
            foreach (PSPDEV dev in list)
            {
                comboBoxEdit1.Properties.Items.Add(dev.Name);
                comboBoxEdit6.Properties.Items.Add(dev.Name);
            }
            foreach (PSPDEV dev in list1)
            {
                comboBoxEdit3.Properties.Items.Add(dev.Name);
                comboBoxEdit4.Properties.Items.Add(dev.Name);
            }
            foreach (PSPDEV dev in list2)
            {
                comboBoxEdit2.Properties.Items.Add(dev.Name);
                comboBoxEdit5.Properties.Items.Add(dev.Name);
            }
        }
    }
}