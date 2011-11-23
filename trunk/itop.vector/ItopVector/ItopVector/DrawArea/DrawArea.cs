using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ItopVector.Core;
using ItopVector.Core.Document;
using ItopVector.Core.Figure;
using ItopVector.Core.Func;
using ItopVector.Core.Interface;
using ItopVector.Core.Interface.Figure;
using ItopVector.Core.Interface.Paint;
using ItopVector.Core.Paint;
using ItopVector.Core.Types;
using ItopVector.Dialog;
using ItopVector.Resource;
using System.Drawing.Imaging;

namespace ItopVector.DrawArea
{
	// using ItopVector.CommonControl.Menu;
	//using ItopVector.Core.Animate;

	[ToolboxItem(false)]
	public class DrawArea : UserControl, IControl, IItopVector,IMouseEvent
	{
		// Events
		public event EventHandler GraphChanged;
		public event OnTipEventHandler OnTipEvent;
		public event TrackPopupEventHandler OnTrackPopup;
		public event EventHandler OperationChanged;
		public event PostBrushEventHandler PostBrushEvent;
		public event EventHandler ScaleChanged;

		public event PaintMapEventHandler PaintMap; 
	
		public event PaintMapEventHandler AfterPaintPage;

		public event PaintMapEventHandler OnBeforeRenderTo;

		public event AddSvgElementEventHandler BeforeAddSvgElement;

        public event PolyLineBreakEventHandler OnPolyLineBreak;

        public event ElementMoveEventHandler OnElementMove;

        public event AddSvgElementEventHandler OnAddElement;

        public event MouseEventHandler OnMouseMove;

        public event MouseEventHandler OnMouseDown;

        public event MouseEventHandler OnMouseUp;

		// Methods
		public DrawArea()
		{
			this.components = null;
			this.mouseAreaControl = null;
			this.pageSetting = new PageSettings();
			this.drawMode = DrawModeType.ScreenImage;
			this.virtualLeft = 0f;
			this.virtualTop = 0f;
			this.scale = 1f;
			this.coordTransform = new Matrix();
			this.viewSize = new SizeF(500f, 500f);
			this.showGrid = true;
			this.showRule = true;
			this.showGuides = false;
			this.lockGuides = false;
			this.gridSize = new SizeF(10f, 10f);
			this.firstload = true;
			this.operation = ToolOperation.None;
            
			this.centerPoint = PointF.Empty;
			this.oldpoint = Point.Empty;
			this.prepathchanged = false;
			this.preGraph = null;
			this.UpdateRule = true;
			this.RefLines = new ArrayList(0x10);
			this.SnapToGrid = false;
			this.SnapToGuides = false;
			this.PreTextSelect = new SvgElementCollection();
			this.InitializeComponent();
			this.viewer.drawArea = this;
			this.margin = Size.Round(this.viewer.margin);
			this.mouseAreaControl.Focus();
			this.mouseAreaControl.PicturePanel = this;
			this.mouseAreaControl.DragAndDrop+=new DragEventHandler(mouseAreaControl_DragDrop);
			this.FillBrush = new SolidColor(Color.White);
			this.stroke = new Stroke(new SolidColor(Color.Black));
			this.backColor = new SolidBrush(Color.White);
			this.RatTextStyle = new ItopVector.Struct.TextStyle("宋体", 12, false, false, false);
			base.MouseWheel += new MouseEventHandler(this.DealMouseWheel);
			addedElements=new SvgElementCollection();
			this.viewer.ViewChanged +=new ViewChangedEventHandler(viewer_ViewChanged);
			this.mouseAreaControl.BeforeDragDrop+=new DragEventHandler(DrawArea_BeforeDragDrop);
            this.mouseAreaControl.OnPolyLineBreak += new PolyLineBreakEventHandler(mouseAreaControl_OnPolyLineBreak);
            this.mouseAreaControl.OnAddElement += new AddSvgElementEventHandler(mouseAreaControl_OnAddElement);
            this.mouseAreaControl.MouseMove += new MouseEventHandler(mouseAreaControl_MouseMove);
            this.mouseAreaControl.MouseDown += new MouseEventHandler(mouseAreaControl_MouseDown);
            this.mouseAreaControl.MouseUp += new MouseEventHandler(mouseAreaControl_MouseUp);
            this.mouseAreaControl.OnElementMove += new ElementMoveEventHandler(mouseAreaControl_OnElementMove);
            
            //            base.SetStyle(ControlStyles.DoubleBuffer | (ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint), true);
		}

        void mouseAreaControl_OnElementMove(object sender, MoveEventArgs e)
        {
           if (OnElementMove!=null)
           {
               OnElementMove(sender, e);
           }
        } 

        void mouseAreaControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (OnMouseUp!=null)
            {
                OnMouseUp(sender,e);                
            }
            
        }
        void mouseAreaControl_MouseDown(object sender, MouseEventArgs e)
        {
            if(OnMouseDown!=null)
            {
                OnMouseDown(sender,e);
            }
        }

        void mouseAreaControl_MouseMove(object sender, MouseEventArgs e)
        {
           if (OnMouseMove!=null)
           {
               OnMouseMove(sender,e);
           }
        }

        void mouseAreaControl_OnAddElement(object sender, AddSvgElementEventArgs e)
        {
            if (OnAddElement!=null)
            {                
                OnAddElement(sender,e);
            }
        }

        void mouseAreaControl_OnPolyLineBreak(object sender, BreakElementEventArgs e)
        {
            if (OnPolyLineBreak!=null)
            {
                OnPolyLineBreak(sender,e);
            }
        }

        void polyOperation_OnPolyLineBreak(object sender, BreakElementEventArgs e)
        {
            if (OnPolyLineBreak!=null)
            {
                OnPolyLineBreak(sender,e);
            }
        }
        /// <summary>
        /// 单击增加预定义Symbol
        /// </summary>
        /// <param name="element"></param>
        /// <param name="p1"></param>
		public void AddSymbol(SvgElement element,Point p1)
		{
			SvgDocument document1 = this.SVGDocument;
			IGraph graph1 =(IGraph)element;
			if (graph1==null)return;
			if (graph1 is ConnectLine)
			{
				Point point2 = p1;
						
				ConnectLine connect =document1.ImportNode((SvgElement)graph1,true) as ConnectLine;
		

				connect.X1 += point2.X;
				connect.X2 += point2.X;
				connect.Y1 += point2.Y;
				connect.Y2 += point2.Y;
				this.AddElement(connect);
			}
			else
			{
				IGraph graph2;	
				if (!document1.DefsElementContains((SvgElement) graph1,out graph2))
				{
					if((graph1 as SvgElement).ParentNode is State)
					{
						State state =(graph1 as SvgElement).ParentNode.Clone() as State;
						foreach(SvgElement element1 in state.ChildList)
						{
							element1.Attributes.RemoveNamedItem("visibility");//删除可能有的隐藏属性
						}
						document1.AddDefsElement(state);
					}							
					else 
					{						
						graph2 = document1.AddDefsElement((SvgElement) graph1) as IGraph;						
					}		
				}

				if (graph2!=null)graph1=graph2;
				Use use1 = (Use) document1.CreateElement(document1.Prefix, "use", document1.NamespaceURI);
				use1.GraphId = graph1.ID;
				Point point1 = p1;
				RectangleF ef1 = graph1.GPath.GetBounds(graph1.Transform.Matrix);
                float X = (point1.X - ef1.X) - (ef1.Width / 2f);
                float Y = (point1.Y - ef1.Y) - (ef1.Height / 2f);
                Transf tf = new Transf();
                tf.Matrix.Translate(X, Y);
                use1.Transform = tf; 
                //use1.X = (point1.X - ef1.X) - (ef1.Width / 2f);
                //use1.Y = (point1.Y - ef1.Y) - (ef1.Height/2f);
				use1.GraphBrush = this.FillBrush.Clone();
				use1.GraphStroke = this.Stroke.Clone() as Stroke;
				this.AddElement(use1);
			}
		}
		public SvgElement AddElement(ISvgElement mypath)
		{
			if (BeforeAddSvgElement!=null)
			{
				AddSvgElementEventArgs e = new AddSvgElementEventArgs(mypath);
				BeforeAddSvgElement(this,e);
				if (e.Cancel)return null;
			} 
			AttributeFunc.SetAttributeValue((XmlElement)mypath,"layer",SvgDocument.currentLayer);
			XmlNode node1 = this.svgDocument.RootElement;
			XmlNode newNode =null;
			if (!this.svgDocument.OnlyShowCurrent)
			{
				node1 = this.svgDocument.DocumentElement;
			}
			if (!(node1 is ContainerElement))
			{
				MessageBox.Show(DrawAreaConfig.GetLabelForName("addelement").Trim());
			}
			else
			{
				if (this.svgDocument.SelectCollection.Count > 0)
				{
					SvgElement element1 = (SvgElement) this.svgDocument.SelectCollection[0];
					if (this.svgDocument.FlowChilds.IndexOf(element1) >= 0)
					{
						for (node1 = element1; (!(node1 is ItopVector.Core.Interface.IContainer) || (node1 is Text)) && (node1 != null); node1 = node1.ParentNode)
						{
						}
					}
				}
				if (node1 == null)
				{
					node1 = this.svgDocument.RootElement;
				}
				Matrix matrix1 = new Matrix();
				if (node1 is IGraph)
				{
					matrix1 = ((IGraph) node1).GraphTransform.Matrix.Clone();
					Matrix matrix2 = this.coordTransform.Clone();
					matrix2.Invert();
					matrix1.Multiply(matrix2, MatrixOrder.Append);
				}
				matrix1.Invert();
				matrix1 = TransformFunc.RoundMatrix(matrix1, 2);
				bool flag1 = this.svgDocument.AcceptChanges;
				//				this.SVGDocument.AcceptChanges = false;
				this.svgDocument.AcceptChanges = true;
				if (mypath is IGraphPath)
				{
					ISvgBrush brush1 = ((IGraphPath) mypath).GraphBrush;
					if ((brush1 is ITransformBrush) && (((SvgElement) brush1).ParentNode == null))
					{
						bool flag2 = this.svgDocument.AcceptChanges;
						this.svgDocument.AcceptChanges = true;
						this.svgDocument.NumberOfUndoOperations++;
						XmlNode node2 = this.SVGDocument.AddDefsElement((SvgElement) brush1);
						this.svgDocument.AcceptChanges = false;
						if (node2 is ITransformBrush)
						{
							string text1 = ((SvgElement) node2).ID;
							AttributeFunc.SetAttributeValue((SvgElement) mypath, "fill", "url(#" + text1 + ")");
						}
						this.svgDocument.AcceptChanges = flag2;
					}
					brush1 = ((IGraphPath) mypath).GraphStroke.Brush;
					if ((brush1 is ITransformBrush) && (((SvgElement) brush1).ParentNode == null))
					{
						bool flag3 = this.svgDocument.AcceptChanges;
						this.svgDocument.AcceptChanges = true;
						this.svgDocument.NumberOfUndoOperations++;
						XmlNode node3 = this.SVGDocument.AddDefsElement((SvgElement) brush1);
						this.svgDocument.AcceptChanges = false;
						if (node3 is ITransformBrush)
						{
							string text2 = ((SvgElement) node3).ID;
							AttributeFunc.SetAttributeValue((SvgElement) mypath, "stroke", "url(#" + text2 + ")");
						}
						this.svgDocument.AcceptChanges = flag3;
					}
				}
				if (!matrix1.IsIdentity && (mypath is IGraph))
				{
					bool flag4 = this.svgDocument.AcceptChanges;
					this.svgDocument.AcceptChanges = false;
					Matrix matrix3 = ((IGraph) mypath).Transform.Matrix.Clone();
					matrix1.Multiply(matrix3);
					Transf transf1 = new Transf();
					transf1.setMatrix(matrix1);
					AttributeFunc.SetAttributeValue((SvgElement) mypath, "transform", transf1.ToString());
					this.svgDocument.AcceptChanges = flag4;
				}
				if (((SvgElement) mypath).ParentNode != node1)
				{
					if (((ContainerElement) node1).IsValidChild((SvgElement) mypath))
					{
						//						node1.AppendChild((SvgElement) mypath);
						SvgElement element1 =(SvgElement) mypath;//(SvgElement)this.svgDocument.ImportNode((SvgElement) mypath,true);
						newNode = node1.AppendChild(element1);
						this.svgDocument.Render(element1);
						if(this.inserting)
						{
							this.addedElements.Add(element1);
                            if(OnAddElement!=null)
                            {
                                 AddSvgElementEventArgs e = new AddSvgElementEventArgs(element1);
                                OnAddElement(element1,e);
                            }
						}
						else
						{
							this.svgDocument.CurrentElement = element1;
						}
					}
					else
					{
						MessageBox.Show(DrawAreaConfig.GetLabelForName("addelement").Trim());
					}
				}
				this.SVGDocument.AcceptChanges = flag1;
			}
			return newNode!=null?newNode as SvgElement:null;
		}

		private float Approach(float scale)
		{
			float single1 = 100f/scale;
			//			int num1 = (int) single1;
			float single2 = (int) Math.Log10((double) single1);
			float single3 = (float) (((double) single1)/Math.Pow(10, (double) single2));
			float single4 = 1f;
			if ((single3 >= 2f) && (single3 < 5f))
			{
				single4 = 2f;
			}
			else if ((single3 >= 5f) && (single3 < 10f))
			{
				single4 = 5f;
			}
			else if (single3 == 10f)
			{
				single4 = 10f;
			}
			single4 = (float) (single4*Math.Pow(10, (double) ((int) single2)));
			return (float) ((int) single4);
		}

		public void AttachProperty()
		{
			if (File.Exists(Application.StartupPath + @"\Preference\preference.xml"))
			{
				try
				{
					XmlDocument document1 = new XmlDocument();
					document1.Load(Application.StartupPath + @"\Preference\preference.xml");
					XmlNode node1 = document1.DocumentElement.SelectSingleNode("//*[@id='ShowRule']");
					if (node1 != null)
					{
						this.ShowRule = node1.Attributes["Value"].Value == "true";
					}
					node1 = document1.DocumentElement.SelectSingleNode("//*[@id='HighQuality']");
					if (node1 != null)
					{
						svgDocument.SmoothingMode = (node1.Attributes["Value"].Value.Trim() == "true") ? SmoothingMode.HighQuality : SmoothingMode.HighSpeed;
					}
					node1 = document1.DocumentElement.SelectSingleNode("//*[@id='ConnectPoints']");
					if (node1 != null)
					{
						svgDocument.ShowConnectPoints = node1.Attributes["Value"].Value.Trim() == "true";
					}

					node1 = document1.DocumentElement.SelectSingleNode("//*[@id='ShowGrid']");
					if (node1 != null)
					{
						this.ShowGrid = node1.Attributes["Value"].Value == "true";
					}
					node1 = document1.DocumentElement.SelectSingleNode("//*[@id='SnapToGrid']");
					if (node1 != null)
					{
						string text1 = node1.Attributes["Value"].Value.Trim();
						this.SnapToGrid = text1 == "true";
					}
					node1 = document1.DocumentElement.SelectSingleNode("//*[@id='ShowGuide']");
					if (node1 != null)
					{
						this.ShowGuides = node1.Attributes["Value"].Value == "true";
					}
					node1 = document1.DocumentElement.SelectSingleNode("//*[@id='LockGuide']");
					if (node1 != null)
					{
						this.lockGuides = node1.Attributes["Value"].Value == "true";
					}
					node1 = document1.DocumentElement.SelectSingleNode("//*[@id='GridHeight']");
					if (node1 != null)
					{
						try
						{
							float single1 = ItopVector.Core.Func.Number.parseToFloat(node1.Attributes["Value"].Value, null, ItopVector.Core.Func.SvgLengthDirection.Horizontal);
							this.gridSize.Height = single1;
						}
						catch (Exception)
						{
						}
					}
					node1 = document1.DocumentElement.SelectSingleNode("//*[@id='GridWidth']");
					if (node1 != null)
					{
						try
						{
							float single2 = ItopVector.Core.Func.Number.parseToFloat(node1.Attributes["Value"].Value, null, ItopVector.Core.Func.SvgLengthDirection.Horizontal);
							this.gridSize.Width = single2;
						}
						catch (Exception)
						{
						}
					}
				}
				catch (Exception)
				{
				}
			}
			base.Invalidate();
		}

