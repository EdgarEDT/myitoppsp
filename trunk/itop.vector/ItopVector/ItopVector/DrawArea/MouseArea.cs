using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml;
using System.Text;
using ItopVector.Core;
using ItopVector.Core.Animate;
using ItopVector.Core.Document;
using ItopVector.Core.Figure;
using ItopVector.Core.Func;
using ItopVector.Core.Interface;
using ItopVector.Core.Interface.Figure;
using ItopVector.Core.Types;
using ItopVector.Dialog;
using ItopVector.Resource;
using ItopVector.Core.Paint;

namespace ItopVector.DrawArea
{
	
	internal class MouseArea : Control
	{
        public event PolyLineBreakEventHandler OnPolyLineBreak;
        public event ElementMoveEventHandler OnElementMove;
		// Methods
		public MouseArea()
		{
			this.components = null;
			this.picturePanel = null;
			this.startPoint = PointF.Empty;
			this.defaultCursor = Cursors.Default;
			this.shiftDown = false;
//			this.filename = string.Empty;
//			this.selectPath = new GraphicsPath();
			this.graphCenterPoint = PointF.Empty;
			this.currentOperation = ToolOperation.None;
//			this.undostack = new UndoStack();
			this.oldPoint = Point.Empty;
			this.hori = false;
			this.oldindex = 0;
			this.mousedown = false;
			this.win32 = new Win32();
			this.lineOperation = null;
			this.SelectOperation = null;
			this.DrawOperation = null;
			this.ViewOperation = null;
			this.ColorOperation = null;
			this.BezierOperation = null;
			this.IsDrawing = false;
			this.TextOperation = null;
			this.editingOperation = null;
			this.polyOperation = null;
			this.FlipOperation = null;
			this.SubOperation = null;
			this.InitializeComponent();
			base.SetStyle(ControlStyles.DoubleBuffer | (ControlStyles.AllPaintingInWmPaint | (ControlStyles.SupportsTransparentBackColor | (ControlStyles.Selectable | ControlStyles.UserPaint))), true);
			this.CreateMenus();
		}

		internal MouseArea(ItopVector.DrawArea.DrawArea pl)
		{
			this.components = null;
			this.picturePanel = null;
			this.startPoint = PointF.Empty;
			this.defaultCursor = Cursors.Default;
			this.shiftDown = false;
//			this.filename = string.Empty;
//			this.selectPath = new GraphicsPath();
			this.graphCenterPoint = PointF.Empty;
			this.currentOperation = ToolOperation.None;
//			this.undostack = new UndoStack();
			this.oldPoint = Point.Empty;
			this.hori = false;
			this.oldindex = 0;
			this.mousedown = false;
			this.win32 = new Win32();
			this.lineOperation = null;
			this.SelectOperation = null;
			this.DrawOperation = null;
			this.ViewOperation = null;
			this.ColorOperation = null;
			this.BezierOperation = null;
			this.IsDrawing = false;
			this.TextOperation = null;
			this.editingOperation = null;
			this.polyOperation = null;
			this.FlipOperation = null;
			this.SubOperation = null;
			this.InitializeComponent();
			this.picturePanel = pl;
			base.SetStyle(ControlStyles.DoubleBuffer | (ControlStyles.AllPaintingInWmPaint | (ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint)), true);
//			base.SetStyle(ControlStyles.DoubleBuffer | (ControlStyles.AllPaintingInWmPaint | (ControlStyles.SupportsTransparentBackColor | (ControlStyles.Selectable | ControlStyles.UserPaint))), true);
			this.CreateMenus();
		}

		public void AddElement(SvgElement path)
		{
			if (path != null)
			{
				this.picturePanel.AddElement(path);
			}
            if (OnAddElement != null)
            {
                AddSvgElementEventArgs e = new AddSvgElementEventArgs(path);
                OnAddElement(path,e);
            }
		}

		public void ClearSelection()
		{
			this.SVGDocument.ClearSelects();
		}
		public void ConvertToPath(SvgElement element)
		{
			SvgElement element1 = element;
			if (!(element1 is GraphPath) || (element1.Name == "path"))
			{
				return;
			}
			SvgDocument document1 = this.SVGDocument;
			bool flag1 = document1.AcceptChanges;
			document1.AcceptChanges = false;
			GraphPath path1 = (GraphPath) document1.CreateElement(document1.Prefix, "path", document1.NamespaceURI);
			foreach (XmlAttribute attribute1 in element1.Attributes)
			{
				path1.SetAttributeNode((XmlAttribute) attribute1.Clone());
			}
			path1.InfoList = (ArrayList) element1.InfoList.Clone();
			XmlNode node1 = element1.NextSibling;
			while (!(node1 is SvgElement) && (node1 != null))
			{
				node1 = node1.NextSibling;
			}
			string text1 = PathFunc.GetPathString(((GraphPath) element1).GPath);
			AttributeFunc.SetAttributeValue(path1, "d", text1);
			SvgElementCollection collection1 = element1.AnimateList.Clone();
			for (int num1 = 0; num1 < collection1.Count; num1++)
			{
				ItopVector.Core.Animate.Animate animate1 = (ItopVector.Core.Animate.Animate) collection1[num1];
				path1.AppendChild(animate1.Clone());
			}
			document1.AcceptChanges = true;
			document1.NumberOfUndoOperations = (element1.AnimateList.Count*2) + 200;
			if (node1 == null)
			{
				element1.ParentNode.AppendChild(path1);
			}
			else
			{
				element1.ParentNode.InsertBefore(path1, node1);
			}
			this.SVGDocument.ClearSelects();
			this.SVGDocument.AddSelectElement(path1);
			element1.ParentNode.RemoveChild(element1);
			document1.NotifyUndo();
			document1.AcceptChanges = flag1;
		}

		public void ConvertToPath()
		{
			if (this.SVGDocument.SelectCollection.Count == 1)
			{
				SvgElement element1 = (SvgElement) this.SVGDocument.SelectCollection[0];
				
				this.ConvertToPath(element1);
			}
		}

		public void ConvertToSymbol(SvgElement element)
		{
			SvgDocument document1 = this.SVGDocument;
			bool flag1 = document1.AcceptChanges;
			XmlNode node1 = element.NextSibling;
			SvgElement element1 = (SvgElement) element.ParentNode;
			while (!(node1 is SvgElement) && (node1 != null))
			{
				node1 = node1.NextSibling;
			}
			document1.AcceptChanges = false;
			Symbol symbol1 = (Symbol) document1.CreateElement(document1.Prefix, "symbol", document1.NamespaceURI);
			document1.AcceptChanges = true;
			document1.NumberOfUndoOperations = 4;
			document1.AddDefsElement(symbol1);
			symbol1.AppendChild(element);
			document1.AcceptChanges = false;
			string text1 = CodeFunc.CreateString(document1, "symbol");
			symbol1.ID = text1;
			Use use1 = (Use) document1.CreateElement(document1.Prefix, "use", document1.NamespaceURI);
			use1.GraphId = "#" + text1;
			document1.AcceptChanges = true;
			if (node1 != null)
			{
				element1.InsertBefore(use1, node1);
			}
			else
			{
				element1.AppendChild(use1);
			}
			document1.AcceptChanges = flag1;
		}

