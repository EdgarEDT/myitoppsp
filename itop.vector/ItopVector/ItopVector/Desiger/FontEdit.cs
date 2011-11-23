using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ItopVector.Selector;

namespace ItopVector.Design
{
	internal class FontEditor : DropDownEditor
	{
		// Methods
		public FontEditor()
		{
			this.selectChanged = false;

		}

		private void selectedIndexChanged(object sender, EventArgs e)
		{
			this.selectChanged = true;
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
				FontFamilySelector box1 =new FontFamilySelector(); 

				box1.BorderStyle = BorderStyle.None;
				box1.Height = 150;
				if(value!=null)
				{
					box1.SelectedIndex = box1.FindString(value.ToString());
				}
				box1.SelectedIndexChanged += new EventHandler(this.selectedIndexChanged);
				base.editorService.DropDownControl(box1);
				if (this.selectChanged && (box1.SelectedItem != null))
				{
					value = box1.SelectedItem.ToString();
				}
				this.selectChanged = false;
			}
			return value;
		}


		// Fields
		private bool selectChanged;
	}
}

