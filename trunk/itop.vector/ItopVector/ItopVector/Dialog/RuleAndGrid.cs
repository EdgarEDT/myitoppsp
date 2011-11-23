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

 
namespace ItopVector.Dialog
{
	public class RuleAndGrid : UserControl
	{
		// Methods
		public RuleAndGrid()
		{
			this.components = null;
			this.preferenceDocument = null;
			this.InitializeComponent();
			this.UpdateLabel();
		}
 
		private void chkShowGuide_CheckedChanged(object sender, EventArgs e)
		{
			this.chkLockGuide.Enabled = this.chkShowGuide.Checked;
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
			this.boxRule = new GroupBox();
			this.chkShowRule = new CheckBox();
			this.boxGrid = new GroupBox();
			this.txtGridHeight = new TextBox();
			this.lblGridHeight = new Label();
			this.txtGridWidth = new TextBox();
			this.lblGridWidth = new Label();
			this.chkSnapToGrid = new CheckBox();
			this.chkShowGrid = new CheckBox();
			this.chkShowGuide = new CheckBox();
			this.label1 = new Label();
			this.chkLockGuide = new CheckBox();
			this.boxRule.SuspendLayout();
			this.boxGrid.SuspendLayout();
			base.SuspendLayout();
			this.boxRule.Anchor = AnchorStyles.Right | (AnchorStyles.Left | AnchorStyles.Top);
			Control[] controlArray1 = new Control[1] { this.chkShowRule } ;
			this.boxRule.Controls.AddRange(controlArray1);
			this.boxRule.Location = new Point(8, 8);
			this.boxRule.Name = "boxRule";
			this.boxRule.Size = new Size(0x100, 0x30);
			this.boxRule.TabIndex = 0;
			this.boxRule.TabStop = false;
			this.chkShowRule.Location = new Point(0x10, 0x10);
			this.chkShowRule.Name = "chkShowRule";
			this.chkShowRule.Size = new Size(200, 0x18);
			this.chkShowRule.TabIndex = 0;
			this.boxGrid.Anchor = AnchorStyles.Right | (AnchorStyles.Left | AnchorStyles.Top);
			Control[] controlArray2 = new Control[6] { this.txtGridHeight, this.lblGridHeight, this.txtGridWidth, this.lblGridWidth, this.chkSnapToGrid, this.chkShowGrid } ;
			this.boxGrid.Controls.AddRange(controlArray2);
			this.boxGrid.Location = new Point(8, 0x48);
			this.boxGrid.Name = "boxGrid";
			this.boxGrid.Size = new Size(0x100, 80);
			this.boxGrid.TabIndex = 1;
			this.boxGrid.TabStop = false;
			this.txtGridHeight.Anchor = AnchorStyles.Right | AnchorStyles.Top;
			this.txtGridHeight.Location = new Point(0xc0, 0x30);
			this.txtGridHeight.Name = "txtGridHeight";
			this.txtGridHeight.Size = new Size(0x38, 0x15);
			this.txtGridHeight.TabIndex = 5;
			this.txtGridHeight.Text = "";
			this.lblGridHeight.Anchor = AnchorStyles.Right | AnchorStyles.Top;
			this.lblGridHeight.Location = new Point(0x88, 0x30);
			this.lblGridHeight.Name = "lblGridHeight";
			this.lblGridHeight.Size = new Size(0x30, 0x17);
			this.lblGridHeight.TabIndex = 4;
			this.lblGridHeight.TextAlign = ContentAlignment.MiddleLeft;
			this.txtGridWidth.Location = new Point(0x40, 0x30);
			this.txtGridWidth.Name = "txtGridWidth";
			this.txtGridWidth.Size = new Size(0x38, 0x15);
			this.txtGridWidth.TabIndex = 3;
			this.txtGridWidth.Text = "";
			this.lblGridWidth.Location = new Point(0x10, 0x30);
			this.lblGridWidth.Name = "lblGridWidth";
			this.lblGridWidth.Size = new Size(40, 0x17);
			this.lblGridWidth.TabIndex = 2;
			this.lblGridWidth.TextAlign = ContentAlignment.MiddleLeft;
			this.chkSnapToGrid.Anchor = AnchorStyles.Right | AnchorStyles.Top;
			this.chkSnapToGrid.Location = new Point(0x88, 0x10);
			this.chkSnapToGrid.Name = "chkSnapToGrid";
			this.chkSnapToGrid.Size = new Size(0x70, 0x18);
			this.chkSnapToGrid.TabIndex = 1;
			this.chkShowGrid.Location = new Point(0x10, 0x10);
			this.chkShowGrid.Name = "chkShowGrid";
			this.chkShowGrid.Size = new Size(80, 0x18);
			this.chkShowGrid.TabIndex = 0;
			this.chkShowGuide.Location = new Point(8, 160);
			this.chkShowGuide.Name = "chkShowGuide";
			this.chkShowGuide.Size = new Size(120, 0x18);
			this.chkShowGuide.TabIndex = 2;
			this.chkShowGuide.CheckedChanged += new EventHandler(this.chkShowGuide_CheckedChanged);
			this.label1.Anchor = AnchorStyles.Right | (AnchorStyles.Left | AnchorStyles.Top);
			this.label1.BorderStyle = BorderStyle.Fixed3D;
			this.label1.Location = new Point(0x68, 0xa8);
			this.label1.Name = "label1";
			this.label1.Size = new Size(0x9e, 2);
			this.label1.TabIndex = 3;
			this.chkLockGuide.Enabled = false;
			this.chkLockGuide.Location = new Point(0x18, 0xc0);
			this.chkLockGuide.Name = "chkLockGuide";
			this.chkLockGuide.TabIndex = 4;
			Control[] controlArray3 = new Control[5] { this.chkShowGuide, this.chkLockGuide, this.label1, this.boxGrid, this.boxRule } ;
			base.Controls.AddRange(controlArray3);
			base.Name = "RuleAndGrid";
			base.Size = new Size(0x110, 0x160);
			this.boxRule.ResumeLayout(false);
			this.boxGrid.ResumeLayout(false);
			base.ResumeLayout(false);
		}
 
