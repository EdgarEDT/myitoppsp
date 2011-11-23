/***********************************************************************
 * Module:  ItopVectorControl.cs
 * Author:  Administrator
 * Purpose: Definition of the Class ItopVector.ItopVectorControl.ItopVectorControl
 ***********************************************************************/

using System;
using ItopVector.Core;
using ItopVector.Core.Document;
using ItopVector.Core.Func;
using ItopVector.Core.Interface;
using ItopVector.Core.Interface.Figure;
using ItopVector.Core.Paint;
using ItopVector.DrawArea;
using ItopVector.Dialog;
using ItopVector.Resource;
using ItopVector.Core.Figure;
using System.Drawing.Imaging;


using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml;
using System.Text;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using ItopVector.Core.Types;   


namespace ItopVector
{

	[ToolboxItem(true)]//, DefaultEvent("ScaleChanged"),  DefaultProperty("AutoFitWindow")]
	//	[LicenseProvider(typeof(AXODDLDSF))]
	[ToolboxBitmap(typeof(ItopVector.ItopVectorControl), "Bitmap11.bmp")]
	[Guid("15C969FD-5CA4-494d-AF9B-A0C9CCE57888")]   
	public class ItopVectorControl : UserControl, IControl,IItopVector,IMouseEvent
	{
		private System.ComponentModel.IContainer components;
		private ItopVector.DrawArea.DrawArea drawArea1;
		private Icon mainIcon;
		private Hashtable roots;
		private SvgDocument svgDocument;
		
		private Timer timer1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private string unnamed;
		private PropertyGrid propertyGrid;
//		private SvgElement preelement;
		private ItopVector.Selector.ShapeSelector shapeSelector;
		private PageSettings pageSetting;
		private PrintSetupDialog pageSetupDlg1;
		private bool canEdit;
		public static int isOpen = 0;
		private AerialView dialog1;
		public float top = 0F;
		public float left = 0F;
		/// Events
//		public event EventHandler DesignModeChanged;      
		public event OnTipEventHandler OnTipEvent;      
		public event TrackPopupEventHandler OnTrackPopup;
		public event EventHandler OperationChanged;
		public event PostBrushEventHandler PostBrushEvent;
		public event EventHandler ScaleChanged;
		public event OnDocumentChangedEventHandler DocumentChanged;
		public event EventHandler UpdateChanged;
		public event PaintMapEventHandler PaintMap;    
		public event PaintMapEventHandler AfterPaintPage;
        public event MouseEventHandler OnMouseUp;
		/// Methods
		/// 
		#region 构造与销毁
		public ItopVectorControl()
		{
			//int num1;
			this.components = null;
			this.pageSetting=new PageSettings();
			this.svgDocument = new SvgDocument();
			this.mainIcon = null;
			this.roots = new Hashtable(0x10);
			this.unnamed = string.Empty;
			this.InitializeComponent();
			this.mainIcon = this.GetIconFromResource(base.GetType(), "ItopVector.ItopVectorControl.Resource.main.ico");
			this.CreateMenu();
			this.drawArea1.OperationChanged += new EventHandler(this.ChangeOperation);
			this.drawArea1.ScaleChanged += new EventHandler(this.ChangeScale);
			this.drawArea1.OnTrackPopup += new TrackPopupEventHandler(this.TrackPopup);
			this.drawArea1.OnTipEvent += new OnTipEventHandler(this.ToolTip);
			this.drawArea1.PostBrushEvent += new PostBrushEventHandler(this.PostBrush);
			this.drawArea1.LeftClick +=new SvgElementEventHandler(drawArea1_LeftClick);
			this.drawArea1.DoubleLeftClick+=new SvgElementEventHandler(drawArea1_DoubleLeftClick);
			this.drawArea1.RightClick+=new SvgElementEventHandler(drawArea1_RightClick);
			this.drawArea1.MoveOver+=new SvgElementEventHandler(drawArea1_MoveOver);
			this.drawArea1.MoveIn+=new SvgElementEventHandler(drawArea1_MoveIn);
			this.drawArea1.MoveOut+=new SvgElementEventHandler(drawArea1_MoveOut);
			this.drawArea1.DragAndDrop+=new DragEventHandler(drawArea1_DragDrop);
			this.drawArea1.PaintMap+=new PaintMapEventHandler(drawArea1_PaintMap);            
			this.drawArea1.AfterPaintPage+=new PaintMapEventHandler(drawArea1_AfterPaintPage);
			this.Disposed+=new EventHandler(DocumentControl_Disposed);
			this.UpdateProperty();
			this.unnamed = ItopVector.Resource.LayoutManager.GetLabelForName("unnamedelement").Trim();
            this.drawArea1.OnMouseUp += new MouseEventHandler(drawArea1_OnMouseUp);
           
        }

     


