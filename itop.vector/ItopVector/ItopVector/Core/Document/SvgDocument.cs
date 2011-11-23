using ItopVector.Core;
using ItopVector.Core.Animate;
using ItopVector.Core.ClipAndMask;
using ItopVector.Core.Figure;
using ItopVector.Core.Func;
using ItopVector.Core.Interface;
using ItopVector.Core.Interface.Figure;
using ItopVector.Core.Paint;
using ItopVector.Core.SVGTextNode;
using ItopVector.Core.UnDo;
using System;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using System.Drawing;
namespace ItopVector.Core.Document
{

	public class SvgDocument : XmlDocument,IDisposable
	{
		// Events
		public event ControlTimeChangeEventHandler OnControlTimeChanged;
		public event OnDocumentChangedEventHandler OnDocumentChanged;
		public event EventHandler OnDocumentEditRootChanged;
		public event EventHandler OnInKeyChanged;
		public event EventHandler OnNotifyChange;
		public event EventHandler OnPlayAnimStatusChanged;
		public event EventHandler OnRecordAnimChangedEvent;
		public event EventHandler OnUpdateChanged;
//		public event SvgElementChangeEventHandler CurrentElementChanged;

		// Methods
		static SvgDocument()
		{
			SvgDocument.TonliNamespace="http://www.tonli.com/tonli";
			SvgDocument.SvgNamespace = "http://www.w3.org/2000/svg";
			SvgDocument.XLinkNamespace = "http://www.w3.org/1999/xlink";
			SvgDocument.AudioNamespace = "http://ns.adobe.com/AdobeSVGViewerExtensions/3.0/";
			string[] textArray1 = new string[4] { "org.w3c.svg.static", "http://www.w3.org/TR/Svg11/feature#Shape", "http://www.w3.org/TR/Svg11/feature#BasicText", "http://www.w3.org/TR/Svg11/feature#OpacityAttribute" } ;
			SvgDocument.supportedFeatures = new ArrayList(textArray1);
		}

		public SvgDocument()
		{
			this.styleElements = new ArrayList();
			this.recordanim = true;
			this.playAnim = false;
			this.controltime = 0;
			this.filename = "未命名";
			this.update = true;
			this.undoStack = new UndoStack();
			this.xmlreader = null;
			this.preelement = null;
			this.elements = new ArrayList(0x10);
			this.infos = new Hashtable(0x10);
			this.groups = new ArrayList(0x10);
			this.undoGroup = new ArrayList(0x10);
			this.errorinfos = new ArrayList(0x10);
			this.selectCollection = new SvgElementCollection();
			this.oldSelect = new SvgElementCollection();
			this.root = null;
			this.serialize = null;
			this.editRoots = new SvgElementCollection();
			//this.styleSheetList = null;
			this.ChangeElements = new SvgElementCollection();
			this.NumberOfUndoOperations = 1;
			this.ValidPath = new GraphicsPath();
			this.firstload = false;
			this.FilePath = string.Empty;
			this.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
			this.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
			this.Changed = false;
			this.SelectChanged = false;
			this.OnlyShowCurrent = false;
			this.DefsChanged = true;
			this.OldControlTime = 0;
			this.PlayAnimChanged = false;
			this.AutoShowAnim = false;
			this.XmlParserContext = null;
			XmlNamespaceManager manager1 = new XmlNamespaceManager(base.NameTable);
			manager1.PushScope();
			manager1.AddNamespace("svg", SvgDocument.SvgNamespace);
			manager1.AddNamespace("xlink", SvgDocument.XLinkNamespace);
			manager1.AddNamespace("a", SvgDocument.AudioNamespace);
			manager1.AddNamespace("tonli", SvgDocument.TonliNamespace);
			base.NodeChanged += new XmlNodeChangedEventHandler(this.ChangeNode);
			base.NodeInserted += new XmlNodeChangedEventHandler(this.ChangeNode);
			base.NodeRemoved += new XmlNodeChangedEventHandler(this.ChangeNode);
			this.XmlParserContext = new System.Xml.XmlParserContext(base.NameTable, manager1, "", "svg", "-//W3C//DTD SVG 1.1//EN", "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd", this.BaseURI, "zh", XmlSpace.None, Encoding.UTF8);
			this.selectCollection.NotifyEvent = true;
			this.selectCollection.OnCollectionChangedEvent += new OnCollectionChangedEventHandler(this.ChangeSelect);
			this.serialize = new SerializeDocument(this);
			this.AddStyleElement(SvgDocument.SvgNamespace, "style");
			this.SvgdataUid=string.Empty;
			layers =new SvgElementCollection();
		}

