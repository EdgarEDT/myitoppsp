using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Common;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using Itop.Domain.Table;
using Itop.TLPSP.DEVICE;
namespace Itop.TLPSP.DEVICE
{
    public partial class PDreltypefrmedit : DevExpress.XtraEditors.XtraForm {
        public PDreltypefrmedit()
        {
            InitializeComponent();
        }
        private Ps_pdreltype pdtype = new Ps_pdreltype();
        public Ps_pdreltype Pdtype
        {
            get{
                //pdtype.Year = Year;
                //pdtype.PeopleSum = Peplesum;
                pdtype.Title= Title;
                pdtype.S1 = S1;
                return pdtype;
            }
            set{
                if (value!=null)
                {
                    pdtype=value;
                    Title = pdtype.Title;
                    S1 = pdtype.S1;
                    //Peplesum = pdtype.PeopleSum;
                    //Year = pdtype.Year;
                }
                
            }
        }
        //public int Peplesum
        //{
        //    get
        //    {
        //        return Convert.ToInt32(spinEdit1.Value);
        //    }
        //    set
        //    {
        //        spinEdit1.Value = Convert.ToDecimal(value);
        //    }
        //}
        public string Title {
            //get { return this.textEdit1.Text; }
            //set { textEdit1.Text = value; }
            get { return this.comboBoxEdit1.Text; }
            set { comboBoxEdit1.Text = value; }
        }
        private string s1;
        public string  S1
        {
            get { return s1; }
            set { 
                if (!string.IsNullOrEmpty(value))
                {
                    PSPDEV devzx = new PSPDEV();
                    devzx.SUID = value;
                    devzx = Services.BaseService.GetOneByKey<PSPDEV>(devzx);
                    if (devzx!=null)
                    {
                        comboBoxEdit2.Text= devzx.Name;
                        s1 = devzx.SUID;
                    }
                    else
                    {
                        comboBoxEdit2.Text = value;
                        s1 = value;
                    }
                }
               }
        }
        //public int Year {
        //    get { return Convert.ToInt32(comboBoxEdit2.Text); }
        //    set {
        //        if (value!=null)
        //            {
        //                comboBoxEdit2.Text = value.ToString(); 
        //            }
        //        }
        //}
        private void PDtypefrmedit_Load(object sender, EventArgs e) {
            string DQ = "市区";
            string conn = "ProjectID='" + Itop.Client.MIS.ProgUID + "' and Col1='" + DQ + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            foreach (PS_Table_AreaWH area in list) {
                this.comboBoxEdit1.Properties.Items.Add(area.Title);
            }
            //for (int i = 0; i < 60;i++ ) {
            //    string y = (2000 + i).ToString();
            //    this.comboBoxEdit2.Properties.Items.Add(y);
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
        }

       
      

        private void comboBoxEdit2_Properties_Click(object sender, EventArgs e)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();
            dlg.InitDeviceType("01");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, object> dic = dlg.GetSelectedDevice();
                PSPDEV devzx = dic["device"] as PSPDEV;
                S1 = devzx.SUID;

            }
        }


    }
}