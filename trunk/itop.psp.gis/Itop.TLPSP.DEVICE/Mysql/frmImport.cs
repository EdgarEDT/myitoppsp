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
            importTables.Add("ĸ��", "cdb_bus");
            importTables.Add("��·", "cdb_acline");
            importTables.Add("�������ѹ��", "cdb_trans_2w");
            importTables.Add("�������ѹ��", "cdb_trans_3w");
            importTables.Add("�����", "cdb_generat a,  cpar_gen_lib b where a.gen_par=b.Par_No");
            importTables.Add("����", "cdb_load");
            importTables.Add("����������", "cdb_pcaprea");
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
                MessageBox.Show("��ȡ���ݿ��б�ʧ��: " + ex.Message);
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
                MessageBox.Show("��ȡ���ݱ�ʧ��: " + ex.Message);
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

      
        int ncount;
        int ncurrent;

        private void connectBtn_Click_1(object sender, EventArgs e)
        {
            if (conn != null)
                conn.Close();//charset=latin1

            string connStr = String.Format("server={0};user id={1}; password={2}; database=mysql; pooling=false;",
                server.Text, userid.Text, password.Text);

            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();

                GetDatabases();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("���ӷ�����ʧ��: " + ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (tables.SelectedIndex < 0) return;
            showTable(tables.SelectedItem.ToString());
            checkedListBox1.SelectedItem = null;
        }

        private void btImport_Click_1(object sender, EventArgs e)
        {
        
            //����
            if (checkedListBox1.SelectedItem == null) return;
            IEnumerator ie = null;
            switch (checkedListBox1.SelectedItem.ToString()) {
                case "ĸ��":
                    ie = importMX();
                    break;
                case "��·":
                    ie = importXL();
                    break;
                case "�������ѹ��":
                    ie = importBYQ2();
                    break;
                case "�������ѹ��":
                    ie = importBYQ3();
                    break;
                case "�����":
                    ie = importFDJ();
                    break;
                case "����":
                    ie = importFH();
                    break;
                case "����������":
                    ie = importBLDR();
                    break;
            }
            ncount = (gridControl1.DataSource as DataTable).Rows.Count;
            ncurrent = 0;
            if (ie != null) {
                WaitDialogForm msgbox = new WaitDialogForm("","�������ݣ����Ժ򡣡���");
                msgbox.Show();
                
                while (ie.MoveNext()) {
                    msgbox.SetCaption(string.Format("�����{0}/{1}" , ncurrent , ncount));
                }
                Thread.Sleep(1000);
                msgbox.Close();
            }
       
        }

    }
}