		public void Copy()
		{
			SvgDocument document1 = this.SVGDocument;
			DataFormats.Format format1 = DataFormats.GetFormat("SvgElement");
			bool flag1 = document1.AcceptChanges;
			document1.AcceptChanges = false;
			SVG svg1 = (SVG) document1.DocumentElement.CloneNode(false);
			SvgElementCollection.ISvgElementEnumerator enumerator1 = this.SVGDocument.SelectCollection.GetEnumerator();
			StringBuilder outxml=new StringBuilder();
			while (enumerator1.MoveNext())
			{
				SvgElement element1 = (SvgElement) enumerator1.Current;
				outxml.Append(element1.OuterXml);
//				if (element1 is IGraph)
//				{
//					SvgElement[] elementArray1 = element1.CloneElements();
//					for (int num1 = 0; num1 < elementArray1.Length; num1++)
//					{
//						SvgElement element2 = elementArray1[num1];
//						svg1.AppendChild(element2);
//					}
//				}
			}
			svg1.InnerXml=outxml.ToString();
			document1.AcceptChanges = flag1;
			CopyData data1 = new CopyData(svg1.OuterXml);
			DataObject obj1 = new DataObject(format1.Name, data1);
			Clipboard.SetDataObject(obj1);
		}

		private void CreateMenus()
		{
		}

		public void Cut()
		{
			SvgDocument document1 = this.SVGDocument;
			DataFormats.Format format1 = DataFormats.GetFormat("SvgElement");
			bool flag1 = document1.AcceptChanges;
			document1.AcceptChanges = false;
			SVG svg1 = (SVG) document1.DocumentElement.CloneNode(false);
			SvgElementCollection.ISvgElementEnumerator enumerator1 = this.SVGDocument.SelectCollection.GetEnumerator();
			while (enumerator1.MoveNext())
			{
				SvgElement element1 = (SvgElement) enumerator1.Current;
				if (element1 is IGraph)
				{
					SvgElement[] elementArray1 = element1.CloneElements();
					svg1.FormatOutXml = false;
					SvgElement[] elementArray2 = elementArray1;
					for (int num1 = 0; num1 < elementArray2.Length; num1++)
					{
						SvgElement element2 = elementArray2[num1];
						svg1.AppendChild(element2);
					}
				}
			}
			document1.AcceptChanges = flag1;
			this.Delete();
			CopyData data1 = new CopyData(svg1.OuterXml);
			DataObject obj1 = new DataObject(format1.Name, data1);
			Clipboard.SetDataObject(obj1);
		}

		public virtual bool DealDialogKey(Keys keyData)
		{
			bool flag1 = false;
			if(this.CanEdit==false)
			{
				goto Label_028C;
			}
			if (this.editingOperation != null)
			{
				flag1 = this.editingOperation.ProcessDialogKey(keyData);
				if (flag1)
				{
					return true;
				}
			}
			if (!flag1)
			{
				Keys keys1 = keyData;
				if (keys1 <= Keys.Delete)
				{
					if (keys1 == Keys.Tab)
					{
						int num1 = 0;
						if (this.SVGDocument.SelectCollection.Count > 0)
						{
							SvgElement element1 = (SvgElement) this.SVGDocument.SelectCollection[this.SVGDocument.SelectCollection.Count - 1];
							num1 = this.picturePanel.ElementList.IndexOf(element1);
							if ((num1 < 0) || (num1 == (this.picturePanel.ElementList.Count - 1)))
							{
								num1 = 0;
							}
							else
							{
								num1 = Math.Max(0, Math.Min(num1 + 1, this.picturePanel.ElementList.Count - 1));
							}
						}
						if ((num1 >= 0) && (num1 < this.picturePanel.ElementList.Count))
						{
							this.SVGDocument.ClearSelects();
							this.SVGDocument.AddSelectElement((IGraph) this.picturePanel.ElementList[num1]);
						}
						return true;
					}
					switch (keys1)
					{
						case Keys.Left:
							{
								this.Translate(-1, 0);
								return true;
							}
						case Keys.Up:
							{
								this.Translate(0, -1);
								return true;
							}
						case Keys.Right:
							{
								this.Translate(1, 0);
								return true;
							}
						case Keys.Down:
							{
								this.Translate(0, 1);
								return true;
							}
						case Keys.Delete:
							{
                                //this.Delete();
								return true;
							}
					}
				}
				else
				{
					switch (keys1)
					{
						case (Keys.Control | Keys.A):
							{
								this.SelectCurrentLay();
								return true;
							}
						case (Keys.Control | Keys.B):
						case (Keys.Control | Keys.E):
						case (Keys.Control | Keys.F):
						case (Keys.Control | Keys.W):
						case (Keys.Control | Keys.Y):
							{
								goto Label_028C;
							}
						case (Keys.Control | Keys.C):
							{
								this.Copy();
								return true;
							}
						case (Keys.Control | Keys.D):
							{
								this.ClearSelection();
								return true;
							}
						case (Keys.Control | Keys.G):
							{
								this.Group(this.SVGDocument.SelectCollection);
								return true;
							}
						case (Keys.Control | Keys.V):
							{
								this.Paste();
								return true;
							}
						case (Keys.Control | Keys.X):
							{
								this.Cut();
								return true;
							}
						case (Keys.Control | Keys.Z):
							{
								if (this.editingOperation != null)
								{
									return this.editingOperation.Undo();
								}
								return false;
							}
						case (Keys.Control | (Keys.Shift | Keys.G)):
							{
								if (this.picturePanel.CurrentElement is ItopVector.Core.Figure.Group)
								{
									ItopVector.Core.Figure.Group group1 = (ItopVector.Core.Figure.Group) this.picturePanel.CurrentElement;
									this.UnGroup(group1);
								}
								return true;
							}
					}
				}
			}
			Label_028C:
			return false;
		}

		public void Delete()
		{
			bool flag1 = this.SVGDocument.AcceptChanges;
			this.SVGDocument.AcceptChanges = true;
			this.SVGDocument.NumberOfUndoOperations = this.SVGDocument.SelectCollection.Count;
			SvgElementCollection.ISvgElementEnumerator enumerator1 = this.SVGDocument.SelectCollection.GetEnumerator();
			while (enumerator1.MoveNext())
			{
				SvgElement element1 = (SvgElement) enumerator1.Current;
				if (element1.ParentNode is SvgElement)
				{
					if (element1 is IGraph)
						(element1 as IGraph).RemoveAllConnectLines();
					if(element1 is ConnectLine)
					{
						IGraph graph= (element1 as ConnectLine).StartGraph;
						if (graph !=null)
							graph.ConnectLines.Remove(element1);
						graph= (element1 as ConnectLine).EndGraph;
						if (graph !=null)
							graph.ConnectLines.Remove(element1);

					}
					element1.ParentNode.RemoveChild(element1);					
				}
			}
			this.SVGDocument.AcceptChanges = flag1;
			this.SVGDocument.ClearSelects();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
			if(this.editingOperation!=null)
				this.editingOperation.Dispose();
		}

