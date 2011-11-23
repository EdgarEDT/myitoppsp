				
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

using Itop.Client.MainMenu;

namespace Itop.Client.Console {
    public static class TreeStyleMenuFactory {
        public static Control Create() {
            TreeStyleMenu result = new TreeStyleMenu();

            CreateMainMenu cm = new CreateMainMenu();
            DataSet data = cm.GetData();
            result.SetMenu(data.Tables[0]);
            return result;
        }
    }
}
