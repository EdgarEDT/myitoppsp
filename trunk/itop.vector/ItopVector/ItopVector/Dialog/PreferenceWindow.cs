using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using ItopVector.Resource;
//using ItopVector.WinControls.FuncControl;
using ItopVector.Core;
using ItopVector.Core.Func;
using ItopVector.Core.Interface.Figure;
using System.Xml;
//using ItopVector.WinControls.ColorManage.ColorPicker;
using System.Drawing.Text;
using ItopVector.Core.Paint;
using System.IO;

namespace ItopVector.Dialog
{
		public class PreferenceWindow : Form
 {
	 // Methods
			public PreferenceWindow()
			{
//				this.normalcode = new NormalCode();
//				this.indent = new Indent();
				this.ruleAndGrid = new RuleAndGrid();
				this.normalDesign = new NormalDesign();
				this.FolderImages = null; 
				this.preferenceDocument = null;
				this.valueDocument = null;
				this.components = null;
				this.InitializeComponent();
				this.preferenceDocument = new XmlDocument();
				Stream stream1 = Files.GetFileStream("ItopVector.Resource.Preference.Config.xml");
				if (stream1 != null)
				{
					this.preferenceDocument = new XmlDocument();
					this.preferenceDocument.Load(stream1);
				}
				string text1 = Application.StartupPath + @"\Preference\preference.xml";
				if (File.Exists(text1))
				{
					this.valueDocument = new XmlDocument();
					this.valueDocument.Load(text1);
				}
				this.CreateTabControl();
				this.CreateTree();
				if (this.valueDocument != null)
				{
//					this.normalcode.PreferenceDocument = this.valueDocument;
					this.normalDesign.PreferenceDocument = this.valueDocument;
					this.ruleAndGrid.PreferenceDocument = this.valueDocument;
				}
			}
 
			private void btnOk_Click(object sender, EventArgs e)
			{
//				this.normalcode.SaveProperty();
				this.ruleAndGrid.SaveProperty();
				this.normalDesign.SaveProperty();
				if (File.Exists(Application.StartupPath + @"\Preference\preference.xml") && (this.valueDocument != null))
				{
					this.valueDocument.Save(Application.StartupPath + @"\Preference\preference.xml");
				}

			}
 
			private void CreateTabControl()
			{
//				TabPage page1 = new TabPage(string.Empty, this.normalcode);
//				this.tabControl1.TabPages.Add(page1);
//				TabPage page2 = new TabPage(string.Empty, this.indent);
//				this.tabControl1.TabPages.Add(page2);
				TabPage page3 = new TabPage("常规");//, this.normalDesign);
				page3.Controls.Add(this.normalDesign);
				this.tabControl1.TabPages.Add(page3);
				TabPage page4 = new TabPage("网格、辅助线");//, this.ruleAndGrid);
				page4.Controls.Add(this.ruleAndGrid);
				this.tabControl1.TabPages.Add(page4);
				this.tabControl1.SelectedIndex = 0;
			}
 
			private void CreateTree()
			{
				if (this.preferenceDocument != null)
				{
					foreach (XmlNode node1 in this.preferenceDocument.DocumentElement.ChildNodes)
					{
						XmlAttribute attribute1 = node1.Attributes["label"];
						if (attribute1 != null)
						{
							TreeNode node2 = this.treeView1.Nodes.Add(attribute1.Value.Trim());
							node2.Tag = node1;
						}
					}
				}
				if (this.treeView1.Nodes.Count > 0)
				{
					this.treeView1.SelectedNode = this.treeView1.Nodes[0];
				}
			}
 
			protected override void Dispose(bool disposing)
			{
				if (disposing && (this.components != null))
				{
					this.components.Dispose();
				}
				base.Dispose(disposing);
			}
 
