using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmFKbi : FormBase
    {
        public FrmFKbi()
        {
            InitializeComponent();
        }

        private void FrmFKbi_Load(object sender, EventArgs e)
        {
            this.spinEdit1.Value = decimal.Parse("0.96");
        }
        public double GetVal
        {
            get { return Convert.ToDouble(this.spinEdit1.Value); }
        }
    }
}