using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.TLPsp
{
    public partial class start : FormBase
    {
        public start()
        {
            InitializeComponent();
        }

        public void setEnabled(bool b)
        {
            simpleButton2.Enabled = b;
            simpleButton2.Visible = b;
            simpleButton3.Visible = !b;
            simpleButton3.Enabled = !b;
        }       
    }
}