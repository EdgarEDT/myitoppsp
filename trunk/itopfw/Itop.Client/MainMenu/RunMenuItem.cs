
using System;
using System.Collections.Generic;
using System.Text;

using Itop.Client.Login;

using Itop.Server.Interface.MainMenu;
using Itop.Common;

namespace Itop.Client.MainMenu {
    static class RunMenuItem {
        /// <summary>
        /// �˵�����
        /// </summary>
        static public void Click(string assemblyName, string className, string methodName, int objId) {
            if (string.IsNullOrEmpty(assemblyName) || string.IsNullOrEmpty(className)
                || string.IsNullOrEmpty(methodName)) {
                return;
            }

            // ���÷���
            string[] paramValue = (objId == -1) ? new string[0] : new string[] { objId.ToString() };
            bool result = (bool)MethodInvoker.Execute(assemblyName, className, methodName, paramValue);

            if (result && MIS.Token != string.Empty) {
                //��¼�˵��������
                IMainMenuAction m = RemotingHelper.GetRemotingService<IMainMenuAction>();
                m.AddMenuClick(MIS.UserInfo, assemblyName, className, methodName, objId);
                MIS.MainFormInterface.RefreshRecentMenu();
            }
        }
    }
}
