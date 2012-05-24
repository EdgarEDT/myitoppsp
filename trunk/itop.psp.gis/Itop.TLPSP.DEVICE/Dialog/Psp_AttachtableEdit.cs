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
namespace Itop.TLPSP.DEVICE
{
    public partial class Psp_AttachtableEdit : DevExpress.XtraEditors.XtraForm {
        public Psp_AttachtableEdit()
        {
            InitializeComponent();
            Init();
        }
        public Psp_Attachtable parentobj = new Psp_Attachtable();
        private Psp_Attachtable rowdate = new Psp_Attachtable();
        public Psp_Attachtable RowData
        {
            get
            {
                rowdate.startYear = startyear;
                rowdate.endYear = endyear;
                rowdate.ZHI = zhi;
                rowdate.S1 = S1;
                rowdate.S2= S2;
               
                return rowdate;
            }
            set
            {
                rowdate = value;
                startyear = rowdate.startYear;
                endyear = rowdate.endYear;
                zhi = rowdate.ZHI;
                S1 = rowdate.S1;
                S2 = rowdate.S2;
               
            }
        }
        public string startyear
        {
            get { 
                
                return comboBoxEdit2.Text;
            }
            set {
                comboBoxEdit2.Text = value; 
            }
        }
        public string endyear
        {
            get { return comboBoxEdit3.Text; }
            set { comboBoxEdit3.Text = value; }
        }
        public double zhi {
            get { return (double)spinEdit1.Value; }
            set { spinEdit1.Value = Convert.ToDecimal(value); }
        }
        public string S2 {
            get { return comboBoxEdit4.Text; }
            set { comboBoxEdit4.Text = value; }
        }
      
        public string S1 {
            get { return comboBoxEdit1.Text; }
            set { comboBoxEdit1.Text = value; }
        }
        protected void Init() {
            comboBoxEdit4.Properties.Items.Add("投产");
            comboBoxEdit4.Properties.Items.Add("退出");
            comboBoxEdit4.Properties.Items.Add("扩容");
            for (int i = 0; i < 30;i++ )
            {
                string s = (2000 + i).ToString();
                comboBoxEdit2.Properties.Items.Add(s);
                comboBoxEdit3.Properties.Items.Add(s);

            }
            string sql = "where type='02'or type='03'and projectid='"+ Itop.Client.MIS.ProgUID+"' ORDER BY Number";
            IList<PSPDEV> list1 = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            List<PSPDEV> list=list1 as List<PSPDEV>;
            comboBoxEdit1.Properties.Items.AddRange(list.ToArray());
        }
        private void simpleButton1_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}