		private void Break(SvgElementCollection list)
		{
			bool flag1 = this.svgDocument.AcceptChanges;
			this.svgDocument.AcceptChanges = true;
			SvgElementCollection collection1 = new SvgElementCollection();
			SvgElementCollection.ISvgElementEnumerator enumerator1 = list.GetEnumerator();
			while (enumerator1.MoveNext())
			{
				SvgElement element1 = (SvgElement) enumerator1.Current;
				if ((element1 is Group) && (element1 != this.svgDocument.DocumentElement))
				{
					collection1.Add(element1);
				}
			}
			this.svgDocument.NumberOfUndoOperations += (collection1.Count + 0x7d0);
			SvgElementCollection.ISvgElementEnumerator enumerator2 = collection1.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				SvgElement element2 = (SvgElement) enumerator2.Current;
				XmlNode node1 = element2.NextSibling;
				goto Label_009B;
			Label_009B:
				if (!(node1 is SvgElement) && (node1 != null))
				{
					node1 = node1.NextSibling;
					goto Label_009B;
				}
				this.Break(element2, (SvgElement) element2.ParentNode, node1);
			}
			this.svgDocument.NotifyUndo();
			this.svgDocument.AcceptChanges = flag1;
		}

		private int Break(SvgElement graph, SvgElement parent, XmlNode next)
		{
			if (graph is Group)
			{
				if (graph is Text)
				{
					Graphics graphics1 = base.CreateGraphics();
					SvgElementCollection collection1 = ((Text) graph).Break(graphics1);
					graphics1.Dispose();
					SvgElementCollection.ISvgElementEnumerator enumerator1 = collection1.GetEnumerator();
					while (enumerator1.MoveNext())
					{
						SvgElement element1 = (SvgElement) enumerator1.Current;
						this.svgDocument.NumberOfUndoOperations++;
						element1.ID = CodeFunc.CreateString(this.svgDocument, "text");
						ISvgBrush brush1 = ((IGraphPath) element1).GraphBrush;
						if ((brush1 is ITransformBrush) && (((SvgElement) brush1).ParentNode == null))
						{
							string text1 = CodeFunc.CreateString(this.svgDocument, ((SvgElement) brush1).Name);
							AttributeFunc.SetAttributeValue(element1, "fill", "url(#" + text1 + ")");
							AttributeFunc.SetAttributeValue((SvgElement) brush1, "id", text1);
							this.svgDocument.NumberOfUndoOperations++;
							this.SVGDocument.AddDefsElement((SvgElement) brush1);
						}
						brush1 = ((IGraphPath) element1).GraphStroke.Brush;
						if ((brush1 is ITransformBrush) && (((SvgElement) brush1).ParentNode == null))
						{
							string text2 = CodeFunc.CreateString(this.svgDocument, ((SvgElement) brush1).Name);
							AttributeFunc.SetAttributeValue(element1, "stroke", "url(#" + text2 + ")");
							AttributeFunc.SetAttributeValue((SvgElement) brush1, "id", text2);
							this.svgDocument.NumberOfUndoOperations++;
							this.SVGDocument.AddDefsElement((SvgElement) brush1);
						}
						if (next == null)
						{
							parent.AppendChild(element1);
							continue;
						}
						parent.InsertBefore(element1, next);
					}
					this.svgDocument.NumberOfUndoOperations++;
					graph.ParentNode.RemoveChild(graph);
				}
				else
				{
					SvgElementCollection collection2 = ((Group) graph).GraphList.Clone();
					for (int num1 = collection2.Count - 1; num1 >= 0; num1--)
					{
						SvgElement element2 = (SvgElement) collection2[num1];
						Matrix matrix1 = new Matrix();
						Matrix matrix2 = new Matrix();
						if (element2 is IGraph)
						{
							if (graph.SvgAttributes.ContainsKey("transform"))
							{
								matrix1 = ((Matrix) graph.SvgAttributes["transform"]).Clone();
							}
							if (element2.SvgAttributes.ContainsKey("transform"))
							{
								matrix2 = ((Matrix) graph.SvgAttributes["transform"]).Clone();
							}
							matrix1.Multiply(matrix2);
							Transf transf1 = new Transf();
							transf1.setMatrix(matrix1);
							AttributeFunc.SetAttributeValue(element2, "transform", transf1.ToString());
						}
						this.Break(element2, parent, next);
					}
					graph.ParentNode.RemoveChild(graph);
				}
				return 1;
			}
			if ((graph is IGraph) && (graph.ParentNode != parent))
			{
				this.svgDocument.NumberOfUndoOperations += 2;
				if (next != null)
				{
					parent.InsertBefore(graph.Clone(), next);
				}
				else
				{
					parent.PrependChild(graph.Clone());
				}
				return 0;
			}
			return 0;
		}

		public void ChangeElementMatrix(float scalex, float scaley, float rotate, float skewx, float skewy)
		{
			PointF tf1 = this.mouseAreaControl.CenterPoint;
			scalex = (float) Math.Round((double) scalex, 2);
			scaley = (float) Math.Round((double) scaley, 2);
			rotate = (float) Math.Round((double) rotate, 2);
			skewx = (float) Math.Round((double) skewx, 2);
			skewy = (float) Math.Round((double) skewy, 2);
			bool flag1 = this.svgDocument.AcceptChanges;
			this.svgDocument.AcceptChanges = true;
			this.svgDocument.NumberOfUndoOperations = this.svgDocument.SelectCollection.Count*40;
			if (this.svgDocument.RecordAnim)
			{
			}
			else
			{
				Transf transf1 = new Transf();
				tf1 = this.PointToView(this.mouseAreaControl.CenterPoint);
				transf1.setTranslate(tf1.X, tf1.Y);
				transf1.setScale(scalex, scaley);
				transf1.setRotate(rotate);
				transf1.setSkewX(skewx);
				transf1.setSkewX(skewy);
				transf1.setTranslate(-tf1.X, -tf1.Y);
				Matrix matrix2 = transf1.Matrix;
				SvgElementCollection.ISvgElementEnumerator enumerator2 = this.svgDocument.SelectCollection.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					SvgElement element2 = (SvgElement) enumerator2.Current;
					if (element2 is IGraph)
					{
						IGraph graph1 = (IGraph) element2;
						Matrix matrix3 = graph1.Transform.Matrix.Clone();
						matrix3.Multiply(matrix2, MatrixOrder.Append);
						Transf transf2 = new Transf();
						transf2.setMatrix(matrix3);
						AttributeFunc.SetAttributeValue(element2, "transform", transf2.ToString());
					}
				}
			}
			SvgElementCollection collection1 = this.svgDocument.SelectCollection.Clone();
			this.svgDocument.SelectCollection.Clear();
			this.svgDocument.SelectCollection.AddRange(collection1);
			this.svgDocument.NotifyUndo();
			this.svgDocument.AcceptChanges = flag1;
		}

		private void ChangeInKey(object sender, EventArgs e)
		{
		}


		private void ChangeSelect(object sender, CollectionChangedEventArgs e)
		{
			if(this.SVGDocument == null)return;
			SvgElement element1 = this.SVGDocument.CurrentElement;
			if (this.mouseAreaControl.SubOperation != null)
			{
				if (element1 is IPath)
				{
					this.mouseAreaControl.SubOperation.CurrentGraph = (IPath) element1;
				}
				else
				{
					this.mouseAreaControl.SubOperation.CurrentGraph = null;
				}
			}
			else if (this.mouseAreaControl.editingOperation is BezierOperation)
			{
				this.mouseAreaControl.BezierOperation.CurrentGraph = null;

			}
			else if (this.mouseAreaControl.editingOperation is PolyOperation)
			{
				this.mouseAreaControl.polyOperation.CurrentGraph = null;
			}
			else if (this.mouseAreaControl.editingOperation is Line)
			{
				this.mouseAreaControl.lineOperation.CurrentGraph = null;
			}
			//this.ChangeSelectEx(sender,e);
		}

		/// <summary>
		/// 
		/// </summary>
		public void CreatePreGraph()
		{
			switch (this.Operation)
			{
                case ToolOperation.InterEnclosurePrint:
                case ToolOperation.Rectangle:
                case ToolOperation.AngleRectangle:
                {
                    if (!(this.prepreGraph is ItopVector.Core.Figure.RectangleElement))
                    {
                        this.preGraph = (ItopVector.Core.Figure.RectangleElement)this.svgDocument.CreateElement(this.svgDocument.Prefix, "rect", this.svgDocument.NamespaceURI);
                        break;
                    }
                    this.preGraph = this.prepreGraph;
                    break;
                }                 
				case ToolOperation.Circle:
				{
					if (!(this.prepreGraph is Circle))
					{
						this.preGraph = (Circle) this.svgDocument.CreateElement(this.svgDocument.Prefix, "circle", this.svgDocument.NamespaceURI);
						goto Label_042D;
					}
					this.preGraph = this.prepreGraph;
					goto Label_042D;
				}
				case ToolOperation.Ellipse:				
				{
					if (!(this.prepreGraph is Ellips))
					{
						this.preGraph = (Ellips) this.svgDocument.CreateElement(this.svgDocument.Prefix, "ellipse", this.svgDocument.NamespaceURI);
						goto Label_042D;
					}
					this.preGraph = this.prepreGraph;
					goto Label_042D;
				}
				case ToolOperation.Line:
				{
					if (!(this.prepreGraph is Line))
					{
						this.preGraph = (Graph) this.svgDocument.CreateElement(this.svgDocument.Prefix, "line", this.svgDocument.NamespaceURI);
						goto Label_042D;
					}
					this.preGraph = this.prepreGraph;
					goto Label_042D;
				}
				case ToolOperation.ConnectLine://连接线
				{
					if (!(this.prepreGraph is ConnectLine))
					{
						this.preGraph = (Graph) this.svgDocument.CreateElement(this.svgDocument.Prefix, "connectline", this.svgDocument.NamespaceURI);
						(this.preGraph as Graph).SetAttribute("type","line");
						goto Label_042D;
					}
					this.preGraph = this.prepreGraph;
					goto Label_042D;
				}
				case ToolOperation.PolyLine:                
				case ToolOperation.XPolyLine:
				case ToolOperation.YPolyLine:
				case ToolOperation.LeadLine:
				case ToolOperation.FreeLines:
				case ToolOperation.Confines_GuoJie:
				case ToolOperation.Confines_ShengJie:
				case ToolOperation.Confines_ShiJie:
				case ToolOperation.Confines_XianJie:
				case ToolOperation.Confines_XiangJie:
				case ToolOperation.Railroad:
				{
					if (!(this.prepreGraph is Polyline))
					{
						this.preGraph = (Graph) this.svgDocument.CreateElement(this.svgDocument.Prefix, "polyline", this.svgDocument.NamespaceURI);
						goto Label_042D;
					}
					this.preGraph = this.prepreGraph;
					goto Label_042D;
				}              
				case ToolOperation.Polygon:
				case ToolOperation.Enclosure:
                case ToolOperation.InterEnclosure:
				case ToolOperation.AreaPolygon:
				{
					if (!(this.prepreGraph is Polygon))
					{
						this.preGraph = (Graph) this.svgDocument.CreateElement(this.svgDocument.Prefix, "polygon", this.SVGDocument.NamespaceURI);
                        goto Label_042D;                                                                                                      
					}
					this.preGraph = this.prepreGraph;
					goto Label_042D;
				}
				case ToolOperation.EqualPolygon:
				{
					this.preGraph = (Graph) this.svgDocument.CreateElement(this.svgDocument.Prefix, "polygon", this.svgDocument.NamespaceURI);
					((Polygon) this.preGraph).LineCount = LineCount; // (int) this.LinesUpDown.Value;
					((Polygon) this.preGraph).Indent = Indent; //(float) this.indentUpDown1.Value;
					goto Label_042D;
				}
				case ToolOperation.Bezier:
				case ToolOperation.Pie:
				case ToolOperation.Arc:
				{
					bool flag1 = false;
					if (((this.prepreGraph is GraphPath)) && (((SvgElement) this.prepreGraph).Name == "path"))
					{
						this.preGraph = this.prepreGraph;
						flag1 = true;
					}
					if (!flag1)
					{
						this.preGraph = (Graph) this.svgDocument.CreateElement(this.svgDocument.Prefix, "path", this.svgDocument.NamespaceURI);
					}
					goto Label_042D;
				}
				case ToolOperation.Text:
				{
					if (!(this.prepreGraph is Text))
					{
						this.preGraph = (Text) this.svgDocument.CreateElement(this.svgDocument.Prefix, "text", this.svgDocument.NamespaceURI);

						goto Label_042D;
					}

					this.preGraph = this.prepreGraph;
					goto Label_042D;
				}
				case ToolOperation.Image:
				{
					this.preGraph = (Graph) this.svgDocument.CreateElement(this.svgDocument.Prefix, "image", this.svgDocument.NamespaceURI);
					goto Label_042D;
				}
				case ToolOperation.PreShape:
				{
					if (this.svgDocument != null)
					{
						this.preGraph = new GraphPath("", "path", "", this.svgDocument);
						((GraphPath) this.preGraph).SetAttribute("d", this.shapePathString);
					}
					goto Label_042D;
				}
				default:
				{
					this.preGraph = null;
					goto Label_042D;
				}
			}
			Label_042D:
				this.prepreGraph = this.preGraph;

			if (this.preGraph is IGraphPath)
			{
				
				this.svgDocument.AcceptChanges=false;
				if(this.Operation==ToolOperation.AreaPolygon)
				{
					AttributeFunc.SetAttributeValue((XmlElement)preGraph,"IsArea","1");
					return;
				}
				if(this.Operation==ToolOperation.LeadLine)
				{
					AttributeFunc.SetAttributeValue((XmlElement)preGraph,"IsLead","1");
					return;
				}
				if(this.Operation==ToolOperation.XPolyLine || this.Operation==ToolOperation.YPolyLine)
				{
					AttributeFunc.SetAttributeValue((XmlElement)preGraph,"style","fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;stroke-width:4;");
					return;
				}
				if(this.Operation==ToolOperation.Enclosure|| this.Operation==ToolOperation.InterEnclosure ||this.operation==ToolOperation.InterEnclosurePrint)
				{
					//					((IGraphPath) preGraph).GraphBrush =new SolidColor(Color.Empty);
					//((IGraphPath) preGraph).GraphStroke.StrokeColor=Color.Red;
					//AttributeFunc.SetAttributeValue((XmlElement)preGraph,"stroke","#FF8081");
					//AttributeFunc.SetAttributeValue((XmlElement)preGraph,"stroke-width","2");
                    AttributeFunc.SetAttributeValue((XmlElement)preGraph,"style", "fill:#C0C0FF;fill-opacity:0.3;stroke:#000000;stroke-opacity:1;");
					return;
				}
				if(Operation== ToolOperation.Confines_GuoJie)
				{	
					((IGraphPath) preGraph).GraphBrush =new SolidColor(Color.Empty);
					AttributeFunc.SetAttributeValue((XmlElement)preGraph,"style","fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;stroke-width:4;stroke-dasharray:10 3 3 3;");
					return;
				}
				if(Operation==  ToolOperation.Confines_ShengJie)
				{
					((IGraphPath) preGraph).GraphBrush =new SolidColor(Color.Empty);
					AttributeFunc.SetAttributeValue((XmlElement)preGraph,"style","fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;stroke-dasharray:10 3 3 3;stroke-width:3;");
					return;
				}
				if(Operation== ToolOperation.Confines_ShiJie)
				{ 
					((IGraphPath) preGraph).GraphBrush =new SolidColor(Color.Empty);
					AttributeFunc.SetAttributeValue((XmlElement)preGraph,"style","fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;stroke-width:3;stroke-dasharray:8 3 8 ;");
					return;
				}
				if(Operation== ToolOperation.Confines_XianJie)
				{
					((IGraphPath) preGraph).GraphBrush =new SolidColor(Color.Empty);
					AttributeFunc.SetAttributeValue((XmlElement)preGraph,"style","fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;stroke-width:2.5;stroke-dasharray:10 3 3 3;");
					return;
				}
				if(Operation== ToolOperation.Confines_XiangJie)
				{
					((IGraphPath) preGraph).GraphBrush =new SolidColor(Color.Empty);
					AttributeFunc.SetAttributeValue((XmlElement)preGraph,"style","fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;stroke-width:2;stroke-dasharray:6 2 6;");
					return;
				}
				if(Operation== ToolOperation.Railroad)
				{
					((IGraphPath) preGraph).GraphBrush =new SolidColor(Color.Empty);
					AttributeFunc.SetAttributeValue((XmlElement)preGraph,"style","fill:#FFFFFF;fill-opacity:1;stroke:#000000;stroke-opacity:1;stroke-width:3;stroke-dasharray:5 5;");
					return;
				}
				else 
				{
					((IGraphPath) preGraph).GraphBrush = this.FillBrush.Clone();
					((IGraphPath) preGraph).GraphStroke = this.Stroke.Clone() as Stroke;
				}
				
			}
			if (this.preGraph is Text)
			{
				//				((Text)this.preGraph).FontName=this.RatTextStyle.FontName;
				//				((Text)this.preGraph).Size=this.RatTextStyle.Size;				
				this.svgDocument.AcceptChanges=false;
				AttributeFunc.SetAttributeValue((SvgElement) preGraph, "font-family", this.RatTextStyle.FontName);
				AttributeFunc.SetAttributeValue((SvgElement) preGraph, "font-size", this.RatTextStyle.Size.ToString());
				if (this.RatTextStyle.Bold)
					AttributeFunc.SetAttributeValue((SvgElement) preGraph, "font-weight", "bold");
				if (this.RatTextStyle.Italic)
					AttributeFunc.SetAttributeValue((SvgElement) preGraph, "font-style", "italic");
				if (this.RatTextStyle.Underline)
					AttributeFunc.SetAttributeValue((SvgElement) preGraph, "text-decoration", "underline");
			}
			//			if(svgDocument!=null)
			//			{
			////				svgDocument.CurrentElement=(SvgElement)preGraph;
			//				svgDocument.SelectCollection.Clear();
			//				svgDocument.SelectCollection.Add(preGraph);
			//			}
		}

		public void CreateRef(Point screen, bool hori, bool post)
		{
			if (!post)
			{
				Win32 win1 = this.mouseAreaControl.win32;
				win1.hdc = win1.W32GetDC(this.mouseAreaControl.Handle);
				win1.W32SetROP2(7);
				GraphicsPath path1 = new GraphicsPath();
				if (hori)
				{
					path1.AddLine(0, this.oldpoint.Y, base.Width, this.oldpoint.Y);
				}
				else
				{
					path1.AddLine(this.oldpoint.X, 0, this.oldpoint.X, base.Height);
				}
				win1.W32PolyDraw(path1);
				Point point1 = this.PointToView(this.mouseAreaControl.PointToClient(screen));
				if (this.SnapToGrid)
				{
					int num1 = (int) ((point1.X + (this.gridSize.Width/2f))/this.gridSize.Width);
					int num2 = (int) ((point1.Y + (this.gridSize.Height/2f))/this.gridSize.Height);
					point1 = new Point((int) (num1*this.gridSize.Width), (int) (this.gridSize.Height*num2));
				}
				this.oldpoint = Point.Round(this.PointToSystem(new PointF((float) point1.X, (float) point1.Y)));
				path1.Reset();
				if (hori)
				{
					path1.AddLine(0, this.oldpoint.Y, base.Width, this.oldpoint.Y);
				}
				else
				{
					path1.AddLine(this.oldpoint.X, 0, this.oldpoint.X, base.Height);
				}
				win1.W32PolyDraw(path1);
				win1.ReleaseDC();
				path1.Dispose();
			}
			else
			{
				RefLine line1;
				GraphicsPath path2 = new GraphicsPath();
				if (hori)
				{
					path2.AddLine(0, this.oldpoint.Y, base.Width, this.oldpoint.Y);
				}
				else
				{
					path2.AddLine(this.oldpoint.X, 0, this.oldpoint.X, base.Height);
				}
				Win32 win2 = this.mouseAreaControl.win32;
				win2.hdc = win2.W32GetDC(this.mouseAreaControl.Handle);
				win2.W32SetROP2(7);
				win2.W32PolyDraw(path2);

				win2.ReleaseDC();
				path2.Dispose();
				this.oldpoint = Point.Empty;
				Point point2 = this.PointToView(this.mouseAreaControl.PointToClient(screen));
				if (this.SnapToGrid)
				{
					int num3 = (int) ((point2.X + (this.gridSize.Width/2f))/this.gridSize.Width);
					int num4 = (int) ((point2.Y + (this.gridSize.Height/2f))/this.gridSize.Height);
					point2 = new Point((int) (num3*this.gridSize.Width), (int) (this.gridSize.Height*num4));
				}
				point2 = Point.Round(this.PointToSystem(new PointF((float) point2.X, (float) point2.Y)));
				if (hori)
				{
					base.Invalidate(new System.Drawing.Rectangle(0, point2.Y - 2, base.Width, 4));
				}
				else
				{
					base.Invalidate(new System.Drawing.Rectangle(point2.X - 2, 0, 4, base.Height));
				}
				if ((hori && (point2.Y >= 0)) && (point2.Y <= base.Height))
				{
					base.Invalidate(new System.Drawing.Rectangle(0, point2.Y - 1, base.Width, 2));
					point2 = this.PointToView(point2);
					line1 = new RefLine(point2.Y, hori);
					this.RefLines.Add(line1);
				}
				else if ((point2.X >= 0) && (point2.X <= base.Width))
				{
					base.Invalidate(new System.Drawing.Rectangle(point2.X - 1, 0, 2, base.Height));
					point2 = this.PointToView(point2);
					line1 = new RefLine(point2.X, hori);
					this.RefLines.Add(line1);
				}
			}
		}

		private void DealMouseWheel(object sender, MouseEventArgs e)
		{
			int num1;
			int num2 = 120;
			int num3 = Math.Abs(e.Delta)/num2;
			if(this.mouseAreaControl.editingOperation is ViewOperation)
			{
				this.mouseAreaControl.editingOperation.OnMouseWheel(e);
			}
			else
			{
				if (SystemInformation.MouseWheelScrollLines > 0)
				{
					num1 = this.vScrollBar1.Value - (((Math.Sign(e.Delta)*SystemInformation.MouseWheelScrollLines)*num3)*5);
				}
				else
				{
					num1 = this.vScrollBar1.Value - (Math.Sign(e.Delta)*this.vScrollBar1.LargeChange);
				}
				this.vScrollBar1.Value = Math.Max(this.vScrollBar1.Minimum, Math.Min(this.vScrollBar1.Maximum - this.vScrollBar1.LargeChange, num1));
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);

			if(this.svgDocument!=null)
			{
				this.svgDocument.SelectCollection.OnCollectionChangedEvent -= new OnCollectionChangedEventHandler(this.ChangeSelect);
			}
			
		}
		internal SvgElement CreateSymbol(bool isall, bool isshape, bool isdocument)
		{
			if ((this.svgDocument == null) || this.ElementList.Count==0)
			{
				return null;
			}
			bool flag1 = this.svgDocument.AcceptChanges;
			this.svgDocument.AcceptChanges = false;
			SvgElement facbce1 = null;
			if (isshape)
			{
				facbce1 = this.svgDocument.CreateElement(this.svgDocument.Prefix, "path", this.svgDocument.NamespaceURI) as SvgElement;
				SvgElementCollection collection1 = (this.svgDocument.RootElement as SVG).GraphList;
				if (!isall)
				{
					collection1 = this.svgDocument.SelectCollection;
				}
				StringBuilder builder1 = new StringBuilder(1000);
				for (int num1 = 0; num1 < collection1.Count; num1++)
				{
					this.ConvertToPath((collection1[num1] as SvgElement).Clone() as SvgElement, builder1);
				}
				facbce1.SetAttribute("d", builder1.ToString());
				builder1 = null;
			}
			else
			{
				facbce1 = this.svgDocument.CreateElement(this.svgDocument.Prefix, "symbol", this.svgDocument.NamespaceURI) as SvgElement;
				SvgElementCollection collection2 = new SvgElementCollection();
				if (!isall)
				{
					collection2 = this.svgDocument.SelectCollection;
				}
				else
				{
					collection2 = (this.svgDocument.RootElement as SVG).GraphList;
				}
				SvgElementCollection collection3 = new SvgElementCollection();
				foreach(SvgElement element in collection2)
				{
					addChild(element,facbce1 as Symbol);
				}
				

				if (collection3.Count > 0)
				{
					SvgElement facbce2 = this.svgDocument.CreateElement(this.svgDocument.Prefix, "defs", this.svgDocument.NamespaceURI) as SvgElement;
					facbce1.PrependChild(facbce2);
					facbce1.PrependChild(this.svgDocument.CreateTextNode("\n"));
					facbce1.InsertAfter(this.svgDocument.CreateTextNode("\n"), facbce2);
					facbce2.AppendChild(this.svgDocument.CreateTextNode("\n"));
					for (int num3 = 0; num3 < collection3.Count; num3++)
					{
						SvgElement facbce3 = collection3[num3] as SvgElement;
						facbce3 = facbce3.Clone() as SvgElement;
						facbce2.AppendChild(facbce3);
						facbce2.AppendChild(this.svgDocument.CreateTextNode("\n"));
					}
				}
				//facbce1.SetAttribute("overflow", "visible");
			}
			if (isdocument)
			{
				SvgElement facbce4 = (this.svgDocument.RootElement as SvgElement).CloneNode(false) as SvgElement;
				if (facbce1 != null)
				{
					facbce4.AppendChild(this.svgDocument.CreateTextNode("\n"));
					facbce4.AppendChild(facbce1);
					facbce4.AppendChild(this.svgDocument.CreateTextNode("\n"));
				}
				facbce1 = facbce4;
			}
			this.svgDocument.AcceptChanges = flag1;
			return facbce1;
		}
		private void addChild(SvgElement element,Group symbol)
		{
			
			if(element is Use)
			{

				SvgElementCollection list = ((element as Use).RefElement as Symbol).GraphList;
				Use use =element as Use;
				Matrix matrix1 = new Matrix();
				if (use.SvgAttributes.ContainsKey("transform"))
				{
					matrix1 = use.SvgAttributes["transform"] as Matrix;
					matrix1 = matrix1.Clone();
				}
				matrix1.Translate(use.X,use.Y);

				Group g = this.svgDocument.CreateElement("g") as Group;
				g.SetAttribute("transform",new Transf(matrix1).ToString());
				foreach(SvgElement graph in list)
				{
					addChild(graph,g);
				}
				symbol.AppendChild(g);
			}
			else
			{
				if(element is ConnectLine)
				{
					ConnectLine cline = element as ConnectLine;
					PointF point1 = cline.Points[0];
					PointF point2 = cline.Points[1];
						
						
					SvgElement element2= element.CloneNode(true) as SvgElement;

						
					element2.RemoveAttribute("transform");
					element2.RemoveAttribute("layer");
						
					element2.RemoveAttribute("start");
					element2.RemoveAttribute("end");
					element2.SetAttribute("x1",""+point1.X);
					element2.SetAttribute("y1",""+point1.Y);
					element2.SetAttribute("x2",""+point2.X);
					element2.SetAttribute("y2",""+point2.Y);

					symbol.PrependChild(element2);
				}
				else
				{
					SvgElement element2=element.Clone() as SvgElement;
					element2.RemoveAttribute("layer");
					symbol.AppendChild(element2);
				}
			}
		}
		public void ConvertToPath(SvgElement element,StringBuilder builder)
		{
			SvgElement element1 = element;
			builder.Append( PathFunc.GetPathString(((IGraph) element1).GPath));
		}

		public void FitWindow()
		{
			float single1 = this.ViewSize.Height;
			float single2 = this.ViewSize.Width;
			if (single1 == 0f)
			{
				single1 = base.Height;
			}
			if (single2 == 0f)
			{
				single2 = base.Width;
			}
			if (fullDrawMode)
			{
				SetScroll();
			}
			else
			{
				float single3 = ((float) (base.Height - 50))/single1;
				single3 = Math.Min(single3, ((float) (base.Width - 50))/single2);
				this.ScaleUnit = single3;
			}
			if (this.firstload || !fullDrawMode)
			{

				int num1 =Math.Max(this.vScrollBar1.Minimum ,(this.vScrollBar1.Maximum - this.vScrollBar1.LargeChange)/2);
				int num2 =Math.Max(this.hScrollBar1.Minimum ,(this.hScrollBar1.Maximum - this.hScrollBar1.LargeChange)/2);
			
				this.vScrollBar1.Value = num1;
				this.hScrollBar1.Value = num2;
			}
		
		}

		/// <summary>
		/// 变换选中对象
		/// </summary>
		/// <param name="matrix"></param>
		public void MatrixSelectionEx(Matrix matrix)
		{
			bool flag1 = this.svgDocument.AcceptChanges;
			this.svgDocument.AcceptChanges = true;
			this.svgDocument.NumberOfUndoOperations = 10*this.svgDocument.SelectCollection.Count;
			SvgElementCollection.ISvgElementEnumerator enumerator1 = this.svgDocument.SelectCollection.GetEnumerator();
			while (enumerator1.MoveNext())
			{
				SvgElement element1 = (SvgElement) enumerator1.Current;
				if (element1 is IGraph)
				{
					IGraph graph1 = element1 as IGraph;
					Matrix matrix3 =new Matrix();
					if(graph1.LimitSize && graph1 is Use)
					{						
						Use use1 =graph1 as Use;
						using (Matrix matrix1=this.coordTransform.Clone())
						{
							float f1 = 1/matrix1.Elements[0];				
							matrix3.Scale(f1,f1,MatrixOrder.Prepend);
							Symbol symbol = use1.RefElement as Symbol;
							RectangleF rf =symbol.GPath.GetBounds(symbol.Transform.Matrix);
							float f2 = (use1.X+rf.X+(rf.Width)/2)*(matrix1.Elements[0] -1);
							float f3 = (use1.Y+rf.Y+(rf.Height)/2)*(matrix1.Elements[0] -1);
							matrix3.Translate(f2,f3,MatrixOrder.Prepend);
							matrix3.Invert();
						}
					}
					
					Matrix matrix2 = ((IGraph) element1).Transform.Matrix.Clone();
					matrix2.Multiply(matrix3);
					matrix2.Multiply(matrix, MatrixOrder.Append);
					Transf transf1 = new Transf();
					transf1.setMatrix(matrix2);
					AttributeFunc.SetAttributeValue(element1, "transform", transf1.ToString());
					

				}
			}
			this.svgDocument.NotifyUndo();
			this.svgDocument.AcceptChanges = flag1;

		}

		/// <summary>
		/// 翻转
		/// </summary>
		/// <param name="vert">垂直</param>
		private void Flip(bool vert)
		{
			PointF tf1 = this.PointToView(this.mouseAreaControl.CenterPoint);
			Matrix matrix1 = new Matrix();
			matrix1.Translate(tf1.X, tf1.Y);
			if (vert)
			{
				matrix1.Scale(1f, -1f);
			}
			else
			{
				matrix1.Scale(-1f, 1f);
			}
			matrix1.Translate(-tf1.X, -tf1.Y);
			this.MatrixSelection(matrix1);
			//            bool flag1 = this.svgDocument.AcceptChanges;
			//            this.svgDocument.AcceptChanges = true;
			//            this.svgDocument.NumberOfUndoOperations = 10 * this.svgDocument.SelectCollection.Count;
			//            SvgElementCollection.ISvgElementEnumerator enumerator1 = this.svgDocument.SelectCollection.GetEnumerator();
			//            while (enumerator1.MoveNext())
			//            {
			//                SvgElement element1 = (SvgElement) enumerator1.Current;
			//                if (element1 is IGraph)
			//                {
			//                    Matrix matrix2 = ((IGraph) element1).Transform.Matrix.Clone();
			//                    matrix2.Multiply(matrix1, MatrixOrder.Append);
			//                    Transf transf1 = new Transf();
			//                    transf1.setMatrix(matrix2);
			//                    AttributeFunc.SetAttributeValue(element1, "transform", transf1.ToString());
			//                }
			//            }
			//            this.svgDocument.NotifyUndo();
			//            this.svgDocument.AcceptChanges = flag1;
		}

		private void hRule_MouseMove(object sender, MouseEventArgs e)
		{
			if ((e.Button == MouseButtons.Left) && (OperationFunc.IsSelectOperation(this.operation) || OperationFunc.IsTransformOperation(this.operation)))
			{
				this.CreateRef(Control.MousePosition, sender == this.hRule, false);
			}
		}

		private void hRule_MouseUp(object sender, MouseEventArgs e)
		{
			if ((e.Button == MouseButtons.Left) && (OperationFunc.IsSelectOperation(this.operation) || OperationFunc.IsTransformOperation(this.operation)))
			{
				this.CreateRef(Control.MousePosition, sender == this.hRule, true);
			}
		}

		private void hRule_Paint(object sender, PaintEventArgs e)
		{
			StringFormat format1 = new StringFormat();
			float single1 = this.ScaleUnit;
			int num1 = 0;
			if (this.svgDocument != null)
			{
				RectangleF ef1 = this.svgDocument.RootElement.ViewPort;
				num1 = (int) ef1.X;
				SizeF ef2 = this.ViewSize;
				single1 = (single1*ef2.Width)/ef1.Width;
			}
			float single2 = this.Approach(single1);
			float single3 = (single2*single1)/10f;
			float single4 = this.margin.Width - this.virtualLeft;
			//			int num2 = (int) (single2*single1);
			Pen pen1 = new Pen(Color.Black, 1f);
			pen1.Alignment = PenAlignment.Center;
			float single5 = single4;
			while (single5 >= -single3)
			{
				if ((num1%10) == 0)
				{
					e.Graphics.DrawLine(pen1, single5, 0f, single5, (float) this.hRule.Height);
					float single6 = (float) Math.Round((double) ((single5 - single4)/single1), 0);
					e.Graphics.DrawString(single6.ToString(), new Font("Times New Roman", 7f), Brushes.Black, new RectangleF(single5, 0f, 200f, 10f), format1);
				}
				else if ((num1%2) == 0)
				{
					e.Graphics.DrawLine(pen1, single5, 10f, single5, (float) this.hRule.Height);
				}
				else
				{
					e.Graphics.DrawLine(pen1, single5, 12f, single5, (float) this.hRule.Height);
				}
				single5 -= single3;
				num1++;
			}
			single5 = single4 + single3;
			for (num1 = 1; single5 <= (this.hRule.Width + single3); num1++)
			{
				if ((num1%10) == 0)
				{
					e.Graphics.DrawLine(pen1, single5, 0f, single5, (float) this.hRule.Height);
					float single7 = (float) Math.Round((double) ((single5 - single4)/single1), 0);
					e.Graphics.DrawString(single7.ToString(), new Font("Times New Roman", 7f), Brushes.Black, new RectangleF(single5, 0f, 200f, 10f), format1);
				}
				else if ((num1%2) == 0)
				{
					e.Graphics.DrawLine(pen1, single5, 10f, single5, (float) this.hRule.Height);
				}
				else
				{
					e.Graphics.DrawLine(pen1, single5, 12f, single5, (float) this.hRule.Height);
				}
				single5 += single3;
			}
			e.Graphics.DrawLine(pen1, 0, this.hRule.Height - 1, this.hRule.Width, this.hRule.Height - 1);
		}

		private void hScrollBar1_ValueChanged(object sender, EventArgs e)
		{
			this.VirtualLeft = this.hScrollBar1.Value;
			if (this.UpdateRule)
			{
				this.hRule.Invalidate();
			}
			base.Focus();
		}

		private void InitializeComponent()
		{
			ResourceManager resources = new ResourceManager(typeof (ItopVector.DrawArea.DrawArea));
			this.hScrollBar1 = new HScrollBar();
			this.label1 = new Label();
			this.vScrollBar1 = new VScrollBar();
			this.hRule = new Label();
			this.vRule = new Label();
			this.label2 = new Label();
			this.viewer = new Viewer();
			this.mouseAreaControl = new MouseArea();
			this.viewer.SuspendLayout();
			base.SuspendLayout();
			this.hScrollBar1.Anchor = AnchorStyles.Right | (AnchorStyles.Left | AnchorStyles.Bottom);
			this.hScrollBar1.Location = new Point(0, 360);
			this.hScrollBar1.Name = "hScrollBar1";
			this.hScrollBar1.Size = new Size(0x170, 0x10);
			this.hScrollBar1.TabIndex = 0;
			this.hScrollBar1.ValueChanged += new EventHandler(this.hScrollBar1_ValueChanged);
			this.label1.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
			this.label1.BackColor = SystemColors.Control;
			this.label1.Location = new Point(0x170, 360);
			this.label1.Name = "label1";
			this.label1.Size = new Size(0x10, 0x10);
			this.label1.TabIndex = 1;
			this.vScrollBar1.Anchor = AnchorStyles.Right | (AnchorStyles.Bottom | AnchorStyles.Top);
			this.vScrollBar1.Location = new Point(0x170, 0);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new Size(0x10, 360);
			this.vScrollBar1.TabIndex = 2;
			this.vScrollBar1.ValueChanged += new EventHandler(this.vScrollBar1_ValueChanged);
			this.hRule.Anchor = AnchorStyles.Right | (AnchorStyles.Left | AnchorStyles.Top);
			this.hRule.BackColor = Color.White;
			this.hRule.Name = "hRule";
			this.hRule.Size = new Size(0x170, 16);
			this.hRule.TabIndex = 3;
			this.hRule.Paint += new PaintEventHandler(this.hRule_Paint);
			this.hRule.MouseUp += new MouseEventHandler(this.hRule_MouseUp);
			this.hRule.MouseMove += new MouseEventHandler(this.hRule_MouseMove);
			this.vRule.Anchor = AnchorStyles.Left | (AnchorStyles.Bottom | AnchorStyles.Top);
			this.vRule.BackColor = Color.White;
			this.vRule.Name = "vRule";
			this.vRule.Size = new Size(16, 360);
			this.vRule.TabIndex = 4;
			this.vRule.Paint += new PaintEventHandler(this.vRule_Paint);
			this.vRule.MouseUp += new MouseEventHandler(this.hRule_MouseUp);
			this.vRule.MouseMove += new MouseEventHandler(this.hRule_MouseMove);
			this.label2.BackColor = Color.White;
			this.label2.Name = "label2";
			this.label2.Size = new Size(16, 16);
			this.label2.TabIndex = 5;
			this.label2.Paint += new PaintEventHandler(this.label2_Paint);
			this.viewer.AllowDrop = true;
			this.viewer.Anchor = AnchorStyles.Right | (AnchorStyles.Left | (AnchorStyles.Bottom | AnchorStyles.Top));
			this.viewer.BackColor = Color.Transparent;

			this.viewer.Controls.AddRange(new Control[1] {this.mouseAreaControl});
			this.viewer.Name = "viewer";
			this.viewer.Size = new Size(0x170, 360);
			this.viewer.SVGDocument = null;
			this.viewer.TabIndex = 6;
			this.viewer.Text = "viewer1";
			this.mouseAreaControl.AllowDrop = true;
			this.mouseAreaControl.BackColor = Color.Transparent;
			this.mouseAreaControl.CenterPoint = (PointF) resources.GetObject("mouseAreaControl.CenterPoint");
			this.mouseAreaControl.CurrentOperation = ToolOperation.None;
			this.mouseAreaControl.DefaultCursor = Cursors.Default;
			this.mouseAreaControl.Dock = DockStyle.Fill;
			this.mouseAreaControl.Name = "mouseAreaControl";
			//			this.mouseAreaControl.PicturePanel = null;
			this.mouseAreaControl.ShiftDown = false;
			this.mouseAreaControl.Size = new Size(0x170, 360);
			this.mouseAreaControl.TabIndex = 0;
			//			this.BackColor = Color.LightGray;
			this.BackColor = Color.FromArgb(144,153,174);
			Control[] controlArray2 = new Control[7] {this.vScrollBar1, this.label1, this.hScrollBar1, this.label2, this.vRule, this.hRule, this.viewer};
			base.Controls.AddRange(controlArray2);
			base.Name = "DrawArea";
			base.Size = new Size(0x180, 0x178);
			this.viewer.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		public void InvadatePosLine()
		{
		}

		public void InvadateRect(System.Drawing.Rectangle rect)
		{
			this.viewer.Invalidate(rect);
		}

		public void InvalidateElement(SvgElement element)
		{
			this.viewer.InvalidateElement(element);
		}

		public void InvalidateRule()
		{
			this.hRule.Invalidate();
			this.vRule.Invalidate();
		}

		/// <summary>
		/// 左上角十字
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void label2_Paint(object sender, PaintEventArgs e)
		{
			using (Pen pen1 = new Pen(Brushes.Black, 1f))
			{
				e.Graphics.DrawLine(pen1, 15, 0, 15, 16);
				e.Graphics.DrawLine(pen1, 0, 15, 16, 15);
			}
			using (Pen pen2 = new Pen(Brushes.Blue, 1f))
			{
				pen2.DashStyle = DashStyle.Dot;
				e.Graphics.DrawLine(pen2, 7, 0, 7, 16);
				e.Graphics.DrawLine(pen2, 0, 7, 16, 7);
			}
		}

		public void NotifyContextMenu(Point p)
		{
			if (this.OnTrackPopup != null)
			{
				this.OnTrackPopup(this, p);
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize (e);
			SetScroll();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			
			if (this.svgDocument != null)
			{
				if (this.firstload)
				{
					this.FitWindow();

					this.firstload = false;
				}
				SizeF ef1 = this.ViewSize;
				float single1 = ef1.Width;
				float single2 = ef1.Height;
				if (single1 == 0f)
				{
					single1 = base.Width;
				}
				if (single2 == 0f)
				{
					single2 = base.Height;
				}
				PointF[] tfArray2 = new PointF[3] {new PointF(0f, 0f), new PointF(single1, single2),new PointF(Width/2f,Height/2f)};
				PointF[] tfArray1 = tfArray2;
			{
				this.coordTransform.Reset();
				this.coordTransform.Translate(-this.virtualLeft, -this.virtualTop);

				this.coordTransform.Translate((float) this.margin.Width, (float) this.margin.Height);
				this.coordTransform.Scale(this.scale, this.scale);
				this.coordTransform.TransformPoints(tfArray1);
			}
				RectangleF rf1 = new RectangleF(tfArray1[0].X, tfArray1[0].Y, tfArray1[1].X - tfArray1[0].X, tfArray1[1].Y - tfArray1[0].Y);
				RectangleF rf2 = rf1;


				PaintMapEventArgs paintMapEventArgs = new PaintMapEventArgs(e.Graphics,new PointF(rf1.X,rf1.Y),this.Bounds);


				paintMapEventArgs.CenterPoint = new PointF(-(Width/2f - ( tfArray1[0].X)),-(Height/2f -(tfArray1[0].Y))  );
				if(PaintMap !=null)
					PaintMap(this,paintMapEventArgs);

				if(!fullDrawMode)
				{
					rf2.Offset(6,6);
					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(108,114,130)),rf2);
					if(BkColor!=null)
					{
						e.Graphics.FillRectangle(BkColor,rf1);
					}
					else
					{
						e.Graphics.FillRectangle(Brushes.White,rf1);
					}
					if (this.showGrid)
					{
						this.PaintGrid(e.Graphics, rf1, new SizeF(this.GridSize.Width*this.coordTransform.Elements[0], this.GridSize.Height*this.coordTransform.Elements[3]));
					}
					e.Graphics.DrawRectangle(Pens.Black, tfArray1[0].X, tfArray1[0].Y, tfArray1[1].X - tfArray1[0].X, tfArray1[1].Y - tfArray1[0].Y);

				}
				if(fullDrawMode)
				{
					//					e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(108,114,130)),0,0,this.mouseAreaControl.Width,this.mouseAreaControl.Height);
					if(BkColor!=null)
					{
						e.Graphics.FillRectangle(BkColor,0,0,this.mouseAreaControl.Width,this.mouseAreaControl.Height);
					}
					else
					{
						e.Graphics.FillRectangle(Brushes.White,0,0,this.mouseAreaControl.Width,this.mouseAreaControl.Height);
					}
					if (this.showGrid)
					{
						this.PaintGrid(e.Graphics, rf1, new SizeF(this.GridSize.Width*this.coordTransform.Elements[0], this.GridSize.Height*this.coordTransform.Elements[3]));
						e.Graphics.DrawRectangle(Pens.Black, tfArray1[0].X, tfArray1[0].Y, tfArray1[1].X - tfArray1[0].X, tfArray1[1].Y - tfArray1[0].Y);
					}
					
				}
				if(AfterPaintPage !=null)
					AfterPaintPage(this,paintMapEventArgs);
				
			}
		}

		/// <summary>
		/// 绘制网格线
		/// </summary>
		/// <param name="g"></param>
		/// <param name="rect"></param>
		/// <param name="size"></param>
		/// 
		private void PaintGrid(Graphics g, RectangleF rect, SizeF size)
		{
			if (this.showGrid)
			{
				float single1 = (float) Math.Round((double) size.Width, 1);
				float single2 = (float) Math.Round((double) size.Height, 1);
				//				float single3 = single1;
				//				float single4 = single2;
				//				float single5 = single1 - ((int) single1);
				//				float single6 = single2 - ((int) single2);
				//				if (single5 != 0f)
				//				{
				//					single3 = single1*10f;
				//				}
				//				if (single6 != 0f)
				//				{
				//					single4 = single1*10f;
				//				}
				//				float single3 = this.GridSize.Width;// * this.scale;
				using (Pen pen1 = new Pen(Color.LightGray))
				{
					pen1.DashStyle = DashStyle.Dot;
					for (float single4 = 0f; single4 < rect.Width; single4 += single1)
					{
						g.DrawLine(pen1,rect.X + single4, rect.Y, rect.X + single4, rect.Y + rect.Height);
					}
					//single3 = this.GridSize.Height;// * this.scale;
					for (float single5 = 0f; single5 < rect.Height; single5 += single2)
					{
						g.DrawLine(pen1, rect.X, rect.Y + single5, rect.X + rect.Width,rect.Y + single5);
					}
				}
				return;				
			}
		}
		//效率低
		private void PaintGrid2(Graphics g, RectangleF rect, SizeF size)
		{
			if (this.showGrid)
			{
				float single1 = (float) Math.Round((double) size.Width, 1);
				float single2 = (float) Math.Round((double) size.Height, 1);
				float single3 = single1;
				float single4 = single2;
				float single5 = single1 - ((int) single1);
				float single6 = single2 - ((int) single2);
				if (single5 != 0f)
				{
					single3 = single1*10f;
				}
				if (single6 != 0f)
				{
					single4 = single1*10f;
				}
				using (Bitmap bitmap1 = new Bitmap((int) single3, (int) single4))
				{
					using (Pen pen1 = new Pen(Color.LightGray))
					{
						pen1.DashStyle = DashStyle.Dot;
						using (Graphics graphics1 = Graphics.FromImage(bitmap1))
						{
							for (int num1 = 1; num1 < 10; num1++)
							{
								graphics1.DrawLine(pen1, num1*single1, 0f, num1*single1, (float) bitmap1.Height);
							}
							for (int num2 = 1; num2 < 10; num2++)
							{
								graphics1.DrawLine(pen1, 0f, num2*single2, (float) bitmap1.Width, num2*single2);
							}
							graphics1.DrawRectangle(pen1, 0, 0, bitmap1.Width, bitmap1.Height);
							using (TextureBrush brush1 = new TextureBrush(bitmap1))
							{
								GraphicsContainer container1 = g.BeginContainer();
								g.TranslateTransform(rect.X, rect.Y);
								g.FillRectangle(brush1, 0f, 0f, rect.Width, rect.Height);
								g.EndContainer(container1);
							}
						}
					}
				}
			}
		}

		public PointF PointToSystem(PointF point)
		{
			PointF[] tfArray2 = new PointF[1] {point};
			PointF[] tfArray1 = tfArray2;
			this.coordTransform.TransformPoints(tfArray1);
			return tfArray1[0];
		}

		public void PointToSystem(PointF[] points)
		{
			this.coordTransform.TransformPoints(points);
		}

		public Point PointToView(Point point)
		{
			Point[] pointArray1 = new Point[1] {point};
			Matrix matrix1 = this.coordTransform.Clone();
			matrix1.Invert();
			matrix1.TransformPoints(pointArray1);
			return pointArray1[0];
		}

		public void PointToView(PointF[] points)
		{
			Matrix matrix1 = this.coordTransform.Clone();
			matrix1.Invert();
			matrix1.TransformPoints(points);
		}

		public PointF PointToView(PointF point)
		{
			PointF[] tfArray1 = new PointF[1] {point};
			Matrix matrix1 = this.coordTransform.Clone();
			matrix1.Invert();
			matrix1.TransformPoints(tfArray1);
			return tfArray1[0];
		}

		public void PostBrush(ISvgBrush brush)
		{
			if (this.PostBrushEvent != null)
			{
				this.PostBrushEvent(this, brush);
			}
		}

		public void RenderTo(Graphics g)
		{
			if ((g != null) && ((this.svgDocument != null) && (this.svgDocument.DocumentElement != null)))
			{
				SVG svg1 = this.svgDocument.DocumentElement as SVG;
				try
				{
					SmoothingMode mode1=this.svgDocument.SmoothingMode;
					this.svgDocument.SmoothingMode =g.SmoothingMode;
					if (OnBeforeRenderTo!=null)
					{
						Rectangle bounds = new Rectangle(0,0,this.DocumentSizeEx.Width,this.DocumentSizeEx.Height);

						OnBeforeRenderTo(this,new PaintMapEventArgs(g,new PointF(bounds.Width/2,bounds.Height/2),bounds));
					}
					svg1.Draw(g, this.svgDocument.ControlTime);
					this.svgDocument.SmoothingMode = mode1;
				}
				catch
				{
				}
			}
		}

		public void RotateSelection(float angle)
		{
			using (System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix())
			{
				PointF tf1 = this.PointToView(this.mouseAreaControl.CenterPoint);
                matrix.Translate(tf1.X, tf1.Y);
				matrix.Rotate(angle);
                matrix.Translate(-tf1.X, -tf1.Y);
				this.MatrixSelection(matrix);
			}
		}
        public void RotateSelection(float angle,PointF center)
        {
            using (System.Drawing.Drawing2D.Matrix matrix = new System.Drawing.Drawing2D.Matrix())
            {
                PointF tf1 = this.PointToView(this.mouseAreaControl.CenterPoint);
                matrix.Translate(center.X, center.Y);
                matrix.Rotate(angle);
                matrix.Translate(-center.X, -center.Y);
                this.MatrixSelection(matrix);
            }
        } 
		public bool SelectMenuItem(object sender)
		{ ////////////////////////////////////////////////////////
			MenuItem command1 = new MenuItem();
			string text1 = sender.ToString();
			bool flag1 = false;
			switch (text1)
			{
				case "rule":
				{
					this.ShowRule = !this.showRule;
					command1.Checked = this.showRule;
					return true;
				}
				case "showgrid":
				{
					this.ShowGrid = !this.showGrid;
					command1.Checked = this.showGrid;
					return true;
				}
				case "snaptogrid":
				{
					this.SnapToGrid = !this.SnapToGrid;
					command1.Checked = this.SnapToGrid;
					return true;
				}
				case "showguides":
				{
					bool flag2 = !this.showGuides;
					this.ShowGuides = flag2;
					command1.Checked = this.showGuides;
					return true;
				}
				case "snaptoguides":
				{
					this.SnapToGuides = !this.SnapToGuides;
					command1.Checked = this.SnapToGuides;
					return true;
				}
				case "lockguides":
				{
					this.lockGuides = !this.lockGuides;
					command1.Checked = this.lockGuides;
					return true;
				}
				case "clearguides":
				{
					foreach (RefLine line1 in this.RefLines)
					{
						if (line1.Hori)
						{
							PointF tf1 = new PointF(0f, (float) line1.Pos);
							tf1 = this.PointToSystem(tf1);
							this.viewer.Invalidate(new System.Drawing.Rectangle(0, (int) tf1.Y, base.Width, 2));
							continue;
						}
						PointF tf2 = new PointF((float) line1.Pos, 0f);
						tf2 = this.PointToSystem(tf2);
						this.viewer.Invalidate(new System.Drawing.Rectangle((int) tf2.X, 0, 2, base.Height));
					}
					this.RefLines.Clear();
					return flag1;
				}
				case "fast":
				{
					if (this.svgDocument.SmoothingMode != SmoothingMode.HighSpeed)
					{
						this.svgDocument.SmoothingMode = SmoothingMode.HighSpeed;
						this.viewer.Invalidate();
					}
					return true;
				}
				case "highquality":
				{
					if (this.svgDocument.SmoothingMode != SmoothingMode.HighQuality)
					{
						this.svgDocument.SmoothingMode = SmoothingMode.HighQuality;
						this.viewer.Invalidate();
					}
					return true;
				}
				case "antialiastext":
				{
					if (this.svgDocument.TextRenderingHint != TextRenderingHint.AntiAlias)
					{
						this.svgDocument.TextRenderingHint = TextRenderingHint.AntiAlias;
						this.viewer.Invalidate();
					}
					return true;
				}
				case "converttopath":
				{
					this.mouseAreaControl.ConvertToPath();
					return true;
				}
				case "converttosymbol":
				{
					this.mouseAreaControl.ConvertToSymbol(this.svgDocument.CurrentElement);
					return true;
				}
				case "gotop":
				{
					this.mouseAreaControl.GoTop(this.svgDocument.CurrentElement);
					return true;
				}
				case "goup":
				{
					this.mouseAreaControl.GoUp(this.svgDocument.CurrentElement);
					return true;
				}
				case "godown":
				{
					this.mouseAreaControl.GoDown(this.svgDocument.CurrentElement);
					return true;
				}
				case "gobottom":
				{
					this.mouseAreaControl.GoBottom(this.svgDocument.CurrentElement);
					return true;
				}
				case "group":
				{
					this.mouseAreaControl.Group(this.svgDocument.SelectCollection);
					return true;
				}
				case "ungroup":
				{
					this.mouseAreaControl.UnGroup((Group) this.svgDocument.CurrentElement);
					return true;
				}
				case "copy":
				{
					this.mouseAreaControl.Copy();
					return true;
				}
				case "paste":
				{
					this.mouseAreaControl.Paste();
					return true;
				}
				case "cut":
				{
					this.mouseAreaControl.Cut();
					return true;
				}
				case "delete":
				{
					this.mouseAreaControl.Delete();
					return true;
				}
				case "selectall":
				{
					this.mouseAreaControl.SelectAll();
					return true;
				}
				case "selectcurrentlay":
				{
					this.mouseAreaControl.SelectCurrentLay();
					return true;
				}
				case "clearselects":
				{
					this.mouseAreaControl.ClearSelection();
					return true;
				}
				case "zoomin":
				{
					this.ScaleUnit = 2f*this.scale;
					return true;
				}
				case "zoomout":
				{
					this.ScaleUnit = this.scale/2f;
					return true;
				}
				case "editgrid":
				{
					GridWindow window1 = new GridWindow();
					window1.SizeValue = this.gridSize;
					if (window1.ShowDialog(this) == DialogResult.OK)
					{
						this.GridSize = window1.SizeValue;
					}
					return true;
				}
				case "undo":
				{
					if (!this.mouseAreaControl.Undo())
					{
						this.svgDocument.Undo();
					}
					return true;
				}
				case "redo":
				{
					if (!this.mouseAreaControl.Redo())
					{
						this.svgDocument.Redo();
					}
					return true;
				}
				case "break":
				{
					this.Break(this.svgDocument.SelectCollection);
					return true;
				}
				case "flipx":
				{
					this.Flip(false);
					return flag1;
				}
				case "flipy":
				{
					this.Flip(true);
					return flag1;
				}
				case "rotate90":
				{
					this.RotateSelection(90);

					return flag1;
				}
				case "rotate_90":
				{
					this.RotateSelection(-90);
					return flag1;
				}

				case "link":
				{
					this.mouseAreaControl.Link();
					return flag1;
				}
				case "unlink":
				{
					this.mouseAreaControl.UnLink();
					return flag1;
				}
			}
			return flag1;
		}

		private PointF GetCenterPoint(float scale1)
		{
			PointF pf =new PointF(this.Width/2,this.Height/2);
			PointF pf2 = PointF.Empty;//this.PointToView(pf);
			using (Matrix matrix1 =new Matrix())
			{
				matrix1.Translate(-this.virtualLeft, -this.virtualTop);
				matrix1.Translate((float) this.margin.Width, (float) this.margin.Height);
				matrix1.Scale(scale1, scale1);
				matrix1.Invert();
				PointF[] pfs =new PointF[1]{pf};
				matrix1.TransformPoints(pfs);
				pf2 = pfs[0];
			}
			return pf2;
		}
        public void GoLocation(float x, float y)
        {
            PointF center = GetCenterPoint();
            float num1 = (center.X - x) * this.ScaleRatio;
            float num2 = (center.Y - y) * this.ScaleRatio;
            this.MovePicture(this.VirtualLeft - num1, this.VirtualTop - num2,true);
            this.SetScrollDelta(-(int)(num1), -(int)(num2));
        }
		public PointF GetCenterPoint()
		{
			return this.GetCenterPoint(this.scale);
		}
		public void SetScroll()
		{
			PointF offset =PointF.Empty;
			if (oldScale!=scale)
			{
				PointF pf1 = GetCenterPoint(oldScale);
				float f1 = (scale -oldScale);
				offset = new PointF((pf1.X*f1),(pf1.Y*f1));
				oldScale =scale;
			}
			int num2=30;
			int num3=50;
			float single1 = ((float) (this.vScrollBar1.Value - this.vScrollBar1.Minimum))/((float) (this.vScrollBar1.Maximum - this.vScrollBar1.Minimum));
			float single2 = ((float) (this.hScrollBar1.Value - this.hScrollBar1.Minimum))/((float) (this.hScrollBar1.Maximum - this.hScrollBar1.Minimum));
			if (float.IsNaN(single1))single1=0f;
			if (float.IsNaN(single2))single2=0f;
			this.vScrollBar1.Minimum = 0;
			SizeF ef1 = this.ViewSize;
			float single3 = ef1.Height;
			float single4 = ef1.Width;
			if (single3 == 0f)
			{
				single3 = base.Height;
			}
			if (single4 == 0f)
			{
				single4 = base.Width;
			}
			num3 = Math.Max(50,this.Height -32);
			int num1 = (this.margin.Height-16)*2 + (int) (single3*this.scale);
			if(num1>0)
			{
				this.vScrollBar1.Enabled=true;
				this.vScrollBar1.Maximum = num1;
				this.vScrollBar1.SmallChange = num2 ;
				this.vScrollBar1.LargeChange = num3;
				//				this.vScrollBar1.Value = this.vScrollBar1.Minimum + ((int) (single1*(this.vScrollBar1.Maximum - this.vScrollBar1.Minimum)));
			}
			else
			{
				this.vScrollBar1.Maximum=0;
				this.vScrollBar1.Value =0;
				this.vScrollBar1.Enabled=false;
			}
			
			this.hScrollBar1.Minimum = 0;
			num3= Math.Max(50,this.Width-32);
			num1 = (((this.margin.Width -16)*2) + ((int) (single4*this.scale)));
			if(num1>0)
			{				
				this.hScrollBar1.Enabled=true;
				this.hScrollBar1.Maximum = num1;
				this.hScrollBar1.SmallChange = num2;
				this.hScrollBar1.LargeChange = num3;
				//				this.hScrollBar1.Value = this.hScrollBar1.Minimum + ((int) (single2*(this.hScrollBar1.Maximum - this.hScrollBar1.Minimum)));
			}
			else
			{
				this.hScrollBar1.Value =0;
				this.hScrollBar1.Maximum = 0;
				this.hScrollBar1.Enabled=false;	
			}	
			if(offset !=PointF.Empty)
				SetScrollDelta((int)offset.X,(int)offset.Y);
		}

		public void SetScrollDelta(int x, int y)
		{
			int num1 = this.hScrollBar1.Value + x;
			int num2 = this.vScrollBar1.Value + y;
			num1 = Math.Max(this.hScrollBar1.Minimum, Math.Min(num1, this.hScrollBar1.Maximum -this.hScrollBar1.LargeChange));
			num2 = Math.Max(this.vScrollBar1.Minimum, Math.Min(num2, this.vScrollBar1.Maximum -this.vScrollBar1.LargeChange));
			this.hScrollBar1.Value = num1;
			this.vScrollBar1.Value = num2;
		}

		public void ShowAll()
		{
			if ((this.svgDocument != null) && (this.svgDocument.DocumentElement is SVG))
			{
				RectangleF ef1 = ((SVG) this.svgDocument.DocumentElement).GetBounds();
				SizeF ef2 = this.ViewSize;
				float single1 = Math.Max(ef2.Width, ef1.Width);
				float single2 = Math.Max(ef2.Height, ef1.Height);
				if ((ef1.Width != 0f) && (ef1.Height != 0f))
				{
					float single3 = ((float) (base.Height - 50))/single2;
					single3 = Math.Min(single3, ((float) (base.Width - 50))/single1);
					this.ScaleUnit = single3;
					this.vScrollBar1.Value = this.vScrollBar1.Maximum/2;
					this.hScrollBar1.Value = this.hScrollBar1.Maximum/2;
				}
			}
		}

		public void ToolTip(string tooltip, byte tiptype)
		{
			if (this.OnTipEvent != null)
			{
				this.OnTipEvent(this, tooltip, tiptype);
			}
		}

		public bool UpdateMenuItem(object sender)
		{
			object obj1;
			MenuItem command1 = sender as MenuItem;
			string text1 = string.Empty;
			//            if (!(command1.Tag is XmlElement))
			//            {
			//                return false;
			//            }
			//            text1 = ((XmlElement) command1.Tag).GetAttribute("Method").Trim().ToLower();
			bool flag1 = false;

			if (((obj1 = text1) != null) && ((obj1 = command1.MenuItems.IndexOf(command1)) != null))
			{
				obj1 = 0;
				switch (((int) obj1))
				{
					case 0:
					{
						command1.Enabled = this.svgDocument.SelectCollection.Count > 0;
						return flag1;
					}
					case 1:
					{
						command1.Checked = this.showRule;
						return true;
					}
					case 2:
					{
						command1.Checked = this.showGrid;
						return true;
					}
					case 3:
					{
						command1.Checked = this.SnapToGrid;
						return true;
					}
					case 4:
					{
						command1.Checked = this.showGuides;
						return true;
					}
					case 5:
					{
						command1.Checked = this.SnapToGuides;
						return true;
					}
					case 6:
					{
						command1.Checked = this.lockGuides;
						return true;
					}
					case 7:
					{
						command1.Checked = this.svgDocument.SmoothingMode == SmoothingMode.HighSpeed;
						return true;
					}
					case 8:
					{
						command1.Checked = this.svgDocument.SmoothingMode == SmoothingMode.HighQuality;
						return true;
					}
					case 9:
					{
						command1.Checked = this.svgDocument.TextRenderingHint == TextRenderingHint.AntiAlias;
						return true;
					}
					case 10:
					{
						command1.Enabled = this.scale <= 2800f;
						return true;
					}
					case 11:
					{
						command1.Enabled = this.scale > 0.05;
						return true;
					}
					case 12:
					{
						command1.Enabled = this.svgDocument.CanUndo;
						if (this.mouseAreaControl.editingOperation != null)
						{
							command1.Enabled = command1.Enabled || this.mouseAreaControl.editingOperation.CanUndo;
						}
						return true;
					}
					case 13:
					{
						command1.Enabled = this.svgDocument.CanRedo;
						if (this.mouseAreaControl.editingOperation != null)
						{
							command1.Enabled = command1.Enabled || this.mouseAreaControl.editingOperation.CanRedo;
						}
						return true;
					}
					case 14:
					{
						command1.Enabled = false;
						SvgElementCollection.ISvgElementEnumerator enumerator1 = this.svgDocument.SelectCollection.GetEnumerator();
						while (enumerator1.MoveNext())
						{
							SvgElement element1 = (SvgElement) enumerator1.Current;
							if (element1 is Group)
							{
								command1.Enabled = true;
								return true;
							}
						}
						return true;
					}
					case 15:
					case 0x10:
					case 0x11:
					case 0x12:
					case 0x13:
					case 20:
					case 0x15:
					case 0x16:
					case 0x17:
					case 0x18:
					case 0x19:
					case 0x1a:
					case 0x1b:
					case 0x1c:
					case 0x1d:
					{
						bool flag2 = false;
						if (this.svgDocument == null)
						{
							command1.Enabled = false;
							return flag1;
						}
						if (this.svgDocument.SelectCollection.Count > 0)
						{
							flag2 = (this.svgDocument.CurrentElement is IGraph) && (this.svgDocument.CurrentElement != this.svgDocument.DocumentElement);
							if ((text1 != "align") && (text1 != "group"))
							{
								if (text1 == "ungroup")
								{
									flag2 = (flag2 && (this.svgDocument.SelectCollection.Count == 1)) && (this.svgDocument.CurrentElement is Group);
								}
							}
							else
							{
								flag2 = flag2 && (this.svgDocument.SelectCollection.Count > 1);
							}
						}
						command1.Enabled = flag2;
						return flag1;
					}
					case 30:
					{
						if (this.svgDocument == null)
						{
							command1.Enabled = false;
							return flag1;
						}
						if (this.svgDocument.SelectCollection.Count != 1)
						{
							command1.Enabled = false;
							return flag1;
						}
						if (!(this.svgDocument.CurrentElement is ItopVector.Core.Figure.Link))
						{
							command1.Enabled = false;
							return flag1;
						}
						command1.Enabled = true;
						return flag1;
					}
					case 0x1f:
					case 0x20:
					{
						command1.Enabled = (this.svgDocument.CurrentElement is IGraph) && (this.svgDocument.CurrentElement != this.svgDocument.DocumentElement);
						return flag1;
					}
				}
			}
			return flag1;
		}

		public void UpdateRegion()
		{
			this.viewer.Update();
		}

		public void UpdateToolBar(object sender)
		{
			ToolBarButton button1 = sender as ToolBarButton;
			string text1 = string.Empty;
			if (button1.Tag is XmlElement)
			{
				string text2;
				text1 = ((XmlElement) button1.Tag).GetAttribute("Method").Trim().ToLower();
				if ((text2 = text1) == null)
				{
					return;
				}
				text2 = string.IsInterned(text2);
				if (((text2 != "copy") && (text2 != "cut")) && (text2 != "delete"))
				{
					if (text2 == "rule")
					{
						button1.Pushed = this.ShowRule;
					}
					else if (text2 == "showgrid")
					{
						button1.Pushed = this.showGrid;
					}
					else if (text2 == "undo")
					{
						bool flag1 = this.svgDocument.CanUndo;
						if (this.mouseAreaControl.editingOperation != null)
						{
							flag1 = this.mouseAreaControl.editingOperation.CanUndo || flag1;
						}
						flag1 = flag1;
						button1.Enabled = flag1;
					}
					else if (text2 == "redo")
					{
						bool flag2 = this.svgDocument.CanRedo;
						if (this.mouseAreaControl.editingOperation != null)
						{
							flag2 = this.mouseAreaControl.editingOperation.CanRedo || flag2;
						}
						button1.Enabled = flag2;
					}
				}
				else
				{
					SvgElementCollection.ISvgElementEnumerator enumerator1 = this.svgDocument.SelectCollection.GetEnumerator();
					while (enumerator1.MoveNext())
					{
						SvgElement element1 = (SvgElement) enumerator1.Current;
						if (element1 is IGraph)
						{
							button1.Enabled = true;
							return;
						}
					}
					button1.Enabled = false;
				}
			}
		}

		private void vRule_Paint(object sender, PaintEventArgs e)
		{
			Pen pen1 = new Pen(Color.Black);
			pen1.Alignment = PenAlignment.Center;
			StringFormat format1 = new StringFormat(StringFormat.GenericDefault);
			format1.FormatFlags = StringFormatFlags.DirectionVertical;
			float single1 = this.ScaleUnit;
			int num1 = 0;
			if (this.svgDocument != null)
			{
				RectangleF ef1 = this.svgDocument.RootElement.ViewPort;
				num1 = (int) ef1.X;
				SizeF ef2 = this.ViewSize;
				single1 = (single1*ef2.Height)/ef1.Height;
			}
			float single2 = this.Approach(single1);
			float single3 = (single2*single1)/10f;
			float single4 = this.margin.Height - this.virtualTop;
			//			int num2 = (int) (single2*single1);
			float single5 = single4;
			while (single5 >= (-single3*5f))
			{
				if ((num1%10) == 0)
				{
					e.Graphics.DrawLine(pen1, 0f, single5, (float) this.hRule.Width, single5);
					float single6 = (float) Math.Round((double) ((single5 - single4)/single1), 0);
					e.Graphics.DrawString(single6.ToString(), new Font("Times New Roman", 7f), Brushes.Black, new RectangleF(0f, single5, 30f, 100f), format1);
				}
				else if ((num1%2) == 0)
				{
					e.Graphics.DrawLine(pen1, 10f, single5, (float) this.hRule.Width, single5);
				}
				else
				{
					e.Graphics.DrawLine(pen1, 12f, single5, (float) this.hRule.Width, single5);
				}
				single5 -= single3;
				num1++;
			}
			single5 = single4 + single3;
			for (num1 = 1; single5 <= (this.vRule.Height + (single3*5f)); num1++)
			{
				if ((num1%10) == 0)
				{
					e.Graphics.DrawLine(pen1, 0f, single5, (float) this.hRule.Width, single5);
					float single7 = (float) Math.Round((double) ((single5 - single4)/single1), 0);
					e.Graphics.DrawString(single7.ToString(), new Font("Times New Roman", 7f), Brushes.Black, new RectangleF(0f, single5, 30f, 100f), format1);
				}
				else if ((num1%2) == 0)
				{
					e.Graphics.DrawLine(pen1, 10f, single5, (float) this.hRule.Width, single5);
				}
				else
				{
					e.Graphics.DrawLine(pen1, 12f, single5, (float) this.hRule.Width, single5);
				}
				single5 += single3;
			}
			e.Graphics.DrawLine(pen1, this.vRule.Width - 1, 0, this.vRule.Width - 1, this.vRule.Height);
		}

		private void vScrollBar1_ValueChanged(object sender, EventArgs e)
		{
			this.VirtualTop = this.vScrollBar1.Value;
			if (this.UpdateRule)
			{
				this.vRule.Invalidate();
			}
			base.Focus();
		}


		/// <summary>
		/// 形状改变
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void shapeSelector_SelectedChanged(object sender, EventArgs e)
		{
			ItopVector.Selector.ShapeSelector shapeselector = sender as ItopVector.Selector.ShapeSelector;
			if (shapeselector != null)
			{
				this.shapePathString = shapeselector.SelectedShape.PathString;

				if (this.operation == ToolOperation.PreShape)
				{
					this.CreatePreGraph();
				}
				else
				{
					this.Operation = ToolOperation.PreShape;

				}

				//MessageBox.Show(str);
			}

		}

		// Properties
		[Browsable(false)]
		public PointF CenterPoint
		{
			get { return this.centerPoint; }
			set
			{
				if (this.centerPoint != value)
				{
					this.centerPoint = value;
					this.mouseAreaControl.CenterPoint = value;
					Matrix matrix1 = this.SelectMatrix.Clone();
					if (matrix1.IsInvertible)
						matrix1.Invert();
					PointF[] tfArray2 = new PointF[1] {value};
					PointF[] tfArray1 = tfArray2;
					matrix1.TransformPoints(tfArray1);
					this.viewer.oldselectPoint = tfArray1[0];
				}
			}
		}
		[Browsable(false)]
		public RectangleF ContentBounds
		{
			get
			{
				if(this.svgDocument!=null && this.svgDocument.RootElement is SVG)
				{
					return ((SVG)this.svgDocument.RootElement).ContentBounds;
				}
				return RectangleF.Empty;
			}
		}
		[Browsable(false)]
		public PointF[] ControlPoints
		{
			get { return this.viewer.Bouns; }
		}
		[Browsable(false)]
		public Matrix CoordTransform
		{
			get { return this.coordTransform; }
			set { this.coordTransform = value; }
		}
		[Browsable(false)]
		public SvgElement CurrentElement
		{
			get { return this.svgDocument.CurrentElement; }
		}

		[Browsable(false)]
		public Size DocumentSizeEx
		{
			get { return Size.Round(this.viewSize); }
		}
		[Browsable(false)]
		public Size ViewMargin
		{
			get {return this.margin;}
			set 
			{
				this.margin =value;
				this.viewer.margin = value;
			}

		}

		public DrawModeType DrawMode
		{
			get{return drawMode;}
			set{drawMode =value;}
		}
		public SvgElementCollection ElementList
		{
			get
			{
				if (this.viewer == null)
				{
					return new SvgElementCollection();
				}
				return this.viewer.ElementList;
			}
		}

		public SizeF GridSize
		{
			get { return this.gridSize; }
			set
			{
				if (this.gridSize != value)
				{
					this.gridSize = value;
					base.Invalidate();
				}
			}
		}

		public ToolOperation Operation
		{
			get { return this.operation; }
			set
			{
				if (this.operation != value)
				{
					this.operation = value;
					CreatePreGraph();
					this.mouseAreaControl.CurrentOperation = value;
					this.viewer.Invalidate();
					if (this.OperationChanged != null)
					{
						this.OperationChanged(this, new EventArgs());
					}
				}
			}
		}

		public IGraph PreGraph
		{
			get { return this.preGraph; }
			set
			{
				if (value is Symbol)
				{						
					this.Operation = ToolOperation.Symbol;
				}
				if (this.preGraph != value)
				{
					this.preGraph = value;
					if (value != null)
					{
						value.GraphTransform.Matrix.Reset();
						value.GraphTransform.setMatrix(this.coordTransform);
					}
					
					if (this.GraphChanged != null)
					{
						this.GraphChanged(this, new EventArgs());
					}
				}
			}
		}

		public float ScaleUnit
		{
			get { return this.scale; }
			set
			{
				float single1 = value;
				if ((single1 >= 2000f) || (single1 < 0.0025f))
				{
					if ((this.Operation == ToolOperation.IncreaseView) || ((this.Operation == ToolOperation.DecreaseView) && (this.svgDocument != null)))
					{
						this.mouseAreaControl.Cursor = SpecialCursors.NoViewChangeCursor;
					}
					single1 = (single1 >= 2000f) ? 200f : 0.0025f;
				}
				if (single1 !=0.0025f)
					single1 = (float) Math.Round((double) single1, 8);
				if (this.scale != single1)
				{
					this.oldScale = this.scale;
					this.scale = single1;
					if(this.operation!=ToolOperation.WindowZoom)
					{
						this.SetScroll();
					}
					//base.Invalidate();
					this.hRule.Invalidate();
					this.vRule.Invalidate();
					this.viewer.ScaleUnit = single1;
					
					if (this.ScaleChanged != null)
					{
						this.ScaleChanged(this, new EventArgs());
					}                    
				}
			}
		}

		public bool SelectChanged
		{
			set { this.viewer.selectChanged = true; }
		}

		public Matrix SelectMatrix
		{
			get
			{
				if (this.viewer != null)
				{
					return this.viewer.SelectMatrix.Clone();
				}
				return new Matrix();
			}
		}

		public GraphicsPath SelectPath
		{
			get
			{
				if (this.viewer != null)
				{
					return (GraphicsPath) this.viewer.SelectPath.Clone();
				}
				return new GraphicsPath();
			}
		}

		public bool ShowGrid
		{
			get { return this.showGrid; }
			set
			{
				if (this.showGrid != value)
				{
					this.showGrid = value;
					base.Invalidate();
				}
			}
		}

		public bool ShowGuides
		{
			get { return this.showGuides; }
			set
			{
				if (this.showGuides != value)
				{
					this.showGuides = value;
				}
			}
		}

		public bool ShowRule
		{
			get { return this.showRule; }
			set
			{
				bool flag1;
				bool flag2;
				this.showRule = value;
				this.label2.Visible = flag1 = value;
				this.vRule.Visible = flag2 = flag1;
				this.hRule.Visible = flag2;
			}
		}

		public SvgDocument SVGDocument
		{
			get { return this.svgDocument; }
			set
			{
				if (this.svgDocument != value)
				{
					if (this.svgDocument!=null)
					{
						svgDocument=null;
						GC.Collect();
					}
					SvgDocument document1;
					this.svgDocument = document1 = value;
					this.viewer.SVGDocument = document1;
					base.Invalidate();
					this.viewer.Invalidate();
					if (value != null)
					{
						value.SelectCollection.OnCollectionChangedEvent += new OnCollectionChangedEventHandler(this.ChangeSelect);
					}
				}
			}
		}

		public SizeF ViewSize
		{
			get
			{
				if (this.svgDocument != null)
				{
					if (this.svgDocument.OnlyShowCurrent)
					{
						IViewportElement element1 = this.svgDocument.RootElement.ViewportElement;
						if (element1 != null)
						{
							this.viewSize = new SizeF(element1.ViewPort.Width, element1.ViewPort.Height);
						}
					}
					else
					{
						SVG svg1 = (SVG) this.svgDocument.DocumentElement;
						if(svg1 !=null)
							this.viewSize = new SizeF(svg1.Width, svg1.Height);
					}
				}
				return this.viewSize;
			}
		}
		public void MovePicture(float left,float top,bool move)
		{
			this.virtualLeft =Math.Max(0, Math.Min(left,this.hScrollBar1.Maximum - this.hScrollBar1.LargeChange));
			this.virtualTop = Math.Max(0, Math.Min(top,this.vScrollBar1.Maximum - this.vScrollBar1.LargeChange));
			//
			this.viewer.MovePicture(this.virtualLeft,this.virtualTop,move);
            
		}

		public float VirtualLeft
		{
			get { return this.virtualLeft; }
			set
			{
				if (this.virtualLeft != value)
				{
					this.virtualLeft = Math.Max(0, value);
					base.Invalidate();
					this.viewer.VirtualLeft = value;
				}
			}
		}

		public float VirtualTop
		{
			get { return this.virtualTop; }
			set
			{
				if (this.virtualTop != value)
				{
					this.virtualTop = Math.Max(0, value);
					base.Invalidate();
					this.viewer.VirtualTop = value;
				}
			}
		}

		public Struct.TextStyle RatFont
		{
			get { return this.RatTextStyle; }
			set { this.RatTextStyle = value; }

		}
		public void BeginInsert()
		{
			this.inserting=true;
		}
		public void EndInsert()
		{
			this.inserting=false;
			this.svgDocument.SelectCollection.Clear();
			this.svgDocument.SelectCollection.AddRange(this.addedElements);
			this.addedElements.Clear();
		}
		public Brush BkColor
		{
			set
			{
				this.backColor=value;
			}
			get
			{
				return this.backColor;
			}
		}
		
		public bool FullDrawMode
		{
			set
			{
				this.fullDrawMode=value;
				
			}
			get
			{
				return this.fullDrawMode;
			}
		}
		public Graphics tempGraphics
		{
			get{return viewer.tempGra();}
		}
		public int LineCount
		{

			get{return this.lineCount;}
			set
			{
				if(value<3 || value>20)return;
				this.lineCount = value;
				if(this.operation==ToolOperation.EqualPolygon)
				{
					(preGraph as Polygon).LineCount = lineCount;
				}
			}
		}
		public float Indent
		{

			get{return this.indent;}
			set
			{
				if(value<0.05f || value>1f)return;
				this.indent = value;
				if(this.operation==ToolOperation.EqualPolygon)
				{
					(preGraph as Polygon).Indent = indent;
				}
			}
		}
		// Fields
		private Brush backColor;
		private bool fullDrawMode=false;

		private PointF centerPoint;
		private Container components;
		private Matrix coordTransform;
		public bool firstload;
		private SizeF gridSize;
		private Label hRule;
		public HScrollBar hScrollBar1;
		private Label label1;
		private Label label2;
		public bool lockGuides;
		private Size margin;
		internal MouseArea mouseAreaControl;
        internal ItopVector.DrawArea.PolyOperation polyOperation;
		private Point oldpoint;
		private ToolOperation operation;
		private IGraph preGraph;
		public IGraph prepreGraph;
		private string shapePathString;
		private bool prepathchanged;
		public SvgElementCollection PreTextSelect;
		public ArrayList RefLines;
		private float scale;
		private bool showGrid;
		private bool showGuides;
		private bool showRule;
		public bool SnapToGrid;
		public bool SnapToGuides;
		private SvgDocument svgDocument;
		public bool UpdateRule;
		public Viewer viewer;
		private SizeF viewSize;
		private float virtualLeft;
		private float virtualTop;
		private Label vRule;
		public VScrollBar vScrollBar1;
		private PropertyGrid propertyGrid;
		private PageSettings pageSetting;
		private PageSetupDialog pageSetupDlg1;

		public ISvgBrush FillBrush;
		private Stroke stroke;
		private Struct.TextStyle RatTextStyle;
		private SvgElementCollection addedElements;
		private bool inserting;
		private DrawModeType drawMode;
		private bool canEdit;
		ItopVector.Selector.SymbolSelector symbolSelector;
		public event DragEventHandler BeforeDragDrop;
		public float oldScale=1;
		private int lineCount=3;//多边形边数
		private float indent=1f;//缩进
		private bool freeSelect=false;//可选中所有图层的图元

		#region 文件操作

		public SvgDocument NewFile()
		{
			float width =400; 
			float height =500; 

			SizeF size = new SizeF(width,height);
			try
			{
                width = this.pageSetting.PaperSize.Width*0.9f;
				height = this.pageSetting.PaperSize.Height*0.9f;
				if (this.pageSetting.Landscape)
				{
					size = new SizeF(height, width);
				}
				else
				{
					size = new SizeF(width, height);
				}
			}
			catch{}

			this.SVGDocument = SvgDocumentFactory.CreateDocument(size);
			this.AttachProperty();
			return this.svgDocument;

		}

		public bool OpenFile(string fileName)
		{
			this.Cursor = Cursors.WaitCursor;
			this.SVGDocument = SvgDocumentFactory.CreateDocumentFromFile(fileName);
			this.AttachProperty();
			this.Cursor = Cursors.Default;
			
			return (this.SVGDocument != null);
		}

		private bool save(bool showdialog)
		{
			SvgDocument document1 = this.SVGDocument;
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
			saveFileDialog1.Filter = "SVG文件|*.svg";

			saveFileDialog1.FileName = document1.FileName;
			//		   this.plTip.Text = LayoutManager.GetLabelForName("savingfile").Trim();
			if ((document1.FilePath == string.Empty) || showdialog)
			{
				if (saveFileDialog1.ShowDialog() != DialogResult.OK)
				{
					return false;
				}
				string text1 = saveFileDialog1.FileName;
				//				string text2 = Path.GetExtension(text1);
				this.Cursor = Cursors.WaitCursor;
				StreamWriter writer1 = new StreamWriter(text1, false, Encoding.UTF8);
				string text3 = this.svgDocument.OuterXml;
				writer1.Write(text3);
				writer1.Flush();
				writer1.Close();
				//			   int num1 = this.documentList.IndexOf(this.CurrentDocument);
				//			   if ((num1 >= 0) && (num1 < this.filePaths.Count))
				//			   {
				//				   string text4 = (string) this.filePaths[num1];
				//				   MenuCommand command1 = this.menuControl.MenuCommands[this.menuControl.MenuCommands.Count - 2].MenuCommands[text4];
				//				   if (command1 != null)
				//				   {
				//					   command1.Text = text1;
				//				   }
				//				   this.filePaths[num1] = text1;
				//			   }
				document1.FilePath = text1;
				document1.FileName = Path.GetFileNameWithoutExtension(text1);
				this.Cursor = Cursors.Default;
				document1.Update = true;
				//			   this.plTip.Text = LayoutManager.GetLabelForName("savefilesuccessfully").Trim();
				//			   this.AddRecent(document1.FilePath.Trim());
				return true;
			}
			if (!document1.Update)
			{
				StreamWriter writer2 = new StreamWriter(document1.FilePath, false, Encoding.UTF8);
				string text6 = this.svgDocument.OuterXml;
				writer2.Write(text6);
				writer2.Flush();
				writer2.Close();
				document1.Update = true;
				//			   this.plTip.Text = LayoutManager.GetLabelForName("savefilesuccessfully").Trim();
			}
			//		   this.tabControl1.TabPages[index].Title = document1.FileName;
			this.Cursor = Cursors.Default;
			document1.Update = true;
			//		   this.plTip.Text = LayoutManager.GetLabelForName("savefilesuccessfully").Trim();
			return true;
		}

		public bool Save()
		{
			return save(false);
		}

		public bool SaveAs()
		{
			return save(true);
		}

		public bool Undo()
		{
			if (this.SVGDocument.CanUndo)
			{
				this.SVGDocument.Undo();
			}
			return this.SVGDocument.CanUndo;
		}

		public bool Redo()
		{
			if (this.SVGDocument.CanRedo)
			{
				this.SVGDocument.Redo();
			}
			return this.SVGDocument.CanRedo;
		}

		#endregion 文件操作

		#region IItopVector 成员

		public void Copy()
		{
			this.mouseAreaControl.Copy();
		}

		public void Cut()
		{
			this.mouseAreaControl.Cut();
		}

		public void Paste()
		{
			this.mouseAreaControl.Paste();
		}

		public void Delete()
		{
			this.mouseAreaControl.Delete();
		}

		public void Group()
		{
			this.mouseAreaControl.Group(this.svgDocument.SelectCollection);
		}

		public void UnGroup()
		{
			if (this.svgDocument.CurrentElement is Group)
			{
				this.mouseAreaControl.UnGroup((Group) this.svgDocument.CurrentElement);
			}
		}

		public void ChangeLevel(LevelType level)
		{
			SvgElementCollection col= svgDocument.SelectCollection;
			for(int i=0;i<col.Count;i++)
			{
				SvgElement svgele=(SvgElement)col[i];
				switch (level)
				{
					case LevelType.Top:
						
						//this.mouseAreaControl.GoTop(this.svgDocument.CurrentElement);
						this.mouseAreaControl.GoTop(svgele);
						break;
					case LevelType.Up:
						//this.mouseAreaControl.GoUp(this.svgDocument.CurrentElement);
						this.mouseAreaControl.GoUp(svgele);
						break;
					case LevelType.Down:
						//this.mouseAreaControl.GoDown(this.svgDocument.CurrentElement);
						this.mouseAreaControl.GoDown(svgele);
						break;
					case LevelType.Bottom:
						//this.mouseAreaControl.GoBottom(this.svgDocument.CurrentElement);
						this.mouseAreaControl.GoBottom(svgele);
						break;
				}
			}
		}
		public void ChangeLevel(string SymbolTagName,LevelType type)
		{
			XmlNodeList list=this.svgDocument.SelectNodes("svg/"+SymbolTagName);
			//foreach(XmlNode node in list)
			for(int i=0;i<list.Count;i++)
			{
				XmlNode node=list[i];
				switch (type)
				{
					case LevelType.Top:
						this.mouseAreaControl.GoTop((SvgElement)node);
						break;
					case LevelType.Up:
						this.mouseAreaControl.GoUp((SvgElement)node);
						break;
					case LevelType.Down:
						this.mouseAreaControl.GoDown((SvgElement)node);
						break;
					case LevelType.Bottom:
						this.mouseAreaControl.GoBottom((SvgElement)node);
						break;
				}
			}
		}


		public bool IsModified
		{
			get
			{
				return !this.svgDocument.Update;
			}
			set
			{
				this.svgDocument.Update = !value;
			}
		}

		public bool IsShowRule
		{
			get { return this.ShowRule; }
			set { this.ShowRule = value; }
		}

		public bool IsShowGrid
		{
			get { return this.ShowGrid; }
			set { this.ShowGrid = value; }
		}

		public bool IsPasteGrid
		{
			get { return this.SnapToGrid; }
			set { this.SnapToGrid = value; }
		}
		public Pen TempPen
		{
			set{viewer.TempPen=value;}
			
		}
		public bool IsShowTip
		{
			get
			{
				// TODO:  添加 ItopVectorControl.IsShowTip getter 实现
				return false;
			}
			set
			{
				// TODO:  添加 ItopVectorControl.IsShowTip setter 实现
			}
		}
		[Browsable(false)]
		public System.Drawing.Drawing2D.SmoothingMode SmoothingMode
		{
			get
			{
				return this.svgDocument.SmoothingMode;
			}
			set
			{
				this.svgDocument.SmoothingMode = value;
			}
		}

		[Browsable(false)]
		public SizeF DocumentSize
		{
			get { return Size.Round(this.viewSize); }
			set
			{
				if(this.svgDocument==null)return;
				ItopVector.Core.Figure.SVG svg =this.SVGDocument.RootElement as ItopVector.Core.Figure.SVG;
				SizeF size1=value;
				if(svg !=null)
				{
					svg.Width = size1.Width;
					svg.Height = size1.Height;
					this.FitWindow();
				}
			}
		}
		[Browsable(false)]
		public Color DocumentbgColor
		{
			get
			{
				// TODO:  添加 ItopVectorControl.DocumentbgColor getter 实现
				return new Color();
			}
			set
			{
				// TODO:  添加 ItopVectorControl.DocumentbgColor setter 实现
			}
		}
		[Browsable(false)]
		public bool Scrollable
		{
			get
			{
				// TODO:  添加 ItopVectorControl.Scrollable getter 实现
				return false;
			}
			set
			{
				// TODO:  添加 ItopVectorControl.Scrollable setter 实现
			}
		}
		[Browsable(false)]
		public float ScaleRatio
		{
			get { return this.ScaleUnit; }
			set { this.ScaleUnit = value; }
		}
		[Browsable(false)]
		public Stroke Stroke
		{
			get { return this.stroke; }
			set
			{
				if (value == null || this.stroke == value) return;
				
				this.stroke = value;
				if(this.svgDocument!=null && this.preGraph!=null)
				{
					this.svgDocument.AcceptChanges=false;				
					((IGraphPath) preGraph).GraphStroke = this.stroke.Clone() as Stroke;
				}

			}
		}
		[Browsable(false)]
		public SolidColor Fill
		{
			get { return (SolidColor) this.FillBrush; }
			set
			{
				if (value == null || this.FillBrush == value) return;
				this.FillBrush = value;
				if(this.svgDocument!=null && this.preGraph!=null)
				{
					this.svgDocument.AcceptChanges=false;
					((IGraphPath) preGraph).GraphBrush = this.FillBrush.Clone();
				}
				
			}
		}

		[Browsable(false)]
		public Struct.TextStyle TextStyle
		{
			get { return this.RatTextStyle; }
			set { this.RatTextStyle = value; }
		}

		[Browsable(false)]
		public Struct.TextStyle TextFont
		{
			get { return this.RatFont; }
			set { this.RatTextStyle = value; }
		}

		public bool Open(string filename)
		{
			return this.OpenFile(filename);
		}

		bool ItopVector.IItopVector.Save(string filename)
		{
			this.svgDocument.FilePath=filename;

			return this.save(false);
		}

		public bool ExportImageToClipboard(ExportType type)
		{

			Bitmap bmp =getBmp(type);
			//			Rectangle rect = new Rectangle(Point.Empty,bmp.Size);
			//
			//			Bitmap TheClippedBmp = new Bitmap((int) rect.Width,(int) rect.Height)  ;   
			//			Graphics Gra = Graphics.FromImage(TheClippedBmp);
			////			Gra.FillRectangle(this.backColor, 0,0,(int) rect.Width,(int) rect.Height);
			//			Gra.DrawImage(bmp, new Rectangle(0, 0,(int) rect.Width,(int) rect.Height), rect, GraphicsUnit.Pixel) ; 

			if (bmp !=null)
			{
				DataObject data = new DataObject(DataFormats.Bitmap,bmp);

				Clipboard.SetDataObject(data,true);
			}
			return true;
		}
		private Bitmap getSelectedBmp()
		{
			SvgElementCollection collection= this.svgDocument.SelectCollection;
			if (collection.Count==0)return null;

			RectangleF contectBounds = this.ContentBounds;

			
			RectangleF rect =Rectangle.Empty;
			foreach(IGraph graph1 in collection)
			{
				if(rect==Rectangle.Empty)
					rect = graph1.GPath.GetBounds(graph1.Transform.Matrix);
				else
					rect = RectangleF.Union(rect,graph1.GPath.GetBounds(graph1.Transform.Matrix));
			}	
			rect.Width +=20;
			rect.Height+=20;
			Bitmap bmp=new Bitmap((int)rect.Width,(int)rect.Height,this.CreateGraphics());
			using(Graphics g = Graphics.FromImage(bmp))
			{
				g.CompositingQuality = CompositingQuality.HighQuality;
				g.Clear((backColor as SolidBrush).Color);
				Matrix matrix1=((SVG)this.SVGDocument.RootElement).GraphTransform.Matrix;
				matrix1.Reset();
				matrix1.Translate(-rect.X,-rect.Y);
				g.TranslateTransform(10,10);
				foreach(IGraph graph1 in collection)
				{
					graph1.GraphTransform.Matrix = matrix1.Clone();
					graph1.Draw(g,0);
				}				
			}
			return bmp;

		}
		private Bitmap getBmp(ExportType type)
		{
			Bitmap bmp =null;
			if(type == ExportType.All)
			{
				bmp = getAllBmp();
			}
			else if(type ==ExportType.Selected)
			{
				bmp = getSelectedBmp();
			}
			return bmp;
		}
		private Bitmap getAllBmp()
		{
			RectangleF contectBounds = this.ContentBounds;
			contectBounds.Width +=20;
			contectBounds.Height+=20;
			SizeF ef2 = this.ContentBounds.Size;
			SizeF ef1 = this.DocumentSize;
			ef1 =ef2;//new SizeF(ef2.Width>ef1.Width?ef2.Width:ef1.Width,ef2.Height>ef1.Height?ef2.Height:ef1.Height);
			float single1 = ef1.Width +20;//+ 3700f;
			float single2 = ef1.Height+20;// + 700f;
			if (ef1.Width != ((int) ef1.Width))
			{
				single1 = ((int) single1) + 1;
			}
			if (ef1.Height != ((int) ef1.Height))
			{
				single2 = ((int) single2) + 1;
			}
			if ((single1 <= 0f) || (single2 <= 0f))
			{
				return new Bitmap(16,16);
			}
			if (single1>10000)
			{
				single1 = 10000;
			}
			if (single2 > 10000)
			{
				single2 = 10000;
			}
			Bitmap bitmap1 = new Bitmap((int) single1, (int) single2);
			
			using (Graphics g = Graphics.FromImage(bitmap1))
			{					
				g.SmoothingMode = SmoothingMode.HighQuality;	
				g.Clear((backColor as SolidBrush).Color);

				Matrix matrix1=new Matrix();
				matrix1=((SVG)this.SVGDocument.RootElement).GraphTransform.Matrix;
				matrix1.Reset();
				matrix1.Translate(-contectBounds.Left, -contectBounds.Top);
				g.TranslateTransform(10,10);
				try
				{
					this.RenderTo(g);
				}
				finally
				{
					matrix1.Dispose();
				}
			}
			return bitmap1;							
		}
		public Bitmap GetBmp(ExportType type)
		{
			return getBmp(type);
		}
		public byte[] ExportImageToBinary(ExportType type)
		{
			Bitmap bmp = getBmp(type);
			MemoryStream ms =new MemoryStream();
			bmp.Save(ms,ImageFormat.Png);

			return ms.ToArray();
		}
		public bool ExportImage(string filename, ImageFormat filetype,ExportType type)
		{
			getBmp(type).Save(filename, filetype);
			return true;
		}
		public bool ExportImage(string filename, ImageFormat filetype)
		{
			return ExportImage(filename,filetype,ExportType.All);
		}
		public void ExportImage()
		{
			ExportImageDialog dlg =new ExportImageDialog(this);
			dlg.ShowDialog(this);
		}
		public void PaperSetup()
		{
			if (pageSetupDlg1.ShowDialog(this) == DialogResult.OK)
			{
				this.pageSetting = pageSetupDlg1.PageSettings;
			}
		}

		internal PageSettings PageSettings
		{
			get { return this.pageSetting; }
			set
			{
				if(value==null)return;
				this.pageSetting = value;
			}
		}

		public void Print()
		{
			try
			{
				ItopVector.Dialog.PrintDialog dialog1 = new ItopVector.Dialog.PrintDialog(this);
				dialog1.ShowDialog(this);
			}
			catch (Exception exception1)
			{
				MessageBox.Show(exception1.Message);
			}
		}

		public void PrintPreview()
		{
		}
        private Matrix getMatrix(IGraph graph) {
            if (graph is Use) {
                return (graph as Use).GetBaseTransf().Matrix;
            } else {
                return graph.Transform.Matrix;
            }
        }
		/// <summary>
		/// 对齐
		/// </summary>
		/// <param name="align"></param>
		public void Align(AlignType align)
		{
			if (this.svgDocument.SelectCollection.Count<2)return;

			SvgElementCollection.ISvgElementEnumerator enumerator1=this.svgDocument.SelectCollection.GetEnumerator();
			IGraph firstgraph=null;
			RectangleF rf1=RectangleF.Empty;
			switch(align)
			{
				case AlignType.Left:
					
					while(enumerator1.MoveNext())
					{
						SvgElement element1 =enumerator1.Current as SvgElement;
						if(firstgraph==null)
						{
							firstgraph =element1 as IGraph;
							using (GraphicsPath path1= firstgraph.GPath.Clone() as GraphicsPath)
							{
                                path1.Transform(getMatrix(firstgraph));
								rf1 =path1.GetBounds();
							}
							continue;
						}
						IGraph graph1 =element1 as IGraph;
						if(graph1 !=null)
						{
							using(GraphicsPath path2 = graph1.GPath.Clone() as GraphicsPath)
							{
								path2.Transform(getMatrix(graph1));
								RectangleF rf2=path2.GetBounds();

                                Transf transf = new Transf(getMatrix(graph1));
								transf.Matrix.Translate(rf1.Left - rf2.Left,0,MatrixOrder.Append);
								graph1.Transform = transf;
							}
						}
					}

					break;
				case AlignType.Right:
					while(enumerator1.MoveNext())
					{
						SvgElement element1 =enumerator1.Current as SvgElement;
						if(firstgraph==null)
						{
							firstgraph =element1 as IGraph;
							using (GraphicsPath path1= firstgraph.GPath.Clone() as GraphicsPath)
							{
                                path1.Transform(getMatrix(firstgraph));
								rf1 =path1.GetBounds();
							}
							continue;
						}
						IGraph graph1 =element1 as IGraph;
						if(graph1 !=null)
						{
							using(GraphicsPath path2 = graph1.GPath.Clone() as GraphicsPath)
							{
								path2.Transform(getMatrix(graph1));
								RectangleF rf2=path2.GetBounds();
								Transf transf=new Transf(getMatrix(graph1));
								transf.Matrix.Translate(rf1.Right - rf2.Right,0,MatrixOrder.Append);
								graph1.Transform = transf;
							}
						}
					}

					break;
				case AlignType.Top:
					while(enumerator1.MoveNext())
					{
						SvgElement element1 =enumerator1.Current as SvgElement;
						if(firstgraph==null)
						{
							firstgraph =element1 as IGraph;
							using (GraphicsPath path1= firstgraph.GPath.Clone() as GraphicsPath)
							{
                                path1.Transform(getMatrix(firstgraph));
								rf1 =path1.GetBounds();
							}
							continue;
						}
						IGraph graph1 =element1 as IGraph;
						if(graph1 !=null)
						{
							using(GraphicsPath path2 = graph1.GPath.Clone() as GraphicsPath)
							{
								path2.Transform(getMatrix(graph1));
								RectangleF rf2=path2.GetBounds();
								Transf transf=new Transf(getMatrix(graph1));
								transf.Matrix.Translate(0,rf1.Top - rf2.Top,MatrixOrder.Append);
								graph1.Transform = transf;
							}
						}
					}
					break;
				case AlignType.Bottom:
					while(enumerator1.MoveNext())
					{
						SvgElement element1 =enumerator1.Current as SvgElement;
						if(firstgraph==null)
						{
							firstgraph =element1 as IGraph;
							using (GraphicsPath path1= firstgraph.GPath.Clone() as GraphicsPath)
							{
                                path1.Transform(getMatrix(firstgraph));
								rf1 =path1.GetBounds();
							}
							continue;
						}
						IGraph graph1 =element1 as IGraph;
						if(graph1 !=null)
						{
							using(GraphicsPath path2 = graph1.GPath.Clone() as GraphicsPath)
							{
								path2.Transform(getMatrix(graph1));
								RectangleF rf2=path2.GetBounds();
								Transf transf=new Transf(getMatrix(graph1));
								transf.Matrix.Translate(0,rf1.Bottom - rf2.Bottom,MatrixOrder.Append);
								graph1.Transform = transf;
							}
						}
					}
					break;
				case AlignType.VerticalCenter:
					
					float centerx = 0f;
					while(enumerator1.MoveNext())
					{
						SvgElement element1 =enumerator1.Current as SvgElement;
						if(firstgraph==null)
						{
							firstgraph =element1 as IGraph;
							using (GraphicsPath path1= firstgraph.GPath.Clone() as GraphicsPath)
							{
                                path1.Transform(getMatrix(firstgraph));
								rf1 =path1.GetBounds();
								centerx = rf1.X + rf1.Width /2;
							}
							continue;
						}
						IGraph graph1 =element1 as IGraph;
						if(graph1 !=null)
						{
							using(GraphicsPath path2 = graph1.GPath.Clone() as GraphicsPath)
							{
								path2.Transform(getMatrix(graph1));
								RectangleF rf2=path2.GetBounds();
								float ft1=rf2.X + rf2.Width / 2;
								Transf transf=new Transf(getMatrix(graph1));
								transf.Matrix.Translate(centerx - ft1,0,MatrixOrder.Append);
								graph1.Transform = transf;
							}
						}
					}
					break;
				case AlignType.HorizontalCenter:
					float centery = 0f;
					while(enumerator1.MoveNext())
					{
						SvgElement element1 =enumerator1.Current as SvgElement;
						if(firstgraph==null)
						{
							firstgraph =element1 as IGraph;
							using (GraphicsPath path1= firstgraph.GPath.Clone() as GraphicsPath)
							{
                                path1.Transform(getMatrix(firstgraph));
								rf1 =path1.GetBounds();
								centery = rf1.Y + rf1.Height /2;
							}
							continue;
						}
						IGraph graph1 =element1 as IGraph;
						if(graph1 !=null)
						{
							using(GraphicsPath path2 = graph1.GPath.Clone() as GraphicsPath)
							{
								path2.Transform(getMatrix(graph1));
								RectangleF rf2=path2.GetBounds();
								float ft1=rf2.Y + rf2.Height / 2;
								Transf transf=new Transf(getMatrix(graph1));
								transf.Matrix.Translate(0,centery - ft1,MatrixOrder.Append);
								graph1.Transform = transf;
							}
						}
					}
					break;
			}
			this.svgDocument.NotifyUndo();

		}

		public void Clear()
		{
			this.svgDocument.Clear();
		}

		public void ClearBuffer()
		{
			this.svgDocument.ClearUndos();
		}

		void ItopVector.IItopVector.Redo()
		{
			this.svgDocument.Redo();
		}

		void ItopVector.IItopVector.Undo()
		{
			this.svgDocument.Undo();
		}

		public void Distribute(DistributeType type)
		{
			// TODO:  添加 ItopVectorControl.Distribute 实现
		}

		public void MakeSameSize(SizeType type)
		{
			// TODO:  添加 ItopVectorControl.MakeSameSize 实现
		}

		/// <summary>
		/// 变换选中对象
		/// </summary>
		/// <param name="matrix"></param>
		public void MatrixSelection(Matrix matrix)
		{
			this.MatrixSelectionEx(matrix);
		}

		public void SelectAll()
		{
			this.SelectMenuItem("selectall");
		}

		public void SelectNone()
		{
			this.SelectMenuItem("clearselects");
		}

		public String ExportSymbol(bool wholecontent, bool exportshape, bool createdocument, string id)
		{
			// TODO:  添加 ItopVectorControl.ExportSymbol 实现
			return null;
		}

		public void ShowExportSymbolDialog(string filefilter)
		{
			// TODO:  添加 ItopVectorControl.ShowExportSymbolDialog 实现
		}

		//		public void MouseDown(object sender, MouseEventArgs e)
		//		{
		//			// TODO:  添加 ItopVectorControl.MouseDown 实现
		//		}
		//
		//		public void MouseUp(object sender, MouseEventArgs e)
		//		{
		//			// TODO:  添加 ItopVectorControl.MouseUp 实现
		//		}
		//
		//		public void MouseMove(object sender, MouseEventArgs e)
		//		{
		//			// TODO:  添加 ItopVectorControl.MouseMove 实现
		//		}
		//
		//		public void MouseEnter(object sender, EventArgs e)
		//		{
		//			// TODO:  添加 ItopVectorControl.MouseEnter 实现
		//		}
		//
		//		public void MouseLeave(object sender, EventArgs e)
		//		{
		//			// TODO:  添加 ItopVectorControl.MouseLeave 实现
		//		}

		public void DocumentChanged(object sender, EventArgs e)
		{
			// TODO:  添加 ItopVectorControl.DocumentChanged 实现
		}

		void ItopVector.IItopVector.OperationChanged(object sender, EventArgs e)
		{
			// TODO:  添加 ItopVectorControl.ItopVector.IItopVector.OperationChanged 实现
		}

		#endregion
		[Browsable(false)]
		public bool FreeSelect
		{
			get{return freeSelect;}
			set{freeSelect =value;
				if(this.mouseAreaControl.SelectOperation!=null)
					this.mouseAreaControl.SelectOperation.FreeSelect =value;				
			}            
		}

		[Browsable(false)]
		public ItopVector.Selector.SymbolSelector SymbolSelector
		{
			get{return this.symbolSelector;}
			set
			{
				if(value==null || value==this.symbolSelector)return;
				this.symbolSelector=value;
			}
		}
		#region IItopVector 成员

		[Browsable(false)]
		public PropertyGrid PropertyGrid
		{
			get { return propertyGrid; }
			set
			{
				if (propertyGrid != null)
				{
					//					propertyGrid.PropertyValueChanged-=new PropertyValueChangedEventHandler(propertyGrid_PropertyValueChanged);

				}
				if (propertyGrid == value) return;

				propertyGrid = value;
				if (propertyGrid != null)
				{
					//					propertyGrid.PropertyValueChanged+=new PropertyValueChangedEventHandler(propertyGrid_PropertyValueChanged);
					propertyGrid.SelectedObjects = null;

				}
			}
		}
