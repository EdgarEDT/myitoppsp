
using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Server.Interface.MainMenu {
    /// <summary>
    /// 菜单项目的附加数据
    /// </summary>
    [Serializable]
    public class MenuItemData {
        private int m_menuId;

        private string m_caption;

        private string m_assemblyName;

        private string m_className;

        public int MenuId {
            get { return m_menuId; }
            set { m_menuId = value; }
        }

        public string Caption {
            get { return m_caption; }
            set { m_caption = value; }
        }

        public string AssemblyName {
            get { return m_assemblyName; }
            set { m_assemblyName = value; }
        }

        public string ClassName {
            get { return m_className; }
            set { m_className = value; }
        }
    }
}
