using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Resources;
using System.IO;

namespace ItopVectorDraw
{
	public delegate void OnFileManagerEventHandler(object sender, object[] formlist, Action action);


	public enum Action
	{
		
		Activate = 0,
		Close = 1,
		CloseAll = 3,
		Save = 2
	}

	public class FormManager : Form
	{
		// Fields
		private Button button1;
		private Button button2;
		private Button button3;
		private Button button4;
		private Button button5;
		private Container components;
		private ListView listView1;
		private ColumnHeader nameheader;
		private ColumnHeader pathheader;

		
		private Hashtable frmTable;
		// Events
		public event OnFileManagerEventHandler FileManagerEvent;

		// Methods
		public FormManager()
		{
			this.components = null;
			this.InitializeComponent();
			this.listView1.Items.Clear();
			this.listView1.Focus();
			this.UpdateLabel();
			frmTable =new Hashtable(16);
		}

		public Form[] Documents
		{
			set
			{
				this.listView1.Items.Clear();
				if (value != null)
				{
					
					foreach (Form form1 in value)
					{
						frmDocument frm=null;
						if((frm =form1 as frmDocument)==null)continue;
						string text2 = frm.FileName;
						string text1 = frm.FilePath;
						ListViewItem item = this.listView1.Items.Add(text2);
						item.SubItems.Add(text1);
						this.frmTable.Add(item,frm);
					}
				}
				this.button4.Enabled = this.listView1.Items.Count > 0;
			}
		}
		
		public void AddFile(string filename, string filepath)
		{
			if (filename != string.Empty)
			{
				ListViewItem item1 = this.listView1.Items.Add(filename);
				if (filepath != string.Empty)
				{
					item1.SubItems.Add(filepath);
				}
			}
		}
		
		private void button1_Click(object sender, EventArgs e)
		{
			ArrayList list1 = new ArrayList(0x10);
			
			if(sender == this.button4)
			{
				foreach(ListViewItem item1 in this.listView1.Items )
				{
					list1.Add(item1);
				}
			}
			else
			{
				foreach (ListViewItem item1 in this.listView1.SelectedItems)
				{
					list1.Add(item1);
				}
			}

			if ((list1.Count > 0) || ((sender == this.button4) && (this.listView1.Items.Count > 0)))
			{
				ListViewItem[] intArray1 = new ListViewItem[list1.Count];
				list1.CopyTo(intArray1);
				Action action1 = Action.Activate;
				if(sender==this.button1)
				{
                    frmDocument frm =this.frmTable[intArray1[0]] as frmDocument;
					frm.Activate();
				}
				else if (sender == this.button2)
				{
					action1 = Action.Save;
					foreach(ListViewItem item in intArray1)
					{
						frmDocument frm =this.frmTable[item] as frmDocument;
						if(frm!=null)
						{
							frm.Save();
						}
					}
												  
				}
				else if (sender == this.button3)
				{
					action1 = Action.Close;
					foreach(ListViewItem item in intArray1)
					{
						frmDocument frm =this.frmTable[item] as frmDocument;
						if(frm!=null)
						{
							frm.Close();
							if(frm.IsDisposed)
							{
								this.frmTable.Remove(item);
								this.listView1.Items.Remove(item);
							}
						}
					}
				}
				else if (sender == this.button4)
				{
					action1 = Action.CloseAll;
					foreach(ListViewItem item in intArray1)
					{
						frmDocument frm =this.frmTable[item] as frmDocument;
						if(frm!=null)
						{
							frm.Close();
							if(frm.IsDisposed)
							{
								this.frmTable.Remove(item);
								this.listView1.Items.Remove(item);
							}
						}
					}
				}
				if (this.FileManagerEvent != null)
				{
//					this.FileManagerEvent(this, intArray1, action1);
				}
			}
			this.button4.Enabled = this.listView1.Items.Count > 0;
		}
 

		private void button5_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		public void ClearItems()
		{
			if (this.listView1 != null)
			{
				this.listView1.Items.Clear();
			}
		}

