namespace ItopVector.Design
{
	using System;
	using System.ComponentModel;
	using System.Windows.Forms;
	using System.Windows.Forms.Design;

	internal class ListFontSize : DropDownEditor
	{
		// Methods
		public ListFontSize()
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
				ListBox box1 = new ListBox();
				float[] singleArray1 = new float[] { 
													   8f, 9f, 10f, 11f, 12f, 13f, 14f, 16f, 18f, 20f, 22f, 24f, 26f, 28f, 36f, 48f, 
													   72f, 80f, 88f, 96f, 128f, 168f
												   } ;
				for (int num1 = 0; num1 < singleArray1.Length; num1++)
				{
					box1.Items.Add(singleArray1[num1]);
				}
				
				singleArray1 = null;
				box1.BorderStyle = BorderStyle.None;
				box1.Height = 150;
				box1.Width = 80;
				if(value!=null)
				{
                    //Struct.Float fl= (Struct.Float) value;
                    float f1 = (float)value;
                    box1.SelectedIndex = box1.FindString(f1.ToString());
				}
				box1.SelectedIndexChanged += new EventHandler(this.selectedIndexChanged);
				base.editorService.DropDownControl(box1);
				if (this.selectChanged && (box1.SelectedItem != null))
				{
					try
					{
                        //value =new Struct.Float( float.Parse(box1.SelectedItem.ToString()));
                        value = float.Parse(box1.SelectedItem.ToString());
					}
					catch
					{
					}
				}
				this.selectChanged = false;
			}
			return value;
		}


		// Fields
		private bool selectChanged;
	}
}

