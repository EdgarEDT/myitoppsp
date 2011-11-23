namespace ItopVector.Core.Paint
{
    using ItopVector.Core;
    using ItopVector.Core.Document;
    using ItopVector.Core.Figure;
    using ItopVector.Core.Func;
    using ItopVector.Core.Interface;
    using ItopVector.Core.Interface.Paint;
    using ItopVector.Core.Types;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Xml;

	/// <summary>
	/// Ô²ÐÎÌÝ¶È
	/// </summary>
    public class RadialGradients : ContainerElement, IGradientBrush, IContainer
    {
        // Methods
        internal RadialGradients(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
        {
            this.cx = 0.5f;
            this.cy = 0.5f;
            this.r = 0.5f;
            this.gradientpath = new GraphicsPath();
            this.gradientTransform = new Transf();
            this.bounds = RectangleF.Empty;
            this.gradientUnit = Units.ObjectBoundingBox;
            this.spreadMethod = SpreadMethods.Pad;
            this.stops = new SvgElementCollection();
            this.boundsPoints = new PointF[0];
            this.coord = new Matrix();
            this.pen = null;
        }

        public new ISvgBrush Clone()
        {
            return (ISvgBrush) base.Clone();
        }

        public new bool IsEmpty()
        {
            return false;
        }

        public bool IsSolidColor()
        {
            return true;
        }

        public override bool IsValidChild(XmlNode element)
        {
            if (element is GradientStop)
            {
                return true;
            }
            return false;
        }

        public void Paint(GraphPath figure, Graphics g, int time)
        {
            this.Paint(figure.GPath, g, time, 1f);
        }

        public void Paint(GraphicsPath path, Graphics g, int time)
        {
            this.Paint(path, g, time, 1f);
        }

        public void Paint(GraphicsPath path, Graphics g, int time, float opacity)
        {
            int num1 = 0;
            int num2 = 0;
            AnimFunc.CreateAnimateValues(this, time, out num1, out num2);
            path.FillMode = FillMode.Alternate;
            g.SmoothingMode = base.OwnerDocument.SmoothingMode;
            SpreadMethods methods1 = this.SpreadMethod;
            bool flag1 = this.Units == Units.UserSpaceOnUse;
            float single1 = this.CX;
            float single2 = this.CY;
            float single3 = this.R;
            float single4 = this.FX;
            float single5 = this.FY;
            PointF[] tfArray2 = new PointF[7] { new PointF(single1, single2), new PointF(single1 + single3, single2), new PointF(single1 + (single3 * ((float) Math.Sin(1.8325957145940459))), single2 + (single3 * ((float) Math.Cos(1.8325957145940459)))), new PointF(single1 + (single3 * ((float) Math.Sin(1.3089969389957472))), single2 + (single3 * ((float) Math.Cos(1.3089969389957472)))), new PointF(single1, single2 + single3), PointF.Empty, PointF.Empty } ;
            this.boundsPoints = tfArray2;
            GraphicsPath path1 = this.gradientpath;
            path1.Reset();
            path1.AddEllipse((float) (single1 - single3), (float) (single2 - single3), (float) (2f * single3), (float) (2f * single3));
            RectangleF ef1 = RectangleF.Empty;
            RectangleF ef2 = PathFunc.GetBounds(path);
            RectangleF ef3 = RectangleF.Empty;
            this.coord.Reset();
            if (flag1)
            {
                ef3 = ((SVG) base.OwnerDocument.DocumentElement).ViewPort;
            }
            else
            {
                ef2 = new RectangleF(0f, 0f, 1f, 1f);
                ef3 = ef2;
                ef1 = PathFunc.GetBounds(path);
                this.coord.Translate(ef1.X, ef1.Y);
                this.coord.Scale(ef1.Width, ef1.Height);
            }
			//if (this.stops.Count==0)return;

            ColorBlend blend1 = new ColorBlend(this.Stops.Count);
            Color[] colorArray1 = new Color[this.Stops.Count];
            float[] singleArray1 = new float[this.Stops.Count];
            SvgElementCollection collection1 = this.Stops;
            for (int num3 = 0; num3 < collection1.Count; num3++)
            {
                GradientStop stop1 = (GradientStop) collection1[num3];
                AnimFunc.CreateAnimateValues(stop1, time, out num1, out num2);
                int num4 = 0xff;
                if ((stop1.Opacity >= 0f) && (stop1.Opacity <= 255f))
                {
                    if (stop1.Opacity <= 1f)
                    {
                        num4 = (int) (stop1.Opacity * 255f);
                    }
                    else
                    {
                        num4 = (int) stop1.Opacity;
                    }
                }
                num4 = (int) Math.Min((float) (opacity * 255f), (float) num4);
                Color color1 = stop1.Color;
                float single6 = Math.Min((float) 1f, Math.Max((float) 0f, stop1.ColorOffset));
                colorArray1[num3] = Color.FromArgb(num4, color1.R, color1.G, color1.B);
                singleArray1[num3] = single6;
            }
            float[] singleArray2 = (float[]) singleArray1.Clone();
            Color[] colorArray2 = (Color[]) colorArray1.Clone();
            Array.Sort(singleArray2, colorArray2);
            Color color2 = colorArray2[0];
            Color color3 = colorArray2[colorArray2.Length - 1];
            if (singleArray2[0] != 0f)
            {
                float[] singleArray3 = (float[]) singleArray2.Clone();
                Color[] colorArray3 = (Color[]) colorArray2.Clone();
                singleArray2 = new float[singleArray2.Length + 1];
                colorArray2 = new Color[colorArray2.Length + 1];
                colorArray3.CopyTo(colorArray2, 1);
                singleArray3.CopyTo(singleArray2, 1);
                singleArray2[0] = 0f;
                colorArray2[0] = color2;
            }
            if (singleArray2[singleArray2.Length - 1] != 1f)
            {
                float[] singleArray4 = (float[]) singleArray2.Clone();
                Color[] colorArray4 = (Color[]) colorArray2.Clone();
                singleArray2 = new float[singleArray2.Length + 1];
                singleArray4.CopyTo(singleArray2, 0);
                singleArray2[singleArray2.Length - 1] = 1f;
                colorArray2 = new Color[colorArray2.Length + 1];
                colorArray4.CopyTo(colorArray2, 0);
                colorArray2[colorArray2.Length - 1] = color3;
            }
            if (methods1 == SpreadMethods.Pad)
            {
                float single7 = Math.Min((float) (single1 - single3), ef2.X);
                float single8 = Math.Min((float) (single2 - single3), ef2.Y);
                float single9 = Math.Max(single3, (float) (ef2.Width / 2f));
                float single10 = this.cx - single3;
                float single11 = this.r;
                for (int num5 = 0; num5 < singleArray2.Length; num5++)
                {
                    singleArray2[num5] = ((single10 + (single11 * singleArray2[num5])) - single7) / single9;
                }
                if (singleArray2[0] != 0f)
                {
                    float[] singleArray5 = (float[]) singleArray2.Clone();
                    Color[] colorArray5 = (Color[]) colorArray2.Clone();
                    singleArray2 = new float[singleArray2.Length + 1];
                    colorArray2 = new Color[colorArray2.Length + 1];
                    colorArray5.CopyTo(colorArray2, 1);
                    singleArray5.CopyTo(singleArray2, 1);
                    singleArray2[0] = 0f;
                    colorArray2[0] = color2;
                }
                if (singleArray2[singleArray2.Length - 1] != 1f)
                {
                    float[] singleArray6 = (float[]) singleArray2.Clone();
                    Color[] colorArray6 = (Color[]) colorArray2.Clone();
                    singleArray2 = new float[singleArray2.Length + 1];
                    singleArray6.CopyTo(singleArray2, 0);
                    singleArray2[singleArray2.Length - 1] = 1f;
                    colorArray2 = new Color[colorArray2.Length + 1];
                    colorArray6.CopyTo(colorArray2, 0);
                    colorArray2[colorArray2.Length - 1] = color3;
                }
            }
            Array.Reverse(colorArray2);
            Array.Reverse(singleArray2);
            for (int num6 = 0; num6 < singleArray2.Length; num6++)
            {
                singleArray2[num6] = 1f - singleArray2[num6];
            }
            Matrix matrix1 = this.Transform.Matrix.Clone();
            path1 = (GraphicsPath) this.gradientpath.Clone();
            path1.Transform(matrix1);
            this.brush = new PathGradientBrush(path1);
            blend1.Colors = colorArray2;
            blend1.Positions = singleArray2;
            this.brush.InterpolationColors = blend1;
            if (methods1 == SpreadMethods.Reflect)
            {
                this.brush.WrapMode = WrapMode.Tile;
            }
            else if (methods1 == SpreadMethods.Repeat)
            {
                this.brush.WrapMode = WrapMode.TileFlipXY;
            }
            else
            {
                this.brush.WrapMode = WrapMode.Clamp;
            }
            if (AttributeFunc.FindAttribute("fx", this).ToString() == string.Empty)
            {
                single4 = this.CX;
            }
            if (AttributeFunc.FindAttribute("fy", this).ToString() == string.Empty)
            {
                single5 = this.CY;
            }
            PointF[] tfArray3 = new PointF[1] { new PointF(single4, single5) } ;
            PointF[] tfArray1 = tfArray3;
            matrix1.TransformPoints(tfArray1);
            this.brush.CenterPoint = tfArray1[0];
            g.FillPath(new SolidBrush(colorArray2[0]), path);
            GraphicsContainer container1 = g.BeginContainer();
            g.Transform = this.coord;
            Matrix matrix2 = this.coord.Clone();
            matrix2.Invert();
            GraphicsPath path2 = (GraphicsPath) path.Clone();
            path2.Transform(matrix2);
            g.FillPath(this.brush, path2);
            g.EndContainer(container1);
            this.pretime = -1;
        }

        public void Stroke(GraphicsPath path, Graphics g, int time, float opacity)
        {
            int num1 = 0;
            int num2 = 0;
            AnimFunc.CreateAnimateValues(this, time, out num1, out num2);
            path.FillMode = FillMode.Alternate;
            g.SmoothingMode = base.OwnerDocument.SmoothingMode;
            SpreadMethods methods1 = this.SpreadMethod;
            bool flag1 = this.Units == Units.UserSpaceOnUse;
            float single1 = this.CX;
            float single2 = this.CY;
            float single3 = this.R;
            float single4 = this.FX;
            float single5 = this.FY;
            PointF[] tfArray2 = new PointF[7] { new PointF(single1, single2), new PointF(single1 + single3, single2), new PointF(single1 + (single3 * ((float) Math.Sin(1.8325957145940459))), single2 + (single3 * ((float) Math.Cos(1.8325957145940459)))), new PointF(single1 + (single3 * ((float) Math.Sin(1.3089969389957472))), single2 + (single3 * ((float) Math.Cos(1.3089969389957472)))), new PointF(single1, single2 + single3), PointF.Empty, PointF.Empty } ;
            this.boundsPoints = tfArray2;
            GraphicsPath path1 = this.gradientpath;
            path1.Reset();
            path1.AddEllipse((float) (single1 - single3), (float) (single2 - single3), (float) (2f * single3), (float) (2f * single3));
            RectangleF ef1 = RectangleF.Empty;
            RectangleF ef2 = PathFunc.GetBounds(path);
            RectangleF ef3 = RectangleF.Empty;
            this.coord.Reset();
            if (flag1)
            {
                ef3 = ((SVG) base.OwnerDocument.DocumentElement).ViewPort;
            }
            else
            {
                ef2 = new RectangleF(0f, 0f, 1f, 1f);
                ef3 = ef2;
                ef1 = PathFunc.GetBounds(path);
                this.coord.Translate(ef1.X, ef1.Y);
                this.coord.Scale(ef1.Width, ef1.Height);
            }
            ColorBlend blend1 = new ColorBlend(this.Stops.Count);
            Color[] colorArray1 = new Color[this.Stops.Count];
            float[] singleArray1 = new float[this.Stops.Count];
            SvgElementCollection collection1 = this.Stops;
            for (int num3 = 0; num3 < collection1.Count; num3++)
            {
                GradientStop stop1 = (GradientStop) collection1[num3];
                AnimFunc.CreateAnimateValues(stop1, time, out num1, out num2);
                int num4 = 0xff;
                if ((stop1.Opacity >= 0f) && (stop1.Opacity <= 255f))
                {
                    if (stop1.Opacity <= 1f)
                    {
                        num4 = (int) (stop1.Opacity * 255f);
                    }
                    else
                    {
                        num4 = (int) stop1.Opacity;
                    }
                }
                num4 = (int) Math.Min((float) (opacity * 255f), (float) num4);
                Color color1 = stop1.Color;
                float single6 = Math.Min((float) 1f, Math.Max((float) 0f, stop1.ColorOffset));
                colorArray1[num3] = Color.FromArgb(num4, color1.R, color1.G, color1.B);
                singleArray1[num3] = single6;
            }
            float[] singleArray2 = (float[]) singleArray1.Clone();
            Color[] colorArray2 = (Color[]) colorArray1.Clone();
            Array.Sort(singleArray2, colorArray2);
            Color color2 = colorArray2[0];
            Color color3 = colorArray2[colorArray2.Length - 1];
            if (singleArray2[0] != 0f)
            {
                float[] singleArray3 = (float[]) singleArray2.Clone();
                Color[] colorArray3 = (Color[]) colorArray2.Clone();
                singleArray2 = new float[singleArray2.Length + 1];
                colorArray2 = new Color[colorArray2.Length + 1];
                colorArray3.CopyTo(colorArray2, 1);
                singleArray3.CopyTo(singleArray2, 1);
                singleArray2[0] = 0f;
                colorArray2[0] = color2;
            }
            if (singleArray2[singleArray2.Length - 1] != 1f)
            {
                float[] singleArray4 = (float[]) singleArray2.Clone();
                Color[] colorArray4 = (Color[]) colorArray2.Clone();
                singleArray2 = new float[singleArray2.Length + 1];
                singleArray4.CopyTo(singleArray2, 0);
                singleArray2[singleArray2.Length - 1] = 1f;
                colorArray2 = new Color[colorArray2.Length + 1];
                colorArray4.CopyTo(colorArray2, 0);
                colorArray2[colorArray2.Length - 1] = color3;
            }
            if (methods1 == SpreadMethods.Pad)
            {
                float single7 = Math.Min((float) (single1 - single3), ef2.X);
                float single8 = Math.Min((float) (single2 - single3), ef2.Y);
                float single9 = Math.Max(single3, (float) (ef2.Width / 2f));
                float single10 = this.cx - single3;
                float single11 = this.r;
                for (int num5 = 0; num5 < singleArray2.Length; num5++)
                {
                    singleArray2[num5] = ((single10 + (single11 * singleArray2[num5])) - single7) / single9;
                }
                if (singleArray2[0] != 0f)
                {
                    float[] singleArray5 = (float[]) singleArray2.Clone();
                    Color[] colorArray5 = (Color[]) colorArray2.Clone();
                    singleArray2 = new float[singleArray2.Length + 1];
                    colorArray2 = new Color[colorArray2.Length + 1];
                    colorArray5.CopyTo(colorArray2, 1);
                    singleArray5.CopyTo(singleArray2, 1);
                    singleArray2[0] = 0f;
                    colorArray2[0] = color2;
                }
                if (singleArray2[singleArray2.Length - 1] != 1f)
                {
                    float[] singleArray6 = (float[]) singleArray2.Clone();
                    Color[] colorArray6 = (Color[]) colorArray2.Clone();
                    singleArray2 = new float[singleArray2.Length + 1];
                    singleArray6.CopyTo(singleArray2, 0);
                    singleArray2[singleArray2.Length - 1] = 1f;
                    colorArray2 = new Color[colorArray2.Length + 1];
                    colorArray6.CopyTo(colorArray2, 0);
                    colorArray2[colorArray2.Length - 1] = color3;
                }
            }
            Array.Reverse(colorArray2);
            Array.Reverse(singleArray2);
            for (int num6 = 0; num6 < singleArray2.Length; num6++)
            {
                singleArray2[num6] = 1f - singleArray2[num6];
            }
            Matrix matrix1 = this.Transform.Matrix.Clone();
            path1 = (GraphicsPath) this.gradientpath.Clone();
            path1.Transform(matrix1);
            this.brush = new PathGradientBrush(path1);
            blend1.Colors = colorArray2;
            blend1.Positions = singleArray2;
            this.brush.InterpolationColors = blend1;
            if (methods1 == SpreadMethods.Reflect)
            {
                this.brush.WrapMode = WrapMode.TileFlipXY;
            }
            else if (methods1 == SpreadMethods.Reflect)
            {
                this.brush.WrapMode = WrapMode.Tile;
            }
            else
            {
                this.brush.WrapMode = WrapMode.Clamp;
            }
            if (AttributeFunc.FindAttribute("fx", this).ToString() == string.Empty)
            {
                single4 = this.CX;
            }
            if (AttributeFunc.FindAttribute("fy", this).ToString() == string.Empty)
            {
                single5 = this.CY;
            }
            PointF[] tfArray3 = new PointF[1] { new PointF(single4, single5) } ;
            PointF[] tfArray1 = tfArray3;
            matrix1.TransformPoints(tfArray1);
            this.brush.CenterPoint = tfArray1[0];
            this.brush.Transform = this.coord;
            if (this.pen != null)
            {
                this.pen.Brush = this.brush;
                g.DrawPath(this.pen, path);
            }
            this.pretime = -1;
        }


        // Properties
        public PointF[] BoundsPoints
        {
            get
            {
                return this.boundsPoints;
            }
        }

        public override SvgElementCollection ChildList
        {
            get
            {
                return this.stops;
            }
        }

        public Matrix Coord
        {
            get
            {
                return this.coord;
            }
        }

        public float CX
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("cx"))
                {
                    this.cx = (float) this.svgAnimAttributes["cx"];
                }
                else if (this.gradientUnit == Units.ObjectBoundingBox)
                {
                    this.cx = 0.5f;
                }
                else
                {
                    this.cx = 0.5f * base.ViewPort.Width;
                }
                return this.cx;
            }
            set
            {
                if (this.cx != value)
                {
                    this.cx = value;
                    AttributeFunc.SetAttributeValue(this, "cx", this.cx.ToString());
                }
            }
        }

        public float CY
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("cy"))
                {
                    this.cy = (float) this.svgAnimAttributes["cy"];
                }
                else if (this.gradientUnit == Units.ObjectBoundingBox)
                {
                    this.cy = 0.5f;
                }
                else
                {
                    this.cy = 0.5f * base.ViewPort.Height;
                }
                return this.cy;
            }
            set
            {
                if (this.cy != value)
                {
                    this.cy = value;
                    AttributeFunc.SetAttributeValue(this, "cy", this.cx.ToString());
                }
            }
        }

        public float FX
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("fx"))
                {
                    this.fx = (float) this.svgAnimAttributes["fx"];
                }
                else
                {
                    this.fx = 0.5f;
                }
                return this.fx;
            }
            set
            {
                if (this.fx != value)
                {
                    this.fx = value;
                    AttributeFunc.SetAttributeValue(this, "fx", this.cx.ToString());
                }
            }
        }

        public float FY
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("fy"))
                {
                    this.fx = (float) this.svgAnimAttributes["fy"];
                }
                else
                {
                    this.fx = 0f;
                }
                return this.fx;
            }
            set
            {
                if (this.fy != value)
                {
                    this.cy = value;
                    AttributeFunc.SetAttributeValue(this, "fy", this.cx.ToString());
                }
            }
        }

        public GraphicsPath GradientPath
        {
            get
            {
                return this.gradientpath;
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

        public float R
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("r"))
                {
                    this.r = (float) this.svgAnimAttributes["r"];
                }
                else if (this.gradientUnit == Units.ObjectBoundingBox)
                {
                    this.r = 0.5f;
                }
                else
                {
                    this.r = 0.5f * base.ViewPort.Width;
                }
                return this.r;
            }
            set
            {
                if (this.fx != value)
                {
                    this.fx = value;
                    AttributeFunc.SetAttributeValue(this, "r", this.cx.ToString());
                }
            }
        }

        public SpreadMethods SpreadMethod
        {
            get
            {
                string text1 = string.Empty;
                if (this.svgAnimAttributes.ContainsKey("spreadMethod"))
                {
                    text1 = this.svgAnimAttributes["spreadMethod"].ToString();
                }
                if (text1 == "reflect")
                {
                    this.spreadMethod = SpreadMethods.Reflect;
                }
                else if (text1 == "repeat")
                {
                    this.spreadMethod = SpreadMethods.Repeat;
                }
                else
                {
                    this.spreadMethod = SpreadMethods.Pad;
                }
                return this.spreadMethod;
            }
            set
            {
                this.spreadMethod = value;
            }
        }

        public SvgElementCollection Stops
        {
            get
            {
                return this.stops;
            }
            set
            {
                this.stops = value;
            }
        }

        public Transf Transform
        {
            get
            {
                Matrix matrix1 = new Matrix();
                if (this.svgAnimAttributes.ContainsKey("gradientTransform"))
                {
                    matrix1 = (Matrix) this.svgAnimAttributes["gradientTransform"];
                }
                Transf transf1 = new Transf();
                transf1.setMatrix(matrix1);
                return transf1;
            }
            set
            {
                if (this.gradientTransform != value)
                {
                    this.gradientTransform = value;
                    string text1 = this.gradientTransform.ToString();
                    AttributeFunc.SetAttributeValue(this, "gradientTransform", text1);
                }
            }
        }

        public Units Units
        {
            get
            {
                string text1 = string.Empty;
                if (this.svgAnimAttributes.ContainsKey("gradientUnits"))
                {
                    text1 = this.svgAnimAttributes["gradientUnits"].ToString();
                }
                this.gradientUnit = (text1 == "userSpaceOnUse") ? Units.UserSpaceOnUse : Units.ObjectBoundingBox;
                return this.gradientUnit;
            }
            set
            {
                if (this.gradientUnit != value)
                {
                    this.gradientUnit = value;
                    AttributeFunc.SetAttributeValue(this, "gradientUnits", this.gradientUnit.ToString());
                }
            }
        }


        // Fields
        private RectangleF bounds;
        private PointF[] boundsPoints;
        private PathGradientBrush brush;
        private Matrix coord;
        private float cx;
        private float cy;
        private float fx;
        private float fy;
        private GraphicsPath gradientpath;
        private Transf gradientTransform;
        private Units gradientUnit;
        private System.Drawing.Pen pen;
        private float r;
        private SpreadMethods spreadMethod;
        private SvgElementCollection stops;
    }
}

