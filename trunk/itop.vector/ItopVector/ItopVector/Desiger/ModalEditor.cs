namespace ItopVector.Design
{
	using System;
	using System.ComponentModel;
	using System.Drawing.Design;

	internal class ModalEditor : BaseEditor
	{
		// Methods
		public ModalEditor()
		{
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if ((context != null) && (context.Instance != null))
			{
				return UITypeEditorEditStyle.Modal;
			}
			return base.GetEditStyle(context);
		}

	}
}