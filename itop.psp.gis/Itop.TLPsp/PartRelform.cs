using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ItopVector.Core.Figure;
using ItopVector.Core.Interface.Figure;
using Itop.Domain.Stutistic;
using Itop.Domain.Graphics;
using Itop.Client.Base;
using System.IO;
using System.Threading;
using ItopVector.Tools;
using ItopVector.Core.Interface;
using System.Xml.XPath;
using ItopVector.Core;
using ItopVector.Core.Types;
using Itop.Client.Common;
using System.Diagnostics;
using Itop.MapView;
namespace Itop.TLPsp
{
    public partial class PartRelform : FormBase
    {
        PSPDEV leel = new PSPDEV();
        public List<int> lineVnumlist = new List<int>();
        public List<int> lineDnumlist = new List<int>();
        public string deleteprojec = null;
        public PartRelform()
        {
            InitializeComponent();
            //WireCategory wirewire = new WireCategory();
            //IList list1 = Services.BaseService.GetList("SelectWireCategoryList", wirewire);
        }
        public PartRelform(PSPDEV pspDEV)
        {
            InitializeComponent();
            leel = pspDEV;
            //WireCategory wirewire = new WireCategory();
            //IList list1 = Services.BaseService.GetList("SelectWireCategoryList", wirewire);
            //foreach (WireCategory sub in list1)
            //{
 
            //}

        }

        private void Compubton_Click(object sender, EventArgs e)
        {

            if (RegionSelect.Checked)
            {
                this.DialogResult = DialogResult.OK;
            }
            if (SelectVtype.Checked)
            {
                string tempp = comboV.Text;
                int tel = tempp.Length;
                if (tempp.Contains("kV") || tempp.Contains("KV") || tempp.Contains("kv") || tempp.Contains("Kv"))
                {
                    tempp = tempp.Substring(0, tel - 2);
                }  
                //pspDev.VoltR = Convert.ToDouble(tempp);
                leel.VoltR = Convert.ToDouble(tempp);
                //textBox2.Text=
                leel.Type = "Polyline";
                // pspDev.SvgUID = tlVectorControl1.SVGDocument.SvgdataUid;
                IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDandTypeandVoltR", leel);
                // int j = 0;
                if (list1.Count==0)
                {
                    MessageBox.Show("没有此电压等级的线路，请重新选择电压等级！");
                    return;
                }
                else
                {
                    for (int i = 0; i < list1.Count; i++)
                    {
                        PSPDEV psp = (PSPDEV)list1[i];
                        if (psp.LineStatus != "断开")
                        {
                            lineVnumlist.Add(psp.Number);
                        }
                    }
                    lineVnumlist.Sort();

                    this.DialogResult = DialogResult.Ignore;
                }
                
            }
            if (DefineDelete.Checked)
            {
                this.DialogResult = DialogResult.Yes;
            }
        }
        public string LineLevel                           //选择电压等级
        {
            get
            {
                if (comboV.Text == "")
                    comboV.Text = "0";
                return comboV.Text;
            }
            set
            {
                comboV.Text = value;
            }
        }

        private void SelectVtype_CheckedChanged(object sender, EventArgs e)
        {
            if (SelectVtype.Checked)
            {
                comboV.Enabled = true;
            }
            else
                comboV.Enabled = false;
        }

        private void DefineDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (DefineDelete.Checked)
            {
                Selbutt.Enabled = true;
            }
            else
                Selbutt.Enabled = false;
        }

        private void Selbutt_Click(object sender, EventArgs e)
        {
            if (DefineDelete.Checked)
            {
                this.Visible = false;
                DefineDelform df = new DefineDelform(leel);
                df.ShowDialog();
                if (df.DialogResult==DialogResult.OK)
                {
                    this.Visible = true;
                    lineDnumlist = df.linenums;
                    lineVnumlist = df.transnums;
                    deleteprojec = df.Defineproject;
                }
            }
        }

        private void Cancelbton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}