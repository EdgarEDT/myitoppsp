namespace ItopVector.Design
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Drawing.Design;
	using System.Drawing.Drawing2D;
	using System.Text.RegularExpressions;
	using System.Windows.Forms.Design;
	using ItopVector.Selector;

	internal class StrokeStyleEditor : DropDownEditor
	{
		// Methods
		static StrokeStyleEditor()
		{
			StrokeStyleEditor._regex = new Regex(@"\s+");
		}

		public StrokeStyleEditor()
		{
			this.changed = false;
		}

		private void selectedIndexChanged(object sender, EventArgs e)
		{
			this.changed = true;
			base.editorService.CloseDropDown();
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (((context != null) && (context.Instance != null)) && (provider != null))
			{
				base.editorService = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
				if (base.editorService == null)
				{
					return value;
				}
				StrokeStyleSelector selector1 = new StrokeStyleSelector();
				selector1.Height = 150;
				string text1="1";
				if(value!=null)
				{
					text1 = value.ToString().Replace(",", " ");
				}
				text1 = StrokeStyleEditor._regex.Replace(text1, " ");
				int num1 = selector1.FindString(text1);
				text1 = null;
				selector1.SelectedIndex = num1;
				selector1.SelectedIndexChanged += new EventHandler(this.selectedIndexChanged);
				base.editorService.DropDownControl(selector1);
				if (this.changed && (selector1.SelectedItem != null))
				{
					value = selector1.SelectedItem.ToString();
				}
				this.changed = false;
			}
			return value;
		}

		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override void PaintValue(PaintValueEventArgs e)
		{
            if (e.Value == null) return;
			string text1 = e.Value.ToString();
			text1 = text1.Replace(",", " ");
			text1 = StrokeStyleEditor._regex.Replace(text1, " ");
			char[] chArray1 = new char[] { ' ' } ;
			string[] textArray1 = text1.Split(chArray1);
			bool flag1 = false;
			float[] singleArray1 = null;
			if (textArray1.Length > 1)
			{
				flag1 = true;
				singleArray1 = new float[textArray1.Length];
				try
				{
					for (int num1 = 0; num1 < singleArray1.Length; num1++)
					{
						singleArray1[num1] = float.Parse(textArray1[num1]);
					}
				}
				catch
				{
					flag1 = false;
				}
			}
			using (Pen pen1 = new Pen(Color.Black, 1f))
			{
				pen1.Alignment = PenAlignment.Center;
				if (flag1)
				{
					pen1.DashPattern = singleArray1;
				}
				float single1 = e.Bounds.Y + (((float) e.Bounds.Height) / 2f);
				e.Graphics.DrawLine(pen1, (float) (e.Bounds.X + 1), single1, (float) (e.Bounds.Right - 1), single1);
			}
		}


		// Fields
		private bool changed;
		private static Regex _regex;
	}
}