		public void GoBottom(SvgElement element)
		{
			if (element.ParentNode != null&& !(element is SVG))
			{
				SvgElement element1 = (SvgElement) element.ParentNode;
				bool flag1 = this.picturePanel.SVGDocument.AcceptChanges;
				this.picturePanel.SVGDocument.AcceptChanges = true;
				this.picturePanel.SVGDocument.NumberOfUndoOperations = 2;
//				(element as IGraph).Layer.Remove(element);
				element1.RemoveChild(element);
				element.AllowRename =false;
				element =element1.PrependChild(element) as SvgElement;
				element.AllowRename =true;
				this.picturePanel.SVGDocument.AcceptChanges = flag1;				
//				(element as IGraph).Layer.GraphList.Insert(0,element);
			}
		}

		public void GoDown(SvgElement element)
		{
			if (element.ParentNode != null&& !(element is SVG))
			{
				SvgElement element1 = (SvgElement) element.ParentNode;
				XmlNode node1 = element.PreviousSibling;
				while (!(node1 is SvgElement) && (node1 != null))
				{
					node1 = node1.PreviousSibling;
				}
				if (node1 != null)
				{
					bool flag1 = this.picturePanel.SVGDocument.AcceptChanges;
					this.picturePanel.SVGDocument.AcceptChanges = true;
					this.picturePanel.SVGDocument.NumberOfUndoOperations = 2;
					element1.RemoveChild(element);
					element.AllowRename =false;
					element1.InsertBefore(element, node1);
					element.AllowRename =true;
					this.picturePanel.SVGDocument.AcceptChanges = flag1;
				}
			}
		}

		public void GoTop(SvgElement element)
		{
			if (element.ParentNode != null&& !(element is SVG))
			{
				SvgElement element1 = (SvgElement) element.ParentNode;
				bool flag1 = this.picturePanel.SVGDocument.AcceptChanges;
				this.picturePanel.SVGDocument.AcceptChanges = true;
				this.picturePanel.SVGDocument.NumberOfUndoOperations = 2;
				element1.RemoveChild(element);
				element.AllowRename =false;
				element1.AppendChild(element);
				element.AllowRename =true;
				this.picturePanel.SVGDocument.AcceptChanges = flag1;
			}
		}

		public void GoUp(SvgElement element)
		{
			if (element.ParentNode != null && !(element is SVG))
			{
				SvgElement element1 = (SvgElement) element.ParentNode;
				XmlNode node1 = element.NextSibling;
				while (!(node1 is SvgElement) && (node1 != null))
				{
					node1 = node1.NextSibling;
				}
				if (node1 != null)
				{
					bool flag1 = this.picturePanel.SVGDocument.AcceptChanges;
					this.picturePanel.SVGDocument.AcceptChanges = true;
					this.picturePanel.SVGDocument.NumberOfUndoOperations = 2;
					element1.RemoveChild(element);
					element.AllowRename =false;
					element1.InsertAfter(element, node1);
					element.AllowRename =true;
					this.picturePanel.SVGDocument.AcceptChanges = flag1;
				}
			}
		}

		public void Group(SvgElementCollection collection)
		{
			if (collection.Count >= 2)
			{
				SvgDocument document1 = this.SVGDocument;
//				bool flag1 = document1.AcceptChanges;
				document1.AcceptChanges = false;
				ItopVector.Core.Figure.Group group1 = (ItopVector.Core.Figure.Group) document1.CreateElement(document1.Prefix, "g", document1.NamespaceURI);
				SvgElement element1 = null;
				SvgElementCollection.ISvgElementEnumerator enumerator1 = collection.GetEnumerator();
				while (enumerator1.MoveNext())
				{
					SvgElement element2 = (SvgElement) enumerator1.Current;
					if (element1 == null)
					{
						element1 = (SvgElement) element2.ParentNode;
						continue;
					}
					if (element1 != element2.ParentNode)
					{
						MessageBox.Show(DrawAreaConfig.GetLabelForName("notinsamelayer"));
						return;
					}
				}
				if (element1 is ItopVector.Core.Figure.Group)
				{
					document1.AcceptChanges = true;
					document1.NumberOfUndoOperations = 200 + (2*collection.Count);
					element1.InsertBefore(group1, (SvgElement) collection[0]);
					for (int num1 = collection.Count - 1; num1 >= 0; num1--)
					{
						SvgElement element3 = (SvgElement) collection[num1];
						element3.AllowRename =false;
						group1.PrependChild(element3);
						element3.AllowRename =true;
					}
					((XmlElement)group1).SetAttribute("layer",SvgDocument.currentLayer);
					this.SVGDocument.ClearSelects();
					this.SVGDocument.CurrentElement = group1;
					this.SVGDocument.NotifyUndo();
				}
			}
		}

		private void InitializeComponent()
		{
			this.AllowDrop = true;
			base.DragEnter += new DragEventHandler(this.MouseAreaControl_DragEnter);
			base.DragDrop += new DragEventHandler(this.MouseAreaControl_DragDrop);
		}

		public void Link()
		{
			SvgElementCollection collection1 = this.SVGDocument.SelectCollection;
			if (collection1.Count != 0)
			{
				SvgDocument document1 = this.SVGDocument;
//				bool flag1 = document1.AcceptChanges;
				document1.AcceptChanges = false;
				ItopVector.Core.Figure.Link link1 = (ItopVector.Core.Figure.Link) document1.CreateElement(document1.Prefix, "a", document1.NamespaceURI);
				SvgElement element1 = null;
				SvgElementCollection.ISvgElementEnumerator enumerator1 = collection1.GetEnumerator();
				while (enumerator1.MoveNext())
				{
					SvgElement element2 = (SvgElement) enumerator1.Current;
					if (element1 == null)
					{
						element1 = (SvgElement) element2.ParentNode;
						continue;
					}
					if (element1 != element2.ParentNode)
					{
						MessageBox.Show(DrawAreaConfig.GetLabelForName("notinsamelayer"));
						return;
					}
				}
				LinkDialog dialog1 = new LinkDialog();
				if (dialog1.ShowDialog(this) == DialogResult.OK)
				{
					string text1 = dialog1.Link;
					string text2 = dialog1.Target;
					if (text1.Length > 0)
					{
						AttributeFunc.SetAttributeValue(link1, "xlink:href", text1);
						if (text2.Length > 0)
						{
							AttributeFunc.SetAttributeValue(link1, "target", text2);
						}
						if (element1 is ItopVector.Core.Figure.Group)
						{
							document1.AcceptChanges = true;
							document1.NumberOfUndoOperations = 200 + (2*collection1.Count);
							element1.InsertBefore(link1, (SvgElement) collection1[0]);
							AttributeFunc.SetAttributeValue(link1, "xlink:href", text1);
							for (int num1 = collection1.Count - 1; num1 >= 0; num1--)
							{
								SvgElement element3 = (SvgElement) collection1[num1];
								element3.AllowRename =false;
								link1.PrependChild(element3);
								element3.AllowRename =true;
							}
							this.SVGDocument.ClearSelects();
							this.SVGDocument.CurrentElement = link1;
							this.SVGDocument.NotifyUndo();
						}
					}
				}
			}
		}

