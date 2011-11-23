using System;
using System.Collections.Generic;
using System.Text;
using Itop.Server.Interface;
using Itop.Domain.RightManager;
using Itop.Common;

namespace Itop.BusinessRule.RightManager {
    public partial class SmmgroupRule {

        /// <summary>
        /// 检查组是否合法
        /// </summary>
        /// <param name="smmuser">组</param>
        /// <param name="strErr">错误信息</param>
        /// <param name="isnew">是否新记录</param>
        /// <returns>True合法</returns>
        public static bool Check(Smmgroup data, ref string strErr, bool isnew) {
            if (string.IsNullOrEmpty(data.Groupno)) {
                strErr = "组号不能为空！";
                return false;
            }
            if (string.IsNullOrEmpty(data.Groupname)) {
                strErr = "组名不能为空！";
                return false;
            }
            //如是新增组,检查组号是否存在
            if (isnew) {
                IBaseService service = RemotingHelper.GetRemotingService<IBaseService>();
                Smmgroup group1 = service.GetOneByKey<Smmgroup>(data.Groupno);
                if (group1 != null) {
                    strErr = "已经存在组号为[" + data.Groupno + "]的组.";
                    return false;
                }
            }
            
            return true;
        }
    }
}
