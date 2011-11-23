
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;

namespace Itop.Client.Service {
    /// <summary>
    /// ���ط������(��ʱ����)
    /// </summary>
    public static class ServiceBus {
        private static Dictionary<Type, object> m_services = new Dictionary<Type, object>();

        /// <summary>
        /// ���ĳ������
        /// </summary>
        /// <param name="serviceName">��������</param>
        /// <returns>�������</returns>
        public static T GetService<T>() {
            Type t = typeof(T);
            return m_services.ContainsKey(t) ? (T)m_services[t] : default(T);
        }

        /// <summary> 
        /// ע�����
        /// </summary>
        /// <param name="serviceName">��������</param>
        /// <param name="service">�������</param>
        public static void RegisterService<T>( object service) {
            Type t = typeof(T);
            if (m_services.ContainsKey(t))
                m_services[t] = service;
            else
                m_services.Add(t, service);
        }
        /// <summary>
        /// ���ط��� 
        /// </summary>
        /// <param name="filename">���������ļ�</param>
        public static void LoadConfig(string filename){
            XmlDocument dom = new XmlDocument();
            try {
                dom.Load(filename);
                XmlNodeList list = dom.GetElementsByTagName("service");
                foreach (XmlNode node in list) {
                    Type t1 = getType(node.Attributes["interface"].Value);
                    Type t2 = getType(node.Attributes["compnent"].Value);
                    if (t1 == null || t2 == null) continue;
                    object service = Activator.CreateInstance(t2);
                    if (service == null) return;
                    if (m_services.ContainsKey(t1))
                        m_services[t1] = service;
                    else
                        m_services.Add(t1, service);
                }

            } catch { }
        }
        private static Type getType(string typename) {

            Type t = Type.GetType(typename);
            if (t == null) {
                try {
                    string[] array1 = typename.Split(',');
                    Assembly.Load(array1[1].Trim() + ".dll");
                    t = Type.GetType(typename);
                } catch { }
            }

            return t;
        }
    }
}
