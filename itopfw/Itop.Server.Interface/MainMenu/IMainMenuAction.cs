				
using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using Itop.Server.Interface.Login;

namespace Itop.Server.Interface.MainMenu {
    public interface IMainMenuAction {
        DataSet GetMainMenuDataSet(UserInfo userInfo);

        /// <summary>
        /// 增加菜单项
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="parentMenuId"></param>
        /// <param name="itemData"></param>
        /// <returns></returns>
        bool AddMenuItem(UserInfo userInfo, string parentMenuId, MenuItemData itemData);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="itemData"></param>
        /// <returns></returns>
        bool EditMenuItem(UserInfo userInfo, MenuItemData itemData);

        /// <summary>
        /// 记录菜单单击次数
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="assemblyName"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="objId"></param>
        void AddMenuClick(UserInfo userInfo, string assemblyName, 
            string className, string methodName, int objId);

        DataSet GetRecentData(UserInfo userInfo);
    }
}