/**/
		[Browsable(false)]
		public ToolOperation CurrentOperation
		{
			get { return this.Operation; }
			set { this.Operation = value; }
		}

		#endregion

		public void FlipX()
		{
			this.SelectMenuItem("flipx");
		}

		public void FlipY()
		{
			this.SelectMenuItem("flipy");

		}

		public void SetOption()
		{
			ItopVector.Dialog.PreferenceWindow dlg = new ItopVector.Dialog.PreferenceWindow();
			if (dlg.ShowDialog(this) == DialogResult.OK)
				this.AttachProperty();

		}
		public bool CanEdit
		{
			get
			{
				return canEdit;
			}
			set
			{
				canEdit=value;
				this.mouseAreaControl.CanEdit=canEdit;
			}
		}
		public RectangleF SelectedRectangle
		{
			get{return viewer.SelectedRectangle;}
		}
		/*
		private void ChangeSelectEx(object sender, CollectionChangedEventArgs e)
		{
			SvgElement element1 = this.svgDocument.CurrentElement;
			if (this.propertyGrid != null)
			{
				if (this.SVGDocument.SelectCollection.Count == 1)
				{
					if (this.svgDocument.CurrentElement is ItopVector.Core.Figure.SVG)
					{
						this.propertyGrid.SelectedObject = null;
					}
					else
					{
						switch (this.svgDocument.CurrentElement.LocalName)
						{
							case "line":
							case "polyline":
							
								this.propertyGrid.SelectedObject = new ItopVector.Property.PropertyLineMarker(this.svgDocument.CurrentElement);
								break;
							case "text":
								this.propertyGrid.SelectedObject = new ItopVector.Property.PropertyText(this.svgDocument.CurrentElement);
								break;
							case "use":
							case "g":
								this.propertyGrid.SelectedObject = new ItopVector.Property.PropertyUse(this.svgDocument.CurrentElement);
								break;
							case "image":
								this.propertyGrid.SelectedObject = new ItopVector.Property.PropertyImage(this.svgDocument.CurrentElement);
								break;
							case "rect":
								this.propertyGrid.SelectedObject = new ItopVector.Property.PropertyRoundRect(this.svgDocument.CurrentElement);
								break;

							default:
								this.propertyGrid.SelectedObject = new ItopVector.Property.PropertyFill(this.svgDocument.CurrentElement);
								break;
						}
					}
				}
				else if (this.svgDocument.SelectCollection.Count == 0)
				{
					this.propertyGrid.SelectedObject = null;

				}
				else
				{
					object[] list1 = new object[this.svgDocument.SelectCollection.Count];
					for (int i = 0; i < this.svgDocument.SelectCollection.Count; i++)
					{
						SvgElement element2 = (SvgElement) this.svgDocument.SelectCollection[i];
						ItopVector.Property.PropertyBase propertybase = null;
						switch (element2.LocalName)
						{
							case "line":
							case "polyline":
					
								propertybase = new ItopVector.Property.PropertyLineMarker(element2);
								break;
							case "text":
								propertybase = new ItopVector.Property.PropertyText(element2);
								break;
							case "use":
							case "g":
								propertybase = new ItopVector.Property.PropertyUse(element2);
								break;
							case "image":
								propertybase = new ItopVector.Property.PropertyImage(element2);
								break;
							case "rect":
								propertybase = new ItopVector.Property.PropertyRoundRect(element2);
								break;
							default:
								propertybase = new ItopVector.Property.PropertyFill(element2);
								break;
						}
						list1[i] = propertybase;

					}
					this.propertyGrid.SelectedObjects = list1;
				}

			}
		}
		*/

		/// <summary>
		/// 触发Mouseclick事件
		/// </summary>
		/// <param name="element"></param>
		/// <param name="e"></param>
		internal void OnMouseDownEvent(ISvgElement element, MouseEventArgs e)
		{
			SvgElementSelectedEventArgs args =new SvgElementSelectedEventArgs(element);
			args.Mouse=e;
//			SvgElement[] elements =new SvgElement[svgDocument.SelectCollection.Count];
//			svgDocument.SelectCollection.CopyTo(elements,0);
//			args.Elements = elements;
			switch(e.Button)
			{
				case MouseButtons.Left:
					if (e.Clicks==1 && LeftClick!=null)
					{
						LeftClick(this,args);
					}
					else if(e.Clicks>1 && DoubleLeftClick!=null)
					{
						DoubleLeftClick(this,args);

						
					}
					break;
				case MouseButtons.Right:
					if (e.Clicks==1 && RightClick!=null)
					{
						if (element is SVG)
						{
							RightClick(this,args);
						}
						else
						{
							foreach(SvgElement element2 in this.svgDocument.SelectCollection)
							{
								IGraph graph2 = (IGraph)element2;
								using(GraphicsPath path4 = (GraphicsPath) graph2.GPath.Clone())
								{
									RectangleF rf = path4.GetBounds(graph2.Transform.Matrix);
									Point pt1 = this.PointToView(new Point(e.X,e.Y));
									if(rf.Contains(pt1))
									{
										RightClick(this,args);
										return;
									}
								}
							}
							args.SvgElement = this.svgDocument.RootElement;
							RightClick(this,args);
						}
					}
					else if(e.Clicks>1 && DoubleRightClick!=null)
					{
						DoubleRightClick(this,args);

					}
					break;
			}
		}
		internal void OnMoveOver(ISvgElement element, MouseEventArgs e)
		{
			SvgElementSelectedEventArgs args =new SvgElementSelectedEventArgs(element);
			args.Mouse=e;
			if (MoveOver!=null)
			{
				MoveOver(this,args);
			}
		}
		internal void OnMoveIn(ISvgElement element, MouseEventArgs e)
		{
			SvgElementSelectedEventArgs args =new SvgElementSelectedEventArgs(element);
			args.Mouse=e;
			if (MoveIn!=null)
			{
				MoveIn(this,args);
			}
		}
		internal void OnMoveOut(ISvgElement element, MouseEventArgs e)
		{
			SvgElementSelectedEventArgs args =new SvgElementSelectedEventArgs(element);
			args.Mouse=e;
			if (MoveOut!=null)
			{
				MoveOut(this,args);
			}
		}
		internal void OnDragDrop(ISvgElement element, DragEventArgs e)
		{
			if (DragAndDrop!=null)
			{
				DragAndDrop(element,e);
			}
		}
		internal void  SetToolTip(string text)
		{
			mouseAreaControl.ToolTip2.SetToolTip(mouseAreaControl,text);
			
		}
		public event ViewChangedEventHandler ViewChanged;
		private void viewer_ViewChanged(object sender, ViewChangedEventArgs e)
		{
            
			if (ViewChanged !=null)
			{
				ViewChanged(this,e);
			}
		}
		private void mouseAreaControl_DragDrop(object sender, DragEventArgs e)
		{
			if(DragAndDrop!=null)
			{
				DragAndDrop(sender,e);
			}
		}
		#region IMouseEvent 成员

		public event ItopVector.DrawArea.SvgElementEventHandler LeftClick;

		public event ItopVector.DrawArea.SvgElementEventHandler RightClick;

		public event ItopVector.DrawArea.SvgElementEventHandler DoubleLeftClick;

		public event ItopVector.DrawArea.SvgElementEventHandler DoubleRightClick;

		public event ItopVector.DrawArea.SvgElementEventHandler MoveOver;

		public event ItopVector.DrawArea.SvgElementEventHandler MoveIn;

		public event ItopVector.DrawArea.SvgElementEventHandler MoveOut;

		public event DragEventHandler DragAndDrop;

		#endregion

		private void DrawArea_BeforeDragDrop(object sender, DragEventArgs e)
		{
			if (BeforeDragDrop!=null)
				BeforeDragDrop(sender,e);
		}
	}
}