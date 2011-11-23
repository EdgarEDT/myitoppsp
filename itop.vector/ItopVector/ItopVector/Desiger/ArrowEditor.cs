using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;
using ItopVector.Core.Figure;
using ItopVector.Selector;

namespace ItopVector.Design
{
	internal class ArrowEditor : DropDownEditor
	{
		// Methods
		public ArrowEditor()
		{
			this.changed = false;
		}

		private void selector1_SelectedIndexChanged(object sender, EventArgs e)
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
				ArrowSelector selector1 = new ArrowSelector();
				if (value is Struct.PropertyLineMarker)
				{
					Struct.PropertyLineMarker acaadf1 = (Struct.PropertyLineMarker) value;
					string text1 = acaadf1.Id;
					if (text1.StartsWith("start"))
					{
						text1 = text1.Substring(5);
					}
					else if (text1.StartsWith("end"))
					{
						text1 = text1.Substring(3);
					}
					int num1 = selector1.FindString(text1);
					selector1.SelectedIndex = num1;
					acaadf1 = (Struct.PropertyLineMarker) value;
					selector1.EndArrow = acaadf1.IsEndArrow;
				}
				selector1.Height = 150;
				selector1.SelectedIndexChanged += new EventHandler(this.selector1_SelectedIndexChanged);
				base.editorService.DropDownControl(selector1);
				if (this.changed)
				{
					value = new Struct.PropertyLineMarker(selector1.SelectedArrow, null, selector1.EndArrow, string.Empty);
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
			if (e.Value is Struct.PropertyLineMarker)
			{
				Struct.PropertyLineMarker acaadf1 = (Struct.PropertyLineMarker) e.Value;
				if (acaadf1.SvgElement != null)
				{
					Marker cfb1 = acaadf1.SvgElement;
					
					{
						using (GraphicsPath path1 = new GraphicsPath())
						{
							Rectangle rectangle1 = e.Bounds;
							GraphicsContainer container1 = e.Graphics.BeginContainer();
							e.Graphics.SetClip(rectangle1);
							e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
							if (acaadf1.IsEndArrow)
							{
								path1.AddLine((float) rectangle1.X, rectangle1.Y + (((float) rectangle1.Height) / 2f), (float) rectangle1.Right, rectangle1.Y + (((float) rectangle1.Height) / 2f));
								e.Graphics.DrawPath(Pens.Black, path1);

								GraphicsContainer container2 = e.Graphics.BeginContainer();
								PointF p1=path1.PathPoints[1];
								using (Matrix matrix1 = new Matrix())
								{
//									matrix1.Multiply(new Matrix(-1f, 0f, 0f, 1f, 0f, 0f));
									matrix1.Translate(p1.X,p1.Y, MatrixOrder.Append);
									cfb1.GraphTransform.Matrix.Multiply(matrix1);
								}

								cfb1.Draw(e.Graphics,0);
								e.Graphics.EndContainer(container2);
								
							}
							else
							{
								path1.AddLine((float) rectangle1.X, rectangle1.Y + (((float) rectangle1.Height) / 2f), (float) rectangle1.Right, rectangle1.Y + (((float) rectangle1.Height) / 2f));
								e.Graphics.DrawPath(Pens.Black, path1);

								GraphicsContainer container2 = e.Graphics.BeginContainer();
								PointF p1=path1.PathPoints[0];
								e.Graphics.TranslateTransform(p1.X,p1.Y);

								cfb1.Draw(e.Graphics,0);								
								e.Graphics.EndContainer(container2);
								
							}
							e.Graphics.EndContainer(container1);
						}
					}
				}
			}
		}


		// Fields
		private bool changed;
	}
}

