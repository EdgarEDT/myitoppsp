using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ItopVector.Dialog
{
	/// <summary>
	/// NewConnectDialog ��ժҪ˵����
	/// </summary>
	public class InputDialog : Form
	{
		private Label label5;
		private TextBox tbName;
		private Button btOk;
		private Button btCancel;
		private String inputText;
		private System.Windows.Forms.Label label1;
		/// <summary>
		/// ����������������
		/// </summary>
		private Container components = null;

		public int inputLength;

		public InputDialog()
		{
			//
			// Windows ���������֧���������
			//
			inputText=string.Empty;
			inputLength=100;
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
			this.tbName = new System.Windows.Forms.TextBox();
			this.btOk = new System.Windows.Forms.Button();
			this.btCancel = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tbName
			// 
			this.tbName.Location = new System.Drawing.Point(8, 8);
			this.tbName.MaxLength = 200;
			this.tbName.Multiline = true;
			this.tbName.Name = "tbName";
			this.tbName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbName.Size = new System.Drawing.Size(272, 136);
			this.tbName.TabIndex = 0;
			this.tbName.Text = "";
			// 
			// btOk
			// 
			this.btOk.Location = new System.Drawing.Point(152, 168);
			this.btOk.Name = "btOk";
			this.btOk.Size = new System.Drawing.Size(56, 22);
			this.btOk.TabIndex = 1;
			this.btOk.Text = "ȷ��(&O)";
			this.btOk.Click += new System.EventHandler(this.button2_Click);
			// 
			// btCancel
			// 
			this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.btCancel.Location = new System.Drawing.Point(216, 168);
			this.btCancel.Name = "btCancel";
			this.btCancel.Size = new System.Drawing.Size(56, 22);
			this.btCancel.TabIndex = 3;
			this.btCancel.Text = "ȡ��(&C)";
			// 
			// label5
			// 
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label5.Location = new System.Drawing.Point(8, 152);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(272, 2);
			this.label5.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "�����룺";
			// 
			// InputDialog
			// 
			this.AcceptButton = this.btOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(292, 207);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.tbName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btOk);
			this.Controls.Add(this.btCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InputDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "�����ı�";
			this.Load += new System.EventHandler(this.InputDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad (e);
			this.tbName.Text=this.inputText;
			this.tbName.MaxLength=this.inputLength;
		}

		
		private void button2_Click(object sender, EventArgs e)
		{
			if (this.tbName.Text.Trim()==string.Empty)
				return;
			this.InputString=this.tbName.Text;

			this.DialogResult=DialogResult.OK;
			this.Close();
		}

		private void InputDialog_Load(object sender, System.EventArgs e)
		{
		
		}

		public string InputString
		{

			get
			{
				return inputText;
			}
			set
			{
				this.inputText=value;
			}
		}

		public int InputLength
		{
			get { return this.inputLength; }
			set {this.inputLength=value;}
		}
	}
}
