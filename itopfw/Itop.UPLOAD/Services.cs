using System;
using System.Collections.Generic;
using System.Text;
using Itop.Common;
using Itop.Server.Interface;
using System.Reflection;
using System.Windows.Forms;

namespace Itop.UPLOAD
{
    public class Services
    {
        public static IBaseService BaseService
        {
            get
            {
                return RemotingHelper.GetRemotingService<IBaseService>();
            }
        }

    }
}
