using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using System.Xml;
using System.IO;
using DevExpress.XtraTreeList.Columns;
using System.Reflection;
using System.Collections;
using Itop.Client.Projects;
using Itop.Client.Base;
namespace Itop.TLPSP.DEVICE
{
    public partial class SelshortbusForm1 : FormBase
    {
        public SelshortbusForm1()
        {
            InitializeComponent();
        }
        private DataTable dt = new DataTable();
        private string projectid;
        public string ProjectID
        {
            get
            {
                return projectid;
            }
            set
            {
                projectid = value;
            }
        }
        private string projectSUID;
        public string ProjectSUID
        {
            get
            {
                return projectSUID;
            }
            set
            {
                projectSUID = value;
            }
        }

        private List<PSPDEV> _shortbus = new List<PSPDEV>();
        public List<PSPDEV> Shortbus
        {
            get { return _shortbus; }
            set { _shortbus = value; }
        }
        public DataTable DT
        {
            set { dt = value; }
            get { return dt; }
        }
        public void Init()
        {
            dt.Columns.Add("A");
            dt.Columns.Add("B");
            dt.Columns.Add("C", typeof(bool));
            dt.Columns.Add("D");         
            {
                   dt.Clear();
                    string con = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + projectSUID + "'AND PSPDEV.type='01'AND PSPDEV.KSwitchStatus = '0' order by PSPDEV.number";                
                    IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV dev in list)
                    {
                            DataRow row = dt.NewRow();
                            row["A"] = dev.SUID;
                            row["B"] = dev.Name;
                            row["C"] = false;
                            row["D"] = dev.Number;
                            dt.Rows.Add(row);                    
                    }
                gridControl1.DataSource = dt;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
          
            this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SelshortbusForm1_Load(object sender, EventArgs e)
        {
            Init();
        }

    }
}