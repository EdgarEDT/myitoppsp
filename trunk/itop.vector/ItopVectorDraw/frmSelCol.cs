using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ItopVectorDraw
{
	/// <summary>
	/// frmSelCol ��ժҪ˵����
	/// </summary>
	public class frmSelCol : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		public ArrayList list;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSelCol()  
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(136, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "����ڸ����豸���ƣ�";
			// 
			// listBox1
			// 
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(8, 32);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(328, 160);
			this.listBox1.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 208);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(136, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "����ڷ�λ��";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(8, 232);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(328, 21);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = "";
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(208, 264);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(56, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "ȷ��";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(280, 264);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(56, 23);
			this.button2.TabIndex = 4;
			this.button2.Text = "ȡ��";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// frmSelCol
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(344, 301);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSelCol";
			this.Text = "���ó��ڵ�λ�� ";
			this.Load += new System.EventHandler(this.frmSelCol_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmSelCol_Load(object sender, System.EventArgs e)
		{
			int i=list.Count;
			IEnumerator IEmu=list.GetEnumerator();
			while (IEmu.MoveNext())
			{
				if(IEmu.Current.ToString()!="")
				{
					listBox1.Items.Add(IEmu.Current);
				}
			}
//			frmLogNotes f=new frmLogNotes();
//			f.Show();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			
			
		}
			
	}
}
