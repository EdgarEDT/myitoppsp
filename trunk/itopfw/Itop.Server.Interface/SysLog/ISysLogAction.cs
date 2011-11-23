		
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Itop.Server.Interface.Login;

namespace Itop.Server.Interface.SysLog {
    public interface ISysLogAction {
        void WriteLog(UserInfo userInfo, string moduleName, string info);

        DataSet GetLogData(UserInfo userInfo);
    }
}
