using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmGlebeTypeList : FormBase
    {
        public frmGlebeTypeList()
        {
            InitializeComponent();
        }

        private void ctrlglebeType11_Load(object sender, EventArgs e)
        {
            ctrlglebeType11.RefreshData();
        }
    }
}