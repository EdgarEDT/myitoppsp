			
using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Client {
    /// <summary>
    /// 主窗体接口
    /// </summary>
    public interface IMainForm {
        /// <summary>
        /// 设置主窗体的状态条信息
        /// </summary>
        void SetStatusLabel();

        /// <summary>
        /// 重新创建主菜单
        /// </summary>
        void RefreshMainMenu();

        /// <summary>
        /// 更新常用功能
        /// </summary>
        void RefreshRecentMenu();
    }
}
