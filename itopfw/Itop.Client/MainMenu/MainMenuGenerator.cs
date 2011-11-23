				
using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

using Itop.Client.Tree;

using Itop.Server.Interface.Tree;

namespace Itop.Client.MainMenu {
    /// <summary>
    /// 主菜单生成器
    /// </summary>
    public class MainMenuGenerator : TreeGenerator<ToolStripMenuItem, ToolStripItemCollection> {
        protected override void AddSeparator(ToolStripMenuItem parent){
            parent.DropDownItems.Add(new ToolStripSeparator());    
        }

        protected override void AddNode(ToolStripMenuItem parent, ToolStripMenuItem child) {
            parent.DropDownItems.Add(child);
        }

        protected override void AddRoot(ToolStripMenuItem child) {
            Root.Add(child);
        }

        protected override TagData GetTag(ToolStripMenuItem item) {
            return item.Tag as TagData;
        }

        protected override void SetTag(ToolStripMenuItem item, TagData treeData) {
            item.Tag = treeData;
            item.Text = treeData[TextName].ToString();
        }

        public MainMenuGenerator(ToolStripItemCollection root, string textName, string levelName)
            : base(root, textName, levelName) {
            GenSeparator = true;
        }

        public MainMenuGenerator(ToolStripItemCollection root)
            : this(root, "MenuCaption", "MenuLevel") { }
    }
}
