namespace ItopVector.Selector
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;

	[ToolboxItem(false)]
	internal class StrokeWidthSelector : ListBox
	{
		// Methods
		public StrokeWidthSelector()
		{
			this.DrawMode = DrawMode.OwnerDrawFixed;
			this.ItemHeight = 0x12;
			base.Items.Add(1f);
			base.Items.Add(2f);
			base.Items.Add(2.6f);
			base.Items.Add(3f);
			base.Items.Add(3.6f);
			base.Items.Add(4f);
			base.Items.Add(4.6f);
			base.Items.Add(5f);
			base.Items.Add(6f);
			base.BorderStyle = BorderStyle.None;
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			int num1 = e.Index;
			if (e.Index == -1)
			{
				num1 = (this.SelectedIndex >= 0) ? this.SelectedIndex : 0;
			}
			if ((num1 >= 0) || (num1 < base.Items.Count))
			{
				if ((e.State == DrawItemState.Selected) || (e.State == DrawItemState.None))
				{
					e.DrawBackground();
				}
				int num2 = 4;
				int num3 = 2;
				int num4 = 20;
				int num5 = 4;
				new Rectangle(num2, e.Bounds.Top + num3, e.Bounds.Width - (2 * num2), e.Bounds.Height - (2 * num3));
				Color color1 = e.ForeColor;
				if (e.State == (DrawItemState.ComboBoxEdit | (DrawItemState.Focus | DrawItemState.Selected)))
				{
					color1 = Color.Black;
				}
				using (StringFormat format1 = new StringFormat(StringFormat.GenericTypographic))
				{
					format1.LineAlignment = StringAlignment.Center;
					if ((base.Items[num1] is float) && (((float) base.Items[num1]) > 0f))
					{
						float single1 = (float) base.Items[num1];
						e.Graphics.DrawString(single1.ToString(), e.Font, new SolidBrush(color1), (RectangleF) new Rectangle(num2, e.Bounds.Top + num3, num4, e.Bounds.Height - (2 * num3)), format1);
						float single2 = (((e.Bounds.Height - num3) - single1) / 2f) + 1f;
						e.Graphics.FillRectangle(new SolidBrush(color1), new RectangleF((float) ((num2 + num4) + num5), e.Bounds.Top + single2, (float) (((e.Bounds.Width - num4) - num2) - (2 * num5)), single1));
					}
				}
			}
			base.OnDrawItem(e);
		}


	}
}

