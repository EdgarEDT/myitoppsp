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
    public partial class PdDateEdit : DevExpress.XtraEditors.XtraForm {
        public PdDateEdit() {
            InitializeComponent();
            Init();
        }
        public PDrelregion parentobj = new PDrelregion();
        private PDrelcontent rowdate = new PDrelcontent();
        public PDrelcontent RowData
        {
            get
            {
                rowdate.TDdatetime = TDdatetime;
                rowdate.TDtime = TDtime;
                rowdate.TDtype = TDtype;
                rowdate.PeopleRegion = PeopleRegion;
                rowdate.AvgFH = AvgFH;
                rowdate.S1 = S1;
                return rowdate;
            }
            set
            {
                rowdate = value;
                TDdatetime = rowdate.TDdatetime;
                TDtime = rowdate.TDtime;
                PeopleRegion = rowdate.PeopleRegion;
                TDtype = rowdate.TDtype;
                AvgFH = rowdate.AvgFH;
                S1 = rowdate.S1;
            }
        }
        public DateTime TDdatetime
        {
            get { 
                
                return dateEdit1.DateTime;
            }
            set {
                dateEdit1.DateTime = value; 
            }
        }
        public double TDtime
        {
            get { return (double)spinEdit1.Value; }
            set { spinEdit1.Value = Convert.ToDecimal(value); }
        }
        public int PeopleRegion {
            get { return (int)spinEdit1.Value; }
            set { spinEdit1.Value = Convert.ToDecimal(value); }
        }
        public string TDtype {
            get { return comboBoxEdit1.Text; }
            set { comboBoxEdit1.Text = value; }
        }
        public double AvgFH {
            get { return (double)spinEdit3.Value; }
            set { spinEdit3.Value = Convert.ToDecimal(value); }
        }
        public string S1 {
            get { return spinEdit4.Value.ToString(); }
            set { spinEdit4.Value = Convert.ToDecimal(value); }
        }
        protected void Init() {
            comboBoxEdit1.Properties.Items.Add("故障停电");
            comboBoxEdit1.Properties.Items.Add("外部影响");
            comboBoxEdit1.Properties.Items.Add("系统资源不足限电");
            
        }
        private void simpleButton1_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
        }
    }
}