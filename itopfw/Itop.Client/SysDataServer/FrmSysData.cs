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
       
      
        //�������
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
        //�½�����
        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SysDataServer ds = new SysDataServer();
            ds.Sort = gridView1.RowCount + 1;
            FrmSysDataAdd frm = new FrmSysDataAdd();
            frm.sds = ds;
            frm.Text = "��������";
            if (frm.ShowDialog()==DialogResult.OK)
            {
                ds = frm.sds;
                Services.BaseService.Create<SysDataServer>(ds);
                InitData();

            }
            
        }
        //�༭
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
            frm.Text = "�༭����";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ds = frm.sds;
                Services.BaseService.Update<SysDataServer>(ds);
                InitData();

            }
        }
        //ɾ��
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
            if (MessageBox.Show("ȷ��Ҫɾ������"+ds.CityName+"?","ѯ��",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                //ɾ�����ݿ����

                
                Services.BaseService.Delete<SysDataServer>(ds);
            }
        }

       
        

       

        
    }
}