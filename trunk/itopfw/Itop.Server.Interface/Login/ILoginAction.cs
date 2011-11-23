		
using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Itop.Server.Interface.Login {
    /// <summary>
    /// 登录接口
    /// </summary>
    public interface ILoginAction {
        void Login(LoginData data, out string token, out LoginStatus status);

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="token">登录令牌</param>
        /// <param name="userNumber">工号</param>
        void Out(string token, string userNumber);

        /// <summary>
        /// 根据工号获得用户名
        /// </summary>
        /// <param name="userInfo">用户登录信息</param>
        /// <param name="userNumber">工号</param>
        /// <returns>用户名</returns>
        string GetUserName(UserInfo userInfo, string userNumber);
    }
}
