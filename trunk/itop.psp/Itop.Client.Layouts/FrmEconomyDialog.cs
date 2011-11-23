using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Layouts;
using Itop.Client.Base;
namespace Itop.Client.Layouts
{
    public partial class FrmEconomyDialog : FormBase
    {

        IList<EconomyData> edlist=new List<EconomyData>();
        Economy economy;

        public Economy Econ
        {
            get { return economy; }
            set { economy = value; }  
        }

        public IList<EconomyData> EDlist
        {
            get { return edlist; }
            set { edlist=value;}
        }


        public FrmEconomyDialog()
        {
            InitializeComponent();
        }

        private void FrmEconomyDialog_Load(object sender, EventArgs e)
        {
            IList<Economy> list = new List<Economy>();

            list.Add(economy);
            vGridControl1.DataSource = list;

            edlist.Clear();
            EconomyData ed = new EconomyData();
            ed.S1 = 2001;
            ed.S2 = 10000;
            edlist.Add(ed);

            ed = new EconomyData();
            ed.S1 = 2002;
            ed.S2 = 20000;
            edlist.Add(ed);

            ed = new EconomyData();
            ed.S1 = 2003;
            ed.S2 = 30000;
            edlist.Add(ed);

            vGridControl2.DataSource = edlist;


        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int years = economy.S20;
            int counts = economy.S21;

            if (edlist.Count > 0)
                edlist.Clear();
            for (int i = 0; i < counts; i++)
            {
                EconomyData ed = new EconomyData();
                ed.S1 = years + i;
                edlist.Add(ed);
            }
            vGridControl2.Refresh();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}