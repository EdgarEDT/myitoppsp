using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using System.Text.RegularExpressions;
using Itop.Domain.Stutistic;
using Itop.Domain.Graphics;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmProjTZEdit : FormBase
    {
        public FrmProjTZEdit()
        {
            InitializeComponent();
        }
        public Ps_Table_BuildPro Currentptb = null;
       
        private decimal Changetz(string str)
        {
            decimal tempdl = 0;
            if (decimal.TryParse(str, out tempdl))
            {
                
            }
            return tempdl;
        }
        private void FrmProjTZEdit_Load(object sender, EventArgs e)
        {
            sptz0.Value = decimal.Parse(Currentptb.AllVolumn.ToString());
            sptz1.Value = Changetz((Currentptb.Col6 == null || Currentptb.Col6 == "") ? "" : Currentptb.Col6);
            sptz2.Value = Changetz((Currentptb.Col7 == null || Currentptb.Col7 == "") ? "" : Currentptb.Col7);
            sptz3.Value = Changetz((Currentptb.Col8 == null || Currentptb.Col8 == "") ? "" : Currentptb.Col8);
            sptz4.Value = Changetz((Currentptb.Col9 == null || Currentptb.Col9 == "") ? "" : Currentptb.Col9);
           
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Currentptb.AllVolumn =double.Parse(sptz0.Value.ToString());
            Currentptb.Col6 = sptz1.Value.ToString();
            Currentptb.Col7 = sptz2.Value.ToString();
            Currentptb.Col8 = sptz3.Value.ToString();
            Currentptb.Col9 = sptz4.Value.ToString();
            DialogResult = DialogResult.OK;
            this.Close();
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}