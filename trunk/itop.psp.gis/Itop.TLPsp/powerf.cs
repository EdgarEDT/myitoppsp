using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.TLPsp
{
    public partial class powerf : FormBase
    {
        public powerf()
        {
            InitializeComponent();
        }
         public powerf(PSPDEV pspDev)

        {
            InitializeComponent();
            InitData(pspDev);
        }
        public void InitData(PSPDEV pspDev)
        {
            PSPDEV dev = new PSPDEV();
            dev.SvgUID = pspDev.SvgUID;
            //dev = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVBySvgUID", dev);
            powerfa.Text = pspDev.PowerFactor.ToString();//功率因子
            standardv.Text = pspDev.StandardVolt.ToString();//基准电压
            standardc.Text = pspDev.StandardCurrent.ToString();//基准电流
            textBox1.Text = pspDev.BigP.ToString();
            textBox2.Text = pspDev.iV.ToString();
            textBox3.Text = pspDev.jV.ToString();
        }
        public string powerfactor//功率因子
        {
            get
            {
                return powerfa.Text;
            }
            set
            {
                powerfa.Text = value;
            }
        }
        public string bigP//功率因子
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
        public string standardvolt//基准电压
        {
            get
            {
                return standardv.Text;
            }
            set
            {
                standardv.Text = value;
            }
        }
        public string standardcurrent//基准电流
        {
            get
            {
                return standardc.Text;
            }
            set
            {
                standardc.Text = value;
            }
        }
        public string Vmin
        {
            get
            {
                return textBox2.Text;
            }
            set
            {
                textBox2.Text = value;
            }
        }

        public string Vmax
        {
            get
            {
                return textBox3.Text;
            }
            set
            {
                textBox3.Text = value;
            }
        }

        
    }
}