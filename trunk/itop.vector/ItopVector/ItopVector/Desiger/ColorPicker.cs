using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
//using DevComponents.DotNetBar;

namespace ItopVector.Desiger
{
	/// <summary>
	/// Summary description for ColorPicker.
	/// </summary>
	public class ColorPicker : UserControl
	{
		private Color[] m_CustomColors = new Color[48];
		private Rectangle[] m_CustomColorsPos = new Rectangle[48];
		private ListBox listWeb;
		private ListBox listSystem;
		private IContainer components;
//		private TabItem tabCustom;
//		private TabItem tabWeb;
//		private TabItem tabSystem;
		private Panel colorPanel;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;

		private Color selectedColor = Color.Empty;

		
		public Color SelectedColor
		{
			get { return this.selectedColor; }
			set
			{
				if (this.selectedColor == value) return;
				this.selectedColor = value;
				ColorSelected();
			}
		}

		public ColorPicker()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			InitCustomColors();
			InitOtherColors();
		}

		private void InitCustomColors()
		{
			m_CustomColors[0] = Color.FromArgb(255, 255, 255);
			m_CustomColors[1] = Color.FromArgb(255, 195, 198);
			m_CustomColors[2] = Color.FromArgb(255, 227, 198);
			m_CustomColors[3] = Color.FromArgb(255, 255, 198);
			m_CustomColors[4] = Color.FromArgb(198, 255, 198);
			m_CustomColors[5] = Color.FromArgb(198, 255, 255);
			m_CustomColors[6] = Color.FromArgb(198, 195, 255);
			m_CustomColors[7] = Color.FromArgb(255, 195, 255);

			m_CustomColors[8] = Color.FromArgb(231, 227, 231);
			m_CustomColors[9] = Color.FromArgb(255, 130, 132);
			m_CustomColors[10] = Color.FromArgb(255, 195, 132);
			m_CustomColors[11] = Color.FromArgb(255, 255, 132);
			m_CustomColors[12] = Color.FromArgb(132, 255, 132);
			m_CustomColors[13] = Color.FromArgb(132, 255, 255);
			m_CustomColors[14] = Color.FromArgb(132, 130, 255);
			m_CustomColors[15] = Color.FromArgb(255, 130, 255);

			m_CustomColors[16] = Color.FromArgb(198, 195, 198);
			m_CustomColors[17] = Color.FromArgb(255, 0, 0);
			m_CustomColors[18] = Color.FromArgb(255, 130, 0);
			m_CustomColors[19] = Color.FromArgb(255, 255, 0);
			m_CustomColors[20] = Color.FromArgb(0, 255, 0);
			m_CustomColors[21] = Color.FromArgb(0, 255, 255);
			m_CustomColors[22] = Color.FromArgb(0, 0, 255);
			m_CustomColors[23] = Color.FromArgb(255, 0, 255);

			m_CustomColors[24] = Color.FromArgb(132, 130, 132);
			m_CustomColors[25] = Color.FromArgb(198, 0, 0);
			m_CustomColors[26] = Color.FromArgb(198, 65, 0);
			m_CustomColors[27] = Color.FromArgb(198, 195, 0);
			m_CustomColors[28] = Color.FromArgb(0, 195, 0);
			m_CustomColors[29] = Color.FromArgb(0, 195, 198);
			m_CustomColors[30] = Color.FromArgb(0, 0, 198);
			m_CustomColors[31] = Color.FromArgb(198, 0, 198);

			m_CustomColors[32] = Color.FromArgb(66, 65, 66);
			m_CustomColors[33] = Color.FromArgb(132, 0, 0);
			m_CustomColors[34] = Color.FromArgb(132, 65, 0);
			m_CustomColors[35] = Color.FromArgb(132, 130, 0);
			m_CustomColors[36] = Color.FromArgb(0, 130, 0);
			m_CustomColors[37] = Color.FromArgb(0, 130, 132);
			m_CustomColors[38] = Color.FromArgb(0, 0, 132);
			m_CustomColors[39] = Color.FromArgb(132, 0, 132);

			m_CustomColors[40] = Color.FromArgb(0, 0, 0);
			m_CustomColors[41] = Color.FromArgb(66, 0, 0);
			m_CustomColors[42] = Color.FromArgb(132, 65, 66);
			m_CustomColors[43] = Color.FromArgb(66, 65, 0);
			m_CustomColors[44] = Color.FromArgb(0, 65, 0);
			m_CustomColors[45] = Color.FromArgb(0, 65, 66);
			m_CustomColors[46] = Color.FromArgb(0, 0, 66);
			m_CustomColors[47] = Color.FromArgb(66, 0, 66);
		}

