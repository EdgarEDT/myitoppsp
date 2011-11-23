namespace ItopVector.Core.Paint
{
    using ItopVector.Core.Figure;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

	/// <summary>
	/// µ¥É«Àà
	/// </summary>
    public class SolidColor : ISvgBrush
    {
        // Methods
        public SolidColor(System.Drawing.Color temp)
        {
            this.opacity = 1f;
            this.pen = new Pen(temp);
            this.color = temp;
            this.opacity = ((float) this.color.A) / 255f;
        }

		public ISvgBrush Clone()
		{
			SolidColor brush1 =this.MemberwiseClone() as SolidColor;
			
			brush1.pen =this.pen.Clone() as Pen;
			
	
            return brush1;
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
            return true;
        }

        public void Paint(GraphPath figure, Graphics g, int time)
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
            SolidBrush brush1 = null;
            if (this.color.IsEmpty)
            {
                brush1 = new SolidBrush(System.Drawing.Color.Empty);
            }
            else
            {
                brush1 = new SolidBrush(System.Drawing.Color.FromArgb((int) (255f * opacity), this.color.R, this.color.G, this.color.B));
            }
            g.FillPath(brush1, path);
        }

        public void Stroke(GraphicsPath path, Graphics g, int time, float opacity)
        {
            SolidBrush brush1 = new SolidBrush(System.Drawing.Color.FromArgb((int) (this.color.A * opacity), this.color.R, this.color.G, this.color.B));
            this.pen.Brush = brush1;
//			this.pen.ResetTransform();
//			this.pen.Width=1;//
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
				if (value==null)return;
                this.pen = value;
				
            }
        }


        // Fields
        private System.Drawing.Color color;
        private float opacity;
        private System.Drawing.Pen pen;
    }
}

