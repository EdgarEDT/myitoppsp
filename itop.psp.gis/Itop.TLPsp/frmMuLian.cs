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
    public partial class frmMuLian : FormBase
    {
        private string UID;
        public frmMuLian(string svgDocumentUID)
        {
            svgUID = svgDocumentUID;
            InitializeComponent();
            InitCom();
        }
        public frmMuLian(PSPDEV dev, string svgDocumentUID)
        {
            svgUID = svgDocumentUID;
            InitializeComponent();
            InitCom();
            InitData(dev);
        }
        private void InitData(PSPDEV dev)
        {
            svgUID = dev.SvgUID;
            Name = dev.Name;
            FirstNodeName = dev.HuganLine1;
            LastNodeName = dev.HuganLine2;
            SwitchStatus = dev.HuganLine3;
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
        private void InitCom()
        {
            PSPDEV psp = new PSPDEV();
            psp.SvgUID = UID;
            psp.Lable = "Ä¸Ïß½Úµã";
            psp.Type = "Use";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandLableandType", psp);
            foreach (PSPDEV dev in list)
            {
                comboBoxEdit1.Properties.Items.Add(dev.Name);
                comboBoxEdit2.Properties.Items.Add(dev.Name);

            }
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
        public string FirstNodeName
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
        public string LastNodeName
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
        public string SwitchStatus
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
    }
}