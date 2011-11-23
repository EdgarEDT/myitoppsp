using ItopVector.Core.Document;
using ItopVector.Core.Func;
using ItopVector.Core.Figure;
using ItopVector.Core.Interface.Figure;
using System;
using System.Drawing.Drawing2D; 
using System.Drawing;

namespace ItopVector.Core.Figure
{

	/// <summary>
	/// 连接线（直线)
	/// </summary>
	public class ConnectLine : Line
	{
		// Methods
		private IGraph startGraph;
		private IGraph endGraph;
		private int startGraphPointIndex=0;
		private int endGraphPointIndex=0;
		private Enums.ConnectType type;
		private PointF[] linepoints;
		private string strStartGraph;
		private string strEndGraph;

		internal ConnectLine(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
			this.startGraph=null;
			this.endGraph=null;
			this.strEndGraph=string.Empty;
			this.strStartGraph=string.Empty;
			this.type=Enums.ConnectType.none;
			this.linepoints=new PointF[0];			
		}
		public override void Draw(Graphics g, int time)
		{
			
			if (base.DrawVisible)
			{
				UpatePath(g);
				base.Draw(g,time);
			}
		}
		public override GraphicsPath GPath
		{
			get
			{
				return this.graphPath;				
			}
			set
			{
				
			}
		}

		
		internal void UpatePath(Graphics g)
		{
			
			if ((base.pretime != base.OwnerDocument.ControlTime) || (base.graphPath == null)||(base.IsChanged))
			{
				PointF[] tfArray3;
				if (base.graphPath == null)
				{
					base.graphPath = new GraphicsPath();
				}
				base.graphPath.Reset();
				PointF tf1 = new PointF(this.X1, this.Y1);
				PointF tf2 = new PointF(this.X2, this.Y2);
				using (Matrix matrix1 = new Matrix())
				{
					using (Matrix matrix2 = new Matrix())
					{
						bool flag1 = false;
						bool flag2 = false;
						if (this.StartGraph != null)
						{
							flag1 = true;
							matrix1.Multiply(this.startGraph.Transform.Matrix);
							RectangleF ef1 = this.GetBounds(this.startGraph, matrix1);
							PointF tf3 = new PointF(ef1.X + (ef1.Width / 2f), ef1.Y + (ef1.Height / 2f));
							PointF[] tfArray1 = (this.startGraph as IGraph).ConnectPoints.Clone() as PointF[];
							if ((this.startGraphPointIndex >= 0) && (this.startGraphPointIndex < tfArray1.Length))
							{
								flag1 = false;
								using (Matrix matrix3 = this.startGraph.Transform.Matrix.Clone())
								{
									matrix3.TransformPoints(tfArray1);
									tf3 = tfArray1[this.startGraphPointIndex];
								}
								using (Matrix matrix5 = this.Transform.Matrix.Clone())
								{
									matrix5.Invert();
									PointF[] tfArray5=new PointF[]{tf3};
									matrix5.TransformPoints(tfArray5);
									tf3=tfArray5[0];
								}
							}
							tf1 = tf3;							
						}
						if (this.EndGraph != null)
						{
							flag2 = true;
							matrix2.Multiply(this.endGraph.Transform.Matrix);
							RectangleF ef2 = this.GetBounds(this.endGraph, matrix2);
							PointF tf4 = new PointF(ef2.X + (ef2.Width / 2f), ef2.Y + (ef2.Height / 2f));
							PointF[] tfArray2 =(this.endGraph as IGraph).ConnectPoints.Clone() as PointF[];
							if ((this.endGraphPointIndex >= 0) && (this.endGraphPointIndex < tfArray2.Length))
							{
								flag2 = false;
								using (Matrix matrix4 = this.endGraph.Transform.Matrix.Clone())
								{
									matrix4.TransformPoints(tfArray2);
									tf4 = tfArray2[this.endGraphPointIndex];
								}
								using (Matrix matrix5 = this.Transform.Matrix.Clone())
								{
									matrix5.Invert();
									PointF[] tfArray5=new PointF[]{tf4};
									matrix5.TransformPoints(tfArray5);
									tf4=tfArray5[0];
								}
							}
							
							tf2 = tf4;

						}
						if ((flag1 || flag2) && (this.startGraph != this.endGraph))
						{
							PointF tf5 = tf1;
							PointF tf6 = tf2;
							using (GraphicsPath path1 = new GraphicsPath())
							{
								path1.AddLine(tf1, tf2);
								path1.Widen(new Pen(Color.White, 0.1f));
								if (flag1)
								{
									using (Region region1 = new Region(this.startGraph.GPath))
									{
										region1.Transform(matrix1);
										region1.Intersect(path1);
										if (!region1.IsEmpty(g))
										{
											tf5 = this.Intersect(region1.GetBounds(g), tf5);
											using (Matrix matrix5 = this.Transform.Matrix.Clone())
											{
												matrix5.Invert();
												PointF[] tfArray5=new PointF[]{tf5};
												matrix5.TransformPoints(tfArray5);
												tf5=tfArray5[0];
											}
										}
									}
								}
								if (flag2)
								{
									using (Region region2 = new Region(this.endGraph.GPath))
									{
										region2.Transform(matrix2);
										region2.Intersect(path1);
										if (!region2.IsEmpty(g))
										{
											tf6 = this.Intersect(region2.GetBounds(g), tf6);
											using (Matrix matrix5 = this.Transform.Matrix.Clone())
											{
												matrix5.Invert();
												PointF[] tfArray5=new PointF[]{tf6};
												matrix5.TransformPoints(tfArray5);
												tf6=tfArray5[0];
											}
										}
									}
								}
								tf1 = tf5;
								tf2 = tf6;
							}
						}
					}
				}
				bool flag3 = Math.Abs( (tf2.Y - tf1.Y)) > Math.Abs((tf2.X - tf1.X));
				
				string text1= base.SvgAttributes["type"].ToString().Trim();			
				this.type = (Enums.ConnectType)Enum.Parse(typeof(Enums.ConnectType),text1,true);

				switch (this.type)
				{
					case Enums.ConnectType.Line:
					{
						base.graphPath.AddLine(tf1, tf2);
						break;
					}
					case Enums.ConnectType.Polyline:
					{
						if (!flag3)
						{
							bool flag5 = tf1.X < tf2.X;
                            tfArray3 = new PointF[] { tf1, new PointF(tf1.X + ((flag5 ? 1 : -1) * 30), tf1.Y), new PointF(tf2.X - ((flag5 ? 1 : -1) * 30), tf2.Y), tf2 };
							base.graphPath.AddLines(tfArray3);
							break;
						}
						bool flag4 = tf1.Y < tf2.Y;
						tfArray3 = new PointF[] { tf1, new PointF(tf1.X, tf1.Y + ((flag4 ? 1 : -1) * 30)), new PointF(tf2.X, tf2.Y - ((flag4 ? 1 : -1) * 30)), tf2 } ;
						base.graphPath.AddLines(tfArray3);
						break;
					}
					case Enums.ConnectType.RightAngle:
					{
						if (!flag3)
						{
							float single2 = (tf1.X + tf2.X) / 2f;
							tfArray3 = new PointF[] { tf1, new PointF(single2, tf1.Y), new PointF(single2, tf2.Y), tf2 } ;
							base.graphPath.AddLines(tfArray3);
							break;
						}
						float single1 = (tf1.Y + tf2.Y) / 2f;
						tfArray3 = new PointF[] { tf1, new PointF(tf1.X, single1), new PointF(tf2.X, single1), tf2 } ;
						base.graphPath.AddLines(tfArray3);
						break;
					}
					case Enums.ConnectType.Spline:
					{
						if (!flag3)
						{
							bool flag7 = tf1.X < tf2.X;
							base.graphPath.AddBezier(tf1, new PointF(tf1.X + ((flag7 ? 1 : -1) * 70), tf1.Y), new PointF(tf2.X - ((flag7 ? 1 : -1) * 70), tf2.Y), tf2);
							break;
						}
						bool flag6 = tf1.Y < tf2.Y;
						base.graphPath.AddBezier(tf1, new PointF(tf1.X, tf1.Y + ((flag6 ? 1 : -1) * 70)), new PointF(tf2.X, tf2.Y - ((flag6 ? 1 : -1) * 70)), tf2);
						break;
					}
				}
				tfArray3 = new PointF[] { tf1, tf2 } ;
				this.linepoints = tfArray3;
//				IsChanged=false;
				//base.pretime =this.OwnerDocument.ControlTime;
			}
		}
		public GraphicsPath GetTempPath(IGraph graph,PointF offset)
		{
			GraphicsPath path =base.graphPath.Clone() as GraphicsPath;
			if(path.PointCount==0)return path;
			GraphicsPath path2 =new GraphicsPath();
			path.Flatten(base.GraphTransform.Matrix);
			PointF[] points =new PointF[]{offset};
			graph.GraphTransform.Matrix.TransformVectors(points);
			offset = points[0];
			
			if(graph == startGraph)
			{

				path2.AddLine(new PointF(path.PathPoints[0].X+offset.X,path.PathPoints[0].Y+offset.Y),path.PathPoints[path.PointCount-1]);
			}
			else
			{
				path2.AddLine(path.PathPoints[0],new PointF(path.PathPoints[path.PointCount-1].X+offset.X,path.PathPoints[path.PointCount-1].Y+offset.Y));

			}
			return path2;
		}
		public GraphicsPath GetTempPath2(IGraph graph,PointF offset)
		{
			GraphicsPath path =base.graphPath.Clone() as GraphicsPath;
			GraphicsPath path2 =new GraphicsPath();
			path.Flatten(base.GraphTransform.Matrix);
			PointF[] points =new PointF[]{offset};
			PointF[] points2 =new PointF[]{offset};
			graph.GraphTransform.Matrix.TransformVectors(points);
			offset = points[0];
			graph.GraphTransform.Matrix.TransformVectors(points2);
			PointF offset2 = points2[0];

			if(path.PointCount>0)
				path2.AddLine(new PointF(path.PathPoints[0].X+offset.X,path.PathPoints[0].Y+offset.Y),new PointF(path.PathPoints[path.PointCount-1].X+offset2.X,path.PathPoints[path.PointCount-1].Y+offset2.Y));
			

			return path2;
		}
		internal GraphicsPath ConnectPath()
		{
			PointF[] tfArray1;
			GraphicsPath path1 = new GraphicsPath();
			PointF tf1 = new PointF(this.X1, this.Y1);
			PointF tf2 = new PointF(this.X2, this.Y2);
			bool flag1 = Math.Abs((tf2.Y - tf1.Y)) > Math.Abs((tf2.X - tf1.X));

			string text1= base.SvgAttributes["type"].ToString().Trim();
			
			this.type = (Enums.ConnectType)Enum.Parse(typeof(Enums.ConnectType),text1,true);
			switch (this.type)
			{
				case Enums.ConnectType.Line:
				{
					path1.AddLine(tf1, tf2);
					return path1;
				}
				case Enums.ConnectType.Polyline:
				{
					if (!flag1)
					{
						bool flag3 = tf1.X < tf2.X;
						tfArray1 = new PointF[] { tf1, new PointF(tf1.X + ((flag3 ? 1 : -1) * 30), tf1.Y), new PointF(tf2.X - ((flag3 ? 1 : -1) * 30), tf2.Y), tf2 } ;
						path1.AddLines(tfArray1);
						return path1;
					}
					bool flag2 = tf1.Y < tf2.Y;
					tfArray1 = new PointF[] { tf1, new PointF(tf1.X, tf1.Y + ((flag2 ? 1 : -1) * 30)), new PointF(tf2.X, tf2.Y - ((flag2 ? 1 : -1) * 30)), tf2 } ;
					path1.AddLines(tfArray1);
					return path1;
				}
				case Enums.ConnectType.RightAngle:
				{
					if (!flag1)
					{
						float single2 = (tf1.X + tf2.X) / 2f;
						tfArray1 = new PointF[] { tf1, new PointF(single2, tf1.Y), new PointF(single2, tf2.Y), tf2 } ;
						path1.AddLines(tfArray1);
						return path1;
					}
					float single1 = (tf1.Y + tf2.Y) / 2f;
					tfArray1 = new PointF[] { tf1, new PointF(tf1.X, single1), new PointF(tf2.X, single1), tf2 } ;
					path1.AddLines(tfArray1);
					return path1;
				}
				case Enums.ConnectType.Spline:
				{
					if (!flag1)
					{
						bool flag5 = tf1.X < tf2.X;
						path1.AddBezier(tf1, new PointF(tf1.X + ((flag5 ? 1 : -1) * 70), tf1.Y), new PointF(tf2.X - ((flag5 ? 1 : -1) * 70), tf2.Y), tf2);
						return path1;
					}
					bool flag4 = tf1.Y < tf2.Y;
					path1.AddBezier(tf1, new PointF(tf1.X, tf1.Y + ((flag4 ? 1 : -1) * 70)), new PointF(tf2.X, tf2.Y - ((flag4 ? 1 : -1) * 70)), tf2);
					return path1;
				}
			}
			return path1;
		}