		private void MouseAreaControl_DragDrop(object sender, DragEventArgs e)
		{
			
			object obj1 = e.Data.GetData("SvgElement");
			if (obj1 is IGraph)
			{
				if (BeforeDragDrop!=null)
					BeforeDragDrop(obj1,e);
				if (e.Effect == DragDropEffects.None)return;

				IGraph graph1 = (IGraph) obj1;
				if (graph1 == this.SVGDocument.RootElement)
				{
					MessageBox.Show("不能将对象应用到自身！");
				}
				
				else
				{
					SvgDocument document1 = this.SVGDocument;
					if (graph1 is ConnectLine)
					{
						Point point2 = this.picturePanel.PointToView(base.PointToClient(new Point(e.X, e.Y)));
						
						ConnectLine connect =document1.ImportNode((SvgElement)graph1,true) as ConnectLine;

						#region delete						
						//						ConnectLine connect =document1.CreateElement("tonli","connectline",SvgDocument.TonliNamespace) as ConnectLine;
						//					
						//						foreach(XmlAttribute attribute in (graph1 as SvgElement).Attributes )
						//						{
						//							if(attribute.LocalName!="id" && attribute.LocalName !="tonli")
						//							{
						//								AttributeFunc.SetAttributeValue(connect,attribute.LocalName,attribute.Value);
						//							}
						//						}
						#endregion

						connect.X1 += point2.X;
						connect.X2 += point2.X;
						connect.Y1 += point2.Y;
						connect.Y2 += point2.Y;
						this.picturePanel.AddElement(connect);

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
						Point point1 = this.picturePanel.PointToView(base.PointToClient(new Point(e.X, e.Y)));
						RectangleF ef1 = graph1.GPath.GetBounds(graph1.Transform.Matrix);
						float X = (point1.X - ef1.X) - (ef1.Width/2f);
						float Y = (point1.Y - ef1.Y) - (ef1.Height/2f);
                        Transf tf = new Transf();
                        tf.Matrix.Translate(X, Y);
                        use1.Transform = tf;

						use1.GraphBrush = this.picturePanel.FillBrush.Clone();
						use1.GraphStroke = this.picturePanel.Stroke.Clone() as Stroke;
						this.picturePanel.AddElement(use1);
                        if (OnAddElement!=null)
                        {
                            AddSvgElementEventArgs eTemp = new AddSvgElementEventArgs(use1);
                            OnAddElement(use1,eTemp);
                        }
//						this.picturePanel.InvalidateElement(use1);
					}
				}
				if(DragAndDrop!=null)
				{
					DragAndDrop(graph1,e);
				}
			}
		}		

