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
        /// 构造函数
        /// </summary>
        /// <param name="info">备注信息</param>
        public ToDoAttribute(string info) {
            m_info = info;
        }
    }
}
