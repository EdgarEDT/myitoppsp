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
using System.Data.SqlClient;
using System.IO;
namespace Itop.Client
{
    public partial class FrmSysDataFiles : Itop.Client.Base.FormBase
    {
        public FrmSysDataFiles() {
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

        private void FrmSysDataFiles_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            IList<SysDataFiles> sdflist = ServicesSys.BaseService.GetList<SysDataFiles>("SelectSysDataFilesList", "");
            gridControl1.DataSource = sdflist;
            if (sdflist.Count>0)
            {
                barAdd.Enabled = false;
                barEdit.Enabled = true;
                barSave.Enabled = true;
            }
            else
            {
                barAdd.Enabled = true;
                barEdit.Enabled = false;
                barSave.Enabled = false;
            }
            
        }

        private void barAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SysDataFiles sdf=new SysDataFiles ();
            sdf.FileDesc="���ڴ����������ݿ���ļ�";
            FrmSysDataFileAdd frm = new FrmSysDataFileAdd();
            frm.file = sdf;
            frm.Text = "��������ļ�";
            if (frm.ShowDialog()==DialogResult.OK)
            {
                try
                {
                    ServicesSys.BaseService.Create<SysDataFiles>(frm.file);
                }
                catch (Exception)
                {
                    
                    throw;
                }
                InitData();
            }
        }

        private void barEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.RowCount < 1)
            {
                return;
            }
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }
            SysDataFiles ds = gridView1.GetRow(gridView1.FocusedRowHandle) as SysDataFiles;
            FrmSysDataFileAdd frm = new FrmSysDataFileAdd();
            frm.file = ds;
            frm.Text = "�༭�����ļ�";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                ds = frm.file;
                ServicesSys.BaseService.Update<FrmSysDataFileAdd>(ds);
                InitData();

            }
        }
        //���������ļ�
        private void barSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog frm = new SaveFileDialog();
            frm.Filter = "sql�ļ�(*.sql)|*.sql";
            string filePath;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                filePath = frm.FileName;
                SysDataFiles file = gridView1.GetRow(gridView1.FocusedRowHandle) as SysDataFiles;
                try
                {
                    getfile(file.Files, filePath);
                    MessageBox.Show("�ѳɹ����������ļ���");
                }
                catch (Exception)
                {

                    MessageBox.Show("���������ļ�ʧ�ܣ�");
                }
                
            }
        }
        public void getfile(byte[] bt, string filename)
        {
            BinaryWriter bw;
            FileStream fs;
            try
            {
                fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
                bw = new BinaryWriter(fs);
                bw.Write(bt);
                bw.Flush();
                bw.Close();
                fs.Close();

            }
            catch
            {

            }

        }
       
        

       

        
    }
}