		private void InitOtherColors()
		{
			listWeb.BeginUpdate();
			listWeb.Items.Clear();
			Type type = typeof (Color);
			PropertyInfo[] fields = type.GetProperties(BindingFlags.Public | BindingFlags.Static);
			Color clr = new Color();
			foreach (PropertyInfo pi in fields)
				listWeb.Items.Add(pi.GetValue(clr, null));
			listWeb.EndUpdate();

			listSystem.BeginUpdate();
			listSystem.Items.Clear();
			type = typeof (SystemColors);
			fields = type.GetProperties(BindingFlags.Public | BindingFlags.Static);
			foreach (PropertyInfo pi in fields)
				listSystem.Items.Add(pi.GetValue(clr, null));
			listSystem.EndUpdate();
		}


		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.listSystem = new System.Windows.Forms.ListBox();
			this.listWeb = new System.Windows.Forms.ListBox();
			this.colorPanel = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.SuspendLayout();
			// 
			// listSystem
			// 
			this.listSystem.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listSystem.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listSystem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listSystem.IntegralHeight = false;
			this.listSystem.Location = new System.Drawing.Point(0, 0);
			this.listSystem.Name = "listSystem";
			this.listSystem.Size = new System.Drawing.Size(200, 159);
			this.listSystem.TabIndex = 0;
			this.listSystem.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawSystemItem);
			this.listSystem.SelectedIndexChanged += new System.EventHandler(this.SystemSelectionChange);
			// 
			// listWeb
			// 
			this.listWeb.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.listWeb.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listWeb.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listWeb.IntegralHeight = false;
			this.listWeb.Location = new System.Drawing.Point(0, 0);
			this.listWeb.Name = "listWeb";
			this.listWeb.Size = new System.Drawing.Size(200, 159);
			this.listWeb.TabIndex = 0;
			this.listWeb.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawWebItem);
			this.listWeb.SelectedIndexChanged += new System.EventHandler(this.WebSelectionChange);
			// 
			// colorPanel
			// 
			this.colorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.colorPanel.Location = new System.Drawing.Point(0, 0);
			this.colorPanel.Name = "colorPanel";
			this.colorPanel.Size = new System.Drawing.Size(200, 159);
			this.colorPanel.TabIndex = 2;
			this.colorPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CustomColorMouseUp);
			this.colorPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintCustomColors);
			// 
			// tabControl1
			// 
			this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(208, 184);
			this.tabControl1.TabIndex = 3;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.colorPanel);
			this.tabPage1.Location = new System.Drawing.Point(4, 4);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(200, 159);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "自定义";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.listWeb);
			this.tabPage2.Location = new System.Drawing.Point(4, 4);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(200, 159);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Web";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.listSystem);
			this.tabPage3.Location = new System.Drawing.Point(4, 4);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(200, 159);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "系统";
			// 
			// ColorPicker
			// 
			this.Controls.Add(this.tabControl1);
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "ColorPicker";
			this.Size = new System.Drawing.Size(208, 184);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void PaintCustomColors(object sender, PaintEventArgs e)
		{
			Rectangle r = Rectangle.Empty;
			int x = 6, y = 6;
			Graphics g = e.Graphics;
			Border3DSide side = (Border3DSide.Left | Border3DSide.Right | Border3DSide.Top | Border3DSide.Bottom);
			int width = colorPanel.ClientRectangle.Width;
			int iIndex = 0;
			foreach (Color clr in m_CustomColors)
			{
				r = new Rectangle(x, y, 21, 21);
				if (r.Right > width)
				{
					y += 25;
					x = 6;
					r.X = x;
					r.Y = y;
				}
				ControlPaint.DrawBorder3D(g, x, y, 21, 21, Border3DStyle.Sunken, side);
				r.Inflate(-2, -2);
				g.FillRectangle(new SolidBrush(clr), r);

				m_CustomColorsPos[iIndex] = r;
				iIndex++;

				x += 24;
			}
		}

		private void DrawWebItem(object sender, DrawItemEventArgs e)
		{
			Rectangle r = e.Bounds;
			Rectangle rClr = new Rectangle(r.X + 1, r.Y + 2, 24, r.Height - 4);

			Brush textbrush = SystemBrushes.ControlText;
			if ((e.State & DrawItemState.Selected) != 0)
			{
				textbrush = SystemBrushes.HighlightText;
				e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
			}
			else
				e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);

			Color clr = (Color) listWeb.Items[e.Index];
			e.Graphics.FillRectangle(new SolidBrush(clr), rClr);
			e.Graphics.DrawRectangle(SystemPens.ControlText, rClr);
			r.Offset(30, 0);
			r.Width -= 30;
			e.Graphics.DrawString(clr.Name, listWeb.Font, textbrush, r, StringFormat.GenericTypographic);
		}

		private void DrawSystemItem(object sender, DrawItemEventArgs e)
		{
			Rectangle r = e.Bounds;
			Rectangle rClr = new Rectangle(r.X + 1, r.Y + 2, 24, r.Height - 4);

			Brush textbrush = SystemBrushes.ControlText;
			if ((e.State & DrawItemState.Selected) != 0)
			{
				textbrush = SystemBrushes.HighlightText;
				e.Graphics.FillRectangle(SystemBrushes.Highlight, e.Bounds);
			}
			else
				e.Graphics.FillRectangle(SystemBrushes.Window, e.Bounds);

			Color clr = (Color) listSystem.Items[e.Index];
			e.Graphics.FillRectangle(new SolidBrush(clr), rClr);
			e.Graphics.DrawRectangle(SystemPens.ControlText, rClr);
			r.Offset(30, 0);
			r.Width -= 30;
			e.Graphics.DrawString(clr.Name, listWeb.Font, textbrush, r, StringFormat.GenericTypographic);
		}

		private void CustomColorMouseUp(object sender, MouseEventArgs e)
		{
			for (int i = 0; i < 48; i++)
			{
				if (m_CustomColorsPos[i].Contains(e.X, e.Y))
				{
					selectedColor = m_CustomColors[i];
					ColorSelected();
					break;
				}
			}
		}

		private void ColorSelected()
		{
			this.BackColor = selectedColor;
//			PopupContainerControl ctrl = this.Parent as PopupContainerControl;
//			if (ctrl == null)
//				return;
//
//			// Change the Parent Item image to indicate which color was selected last
//			// Assumes that Image with ImageIndex 21 is used on button
//			ButtonItem btn = ctrl.ParentItem as ButtonItem;
//			Bitmap bmp = new Bitmap(btn.ImageList.Images[btn.ImageIndex], btn.ImageList.ImageSize);
//			Graphics g = Graphics.FromImage(bmp);
//			g.DrawImageUnscaled(btn.ImageList.Images[btn.ImageIndex], 0, 0);
//			using (SolidBrush brush = new SolidBrush(this.selectedColor))
//				g.FillRectangle(brush, 0, 12, 16, 4);
//			g.Dispose();
////			bmp.MakeTransparent(btn.ImageList.TransparentColor);
//			//btn.ImageIndex=-1;
//			DotNetBarManager manager = null;
//			if (btn.ContainerControl is Bar)
//				manager = ((Bar) btn.ContainerControl).Owner as DotNetBarManager;
//			else if (btn.ContainerControl is MenuPanel)
//				manager = ((MenuPanel) btn.ContainerControl).Owner as DotNetBarManager;
//			if (manager != null && btn.Name != "")
//			{
//				ArrayList items = manager.GetItems(btn.Name, true);
//				foreach (ButtonItem item in items)
//					item.Image = bmp;
//			}
//			else
//				btn.Image = bmp;
//
//			BaseItem topItem = ctrl.ParentItem;
//			while (topItem.Parent is ButtonItem)
//				topItem = topItem.Parent;
//			topItem.Expanded = false;
//			if (topItem.Parent != null)
//				topItem.Parent.AutoExpand = false;
		}

		private void WebSelectionChange(object sender, EventArgs e)
		{
			if (listWeb.SelectedItem != null)
			{
				selectedColor = (Color) listWeb.SelectedItem;
				ColorSelected();
			}
		}

		private void SystemSelectionChange(object sender, EventArgs e)
		{
			if (listSystem.SelectedItem != null)
			{
				selectedColor = (Color) listSystem.SelectedItem;
				ColorSelected();
			}
		}

		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			colorPanel.BackColor = this.BackColor;
		}
	}
}