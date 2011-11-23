
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Itop.Server.Interface.Tree;
using Itop.Client.MainMenu;

namespace Itop.Client.Console {
    /// <summary>
    /// 树型菜单
    /// </summary>
    public partial class TreeStyleMenu : UserControl {
        private DataTable m_mainMenuTable;

        public TreeStyleMenu() {
            InitializeComponent();

            SetTreeViewStyle(m_treeViewOne);
            SetTreeViewStyle(m_treeViewTwo);
        }

        private void SetTreeViewStyle(TreeView tv) {
            tv.FullRowSelect = true;
            tv.ItemHeight = 25;
            tv.Indent = 25;
            tv.HideSelection = true;
            tv.ShowLines = false;
            tv.BorderStyle = BorderStyle.FixedSingle;
        }

        public void SetMenu(DataTable t) {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeStyleMenu));
            m_toolStrip.Items.Clear();

            m_mainMenuTable = t;

            DataRow[] rows = m_mainMenuTable.Select("Len(MenuLevel) = 2");
            foreach (DataRow row in rows) {
                string caption = row["MenuCaption"].ToString();
                if (caption.Length < 4) {
                    caption += "　　"; // 两个全角空格
                }
                ToolStripButton item = new ToolStripButton(caption);
                item.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                item.Alignment = ToolStripItemAlignment.Left;
                item.AutoToolTip = false;
                item.Tag = row["MenuLevel"];
                item.CheckOnClick = true;
                item.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
                m_toolStrip.Items.Add(item);
                item.Click += item_Click;

                m_toolStrip.Items.Add(new ToolStripSeparator());
            }
        }

        void item_Click(object sender, EventArgs e) {
            m_treeViewOne.Nodes.Clear();
            m_treeViewTwo.Nodes.Clear();
            foreach (ToolStripItem i in m_toolStrip.Items) {
                if ((i != sender) && (i is ToolStripButton)) {
                    ((ToolStripButton)i).Checked = false;
                }
            }

            ToolStripButton btn = sender as ToolStripButton;
            if (btn == null)
                return;

            btn.Checked = true;

            string menuLevel = (string)btn.Tag;
            string filter = string.Format("Len(MenuLevel) = {0} and SubString(MenuLevel, 1, {1}) = {2} and MenuCaption <> '-' ",
                menuLevel.Length + 2, menuLevel.Length, menuLevel);
            DataRow[] rows = m_mainMenuTable.Select(filter, "MenuLevel");
            foreach (DataRow row in rows) {
                TreeNode node = new TreeNode(row["MenuCaption"].ToString());
                m_treeViewOne.Nodes.Add(node);
                TagData tag = new TagData();
                tag["MenuLevel"] = row["MenuLevel"];
                tag["ActionId"] = row["ActionId"];
                tag["AssemblyName"] = row["AssemblyName"];
                tag["ClassName"] = row["ClassName"];
                tag["MethodName"] = row["MethodName"];
                tag["ObjId"] = row["ObjId"];
                node.Tag = tag;
            }

            if (m_treeViewOne.Nodes.Count != 0) {
                m_treeViewOne.SelectedNode = m_treeViewOne.Nodes[0];
            }
        }

        private void m_treeViewOne_AfterSelect(object sender, TreeViewEventArgs e) {
            m_treeViewTwo.Nodes.Clear();

            if (e.Node == null)
                return;

            TagData tag = e.Node.Tag as TagData;

            string menuLevel = tag["MenuLevel"].ToString();
            string filter = string.Format("Len(MenuLevel) > {0} and SubString(MenuLevel, 1, {1}) = {2} and MenuCaption <> '-' ",
                menuLevel.Length, menuLevel.Length, menuLevel);
            DataRow[] rows = m_mainMenuTable.Select(filter, "MenuLevel");
            CreateMainMenu createMainMenu = new CreateMainMenu();
            DataSet data = new DataSet();
            DataTable table = new DataTable();
            data.Tables.Add(table);
            table.Columns.Add("MenuCaption");
            table.Columns.Add("MenuLevel");
            table.Columns.Add("ActionId");
            table.Columns.Add("AssemblyName");
            table.Columns.Add("ClassName");
            table.Columns.Add("MethodName");
            table.Columns.Add("ObjId");
            foreach (DataRow row in rows) {
                table.Rows.Add(new object[] { row["MenuCaption"], row["MenuLevel"], row["ActionId"],
                row["AssemblyName"], row["ClassName"], row["MethodName"], row["ObjId"]});
            }
            createMainMenu.Execute(data, m_treeViewTwo);

            m_treeViewTwo.ExpandAll();
        }

        private void m_treeViewOne_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Node == null)
                return;

            if (e.Node.Nodes.Count != 0)
                return;

            TagData tag = e.Node.Tag as TagData;

            string assemblyName = tag["AssemblyName"].ToString();
            string className = tag["ClassName"].ToString();
            string methodName = tag["MethodName"].ToString();
            int objId = Convert.ToInt32(tag["ObjId"]);
            RunMenuItem.Click(assemblyName, className, methodName, objId);
        }

        private void m_treeViewOne_MouseMove(object sender, MouseEventArgs e) {
            TreeView tv = sender as TreeView;
            if (tv == null)
                return;

            tv.Cursor = (tv.GetNodeAt(e.X, e.Y) == null) ? Cursors.Default : Cursors.Hand;
        }
    }
}
