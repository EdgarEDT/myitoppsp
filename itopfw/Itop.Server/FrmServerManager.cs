using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using Itop.Server.Impl;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels.Http;
using System.Threading;
using System.Diagnostics;

namespace Itop.Server {
    public partial class FrmServerManager : Form{
        public FrmServerManager() {
            InitializeComponent();
        }
        protected override void OnClosing(CancelEventArgs e) {
            if (base.Visible) {
                e.Cancel = true;
                base.Hide();
                this.notifyIcon1.Visible = true;
            }
            base.OnClosing(e);
        }
        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            base.Hide();
            this.notifyIcon1.Visible = true;
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            this.cbbProtocol.Text = Settings.RemotingProtocol;
            this.txtPort.Text = Settings.RemotingPort;
            this.txtPwd.Text = Settings.Pwd;
            this.txtUid.Text = Settings.Uid;
            this.txtDatabase.Text = Settings.Database;
            this.txtDataServer.Text = Settings.DataServer;
            Start();            
        }
        

        private void Start() {
            string fileName = "Itop.Server.exe.config";
            try {
                RemotingConfiguration.Configure(fileName, false);
                Program.Log.Info("�����������");
                Program.Log.Info(Settings.RemotingProtocol + " �����ڶ˿� [" + Settings.RemotingPort + "] ���м���.");

            } catch(Exception e) {
                Program.Log.Error("Remoting��������ʧ��.", e);
            }
        }
        private void Stop() {
            //��õ�ǰ��ע���ͨ����
            IChannel[] channels = ChannelServices.RegisteredChannels;

            //�ر�ͨ����
            foreach (IChannel eachChannel in channels) {  
              
                if (eachChannel.ChannelName.ToLower() == "tcp") {
                    TcpChannel tcpChannel = (TcpChannel)eachChannel;

                    //�رռ�����
                    tcpChannel.StopListening(null);

                    //ע��ͨ����
                    ChannelServices.UnregisterChannel(tcpChannel);
                } else if (eachChannel.ChannelName.ToLower() == "http") {
                    HttpChannel httpChannel = (HttpChannel)eachChannel;
                    //�رռ�����
                    httpChannel.StopListening(null);
                    //ע��ͨ����
                    ChannelServices.UnregisterChannel(httpChannel);
                }
            }           
        }

        private void Restart() {
            Program.Log.Info("����ʼ����");
            Application.Exit();
            Application.DoEvents();
            Application.Restart();
        }
        private void Exit() {
            if (MessageBox.Show("�Ƿ�ر�"+this.Text+"? " , "ItopServer", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
                Stop();
                Program.Log.Info("������ֹͣ");
                Application.Exit();
            }

        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) {
            base.Show();
            this.notifyIcon1.Visible = false;
            if (base.WindowState == FormWindowState.Minimized) {
                base.WindowState = FormWindowState.Normal;
            }
        }

  
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            switch (e.ClickedItem.Name) {
                case "mShow":
                    notifyIcon1_MouseDoubleClick(sender, null);
                    break;
                case "mRestart":
                    Restart();                    
                    break;
                case "mExit":
                    Exit();
                    break;
                default:
                    break;
            }
        }

        private void btExit_Click(object sender, EventArgs e) {
            Exit();
        }

        private void btSave_Click(object sender, EventArgs e) {
            
            Settings.RemotingProtocol = this.cbbProtocol.Text;
            Settings.RemotingPort = this.txtPort.Text;
            Settings.Pwd = this.txtPwd.Text;
            Settings.Uid = this.txtUid.Text;
            Settings.Database = this.txtDatabase.Text;
            Settings.DataServer = this.txtDataServer.Text;
            //��������
         
            Settings.Save();
      
            SqlMapHelper.Reset();
        }

        private void btRestart_Click(object sender, EventArgs e) {
            Restart();
        }

        private void btHide_Click(object sender, EventArgs e) {
            Close();
        }
    }
}