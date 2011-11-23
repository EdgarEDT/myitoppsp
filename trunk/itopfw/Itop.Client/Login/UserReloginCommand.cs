				
using System;
using System.Collections.Generic;
using System.Text;

namespace Itop.Client.Login {
    public class UserReloginCommand: MainMenu.IMenuCommand {
        #region IMenuCommand Members

        public bool Execute() {
            return new LoginForm(true).ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        #endregion
    }
}
