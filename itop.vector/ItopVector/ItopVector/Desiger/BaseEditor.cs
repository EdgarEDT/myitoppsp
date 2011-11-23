using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace ItopVector.Design
{
	internal class BaseEditor : UITypeEditor
	{
		// Methods
		public BaseEditor()
		{
			this.editorService = null;
		}


		// Fields
		protected IWindowsFormsEditorService editorService;
	}
}

