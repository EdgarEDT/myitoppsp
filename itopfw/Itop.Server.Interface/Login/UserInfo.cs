
using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Server.Interface.Login {
    [Serializable]
    public struct UserInfo {
        /// <summary>
        /// 登录令牌
        /// </summary>
        public string Token;

        /// <summary>
        /// 工号
        /// </summary>
        public string Number;

        public UserInfo(string token, string number) {
            Token = token;
            Number = number;
        }
    }
}
