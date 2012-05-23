using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;

namespace Itop.Client
{
    public partial class FormDataSetup : FormBase
    {
        private DataSet ds = new DataSet();

        public FormDataSetup()
        {
            InitializeComponent();
            InitData();
        }

        private void InitData()
        {
            try
            {
                ds.ReadXml(Application.StartupPath + "\\CONFIG.XML");
            }
            catch
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Server", typeof(string));
                dt.Columns.Add("UserId", typeof(string));
                dt.Columns.Add("PassWord", typeof(string));
                ds.Tables.Add(dt);
            }	
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {

            if (!DBLine())
                return;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (Tserver.Text.Trim() == dr["Server"].ToString())
                {
                    MessageBox.Show("已存在的数据库连接！", "提示！", MessageBoxButtons.OK);
                    return;
                }

            }

            DataRow dataRow = ds.Tables[0].NewRow();
            dataRow["Server"] = Tserver.Text.Trim();
            dataRow["UserId"] = Tuserid.Text.Trim();
            dataRow["PassWord"] = Tpassword.Text.Trim();
            ds.Tables[0].Rows.Add(dataRow);
            ds.WriteXml(Application.StartupPath + "\\Config.xml");
            this.DialogResult = DialogResult.OK;
        }

        private bool DBLine()
        {
            if (!CheckForm())
            {
                return false;
            }
            string connstr = "server=" + Tserver.Text.Trim() + ";database=Master;uid=" + Tuserid.Text.Trim() + ";pwd=" + Tpassword.Text.Trim() + ";";
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                conn.Open();
                conn.Close();
                return true;
            }
            catch
            {
                MessageBox.Show("数据库连接失败！", "提示", MessageBoxButtons.OK);
                return false;
            }
        }




        private void FormDataSetup_Load(object sender, EventArgs e)
        {
            Tserver.DataSource = SqlLocator.GetServers();
        }



        private bool CheckForm()
        {
            if (Tserver.Text.Trim() == "")
            {
                MessageBox.Show("服务器名称未输入！", "提示！", MessageBoxButtons.OK);
                return false;
            }

            if (Tuserid.Text.Trim() == "")
            {
                MessageBox.Show("用户名未输入！", "提示！", MessageBoxButtons.OK);
                return false;
            }

            if (Tpassword.Text.Trim() == "")
            {
                MessageBox.Show("密码未输入！", "提示！", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }



    }
}