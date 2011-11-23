using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Server.Interface.ObjProp {
    /// <summary>
    /// 属性树
    /// </summary>
    [Serializable]
    public class PropertyTree {
        private List<PropertyTree> m_chilren = new List<PropertyTree>();

        private Dictionary<int, string> m_this = new Dictionary<int, string>();

        public List<PropertyTree> Children {
            get { return m_chilren; }
        }

        public string this[int id] {
            get { return m_this.ContainsKey(id) ? m_this[id] : string.Empty; }
            set {
                if (m_this.ContainsKey(id))
                    m_this[id] = value;
                else
                    m_this.Add(id, value);
            }
        }

        public Dictionary<int, string> Props {
            get { return m_this; }
        }
    }
}
