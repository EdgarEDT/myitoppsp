using System.Drawing;
using System.Drawing.Drawing2D;
using ItopVector.Core.Document;
using ItopVector.Core.Func;

namespace ItopVector.Core.Figure
{

	/// <summary>
	/// Ô²
	/// </summary>
    public class Circle : GraphPath
    {
        // Methods
        internal Circle(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
        {
        }


        // Properties
		/// <summary>
		/// Ô²ÐÄx×ø±ê
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
		/// Ô²ÐÄy×ø±ê
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

		/// <summary>
		/// Ô²Â·¾¶
		/// </summary>
        public override GraphicsPath GPath
        {
            get
            {
                SvgDocument document1 = base.OwnerDocument;
                if (this.pretime != document1.ControlTime)
                {
					this.connectPoints=new PointF[0];
                    this.graphPath = new GraphicsPath();
                    float single1 = this.R;
                    if (single1 != 0f)
                    {
                        this.graphPath.AddEllipse(this.CX - single1, this.CY - single1, single1 * 2f, single1 * 2f);
						this.CreateConnectPoint();
                    }

                }
                return this.graphPath;
            }
            set
            {
                this.graphPath = value;
            }
        }

		/// <summary>
		/// Ô²°ë¾¶
		/// </summary>
        public float R
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("r"))
                {
                    this.r = (float) this.svgAnimAttributes["r"];
                }
                else
                {
                    this.r = 0f;
                }
                return this.r;
            }
            set
            {
                if (this.r != value)
                {
                    this.r = value;
                    AttributeFunc.SetAttributeValue(this, "r", value.ToString());
                }
            }
        }


        // Fields
        private float cx;
        private float cy;
        private float r;
    }
}

