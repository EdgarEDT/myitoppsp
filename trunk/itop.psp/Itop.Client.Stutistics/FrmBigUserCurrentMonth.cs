using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.Client.Stutistics
{
    public partial class FrmBigUserCurrentMonth : FormBase
    {
        private int month=0;
        private List<int> listYearsForChoose = new List<int>();
        public List<int> ListYearsForChoose
        {
            get { return listYearsForChoose; }
            set { listYearsForChoose = value; }
        }
        public int Month
        {
            get { return month; }
            set { month = value; }
        }
        public FrmBigUserCurrentMonth()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            month = Convert.ToInt32(comboBox1.Text.Replace("��",""));
            this.DialogResult = DialogResult.OK;
        }

        private void FrmBigUserCurrentMonth_Load(object sender, EventArgs e)
        {
            foreach (int i in listYearsForChoose)
            {
                comboBox1.Items.Add(i + "��");

            
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.Text = comboBox1.Items[0].ToString();
        }
    }
}