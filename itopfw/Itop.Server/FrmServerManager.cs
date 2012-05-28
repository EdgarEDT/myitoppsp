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
            if (Settings.IsOneServer == "two")
            {
                this.notifyIcon1.Visible = false;
            }
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
                Program.Log.Info("服务启动完毕");
                Program.Log.Info(Settings.RemotingProtocol + " 服务在端口 [" + Settings.RemotingPort + "] 进行监听.");

            } catch(Exception e) {
                Program.Log.Error("Remoting服务启动失败.", e);
            }
        }
        private void Stop() {
            //获得当前已注册的通道；
            IChannel[] channels = ChannelServices.RegisteredChannels;

            //关闭通道；
            foreach (IChannel eachChannel in channels) {  
              
                if (eachChannel.ChannelName.ToLower() == "tcp") {
                    TcpChannel tcpChannel = (TcpChannel)eachChannel;

                    //关闭监听；
                    tcpChannel.StopListening(null);

                    //注销通道；
                    ChannelServices.UnregisterChannel(tcpChannel);
                } else if (eachChannel.ChannelName.ToLower() == "http") {
                    HttpChannel httpChannel = (HttpChannel)eachChannel;
                    //关闭监听；
                    httpChannel.StopListening(null);
                    //注销通道；
                    ChannelServices.UnregisterChannel(httpChannel);
                }
            }           
        }

        private void Restart() {
            Program.Log.Info("服务开始重启");
            Application.Exit();
            Application.DoEvents();
            Application.Restart();
        }
        private void Exit() {
            if (MessageBox.Show("是否关闭"+this.Text+"? " , "ItopServer", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes) {
                Stop();
                Program.Log.Info("服务已停止");
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
            //保存配置
         
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