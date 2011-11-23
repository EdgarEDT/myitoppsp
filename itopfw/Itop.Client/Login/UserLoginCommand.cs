				
using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Client.Login {
    public class UserLoginCommand: MainMenu.IMenuCommand {
        public bool Execute() {
            return new LoginForm().ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }
    }
}
