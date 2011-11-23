using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using System.Collections;
using Itop.Common;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.IO;
using System.Xml;
using Itop.Client.Stutistics;
using Itop.Client.Base;
namespace Itop.TLPSP.DEVICE
{
    public partial class DlqiCheckform : FormBase
    {
        public DlqiCheckform()
        {
            InitializeComponent();
        }
         public DlqiCheckform(PSPDEV pspDEV)
         {
             InitializeComponent();
             this.ctrlDlqicheck1.duanluqi = pspDEV;
             this.ctrlDlqicheck1.RefreshData1();
         }
        public CtrlDlqicheck getusercltr
        {
            get 
            {
                return ctrlDlqicheck1;
            }
        }
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlDlqicheck1.PrintPreview();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.ctrlDlqicheck1.GridControl);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ctrlDlqicheck1.FocusedObject == null)
            {
                MsgBox.Show("没有记录存在，不可以保存样式！");
                return;
            }
            string filepath = Path.GetTempPath();
            this.ctrlDlqicheck1.gridView.SaveLayoutToXml(filepath + "duluqi.xml");
        }
    }
}