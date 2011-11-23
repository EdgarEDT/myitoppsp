
using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

using Itop.Server.Interface.Tree;

namespace Itop.Client.Tree {
    /// <summary>
    /// TreeViewÉú³ÉÆ÷
    /// </summary>
    public class TreeViewGenerator : TreeGenerator<TreeNode, TreeNodeCollection> {
        protected override void AddSeparator(TreeNode parent) {
            // do nothing
        }

        protected override void AddNode(TreeNode parent, TreeNode child) {
            parent.Nodes.Add(child);
        }

        protected override void AddRoot(TreeNode child) {
            Root.Add(child);
        }

        protected override TagData GetTag(TreeNode item) {
            return item.Tag as TagData;
        }

        protected override void SetTag(TreeNode item, TagData treeData) {
            item.Tag = treeData;
            item.Text = treeData[TextName].ToString();
        }

        public TreeViewGenerator(TreeNodeCollection root, string textName, string levelName)
            : base(root, textName, levelName) {
            GenSeparator = false;
        }

        public TreeViewGenerator(TreeNodeCollection root)
            : this(root, "MenuCaption", "MenuLevel") { }

        public TreeNode FindNode(TreeNodeCollection root, TagData tagData) {
            foreach (TreeNode node in root) {
                TagData tag = node.Tag as TagData;
                if (tag == tagData) {
                    return node;
                }

                // µÝ¹éÕÒ×ÓÊ÷
                TreeNode n = FindNode(node.Nodes, tagData);
                if (n != null)
                    return n;
            }

            return null;
        }
    }
}