		public string SelectCollectionToString()
		{			
			string head=string.Empty;

			Rectangle bounds1= getBounds(this.selectCollection);

			bounds1.Inflate(10,10);
			head = SvgDocumentFactory.CreateDocument(new Size(bounds1.Width,bounds1.Height)).RootElement.OuterXml;

			StringBuilder sb = new StringBuilder();

			sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");

			sb.Append("\r\n");
			sb.Append(head.Replace("</svg>",""));
			sb.Append("\r\n");
			XmlNodeList defsList = GetElementsByTagName("defs");
			if(defsList.Count>0)
			{
				sb.Append("<defs>\r\n");
				foreach(XmlNode node in defsList)
				{
					sb.Append(node.InnerXml);
					sb.Append("\r\n");
				}
				sb.Append("</defs>");
			}
			Point offset =new Point(bounds1.X,bounds1.Y);
			IList connectLines =new ArrayList();
			foreach(Graph graph1 in selectCollection)
			{
				getOuterXml(graph1,offset,connectLines, sb,selectCollection);
				sb.Append("\r\n");
			}
			sb.Append("</svg>");
			
			return sb.ToString();
		}
		private string getOuterXml(Graph graph1,Point offset,IList connectLines ,StringBuilder sb,SvgElementCollection selectCollection)
		{
			XmlElement node = graph1.Clone() as XmlElement;
			if(graph1 is ConnectLine)
			{
				ConnectLine cline = graph1 as ConnectLine;
				if(connectLines.Contains(graph1.ID) ||(cline.StartGraph==null||cline.EndGraph==null)||(!selectCollection.Contains(cline.StartGraph)||!selectCollection.Contains(cline.EndGraph))) return string.Empty;

				connectLines.Add(graph1.ID);
			}else
			{
				Matrix matrix1 =new Matrix();
				if (graph1.SvgAttributes.ContainsKey("transform"))
				{
					matrix1 = (graph1.SvgAttributes["transform"] as Matrix).Clone();
				}
				matrix1.Translate(-offset.X,-offset.Y,MatrixOrder.Append);
				node.SetAttribute("transform", new ItopVector.Core.Types.Transf(matrix1).ToString());
			}

			sb.Append(node.OuterXml);

			foreach(Graph graph2 in graph1.ConnectLines)
			{
				getOuterXml(graph2,Point.Empty,connectLines, sb,selectCollection);
			}

			return sb.ToString();
		}
		private System.Drawing.Rectangle getBounds(ICollection graphList)
		{
			System.Drawing.RectangleF contectbounds=System.Drawing.Rectangle.Empty;
			foreach(IGraph graph1 in graphList)
			{
				System.Drawing.RectangleF rtf1=PathFunc.GetBounds(graph1.GPath,graph1.Transform.Matrix);
				if(rtf1==System.Drawing.RectangleF.Empty)continue;
				if(contectbounds==System.Drawing.RectangleF.Empty)
				{
					contectbounds=rtf1;
				}
				else
				{
					contectbounds= System.Drawing.RectangleF.Union(contectbounds,rtf1);
				}
			}
			return System.Drawing.Rectangle.Ceiling(contectbounds);
		}
		public bool DefsElementContains(SvgElement element,out IGraph graph)
		{
			XmlNode node1 = base.DocumentElement.SelectSingleNode("//*[@id='"+element.GetAttribute("id")+"']");
			graph=null;
			if (node1 !=null)graph = node1 as IGraph;
			return node1!=null;
		}
		public bool DefsElementContains(SvgElement element)
		{
			XmlNode node1 = base.DocumentElement.SelectSingleNode("//*[@id='"+element.GetAttribute("id")+"']");
			
			return node1!=null;
		}
		public XmlNode AddDefsElement(SvgElement element)
		{
			this.DefsChanged = true;
			bool flag1 = this.AcceptChanges;
			this.AcceptChanges = false;
			XmlNode node1 = base.DocumentElement.SelectSingleNode("*[name()='defs']");
			this.AcceptChanges = flag1;
			if (!(node1 is SvgElement))
			{
				this.AcceptChanges = false;
				node1 = this.CreateElement(this.Prefix, "defs", this.NamespaceURI);
				this.AcceptChanges = true;
				this.NumberOfUndoOperations++;
				base.DocumentElement.PrependChild(node1);
			}
			this.AcceptChanges = flag1;
			if (node1 is SvgElement)
			{
				string nid=element.GetAttribute("id");
				XmlNode node= node1.SelectSingleNode("svg/defs/*[@id='"+nid+"']");
				if(node !=null)
				{
					return node;
				}
				else
				{
					return ((SvgElement) node1).AppendChild(element);
				}
			}
			return null;
		}

		public void AddFlowElement(SvgElement element)
		{
			this.serialize.AddElement(element);
//			if (this.OnDocumentChanged != null)
//			{
//				DocumentChangedEventArgs args1 = new DocumentChangedEventArgs();
//				SvgElement[] elementArray1 = new SvgElement[1] { element } ;
//				args1.ChangeElements = elementArray1;
//				this.OnDocumentChanged(this, args1);
//			}
		}

		public void AddSelectElement(ISvgElement element)
		{
			if (!this.selectCollection.Contains(element))
			{
				this.selectCollection.Add(element);
			}
		}

		public void AddStyleElement(string ns, string localName)
		{
			string[] textArray1 = new string[2] { ns, localName } ;
			this.styleElements.Add(textArray1);
		}

