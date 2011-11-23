using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PspShapesLib;
using Itop.Domain.Graphics;
using Netron.GraphLib;
using Itop.Client.Base;
namespace Itop.TLPSP.DEVICE
{
    public partial class frmAutojxt : FormBase
    {
        public frmAutojxt() {
            InitializeComponent();
            ucGraph1.Showdeep = true;
            ucGraph1.Showsbtop = false;
            ucGraph1.ShowSave = true;
            ucGraph1.ShowCreat = false;
            ucGraph1.Open("jxt"+Itop.Client.MIS.ProgUID);
            ucGraph1.OnShapeMenuItemClick += new Netron.GraphLib.ShapeInfo(ucGraph1_OnShapeMenuItemClick);
        }
        public event EventHandler OnLoactionXL;//定位线路
        void ucGraph1_OnShapeMenuItemClick(object sender, Netron.GraphLib.Shape shape) {
            MenuItem menu = sender as MenuItem;
            switch (menu.Text) {
                case "定位到地理信息图":
                    if (OnLoactionXL != null)
                        OnLoactionXL(((BaseShape)shape).DeviceID,null);
                    break;
            }
        }
        public void LocationXlbyId(string deviceid) {
            PSPDEV dev= DeviceHelper.GetDevice<PSPDEV>(deviceid);
            if(dev == null)return;
            Shape shape = ucGraph1.GraphControl.GetShapeByLabel(dev.Name);
            ucGraph1.GraphControl.Deselect();
            if (shape != null) {
                shape.IsSelected = true;
                ucGraph1.GraphControl.Invalidate();
            }
        }
    }
}