		private void MouseAreaControl_DragEnter(object sender, DragEventArgs e)
		{
			if (!CanEdit)return;
			if (e.Data.GetDataPresent("SvgElement"))
			{
				e.Effect = DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			base.Focus();
			if (!this.picturePanel.firstload && (this.SVGDocument != null))
			{
				if (this.editingOperation != null)
				{
					this.editingOperation.OnMouseDown(e);
				}
				this.picturePanel.InvadatePosLine();
				this.mousedown = true;
				//事件
				if (this.SVGDocument.SelectCollection.Count>0 && e.Clicks>1)
				{
					isDoubleClick=true;
					this.picturePanel.OnMouseDownEvent(this.SVGDocument.SelectCollection[0],e);
				}
				if(this.SVGDocument.SelectCollection.Count==0)// && currentOperation==ToolOperation.Text)
				{
					this.picturePanel.OnMouseDownEvent(this.SVGDocument.RootElement,e); 
				}
                if (currentOperation == ToolOperation.Text)
                {
                    InputDialog dcd1 = new InputDialog();
                    dcd1.InputString = "";
                    if (dcd1.ShowDialog() == DialogResult.OK)
                    {
                       Text t=(Text) this.SVGDocument.CreateElement(this.SVGDocument.Prefix, "text", this.SVGDocument.NamespaceURI);
                       t.TextString = dcd1.InputString;
                       PointF p1 = this.picturePanel.PointToView(new PointF(e.X,e.Y));
                       t.X = p1.X;
                       t.Y = p1.Y;
                       t.Layer = this.SVGDocument.CurrentLayer;
                       this.SVGDocument.RootElement.AppendChild(t);
                       CurrentOperation = ToolOperation.Select;
                    }
                    else
                    {
                        CurrentOperation = ToolOperation.Select;
                    }
                }
			}
		}
		bool isDoubleClick=false;

		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (this.SVGDocument != null)
			{
				this.picturePanel.ToolTip(string.Empty, 1);
				this.picturePanel.ToolTip(string.Empty, 0);
				this.picturePanel.InvadatePosLine();
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if ((!this.picturePanel.firstload && (this.SVGDocument != null)))
			{
				SizeF ef1 = this.picturePanel.GridSize;
				float single1 = ef1.Height;
				float single2 = ef1.Width;
				Point point1 = this.picturePanel.PointToView(new Point(e.X, e.Y));
				this.picturePanel.ToolTip(point1.X.ToString() + "," + point1.Y.ToString(), 0);
				this.picturePanel.InvadatePosLine();
				if (((this.picturePanel.ShowGuides && !this.picturePanel.lockGuides) && (e.Button == MouseButtons.None)) && (OperationFunc.IsSelectOperation(this.currentOperation) || OperationFunc.IsTransformOperation(this.currentOperation)))
				{
					this.oldindex = 0;
					foreach (RefLine line1 in this.picturePanel.RefLines)
					{
						if ((line1.Hori && (Math.Abs(point1.Y - line1.Pos) < 2)) || (!line1.Hori && (Math.Abs(point1.X - line1.Pos) < 2)))
						{
							this.hori = line1.Hori;
							this.oldPoint = base.PointToClient(Control.MousePosition);
							this.Cursor = SpecialCursors.DragInfoCursor;
							this.picturePanel.ToolTip("移动鼠标以拖动辅助线，拖出工作区删除辅助线", 1);
							return;
						}
						this.oldindex++;
					}
					this.oldindex = -1;
				}
				if (((this.picturePanel.ShowGuides && !this.picturePanel.lockGuides) && ((e.Button == MouseButtons.Left) && this.mousedown)) && (((this.oldindex >= 0) && (this.oldindex < this.picturePanel.RefLines.Count)) && (OperationFunc.IsSelectOperation(this.currentOperation) || OperationFunc.IsTransformOperation(this.currentOperation))))
				{
					if (this.hori)
					{
						this.win32.hdc = this.win32.W32GetDC(base.Handle);
						this.win32.W32SetROP2(7);
						GraphicsPath path1 = new GraphicsPath();
						path1.AddLine(new PointF(0f, (float) this.oldPoint.Y), new PointF((float) base.Width, (float) this.oldPoint.Y));
						this.win32.W32PolyDraw(path1);
						this.oldPoint = this.picturePanel.PointToView(base.PointToClient(Control.MousePosition));
						if (this.picturePanel.SnapToGrid)
						{
							int num1 = (int) ((this.oldPoint.X + (single2/2f))/single2);
							int num2 = (int) ((this.oldPoint.Y + (single1/2f))/single1);
							this.oldPoint = new Point((int) (num1*single2), (int) (num2*single1));
						}
						this.oldPoint = Point.Round(this.picturePanel.PointToSystem(new PointF((float) this.oldPoint.X, (float) this.oldPoint.Y)));
						path1.Reset();
						path1.AddLine(new PointF(0f, (float) this.oldPoint.Y), new PointF((float) base.Width, (float) this.oldPoint.Y));
						this.win32.W32PolyDraw(path1);
						this.win32.ReleaseDC();
						path1.Dispose();                        
					}
					else
					{
						this.win32.hdc = this.win32.W32GetDC(base.Handle);
						this.win32.W32SetROP2(7);
						GraphicsPath path2 = new GraphicsPath();
						path2.AddLine(new PointF((float) this.oldPoint.X, 0f), new PointF((float) this.oldPoint.X, (float) base.Height));
						this.win32.W32PolyDraw(path2);
						this.oldPoint = this.picturePanel.PointToView(base.PointToClient(Control.MousePosition));
						if (this.picturePanel.SnapToGrid)
						{
							int num3 = (int) ((this.oldPoint.X + (single2/2f))/single2);
							int num4 = (int) ((this.oldPoint.Y + (single1/2f))/single1);
							this.oldPoint = new Point((int) (num3*single2), (int) (num4*single1));
						}
						this.oldPoint = Point.Round(this.picturePanel.PointToSystem(new PointF((float) this.oldPoint.X, (float) this.oldPoint.Y)));
						path2.Reset();
						path2.AddLine(new PointF((float) this.oldPoint.X, 0f), new PointF((float) this.oldPoint.X, (float) base.Height));
						this.win32.W32PolyDraw(path2);
						this.win32.ReleaseDC();
						path2.Dispose();
					}
				}
				else if (this.editingOperation != null)
				{
					if(moving)return;
					DateTime time1 =DateTime.Now;
					
					moving =true;
					this.editingOperation.OnMouseMove(e);

					moving =false;
					DateTime time2 =DateTime.Now;

					TimeSpan ts=time2-time1;

					this.picturePanel.ToolTip(ts.ToString(), 1);
				}
			}
		}
     
		bool moving=false;
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (this.SVGDocument == null)
			{
				return;
			}
			if (this.SVGDocument.PlayAnim)
			{
				return;
			}
			base.OnMouseUp(e);
			if (this.picturePanel.firstload)
			{
				return;
			}
			this.picturePanel.InvadatePosLine();
			if (e.Button == MouseButtons.Right)
			{
				if (this.CurrentOperation != ToolOperation.IncreaseView && this.CurrentOperation != ToolOperation.DecreaseView)
					this.picturePanel.NotifyContextMenu(Control.MousePosition);
				goto Label_0374;
			}
			if ((((e.Button != MouseButtons.Left) || !this.mousedown) || ((this.oldindex < 0) || (this.oldindex >= this.picturePanel.RefLines.Count))) || (!OperationFunc.IsSelectOperation(this.currentOperation) && !OperationFunc.IsTransformOperation(this.currentOperation)))
			{  
				goto Label_0374;
			}
			Point point1 = this.picturePanel.PointToView(base.PointToClient(Control.MousePosition));
			RefLine line1 = (RefLine) this.picturePanel.RefLines[this.oldindex];
			int num1 = line1.Pos;
			Matrix matrix1 = this.picturePanel.CoordTransform.Clone();
			SizeF ef1 = this.picturePanel.GridSize;
			float single1 = ef1.Width;
			float single2 = ef1.Height;
			if (this.picturePanel.SnapToGrid)
			{
				int num2 = (int) ((point1.X + (single1/2f))/single1);
				int num3 = (int) ((point1.Y + (single2/2f))/single2);
				point1 = new Point((int) (num2*single1), (int) (num3*single2));
			}
			point1 = Point.Round(this.picturePanel.PointToSystem(new PointF((float) point1.X, (float) point1.Y)));
			if (this.hori)
			{
				Point[] pointArray3 = new Point[1] {new Point(0, num1)};
				Point[] pointArray1 = pointArray3;
				matrix1.TransformPoints(pointArray1);
				this.picturePanel.Invalidate(new System.Drawing.Rectangle(0, pointArray1[0].Y - 4, base.Width, 8));
				this.picturePanel.Invalidate(new System.Drawing.Rectangle(0, point1.Y - 1, base.Width, 2));
				if ((point1.Y < 0) || (point1.Y > base.Height))
				{
					this.picturePanel.RefLines.RemoveAt(this.oldindex);
					goto Label_0372;
				}
				point1 = this.picturePanel.PointToView(point1);
				this.picturePanel.RefLines[this.oldindex] = new RefLine(point1.Y, this.hori);
				return;
			}
			Point[] pointArray4 = new Point[1] {new Point(num1, 0)};
			Point[] pointArray2 = pointArray4;
			matrix1.TransformPoints(pointArray2);
			this.picturePanel.Invalidate(new System.Drawing.Rectangle(pointArray2[0].X - 4, 0, 8, base.Height));
			this.picturePanel.Invalidate(new System.Drawing.Rectangle(point1.X - 4, 0, 8, base.Height));
			if ((point1.X < 0) || (point1.X > base.Width))
			{
				this.picturePanel.RefLines.RemoveAt(this.oldindex);
				return;
			}
			point1 = this.picturePanel.PointToView(point1);
			this.picturePanel.RefLines[this.oldindex] = new RefLine(point1.X, this.hori);
			Label_0372:
			return;
			Label_0374:
			if (this.editingOperation != null)
			{
				this.editingOperation.OnMouseUp(e);                
			}
			this.oldindex = -1;
			this.mousedown = false;
			//事件
			if (this.SVGDocument.SelectCollection.Count>0 && !isDoubleClick)
			{              
				this.picturePanel.OnMouseDownEvent(this.SVGDocument.SelectCollection[0],e);
			}
			isDoubleClick=false;
		}
      

		protected override void OnPaint(PaintEventArgs e)
		{
			if (this.editingOperation != null)
			{
				if (this.SVGDocument != null)
					e.Graphics.SmoothingMode = this.SVGDocument.SmoothingMode;
				this.editingOperation.OnPaint(e);
			}
		}

		public void Paste()
		{
			DataFormats.Format format1 = DataFormats.GetFormat("SvgElement");
			IDataObject obj1 = Clipboard.GetDataObject();
			try
			{
				if (!obj1.GetDataPresent(format1.Name))
				{
					return;
				}
				object obj2 = obj1.GetData(format1.Name);
				if (!(obj2 is CopyData))
				{
					return;
				}
				string text1 = ((CopyData) obj2).XmlStr;
				SvgDocument document1 = this.picturePanel.SVGDocument;
				bool flag1 = document1.AcceptChanges;
				document1.AcceptChanges = false;
				XmlDocumentFragment fragment1 = document1.CreateDocumentFragment();
				bool flag2 = document1.firstload;
				document1.firstload = true;
				fragment1.InnerXml = text1;
				//document1.DealLast();
				document1.firstload = flag2;
				XmlNode node1 = fragment1.FirstChild;
				document1.AcceptChanges = true;
				if (!(node1 is SVG))
				{
					return;
				}
				document1.NumberOfUndoOperations = (2*node1.ChildNodes.Count) + 200;
				DateTime dt1=DateTime.Now;
				this.picturePanel.BeginInsert();
//				for (int num1 = 0; num1 < node1.ChildNodes.Count; num1++)
//				{
//					XmlNode node2 = node1.ChildNodes[num1];
//					if (node2 is IGraph)
//					{此方法count在减少，内部XML功能，不好控制，所以撇了
//						this.picturePanel.AddElement((SvgElement) node2);
//						num1--;
//					}
//				}
				foreach(XmlNode node2 in node1.ChildNodes)
				{
					if(node2 is IGraph)
					{
						this.picturePanel.AddElement(node2.CloneNode(true) as SvgElement);
					}
				}
				this.picturePanel.EndInsert();
				
				document1.AcceptChanges = flag1;
				document1.NotifyUndo();

//				DateTime dt2=DateTime.Now;
//				TimeSpan tsp=dt2 -dt1;
//				this.picturePanel.ToolTip(tsp.ToString()+tsp.Milliseconds.ToString(),1);

			}
			catch (Exception)
			{
				MessageBox.Show("粘贴对象失败!");
			}
		}

