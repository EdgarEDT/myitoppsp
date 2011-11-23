using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.History
{
    public partial class FormHistoryTypeEditDeal : DevExpress.XtraEditors.XtraForm
    {
        private string typeTitle = string.Empty;
        public List<string> addlist = new List<string>();
        public List<string> reducelist = new List<string>();
        public List<string> changeUnitslist = new List<string>();

        public string TypeTitle
        {
            get { return typeTitle; }
            set { typeTitle = value; }
        }
    

        public FormHistoryTypeEditDeal()
        {
            InitializeComponent();
        }

        private void FormHistoryTypeEditDeal_Load(object sender, EventArgs e)
        {
            this.Text = TypeTitle;
            int m = 0;
            listBox1.DataSource = addlist;
            listBox2.DataSource = reducelist;
            listBox3.DataSource = changeUnitslist;
            Point temP = new Point();
            if (addlist.Count>0)
            {
                temP.X = 14;
                temP.Y = 25 + m * (136);
                label1.Location = temP;
                temP.X = 11;
                temP.Y = 40 + m * (136);
                listBox1.Location = temP;
                m = m + 1;
            }
            else
            {
                label1.Visible = false;
                listBox1.Visible = false;

            }
            if (reducelist.Count > 0)
            {
                temP.X = 14;
                temP.Y = 25 + m * (136);
                label2.Location= temP;
                temP.X = 11;
                temP.Y = 40 + m * (136);
                listBox2.Location = temP;
                m = m + 1;
            }
            else
            {
                label2.Visible = false;
                listBox2.Visible = false;

            }
            if (changeUnitslist.Count > 0)
            {
                temP.X = 14;
                temP.Y = 25 + m * (136);
                label3.Location = temP;
                temP.X = 11;
                temP.Y = 40 + m * (136);
                listBox3.Location = temP;
                m = m + 1;
            }
            else
            {
                label3.Visible = false;
                listBox3.Visible = false;

            }

            System.Drawing.Size tempsize = new Size();
            tempsize.Width=groupBox1.Size.Width;
            tempsize.Height=162+(m-1)*140;

            groupBox1.Size=tempsize;
            temP.X = simpleButton1.Location.X;
            temP.Y = groupBox1.Size.Height + 15;
            simpleButton1.Location = temP;
            temP.X = simpleButton2.Location.X;
            temP.Y = groupBox1.Size.Height + 15;
            simpleButton2.Location = temP;

            tempsize.Width=this.Size.Width;
            tempsize.Height=560 - (3 - m) * 140;
            this.Size = tempsize;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}