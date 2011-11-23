using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using Itop.Client.Base;

namespace ItopVector.Tools
{
    public partial class frmLineParMng : FormModuleBase
    {
        public static string key = "No";
        public frmLineParMng()
        {
            InitializeComponent();
        }

        private void frmSubstationParMng_Load(object sender, EventArgs e)
        {
            barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            loadData();
        }
        public void loadData()
        {
            ctrlPSP_SubstationPar1.RefreshData(3);
        
        }
        protected override void Add()
        {
             ctrlPSP_SubstationPar1.AddObject(3);
        }
        protected override void Edit()
        {
             ctrlPSP_SubstationPar1.UpdateObject();
        }
        protected override void Del()
        {
             ctrlPSP_SubstationPar1.DeleteObject();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
          
           
        }
    }
}