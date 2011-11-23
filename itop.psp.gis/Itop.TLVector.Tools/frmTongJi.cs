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
using System.Xml;
using ItopVector.Core.Interface.Figure;

namespace ItopVector.Tools
{
    public delegate void OnOpensub2handler(object sender, string sid);
    public partial class frmTongJi : FormModuleBase
    {
        IList<PSP_SubstationUserNum> list1 = null;
        ArrayList list2 = new ArrayList();
        public IList<PSP_SubstationMng> sellist = null;
        public event OnOpensub2handler OnOpen;
        public string KeyID = "";
        public string SUID = "";

        public frmTongJi()
        {
            InitializeComponent();
        }

        private void frmTongJi_Load(object sender, EventArgs e)
        {
            barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barDel.Caption = "删除选择的变电站";
            //barEditItem3.Visibility =DevExpress.XtraBars.BarItemVisibility.Always;
            PSP_SubstationMng m = new PSP_SubstationMng();
            sellist= Services.BaseService.GetList<PSP_SubstationMng>("SelectPSP_SubstationMngList", m);
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
            list2.Clear();
            PSP_SubstationUserNum n1=new PSP_SubstationUserNum();
            n1.col2 = uid;
            list1 = Services.BaseService.GetList<PSP_SubstationUserNum>("SelectPSP_SubstationNum2", n1);
            for (int i = 0; i < list1.Count;i++ )
            {
                
                PSP_SubstationUserNum obj = list1[i];
                PSP_SubstationUserNum temp = new PSP_SubstationUserNum();
                temp.SubStationID = list1[i].SubStationID;
                temp.col2 = uid;
                IList<PSP_SubstationUserNum> l2 = Services.BaseService.GetList<PSP_SubstationUserNum>("SelectPSP_SubstationNum1", temp);
                for (int j = 0; j < l2.Count;j++ )
                {
                    if (l2[j].col1 == "是" && l2[j].col2 == "否")
                    {
                        obj.col1 = "一票否决";
                        list2.Add(obj);
                        list1.RemoveAt(i);
                        break;
                    }
                }
            }
            //for (int i = 0; i < list1.Count; i++)
            //{
            //    list1[i].col1 = list1[i].num.ToString();
            //}
            //list1[i].col1 = list1[i].num.ToString();
            for (int i = 0; i < list2.Count;i++ )
            {
                list1.Add((PSP_SubstationUserNum)list2[i]);
            }
           
            gridControl.DataSource = list1;
            gridControl.RefreshDataSource();
        }
        public void LoadData2()
        {
            PSP_SubstationUserNum n1 = new PSP_SubstationUserNum();
            list1 = Services.BaseService.GetList<PSP_SubstationUserNum>("SelectPSP_SubstationNum2", n1);
            for (int i = 0; i < list1.Count; i++)
            {

                PSP_SubstationUserNum obj = list1[i];
                PSP_SubstationUserNum temp = new PSP_SubstationUserNum();
                temp.SubStationID = list1[i].SubStationID;
                IList<PSP_SubstationUserNum> l2 = Services.BaseService.GetList<PSP_SubstationUserNum>("SelectPSP_SubstationNum1", temp);
                for (int j = 0; j < l2.Count; j++)
                {
                    if (l2[j].col1 == "是" && l2[j].col2 == "否")
                    {
                        obj.col1 = "一票否决";
                        list2.Add(obj);
                        list1.RemoveAt(i);
                        break;
                    }
                }
            }
            for (int i = 0; i < list1.Count; i++)
            {
                list1[i].col1 = list1[i].num.ToString();
            }
            //list1[i].col1 = list1[i].num.ToString();
            for (int i = 0; i < list2.Count; i++)
            {
                list1.Add((PSP_SubstationUserNum)list2[i]);
            }

            gridControl.DataSource = list1;
            gridControl.RefreshDataSource();
        }
        protected override void Print()
        {
            ComponentPrint.ShowPreview(this.gridControl, "评分结果");
        }
        protected override void Del()
        {
            if(MessageBox.Show("是否删除选择的变电站？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes){
                KeyID = lookUpEdit1.Properties.GetKeyValueByDisplayText(lookUpEdit1.Text).ToString();
                if (FocusedObject != null)
                {
                    SUID = FocusedObject.SubStationID;
                }
                this.DialogResult = DialogResult.Ignore;
                this.Close();
            }
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

        private void gridControl_Click(object sender, EventArgs e)
        {
            if (FocusedObject == null) return;
            
            if (OnOpen != null)
            {
                OnOpen(sender, FocusedObject.userID);
            }
        }
    }
}