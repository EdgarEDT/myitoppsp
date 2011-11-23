using System;
using System.Collections.Generic;
using System.Text;
using Itop.TLPSP.DEVICE;
using System.Xml;
using ItopVector.Core;
using ItopVector.Core.Interface.Figure;
using System.Windows.Forms;
using ItopVector.Core.Figure;

namespace ItopVector.Tools
{
    /// <summary>
    /// 系统接线图线路定位
    /// </summary>
    partial class frmMain_wj
    {
        private void openAutojxt() {
            if (dlg == null) {
                dlg = new frmAutojxt();
                dlg.OnLoactionXL += new EventHandler(dlg_OnLoactionXL);
                dlg.FormClosed += new System.Windows.Forms.FormClosedEventHandler(dlg_FormClosed);
                dlg.ShowIcon = false;
                dlg.Owner = this;
                dlg.Show();
            } else {
                dlg.WindowState = System.Windows.Forms.FormWindowState.Normal;
            }
            
        }
        void locationJxtXL() {
            openAutojxt();
            
            Application.DoEvents();
            Polyline pol = tlVectorControl1.SVGDocument.CurrentElement as Polyline;
            if(pol!=null){
                string id = pol.GetAttribute("Deviceid");
                dlg.LocationXlbyId(id);
            }
        }
        void dlg_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e) {
            dlg = null;
        }
        frmAutojxt dlg = null;
        void dlg_OnLoactionXL(object sender, EventArgs e) {
            if (dlg != null) dlg.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            string deviceid = sender as string;
            ItopVectorControl xltVectorCtrl = tlVectorControl1;
            XmlNodeList elelist = xltVectorCtrl.SVGDocument.SelectNodes("svg/*[@Deviceid='" + deviceid + "']");
            if (elelist.Count == 1) {
                SvgElement ele = (SvgElement)elelist[0];
                xltVectorCtrl.GoLocation((IGraph)ele);
                xltVectorCtrl.Refresh();
                xltVectorCtrl.SVGDocument.CurrentElement = ele;
            } else {
                xltVectorCtrl.SVGDocument.SelectCollection.Clear();
                for (int i = 0; i < elelist.Count; i++) {
                    SvgElement ee = (SvgElement)elelist[i];

                    xltVectorCtrl.SVGDocument.SelectCollection.Add(ee);
                    xltVectorCtrl.Refresh();
                }
            }
        }
    }
}
