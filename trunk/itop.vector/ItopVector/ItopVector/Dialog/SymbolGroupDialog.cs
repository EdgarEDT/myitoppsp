using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

namespace ItopVector.Dialog
{
	/// <summary>
	/// SymbolGroupDialog 的摘要说明。
	/// </summary>
	public class SymbolGroupDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private ItopVector.Core.Document.SvgDocument symbolDoc;
		private Hashtable nodeTable;
		public  XmlElement SelectedElement;
		public XmlElement ToInsertElement;
		private System.Windows.Forms.Button button3;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SymbolGroupDialog()
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
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(8, 8);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(192, 256);
			this.listBox1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Location = new System.Drawing.Point(208, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(2, 256);
			this.label1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(216, 208);
			this.button1.Name = "button1";
			this.button1.TabIndex = 2;
			this.button1.Text = "确认(&O)";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(216, 240);
			this.button2.Name = "button2";
			this.button2.TabIndex = 2;
			this.button2.Text = "取消(&C)";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(216, 8);
			this.button3.Name = "button3";
			this.button3.TabIndex = 2;
			this.button3.Text = "管理…(&M)";
			this.button3.Visible = false;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// SymbolGroupDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(298, 273);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SymbolGroupDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "选择追加元件库组";
			this.ResumeLayout(false);

		}
		#endregion

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);

			this.SetListBox();
		}
		private void SetListBox()
		{
			this.listBox1.Items.Clear();
			nodeTable.Clear();
			if(symbolDoc !=null)
			{
				XmlNodeList list1 = symbolDoc.GetElementsByTagName("group");
				for (int num1 = 0; num1 < list1.Count; num1++)
				{
					XmlElement element1 = list1[num1] as XmlElement;

					string strid = element1.GetAttribute("id");
					if(strid==string.Empty)strid = "未命名";
					this.listBox1.Items.Add(strid);
					nodeTable.Add(num1,element1);
				}
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if(this.listBox1.SelectedIndex<=this.listBox1.Items.Count -1)
			{
				this.SelectedElement = nodeTable[this.listBox1.SelectedIndex] as XmlElement;
				if(this.ToInsertElement !=null && this.ToInsertElement is ItopVector.Core.Figure.Symbol)
				{
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			SymbolManagerDialog dlg =new SymbolManagerDialog();
			dlg.SymbolDoc = this.symbolDoc;
			if(dlg.ShowDialog(this)==DialogResult.OK)
			{
				this.SetListBox();
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
