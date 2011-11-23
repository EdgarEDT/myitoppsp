using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ItopVectorDraw
{
	/// <summary>
	/// ����������ʱ���ṩ���ò����Ĺ�����壬��Ҫ��������VectorControl��Star������ز���
	/// </summary>
	public class StarOption : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		internal System.Windows.Forms.NumericUpDown radialPicker;
		internal System.Windows.Forms.NumericUpDown IndentPicker;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StarOption()
		{
			InitializeComponent();
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

		#region �����������ɵĴ���
		/// <summary> 
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			this.radialPicker = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.IndentPicker = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.radialPicker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.IndentPicker)).BeginInit();
			this.SuspendLayout();
			// 
			// radialPicker
			// 
			this.radialPicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.radialPicker.Location = new System.Drawing.Point(72, 8);
			this.radialPicker.Maximum = new System.Decimal(new int[] {
																		 20,
																		 0,
																		 0,
																		 0});
			this.radialPicker.Minimum = new System.Decimal(new int[] {
																		 3,
																		 0,
																		 0,
																		 0});
			this.radialPicker.Name = "radialPicker";
			this.radialPicker.Size = new System.Drawing.Size(64, 21);
			this.radialPicker.TabIndex = 3;
			this.radialPicker.Value = new System.Decimal(new int[] {
																	   3,
																	   0,
																	   0,
																	   0});
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "��    ��";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// IndentPicker
			// 
			this.IndentPicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.IndentPicker.Location = new System.Drawing.Point(72, 40);
			this.IndentPicker.Minimum = new System.Decimal(new int[] {
																		 5,
																		 0,
																		 0,
																		 0});
			this.IndentPicker.Name = "IndentPicker";
			this.IndentPicker.Size = new System.Drawing.Size(64, 21);
			this.IndentPicker.TabIndex = 5;
			this.IndentPicker.Value = new System.Decimal(new int[] {
																	   100,
																	   0,
																	   0,
																	   0});
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 23);
			this.label2.TabIndex = 4;
			this.label2.Text = "�����뾶";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(136, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(16, 23);
			this.label3.TabIndex = 6;
			this.label3.Text = "%";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// StarOption
			// 
			this.Controls.Add(this.label3);
			this.Controls.Add(this.IndentPicker);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.radialPicker);
			this.Controls.Add(this.label1);
			this.Name = "StarOption";
			this.Size = new System.Drawing.Size(152, 72);
			((System.ComponentModel.ISupportInitialize)(this.radialPicker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.IndentPicker)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
	}
}
