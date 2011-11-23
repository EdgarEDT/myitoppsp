using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using CADImport;

namespace Itop.CADConvert
{
	/// <summary>
	/// frmLayout 的摘要说明。
	/// </summary>
	public class frmLayout : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button btCancel;
		private System.Windows.Forms.Button btOK;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ColumnHeader lay1;
		private System.Windows.Forms.ColumnHeader layName;
		private System.Windows.Forms.Button btSelectAll;
		private System.Windows.Forms.Button btUnSelectAll;
		public ArrayList Layouts;

		
		public frmLayout(CADLayoutCollection layouts)
		{
			InitializeComponent();

			Layouts = new ArrayList();
			
			foreach(CADLayout layout in layouts)
			{
//				if(layout.PaperSpaceBlock==null)continue;
				ListViewItem item = listView1.Items.Add("");
				item.StateImageIndex=1;
				item.Tag = layout;
				item.SubItems.Add(layout.Name);
			}
			listView1.FullRowSelect =true;
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmLayout));
			this.listView1 = new System.Windows.Forms.ListView();
			this.lay1 = new System.Windows.Forms.ColumnHeader();
			this.layName = new System.Windows.Forms.ColumnHeader();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.btCancel = new System.Windows.Forms.Button();
			this.btOK = new System.Windows.Forms.Button();
			this.btSelectAll = new System.Windows.Forms.Button();
			this.btUnSelectAll = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.lay1,
																						this.layName});
			this.listView1.Location = new System.Drawing.Point(8, 8);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(336, 264);
			this.listView1.StateImageList = this.imageList1;
			this.listView1.TabIndex = 0;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseUp);
			this.listView1.Click += new System.EventHandler(this.listView1_Click);
			// 
			// lay1
			// 
			this.lay1.Text = "";
			this.lay1.Width = 30;
			// 
			// layName
			// 
			this.layName.Text = "布局名称";
			this.layName.Width = 290;
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btCancel
			// 
			this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Location = new System.Drawing.Point(272, 280);
			this.btCancel.Name = "btCancel";
			this.btCancel.TabIndex = 1;
			this.btCancel.Text = "取消";
			// 
			// btOK
			// 
			this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btOK.Location = new System.Drawing.Point(192, 280);
			this.btOK.Name = "btOK";
			this.btOK.TabIndex = 1;
			this.btOK.Text = "确认";
			this.btOK.Click += new System.EventHandler(this.btOK_Click);
			// 
			// btSelectAll
			// 
			this.btSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btSelectAll.Location = new System.Drawing.Point(8, 280);
			this.btSelectAll.Name = "btSelectAll";
			this.btSelectAll.Size = new System.Drawing.Size(56, 23);
			this.btSelectAll.TabIndex = 1;
			this.btSelectAll.Text = "全选";
			this.btSelectAll.Click += new System.EventHandler(this.btSelectAll_Click);
			// 
			// btUnSelectAll
			// 
			this.btUnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btUnSelectAll.Location = new System.Drawing.Point(72, 280);
			this.btUnSelectAll.Name = "btUnSelectAll";
			this.btUnSelectAll.Size = new System.Drawing.Size(56, 23);
			this.btUnSelectAll.TabIndex = 1;
			this.btUnSelectAll.Text = "全消";
			this.btUnSelectAll.Click += new System.EventHandler(this.btUnSelectAll_Click);
			// 
			// frmLayout
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(352, 317);
			this.Controls.Add(this.btCancel);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.btOK);
			this.Controls.Add(this.btSelectAll);
			this.Controls.Add(this.btUnSelectAll);
			this.Name = "frmLayout";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "选择导入布局列表";
			this.Load += new System.EventHandler(this.frmLayout_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmLayout_Load(object sender, System.EventArgs e)
		{
		
		}

		private void listView1_Click(object sender, System.EventArgs e)
		{

		}

		private void listView1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			ListViewItem item = listView1.GetItemAt(e.X,e.Y);
			if(item==null)return;
			if(e.X<17)
				item.StateImageIndex = item.StateImageIndex ==1?0:1;
		}

		private void btOK_Click(object sender, System.EventArgs e)
		{
			foreach(ListViewItem item in listView1.Items)
			{
				if(item.StateImageIndex==1)
					Layouts.Add(item.Index);
			}
			DialogResult  = DialogResult.OK;
			
		}

		private void btSelectAll_Click(object sender, System.EventArgs e)
		{
			foreach(ListViewItem item in listView1.Items)
			{
				item.StateImageIndex=1;
			}
		}

		private void btUnSelectAll_Click(object sender, System.EventArgs e)
		{
			foreach(ListViewItem item in listView1.Items)
			{
				item.StateImageIndex=0;
			}
		}
	}
}
