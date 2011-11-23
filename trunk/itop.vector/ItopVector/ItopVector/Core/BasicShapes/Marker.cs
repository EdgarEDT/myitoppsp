using System.Drawing;
using System.Drawing.Drawing2D;
using ItopVector.Core.Document;
using ItopVector.Core.Interface.Figure;
using ItopVector.Core.Paint;

namespace ItopVector.Core.Figure
{
	public class Marker : Group
	{
		// Methods
		internal Marker(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
			markerTransForm = new Matrix();
            brush = new SolidColor(Color.Black);
            stroke = new Stroke();
		}

		public override void Draw(Graphics g, int time)
		{
//            this.GPath.Reset();
			GraphicsContainer container1 = g.BeginContainer();
			g.SmoothingMode = base.OwnerDocument.SmoothingMode;
			Matrix matrix1 = base.Transform.Matrix.Clone();

			if (!base.Visible)
			{
				g.SetClip(Rectangle.Empty);
			}

			markerTransForm.Multiply(matrix1, MatrixOrder.Prepend);

			base.GraphTransform.Matrix.Multiply(markerTransForm, MatrixOrder.Prepend);

			SvgElementCollection.ISvgElementEnumerator enumerator1 = base.GraphList.GetEnumerator();
			while (enumerator1.MoveNext())
			{
				IGraph graph1 = (IGraph) enumerator1.Current;
				graph1.GraphTransform.Matrix = base.GraphTransform.Matrix.Clone();
                graph1.IsMarkerChild = IsMarkerChild;
				graph1.Draw(g, time);
			}
			this.pretime = time;
			g.EndContainer(container1);
		}
        public override ItopVector.Core.Paint.Stroke GraphStroke {
            get {
                return stroke;
            }
            set {
                ISvgBrush brush1 = new SolidColor(value.Brush.Pen.Color);
                brush1.Pen.Width = value.Brush.Pen.Width;
                stroke = new Stroke(brush1);
                brush = new SolidColor(stroke.Brush.Pen.Color);
            }
        }
        public override ItopVector.Core.Paint.ISvgBrush GraphBrush {
            get {
                return brush;
            }
            set {
                brush = value;

            }
        }
		public Matrix MarkerTransForm
		{
			get
			{
				markerTransForm = new Matrix();
                //float f1 =stroke.Brush.Pen.Width/2f;
                //f1 =f1>1f?f1:1f;
                //markerTransForm.Scale(f1, f1);
				return markerTransForm;
			}
			set { markerTransForm = value; }
		}

		public string Id
		{
			get { return this.GetAttribute("id"); }
		}
        private ISvgBrush brush;
        private Stroke stroke;
		private Matrix markerTransForm;

	}
}