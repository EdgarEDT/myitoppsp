using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client
{
    public partial class UserText : UserControl
    {
        public void SetFocx()
        {
            this.txtbox.Focus();
        }
        public Image image
        {
            set
            {
                label1.Image = value;
            }
        }
        public TextBox tbox
        {
            get
            {
                return txtbox;
            }
        }
        public UserText()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.txtbox.Focus();
        }

        private void UserText_Enter(object sender, EventArgs e)
        {
            this.txtbox.Focus();
        }
    }
}
