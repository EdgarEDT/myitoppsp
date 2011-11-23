namespace ItopVector.Design
{
	using System;
	using System.ComponentModel;
	using System.Windows.Forms;
	using System.Windows.Forms.Design;

	internal class Number180Editor : DropDownEditor
	{
		// Methods
		public Number180Editor()
		{
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
				NumericUpDown down1 = new NumericUpDown();
				down1.Height = 40;
				down1.BorderStyle = BorderStyle.None;
				try
				{
					Struct.Float fl= (Struct.Float) value;
				
					down1.Value = decimal.Parse(fl.F.ToString());
				}
				catch
				{
				}
				down1.Minimum = new decimal(0);
				down1.Maximum = new decimal(179);
				down1.DecimalPlaces = 0;
				down1.Increment =new decimal(10);// new decimal(1, 0, 0, false, 1);
				base.editorService.DropDownControl(down1);
				value =new Struct.Float( (float) down1.Value);
			}
			return value;
		}

	}
}

