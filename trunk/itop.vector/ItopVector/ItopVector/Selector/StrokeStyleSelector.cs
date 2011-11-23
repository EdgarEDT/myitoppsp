namespace ItopVector.Selector
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Runtime.InteropServices;
	using System.Text;
	using System.Windows.Forms;
	[ToolboxItem(false)]
	internal class StrokeStyleSelector : ListBox
	{
		// Methods
		public StrokeStyleSelector()
		{
			this.DrawMode = DrawMode.OwnerDrawFixed;
			this.ItemHeight = 0x12;
			base.Items.Add(new DashedStyle(null));
			float[] singleArray1 = new float[] { 1f, 1f } ;
			base.Items.Add(new DashedStyle(singleArray1));
			singleArray1 = new float[] { 2f, 2f } ;
			base.Items.Add(new DashedStyle(singleArray1));
			singleArray1 = new float[] { 3f, 3f } ;
			base.Items.Add(new DashedStyle(singleArray1));
			singleArray1 = new float[] { 4f, 4f } ;
			base.Items.Add(new DashedStyle(singleArray1));
			base.Items.Add(new DashedStyle(new float[] { 2f, 1f, 1f, 1f } ));
			base.Items.Add(new DashedStyle(new float[] { 3f, 1f, 1f, 1f } ));
			base.Items.Add(new DashedStyle(new float[] { 4f, 1f, 1f, 1f } ));
			base.Items.Add(new DashedStyle(new float[] { 2f, 1f, 1f, 1f, 1f, 1f } ));
			base.Items.Add(new DashedStyle(new float[] { 3f, 1f, 1f, 1f, 1f, 1f } ));
			base.Items.Add(new DashedStyle(new float[] { 4f, 1f, 1f, 1f, 1f, 1f } ));
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
				new Rectangle(num2, e.Bounds.Top + num3, e.Bounds.Width - (2 * num2), e.Bounds.Height - (2 * num3));
				Color color1 = e.ForeColor;
				if (e.State == (DrawItemState.ComboBoxEdit | (DrawItemState.Focus | DrawItemState.Selected)))
				{
					color1 = Color.Black;
				}
				using (Pen pen1 = new Pen(color1, 2f))
				{
					if (base.Items[num1] is DashedStyle)
					{
						DashedStyle efecfffdd1 = (DashedStyle) base.Items[num1];
						float[] singleArray1 = efecfffdd1.Array;
						if (singleArray1 == null)
						{
							e.Graphics.DrawLine(pen1, (int) (e.Bounds.Left + num2), (int) ((e.Bounds.Top + (e.Bounds.Height / 2)) - 1), (int) ((e.Bounds.Left + e.Bounds.Width) - (2 * num2)), (int) ((e.Bounds.Top + (e.Bounds.Height / 2)) - 1));
						}
						else
						{
							pen1.DashPattern = singleArray1;
							e.Graphics.DrawLine(pen1, (int) (e.Bounds.Left + num2), (int) ((e.Bounds.Top + (e.Bounds.Height / 2)) - 1), (int) ((e.Bounds.Left + e.Bounds.Width) - (2 * num2)), (int) ((e.Bounds.Top + (e.Bounds.Height / 2)) - 1));
						}
					}
				}
			}
			base.OnDrawItem(e);
		}


		
		// Nested Types
		[StructLayout(LayoutKind.Sequential)]
			private struct DashedStyle
		{
			private float[] floatArray;
			public DashedStyle(float[] a)
			{
				this.floatArray = a;
			}
			public override string ToString()
			{
				if ((this.floatArray == null) || (this.floatArray.Length == 0))
				{
					return "none";
				}
				StringBuilder builder1 = new StringBuilder();
				for (int num1 = 0; num1 < this.floatArray.Length; num1++)
				{
					builder1.Append(this.floatArray[num1].ToString());
					builder1.Append(" ");
				}
				return builder1.ToString(0, builder1.Length - 1);
			}
			public float[] Array
			{
				get
				{
					return this.floatArray;
				}
			}
		}
	}
}

