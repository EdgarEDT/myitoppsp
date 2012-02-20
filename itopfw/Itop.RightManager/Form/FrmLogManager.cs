using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Server.Interface;
using Itop.Common;
using Itop.Domain.RightManager;
using System.Collections;

namespace Itop.RightManager.UI
{
    public partial class FrmLogManager : Itop.Client.Base.FormBase
    {

        #region 变量，属性

        private IBaseService sysService;
        private IList list;

        public IBaseService SysService
        {
            get
            {
                if (sysService == null)
                {
                    sysService = RemotingHelper.GetRemotingService<IBaseService>();
                }
                if (sysService == null) MsgBox.Show("IBaseService服务没有注册");
                return sysService;
            }
        }
        #endregion

        public FrmLogManager()
        {
            InitializeComponent();
        }

        public bool Execute()
        {
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            ShowDialog();
            return true;
        }

        private void FrmLogManager_Load(object sender, EventArgs e)
        {
            this.dataGridView.DefaultCellStyle.BackColor = this.BackColor;
            this.dataGridView.RowHeadersDefaultCellStyle.BackColor = this.BackColor;
            this.dataGridView.ColumnHeadersDefaultCellStyle.BackColor = this.BackColor;
            this.dataGridView.GridColor = System.Drawing.Color.White;
            RefData();


            
        }



        private void RefData()
        {
            list = SysService.GetList<SMMLOG>();
            dataGridView.DataSource = null;
            dataGridView.DataSource = list;

            try
            {
                dataGridView.Columns["UID"].Visible = false;
                dataGridView.Columns["CZSTATE"].Visible = false;
                dataGridView.Columns["CZPROG"].Visible = false;

                dataGridView.Columns["RQ"].HeaderText = "日期";
                dataGridView.Columns["USERID"].HeaderText = "操作用户";
                dataGridView.Columns["CZNOTES"].HeaderText = "操作过程";
                dataGridView.Columns["CZCOMPUTE"].HeaderText = "计算机名";
                dataGridView.Columns["CZIP"].HeaderText = "IP";

                dataGridView.Columns["RQ"].DisplayIndex = 0;
                dataGridView.Columns["USERID"].DisplayIndex = 1;
                dataGridView.Columns["CZNOTES"].DisplayIndex = 2;
                dataGridView.Columns["CZCOMPUTE"].DisplayIndex = 3;
                dataGridView.Columns["CZIP"].DisplayIndex = 4;

                dataGridView.Columns["RQ"].ReadOnly = true;
                dataGridView.Columns["USERID"].ReadOnly = true;
                dataGridView.Columns["CZNOTES"].ReadOnly = true;
                dataGridView.Columns["CZCOMPUTE"].ReadOnly = true;
                dataGridView.Columns["CZIP"].ReadOnly = true;


                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch { }

        }

       

        private void toolRes_Click(object sender, EventArgs e)
        {
            RefData();
        }

        private void toolClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (dataGridView.CurrentRow != null)
            {
                string uid = dataGridView.CurrentRow.Cells["UID"].Value.ToString();

                if (MsgBox.ShowYesNo("你确定要删除么？") == DialogResult.Yes)
                {
                    SysService.DeleteByKey<SMMLOG>(uid);

                    //list.RemoveAt(dataGridView.CurrentRow.Index);
                    RefData();

                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (MsgBox.ShowYesNo("你确定要删除么？") == DialogResult.Yes)
                {
                    SMMLOG sm = new SMMLOG();
                    SysService.Delete<SMMLOG>(sm);
                    list.Clear();
                    RefData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MsgBox.Show("删除失败");
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }
}