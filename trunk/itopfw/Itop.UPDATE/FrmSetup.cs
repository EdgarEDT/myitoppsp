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

namespace Itop.UPDATE {
    public partial class FrmSetup : Form {
        public FrmSetup() {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            txtServer.Text = RemotingHelper.ServerAddress;
            txtPort.Text = RemotingHelper.ServerPort;
            txtProtocol.Text = RemotingHelper.ServerProtocol;
        }
        private void updateConfig() {
            RemotingHelper.ServerAddress = txtServer.Text;
            RemotingHelper.ServerPort = txtPort.Text;
            RemotingHelper.ServerProtocol = txtProtocol.Text;
        }

        private void btOk_Click(object sender, EventArgs e) {
            updateConfig();
            this.DialogResult = DialogResult.OK;
        }
    }
}