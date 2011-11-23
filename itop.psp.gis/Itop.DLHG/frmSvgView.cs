using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;

namespace Itop.DLGH
{
    public partial class frmSvgView : FormBase
    {
        public frmSvgView()
        {   
            InitializeComponent();                   
        }
        public void Open(string svgDataUID)
        {
            ctrlSvgView1.OpenFromDatabase(svgDataUID);
        }
        public bool Start()
        {
          
            this.Show();
           
            Open("f3eafde4-50c5-4112-925c-c569513230f0");
            
            return true;
        }
      
    }
}