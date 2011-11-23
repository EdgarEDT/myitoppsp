
using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Server.Interface.Tree {
    [Serializable]
    public class TagData {
        private Dictionary<string, object> m_data;

        public TagData() {
            m_data = new Dictionary<string, object>();
        }

        public object this[string index] {
            get { return m_data[index]; }
            set {
                if (m_data.ContainsKey(index))
                    m_data[index] = value;
                else
                    m_data.Add(index, value);
            }
        }

        public override int GetHashCode() {
            return m_data.GetHashCode();
        }

        public override bool Equals(object obj) {
            if (obj == null) {
                return false;
            }
            if (obj is TagData) {
                TagData t = obj as TagData;
                foreach (KeyValuePair<string, object> p in t.m_data) {
                    if (m_data[p.Key].ToString() != t[p.Key].ToString())
                        return false;

                }
                return true;
            } else
                return base.Equals(obj);
        }

        static public bool operator ==(TagData left, TagData right) {
            return left.Equals(right);  
        }

        static public bool operator !=(TagData left, TagData right) {
            return !(left == right);
        }
    }
}
