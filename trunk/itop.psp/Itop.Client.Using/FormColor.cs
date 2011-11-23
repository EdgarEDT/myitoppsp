using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Client.Common;
using Itop.Client.Base;
namespace Itop.Client.Using{
    public partial class FormColor : FormBase
    {
        public DataTable DT
        {
            set { this.ctrlBaseColor1.DT = value; }
        }

        public string ID
        {
            set { this.ctrlBaseColor1.ID = value; }
        }

        public int For
        {
            set { this.ctrlBaseColor1.For = value; }
        }


        public FormColor()
        {
            InitializeComponent();
        }

        private void FormColor_Load(object sender, EventArgs e)
        {
            this.ctrlBaseColor1.RefreshData();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.textBox1.Focus();
            this.ctrlBaseColor1.SaveList();
            this.DialogResult = DialogResult.OK;
        }



    }
}