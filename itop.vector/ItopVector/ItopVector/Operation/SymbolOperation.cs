namespace ItopVector.DrawArea
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;
	using ItopVector.Core.Interface.Figure;
	using ItopVector.Core;
	using ItopVector.Core.Document;
	using ItopVector.Core.Figure;

	internal class SymbolOperation : IOperation
	{
		// Methods
		public SymbolOperation()
		{
			this.mouseAreaControl = null;
			this.startPoint = Point.Empty;
			this.oldleft = 0f;
			this.oldtop = 0f;
		}

		internal SymbolOperation(MouseArea mousearea)
		{
			this.startPoint = Point.Empty;
			this.oldleft = 0f;
			this.oldtop = 0f;
			this.mouseAreaControl = mousearea;
		}

		public void Dispose()
		{
			this.mouseAreaControl.ViewOperation = null;
		}
		public void OnMouseWheel(MouseEventArgs e)
		{
			// TODO:  ÃÌº” BezierOperation.DealMouseWheel  µœ÷
		}
		public void OnMouseDown(MouseEventArgs e)		
		{			
		}

		public void OnMouseMove(MouseEventArgs e)
		{			
		}

		public void OnMouseUp(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Point point1=new Point(e.X,e.Y);
				IGraph graph = CreateGraph(Point.Empty,point1,this.mouseAreaControl.CurrentOperation);
				if (graph !=null)
					this.mouseAreaControl.PicturePanel.AddSymbol(graph as SvgElement,this.mouseAreaControl.PicturePanel.PointToView(point1));
			}
		}
		private IGraph CreateGraph(Point startpoint, Point endpoint, ToolOperation operation)
		{
			SvgDocument document1 = this.mouseAreaControl.SVGDocument;
			if (document1 == null)
			{
				return null;
			}
			bool flag1 = document1.AcceptChanges;
			document1.AcceptChanges = false;
			IGraph graph1 = this.mouseAreaControl.PicturePanel.PreGraph;
			if (graph1 == null)
			{
				return null;
			}
			IGraph graph2 = (IGraph) ((SvgElement) graph1).Clone();
//			Point point1 = this.mouseAreaControl.PicturePanel.PointToView(startpoint);
//			Point point2 = this.mouseAreaControl.PicturePanel.PointToView(endpoint);
//			float single1 = this.mouseAreaControl.PicturePanel.ScaleUnit;
//			SizeF ef1 = this.mouseAreaControl.PicturePanel.GridSize;
//			float single2 = ef1.Height;
//			float single3 = ef1.Width;
//			if (this.mouseAreaControl.PicturePanel.SnapToGrid)
//			{
//				int num1 = (int) ((point1.X + (single3 / 2f)) / single3);
//				int num2 = (int) ((point1.Y + (single2 / 2f)) / single2);
//				point1 = new Point((int) (num1 * single3), (int) (num2 * single2));
//				num1 = (int) ((point2.X + (single3 / 2f)) / single3);
//				num2 = (int) ((point2.Y + (single2 / 2f)) / single2);
//				point2 = new Point((int) (num1 * single3), (int) (single2 * num2));
//			}
//			float single4 = Math.Min(point1.X, point2.X);
//			float single5 = Math.Min(point1.Y, point2.Y);
//			float single6 = Math.Max(point1.X, point2.X);
//			float single7 = Math.Max(point1.Y, point2.Y);
//			float single8 = single6 - single4;
//			float single9 = single7 - single5;
			
			this.mouseAreaControl.SVGDocument.AcceptChanges = flag1;
			return graph2;
		}
		public void OnPaint(PaintEventArgs e)
		{
		}

		public bool ProcessDialogKey(Keys keydate)
		{
			return false;
		}

		public bool Redo()
		{
			return false;
		}

		public bool Undo()
		{
			return false;
		}


		// Properties
		public bool CanRedo
		{
			get
			{
				return false;
			}
		}

		public bool CanUndo
		{
			get
			{
				return false;
			}
		}


		// Fields
		private MouseArea mouseAreaControl;
		private float oldleft;
		private float oldtop;
		private Point startPoint;
	}
}