		public void SaveProperty()
		{
			if (this.preferenceDocument != null)
			{
				XmlNode node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='ShowRule']");
				if (node1 is XmlElement)
				{
					((XmlElement) node1).SetAttribute("Value", this.chkShowRule.Checked.ToString().ToLower());
				}
				node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='ShowGrid']");
				if (node1 is XmlElement)
				{
					((XmlElement) node1).SetAttribute("Value", this.chkShowGrid.Checked.ToString().ToLower());
				}
				node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='SnapToGrid']");
				if (node1 is XmlElement)
				{
					string text1 = this.chkSnapToGrid.Checked.ToString().ToLower();
					((XmlElement) node1).SetAttribute("Value", text1);
				}
				node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='ShowGuide']");
				if (node1 is XmlElement)
				{
					((XmlElement) node1).SetAttribute("Value", this.chkShowGuide.Checked.ToString().ToLower());
				}
				node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='LockGuide']");
				if (node1 is XmlElement)
				{
					((XmlElement) node1).SetAttribute("Value", this.chkLockGuide.Text);
				}
				node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='GridWidth']");
				if (node1 is XmlElement)
				{
					try
					{
						float single1 = Number.parseToFloat(this.txtGridWidth.Text.Trim(), null, SvgLengthDirection.Horizontal);
						((XmlElement) node1).SetAttribute("Value", single1.ToString() + "px");
					}
					catch (Exception)
					{
					}
				}
				node1 = this.preferenceDocument.DocumentElement.SelectSingleNode("//*[@id='GridHeight']");
				if (node1 is XmlElement)
				{
					try
					{
						float single2 = Number.parseToFloat(this.txtGridHeight.Text.Trim(), null, SvgLengthDirection.Horizontal);
						((XmlElement) node1).SetAttribute("Value", single2.ToString() + "px");
					}
					catch (Exception)
					{
						return;
					}
				}
			}
		}
 
		private void UpdateLabel()
		{
			this.lblGridHeight.Text = Preference.GetLabelForName("gridheight").Trim();
			this.lblGridWidth.Text = Preference.GetLabelForName("gridwidth").Trim();
			this.chkLockGuide.Text = Preference.GetLabelForName("lockguide").Trim();
			this.chkShowGrid.Text = Preference.GetLabelForName("showgrid").Trim();
			this.chkSnapToGrid.Text = Preference.GetLabelForName("snaptogrid").Trim();
			this.chkShowRule.Text = Preference.GetLabelForName("showrule").Trim();
			this.chkShowGuide.Text = Preference.GetLabelForName("guideconfig").Trim();
			this.boxGrid.Text = Preference.GetLabelForName("gridconfig").Trim();
			this.boxRule.Text = Preference.GetLabelForName("ruleconfig").Trim();
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
						XmlNode node1 = value.DocumentElement.SelectSingleNode("//*[@id='ShowRule']");
						if (node1 != null)
						{
							this.chkShowRule.Checked = node1.Attributes["Value"].Value == "true";
						}
						node1 = value.DocumentElement.SelectSingleNode("//*[@id='ShowGrid']");
						if (node1 != null)
						{
							this.chkShowGrid.Checked = node1.Attributes["Value"].Value == "true";
						}
						node1 = value.DocumentElement.SelectSingleNode("//*[@id='SnapToGrid']");
						if (node1 != null)
						{
							this.chkSnapToGrid.Checked = node1.Attributes["Value"].Value == "true";
						}
						node1 = value.DocumentElement.SelectSingleNode("//*[@id='GridWidth']");
						if (node1 != null)
						{
							this.txtGridWidth.Text = node1.Attributes["Value"].Value;
						}
						node1 = value.DocumentElement.SelectSingleNode("//*[@id='GridHeight']");
						if (node1 != null)
						{
							this.txtGridHeight.Text = node1.Attributes["Value"].Value;
						}
						node1 = value.DocumentElement.SelectSingleNode("//*[@id='ShowGuide']");
						if (node1 != null)
						{
							this.chkShowGuide.Checked = node1.Attributes["Value"].Value == "true";
						}
						node1 = value.DocumentElement.SelectSingleNode("//*[@id='LockGuide']");
						if (node1 != null)
						{
							this.chkLockGuide.Checked = node1.Attributes["Value"].Value == "true";
						}
					}
				}
			}
		}
 

		// Fields
		private GroupBox boxGrid;
		private GroupBox boxRule;
		private CheckBox chkLockGuide;
		private CheckBox chkShowGrid;
		private CheckBox chkShowGuide;
		private CheckBox chkShowRule;
		private CheckBox chkSnapToGrid;
		private Container components;
		private Label label1;
		private Label lblGridHeight;
		private Label lblGridWidth;
		private XmlDocument preferenceDocument;
		private TextBox txtGridHeight;
		private TextBox txtGridWidth;
	}
 
}
