	
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

using Itop.Server.Interface.Login;
using Itop.Domain.RightManager;

namespace Itop.Server.Impl.Login {
    public class LoginAction: MarshalByRefObject, ILoginAction {
        #region ILoginAction Members

        public void Login(LoginData data, out string token, out LoginStatus status) {
            Smmuser user= SqlMapHelper.DefaultSqlMap.QueryForObject<Smmuser>("SelectSmmuserByKey", data.UserNumber);
            if (user == null)
                status = LoginStatus.InvalidUser;
            else {
                
                if (MainDataModule.Decrypt(user.Password) == data.Password)
                    status = LoginStatus.OK;
                else
                    status = LoginStatus.InvalidPassword;
            }

            token = string.Empty;
            if (status == LoginStatus.OK) {
                token = Guid.NewGuid().ToString();
                UserStateSingleton.Instance.AddUser(new UserInfo(token, data.UserNumber));
            }
        }

        public void Out(string token, string userNumber) {
            UserStateSingleton.Instance.RemoveUser(new UserInfo(token, userNumber));
        }

        public string GetUserName(UserInfo userInfo, string userNumber) {
            if (!UserStateSingleton.Instance.IsValidUser(userInfo)) {
                return string.Empty;
            }
            Smmuser user = SqlMapHelper.DefaultSqlMap.QueryForObject<Smmuser>("SelectSmmuserByKey", userInfo.Number);

            
            return user.UserName;
        }
        #endregion
    }
}
