using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using ItopVector.Core.Document;
using ItopVector.Core.Interface.Figure;
using ItopVector.Core.Func;
using ItopVector.Core.Types;

namespace ItopVector.Core.Figure
{
	public class Symbol : Group
	{
		// Methods
		internal Symbol(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
		}

		public void DrawInBox(Graphics g, RectangleF box,GraphicsUnit gu)
		{
			if (box.IsEmpty)return;
			GraphicsContainer gc = g.BeginContainer(box,this.GPath.GetBounds(),gu);
			SvgElementCollection.ISvgElementEnumerator enumerator1 = base.GraphList.GetEnumerator();
			Matrix matrix1 = base.GraphTransform.Matrix.Clone();
			while (enumerator1.MoveNext())
			{
				IGraph graph1 = (IGraph) enumerator1.Current;
				Matrix matrix2 = graph1.GraphTransform.Matrix;

				graph1.GraphTransform.Matrix =new Matrix();
				

				graph1.Draw(g, 0);
			}
			g.EndContainer(gc);
		}
		public void DrawInBox(Graphics g, RectangleF box)
		{
			DrawInBox(g,box,GraphicsUnit.Pixel);
		}
		public override void Draw(Graphics g, int time)
		{
			Matrix matrix1 = base.GraphTransform.Matrix.Clone();
			matrix1.Multiply(base.Transform.Matrix,MatrixOrder.Prepend);
			XmlNode node1 = this.ParentNode;
			if (this.UseElement != null)
			{
				node1 = this.UseElement;
			}
			if (node1 is IGraphPath && !(node1 is SVG))
			{
				base.TempFillOpacity = ((IGraphPath) node1).TempFillOpacity;
				base.TempOpacity = ((IGraphPath) node1).TempOpacity;
				base.TempStrokeOpacity = ((IGraphPath) node1).TempStrokeOpacity;
			}
			this.graphPath.Reset();
			SvgElementCollection.ISvgElementEnumerator enumerator1 = base.GraphList.GetEnumerator();
			while (enumerator1.MoveNext())
			{
				IGraph graph1 = (IGraph) enumerator1.Current;
				graph1.GraphTransform.Matrix = matrix1.Clone();
				graph1.Draw(g, time);
//				if ((this.pretime != time))
//				{
//					GraphicsPath path1 = (GraphicsPath) graph1.GPath.Clone();
//					path1.Transform(graph1.Transform.Matrix);
//					if(graph1 is Text)
//					{
//						this.graphPath.AddRectangle(path1.GetBounds());
//					}else if (path1.PointCount > 0)
//					{
//						this.graphPath.StartFigure();
//						this.graphPath.AddPath(path1, false);
//					}
//				}
			}
			this.pretime = time;
		}

		public override bool LimitSize
		{
			get
			{
				string text = GetAttribute("limitsize");
				return text=="true"?true:false;
			}
			set
			{
				SetAttribute("limitsize",value?"true":"false");
				base.LimitSize=value;
			}
		}
        public float Scale {
            get {
                string text = GetAttribute("scale");
                float scale = 1;
                try {
                    scale = float.Parse(text);
                } catch { }
                return scale;
            }
            set {
                Matrix matrix1 = new Matrix();
                float f1 = value;
                matrix1.Scale(f1, f1, MatrixOrder.Prepend);
                Transf transf1 = new Transf();
                transf1.setMatrix(matrix1);
                base.Transform = transf1;
                SetAttribute("scale", value.ToString());
                this.pretime =- 1;
            }
        }
        public override ItopVector.Core.Types.Transf Transform {
            get {
                //Matrix matrix1 = new Matrix();
                //if (this.svgAnimAttributes.ContainsKey("transform")) {
                //    matrix1 = (Matrix)this.svgAnimAttributes["transform"];
                //}
                //matrix1 = matrix1.Clone();
                //float f1 = Scale;
                //matrix1.Scale(f1, f1, MatrixOrder.Prepend);
                //Transf transf1 = new Transf();
                //transf1.setMatrix(matrix1);
                return base.Transform;
            }
            set {
                base.Transform = value;
            }
        }
		public override GraphicsPath GPath
		{
			get
			{
				if (graphPath.PointCount == 0)
				{
					this.graphPath.Reset();
					SvgElementCollection.ISvgElementEnumerator enumerator1 = base.GraphList.GetEnumerator();
					while (enumerator1.MoveNext())
					{
						IGraph graph1 = (IGraph) enumerator1.Current;
						GraphicsPath path1 = (GraphicsPath) graph1.GPath.Clone();
						path1.Transform(graph1.Transform.Matrix);
						if(graph1 is Text)
						{
							this.graphPath.AddRectangle(path1.GetBounds());
						}else 
						if (path1.PointCount > 0)
						{
							this.graphPath.StartFigure();
							base.graphPath.AddPath(path1, false);
						}
					}
				}
				return base.graphPath;
			}
			set { base.graphPath = value; }
		}
	}
}