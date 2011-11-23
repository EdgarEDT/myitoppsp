	
using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Itop.Server.Interface.Login;

namespace Itop.Server.Interface.Rights {
    /// <summary>
    /// 权限
    /// </summary>
    public interface IRightsAction {
        /// <summary>
        /// 获得需要设置权限的用户列表
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        DataSet GetPersonList(UserInfo userInfo);

        /// <summary>
        /// 是否具有某个权限
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="rightId">权限Id</param>
        /// <returns>true：有该权限，false：无</returns>
        bool HasRight(UserInfo userInfo, int rightId);

        /// <summary>
        /// 是否具有某个权限
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="actionId">Action Id</param>
        /// <returns>true：有该权限，false：无</returns>
        bool HasRight(UserInfo userInfo, string actionId);

        /// <summary>
        /// 获得人员的权限
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="userId">人员Id</param>
        /// <returns>DataSet</returns>
        DataSet GetPersonRights(UserInfo userInfo, int userId);

        /// <summary>
        /// 获得某个人员不具备的权限
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="userId">人员Id</param>
        /// <returns>DataSet</returns>
        DataSet GetRightsPersonNotHas(UserInfo userInfo, int userId);

        /// <summary>
        /// 增加权限
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="userId">人员Id</param>
        /// <param name="rightId">权限Id</param>
        void AddRight(UserInfo userInfo, int userId, int rightId);

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="userId">人员Id</param>
        /// <param name="rightId">权限Id</param>
        void DeleteRight(UserInfo userInfo, int userId, int rightId);
    }
}
