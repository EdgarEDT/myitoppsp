using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Itop.Common;
using Itop.Server.Interface.Login;
using Itop.Client.Option;
using System.Reflection;
using Itop.Client.Base;
using Itop.Domain;
using Itop.Client.Projects;

namespace Itop.Client.Login
{
    public partial class loginsetting : FormBase
    {
        public loginsetting()
        {
            InitializeComponent();
        }

        private void sbtnOk_Click(object sender, EventArgs e)
        {
            updateConfig();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void updateConfig()
        {
            RemotingHelper.ServerAddressSys = txtServer.Text;
            RemotingHelper.ServerPortSys = txtPort.Text;
            RemotingHelper.ServerProtocolSys = txtProtocol.Text;
            SetComboData();
        }
        private void loginsetting_Load(object sender, EventArgs e)
        {
            txtServer.Text = RemotingHelper.ServerAddressSys;
            txtPort.Text = RemotingHelper.ServerPortSys;
            txtProtocol.Text = RemotingHelper.ServerProtocolSys;
            SetComboData();
            combCity.EditValue = RemotingHelper.CityName;
        }

        private void sbtnCanser_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SetComboData()
        {
            try
            {
                IList<SysDataServer> dslist = ServicesSys.BaseService.GetList<SysDataServer>("SelectSysDataServerList", "");
                combCity.Properties.Columns.Clear();
                combCity.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
                combCity.Properties.DataSource = dslist;
                combCity.Properties.DisplayMember = "CityName";
                combCity.Properties.ValueMember = "ID";
                combCity.Properties.NullText = "请选择城市";
                combCity.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CityName", "城市名称")});
            }
            catch (Exception)
            {

            }
           
        }

       
        private void combCity_EditValueChanged(object sender, EventArgs e)
        {
            RemotingHelper.CityName = combCity.EditValue.ToString();
        }
        private void btnRefreshCity_Click(object sender, EventArgs e)
        {
            updateConfig();
            try
            {
                IList<SysDataServer> dslist = ServicesSys.BaseService.GetList<SysDataServer>("SelectSysDataServerList", "");
            }
            catch (Exception)
            {

                MessageBox.Show("无法连接到服务器！请确保服务器参数正确并确认服务器已启动");
                return;
            }
            
          
        }
    }
}
