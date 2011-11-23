	
using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Server.Interface.Login {
    /// <summary>
    /// 登录的状态
    /// </summary>
    public enum LoginStatus {
        /// <summary>
        /// 登录成功
        /// </summary>
        OK,
        /// <summary>
        /// 无效的用户名
        /// </summary>
        InvalidUser,
        /// <summary>
        /// 密码错误
        /// </summary>
        InvalidPassword
    }
}
