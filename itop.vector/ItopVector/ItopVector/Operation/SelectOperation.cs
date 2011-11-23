namespace ItopVector.DrawArea
{
	using ItopVector;
	using ItopVector.Core;
	using ItopVector.Core.Animate;
	using ItopVector.Core.Document;
	using ItopVector.Core.Figure;
	using ItopVector.Core.Func;
	using ItopVector.Core.Interface.Figure;
	using ItopVector.Core.UnDo;
	using ItopVector.Core.Interface;
	using ItopVector.Resource;
	using System;
	using System.Collections;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;
	using System.Xml;


	/// <summary>
	/// 选择操作
	/// 
	/// </summary>
	public class SelectOperation : IOperation
	{
        public event ElementMoveEventHandler OnElementMove;
		// Methods
		public SelectOperation()
		{
			this.mouseAreaControl = null;
			this.toBeSelectedGraph = null;
			this.toBeSelectedPath = null;
			this.currentMousePoint = MousePoint.None;
			this.reversePath = new GraphicsPath();
			this.oriPath = new GraphicsPath();
			this.startpoint = PointF.Empty;
			this.movePoint = PointF.Empty;
			this.selectMatrix = new Matrix();
			this.totalmatrix = new Matrix();
			this.controlPoint = PointF.Empty;
			this.selectAreaPath = new GraphicsPath();
			this.win32 = new Win32();
			this.AreaPoints = new ArrayList(0x10);
			this.ps = new PointF[8];
			this.rotateindex = 0;
			this.selectCollection = new SvgElementCollection();
			this.tooltips = new Hashtable(0x10);
			this.rotatepath = new GraphicsPath();
		}

		internal SelectOperation(MouseArea mousecontrol)
		{
			this.mouseAreaControl = null;
			this.toBeSelectedGraph = null;
			this.toBeSelectedPath = null;
			this.currentMousePoint = MousePoint.None;
			this.reversePath = new GraphicsPath();
			this.oriPath = new GraphicsPath();
			this.startpoint = PointF.Empty;
			this.movePoint = PointF.Empty;
			this.selectMatrix = new Matrix();
			this.totalmatrix = new Matrix();
			this.controlPoint = PointF.Empty;
			this.selectAreaPath = new GraphicsPath();
			this.AreaPoints = new ArrayList(0x10);
			this.ps = new PointF[8];
			this.rotateindex = 0;
			this.selectCollection = new SvgElementCollection();
			this.tooltips = new Hashtable(0x10);
			this.rotatepath = new GraphicsPath();
			this.mouseAreaControl = mousecontrol;
			this.win32 = this.mouseAreaControl.win32;
			this.mouseAreaControl.DefaultCursor = SpecialCursors.selectCursor;
			this.freeSelect = mouseAreaControl.PicturePanel.FreeSelect;
		}		
		public bool FreeSelect
		{
			get{return freeSelect;}
			set{freeSelect =value;}            
		}
		public void Dispose()
		{
			this.mouseAreaControl.SelectOperation = null;
		}
      
		private MousePoint GetMousePoint(GraphicsPath path, PointF point, Matrix matrix, ToolOperation operation)
		{
			if (operation == ToolOperation.Flip)
			{
				this.currentMousePoint = MousePoint.Flip;
			}
			RectangleF ef1 = PathFunc.GetBounds(path);
			PointF[] tfArray1 = new PointF[9] { new PointF(ef1.X, ef1.Y), new PointF(ef1.X + (ef1.Width / 2f), ef1.Y), new PointF(ef1.Right, ef1.Y), new PointF(ef1.Right, ef1.Y + (ef1.Height / 2f)), new PointF(ef1.Right, ef1.Bottom), new PointF(ef1.X + (ef1.Width / 2f), ef1.Bottom), new PointF(ef1.X, ef1.Bottom), new PointF(ef1.X, ef1.Y + (ef1.Height / 2f)), new PointF(ef1.X + (ef1.Width / 2f), ef1.Y + (ef1.Height / 2f)) } ;
			this.ps = tfArray1;
			matrix.TransformPoints(this.ps);
			//			this.ps = this.ps;
			RectangleF ef2 = new RectangleF(this.ps[0].X - 2f, this.ps[0].Y - 2f, 5f, 5f);
			GraphicsPath path1 = new GraphicsPath();
			path1.AddRectangle(ef2);
			ef2 = new RectangleF(this.ps[2].X - 2f, this.ps[2].Y - 2f, 5f, 5f);
			GraphicsPath path2 = new GraphicsPath();
			path2.AddRectangle(ef2);
			ef2 = new RectangleF(this.ps[1].X - 2f, this.ps[1].Y - 2f, 5f, 5f);
			GraphicsPath path3 = new GraphicsPath();
			path3.AddRectangle(ef2);
			ef2 = new RectangleF(this.ps[7].X - 2f, this.ps[7].Y - 2f, 5f, 5f);
			GraphicsPath path4 = new GraphicsPath();
			path4.AddRectangle(ef2);
			ef2 = new RectangleF(this.ps[6].X - 2f, this.ps[6].Y - 2f, 5f, 5f);
			GraphicsPath path5 = new GraphicsPath();
			path5.AddRectangle(ef2);
			ef2 = new RectangleF(this.ps[3].X - 2f, this.ps[3].Y - 2f, 5f, 5f);
			GraphicsPath path6 = new GraphicsPath();
			path6.AddRectangle(ef2);
			ef2 = new RectangleF(this.ps[5].X - 3f, this.ps[5].Y - 2f, 5f, 5f);
			GraphicsPath path7 = new GraphicsPath();
			path7.AddRectangle(ef2);
			ef2 = new RectangleF(this.ps[4].X - 2f, this.ps[4].Y - 2f, 5f, 5f);
			GraphicsPath path8 = new GraphicsPath();
			path8.AddRectangle(ef2);
			GraphicsPath path9 = new GraphicsPath();
			path9.AddLine(this.ps[0], this.ps[6]);
			GraphicsPath path10 = new GraphicsPath();
			path10.AddLine(this.ps[2], this.ps[4]);
			GraphicsPath path11 = new GraphicsPath();
			path11.AddLine(this.ps[0], this.ps[2]);
			GraphicsPath path12 = new GraphicsPath();
			path12.AddLine(this.ps[6], this.ps[4]);
			GraphicsPath path13 = new GraphicsPath();
			path13.AddRectangle(new RectangleF(this.ps[0].X - 5f, this.ps[0].Y - 5f, 8f, 8f));
			GraphicsPath path14 = new GraphicsPath();
			path14.AddRectangle(new RectangleF(this.ps[2].X - 2f, this.ps[2].Y - 5f, 8f, 8f));
			GraphicsPath path15 = new GraphicsPath();
			path15.AddRectangle(new RectangleF(this.ps[4].X - 3f, this.ps[4].Y - 3f, 8f, 8f));
			GraphicsPath path16 = new GraphicsPath();
			path16.AddRectangle(new RectangleF(this.ps[6].X - 2f, this.ps[6].Y - 5f, 8f, 8f));
			PointF tf1 = this.mouseAreaControl.CenterPoint;
			GraphicsPath path17 = new GraphicsPath();
			if (!tf1.IsEmpty)
			{
				path17.AddEllipse((float) (tf1.X - 3f), (float) (tf1.Y - 3f), (float) 6f, (float) 6f);
			}
			RectangleF ef3 = new RectangleF(point.X - 1f, point.Y - 1f, 2f, 2f);
			GraphicsPath path18 = (GraphicsPath) path.Clone();
			path.Transform(matrix);
			Pen pen1 = new Pen(Color.Blue, 3f);
			pen1.Alignment = PenAlignment.Center;
			MousePoint point1 = MousePoint.None;
			if (OperationFunc.IsSelectOperation(operation))
			{
				ToolOperation operation1 = operation;
				if ((operation1==ToolOperation.Exceptant)||(operation1 == ToolOperation.Select) && (path.IsVisible(point) || path.IsOutlineVisible(point, pen1)))
				{
					return MousePoint.Translate;
				}
			}
			else if (OperationFunc.IsTransformOperation(operation))
			{
				//				if (path17.IsVisible(point) || path17.IsOutlineVisible(point, pen1))
				//				{
				//					point1 = MousePoint.CenterPoint;
				//				}
				//				else 
				if (path1.IsVisible(point) || path1.IsOutlineVisible(point, pen1))
				{
					point1 = MousePoint.ScaleTopLeft;
				}
				else if (path3.IsVisible(point) || path3.IsOutlineVisible(point, pen1))
				{
					point1 = MousePoint.ScaleTopMiddle;
				}
				else if (path2.IsVisible(point) || path2.IsOutlineVisible(point, pen1))
				{
					point1 = MousePoint.ScaleTopRight;
				}
				else if (path4.IsVisible(point) || path4.IsOutlineVisible(point, pen1))
				{
					point1 = MousePoint.ScaleMiddleLeft;
				}
				else if (path6.IsVisible(point) || path6.IsOutlineVisible(point, pen1))
				{
					point1 = MousePoint.ScaleMiddleRight;
				}
				else if (path5.IsVisible(point) || path5.IsOutlineVisible(point, pen1))
				{
					point1 = MousePoint.ScaleBottomLeft;
				}
				else if (path7.IsVisible(point) || path7.IsOutlineVisible(point, pen1))
				{
					point1 = MousePoint.ScaleBottomMiddle;
				}
				else if (path8.IsVisible(point) || path8.IsOutlineVisible(point, pen1))
				{
					point1 = MousePoint.ScaleBottomRight;
				}
				else
				{
					if (path9.IsVisible(point) || path9.IsOutlineVisible(point, pen1))
					{
						return MousePoint.SkewYLeft;
					}
					if (path10.IsVisible(point) || path10.IsOutlineVisible(point, pen1))
					{
						point1 = MousePoint.SkewYRight;
					}
					else if (path11.IsVisible(point) || path11.IsOutlineVisible(point, pen1))
					{
						point1 = MousePoint.SkewXTop;
					}
					else if (path12.IsVisible(point) || path12.IsOutlineVisible(point, pen1))
					{
						point1 = MousePoint.SkewXBottom;
					}
					else if (path13.IsVisible(point) || path13.IsOutlineVisible(point, pen1))
					{
						this.rotateindex = 6;
						point1 = MousePoint.Rotate;
					}
					else if (path14.IsVisible(point) || path14.IsOutlineVisible(point, pen1))
					{
						this.rotateindex = 2;
						point1 = MousePoint.Rotate;
					}
					else if (path15.IsVisible(point) || path15.IsOutlineVisible(point, pen1))
					{
						this.rotateindex = 0;
						point1 = MousePoint.Rotate;
					}
					else if (path16.IsVisible(point) || path16.IsOutlineVisible(point, pen1))
					{
						this.rotateindex = 4;
						point1 = MousePoint.Rotate;
					}
					else if (path.IsVisible(point))// || path.IsOutlineVisible(point,pen1))
					{
						point1 = MousePoint.Translate;
					}
				}
				if (point1 != MousePoint.CenterPoint)
				{
					if (operation == ToolOperation.Rotate)
					{
						if ((point1 == MousePoint.Rotate) || (point1 == MousePoint.Translate))
						{
							point1 = point1;
						}
						else
						{
							point1 = MousePoint.None;
						}
					}
					else if (operation == ToolOperation.Scale)
					{
						if ((((point1 == MousePoint.ScaleBottomLeft) || (point1 == MousePoint.ScaleBottomMiddle)) || ((point1 == MousePoint.ScaleBottomMiddle) || (point1 == MousePoint.ScaleBottomRight))) || ((((point1 == MousePoint.ScaleMiddleLeft) || (point1 == MousePoint.ScaleMiddleRight)) || ((point1 == MousePoint.ScaleTopLeft) || (point1 == MousePoint.ScaleTopMiddle))) || ((point1 == MousePoint.ScaleTopRight) || (point1 == MousePoint.Translate))))
						{
							point1 = point1;
						}
						else
						{
							point1 = MousePoint.None;
						}
					}
					else if (operation == ToolOperation.Skew)
					{
						if (((point1 == MousePoint.SkewXBottom) || (point1 == MousePoint.SkewXTop)) || (((point1 == MousePoint.SkewYLeft) || (point1 == MousePoint.SkewYRight)) || (point1 == MousePoint.Translate)))
						{
							point1 = point1;
						}
						else
						{
							point1 = MousePoint.None;
						}
					}
				}
			}
			//            this.mouseAreaControl.Invalidate();
			return point1;
		}
		public void OnMouseWheel(MouseEventArgs e)
		{
			// TODO:  添加 BezierOperation.DealMouseWheel 实现
		}
		public void OnMouseDown(MouseEventArgs e)
		{
			ToolOperation operation1 = this.mouseAreaControl.CurrentOperation;
			this.startpoint = this.mouseAreaControl.PicturePanel.PointToView(new PointF( e.X, e.Y));

			if ((OperationFunc.IsSelectOperation(operation1) || OperationFunc.IsTransformOperation(operation1)) && (e.Button == MouseButtons.Left))
			{
				this.selectCollection = this.mouseAreaControl.SVGDocument.SelectCollection.Clone();
					
				if ((this.currentMousePoint == MousePoint.None) && (this.toBeSelectedGraph != null))
				{
					this.currentMousePoint = MousePoint.Translate;
					this.toBeSelectedPath = (GraphicsPath) this.toBeSelectedGraph.GPath.Clone();
					this.toBeSelectedPath.Transform(this.toBeSelectedGraph.GraphTransform.Matrix);
					if (((Control.ModifierKeys != Keys.Control) && !this.selectCollection.Contains(this.toBeSelectedGraph)) && (/*!this.toBeSelectedGraph.IsLock &&*/ this.toBeSelectedGraph.DrawVisible))
					{
						this.selectCollection.Clear();
						this.selectCollection.Add(this.toBeSelectedGraph);
					}
				}
				if ((this.toBeSelectedGraph != null) && (Control.ModifierKeys == Keys.Shift))
				{
					if ((!this.mouseAreaControl.SVGDocument.SelectCollection.Contains(this.toBeSelectedGraph) && !this.toBeSelectedGraph.IsLock) && this.toBeSelectedGraph.DrawVisible)
					{
						this.mouseAreaControl.SVGDocument.SelectCollection.Add(this.toBeSelectedGraph);
					}
					else if (this.mouseAreaControl.SVGDocument.SelectCollection.Contains(this.toBeSelectedGraph))
					{
						this.mouseAreaControl.SVGDocument.SelectCollection.Remove(this.toBeSelectedGraph);
					}
				}
				//					this.startpoint = this.mouseAreaControl.PicturePanel.PointToView(new PointF( e.X, e.Y));

				if (operation1 == ToolOperation.AreaSelect)
				{
					this.AreaPoints.Clear();
					this.AreaPoints.Add(new PointF(e.X,e.Y));
				}
				this.reversePath.Reset();
				
			}	
			if(OperationFunc.IsConnectLineOperation(operation1) && this.toBeSelectedGraph!=null)
			{
				//this.startpoint = new PointF((float) e.X, (float) e.Y);
				
				if(MouseArea.ConnectLinePoints==0)
				{
					MouseArea.ConLineStartPoint.X=e.X;
					MouseArea.ConLineStartPoint.Y=e.Y;
					MouseArea.SvgStartObj=this.toBeSelectedGraph;

				}
				if(MouseArea.ConnectLinePoints==1)
				{
					MouseArea.ConLineEndPoint.X=e.X;
					MouseArea.ConLineEndPoint.Y=e.Y;
					MouseArea.SvgEndObj=this.toBeSelectedGraph;
				}
				MouseArea.ConnectLinePoints+=1;
			}
			
		}

		public void OnMouseMove(MouseEventArgs e)
		{			
			SvgElementCollection collection1;
			PointF tf1 = this.mouseAreaControl.PicturePanel.PointToView(this.mouseAreaControl.CenterPoint);
			PointF tf2 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
			ToolOperation operation1 = this.mouseAreaControl.CurrentOperation;
			if (this.mouseAreaControl.CurrentOperation == ToolOperation.AreaSelect && !OperationFunc.IsConnectLineOperation(operation1))
			{
				if (!this.tooltips.ContainsKey("select"))
				{
					this.tooltips.Add("select", DrawAreaConfig.GetLabelForName("select").Trim());
				}
				this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["select"], 1);
			}
			if(OperationFunc.IsConnectLineOperation(operation1) && MouseArea.ConnectLinePoints==0)
			{
				if (!this.tooltips.ContainsKey("connlinestart"))
				{
					this.tooltips.Add("connlinestart", DrawAreaConfig.GetLabelForName("connlinestart").Trim());
				}
				this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["connlinestart"], 1);
			}
			if(OperationFunc.IsConnectLineOperation(operation1) && MouseArea.ConnectLinePoints==1)
			{
				if (!this.tooltips.ContainsKey("connlineend"))
				{
					this.tooltips.Add("connlineend", DrawAreaConfig.GetLabelForName("connlineend").Trim());
				}
				this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["connlineend"], 1);
			}
			if (e.Button != MouseButtons.None)
			{
				if (e.Button == MouseButtons.Left)
				{
					//					string aaa=((XmlElement)selectCollection[0]).GetAttribute("layer").ToString();
					if(this.mouseAreaControl.CanEdit==false )
					{
						goto Lable_CanMove;
					}
					
					if (operation1 == ToolOperation.AreaSelect)
					{
						this.AreaPoints.Add(new PointF((float) e.X, (float) e.Y));
						this.mouseAreaControl.Invalidate();
					}
					else if (operation1 != ToolOperation.ColorSelect)
					{
						SizeF ef1 = this.mouseAreaControl.PicturePanel.GridSize;
						float single1 = ef1.Height;
						float single2 = ef1.Width;
						if (this.currentMousePoint == MousePoint.CenterPoint)
						{
							this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
							this.win32.W32SetROP2(7);
							this.win32.W32PolyDraw(this.reversePath);
							this.reversePath.Reset();
							PointF tf5 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
							if (this.mouseAreaControl.PicturePanel.SnapToGrid)
							{
								int num2 = (int) ((tf5.X + (single2 / 2f)) / single2);
								int num3 = (int) ((tf5.Y + (single1 / 2f)) / single1);
								tf5 = (PointF) new Point((int) (num2 * single2), (int) (single1 * num3));
							}
							PointF tf6 = this.mouseAreaControl.PicturePanel.PointToSystem(tf5);
							this.reversePath.AddEllipse((float) (tf6.X - 3f), (float) (tf6.Y - 3f), (float) 8f, (float) 8f);
							this.win32.W32PolyDraw(this.reversePath);
							this.win32.ReleaseDC();
						}
						else if ((this.currentMousePoint != MousePoint.None) && (this.toBeSelectedPath != null)&&(this.toBeSelectedGraph==null||(this.toBeSelectedGraph!=null && !this.toBeSelectedGraph.IsLock)) && (((IGraph)selectCollection[0]).Layer.ID==SvgDocument.currentLayer || freeSelect))
						{//拖动所选图形
							if(operation1==ToolOperation.WindowZoom)
							{
								this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
								this.win32.W32SetROP2(7);
								this.win32.W32PolyDraw(this.reversePath);
								this.reversePath.Reset();
								PointF tf12 = new PointF((float) e.X, (float) e.Y);
								PointF tf13 = this.mouseAreaControl.PicturePanel.PointToSystem(startpoint);
								float single9 = Math.Min(tf13.X, tf12.X);
								float single10 = Math.Min(tf13.Y, tf12.Y);
								float single11 = Math.Max(tf13.X, tf12.X);
								float single12 = Math.Max(tf13.Y, tf12.Y);
								this.reversePath.AddRectangle(new RectangleF(single9, single10, single11 - single9, single12 - single10));
								this.win32.W32PolyDraw(this.reversePath);
								this.win32.ReleaseDC();
								goto Lable_CanMove;
							}
							this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
							this.win32.W32SetROP2(7);
							this.win32.W32PolyDraw(this.reversePath);
							//this.win32.ReleaseDC();
							float single3 = 0f;
							float single4 = 0f;
							PointF tf7 = this.startpoint;// this.mouseAreaControl.PicturePanel.PointToView(this.startpoint);
							PointF tf8 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
							if (this.mouseAreaControl.PicturePanel.SnapToGrid)
							{
								int num4 = (int) ((tf7.X + (single2 / 2f)) / single2);
								int num5 = (int) ((tf7.Y + (single1 / 2f)) / single1);
								tf7 = (PointF) new Point((int) (num4 * single2), (int) (num5 * single1));
								num4 = (int) ((tf8.X + (single2 / 2f)) / single2);
								num5 = (int) ((tf8.Y + (single1 / 2f)) / single1);
								tf8 = (PointF) new Point((int) (num4 * single2), (int) (single1 * num5));
							}
							tf7 = this.mouseAreaControl.PicturePanel.PointToSystem(tf7);
							tf8 = this.mouseAreaControl.PicturePanel.PointToSystem(tf8);
							PointF tf9 = tf7;
							PointF tf10 = tf8;
							this.reversePath.Reset();
							int num6 = 0;
							SvgElementCollection clines=new SvgElementCollection();
							SvgElementCollection.ISvgElementEnumerator enumerator1 = this.selectCollection.GetEnumerator();
							
							while (enumerator1.MoveNext())
							{
								float single5;
								float single6;
								GraphicsPath path7;
								SvgElement element2 = (SvgElement) enumerator1.Current;
								if (!(element2 is IGraph) )
								{
									continue;
								}
								if (element2 is Use && (element2 as Use).LimitSize && currentMousePoint!=MousePoint.Translate)continue;
								this.reversePath.StartFigure();
								Matrix matrix6 = ((IGraph) element2).GraphTransform.Matrix.Clone();
								PointF tf11 = PointF.Empty;
								GraphicsPath path6 = (GraphicsPath) ((IGraph) element2).GPath.Clone();
								RectangleF ef2 = PathFunc.GetBounds(path6);
								PointF[] tfArray3 = new PointF[9] { new PointF(ef2.X, ef2.Y), new PointF(ef2.X, ef2.Y + (ef2.Height / 2f)), new PointF(ef2.X, ef2.Bottom), new PointF(ef2.X + (ef2.Width / 2f), ef2.Y), new PointF(ef2.Right, ef2.Y), new PointF(ef2.Right, ef2.Y + (ef2.Height / 2f)), new PointF(ef2.Right, ef2.Bottom), new PointF(ef2.X + (ef2.Width / 2f), ef2.Bottom), new PointF(ef2.X + (ef2.Width / 2f), ef2.Y + (ef2.Height / 2f)) } ;
								PointF[] tfArray1 = tfArray3;
								Matrix matrix7 = matrix6.Clone();
								if(matrix7.IsInvertible)
									matrix7.Invert();
								PointF[] tfArray4 = new PointF[3] { tf9, tf10, this.mouseAreaControl.CenterPoint } ;
								PointF[] tfArray2 = tfArray4;
								if(this.currentMousePoint!=MousePoint.Rotate)
								{
									matrix7.TransformPoints(tfArray2);
								}
								tf7 = tfArray2[0];
								tf8 = tfArray2[1];
								tf1 = tfArray2[2];
								matrix6.Reset();
								single3 = tf8.X - tf7.X;
								single4 = tf8.Y - tf7.Y;

								switch (this.currentMousePoint)
								{
									case MousePoint.ScaleTopLeft:
									case MousePoint.ScaleTopMiddle:
									case MousePoint.ScaleTopRight:
									case MousePoint.ScaleMiddleLeft:
									case MousePoint.ScaleMiddleRight:
									case MousePoint.ScaleBottomLeft:
									case MousePoint.ScaleBottomMiddle:
									case MousePoint.ScaleBottomRight:
									{
										if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
										{
											goto Label_0D82;
										}
										if (this.currentMousePoint != MousePoint.ScaleTopLeft)
										{
											goto Label_0CC6;
										}
										tf11 = tfArray1[6];
										goto Label_0D85;
									}
									case MousePoint.ScaleFromCenter:
									case MousePoint.SkewXFromCenter:
									case MousePoint.SkewYFromCenter:
									case MousePoint.RotateFromCenter:
									{
										goto Label_MOVE;
									}
									case MousePoint.SkewXTop:
									case MousePoint.SkewXBottom:
									case MousePoint.SkewYLeft:
									case MousePoint.SkewYRight:
									{
										if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
										{
											goto Label_0ED9;
										}
										if (this.currentMousePoint != MousePoint.SkewXBottom)
										{
											goto Label_0E88;
										}
										tf11 = tfArray1[0];
										goto Label_0EDC;
									}
									case MousePoint.Rotate://旋转
									{
										if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
										{
											goto Label_103D;
										}
										tf11 = tfArray1[this.rotateindex];
										goto Label_ROTATE;
									}
									case MousePoint.Translate:
									{
										//										if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)//移动范围为45角的倍数
										//										{
										//											single5 = (((float) Math.Atan2((double) single4, (double) single3)) * 180f) / 3.141593f;
										//											single5 = ((int) Math.Round((double) (single5 / 45f), 0)) * 0x2d;
										//											if ((single5 != 90f) && (single5 != -90f))
										//											{
										//												break;
										//											}
										//											single3 = 0f;
										//										}
										if (element2 is IGraph)
										{
								
											foreach(ConnectLine cline in (element2 as IGraph).ConnectLines)
											{
												if(!this.selectCollection.Contains(cline) && cline.Visible )
												{
													if(selectCollection.Contains(cline.StartGraph) && selectCollection.Contains(cline.EndGraph))
													{
														if(!clines.Contains(cline))
														{
															clines.Add(cline);
															GraphicsPath path11 =cline.GetTempPath2(element2 as IGraph,new PointF(single3,single4));
															if(path11.PointCount>0)
																this.reversePath.AddPath(path11,false);
														}
													}
													else
													{
														GraphicsPath path11 =cline.GetTempPath(element2 as IGraph,new PointF(single3,single4));
														if(path11.PointCount>0)
															this.reversePath.AddPath(path11,false);
													}
												}
											}
										}
										goto Label_0C84;
									}
									default:
									{
										goto Label_MOVE;
									}
								}
								single5 = (float) Math.Tan((single5 / 180f) * 3.1415926535897931);
								single4 = single3 * single5;
							Label_0C84:
								matrix6.Translate(single3, single4, MatrixOrder.Prepend);
								goto Label_MOVE;
							Label_0CC6:
								if (this.currentMousePoint == MousePoint.ScaleTopRight)
								{
									tf11 = tfArray1[2];
									goto Label_0D85;
								}
								if (this.currentMousePoint == MousePoint.ScaleBottomLeft)
								{
									tf11 = tfArray1[4];
									goto Label_0D85;
								}
								if (this.currentMousePoint == MousePoint.ScaleBottomRight)
								{
									tf11 = tfArray1[0];
									goto Label_0D85;
								}
								if (this.currentMousePoint == MousePoint.ScaleMiddleLeft)
								{
									tf11 = tfArray1[5];
									goto Label_0D85;
								}
								if (this.currentMousePoint == MousePoint.ScaleMiddleRight)
								{
									tf11 = tfArray1[1];
									goto Label_0D85;
								}
								if (this.currentMousePoint == MousePoint.ScaleTopMiddle)
								{
									tf11 = tfArray1[7];
									goto Label_0D85;
								}
								if (this.currentMousePoint == MousePoint.ScaleBottomMiddle)
								{
									tf11 = tfArray1[3];
								}
								goto Label_0D85;
							Label_0D82:
								tf11 = tf1;
							Label_0D85:
								
								single3 = (tf8.X - tf11.X) / (tf7.X - tf11.X);
								single4 = (tf8.Y - tf11.Y) / (tf7.Y - tf11.Y);
								if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
								{
									single3 = single4 = Math.Min(single3, single4);
								}
								if ((this.currentMousePoint == MousePoint.ScaleMiddleLeft) || (this.currentMousePoint == MousePoint.ScaleMiddleRight))
								{
									single4 = 1f;
								}
								else if ((this.currentMousePoint == MousePoint.ScaleTopMiddle) || (this.currentMousePoint == MousePoint.ScaleBottomMiddle))
								{
									single3 = 1f;
								}
								matrix6.Translate(tf11.X, tf11.Y, MatrixOrder.Prepend);
								matrix6.Scale(single3, single4, MatrixOrder.Prepend);
								matrix6.Translate(-tf11.X, -tf11.Y, MatrixOrder.Prepend);
								goto Label_MOVE;
							Label_0E88:
								if (this.currentMousePoint == MousePoint.SkewXTop)
								{
									tf11 = tfArray1[6];
									goto Label_0EDC;
								}
								if (this.currentMousePoint == MousePoint.SkewYLeft)
								{
									tf11 = tfArray1[4];
									goto Label_0EDC;
								}
								if (this.currentMousePoint == MousePoint.SkewYRight)
								{
									tf11 = tfArray1[0];
								}
								goto Label_0EDC;
							Label_0ED9:
								tf11 = tf1;
							Label_0EDC:
								single3 /= (tfArray1[6].Y - tfArray1[4].Y);
								single4 /= (tfArray1[6].X - tfArray1[2].X);
								if ((this.currentMousePoint == MousePoint.SkewXTop) || (this.currentMousePoint == MousePoint.SkewXBottom))
								{
									single4 = 0f;
									if (this.currentMousePoint == MousePoint.SkewXTop)
									{
										single3 = -single3;
									}
								}
								else if ((this.currentMousePoint == MousePoint.SkewYLeft) || (this.currentMousePoint == MousePoint.SkewYRight))
								{
									if (this.currentMousePoint == MousePoint.SkewYLeft)
									{
										single4 = -single4;
									}
									single3 = 0f;
								}
								single3 = (single3 == 0f) ? 0f : ((single3 / Math.Abs(single3)) * Math.Min(Math.Abs(single3), (float) 3f));
								single4 = (single4 == 0f) ? 0f : ((single4 / Math.Abs(single4)) * Math.Min(Math.Abs(single4), (float) 3f));
								matrix6.Translate(tf11.X, tf11.Y, MatrixOrder.Prepend);
								matrix6.Shear(single3, single4, MatrixOrder.Prepend);
								matrix6.Translate(-tf11.X, -tf11.Y, MatrixOrder.Prepend);
								goto Label_MOVE;
							Label_103D:
								tf11 = tf1;
							Label_ROTATE://旋转
								single6 = 0f;
								float single7=0f;
								if(tf7.X==tf11.X)
								{
									single7 = 1.570796f;
								}
								else
								{
									single7 = (float) Math.Atan((double) ((tf7.Y - tf11.Y) / (tf7.X - tf11.X)));
								}
								float single8 = 0f;
								if(tf8.X==tf11.X)
								{
									single8 = 1.570796f;
								}
								else
								{
									single8 = (float) Math.Atan((double) ((tf8.Y - tf11.Y) / (tf8.X - tf11.X)));
								}
								single6 = ((float) (((double) (single8 - single7)) / 3.1415926535897931)) * 180f;
								if (((tf8.X - tf11.X) * (tf7.X - tf11.X)) < 0f)
								{
									single6 += 180f;
								}
								if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift) && (single6 != 0f))
								{
									single6 = (single6 / Math.Abs(single6)) * ((((int) (Math.Abs(single6) + 23f)) / 0x2d) * 0x2d);
								}								
								matrix6.RotateAt(single6,tf11);

							Label_MOVE://移动
								path7 = new GraphicsPath();
								if (element2 is Text)
								{
									RectangleF ef3 = ((IGraph) element2).GPath.GetBounds();
									path7.AddRectangle(ef3);
								}
								else
								{
									path7 = (GraphicsPath) ((IGraph) element2).GPath.Clone();
									if ((path7.PointCount > 0xbb8) || (element2 is Group))
									{
										RectangleF ef4 = path7.GetBounds();
										path7.Reset();
										path7.AddRectangle(ef4);
									}
								}
								using (Matrix matrix8 = ((IGraph) element2).GraphTransform.Matrix.Clone())
								{
									if(this.currentMousePoint==MousePoint.Rotate)
									{
										matrix8.Multiply(matrix6,MatrixOrder.Append);
									}
									else
									{
										matrix8.Multiply(matrix6, MatrixOrder.Prepend);
									}
									path7.Transform(matrix8);
								}
								if (path7.PointCount > 0)
								{
									this.reversePath.AddPath(path7, false);
								}
								num6++;
							}
							if (this.reversePath.PointCount > 0xbb8)
							{
								RectangleF ef5 = this.reversePath.GetBounds();
								this.reversePath.Reset();
								this.reversePath.AddRectangle(ef5);
							}
							this.win32.W32PolyDraw(this.reversePath);
							this.win32.ReleaseDC();
						}
						else if ((this.currentMousePoint == MousePoint.None) && (OperationFunc.IsSelectOperation(operation1) || OperationFunc.IsTransformOperation(operation1)))
						{//选区
						
							this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
							this.win32.W32SetROP2(7);
							this.win32.W32PolyDraw(this.reversePath);
							
							this.reversePath.Reset();
							PointF tf12 = new PointF((float) e.X, (float) e.Y);
							PointF tf13 = this.mouseAreaControl.PicturePanel.PointToSystem(startpoint);

							float single9 = Math.Min(tf13.X, tf12.X);
							float single10 = Math.Min(tf13.Y, tf12.Y);
							float single11 = Math.Max(tf13.X, tf12.X);
							float single12 = Math.Max(tf13.Y, tf12.Y);
							this.reversePath.AddRectangle(new RectangleF(single9, single10, single11 - single9, single12 - single10));
							this.win32.W32PolyDraw(this.reversePath);
						
							this.win32.ReleaseDC();
						}
					}
				}// 鼠标左键按下
				
				return;
			}
			Lable_CanMove:
				this.selectMatrix.Reset();
			this.toBeSelectedGraph = null;
			this.toBeSelectedPath = null;
			Matrix matrix1 = this.mouseAreaControl.PicturePanel.CoordTransform.Clone();
			matrix1.Invert();
		
			Pen pen2 = new Pen(Color.Blue,3/this.mouseAreaControl.PicturePanel.ScaleUnit);
			pen2.Alignment = PenAlignment.Center;
			if ((!OperationFunc.IsSelectOperation(operation1)) /*&& (operation1 != ToolOperation.ShapeTransform))*/ && !OperationFunc.IsTransformOperation(operation1) && !OperationFunc.IsConnectLineOperation(operation1))
			{
				goto Label_05AD;
			}
			
			if (this.mouseAreaControl.SVGDocument.SelectCollection.Count > 0) 
			{
				
				if ((operation1 == ToolOperation.Select) ||(operation1 == ToolOperation.Exceptant)|| (operation1 == ToolOperation.ShapeTransform))
				{//在图元上移动
					
					GraphicsPath path1 = new GraphicsPath();
					path1.AddEllipse((float) (this.mouseAreaControl.CenterPoint.X - 4f), (float) (this.mouseAreaControl.CenterPoint.Y - 4f), (float) 8f, (float) 8f);
					//					if (path1.IsVisible(new PointF(e.X + this.mouseAreaControl.PicturePanel.VirtualLeft, e.Y + this.mouseAreaControl.PicturePanel.VirtualTop)))
					//					{
					//						
					//						this.CurrentMousePoint = MousePoint.CenterPoint;
					//						return;
					//					}
					
					
					this.selectMatrix = this.mouseAreaControl.PicturePanel.SelectMatrix.Clone();
					GraphicsPath path2 = (GraphicsPath) this.mouseAreaControl.PicturePanel.SelectPath.Clone();
					PointF tf3 = tf2;
					Matrix matrix2 = this.selectMatrix.Clone();
					matrix2.Multiply(matrix1, MatrixOrder.Append);
					this.CurrentMousePoint = this.GetMousePoint(path2, tf3, matrix2, operation1);

					//
					
					//					if(selectCollection.Count>0)
					//					{
					//						if(((XmlElement)selectCollection[0]).GetAttribute("layer").ToString()!=SvgDocument.currentLayer)
					//						{
					//							this.CurrentMousePoint = MousePoint.None;
					//							return;
					//						}
					//					}

					if (this.currentMousePoint == MousePoint.None) //从选中的图元移出
					{
						if(graphTemp2!=null) //graphTemp2 选中的图元
						{
							this.mouseAreaControl.PicturePanel.OnMoveOut(mouseAreaControl.SVGDocument.SelectCollection[0] as SvgElement,e);
							graphTemp2=null;
						}
						goto Label_044B;
					}
				
					this.toBeSelectedPath = (GraphicsPath) this.mouseAreaControl.PicturePanel.SelectPath.Clone();
					this.selectMatrix = this.mouseAreaControl.PicturePanel.SelectMatrix.Clone();
					//					
				
					if(this.currentMousePoint==MousePoint.Translate)  //移进选中的图元
					{
						if(graphTemp2==null)
						{
							bool flag1=false;
							foreach(IGraph graph11 in mouseAreaControl.SVGDocument.SelectCollection)
							{
								using (Matrix matrix5 = graph11.GraphTransform.Matrix.Clone())
								{
									matrix5.Multiply(matrix1, MatrixOrder.Append);
				
									using(GraphicsPath path5 =graph11.GPath.Clone() as GraphicsPath)
									{
										path5.Transform(matrix5);
						
										if(graph11 is Line || graph11 is Polyline )//线
										{
											//											using(Pen pen2 = new Pen(Color.Blue, ((GraphPath)graph11).GraphStroke.StrokePen.Width + 2f))
											//											{
											//												pen2.Alignment = PenAlignment.Center;						
											flag1=path5.IsOutlineVisible(tf2,pen2);
											//											}
										}
										else if (graph11 is Use || graph11 is Text)
										{
											flag1 = path5.GetBounds().Contains(tf2);

										}
										else
										{
							
											flag1=path5.IsVisible(tf2)||path5.IsOutlineVisible(tf2,pen2);//.GetBounds().Contains(tf2);							
										}
									}
								}
								if (flag1)
								{

									graphTemp2=graph11;
									break;
								}

							}
							//							graphTemp2=mouseAreaControl.SVGDocument.SelectCollection[0] as IGraph;

							if (graphTemp2!=null)
							{
								this.mouseAreaControl.PicturePanel.OnMoveIn(graphTemp2 as SvgElement,e);
							}
							return;
						}
					}
					//事件
					if(this.currentMousePoint == MousePoint.Translate) //在选中的图元上移动
					{
						this.mouseAreaControl.PicturePanel.OnMoveOver(this.mouseAreaControl.SVGDocument.SelectCollection[0],e);					
					}
			
					//					return;    test
				}
				
			
				if (((operation1 == ToolOperation.FreeTransform) || (operation1 == ToolOperation.Scale)) || ((operation1 == ToolOperation.Rotate) || (operation1 == ToolOperation.Skew)))
				{
					//this.mouseAreaControl.PicturePanel.OnMoveOver(this.mouseAreaControl.SVGDocument.SelectCollection[0],e);
					//					if ((Math.Abs((float) (e.X - this.mouseAreaControl.CenterPoint.X)) < 2f) && (Math.Abs((float) (e.Y - this.mouseAreaControl.CenterPoint.Y)) < 2f))
					//					{
					//						//中心点
					//						this.CurrentMousePoint = MousePoint.CenterPoint;
					//						return;
					//					}
					
					GraphicsPath path3 = (GraphicsPath) this.mouseAreaControl.PicturePanel.SelectPath.Clone();
					this.selectMatrix = this.mouseAreaControl.PicturePanel.SelectMatrix.Clone();
					Matrix matrix3 = this.selectMatrix.Clone();
					matrix3.Multiply(matrix1, MatrixOrder.Append);
					PointF tf4 = tf2;
					bool flag1 = true;
					if ((this.mouseAreaControl.SVGDocument.SelectCollection.Count == 1) && (this.mouseAreaControl.SVGDocument.SelectCollection[0] is IGraph))
					{
						SvgElement element1 = (SvgElement) this.mouseAreaControl.SVGDocument.SelectCollection[0];
						if (!flag1)
						{
							GraphicsPath path4 = (GraphicsPath) ((IGraph) element1).GPath.Clone();
							Matrix matrix4 = ((IGraph) element1).GraphTransform.Matrix.Clone();
							matrix4.Multiply(matrix1, MatrixOrder.Append);
							path4.Transform(matrix4);
							Pen pen1 = new Pen(Color.Blue, 3f);
							pen1.Alignment = PenAlignment.Center;
							if (path4.IsVisible(tf2))
							{
								this.CurrentMousePoint = MousePoint.Translate;
							}
							else
							{
								this.CurrentMousePoint = MousePoint.None;
							}
						}
					}
					if (flag1)
					{
						this.CurrentMousePoint = this.GetMousePoint(path3, new PointF((float) e.X, (float) e.Y), this.selectMatrix.Clone(), operation1);
					}
					//事件
					if(this.currentMousePoint == MousePoint.Translate) //在选中图元上移动
					{
						this.mouseAreaControl.PicturePanel.OnMoveOver(this.mouseAreaControl.SVGDocument.SelectCollection[0],e);
					
					}
					if (this.currentMousePoint != MousePoint.None)
					{
						this.toBeSelectedPath = (GraphicsPath) this.mouseAreaControl.PicturePanel.SelectPath.Clone();
						this.selectMatrix = this.mouseAreaControl.PicturePanel.SelectMatrix.Clone();
						return;
					}
					
				}
			}
			Label_044B://没有选中图元时
            ILayer layer1 = this.mouseAreaControl.SVGDocument.Layers[SvgDocument.currentLayer] as ILayer;
            if (layer1 == null) return;
            collection1 = layer1.GraphList; // this.mouseAreaControl.PicturePanel.ElementList;
            if (collection1 == null)
            {
                return;
            }

            for (int num1 = collection1.Count - 1; num1 >= 0; num1--)
            {
                IGraph graph1 = (IGraph)collection1[num1];
                if (graph1.CanSelect && graph1.DrawVisible && graph1.Visible)
                {
                    bool flag1 = false;

                    using (GraphicsPath path5 = (GraphicsPath)graph1.GPath.Clone())
                    {
                        using (Matrix matrix5 = graph1.GraphTransform.Matrix.Clone())
                        {
                            matrix5.Multiply(matrix1, MatrixOrder.Append);

                            path5.Transform(matrix5);

                            if (graph1 is Line || graph1 is Polyline)//绾?
                            {
                                //								using(Pen pen2 = new Pen(Color.Blue, ((GraphPath)graph1).GraphStroke.StrokePen.Width + 2f))
                                //								{
                                //									pen2.Alignment = PenAlignment.Center;						
                                flag1 = path5.IsOutlineVisible(tf2, pen2);
                                //								}
                            }
                            else if (graph1 is Use || graph1 is Text)
                            {
                                flag1 = path5.GetBounds().Contains(tf2);

                            }
                            else
                            {

                                flag1 = (path5.IsVisible(tf2) || path5.IsOutlineVisible(tf2, pen2));//.GetBounds().Contains(tf2);							
                            }
                        }


                        if (flag1)// && ((XmlElement)graph1).GetAttribute("layer").ToString()==SvgDocument.currentLayer)
                        {

                            //if(this.mouseAreaControl.Cursor != SpecialCursors.DragInfoCursor)

                            //							path5.Transform((this.mouseAreaControl.SVGDocument.RootElement as SVG).GraphTransform.Matrix);
                            //							System.Windows.Forms.ControlPaint.DrawBorder3D(this.mouseAreaControl.CreateGraphics(),Rectangle.Ceiling(path5.GetBounds()),Border3DStyle.Raised);

                            this.toBeSelectedGraph = graph1;
                            this.toBeSelectedPath = (GraphicsPath)graph1.GPath.Clone();
                            this.selectMatrix = graph1.GraphTransform.Matrix.Clone();
                            //							if (!this.tooltips.ContainsKey("select"))
                            //							{
                            //								this.tooltips.Add("select", DrawAreaConfig.GetLabelForName("select"));
                            //							}
                            //							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["select"], 1);
                            //娌℃湁閫変腑鍥惧厓鏃讹紝榧犳爣绉诲叆鍥惧厓鍖哄煙

                            this.mouseAreaControl.Cursor = SpecialCursors.DragInfoCursor;
                            if (this.mouseAreaControl.CurrentOperation == ToolOperation.WindowZoom)
                            {
                                this.mouseAreaControl.Cursor = SpecialCursors.WindowZoom;
                            }
                            //if (this.currentMousePoint == MousePoint.None && graphTemp == null)
                            {
                                if (this.currentMousePoint == MousePoint.None && graphTemp == null) {
                                    graphTemp = graph1;
                                    this.mouseAreaControl.PicturePanel.OnMoveIn(graph1 as SvgElement, e);
                                    return;
                                } else if (graphTemp!=null && graphTemp != graph1) {
                                    this.mouseAreaControl.PicturePanel.OnMoveOut(graphTemp as SvgElement, e);
                                    graphTemp = graph1;
                                    this.mouseAreaControl.PicturePanel.OnMoveIn(graph1 as SvgElement, e);
                                    return;
                                }
                                
                            }
                            //娌℃湁閫変腑鍥惧厓鏃讹紝榧犳爣鍦ㄥ浘鍏冨尯鍩熺Щ鍔?					

                            this.mouseAreaControl.PicturePanel.OnMoveOver(graph1 as SvgElement, e);

                            return;
                        }
                    }
                }
            }

			Label_05AD:
				if(graphTemp!=null) //没有选中图元时，鼠标移出图元区域
				{
					//if(this.mouseAreaControl.Cursor == SpecialCursors.DragInfoCursor)
					this.mouseAreaControl.PicturePanel.OnMoveOut(graphTemp as SvgElement,e);
					graphTemp=null;
					//					graphTemp2=null;
				}
			this.CurrentMousePoint = MousePoint.None;
		}

		public void OnMouseUp(MouseEventArgs e)
		{
			ToolOperation operation1 = this.mouseAreaControl.CurrentOperation;
			if (e.Button != MouseButtons.None)
			{
				if (e.Button == MouseButtons.Left )
				{
					if(operation1 ==ToolOperation.WindowZoom)
					{
						
						ItopVector.DrawArea.DrawArea area1 = this.mouseAreaControl.PicturePanel;
						PointF tf13 = this.mouseAreaControl.PicturePanel.PointToSystem(startpoint);
						if((int)tf13.X==e.X && (int)tf13.Y==e.Y){return;}
						PointF Vstartpoint=this.startpoint;//this.mouseAreaControl.PicturePanel.PointToView(startpoint);
						PointF Vendpoint=this.mouseAreaControl.PicturePanel.PointToView(new PointF(e.X,e.Y));
						float a1=820/Math.Abs(tf13.X-e.X);
						float a2=570/Math.Abs(tf13.Y-e.Y);	
						//float a1=(area1.DocumentSizeEx.Width/Math.Abs(Vstartpoint.X-Vendpoint.X));
						//						float a2=(area1.DocumentSizeEx.Height/Math.Abs(Vstartpoint.Y-Vendpoint.Y));	
						//						area1.VirtualLeft=(startpoint.X+e.X)/2;
						//						area1.VirtualTop=(startpoint.Y+e.Y)/2;
						area1.VirtualLeft=(Vstartpoint.X+Vendpoint.X)/2;
						area1.VirtualTop=(Vstartpoint.Y+Vendpoint.Y)/2;
						float scale=area1.ScaleUnit;
						float vLeft=area1.VirtualLeft;
						float vTop=area1.VirtualTop;
						float XMove=vLeft-(tf13.X+e.X)/2;
						float YMove=vTop-(tf13.Y+e.Y)/2;
						float mx=((tf13.X+e.X)/2)/(area1.hScrollBar1.Maximum);
						float my=((tf13.Y+e.Y)/2)/(area1.vScrollBar1.Maximum);
						if(a1<a2)
						{
							area1.ScaleUnit *= a1;
							area1.VirtualLeft=area1.hScrollBar1.Maximum*mx;// +20*(area1.ScaleUnit-1);//+Math.Abs(Math.Abs(Vstartpoint.X)-Math.Abs(Vendpoint.X))/2;
							area1.VirtualTop=area1.vScrollBar1.Maximum*my+330;//+330*(area1.ScaleUnit-1);//+Math.Abs(Math.Abs(Vstartpoint.Y)-Math.Abs(Vendpoint.Y))/2;
							
						}
						else
						{
							area1.ScaleUnit *= a2;
							area1.VirtualLeft=area1.hScrollBar1.Maximum*mx;// +20*(area1.ScaleUnit-1);//+Math.Abs(Math.Abs(Vstartpoint.X)-Math.Abs(Vendpoint.X))/2;
							area1.VirtualTop=area1.vScrollBar1.Maximum*my+330;//+330*(area1.ScaleUnit-1);//+Math.Abs(Math.Abs(Vstartpoint.Y)-Math.Abs(Vendpoint.Y))/2;
						
						}
						
						goto Lable_MoveIn;
					}
					if (operation1 == ToolOperation.AreaSelect)
					{
						this.AreaPoints.Add(new PointF((float) e.X, (float) e.Y));
						PointF[] tfArray1 = new PointF[this.AreaPoints.Count];
						this.AreaPoints.CopyTo(tfArray1, 0);
						this.AreaPoints.Clear();
						Matrix matrix1 = new Matrix();
						this.selectAreaPath = new GraphicsPath();
						this.selectAreaPath.AddLines(tfArray1);
						
						this.selectAreaPath.CloseFigure();
						Region region1 = new Region(this.selectAreaPath);
						this.mouseAreaControl.SVGDocument.ClearSelects();
						
						SvgElementCollection collection1 =new SvgElementCollection();
						if (freeSelect)
						{
							foreach(Layer layer in this.mouseAreaControl.SVGDocument.Layers)
							{
								if(layer.Visible)
								{
									collection1.AddRange(layer.GraphList);
								}
							}
						}
						else
						{
							collection1 = this.mouseAreaControl.SVGDocument.CurrentLayer.GraphList;
						}


						SvgElementCollection.ISvgElementEnumerator enumerator1 = collection1.GetEnumerator();
						
						while (enumerator1.MoveNext())
						{
							IGraph graph1 = (IGraph) enumerator1.Current;						
							
							GraphicsPath path1 = (GraphicsPath) graph1.GPath.Clone();
							path1.Transform(graph1.GraphTransform.Matrix);
							RectangleF ef1 = PathFunc.GetBounds(path1);
							if ((region1.IsVisible(ef1/*new System.Drawing.Rectangle((int) ef1.X, (int) ef1.Y, (int) ef1.Width, (int) ef1.Height)*/) && !graph1.IsLock) && (graph1.DrawVisible /*&& (AnimFunc.GetKeyIndex((SvgElement) graph1, this.mouseAreaControl.SVGDocument.ControlTime, true) >= 0)*/))
							{
								this.mouseAreaControl.SVGDocument.AddSelectElement(graph1);
							}						
						}

						GraphicsPath path2 = new GraphicsPath();
						path2.AddLines(tfArray1);
						RectangleF ef2 = path2.GetBounds();
						this.mouseAreaControl.Invalidate(new System.Drawing.Rectangle(((int) ef2.X) - 10, ((int) ef2.Y) - 10, ((int) ef2.Width) + 20, ((int) ef2.Height) + 20));
						return;
					}
					if (this.currentMousePoint == MousePoint.CenterPoint && (((XmlElement)selectCollection[0]).GetAttribute("layer").ToString()==SvgDocument.currentLayer||freeSelect))
					{
						PointF tf1 = this.mouseAreaControl.PicturePanel.PointToView(this.mouseAreaControl.CenterPoint);
						PointF tf2 = this.mouseAreaControl.CenterPoint;
						this.mouseAreaControl.PicturePanel.Invalidate(new System.Drawing.Rectangle(((int) tf2.X) - 6, ((int) tf2.Y) - 6, 12, 12));
						SizeF ef3 = this.mouseAreaControl.PicturePanel.GridSize;
						float single1 = ef3.Height;
						float single2 = ef3.Width;
						PointF tf3 = startpoint;//this.mouseAreaControl.PicturePanel.PointToView(this.);
						PointF tf4 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
						if (this.mouseAreaControl.PicturePanel.SnapToGrid)
						{
							int num1 = (int) ((tf3.X + (single2 / 2f)) / single2);
							int num2 = (int) ((tf3.Y + (single1 / 2f)) / single1);
							tf3 = (PointF) new Point((int) (num1 * single2), (int) (num2 * single1));
							num1 = (int) ((tf4.X + (single2 / 2f)) / single2);
							num2 = (int) ((tf4.Y + (single1 / 2f)) / single1);
							tf4 = (PointF) new Point((int) (num1 * single2), (int) (single1 * num2));
						}
						tf3 = this.mouseAreaControl.PicturePanel.PointToSystem(tf3);
						tf4 = this.mouseAreaControl.PicturePanel.PointToSystem(tf4);
						this.mouseAreaControl.PicturePanel.CenterPoint = tf4;
					}
					else if ((this.currentMousePoint != MousePoint.None) && (this.toBeSelectedPath != null))
					{
						if ((((Control.ModifierKeys != Keys.Control) && (Control.ModifierKeys != Keys.Shift) && (this.toBeSelectedGraph != null)) && (!this.mouseAreaControl.SVGDocument.SelectCollection.Contains(this.toBeSelectedGraph) /*&& !this.toBeSelectedGraph.IsLock*/)) && this.toBeSelectedGraph.DrawVisible)// && ((XmlElement)selectCollection[0]).GetAttribute("layer").ToString()==SvgDocument.currentLayer)
						{
							this.mouseAreaControl.SVGDocument.ClearSelects();
							this.mouseAreaControl.SVGDocument.AddSelectElement(this.toBeSelectedGraph);
							
						}
						if(mouseAreaControl.CanEdit==false || (((XmlElement)selectCollection[0]).GetAttribute("layer").ToString()!=SvgDocument.currentLayer&&!freeSelect)||(this.toBeSelectedGraph!=null && this.toBeSelectedGraph.IsLock))
						{
							goto Lable_MoveIn;
						}
						this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
						this.win32.W32SetROP2(7);
						this.win32.W32PolyDraw(this.reversePath);
						this.win32.ReleaseDC();
						PointF tf5 = this.mouseAreaControl.PicturePanel.PointToView(this.mouseAreaControl.CenterPoint);
						float single3 = 0f;
						float single4 = 0f;
						PointF tf6 =startpoint;// this.mouseAreaControl.PicturePanel.PointToView(this.);
						PointF tf7 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
						SizeF ef4 = this.mouseAreaControl.PicturePanel.GridSize;
						float single5 = ef4.Height;
						float single6 = ef4.Width;
						if (this.mouseAreaControl.PicturePanel.SnapToGrid)
						{
							int num3 = (int) ((tf6.X + (single6 / 2f)) / single6);
							int num4 = (int) ((tf6.Y + (single5 / 2f)) / single5);
							tf6 = (PointF) new Point((int) (num3 * single6), (int) (num4 * single5));
							num3 = (int) ((tf7.X + (single6 / 2f)) / single6);
							num4 = (int) ((tf7.Y + (single5 / 2f)) / single5);
							tf7 = (PointF) new Point((int) (num3 * single6), (int) (single5 * num4));
						}
						PointF tf8 = this.mouseAreaControl.PicturePanel.PointToSystem(tf6);
						PointF tf9 = this.mouseAreaControl.PicturePanel.PointToSystem(tf7);
						PointF tf10 = tf6;
						PointF tf11 = tf7;
						PointF tf12 = tf5;
						SvgDocument document1 = this.mouseAreaControl.SVGDocument;

						bool flag1 = this.mouseAreaControl.PicturePanel.SVGDocument.AcceptChanges;
						this.mouseAreaControl.PicturePanel.SVGDocument.AcceptChanges = true;
						SvgDocument document2 = this.mouseAreaControl.SVGDocument;
						document2.NumberOfUndoOperations += (this.mouseAreaControl.SVGDocument.SelectCollection.Count + 0x7d0);
						SvgElementCollection.ISvgElementEnumerator enumerator2 = this.mouseAreaControl.SVGDocument.SelectCollection.GetEnumerator();
						while (enumerator2.MoveNext())
						{
							float single10;
							SvgElement element1 = (SvgElement) enumerator2.Current;
							if (!(element1 is IGraph))
							{
								continue;
							}
							if (element1 is Use && (element1 as Use).LimitSize && currentMousePoint!=MousePoint.Translate)continue;
							Matrix matrix2 = ((IGraph) element1).GraphTransform.Matrix.Clone();
							Matrix matrix3 = this.mouseAreaControl.PicturePanel.CoordTransform.Clone();
							matrix3.Invert();
							matrix2.Multiply(matrix3, MatrixOrder.Append);
							PointF tf13 = PointF.Empty;
							GraphicsPath path3 = (GraphicsPath) ((IGraph) element1).GPath.Clone();
							RectangleF ef5 = PathFunc.GetBounds(path3);
							PointF[] tfArray6 = new PointF[9] { new PointF(ef5.X, ef5.Y), new PointF(ef5.X, ef5.Y + (ef5.Height / 2f)), new PointF(ef5.X, ef5.Bottom), new PointF(ef5.X + (ef5.Width / 2f), ef5.Y), new PointF(ef5.Right, ef5.Y), new PointF(ef5.Right, ef5.Y + (ef5.Height / 2f)), new PointF(ef5.Right, ef5.Bottom), new PointF(ef5.X + (ef5.Width / 2f), ef5.Bottom), new PointF(ef5.X + (ef5.Width / 2f), ef5.Y + (ef5.Height / 2f)) } ;
							PointF[] tfArray2 = tfArray6;
							Matrix matrix4 = matrix2.Clone();
							if(matrix4.IsInvertible)
								matrix4.Invert();
							PointF[] tfArray7 = new PointF[3] { tf10, tf11, tf12 } ;
							PointF[] tfArray3 = tfArray7;
							if(this.currentMousePoint!=MousePoint.Rotate)
							{
								matrix4.TransformPoints(tfArray3);
							}
							tf6 = tfArray3[0];
							tf7 = tfArray3[1];
							tf5 = tfArray3[2];
							single3 = tf7.X - tf6.X;
							single4 = tf7.Y - tf6.Y;
							Matrix matrix5 = new Matrix();
							PointF tf23 = this.mouseAreaControl.PicturePanel.PointToSystem(startpoint);

							if ((Math.Abs((float) (e.X - tf23.X)) <= 1f) && (Math.Abs((float) (e.Y - tf23.Y)) <= 1f))
							{
								return;
							}
							switch (this.currentMousePoint)
							{
								case MousePoint.ScaleTopLeft:
								case MousePoint.ScaleTopMiddle:
								case MousePoint.ScaleTopRight:
								case MousePoint.ScaleMiddleLeft:
								case MousePoint.ScaleMiddleRight:
								case MousePoint.ScaleBottomLeft:
								case MousePoint.ScaleBottomMiddle:
								case MousePoint.ScaleBottomRight:
								{
									if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
									{
										goto Label_101C;
									}
									if (this.currentMousePoint != MousePoint.ScaleTopLeft)
									{
										goto Label_0F60;
									}
									tf13 = tfArray2[6];
									goto Label_1020;
								}
								case MousePoint.ScaleFromCenter:
								case MousePoint.SkewXFromCenter:
								case MousePoint.SkewYFromCenter:
								case MousePoint.RotateFromCenter:
								{
									continue;
								}
								case MousePoint.SkewXTop:
								case MousePoint.SkewXBottom:
								case MousePoint.SkewYLeft:
								case MousePoint.SkewYRight:
								{
									if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
									{
										goto Label_16F2;
									}
									if (this.currentMousePoint != MousePoint.SkewXBottom)
									{
										goto Label_16A1;
									}
									tf13 = tfArray2[0];
									goto Label_16F6;
								}
								case MousePoint.Rotate:
								{
									if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
									{
										goto Label_1EAB;
									}
									tf13 = tfArray2[this.rotateindex];
									goto Label_1EAF;
								}
								case MousePoint.Translate:
								{
									goto Label_09B7;
								}
								default:
								{
									continue;
								}
							}
						Label_09B7:
							if (((element1.InfoList.Count == 1) && (document1.ControlTime == 0)) || !document1.RecordAnim)
							{
								Matrix matrix6 = ((IGraph) element1).Transform.Matrix.Clone();
								if (element1.SvgAttributes.ContainsKey("transform"))
								{
									Matrix matrix7 = ((Matrix) element1.SvgAttributes["transform"]).Clone();
									matrix7.Invert();
									matrix6.Multiply(matrix7, MatrixOrder.Append);
								}
								matrix5.Reset();
								matrix5.Translate(single3, single4);
								Matrix matrix8 = ((IGraph) element1).Transform.Matrix.Clone();
								matrix8.Multiply(matrix5);
								matrix6.Invert();
								matrix6.Multiply(matrix8, MatrixOrder.Append);
								//								string[] textArray1 = new string[13];
								//								textArray1[0] = "matrix(";
								//								double num13 = Math.Round((double) matrix6.Elements[0], 4);
								//								textArray1[1] = num13.ToString();
								//								textArray1[2] = ",";
								//								double num14 = Math.Round((double) matrix6.Elements[1], 4);
								//								textArray1[3] = num14.ToString();
								//								textArray1[4] = ",";
								//								double num15 = Math.Round((double) matrix6.Elements[2], 4);
								//								textArray1[5] = num15.ToString();
								//								textArray1[6] = ",";
								//								double num16 = Math.Round((double) matrix6.Elements[3], 4);
								//								textArray1[7] = num16.ToString();
								//								textArray1[8] = ",";
								//								double num17 = Math.Round((double) matrix6.Elements[4], 4);
								//								textArray1[9] = num17.ToString();
								//								textArray1[10] = ",";
								//								double num18 = Math.Round((double) matrix6.Elements[5], 4);
								//								textArray1[11] = num18.ToString();
								//								textArray1[12] = ")";
								//								string text1 = string.Concat(textArray1);
								//								AttributeFunc.SetAttributeValue(element1, "transform", text1);
								(element1 as IGraph).Transform= new ItopVector.Core.Types.Transf(matrix6);
								this.mouseAreaControl.PicturePanel.InvalidateElement(element1);
                                if (element1 is Use)
                                {
                                    MoveEventArgs moveElement = new MoveEventArgs(element1, tf6, tf7);
                                    if (OnElementMove != null)
                                    {
                                        OnElementMove(element1, moveElement);
                                    }
                                }
							}
                           
							continue;
						Label_0F60:
							if (this.currentMousePoint == MousePoint.ScaleTopRight)
							{
								tf13 = tfArray2[2];
								goto Label_1020;
							}
							if (this.currentMousePoint == MousePoint.ScaleBottomLeft)
							{
								tf13 = tfArray2[4];
								goto Label_1020;
							}
							if (this.currentMousePoint == MousePoint.ScaleBottomRight)
							{
								tf13 = tfArray2[0];
								goto Label_1020;
							}
							if (this.currentMousePoint == MousePoint.ScaleMiddleLeft)
							{
								tf13 = tfArray2[5];
								goto Label_1020;
							}
							if (this.currentMousePoint == MousePoint.ScaleMiddleRight)
							{
								tf13 = tfArray2[1];
								goto Label_1020;
							}
							if (this.currentMousePoint == MousePoint.ScaleTopMiddle)
							{
								tf13 = tfArray2[7];
								goto Label_1020;
							}
							if (this.currentMousePoint == MousePoint.ScaleBottomMiddle)
							{
								tf13 = tfArray2[3];
							}
							goto Label_1020;
						Label_101C:
							tf13 = tf5;
						Label_1020:
							single3 = (tf7.X - tf13.X) / (tf6.X - tf13.X);
							single4 = (tf7.Y - tf13.Y) / (tf6.Y - tf13.Y);
							if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
							{
								single3 = single4 = Math.Min(single3, single4);
							}
							if ((this.currentMousePoint == MousePoint.ScaleMiddleLeft) || (this.currentMousePoint == MousePoint.ScaleMiddleRight))
							{
								single4 = 1f;
							}
							else if ((this.currentMousePoint == MousePoint.ScaleTopMiddle) || (this.currentMousePoint == MousePoint.ScaleBottomMiddle))
							{
								single3 = 1f;
							}
							if (((element1.InfoList.Count == 1) && (document1.ControlTime == 0)) )
							{
								Matrix matrix12 = ((IGraph) element1).Transform.Matrix.Clone();
								if (element1.SvgAttributes.ContainsKey("transform"))
								{
									Matrix matrix13 = ((Matrix) element1.SvgAttributes["transform"]).Clone();
									if(matrix13.IsInvertible)
										matrix13.Invert();
									matrix12.Multiply(matrix13, MatrixOrder.Append);
								}
								matrix5.Reset();
								matrix5.Translate(tf13.X, tf13.Y, MatrixOrder.Prepend);
								matrix5.Scale(single3, single4, MatrixOrder.Prepend);
								matrix5.Translate(-tf13.X, -tf13.Y, MatrixOrder.Prepend);
								Matrix matrix14 = ((IGraph) element1).Transform.Matrix.Clone();
								matrix14.Multiply(matrix5);
								if(matrix12.IsInvertible)
									matrix12.Invert();
								matrix12.Multiply(matrix14, MatrixOrder.Append);
								string[] textArray2 = new string[13];
								textArray2[0] = "matrix(";
								double num19 = Math.Round((double) matrix12.Elements[0], 2);
								textArray2[1] = num19.ToString();
								textArray2[2] = ",";
								double num20 = Math.Round((double) matrix12.Elements[1], 2);
								textArray2[3] = num20.ToString();
								textArray2[4] = ",";
								double num21 = Math.Round((double) matrix12.Elements[2], 2);
								textArray2[5] = num21.ToString();
								textArray2[6] = ",";
								double num22 = Math.Round((double) matrix12.Elements[3], 2);
								textArray2[7] = num22.ToString();
								textArray2[8] = ",";
								double num23 = Math.Round((double) matrix12.Elements[4], 2);
								textArray2[9] = num23.ToString();
								textArray2[10] = ",";
								double num24 = Math.Round((double) matrix12.Elements[5], 2);
								textArray2[11] = num24.ToString();
								textArray2[12] = ")";
								string text2 = string.Concat(textArray2);
								AttributeFunc.SetAttributeValue(element1, "transform", text2);
								this.mouseAreaControl.PicturePanel.InvalidateElement(element1);
							}                            
							continue;
						Label_16A1:
							if (this.currentMousePoint == MousePoint.SkewXTop)
							{
								tf13 = tfArray2[6];
								goto Label_16F6;
							}
							if (this.currentMousePoint == MousePoint.SkewYLeft)
							{
								tf13 = tfArray2[4];
								goto Label_16F6;
							}
							if (this.currentMousePoint == MousePoint.SkewYRight)
							{
								tf13 = tfArray2[0];
							}
							goto Label_16F6;
						Label_16F2:
							tf13 = tf5;
						Label_16F6:
							single3 /= (tfArray2[6].Y - tfArray2[4].Y);
							single4 /= (tfArray2[6].X - tfArray2[2].X);
							if ((this.currentMousePoint == MousePoint.SkewXTop) || (this.currentMousePoint == MousePoint.SkewXBottom))
							{
								single4 = 0f;
								if (this.currentMousePoint == MousePoint.SkewXTop)
								{
									single3 = -single3;
								}
							}
							else if ((this.currentMousePoint == MousePoint.SkewYLeft) || (this.currentMousePoint == MousePoint.SkewYRight))
							{
								single3 = 0f;
								if (this.currentMousePoint == MousePoint.SkewYLeft)
								{
									single4 = -single4;
								}
							}
							single3 = (single3 == 0f) ? 0f : ((single3 / Math.Abs(single3)) * Math.Min(Math.Abs(single3), (float) 3f));
							single4 = (single4 == 0f) ? 0f : ((single4 / Math.Abs(single4)) * Math.Min(Math.Abs(single4), (float) 3f));
							float single8 = (float) ((Math.Atan((double) single3) / 3.1415926535897931) * 180);
							float single9 = (float) ((Math.Atan((double) single4) / 3.1415926535897931) * 180);
							matrix5.Translate(tf13.X, tf13.Y, MatrixOrder.Prepend);
							matrix5.Shear(single3, single4, MatrixOrder.Prepend);
							matrix5.Translate(-tf13.X, -tf13.Y, MatrixOrder.Prepend);
							if (((element1.InfoList.Count == 1) && (document1.ControlTime == 0)))
							{
								Matrix matrix15 = ((IGraph) element1).Transform.Matrix.Clone();
								if (element1.SvgAttributes.ContainsKey("transform"))
								{
									Matrix matrix16 = ((Matrix) element1.SvgAttributes["transform"]).Clone();
									matrix16.Invert();
									matrix15.Multiply(matrix16, MatrixOrder.Append);
								}
								matrix5.Reset();
								single8 = (float) ((Math.Atan((double) single3) / 3.1415926535897931) * 180);
								single9 = (float) ((Math.Atan((double) single4) / 3.1415926535897931) * 180);
								matrix5.Translate(tf13.X, tf13.Y, MatrixOrder.Prepend);
								matrix5.Shear(single3, single4, MatrixOrder.Prepend);
								matrix5.Translate(-tf13.X, -tf13.Y, MatrixOrder.Prepend);
								Matrix matrix17 = ((IGraph) element1).Transform.Matrix.Clone();
								matrix17.Multiply(matrix5);
								matrix15.Invert();
								matrix15.Multiply(matrix17, MatrixOrder.Append);
								string[] textArray3 = new string[13];
								textArray3[0] = "matrix(";
								double num25 = Math.Round((double) matrix15.Elements[0], 2);
								textArray3[1] = num25.ToString();
								textArray3[2] = ",";
								double num26 = Math.Round((double) matrix15.Elements[1], 2);
								textArray3[3] = num26.ToString();
								textArray3[4] = ",";
								double num27 = Math.Round((double) matrix15.Elements[2], 2);
								textArray3[5] = num27.ToString();
								textArray3[6] = ",";
								double num28 = Math.Round((double) matrix15.Elements[3], 2);
								textArray3[7] = num28.ToString();
								textArray3[8] = ",";
								double num29 = Math.Round((double) matrix15.Elements[4], 2);
								textArray3[9] = num29.ToString();
								textArray3[10] = ",";
								double num30 = Math.Round((double) matrix15.Elements[5], 2);
								textArray3[11] = num30.ToString();
								textArray3[12] = ")";
								string text3 = string.Concat(textArray3);
								AttributeFunc.SetAttributeValue(element1, "transform", text3);
								this.mouseAreaControl.PicturePanel.InvalidateElement(element1);
							}                            
							continue;
						Label_1EAB:
							tf13 = tf5;
						Label_1EAF:
							single10 = 0f;
							float single11 = (float) Math.Atan((double) ((tf6.Y - tf13.Y) / (tf6.X - tf13.X)));
							float single12 = (float) Math.Atan((double) ((tf7.Y - tf13.Y) / (tf7.X - tf13.X)));
							single10 = ((float) (((double) (single12 - single11)) / 3.1415926535897931)) * 180f;
							if (((tf7.X - tf13.X) * (tf6.X - tf13.X)) < 0f)
							{
								single10 += 180f;
							}
							if (((Control.ModifierKeys & Keys.Shift) == Keys.Shift) && (single10 != 0f))
							{
								single10 = (single10 / Math.Abs(single10)) * ((((int) (Math.Abs(single10) + 23f)) / 0x2d) * 0x2d);
							}
							if (single10 < 0f)
							{
								single10 += 360f;
							}
							if (((element1.InfoList.Count == 1) && (document1.ControlTime == 0)) || !document1.RecordAnim)
							{
								using(Matrix matrix18 = ((IGraph) element1).Transform.Matrix.Clone())
								{
									if (element1.SvgAttributes.ContainsKey("transform"))
									{
										using(Matrix matrix19 = ((Matrix) element1.SvgAttributes["transform"]).Clone())
										{
											matrix19.Invert();
											matrix18.Multiply(matrix19, MatrixOrder.Append);
										}
									}
									matrix5.Reset();
									matrix5.RotateAt(single10, tf13);
									using(Matrix matrix20 = ((IGraph) element1).Transform.Matrix.Clone())
									{
										if(this.currentMousePoint==MousePoint.Rotate)
										{
											matrix20.Multiply(matrix5,MatrixOrder.Append);
										}
										else
										{
											matrix20.Multiply(matrix5);
										}
										matrix18.Invert();
										matrix18.Multiply(matrix20, MatrixOrder.Append);
									}
									string[] textArray4 = new string[13];
									textArray4[0] = "matrix(";
									double num31 = Math.Round((double) matrix18.Elements[0], 2);
									textArray4[1] = num31.ToString();
									textArray4[2] = ",";
									double num32 = Math.Round((double) matrix18.Elements[1], 2);
									textArray4[3] = num32.ToString();
									textArray4[4] = ",";
									double num33 = Math.Round((double) matrix18.Elements[2], 2);
									textArray4[5] = num33.ToString();
									textArray4[6] = ",";
									double num34 = Math.Round((double) matrix18.Elements[3], 2);
									textArray4[7] = num34.ToString();
									textArray4[8] = ",";
									double num35 = Math.Round((double) matrix18.Elements[4], 2);
									textArray4[9] = num35.ToString();
									textArray4[10] = ",";
									double num36 = Math.Round((double) matrix18.Elements[5], 2);
									textArray4[11] = num36.ToString();
									textArray4[12] = ")";
									string text4 = string.Concat(textArray4);
									AttributeFunc.SetAttributeValue(element1, "transform", text4);
									this.mouseAreaControl.PicturePanel.InvalidateElement(element1);
								}
							}
                            
						}
						this.mouseAreaControl.SVGDocument.NotifyUndo();
						this.mouseAreaControl.SVGDocument.AcceptChanges = flag1;
						if (this.currentMousePoint == MousePoint.Translate)
						{
							Matrix matrix21 = new Matrix();
							matrix21.Translate(tf9.X - tf8.X, tf9.Y - tf8.Y);
							PointF[] tfArray10 = new PointF[1] { this.mouseAreaControl.CenterPoint } ;
							PointF[] tfArray5 = tfArray10;
							matrix21.TransformPoints(tfArray5);
							this.mouseAreaControl.CenterPoint = tfArray5[0];
						}
					}
					else if (this.mouseAreaControl.CanEdit&&(this.currentMousePoint == MousePoint.None) && (OperationFunc.IsSelectOperation(operation1) || OperationFunc.IsTransformOperation(operation1)))
					{
						//框选择					
						this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
						this.win32.W32SetROP2(7);
						this.win32.W32PolyDraw(this.reversePath);
						this.win32.ReleaseDC();
						PointF tf14 = new PointF((float) e.X, (float) e.Y);
						PointF tf15 =startpoint;// this.mouseAreaControl.PicturePanel.PointToView(this.);
						PointF tf16 = this.mouseAreaControl.PicturePanel.PointToView(tf14);
						float single13 = this.mouseAreaControl.PicturePanel.ScaleUnit;
						float single14 = Math.Min(tf15.X, tf16.X);
						float single15 = Math.Min(tf15.Y, tf16.Y);
						float single16 = Math.Max(tf15.X, tf16.X);
						float single17 = Math.Max(tf15.Y, tf16.Y);
						this.selectAreaPath = new GraphicsPath();
						this.selectAreaPath.AddRectangle(new RectangleF(single14, single15, single16 - single14, single17 - single15));
						Matrix matrix22 = this.mouseAreaControl.PicturePanel.CoordTransform.Clone();
						this.selectAreaPath.Transform(matrix22);
						RectangleF ef6 = PathFunc.GetBounds(this.selectAreaPath);
					
						this.mouseAreaControl.SVGDocument.ClearSelects();
						SvgElementCollection collection1 =null;// this.mouseAreaControl.PicturePanel.ElementList;

						
						if ( !freeSelect)
						{
							ILayer layer1 = this.mouseAreaControl.SVGDocument.Layers[SvgDocument.currentLayer] as ILayer;
							if(layer1 ==null)return;
							collection1 = layer1.GraphList; // 
						}
						else
						{
							collection1 =this.mouseAreaControl.PicturePanel.ElementList;
						}
						if (collection1 == null)
						{
							return;
						}
						int num11 = 0;
						SvgElementCollection collection2 = new SvgElementCollection();
						SvgElementCollection.ISvgElementEnumerator enumerator3 = collection1.GetEnumerator();
						while (enumerator3.MoveNext())
						{
							IGraph graph2 = (IGraph) enumerator3.Current;
							if ((graph2.CanSelect&&!graph2.IsLock && graph2.DrawVisible) && graph2.Visible )
							{
								GraphicsPath path4 = (GraphicsPath) graph2.GPath.Clone();
								path4.Transform(graph2.GraphTransform.Matrix);
								RectangleF ef7 = PathFunc.GetBounds(path4);
								if (ef6.Contains(ef7))
								{
									//int num12 = AnimFunc.GetKeyIndex((SvgElement) graph2, this.mouseAreaControl.SVGDocument.ControlTime, true);
									collection2.Add((SvgElement) graph2);
									num11++;
								}
							}
						}
						if (collection2.Count > 0)
						{
							this.mouseAreaControl.SVGDocument.SelectCollection.AddRange(collection2);
						}
						else
						{
							this.mouseAreaControl.SVGDocument.CurrentElement = (SvgElement) this.mouseAreaControl.SVGDocument.DocumentElement;
						}
					}
					else if(OperationFunc.IsConnectLineOperation(operation1) )
					{
						string type="";
						
						if(MouseArea.ConnectLinePoints==2)
						{
							switch  (operation1)
							{
								case ToolOperation.ConnectLine_Line:
									type="line";
									break;
								case ToolOperation.ConnectLine_Polyline:
									type="polyline";
									break;
								case ToolOperation.ConnectLine_Rightangle:
									type="rightangle";
									break;
								case ToolOperation.ConnectLine_Spline:
									type="spline";
									break;
							}
							XmlElement n1=this.mouseAreaControl.SVGDocument.CreateElement("tonli","connectline","http://www.tonli.com/tonli");
							n1.SetAttribute("x1",MouseArea.ConLineStartPoint.X.ToString());
							n1.SetAttribute("y1",MouseArea.ConLineStartPoint.Y.ToString());
							n1.SetAttribute("x2",MouseArea.ConLineEndPoint.X.ToString());
							n1.SetAttribute("y2",MouseArea.ConLineEndPoint.Y.ToString());
							n1.SetAttribute("start","#"+MouseArea.SvgStartObj.ID);
							n1.SetAttribute("end","#"+MouseArea.SvgEndObj.ID);
							n1.SetAttribute("type",type);
							n1.SetAttribute("layer",SvgDocument.currentLayer);
							this.mouseAreaControl.SVGDocument.RootElement.AppendChild(n1);
							MouseArea.ConnectLinePoints=0;
							ToolOperation oper=this.mouseAreaControl.CurrentOperation;
							this.mouseAreaControl.CurrentOperation=ToolOperation.Select;
							this.mouseAreaControl.CurrentOperation=oper;							
						}
					}
				}//鼠标左键抬起
			Lable_MoveIn:
				this.toBeSelectedGraph = null;
				this.toBeSelectedPath = null;
				this.reversePath.Reset();
				this.CurrentMousePoint = this.currentMousePoint;

			}
		}

		public void OnPaint(PaintEventArgs e)
		{
			this.reversePath.Reset();
			if (this.AreaPoints.Count > 1)
			{
				PointF[] tfArray1 = new PointF[this.AreaPoints.Count];
				this.AreaPoints.CopyTo(tfArray1, 0);
				e.Graphics.DrawLines(Pens.Blue, tfArray1);
			}
			e.Graphics.FillPath(Brushes.Green, this.rotatepath);
		}

		public bool ProcessDialogKey(Keys keydate)
		{
			if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
			{
				return true;
			}
			if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
			{
				return true;
			}
			return false;
		}

		public bool Redo()
		{
			return false;
		}

		private void SelectSameBrush()
		{
			this.mouseAreaControl.SVGDocument.ClearSelects();
			SvgElementCollection.ISvgElementEnumerator enumerator1 = this.mouseAreaControl.SVGDocument.FlowChilds.GetEnumerator();
			while (enumerator1.MoveNext())
			{
				SvgElement element1 = (SvgElement) enumerator1.Current;
				if (element1 is IGraph)
				{
					IGraph graph1 = (IGraph) element1;
				}
			}
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

		private MousePoint CurrentMousePoint
		{
			set
			{
				this.currentMousePoint = value;
				string text1 = string.Empty;
				switch (value)
				{
					case MousePoint.ScaleTopLeft:
					case MousePoint.ScaleBottomRight:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.TopLeftScaleCursor;
						if ((Control.ModifierKeys & (Keys.Control | Keys.Shift)) != (Keys.Control | Keys.Shift))
						{
							if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
							{
								if (!this.tooltips.ContainsKey("ctrlanglescale"))
								{
									this.tooltips.Add("ctrlanglescale", DrawAreaConfig.GetLabelForName("ctrlanglescale"));
								}
								this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["ctrlanglescale"], 1);
								return;
							}
							if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
							{
								if (!this.tooltips.ContainsKey("shiftanglescale"))
								{
									this.tooltips.Add("shiftanglescale", DrawAreaConfig.GetLabelForName("shiftanglescale"));
								}
								this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["shiftanglescale"], 1);
								return;
							}
							if (!this.tooltips.ContainsKey("anglescale"))
							{
								this.tooltips.Add("anglescale", DrawAreaConfig.GetLabelForName("anglescale"));
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["anglescale"], 1);
							return;
						}
						if (!this.tooltips.ContainsKey("ctrlshiftanglescale"))
						{
							this.tooltips.Add("ctrlshiftanglescale", DrawAreaConfig.GetLabelForName("ctrlshiftanglescale"));
						}
						this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["ctrlshiftanglescale"], 1);
						return;
					}
					case MousePoint.ScaleTopMiddle:
					case MousePoint.ScaleBottomMiddle:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.HScaleCursor;
						if ((Control.ModifierKeys & (Keys.Control | Keys.Shift)) != (Keys.Control | Keys.Shift))
						{
							if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
							{
								if (!this.tooltips.ContainsKey("ctrlvertscale"))
								{
									this.tooltips.Add("ctrlvertscale", DrawAreaConfig.GetLabelForName("ctrlvertscale"));
								}
								this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["ctrlvertscale"], 1);
								return;
							}
							if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
							{
								if (!this.tooltips.ContainsKey("shiftvertscale"))
								{
									this.tooltips.Add("shiftvertscale", DrawAreaConfig.GetLabelForName("shiftvertscale"));
								}
								this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["shiftvertscale"], 1);
								return;
							}
							if (!this.tooltips.ContainsKey("vertscale"))
							{
								this.tooltips.Add("vertscale", DrawAreaConfig.GetLabelForName("vertscale"));
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["vertscale"], 1);
							return;
						}
						if (!this.tooltips.ContainsKey("ctrlshiftvertscale"))
						{
							this.tooltips.Add("ctrlshiftvertscale", DrawAreaConfig.GetLabelForName("ctrlshiftvertscale"));
						}
						this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["ctrlshiftvertscale"], 1);
						return;
					}
					case MousePoint.ScaleTopRight:
					case MousePoint.ScaleBottomLeft:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.TopRightScaleCursor;
						if ((Control.ModifierKeys & (Keys.Control | Keys.Shift)) != (Keys.Control | Keys.Shift))
						{
							if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
							{
								if (!this.tooltips.ContainsKey("ctrlanglescale"))
								{
									this.tooltips.Add("ctrlanglescale", DrawAreaConfig.GetLabelForName("ctrlanglescale"));
								}
								this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["ctrlanglescale"], 1);
								return;
							}
							if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
							{
								if (!this.tooltips.ContainsKey("shiftanglescale"))
								{
									this.tooltips.Add("shiftanglescale", DrawAreaConfig.GetLabelForName("shiftanglescale"));
								}
								this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["shiftanglescale"], 1);
								return;
							}
							if (!this.tooltips.ContainsKey("anglescale"))
							{
								this.tooltips.Add("anglescale", DrawAreaConfig.GetLabelForName("anglescale"));
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["anglescale"], 1);
							return;
						}
						if (!this.tooltips.ContainsKey("ctrlshiftanglescale"))
						{
							this.tooltips.Add("ctrlshiftanglescale", DrawAreaConfig.GetLabelForName("ctrlshiftanglescale"));
						}
						this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["ctrlshiftanglescale"], 1);
						return;
					}
					case MousePoint.ScaleMiddleLeft:
					case MousePoint.ScaleMiddleRight:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.VScaleCursor;
						if ((Control.ModifierKeys & (Keys.Control | Keys.Shift)) != (Keys.Control | Keys.Shift))
						{
							if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
							{
								if (!this.tooltips.ContainsKey("ctrlhoriscale"))
								{
									this.tooltips.Add("ctrlhoriscale", DrawAreaConfig.GetLabelForName("ctrlhoriscale"));
								}
								this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["ctrlhoriscale"], 1);
								return;
							}
							if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
							{
								if (!this.tooltips.ContainsKey("shifthoriscale"))
								{
									this.tooltips.Add("shifthoriscale", DrawAreaConfig.GetLabelForName("shifthoriscale"));
								}
								this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["shifthoriscale"], 1);
								return;
							}
							if (!this.tooltips.ContainsKey("horiscale"))
							{
								this.tooltips.Add("horiscale", DrawAreaConfig.GetLabelForName("horiscale"));
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["horiscale"], 1);
							return;
						}
						if (!this.tooltips.ContainsKey("ctrlshifthoriscale"))
						{
							this.tooltips.Add("ctrlshifthoriscale", DrawAreaConfig.GetLabelForName("ctrlshifthoriscale"));
						}
						this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["ctrlshifthoriscale"], 1);
						return;
					}
					case MousePoint.ScaleFromCenter:
					case MousePoint.SkewXFromCenter:
					case MousePoint.SkewYFromCenter:
					case MousePoint.RotateFromCenter:
					case MousePoint.ShapeMovePoint:
					case MousePoint.ShapeMoveControl:
					case MousePoint.ShapeMoveLine:
					case MousePoint.Flip:
					{
						return;
					}
					case MousePoint.SkewXTop:
					case MousePoint.SkewXBottom:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.SkewXCursor;
						if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
						{
							if (!this.tooltips.ContainsKey("skew"))
							{
								this.tooltips.Add("skew", DrawAreaConfig.GetLabelForName("skew"));
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["skew"], 1);
							return;
						}
						if (!this.tooltips.ContainsKey("ctrlshew"))
						{
							this.tooltips.Add("ctrlshew", DrawAreaConfig.GetLabelForName("ctrlshew"));
						}
						this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["ctrlshew"], 1);
						return;
					}
					case MousePoint.SkewYLeft:
					case MousePoint.SkewYRight:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.SkewYCursor;
						if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
						{
							if (!this.tooltips.ContainsKey("skew"))
							{
								this.tooltips.Add("skew", DrawAreaConfig.GetLabelForName("skew"));
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["skew"], 1);
							return;
						}
						if (!this.tooltips.ContainsKey("ctrlshew"))
						{
							this.tooltips.Add("ctrlshew", DrawAreaConfig.GetLabelForName("ctrlshew"));
						}
						this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["ctrlshew"], 1);
						return;
					}
					case MousePoint.Rotate:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.RotateCursor;
						if ((Control.ModifierKeys & (Keys.Control | Keys.Shift)) != (Keys.Control | Keys.Shift))
						{
							if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
							{
								if (!this.tooltips.ContainsKey("ctrlrotate"))
								{
									this.tooltips.Add("ctrlrotate", DrawAreaConfig.GetLabelForName("ctrlrotate"));
								}
								this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["ctrlrotate"], 1);
								return;
							}
							if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
							{
								if (!this.tooltips.ContainsKey("shiftrotate"))
								{
									this.tooltips.Add("shiftrotate", DrawAreaConfig.GetLabelForName("shiftrotate"));
								}
								this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["shiftrotate"], 1);
								return;
							}
							if (!this.tooltips.ContainsKey("rotate"))
							{
								this.tooltips.Add("rotate", DrawAreaConfig.GetLabelForName("rotate"));
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["rotate"], 1);
							return;
						}
						if (!this.tooltips.ContainsKey("ctrlshiftrotate"))
						{
							this.tooltips.Add("ctrlshiftrotate", DrawAreaConfig.GetLabelForName("ctrlshiftrotate"));
						}
						this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["ctrlshiftrotate"], 1);
						return;
					}
					case MousePoint.Translate:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.moveBezierCursor;
						this.mouseAreaControl.Cursor = SpecialCursors.moveBezierCursor;
						if (!this.tooltips.ContainsKey("translate"))
						{
							this.tooltips.Add("translate", DrawAreaConfig.GetLabelForName("translate"));
						}
						this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["translate"], 1);
						return;
					}
					case MousePoint.CenterPoint:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.CenterPointCursor;
						if (!this.tooltips.ContainsKey("centerpoint"))
						{
							this.tooltips.Add("centerpoint", DrawAreaConfig.GetLabelForName("centerpoint"));
						}
						this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["centerpoint"], 1);
						return;
					}
					case MousePoint.None:
					{
						this.mouseAreaControl.Cursor = this.mouseAreaControl.DefaultCursor;
						if(!OperationFunc.IsConnectLineOperation(this.mouseAreaControl.CurrentOperation))
						{
							if (!this.tooltips.ContainsKey("select"))
							{
								this.tooltips.Add("select", DrawAreaConfig.GetLabelForName("select"));
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["select"], 1);
						}
						return;
					}
				}
			}
		}


		// Fields
		private ArrayList AreaPoints;
		private PointF controlPoint;
		private MousePoint currentMousePoint;
		private MouseArea mouseAreaControl;
		private PointF movePoint;
		private GraphicsPath oriPath;
		private PointF[] ps;
		private GraphicsPath reversePath;
		private int rotateindex;
		private GraphicsPath rotatepath;
		private GraphicsPath selectAreaPath;
		private SvgElementCollection selectCollection;
		private Matrix selectMatrix;
		private PointF startpoint;
		private IGraph toBeSelectedGraph;
		private GraphicsPath toBeSelectedPath;
		private Hashtable tooltips;
		private Matrix totalmatrix;
		private Win32 win32;
		private IGraph graphTemp;  //没有选中图元时，用来存储鼠标位置图元
		private IGraph graphTemp2; //选中图元时用来存储图元变量
		private bool freeSelect;
	}
}

