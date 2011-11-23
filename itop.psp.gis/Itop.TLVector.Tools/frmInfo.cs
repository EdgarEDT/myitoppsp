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
    public partial class frmInfo : FormBase
    {
        public string Info = "";
        Font f = new Font("ËÎÌו", 10,FontStyle.Bold);
        
        SolidBrush brush = new SolidBrush(Color.Red);
        public frmInfo()
        {
            
            InitializeComponent();
        }

        private void frmWaiting_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString(Info,f,brush,new PointF(10f,10f));
        }
    }
}