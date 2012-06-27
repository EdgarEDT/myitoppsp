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
using Itop.Client.Base;
namespace Itop.TLPsp.Graphical {
    public partial class frmSetPower : FormBase {
        public frmSetPower() {
            InitializeComponent();
        }
        private string projectid;
        private IList listpower = new List<PSPDEV>();
        public IList ListPower {
            get { return listpower; }
            set { listpower = value; }
        }
        private IList listbranch = new List<PSPDEV>();
        public IList ListBranch {
            get { return listbranch; }
            set { listbranch = value; }
        }
        public string ProjectID {
            get { return projectid; }
            set { projectid = value; }
        }
        private void simpleButton1_Click(object sender, EventArgs e) {
            frmPowerAndLineList frmPL = new frmPowerAndLineList("01", ProjectID);
            if (frmPL.ShowDialog() == DialogResult.OK) {
                DataTable dataTable = frmPL.DT;
                for (int i = 0; i < dataTable.Rows.Count; i++) {
                    DataRow dr = dataTable.Rows[i];
                    string con = " where suid = '" + dr.ItemArray[0].ToString() + "'";
                    IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                    if (dr.ItemArray[2].ToString() == "True" && list.Count > 0) {
                        listpower.Add((PSPDEV)list[0]);
                    }

                }
            } else {
                return;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e) {
            frmPowerAndLineList frmPL = new frmPowerAndLineList("05", ProjectID);
            if (frmPL.ShowDialog() == DialogResult.OK) {
                DataTable dataTable = frmPL.DT;
                for (int i = 0; i < dataTable.Rows.Count; i++) {
                    DataRow dr = dataTable.Rows[i];
                    string con = " where suid = '" + dr.ItemArray[0].ToString() + "'";
                    IList list = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                    if (dr.ItemArray[2].ToString() == "True" && list.Count > 0) {
                        listbranch.Add((PSPDEV)list[0]);
                    }

                }
            } else {
                return;
            }
        }
    }
}