		#region delete
		private void Paste2()
		{
			DataFormats.Format format1 = DataFormats.GetFormat("SvgElement");
			IDataObject obj1 = Clipboard.GetDataObject();
			try
			{
				if (!obj1.GetDataPresent(format1.Name))
				{
					return;
				}
				object obj2 = obj1.GetData(format1.Name);
				if (!(obj2 is CopyData))
				{
					return;
				}
				string text1 = ((CopyData) obj2).XmlStr;
				SvgDocument document1 = this.picturePanel.SVGDocument;
				bool flag1 = document1.AcceptChanges;
				SvgDocument fadde2 = SvgDocumentFactory.CreateDocument();
				fadde2.LoadXml("<?xml version=\"1.0\"?>\r\n" + text1);
				SvgElement node1 = (SvgElement) fadde2.RootElement;
				if (node1 == null)
				{
					return;
				}
				document1.AcceptChanges = true;

				document1.NumberOfUndoOperations = (2*node1.ChildNodes.Count) + 200;
				DateTime dt1=DateTime.Now;
				for (int num1 = 0; num1 < node1.ChildNodes.Count; num1++)
				{
					XmlNode node2 = node1.ChildNodes[num1];
					if (node2 is IGraph)
					{
						SvgElement element1=document1.ImportNode(node2,true) as SvgElement;
						this.picturePanel.AddElement(element1);
					}
				}
				document1.AcceptChanges = flag1;
				document1.NotifyUndo();

				DateTime dt2=DateTime.Now;
				TimeSpan tsp=dt2 -dt1;
				this.picturePanel.ToolTip(tsp.ToString()+tsp.Milliseconds.ToString(),1);

			}
			catch (Exception)
			{
				MessageBox.Show("粘贴对象失败!");
			}
		}
		#endregion

		private void PostBezier(object sender, EventArgs e)
		{
			if (this.BezierOperation != null)
			{
				SvgElement element1 = (SvgElement) this.picturePanel.PreGraph;
				if (element1 is GraphPath)
				{
					this.BezierOperation.CurrentGraph = (IPath) ((SvgElement) this.picturePanel.PreGraph).Clone();
				}
			}
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			return (this.DealDialogKey(keyData) || base.ProcessDialogKey(keyData));
		}

		public bool Redo()
		{
			if (this.editingOperation != null)
			{
				return this.editingOperation.Redo();
			}
			return false;
		}
		public void SelectCurrentLay()
		{
			this.SVGDocument.ClearSelects();

			
			if(this.SVGDocument.CurrentLayer.GraphList.Count>0)
			{
				this.SVGDocument.SelectCollection.AddRange(this.SVGDocument.CurrentLayer.GraphList);
			}
			else
			{
				this.SVGDocument.CurrentElement = this.SVGDocument.RootElement;
			}
		}

		public void SelectAll()
		{
			this.SVGDocument.ClearSelects();

			SvgElementCollection collection1=new SvgElementCollection();
			
			foreach(Layer layer in SVGDocument.Layers)
			{
				if (!layer.Visible)continue;
				foreach(IGraph graph1 in layer.GraphList)
				{
					if (!graph1.IsLock)
					{
						collection1.Add(graph1);
					}
				}
			}
			if(collection1.Count>0)
			{
				this.SVGDocument.SelectCollection.AddRange(collection1);
			}
			else
			{
				this.SVGDocument.CurrentElement = this.SVGDocument.RootElement;
			}
		}

