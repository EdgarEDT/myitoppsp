using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using Itop.Domain;

namespace Itop.Server.Interface {
    /// <summary>
    /// ������������
    /// </summary>
    public interface IConfigService {
        /// <summary>
        /// ��ȡ���ݿ��������ö���
        /// </summary>
        /// <returns></returns>
        DataConfig GetDataConfig();
    }
}
