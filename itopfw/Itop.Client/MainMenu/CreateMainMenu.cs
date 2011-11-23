
using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;
using System.Data;

using Itop.Common;
using Itop.Client.Tree;

using Itop.Server.Interface.MainMenu;
using Itop.Server.Interface.Tree;

namespace Itop.Client.MainMenu {
    public class CreateMainMenu {
        private bool IsSubLevel(string parent, string child) {
            return (child.Length - parent.Length == 2) && child.StartsWith(parent);
        }

        public DataSet GetData() {
            IMainMenuAction mainMenuAction = RemotingHelper.GetRemotingService<IMainMenuAction>();
            if (mainMenuAction == null)
                return null;

            return mainMenuAction.GetMainMenuDataSet(MIS.UserInfo);
        }

        public DataSet Execute(DataSet data, TreeView treeView) {
            if (data == null)
                data = GetData();

            TreeViewGenerator tv = new TreeViewGenerator(treeView.Nodes);
            tv.Execute(data.Tables[0]);

            return data;
        }

        public DataSet Execute(MenuStrip mainMenu) {
            return Execute(null, mainMenu, true);
        }

        public DataSet Execute(DataSet data, MenuStrip mainMenu, bool addClickEvent) {
            if (data == null)
                data = GetData();
            MainMenuGenerator mm = new MainMenuGenerator(mainMenu.Items);
            mm.AddClickEvent = addClickEvent;
            if (addClickEvent) {
                mm.AddClickEventHandler += delegate(ToolStripMenuItem item, TagData tag) {
                    item.Click += delegate {
                        RunMenuItem.Click(tag["AssemblyName"].ToString(), tag["ClassName"].ToString(),
                            tag["MethodName"].ToString(), (int)tag["ObjId"]);
                    };
                };
            }
            mm.Execute(data.Tables[0]);

            return data;
        }
    }
}