		private void Translate(int x, int y)
		{
			SvgElementCollection.ISvgElementEnumerator enumerator1 = this.SVGDocument.SelectCollection.GetEnumerator();
			while (enumerator1.MoveNext())
			{
				SvgElement element1 = (SvgElement) enumerator1.Current;
				if (element1 == null)
				{
					return;
				}
				if (!(element1 is IGraph))
				{
					return;
				}
				IGraph graph1 = (IGraph) element1;
				Matrix matrix3 =new Matrix();
				if(graph1.LimitSize && graph1 is Use)
				{						
					Use use1 =graph1 as Use;
					using (Matrix matrix1=this.picturePanel.CoordTransform.Clone())
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
				{
					Matrix matrix2 = graph1.Transform.Matrix.Clone();
					matrix2.Multiply(matrix3);
					matrix2.Translate((float) x, (float) y, MatrixOrder.Append);
					graph1.Transform = new Transf(matrix2);
				}
			}			
		}

		public bool Undo()
		{
			if (this.editingOperation != null)
			{
				return this.editingOperation.Undo();
			}
			return false;
		}

		public void UnGroup(ItopVector.Core.Figure.Group group)
		{
			if (group.ParentNode is ItopVector.Core.Figure.Group)
			{
				string LayerID=group.GetAttribute("layer");
				XmlNode node1 = group.PreviousSibling;
				while (!(node1 is SvgElement) && (node1 != null))
				{
					node1 = node1.PreviousSibling;
				}
				SvgElementCollection collection1 = group.GraphList;
				SvgDocument document1 = this.SVGDocument;
				document1.NumberOfUndoOperations += ((2*collection1.Count) + 500);
				ItopVector.Core.Figure.Group group1 = (ItopVector.Core.Figure.Group) group.ParentNode;
				Matrix matrix1 = group.Transform.Matrix.Clone();
				for (int num1 = collection1.Count - 1; num1 >= 0; num1--)
				{
					SvgElement element1 = (SvgElement) collection1[num1];
					if (element1 is IGraph)
					{
						Matrix matrix2 = new Matrix();
						if (element1.SvgAttributes.ContainsKey("transform"))
						{
							matrix2 = ((Matrix) element1.SvgAttributes["transform"]).Clone();
						}
						matrix2.Multiply(matrix1, MatrixOrder.Append);
						Transf transf1 = new Transf();
						transf1.setMatrix(matrix2);
						string text1 = transf1.ToString();
						AttributeFunc.SetAttributeValue(element1, "transform", text1);
						AttributeFunc.SetAttributeValue(element1, "layer", LayerID);
					}
					element1.AllowRename =false;
					if (node1 != null)
					{
						group1.InsertAfter(element1, node1);
					}
					else
					{
						group1.PrependChild(element1);
					}
					element1.AllowRename =true;
				}
				group1.RemoveChild(group);
				this.SVGDocument.NotifyUndo();
			}
		}

		public void UnGroup2(ItopVector.Core.Figure.Group group)
		{
			if (group.ParentNode is ItopVector.Core.Figure.Group)
			{
				string LayerID=group.GetAttribute("layer");
				XmlNode node1 = group.PreviousSibling;
				while (!(node1 is SvgElement) && (node1 != null))
				{
					node1 = node1.PreviousSibling;
				}
				SvgElementCollection collection1 = group.GraphList;
				SvgDocument document1 = this.SVGDocument;
				document1.NumberOfUndoOperations += ((2*collection1.Count) + 500);
				ItopVector.Core.Figure.Group group1 = (ItopVector.Core.Figure.Group) group.ParentNode;
				Matrix matrix1 = group.Transform.Matrix.Clone();
				for (int num1 = collection1.Count - 1; num1 >= 0; num1--)
				{
					SvgElement element1 = (SvgElement) collection1[num1];
					if (element1 is IGraph)
					{
						Matrix matrix2 = new Matrix();
						if (element1.SvgAttributes.ContainsKey("transform"))
						{
							matrix2 = ((Matrix) element1.SvgAttributes["transform"]).Clone();
						}
						matrix2.Multiply(matrix1, MatrixOrder.Append);
						Transf transf1 = new Transf();
						transf1.setMatrix(matrix2);
						string text1 = transf1.ToString();
						AttributeFunc.SetAttributeValue(element1, "transform", text1);
//						AttributeFunc.SetAttributeValue(element1, "layer", LayerID);
					}
					element1.AllowRename =false;
					if (node1 != null)
					{
						group1.InsertAfter(element1, node1);
					}
					else
					{
						group1.PrependChild(element1);
					}
					element1.AllowRename =true;
				}
				group1.RemoveChild(group);
				this.SVGDocument.NotifyUndo();
			}
		}
		public void UnLink()
		{
			ItopVector.Core.Figure.Link link1 = null;
			if (this.SVGDocument.CurrentElement is ItopVector.Core.Figure.Link)
			{
				link1 = (ItopVector.Core.Figure.Link) this.SVGDocument.CurrentElement;
			}
			if (link1 != null)
			{
				if (!(link1.ParentNode is ItopVector.Core.Figure.Group))
				{
					return;
				}
				XmlNode node1 = link1.PreviousSibling;
				while (!(node1 is SvgElement) && (node1 != null))
				{
					node1 = node1.PreviousSibling;
				}
				SvgElementCollection collection1 = link1.GraphList;
				SvgDocument document1 = this.SVGDocument;
				document1.NumberOfUndoOperations += ((2*collection1.Count) + 500);
				ItopVector.Core.Figure.Group group1 = (ItopVector.Core.Figure.Group) link1.ParentNode;
//				Matrix matrix1 = link1.Transform.Matrix.Clone();
				for (int num1 = collection1.Count - 1; num1 >= 0; num1--)
				{
					SvgElement element1 = (SvgElement) collection1[num1];
					element1.AllowRename =false;
					if (node1 != null)
					{
						group1.InsertAfter(element1, node1);
					}
					else
					{
						group1.PrependChild(element1);
					}
					element1.AllowRename =true;
				}
				group1.RemoveChild(link1);
				this.SVGDocument.NotifyUndo();
			}
		}


		// Properties
		public PointF CenterPoint
		{
			get { return this.graphCenterPoint; }
			set { this.graphCenterPoint = value; }
		}

		public ToolOperation CurrentOperation
		{
			get { return this.currentOperation; }
			set
			{
                if (!(OperationFunc.IsSelectOperation(value) || OperationFunc.IsTransformOperation(value) || OperationFunc.IsConnectLineOperation(value)))
                {
                    elementMoveFlag = true;
                }         
				this.currentOperation = value;
				if (this.picturePanel != null)
				{
					if (this.picturePanel.Operation != value)
					{
						this.picturePanel.Operation = value;
					}
                    if ((this.SubOperation != null) && (value != ToolOperation.ShapeTransform) && (value != ToolOperation.Custom11) && (value != ToolOperation.Custom12) && (value != ToolOperation.Custom13) && (value != ToolOperation.Custom14)
                        && (value != ToolOperation.Custom15))
					{
						this.SubOperation.Dispose();
                        elementMoveFlag = true;
					}
                    if (value == ToolOperation.ShapeTransform || value == ToolOperation.Custom11 || value == ToolOperation.Custom12 || value == ToolOperation.Custom13 || value == ToolOperation.Custom14 || value == ToolOperation.Custom15)
					{                       
						SvgElement element1 = this.SVGDocument.CurrentElement;
						IPath path1 = null;
						if (element1 is IPath)
						{
							path1 = (IPath) element1;
						}
						if (path1 != null)
						{
							this.SubOperation = new ItopVector.DrawArea.SubOperation(this, path1);
						}
						else
						{
							this.SubOperation = new ItopVector.DrawArea.SubOperation(this);
						}
						this.DefaultCursor = SpecialCursors.dotCursor;
                        if (element1 is Polyline)
                        {
                            this.polyOperation.OnPolyLineBreak += new PolyLineBreakEventHandler(polyOperation_OnPolyLineBreak);
                        }                        
					}
					else if ((value == ToolOperation.Bezier) || (value == ToolOperation.ConvertAnchor))
					{
						if (this.BezierOperation == null)
						{
							SvgElement element2 = this.picturePanel.CurrentElement;
							IPath path2 = null;
							if (element2 is MotionAnimate)
							{
								path2 = (IPath) element2;
							}
							if (path2 == null)
							{
								element2 = (SvgElement) this.picturePanel.PreGraph;
								if (element2 != null)
								{
									this.picturePanel.PreGraph.GraphTransform.Matrix = this.picturePanel.CoordTransform.Clone();
									if (element2.Name.Trim() == "path")
									{
										path2 = (GraphPath) element2;
									}
								}
							}
							if (path2 != null)
							{
								this.BezierOperation = new ItopVector.DrawArea.BezierOperation(this, path2);
							}
							else
							{
								this.BezierOperation = new ItopVector.DrawArea.BezierOperation(this);
							}
						}
						if ((this.editingOperation != null) && (this.editingOperation != this.BezierOperation))
						{
							this.editingOperation.Dispose();
						}
						this.editingOperation = this.BezierOperation;
						this.DefaultCursor = SpecialCursors.bezierCursor;
					}
					else if (value == ToolOperation.Text)
					{
						if (this.TextOperation == null)
						{
							this.TextOperation = new ItopVector.DrawArea.TextOperation(this);
						}
						if ((this.editingOperation != null) && (this.editingOperation != this.TextOperation))
						{
							this.editingOperation.Dispose();
						}
						this.editingOperation = this.TextOperation;
						this.DefaultCursor = SpecialCursors.TextCursor;
					}
					else if ((value == ToolOperation.Polygon) || (value == ToolOperation.PolyLine) ||(value == ToolOperation.Enclosure) 
                        || (value==ToolOperation.InterEnclosure)
                        || (value==ToolOperation.AreaPolygon) ||(value==ToolOperation.LeadLine)
						||(value == ToolOperation.Confines_XiangJie) || (value == ToolOperation.Confines_GuoJie) || (value == ToolOperation.Confines_ShengJie)
						||(value == ToolOperation.Confines_ShiJie)   ||(value == ToolOperation.Confines_XianJie) || (value == ToolOperation.Railroad) ||
						(value == ToolOperation.XPolyLine) || (value == ToolOperation.YPolyLine))
					{
                        if (this.polyOperation == null)
                        {
                            this.polyOperation = new PolyOperation(this);
                        }
                        if ((this.editingOperation != null) && (this.editingOperation != this.polyOperation))
                        {
                            this.editingOperation.Dispose();
                        }
                        SvgElement element3 = this.SVGDocument.CurrentElement;
                        SvgElement element4 = null;
                        if ((element4 == null) && (this.picturePanel.PreGraph is IGraphPath))
                        {
                            element4 = (SvgElement)this.picturePanel.PreGraph;
                        }
                        if (element4 is IGraphPath)
                        {
                            this.polyOperation.CurrentGraph = (IGraphPath)element4;
                        }
                        this.editingOperation = this.polyOperation;
                        this.DefaultCursor = SpecialCursors.PolyDraw;
                    }               
					else if (OperationFunc.IsDrawOperation(value))
					{
						if (this.DrawOperation == null)
						{
							this.DrawOperation = new ItopVector.DrawArea.DrawOperation(this);
						}
						if ((this.editingOperation != null) && (this.editingOperation != this.DrawOperation))
						{
							this.editingOperation.Dispose();
						}
						this.editingOperation = this.DrawOperation;
						this.DefaultCursor = SpecialCursors.drawCursor;
					}
					else if (value == ToolOperation.Symbol)
					{
						this.editingOperation = new SymbolOperation(this);
						this.DefaultCursor = SpecialCursors.drawCursor;
					}
					else if (value == ToolOperation.Distance)
					{
						//测距
						this.editingOperation = new DistanceOperation(this);
						this.defaultCursor = SpecialCursors.drawCursor;
					}
					else if (value == ToolOperation.Flip)
					{
						if (this.FlipOperation == null)
						{
							this.FlipOperation = new ItopVector.DrawArea.FlipOperation(this);
						}
						if ((this.editingOperation != null) && (this.editingOperation != this.FlipOperation))
						{
							this.editingOperation.Dispose();
						}
						this.editingOperation = this.FlipOperation;
						this.DefaultCursor = SpecialCursors.selectCursor;
					}
					else if (OperationFunc.IsSelectOperation(value) || OperationFunc.IsTransformOperation(value) || OperationFunc.IsConnectLineOperation(value))
					{
						if (this.SelectOperation == null)
						{
							this.SelectOperation = new ItopVector.DrawArea.SelectOperation(this);
						}
						if ((this.editingOperation != this.SelectOperation) && (this.editingOperation != null))
						{
							this.editingOperation.Dispose();
						}
						this.editingOperation = this.SelectOperation;
                        if (elementMoveFlag)
                        {
                            this.SelectOperation.OnElementMove += new ElementMoveEventHandler(SelectOperation_OnElementMove);
                            elementMoveFlag = false;
                        }                 
						this.DefaultCursor = SpecialCursors.selectCursor;
						if(value==ToolOperation.WindowZoom)
						{
							this.DefaultCursor = SpecialCursors.WindowZoom;
						}
						
					}
					else if (OperationFunc.IsViewOperation(value))
					{
						if (this.ViewOperation == null)
						{
							this.ViewOperation = new ItopVector.DrawArea.ViewOperation(this);
						}
						if ((this.editingOperation != null) && (this.editingOperation != this.ViewOperation))
						{
							this.editingOperation.Dispose();
						}
						this.editingOperation = this.ViewOperation;
						if (value == ToolOperation.IncreaseView)
						{
							this.DefaultCursor = SpecialCursors.increaseCursor;
						}
						else if (value == ToolOperation.DecreaseView)
						{
							this.DefaultCursor = SpecialCursors.decreaseCursor;
						}
						else
						{
							if (value != ToolOperation.Roam)
							{
								return;
							}
							this.DefaultCursor = SpecialCursors.handCurosr;
						}
					}
					else if (OperationFunc.IsColorOperation(value))
					{
						if (this.ColorOperation == null)
						{
							this.ColorOperation = new ItopVector.DrawArea.ColorOperation(this);
						}
						if ((this.editingOperation != null) && (this.editingOperation != this.ColorOperation))
						{
							this.editingOperation.Dispose();
						}
						this.editingOperation = this.ColorOperation;
						if (value == ToolOperation.GradientTransform)
						{
							this.DefaultCursor = SpecialCursors.GradientTransformCursor;
						}
						else if (value == ToolOperation.ColorPicker)
						{
							this.DefaultCursor = SpecialCursors.ColorPickerCursor;
						}
					}
					
				}
			}
		}

        void SelectOperation_OnElementMove(object sender, MoveEventArgs e)
        {
            if (OnElementMove!=null)
            {
                OnElementMove(sender, e);
            }
        }

        void polyOperation_OnPolyLineBreak(object sender, BreakElementEventArgs e)
        {
            if (OnPolyLineBreak!=null)
            {
                OnPolyLineBreak(sender,e);
            }
        }

		public Cursor DefaultCursor
		{
			get { return this.defaultCursor; }
			set
			{
				this.defaultCursor = value;
				this.Cursor = value;
			}
		}

		internal ItopVector.DrawArea.DrawArea PicturePanel
		{
			get { return this.picturePanel; }
			set { this.picturePanel = value; }
		}

		public bool ShiftDown
		{
			get { return this.shiftDown; }
			set
			{
				if (this.shiftDown != value)
				{
					this.shiftDown = value;
				}
			}
		}

		public PointF StartPoint
		{
			get { return this.startPoint; }
		}

		public SvgDocument SVGDocument
		{
			get { return this.picturePanel.SVGDocument; }
		}
		public ToolTip ToolTip2
		{
			get
			{
				if(toolTip==null)
					toolTip =new ToolTip();
				return toolTip;
			}
			
		}
	


		// Fields
		ToolTip toolTip;
		public ItopVector.DrawArea.BezierOperation BezierOperation;
		public ItopVector.DrawArea.ColorOperation ColorOperation;
		private Container components;
		private ToolOperation currentOperation;
		private Cursor defaultCursor;
		public ItopVector.DrawArea.DrawOperation DrawOperation;
		public ItopVector.DrawArea.IOperation editingOperation;
//		private string filename;
		public ItopVector.DrawArea.FlipOperation FlipOperation;
		private PointF graphCenterPoint;
		private bool hori;
		public bool IsDrawing;
		private bool mousedown;
		private int oldindex;
		private Point oldPoint;
		private ItopVector.DrawArea.DrawArea picturePanel;
		public ItopVector.DrawArea.PolyOperation polyOperation;
		public ItopVector.DrawArea.LineOperation lineOperation;
		public ItopVector.DrawArea.SelectOperation SelectOperation;
//		private GraphicsPath selectPath;
		private bool shiftDown;
		private PointF startPoint;
		public ItopVector.DrawArea.SubOperation SubOperation;
		public ItopVector.DrawArea.TextOperation TextOperation;
//		private UndoStack undostack;
		public ItopVector.DrawArea.ViewOperation ViewOperation;
		public Win32 win32;
		public bool CanEdit=true;
        private bool elementMoveFlag = true;
		public static int ConnectLinePoints=0;
		public static Point ConLineStartPoint;
		public static Point ConLineEndPoint;
		public static ISvgElement SvgStartObj;
		public static ISvgElement SvgEndObj;
		public event DragEventHandler DragAndDrop;
        public event AddSvgElementEventHandler OnAddElement;
		public event DragEventHandler BeforeDragDrop;
	}
}