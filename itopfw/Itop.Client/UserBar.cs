using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace Itop.Client
{
    public partial class UserBar : UserControl
    {
        public  enum bartype
        {
            min,
            max,
            close
        }
        public bartype _abrtype;
        public bartype BarType
        {
            set { 
                _abrtype = value;
                switch (BarType)
                {
                    case bartype.min:
                        labMin.ImageIndex = 0;
                        break;
                    case bartype.max:
                        labMin.ImageIndex = 2;
                        break;
                    case bartype.close:
                        labMin.ImageIndex = 4;
                        break;
                    default:
                        break;
                }
            }
            get { return _abrtype; }
        }
        public UserBar()
        {
            InitializeComponent();
           
        }
        public delegate void barClick();
        public event barClick BarClick;

        private void labMin_MouseEnter(object sender, EventArgs e)
        {
            labMin.ImageIndex += 1;
        }

        private void labMin_MouseLeave(object sender, EventArgs e)
        {
            labMin.ImageIndex -= 1;
        }
        private void labMin_Click(object sender, EventArgs e)
        {
            BarClick();
        }

       
    }
}
