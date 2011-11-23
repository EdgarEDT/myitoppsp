using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using System.Collections;
using Itop.Client.Common;

namespace ItopVector.Tools
{
    public partial class frmCS : Itop.Client.Base.FormBase
    {
        public frmCS()
        {
            InitializeComponent();
            InitData();
        }
        private void InitData()
        {
            CanShu cs = new CanShu();
            IList list = Services.BaseService.GetList("SelectCanShuList",cs);
            gridControl1.DataSource = list;
            FenZhi fz = new FenZhi();
            IList listf = Services.BaseService.GetList("SelectFenZhiList", fz);
            gridControl.DataSource = listf;
        }

        private void frmCS_Leave(object sender, EventArgs e)
        {

            foreach (CanShu cs in gridControl1.DataSource as IList)
            {
                Services.BaseService.Update<CanShu>(cs);
            }
            foreach (FenZhi fz in gridControl.DataSource as IList)
            {
                Services.BaseService.Update<FenZhi>(fz);
            }
        }

        private void frmCS_FormClosed(object sender, FormClosedEventArgs e)
        {

            foreach (CanShu cs in gridControl1.DataSource as IList)
            {
                Services.BaseService.Update<CanShu>(cs);
            }
            foreach (FenZhi fz in gridControl.DataSource as IList)
            {
                Services.BaseService.Update<FenZhi>(fz);
            }
        }
         
    }
}