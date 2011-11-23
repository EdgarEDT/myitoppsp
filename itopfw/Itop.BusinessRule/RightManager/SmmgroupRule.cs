using System;
using System.Collections.Generic;
using System.Text;
using Itop.Server.Interface;
using Itop.Domain.RightManager;
using Itop.Common;

namespace Itop.BusinessRule.RightManager {
    public partial class SmmgroupRule {

        /// <summary>
        /// ������Ƿ�Ϸ�
        /// </summary>
        /// <param name="smmuser">��</param>
        /// <param name="strErr">������Ϣ</param>
        /// <param name="isnew">�Ƿ��¼�¼</param>
        /// <returns>True�Ϸ�</returns>
        public static bool Check(Smmgroup data, ref string strErr, bool isnew) {
            if (string.IsNullOrEmpty(data.Groupno)) {
                strErr = "��Ų���Ϊ�գ�";
                return false;
            }
            if (string.IsNullOrEmpty(data.Groupname)) {
                strErr = "��������Ϊ�գ�";
                return false;
            }
            //����������,�������Ƿ����
            if (isnew) {
                IBaseService service = RemotingHelper.GetRemotingService<IBaseService>();
                Smmgroup group1 = service.GetOneByKey<Smmgroup>(data.Groupno);
                if (group1 != null) {
                    strErr = "�Ѿ��������Ϊ[" + data.Groupno + "]����.";
                    return false;
                }
            }
            
            return true;
        }
    }
}
