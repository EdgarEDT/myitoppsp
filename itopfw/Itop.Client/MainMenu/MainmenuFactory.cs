using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using System.Windows.Forms;
using Itop.Server.Interface;
using Itop.Common;
using System.Data;
using Itop.Domain.RightManager;

namespace Itop.Client.MainMenu {
    public class MainmenuFactory {

        internal static void Create(ToolStripItemCollection items) {

            IBaseService service = RemotingHelper.GetRemotingService<IBaseService>();

            IList list = service.GetList<Smmprog>();
            
            DataTable table = DataConverter.ToDataTable(list, typeof(Smmprog));
            ClearMenu(items);
            CreateSubmenu(items, table, string.Empty);
            
        }

        private static void ClearMenu(ToolStripItemCollection items) {
            if (items.Count == 0) return;
            for (int i = items.Count - 1; i >= 0; i--) {
                if (items[i].Tag is Smmprog || items[i].Text=="´°¿Ú") items.RemoveAt(i);
            }
            
        }

        static void CreateSubmenu(ToolStripItemCollection items, DataTable table, string parentid) {
            DataRow[] rows = table.Select(string.Format("parentid='{0}'",parentid));
            foreach (DataRow row in rows) {
                string text = row["progname"].ToString();

                if (text == "-") {
                    items.Add(new ToolStripSeparator());
                    continue;
                } else {
                    ToolStripMenuItem item = new ToolStripMenuItem(row["progname"].ToString());
                    item.Tag = DataConverter.RowToObject<Smmprog>(row);
                    item.Click += new EventHandler(item_Click);

                    items.Add(item);
                    CreateSubmenu(item.DropDownItems, table, row["progid"].ToString());
                }
            }
        }

        static void item_Click(object sender, EventArgs e) {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            Smmprog prog = item.Tag as Smmprog;
            if (prog != null && !string.IsNullOrEmpty(prog.AssemblyName) && !string.IsNullOrEmpty(prog.ClassName)) {
                MethodInvoker.Execute(prog.AssemblyName, prog.ClassName, prog.MethodName, new string[0]);
            }
        }
    }
}
