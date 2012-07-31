using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.Forecast
{
    public partial class ForecastUnitConsumptionValueSet : Form
    {
        public ForecastUnitConsumptionValueSet()
        {
            InitializeComponent();
        }
        //为真表示单产单位为kWh/元，为假表示Wh/元
        public bool isdhkw = true;
        private void ForecastMaxHourSet_Load(object sender, EventArgs e)
        {
            if (isdhkw)
            {
                rbdhone.Checked = true;
                rbdhtwo.Checked = false;
            }
            else
            {
                rbdhone.Checked = false;
                rbdhtwo.Checked = true;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }


        private void rbdhone_CheckedChanged(object sender, EventArgs e)
        {
            rbdhtwo.Checked = !rbdhone.Checked;
            isdhkw = rbdhone.Checked;
        }

        private void rbdhtwo_CheckedChanged(object sender, EventArgs e)
        {
            rbdhone.Checked = !rbdhtwo.Checked;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {

        }

    

        

        
    }
}
