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
    public partial class frmSubstationParMng : FormModuleBase
    {
        public static string key = "No";
        public frmSubstationParMng()
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
            ctrlPSP_SubstationPar1.RefreshData(1);
            ctrlPSP_SubstationPar2.RefreshData(2);
        }
        protected override void Add()
        {
            if (tabPage1.CanFocus)
            {
                ctrlPSP_SubstationPar1.AddObject(1);
            }
            if (tabPage2.CanFocus)
            {
                ctrlPSP_SubstationPar2.AddObject(2);
            }
        }
        protected override void Edit()
        {
            if (tabPage1.CanFocus)
            {
                ctrlPSP_SubstationPar1.UpdateObject();
            }
            if (tabPage2.CanFocus)
            {
                ctrlPSP_SubstationPar2.UpdateObject();
            }
        }
        protected override void Del()
        {
            if (tabPage1.CanFocus)
            {
                ctrlPSP_SubstationPar1.DeleteObject();
            }
            if (tabPage2.CanFocus)
            {
                ctrlPSP_SubstationPar2.DeleteObject();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //System.Drawing.Bitmap bmp = new Bitmap(15000, 15000, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            //Graphics g = Graphics.FromImage(bmp);
            //g.Clear(Color.Blue);
            //bmp.Save("C:\\11.bmp");
            if (checkBox1.Checked == true)
            {
                key = "Yes";
            }
            else
            {
                key = "No";
            }
        }
    }
}