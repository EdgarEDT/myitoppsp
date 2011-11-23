using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ItopVector.Selector;
using ItopVector.Core.Figure;
using ItopVector.Core;
using System.Xml;


namespace ItopVector.Design
{
	internal class LayerEdit : DropDownEditor
	{
		// Methods
		public LayerEdit()
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
				if(value==null)return value;

				ListBox box1 =new ListBox(); 

				if (value is SvgElement )
				{
					
					XmlNodeList list = (value as SvgElement).OwnerDocument.GetElementsByTagName("layer");
					foreach(Layer layer in list)
					{
						if (layer !=null)
						{
							box1.Items.Add(layer);
						}
					}
				}
				box1.BorderStyle = BorderStyle.None;
				box1.Height = 150;
				box1.SelectedIndex = box1.FindString(value.ToString());
				box1.SelectedIndexChanged += new EventHandler(this.selectedIndexChanged);
				base.editorService.DropDownControl(box1);
				if (this.selectChanged && (box1.SelectedItem != null))
				{
					value = box1.SelectedItem;
				}
				
				this.selectChanged = false;
			}
			return value;
		}

		// Fields
		private bool selectChanged;
	}
}

