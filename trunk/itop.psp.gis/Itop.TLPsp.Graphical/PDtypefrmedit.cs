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
namespace Itop.TLPsp.Graphical {
    public partial class PDtypefrmedit : DevExpress.XtraEditors.XtraForm {
        public PDtypefrmedit() {
            InitializeComponent();
        }
        private PDrelregion pdtype = new PDrelregion();
        public PDrelregion Pdtype
        {
            get{
                pdtype.Year = Year;
                pdtype.PeopleSum = Peplesum;
                pdtype.AreaName = Areaname;
                return pdtype;
            }
            set{
                if (value!=null)
                {
                    pdtype=value;
                    Areaname = pdtype.AreaName;
                    Peplesum = pdtype.PeopleSum;
                    Year = pdtype.Year;
                }
                
            }
        }
        public int Peplesum
        {
            get
            {
                return Convert.ToInt32(spinEdit1.Value);
            }
            set
            {
                spinEdit1.Value = Convert.ToDecimal(value);
            }
        }
        public string Areaname {
            //get { return this.textEdit1.Text; }
            //set { textEdit1.Text = value; }
            get { return this.comboBoxEdit1.Text; }
            set { comboBoxEdit1.Text = value; }
        }
        public int Year {
            get { return Convert.ToInt32(comboBoxEdit2.Text); }
            set {
                if (value!=null)
                    {
                        comboBoxEdit2.Text = value.ToString(); 
                    }
                }
        }
        private void PDtypefrmedit_Load(object sender, EventArgs e) {
            string DQ = "市区";
            string conn = "ProjectID='" + Itop.Client.MIS.ProgUID + "' and Col1='" + DQ + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            foreach (PS_Table_AreaWH area in list) {
                this.comboBoxEdit1.Properties.Items.Add(area.Title);
            }
            for (int i = 0; i < 60;i++ ) {
                string y = (2000 + i).ToString();
                this.comboBoxEdit2.Properties.Items.Add(y);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
        }


    }
}