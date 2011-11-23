using System.Windows.Forms;
using System.Xml;

namespace ItopVector.DataSever
{
	/// <summary>
	/// 树结点扩展类
	/// </summary>
	/// 	
	public class TreeNodeEx:TreeNode
	{		
		public TreeNodeEx(string text):base(text)
		{
			
		}	
		public TreeNodeEx():this("")
		{
		}
		/// <summary>
		/// 层次
		/// </summary>
		public int Level
		{
			get
			{
				return GetLevel();
			}
		}
		
		private int GetLevel()
		{
			int layer=1;
			for(TreeNode node=this.Parent;node!=null;node=node.Parent)
			{
				layer++;
			}
			return layer;
		}
		public string GetConnectionUid()
		{
			string uid=string.Empty;

			XmlElement element1=GetConnectionNode();
			if(element1!=null)
			{
				uid=element1.GetAttribute("uid");
			}
			return uid;			
		}
		public XmlElement GetConnectionNode()
		{
			XmlElement element1=null;
		
			if(this.Tag is XmlElement) 
			{
				element1= this.Tag as XmlElement;
			}
			for(TreeNode node=this.Parent;node!=null;node=node.Parent)
			{
				if(node.Tag is XmlElement)
				{
					element1 =node.Tag as XmlElement;
				}
			}
			return element1;            
		}		
		public TreeNode[] GetFolders()
		{
			return null;
		}
		public TreeNode[] GetFiles()
		{
			return null;
		}

		public static  TreeNode FindNodeByTag(TreeNode parentNode,object tagvalue)
		{
			TreeNode node1 =null;
			foreach(TreeNode node2 in parentNode.Nodes)
			{
				if(node2.Nodes.Count>0)
				{
					node1=FindNodeByTag(node2,tagvalue);
					if(node1!=null)
					{
						break;
					}
				}
				if(node2.Tag==tagvalue)
				{
					node1=node2;
					break;
				}				
			}
			return node1;
		}
		public static  TreeNode FindNodeByTag(TreeView treeView,object tagvalue)
		{
			TreeNode node1 =null;
			foreach(TreeNode node2 in treeView.Nodes)
			{
				if(node2.Nodes.Count>0)
				{
					node1=FindNodeByTag(node2,tagvalue);
					if(node1!=null)
					{
						break;
					}
				}
				if(node2.Tag==tagvalue)
				{
					node1=node2;
					break;
				}				
			}
			return node1;
		}
	}
}