		public void Delete(int index)
		{
			if ((this.listView1 != null) && ((index >= 0) && (index < this.listView1.Items.Count)))
			{
				this.listView1.Items.RemoveAt(index);
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

		#region 构造
		private void InitializeComponent()
		{
			ListViewItem.ListViewSubItem[] itemArray1 = new ListViewItem.ListViewSubItem[1] { new ListViewItem.ListViewSubItem(null, "", SystemColors.WindowText, SystemColors.Window, new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x86)) } ;
			ListViewItem item1 = new ListViewItem(itemArray1, -1);
			this.button1 = new Button();
			this.button2 = new Button();
			this.button3 = new Button();
			this.button4 = new Button();
			this.button5 = new Button();
			this.listView1 = new ListView();
			this.nameheader = new ColumnHeader();
			this.pathheader = new ColumnHeader();
			base.SuspendLayout();
			this.button1.Enabled = false;
			this.button1.Location = new Point(0x120, 8);
			this.button1.Name = "button1";
			this.button1.Size = new Size(0x58, 0x18);
			this.button1.TabIndex = 1;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.Enabled = false;
			this.button2.Location = new Point(0x120, 40);
			this.button2.Name = "button2";
			this.button2.Size = new Size(0x58, 0x18);
			this.button2.TabIndex = 2;
			this.button2.Click += new EventHandler(this.button1_Click);
			this.button3.Enabled = false;
			this.button3.Location = new Point(0x120, 0x48);
			this.button3.Name = "button3";
			this.button3.Size = new Size(0x58, 0x18);
			this.button3.TabIndex = 3;
			this.button3.Click += new EventHandler(this.button1_Click);
			this.button4.Enabled = false;
			this.button4.Location = new Point(0x120, 0x68);
			this.button4.Name = "button4";
			this.button4.Size = new Size(0x58, 0x18);
			this.button4.TabIndex = 4;
			this.button4.Click += new EventHandler(this.button1_Click);
			this.button5.Location = new Point(0x120, 0x130);
			this.button5.Name = "button5";
			this.button5.Size = new Size(0x58, 0x18);
			this.button5.TabIndex = 5;
			this.button5.Click += new EventHandler(this.button5_Click);
			ColumnHeader[] headerArray1 = new ColumnHeader[2] { this.nameheader, this.pathheader } ;
			this.listView1.Columns.AddRange(headerArray1);
			this.listView1.HideSelection = false;
			ListViewItem[] itemArray2 = new ListViewItem[1] { item1 } ;
			this.listView1.Items.AddRange(itemArray2);
			this.listView1.Location = new Point(8, 8);
			this.listView1.Name = "listView1";
			this.listView1.Size = new Size(0x110, 320);
			this.listView1.TabIndex = 6;
			this.listView1.View = View.Details;
			this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
			this.nameheader.Width = 100;
			this.pathheader.Width = 150;
			this.AutoScaleBaseSize = new Size(6, 14);
			base.ClientSize = new Size(0x180, 0x155);
			Control[] controlArray1 = new Control[6] { this.listView1, this.button5, this.button4, this.button3, this.button2, this.button1 } ;
			base.Controls.AddRange(controlArray1);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormManager";
			base.ShowInTaskbar = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			base.ResumeLayout(false);
		}
		#endregion

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			bool flag1;
			this.button3.Enabled = flag1 = this.listView1.SelectedItems.Count > 0;
			this.button2.Enabled = flag1;
			this.button1.Enabled = flag1;
			this.button4.Enabled = this.listView1.Items.Count>0;
		}

		public void SetActive(int index)
		{
			if ((index >= 0) && (index < this.listView1.Items.Count))
			{
				this.listView1.Items[index].Checked = true;
				this.listView1.Focus();
			}
		}

		private void UpdateLabel()
		{
			this.Text = "窗口";
			this.pathheader.Text = "路径";
			this.nameheader.Text = "名称";
			this.button5.Text = "关闭";
			this.button4.Text = "关闭全部";
			this.button3.Text = "关闭窗口(&C)";
			this.button2.Text = "保存(&S)";
			this.button1.Text = "激活(&A)";
		}		
	}
}
