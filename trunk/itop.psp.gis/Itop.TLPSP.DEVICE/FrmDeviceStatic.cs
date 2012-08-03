using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;

namespace Itop.TLPSP.DEVICE
{
    public partial class FrmDeviceStatic : DevExpress.XtraEditors.XtraForm
    {
        public FrmDeviceStatic()
        {
            InitializeComponent();
        }
        public void Init( string[] type,string year)
        {
            this.Text = "设备参数管理";
            this.WindowState = FormWindowState.Maximized;
            InitDeviceType(type, year);
        }
        private UCDeviceBase createInstance(string classname)
        {
            return Assembly.GetExecutingAssembly().CreateInstance(classname, false) as UCDeviceBase;
        }
        private void showDevice(UCDeviceBase device)
        {
            if (device == null) return;
            device.Dock = DockStyle.Fill;
            this.Controls.Add(device);
        }
        private void InitDeviceType(string[] type,string year)
        {
          DataTable dt=  DeviceTypeHelper.GetDeviceTypes(type);
            if (dt.Rows.Count>0)
            {
                string strID = dt.Rows[0]["id"].ToString();
                string dtype = dt.Rows[0]["class"].ToString();
               

                   UCDeviceBase device = null;
               
                    device = createInstance(dtype);
                    device.ID = strID;
                    device.ProjectID = Itop.Client.MIS.ProgUID;
                    showDevice(device);   
                    
                    string strCon = " year(cast(OperationYear as datetime))<" + year + "  and Type='" + strID + "' ";
                    device.Statictable(strCon);
                   curDevice = device;
            }
          
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        UCDeviceBase curDevice;
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            curDevice.gridControl1.ExportToExcelOld("C:\\temp.xls");
            System.Diagnostics.Process.Start("C:\\temp.xls");
        }

    }
}