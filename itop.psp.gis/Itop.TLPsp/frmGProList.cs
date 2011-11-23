using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.Collections;

namespace Itop.TLPsp
{
    public partial class frmGProList : FormModuleBase
    {
        public frmGProList()
        {
            InitializeComponent();
        }
        public void InitData(string svgUid,string sid)
        {
            //ctrlglebeProperty1.InitData(svgUid,sid);
        }
        public void InitDataSub(string svgUid,string sid)
        {
            //ctrlglebeProperty1.InitDataSub(svgUid,sid);
        }
        protected override void Print()
        {
            ComponentPrint.ShowPreview(this.gridControl, this.Text);
        }
        public void LoadData(List<LineInfo> list)
        {
            gridControl.DataSource = list;
            gridControl.RefreshDataSource();
        }
        private void frmglebePropertyList_Load(object sender, EventArgs e)
        {
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            //this.Text = ctrlglebeProperty1.GridControl.Text;

        }

        private void gridControl_Click(object sender, EventArgs e)
        {
         
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            List<LineInfo> clist =new List<LineInfo>();
            clist.Clear();
            LineInfo a = gridView.GetRow(this.gridView.FocusedRowHandle) as LineInfo;
            PSPDEV psp = new PSPDEV();
            psp.SvgUID = a.SvgUID;
            psp.Type = "Polyline";
            IList list1 = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", psp);
            if (a.ObligateField1=="整体网架规划")
            {
                for (int i = 0; i < list1.Count;i++ )
                {
                    LineInfo l1 = new LineInfo();
                    psp = list1[i] as PSPDEV;
                    if (psp.LineStatus=="等待")
                    {
                        
                        l1.EleID = psp.EleID;
                        l1.ObligateField2 = psp.Name;
                        l1.ObligateField3 = "淘汰";
                        clist.Add(l1);
                    }
                    else if (psp.LineStatus=="待选")
                    {
                        l1.EleID = psp.EleID;
                        l1.ObligateField2 = psp.Name;
                        l1.ObligateField3 = "待选";
                        clist.Add(l1);
                    }
                }
            }
            else
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    LineInfo l1 = new LineInfo();
                    psp = list1[i] as PSPDEV;
                    if (psp.LineStatus == "等待")
                    {

                        l1.EleID = psp.EleID;
                        l1.ObligateField2 = psp.Name;
                        l1.ObligateField3 = "待选";
                        clist.Add(l1);
                    }
                    else if (psp.LineStatus == "待选")
                    {
                        l1.EleID = psp.EleID;
                        l1.ObligateField2 = psp.Name;
                        l1.ObligateField3 = "待建";
                        clist.Add(l1);
                    }
                }
            }
            FormGXXlist gxx = new FormGXXlist();
            gxx.gridview.GroupPanelText = a.ObligateField1+"线路情况";
            gxx.Show();
            gxx.LoadData(clist);
        }
    }
}