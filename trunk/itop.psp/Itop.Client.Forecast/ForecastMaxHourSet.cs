using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.Forecast
{
    public partial class ForecastMaxHourSet : Form
    {
        public ForecastMaxHourSet()
        {
            InitializeComponent();
        }
        //为真表示电量单位为亿kwh，为假表示电量单位为万kwh
        public bool isyqwh = true;
        private void ForecastMaxHourSet_Load(object sender, EventArgs e)
        {
            if (isyqwh)
            {
                rbdlone.Checked = true;
                rbdltwo.Checked = false;
            }
            else
            {
                rbdlone.Checked = false;
                rbdltwo.Checked = true;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void rbdlone_CheckedChanged(object sender, EventArgs e)
        {
            rbdltwo.Checked = !rbdlone.Checked;
            isyqwh = rbdlone.Checked;
        }

        private void rbdltwo_CheckedChanged(object sender, EventArgs e)
        {
            rbdlone.Checked = !rbdltwo.Checked;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
