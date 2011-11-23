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
	/// œﬂ–‘Ã›∂»
	/// </summary>
    public class LinearGradient : ContainerElement, IGradientBrush
    {
        // Methods
        internal LinearGradient(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
        {
            this.x1 = 0f;
            this.y1 = 0f;
            this.x2 = 1f;
            this.y2 = 0f;
            this.gradientTransform = new Transf();
            this.gradientUnit = Units.ObjectBoundingBox;
            this.spreadMethod = SpreadMethods.Pad;
            this.boundsPath = new GraphicsPath();
            this.boundrect = RectangleF.Empty;
            this.stops = new SvgElementCollection();
            this.gradientstart = PointF.Empty;
            this.gradientend = PointF.Empty;
            this.rotatepoint = PointF.Empty;
            this.translatepoint = PointF.Empty;
            this.scalePoint = PointF.Empty;
            this.boundsPoints = new PointF[0];
            this.ratiomatrix = new Matrix();
            this.brush = null;
            this.coord = new Matrix();
            this.gradientpath = new GraphicsPath();
            this.pen = null;
            this.GraphMatrix = new Matrix();
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
            this.Paint(figure.GPath, g, time);
        }

        public void Paint(GraphicsPath path, Graphics g, int time)
        {
            this.Paint(path, g, time, 1f);
        }

        public void Paint(GraphicsPath path, Graphics g, int time, float opacity)
        {
            int num1 = 0;
            int num2 = 0;
            GraphicsContainer container1 = g.BeginContainer();
            AnimFunc.CreateAnimateValues(this, time, out num1, out num2);
            g.SmoothingMode = base.OwnerDocument.SmoothingMode;
            SpreadMethods methods1 = this.SpreadMethod;
            bool flag1 = this.Units == Units.UserSpaceOnUse;
            float single1 = this.X1;
            float single2 = this.Y1;
            float single3 = this.X2;
            float single4 = this.Y2;
            if ((single1 == single3) && (single2 == single4))
            {
                single1 -= 1E-05f;
                single3 += 1E-05f;
            }
            float single5 = single1;
            float single6 = single2;
            float single7 = single3;
            float single8 = single4;
            RectangleF ef1 = RectangleF.Empty;
            RectangleF ef2 = RectangleF.Empty;
            Matrix matrix1 = this.Transform.Matrix.Clone();
            PointF[] tfArray4 = new PointF[2] { new PointF(single1, single2), new PointF(single3, single4) } ;
            PointF[] tfArray1 = tfArray4;
            matrix1.TransformPoints(tfArray1);
            single1 = tfArray1[0].X;
            single3 = tfArray1[1].X;
            single2 = tfArray1[0].Y;
            single4 = tfArray1[1].Y;
            bool flag2 = single2 == single4;
            bool flag3 = single1 == single3;
            float single9 = 1f;
            this.coord = new Matrix();
            this.ratiomatrix = new Matrix();
            RectangleF ef3 = PathFunc.GetBounds(path);
            if (flag1)
            {
                ef1 = ((SVG) base.OwnerDocument.DocumentElement).ViewPort;
            }
            else
            {
                ef3 = new RectangleF(0f, 0f, 1f, 1f);
                ef1 = ef3;
                ef2 = PathFunc.GetBounds(path);
                this.coord.Translate(ef2.X, ef2.Y);
                this.coord.Scale(ef2.Width, ef2.Width);
                this.ratiomatrix.Scale(1f, ef2.Height / ef2.Width);
            }
            PointF tf1 = new PointF(single1, single2);
            PointF tf2 = new PointF(single3, single4);
            PointF tf3 = tf1;
            PointF tf4 = tf2;
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
                float single10 = Math.Min((float) 1f, Math.Max((float) 0f, stop1.ColorOffset));
                colorArray1[num3] = Color.FromArgb(num4, color1.R, color1.G, color1.B);
                singleArray1[num3] = single10;
            }
            float[] singleArray2 = (float[]) singleArray1.Clone();
            Color[] colorArray2 = (Color[]) colorArray1.Clone();
            Array.Sort(singleArray2, colorArray2);
            Color color2 = colorArray2[0];
            Color color3 = colorArray2[colorArray2.Length - 1];
            float single11 = singleArray2[0];
            float single12 = singleArray2[singleArray2.Length - 1];
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
            PointF[] tfArray2 = new PointF[1];
            PointF[] tfArray3 = new PointF[4];
            if (methods1 == SpreadMethods.Pad)
            {
                if (flag2)
                {
                    tf1 = new PointF(Math.Min(single1, ef3.X), tf1.Y);
                    tf2 = new PointF(Math.Max(single3, ef3.Right), tf2.Y);
                    float single13 = single1;
                    float single14 = single3 - single1;
                    for (int num5 = 0; num5 < singleArray2.Length; num5++)
                    {
                        singleArray2[num5] = ((single13 + (single14 * singleArray2[num5])) - tf1.X) / (tf2.X - tf1.X);
                    }
                }
                else if (flag3)
                {
                    tf1 = new PointF(tf1.X, Math.Min(single2, ef3.Y));
                    tf2 = new PointF(tf2.X, Math.Max(single4, ef3.Bottom));
                    float single15 = single2;
                    float single16 = single4 - single2;
                    for (int num6 = 0; num6 < singleArray2.Length; num6++)
                    {
                        singleArray2[num6] = ((single15 + (single16 * singleArray2[num6])) - tf1.Y) / (tf2.Y - tf1.Y);
                    }
                }
                else
                {
                    single9 = (single4 - single2) / (single3 - single1);
                    float single17 = (single2 - (single9 * single1)) / (1f + (single9 * single9));
                    PointF tf5 = ef3.Location;
                    PointF tf6 = new PointF(ef3.Right, ef3.Y);
                    PointF tf7 = new PointF(ef3.Right, ef3.Bottom);
                    PointF tf8 = new PointF(ef3.X, ef3.Bottom);
                    PointF[] tfArray5 = new PointF[4] { tf5, tf6, tf7, tf8 } ;
                    tfArray2 = tfArray5;
                    for (int num7 = 0; num7 < tfArray2.Length; num7++)
                    {
                        PointF tf9 = tfArray2[num7];
                        float single18 = ((((single9 * single9) * tf9.Y) + (single9 * tf9.X)) / (1f + (single9 * single9))) + single17;
                        float single19 = (single9 * (tf9.Y - single18)) + tf9.X;
                        tfArray3[num7] = new PointF(single19, single18);
                        if (single1 < single3)
                        {
                            if (single19 < tf1.X)
                            {
                                tf1 = new PointF(single19, single18);
                            }
                            else if (single19 > tf2.X)
                            {
                                tf2 = new PointF(single19, single18);
                            }
                        }
                        else if (single19 < tf2.X)
                        {
                            tf2 = new PointF(single19, single18);
                        }
                        else if (single19 > tf1.X)
                        {
                            tf1 = new PointF(single19, single18);
                        }
                    }
                    float single20 = single1;
                    float single21 = single3 - single1;
                    for (int num8 = 0; num8 < singleArray2.Length; num8++)
                    {
                        singleArray2[num8] = ((single20 + (single21 * singleArray2[num8])) - tf1.X) / (tf2.X - tf1.X);
                    }
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
            this.brush = new LinearGradientBrush(tf1, tf2, color2, color3);
            if (methods1 == SpreadMethods.Reflect)
            {
                this.brush.WrapMode = WrapMode.TileFlipXY;
            }
            else
            {
                this.brush.WrapMode = WrapMode.Tile;
            }
            blend1.Colors = colorArray2;
            blend1.Positions = singleArray2;
            this.brush.InterpolationColors = blend1;
            this.coord.Multiply(this.ratiomatrix);
            GraphicsContainer container2 = g.BeginContainer();
            g.Transform = this.coord;
            Matrix matrix2 = this.coord.Clone();
            matrix2.Invert();
            GraphicsPath path1 = (GraphicsPath) path.Clone();
            path1.Transform(matrix2);
            g.FillPath(this.brush, path1);
            g.EndContainer(container2);
            if (!base.OwnerDocument.PlayAnim)
            {
                this.gradientpath = new GraphicsPath();
                PointF[] tfArray6 = new PointF[8] { new PointF((single5 + single7) / 2f, ((single6 + single8) / 2f) + 0.5f), new PointF(single7, single8 + 0.5f), new PointF(single7, single8), PointF.Empty, new PointF(single7, ((single6 + single8) / 2f) + 1f), PointF.Empty, PointF.Empty, PointF.Empty } ;
                this.boundsPoints = tfArray6;
                this.gradientpath.AddLine(new PointF(single5, single6), new PointF(single5, single6 + 1f));
                this.gradientpath.StartFigure();
                this.gradientpath.AddLine(new PointF(single7, single8), new PointF(single7, single8 + 1f));
            }
            this.pretime = time;
            g.EndContainer(container1);
        }

        public void Stroke(GraphicsPath path, Graphics g, int time, float opacity)
        {
            int num1 = 0;
            int num2 = 0;
            GraphicsContainer container1 = g.BeginContainer();
            AnimFunc.CreateAnimateValues(this, time, out num1, out num2);
            g.SmoothingMode = base.OwnerDocument.SmoothingMode;
            SpreadMethods methods1 = this.SpreadMethod;
            bool flag1 = this.Units == Units.UserSpaceOnUse;
            float single1 = this.X1;
            float single2 = this.Y1;
            float single3 = this.X2;
            float single4 = this.Y2;
            if ((single1 == single3) && (single2 == single4))
            {
                single1 -= 1E-05f;
                single3 += 1E-05f;
            }
            float single5 = single1;
            float single6 = single2;
            float single7 = single3;
            float single8 = single4;
            RectangleF ef1 = RectangleF.Empty;
            RectangleF ef2 = RectangleF.Empty;
            Matrix matrix1 = this.Transform.Matrix.Clone();
            PointF[] tfArray4 = new PointF[2] { new PointF(single1, single2), new PointF(single3, single4) } ;
            PointF[] tfArray1 = tfArray4;
            matrix1.TransformPoints(tfArray1);
            single1 = tfArray1[0].X;
            single3 = tfArray1[1].X;
            single2 = tfArray1[0].Y;
            single4 = tfArray1[1].Y;
            bool flag2 = single2 == single4;
            bool flag3 = single1 == single3;
            float single9 = 1f;
            this.coord = new Matrix();
            this.ratiomatrix = new Matrix();
            RectangleF ef3 = PathFunc.GetBounds(path);
            if (flag1)
            {
                ef1 = ((SVG) base.OwnerDocument.DocumentElement).ViewPort;
            }
            else
            {
                ef3 = new RectangleF(0f, 0f, 1f, 1f);
                ef1 = ef3;
                ef2 = PathFunc.GetBounds(path);
                this.coord.Translate(ef2.X, ef2.Y);
                this.coord.Scale(ef2.Width, ef2.Width);
                this.ratiomatrix.Scale(1f, ef2.Height / ef2.Width);
            }
            PointF tf1 = new PointF(single1, single2);
            PointF tf2 = new PointF(single3, single4);
            PointF tf3 = tf1;
            PointF tf4 = tf2;
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
                float single10 = Math.Min((float) 1f, Math.Max((float) 0f, stop1.ColorOffset));
                colorArray1[num3] = Color.FromArgb(num4, color1.R, color1.G, color1.B);
                singleArray1[num3] = single10;
            }
            float[] singleArray2 = (float[]) singleArray1.Clone();
            Color[] colorArray2 = (Color[]) colorArray1.Clone();
            Array.Sort(singleArray2, colorArray2);
            Color color2 = colorArray2[0];
            Color color3 = colorArray2[colorArray2.Length - 1];
            float single11 = singleArray2[0];
            float single12 = singleArray2[singleArray2.Length - 1];
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
            PointF[] tfArray2 = new PointF[1];
            PointF[] tfArray3 = new PointF[4];
            if (methods1 == SpreadMethods.Pad)
            {
                if (flag2)
                {
                    tf1 = new PointF(Math.Min(single1, ef3.X), tf1.Y);
                    tf2 = new PointF(Math.Max(single3, ef3.Right), tf2.Y);
                    float single13 = single1;
                    float single14 = single3 - single1;
                    for (int num5 = 0; num5 < singleArray2.Length; num5++)
                    {
                        singleArray2[num5] = ((single13 + (single14 * singleArray2[num5])) - tf1.X) / (tf2.X - tf1.X);
                    }
                }
                else if (flag3)
                {
                    tf1 = new PointF(tf1.X, Math.Min(single2, ef3.Y));
                    tf2 = new PointF(tf2.X, Math.Max(single4, ef3.Bottom));
                    float single15 = single2;
                    float single16 = single4 - single2;
                    for (int num6 = 0; num6 < singleArray2.Length; num6++)
                    {
                        singleArray2[num6] = ((single15 + (single16 * singleArray2[num6])) - tf1.Y) / (tf2.Y - tf1.Y);
                    }
                }
                else
                {
                    single9 = (single4 - single2) / (single3 - single1);
                    float single17 = (single2 - (single9 * single1)) / (1f + (single9 * single9));
                    PointF tf5 = ef3.Location;
                    PointF tf6 = new PointF(ef3.Right, ef3.Y);
                    PointF tf7 = new PointF(ef3.Right, ef3.Bottom);
                    PointF tf8 = new PointF(ef3.X, ef3.Bottom);
                    PointF[] tfArray5 = new PointF[4] { tf5, tf6, tf7, tf8 } ;
                    tfArray2 = tfArray5;
                    for (int num7 = 0; num7 < tfArray2.Length; num7++)
                    {
                        PointF tf9 = tfArray2[num7];
                        float single18 = ((((single9 * single9) * tf9.Y) + (single9 * tf9.X)) / (1f + (single9 * single9))) + single17;
                        float single19 = (single9 * (tf9.Y - single18)) + tf9.X;
                        tfArray3[num7] = new PointF(single19, single18);
                        if (single1 < single3)
                        {
                            if (single19 < tf1.X)
                            {
                                tf1 = new PointF(single19, single18);
                                goto Label_0738;
                            }
                            if (single19 > tf2.X)
                            {
                                tf2 = new PointF(single19, single18);
                            }
                        }
                        else if (single19 < tf2.X)
                        {
                            tf2 = new PointF(single19, single18);
                        }
                        else if (single19 > tf1.X)
                        {
                            tf1 = new PointF(single19, single18);
                        }
                    Label_0738:;
                    }
                    float single20 = single1;
                    float single21 = single3 - single1;
                    for (int num8 = 0; num8 < singleArray2.Length; num8++)
                    {
                        singleArray2[num8] = ((single20 + (single21 * singleArray2[num8])) - tf1.X) / (tf2.X - tf1.X);
                    }
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
            this.brush = new LinearGradientBrush(tf1, tf2, color2, color3);
            if (methods1 == SpreadMethods.Reflect)
            {
                this.brush.WrapMode = WrapMode.TileFlipXY;
            }
            else
            {
                this.brush.WrapMode = WrapMode.Tile;
            }
            blend1.Colors = colorArray2;
            blend1.Positions = singleArray2;
            this.brush.InterpolationColors = blend1;
            this.coord.Multiply(this.ratiomatrix);
            this.brush.Transform = this.coord;
            GraphicsPath path1 = (GraphicsPath) path.Clone();
            if (this.pen != null)
            {
                this.pen.Brush = this.brush;
                g.DrawPath(this.pen, path1);
            }
            if (!base.OwnerDocument.PlayAnim)
            {
                this.gradientpath = new GraphicsPath();
                PointF[] tfArray6 = new PointF[8] { new PointF((single5 + single7) / 2f, ((single6 + single8) / 2f) + 0.5f), new PointF(single7, single8 + 0.5f), new PointF(single7, single8), PointF.Empty, new PointF(single7, ((single6 + single8) / 2f) + 1f), PointF.Empty, PointF.Empty, PointF.Empty } ;
                this.boundsPoints = tfArray6;
                this.gradientpath.AddLine(new PointF(single5, single6), new PointF(single5, single6 + 1f));
                this.gradientpath.StartFigure();
                this.gradientpath.AddLine(new PointF(single7, single8), new PointF(single7, single8 + 1f));
            }
            this.pretime = time;
            g.EndContainer(container1);
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

        public Matrix RatioMatrix
        {
            get
            {
                return this.ratiomatrix;
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

        public float X1
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("x1"))
                {
                    this.x1 = (float) this.svgAnimAttributes["x1"];
                }
                else
                {
                    this.x1 = 0f;
                }
                return this.x1;
            }
            set
            {
                if (this.x1 != value)
                {
                    this.x1 = value;
                    AttributeFunc.SetAttributeValue(this, "x1", this.x1.ToString());
                }
            }
        }

        public float X2
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("x2"))
                {
                    this.x2 = (float) this.svgAnimAttributes["x2"];
                }
                else if (this.gradientUnit == Units.ObjectBoundingBox)
                {
                    this.x2 = 1f;
                }
                else
                {
                    this.x2 = base.ViewPort.Width;
                }
                return this.x2;
            }
            set
            {
                if (this.x2 != value)
                {
                    this.x2 = value;
                    AttributeFunc.SetAttributeValue(this, "x2", this.x2.ToString());
                }
            }
        }

        public float Y1
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("y1"))
                {
                    this.y1 = (float) this.svgAnimAttributes["y1"];
                }
                else
                {
                    this.y1 = 0f;
                }
                return this.y1;
            }
            set
            {
                if (this.y1 != value)
                {
                    this.y1 = value;
                    AttributeFunc.SetAttributeValue(this, "y1", this.y1.ToString());
                }
            }
        }

        public float Y2
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("y2"))
                {
                    this.y2 = (float) this.svgAnimAttributes["y2"];
                }
                else
                {
                    this.y2 = 0f;
                }
                return this.y2;
            }
            set
            {
                if (this.y2 != value)
                {
                    this.y2 = value;
                    AttributeFunc.SetAttributeValue(this, "y2", this.x2.ToString());
                }
            }
        }


        // Fields
        private RectangleF boundrect;
        private GraphicsPath boundsPath;
        private PointF[] boundsPoints;
        private LinearGradientBrush brush;
        private Matrix coord;
        private PointF gradientend;
        private GraphicsPath gradientpath;
        private PointF gradientstart;
        private Transf gradientTransform;
        private Units gradientUnit;
        public Matrix GraphMatrix;
        private System.Drawing.Pen pen;
        private Matrix ratiomatrix;
        private PointF rotatepoint;
        private PointF scalePoint;
        private SpreadMethods spreadMethod;
        private SvgElementCollection stops;
        private PointF translatepoint;
        private float x1;
        private float x2;
        private float y1;
        private float y2;
    }
}

