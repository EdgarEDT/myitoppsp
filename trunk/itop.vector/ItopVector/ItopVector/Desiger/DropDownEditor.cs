using System.ComponentModel;
using System.Drawing.Design;

namespace ItopVector.Design
{
	internal class DropDownEditor : BaseEditor
	{
		// Methods
		public DropDownEditor()
		{
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if ((context != null) && (context.Instance != null))
			{
				return UITypeEditorEditStyle.DropDown;
			}
			return base.GetEditStyle(context);
		}

	}
}

