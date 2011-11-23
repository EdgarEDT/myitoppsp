using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using ItopVector.Resource;
using ItopVector.Core;
using ItopVector.Core.Func;
using ItopVector.Core.Interface.Figure;
using System.Xml;
using System.Drawing.Text;
using ItopVector.Core.Paint;


namespace ItopVector.Dialog
{
	public class NormalDesign : UserControl
	{
		// Methods
		public NormalDesign()
		{
			decimal num1;
			decimal num2;
			this.components = null;
			this.preferenceDocument = null;
			this.InitializeComponent();
			this.numericWidth.Minimum = num1 = new decimal(2);
			this.numericHeight.Minimum = num1;
			this.numericWidth.Maximum = num2 = new decimal(0x98967f);
			this.numericHeight.Maximum = num2;
			this.UpdateLabel();
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
			this.label2 = new System.Windows.Forms.Label();
			this.lblDrawAnim = new System.Windows.Forms.Label();
			this.chkAntiAlias = new System.Windows.Forms.CheckBox();
			this.chkHighQuality = new System.Windows.Forms.CheckBox();
			this.numericWidth = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lbwidth = new System.Windows.Forms.Label();
			this.numericHeight = new System.Windows.Forms.NumericUpDown();
			this.lbheight = new System.Windows.Forms.Label();
			this.lbfilesize = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.chbShowConnectPoint = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.numericWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericHeight)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label2.Location = new System.Drawing.Point(72, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(232, 2);
			this.label2.TabIndex = 5;
			// 
			// lblDrawAnim
			// 
			this.lblDrawAnim.Location = new System.Drawing.Point(8, 8);
			this.lblDrawAnim.Name = "lblDrawAnim";
			this.lblDrawAnim.Size = new System.Drawing.Size(56, 23);
			this.lblDrawAnim.TabIndex = 4;
			this.lblDrawAnim.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chkAntiAlias
			// 
			this.chkAntiAlias.Location = new System.Drawing.Point(24, 56);
			this.chkAntiAlias.Name = "chkAntiAlias";
			this.chkAntiAlias.Size = new System.Drawing.Size(240, 24);
			this.chkAntiAlias.TabIndex = 7;
			// 
			// chkHighQuality
			// 
			this.chkHighQuality.Location = new System.Drawing.Point(24, 32);
			this.chkHighQuality.Name = "chkHighQuality";
			this.chkHighQuality.Size = new System.Drawing.Size(240, 24);
			this.chkHighQuality.TabIndex = 6;
			// 
			// numericWidth
			// 
			this.numericWidth.Location = new System.Drawing.Point(64, 184);
			this.numericWidth.Maximum = new System.Decimal(new int[] {
																		 5,
																		 0,
																		 0,
																		 0});
			this.numericWidth.Minimum = new System.Decimal(new int[] {
																		 5,
																		 0,
																		 0,
																		 0});
			this.numericWidth.Name = "numericWidth";
			this.numericWidth.Size = new System.Drawing.Size(88, 21);
			this.numericWidth.TabIndex = 16;
			this.numericWidth.Value = new System.Decimal(new int[] {
																	   5,
																	   0,
																	   0,
																	   0});
			this.numericWidth.Visible = false;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(160, 184);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(24, 23);
			this.label6.TabIndex = 15;
			this.label6.Text = "px";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label6.Visible = false;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(160, 152);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(24, 23);
			this.label5.TabIndex = 14;
			this.label5.Text = "px";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label5.Visible = false;
			// 
			// lbwidth
			// 
			this.lbwidth.Location = new System.Drawing.Point(8, 184);
			this.lbwidth.Name = "lbwidth";
			this.lbwidth.Size = new System.Drawing.Size(48, 23);
			this.lbwidth.TabIndex = 13;
			this.lbwidth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbwidth.Visible = false;
			// 
			// numericHeight
			// 
			this.numericHeight.Location = new System.Drawing.Point(64, 152);
			this.numericHeight.Maximum = new System.Decimal(new int[] {
																		  5,
																		  0,
																		  0,
																		  0});
			this.numericHeight.Minimum = new System.Decimal(new int[] {
																		  5,
																		  0,
																		  0,
																		  0});
			this.numericHeight.Name = "numericHeight";
			this.numericHeight.Size = new System.Drawing.Size(88, 21);
			this.numericHeight.TabIndex = 12;
			this.numericHeight.Value = new System.Decimal(new int[] {
																		5,
																		0,
																		0,
																		0});
			this.numericHeight.Visible = false;
			// 
			// lbheight
			// 
			this.lbheight.Location = new System.Drawing.Point(8, 152);
			this.lbheight.Name = "lbheight";
			this.lbheight.Size = new System.Drawing.Size(48, 23);
			this.lbheight.TabIndex = 11;
			this.lbheight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbheight.Visible = false;
			// 
			// lbfilesize
			// 
			this.lbfilesize.Location = new System.Drawing.Point(8, 120);
			this.lbfilesize.Name = "lbfilesize";
			this.lbfilesize.TabIndex = 10;
			this.lbfilesize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lbfilesize.Visible = false;
			// 
			// label4
			// 
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label4.Location = new System.Drawing.Point(104, 128);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(200, 2);
			this.label4.TabIndex = 9;
			this.label4.Visible = false;
			// 
			// chbShowConnectPoint
			// 
			this.chbShowConnectPoint.Location = new System.Drawing.Point(24, 80);
			this.chbShowConnectPoint.Name = "chbShowConnectPoint";
			this.chbShowConnectPoint.Size = new System.Drawing.Size(240, 24);
			this.chbShowConnectPoint.TabIndex = 7;
			this.chbShowConnectPoint.Text = "显示连接点";
			// 
			// NormalDesign
			// 
			this.Controls.Add(this.numericWidth);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.lbwidth);
			this.Controls.Add(this.numericHeight);
			this.Controls.Add(this.lbheight);
			this.Controls.Add(this.lbfilesize);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.chkAntiAlias);
			this.Controls.Add(this.chkHighQuality);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lblDrawAnim);
			this.Controls.Add(this.chbShowConnectPoint);
			this.Name = "NormalDesign";
			this.Size = new System.Drawing.Size(320, 328);
			((System.ComponentModel.ISupportInitialize)(this.numericWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericHeight)).EndInit();
			this.ResumeLayout(false);

		}
 
		public void SaveProperty()
		{
			if (this.preferenceDocument != null)
			{
				XmlNode node1;
				node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='HighQuality']");
				if (node1 is XmlElement)
				{
					((XmlElement) node1).SetAttribute("Value", this.chkHighQuality.Checked.ToString().ToLower());
				}
				node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='AntiAlias']");
				if (node1 is XmlElement)
				{
					((XmlElement) node1).SetAttribute("Value", this.chkAntiAlias.Checked.ToString().ToLower());
				}
				node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='ConnectPoints']");
				if (node1 is XmlElement)
				{
					((XmlElement) node1).SetAttribute("Value", this.chbShowConnectPoint.Checked.ToString().ToLower());
				}
			}
		}
 
		private void UpdateLabel()
		{
			this.chkAntiAlias.Text = Preference.GetLabelForName("antialias").Trim();
			this.chkHighQuality.Text = Preference.GetLabelForName("highquality").Trim();
			this.lblDrawAnim.Text = Preference.GetLabelForName("drawconfig").Trim();
			this.lbfilesize.Text = Preference.GetLabelForName("filesize").Trim();
			this.lbwidth.Text = Preference.GetLabelForName("filewidth").Trim();
			this.lbheight.Text = Preference.GetLabelForName("fileheight").Trim();
		}
 

		// Properties
		public XmlDocument PreferenceDocument
		{
			set
			{
				if (this.preferenceDocument != value)
				{
					this.preferenceDocument = value;
					if (value != null)
					{
						XmlNode node1;
						node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='AntiAlias']");
						if (node1 is XmlElement)
						{
							this.chkAntiAlias.Checked = node1.Attributes["Value"].Value.Trim() == "true";
						}
						node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='HighQuality']");
						if (node1 is XmlElement)
						{
							this.chkHighQuality.Checked = node1.Attributes["Value"].Value.Trim() == "true";
						}
						node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='ConnectPoints']");
						if (node1 is XmlElement)
						{
							this.chbShowConnectPoint.Checked = node1.Attributes["Value"].Value.Trim() == "true";
						}

						node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='FileWidth']");
						int num1 = 400;
						if (node1 is XmlElement)
						{
							try
							{
								string text1 = ((XmlElement) node1).GetAttribute("Value").Trim();
								if (text1.Length > 0)
								{
									num1 = (int) float.Parse(text1);
								}
							}
							catch (Exception)
							{
							}
						}
						this.numericWidth.Value = (decimal) num1;
						node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='FileHeight']");
						num1 = 300;
						if (node1 is XmlElement)
						{
							try
							{
								string text2 = ((XmlElement) node1).GetAttribute("Value").Trim();
								if (text2.Length > 0)
								{
									num1 = (int) float.Parse(text2);
								}
							}
							catch (Exception)
							{
							}
						}
						this.numericHeight.Value = (decimal) num1;
					}
				}
			}
		}
 

		// Fields
		private CheckBox chkAntiAlias;
		private CheckBox chkHighQuality;
		private Container components;
		private Label label2;
		private Label label4;
		private Label label5;
		private Label label6;
		private Label lbfilesize;
		private Label lbheight;
		private Label lblDrawAnim;
		private Label lbwidth;
		private System.Windows.Forms.NumericUpDown numericHeight;
		private System.Windows.Forms.NumericUpDown numericWidth;
		private System.Windows.Forms.CheckBox chbShowConnectPoint;
		private XmlDocument preferenceDocument;
	}
 
}
