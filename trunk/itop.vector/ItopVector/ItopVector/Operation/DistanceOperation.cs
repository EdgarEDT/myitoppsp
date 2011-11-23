namespace ItopVector.DrawArea
{
	using ItopVector;
	using ItopVector.Core;
	using ItopVector.Core.Animate;
	using ItopVector.Core.Document;
	using ItopVector.Core.Figure;
	using ItopVector.Core.Func;
	using ItopVector.Core.Interface.Figure;
	using ItopVector.Core.Paint;
	using ItopVector.Resource;
	using System;
	using System.Collections;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;
	using System.Xml;

	internal class DistanceOperation : IOperation
	{
		/// <summary>
		/// 测距操作
		/// </summary>
		/// <param name="mc"></param>
		public DistanceOperation(MouseArea mc)
		{
			this.reversePath = new GraphicsPath();
			this.graph = null;
			this.points = new ArrayList();
			this.moveindex = -1;
			this.startpoint = PointF.Empty;
			this.prePoint = PointF.Empty;
			this.nextPoint = PointF.Empty;
			this.insertindex = -1;
			this.tooltips = new Hashtable(0x10);
			this.oldpoints = string.Empty;
			this.mousedown = false;
			this.mouseAreaControl = mc;
			this.win32 = mc.win32;
			this.mouseAreaControl.DefaultCursor = SpecialCursors.MeasureCursor;
			this.tempPath=new GraphicsPath();
			this.connectPoint=PointF.Empty;
		}

		

		private void ChangeSelect(object sender, CollectionChangedEventArgs e)
		{
			if ((this.mouseAreaControl.SVGDocument.CurrentElement != this.graph) && !this.mousedown)
			{
				this.graph = null;
			}
		}

		public void Dispose()
		{
			this.graph = null;
			this.mouseAreaControl.lineOperation = null;
		}
		public void OnMouseWheel(MouseEventArgs e)
		{
			// TODO:  添加 BezierOperation.DealMouseWheel 实现
		}
		public void OnMouseDown(MouseEventArgs e)
		{			
			if (e.Button != MouseButtons.Left)
			{
				return;
			}
			this.startpoint = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
			
			this.points.Add(this.startpoint);
			this.mousedown = true;
		}
		public void OnMouseMove(MouseEventArgs e)
		{			
			if ( !this.mousedown)
			{
				return;
			}
			this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
			this.win32.W32SetROP2(7);
			this.win32.W32PolyDraw(this.reversePath);
			this.reversePath.Reset();
			PointF tf4 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
			
			PointF[] pts = new PointF[this.points.Count+1];			
			this.points.CopyTo(0,pts,0,this.points.Count);
			pts[this.points.Count]=tf4;
			this.mouseAreaControl.PicturePanel.PointToSystem(pts);

			this.reversePath.AddLines(pts);
			this.win32.W32PolyDraw(this.reversePath);
			this.win32.ReleaseDC();
		}

		public void OnMouseUp(MouseEventArgs e)
		{
			
		}

		public void OnPaint(PaintEventArgs e)
		{
			this.reversePath.Reset();
			
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

		private void UpdateGraph(string newpoints)
		{
			
		}

		private PointF[] GetPoints()
		{
			PointF[] pts = (PointF[])this.points.ToArray(typeof(PointF));
			this.mouseAreaControl.PicturePanel.PointToSystem(pts);
			return pts;
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

		public IGraphPath CurrentGraph
		{
			set
			{
				
			}
		}

		private PolyOperate Operate
		{
			set
			{		
			}
		}
		protected virtual bool InPoint(PointF p1, PointF p2)
		{
			double num1 = Math.Sqrt(Math.Pow((double) (p2.X - p1.X), 2) + Math.Pow((double) (p2.Y - p1.Y), 2));
			return (num1 < 4);
		}

		protected virtual bool InPoint(PointF p1, PointF p2, float delta)
		{
			double num1 = Math.Sqrt(Math.Pow((double) (p2.X - p1.X), 2) + Math.Pow((double) (p2.Y - p1.Y), 2));
			return (num1 < delta);
		}

		protected IGraph FindGraphByPoint(PointF mousepoint, ref PointF connectpoint)
		{
			SvgElementCollection collection1 =((SVG)(this.mouseAreaControl.SVGDocument.RootElement)).GraphList;
			for (int num1 = collection1.Count - 1; num1 >= 0; num1--)
			{
				IGraph ab1 = collection1[num1] as IGraph;
				if (ab1 != null)
				{
					PointF[] tfArray1 = ab1.ConnectPoints;//连接点
					if (tfArray1.Length>0)
					{
						tfArray1 = tfArray1.Clone() as PointF[];
						ab1.GraphTransform.Matrix.TransformPoints(tfArray1);
						for (int num2 = 0; num2 < tfArray1.Length; num2++)
						{
							if (this.InPoint(mousepoint, tfArray1[num2], 6f))
							{
								connectpoint = tfArray1[num2];
								return ab1;
							}
						}
						RectangleF ef1 = ab1.GPath.GetBounds();
						PointF[] tfArray2 = new PointF[] { new PointF(ef1.X + (ef1.Width / 2f), ef1.Y + (ef1.Height / 2f)) } ;//中心点
						tfArray1 = tfArray2;
						ab1.GraphTransform.Matrix.TransformPoints(tfArray1);
						if (this.InPoint(mousepoint, tfArray1[0], 10f))
						{
							connectpoint = tfArray1[0];
							return ab1;
						}
					}
				}
			}
			return null;
		}
		protected void InvadatePath(GraphicsPath path)
		{
			if ((path != null) && (path.PointCount > 1))
			{
				path.FillMode = FillMode.Winding;
				RectangleF ef1 = path.GetBounds();
				int num1 = 4;
				Rectangle rectangle1 = new Rectangle(((int) ef1.X) - num1, ((int) ef1.Y) - num1, ((int) ef1.Width) + (2 * num1), ((int) ef1.Height) + (2 * num1));
				if (!rectangle1.IsEmpty)
				{
					this.mouseAreaControl.PicturePanel.InvadateRect(rectangle1);
				}
			}
		}
		// Fields
		
		private IGraph graph;
		private int insertindex;
		private MouseArea mouseAreaControl;
		private bool mousedown;
		private int moveindex;
		private PointF nextPoint;
		private string oldpoints;
		private PolyOperate operate;
		private ArrayList points;
		private PointF prePoint;
		private GraphicsPath reversePath;
		private PointF startpoint;
		private Hashtable tooltips;
		private Win32 win32;

		//
		private IGraph connectGraph;
		private PointF connectPoint;
		private GraphicsPath tempPath;
	}
}

