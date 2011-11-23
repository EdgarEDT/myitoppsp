namespace ItopVector.Core.Paint
{
    using ItopVector.Core.Figure;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
	using ItopVector.Core.Func;

	/// <summary>
	/// µ¥É«Àà
	/// </summary>
    internal class RatHatchBrush : ISvgBrush
    {
        // Methods
		public RatHatchBrush():this(Color.White,ItopVector.PatternType.None,Color.Black)
		{
			
		}
		public RatHatchBrush(Color backcolor,ItopVector.PatternType patterntype ,Color forecolor):this(Color.White,ItopVector.PatternType.None,Color.Black,1f)
		{
			
		}
		public RatHatchBrush(Color backcolor,ItopVector.PatternType patterntype ,Color forecolor,float fillopacity)
		{
			pattern=new ItopVector.Struct.Pattern(backcolor,patterntype,forecolor);	
			
			this.opacity = fillopacity;
			this.pen = null;
			this.color = forecolor;
			this.opacity = ((float) this.color.A) / 255f;
		}
		public RatHatchBrush(Struct.Pattern pattern)
		{
			this.pattern=pattern;
			this.pen = null;
			this.color = pattern.ForeColor;
			this.opacity = ((float) this.color.A) / 255f;

		}
       

        public ISvgBrush Clone()
        {
            return new SolidColor(this.color);
        }

        public bool IsEmpty()
        {
            if (this.color == System.Drawing.Color.Empty)
            {
                return true;
            }
            return false;
        }

        public bool IsSolidColor()
        {
            return false;
        }

        public void Paint(GraphPath figure, Graphics g, int time)
        {
            Brush brush1 = null;
            brush1 = BrushManager.GetGDIBrushFromPatten(this.pattern,figure.GetBounds());
           
            g.FillPath(brush1, figure.GPath);
        }

        public void Paint(GraphicsPath path, Graphics g, int time)
        {
            SolidBrush brush1 = null;
            if (this.color.IsEmpty)
            {
                brush1 = new SolidBrush(System.Drawing.Color.Empty);
            }
            else
            {
                brush1 = new SolidBrush(System.Drawing.Color.FromArgb((int) (255f * this.opacity), this.color.R, this.color.G, this.color.B));
            }
            g.FillPath(brush1, path);
        }

        public void Paint(GraphicsPath path, Graphics g, int time, float opacity)
        {
            Brush brush1 = null;
          
			this.pattern.Opacity=opacity;

            brush1 = BrushManager.GetGDIBrushFromPatten(this.pattern,path.GetBounds());
           
            g.FillPath(brush1, path);
        }

        public void Stroke(GraphicsPath path, Graphics g, int time, float opacity)
        {
            SolidBrush brush1 = new SolidBrush(System.Drawing.Color.FromArgb((int) (this.color.A * opacity), this.color.R, this.color.G, this.color.B));
            this.pen.Brush = brush1;
            g.DrawPath(this.pen, path);
        }

        public override string ToString()
        {
            if (this.color.IsEmpty)
            {
                return "none";
            }
            string text1 = "fill:";
            if ((this.color.IsKnownColor || this.color.IsNamedColor) || this.color.IsSystemColor)
            {
                text1 = text1 + this.color.Name.ToLower() + ";";
            }
            else
            {
                string text3 = text1;
                string[] textArray1 = new string[8] { text3, "rgb(", this.Color.R.ToString(), ",", this.Color.G.ToString(), ",", this.Color.B.ToString(), ");" } ;
                text1 = string.Concat(textArray1);
            }
            double num4 = Math.Round((double) this.Opacity, 2);
            return (text1 + "fill-opacity:" + num4.ToString() + ";");
        }


        // Properties
        public System.Drawing.Color Color
        {
            get
            {
                return System.Drawing.Color.FromArgb((int) (this.opacity * 255f), this.color.R, this.color.G, this.color.B);
            }
            set
            {
                if (this.color != value)
                {
                    this.color = value;
                }
            }
        }

        public ElementType ElementType
        {
            get
            {
                return ElementType.SolidColor;
            }
        }

        public float Opacity
        {
            get
            {
                return this.opacity;
            }
            set
            {
                this.opacity = value;
            }
        }

        public System.Drawing.Pen Pen
        {
			get
			{
				return this.pen;
			}
            set
            {
                this.pen = value;
            }
        }
		public Struct.Pattern Pattern
		{
			get
			{
				return this.pattern;
			}
			set
			{
				this.pattern=value;
			}
		}


        // Fields
        private System.Drawing.Color color;
        private float opacity;
        private System.Drawing.Pen pen;
		private Struct.Pattern pattern;
    }
}

