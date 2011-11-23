			
using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Itop.Server.Interface.Login;

namespace Itop.Server.Interface.ToDo {
    /// <summary>
    /// 待办事宜
    /// </summary>
    public interface IToDoAction {
        /// <summary>
        /// 获得待办事宜数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        DataSet GetToToList(UserInfo userInfo);
    }
}
