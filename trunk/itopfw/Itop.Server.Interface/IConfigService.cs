using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using Itop.Domain;

namespace Itop.Server.Interface {
    /// <summary>
    /// 服务器端配置
    /// </summary>
    public interface IConfigService {
        /// <summary>
        /// 获取数据库连接配置对象
        /// </summary>
        /// <returns></returns>
        DataConfig GetDataConfig();
    }
}
