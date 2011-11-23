using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using ItopVector.Core.Figure;
using ItopVector.Core.Document;

namespace ItopVector.Dialog
{
	/// <summary>
	/// 图层管理对话框
	/// </summary>
	public class LayerManagerDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private ItopVector.Core.Document.SvgDocument symbolDoc;
		private Hashtable nodeTable;
//		public  XmlElement SelectedElement;
//		public XmlElement ToInsertElement;
		private System.Windows.Forms.CheckedListBox checkedListBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btSave;
		private System.Windows.Forms.Button btCancel;
		private System.Windows.Forms.Button btAdd;
		private System.Windows.Forms.Button btModify;
		private System.Windows.Forms.Button btDelete;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btGoUp;
		private System.Windows.Forms.Button btGoNext;
		private System.Windows.Forms.Button btSelectAll;
		private System.Windows.Forms.Button btUnSelectAll;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public LayerManagerDialog()
		{
			//SelectedElement= null;
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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("asdsad");
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("asdasd");
			this.label1 = new System.Windows.Forms.Label();
			this.btSave = new System.Windows.Forms.Button();
			this.btCancel = new System.Windows.Forms.Button();
			this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btAdd = new System.Windows.Forms.Button();
			this.btModify = new System.Windows.Forms.Button();
			this.btDelete = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.btGoUp = new System.Windows.Forms.Button();
			this.btGoNext = new System.Windows.Forms.Button();
			this.btSelectAll = new System.Windows.Forms.Button();
			this.btUnSelectAll = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Location = new System.Drawing.Point(280, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(2, 360);
			this.label1.TabIndex = 1;
			// 
			// btSave
			// 
			this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btSave.Location = new System.Drawing.Point(288, 320);
			this.btSave.Name = "btSave";
			this.btSave.TabIndex = 2;
			this.btSave.Text = "确认(&O)";
			this.btSave.Click += new System.EventHandler(this.btSave_Click);
			// 
			// btCancel
			// 
			this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Location = new System.Drawing.Point(288, 344);
			this.btCancel.Name = "btCancel";
			this.btCancel.TabIndex = 2;
			this.btCancel.Text = "取消(&C)";
			// 
			// checkedListBox1
			// 
			this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.checkedListBox1.Location = new System.Drawing.Point(8, 8);
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.Size = new System.Drawing.Size(264, 356);
			this.checkedListBox1.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.ForeColor = System.Drawing.Color.Navy;
			this.label2.Location = new System.Drawing.Point(288, 224);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 40);
			this.label2.TabIndex = 4;
			this.label2.Text = "打钩为显示,不打钩为隐藏";
			// 
			// btAdd
			// 
			this.btAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btAdd.Location = new System.Drawing.Point(288, 8);
			this.btAdd.Name = "btAdd";
			this.btAdd.TabIndex = 2;
			this.btAdd.Text = "增加(&N)";
			this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
			// 
			// btModify
			// 
			this.btModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btModify.Location = new System.Drawing.Point(288, 32);
			this.btModify.Name = "btModify";
			this.btModify.TabIndex = 2;
			this.btModify.Text = "修改(&M)";
			this.btModify.Click += new System.EventHandler(this.btModify_Click);
			// 
			// btDelete
			// 
			this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btDelete.Location = new System.Drawing.Point(288, 56);
			this.btDelete.Name = "btDelete";
			this.btDelete.TabIndex = 2;
			this.btDelete.Text = "删除(&D)";
			this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.ForeColor = System.Drawing.Color.Navy;
			this.label3.Location = new System.Drawing.Point(288, 184);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 40);
			this.label3.TabIndex = 4;
			this.label3.Text = "被选中图层为当前操作图层";
			// 
			// btGoUp
			// 
			this.btGoUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btGoUp.Location = new System.Drawing.Point(288, 88);
			this.btGoUp.Name = "btGoUp";
			this.btGoUp.TabIndex = 2;
			this.btGoUp.Text = "上移(&U)";
			this.btGoUp.Visible = false;
			this.btGoUp.Click += new System.EventHandler(this.btGoUp_Click);
			// 
			// btGoNext
			// 
			this.btGoNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btGoNext.Location = new System.Drawing.Point(288, 112);
			this.btGoNext.Name = "btGoNext";
			this.btGoNext.TabIndex = 2;
			this.btGoNext.Text = "下移(&X)";
			this.btGoNext.Visible = false;
			this.btGoNext.Click += new System.EventHandler(this.btGoNext_Click);
			// 
			// btSelectAll
			// 
			this.btSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btSelectAll.Location = new System.Drawing.Point(288, 136);
			this.btSelectAll.Name = "btSelectAll";
			this.btSelectAll.TabIndex = 2;
			this.btSelectAll.Text = "全选(&A)";
			this.btSelectAll.Click += new System.EventHandler(this.btSelectAll_Click);
			// 
			// btUnSelectAll
			// 
			this.btUnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btUnSelectAll.Location = new System.Drawing.Point(288, 160);
			this.btUnSelectAll.Name = "btUnSelectAll";
			this.btUnSelectAll.TabIndex = 2;
			this.btUnSelectAll.Text = "全消(&Q)";
			this.btUnSelectAll.Click += new System.EventHandler(this.btUnSelectAll_Click);
			// 
			// listView1
			// 
			this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																					  listViewItem1,
																					  listViewItem2});
			this.listView1.Location = new System.Drawing.Point(24, 136);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(160, 120);
			this.listView1.TabIndex = 5;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.Visible = false;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(288, 288);
			this.button1.Name = "button1";
			this.button1.TabIndex = 2;
			this.button1.Text = "应用(&B)";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// LayerManagerDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(370, 383);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.checkedListBox1);
			this.Controls.Add(this.btSave);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btCancel);
			this.Controls.Add(this.btAdd);
			this.Controls.Add(this.btModify);
			this.Controls.Add(this.btDelete);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btGoUp);
			this.Controls.Add(this.btGoNext);
			this.Controls.Add(this.btSelectAll);
			this.Controls.Add(this.btUnSelectAll);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LayerManagerDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "图层管理";
			this.Load += new System.EventHandler(this.LayerManagerDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 数据初始化
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			if(symbolDoc !=null)
			{
				int num1 =0;
				foreach(Layer layer in symbolDoc.Layers)
				{
					
					string strLayerID=layer.ID;
					this.checkedListBox1.Items.Add(layer,layer.Visible);
					if(strLayerID==SvgDocument.currentLayer){
						this.checkedListBox1.SelectedIndex=num1;
					}
					num1++;					
				}
			}
		}

		/// <summary>
		/// 确认
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btSave_Click(object sender, System.EventArgs e)
		{

			if(checkedListBox1.SelectedItem==null )
			{
				MessageBox.Show("请选择当前图层.","提示");
				return;
			}
			if(!this.checkedListBox1.GetItemChecked(checkedListBox1.SelectedIndex))
			{
				MessageBox.Show("当前图层不可隐藏.","提示");
				return;
			}

			SvgDocument.currentLayer=(checkedListBox1.SelectedItem as Layer).ID;

			for(int i=0;i <this.checkedListBox1.Items.Count;i++)
			{
				(this.checkedListBox1.Items[i] as Layer).Visible = this.checkedListBox1.GetItemChecked(i);
			}			
			this.DialogResult = DialogResult.OK;
			Close();
		}		

		private void LayerManagerDialog_Load(object sender, System.EventArgs e)
		{
		
		}

		/// <summary>
		/// 增加
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btAdd_Click(object sender, System.EventArgs e)
		{
			InputDialog dlg =new InputDialog();
			if(dlg.ShowDialog(this) == DialogResult.OK)
			{
				if(Layer.CkLayerExist(dlg.InputString,this.SymbolDoc))
				{
					MessageBox.Show("文档中已经存在同名图层。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
					return;
				}
				this.checkedListBox1.Items.Add(Layer.CreateNew(dlg.InputString,this.SymbolDoc),true);
			}		
		}
	
		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btModify_Click(object sender, System.EventArgs e)
		{
			if (this.checkedListBox1.SelectedIndex>=0 && this.checkedListBox1.SelectedIndex <this.checkedListBox1.Items.Count)
			{
				Layer layer = this.checkedListBox1.Items[this.checkedListBox1.SelectedIndex] as Layer;
				if(!CkRight(layer))
				{
					MessageBox.Show("基础图层不能改名或删除。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
					return;
				}
				InputDialog dlg =new InputDialog();
				dlg.InputString = layer.Label;
				if(dlg.ShowDialog(this) == DialogResult.OK)
				{
					layer.Label = dlg.InputString;
				}		
			}
		}
		private bool CkRight(Layer lar)
		{
			if(lar.Label=="背景层" /*|| lar.Label=="规划层" || lar.Label=="统计层"*/)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btDelete_Click(object sender, System.EventArgs e)
		{
			int index = this.checkedListBox1.SelectedIndex;
			if (index>=0 && index <this.checkedListBox1.Items.Count && this.checkedListBox1.Items.Count>1)
			{
				Layer layer = this.checkedListBox1.Items[this.checkedListBox1.SelectedIndex] as Layer;
				if(!CkRight(layer))
				{
					MessageBox.Show("基础图层不能改名或删除。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
					return;
				}
				if (layer.GraphList.Count>0)
				{
					MessageBox.Show(this,"此图层下有图元，不可删除！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
					return ;
				}
				if(MessageBox.Show(this,"是否删除图层："+layer.Label+"?","请确认",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
				{
					//在文档中移除
					layer.Remove();
					//在列表中移除
					this.checkedListBox1.Items.Remove(layer);
					layer =null;					
					index = index==0?0:index -1;
					checkedListBox1.SelectedIndex =index;
				}
			}
		}
		private void btGoUp_Click(object sender, System.EventArgs e)
		{
			int index = checkedListBox1.SelectedIndex;
			if (index>0)
			{
				Layer layer = checkedListBox1.SelectedItem as Layer;
			
				bool flag = checkedListBox1.GetItemChecked(index);
				layer.GoUp();
				checkedListBox1.Items.Remove(layer);
				checkedListBox1.Items.Insert(index -1,layer);	
				checkedListBox1.SetItemChecked(index-1,flag);
				checkedListBox1.SelectedItem = layer;
			}
		}
		private void btGoNext_Click(object sender, System.EventArgs e)
		{
			int index = checkedListBox1.SelectedIndex;
			if (index>=0 && index <checkedListBox1.Items.Count -1)
			{
				Layer layer = checkedListBox1.SelectedItem as Layer;
				bool flag = checkedListBox1.GetItemChecked(index);
				layer.GoDown();
				checkedListBox1.Items.Remove(layer);
				checkedListBox1.Items.Insert(index+1,layer);				
				checkedListBox1.SetItemChecked(index+1,flag);
				checkedListBox1.SelectedItem = layer;
			}
		}
		private void btUnSelectAll_Click(object sender, System.EventArgs e)
		{
			for(int i=0; i<checkedListBox1.Items.Count;i++)
			{
				checkedListBox1.SetItemChecked(i,false);
			}
		}
		private void btSelectAll_Click(object sender, System.EventArgs e)
		{
			for(int i=0; i<checkedListBox1.Items.Count;i++)
			{
				checkedListBox1.SetItemChecked(i,true);
			}	
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if(checkedListBox1.SelectedItem==null )
			{
				MessageBox.Show("请选择当前图层.","提示");
				return;
			}
			if(!this.checkedListBox1.GetItemChecked(checkedListBox1.SelectedIndex))
			{
				MessageBox.Show("当前图层不可隐藏.","提示");
				return;
			}

			SvgDocument.currentLayer=(checkedListBox1.SelectedItem as Layer).ID;

			for(int i=0;i <this.checkedListBox1.Items.Count;i++)
			{
				(this.checkedListBox1.Items[i] as Layer).Visible = this.checkedListBox1.GetItemChecked(i);
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
