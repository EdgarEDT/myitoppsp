using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.Stutistics
{
    public partial class Frm_Power : Itop.Client.Base.FormModuleBase
    {
        string type = "";
        public void IsSubstation()
        {type="变电站";
            this.ShowDialog();}

        public void IsWater()
        { type="火电厂";
            this.ShowDialog();}

        public void IsFire()
        { type="水电厂";
            this.ShowDialog();}

        public void Wind()
        { type="风场";
            this.ShowDialog();}
        



        public Frm_Power()
        {
            InitializeComponent();
        }

        private void Frm_Power_Load(object sender, EventArgs e)
        {
            this.ctrlPs_Power1.TYPE = type;
            this.ctrlPs_Power1.GridView.GroupPanelText = type;
            this.ctrlPs_Power1.RefreshData();
        }

        protected override void Add()
        {
            this.ctrlPs_Power1.AddObject();
        }

        protected override void Edit()
        {
            this.ctrlPs_Power1.UpdateObject();
        }

        protected override void Del()
        {
            this.ctrlPs_Power1.DeleteObject();
        }

        protected override void Print()
        {
            this.ctrlPs_Power1.PrintPreview();
        }
    }
}