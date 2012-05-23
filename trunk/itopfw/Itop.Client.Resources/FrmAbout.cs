using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Itop.Client.Resources
{
    public partial class FrmAbout : XtraForm 
    {
        public FrmAbout()
        {
            InitializeComponent();
            
            this.BackgroundImage = Itop.Client.Resources.ImageListRes.GetAboutPhoto();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}