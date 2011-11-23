			
using System;
using System.Collections.Generic;
using System.Text;

using Itop.Server.Interface.Login;

namespace Itop.Server.Impl.Login {
    class UserStateSingleton {
        private Dictionary<string, UserInfo> m_data = new Dictionary<string, UserInfo>();
        private UserStateSingleton() { }

        static private UserStateSingleton m_instance = new UserStateSingleton();

        static public UserStateSingleton Instance {
            get { return m_instance; }
        }

        public void AddUser(UserInfo userInfo) {
            lock (m_data) {
                if (!m_data.ContainsKey(userInfo.Token))
                    m_data.Add(userInfo.Token, userInfo);
            }
        }

        public void RemoveUser(UserInfo userInfo) {
            lock (m_data) {
                if (m_data.ContainsKey(userInfo.Token)) {
                    m_data.Remove(userInfo.Token);
                }
            }
        }

        public bool IsValidUser(UserInfo userInfo) { 
            lock(m_data) {
                bool result = m_data.ContainsKey(userInfo.Token);
                if (result) {
                    result = m_data[userInfo.Token].Number == userInfo.Number;
                }
                return result;
            }
        }
    }
}
