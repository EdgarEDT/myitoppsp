using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Itop.TLPSP.DEVICE
{
    public partial class FrmcopyDYtitle : DevExpress.XtraEditors.XtraForm
    {
        public FrmcopyDYtitle()
        {
            InitializeComponent();
        }
        public string DYTitle
        {
            get { return textEdit1.Text; }
            set { textEdit1.Text = value; }
        }
    }
}