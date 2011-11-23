using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Client.Common
{
    public class Strings
    {
        public readonly static string ConnectServerFail= "无法连接服务器，请稍候再试！";
        public readonly static string AccessDatabaseFail = "访问数据库失败！";
        public readonly static string Exception = "程序意外终止!";

        public readonly static string OperateSucceed = "操作已成功";
        public readonly static string SaveSucceed = "保存成功";
        public readonly static string OperateFail = "操作失败";
        public readonly static string SubmitDelete = "确实要删除吗？";
        public readonly static string SubmitUpdateExchRate = "确实要更改汇率信息吗？";

        public readonly static string RecordDeleted = "该记录可能已经被其他用户删除！";
        public readonly static string NoProject = "没有加载到项目信息！";
        public readonly static string NoExchangerate = "没有加载到汇率信息！";
    }
}
