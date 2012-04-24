using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using System.Collections;
using Itop.Client.Projects;
using Itop.Domain.Graphics;
using System.IO;
using System.Xml;
using System.Reflection;

namespace Itop.TLPSP.DEVICE
{
    public partial class Ucjd : DevExpress.XtraEditors.XtraUserControl
    {
        public Ucjd()
        {
            InitializeComponent();
        }
        private PSPDEV paretobj;
        public PSPDEV ParentObj
        {
            get
            {
                return paretobj;
            }
            set{
                if (value!=null)
                {
                    paretobj = value;
                    Init();
                }

            }
        }
        private void Init()
        {
            ucDeviceZX1.ID = "70";
            ucDeviceZX1.ProjectID = Itop.Client.MIS.ProgUID;
            ucDeviceZX1.strCon = " where 1=1 and AreaID='" + paretobj.SUID + "'and ";   //获得此导线下的所有节点
            ucDeviceZX1.Init();
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ucDeviceZX1.ProjectID = paretobj.SUID;
            ucDeviceZX1.Add();
        }
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            ucDeviceZX1.Edit();
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            ucDeviceZX1.Delete();
        }

       
    }
}
