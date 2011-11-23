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

	internal class LineOperation : IOperation
	{
		// Methods
		public LineOperation(MouseArea mc)
		{
			this.mouseAreaControl = null;
			this.win32 = null;
			this.reversePath = new GraphicsPath();
			this.graph = null;
			this.operate = PolyOperate.Draw;
			this.points = new PointF[0];
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
			this.mouseAreaControl.PicturePanel.GraphChanged += new EventHandler(this.ChangeGraph);
			this.mouseAreaControl.SVGDocument.SelectCollection.OnCollectionChangedEvent += new OnCollectionChangedEventHandler(this.ChangeSelect);
			this.mouseAreaControl.DefaultCursor = SpecialCursors.bezierCursor;
			this.tempPath=new GraphicsPath();
			this.connectPoint=PointF.Empty;
		}

		private void ChangeGraph(object sender, EventArgs e)
		{
			this.graph = this.mouseAreaControl.PicturePanel.PreGraph;
			this.points = new PointF[0];
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
			this.mouseAreaControl.PicturePanel.GraphChanged -= new EventHandler(this.ChangeGraph);
			this.mouseAreaControl.lineOperation = null;
		}
		public void OnMouseWheel(MouseEventArgs e)
		{
			// TODO:  添加 BezierOperation.DealMouseWheel 实现
		}
		public void OnMouseDown(MouseEventArgs e)
		{
			bool flag2;
			bool flag3;
			PointF tf1;
			if (e.Button != MouseButtons.Left)
			{
				return;
			}
			this.startpoint = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
			SizeF ef1 = this.mouseAreaControl.PicturePanel.GridSize;
			float single1 = ef1.Height;
			float single2 = ef1.Width;
			if (this.mouseAreaControl.PicturePanel.SnapToGrid)
			{
				int num1 = (int) ((this.startpoint.X + (single2 / 2f)) / single2);
				int num2 = (int) ((this.startpoint.Y + (single1 / 2f)) / single1);
				this.startpoint = (PointF) new Point((int) (num1 * single2), (int) (num2 * single1));
			}
			SvgDocument document1 = this.mouseAreaControl.SVGDocument;
			bool flag1 = document1.AcceptChanges;
			document1.NumberOfUndoOperations = 200;
			this.mousedown = true;
			switch (this.operate)
			{
				case PolyOperate.Draw:
				{
					document1.AcceptChanges = false;
					flag2 = false;
					if (this.graph != null)
					{
						if (!(this.graph is Polygon) && !(this.graph is Polyline))
						{
							flag2 = true;
						}
						break;
					}
					flag2 = true;
					break;
				}
				case PolyOperate.MovePath:
				{
					goto Label_05F5;
				}
				case PolyOperate.MovePoint:
				{
					PointF tf3;
					this.nextPoint = tf3 = PointF.Empty;
					this.prePoint = tf3;
					if ((this.moveindex < 0) || (this.moveindex >= this.points.Length))
					{
						goto Label_05F5;
					}
					flag3 = this.graph is Polygon;
					if ((this.moveindex - 1) < 0)
					{
						if ((this.points.Length >= 3) && flag3)
						{
							this.prePoint = this.points[this.points.Length - 1];
						}
						goto Label_042C;
					}
					this.prePoint = this.points[this.moveindex - 1];
					goto Label_042C;
				}
				case PolyOperate.Del:
				{
					if ((this.moveindex >= 0) && (this.moveindex < this.points.Length))
					{
						ArrayList list1 = new ArrayList(this.points);
						list1.RemoveAt(this.moveindex);
						this.points = new PointF[list1.Count];
						list1.CopyTo(this.points);
						Matrix matrix1 = this.graph.GraphTransform.Matrix.Clone();
						matrix1.Invert();
						if (this.points.Length > 0)
						{
							matrix1.TransformPoints(this.points);
						}
					}
					goto Label_05F5;
				}
				case PolyOperate.Add:
				{
					if ((this.insertindex < 0) || (this.insertindex >= this.points.Length))
					{
						goto Label_05F5;
					}
					this.points = new PointF[0];
					if (!(this.graph is Polygon))
					{
						if (this.graph is Polyline)
						{
							this.points = ((Polyline) this.graph).Points;
						}
						goto Label_058D;
					}
					this.points = ((Polygon) this.graph).Points;
					goto Label_058D;
				}
				default:
				{
					goto Label_05F5;
				}
			}
			if (flag2)
			{
				IGraph graph1 = this.mouseAreaControl.PicturePanel.PreGraph;
				if (graph1 == null)
				{
					return;
				}
				this.graph = (Graph) ((SvgElement) graph1).Clone();
				this.mouseAreaControl.SVGDocument.AcceptChanges = false;
				if (this.graph != null)
				{
					((SvgElement) this.graph).RemoveAttribute("points");
				}
				if (((SvgElement) this.graph) is IGraphPath)
				{
					if ((((SvgElement) graph1).GetAttribute("style") != string.Empty) && (((SvgElement) graph1).GetAttribute("style") != null))
					{
						this.mouseAreaControl.SVGDocument.AcceptChanges = false;
						AttributeFunc.SetAttributeValue((SvgElement) this.graph, "style", ((SvgElement) this.graph).GetAttribute("style"));
					}
					ISvgBrush brush1 = ((IGraphPath) graph1).GraphBrush;
					if (brush1 is SvgElement)
					{
						ISvgBrush brush2 = (ISvgBrush) ((SvgElement) brush1).Clone();
						((IGraphPath) this.graph).GraphBrush = brush2;
						((SvgElement) brush2).pretime = -1;
					}
					else
					{
						((IGraphPath) this.graph).GraphBrush = brush1;
					}
					brush1 = ((IGraphPath) graph1).GraphStroke.Brush;
					if (brush1 is SvgElement)
					{
						ISvgBrush brush3 = (ISvgBrush) ((SvgElement) brush1).Clone();
						((IGraphPath) this.graph).GraphStroke = new Stroke(brush3);
						((SvgElement) brush3).pretime = -1;
					}
					else
					{
						((IGraphPath) this.graph).GraphStroke.Brush = brush1;
					}
				}
			}
			PointF[] tfArray1 = new PointF[0];
			if (this.graph is Polygon)
			{
				tfArray1 = ((Polygon) this.graph).Points;
			}
			else if (this.graph is Polyline)
			{
				tfArray1 = ((Polyline) this.graph).Points;
			}
			this.points = new PointF[1];
			if (tfArray1 != null)
			{
				this.points = new PointF[tfArray1.Length + 1];
				tfArray1.CopyTo(this.points, 0);
			}
			this.points[this.points.Length - 1] = this.startpoint;
			goto Label_05F5;
			Label_042C:
				if ((this.moveindex + 1) < this.points.Length)
				{
					this.nextPoint = this.points[this.moveindex + 1];
					goto Label_05F5;
				}
			if ((this.points.Length >= 3) && flag3)
			{
				this.nextPoint = this.points[0];
			}
			goto Label_05F5;
			Label_058D:
				tf1 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
			ArrayList list2 = new ArrayList(this.points);
			list2.Insert(this.insertindex, tf1);
			this.points = new PointF[list2.Count];
			list2.CopyTo(this.points);
			Label_05F5:
				if (((this.operate == PolyOperate.Del) || (this.operate == PolyOperate.Draw)) || (this.operate == PolyOperate.Add))
				{
					string text1 = string.Empty;
					int num3 = 0;
					PointF[] tfArray2 = this.points;
					for (int num4 = 0; num4 < tfArray2.Length; num4++)
					{
						PointF tf2 = tfArray2[num4];
						text1 = text1 + tf2.X.ToString() + " " + tf2.Y.ToString();
						if (num3 < (this.points.Length - 1))
						{
							text1 = text1 + ",";
						}
						num3++;
					}
					this.mouseAreaControl.PicturePanel.InvalidateElement((SvgElement) this.graph);
					if (((SvgElement) this.graph).ParentNode == null)
					{
						this.UpdateGraph(text1);
						document1.AcceptChanges = true;
						IGraph graph2 = this.graph;
						this.mouseAreaControl.PicturePanel.AddElement(this.graph);
						this.graph = graph2;
					}
					else
					{
						document1.AcceptChanges = true;
						this.UpdateGraph(text1);
					}
					document1.AcceptChanges = flag1;
					this.mouseAreaControl.Invalidate();
					if (this.graph != null)
					{
						((SvgElement) this.graph).pretime = -1;
						this.mouseAreaControl.PicturePanel.InvalidateElement((SvgElement) this.graph);
					}
				}
			document1.NotifyUndo();
			this.reversePath.Reset();
		}

		public void OnMouseMove(MouseEventArgs e)
		{
			if (this.graph == null)
			{
				this.Operate = PolyOperate.Draw;
				return;
			}
			if (((SvgElement) this.graph).ParentNode == null)
			{
				this.Operate = PolyOperate.Draw;
				return;
			}
			if (e.Button == MouseButtons.None)
			{
				int num4;
				PointF[] tfArray1 = (PointF[]) this.points.Clone();
				int num1 = 0;
				this.insertindex = num4 = -1;
				this.moveindex = num4;
				GraphicsPath path1 = new GraphicsPath();
				Pen pen1 = new Pen(Color.Beige, 4f);
				PointF[] tfArray3 = tfArray1;
				for (int num5 = 0; num5 < tfArray3.Length; num5++)
				{
					PointF tf1 = tfArray3[num5];
					RectangleF ef1 = new RectangleF(tf1.X - 2f, tf1.Y - 2f, 4f, 4f);
					if (ef1.Contains((PointF) new Point(e.X, e.Y)))
					{
						this.moveindex = num1;
                        if (((Control.ModifierKeys & Keys.Control) == Keys.Control) || (this.mouseAreaControl.CurrentOperation == ToolOperation.ShapeTransform) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom11)
                            || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom12) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom13) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom14) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom15))
						{
							this.Operate = PolyOperate.MovePoint;
							return;
						}
						this.Operate = PolyOperate.Del;
						return;
					}
					if ((num1 - 1) >= 0)
					{
						path1.Reset();
						PointF tf2 = tfArray1[num1 - 1];
						path1.AddLine(tf2, tf1);
						if (path1.IsOutlineVisible(new PointF((float) e.X, (float) e.Y), pen1))
						{
                            if (((Control.ModifierKeys & Keys.Control) == Keys.Control) || (this.mouseAreaControl.CurrentOperation == ToolOperation.ShapeTransform) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom11)
                                || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom12) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom13) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom14) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom15))
							{
								this.Operate = PolyOperate.MovePath;
							}
							else
							{
								this.Operate = PolyOperate.Add;
							}
							this.insertindex = num1;
							return;
						}
					}
					if (((num1 == (tfArray1.Length - 1)) && (this.mouseAreaControl.CurrentOperation == ToolOperation.Polygon)) && (tfArray1.Length >= 3))
					{
						path1.Reset();
						PointF tf3 = tfArray1[0];
						path1.AddLine(tf3, tf1);
						if (path1.IsOutlineVisible(new PointF((float) e.X, (float) e.Y), pen1))
						{
                            if (((Control.ModifierKeys & Keys.Control) == Keys.Control) || (this.mouseAreaControl.CurrentOperation == ToolOperation.ShapeTransform) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom11)
                                || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom12) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom13) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom14) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom15))
							{
								this.Operate = PolyOperate.MovePath;
							}
							else
							{
								this.Operate = PolyOperate.Add;
							}
							this.insertindex = num1;
							return;
						}
					}
					num1++;
				}
                if (((Control.ModifierKeys & Keys.Control) == Keys.Control) || (this.mouseAreaControl.CurrentOperation == ToolOperation.ShapeTransform) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom11)
                    || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom12) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom13) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom14) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom15))
				{
					this.Operate = PolyOperate.MovePoint;
				}
				else
				{
					this.Operate = PolyOperate.Draw;
				}
			}
			if ((e.Button != MouseButtons.Left) || !this.mousedown)
			{
				return;
			}
			this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
			this.win32.W32SetROP2(7);
			this.win32.W32PolyDraw(this.reversePath);
			this.reversePath.Reset();
			PointF tf4 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
			SizeF ef2 = this.mouseAreaControl.PicturePanel.GridSize;
			float single1 = ef2.Height;
			float single2 = ef2.Width;
			if (this.mouseAreaControl.PicturePanel.SnapToGrid)
			{
				int num2 = (int) ((tf4.X + (single2 / 2f)) / single2);
				int num3 = (int) ((tf4.Y + (single1 / 2f)) / single1);
				tf4 = (PointF) new Point((int) (num2 * single2), (int) (num3 * single1));
			}
			tf4 = this.mouseAreaControl.PicturePanel.PointToSystem(tf4);
			switch (this.operate)
			{
				case PolyOperate.MovePath:
				{
					PointF[] tfArray2 = (PointF[]) this.points.Clone();
					if (this.graph is ConnectLine)
					{
						tfArray2=this.graph.GPath.PathPoints.Clone() as PointF[];
						this.graph.GraphTransform.Matrix.TransformPoints(tfArray2);
					}
					Matrix matrix1 = new Matrix();
					PointF tf5 = this.mouseAreaControl.PicturePanel.PointToSystem(this.startpoint);
					matrix1.Translate(tf4.X - tf5.X, tf4.Y - tf5.Y);
					matrix1.TransformPoints(tfArray2);
					if ((tfArray2.Length < 3) || !(this.graph is Polygon))
					{
						this.reversePath.AddLines(tfArray2);
						break;
					}
					this.reversePath.AddPolygon(tfArray2);
					break;
				}
				case PolyOperate.MovePoint:
				{
					if ((this.moveindex >= 0) && (this.moveindex < this.points.Length))
					{
						if (!this.prePoint.IsEmpty)
						{
							this.reversePath.AddLine(this.prePoint, tf4);
						}
						if (!this.nextPoint.IsEmpty)
						{
							this.reversePath.AddLine(tf4, this.nextPoint);
						}
						this.reversePath.AddRectangle(new RectangleF(tf4.X - 2f, tf4.Y - 2f, 4f, 4f));
						if(this.graph is ConnectLine)
						{//连接线
							this.InvadatePath(this.tempPath);
							this.tempPath.Reset();
							PointF tf11=PointF.Empty;
							IGraph cdababf1 = this.FindGraphByPoint(new PointF((float) e.X, (float) e.Y), ref tf11);
							this.connectGraph = cdababf1;
							int num7 = 10;
							if (cdababf1 != null)
							{
								this.connectPoint = tf11;
								this.tempPath.AddRectangle(new RectangleF(tf11.X - (num7 / 2), tf11.Y - (num7 / 2), (float) num7, (float) num7));
							}
							this.InvadatePath(this.tempPath);
						}
					}
					goto Label_04D2;
				}
				default:
				{
					goto Label_04D2;
				}
			}
			Label_04D2:
				this.win32.W32PolyDraw(this.reversePath);
			this.win32.ReleaseDC();
		}

		public void OnMouseUp(MouseEventArgs e)
		{
			this.tempPath.Reset();
			if (((this.graph != null) && (((SvgElement) this.graph).ParentNode != null)) && ((e.Button == MouseButtons.Left) && this.mousedown))
			{
				this.mousedown = false;
				if ((this.operate == PolyOperate.MovePoint) || (this.operate == PolyOperate.MovePath))
				{
					PointF tf1 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
					SizeF ef1 = this.mouseAreaControl.PicturePanel.GridSize;
					float single1 = ef1.Height;
					float single2 = ef1.Width;
					if (this.mouseAreaControl.PicturePanel.SnapToGrid)
					{
						int num1 = (int) ((tf1.X + (single2 / 2f)) / single2);
						int num2 = (int) ((tf1.Y + (single1 / 2f)) / single1);
						tf1 = (PointF) new Point((int) (num1 * single2), (int) (num2 * single1));
					}
					tf1 = this.mouseAreaControl.PicturePanel.PointToSystem(tf1);
					switch (this.operate)
					{
						case PolyOperate.MovePath:
						{
							PointF tf2 = this.mouseAreaControl.PicturePanel.PointToSystem(this.startpoint);
							Matrix matrix1 = new Matrix();
							matrix1.Translate(tf1.X - tf2.X, tf1.Y - tf2.Y);
							matrix1.TransformPoints(this.points);
							break;
						}
						case PolyOperate.MovePoint:
						{
							if ((this.moveindex >= 0) && (this.moveindex < this.points.Length))
							{
								this.points[this.moveindex] = tf1;
								
								if(this.graph is ConnectLine)
								{//连接线
									
									bool flag2 = false;
									ConnectLine cebfd1 =this.graph as ConnectLine;

									if(this.connectGraph==null)
									{
										if(this.moveindex==0)
										{

											string text3 = cebfd1.GetAttribute("start");
											if ((text3 != null) && (text3.Length > 0))
											{
												if(cebfd1.StartGraph!=null)
													cebfd1.StartGraph.ConnectLines.Remove(cebfd1);
												AttributeFunc.SetAttributeValue(cebfd1,"start", string.Empty);
												
												flag2 = true;
											}
											float single11 = tf1.X;
											if (cebfd1.GetAttribute("x1") != single11.ToString())
											{
												AttributeFunc.SetAttributeValue(cebfd1,"x1", single11.ToString());
												flag2 = true;
											}
											single11 = tf1.Y;
											if (cebfd1.GetAttribute("y1") != single11.ToString())
											{
												AttributeFunc.SetAttributeValue(cebfd1,"y1", single11.ToString());
												flag2 = true;
											}

										}
										else
										{
											string text2 = cebfd1.GetAttribute("end");
											if ((text2 != null) && (text2.Length > 0))
											{
												if(cebfd1.EndGraph!=null)
													cebfd1.EndGraph.ConnectLines.Remove(cebfd1);
												AttributeFunc.SetAttributeValue(cebfd1,"end", string.Empty);
												
												flag2 = true;
											}
											float single12 = tf1.X;
											if (cebfd1.GetAttribute("x2") != single12.ToString())
											{
												AttributeFunc.SetAttributeValue(cebfd1,"x2", single12.ToString());
												flag2 = true;
											}
											single12 = tf1.Y;
											if (cebfd1.GetAttribute("y2") != single12.ToString())
											{
												AttributeFunc.SetAttributeValue(cebfd1,"y2", single12.ToString());
												flag2 = true;
											}

										}
									}
									else
									{
										SvgElement ab1 = this.connectGraph as SvgElement;
										string text3 = ab1.GetAttribute("id");
										if ((text3 == null) || (text3.Trim().Length == 0))
										{
											text3 = CodeFunc.CreateString(this.mouseAreaControl.SVGDocument,ab1.LocalName);
											AttributeFunc.SetAttributeValue(ab1,"id", text3);
										}
										if (this.moveindex == 0)
										{
											text3 = "#" + text3;
											using (Matrix matrix1 = ((IGraph)ab1).GraphTransform.Matrix.Clone())
											{
												PointF[] tfArray3 = ((IGraph)ab1).ConnectPoints.Clone() as PointF[];
												matrix1.TransformPoints(tfArray3);
												int num1 = Array.IndexOf(tfArray3, this.connectPoint);
												if ((num1 >= 0) && (num1 < tfArray3.Length))
												{
													text3 = text3 + "." + num1.ToString();
												}
												tfArray3 = null;
											}
											if (text3 != cebfd1.GetAttribute("start"))
											{
												IGraph oldgraph=cebfd1.StartGraph;
												if (oldgraph!=null)
													oldgraph.ConnectLines.Remove(cebfd1);
												AttributeFunc.SetAttributeValue(cebfd1,"start", text3);
												flag2 = true;
											}
										}
										else 
										{
											text3 = "#" + text3;
											using (Matrix matrix3 = ((IGraph)ab1).GraphTransform.Matrix.Clone())
											{
												PointF[] tfArray3 = ((IGraph)ab1).ConnectPoints.Clone() as PointF[];
												matrix3.TransformPoints(tfArray3);
												int num2 = Array.IndexOf(tfArray3, this.connectPoint);
												if ((num2 >= 0) && (num2 < tfArray3.Length))
												{
													text3 = text3 + "." + num2.ToString();
												}
												tfArray3 = null;
											}
											if (text3 != cebfd1.GetAttribute("end"))
											{
												IGraph oldgraph=cebfd1.EndGraph;
												if (oldgraph!=null)
													oldgraph.ConnectLines.Remove(cebfd1);
												AttributeFunc.SetAttributeValue(cebfd1,"end", text3);
												flag2 = true;
											}
										}
										
									}
									if(flag2)
									{
										this.mouseAreaControl.PicturePanel.InvalidateElement(this.connectGraph as SvgElement);
									}

								}
							}
							break;
						}
					}
					Matrix matrix2 = this.graph.GraphTransform.Matrix.Clone();
					matrix2.Invert();
					if (this.points.Length > 0)
					{
						matrix2.TransformPoints(this.points);
					}
					SvgDocument document1 = this.mouseAreaControl.SVGDocument;
					bool flag1 = document1.AcceptChanges;
					document1.AcceptChanges = true;
					string text1 = string.Empty;
					int num3 = 0;
					PointF[] tfArray1 = this.points;
					for (int num4 = 0; num4 < tfArray1.Length; num4++)
					{
						PointF tf3 = tfArray1[num4];
						text1 = text1 + tf3.X.ToString() + " " + tf3.Y.ToString();
						if (num3 < (this.points.Length - 1))
						{
							text1 = text1 + ",";
						}
						num3++;
					}
					this.mouseAreaControl.PicturePanel.InvalidateElement((SvgElement) this.graph);
					document1.AcceptChanges = true;
					this.UpdateGraph(text1);
					document1.AcceptChanges = flag1;
					((SvgElement) this.graph).pretime = -1;
					this.mouseAreaControl.Invalidate();
					this.mouseAreaControl.PicturePanel.InvalidateElement((SvgElement) this.graph);
					
				}
			}
		}

		public void OnPaint(PaintEventArgs e)
		{
			this.oldpoints = string.Empty;
			if (this.graph is Line && (((SvgElement) this.graph).ParentNode != null))
			{
				if(this.tempPath.PointCount>0)//连接线 红色方框
				{
					using (Pen pen2 = new Pen(Color.Red, 3f))
					{
						e.Graphics.DrawPath(pen2, this.tempPath);
					}
				}
				e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
				this.points = new PointF[1];
				this.oldpoints = ((SvgElement) this.graph).GetAttribute("points").Trim();
				if (this.graph is Polygon)
				{
					this.points = (PointF[]) ((Polygon) this.graph).Points.Clone();
				}
				else if (this.graph is Polyline)
				{
					this.points = (PointF[]) ((Polyline) this.graph).Points.Clone();
				}
				else if(this.graph is Line)
				{
					this.points = (PointF[]) ((Line) this.graph).Points.Clone();
				}
				if (this.points.Length > 0)
				{
					Matrix matrix1 = this.graph.GraphTransform.Matrix.Clone();
					matrix1.TransformPoints(this.points);
					if (this.points.Length > 1)
					{
						if (this.graph is Polygon)
						{
							e.Graphics.DrawPolygon(Pens.Blue, this.points);
						}
						else if(!(this.graph is ConnectLine))
						{
							e.Graphics.DrawLines(Pens.Blue, this.points);
						}
					}
					SvgElement element1 = (SvgElement) this.graph;
					if ((element1 != null) && !this.mouseAreaControl.SVGDocument.PlayAnim)
					{
						PointF[] tfArray1 = this.points;
						Brush brush1=Brushes.White;
						if(element1 is ConnectLine )brush1=new SolidBrush(Color.Red);
						for (int num1 = 0; num1 < tfArray1.Length; num1++)
						{
							PointF tf1 = tfArray1[num1];
							RectangleF ef1 = new RectangleF(tf1.X - 2f, tf1.Y - 2f, 4f, 4f);
							e.Graphics.FillRectangle(brush1, ef1);
							e.Graphics.DrawRectangle(Pens.Blue, ef1.X, ef1.Y, ef1.Width, ef1.Height);
						}
					}
				}
			}
		}

		public bool ProcessDialogKey(Keys keydate)
		{
			return false;
		}

		public bool Redo()
		{
			if (this.graph != null)
			{
				if (((SvgElement) this.graph).ParentNode == null)
				{
					this.graph.GraphTransform.Matrix = this.mouseAreaControl.PicturePanel.CoordTransform.Clone();
				}
				((SvgElement) this.graph).pretime = -1;
			}
			return false;
		}

		public bool Undo()
		{
			if (this.graph != null)
			{
				if (((SvgElement) this.graph).ParentNode == null)
				{
					this.graph.GraphTransform.Matrix = this.mouseAreaControl.PicturePanel.CoordTransform.Clone();
				}
				((SvgElement) this.graph).pretime = -1;
			}
			return false;
		}

		private void UpdateGraph(string newpoints)
		{
			if (this.graph != null)
			{
				PointF[] tfArray1=new PointF[0];
				if (this.graph is Line)
				{
					tfArray1 = ((Line) this.graph).Points;
				}

				SvgDocument document1 = this.mouseAreaControl.SVGDocument;
				bool flag1 = document1.AcceptChanges;
				document1.AcceptChanges = true;
				document1.NumberOfUndoOperations = 200;
				if (( ((((SvgElement) this.graph).InfoList.Count == 1) && (this.mouseAreaControl.SVGDocument.ControlTime == 0))) || (((SvgElement) this.graph).ParentNode == null))
				{
					//AttributeFunc.SetAttributeValue((SvgElement) this.graph, "points", newpoints);
					if(this.points[0]!=tfArray1[0])
					{
						AttributeFunc.SetAttributeValue((SvgElement) this.graph, "x1", this.points[0].X.ToString());
						AttributeFunc.SetAttributeValue((SvgElement) this.graph, "y1", this.points[0].Y.ToString());

					}
					if(this.points[1]!=tfArray1[1])
					{
						AttributeFunc.SetAttributeValue((SvgElement) this.graph, "x2", this.points[1].X.ToString());
						AttributeFunc.SetAttributeValue((SvgElement) this.graph, "y2", this.points[1].Y.ToString());
					}
					this.graph.IsChanged=true;
				}

                
				document1.NotifyUndo();
				document1.AcceptChanges = flag1;
			}
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
				if (this.graph != value)
				{
					this.graph = value;
					this.mouseAreaControl.Invalidate();
				}
			}
		}

		private PolyOperate Operate
		{
			set
			{
				this.operate = value;
				string text1 = string.Empty;
				switch (this.operate)
				{
					case PolyOperate.Draw:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.PolyDraw;
						if (!this.tooltips.ContainsKey("polydraw"))
						{
							this.tooltips.Add("polydraw", DrawAreaConfig.GetLabelForName("polydraw"));
						}
						text1 = (string) this.tooltips["polydraw"];
						break;
					}
					case PolyOperate.MovePath:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.MovePath;
						if (!this.tooltips.ContainsKey("polymmovepath"))
						{
							this.tooltips.Add("polymmovepath", DrawAreaConfig.GetLabelForName("polymmovepath"));
						}
						text1 = (string) this.tooltips["polymmovepath"];
						break;
					}
					case PolyOperate.MovePoint:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.dotCursor;
						if (this.moveindex >= 0)
						{
							this.mouseAreaControl.Cursor = SpecialCursors.ShapeDragCursor;
						}
						if (!this.tooltips.ContainsKey("polymovepoint"))
						{
							this.tooltips.Add("polymovepoint", DrawAreaConfig.GetLabelForName("polymovepoint"));
						}
						text1 = (string) this.tooltips["polymovepoint"];
						break;
					}
					case PolyOperate.Del:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.PolyDel;
						if (!this.tooltips.ContainsKey("polydel"))
						{
							this.tooltips.Add("polydel", DrawAreaConfig.GetLabelForName("polydel"));
						}
						text1 = (string) this.tooltips["polydel"];
						break;
					}
					case PolyOperate.Add:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.PolyAdd;
						if (!this.tooltips.ContainsKey("polyadd"))
						{
							this.tooltips.Add("polyadd", DrawAreaConfig.GetLabelForName("polyadd"));
						}
						text1 = (string) this.tooltips["polyadd"];
						break;
					}
				}
				if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
				{
					if (!this.tooltips.ContainsKey("ctrlpoly"))
					{
						this.tooltips.Add("ctrlpoly", DrawAreaConfig.GetLabelForName("ctrlpoly"));
					}
					text1 = text1 + ((string) this.tooltips["ctrlpoly"]);
				}
				this.mouseAreaControl.PicturePanel.ToolTip(text1, 1);
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
			SvgElementCollection collection1 =this.mouseAreaControl.SVGDocument.CurrentLayer.GraphList;
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
//						RectangleF ef1 = ab1.GPath.GetBounds();
//						PointF[] tfArray2 = new PointF[] { new PointF(ef1.X + (ef1.Width / 2f), ef1.Y + (ef1.Height / 2f)) } ;//中心点
//						tfArray1 = tfArray2;
//						ab1.GraphTransform.Matrix.TransformPoints(tfArray1);
//						if (this.InPoint(mousepoint, tfArray1[0], 10f))
//						{
//							connectpoint = tfArray1[0];
//							return ab1;
//						}
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
		private PointF[] points;
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