		private RectangleF GetBounds(IGraph graph, Matrix matrix)
		{
			RectangleF ef2;
			using (GraphicsPath path1 = (graph.GPath.Clone() as GraphicsPath))
			{
				RectangleF ef1 = path1.GetBounds();
				path1.Reset();
				path1.AddRectangle(ef1);
				path1.Transform(matrix);
				ef2 = path1.GetBounds();
			}
			return ef2;
		}

		

		private PointF Intersect(RectangleF rect, PointF refPoint)
		{
			PointF[] tfArray2 = new PointF[] { rect.Location, new PointF(rect.Right, rect.Y), new PointF(rect.Right, rect.Bottom), new PointF(rect.X, rect.Bottom) } ;
			PointF[] tfArray1 = tfArray2;
			float single1 = -1f;
			int num1 = 0;
			for (int num2 = 0; num2 < tfArray1.Length; num2++)
			{
				PointF tf1 = tfArray1[num2];
				float single2 = (float) Math.Sqrt(Math.Pow((double) (tf1.X - refPoint.X), 2) + Math.Pow((double) (tf1.Y - refPoint.Y), 2));
				if (num2 == 0)
				{
					single1 = single2;
				}
				else if (single1 < single2)
				{
					num1 = num2;
					single1 = single2;
				}
			}
			PointF tf2 = tfArray1[num1];
			tfArray1 = null;
			return tf2;
		}

