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
using System.Collections;
using System.IO;
using DevExpress.Utils;
using Itop.Client.Projects;

namespace Itop.Client
{
    public partial class FrmSysDataAdd : FormBase
    {
        public SysDataServer sds = null;
        //系统中不能删除的表名
        string[] ObjectTable = new string[] { "Smugroup", "Smmuser", "Smmproject", "Smmprog", "Smmlog", "Smmgroup", "smdgroup", "SAppProps", "glebeType", "glebeProperty", "LineType", "PS_Table_Area_TYPE", "Ps_HistoryType", "WireCategory", "PSP_Project_Sum" };

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
            sCityPYJD.DataBindings.Add("EditValue", sds, "CityPYJD");
            sCityPYWD.DataBindings.Add("EditValue", sds, "CityPYWD");
            sCityPYArea.DataBindings.Add("EditValue", sds, "CityPYArea");


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
            this.Enabled = false;
            if (CheckForm())
            {
                CheckDataCnn();
            }
            this.Enabled = true;

        }
        #region 数据库部分
        private bool CheckForm()
        {
            bool result = true;
            if (txtServerAddress.Text.Trim().Length == 0)
            {
                SQLDMOHelper.MesShow("请输入服务器名称!");
                result = false;
            }
            if (txtServerUser.Text.Trim().Length == 0)
            {
                SQLDMOHelper.MesShow("请输入登录用户名称!");
                result = false;

            }
            if (txtServerPwd.Text.Trim().Length == 0)
            {
                SQLDMOHelper.MesShow("请输入登录密码!");
                result = false;

            }
            return result;
        }
        private void  CheckDataCnn()
        {

            string connstr1 = " Connection Timeout=2; Pooling=False; server=" + txtServerAddress.Text.Trim() + ";database=Master;uid=" + txtServerUser.Text.Trim() + ";pwd=" + txtServerPwd.Text.Trim() + ";";
            string connstr2 = "Connection Timeout=2;  Pooling=False;server=" + txtServerAddress.Text.Trim() + ";database=" + txtServerName.Text.Trim() + ";uid=" + txtServerUser.Text.Trim() + ";pwd=" + txtServerPwd.Text.Trim() + ";";
            if (!CheckForm())
            {
                return;
            }

            if (CheckConn(connstr1))
            {
                if (CheckConn(connstr2))
                {
                    SQLDMOHelper.MesShow("数据库连接畅通!");
                }
                else
                {
                    SQLDMOHelper.MesShow(txtServerName.Text.Trim() + "此数据库不能登录!");
                }
            }
            else
            {
                SQLDMOHelper.MesShow("无法连接到服务器，请确认服务器信息!");
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
            finally
            {
                conn.Dispose();
                GC.Collect();
               
            }
        }
        #endregion
        /// <summary>
        ///  创建数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateData_Click(object sender, EventArgs e)
        {
            if (!CheckForm())
            {
                return;
            }

            string connstr1 = " Connection Timeout=2; Pooling=False ;server=" + txtServerAddress.Text.Trim() + ";database=Master;uid=" + txtServerUser.Text.Trim() + ";pwd=" + txtServerPwd.Text.Trim() + ";";
            if (!CheckConn(connstr1))
            {
                SQLDMOHelper.MesShow("无法连接到服务器，请确认服务器信息!");
                return;
            }
            SQLDMOHelper smh = new SQLDMOHelper(txtServerAddress.Text.Trim(),txtServerUser.Text.Trim(),txtServerPwd.Text.Trim());

            ArrayList datalist = smh.GetDbList();
            if (datalist.Contains(txtServerName.Text.Trim()))
            {
                SQLDMOHelper.MesShow("该服务器中已在名为 " + txtServerName.Text.Trim() + " 的数据库");
                return;
            }
            FrmDirTree frmd = new FrmDirTree();
             SQLDMO.SQLServer svr = new SQLDMO.SQLServerClass();
             svr.Connect(txtServerAddress.Text.Trim(), txtServerUser.Text.Trim(), txtServerPwd.Text.Trim());
            frmd.svr=svr;
            frmd.Text = txtServerAddress.Text.Trim() + "选择路径";
            string FilePath="";
            if (frmd.ShowDialog()==DialogResult.OK)
            {
                FilePath = frmd.SelectPaht;
            }
            else
            {
                return;
            }
            WaitDialogForm frm = new WaitDialogForm("正在创建数据库，请稍后...");
            frm.Show();
            
            if (smh.CreateDB(txtServerName.Text.Trim(), FilePath))
            {
                //读取配置数据表.sql文件
       
                IList<SysDataFiles> sdflist = ServicesSys.BaseService.GetList<SysDataFiles>("SelectSysDataFilesList", "");
                if (sdflist.Count==0)
                {
                    frm.Hide();
                    SQLDMOHelper.MesShow("服务器中创建数据库文件不存在，请管管员先添加该文件！ ");
                    return;
                }
                SysDataFiles file = sdflist[0];
                string path = Application.StartupPath + "\\BlogData";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = path + "\\" + file.FileName;
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                getfile(file.Files, filepath);


                ArrayList alist = smh.GetSqlFile(filepath, txtServerName.Text.Trim());
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                string connstr2 = "Pooling=False ; server=" + txtServerAddress.Text.Trim() + ";database=" + txtServerName.Text.Trim() + ";uid=" + txtServerUser.Text.Trim() + ";pwd=" + txtServerPwd.Text.Trim() + ";";
                SqlConnection conn = new SqlConnection(connstr2);
                frm.Caption = "正在创建数据表，请稍后...";
                if (smh.ExecuteCommand(alist,conn))
                {
                    frm.Caption = "正在初始化数据，请稍后...";

                    //添加数据
                    if (CopyData(smh))
                    {
                        frm.Hide();
                        SQLDMOHelper.MesShow("数据库 " + txtServerName.Text.Trim() + " 已成功创建");
                    }
                    else
                    {
                        frm.Hide();
                        SQLDMOHelper.MesShow("数据库 " + txtServerName.Text.Trim() + " 已成功创建,初始化数据败");
                    }
                    
                }
                else
                {
                    frm.Hide();
                    SQLDMOHelper.MesShow("创建数据库表失败，请检查服务器中创建数据库文件是否损坏");
                }
            }
            frm.Hide();
            
        }
        //从主库复制数据
        public bool CopyData(SQLDMOHelper smh)
        {
            bool result = false;
            string SysServerAddress=ServicesSys.GetServerAddress;
            string SysServerName=ServicesSys.GetServerName;
            string SysUid=ServicesSys.GetUid;
            string SysPwd=ServicesSys.GetPwd;
            string connstr2 = "Pooling=False ; server=" + txtServerAddress.Text.Trim() + ";database=" + txtServerName.Text.Trim() + ";uid=" + txtServerUser.Text.Trim() + ";pwd=" + txtServerPwd.Text.Trim() + ";";
            SqlConnection conn = new SqlConnection(connstr2);
            ArrayList list=new ArrayList ();

            for (int i = 0; i < ObjectTable.Length; i++)
			{
                string insertsql = "insert  " + ObjectTable[i] + " select *  from openrowset( 'SQLOLEDB ', '" + SysServerAddress + "'; '" + SysUid + "'; '" + SysPwd + "'," + SysServerName + ".dbo." + ObjectTable[i] + ") ";
                list.Add(insertsql);
			}
            smh.StartSweet();
            if (smh.ExecuteCommand(list, conn))
            {
                smh.CloseSweet();
                result = true;
                
            }
            return result;
            

        }
        public  void getfile(byte[] bt, string filename)
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
        /// <summary>
        /// 删除数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelData_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除当前数据库吗？（不可恢复，请慎重操作）", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            WaitDialogForm frm = new WaitDialogForm("", "正在删除数据库，请稍后...");
            frm.Show();
            string connstr1 = " Connection Timeout=2; server=" + txtServerAddress.Text.Trim() + ";database=Master;uid=" + txtServerUser.Text.Trim() + ";pwd=" + txtServerPwd.Text.Trim() + ";";
            if (!CheckConn(connstr1))
            {
                frm.Hide();
                SQLDMOHelper.MesShow("无法连接到服务器，请确认服务器信息!");
                return;
            }
            SQLDMOHelper smh = new SQLDMOHelper(txtServerAddress.Text.Trim(), txtServerUser.Text.Trim(), txtServerPwd.Text.Trim());
            ArrayList datalist = smh.GetDbList();
            if (datalist.Contains(txtServerName.Text.Trim()))
            {
                if (smh.KillDB(txtServerName.Text.Trim()))
                {
                    frm.Hide();
                    SQLDMOHelper.MesShow("已成功删除 " + txtServerName.Text.Trim() + " 数据库");
                }

            }
            else
            {
                frm.Hide();
                SQLDMOHelper.MesShow("该服务器中不存在名为 " + txtServerName.Text.Trim() + " 的数据库");
            }
            frm.Hide();
        }

     
      
       
       
    }
}