		public void ChangeElementExpand(SvgElement changeelement, bool oldexpand, bool animate)
		{
			if ((changeelement is IContainer) && !animate)
			{
				SvgElementCollection collection1 = new SvgElementCollection();
				if ((changeelement is IGraph) && !(changeelement is ClipPath))
				{
					SvgElement element1 = ((IGraph) changeelement).ClipPath;
					if (element1 != null)
					{
						collection1.Add(element1);
					}
				}
				collection1.AddRange(((IContainer) changeelement).ChildList);
				if (this.serialize != null)
				{
					this.serialize.UpdateElementChilds(changeelement, oldexpand, collection1);
				}
			}
			else if (animate)
			{
				SvgElementCollection collection2 = new SvgElementCollection();
				collection2.AddRange(changeelement.AnimateList);
				if (this.serialize != null)
				{
					this.serialize.UpdateElementChilds(changeelement, oldexpand, collection2);
				}
			}
			if (this.OnDocumentChanged != null)
			{
				DocumentChangedEventArgs args1 = new DocumentChangedEventArgs();
				SvgElement[] elementArray1 = new SvgElement[1] { changeelement } ;
				args1.ChangeElements = elementArray1;
				this.OnDocumentChanged(this, args1);
			}
		}

		private void ChangeNode(object sender, XmlNodeChangedEventArgs e)
		{
			if (((e.Action == XmlNodeChangedAction.Remove) && (e.OldParent is SvgElement)) && (e.Node is XmlAttribute))
			{
				((SvgElement) e.OldParent).RemoveAttribute((XmlAttribute) e.Node);
			}
			else if ((e.NewParent is SvgElement) && (e.Node is XmlAttribute))
			{
				((SvgElement) e.NewParent).AddAttribute((XmlAttribute) e.Node);
			}
			if (this.AcceptChanges && !this.firstload)
			{
				this.Update = false;
				XmlNode node1 = e.Node;
				XmlNode node2 = e.NewParent;
				XmlNode node3 = e.OldParent;
				XmlNodeChangedAction action1 = e.Action;
				SvgElement element1 = null;
				int num1 = this.undoGroup.Count;
				if ((this.ChangeElements.Count > 0) && (num1 < this.ChangeElements.Count))
				{
					element1 = (SvgElement) this.ChangeElements[num1];
				}
				if (node3 is SvgElement)
				{
					((SvgElement) node3).pretime = -1;
				}
				if (node2 is SvgElement)
				{
					((SvgElement) node2).pretime = -1;
				}
				if (element1 != null)
				{
					element1.pretime = -1;
				}
				UnDoOperation operation1 = new UnDoOperation(this, node1, node3, node2, action1, element1);
				this.PushUndo(operation1);
				if (this.OnDocumentChanged != null)
				{
					DocumentChangedEventArgs args1 = new DocumentChangedEventArgs();
					args1.ChangeElements = new ISvgElement[this.ChangeElements.Count];
					this.ChangeElements.CopyTo(args1.ChangeElements, 0);
					this.OnDocumentChanged(this, args1);
				}
			}
			else
			{
				this.ChangeElements.Clear();
			}
		}

		private void ChangeSelect(object sender, CollectionChangedEventArgs e)
		{
		}

		public void ClearSelects()
		{
			this.selectCollection.Clear();
		}

		public void CollapseAll()
		{
			this.serialize.FlowChilds.Clear();
			this.serialize.FlowChilds.Add(this.RootElement);
			if (this.OnDocumentChanged != null)
			{
				this.OnDocumentChanged(this, new DocumentChangedEventArgs());
			}
		}

		public override XmlAttribute CreateAttribute(string prefix, string localName, string ns)
		{
			return base.CreateAttribute(prefix, localName, ns);
		}

