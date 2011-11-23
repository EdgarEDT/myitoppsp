	
using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Itop.Server.Interface.Login;

namespace Itop.Server.Interface.DevTools {
    /// <summary>
    /// 开发辅助工具
    /// </summary>
    public interface IDevToolsAction {
        /// <summary>
        /// 获得表的文档信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        DataSet GetTableInfo(UserInfo userInfo);

        /// <summary>
        /// 获得表的字段信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        DataSet GetTableFieldsInfo(UserInfo userInfo, int tableId);

        /// <summary>
        /// 获得存储过程的文档信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        DataSet GetStoredProcInfo(UserInfo userInfo);

        /// <summary>
        /// 获得存储过程参数的文档信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="spId">存储过程Id</param>
        /// <returns></returns>
        DataSet GetStoredProcParamsInfo(UserInfo userInfo, int spId);
    }
}