		public override PointF[] Points
		{
			get
			{
				return this.linepoints;
			}
			set
			{
				
			}
		}
		public override ItopVector.Core.Types.Transf Transform
		{
			get
			{
				return base.Transform;
			}
			set
			{
				if (this.StartGraph!=null || this.EndGraph!=null)return;
				PointF tf1 = new PointF(this.X1, this.Y1);
				PointF tf2 = new PointF(this.X2, this.Y2);
				PointF[] points={tf1,tf2};
				value.Matrix.TransformPoints(points);
				this.X1 = points[0].X;
				this.X2 = points[1].X;
				this.Y1 = points[0].Y;
				this.Y2 = points[1].Y;

			}
		}

		public IGraph StartGraph
		{
			get
			{
				if (this.strStartGraph != this.GetAttribute("start"))
				{
					this.strStartGraph = this.GetAttribute("start");
					int num1 = this.strStartGraph.LastIndexOf(".");
					string text1 = this.strStartGraph;
					this.startGraphPointIndex = -1;
					if (num1 >= 0)
					{
						text1 = text1.Substring(0, num1);
						try
						{
							this.startGraphPointIndex = int.Parse(this.strStartGraph.Substring(num1 + 1));
						}
						catch
						{
						}
					}
					this.startGraph = NodeFunc.GetRefNode(text1,base.OwnerDocument) as IGraph;
					if(this.startGraph!=null)
					{
						startGraph.AddConnectLine(this);
					}
					
				}
				return this.startGraph;
			}
			set
			{
				SvgElement ab1 = value as SvgElement;
				string text3 = ab1.GetAttribute("id");
				if ((text3 == null) || (text3.Trim().Length == 0))
				{
					text3 = CodeFunc.CreateString(this.OwnerDocument,ab1.LocalName);
					AttributeFunc.SetAttributeValue(ab1,"id", text3);
				}

				text3 = "#"+text3+".4";
				if (this.startGraph!=null)this.startGraph.ConnectLines.Remove(this);
				AttributeFunc.SetAttributeValue(this,"start", text3);
//				this.startGraph = value;
//				startGraph.AddConnectLine(this);
//				this.strStartGraph = text3;
			}
		}

