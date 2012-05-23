using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Server.Interface;
using Itop.Domain.RightManager;
using Itop.Common;
using Itop.Client;
using System.Collections;
using Itop.Domain;
using Itop.Client.Projects;
namespace Itop.Client
{
    public partial class FrmSysData : Itop.Client.Base.FormBase
    {
        public FrmSysData() {
            InitializeComponent();
           
            
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            //userListView1.ShowType = ShowType.User;
        }
       
      
        //调用入口
        public bool Execute()
        {
            ShowDialog();
            return true;
        }

        private void FrmSysData_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            IList<SysDataServer> syslist = Services.BaseService.GetList<SysDataServer>("SelectSysDataServerList","");
            gridControl1.DataSource = syslist;
        }
        //新建城市
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SysDataServer ds = new SysDataServer();
            ds.Sort = gridView1.RowCount + 1;
            FrmSysDataAdd frm = new FrmSysDataAdd();
            frm.sds = ds;
            frm.Text = "新增城市";
            if (frm.ShowDialog()==DialogResult.OK)
            {
                ds = frm.sds;
                Services.BaseService.Create<SysDataServer>(ds);
                InitData();

            }
            
        }
        //编辑
        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.RowCount<1)
            {
                return;
            }
            if (gridView1.FocusedRowHandle<1)
            {
                return;
            }
            SysDataServer ds = gridView1.GetRow(gridView1.FocusedRowHandle) as SysDataServer;
            FrmSysDataAdd frm = new FrmSysDataAdd();
            frm.sds = ds;
            frm.Text = "编辑城市";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ds = frm.sds;
                Services.BaseService.Update<SysDataServer>(ds);
                InitData();

            }
        }
        //删除
        private void barDel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.RowCount < 1)
            {
                return;
            }
            if (gridView1.FocusedRowHandle < 1)
            {
                return;
            }
            SysDataServer ds = gridView1.GetRow(gridView1.FocusedRowHandle) as SysDataServer;
            if (MessageBox.Show("确定要删除城市"+ds.CityName+"?","询问",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                //删除数据库代码

                
                Services.BaseService.Delete<SysDataServer>(ds);
            }
        }

       
        

       

        
    }
}