		public override XmlElement CreateElement(string prefix, string localName, string ns)
		{
			XmlElement element1;
			if (this.firstload)
			{
//				SvgElement element2 = null;
				if (this.preelement != null)
				{
					//                    if (this.preelement.ParentNode == null)
					//                    {
					//                        if (this.groups.Count > 0)
					//                        {
					//                            element2 = (SvgElement) this.groups[this.groups.Count - 1];
					//                            if ((element2 is ContainerElement) && ((ContainerElement) element2).IsValidChild(this.preelement))
					//                            {
					//                                ((ContainerElement) element2).ChildList.Add(this.preelement);
					//                            }
					//                        }
					//                        this.groups.Add(this.preelement);
					//                    }
					//                    else if (this.groups.Count > 0)
					//                    {
					//						if(this.preelement.ParentNode is ContainerElement && ((ContainerElement) this.preelement.ParentNode).IsValidChild(this.preelement))
					//						{
					//							((ContainerElement)this.preelement.ParentNode).ChildList.Add(this.preelement);
					//						}
					//						
					//                    }
				}
			}
			switch (localName)
			{
				case "clipPath":
				{
					element1 = new ClipPath(prefix, localName, ns, this);
					break;
				}
				case "rect":
				{
					element1 = new RectangleElement(prefix, localName, ns, this);
					break;
				}
				case "path":
				{
					element1 = new GraphPath(prefix, localName, ns, this);
					break;
				}
				case "polyline":
				{
					element1 = new Polyline(prefix, localName, ns, this);
					break;
				}
				case "polygon":
				{
					element1 = new Polygon(prefix, localName, ns, this);
					break;
				}
				case "circle":
				{
					element1 = new Circle(prefix, localName, ns, this);
					break;
				}
				case "ellipse":
				{
					element1 = new Ellips(prefix, localName, ns, this);
					break;
				}
				case "script":
				{
					element1 = new SvgScript(prefix, localName, ns, this);
					break;
				}
				case "line":
				{
					element1 = new Line(prefix, localName, ns, this);
					break;
				}
				case "connectline":
				case "connect":
				{
					element1 = new ConnectLine(prefix, localName, ns, this);
					break;
				}
				case "g":
				{
					element1 = new Group(prefix, localName, ns, this);
					break;
				}
				case "svg":
				{
					element1 = new SVG(prefix, localName, ns, this);
					break;
				}
				case "text":
				{
					element1 = new Text(prefix, localName, ns, this);
					break;
				}
				case "tspan":
				{
					element1 = new TSpan(prefix, localName, ns, this);
					break;
				}
				case "tref":
				{
					element1 = new TRef(prefix, localName, ns, this);
					break;
				}
				case "linearGradient":
				{
					element1 = new LinearGradient(prefix, localName, ns, this);
					break;
				}
				case "radialGradient":
				{
					element1 = new RadialGradients(prefix, localName, ns, this);
					break;
				}
				case "stop":
				{
					element1 = new GradientStop(prefix, localName, ns, this);
					break;
				}
				case "symbol":
				{
					element1 = new ItopVector.Core.Figure.Symbol(prefix, localName, ns, this);
					break;
				}
				case "marker":
				{
					element1 = new ItopVector.Core.Figure.Marker(prefix, localName, ns, this);
					break;
				}
				case "defs":
				{
					element1 = new ItopVector.Core.Figure.Defs(prefix, localName, ns, this);
					break;
				}
				case "image":
				{
					element1 = new ItopVector.Core.Figure.Image(prefix, localName, ns, this);
					break;
				}
				case "a":
				{
					element1 = new ItopVector.Core.Figure.Link(prefix, localName, ns, this);
					break;
				}
				case "use":
				{
					element1 = new ItopVector.Core.Figure.Use(prefix, localName, ns, this);
					break;
				}
				case "animate":
				{
					element1 = new ItopVector.Core.Animate.Animate(prefix, localName, ns, this);
					break;
				}
				case "set":
				{
					element1 = new SetAnimate(prefix, localName, ns, this);
					break;
				}
				case "animateColor":
				{
					element1 = new ColorAnimate(prefix, localName, ns, this);
					break;
				}
				case "animateMotion":
				{
					element1 = new MotionAnimate(prefix, localName, ns, this);
					break;
				}
				case "animateTransform":
				{
					element1 = new TransformAnimate(prefix, localName, ns, this);
					break;
				}
				case "pattern":
				{
					element1 = new Pattern(prefix, localName, ns, this);
					break;
				}
				case "audio3d":
				case "audio":
				{
					element1 = new AudioAnimate(prefix, localName, ns, this);
					break;
				}
				case "state"://状态
				{
					element1 =new State(prefix, localName, ns, this);
					break;
				}
				case "layer":
				{
					element1 =new Layer(prefix, localName, ns, this);
					break;
				}
				default:
				{
					element1 = base.CreateElement(prefix, localName, ns);
					break;
				}
			}
			if (element1 is SvgElement)
			{
				((SvgElement) element1).ShowParticular = this.AutoShowAnim;
			}
			if ((element1 is SvgElement) && this.firstload)
			{
				this.preelement = (SvgElement) element1;
			}
			else
			{
				this.preelement = null;
			}
			if (this.xmlreader != null)
			{
				int num3 = this.xmlreader.LineNumber;
				int num4 = this.xmlreader.LinePosition;
			}
//			if ((element1 is SVG) && (this.DocumentType == null))
//			{
//				XmlDocumentType type1 = this.CreateDocumentType("svg", "-/W3C/DTD SVG 1.1/EN", "http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd", null);
//				this.AppendChild(type1);
//				this.AppendChild(this.CreateWhitespace("\r\n"));
//			}
			
			return element1;
		}
		public override XmlNode CreateNode(XmlNodeType type, string prefix, string name, string namespaceURI)
		{
			return base.CreateNode (type, prefix, name, namespaceURI);
		}


