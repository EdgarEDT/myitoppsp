namespace ItopVector.DrawArea
{
	using ItopVector.Core;
	using ItopVector.Core.Document;
	using ItopVector.Core.Figure;
	using ItopVector.Core.Func;
	using ItopVector.Core.Interface.Figure;
	using ItopVector.Core.Paint;
	using ItopVector.Core.Types;
	using ItopVector.Resource;
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;
	using System.Xml;

	public class DrawOperation : IOperation
	{
		// Methods
		public DrawOperation()
		{
			this.mouseAreaControl = null;
			this.win32 = new Win32();
			this.tempPath = new GraphicsPath();
			this.drawPath = null;
			this.showdialog = false;
			this.filename = string.Empty;
			this.haveTextActive = false;
			this.startPoint = Point.Empty;
			this.mouseDown = false;
			this.reversePath = new GraphicsPath();
			this.first = true;
			this.list = new ArrayList();
			this.tooltips = new Hashtable();
			this.finish = false;
			this.redPath =new GraphicsPath();
		}

		internal DrawOperation(MouseArea mousecontrol):this()
		{			
			this.mouseAreaControl = mousecontrol;
			this.win32 = this.mouseAreaControl.win32;
			this.openFileDialog1 = new OpenFileDialog();
			string text1 = DrawAreaConfig.GetLabelForName("filefilter").Trim();
			this.openFileDialog1.Filter = text1;
			this.openFileDialog1.Title = DrawAreaConfig.GetLabelForName("imagedialog").Trim();
			this.openFileDialog1.FileOk += new CancelEventHandler(this.openFileDialog1_FileOk);
		}
		private  float GetAngle(PointF startpoint, PointF endpoint)
		{
			double num1 = Math.Sqrt(Math.Pow((double) (startpoint.X - endpoint.X), 2) + Math.Pow((double) (startpoint.Y - endpoint.Y), 2));
			double num2 = endpoint.Y - startpoint.Y;
			num2 = (Math.Asin(num2 / num1) / 3.1415926535897931) * 180;
			if (startpoint.X > endpoint.X)
			{
				num2 = 180 - num2;
			}
			if (num2 < 0)
			{
				num2 += 360;
			}
			return (float) num2;
		}
		private void CreateDrawPath(PointF startpoint, PointF endpoint, ToolOperation operation)
		{
			SizeF ef1 = this.mouseAreaControl.PicturePanel.GridSize;
			startpoint = this.mouseAreaControl.PicturePanel.PointToView(startpoint);
			endpoint = this.mouseAreaControl.PicturePanel.PointToView(endpoint);
			float single1 = ef1.Height;
			float single2 = ef1.Width;
			if (this.mouseAreaControl.PicturePanel.SnapToGrid)
			{
				int num1 = (int) ((startpoint.X + (single2 / 2f)) / single2);
				int num2 = (int) ((startpoint.Y + (single1 / 2f)) / single1);
				startpoint = new Point((int) (num1 * single2), (int) (num2 * single1));
				num1 = (int) ((endpoint.X + (single2 / 2f)) / single2);
				num2 = (int) ((endpoint.Y + (single1 / 2f)) / single1);
				endpoint = new Point((int) (num1 * single2), (int) (single1 * num2));
			}
			GraphicsPath path1 = new GraphicsPath();
			float single3 = Math.Min(startpoint.X, endpoint.X);
			float single4 = Math.Min(startpoint.Y, endpoint.Y);
			float single5 = Math.Max(startpoint.X, endpoint.X);
			float single6 = Math.Max(startpoint.Y, endpoint.Y);
			float single7 = single5 - single3;
			float single8 = single6 - single4;
			float single9 = 0f;
			float single10 = 0f;
			float single11 = 0f;
			float single12 = 0f;
			switch (operation)
			{
				case ToolOperation.AngleRectangle:
                case ToolOperation.InterEnclosurePrint:
				{
					single12 = 0f;
					single12 = Math.Max(0, ((ItopVector.Core.Figure.RectangleElement) this.mouseAreaControl.PicturePanel.PreGraph).Angle);
					single10 = single12;
					single11 = single12;
					if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
					{
						single7 = single8 = Math.Max(single7, single8);
						single3 = ((startpoint.X - endpoint.X) < 0) ? ((float) startpoint.X) : (startpoint.X - single8);
						single4 = ((startpoint.Y - endpoint.Y) < 0) ? ((float) startpoint.Y) : (startpoint.Y - single7);
					}
					if (single7 < 5f)
					{
						return;
					}
					if (single8 < 5f)
					{
						return;
					}
					if (single10 > (single7 / 2f))
					{
						single10 = single7;
					}
					if (single11 > (single8 / 2f))
					{
						single11 = single8;
					}
					if ((single10 == 0f) && (single11 == 0f))
					{
						path1.AddRectangle(new RectangleF(single3, single4, single7, single8));
						goto Label_0B89;
					}
					path1.AddArc((float) ((single3 + single7) - single10), single4, single10, single11, (float) 270f, (float) 90f);
					path1.AddArc((float) ((single3 + single7) - single10), (float) ((single4 + single8) - single11), single10, single11, (float) 0f, (float) 90f);
					path1.AddArc(single3, (float) ((single4 + single8) - single11), single10, single11, (float) 90f, (float) 90f);
					path1.AddArc(single3, single4, single10, single11, (float) 180f, (float) 90f);
					path1.CloseFigure();
					goto Label_0B89;
				}
				case ToolOperation.Circle:
				case ToolOperation.Bezier:
				case ToolOperation.FreeLines:
				case ToolOperation.Text:
				{
					goto Label_0B89;
				}
				case ToolOperation.Ellipse:
				case ToolOperation.Pie:
				case ToolOperation.Arc:
				{
					if (this.secondMouse)
					{
						if (!this.pieRect.IsEmpty)
						{
							startpoint = new PointF(this.pieRect.X + (this.pieRect.Width / 2f), this.pieRect.Y + (this.pieRect.Height / 2f));
							PointF tf1 = endpoint;
							endpoint = this.lastPoint;
							float single13 = GetAngle(startpoint, endpoint);
							float single14 = GetAngle(startpoint, tf1);
							single14 -= single13;
							if (single14 <= 0f)
							{
								single14 += 360f;
							}
							if (single14 == 360f)
							{
								path1.AddEllipse(this.pieRect.X, this.pieRect.Y, this.pieRect.Width, this.pieRect.Height);
							}
							else
							{
								path1.AddPie(this.pieRect.X, this.pieRect.Y, this.pieRect.Width, this.pieRect.Height, single13, single14);
							}
							this.startArcAngle = single13;
							this.endArcAngle = single14 + single13;
						}
						goto Label_0B89;
					}
					this.lastPoint = endpoint;
					if (Control.ModifierKeys == Keys.Shift)
					{
						single9 = Math.Max((float) (single5 - single3), (float) (single6 - single4));
						path1.AddEllipse(((startpoint.X - endpoint.X) < 0) ? ((float) startpoint.X) : (startpoint.X - single9), ((startpoint.Y - endpoint.Y) < 0) ? ((float) startpoint.Y) : (startpoint.Y - single9), single9, single9);

					}
					else if (Control.ModifierKeys == (Keys.Control | Keys.Shift))
					{
						single9 = Math.Max((float) (single5 - single3), (float) (single6 - single4));
						single3 = startpoint.X - single9;
						single4 = startpoint.Y - single9;
						path1.AddEllipse(single3, single4, (float) (2f * single9), (float) (2f * single9));						
					}
					else
					{
						if (Control.ModifierKeys == Keys.Control)
						{
							single3 = startpoint.X - Math.Abs((int) (endpoint.X - startpoint.X));
							single4 = startpoint.Y - Math.Abs((int) (endpoint.Y - startpoint.Y));
							single7 = Math.Abs((int) (endpoint.X - startpoint.X)) * 2f;
							single8 = Math.Abs((int) (endpoint.Y - startpoint.Y)) * 2f;
						}
						path1.AddEllipse(single3, single4, single7, single8);						
					}
					this.pieRect = path1.GetBounds();
					goto Label_0B89;
				}
					//				case SVGDeveloper.Base.Enum.Operator.Pie:
					//				case SVGDeveloper.Base.Enum.Operator.Arc:
					//				 {
					//					 if (this.secondMouse)
					//					 {
					//						 goto Label_061B;
					//					 }
					//					 this.lastPoint = endpoint;
					//					 if (Control.ModifierKeys == Keys.Shift)
					//					 {
					//						 single7 = Math.Max((float) (single3 - single1), (float) (single4 - single2));
					//						 path1.AddEllipse(((startpoint.X - endpoint.X) < 0f) ? startpoint.X : (startpoint.X - single7), ((startpoint.Y - endpoint.Y) < 0f) ? startpoint.Y : (startpoint.Y - single7), single7, single7);
					//					 }
					//					 else if (Control.ModifierKeys == (Keys.Control | Keys.Shift))
					//					 {
					//						 single7 = Math.Max((float) (single3 - single1), (float) (single4 - single2));
					//						 single1 = startpoint.X - single7;
					//						 single2 = startpoint.Y - single7;
					//						 path1.AddEllipse(single1, single2, 2f * single7, 2f * single7);
					//					 }
					//					 else
					//					 {
					//						 if (Control.ModifierKeys == Keys.Control)
					//						 {
					//							 single1 = startpoint.X - Math.Abs((float) (endpoint.X - startpoint.X));
					//							 single2 = startpoint.Y - Math.Abs((float) (endpoint.Y - startpoint.Y));
					//							 single5 = Math.Abs((float) (endpoint.X - startpoint.X)) * 2f;
					//							 single6 = Math.Abs((float) (endpoint.Y - startpoint.Y)) * 2f;
					//						 }
					//						 path1.AddEllipse(single1, single2, single5, single6);
					//					 }
					//					 this.pieRect = path1.GetBounds();
					//					 goto Label_0ECF;
					//				 }
				case ToolOperation.Line:
				{
					if ((Math.Abs((int) (startpoint.X - endpoint.X)) <= 2) && (Math.Abs((int) (startpoint.Y - endpoint.Y)) <= 2))
					{
						goto Label_0B89;
					}
					if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
					{
						float single13 = (((float) Math.Atan2((double) (endpoint.Y - startpoint.Y), (double) (endpoint.X - startpoint.X))) * 180f) / 3.141593f;
						single13 = ((int) Math.Round((double) (single13 / 45f), 0)) * 0x2d;
						if ((single13 != 90f) && (single13 != -90f))
						{
							single13 = (float) Math.Tan((single13 / 180f) * 3.1415926535897931);
							endpoint = new PointF(endpoint.X, startpoint.Y + ((int) ((endpoint.X - startpoint.X) * single13)));
							break;
						}
						endpoint = new PointF(startpoint.X, endpoint.Y);
					}
					break;
				}
				case ToolOperation.ConnectLine:
				{
//					if ((Math.Abs((int) (startpoint.X - endpoint.X)) <= 2) && (Math.Abs((int) (startpoint.Y - endpoint.Y)) <= 2))
//					{
//						goto Label_0B89;
//					}
//					if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
//					{
//						float single13 = (((float) Math.Atan2((double) (endpoint.Y - startpoint.Y), (double) (endpoint.X - startpoint.X))) * 180f) / 3.141593f;
//						single13 = ((int) Math.Round((double) (single13 / 45f), 0)) * 0x2d;
//						if ((single13 != 90f) && (single13 != -90f))
//						{
//							single13 = (float) Math.Tan((single13 / 180f) * 3.1415926535897931);
//							endpoint = new PointF(endpoint.X, startpoint.Y + ((int) ((endpoint.X - startpoint.X) * single13)));
//							break;
//						}
//						endpoint = new PointF(startpoint.X, endpoint.Y);
//					}
					break;
				}
				case ToolOperation.PolyLine:
				case ToolOperation.XPolyLine:
				case ToolOperation.YPolyLine:
				case ToolOperation.Confines_GuoJie:
				case ToolOperation.Confines_ShengJie:
				case ToolOperation.Confines_ShiJie:
				case ToolOperation.Confines_XianJie:
				case ToolOperation.Confines_XiangJie:
				case ToolOperation.Railroad:
				case ToolOperation.Polygon:
				case ToolOperation.Enclosure:
				case ToolOperation.InterEnclosure:
				case ToolOperation.AreaPolygon:
				case ToolOperation.LeadLine:
				{
					if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
					{
						float single14 = (((float) Math.Atan2((double) (endpoint.Y - startpoint.Y), (double) (endpoint.X - startpoint.X))) * 180f) / 3.141593f;
						single14 = ((int) Math.Round((double) (single14 / 45f), 0)) * 0x2d;
						if ((single14 != 90f) && (single14 != -90f))
						{
							single14 = (float) Math.Tan((single14 / 180f) * 3.1415926535897931);
							endpoint = new PointF(endpoint.X, startpoint.Y + ((int) ((endpoint.X - startpoint.X) * single14)));
						}
						else
						{
							endpoint = new PointF(startpoint.X, endpoint.Y);
						}
					}
					path1.AddLine(startpoint, endpoint);
					goto Label_0B89;
				}
				case ToolOperation.EqualPolygon:
				{
					int num3 = 3;
					num3 = Math.Max(3, ((Polygon) this.mouseAreaControl.PicturePanel.PreGraph).LineCount);
					double num4 = 6.2831853071795862 / ((double) num3);
					GraphicsPath path2 = new GraphicsPath();
					double num5 = 0;
					double num6 = 0;
					double num7 = Math.Sqrt(Math.Pow((double) (startpoint.X - endpoint.X), 2) + Math.Pow((double) (startpoint.Y - endpoint.Y), 2));
					float single15 = (float) Math.Round((double) ((Polygon) this.mouseAreaControl.PicturePanel.PreGraph).Indent, 1);
					int num8 = num3;
					if (single15 < 1f)
					{
						num8 = num3 * 2;
					}
					Point[] pointArray1 = new Point[num8];
					if (num7 >= 2)
					{
						double num9 = Math.Asin(((double) (endpoint.Y - startpoint.Y)) / num7);
						if (endpoint.X < startpoint.X)
						{
							num9 = 3.1415926535897931 - num9;
						}
						Point point1 = Point.Empty;
						for (int num11 = 0; num11 < num8; num11 += (num8 / num3))
						{
							double num10 = num9 + (num4 * (num11 / (num8 / num3)));
							num5 = num7 * Math.Cos(num10);
							num6 = num7 * Math.Sin(num10);
							pointArray1[num11] = new Point((int)startpoint.X + ((int) num5),(int) startpoint.Y + ((int) num6));
							if (num8 == (2 * num3))
							{
								if (!point1.IsEmpty)
								{
									Point point2 = new Point((point1.X + pointArray1[num11].X) / 2, (point1.Y + pointArray1[num11].Y) / 2);
									point2 = new Point((int)startpoint.X + ((int) (single15 * (point2.X - startpoint.X))), (int)startpoint.Y + ((int) (single15 * (point2.Y - startpoint.Y))));
									pointArray1[num11 - 1] = point2;
								}
								point1 = pointArray1[num11];
								if (num11 == ((2 * num3) - 2))
								{
									Point point3 = new Point((pointArray1[0].X + pointArray1[num11].X) / 2, (pointArray1[0].Y + pointArray1[num11].Y) / 2);
									point3 = new Point((int)startpoint.X + ((int) (single15 * (point3.X - startpoint.X))), (int)startpoint.Y + ((int) (single15 * (point3.Y - startpoint.Y))));
									pointArray1[num11 + 1] = point3;
								}
							}
						}
						path1.AddPolygon(pointArray1);
						path1.StartFigure();
						path1.AddLine(startpoint, endpoint);
					}
					goto Label_0B89;
				}
				case ToolOperation.Image:
				{
					if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
					{
						single7 = single8 = Math.Min(single7, single8);
					}
					path1.AddRectangle(new RectangleF(single3, single4, single7, single8));
					goto Label_0B89;
				}
				case ToolOperation.PreShape:
				{
					if (this.first)
					{
						this.reversePath = PathFunc.GetPathFromGraph(this.mouseAreaControl.PicturePanel.PreGraph);
						this.reversePath.Transform(this.mouseAreaControl.PicturePanel.CoordTransform);
						this.first = false;
					}
					GraphicsPath path3 = (GraphicsPath) this.reversePath.Clone();
					single3 = startpoint.X;
					single4 = startpoint.Y;
					single7 = endpoint.X - startpoint.X;
					single8 = endpoint.Y - startpoint.Y;
					if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
					{
						single7 = single8 = Math.Max(single7, single8);
						single3 = ((startpoint.X - endpoint.X) < 0) ? ((float) startpoint.X) : (startpoint.X - single8);
						single4 = ((startpoint.Y - endpoint.Y) < 0) ? ((float) startpoint.Y) : (startpoint.Y - single7);
					}
					if (Math.Abs(single7) < 2f)
					{
						return;
					}
					if (Math.Abs(single8) < 2f)
					{
						return;
					}
					path3.Flatten(new Matrix());
					float single16 = single3;
					float single17 = single4;
					RectangleF ef2 = path3.GetBounds();
					Matrix matrix1 = new Matrix();
					matrix1.Translate(single16 - ef2.X, single17 - ef2.Y);
					matrix1.Translate(ef2.X, ef2.Y);
					matrix1.Scale(single7 / ef2.Width, single8 / ef2.Height);
					matrix1.Translate(-ef2.X, -ef2.Y);
					path1 = (GraphicsPath) this.reversePath.Clone();
					path1.Transform(matrix1);
					goto Label_0B89;
				} 
				
				default:
				{
					goto Label_0B89;
				}
			}
			path1.AddLine(startpoint, endpoint);
			Label_0B89:
				path1.Transform(this.mouseAreaControl.PicturePanel.CoordTransform);
			this.tempPath = path1;
		}

		private IGraph CreateGraph(PointF startpoint, PointF endpoint, ToolOperation operation)
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
			if ((operation == ToolOperation.Ellipse) && ((Control.ModifierKeys == Keys.Shift) || (Control.ModifierKeys == (Keys.Control | Keys.Shift))))
			{
				graph2 = (Circle) document1.CreateElement(document1.Prefix, "circle", document1.NamespaceURI);
				foreach (XmlAttribute attribute1 in ((SvgElement) graph1).Attributes)
				{
					((SvgElement) graph2).SetAttributeNode((XmlAttribute) attribute1.Clone());
				}
			}
			if (((SvgElement) graph1) is IGraphPath)
			{
				if ((((SvgElement) graph1).GetAttribute("style") != string.Empty) && (((SvgElement) graph1).GetAttribute("style") != null))
				{
					this.mouseAreaControl.SVGDocument.AcceptChanges = false;
					AttributeFunc.SetAttributeValue((SvgElement) graph2, "style", ((SvgElement) graph1).GetAttribute("style"));
				}
				ISvgBrush brush1 = ((IGraphPath) graph1).GraphBrush;
				if (brush1 is SvgElement)
				{
					ISvgBrush brush2 = (ISvgBrush) ((SvgElement) brush1).Clone();
					((IGraphPath) graph2).GraphBrush = brush2;
					((SvgElement) brush2).pretime = -1;
				}
				else
				{
					((IGraphPath) graph2).GraphBrush = brush1;
				}
				brush1 = ((IGraphPath) graph1).GraphStroke.Brush;
				if (brush1 is SvgElement)
				{
					ISvgBrush brush3 = (ISvgBrush) ((SvgElement) brush1).Clone();
					((IGraphPath) graph2).GraphStroke = new Stroke(brush3);
					((SvgElement) brush3).pretime = -1;
				}
				else
				{
					((IGraphPath) graph2).GraphStroke.Brush = brush1;
				}
			}
			if (document1 == null)
			{
				return null;
			}
			PointF point1 = this.mouseAreaControl.PicturePanel.PointToView(startpoint);
			PointF point2 = this.mouseAreaControl.PicturePanel.PointToView(endpoint);
			float single1 = this.mouseAreaControl.PicturePanel.ScaleUnit;
			SizeF ef1 = this.mouseAreaControl.PicturePanel.GridSize;
			float single2 = ef1.Height;
			float single3 = ef1.Width;
			if (this.mouseAreaControl.PicturePanel.SnapToGrid)
			{
				int num1 = (int) ((point1.X + (single3 / 2f)) / single3);
				int num2 = (int) ((point1.Y + (single2 / 2f)) / single2);
				point1 = new Point((int) (num1 * single3), (int) (num2 * single2));
				num1 = (int) ((point2.X + (single3 / 2f)) / single3);
				num2 = (int) ((point2.Y + (single2 / 2f)) / single2);
				point2 = new Point((int) (num1 * single3), (int) (single2 * num2));
			}
			float single4 = Math.Min(point1.X, point2.X);
			float single5 = Math.Min(point1.Y, point2.Y);
			float single6 = Math.Max(point1.X, point2.X);
			float single7 = Math.Max(point1.Y, point2.Y);
			float single8 = single6 - single4;
			float single9 = single7 - single5;
			switch (operation)
			{
				case ToolOperation.AngleRectangle:
                {
                    float single10 = 0f;
                    single10 = Math.Max(0, ((ItopVector.Core.Figure.RectangleElement)this.mouseAreaControl.PicturePanel.PreGraph).Angle);
                    float single11 = single10;
                    float single12 = single10;
                    if ((single8 < 5f) || (single9 < 5f))
                    {
                        return null;
                    }
                    if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                    {
                        single8 = single9 = Math.Max(single8, single9);
                        single4 = ((point1.X - point2.X) < 0) ? ((float)point1.X) : (point1.X - single8);
                        single5 = ((point1.Y - point2.Y) < 0) ? ((float)point1.Y) : (point1.Y - single9);
                    }
                    if (single11 > (single8 / 2f))
                    {
                        single11 = single8;
                    }
                    if (single12 > (single9 / 2f))
                    {
                        single12 = single9;
                    }
                    ItopVector.Core.Figure.RectangleElement rectangle1 = (ItopVector.Core.Figure.RectangleElement)graph2;
                    rectangle1.X = single4;
                    rectangle1.Y = single5;
                    rectangle1.Width = single8;
                    rectangle1.Height = single9;
                    rectangle1.RX = single11;
                    rectangle1.RY = single12;
                   // AttributeFunc.SetAttributeValue((XmlElement)rectangle1, "style", "fill:#C0C0FF;fill-opacity:0.3;stroke:#000000;stroke-opacity:1;");
                    goto Label_0F08;
                }
                case ToolOperation.InterEnclosurePrint:
				{
					float single10 = 0f;
					single10 = Math.Max(0, ((ItopVector.Core.Figure.RectangleElement) this.mouseAreaControl.PicturePanel.PreGraph).Angle);
					float single11 = single10;
					float single12 = single10;
					if ((single8 < 5f) || (single9 < 5f))
					{
						return null;
					}
					if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
					{
						single8 = single9 = Math.Max(single8, single9);
						single4 = ((point1.X - point2.X) < 0) ? ((float) point1.X) : (point1.X - single8);
						single5 = ((point1.Y - point2.Y) < 0) ? ((float) point1.Y) : (point1.Y - single9);
					}
					if (single11 > (single8 / 2f))
					{
						single11 = single8;
					}
					if (single12 > (single9 / 2f))
					{
						single12 = single9;
					}
					ItopVector.Core.Figure.RectangleElement rectangle1 = (ItopVector.Core.Figure.RectangleElement) graph2;
					rectangle1.X = single4;
					rectangle1.Y = single5;
					rectangle1.Width = single8;
					rectangle1.Height = single9;
					rectangle1.RX = single11;
					rectangle1.RY = single12;
                    AttributeFunc.SetAttributeValue((XmlElement)rectangle1, "style", "fill:#C0C0FF;fill-opacity:0.0;stroke:#000000;stroke-opacity:1;");
					goto Label_0F08;
				}
				case ToolOperation.Circle:
				case ToolOperation.Bezier:
				case ToolOperation.Text:
				{
					goto Label_0F08;
				}
				case ToolOperation.Ellipse:
				{
					if ((single8 < 3f) || (single9 < 3f))
					{
						return null;
					}
					if (Control.ModifierKeys == Keys.Shift)
					{
						float single13 = Math.Max((float) (single6 - single4), (float) (single7 - single5)) / 2f;
						if (single13 < 3f)
						{
							return null;
						}
						single4 = ((point1.X - point2.X) < 0) ? ((float) point1.X) : (point1.X - (2f * single13));
						single5 = ((point1.Y - point2.Y) < 0) ? ((float) point1.Y) : (point1.Y - (2f * single13));
						float single14 = single4 + single13;
						float single15 = single5 + single13;
						Circle circle1 = (Circle) graph2;
						circle1.CX = single14;
						circle1.CY = single15;
						circle1.R = single13;
						goto Label_0F08;
					}
					if (Control.ModifierKeys == (Keys.Control | Keys.Shift))
					{
						Circle circle2 = (Circle) graph2;
						circle2.CX = point1.X;
						circle2.CY = point1.Y;
						float single16 = Math.Max(Math.Abs((int) (point1.Y - point2.Y)), Math.Abs((int) (point1.X - point2.X)));
						if (single16 < 3f)
						{
							return null;
						}
						circle2.R = single16;
						goto Label_0F08;
					}
					Ellips ellips1 = (Ellips) graph2;
					if (Control.ModifierKeys == Keys.Control)
					{
						ellips1.CX = point1.X;
						ellips1.CY = point1.Y;
						ellips1.RX = Math.Abs((int) (point2.X - point1.X));
						ellips1.RY = Math.Abs((int) (point2.Y - point1.Y));
						goto Label_0F08;
					}
					ellips1.CX = single4 + (single8 / 2f);
					ellips1.CY = single5 + (single9 / 2f);
					ellips1.RX = single8 / 2f;
					ellips1.RY = single9 / 2f;
					goto Label_0F08;
				}
				case ToolOperation.Line:
				{
					if ((single8 < 1f) && (single9 < 1f))
					{
						return null;
					}
					if ((Math.Abs((int) (point1.X - point2.X)) <= (2f * single1)) && (Math.Abs((int) (point1.Y - point2.Y)) <= (2f * single1)))
					{
						goto Label_0F07;
					}
					if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
					{
						float single17 = (((float) Math.Atan2((double) (point2.Y - point1.Y), (double) (point2.X - point1.X))) * 180f) / 3.141593f;
						single17 = ((int) Math.Round((double) (single17 / 45f), 0)) * 0x2d;
						if ((single17 != 90f) && (single17 != -90f))
						{
							single17 = (float) Math.Tan((single17 / 180f) * 3.1415926535897931);
							point2 = new PointF(point2.X, point1.Y + ((int) ((point2.X - point1.X) * single17)));
							break;
						}
						point2 = new PointF(point1.X, point2.Y);
					}
					break;
				}
				case ToolOperation.ConnectLine://连接线
				 {
					 if (((single8 < 1f) && (single9 < 1f)) ||connectBegin==null || connectEnd==null || connectBegin==connectEnd)
					 {
						 return null;
					 }
					ConnectLine cline =graph2 as ConnectLine;
					cline.SetAttribute("start","#"+connectBegin.ID+"."+connectBeginIndex);

					cline.SetAttribute("end","#"+connectEnd.ID+"."+connectEndIndex);

					connectBegin=null;
					connectEnd =null;

					 break;
				 }
				case ToolOperation.PolyLine:
				case ToolOperation.XPolyLine:
				case ToolOperation.YPolyLine:
				case ToolOperation.FreeLines:
				case ToolOperation.Confines_GuoJie:
				case ToolOperation.Confines_ShengJie:
				case ToolOperation.Confines_ShiJie:
				case ToolOperation.Confines_XianJie:
				case ToolOperation.Confines_XiangJie:
				case ToolOperation.Railroad:
				case ToolOperation.LeadLine:
				{
					if (this.drawPath != null)
					{
						PointF[] tfArray1 = this.drawPath.PathPoints;
						PointF[] tfArray2 = new PointF[tfArray1.Length];
						for (int num3 = 0; num3 < tfArray1.Length; num3++)
						{
							PointF tf1 = this.mouseAreaControl.PicturePanel.PointToView(tfArray1[num3]);
							tf1 = new PointF(tf1.X, tf1.Y);
							tfArray2[num3] = tf1;
						}
						((Polyline) graph2).Points = tfArray2;
					}
					goto Label_0F08;
				}
				case ToolOperation.Polygon:
				case ToolOperation.Enclosure:
				case ToolOperation.InterEnclosure:
				case ToolOperation.AreaPolygon:
				{
					if (this.drawPath != null)
					{
						PointF[] tfArray3 = this.drawPath.PathPoints;
						PointF[] tfArray4 = new PointF[tfArray3.Length];
						for (int num4 = 0; num4 < tfArray3.Length; num4++)
						{
							PointF tf2 = this.mouseAreaControl.PicturePanel.PointToView(tfArray3[num4]);
							tf2 = new PointF(tf2.X, tf2.Y);
							tfArray4[num4] = tf2;
						}
						((Polygon) graph2).Points = tfArray4;
					}
					goto Label_0F08;
				}
				case ToolOperation.EqualPolygon:
				{
					int num5 = 3;
					num5 = Math.Max(3, ((Polygon) this.mouseAreaControl.PicturePanel.PreGraph).LineCount);
					double num6 = 6.2831853071795862 / ((double) num5);
					GraphicsPath path1 = new GraphicsPath();
					double num7 = 0;
					double num8 = 0;
					double num9 = Math.Sqrt(Math.Pow((double) (point1.X - point2.X), 2) + Math.Pow((double) (point1.Y - point2.Y), 2));
					float single18 = (float) Math.Round((double) ((Polygon) this.mouseAreaControl.PicturePanel.PreGraph).Indent, 1);
					int num10 = num5;
					if (single18 < 1f)
					{
						num10 = num5 * 2;
					}
					PointF[] tfArray5 = new PointF[num10];
					if (num9 < 2)
					{
						return null;
					}
					if (num9 >= 2)
					{
						double num11 = Math.Asin(((double) (point2.Y - point1.Y)) / num9);
						if (point2.X < point1.X)
						{
							num11 = 3.1415926535897931 - num11;
						}
						PointF tf3 = PointF.Empty;
						for (int num13 = 0; num13 < num10; num13 += (num10 / num5))
						{
							double num12 = num11 + (num6 * (num13 / (num10 / num5)));
							num7 = num9 * Math.Cos(num12);
							num8 = num9 * Math.Sin(num12);
							tfArray5[num13] = new PointF((float) (point1.X + ((int) num7)), (float) (point1.Y + ((int) num8)));
							if (num10 == (2 * num5))
							{
								if (!tf3.IsEmpty)
								{
									PointF tf4 = new PointF((tf3.X + tfArray5[num13].X) / 2f, (tf3.Y + tfArray5[num13].Y) / 2f);
									tf4 = new PointF((float) (point1.X + ((int) (single18 * (tf4.X - point1.X)))), (float) (point1.Y + ((int) (single18 * (tf4.Y - point1.Y)))));
									tfArray5[num13 - 1] = tf4;
								}
								tf3 = tfArray5[num13];
								if (num13 == ((2 * num5) - 2))
								{
									PointF tf5 = new PointF((tfArray5[0].X + tfArray5[num13].X) / 2f, (tfArray5[0].Y + tfArray5[num13].Y) / 2f);
									tf5 = new PointF((float) (point1.X + ((int) (single18 * (tf5.X - point1.X)))), (float) (point1.Y + ((int) (single18 * (tf5.Y - point1.Y)))));
									tfArray5[num13 + 1] = tf5;
								}
							}
						}
						string text1 = string.Empty;
						for (int num14 = 0; num14 < tfArray5.Length; num14++)
						{
							PointF tf6 = tfArray5[num14];
							text1 = text1 + tf6.X.ToString() + " " + tf6.Y.ToString();
							if (num14 < (tfArray5.Length - 1))
							{
								text1 = text1 + ",";
							}
						}
						AttributeFunc.SetAttributeValue((SvgElement) graph2, "points", text1);
					}
					goto Label_0F08;
				}
				case ToolOperation.Image:
				{
					graph2=null;
					if (this.showdialog && (this.filename != string.Empty))
					{
						graph2 = (ItopVector.Core.Figure.Image) document1.CreateElement(document1.Prefix, "image", document1.NamespaceURI);
						ItopVector.Core.Figure.Image image1 = (ItopVector.Core.Figure.Image) graph2;
						image1.X = single4;
						image1.Y = single5;
						image1.Width = single8;
						image1.Height = single9;
						image1.ImageUrl = this.filename;
					}
					goto Label_0F08;
				}
				case ToolOperation.PreShape:
				{
					GraphicsPath path2 = PathFunc.GetPathFromGraph(this.mouseAreaControl.PicturePanel.PreGraph);
					single4 = point1.X;
					single5 = point1.Y;
					single8 = point2.X - point1.X;
					single9 = point2.Y - point1.Y;
					if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
					{
						single8 = single9 = Math.Max(single8, single9);
						single4 = ((point1.X - point2.X) < 0) ? ((float) point1.X) : (point1.X - single9);
						single5 = ((point1.Y - point2.Y) < 0) ? ((float) point1.Y) : (point1.Y - single8);
					}
					if ((Math.Abs(single8) < 2f) || (Math.Abs(single9) < 2f))
					{
						return null;
					}
					path2.Flatten(new Matrix());
					float single19 = single4;
					float single20 = single5;
					RectangleF ef2 = path2.GetBounds();
					Matrix matrix1 = new Matrix();
					matrix1.Translate(single19 - ef2.X, single20 - ef2.Y);
					matrix1.Translate(ef2.X, ef2.Y);
					matrix1.Scale(single8 / ef2.Width, single9 / ef2.Height);
					matrix1.Translate(-ef2.X, -ef2.Y);
					Transf transf1 = new Transf();
					transf1.setMatrix(matrix1);
					AttributeFunc.SetAttributeValue((SvgElement) graph2, "transform", transf1.ToString());
					goto Label_0F08;
				}
				case ToolOperation.Pie:
				case ToolOperation.Arc:
				{
					
					if (!this.pieRect.IsEmpty)
					{
						SvgElement element9 = graph2 as SvgElement;
						startpoint = new PointF(this.pieRect.X + (this.pieRect.Width / 2f), this.pieRect.Y + (this.pieRect.Height / 2f));
						PointF tf7 = this.mouseAreaControl.PicturePanel.PointToView(endpoint);
						if (tf7 == lastPoint)
						{
							graph2 =null;
							goto Label_0F08;
						}
						endpoint = this.lastPoint;
						float single17 = this.startArcAngle;
						float single18 = this.endArcAngle;
						while (single18 > 360f)
						{
							single18 -= 360f;
						}
						float single19 = this.pieRect.Width / 2f;
						float single20 = this.pieRect.Height / 2f;
						float single21 = startpoint.X;
						float single22 = startpoint.Y;
						string text4 = (((single18 - single17) > 180f) || (single18 < single17)) ? "1" : "0";
						single17 = (float) ((single17 / 180f) * 3.1415926535897931);
						single18 = (float) ((single18 / 180f) * 3.1415926535897931);
						PointF tf8 = this.GetCrossPoint(this.pieRect, startpoint, endpoint);
						float single23 = tf8.X;
						float single24 = tf8.Y;
						tf8 = this.GetCrossPoint(this.pieRect, startpoint, tf7);
						float single25 = tf8.X;
						float single26 = tf8.Y;
						string text5 = string.Concat(new string[] { 
																	  "M", single21.ToString(), " ", single22.ToString(), "L", single23.ToString(), " ", single24.ToString(), "A", single19.ToString(), " ", single20.ToString(), " 0 ", text4, " 1 ", single25.ToString(), 
																	  " ", single26.ToString(), "Z"
																  });
						if (operation ==ToolOperation.Arc)
						{
							text5 = string.Concat(new string[] { "M", single23.ToString(), " ", single24.ToString(), "A", single19.ToString(), " ", single20.ToString(), " 0 ", text4, " 1 ", single25.ToString(), " ", single26.ToString() });
						}
						element9.SetAttribute("d", text5);
						graph2 = element9 as IGraph;
						this.pieRect = RectangleF.Empty;
					}
					else
					{
						graph2 =null;
					}
					goto Label_0F08;
				}
				default:
				{
					goto Label_0F08;
				}
			}
			Label_0F07:
				Line line1 = (Line) graph2;
			line1.X1 = point1.X;
			line1.Y1 = point1.Y;
			line1.X2 = point2.X;
			line1.Y2 = point2.Y;
			Label_0F08:
				this.mouseAreaControl.SVGDocument.AcceptChanges = flag1;
			return graph2;
		}
		private PointF GetCrossPoint(RectangleF ellipseRect, PointF start, PointF end)
		{
			if (end.X == start.X)
			{
				return PointF.Empty;
			}
			float single1 = (end.Y - start.Y) / (end.X - start.X);
			float single2 = start.Y - (single1 * start.X);
			float single3 = ellipseRect.X + (ellipseRect.Width / 2f);
			float single4 = ellipseRect.Y + (ellipseRect.Height / 2f);
			float single5 = ellipseRect.Width / 2f;
			float single6 = ellipseRect.Height / 2f;
			float single7 = (1f / (single5 * single5)) + ((single1 * single1) / (single6 * single6));
			float single8 = (((2f * single1) * (single2 - single4)) / (single6 * single6)) - ((2f * single3) / (single5 * single5));
			float single9 = (((single3 * single3) / (single5 * single5)) + (((single2 - single4) * (single2 - single4)) / (single6 * single6))) - 1f;
			float single10 = (((float) Math.Sqrt((double) ((single8 * single8) - ((4f * single7) * single9)))) - single8) / (2f * single7);
			float single11 = (-((float) Math.Sqrt((double) ((single8 * single8) - ((4f * single7) * single9)))) - single8) / (2f * single7);
			if (end.X < start.X)
			{
				single10 = single11;
			}
			float single12 = (single1 * single10) + single2;
			return new PointF(single10, single12);
		}
		public void Dispose()
		{
			this.mouseAreaControl.DrawOperation = null;
		}
		public void OnMouseWheel(MouseEventArgs e)
		{
			// TODO:  添加 BezierOperation.DealMouseWheel 实现
		}
		public void OnMouseDown(MouseEventArgs e)
		{
			if ((e.Button != MouseButtons.None) && !this.haveTextActive)
			{
				this.mouseDown = true;
				this.first = true;
				if ((e.Button == MouseButtons.Left) && (this.drawPath == null))
				{
					this.startPoint = new Point(e.X, e.Y);
				}
			}
		}

		public void OnMouseMove(MouseEventArgs e)
		{
			ToolOperation operation1 = this.CurrentOperation;
			this.Tip();
			this.mouseAreaControl.Cursor = this.mouseAreaControl.DefaultCursor;
			if ((((e.Button == MouseButtons.Left) || (this.drawPath != null)) && !this.haveTextActive) && (this.mouseDown || (this.drawPath != null)))
			{
				if (this.mouseAreaControl.CurrentOperation == ToolOperation.FreeLines)
				{
					if (this.drawPath == null)
					{
						this.drawPath = new GraphicsPath();
					}
					this.drawPath.AddLine(this.startPoint, new Point(e.X, e.Y));
					this.startPoint = new Point(e.X, e.Y);
					RectangleF ef1 = PathFunc.GetBounds(this.drawPath);
					this.mouseAreaControl.Invalidate(new System.Drawing.Rectangle(((int) ef1.X) - 5, ((int) ef1.Y) - 5, ((int) ef1.Width) + 10, ((int) ef1.Height) + 10));
				}
				else
				{
					this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
					this.win32.W32SetROP2(7);
					this.win32.W32PolyDraw(this.tempPath);
					this.tempPath.Reset();
					this.CreateDrawPath(this.startPoint, new Point(e.X, e.Y), this.CurrentOperation);
					this.win32.W32PolyDraw(this.tempPath);
					this.win32.ReleaseDC();
				}
				if (CurrentOperation == ToolOperation.ConnectLine)
				{
					this.InvadatePath(this.redPath);
					this.redPath.Reset();
					PointF tf11=PointF.Empty;
					IGraph cdababf1 = this.FindGraphByPoint(new PointF(e.X, e.Y), ref tf11,ref connectEndIndex);
					this.connectEnd = cdababf1;
					int num7 = 10;
					if (cdababf1 != null)
					{
						this.lastPoint = tf11;
						this.redPath.AddRectangle(new RectangleF(tf11.X - (num7 / 2), tf11.Y - (num7 / 2), (float) num7, (float) num7));
					}
					this.InvadatePath(this.redPath);
				}
			}
			else if (OperationFunc.IsPieOperator(this.CurrentOperation) && this.secondMouse)
			{

				this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
				this.win32.W32SetROP2(7);
				this.win32.W32PolyDraw(this.tempPath);
				this.tempPath.Reset();
				this.CreateDrawPath(this.startPoint, new Point(e.X, e.Y), this.CurrentOperation);
				this.win32.W32PolyDraw(this.tempPath);
				this.win32.ReleaseDC();
			}
			else if (CurrentOperation == ToolOperation.ConnectLine && !this.mouseDown)
			{
				this.InvadatePath(this.redPath);
				this.redPath.Reset();
				PointF tf11=PointF.Empty;
				IGraph cdababf1 = this.FindGraphByPoint(new PointF(e.X, e.Y), ref tf11,ref connectBeginIndex);
				this.connectBegin = cdababf1;
				int num7 = 10;
				if (cdababf1 != null)
				{
					this.startPoint = tf11;
					this.redPath.AddRectangle(new RectangleF(tf11.X - (num7 / 2), tf11.Y - (num7 / 2), (float) num7, (float) num7));
				}
				this.InvadatePath(this.redPath);
			}
		}

		public void OnMouseUp(MouseEventArgs e)
		{
			this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
			this.win32.W32SetROP2(7);
			this.win32.W32PolyDraw(this.tempPath);
			this.win32.ReleaseDC();
			if (this.mouseDown)
			{
				if (e.Button == MouseButtons.Right)
				{
					if ((this.CurrentOperation == ToolOperation.Polygon) || ((this.CurrentOperation == ToolOperation.PolyLine) && (this.drawPath != null)))
					{
						IGraph graph1 = this.CreateGraph(this.startPoint, new Point(e.X, e.Y), this.CurrentOperation);
						if (graph1 != null)
						{
							this.mouseAreaControl.AddElement((SvgElement) graph1);
						}
					}
					this.startPoint = Point.Empty;
					this.drawPath = null;
					this.finish = true;
				}
				else if ((this.CurrentOperation != ToolOperation.Bezier) && (e.Button != MouseButtons.None))
				{
					this.mouseDown = false;
					ToolOperation operation1 = this.CurrentOperation;
					if (OperationFunc.IsDrawOperation(operation1))
					{
						this.finish = true;
						bool flag1 = OperationFunc.IsPieOperator(this.CurrentOperation);
						if (flag1||(e.Button != MouseButtons.Right) && ((operation1 == ToolOperation.PolyLine) || (operation1 == ToolOperation.Polygon)))
						{
							this.finish = false;
						}

						if (this.finish||(flag1 && secondMouse))
						{
							if (this.drawPath != null)
							{
								PointF point1 = new Point(e.X, e.Y);
								if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
								{
									float single1 = (((float) Math.Atan2((double) (point1.Y - this.startPoint.Y), (double) (point1.X - this.startPoint.X))) * 180f) / 3.141593f;
									single1 = ((int) Math.Round((double) (single1 / 45f), 0)) * 0x2d;
									if ((single1 == 90f) || (single1 == -90f))
									{
										point1 = new PointF(this.startPoint.X, point1.Y);
									}
									else
									{
										single1 = (float) Math.Tan((single1 / 180f) * Math.PI);
										point1 = new PointF(point1.X, this.startPoint.Y + ((int) ((point1.X - this.startPoint.X) * single1)));
									}
								}
								this.drawPath.AddLine(this.startPoint.X, this.startPoint.Y, point1.X, point1.Y);
							}
							IGraph graph2 = null;
							if (((this.CurrentOperation == ToolOperation.Image) && (Math.Abs((int) (e.X - this.startPoint.X)) > 5)) && ((Math.Abs((int) (e.Y - this.startPoint.Y)) > 5) && (e.Button == MouseButtons.Left)))
							{
								this.openFileDialog1.ShowDialog();
								if (this.showdialog && (this.filename != string.Empty))
								{
									graph2 = this.CreateGraph(this.startPoint, new Point(e.X, e.Y), this.CurrentOperation);
								}
							}
							else
							{
								graph2 = this.CreateGraph(this.startPoint, new Point(e.X, e.Y), this.CurrentOperation);
							}
							if (graph2 != null)
							{
								this.mouseAreaControl.AddElement((SvgElement) graph2);
								
							}
							RectangleF ef1 = PathFunc.GetBounds(this.tempPath);
							this.mouseAreaControl.Invalidate(new System.Drawing.Rectangle(((int) ef1.X) - 5, ((int) ef1.Y) - 5, ((int) ef1.Width) + 10, ((int) ef1.Height) + 10));
							this.tempPath.Reset();
							this.drawPath = null;
							this.showdialog = false;
							this.filename = string.Empty;
						}
						else if (!flag1)
						{
							if (this.drawPath == null)
							{
								this.drawPath = new GraphicsPath();
							}
							PointF point2 = new Point(e.X, e.Y);
							if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
							{
								float single2 = (((float) Math.Atan2((double) (point2.Y - this.startPoint.Y), (double) (point2.X - this.startPoint.X))) * 180f) / 3.141593f;
								single2 = ((int) Math.Round((double) (single2 / 45f), 0)) * 0x2d;
								if ((single2 == 90f) || (single2 == -90f))
								{
									point2 = new PointF(this.startPoint.X, point2.Y);
								}
								else
								{
									single2 = (float) Math.Tan((single2 / 180f) * 3.1415926535897931);
									point2 = new PointF(point2.X, this.startPoint.Y + ((int) ((point2.X - this.startPoint.X) * single2)));
								}
							}
							this.drawPath.AddLine(this.startPoint.X, this.startPoint.Y, point2.X, point2.Y);
							this.startPoint = new PointF(point2.X, point2.Y);
							this.mouseAreaControl.Invalidate();
							RectangleF ef2 = PathFunc.GetBounds(this.drawPath);
							this.mouseAreaControl.Invalidate(new System.Drawing.Rectangle(((int) ef2.X) - 5, ((int) ef2.Y) - 5, ((int) ef2.Width) + 10, ((int) ef2.Height) + 10));
						}
						else //画圆弧第一个点时发生
						{
							
							this.win32.hdc = this.win32.W32GetDC(this.mouseAreaControl.Handle);
							this.win32.W32SetROP2(7);
							this.win32.W32PolyDraw(this.tempPath);
//							this.tempPath.Reset();
//							this.CreateDrawPath(this.startPoint, new Point(e.X, e.Y), this.CurrentOperation);
//							this.win32.W32PolyDraw(this.tempPath);
							this.win32.ReleaseDC();
						}
						this.mouseAreaControl.Cursor = this.mouseAreaControl.DefaultCursor;
						if (flag1)
						{
							this.secondMouse = !secondMouse;
						}
						else
						{
							this.secondMouse = false;
						}
					}
					
				}
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

		protected IGraph FindGraphByPoint(PointF mousepoint, ref PointF connectpoint,ref int index)
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

								index=num2;
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
		public void OnPaint(PaintEventArgs e)
		{
			if ((this.mouseAreaControl.CurrentOperation != ToolOperation.Bezier) && (this.drawPath != null))
			{
				e.Graphics.DrawPath(Pens.Black, this.drawPath);
			}
			if (CurrentOperation == ToolOperation.ConnectLine)
			{
				if(this.redPath.PointCount>0)//连接线 红色方框
				{
					using (Pen pen2 = new Pen(Color.Red, 3f))
					{
						e.Graphics.DrawPath(pen2, this.redPath);
					}
				}
			}
		}

		private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
		{
			this.showdialog = true;
			this.filename = this.openFileDialog1.FileName;
		}

		public bool ProcessDialogKey(Keys keydate)
		{
			return false;
		}

		public bool Redo()
		{
			return false;
		}

		private void Tip()
		{
			switch (this.CurrentOperation)
			{
				case ToolOperation.AngleRectangle:
                case ToolOperation.InterEnclosurePrint:
				{
					if (Control.MouseButtons != MouseButtons.None)
					{
						if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
						{
							if (!this.tooltips.ContainsKey("shiftdragrect"))
							{
								this.tooltips.Add("shiftdragrect", DrawAreaConfig.GetLabelForName("shiftdragrect").Trim());
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["shiftdragrect"], 1);
							return;
						}
						if (!this.tooltips.ContainsKey("dragrect"))
						{
							this.tooltips.Add("dragrect", DrawAreaConfig.GetLabelForName("dragrect").Trim());
						}
						this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["dragrect"], 1);
						return;
					}
					if (!this.tooltips.ContainsKey("startrect"))
					{
						this.tooltips.Add("startrect", DrawAreaConfig.GetLabelForName("startrect").Trim());
					}
					this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["startrect"], 1);
					return;
				}
				case ToolOperation.Ellipse:
				{
					if (Control.MouseButtons != MouseButtons.None)
					{
						if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
						{
							if (!this.tooltips.ContainsKey("shiftdragellipse"))
							{
								this.tooltips.Add("shiftdragellipse", DrawAreaConfig.GetLabelForName("shiftdragellipse").Trim());
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["shiftdragellipse"], 1);
							return;
						}
						if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
						{
							if (!this.tooltips.ContainsKey("ctrldragellipse"))
							{
								this.tooltips.Add("ctrldragellipse", DrawAreaConfig.GetLabelForName("ctrldragellipse").Trim());
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["ctrldragellipse"], 1);
							return;
						}
						if ((Control.ModifierKeys & (Keys.Control | Keys.Shift)) == (Keys.Control | Keys.Shift))
						{
							if (!this.tooltips.ContainsKey("ctrlshiftdragellipse"))
							{
								this.tooltips.Add("ctrlshiftdragellipse", DrawAreaConfig.GetLabelForName("ctrlshiftdragellipse").Trim());
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["ctrlshiftdragellipse"], 1);
							return;
						}
						if (!this.tooltips.ContainsKey("dragellipse"))
						{
							this.tooltips.Add("dragellipse", DrawAreaConfig.GetLabelForName("dragellipse").Trim());
						}
						this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["dragellipse"], 1);
						return;
					}
					if (!this.tooltips.ContainsKey("startellipse"))
					{
						this.tooltips.Add("startellipse", DrawAreaConfig.GetLabelForName("startellipse").Trim());
					}
					this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["startellipse"], 1);
					return;
				}
				case ToolOperation.Line:
				{
					if (Control.MouseButtons != MouseButtons.None)
					{
						if (Control.MouseButtons == MouseButtons.Left)
						{
							if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
							{
								if (!this.tooltips.ContainsKey("shiftdragline"))
								{
									this.tooltips.Add("shiftdragline", DrawAreaConfig.GetLabelForName("shiftdragline").Trim());
								}
								this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["shiftdragline"], 1);
								return;
							}
							if (!this.tooltips.ContainsKey("dragline"))
							{
								this.tooltips.Add("dragline", DrawAreaConfig.GetLabelForName("dragline").Trim());
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["dragline"], 1);
						}
						return;
					}
					if (!this.tooltips.ContainsKey("startline"))
					{
						this.tooltips.Add("startline", DrawAreaConfig.GetLabelForName("startline").Trim());
					}
					this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["startline"], 1);
					return;
				}
				case ToolOperation.EqualPolygon:
				{
					if (Control.MouseButtons != MouseButtons.None)
					{
						if (Control.MouseButtons == MouseButtons.Left)
						{
							if (!this.tooltips.ContainsKey("dragpolygon"))
							{
								this.tooltips.Add("dragpolygon", DrawAreaConfig.GetLabelForName("dragpolygon").Trim());
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["dragpolygon"], 1);
						}
						return;
					}
					if (!this.tooltips.ContainsKey("startequalpolygon"))
					{
						this.tooltips.Add("startequalpolygon", DrawAreaConfig.GetLabelForName("startequalpolygon").Trim());
					}
					this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["startequalpolygon"], 1);
					return;
				}
				case ToolOperation.Image:
				{
					if (Control.MouseButtons != MouseButtons.None)
					{
						if (Control.MouseButtons == MouseButtons.Left)
						{
							if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
							{
								if (!this.tooltips.ContainsKey("shiftdragimage"))
								{
									this.tooltips.Add("shiftdragimage", DrawAreaConfig.GetLabelForName("shiftdragimage").Trim());
								}
								this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["shiftdragimage"], 1);
								return;
							}
							if (!this.tooltips.ContainsKey("dragimage"))
							{
								this.tooltips.Add("dragimage", DrawAreaConfig.GetLabelForName("dragimage").Trim());
							}
							this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["dragimage"], 1);
						}
						return;
					}
					if (!this.tooltips.ContainsKey("startimage"))
					{
						this.tooltips.Add("startimage", DrawAreaConfig.GetLabelForName("startimage").Trim());
					}
					this.mouseAreaControl.PicturePanel.ToolTip((string) this.tooltips["startimage"], 1);
					return;
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

		public ToolOperation CurrentOperation
		{
			get
			{
				return this.mouseAreaControl.CurrentOperation;
			}
		}


		// Fields
		private GraphicsPath drawPath;
		private string filename;
		public bool finish;
		private bool first;
		private bool haveTextActive;
		private ArrayList list;
		private MouseArea mouseAreaControl;
		private bool mouseDown;
		private OpenFileDialog openFileDialog1;
		private GraphicsPath reversePath;
		private bool showdialog;
		private PointF startPoint;
		private GraphicsPath tempPath;
		private GraphicsPath redPath;
		private Hashtable tooltips;
		private Win32 win32;
		private RectangleF pieRect;
		private bool secondMouse;
		private float startArcAngle;
		private float endArcAngle;
		private PointF lastPoint;
		private IGraph connectBegin;
		private IGraph connectEnd;
		private int connectBeginIndex;
		private int connectEndIndex;

	}
}