		public IGraph EndGraph
		{
			get
			{
				if (this.strEndGraph != this.GetAttribute("end"))
				{
					this.strEndGraph = this.GetAttribute("end");
					int num1 = this.strEndGraph.LastIndexOf(".");
					string text1 = this.strEndGraph;
					this.endGraphPointIndex = -1;
					if (num1 >= 0)
					{
						text1 = text1.Substring(0, num1);
						try
						{
							this.endGraphPointIndex = int.Parse(this.strEndGraph.Substring(num1 + 1));
						}
						catch
						{
						}
					}
					this.endGraph =  NodeFunc.GetRefNode(text1,base.OwnerDocument) as IGraph;
					if(this.endGraph !=null)
					{
						endGraph.AddConnectLine(this);
					}
					
				}
				return this.endGraph;
			}
			set
			{
				SvgElement ab1 = value as SvgElement;
				string text3 = ab1.GetAttribute("id");
				if ((text3 == null) || (text3.Trim().Length == 0))
				{
					text3 = CodeFunc.CreateString(this.OwnerDocument,ab1.LocalName);
					AttributeFunc.SetAttributeValue(ab1,"id", text3);
				}

				text3 = "#"+text3+".4";
				if (this.endGraph!=null)this.endGraph.ConnectLines.Remove(this);
				AttributeFunc.SetAttributeValue(this,"end", text3);
//				this.endGraph =value;
//				endGraph.AddConnectLine(this);
//				this.strEndGraph = text3;

			}
		}
		public void Remove(IGraph graph)
		{
			
			if (graph == this.startGraph)
			{
				AttributeFunc.SetAttributeValue(this,"start",string.Empty);
				using(GraphicsPath path =graph.GPath.Clone() as GraphicsPath)
				{
					RectangleF tf= path.GetBounds(graph.Transform.Matrix);
					PointF pf=new PointF(tf.X+tf.Width/2,tf.Y+tf.Height/2);
					X1=pf.X;
					Y1=pf.Y;
				}
				this.startGraph=null;
			}
			else if (graph==this.endGraph)
			{
				AttributeFunc.SetAttributeValue(this,"end",string.Empty);
				using(GraphicsPath path =graph.GPath.Clone() as GraphicsPath)
					  {
					RectangleF tf= path.GetBounds(graph.Transform.Matrix);
					PointF pf=new PointF(tf.X+tf.Width/2,tf.Y+tf.Height/2);
					X2=pf.X;
					Y2=pf.Y;
				}
				this.endGraph=null;
//				AttributeFunc.SetAttributeValue(this,"end","use97633.4");
			}
			this.Attributes.RemoveNamedItem("transform");
		}
	}
}

