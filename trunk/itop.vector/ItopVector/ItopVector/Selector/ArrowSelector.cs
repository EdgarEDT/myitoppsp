namespace ItopVector.Selector
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Resources;
	using System.Windows.Forms;
	using System.Xml;
	using ItopVector;
	using ItopVector.Core.Document;
	using ItopVector.Core.Figure;
	using ItopVector.Core;
    using System.Reflection;
    using System.IO;

	[ToolboxItem(false)]
	public sealed class ArrowSelector : ListBox 
	{
		// Methods
		public ArrowSelector()
		{
			this.endArrow = false;
			try
			{
				base.Items.Add("нч");
				SvgDocument fadde1=new SvgDocument();
                string filename = Path.GetDirectoryName(Assembly.GetAssembly(base.GetType()).Location)+"\\symbol\\arrow.xml";
                if (File.Exists(filename)) {
                    fadde1 = SvgDocumentFactory.CreateDocumentFromFile(filename);
                } else {
                    ResourceManager manager1 = new ResourceManager(base.GetType());
                    fadde1.LoadXml(manager1.GetString("arrow"));
                }
				XmlNodeList list1 = fadde1.GetElementsByTagName("marker");
				for (int num1 = 0; num1 < list1.Count; num1++)
				{
					XmlElement element1 = list1[num1] as XmlElement;
					if (element1 != null)
					{
						base.Items.Add(new Arrow((SvgElement)element1));
					}
				}
				
			}
			catch (Exception exception1)
			{
				Console.Write(exception1.Message);
			}
			this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			base.BorderStyle = BorderStyle.None;
			this.ItemHeight = 16;
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			e.Graphics.SetClip(e.Bounds);
			if ((e.State == DrawItemState.Selected) || (e.State == DrawItemState.None))
			{
				e.DrawBackground();
			}
			GraphicsContainer container1 = e.Graphics.BeginContainer();
			if ((e.Index >= 0) && (e.Index < base.Items.Count))
			{
				Arrow arrow1 = base.Items[e.Index] as Arrow;
				if ((arrow1 != null) && (arrow1.SvgElement is Marker))
				{
					
				{
					using (GraphicsPath path1 = new GraphicsPath())
					{
						e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
						Rectangle rectangle1 = new Rectangle(e.Bounds.X + 4, e.Bounds.Y + 2, e.Bounds.Width - 8, e.Bounds.Height - 4);
						e.Graphics.FillRectangle(Brushes.White, rectangle1);
						e.Graphics.DrawRectangle(Pens.Black, rectangle1);
						if (this.endArrow)
						{
							path1.AddLine((int) (e.Bounds.X + 4), (int) (e.Bounds.Y + (e.Bounds.Height / 2)), (int) (e.Bounds.Right - 8), (int) (e.Bounds.Y + (e.Bounds.Height / 2)));
							e.Graphics.DrawPath(Pens.Black, path1);
//							using (Matrix matrix1 = new Matrix())
//							{
//								matrix1.Multiply(new Matrix(-1f, 0f, 0f, 1f, 0f, 0f));
//								matrix1.Translate((float) (2 * rectangle1.Width), 0f, MatrixOrder.Append);
//								path1.Transform(matrix1);
//							}
							GraphicsContainer container2 = e.Graphics.BeginContainer();
							PointF p1=path1.PathPoints[1];
							e.Graphics.TranslateTransform(p1.X,p1.Y);
							e.Graphics.RotateTransform(180);
							(arrow1.SvgElement as Marker).Draw(e.Graphics,0);
							e.Graphics.EndContainer(container2);
							
						}
						else
						{
							path1.AddLine((int) (e.Bounds.X + 8), (int) (e.Bounds.Y + (e.Bounds.Height / 2)), (int) (e.Bounds.Right - 4), (int) (e.Bounds.Y + (e.Bounds.Height / 2)));
							e.Graphics.DrawPath(Pens.Black, path1);

							GraphicsContainer container2 = e.Graphics.BeginContainer();
							PointF p1=path1.PathPoints[0];
							e.Graphics.TranslateTransform(p1.X,p1.Y);							
							(arrow1.SvgElement as Marker).Draw(e.Graphics,0);
							e.Graphics.EndContainer(container2);
							
						}
					}
					goto Label_0325;
				}
				}
				e.Graphics.DrawString("нч", e.Font, new SolidBrush(e.ForeColor), (RectangleF) new Rectangle(e.Bounds.X + 4, e.Bounds.Y + 2, e.Bounds.Width - 8, e.Bounds.Height - 4));
			}
			Label_0325:
				base.OnDrawItem(e);
			e.Graphics.ResetClip();
			e.Graphics.EndContainer(container1);
		}


		// Properties
		public new System.Windows.Forms.DrawMode DrawMode
		{
			get
			{
				return System.Windows.Forms.DrawMode.OwnerDrawFixed;
			}
			set
			{
				base.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			}
		}

		public bool EndArrow
		{
			get
			{
				return this.endArrow;
			}
			set
			{
				this.endArrow = value;
				base.Invalidate();
			}
		}

		public new  ListBox.ObjectCollection Items
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
			}
		}

		public Arrow SelectedArrow
		{
			get
			{
				if ((this.SelectedIndex >= 0) && (this.SelectedIndex < base.Items.Count))
				{
					return (base.Items[this.SelectedIndex] as Arrow);
				}
				return null;
			}
			set
			{
				this.SelectedIndex = base.Items.IndexOf(value);
			}
		}


		// Fields
		private bool endArrow;
		
	}
}

