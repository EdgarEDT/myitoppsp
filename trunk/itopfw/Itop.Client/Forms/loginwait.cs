using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;

namespace Itop.Client.Forms
{
    public partial class loginwait : FormBase
    {
        public loginwait()
        {
            InitializeComponent();
        }

        private void loginwait_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Itop.Client.Resources.ImageListRes.GetLoginWaitPhoto();
            this.TopMost = true;
            timer1.Start();
            this.Opacity = 0.01;
        }
        bool flag = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!flag)
            {
                this.Opacity += 0.01;
            }
            else
            {
                this.Opacity -= 0.01;
            }
            if (this.Opacity==1)
            {
                flag = true;
            }
            if (this.Opacity==0)
            {
                this.Close();
            }
        }
    }
}
