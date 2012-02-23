using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Client.Base;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ItopVector.Tools
{
    public partial class frmTongJi2 : FormModuleBase
    {
        IList<LineList1> list1 = null;
        ArrayList list2 = new ArrayList();
        IList<PSP_PlanList> sellist = null;

        public frmTongJi2()
        {
            InitializeComponent();
        }

        private void frmTongJi_Load(object sender, EventArgs e)
        {
            barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            //barEditItem3.Visibility =DevExpress.XtraBars.BarItemVisibility.Always;
            PSP_PlanList m = new PSP_PlanList();
            sellist = Services.BaseService.GetList<PSP_PlanList>("SelectPSP_PlanListList", m);
            lookUpEdit1.Properties.DataSource = sellist;
            //if (frmSubstationParMng.key == "No")
            //{
                
            //}
            //else
            //{
            //    LoadData2();
            //}
        }
        public void LoadData(string uid)
        {
            LineList1 line = new LineList1();
            line.col1 = uid;
            IList<LineList1> linelist = Services.BaseService.GetList<LineList1>("SelectLineList1ByRefLineEleID", line);
            ArrayList val = new ArrayList();



            for (int i = 0; i < linelist.Count; i++)
            {
                PSP_SubstationUserNum num1 = new PSP_SubstationUserNum();
                num1.userID = Itop.Client.MIS.UserNumber;
                num1.SubStationID = linelist[i].UID;
                num1.num = 3;
                IList<PSP_SubstationUserNum> sublist = Services.BaseService.GetList<PSP_SubstationUserNum>("SelectPSP_SubstationUserNumByUser", num1);
                if (sublist.Count == 0)
                {
                    MessageBox.Show("线路" + linelist[i].LineName + "还没有评分完成，不能自动计算权值。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            LineList1 n1=new LineList1();
            n1.col1 = uid;
            list1 = Services.BaseService.GetList<LineList1>("SelectLineList1ByEleIDToTal", n1);
           
            
            //for (int i = 0; i < list1.Count; i++)
            //{
            //    LineInfo line = new LineInfo();
            //    line.EleID = list1[i].LineEleID;
            //    line.SvgUID = "c5ec3bc7-9706-4cbd-9b8b-632d3606f933";
            //    line = (LineInfo)Services.BaseService.GetObject("SelectLineInfoByEleID", line);
            //    if (line != null)
            //    {
            //        list1[i].col1 = line.ObligateField6;
            //        list1[i].col2 = line.ObligateField7;
            //    }
            //}
            gridControl.DataSource = list1;
            gridControl.RefreshDataSource();
        }
        
        protected override void Print()
        {
            ComponentPrint.ShowPreview(this.gridControl, "评分结果");
        }

        private void gridControl_DoubleClick(object sender, EventArgs e)
        {       if (FocusedObject==null) return;

              PSP_SubstationSelect p = new PSP_SubstationSelect();
              p.UID = FocusedObject.SubStationID;
              p=(PSP_SubstationSelect)Services.BaseService.GetObject("SelectPSP_SubstationSelectByKey",p);  
              frmSubstationProperty frmSub = new frmSubstationProperty();
              frmSub.InitData(p.EleID, "c5ec3bc7-9706-4cbd-9b8b-632d3606f933", "", "");
              frmSub.IsReadonly = true;
              frmSub.ShowDialog();
        }
        #region 公共属性
        /// <summary>
        /// 获取或设置"双击允许修改"标志
        /// </summary>
        //public bool AllowUpdate
        //{
        //    get { return _bAllowUpdate; }
        //    set { _bAllowUpdate = value; }
        //}

        /// <summary>
        /// 获取GridControl对象
        /// </summary>
        public GridControl GridControl
        {
            get { return gridControl; }
        }

        /// <summary>
        /// 获取GridView对象
        /// </summary>
        public GridView GridView
        {
            get { return gridView; }
        }

      
        /// <summary>
        /// 获取焦点对象，即FocusedRow
        /// </summary>
        public PSP_SubstationUserNum FocusedObject
        {
            get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as PSP_SubstationUserNum; }
        }
        #endregion

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            LoadData(lookUpEdit1.Properties.GetKeyValueByDisplayText(lookUpEdit1.Text).ToString());
           //MessageBox.Show( );
        }
    }
}