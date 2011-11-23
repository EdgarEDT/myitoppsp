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
            RemotingHelper.ServerAddress = txtServer.Text;
            RemotingHelper.ServerPort = txtPort.Text;
            RemotingHelper.ServerProtocol = txtProtocol.Text;
        }
        private void loginsetting_Load(object sender, EventArgs e)
        {
            txtServer.Text = RemotingHelper.ServerAddress;
            txtPort.Text = RemotingHelper.ServerPort;
            txtProtocol.Text = RemotingHelper.ServerProtocol;
        }

        private void sbtnCanser_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