		public string  SelectNodesToString(string xpath)
		{
			XmlNodeList list = this.SelectNodes(xpath);

			string head=this.DocumentElement.CloneNode(false).OuterXml;
			StringBuilder sb = new StringBuilder();

			sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");

			sb.Append("\n");
			sb.Append(head.Replace("</svg>",""));
			sb.Append("\n");
			foreach(XmlNode node in list)
			{
				sb.Append(node.OuterXml);
				sb.Append("\n");
			}
			sb.Append("</svg>");
			
			return sb.ToString();
		}
		public static string  Union(string svgdata1,params string[] svgdata)
		{
			if (svgdata.Length==0)return svgdata1;

			SvgDocument doc1 =new SvgDocument();
			doc1.PreserveWhitespace =true;
			doc1.LoadXml(svgdata1);
			
			string head=doc1.DocumentElement.CloneNode(false).OuterXml;
			StringBuilder sb = new StringBuilder();
			StringBuilder sbHead = new StringBuilder();
			StringBuilder sbDefs = new StringBuilder();

			sbHead.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");

			sbHead.Append("\n");
			sbHead.Append(head.Replace("</svg>",""));
			sbHead.Append("\n");
			IList idList = new ArrayList();
			IList graphList =new ArrayList();
			
			foreach(XmlNode node in doc1.DocumentElement.ChildNodes)
			{
				if (node is Defs)
				{
					sbDefs.Append(node.OuterXml.Replace("</defs>",""));		
					sbDefs.Append("\n");
					foreach(XmlNode node2 in node.ChildNodes)
					{
						if (node2  is XmlElement)
						{							
							idList.Add( (node2 as XmlElement).GetAttribute("id"));
						}
					}
					
				}
				if (node is IGraph)
				{
					graphList.Add((node as XmlElement).GetAttribute("id"));
					sb.Append(node.OuterXml);
					sb.Append("\n");
				}
			}
			
			for(int i=0;i<svgdata.Length;i++)
			{
				SvgDocument doc2 =new SvgDocument();

				doc2.PreserveWhitespace=true;
				doc2.LoadXml(svgdata[0]);
				
				foreach(XmlNode node in doc2.DocumentElement.ChildNodes)
				{
					if (node is Defs)
					{
						foreach(XmlNode node2 in node.ChildNodes)
						{							
							if (node2 is XmlElement )
							{
								XmlElement element = node2 as XmlElement; 
								Layer layer=element as Layer;
								if(layer!=null && layer.GraphList.Count==0 )
								{
									continue;
								}
								string id =element.GetAttribute("id");
								if (idList.IndexOf(id)>=0)continue;

								idList.Add(id);
								sbDefs.Append(element.OuterXml);
								sbDefs.Append("\n");
							}
                            if (node2 is Layer)
                            {
                                string id = ((Layer)node2).ID;
                                if (idList.IndexOf(id) >= 0) continue;

                                idList.Add(id);
                                sbDefs.Append(node2.OuterXml);
                                sbDefs.Append("\n");
                            }
						}

					}
					if (node is IGraph)
					{
						string id = (node as XmlElement).GetAttribute("id");
						if (id !=string.Empty && id!=null && graphList.IndexOf(id)>=0)continue;
						graphList.Add(id);
						sb.Append(node.OuterXml);
						sb.Append("\n");
					}
				}
				doc2.Dispose();
			}			
			doc1.Dispose();

			sbDefs.Append("</defs>");
			sbDefs.Append("\n");

			sb.Insert(0,sbDefs.ToString());

			sb.Insert(0,sbHead.ToString());
			sb.Append("</svg>");
			
			return sb.ToString();
		}
		
		public Pattern CreatePattern()
		{
			Pattern pattern1 = (Pattern) this.CreateElement(this.Prefix, "pattern", this.NamespaceURI);
			string text1 = CodeFunc.CreateString(this, "pattern");
			bool flag1 = this.AcceptChanges;
			this.AcceptChanges = flag1;
			SVG svg1 = (SVG) base.DocumentElement;
			AttributeFunc.SetAttributeValue(pattern1, "id", text1);
			AttributeFunc.SetAttributeValue(pattern1, "width", "100");
			AttributeFunc.SetAttributeValue(pattern1, "height", "100");
			AttributeFunc.SetAttributeValue(pattern1, "patternUnits", "userSpaceOnUse");
			AttributeFunc.SetAttributeValue(pattern1, "patternContentUnits", "userSpaceOnUse");
			AttributeFunc.SetAttributeValue(pattern1, "viewBox", "0 0 100 100");
			this.AcceptChanges = true;
			this.selectCollection.Clear();
			this.NumberOfUndoOperations = 1;
			this.AddDefsElement(pattern1);
			this.RootElement = pattern1;
			this.OnlyShowCurrent = true;
			if (this.OnDocumentChanged != null)
			{
				this.OnDocumentChanged(this, new DocumentChangedEventArgs());
			}
			this.AcceptChanges = flag1;
			return pattern1;
		}

		public void CreateSymbol()
		{
			Symbol symbol1 = (Symbol) this.CreateElement(this.Prefix, "symbol", this.NamespaceURI);
			string text1 = CodeFunc.CreateString(this, "symbol");
			bool flag1 = this.AcceptChanges;
			this.AcceptChanges = flag1;
			AttributeFunc.SetAttributeValue(symbol1, "id", text1);
			this.AcceptChanges = true;
			this.selectCollection.Clear();
			this.NumberOfUndoOperations = 1;
			this.AddDefsElement(symbol1);
			this.RootElement = symbol1;
			this.OnlyShowCurrent = true;
			if (this.OnDocumentChanged != null)
			{
				this.OnDocumentChanged(this, new DocumentChangedEventArgs());
			}
			this.AcceptChanges = flag1;
		}

		public void DealLast()
		{
			this.groups.Clear();
			this.preelement = null;

			SvgElement svg =this.RootElement;
	
			XmlNodeList list = svg.GetElementsByTagName("layer");
		
			foreach( Layer node in list)
			{
				this.Layers.Add(node);
			}

			list = svg.GetElementsByTagName("use");
			this.AcceptChanges=false;
			Hashtable hash1 =new Hashtable();
			
			foreach(Use node in list)
			{
				string id =node.GraphId;
				if(hash1.ContainsKey(id))
				{
					node.RefElement = hash1[id] as SvgElement;
				}
				else
				{
					hash1[id]= NodeFunc.GetRefNode(id,this);
					node.RefElement = hash1[id] as SvgElement;
				}
				//((Use)node).GraphId=node.GetAttribute("xlink:href");
			}
			if (svg is SVG)
			{
				Layer layer0=Layer.CreateNew("$0",this);
				ArrayList list2=new ArrayList();
				foreach(SvgElement node in ((Group)svg).ChildList)
				{			
					if(node is IGraph)
					{
//						if(node is Polygon && (node as Polygon).Points.Length<3)
//						{
//                            list2.Add(node); 
//							continue;
//						}
						ILayer layer = (node as IGraph).Layer;
						if(layer !=null)
						{
							layer.Add(node);	
						}
						else
						{
							layer0.Add(node);
							(node as IGraph).Layer = layer0;
						}
					}
				}
				for(int i=list2.Count -1;i>=0;i--)
				{
                    SvgElement node=list2[i] as SvgElement;

					node.ParentNode.RemoveChild(node);
				}
				if (layer0.GraphList.Count==0)
					layer0.Remove();
			}			
		}

