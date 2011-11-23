namespace ItopVector.Design
{
	using System;
	using System.ComponentModel;
	using System.Windows.Forms;
	using System.Windows.Forms.Design;
	using ItopVector.Dialog;

	internal class LabelTextEditor : ModalEditor
	{
		// Methods
		public LabelTextEditor()
		{
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (((context != null) && (context.Instance != null)) && (provider != null))
			{
				base.editorService = (IWindowsFormsEditorService) provider.GetService(typeof(IWindowsFormsEditorService));
				if (base.editorService != null) 
				{
					InputDialog dcd1 = new InputDialog();
					dcd1.InputString = value.ToString();
					if (base.editorService.ShowDialog(dcd1) == DialogResult.OK)
					{
						value = dcd1.InputString;
					}
				}
			}
			return value;
		}

	}
}