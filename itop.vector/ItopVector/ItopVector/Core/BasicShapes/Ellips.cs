using System.Drawing.Drawing2D;
using ItopVector.Core.Document;
using ItopVector.Core.Func;

namespace ItopVector.Core.Figure
{
	/// <summary>
	/// Õ÷‘≤
	/// </summary>
	public class Ellips : GraphPath
	{
		// Methods
		internal Ellips(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
		}


		// Properties
		/// <summary>
		/// Õ÷‘≤‘≤–ƒx◊¯±Í
		/// </summary>
		public float CX
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("cx"))
				{
					this.cx = (float) this.svgAnimAttributes["cx"];
				}
				else
				{
					this.cx = 0f;
				}
				return this.cx;
			}
			set
			{
				if (this.cx != value)
				{
					this.cx = value;
					AttributeFunc.SetAttributeValue(this, "cx", value.ToString());
				}
			}
		}

		/// <summary>
		/// Õ÷‘≤‘≤–ƒy◊¯±Í
		/// </summary>
		public float CY
		{
			get
			{
				if (this.svgAnimAttributes.ContainsKey("cy"))
				{
					this.cy = (float) this.svgAnimAttributes["cy"];
				}
				else
				{
					this.cy = 0f;
				}
				return this.cy;
			}
			set
			{
				if (this.cy != value)
				{
					this.cy = value;
					AttributeFunc.SetAttributeValue(this, "cy", value.ToString());
				}
			}
		}

		public override GraphicsPath GPath
		{
			get
			{
				SvgDocument document1 = base.OwnerDocument;
				if (this.pretime != document1.ControlTime)
				{
					this.graphPath = new GraphicsPath();
					float single1 = this.RX;
					float single2 = this.RY;
					if ((single1 != 0f) && (single2 != 0f))
					{
						this.graphPath.AddEllipse(this.CX - single1, this.CY - single2, single1*2f, single2*2f);
					}
					this.CreateConnectPoint();
				}

				return this.graphPath;
			}
			set { this.graphPath = value; }
		}

		/// <summary>
		/// Õ÷‘≤x÷·∞Îæ∂
		/// </summary>
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

		/// <summary>
		/// Õ÷‘≤y÷·∞Îæ∂
		/// </summary>
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


		// Fields
		private float cx;
		private float cy;
		private float rx;
		private float ry;
	}
}