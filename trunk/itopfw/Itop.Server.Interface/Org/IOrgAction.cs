			
using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Itop.Server.Interface.Login;
using Itop.Server.Interface.Tree;

namespace Itop.Server.Interface.Org {
    /// <summary>
    /// 组织机构接口
    /// </summary>
    public interface IOrgAction {
        /// <summary>
        ///  获得当前的组织机构的数据
        /// </summary>
        /// <param name="userInfo">用户登录信息</param>
        /// <returns>组织机构数据</returns>
        DataSet GetOrgData(UserInfo userInfo);

        OrgOpEnum AddOrg(UserInfo userInfo,int parentOrgId, ref TagData orgData);

        OrgOpEnum ModifyOrg(UserInfo userInfo, ref TagData orgData);

        OrgOpEnum DeleteOrg(UserInfo userInfo, int orgId);
    }
}
