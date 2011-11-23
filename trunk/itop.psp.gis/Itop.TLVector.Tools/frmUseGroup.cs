using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Domain.Graphics;
using Itop.Client.Common;

namespace ItopVector.Tools
{
    public partial class frmUseGroup : FormBase
    {
        public frmUseGroup()
        {
            InitializeComponent();
        }

        private void frmUseGroup_Load(object sender, EventArgs e)
        {
            ctrlUseGroup1.RefreshData();
        }

        private void ctrlUseGroup1_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public UseGroup SelectedUseGroup
        {
            get { return ctrlUseGroup1.FocusedObject; }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ctrlUseGroup1.DeleteObject();
        }
    }
}