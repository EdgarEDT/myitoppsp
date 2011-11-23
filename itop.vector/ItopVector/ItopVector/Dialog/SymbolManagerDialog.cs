using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

using ItopVector.Core;
using ItopVector.Core.Figure;

namespace ItopVector.Dialog
{
	/// <summary>
	/// SymbolGroupDialog 的摘要说明。
	/// </summary>
	public class SymbolManagerDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private ItopVector.Core.Document.SvgDocument symbolDoc;
		private Hashtable nodeTable;
		public  XmlElement SelectedElement;
		public XmlElement ToInsertElement;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.Button btAddGroup;
		private System.Windows.Forms.Button btDelete;
		private System.Windows.Forms.Button btRename;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SymbolManagerDialog()
		{
			SelectedElement= null;
			symbolDoc = null;
			nodeTable = new Hashtable();
			InitializeComponent();
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.btAddGroup = new System.Windows.Forms.Button();
			this.btDelete = new System.Windows.Forms.Button();
			this.btRename = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Location = new System.Drawing.Point(320, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(2, 334);
			this.label1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(328, 286);
			this.button1.Name = "button1";
			this.button1.TabIndex = 2;
			this.button1.Text = "确认(&O)";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(328, 318);
			this.button2.Name = "button2";
			this.button2.TabIndex = 2;
			this.button2.Text = "取消(&C)";
			// 
			// treeView1
			// 
			this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(8, 8);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(304, 334);
			this.treeView1.TabIndex = 3;
			// 
			// btAddGroup
			// 
			this.btAddGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btAddGroup.Location = new System.Drawing.Point(328, 8);
			this.btAddGroup.Name = "btAddGroup";
			this.btAddGroup.TabIndex = 2;
			this.btAddGroup.Text = "增加组(&G)";
			this.btAddGroup.Click += new System.EventHandler(this.btAddGroup_Click);
			// 
			// btDelete
			// 
			this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btDelete.Location = new System.Drawing.Point(328, 32);
			this.btDelete.Name = "btDelete";
			this.btDelete.TabIndex = 2;
			this.btDelete.Text = "删  除(&D)";
			this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
			// 
			// btRename
			// 
			this.btRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btRename.Location = new System.Drawing.Point(328, 88);
			this.btRename.Name = "btRename";
			this.btRename.TabIndex = 2;
			this.btRename.Text = "重命名(&R)";
			this.btRename.Click += new System.EventHandler(this.btRename_Click);
			// 
			// SymbolManagerDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(410, 351);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.btAddGroup);
			this.Controls.Add(this.btDelete);
			this.Controls.Add(this.btRename);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SymbolManagerDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "元件库管理";
			this.Load += new System.EventHandler(this.SymbolManagerDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed (e);			
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			if(symbolDoc !=null)
			{
				XmlNodeList list1 = symbolDoc.GetElementsByTagName("group");
				for (int num1 = 0; num1 < list1.Count; num1++)
				{
					XmlElement element1 = list1[num1] as XmlElement;

					string strid = element1.GetAttribute("id");
					if(strid==string.Empty)strid = "未命名";
					TreeNode node =new TreeNode(strid);
					node.Tag = element1;
					treeView1.Nodes.Add(node);
					this.ExpandNode(node);
				}
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{			
//			if(!this.symbolDoc.Update)
			{	
				//ItopVector.Core.Func.CodeFunc.FormatXmlDocumentString(this.symbolDoc);
				this.symbolDoc.Save(this.symbolDoc.FilePath);
				this.symbolSelector.ReLoad();				
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void treeView1_DoubleClick(object sender, System.EventArgs e)
		{
			Point point1 =this.treeView1.PointToClient(Button.MousePosition);
			TreeNode node = treeView1.GetNodeAt(point1);
			if(node!=null)
			{
				if(node.Nodes.Count==0)
				{
					XmlElement element =node.Tag as XmlElement;
					foreach(XmlNode xmlnode in element.ChildNodes)
					{
						if(xmlnode is ItopVector.Core.Figure.Symbol)
						{
							XmlElement element2=xmlnode as XmlElement;
							string strid = element2.GetAttribute("id");
							if(strid==string.Empty)strid = "未命名";
							TreeNode node2= new TreeNode(strid);
							node2.Tag = element2;
							node.Nodes.Add(node2);
						}
					}
				}
			}
		}

		private void btDelete_Click(object sender, System.EventArgs e)
		{
			TreeNode node = this.treeView1.SelectedNode;
			if(node!=null && node.Nodes.Count==0)
			{
			
				XmlElement element1=null;
				XmlElement element2=node.Tag as XmlElement;
				
				
				if(node.Parent==null)
				{
					foreach(XmlNode xmlnode in element2.ChildNodes)
					{
						if(xmlnode is ItopVector.Core.SvgElement)return;
					}
					element1 = symbolDoc.DocumentElement;
				}
				else
				{
					element1 = node.Parent.Tag as XmlElement;
				}
				try
				{
					element1.RemoveChild(element2);
					node.Remove();
				}
				catch(Exception err)
				{
					MessageBox.Show(err.Message);
				}
			}
		}

		private void btAddGroup_Click(object sender, System.EventArgs e)
		{
			ItopVector.Dialog.InputDialog dlg =new InputDialog();
			if(dlg.ShowDialog()==DialogResult.OK)
			{
				XmlNode xmlnode=this.symbolDoc.SelectSingleNode("//*[@id='" + dlg.InputString + "']");
				
				if(xmlnode!=null )
				{
					MessageBox.Show("已存在名为'"+dlg.InputString+"'组或图元!","增加图元分类");
					return ;
				}
				XmlElement element1 =symbolDoc.CreateElement(symbolDoc.Prefix,"group",symbolDoc.NamespaceURI) as XmlElement;
					
//				if(this.symbolSelector!=null)
//				{
//					element1.SetAttribute("id",dlg.InputString);
//                    this.symbolSelector.AddGroup(element1);
//				}
//				else
				{
					this.symbolDoc.DocumentElement.AppendChild(element1);
					element1.SetAttribute("id",dlg.InputString);
				}
				TreeNode node =new TreeNode(dlg.InputString);
				node.Tag = element1;
				treeView1.Nodes.Add(node);
			}
		}
		private void ExpandNode(TreeNode pTreeNode)
		{
			TreeNode node = pTreeNode;
			if(node!=null)
			{
				if(node.Nodes.Count==0)
				{
					XmlNodeList nodelist =(node.Tag as XmlElement).GetElementsByTagName("symbol");
					foreach(XmlNode xmlnode in nodelist)
					{
						Symbol symbol = xmlnode as ItopVector.Core.Figure.Symbol;
						if(symbol!=null && symbol.Visible)
						{
							XmlElement element2=xmlnode as XmlElement;
							string strid = element2.GetAttribute("label");
							if(strid==string.Empty)
							{								
								strid = element2.GetAttribute("id");
								element2.SetAttribute("label",strid);
							}
							if(strid==string.Empty)
							{
								strid = "未命名";
							}
							TreeNode node2= new TreeNode(strid);
							node2.Tag = element2;
							node.Nodes.Add(node2);
						}
					}
				}
			}
		}
		private ItopVector.Selector.SymbolSelector symbolSelector;
		public ItopVector.Selector.SymbolSelector SymbolSelector
		{
			set
			{
				if(value==null || value==this.symbolSelector)return;
                this.symbolSelector = value;
				this.symbolDoc = this.symbolSelector.SymbolDoc;
			}
		}

		private void SymbolManagerDialog_Load(object sender, System.EventArgs e)
		{
		
		}

		private void btRename_Click(object sender, System.EventArgs e)
		{
			TreeNode node = this.treeView1.SelectedNode;
			if(node!=null )
			{				
				XmlElement element2=node.Tag as XmlElement;
				ItopVector.Dialog.InputDialog dlg =new InputDialog();
				dlg.InputString=node.Text;
				if(dlg.ShowDialog()==DialogResult.OK)
				{
					if(node.Text ==dlg.InputString)return;
//					XmlNode xmlnode=this.symbolDoc.SelectSingleNode("//*[@id='" + dlg.InputString + "']");
//				
//					if(xmlnode!=null  )
//					{
//						MessageBox.Show("已存在名为'"+dlg.InputString+"'组或图元!","增加图元分类");
//						return ;
//					}
					element2.SetAttribute("label",dlg.InputString);
					node.Text = dlg.InputString;
				}
			}
		
		}

		public ItopVector.Core.Document.SvgDocument SymbolDoc
		{
			get
			{
				return this.symbolDoc;
			}
			set
			{
				this.symbolDoc = value;
			}
		}
	}
}
