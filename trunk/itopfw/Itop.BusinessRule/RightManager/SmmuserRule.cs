using System;
using System.Collections.Generic;
using System.Text;
using Itop.Server.Interface;
using Itop.Domain.RightManager;
using Itop.Common;

namespace Itop.BusinessRule.RightManager {
    public partial class SmmuserRule {

        /// <summary>
        /// 检查用户是否合法
        /// </summary>
        /// <param name="smmuser">用户</param>
        /// <param name="strErr">错误信息</param>
        /// <param name="isnew">是否新记录</param>
        /// <returns>True合法</returns>
        public static bool Check(Smmuser smmuser,ref string strErr,bool isnew) {
            if (string.IsNullOrEmpty(smmuser.Userid)) {
                strErr = "用户号不能为空！";
                return false;
            }
            if (string.IsNullOrEmpty(smmuser.UserName)) {
                strErr = "用户名不能为空！";
                return false;
            }
            //如是新增记录,检查用户号是否存在
            if (isnew) {
                IBaseService service = RemotingHelper.GetRemotingService<IBaseService>();
                Smmuser user1 = service.GetOneByKey<Smmuser>(smmuser.Userid);
                if (user1 != null) {
                    strErr = "已经存在用户号为[" + smmuser.Userid + "]的用户.";
                    return false;
                }
            }
            
            return true;
        }
    }
}
