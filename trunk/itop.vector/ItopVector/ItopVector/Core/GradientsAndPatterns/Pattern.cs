namespace ItopVector.Core.Paint
{
    using ItopVector.Core;
    using ItopVector.Core.Document;
    using ItopVector.Core.Figure;
    using ItopVector.Core.Func;
    using ItopVector.Core.Interface;
    using ItopVector.Core.Interface.Figure;
    using ItopVector.Core.Interface.Paint;
    using ItopVector.Core.Types;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Xml;

	/// <summary>
	/// Õº∞∏¿‡
	/// </summary>
    public class Pattern : ContainerElement, IViewportElement, ITransformBrush
    {
        // Methods
        internal Pattern(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
        {
            this.graphList = new SvgElementCollection();
            this.viewBox = null;
            this.boundsPoints = new PointF[0];
            this.ratiomatrix = new Matrix();
            this.coord = new Matrix();
            this.graidentPath = new GraphicsPath();
            this.pen = null;
        }

        public new  ISvgBrush Clone()
        {
            return (ISvgBrush) base.Clone();
        }

        public new  bool IsEmpty()
        {
            return false;
        }

        public override bool IsValidChild(XmlNode element)
        {
            if (element is IGraph)
            {
                return true;
            }
            return false;
        }

        public void Paint(GraphPath graph, Graphics g, int time)
        {
            this.Paint(graph.GPath, g, time, 1f);
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
            bool flag1 = this.Units == Units.UserSpaceOnUse;
            float single1 = this.X;
            float single2 = this.Y;
            float single3 = this.Width;
            float single4 = this.Height;
            ItopVector.Core.Types.ViewBox box1 = TypeFunc.ParseViewBox(this);
            Matrix matrix1 = new Matrix();
            if (box1 != null)
            {
                matrix1 = box1.GetViewMatrix(this);
            }
            RectangleF ef1 = path.GetBounds();
            if (!flag1)
            {
                single1 *= ef1.Width;
                single2 *= ef1.Height;
                single3 = (int) Math.Min((float) (single3 * ef1.Width), ef1.Width);
                single4 = (int) Math.Min((float) (single4 * ef1.Height), ef1.Height);
            }
            this.coord = matrix1.Clone();
            PointF[] tfArray1 = new PointF[7] { new PointF(single1 + (single3 / 2f), single2 + (single4 / 2f)), new PointF(single1, single2 + (single4 / 2f)), new PointF(single1 + single3, single2), new PointF(single1 + (single3 / 2f), single2), new PointF(single1 + single3, single2 + (single4 / 2f)), new PointF(single1 + (single3 / 2f), single2 + single4), new PointF(single1, single2 + single4) } ;
            this.boundsPoints = tfArray1;
            this.graidentPath = new GraphicsPath();
            this.graidentPath.AddRectangle(new RectangleF(single1, single2, single3, single4));
            Bitmap bitmap1 = new Bitmap((int) single3, (int) single4);
            Graphics graphics1 = Graphics.FromImage(bitmap1);
            Matrix matrix2 = matrix1.Clone();
            graphics1.Transform = matrix2;
            SvgElementCollection.ISvgElementEnumerator enumerator1 = this.graphList.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                IGraph graph1 = (IGraph) enumerator1.Current;
                graph1.Draw(graphics1, time);
            }
            TextureBrush brush1 = new TextureBrush(bitmap1, new RectangleF(single1, single2, single3, single4));
            brush1.WrapMode = WrapMode.Tile;
            brush1.Transform = this.Transform.Matrix;
            Matrix matrix3 = new Matrix(1f, 0f, 0f, 1f, ef1.X + (ef1.Width / 2f), ef1.Y + (ef1.Height / 2f));
            this.graidentPath.Transform(matrix3);
            matrix3.TransformPoints(this.boundsPoints);
            g.FillPath(brush1, path);
            bitmap1.Dispose();
            brush1.Dispose();
            g.EndContainer(container1);
        }

        public void Stroke(GraphicsPath path, Graphics g, int time, float opacity)
        {
            int num1 = 0;
            int num2 = 0;
            GraphicsContainer container1 = g.BeginContainer();
            AnimFunc.CreateAnimateValues(this, time, out num1, out num2);
            g.SmoothingMode = base.OwnerDocument.SmoothingMode;
            bool flag1 = this.Units == Units.UserSpaceOnUse;
            float single1 = this.X;
            float single2 = this.Y;
            float single3 = this.Width;
            float single4 = this.Height;
            ItopVector.Core.Types.ViewBox box1 = TypeFunc.ParseViewBox(this);
            Matrix matrix1 = new Matrix();
            if (box1 != null)
            {
                matrix1 = box1.GetViewMatrix(this);
            }
            RectangleF ef1 = path.GetBounds();
            if (!flag1)
            {
                single1 *= ef1.Width;
                single2 *= ef1.Height;
                single3 = (int) Math.Min((float) (single3 * ef1.Width), ef1.Width);
                single4 = (int) Math.Min((float) (single4 * ef1.Height), ef1.Height);
            }
            this.coord = matrix1.Clone();
            PointF[] tfArray1 = new PointF[9] { new PointF(single1, single2), new PointF(single1 + (single3 / 2f), single2), new PointF(single1 + single3, single2), new PointF(single1 + single3, single2 + (single4 / 2f)), new PointF(single1 + single3, single2 + single4), new PointF(single1 + (single3 / 2f), single2 + single4), new PointF(single1, single2 + single4), new PointF(single1, single2 + (single4 / 2f)), new PointF(single1 + (single3 / 2f), single2 + (single4 / 2f)) } ;
            this.boundsPoints = tfArray1;
            this.graidentPath = new GraphicsPath();
            this.graidentPath.AddRectangle(new RectangleF(single1, single2, single3, single4));
            Bitmap bitmap1 = new Bitmap((int) single3, (int) single4);
            Graphics graphics1 = Graphics.FromImage(bitmap1);
            Matrix matrix2 = matrix1.Clone();
            graphics1.Transform = matrix2;
            SvgElementCollection.ISvgElementEnumerator enumerator1 = this.graphList.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                Graph graph1 = (Graph) enumerator1.Current;
                graph1.Draw(graphics1, time);
            }
            TextureBrush brush1 = new TextureBrush(bitmap1, new RectangleF(single1, single2, single3, single4));
            brush1.Transform = this.Transform.Matrix;
            if (this.pen != null)
            {
                this.pen.Brush = brush1;
                g.DrawPath(this.pen, path);
            }
            matrix2.Reset();
            matrix2.Translate(ef1.Width / 2f, ef1.Height / 2f);
            bitmap1.Dispose();
            brush1.Dispose();
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
                return this.graphList;
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
                return this.graidentPath;
            }
        }

        public float Height
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("height"))
                {
                    return (float) this.svgAnimAttributes["height"];
                }
                return 0f;
            }
            set
            {
                AttributeFunc.SetAttributeValue(this, "height", value.ToString());
            }
        }

        public Units PatternContentUnits
        {
            get
            {
                string text1 = string.Empty;
                if (this.svgAnimAttributes.ContainsKey("patternContentUnits"))
                {
                    text1 = this.svgAnimAttributes["patternContentUnits"].ToString();
                }
                return ((text1 == "userSpaceOnUse") ? Units.UserSpaceOnUse : Units.ObjectBoundingBox);
            }
            set
            {
                AttributeFunc.SetAttributeValue(this, "patternContentUnits", value.ToString());
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

        public Transf Transform
        {
            get
            {
                Matrix matrix1 = new Matrix();
                if (this.svgAnimAttributes.ContainsKey("patternTransform"))
                {
                    matrix1 = (Matrix) this.svgAnimAttributes["patternTransform"];
                }
                Transf transf1 = new Transf();
                transf1.setMatrix(matrix1);
                return transf1;
            }
            set
            {
                AttributeFunc.SetAttributeValue(this, "patternTransform", value.ToString());
            }
        }

        public Units Units
        {
            get
            {
                string text1 = string.Empty;
                if (this.svgAnimAttributes.ContainsKey("patternUnits"))
                {
                    text1 = this.svgAnimAttributes["patternUnits"].ToString();
                }
                return ((text1 == "userSpaceOnUse") ? Units.UserSpaceOnUse : Units.ObjectBoundingBox);
            }
            set
            {
                AttributeFunc.SetAttributeValue(this, "patternUnits", value.ToString());
            }
        }

        public ItopVector.Core.Types.ViewBox ViewBox
        {
            get
            {
                if (this.pretime != base.OwnerDocument.ControlTime)
                {
                    this.viewBox = TypeFunc.ParseViewBox(this);
                }
                return this.viewBox;
            }
            set
            {
                this.viewBox = value;
            }
        }

        public float Width
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("width"))
                {
                    return (float) this.svgAnimAttributes["width"];
                }
                return 0f;
            }
            set
            {
                AttributeFunc.SetAttributeValue(this, "width", value.ToString());
            }
        }

        public float X
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("x"))
                {
                    return (float) this.svgAnimAttributes["x"];
                }
                return 0f;
            }
            set
            {
                AttributeFunc.SetAttributeValue(this, "x", value.ToString());
            }
        }

        public float Y
        {
            get
            {
                if (this.svgAnimAttributes.ContainsKey("y"))
                {
                    return (float) this.svgAnimAttributes["y"];
                }
                return 0f;
            }
            set
            {
                AttributeFunc.SetAttributeValue(this, "y", value.ToString());
            }
        }


        // Fields
        private PointF[] boundsPoints;
        private Matrix coord;
		private GraphicsPath graidentPath;
        private SvgElementCollection graphList;
        private System.Drawing.Pen pen;
        private Matrix ratiomatrix;
		private ItopVector.Core.Types.ViewBox viewBox;
		
	}
}