		public void ExpandAll()
		{
			this.serialize.FlowDocument();
			if (this.OnDocumentChanged != null)
			{
				this.OnDocumentChanged(this, new DocumentChangedEventArgs());
			}
		}

		private void HoldErrors(object sender, ValidationEventArgs e)
		{
			this.errorinfos.Add(new SvgException(e.Message, e.Exception.LineNumber, e.Exception.LinePosition));
		}

		public void InsertFlowElement(int index, SvgElement element)
		{
			this.serialize.Insert(index, element);
		}

		public override void Load(string filename)
		{
			Stream stream1 = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
			System.Xml.XmlTextReader reader1 = new System.Xml.XmlTextReader(stream1, XmlNodeType.Document, this.XmlParserContext);
			reader1.XmlResolver = null;
			this.Load(reader1);
			reader1.Close();
			stream1.Close();
		}

		public override void Load(XmlReader reader)
		{
			this.selectCollection.Clear();
			this.editRoots.Clear();
			this.firstload = true;
			this.errorinfos.Clear();
			this.XmlResolver = null;
			if (reader is System.Xml.XmlTextReader)
			{
				this.xmlreader = (System.Xml.XmlTextReader) reader;
			}
			try
			{
				base.Load(reader);
			}
			catch (Exception exception1)
			{
				if (!(exception1 is SvgException))
				{
					throw exception1;
				}
			}
			this.Render(this.RootElement);
			this.DealLast();
			
			//this.Valid();//检查文档有效性
			this.firstload = false;
			this.undoStack.ClearAll();
			this.RootElement = (SvgElement) base.DocumentElement;
		}
		public void Render(XmlNode svgelement)
		{
			
			foreach(XmlNode node in svgelement.ChildNodes)
			{
				if (node is XmlElement)
				{

					if (node is SvgElement)
					{
						if(node.ParentNode is ContainerElement&& ((ContainerElement) node.ParentNode).IsValidChild(node))
						{
							((ContainerElement)node.ParentNode).ChildList.Add((SvgElement)node);
						}
						
					}					
					Render(node);						
				}
			}				
					
		}

		public override void LoadXml(string text)
		{
			System.Xml.XmlTextReader reader1 = new System.Xml.XmlTextReader(text, XmlNodeType.Document, this.XmlParserContext);
			reader1.XmlResolver = null;
			this.Load(reader1);
		}

		public void NotifyChange()
		{
			if (this.OnNotifyChange != null)
			{
				this.OnNotifyChange(this, new EventArgs());
			}
		}

		public void NotifyUndo()
		{
			IUndoOperation[] operationArray1 = new IUndoOperation[this.undoGroup.Count];
			this.undoGroup.CopyTo(operationArray1, 0);
			this.undoGroup.Clear();
			this.NumberOfUndoOperations = 1;
			if (operationArray1.Length >= 1)
			{
				this.undoStack.Push(operationArray1);
			}
			this.ChangeElements.Clear();
		}

		public void PostChange(ISvgElement[] elements, ChangeAction action)
		{
			if (this.OnDocumentChanged != null)
			{
				DocumentChangedEventArgs args1 = new DocumentChangedEventArgs();
				args1.ChangeElements = elements;
				args1.ChangeAction = action;
				this.OnDocumentChanged(this, args1);
			}
		}

		public void PostInkeyChanged(ISvgElement element)
		{
			if (this.OnInKeyChanged != null)
			{
				this.OnInKeyChanged(this, new EventArgs());
			}
		}

		public void PushUndo(IUndoOperation undo)
		{
			if (this.undoGroup.Count == 0)
			{
				this.undoGroup.Add(undo);
			}
			else
			{
				this.undoGroup.Insert(0, undo);
			}
			if (this.undoGroup.Count == this.NumberOfUndoOperations)
			{
				IUndoOperation[] operationArray1 = new IUndoOperation[this.undoGroup.Count];
				this.undoGroup.CopyTo(operationArray1, 0);
				this.undoGroup.Clear();
				this.NumberOfUndoOperations = 1;
				if (operationArray1.Length >= 1)
				{
					this.undoStack.Push(operationArray1);
				}
				this.ChangeElements.Clear();
			}
		}

		public void Redo()
		{
			SvgElement[] elementArray1 = this.undoStack.Redo();
			if (elementArray1 != null)
			{
				DocumentChangedEventArgs args1 = new DocumentChangedEventArgs();
				args1.ChangeElements = elementArray1;
				if (this.OnDocumentChanged != null)
				{
					this.OnDocumentChanged(this, args1);
				}
			}
		}

		public void RemoveFlowElement(SvgElement element)
		{
			this.serialize.RemoveElement(element);
		}

