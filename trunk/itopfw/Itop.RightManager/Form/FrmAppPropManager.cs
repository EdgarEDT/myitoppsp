using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Server.Interface;
using Itop.Common;
using Itop.Domain;
using Itop.Client.Base;

namespace Itop.RightManager.UI { 
    public partial class FrmAppPropManager : FormBase {
        public FrmAppPropManager() {
            InitializeComponent();
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            RefreshData();
        }
        //调用入口
        public bool Execute()
        {
            ShowDialog();
            return true;
        }
        IBaseService service;
        //服务
        public IBaseService RecordService {
            get {
                if (service == null) {
                    service = Itop.Common.RemotingHelper.GetRemotingService<IBaseService>();            
                }
                return service; 
            }
        }
        
        //增加
        private void tsbAdd_Click(object sender, EventArgs e) {

            using (FrmAppPropEdit dlg = new FrmAppPropEdit()) {
                
                if (dlg.ShowDialog() == DialogResult.OK) {
                    
                    RecordService.Create<SAppProps>(dlg.DataObject);
                    RefreshData();
                }
            }            
        }
        //修改
        private void tsbEdit_Click(object sender, EventArgs e) {
            if (dataGridView1.CurrentRow != null) {

                int id = (int)dataGridView1.CurrentRow.Cells["propid"].Value;
                SAppProps data = RecordService.GetOneByKey<SAppProps>(id);
                using (FrmAppPropEdit dlg = new FrmAppPropEdit()) {
                    dlg.DataObject = data;
                    if (dlg.ShowDialog() == DialogResult.OK) {
                        RecordService.Update<SAppProps>(data);
                        RefreshData();
                    }
                }
            }           
        }
        //删除
        private void tsbDel_Click(object sender, EventArgs e) {
            if (dataGridView1.CurrentRow != null) {

                int id = (int)dataGridView1.CurrentRow.Cells["propid"].Value;
               
                RecordService.DeleteByKey<SAppProps>(id);
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }           

        }
        //刷新
        private void tsbRefresh_Click(object sender, EventArgs e) {
            RefreshData();
        }
        private void RefreshData() {
            DataTable table = Itop.Common.DataConverter.ToDataTable(RecordService.GetList<SAppProps>(), typeof(SAppProps));
            this.dataGridView1.DataSource = table;
            if (firstLoad)
                SetHeaderText();
        }

        bool firstLoad = true;
        private void SetHeaderText() {
            firstLoad = false;
            this.dataGridView1.Columns["propid"].HeaderText = "序号";
            this.dataGridView1.Columns["propname"].HeaderText = "参数名";
            this.dataGridView1.Columns["propvalue"].HeaderText = "参数值";
            this.dataGridView1.Columns["proptype"].HeaderText = "参数类型";
            this.dataGridView1.Columns["remark"].HeaderText = "备注";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }

        
    }
}