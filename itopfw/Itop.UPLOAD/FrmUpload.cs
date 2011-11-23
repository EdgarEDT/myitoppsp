using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Common;
using System.Collections;
using Itop.Domain.Update;
using System.IO;

namespace Itop.UPLOAD {
    public partial class FrmUpload : Form {
        public FrmUpload() {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);


           
        }
        private void updateConfig() {
            using (Itop.UPDATE.FrmSetup dlg = new Itop.UPDATE.FrmSetup()) {
                dlg.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            string appPath = Application.StartupPath;
            if (dlg.ShowDialog() == DialogResult.OK) {
                BindingList<SAppUpdate> list = new BindingList<SAppUpdate>();
                foreach(string filename in dlg.FileNames){
                    FileInfo fi = new FileInfo(filename);
                    SAppUpdate obj = new SAppUpdate();
                    obj.FILESIZE = fi.Length;
                    obj.FILENAME = fi.Name;
                    obj.RQ = fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                    string dir =fi.FullName;
                    dir = dir.ToLower().Replace(appPath.ToLower(),".");
                    obj.FILEPATH = dir;

                    list.Add(obj);

                }
                this.dataGridView1.DataSource = list;
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            IList list = this.dataGridView1.DataSource as IList;

            if (list != null) {

                StringBuilder strbuilder = new StringBuilder();

                foreach (SAppUpdate obj in list) {
                    this.label1.Text = obj.FILENAME;
                    object obj2 = Services.BaseService.GetObject("SelectSAppUpdateCountByPath", obj);
                    if (obj2 != null) {
                        FileInfo fi = new FileInfo(obj.FILEPATH);
                        string fdate = fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");

                        if (fdate.CompareTo(obj.RQ)<=0) continue;
                        obj.RQ = fdate;
                        using (FileStream fs = File.OpenRead(obj.FILENAME)) {
                            obj.FILEBLOB = new byte[fs.Length];
                            obj.FILESIZE = fs.Length;
                            fs.Read(obj.FILEBLOB, 0, (int)fs.Length);
                            fs.Close();
                        }
                        Services.BaseService.Update<SAppUpdate>(obj);
                        strbuilder.AppendLine(obj.FILEPATH);

                    } else {

                        using (FileStream fs = File.OpenRead(obj.FILENAME)) {
                            obj.FILEBLOB = new byte[fs.Length];
                            obj.FILESIZE = fs.Length;
                            fs.Read(obj.FILEBLOB, 0, (int)fs.Length);
                            fs.Close();
                        }
                        Services.BaseService.Create<SAppUpdate>(obj);
                        strbuilder.AppendLine(obj.FILEPATH);
                    }
                }

                Services.BaseService.Update("UpdateSAppLastDate", null);
                MessageBox.Show("更新列表\n" + strbuilder.ToString(), "更新列表");

            }
        }

        private void button3_Click(object sender, EventArgs e) {
            Retrive();
         }
        private void Retrive() {
            try {
                IList<SAppUpdate> list = Services.BaseService.GetStrongList<SAppUpdate>();

                BindingList<SAppUpdate> list2 = new BindingList<SAppUpdate>(list);
                this.dataGridView1.DataSource = list2;
            } catch {
                updateConfig();
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            DataGridViewRow row = this.dataGridView1.CurrentRow;
            if (row != null) {
                Services.BaseService.DeleteByKey<SAppUpdate>(row.Cells["UID"].Value.ToString()); ;

                this.dataGridView1.Rows.Remove(row);
            }
        }

        private void btSaveConfig_Click(object sender, EventArgs e) {
            updateConfig();
        }

        private void FrmUpload_Load(object sender, EventArgs e) {
            Retrive();
        }
       

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            e.Cancel = true;
        }
    }
}