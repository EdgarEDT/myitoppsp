using System;
using System.Collections.Generic;
using System.Text;
using Itop.Common;
using Itop.Server.Interface;
using System.Reflection;
using System.Windows.Forms;

namespace Itop.Client.Common
{
    public class Services
    {
        private static IBaseService sysService;
        public static IBaseService BaseService
        {
            get {
                if (sysService == null) {
                    sysService = RemotingHelper.GetRemotingService<IBaseService>();
                }
                if (sysService == null) MsgBox.Show("IBaseService服务没有注册");
                return sysService;
            }
            set {
                sysService = value;
            }
        }

    }
}
