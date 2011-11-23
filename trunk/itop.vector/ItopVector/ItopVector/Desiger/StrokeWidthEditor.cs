namespace ItopVector.Design
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Drawing.Design;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms.Design;
	using ItopVector.Selector;

	internal class StrokeWidthEditor : DropDownEditor
	{ 
		// Methods
		public StrokeWidthEditor()
		{
			this.changed = false;
		}
		

		private void SelectedIndexChanged(object sender, EventArgs e)
		{
			this.changed = true;
			base.editorService.CloseDropDown();
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (((context != null) && (context.Instance != null)) && (provider != null))
			{
                base.editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (base.editorService == null) {
                    return value;
                }

                StrokeWidthSelector dcba1 = new StrokeWidthSelector();
                dcba1.Height = 150;
                int num1 = 0;
                if (value != null) {
                    //Struct.Float fl = (Struct.Float)value;
                    num1 = dcba1.FindString(value.ToString());
                    //num1 = dcba1.FindString(fl.F.ToString());
                }

                dcba1.SelectedIndex = num1;
                dcba1.SelectedIndexChanged += new EventHandler(this.SelectedIndexChanged);
                base.editorService.DropDownControl(dcba1);
                if (this.changed && dcba1.SelectedItem != null) {
                    try {
                        string text1 = dcba1.SelectedItem.ToString();
                        value = float.Parse(text1);
                        //value = new Struct.Float(float.Parse(text1));
                        text1 = null;
                    } catch {
                    }
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

            float single1 = 1f;
            try {

                single1 = (float)e.Value;
            } catch {
            }
            using (Pen pen1 = new Pen(Color.Black, single1)) {
                pen1.Alignment = PenAlignment.Center;
                float single2 = e.Bounds.Y + (((float)e.Bounds.Height) / 2f);
                e.Graphics.DrawLine(pen1, (float)(e.Bounds.X + 1), single2, (float)(e.Bounds.Right - 1), single2);
            }
            
		}


		// Fields
		private bool changed;
	}
}

