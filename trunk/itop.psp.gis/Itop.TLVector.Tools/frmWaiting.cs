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
    public partial class frmWaiting : FormBase
    {
        Font f = new Font("宋体", 10);
        SolidBrush brush = new SolidBrush(Color.Black);
        public frmWaiting()
        {
            InitializeComponent();
        }

        private void frmWaiting_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("正在计算面积，请稍候......",f,brush,new PointF(10f,10f));
        }
    }
}