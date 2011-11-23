using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.BaseData
{
    public partial class FrmColorData : Itop.Client.Base.FormModuleBase
    {
        public FrmColorData()
        {
            InitializeComponent();
        }


        protected override void Add()
        {
            this.ctrlBaseColor1.AddObject();
        }

        protected override void Edit()
        {
            this.ctrlBaseColor1.UpdateObject();
        }

        protected override void Del()
        {
            this.ctrlBaseColor1.DeleteObject();
        }

        protected override void Print()
        {
            this.ctrlBaseColor1.PrintPreview();
        }

        private void FrmColorData_Load(object sender, EventArgs e)
        {
            this.ctrlBaseColor1.RefreshData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorClass csss=new ColorClass();

            MessageBox.Show(csss.GetColor(2000).R.ToString());
        }
    }
}