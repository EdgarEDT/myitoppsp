namespace ItopVector.Core.Paint
{
    using ItopVector.Core;
    using ItopVector.Core.Config;
    using ItopVector.Core.Func;
    using ItopVector.Core.Interface.Figure;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;

	/// <summary>
	/// 元件轮廓
	/// </summary>
    public class Stroke:ICloneable
    {
        // Events
        public event EventHandler OnStrokeBrushChanged;

		// Fields
		private ISvgBrush brush;
		public string DashArray;
		public float DashOffset;
		public LineCap Linecap;
		public LineJoin Linejoin;
		public int MiterLimit;
		public float Opacity;
		private Pen pen;
		private Color strokeColor;
		public float Width;

        // Methods
        public Stroke()
        {
            this.pen = Pens.Black;
            this.brush = new SolidColor(Color.Empty);
            this.strokeColor = Color.Empty;
            this.Width = 1f;
            this.Linecap = LineCap.Square;
            this.Linejoin = LineJoin.Miter;
            this.MiterLimit = 4;
            this.DashArray = null;
            this.DashOffset = 0f;
            this.Opacity = 1f;
        }

        public Stroke(ISvgBrush brush)
        {
            this.pen = brush.Pen;
            //this.brush = new SolidColor(pen.Color);
            this.strokeColor = pen.Color;
            this.Width = 1f;
            this.Linecap = LineCap.Square;
            this.Linejoin = LineJoin.Miter;
            this.MiterLimit = 4;
            this.DashArray = null;
            this.DashOffset = 0f;
            this.Opacity = 1f;
            this.brush = brush.Clone();
        }

        public static Stroke GetStroke(IGraphPath path)
        {
            Pen pen1 = null;
            string text1 = AttributeFunc.ParseAttribute("stroke", (SvgElement) path, false).ToString();
            Color color1 = Color.Empty;
            ISvgBrush brush1 = new SolidColor(Color.Empty);
            if ((text1 != null) || (text1 != string.Empty))
            {
                brush1 = BrushManager.Parsing(text1, ((SvgElement) path).OwnerDocument);
            }
            text1 = AttributeFunc.ParseAttribute("stroke-opacity", (SvgElement) path, false).ToString();
            float single1 = 1f;
            if ((text1 != string.Empty) && (text1 != null))
            {
                single1 = Math.Max((float) 0f, Math.Min((float) 255f, ItopVector.Core.Func.Number.ParseFloatStr(text1)));
            }
            if (single1 > 1f)
            {
                single1 /= 255f;
            }
            single1 = Math.Min(path.Opacity, path.StrokeOpacity);
            float single2 = 1f;
            object obj1 = AttributeFunc.ParseAttribute("stroke-width", (SvgElement) path, false);
            if (obj1 is float)
            {
                single2 = (float) obj1;
            }
            pen1 = new Pen(Color.Empty, single2);
            pen1.Alignment = PenAlignment.Outset;
            string text2 = AttributeFunc.FindAttribute("stroke-linecap", (SvgElement) path).ToString();
            if (text2 == "round")
            {
                LineCap cap1;
                pen1.EndCap = cap1 = LineCap.Round;
                pen1.StartCap = cap1;
            }
            else if (text2 == "square")
            {
                LineCap cap2;
                pen1.EndCap = cap2 = LineCap.Square;
                pen1.StartCap = cap2;
            }
            else
            {
                LineCap cap3;
                pen1.EndCap = cap3 = LineCap.Flat;
                pen1.StartCap = cap3;
            }
            string text3 = AttributeFunc.FindAttribute("stroke-linejoin", (SvgElement) path).ToString();
            if (text3 == "round")
            {
                pen1.LineJoin = LineJoin.Round;
            }
            else if (text3 == "bevel")
            {
                pen1.LineJoin = LineJoin.Bevel;
            }
            else
            {
                pen1.LineJoin = LineJoin.Miter;
            }
            string text4 = AttributeFunc.FindAttribute("stroke-miterlimit", (SvgElement) path).ToString();
            if (text4 == "")
            {
                text4 = "4";
            }
            float single3 = ItopVector.Core.Func.Number.parseToFloat(text4, (SvgElement) path, SvgLengthDirection.Horizontal);
            if (single3 < 1f)
            {
                throw new Exception("stroke-miterlimit " + ItopVector.Core.Config.Config.GetLabelForName("notlowerstr") + " 1:" + text4);
            }
            pen1.MiterLimit = single3;
            string text5 = AttributeFunc.FindAttribute("stroke-dasharray", (SvgElement) path).ToString();
            if ((text5 != "") && (text5 != "none"))
            {
                Regex regex1 = new Regex(@"[\s\,]+");
                text5 = regex1.Replace(text5, ",");
                char[] chArray1 = new char[1] { ',' } ;
                string[] textArray1 = text5.Split(chArray1);
                float[] singleArray1 = new float[textArray1.GetLength(0)];
                for (int num1 = 0; num1 < textArray1.GetLength(0); num1++)
                {
                    singleArray1[num1] = ItopVector.Core.Func.Number.ParseFloatStr(textArray1[num1]) / pen1.Width;
                }
                if ((singleArray1.GetLength(0) % 2) == 1)
                {
                    float[] singleArray2 = new float[singleArray1.GetLength(0) * 2];
                    singleArray1.CopyTo(singleArray2, 0);
                    singleArray1.CopyTo(singleArray2, singleArray1.GetLength(0));
                    singleArray1 = singleArray2;
                }
                pen1.DashPattern = singleArray1;
            }
            string text6 = AttributeFunc.FindAttribute("stroke-dashoffset", (SvgElement) path).ToString();
            float single4 = 0f;
            if (text6 != "")
            {
                single4 = ItopVector.Core.Func.Number.parseToFloat(text6, (SvgElement) path, SvgLengthDirection.Horizontal);
            }
            float single5 = Math.Abs((float) AnimFunc.GetAnimateValue((SvgElement) path, "stroke-dashoffset", DomType.SvgNumber, single4)) / pen1.Width;
            pen1.DashOffset = single5 / single2;
            if (brush1 != null)
            {
                brush1.Pen = pen1;
            }
            Stroke stroke1 = new Stroke(brush1);
            stroke1.Opacity = single1;
            return stroke1;
        }

        public void Paint(Graphics g, IGraphPath path, int time)
        {
			if(path.IsChanged||(path as SvgElement).pretime==-1)
			{
				this.Update(path);
				path.IsChanged=false;
			}
            if (this.brush != null)
            {
                this.brush.Stroke(path.GPath, g, time, this.Opacity);
            }
        }
		public void Paint(Graphics g, IGraphPath path,GraphicsPath gpath, int time)
		{
			if(path.IsChanged ||(path as SvgElement).pretime==-1)
			{
				this.Update(path);
				path.IsChanged=false;
			}
			if (this.brush != null)
			{
				this.brush.Stroke(gpath, g, time, this.Opacity);
			}
		}

        public void Paint(Graphics g, GraphicsPath path, int time)
        {

            if (this.brush != null)
            {
                this.brush.Stroke(path, g, time, this.Opacity);
            }
        }
        public void Update(IGraphPath path, int time) {
            if (path.IsChanged || (path as SvgElement).pretime == -1) {
                this.Update(path);
                path.IsChanged = false;
            }
        }
        private void Update(IGraphPath path)
        {
            Pen pen1 = null;
            string text1 = AttributeFunc.ParseAttribute("stroke", (SvgElement) path, false).ToString();
            ISvgBrush brush1 = new SolidColor(Color.Black);
            if ((text1 != null) && (text1 != string.Empty))
            {
                brush1 = BrushManager.Parsing(text1, ((SvgElement) path).OwnerDocument);
            }
            text1 = AttributeFunc.ParseAttribute("stroke-opacity", (SvgElement) path, false).ToString();
            float single1 = 1f;
            if ((text1 != string.Empty) && (text1 != null))
            {
                single1 = Math.Max((float) 0f, Math.Min((float) 255f, ItopVector.Core.Func.Number.ParseFloatStr(text1)));
            }
            if (single1 > 1f)
            {
                single1 /= 255f;
            }
            single1 = Math.Min(path.Opacity, path.StrokeOpacity);
            float single2 = 1f;
            object obj1 = AttributeFunc.ParseAttribute("stroke-width", (SvgElement) path, false);
            if (obj1 is float)
            {
                single2 = (float) obj1;
            }
            pen1 = new Pen(Color.Empty, single2);
            pen1.Alignment = PenAlignment.Outset;
            string text2 = AttributeFunc.FindAttribute("stroke-linecap", (SvgElement) path).ToString();
            if (text2 == "round")
            {
                LineCap cap1;
                pen1.EndCap = cap1 = LineCap.Round;
                pen1.StartCap = cap1;
            }
            else if (text2 == "square")
            {
                LineCap cap2;
                pen1.EndCap = cap2 = LineCap.Square;
                pen1.StartCap = cap2;
            }
            else
            {
                LineCap cap3;
                pen1.EndCap = cap3 = LineCap.Flat;
                pen1.StartCap = cap3;
            }
            string text3 = AttributeFunc.FindAttribute("stroke-linejoin", (SvgElement) path).ToString();
            if (text3 == "round")
            {
                pen1.LineJoin = LineJoin.Round;
            }
            else if (text3 == "bevel")
            {
                pen1.LineJoin = LineJoin.Bevel;
            }
            else
            {
                pen1.LineJoin = LineJoin.Miter;
            }
            string text4 = AttributeFunc.FindAttribute("stroke-miterlimit", (SvgElement) path).ToString();
            if (text4 == "")
            {
                text4 = "4";
            }
            float single3 = ItopVector.Core.Func.Number.parseToFloat(text4, (SvgElement) path, SvgLengthDirection.Horizontal);
            if (single3 < 1f)
            {
                throw new Exception("stroke-miterlimit " + ItopVector.Core.Config.Config.GetLabelForName("notlowerstr") + " 1:" + text4);
            }
            pen1.MiterLimit = single3;
            string text5 = AttributeFunc.FindAttribute("stroke-dasharray", (SvgElement) path).ToString();
            if ((text5 != "") && (text5 != "none"))
            {
                Regex regex1 = new Regex(@"[\s\,]+");
                text5 = regex1.Replace(text5, ",");
                char[] chArray1 = new char[1] { ',' } ;
                string[] textArray1 = text5.Split(chArray1);
                float[] singleArray1 = new float[textArray1.GetLength(0)];
                for (int num1 = 0; num1 < textArray1.GetLength(0); num1++)
                {
                    singleArray1[num1] = ItopVector.Core.Func.Number.parseToFloat(textArray1[num1], (SvgElement) path, SvgLengthDirection.Horizontal) / pen1.Width;
                }
                if ((singleArray1.GetLength(0) % 2) == 1)
                {
                    float[] singleArray2 = new float[singleArray1.GetLength(0) * 2];
                    singleArray1.CopyTo(singleArray2, 0);
                    singleArray1.CopyTo(singleArray2, singleArray1.GetLength(0));
                    singleArray1 = singleArray2;
                }
                pen1.DashPattern = singleArray1;
            }
            string text6 = AttributeFunc.FindAttribute("stroke-dashoffset", (SvgElement) path).ToString();
            float single4 = 0f;
            if (text6 != "")
            {
                single4 = ItopVector.Core.Func.Number.parseToFloat(text6, (SvgElement) path, SvgLengthDirection.Horizontal);
            }
            float single5 = Math.Abs((float) AnimFunc.GetAnimateValue((SvgElement) path, "stroke-dashoffset", DomType.SvgNumber, single4)) / pen1.Width;
            pen1.DashOffset = single5 / single2;

			this.pen=pen1;
			this.Width=pen1.Width;
            if (brush1 != null)
            {
                brush1.Pen = pen1;
            }
            this.Opacity = single1;
            this.brush = brush1;
        }


        // Properties
        public ISvgBrush Brush
        {
            get
            {
                return this.brush;
            }
            set
            {
                if (this.brush != value)
                {
                    this.brush = value;
                    if (this.OnStrokeBrushChanged != null)
                    {
                        this.OnStrokeBrushChanged(this, new EventArgs());
                    }
                }
            }
        }
		public Color StrokeColor
		{
			get
			{
				if((this.brush as SolidColor)!=null)
				{
					this.strokeColor=((SolidColor)brush).Color;
				}

				return this.strokeColor;
			}
			set
			{
				if(value==this.strokeColor)return;
				this.strokeColor=value;
				if((this.brush as SolidColor)!=null)
				{
					((SolidColor)brush).Color=this.strokeColor;
					if (this.OnStrokeBrushChanged != null)
					{
						this.OnStrokeBrushChanged(this, new EventArgs());
					}
				}
			}
		}

        public Pen StrokePen
        {
            get
            {
                return this.pen;
            }
        }


       
		#region ICloneable 成员

		public object Clone()
		{
			Stroke stroke1 =this.MemberwiseClone() as Stroke;
			stroke1.brush =this.brush.Clone();
			stroke1.pen = this.pen.Clone() as Pen;
			return stroke1;
		}

		#endregion
	}
}

