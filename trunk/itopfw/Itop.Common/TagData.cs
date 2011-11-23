using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Common {
    [Serializable]
    public class Tagdata {
        private Dictionary<string, object> m_data;

        public Tagdata() {
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
            if (obj is Tagdata) {
                Tagdata t = obj as Tagdata;
                foreach (KeyValuePair<string, object> p in t.m_data) {
                    if (m_data[p.Key].ToString() != t[p.Key].ToString())
                        return false;

                }
                return true;
            } else
                return base.Equals(obj);
        }

        static public bool operator ==(Tagdata left, Tagdata right) {
            return left.Equals(right);
        }

        static public bool operator !=(Tagdata left, Tagdata right) {
            return !(left == right);
        }
    }
}
