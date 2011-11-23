using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Server;

namespace Itop.WinService.Manager {
    public partial class FrmSetup : Form {
        public FrmSetup() {
            InitializeComponent();
            this.cbbProtocol.Text = Settings.RemotingProtocol;
            this.txtPort.Text = Settings.RemotingPort;
            this.txtPwd.Text = Settings.Pwd;
            this.txtUid.Text = Settings.Uid;
            this.txtDatabase.Text = Settings.Database;
            this.txtDataServer.Text = Settings.DataServer;
        }

        private void FrmSetup_Load(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            Settings.RemotingProtocol = this.cbbProtocol.Text;
            Settings.RemotingPort = this.txtPort.Text;
            Settings.Pwd = this.txtPwd.Text;
            Settings.Uid = this.txtUid.Text;
            Settings.Database = this.txtDatabase.Text;
            Settings.DataServer = this.txtDataServer.Text;
            //±£¥Ê≈‰÷√
            Settings.Save();
            DialogResult = DialogResult.OK;
        }
    }
}