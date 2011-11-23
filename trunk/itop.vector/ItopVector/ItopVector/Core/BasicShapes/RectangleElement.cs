using System.Drawing;
using System.Drawing.Drawing2D;
using ItopVector.Core.Document;
using ItopVector.Core.Func;

namespace ItopVector.Core.Figure
{
	public class RectangleElement : GraphPath
	{
		// Methods
		internal RectangleElement(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
			this.x = 0f;
			this.y = 0f;
			this.width = 0f;
			this.height = 0f;
			this.rx = 0f;
			this.ry = 0f;
//			this.nullrx = false;
//			this.nullry = false;
			this.Angle = 0;
		}

		public override void Draw(Graphics g, int time)
		{
			base.Draw(g, time);
		}


		// Properties
		public override GraphicsPath GPath
		{
			get
			{
				SvgDocument document1 = base.OwnerDocument;
				if (this.pretime != document1.ControlTime)
				{
					float single1 = 0f;
					float single2 = 0f;
					float single3 = this.X;
					float single4 = this.Y;
					float single5 = this.Width;
					float single6 = this.Height;
					this.graphPath = new GraphicsPath();
					this.graphPath.Reset();
					if (this.svgAnimAttributes.ContainsKey("rx") && this.svgAnimAttributes.ContainsKey("ry"))
					{
						single1 = (float) this.svgAnimAttributes["rx"];
						single2 = (float) this.svgAnimAttributes["ry"];
					}
					else if (this.svgAnimAttributes.ContainsKey("rx") && !this.svgAnimAttributes.ContainsKey("ry"))
					{
						single1 = single2 = (float) this.svgAnimAttributes["rx"];
					}
					else if (this.svgAnimAttributes.ContainsKey("ry") && !this.svgAnimAttributes.ContainsKey("rx"))
					{
						single1 = single2 = (float) this.svgAnimAttributes["ry"];
					}
					if (single1 > (this.width))
					{
						single1 = this.width;
					}
					if (single2 > (this.height))
					{
						single2 = this.height;
					}
					if ((single1 != 0f) || (single2 != 0f))
					{
						this.graphPath.AddArc((single3 + single5) - single1, single4, single1, single2, 270f, 90f);
						this.graphPath.AddArc((single3 + single5) - single1, (single4 + single6) - single2, single1, single2, 0f, 90f);
						this.graphPath.AddArc(single3, (single4 + single6) - single2, single1, single2, 90f, 90f);
						this.graphPath.AddArc(single3, single4, single1, single2, 180f, 90f);
						this.graphPath.CloseFigure();
					}
					else
					{
						this.graphPath.AddRectangle(new RectangleF(this.X, this.Y, this.Width, this.Height));
					}
					this.CreateConnectPoint();
				}

				return this.graphPath;
			}
			set
			{
			}
		}

		public float Height
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("height"))
				{
					this.height = (float) this.svgAnimAttributes["height"];
				}
				else
				{
					this.height = 0f;
				}
				return this.height;
			}
			set
			{
				if (this.height != value)
				{
					this.height = value;
					AttributeFunc.SetAttributeValue(this, "height", value.ToString());
				}
			}
		}

		public float RX
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("rx"))
				{
					this.rx = (float) this.svgAnimAttributes["rx"];
				}
				else
				{
					this.rx = 0f;
				}
				return this.rx;
			}
			set
			{
				if (this.rx != value)
				{
					this.rx = value;
					AttributeFunc.SetAttributeValue(this, "rx", value.ToString());
				}
			}
		}

		public float RY
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("ry"))
				{
					this.ry = (float) this.svgAnimAttributes["ry"];
				}
				else
				{
					this.ry = 0f;
				}
				return this.ry;
			}
			set
			{
				if (this.ry != value)
				{
					this.ry = value;
					AttributeFunc.SetAttributeValue(this, "ry", value.ToString());
				}
			}
		}

		public float Width
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("width"))
				{
					this.width = (float) this.svgAnimAttributes["width"];
				}
				else
				{
					this.width = 0f;
				}
				return this.width;
			}
			set
			{
				if (this.width != value)
				{
					this.width = value;
					AttributeFunc.SetAttributeValue(this, "width", value.ToString());
				}
			}
		}

		public float X
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("x"))
				{
					this.x = (float) this.svgAnimAttributes["x"];
				}
				else
				{
					this.x = 0f;
				}
				return this.x;
			}
			set
			{
				if (this.x != value)
				{
					this.x = value;
					AttributeFunc.SetAttributeValue(this, "x", value.ToString());
				}
			}
		}

		public float Y
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("y"))
				{
					this.y = (float) this.svgAnimAttributes["y"];
				}
				else
				{
					this.y = 0f;
				}
				return this.y;
			}
			set
			{
				if (this.y != value)
				{
					this.y = value;
					AttributeFunc.SetAttributeValue(this, "y", value.ToString());
				}
			}
		}


		// Fields
		public int Angle;
		private float height;
//		private bool nullrx;
//		private bool nullry;
		private float rx;
		private float ry;
		private float width;
		private float x;
		private float y;
	}
}