	
using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Server.Interface.Login {
    [Serializable]
    public struct LoginData { 
        public string UserNumber;
        public string Password;

        public LoginData(string userNumber, string pasword) {
            UserNumber = userNumber;
            Password = pasword;
        }
    }
}
