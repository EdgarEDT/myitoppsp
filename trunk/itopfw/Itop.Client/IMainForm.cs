			
using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Client {
    /// <summary>
    /// ������ӿ�
    /// </summary>
    public interface IMainForm {
        /// <summary>
        /// �����������״̬����Ϣ
        /// </summary>
        void SetStatusLabel();

        /// <summary>
        /// ���´������˵�
        /// </summary>
        void RefreshMainMenu();

        /// <summary>
        /// ���³��ù���
        /// </summary>
        void RefreshRecentMenu();
    }
}
