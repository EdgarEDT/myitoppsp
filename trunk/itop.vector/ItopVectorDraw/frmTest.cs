using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ItopVectorDraw
{
	/// <summary>
	/// frmTest ��ժҪ˵����
	/// </summary>
	public class frmTest : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		ItopVector.Selector.SymbolSelector symbolSelector;
		ItopVector.ItopVectorControl tlVector;
		public frmTest()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			this.symbolSelector=new ItopVector.Selector.SymbolSelector(Application.StartupPath+"\\symbol\\symbol.xml");

			this.symbolSelector.Dock = DockStyle.Fill;
			symbolSelector.SelectedChanged+=new EventHandler(symbolSelector_SelectedChanged);

			tlVector =new ItopVector.ItopVectorControl();
			tlVector.Dock = DockStyle.Fill;
			tlVector.OpenFile("c:\\svgtest.svg");

			this.panel1.Controls.Add(symbolSelector);
			this.panel2.Controls.Add(tlVector);

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
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.panel1.Location = new System.Drawing.Point(0, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 336);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Location = new System.Drawing.Point(208, 8);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(320, 336);
			this.panel2.TabIndex = 0;
			// 
			// frmTest
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(528, 357);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel2);
			this.Name = "frmTest";
			this.Text = "frmTest";
			this.ResumeLayout(false);

		}
		#endregion

		private void symbolSelector_SelectedChanged(object sender, EventArgs e)
		{
			tlVector.DrawArea.PreGraph = symbolSelector.SelectedItem.CloneNode(true) as ItopVector.Core.Interface.Figure.IGraph;
		}
	}
}
