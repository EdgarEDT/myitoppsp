using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Client.Base;
using Itop.Domain;
using System.Data.SqlClient;

namespace Itop.Client
{
    public partial class FrmSysDataAdd : FormBase
    {
        public SysDataServer sds = null;
        public FrmSysDataAdd()
        {
            InitializeComponent();
           
        }
        private void FrmSysDataAdd_Load(object sender, EventArgs e)
        {
            DataBing();
        }
        private void DataBing()
        {
            txtCityName.DataBindings.Add("EditValue", sds, "CityName");
            sCityJd.DataBindings.Add("EditValue", sds, "CityJD");
            sCityWd.DataBindings.Add("EditValue", sds, "CityWD");
            txtCityDesc.DataBindings.Add("EditValue", sds, "CityDesc");

            txtServerAddress.DataBindings.Add("EditValue", sds, "ServerAddress");
            txtServerName.DataBindings.Add("EditValue", sds, "ServerName");
            txtServerUser.DataBindings.Add("EditValue", sds, "ServerUser");
            txtServerPwd.DataBindings.Add("EditValue", sds, "ServerPwd");
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCanser_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!CheckForm())
            {
                //return false;
            }


        }
        #region 数据库部分
        private bool CheckForm()
        {
            bool result = true;
            if (txtServerAddress.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入服务器名称!");
                result = false;
            }
            if (txtServerUser.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入登录用户名称!");
                result = false;

            }
            if (txtServerPwd.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入登录密码!");
                result = false;

            }
            return result;
        }
        private void CheckData()
        {
            string connstr1 = "server=" + txtServerAddress.Text.Trim() + ";database=Master;uid=" + txtServerName.Text.Trim() + ";pwd=" + txtServerPwd.Text.Trim() + ";";
            string connstr2 = "server=" + txtServerAddress.Text.Trim() + ";database=" + txtServerName + ";uid=" + txtServerName.Text.Trim() + ";pwd=" + txtServerPwd.Text.Trim() + ";";

            if (CheckConn(connstr1))
            {
                if (CheckConn(connstr1))
                {

                }
            }
            else
            {

            }
        }
        private bool CheckConn(string connstr)
        {
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                conn.Open();
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        
       
    }
}