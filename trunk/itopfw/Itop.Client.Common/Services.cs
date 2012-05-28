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
                if (sysService == null) MsgBox.Show("IBaseService����û��ע��");
                return sysService;
            }
            set {
                sysService = value;
            }
        }
    }
    public class ServicesSys
    {
        private static IBaseService sysService;
        public static IBaseService BaseService
        {
            get
            {
                if (sysService == null)
                {
                    sysService = RemotingHelper.GetRemotingServiceSys<IBaseService>();
                }
                if (sysService == null) MsgBox.Show("IBaseService����û��ע��");
                return sysService;
            }
            set
            {
                sysService = value;
            }
        }
    }
}
