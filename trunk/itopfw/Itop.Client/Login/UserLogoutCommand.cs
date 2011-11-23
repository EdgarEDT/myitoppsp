
using System;
using System.Collections.Generic;
using System.Text;

using Itop.Server.Interface.Login;
using Itop.Server.Interface.MainMenu;
using Itop.Common;

namespace Itop.Client.Login {
    /// <summary>
    /// 注销用户登录
    /// </summary>
    public static class UserLogoutCommand {
        static public bool Execute() {
            return Exec(true);
        }

        static public bool Exec(bool closeMainForm) {
            if (closeMainForm)
                MIS.MainForm.Close();
            else {
                ILoginAction loginAction = RemotingHelper.GetRemotingService<ILoginAction>();
                if (loginAction == null)
                    return false;

                try {
                    //MIS.WriteApplicationLog("系统", "退出系统");
                    //IMainMenuAction m = RemotingHelper.GetRemotingService<IMainMenuAction>();
                    //m.AddMenuClick(MIS.UserInfo, "Itop.Client.dll", "Itop.Client.Login.UserLogoutCommand", "Execute", -1);
                    loginAction.Out(MIS.Token, MIS.UserNumber);
                    MIS.Token = string.Empty;
                } catch (System.Net.Sockets.SocketException ex) {
                    MIS.WriteExceptionLog(ex.Message);
                    return false;
                }
            }

            return true;
        }
    }
}
