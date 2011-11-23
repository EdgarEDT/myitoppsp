using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using ItopVector.Core.Document;
using ItopVector.Core.Func;
using ItopVector.Core.Interface.Figure;

namespace ItopVector.Core.Figure
{
	public class Line : GraphPath
	{
		// Methods
		internal Line(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
		}


		// Properties
		public override GraphicsPath GPath
		{
			get
			{
				if (this.IsChanged)
				{
					SvgDocument document1 = base.OwnerDocument;
					if (this.pretime != document1.ControlTime)
					{
						this.graphPath = new GraphicsPath();
						this.graphPath.AddLine(this.X1, this.Y1, this.X2, this.Y2);
					}
					
				}
				return this.graphPath;
			}
			set { this.graphPath = value; }
		}

		public virtual PointF[] Points
		{
			get
			{
				PointF p1 = new PointF(this.X1, this.Y1);
				PointF p2 = new PointF(this.X2, this.Y2);
				return new PointF[2] {p1, p2};
			}
			set
			{
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
					AttributeFunc.SetAttributeValue(this, "x1", value.ToString());
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
				else
				{
					this.x2 = 0f;
				}
				return this.x2;
			}
			set
			{
				if (this.x2 != value)
				{
					this.x2 = value;
					AttributeFunc.SetAttributeValue(this, "x2", value.ToString());
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
					AttributeFunc.SetAttributeValue(this, "y1", value.ToString());
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
					AttributeFunc.SetAttributeValue(this, "y2", value.ToString());
				}
			}
		}

		private string extractMarkerUrl(string propValue)
		{
			Regex reUrl = new Regex(@"^url\((?<uri>.+)\)$");

			Match match = reUrl.Match(propValue);
			if (match.Success)
			{
				return match.Groups["uri"].Value;
			}
			else
			{
				return String.Empty;
			}
		}

        public  PointF[] Pt
        {
            get
            {
                PointF p1 = new PointF(this.X1, this.Y1);
                PointF p2 = new PointF(this.X2, this.Y2);
                PointF[] pt2 = new PointF[] { p1, p2 };
                base.Transform.Matrix.TransformPoints(pt2);
                return pt2;
            }
        }
		protected void PaintMarkers(Graphics g)
		{
			string markerStartUrl = extractMarkerUrl(this.GetAttribute("marker-start"));
			string markerMiddleUrl = extractMarkerUrl(this.GetAttribute("marker-mid"));
			string markerEndUrl = extractMarkerUrl(this.GetAttribute("marker-end"));

			PointF pt0 = new PointF(this.X1, this.Y1);
			PointF pt1 = new PointF(this.X2, this.Y2);
			PointF pt2 = new PointF((this.X1 + this.X2)/2, (this.Y1 + this.Y2)/2);
			PointF[] points1 = new PointF[] {pt0, pt1, pt2};
			int num1 = 0;
			int num11 = 1;
//			int num2 = 2;
//			int num22 = 1;
			int num3 = 0;
			int num33 = 1;
			if (this is ConnectLine)
			{
				PointF[] points2 = this.GPath.PathPoints.Clone() as PointF[];

				points1 = points2;
				if (points2.Length > 3)
				{
//					points1 = points2;
					num33 = points1.Length - 1;
					num3 = num33 - 1;
				}
			}

			base.GraphTransform.Matrix.TransformPoints(points1);

			float angle = 0f; //(float)(180*Math.Atan2(points1[1].Y - points1[0].Y,points1[1].X-points1[0].X)/Math.PI);

			GraphicsContainer container1 = g.BeginContainer();

            Marker element1;
			if (markerStartUrl.Length > 0)
			{
				angle = (float) (180*Math.Atan2(points1[num11].Y - points1[num1].Y, points1[num11].X - points1[num1].X)/Math.PI);

                element1 = (Marker)NodeFunc.GetRefNode(markerStartUrl, this.OwnerDocument);
                if (element1 is Marker)
				{
					((Marker) element1).GraphTransform.Matrix = new Matrix();
					Matrix matrix1 = ((Marker) element1).MarkerTransForm;

					matrix1.Rotate(angle);
					matrix1.Translate(points1[num1].X, points1[num1].Y, MatrixOrder.Append);
                    element1.GraphStroke = this.GraphStroke;
                    element1.IsMarkerChild = true;
                    ((Marker)element1).Draw(g, 0);
				}
			}

			if (markerMiddleUrl.Length > 0)
			{
                element1 = (Marker)NodeFunc.GetRefNode(markerMiddleUrl, this.OwnerDocument);
				if (element1 is IGraph)
				{
					((Marker) element1).GraphTransform.Matrix = new Matrix();
					Matrix matrix1 = ((Marker) element1).MarkerTransForm;

					matrix1.Rotate(angle);
					matrix1.Translate(points1[2].X, points1[2].Y, MatrixOrder.Append);
                    element1.GraphStroke = this.GraphStroke;
                    element1.IsMarkerChild = true;
					((IGraph) element1).Draw(g, 0);
				}
			}

			if (markerEndUrl.Length > 0)
			{
				angle = (float) (180*Math.Atan2(points1[num33].Y - points1[num3].Y, points1[num33].X - points1[num3].X)/Math.PI);

                element1 = (Marker)NodeFunc.GetRefNode(markerEndUrl, this.OwnerDocument);
				if (element1 is IGraph)
				{
					((Marker) element1).GraphTransform.Matrix = new Matrix();
					Matrix matrix1 = ((Marker) element1).MarkerTransForm;

					matrix1.Rotate(angle);
					matrix1.Translate(points1[num33].X, points1[num33].Y, MatrixOrder.Append);
                    element1.GraphStroke = this.GraphStroke;
                    element1.IsMarkerChild = true;
					((IGraph) element1).Draw(g, 0);
				}
			}
			g.EndContainer(container1);
		}

//		private float GetAngle(PointF p0,PointF p1,PointF p2)
//		{
//			double angle=(180*(-Math.Atan2((p1.Y-p0.Y),(p1.X-p0.X)) + Math.Atan2((p2.Y-p0.Y),(p2.X-p0.X))))/(Math.PI);
//			return (float)Convert.ToDecimal(angle);
//		}
		public override void Draw(Graphics g, int time)
		{
			if (base.DrawVisible)
			{
				base.Draw(g, time);
				PaintMarkers(g);
			}
		}

		// Fields
		private float x1;
		private float x2;
		private float y1;
		private float y2;
	}
}