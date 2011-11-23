using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using ItopVector.Core.Document;
using ItopVector.Core.Func;
using ItopVector.Core.Paint;

namespace ItopVector.Core.Figure
{
	public class TRef : Text
	{
		// Methods
		internal TRef(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
		}

		public override void Draw(Graphics g, int time)
		{
			int num1 = 0;
			int num2 = 0;
			AnimFunc.CreateAnimateValues(this, time, out num1, out num2);
			GraphicsContainer container1 = g.BeginContainer();
			g.SmoothingMode = base.OwnerDocument.SmoothingMode;
			g.TextRenderingHint = base.OwnerDocument.TextRenderingHint;
			ISvgBrush brush1 = base.GraphBrush;
			float single1 = base.Size;
			this.GPath = new GraphicsPath();
			GraphicsPath path1 = new GraphicsPath();
			StringFormat format1 = base.GetGDIStringFormat();
			FontFamily family1 = base.GetGDIFontFamily();
			this.currentPostion.X += (base.X + base.Dx);
			this.currentPostion.Y += (base.Y + base.Dy);
			SvgElement element1 = this.RefElement;
			string text1 = string.Empty;
			if (element1 is Text)
			{
				text1 = base.TrimText(element1.FirstChild.Value);
			}
			if (text1 != string.Empty)
			{
				float single2 = 0f;
				if (format1.Alignment == StringAlignment.Near)
				{
					single2 = (single1*1f)/6f;
				}
				else if (format1.Alignment == StringAlignment.Far)
				{
					single2 = (-single1*1f)/6f;
				}
				float single3 = (((float) family1.GetCellAscent(FontStyle.Regular))/((float) family1.GetEmHeight(FontStyle.Regular)))*single1;
				if (text1 != null)
				{
					path1.AddString(text1, family1, base.GetGDIStyle(), single1, new PointF(this.currentPostion.X - single2, this.currentPostion.Y - single3), format1);
				}
				this.currentPostion.X -= single2;
				RectangleF ef1 = path1.GetBounds();
				if (!ef1.IsEmpty)
				{
					RectangleF ef2 = path1.GetBounds();
					float single4 = ef2.Width;
					if (format1.Alignment == StringAlignment.Center)
					{
						single4 /= 2f;
					}
					else if (format1.Alignment == StringAlignment.Far)
					{
						single4 = 0f;
					}
					this.currentPostion.X += (single4 + (single1/4f));
				}
				brush1.Paint(path1, g, time);
//                Stroke stroke1 = Stroke.GetStroke(this);
				base.GraphStroke.Paint(g, this, path1, time);
				this.GPath.StartFigure();
				this.GPath.AddPath(path1, false);
			}
			g.EndContainer(container1);
		}


		// Properties
		public string Href
		{
			get { return AttributeFunc.FindAttribute("xlink:href", this).ToString(); }
		}

		public SvgElement RefElement
		{
			get
			{
				XmlNode node1 = NodeFunc.GetRefNode(this.Href, base.OwnerDocument);
				if (node1 is SvgElement)
				{
					return (SvgElement) node1;
				}
				return null;
			}
		}

	}
}