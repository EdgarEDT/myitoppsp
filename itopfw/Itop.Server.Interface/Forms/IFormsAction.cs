
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Itop.Server.Interface.Login;

namespace Itop.Server.Interface.Forms {
    /// <summary>
    /// 表单Action
    /// </summary>
    public interface IFormsAction {
        /// <summary>
        /// 获得模块的元数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>DataSet</returns>
        DataSet GetModuleList(UserInfo userInfo);

        /// <summary>
        /// 获得单行数据的元数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        DataSet GetSingleMetaData(UserInfo userInfo, string name);

        /// <summary>
        /// 获得Single Master元数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="objId"></param>
        /// <returns></returns>
        DataSet GetSingleMasterMetaData(UserInfo userInfo, int objId);

        /// <summary>
        /// 获得Single对象的实际数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        DataSet GetDataSetFromSingle(UserInfo userInfo, string name);

        /// <summary>
        /// 获得TabControl容器的元数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="tabId"></param>
        /// <returns></returns>
        DataSet GetTabContainerData(UserInfo userInfo, string name);

        /// <summary>
        /// 自动创建存储过程
        /// </summary>
        /// <param name="userInfo"></param>
        void CreateStoredProc(UserInfo userInfo);

        /// <summary>
        /// 更新(增删改)Single对象的数据
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="data"></param>
        /// <param name="objId"></param>
        /// <returns></returns>
        DataSet ModifySingle(UserInfo userInfo, DataSet data, int objId);
    }
}