		public void Undo()
		{
			if (this.undoStack.CanUndo)
			{
				SvgElement[] elementArray1 = this.undoStack.Undo();
				if (elementArray1 != null)
				{
					DocumentChangedEventArgs args1 = new DocumentChangedEventArgs();
					args1.ChangeElements = elementArray1;
					if (this.OnDocumentChanged != null)
					{
						this.OnDocumentChanged(this, args1);
					}
				}
			}
		}
		
		public ArrayList getLayerList()
		{
			ArrayList list=new ArrayList();
			
			XmlNodeList Nlist=this.GetElementsByTagName("layer");	

			foreach(XmlNode node in Nlist)
			{
				Layer layer=node as Layer;
				list.Add(layer);
			}
			return list;
		}
		
		public Layer GetLayerByID(string id)
		{
			XmlNode node = this.SelectSingleNode("svg/defs/layer[@id='"+id+"']");

			Layer layer1=null;
			if (node !=null)
				layer1 = node as Layer;

			return layer1;            
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="create">如果没有是否创建，为真创建</param>
		/// <returns></returns>
		public Layer GetLayerByID(string id,bool create)
		{			
			Layer layer1= GetLayerByID(id);
			if(layer1==null && create)
			{
				layer1 = Layer.CreateNew(id,this);
			}
			return layer1 ;
		}


		private XmlValidatingReader Valid()
		{
			XmlValidatingReader reader1 = null;
			string text1 = this.OuterXml;
			reader1 = new XmlValidatingReader(this.OuterXml, XmlNodeType.Document, this.XmlParserContext);
			ValidationEventHandler handler1 = new ValidationEventHandler(this.HoldErrors);
			reader1.ValidationEventHandler += handler1;
			LocalDtdXmlUrlResolver resolver1 = new LocalDtdXmlUrlResolver();
			resolver1.AddDtd("-/W3C/DTD SVG 1.1 Tiny/EN", Application.StartupPath + @"\dtd\svg11-tiny.dtd");
			resolver1.AddDtd("-/W3C/DTD SVG 1.1 Basic/EN", Application.StartupPath + @"\dtd\svg11.dtd");
			resolver1.AddDtd("-/W3C/DTD SVG 1.1/EN", Application.StartupPath + @"\dtd\svg11.dtd");
			resolver1.AddDtd("-/W3C/DTD SVG 20001102/EN", Application.StartupPath + @"\dtd\svg11.dtd");
			reader1.XmlResolver = resolver1;
			if (reader1.CanResolveEntity)
			{
				while (!reader1.EOF)
				{
					try
					{
						reader1.Read();
						continue;
					}
					catch (Exception)
					{
						continue;
					}
				}
			}
			return reader1;
		}


		// Properties
		public bool AcceptChanges
		{
			get
			{
				return this.undoStack.AcceptChanges;
			}
			set
			{
				this.undoStack.AcceptChanges = value;
			}
		}

		public bool CanRedo
		{
			get
			{
				return this.undoStack.CanRedo;
			}
		}

		public bool CanUndo
		{
			get
			{
				return this.undoStack.CanUndo;
			}
		}
		public void ClearUndos()
		{
			this.undoStack.ClearAll();
		}
		public void Clear()
		{
			this.RootElement.RemoveAll();
		}

		public int ControlTime
		{
			get
			{
				return this.controltime;
			}
			set
			{
				if (this.controltime != value)
				{
					this.OldControlTime = this.controltime;
					this.controltime = value;
					if (this.OnControlTimeChanged != null)
					{
						this.OnControlTimeChanged(this, this.OldControlTime, value);
					}
				}
			}
		}

		public SvgElement CurrentElement
		{
			get
			{
				if (this.selectCollection.Count > 0)
				{
					return (SvgElement) this.selectCollection[0];
				}
				return null;
			}
			set
			{
				bool flag1 = this.AcceptChanges;
				this.AcceptChanges = false;
				this.selectCollection.NotifyEvent = false;
				this.ClearSelects();
				this.selectCollection.NotifyEvent = true;
				this.selectCollection.Add(value);
				this.AcceptChanges = flag1;
			}
		}

		public SvgElementCollection EditRoots
		{
			get
			{
				if (!this.editRoots.Contains((SvgElement) base.DocumentElement))
				{
					this.editRoots.Add((SvgElement) base.DocumentElement);
				}
				return this.editRoots;
			}
		}

		public ArrayList ErrorInfos
		{
			get
			{
				return this.errorinfos;
			}
		}

		public string FileName
		{
			get
			{
				return this.filename;
			}
			set
			{
				if (this.filename != value)
				{
					this.filename = value;
				}
			}
		}

		public SvgElementCollection FlowChilds
		{
			get
			{
				if (this.serialize != null)
				{
					return this.serialize.FlowChilds;
				}
				return null;
			}
		}

		public bool PlayAnim
		{
			get
			{
				return this.playAnim;
			}
			set
			{
				if (this.playAnim != value)
				{
					this.playAnim = value;
					if (this.OnPlayAnimStatusChanged != null)
					{
						this.OnPlayAnimStatusChanged(this, new EventArgs());
					}
					this.PlayAnimChanged = true;
				}
			}
		}

		public bool RecordAnim
		{
			get
			{
				return this.recordanim;
			}
			set
			{
				if (this.recordanim != value)
				{
					this.recordanim = value;
					if (this.OnRecordAnimChangedEvent != null)
					{
						this.OnRecordAnimChangedEvent(this, new EventArgs());
					}
				}
			}
		}

		public SvgElement RootElement
		{
			get
			{
				if (this.root == null)
				{
					this.root = (SvgElement) base.DocumentElement;
				}
				return this.root;
			}
			set
			{
				if (this.root != value)
				{
					this.root = value;
					if (!this.editRoots.Contains(value))
					{
						this.selectCollection.Clear();
					}
					else
					{
						for (int num1 = 0; num1 < this.editRoots.Count; num1++)
						{
							SvgElement element1 = (SvgElement) this.editRoots[num1];
							if (element1 == value)
							{
								goto Label_0075;
							}
							XmlNode node1 = element1.ParentNode;
							goto Label_0051;
						Label_0051:
							if (node1 != null)
							{
								if (node1 == value)
								{
									this.editRoots.Remove(element1);
									num1--;
								}
								else
								{
									node1 = node1.ParentNode;
									goto Label_0051;
								}
							}
						Label_0075:;
						}
					}
					this.serialize.FlowDocument();
					if (this.OnDocumentChanged != null)
					{
						this.OnDocumentChanged(this, new DocumentChangedEventArgs());
					}
					if (!this.editRoots.Contains(value))
					{
						this.editRoots.Add(value);
					}
				}
			}
		}

		public SvgElementCollection SelectCollection
		{
			get
			{
				return this.selectCollection;
			}
		}

		//        public ItopVector.Core.Css.StyleSheetList StyleSheetList
		//        {
		//            get
		//            {
		//                if (this.styleSheetList == null)
		//                {
		//                    this.styleSheetList = new ItopVector.Core.Css.StyleSheetList(this);
		//                }
		//                return this.styleSheetList;
		//            }
		//        }

		public bool Update
		{
			get
			{
				return this.update;
			}
			set
			{
				if (this.update != value)
				{
					this.update = value;
					if (this.OnUpdateChanged != null)
					{
						this.OnUpdateChanged(this, new EventArgs());
					}
				}
			}
		}

		public System.Xml.XmlTextReader XmlTextReader
		{
			get
			{
				return this.xmlreader;
			}
			set
			{
				this.xmlreader = value;
			}
		}
		public string FilePath
		{
			get{return this.filePath;}
			set
			{
				if(value==filePath)return;
				this.filePath = value;
				if(value!=string.Empty)
				{
					this.filename=Path.GetFileNameWithoutExtension(filePath);
				}
			}

		}
		public SvgElementCollection Layers
		{
			get{return layers;}
		}
		public Layer CurrentLayer
		{
			get{return layers[currentLayer] as Layer;}
			set{
				if (value.ID !=currentLayer)
				{
					currentLayer = value.ID;
				}
			}
		}
		public string getViewScale()
		{
				if(RootElement.GetAttribute("ViewScale")!=null)
				{
					return RootElement.GetAttribute("ViewScale");
				}
				else
				{
					return "";
				}
			
		}
		public void setViewScale(string val)
		{
			RootElement.SetAttribute("ViewScale",val);
		}
		public string getRZBRatio()
		{
			if(RootElement.GetAttribute("RZBRatio")!=null)
			{
				return RootElement.GetAttribute("RZBRatio");
			}
			else
			{
				return "";
			}
			
		}
		public void setRZBRatio(string val)
		{
			RootElement.SetAttribute("RZBRatio",val);
		}
		
		// Fields
		public static string AudioNamespace;
		public bool AutoShowAnim;
		public bool Changed;
		public SvgElementCollection ChangeElements;
		private int controltime;
		public bool DefsChanged;
		private SvgElementCollection editRoots;
		private ArrayList elements;
		private ArrayList errorinfos;
		private string filename;
		private string filePath;
		public bool firstload;
		private ArrayList groups;
		private Hashtable infos;
		public int NumberOfUndoOperations;
		public int OldControlTime;
		private SvgElementCollection oldSelect;
		public bool OnlyShowCurrent;
		private bool playAnim;
		public bool PlayAnimChanged;
		private SvgElement preelement;
		private bool recordanim;
		private SvgElement root;
		public bool SelectChanged;
		private SvgElementCollection selectCollection;
		private SerializeDocument serialize;
		public System.Drawing.Drawing2D.SmoothingMode SmoothingMode;
		internal ArrayList styleElements;
		//private ItopVector.Core.Css.StyleSheetList styleSheetList;
		public static ArrayList supportedFeatures;
		public static string SvgNamespace;
		public System.Drawing.Text.TextRenderingHint TextRenderingHint;
		private ArrayList undoGroup;
		private UndoStack undoStack;
		private bool update;
		public GraphicsPath ValidPath;
		public static string XLinkNamespace;
		public static string TonliNamespace;
		public System.Xml.XmlParserContext XmlParserContext;
		private System.Xml.XmlTextReader xmlreader;

		public  bool ShowConnectPoints=false;
		public static bool  BkImageLoad=false;
		public static string currentLayer;
//		private string viewScale;
		private SvgElementCollection layers;
		public bool BeginPrint=false;
		/// <summary>
		/// 数据库中唯一ID
		/// </summary>
		public string SvgdataUid;
		#region IDisposable 成员

		public void Dispose()
		{
			this.ClearSelects();
			this.ClearUndos();
			this.Clear();
			
		}

		#endregion
	}
}

