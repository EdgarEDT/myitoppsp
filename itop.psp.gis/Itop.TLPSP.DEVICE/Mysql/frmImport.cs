using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;
using Itop.Domain.Graphics;
using DevExpress.Utils;
using System.Threading;
using Itop.Client.Base;
namespace Itop.TLPSP.DEVICE.Mysql
{
    public partial class frmImport : FormBase
    {
        MySqlConnection conn;
        MySqlDataAdapter da;
        MySqlCommandBuilder cb;
        Hashtable importTables;
        static string strServer = "rabbit";
        static string strUserid = "root";
        static string strpsw = "";
        public frmImport() {
            InitializeComponent();
            importTables = new Hashtable();
            importTables.Add("母线", "cdb_bus");
            importTables.Add("线路", "cdb_acline");
            importTables.Add("两绕组变压器", "cdb_trans_2w");
            importTables.Add("三绕组变压器", "cdb_trans_3w");
            importTables.Add("发电机", "cdb_generat a,  cpar_gen_lib b where a.gen_par=b.Par_No");
            importTables.Add("负荷", "cdb_load");
            importTables.Add("并联电容器", "cdb_pcaprea");
            foreach (string key in importTables.Keys) {
                checkedListBox1.Items.Add(key);
            }
            checkedListBox1.Enabled = false;
            server.Text = strServer;
            userid.Text = strUserid;
            password.Text = strpsw;
            this.CenterToParent();
        }
        
        protected override void OnClosed(EventArgs e) {
            base.OnClosed(e);
            strServer = server.Text;
            strUserid = userid.Text;
            strpsw = password.Text;
        }
        private void connectBtn_Click(object sender, EventArgs e) {
            if (conn != null)
                conn.Close();//charset=latin1

            string connStr = String.Format("server={0};user id={1}; password={2}; database=mysql; pooling=false;",
                server.Text, userid.Text, password.Text);

            try {
                conn = new MySqlConnection(connStr);
                conn.Open();

                GetDatabases();
            } catch (MySqlException ex) {
                MessageBox.Show("连接服务器失败: " + ex.Message);
            }
        }

        private void GetDatabases() {
            MySqlDataReader reader = null;

            MySqlCommand cmd = new MySqlCommand("SHOW DATABASES", conn);
            try {
                reader = cmd.ExecuteReader();
                databaseList.Items.Clear();
                while (reader.Read()) {
                    databaseList.Items.Add(reader.GetString(0));
                }
            } catch (MySqlException ex) {
                MessageBox.Show("获取数据库列表失败: " + ex.Message);
            } finally {
                if (reader != null) reader.Close();
            }
        }
        private void databaseList_SelectedIndexChanged(object sender, System.EventArgs e) {
            MySqlDataReader reader = null;

            conn.ChangeDatabase(databaseList.SelectedItem.ToString());

            MySqlCommand cmd = new MySqlCommand("SHOW TABLES", conn);
            try {
                reader = cmd.ExecuteReader();
                tables.Items.Clear();
                while (reader.Read()) {
                    tables.Items.Add(reader.GetString(0));
                }

            } catch (MySqlException ex) {
                MessageBox.Show("获取数据表失败: " + ex.Message);
            } finally {
                if (tables.Items.Contains("cdb_bus"))
                    checkedListBox1.Enabled = true;
                else {
                    checkedListBox1.Enabled = false;
                    checkedListBox1.SelectedItem = null;
                }
                if (reader != null) reader.Close();
            }
        }

        private void tables_SelectedIndexChanged(object sender, System.EventArgs e) {

            //dataGrid.DataSource = data;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (tables.SelectedIndex < 0) return;
            showTable(tables.SelectedItem.ToString());
            checkedListBox1.SelectedItem = null;
        }
        private void showTable(string table) {
            da = new MySqlDataAdapter("SELECT * FROM " + table, conn);
            DataTable data = new DataTable();
            cb = new MySqlCommandBuilder(da);

            da.Fill(data);
            Encoding encode = Encoding.GetEncoding("latin1");//1252
            foreach (DataRow row in data.Rows) {
                foreach (DataColumn dc in data.Columns) {
                    if (dc.DataType == typeof(string)) {
                        row[dc] = Encoding.Default.GetString(encode.GetBytes(row[dc].ToString()));
                    }
                }
            }

            data.AcceptChanges();
            gridView1.Columns.Clear();
            gridControl1.DataSource = data;
        }
        private void checkedListBox1_MouseClick(object sender, MouseEventArgs e) {
            if (checkedListBox1.SelectedItem == null) return;

            showTable(importTables[checkedListBox1.SelectedItem].ToString());
        }

        private void btImport_Click(object sender, EventArgs e) {
            //导入
            if (checkedListBox1.SelectedItem == null) return;
            IEnumerator ie = null;
            switch (checkedListBox1.SelectedItem.ToString()) {
                case "母线":
                    ie = importMX();
                    break;
                case "线路":
                    ie = importXL();
                    break;
                case "两绕组变压器":
                    ie = importBYQ2();
                    break;
                case "三绕组变压器":
                    ie = importBYQ3();
                    break;
                case "发电机":
                    ie = importFDJ();
                    break;
                case "负荷":
                    ie = importFH();
                    break;
                case "并联电容器":
                    ie = importBLDR();
                    break;
            }
            ncount = (gridControl1.DataSource as DataTable).Rows.Count;
            ncurrent = 0;
            if (ie != null) {
                WaitDialogForm msgbox = new WaitDialogForm("","导入数据，请稍候。。。");
                msgbox.Show();
                
                while (ie.MoveNext()) {
                    msgbox.SetCaption(string.Format("已完成{0}/{1}" , ncurrent , ncount));
                }
                Thread.Sleep(1000);
                msgbox.Close();
            }
        }
        int ncount;
        int ncurrent;

    }
}