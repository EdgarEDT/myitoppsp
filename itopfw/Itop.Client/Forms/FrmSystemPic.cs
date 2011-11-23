using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Itop.Client.Forms
{
    public partial class FrmSystemPic : UserControl
    {
        public FrmSystemPic()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(233, 248, 255);
        }
        string systempic = string.Empty;
        string nowsystempic = string.Empty;
        int m = 0;
        /// <summary>
        /// Ìæ»»Í¼Æ¬
        /// </summary>
        /// <param name="picturename">ÒªÌæ»»Í¼Æ¬µÄÃû³Æ</param>
        public void ChangePicture(string picturename)
        {
            m = 0;
            timer1.Stop();
            label1.Visible = false;
            if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\ItopSystemPic\\SystemPic.jpg"))
	        {
                systempic = string.Empty;
                label1.Visible = true;
                return;
	        }
            if (!File.Exists(System.Windows.Forms.Application.StartupPath + "\\ItopSystemPic\\"+picturename+".jpg"))
            {
                nowsystempic = string.Empty;
                pictureBox1.ImageLocation = nowsystempic;
                return;
            }
            systempic = System.Windows.Forms.Application.StartupPath + "\\ItopSystemPic\\SystemPic.jpg";
            nowsystempic = System.Windows.Forms.Application.StartupPath + "\\ItopSystemPic\\" + picturename + ".jpg";
            timer1.Start();
            pictureBox1.ImageLocation = nowsystempic;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m++;
            if (m%2==0)
            {
                pictureBox1.ImageLocation = systempic;
            }
            else
            {
                pictureBox1.ImageLocation = nowsystempic;
            }
        }
       
    }
}
