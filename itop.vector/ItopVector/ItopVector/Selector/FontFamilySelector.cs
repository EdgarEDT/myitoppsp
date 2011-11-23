namespace ItopVector.Selector
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;

	[ToolboxItem(false)]
	public class FontFamilySelector : ListBox
	{
		// Methods
		public FontFamilySelector()
		{
			this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			
			this.ItemHeight = 18;
			FontFamily[] familyArray1 = FontFamily.Families;
			for (int num1 = 0; num1 < familyArray1.Length; num1++)
			{			
				base.Items.Add(familyArray1[num1].Name);
			}
		}
		
		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			if (e.Index == -1 || e.Index >= this.Items.Count)
				return;

			if ((e.State == DrawItemState.Selected) || (e.State == DrawItemState.None))
			{
				e.DrawBackground();
			}
			try
			{
				
				Brush b = new SolidBrush(e.ForeColor);

				string fontname=this.Items[e.Index].ToString();
				Font   font=new Font(fontname,11);
				
				e.Graphics.DrawString(
					fontname, 
					font,
					b,
					e.Bounds
					);

				b.Dispose();
				font.Dispose();
				fontname=null;
			}
			catch{}


			base.OnDrawItem (e);
		}


	}
}

