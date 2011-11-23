		
using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Server.Interface.Org {
    /// <summary>
    /// 增删改组织机构操作的结果
    /// </summary>
    public enum OrgOpEnum {
        /// <summary>
        /// 调用参数为null，出现这个则是程序Bug。
        /// </summary>
        NullParam,

        /// <summary>
        /// SQL执行失败
        /// </summary>
        Failed,

        /// <summary>
        /// 执行成功
        /// </summary>
        Success,

        /// <summary>
        /// 组织机构已经存在
        /// </summary>
        OrgExists,

        /// <summary>
        /// 组织机构已经不存在
        /// </summary>
        OrgNotExists,

        /// <summary>
        /// 组织机构重名
        /// </summary>
        SameOrgName,

        /// <summary>
        /// 上级组织机构不存在
        /// </summary>
        ParentNotExists,

        /// <summary>
        /// 还有下级组织机构
        /// </summary>
        HasChildOrg,

        /// <summary>
        /// 还有直属人员
        /// </summary>
        HasUsers
    }
}
