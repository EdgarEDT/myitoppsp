namespace ItopVector.Core.Paint
{
    using ItopVector.Core;
    using ItopVector.Core.Document;
    using ItopVector.Core.Func;
    using System;
    using System.Drawing;

    public class GradientStop : SvgElement
    {
        // Methods
        internal GradientStop(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
        {
            this.stopcolor = System.Drawing.Color.Black;
            this.colorOffset = 0f;
            this.stopopacity = 1f;
        }


        // Properties
        public System.Drawing.Color Color
        {
            get
            {
                string text1 = string.Empty;
                if (this.svgAnimAttributes.ContainsKey("stop-color"))
                {
                    text1 = (string) this.svgAnimAttributes["stop-color"];
                }
                this.stopcolor = ColorFunc.ParseColor(text1);
                return this.stopcolor;
            }
            set
            {
                if (this.stopcolor != value)
                {
                    this.stopcolor = value;
                    string text1 = string.Empty;
                    if ((this.stopcolor.IsKnownColor || this.stopcolor.IsNamedColor) || this.stopcolor.IsSystemColor)
                    {
                        text1 = this.stopcolor.Name.ToLower();
                    }
                    else
                    {
                        string[] textArray1 = new string[7] { "rgb(", this.stopcolor.R.ToString(), ",", this.stopcolor.G.ToString(), ",", this.stopcolor.B.ToString(), ")" } ;
                        text1 = string.Concat(textArray1);
                    }
                    AttributeFunc.SetAttributeValue(this, "stop-color", text1);
                }
            }
        }

        public float ColorOffset
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("offset"))
                {
                    this.colorOffset = (float) this.svgAnimAttributes["offset"];
                }
                else
                {
                    this.colorOffset = 0f;
                }
                return this.colorOffset;
            }
            set
            {
                this.colorOffset = (float) Math.Round((double) value, 3);
                AttributeFunc.SetAttributeValue(this, "offset", this.colorOffset.ToString());
            }
        }

        public float Opacity
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("stop-opacity"))
                {
                    this.stopopacity = (float) this.svgAnimAttributes["stop-opacity"];
                }
                else
                {
                    this.stopopacity = 1f;
                }
                return this.stopopacity;
            }
            set
            {
                if (value > 1f)
                {
                    this.stopopacity = value / 255f;
                }
                else
                {
                    this.stopopacity = value;
                }
                AttributeFunc.SetAttributeValue(this, "stop-opacity", this.stopopacity.ToString());
            }
        }


        // Fields
        private float colorOffset;
        private System.Drawing.Color stopcolor;
        private float stopopacity;
    }
}

