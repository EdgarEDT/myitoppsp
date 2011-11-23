
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Itop.Server.Interface.Login;

namespace Itop.Server.Interface.Forms {
    /// <summary>
    /// ��Action
    /// </summary>
    public interface IFormsAction {
        /// <summary>
        /// ���ģ���Ԫ����
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>DataSet</returns>
        DataSet GetModuleList(UserInfo userInfo);

        /// <summary>
        /// ��õ������ݵ�Ԫ����
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        DataSet GetSingleMetaData(UserInfo userInfo, string name);

        /// <summary>
        /// ���Single MasterԪ����
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="objId"></param>
        /// <returns></returns>
        DataSet GetSingleMasterMetaData(UserInfo userInfo, int objId);

        /// <summary>
        /// ���Single�����ʵ������
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        DataSet GetDataSetFromSingle(UserInfo userInfo, string name);

        /// <summary>
        /// ���TabControl������Ԫ����
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="tabId"></param>
        /// <returns></returns>
        DataSet GetTabContainerData(UserInfo userInfo, string name);

        /// <summary>
        /// �Զ������洢����
        /// </summary>
        /// <param name="userInfo"></param>
        void CreateStoredProc(UserInfo userInfo);

        /// <summary>
        /// ����(��ɾ��)Single���������
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="data"></param>
        /// <param name="objId"></param>
        /// <returns></returns>
        DataSet ModifySingle(UserInfo userInfo, DataSet data, int objId);
    }
}
