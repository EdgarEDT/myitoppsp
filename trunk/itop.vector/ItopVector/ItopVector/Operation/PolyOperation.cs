using ItopVector.Core.Interface;
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
	using System.Text;
	using ItopVector.Core.Win32;

    internal class PolyOperation : IOperation
    {
        public event PolyLineBreakEventHandler OnPolyLineBreak;
        // Methods
        public PolyOperation(MouseArea mc)
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
			this.AreaPoints=new ArrayList();
            this.mouseAreaControl = mc;
            this.win32 = mc.win32;
            this.mouseAreaControl.PicturePanel.GraphChanged += new EventHandler(this.ChangeGraph);
            this.mouseAreaControl.SVGDocument.SelectCollection.OnCollectionChangedEvent += new OnCollectionChangedEventHandler(this.ChangeSelect);
            this.mouseAreaControl.DefaultCursor = SpecialCursors.bezierCursor;
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
			this.mouseAreaControl.SVGDocument.SelectCollection.OnCollectionChangedEvent -= new OnCollectionChangedEventHandler(this.ChangeSelect);
            this.mouseAreaControl.polyOperation = null;
        }
		public void OnMouseWheel(MouseEventArgs e)
		{
			// TODO:  添加 BezierOperation.DealMouseWheel 实现
		}
		private bool isKeydown(int key)
		{			
			return User32.GetKeyState(key)>100;
		}
        public void OnMouseDown(MouseEventArgs e)
        {
            bool flag2;
            bool flag3;
            PointF tf1;
			
			
			if (e.Button == MouseButtons.Left)
			{//wlwl
				this.startpoint = this.mouseAreaControl.PicturePanel.PointToView(new PointF(e.X,e.Y));
				if(this.mouseAreaControl.CurrentOperation==ToolOperation.XPolyLine)
				{
					if(points.Length>0)
					{
						this.startpoint = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, points[0].Y));
					}
					else
					{
						this.startpoint = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
					}
				}
				if(this.mouseAreaControl.CurrentOperation==ToolOperation.YPolyLine)
				{
					if(points.Length>0)
					{
						this.startpoint = this.mouseAreaControl.PicturePanel.PointToView(new PointF(points[0].X, (float) e.Y));
					}
					else
					{
						this.startpoint = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
					}
				}
				if(this.mouseAreaControl.CurrentOperation==ToolOperation.PolyLine || 
					this.mouseAreaControl.CurrentOperation==ToolOperation.Polygon ||
					this.mouseAreaControl.CurrentOperation==ToolOperation.InterEnclosure ||
					this.mouseAreaControl.CurrentOperation==ToolOperation.Enclosure ||
					this.mouseAreaControl.CurrentOperation==ToolOperation.LeadLine ||
					this.mouseAreaControl.CurrentOperation==ToolOperation.AreaPolygon)
				{
					this.startpoint = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float) e.X, (float) e.Y));
				}
				if (this.mouseAreaControl.CurrentOperation == ToolOperation.Enclosure || this.mouseAreaControl.CurrentOperation == ToolOperation.InterEnclosure)
				{
					this.AreaPoints.Add(new PointF((float) e.X, (float) e.Y));
				}
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
					case PolyOperate.Break://线路断开
					{                        
                        if ((this.moveindex > 0) && (this.moveindex < this.points.Length - 1))
                        {
                            ArrayList list1 = new ArrayList(this.points);
                            PointF[] points2 = new PointF[this.points.Length - moveindex];

                            this.points = new PointF[moveindex + 1];
                            list1.CopyTo(0, this.points, 0, this.moveindex + 1);
                            list1.CopyTo(moveindex, points2, 0, list1.Count - moveindex);

                            Matrix matrix1 = this.graph.GraphTransform.Matrix.Clone();
                            matrix1.Invert();
                            if (points2.Length > 0)
                            {
                                matrix1.TransformPoints(points2);
                                SvgElement copyEelement = (this.graph as XmlNode).CloneNode(true) as SvgElement;
                                IGraph graph1 = this.graph;

                                copyEelement.SetAttribute("info-name", ((SvgElement)graph1).GetAttribute("info-name") + "-2");
                                ((SvgElement)graph).SetAttribute("info-name", ((SvgElement)graph1).GetAttribute("info-name") + "-1");
                                copyEelement = this.mouseAreaControl.PicturePanel.AddElement(copyEelement);
                                this.mouseAreaControl.SVGDocument.CurrentElement = graph1 as SvgElement;
                                copyEelement.RemoveAttribute("points");
                                UpdateGraph(copyEelement, points2);
                                this.mouseAreaControl.PicturePanel.InvalidateElement(copyEelement);
                                BreakElementEventArgs copy = new BreakElementEventArgs(copyEelement);
                                if (OnPolyLineBreak != null &&  copyEelement!= null)
                                {
                                    
                                    OnPolyLineBreak(this.mouseAreaControl.SVGDocument.CurrentElement,copy);
                                }
                            }
                            if (this.points.Length > 0)
                            {
                                matrix1.TransformPoints(this.points);
                            }
                        }
                        else
                        {
                            return;
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
				int insertIndex= 0;
				if (tfArray1 != null)
				{
					;
					this.points = new PointF[tfArray1.Length + 1];
					
					if(addBegin)
					{
						tfArray1.CopyTo(this.points, 1);
					}
					else
					{
						tfArray1.CopyTo(this.points, 0);
						insertIndex=tfArray1.Length;
					}
				}
				if(addBegin || addEnd)
				{
					Matrix matrix1 = this.graph.GraphTransform.Matrix.Clone();
					matrix1.Invert();
					PointF[] points2 =new PointF[1]{new PointF(e.X,e.Y)};
					matrix1.TransformPoints(points2);
					this.startpoint = points2[0];					
				}

				this.points[insertIndex] = this.startpoint;
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
                Matrix matrix2 = this.graph.GraphTransform.Matrix.Clone();
                matrix2.Invert();               
                //tf1 = this.mouseAreaControl.PicturePanel.PointToView(new PointF((float)e.X, (float)e.Y));//new PointF((float) e.X, (float) e.Y)                
                PointF[] tfTemp = new PointF[1] { new PointF(e.X, e.Y) };
                matrix2.TransformPoints(tfTemp);
                ArrayList list2 = new ArrayList(this.points);
				list2.Insert(this.insertindex, tfTemp[0]);                
				this.points = new PointF[list2.Count];
				list2.CopyTo(this.points);
                
			Label_05F5:
				//2006-10-23 设置围栏初始颜色
//				if(this.mouseAreaControl.CurrentOperation == ToolOperation.Enclosure || this.mouseAreaControl.CurrentOperation == ToolOperation.InterEnclosure)
//				{
//					((XmlElement) this.graph).SetAttribute("style","fill:#C0C0FF;fill-opacity:0.3;stroke:#000000;stroke-opacity:1;");
//				}

                
				if (((this.operate == PolyOperate.Del) || (this.operate == PolyOperate.Draw)) || (this.operate == PolyOperate.Add) ||(this.operate==PolyOperate.Break))
				{   

					StringBuilder text1 = new StringBuilder();
					int num3 = 0;
					PointF[] tfArray2 = this.points;
					for (int num4 = 0; num4 < tfArray2.Length; num4++)
					{
						PointF tf2 = tfArray2[num4];
						text1.Append( tf2.X.ToString() + " " + tf2.Y.ToString());
						if (num3 < (this.points.Length - 1))
						{
							text1.Append( ",");
						}
						num3++;
					}
					this.mouseAreaControl.PicturePanel.InvalidateElement((SvgElement) this.graph);
					if (((SvgElement) this.graph).ParentNode == null)
					{
						this.UpdateGraph(text1.ToString());
						document1.AcceptChanges = true;
						IGraph graph2 = this.graph;
						this.mouseAreaControl.PicturePanel.AddElement(this.graph);
						this.graph = graph2;
					}
					else
					{
						document1.AcceptChanges = true;
						this.UpdateGraph(text1.ToString());
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
			else if(e.Button == MouseButtons.Right)//任务1
			{
				if (this.mouseAreaControl.CurrentOperation == ToolOperation.Enclosure || this.mouseAreaControl.CurrentOperation == ToolOperation.InterEnclosure)
				{
					//this.AreaPoints.Add(new PointF((float) e.X, (float) e.Y));

//					this.mouseAreaControl.GoBottom((SvgElement)this.graph);
//					((SvgElement)this.graph).Clone();
					PointF[] tfArray1 = new PointF[this.AreaPoints.Count];
					this.AreaPoints.CopyTo(tfArray1, 0);
					this.AreaPoints.Clear();
					
					Matrix matrix1 = new Matrix();
					if(tfArray1.Length<3)
					{
						
						this.mouseAreaControl.CurrentOperation=ToolOperation.Select;
						return;
					}
					this.selectAreaPath = new GraphicsPath();
					this.selectAreaPath.AddLines(tfArray1);
					this.selectAreaPath.CloseFigure();
					
				
					Region region1 = new Region(this.selectAreaPath);
					
					RectangleF r1=selectAreaPath.GetBounds();
					/* 2005环境 当前区域需要手动添加进集合*/
					XmlNode node1=((SvgElement)this.graph).Clone();
					SvgElement svgele=(SvgElement)node1;
					
					this.mouseAreaControl.SVGDocument.ClearSelects();
					
					using(Graphics g =Graphics.FromHwnd(IntPtr.Zero))
						  {
						foreach(ILayer layer1 in mouseAreaControl.SVGDocument.Layers)
						{
							if(!layer1.Visible )continue;

							SvgElementCollection.ISvgElementEnumerator	enumerator1 = layer1.GraphList.GetEnumerator();
							while (enumerator1.MoveNext())
							{								
								IGraph graph1 = (IGraph) enumerator1.Current;
								if(!graph1.Visible || !graph1.DrawVisible || this.graph==graph1)continue;
								GraphicsPath path1 = (GraphicsPath) graph1.GPath.Clone();
								path1.Transform(graph1.GraphTransform.Matrix);
								Region ef1 =null;
								if(graph1 is Use)
								{
									ef1 = new Region(PathFunc.GetBounds(path1));
								}
								else if (graph1 is Line)
								{
									ef1 = new Region(PathFunc.GetBounds(path1));

								}else
								{
									ef1 =new Region(path1);
								}
								
								// 设置围栏选择方式为完全包含才能选中
								if(this.mouseAreaControl.CurrentOperation==ToolOperation.Enclosure)
								{
									RectangleF rt1 = ef1.GetBounds(g);
									ef1.Intersect(region1);
									if(ef1.GetBounds(g)==rt1)
									{
										this.mouseAreaControl.SVGDocument.AddSelectElement(graph1);
									}
									continue;
								}

								// 设置围栏选择方式为搭边即选中
								if(this.mouseAreaControl.CurrentOperation==ToolOperation.InterEnclosure)
								{	
									ef1.Intersect(region1);

									if(!ef1.GetBounds(g).IsEmpty)
										//if ((region1.IsVisible(ef1/*new System.Drawing.Rectangle((int) ef1.X, (int) ef1.Y, (int) ef1.Width, (int) ef1.Height)*/) && !graph1.IsLock) && (graph1.DrawVisible /*&& (AnimFunc.GetKeyIndex((SvgElement) graph1, this.mouseAreaControl.SVGDocument.ControlTime, true) >= 0)*/))
									{
										this.mouseAreaControl.SVGDocument.AddSelectElement(graph1);
									}
								}
							}
							
						}
						this.mouseAreaControl.SVGDocument.AddSelectElement(graph);
					}
					
					GraphicsPath path2 = new GraphicsPath();
					path2.AddLines(tfArray1);
					RectangleF ef2 = path2.GetBounds();
					/* 2005 环境下使用,2003环境下需删除否则出现重复.原因不明.*/
					if(this.mouseAreaControl.CurrentOperation==ToolOperation.Enclosure)
					{
						this.mouseAreaControl.SVGDocument.AddSelectElement(svgele);
					}
					this.mouseAreaControl.Invalidate(new System.Drawing.Rectangle(((int) ef2.X) - 10, ((int) ef2.Y) - 10, ((int) ef2.Width) + 20, ((int) ef2.Height) + 20));
					return;
				}
			  
			} //鼠标右键抬起
        }
		bool addBegin=false;
		bool addEnd =false;

        public void OnMouseMove(MouseEventArgs e)
        {			
            if (this.graph == null )
            {
                this.Operate = PolyOperate.Draw;
                return;
            }
            if (((SvgElement) this.graph).ParentNode == null)
            {
                this.Operate = PolyOperate.Draw;
                return;
            }
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom11) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom14) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom15))
			{
				addBegin =false;
				addEnd =false;
			}
            if (isKeydown((int)ItopVector.Core.Win32.Enum.VirtualKeys.VK_B) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom13))
			{
				addBegin =true;
				addEnd =false;				
			}
            else if (isKeydown((int)ItopVector.Core.Win32.Enum.VirtualKeys.VK_E) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom12))
			{
				addEnd =true;
				addBegin=false;
			}
			if(addEnd || addBegin)
			{
				this.Operate = PolyOperate.Draw;
				return;
			}
            if (e.Button == MouseButtons.None)
            {
				this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
				this.win32.W32SetROP2(7);
				this.win32.W32PolyDraw(this.reversePath);
				this.reversePath.Reset();
				this.win32.ReleaseDC();
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
                        if ((Control.ModifierKeys & Keys.Control) == Keys.Control || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom11) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom14))
                        {
                            if (((Control.ModifierKeys & Keys.Alt) == Keys.Alt || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom11)) && this.graph is Polyline && num5 > 0 && num5 < tfArray3.Length-1 && (this.mouseAreaControl.CurrentOperation != ToolOperation.Custom14))
							{
//								PointF[] tfs=new PointF[num5];
//								for(int i=0;i<num5;i++)
//								{
//									tfs[i] = tfArray1[i];
//								}
//								((Polyline) this.graph).Points = tfs;
								this.Operate = PolyOperate.Break;
								return;
							}
                            else 
							{
								this.Operate = PolyOperate.Del;
								return;
							}
                        }
						this.Operate = PolyOperate.MovePoint;
                        return;
                        
                    }
                    if ((num1 - 1) >= 0)
                    {
                        path1.Reset();
                        PointF tf2 = tfArray1[num1 - 1];
                        path1.AddLine(tf2, tf1);
                        if (path1.IsOutlineVisible(new PointF((float) e.X, (float) e.Y), pen1))
                        {
                            if (((Control.ModifierKeys & Keys.Control) == Keys.Control) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom15))
                                
                            {
                                this.Operate = PolyOperate.Add;
                            }
                            else
                            {
								this.Operate = PolyOperate.MovePath;                                
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
                            if (((Control.ModifierKeys & Keys.Control) == Keys.Control) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom11)
                                || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom12) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom13) || (this.mouseAreaControl.CurrentOperation == ToolOperation.Custom14))
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

				if( this.operate==PolyOperate.Draw)
				{
					PointF pf1 =new PointF(e.X,e.Y);
			
					PointF pf2 = this.points[points.Length -1];

					PointF pf3 = pf1;
					if(this.graph is Polygon)
					{
						pf3=points[0];
					}

					PointF[] array1=new PointF[3]{pf2,pf1,pf3}; 

					this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
					this.win32.W32SetROP2(7);
					this.reversePath.Reset();
					this.reversePath.AddLines(array1);
					this.win32.W32PolyDraw(this.reversePath);
					this.win32.ReleaseDC();
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
            //tf4 = this.mouseAreaControl.PicturePanel.PointToView(tf4);//05-16 修改
            switch (this.operate)
            {
                case PolyOperate.MovePath:
                {
                    PointF[] tfArray2 = (PointF[]) this.points.Clone();
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
			this.reversePath.Reset();

            this.oldpoints = string.Empty;
            if (((this.graph is Polygon) || (this.graph is Polyline)) && (((SvgElement) this.graph).ParentNode != null))
            {
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
                if (this.points.Length > 0)
                {
                    Matrix matrix1 = this.graph.GraphTransform.Matrix.Clone();
                    matrix1.TransformPoints(this.points);
                    if (this.points.Length > 1 && this.points.Length<3)
                    {
//                        if (this.graph is Polygon)
//                        {
//                            e.Graphics.DrawPolygon(Pens.Blue, this.points);
//                        }
//                        else
//                        {
                            e.Graphics.DrawLines(Pens.Blue, this.points);
//                        }
                    }
                    SvgElement element1 = (SvgElement) this.graph;
                    if ((element1 != null) && !this.mouseAreaControl.SVGDocument.PlayAnim)
                    {
                        PointF[] tfArray1 = this.points;
                        for (int num1 = 0; num1 < tfArray1.Length; num1++)
                        {
                            PointF tf1 = tfArray1[num1];
                            RectangleF ef1 = new RectangleF(tf1.X - 2f, tf1.Y - 2f, 4f, 4f);
                            e.Graphics.FillRectangle(Brushes.White, ef1);
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
//                string text1 = string.Empty;
//                PointF[] tfArray1 = new PointF[0];
//                if (this.graph is Polygon)
//                {
//                    tfArray1 = ((Polygon) this.graph).Points;
//                }
//                else if (this.graph is Polyline)
//                {
//                    tfArray1 = ((Polyline) this.graph).Points;
//                }
//                int num1 = 0;
//                PointF[] tfArray2 = tfArray1;
//                for (int num10 = 0; num10 < tfArray2.Length; num10++)
//                {
//                    PointF tf1 = tfArray2[num10];
//                    text1 = text1 + tf1.X.ToString() + " " + tf1.Y.ToString();
//                    if (num1 < (tfArray1.Length - 1))
//                    {
//                        text1 = text1 + ",";
//                    }
//                    num1++;
//                }
                SvgDocument document1 = this.mouseAreaControl.SVGDocument;
                bool flag1 = document1.AcceptChanges;
                document1.AcceptChanges = true;
                document1.NumberOfUndoOperations = 200;
                if (( ((((SvgElement) this.graph).InfoList.Count == 1) && (this.mouseAreaControl.SVGDocument.ControlTime == 0))) || (((SvgElement) this.graph).ParentNode == null))
                {
                    AttributeFunc.SetAttributeValue((SvgElement) this.graph, "points", newpoints);
                }
                
                document1.NotifyUndo();
                document1.AcceptChanges = flag1;
            }
        }
		private void UpdateGraph(SvgElement polyline, PointF[] newpoints)
		{
			if (polyline != null)
			{
				StringBuilder text1 = new StringBuilder();
				PointF[] tfArray1 = (PointF[])newpoints.Clone();
				
				for (int num10 = 0; num10 < tfArray1.Length; num10++)
				{
				    PointF tf1 = tfArray1[num10];
				    text1.Append( tf1.X.ToString() + " " + tf1.Y.ToString());
				    if (num10 < (tfArray1.Length - 1))
				    {
				        text1.Append(",");
				    }
				}
				SvgDocument document1 = this.mouseAreaControl.SVGDocument;
				bool flag1 = document1.AcceptChanges;
				document1.AcceptChanges = true;
				document1.NumberOfUndoOperations = 200;
				if (( (((polyline).InfoList.Count == 1) && (this.mouseAreaControl.SVGDocument.ControlTime == 0))) || ((polyline).ParentNode == null))
				{
					AttributeFunc.SetAttributeValue(polyline, "points", text1.ToString());
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
					case PolyOperate.Break:
					{
						this.mouseAreaControl.Cursor = SpecialCursors.PolyBreak;
						
						break;
					}
                }
                if ((Control.ModifierKeys & Keys.Control) != Keys.Control )
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


        // Fields
		private ArrayList AreaPoints;
		private GraphicsPath selectAreaPath;
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
    }
}