			private void InitializeComponent()
			{
				this.btnOk = new System.Windows.Forms.Button();
				this.btnCancel = new System.Windows.Forms.Button();
				this.label1 = new System.Windows.Forms.Label();
				this.treeView1 = new System.Windows.Forms.TreeView();
				this.tabControl1 = new System.Windows.Forms.TabControl();
				this.SuspendLayout();
				// 
				// btnOk
				// 
				this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
				this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
				this.btnOk.Location = new System.Drawing.Point(336, 320);
				this.btnOk.Name = "btnOk";
				this.btnOk.Size = new System.Drawing.Size(75, 24);
				this.btnOk.TabIndex = 0;
				this.btnOk.Text = "确 定";
				this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
				// 
				// btnCancel
				// 
				this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
				this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
				this.btnCancel.Location = new System.Drawing.Point(424, 320);
				this.btnCancel.Name = "btnCancel";
				this.btnCancel.Size = new System.Drawing.Size(75, 24);
				this.btnCancel.TabIndex = 1;
				this.btnCancel.Text = "取 消";
				// 
				// label1
				// 
				this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
					| System.Windows.Forms.AnchorStyles.Right)));
				this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
				this.label1.Location = new System.Drawing.Point(8, 312);
				this.label1.Name = "label1";
				this.label1.Size = new System.Drawing.Size(488, 2);
				this.label1.TabIndex = 2;
				this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
				// 
				// treeView1
				// 
				this.treeView1.ImageIndex = -1;
				this.treeView1.Location = new System.Drawing.Point(8, 8);
				this.treeView1.Name = "treeView1";
				this.treeView1.SelectedImageIndex = -1;
				this.treeView1.ShowLines = false;
				this.treeView1.ShowPlusMinus = false;
				this.treeView1.ShowRootLines = false;
				this.treeView1.Size = new System.Drawing.Size(128, 296);
				this.treeView1.TabIndex = 3;
				this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
				this.treeView1.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCollapse);
				this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
				this.treeView1.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeCollapse);
				this.treeView1.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
				// 
				// tabControl1
				// 
				this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
					| System.Windows.Forms.AnchorStyles.Right)));
				this.tabControl1.Location = new System.Drawing.Point(144, 8);
				this.tabControl1.Name = "tabControl1";
				this.tabControl1.SelectedIndex = 0;
				this.tabControl1.Size = new System.Drawing.Size(352, 296);
				this.tabControl1.TabIndex = 4;
				// 
				// PreferenceWindow
				// 
				this.AcceptButton = this.btnOk;
				this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
				this.CancelButton = this.btnCancel;
				this.ClientSize = new System.Drawing.Size(506, 351);
				this.ControlBox = false;
				this.Controls.Add(this.tabControl1);
				this.Controls.Add(this.treeView1);
				this.Controls.Add(this.label1);
				this.Controls.Add(this.btnCancel);
				this.Controls.Add(this.btnOk);
				this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
				this.MaximizeBox = false;
				this.MinimizeBox = false;
				this.Name = "PreferenceWindow";
				this.ShowInTaskbar = false;
				this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
				this.Text = "参数设置";
				this.ResumeLayout(false);

			}
 
			private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
			{
				e.Node.ImageIndex = 1;
			}
 
			private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
			{
				e.Node.ImageIndex = 0;
				if ((e.Node.Nodes.Count > 0) && e.Node.IsExpanded)
				{
					this.treeView1.SelectedNode = e.Node.Nodes[0];
				}
			}
 
			private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
			{
				if (this.preferenceDocument != null)
				{
					XmlNode node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@method='code']");
					if (e.Node.Tag == node1)
					{
						//this.tabControl1.SelectedIndex = 0;
					}
					else if (e.Node.Tag == this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@method='Tab']"))
					{
						//this.tabControl1.SelectedIndex = 1;
					}
					else if (e.Node.Tag == this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@method='general']"))
					{
						this.tabControl1.SelectedIndex = 0;
					}
					else if (e.Node.Tag == this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@method='ruleandgrid']"))
					{
						this.tabControl1.SelectedIndex = 1;
					}
				}
			}
 
			private void treeView1_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
			{
				e.Node.ImageIndex = 1;
			}
 
			private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
			{
				this.treeView1.CollapseAll();
			}
 

	 // Fields
	 private Button btnCancel;
	 private Button btnOk;
	 private Container components;
	 private ImageList FolderImages;
	 //private Indent indent;
	 private Label label1;
	 //private NormalCode normalcode;
	 private NormalDesign normalDesign;
	 private XmlDocument preferenceDocument;
	 private RuleAndGrid ruleAndGrid;
	 private TabControl tabControl1;
	 private TreeView treeView1;
	 private XmlDocument valueDocument;
 }
 
}
