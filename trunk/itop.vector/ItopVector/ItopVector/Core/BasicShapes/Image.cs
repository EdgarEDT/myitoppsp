using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Xml;
using ItopVector.Core.Document;
using ItopVector.Core.Func;
using ItopVector.Core.Interface.Figure;
using System.IO;

namespace ItopVector.Core.Figure
{
	public class Image : Graph
	{
		// Methods
		internal Image(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
			this.refImage = null;
			this.imageurl = string.Empty;
			this.opacity = 1f;
			this.imageAttributes = null;
			this.transparent = Color.Empty;
		}
        public override RectangleF GetBounds() {
            return new RectangleF(X, Y, Width, Height);
        }
		public override void Draw(Graphics g, int time)
		{
			if (base.DrawVisible)
			{
				if (this.pretime != time)
				{
					int num1 = 0;
					int num2 = 0;
					//AnimFunc.CreateAnimateValues(this, time, out num1, out num2);
				}
				GraphicsContainer container1 = g.BeginContainer();
				g.SmoothingMode = base.OwnerDocument.SmoothingMode;
				Matrix matrix1 = base.Transform.Matrix.Clone();

				ClipAndMask.ClipPath.Clip(g, time, this);
				base.GraphTransform.Matrix.Multiply(matrix1, MatrixOrder.Prepend);
				g.Transform = base.GraphTransform.Matrix;
				Bitmap bitmap1 = this.RefImage;
				if (bitmap1 != null)
				{
					int num3 = (int) this.X;
					int num4 = (int) this.Y;
					int num5 = (int) this.Width;
					int num6 = (int) this.Height;
					Rectangle rectangle1 = new Rectangle(num3, num4, num5, num6);
					g.DrawImage(bitmap1, rectangle1, 0f, 0f, (float) bitmap1.Width, (float) bitmap1.Height, GraphicsUnit.Pixel,this.ImageAttributes);
				}
				ClipAndMask.ClipPath.DrawClip(g, time, this);
				g.EndContainer(container1);
			}
		}


		// Properties
		public override GraphicsPath GPath
		{
			get
			{
				GraphicsPath path1 = new GraphicsPath();
				path1.AddRectangle(new RectangleF(this.X, this.Y, this.Width, this.Height));

				return path1;
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


		private ImageAttributes ImageAttributes
		{
			get
			{
				if ((this.pretime != base.OwnerDocument.ControlTime) || (this.imageAttributes == null))
				{
					this.imageAttributes = new ImageAttributes();
					float single1 = this.Opacity;
					ColorMatrix matrix1 = new ColorMatrix();
					matrix1.Matrix00 = 1f;
					matrix1.Matrix11 = 1f;
					matrix1.Matrix22 = 1f;
					matrix1.Matrix33 = single1;
					matrix1.Matrix44 = 1f;
					this.imageAttributes.SetColorMatrix(matrix1, ColorMatrixFlag.Default, ColorAdjustType.Default);
				}
				this.imageAttributes.ClearColorKey(ColorAdjustType.Default);
				if (ThansparencyKey!=Color.Empty && transparent!=Color.Transparent)
					this.imageAttributes.SetColorKey(transparent,transparent,ColorAdjustType.Default);

				return this.imageAttributes;
			}
		}
		public string ImageUrl
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("xlink:href"))
				{
					this.imageurl = (string) this.svgAnimAttributes["xlink:href"];
				}
				else if (this.svgAnimAttributes.ContainsKey("href"))
				{
					this.imageurl = (string) this.svgAnimAttributes["href"];
				}
				else
				{
					this.imageurl = string.Empty;
				}
				return this.imageurl;
			}
			set
			{
				string text1 = value;
				if (text1.Trim().StartsWith("#"))
				{
					this.imageurl = text1.Substring(1, text1.Trim().Length - 1);
				}
				else
				{
					this.imageurl = text1;
				}
				AttributeFunc.SetAttributeValue(this, "xlink:href", this.imageurl.ToString());
			}
		}
        public string BackgroundImageFile {
            get {
                return this.GetAttribute("backgroundimage");
            }
            set {
                string file = AppDomain.CurrentDomain.BaseDirectory + "\\" + value;
                if (File.Exists(file)||File.Exists(value)) {
                    try {
                        //refImage = Bitmap.FromFile(file);
                        this.SetAttribute("backgroundimage", value);
                        this.ImageUrl = value;
                    } catch { }
                } else if (value == string.Empty) {
                    refImage = null;
                    this.RemoveAttribute("backgroundimage");
                }
            }
        }
		public float Opacity
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("opacity"))
				{
					this.opacity = (float) this.svgAnimAttributes["opacity"];
				}
				else
				{
					this.opacity = 1f;
				}
				XmlNode node1 = this.ParentNode;
				if (this.UseElement != null)
				{
					node1 = this.UseElement;
				}
				if (node1 is IGraph)
				{
					this.opacity = Math.Min(this.opacity, ((IGraph) node1).TempOpacity);
				}
				return this.opacity;
			}
			set
			{
				if (this.opacity != value)
				{
					this.opacity = value;
					AttributeFunc.SetAttributeValue(this, "opacity", this.opacity.ToString());
				}
			}
		}

		public Bitmap RefImage
		{
			get { return this.refImage; }
			set { this.refImage = value; }
		}

		/// <summary>
		/// ͼƬ͸��ɫ
		/// </summary>
		public Color ThansparencyKey
		{
			get{
				if (this.transparent==Color.Empty)
				{
					string color = this.GetAttribute("transparent");

					if(color!=string.Empty)
						this.transparent = ColorFunc.ParseColor(color);

				}
				return this.transparent;
			}
			set{
				if (value!=this.transparent)
				{
					this.transparent =value;
					this.SetAttribute("transparent",ColorFunc.GetColorString(value));
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
		private Color transparent;
		private float height;
		private ImageAttributes imageAttributes;
		private string imageurl;
		private float opacity;
		private Bitmap refImage;
		private float width;
		private float x;
		private float y;

        internal void ResetSize() {
            PointF ptf = new PointF(X, Y);
            PointF[] pts = new PointF[] { ptf };
            this.Transform.Matrix.TransformPoints(pts);
            this.X = pts[0].X;
            this.Y = pts[0].Y;
            this.Width = RefImage.Width;
            this.Height = RefImage.Height;
            this.RemoveAttribute("transform");
        }
    }
}