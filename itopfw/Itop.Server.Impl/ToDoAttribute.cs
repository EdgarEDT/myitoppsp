using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Server.Impl {
    /// <summary>
    /// TODO Attribute
    /// </summary>
    public class ToDoAttribute : Attribute {
        private string m_info;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="info">��ע��Ϣ</param>
        public ToDoAttribute(string info) {
            m_info = info;
        }
    }
}
