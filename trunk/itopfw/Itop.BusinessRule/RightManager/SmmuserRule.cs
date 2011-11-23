using System;
using System.Collections.Generic;
using System.Text;
using Itop.Server.Interface;
using Itop.Domain.RightManager;
using Itop.Common;

namespace Itop.BusinessRule.RightManager {
    public partial class SmmuserRule {

        /// <summary>
        /// ����û��Ƿ�Ϸ�
        /// </summary>
        /// <param name="smmuser">�û�</param>
        /// <param name="strErr">������Ϣ</param>
        /// <param name="isnew">�Ƿ��¼�¼</param>
        /// <returns>True�Ϸ�</returns>
        public static bool Check(Smmuser smmuser,ref string strErr,bool isnew) {
            if (string.IsNullOrEmpty(smmuser.Userid)) {
                strErr = "�û��Ų���Ϊ�գ�";
                return false;
            }
            if (string.IsNullOrEmpty(smmuser.UserName)) {
                strErr = "�û�������Ϊ�գ�";
                return false;
            }
            //����������¼,����û����Ƿ����
            if (isnew) {
                IBaseService service = RemotingHelper.GetRemotingService<IBaseService>();
                Smmuser user1 = service.GetOneByKey<Smmuser>(smmuser.Userid);
                if (user1 != null) {
                    strErr = "�Ѿ������û���Ϊ[" + smmuser.Userid + "]���û�.";
                    return false;
                }
            }
            
            return true;
        }
    }
}