        void drawArea1_OnMouseUp(object sender, MouseEventArgs e)
        {
            if(OnMouseUp!=null)
            {
                OnMouseUp(sender, e);               
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
				this.svgDocument.OnDocumentChanged -= new OnDocumentChangedEventHandler(this.ChangeDocument);
				this.svgDocument.SelectCollection.OnCollectionChangedEvent -= new OnCollectionChangedEventHandler(this.ChangeSelect);
				this.svgDocument.OnUpdateChanged-=new EventHandler(OnUpdateChanged);					
			}	
		}
		private void DocumentControl_Disposed(object sender, EventArgs e)
		{
			if (this.propertyGrid!=null)
			{
				propertyGrid.PropertyValueChanged -=new PropertyValueChangedEventHandler(propertyGrid_PropertyValueChanged);
			}
		}
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ItopVectorControl));
			this.drawArea1 = new ItopVector.DrawArea.DrawArea();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// plDrawArea
			// 
			//			this.plDrawArea.Controls.Add(this.drawArea1);
			//			this.plDrawArea.Dock = System.Windows.Forms.DockStyle.Fill;
			//			this.plDrawArea.DockPadding.All = 1;
			//			this.plDrawArea.Location = new System.Drawing.Point(0, 0);
			//			this.plDrawArea.Name = "plDrawArea";
			//			this.plDrawArea.Size = new System.Drawing.Size(672, 496);
			//			this.plDrawArea.TabIndex = 2;
			//			this.plDrawArea.Paint += new System.Windows.Forms.PaintEventHandler(this.splitter1_Paint);
			// 
			// drawArea1
			// 
			//this.drawArea1.BackColor = Color.FromArgb(100,100,100);
			this.drawArea1.BackColor = Color.FromArgb(144,153,174);
			this.drawArea1.CenterPoint = ((System.Drawing.PointF)(resources.GetObject("drawArea1.CenterPoint")));
			this.drawArea1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.drawArea1.GridSize = ((System.Drawing.SizeF)(resources.GetObject("drawArea1.GridSize")));
			this.drawArea1.Location = new System.Drawing.Point(1, 1);
			this.drawArea1.Name = "drawArea1";
			this.drawArea1.Operation = ToolOperation.Select;
			this.drawArea1.PreGraph = null;
			this.drawArea1.ScaleUnit = 1F;
			this.drawArea1.ShowGrid = true;
			this.drawArea1.ShowGuides = false;
			this.drawArea1.ShowRule = true;
			
			this.drawArea1.Size = new System.Drawing.Size(670, 494);
			this.drawArea1.SVGDocument = null;
			this.drawArea1.TabIndex = 1;
			this.drawArea1.VirtualLeft = 0F;
			this.drawArea1.VirtualTop = 0F;
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "SVG文件|*.svg";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "SVG文件|*.svg";
			// 
			// ItopVectorControl
			// 
			this.Controls.Add(this.drawArea1);
			this.Name = "ItopVectorControl";
			this.Size = new System.Drawing.Size(672, 496);
			this.ResumeLayout(false);

		}
		#endregion		

		#region 文件操作

		public SvgDocument NewFile()
		{
			
			float width=this.pageSetting.PaperSize.Width*0.9f;
			float height=this.pageSetting.PaperSize.Height*0.9f;
			SizeF size=SizeF.Empty;
			if(this.pageSetting.Landscape)
			{
				size=new SizeF(height,width);
				
			}
			else
			{
				size=new SizeF(width,height);				
			}
			
			this.SVGDocument=SvgDocumentFactory.CreateDocument(size);
			this.drawArea1.AttachProperty();
			return this.svgDocument;
		}
		public bool OpenFile(string fileName)
		{
			this.Cursor = Cursors.WaitCursor;	
			this.SVGDocument= SvgDocumentFactory.CreateDocumentFromFile(fileName);
			this.drawArea1.AttachProperty();
			this.Cursor = Cursors.Default;
			if(drawArea1.SVGDocument.getLayerList().Count>0)
			{
				SvgDocument.currentLayer=((drawArea1.SVGDocument.getLayerList()[0]) as  ItopVector.Core.Figure.Layer).ID;
			}
			if(this.SVGDocument==null) return false;
			return true;
		}
		
		private bool save(bool showdialog)
		{
			SvgDocument document1 = this.SVGDocument;
			this.saveFileDialog1.FileName = document1.FileName;

			if ((document1.FilePath == string.Empty) || showdialog)
			{
				if (this.saveFileDialog1.ShowDialog() != DialogResult.OK)
				{
					return false;
				}
				string text1 = this.saveFileDialog1.FileName;
				string text2 = Path.GetExtension(text1);
				this.Cursor = Cursors.WaitCursor;
				StreamWriter writer1 = new StreamWriter(text1, false, Encoding.UTF8);
				string text3 = this.TextContent;
				writer1.Write(text3);
				writer1.Flush();
				writer1.Close();
				
				document1.FilePath = text1;
				document1.FileName = Path.GetFileNameWithoutExtension(text1);
				this.Cursor = Cursors.Default;
				document1.Update = true;
				
				return true;
			}
			if (!document1.Update)
			{
				StreamWriter writer2 = new StreamWriter(document1.FilePath, false, Encoding.UTF8);
				string text6 = this.TextContent;
				writer2.Write(text6);
				writer2.Flush();
				writer2.Close();
				document1.Update = true;
			}
			this.Cursor = Cursors.Default;
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
			if(this.SVGDocument.CanRedo)
			{
				this.SVGDocument.Redo();
			}
			return this.SVGDocument.CanRedo;
		}

		#endregion 文件操作

		public SvgElement AddShape(SvgElement element,Point p1)
		{
			SvgDocument document1 = this.SVGDocument;
			IGraph graph1 =(IGraph)element;
			if (graph1==null)return null;
			SvgElement newnode=null;
			if (graph1 is ConnectLine)
			{
				Point point2 = p1;
						
				ConnectLine connect =document1.ImportNode((SvgElement)graph1,true) as ConnectLine;		

				connect.X1 += point2.X;
				connect.X2 += point2.X;
				connect.Y1 += point2.Y;
				connect.Y2 += point2.Y;
				newnode= this.drawArea1.AddElement(connect);				
			}
			else if (graph1 is Symbol)
			{					
				if (!document1.DefsElementContains((SvgElement) graph1))
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
						document1.AddDefsElement((SvgElement) graph1);						
					}		
				}
				Use use1 = (Use) document1.CreateElement(document1.Prefix, "use", document1.NamespaceURI);
				use1.GraphId = graph1.ID;
				//Point point1 = p1;
                Transf tf = new Transf();
                tf.Matrix.Translate(p1.X,p1.Y);
                use1.Transform = tf;
				use1.GraphBrush = this.drawArea1.FillBrush.Clone();
				use1.GraphStroke = this.drawArea1.Stroke.Clone() as Stroke;
				newnode =this.drawArea1.AddElement(use1);
			}
			else
			{
				newnode =this.drawArea1.AddElement(element);
			}		
			return newnode;
		}
		public void GoLocation(PointF location)
		{
			 GoLocation(location.X,location.Y);
		}
		public void GoLocation(IGraph graph)
		{
            if(graph==null || !this.drawArea1.ElementList.Contains(graph))return;

			GoLocation(graph.CenterPoint);
		}
		public void GoLocation(float x,float y)
		{
            this.drawArea1.GoLocation(x, y);
            //PointF center = drawArea1.GetCenterPoint();
            //float num1 = (center.X - x)*this.ScaleRatio;
            //float num2 = (center.Y - y)*this.ScaleRatio;		
            //this.drawArea1.MovePicture(this.drawArea1.VirtualLeft - num1,this.drawArea1.VirtualTop - num2,true);
            //this.drawArea1.SetScrollDelta(-(int)(num1), -(int)(num2));	
		}
		public SvgElement CreateBySymbolID(string symbolID)
		{
			return this.CreateBySymbolID(symbolID,PointF.Empty);            
		}
		public SvgElement CreateBySymbolID(string symbolID,PointF p1)
		{
			string id = symbolID;
			XmlNode node =null;
			if (this.SymbolSelector!=null)
				node = this.SymbolSelector.SymbolDoc.SelectSingleNode("//*[@id='"+id+"']");
			if (node ==null)
				throw new ApplicationException("无效的图元id: '"+id+"'");
			IGraph graph1= node as IGraph;
			IGraph graph2 =null;
			if (graph1 is ConnectLine)
			{				
				ConnectLine connect =svgDocument.ImportNode((SvgElement)graph1,true) as ConnectLine;
				graph2 = connect;
			}
			else if (graph1 is Symbol)
			{					
				if (!svgDocument.DefsElementContains((SvgElement) graph1))
				{
					if((graph1 as SvgElement).ParentNode is State)
					{
						State state =(graph1 as SvgElement).ParentNode.Clone() as State;
						foreach(SvgElement element1 in state.ChildList)
						{
							element1.Attributes.RemoveNamedItem("visibility");//删除可能有的隐藏属性
						}
						svgDocument.AddDefsElement(state);
					}							
					else 
					{						
						svgDocument.AddDefsElement((SvgElement) graph1);						
					}		
				}
                Use use1 = (Use)svgDocument.CreateElement(svgDocument.Prefix, "use", svgDocument.NamespaceURI);
				use1.GraphId =symbolID;
				PointF point1 = p1;
				RectangleF ef1 = graph1.GPath.GetBounds();
                float X = (point1.X - ef1.X) - (ef1.Width / 2f);
                float Y = (point1.Y - ef1.Y) - (ef1.Height / 2f);
                Transf tf = new Transf();
                tf.Matrix.Translate(X, Y);
                use1.Transform = tf;
				graph2 = use1;
			}
			else
			{
                graph2 =this.svgDocument.ImportNode((SvgElement)graph1,true) as IGraph;
			}
			return graph2 as SvgElement;
		}
		public string SelectToDoc()
		{
			return this.svgDocument.SelectCollectionToString();
		}

		public void ChangeElementMatrix(float scalex, float scaley, float rotate, float skewx, float skewy)
		{
			this.drawArea1.ChangeElementMatrix(scalex, scaley, rotate, skewx, skewy);
		}

		public bool SelectMenuItem(object sender)
		{
			bool flag1 = false;			
			return flag1;
		}
      
		public void LayerManager()
		{
			ItopVector.Dialog.LayerManagerDialog dlg =new LayerManagerDialog();
			dlg.SymbolDoc = this.svgDocument;
			dlg.ShowDialog(this);      	
		}

		public bool UpdateMenuItem(object sender)
		{
			bool flag1 = false;
			if (this.Design)
			{
				return (((flag1 || this.drawArea1.UpdateMenuItem(sender)) ));
			}
			return flag1;
		}
      
		public void UpdateProperty()
		{
			//this.textarea1.AttachProperty();
			this.drawArea1.AttachProperty();
		}
      
		public void UpdateToolBar(object sender)
		{
			if (this.Design)
			{
				this.drawArea1.UpdateToolBar(sender);
			}
			
		}
      
		protected override bool ProcessDialogKey(Keys keydata)
		{
			
			return base.ProcessDialogKey(keydata);
		}
   
		private void ChangeDocument(object sender, DocumentChangedEventArgs e)
		{
			if (this.DocumentChanged!=null)
			{
				this.DocumentChanged(this,e);
			}
		}
      
		private void ChangeOperation(object sender, EventArgs e)
		{
			if (this.OperationChanged != null)
			{
				this.OperationChanged(this, new EventArgs());
			}
		}
      
		private void ChangeScale(object sender, EventArgs e)
		{
			double num1 = Math.Round((double) (this.drawArea1.ScaleUnit * 100f), 0);
			//			this.comboScaleBox.Text = num1.ToString() + "%";
			if (this.ScaleChanged!=null)
			{
				this.ScaleChanged(this,EventArgs.Empty);
			}
										
		}
      
		public virtual void ChangeSelect(object sender, CollectionChangedEventArgs e)
		{
			SvgElement element1 = this.svgDocument.CurrentElement;
			if (this.propertyGrid!=null)
			{
				if (this.SVGDocument.SelectCollection.Count >0)
				{
					if (this.svgDocument.CurrentElement is ItopVector.Core.Figure.SVG)
					{
						this.propertyGrid.SelectedObject=null;
					}
					else
					{
						object[] list1 = new object[this.svgDocument.SelectCollection.Count];
						for(int i=0 ;i<this.svgDocument.SelectCollection.Count;i++)
						{

							SvgElement element2=(SvgElement)this.svgDocument.SelectCollection[i];
							ItopVector.Property.PropertyBase propertybase=null;
							switch(element2.LocalName)
							{
								case"line":
								case "connectline":
									propertybase=new ItopVector.Property.PropertyLineMarker(element2);
									break;

                                case "polyline":
                                    propertybase = new ItopVector.Property.PropertyPolyline(element2);
                                    break;
								case "text":
									propertybase=new ItopVector.Property.PropertyText(element2);
									break;								

								case "image":
									propertybase=new ItopVector.Property.PropertyImage(element2);
									break;
		
								case "rect":
									propertybase = new ItopVector.Property.PropertyRoundRect(element2);
									break;
								case "use":
								case "g":
									propertybase = new ItopVector.Property.PropertyUse(element2);
									break;
								case "polygon":
								case "ellipse":
								case "circle":
								case "path":
									propertybase = new ItopVector.Property.PropertyFill(element2);
									break;
								default:
									propertybase=new ItopVector.Property.PropertyImage(element2);
									break;
							}
							list1[i]=propertybase;
						
						}
						this.propertyGrid.SelectedObjects=list1;

					}					
					
				}
				else if (this.svgDocument.SelectCollection.Count == 0 )
				{
					if(this.propertyGrid.SelectedObject!=null )
					{
						this.propertyGrid.SelectedObject=null;
					}
				}
				
			}
		}
      
		private void comboScaleBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				string text1="";// = this.comboScaleBox.Text;
				float single1 = this.drawArea1.ScaleUnit;
				try
				{
					string text2;
					if ((text2 = text1) == null)
					{
						goto Label_0081;
					}
					text2 = string.IsInterned(text2);
					if (text2 != "适合窗口")
					{
						if (text2 == "显示全部内容")
						{
							goto Label_0068;
						}
						goto Label_0081;
					}
					this.drawArea1.FitWindow();
					single1 = this.drawArea1.ScaleUnit;
					return;
				Label_0068:
					this.drawArea1.ShowAll();
					single1 = this.drawArea1.ScaleUnit;
					return;
				Label_0081:
					single1 = ItopVector.Core.Func.Number.ParseFloatStr(text1);
				}
				finally
				{
					this.drawArea1.ScaleUnit = single1;
				}
			}
		}
      
		private void comboScaleBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			string text1="";// = this.comboScaleBox.Text;
			float single1 = this.drawArea1.ScaleUnit;
			try
			{
				string text2;
				if ((text2 = text1) == null)
				{
					goto Label_0074;
				}
				text2 = string.IsInterned(text2);
				if (text2 != "适合窗口")
				{
					if (text2 == "显示全部内容")
					{
						goto Label_005B;
					}
					goto Label_0074;
				}
				this.drawArea1.FitWindow();
				single1 = this.drawArea1.ScaleUnit;
				return;
			Label_005B:
				this.drawArea1.ShowAll();
				single1 = this.drawArea1.ScaleUnit;
				return;
			Label_0074:
				single1 = ItopVector.Core.Func.Number.ParseFloatStr(text1);
			}
			finally
			{
				this.drawArea1.ScaleUnit = single1;
			}
		}
      
		private void CreateMenu()
		{
		}	
      
		private Icon GetIconFromResource(Type assemblyType, string iconname)
		{
			Assembly assembly1 = Assembly.GetAssembly(assemblyType);
			Stream stream1 = assembly1.GetManifestResourceStream(iconname);
			if (stream1 != null)
			{
				return new Icon(stream1);
			}
			return null;
		}		
      
		//
		public void InvalidateElement(SvgElement element)
		{
			this.drawArea1.InvalidateElement(element);
		}

		private void PostBrush(object sender, ISvgBrush brush)
		{
			if (this.PostBrushEvent != null)
			{
				this.PostBrushEvent(this, brush);
			}
		}
      
		private void SelectMenuItem(object sender, EventArgs e)
		{
			
		}
      
		private void SelectScene(object sender, EventArgs e)
		{
			//			MenuCommand command1 = sender as MenuCommand;
			//			SvgElement element1 = (SvgElement) command1.Tag;
			//			if (element1 != this.svgDocument.RootElement)
			//			{
			//				if (this.svgDocument.EditRoots.Contains(this.svgDocument.RootElement))
			//				{
			//					this.svgDocument.EditRoots.Remove(this.svgDocument.RootElement);
			//				}
			//				this.svgDocument.RootElement = element1;
			//				this.svgDocument.CurrentElement = null;
			//			}
		}
		/// <summary>
		/// 选项设置
		/// </summary>
		public void SetOption()
		{
			ItopVector.Dialog.PreferenceWindow dlg=new ItopVector.Dialog.PreferenceWindow();
			if(dlg.ShowDialog(this)==DialogResult.OK)
				this.drawArea1.AttachProperty();
		}		
      
		private void timer1_Tick(object sender, EventArgs e)
		{
			if (this.svgDocument != null)
			{
				if (!this.svgDocument.PlayAnim)
				{
					this.timer1.Stop();
				}
				else
				{
					this.svgDocument.ControlTime++;
				}
			}
		}
      
		private void ToolTip(object sender, string tooltip, byte type)
		{
			if (this.OnTipEvent != null)
			{
				this.OnTipEvent(sender, tooltip, type);
			}
		}
		private void OnUpdateChanged(object sender, EventArgs e)
		{
			if (this.UpdateChanged !=null)
			{
				this.UpdateChanged(this,e);
			}
		}      
		private void TrackPopup(object sender, Point screenp)
		{
			if (this.OnTrackPopup != null)
			{
				this.OnTrackPopup(sender, screenp);
			}
		}      
		private void TrackScenePopup()
		{
			//			this.sceneMenu = new PopupMenu();
			//			this.sceneMenu.MenuCommands.AddRange(this.ms1);
			//			this.sceneMenu.TrackPopup(this.label2.PointToScreen(new Point(this.label2.Width - 1, this.label2.Height - 1)));
		}      
  
		/// Properties
		/// 
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public ToolOperation CurrentOperation
		{
			get
			{
				return this.drawArea1.Operation;
			}
			set
			{
				this.drawArea1.Operation = value;
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public bool Design
		{
			get
			{
				return true;
			}
			set
			{
	
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public DrawModeType DrawMode
		{
			get{return drawArea1.DrawMode;}
			set{drawArea1.DrawMode =value;}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public ItopVector.DrawArea.DrawArea DrawArea
		{
			get
			{
				return this.drawArea1;
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public SvgElement PreElement
		{
			set
			{
				if (value is IGraph)
				{
					this.drawArea1.PreGraph = (IGraph) value;
				}
			}
		}
		
		[Browsable(false),DesignerSerializationVisibility(0)]
		public SvgDocument SVGDocument
		{
			get
			{
				return this.svgDocument;
			}
			set
			{
				if (value ==null)return;
				if (this.svgDocument != value)
				{
					if(this.svgDocument!=null)
					{
						this.svgDocument.OnDocumentChanged -= new OnDocumentChangedEventHandler(this.ChangeDocument);
						this.svgDocument.SelectCollection.OnCollectionChangedEvent -= new OnCollectionChangedEventHandler(this.ChangeSelect);
						this.svgDocument.OnUpdateChanged-=new EventHandler(OnUpdateChanged);					
					}			
					this.svgDocument = value;
					this.drawArea1.SVGDocument = value;
					if (value != null)
					{						
						if(this.svgDocument.getLayerList().Count>0)
						{
							SvgDocument.currentLayer=((this.svgDocument.getLayerList()[0]) as  ItopVector.Core.Figure.Layer).ID;
						}
						this.svgDocument.OnDocumentChanged += new OnDocumentChangedEventHandler(this.ChangeDocument);
						this.svgDocument.SelectCollection.OnCollectionChangedEvent += new OnCollectionChangedEventHandler(this.ChangeSelect);
						this.svgDocument.OnUpdateChanged+=new EventHandler(OnUpdateChanged);
					}

					//int num1 = this.svgDocument.FlowChilds.Count;
				}
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public string TextContent
		{
			get
			{
				if (this.Design)
				{
					return this.svgDocument.OuterXml;
				}
				return "";//this.textarea1.Document.TextContent;
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public PropertyGrid PropertyGrid
		{
			get
			{
				return propertyGrid;
			}
			set
			{
				if (propertyGrid==value)return;
				if (propertyGrid!=null)
				{
					propertyGrid.PropertyValueChanged-=new PropertyValueChangedEventHandler(propertyGrid_PropertyValueChanged);					
				}
				propertyGrid=value;
				if(propertyGrid!=null)
				{
					propertyGrid.PropertyValueChanged+=new PropertyValueChangedEventHandler(propertyGrid_PropertyValueChanged);
					ChangeSelect(this.svgDocument,new CollectionChangedEventArgs( this.svgDocument.SelectCollection));
				}
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public ItopVector.Selector.SymbolSelector SymbolSelector
		{
			get{return this.DrawArea.SymbolSelector;}
			set
			{
				this.DrawArea.SymbolSelector = value;
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public ToolOperation Operation
		{
			get
			{
				return this.drawArea1.Operation;
			}
			set
			{
				if (this.drawArea1.Operation==value)return;
				this.drawArea1.Operation=value;

			}
		}
   
		[Browsable(false)]
        [DesignerSerializationVisibility(0)]
		public ItopVector.Selector.ShapeSelector ShapeSelector
		{
			get{return this.shapeSelector;}
			set
			{
				if(this.shapeSelector==null&&value!=null)
				{
					this.shapeSelector=value;
					this.shapeSelector.SelectedChanged+=new EventHandler(this.drawArea1.shapeSelector_SelectedChanged);
				}
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public RectangleF ContentBounds
		{
			get
			{
				if(this.svgDocument!=null)
				{
					return new RectangleF(new PointF(0f,0f),this.drawArea1.ViewSize);
				}
				return RectangleF.Empty;
			}
		}
			
		public void AeriaView(ItopVectorControl tlVector)
		{
			if (isOpen == 1)
			{
				dialog1.Activate();
				return;
			}
			try
			{
				dialog1 = new AerialView(this.drawArea1,tlVector);
				dialog1.loadData += new AerialView.loadDateDelegate(this.ReDraw);
				dialog1.Owner = this.ParentForm;
				dialog1.Show();
				isOpen = 1;

				dialog1.Top = Screen.PrimaryScreen.WorkingArea.Height - 380;
				dialog1.Left = Screen.PrimaryScreen.WorkingArea.Width - 300;
			}
			catch (Exception exception1)
			{
				MessageBox.Show(exception1.Message);
			}
		}

		public void ReDraw()
		{
			float single1 = this.drawArea1.ViewSize.Height;
			float single2 = this.drawArea1.ViewSize.Width;
			if (single1 == 0f)
			{
				single1 = this.drawArea1.Height;
			}
			if (single2 == 0f)
			{
				single2 = this.drawArea1.Width;
			}
			float single3 = ((float) (this.Height - 50))/single1;
			single3 = Math.Min(single3, (float) (((float) (this.Width - 50))/single2));
			//this.ScaleUnit = single3;
			if(single3>1)
			{
				this.drawArea1.ScaleRatio = (float) (single3*2);
				this.drawArea1.VirtualTop = top*(single3*2/1) + Math.Abs((this.drawArea1.Height - this.drawArea1.DocumentSize.Height)/2);
				this.drawArea1.VirtualLeft = left*(single3*2/1) + Math.Abs((this.drawArea1.Width - this.drawArea1.DocumentSize.Width)/2);
			}
			else
			{
				this.drawArea1.ScaleRatio =1;
				this.drawArea1.VirtualTop = top;
				this.drawArea1.VirtualLeft = left;
			}			

			if (svgDocument.Update == true)
			{
				dialog1.Refresh();
				svgDocument.Update=false;
			}
			this.Invalidate(true);
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
				this.drawArea1.CanEdit=canEdit;
			}
		}

		

		/// <summary>
		/// 属性改变时发生
		/// </summary>
		/// <param name="s"></param>
		/// <param name="e"></param>
		private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			
		}
		/// <summary>
		/// 旋转选中对象
		/// </summary>
		/// <param name="angle"></param>
		public void RotateSelection(float angle)
		{
			this.drawArea1.RotateSelection(angle);
		}
        public void RotateSelection(float angle,PointF center)
        {
            this.drawArea1.RotateSelection(angle,center);
        }
		public void FlipX()
		{
			this.drawArea1.SelectMenuItem("flipx");
		}
		public void FlipY()
		{
			this.drawArea1.SelectMenuItem("flipy");

		}
		public void Link()
		{
			this.drawArea1.SelectMenuItem("link");
		}
		#region IItopVector 成员

		

		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public bool IsModified
		{
			get
			{				
				return !this.svgDocument.Update;
			}
			set
			{
				this.svgDocument.Update=!value;
			}
		}


		public bool IsShowRule
		{
			get
			{
				
				return this.drawArea1.ShowRule;
			}
			set
			{
				this.drawArea1.ShowRule=value;
			}
		}

		public bool IsShowGrid
		{
			get
			{
				
				return this.drawArea1.ShowGrid;
			}
			set
			{
				this.drawArea1.ShowGrid=value;
			}
		}

		public bool IsPasteGrid
		{
			get
			{
				
				return this.drawArea1.SnapToGrid;
			}
			set
			{
				this.drawArea1.SnapToGrid=value;
			}
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
		[DesignerSerializationVisibility(0)]
		public System.Drawing.Drawing2D.SmoothingMode SmoothingMode
		{
			get
			{
				return this.svgDocument.SmoothingMode;
			}
			set
			{
				this.svgDocument.SmoothingMode=value;
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public SizeF DocumentSize
		{
			get
			{
				
				return this.drawArea1.DocumentSize;
			}
			set
			{
				this.drawArea1.DocumentSize=value;
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Color DocumentbgColor
		{
			get
			{
				// TODO:  添加 ItopVectorControl.DocumentbgColor getter 实现
				return new Color ();
			}
			set
			{
				// TODO:  添加 ItopVectorControl.DocumentbgColor setter 实现
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
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
		[DesignerSerializationVisibility(0)]
		public float ScaleRatio
		{
			get
			{
				
				return drawArea1.ScaleUnit;
			}
			set
			{
				this.drawArea1.ScaleUnit=value;
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Stroke Stroke
		{
			get
			{
				
				return this.drawArea1.Stroke;
			}
			set
			{
				if(value==null || this.drawArea1.Stroke==value)return;
				this.drawArea1.Stroke=value;
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public SolidColor Fill
		{
			get
			{
				
				return (SolidColor)this.drawArea1.Fill;
			}
			set
			{
				if(value==null || this.drawArea1.Fill==value)return;
				this.drawArea1.Fill=value;
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Struct.TextStyle TextStyle
		{
			get
			{				
				return drawArea1.RatFont;
			}
			set
			{
				this.drawArea1.RatFont=value;
			}
		}
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Struct.TextStyle TextFont
		{
			get
			{				
				return drawArea1.RatFont;
			}
			set
			{
				this.drawArea1.RatFont=value;
			}
		}
		[Obsolete("方法已不使用，请使用OpenFile",true)]
		public bool Open(string filename)
		{
			this.drawArea1.OpenFile(filename);
			return false;
		}

		bool ItopVector.IItopVector.Save(string filename)
		{
			this.svgDocument.FilePath=filename;
			return this.save(false);
		}

		public bool ExportImage(string filename, ImageFormat filetype)
		{
			this.drawArea1.ExportImage(filename,filetype);
			
			return false;
		}
		public void ExportImage()
		{
			ExportImageDialog dlg =new ExportImageDialog(this.drawArea1);
			dlg.ShowDialog(this);
		}

		public void PaperSetup()
		{			
			if(this.pageSetupDlg1==null)
			{
				this.pageSetupDlg1=new PrintSetupDialog(this.drawArea1);
			}
			if (pageSetupDlg1.ShowDialog(this) == DialogResult.OK)
			{
				
			}
			this.pageSetupDlg1.Dispose();
			this.pageSetupDlg1=null;
		}
		internal PageSettings PageSettings
		{
			get{return this.pageSetting;}
		}
		[DesignerSerializationVisibility(0)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
				SolidBrush brush=new SolidBrush(value);
				this.drawArea1.BkColor=brush;
			}
		}
//		public Color BackColor
//		{
//			set
//			{
//				SolidBrush brush=new SolidBrush(value);
//				this.drawArea1.BkColor=brush;
//				
//			}
//			get
//			{
//				SolidBrush brush=(SolidBrush)this.drawArea1.BkColor;
//				return brush.Color;
//			}
//		}		
		public bool FullDrawMode
		{
			set
			{
				this.drawArea1.FullDrawMode=value;
			}
			get
			{
				return this.drawArea1.FullDrawMode;
			}
		}
		public void Print()
		{
			try
			{
				ItopVector.Dialog.PrintDialog dialog1 = new ItopVector.Dialog.PrintDialog(this.drawArea1);
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

		public void Align(AlignType align)
		{
			this.DrawArea.Align(align);
		}

		public void ClearBuffer()
		{
			this.svgDocument.ClearUndos();
		}

		public void Clear()
		{
			this.svgDocument.Clear();
		}

		public void Copy()
		{
			this.drawArea1.Copy();
		}

		public void Cut()
		{
			this.drawArea1.Cut();
		}

		public void Paste()
		{
			this.drawArea1.Paste();
		}

		public void Delete()
		{
			this.drawArea1.Delete();
		}

		void ItopVector.IItopVector.Redo()
		{
			this.Redo();
		}

		void ItopVector.IItopVector.Undo()
		{
			this.Undo();
		}

		public void Distribute(DistributeType type)
		{
			// TODO:  添加 ItopVectorControl.Distribute 实现
		}

		public void MakeSameSize(SizeType stype)
		{
			
		}
		public void FitWindow()
		{
			this.drawArea1.FitWindow();
		}
		/// <summary>
		/// 变换选中对象
		/// </summary>
		/// <param name="matrix"></param>
		public void MatrixSelection(Matrix matrix)
		{
			this.drawArea1.MatrixSelection(matrix);
		}

		public void SelectAll()
		{
			this.drawArea1.SelectMenuItem("selectall");
		}
		public void SelectCuurentLay()
		{
			this.drawArea1.SelectMenuItem("selectcuurentlay");
		}

		public void SelectNone()
		{
			this.drawArea1.SelectMenuItem("clearselects");
		}

		public void Group()
		{
			this.drawArea1.Group();
		}

		public void UnGroup()
		{
			this.drawArea1.UnGroup();
		}

		public void ChangeLevel(LevelType level)
		{
			this.drawArea1.ChangeLevel(level);
		}
		/// <summary>
		/// 改变某种图元在垂直方向上的层次，例: ChangeLevel("polygon",LevelType.Bottom)
		/// </summary>
		/// <param name="SymbolTagName"></param>
		/// <param name="type"></param>
		public void ChangeLevel(string SymbolTagName,LevelType type)
		{
			this.drawArea1.ChangeLevel(SymbolTagName,type);
		}

		public String ExportSymbol(/*bool wholecontent, bool exportshape, bool createdocument, string id*/)
		{
			ExportSymbolDialog dlg =new ExportSymbolDialog(this.DrawArea,"(*.xml)|*.xml");
			dlg.ShowDialog(this);
			return null;
		}
		public String ExportSymbol(bool wholecontent, bool exportshape, bool createdocument, string id)
		{
			ExportSymbolDialog dlg =new ExportSymbolDialog(this.DrawArea,"(*.xml)|*.xml");
			dlg.ShowDialog(this);
			return null;
		}
		public void ShowExportSymbolDialog(string filefilter)
		{
			ExportSymbolDialog dlg =new ExportSymbolDialog(this.DrawArea,filefilter);
			dlg.ShowDialog(this);
		}

//		void IItopVector.MouseDown(object sender, MouseEventArgs e)
//		{
//			// TODO:  添加 ItopVectorControl.MouseDown 实现
//		}
//
//		void IItopVector.MouseUp(object sender, MouseEventArgs e)
//		{
//			// TODO:  添加 ItopVectorControl.MouseUp 实现
//		}
//
//		void IItopVector.MouseMove(object sender, MouseEventArgs e)
//		{
//			// TODO:  添加 ItopVectorControl.MouseMove 实现
//		}
//
//		void IItopVector.MouseEnter(object sender, EventArgs e)
//		{
//			// TODO:  添加 ItopVectorControl.MouseEnter 实现
//		}
//
//		void IItopVector.MouseLeave(object sender, EventArgs e)
//		{
//			// TODO:  添加 ItopVectorControl.MouseLeave 实现
//		}

		void IItopVector.DocumentChanged(object sender, EventArgs e)
		{
			// TODO:  添加 ItopVectorControl.DocumentChanged 实现
		}

		void ItopVector.IItopVector.OperationChanged(object sender, EventArgs e)
		{
			// TODO:  添加 ItopVectorControl.ItopVector.IItopVector.OperationChanged 实现
		}

		#endregion
		#region drawArea event
		
		#endregion

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

		private void drawArea1_LeftClick(object sender, SvgElementSelectedEventArgs e)
		{
			if(this.LeftClick!=null)
			{
				LeftClick(this,e);
			}
		}

		private void drawArea1_DoubleLeftClick(object sender, SvgElementSelectedEventArgs e)
		{
			if(this.DoubleLeftClick!=null)
			{
				DoubleLeftClick(this,e);
			}
		}

		private void drawArea1_RightClick(object sender, SvgElementSelectedEventArgs e)
		{
			if(this.RightClick!=null)
			{
				RightClick(this,e);
			}

		}

		private void drawArea1_MoveOver(object sender, SvgElementSelectedEventArgs e)
		{
			if(this.MoveOver!=null)
			{
				MoveOver(this,e);
			}
		}

		private void drawArea1_MoveIn(object sender, SvgElementSelectedEventArgs e)
		{
			if(this.MoveIn!=null)
			{
				MoveIn(this,e);
			}
		}

		private void drawArea1_MoveOut(object sender, SvgElementSelectedEventArgs e)
		{
			if(this.MoveOut!=null)
			{
				MoveOut(this,e);
			}
		}

		private void drawArea1_DragDrop(object sender, DragEventArgs e)
		{
			if(this.DragAndDrop!=null)
			{
				DragAndDrop(this,e);
			}
		}
		/// <summary>
		/// 剪切屏幕图像
		/// </summary>
		/// <param name="SaveFile">是否保存成本地文件</param>
		/// <returns></returns>
		public System.Drawing.Image ClipScreen(bool SaveFile)
		{
			System.Drawing.Image bp;
			picForm frm1=new picForm(SaveFile);
			frm1.ShowDialog();
			bp=frm1.img;
			return bp;
		}
		public Point PointToView(Point point)
		{
			return drawArea1.PointToView(point);
		}
		public void SetToolTip(string text)
		{
			drawArea1.SetToolTip(text);
		}
		/// <summary>
		/// 临时选择画笔
		/// </summary>
		public Pen TempPen
		{
			set{drawArea1.TempPen=value;}
		}
		public RectangleF SelectedRectangle(System.Drawing.Region r)
		{
			Graphics g=drawArea1.tempGraphics;
			return r.GetBounds(g);
			
		}

		private void drawArea1_PaintMap(object sender, PaintMapEventArgs e)
		{
			if (this.PaintMap !=null)
				PaintMap(sender,e);
		}

		private void drawArea1_AfterPaintPage(object sender, PaintMapEventArgs e)
		{
			if (this.AfterPaintPage !=null)
				AfterPaintPage(sender,e);
		}
	}
}