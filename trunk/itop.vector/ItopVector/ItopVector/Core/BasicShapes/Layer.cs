using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using ItopVector.Core.ClipAndMask;
using ItopVector.Core.Document;
using ItopVector.Core.Func;
using ItopVector.Core.Interface.Figure;
using ItopVector.Core.Paint;
using ItopVector.Core.Types;
using ItopVector.Core.Interface;
using ItopVector.Core.Interface.Paint;
namespace ItopVector.Core.Figure
{
	/// <summary>
	/// 图层类扩展元素
	/// </summary>
	public class Layer : SvgElement,ILayer
	{
		// Methods
		private bool islock;
		private bool canSelect;
		private SvgElementCollection graphList;
		internal Layer(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
			canSelect=true;
			islock=false;
			graphList =new SvgElementCollection();
		}
		/// <summary>
		/// 把可见性应用到图层里的图元。
		/// </summary>
		public void AppliedVisibility()
		{
			SvgElementCollection nodelist =this.graphList;//this.OwnerDocument.SelectNodes("//*[@layer='"+this.ID+"']");
			bool flag1= this.Visible;
			foreach(XmlNode node in nodelist)
			{
				IGraph element =node as IGraph ;
				if (element !=null)
				{
					element.DrawVisible = flag1;
				}
			}
			
		}
		public void AppliedVisibility(ICollection elements)
		{
			foreach(IGraph graph in elements)
			{
				if(graph !=null)
				{
					graph.DrawVisible = this.Visible;
				}
			}
		}
		public void AppliedVisibility(IGraph element)
		{
			if(element !=null)
			{
				element.DrawVisible = this.Visible;
			}
		}
		public bool CanSelect
		{
			get{return canSelect;}
			set
			{
				if (canSelect ==value)return;
				canSelect =value;

				SvgElementCollection nodelist =this.graphList;//this.OwnerDocument.SelectNodes("//*[@layer='"+this.ID+"']");
				bool flag1= canSelect;
				foreach(XmlNode node in nodelist)
				{
					IGraph element =node as IGraph ;
					if (element !=null)
					{
						element.CanSelect = flag1;
					}
				}
			}
		}
		public bool IsLock
		{
			get{return islock;}
			set
			{
				if (islock ==value)return;
				islock =value;

				SvgElementCollection nodelist =this.graphList;//this.OwnerDocument.SelectNodes("//*[@layer='"+this.ID+"']");
				bool flag1= islock;
				foreach(XmlNode node in nodelist)
				{
					IGraph element =node as IGraph ;
					if (element !=null)
					{
						element.IsLock = flag1;
					}
				}
			}
		}
		/// <summary>
		/// 图层是否可见
		/// </summary>
		public bool Visible
		{
			get
			{
				return this.GetAttribute("visibility")=="hidden"?false:true;
			}
			set
			{
				if(value == this.Visible)return;

				string text = value?"visible":"hidden";

				this.OwnerDocument.AcceptChanges = true;
				this.SetAttribute("visibility",text);
				this.OwnerDocument.AcceptChanges = false;
				this.AppliedVisibility();
			}
		}
		/// <summary>
		/// 删除此图层
		/// </summary>
		public void Remove()
		{
			this.OwnerDocument.AcceptChanges = true;			
			this.ParentNode.RemoveChild(this);
			this.OwnerDocument.Layers.Remove(this);
			this.OwnerDocument.AcceptChanges = false;
		}
		
		/// <summary>
		/// 增加一个图层
		/// </summary>
		/// <param name="label"></param>
		/// <param name="doc"></param>
		/// <returns></returns>
		/// 
		[Obsolete("方法已废除，请使用CreateNew",true)]
		public static Layer AddNode(string label,SvgDocument doc)
		{
			Layer layer =doc.CreateElement(doc.Prefix, "layer", doc.NamespaceURI) as Layer;
			doc.AcceptChanges =true;
			layer =doc.AddDefsElement(layer) as Layer;	
			doc.AcceptChanges =false;
			layer.Label = label;
			doc.Layers.Add(layer);
			return 	layer;
		}
		public static Layer CreateNew(string label,SvgDocument doc)
		{
			Layer layer =doc.CreateElement(doc.Prefix, "layer", doc.NamespaceURI) as Layer;
			doc.AcceptChanges =true;
			layer =doc.AddDefsElement(layer) as Layer;	
			doc.AcceptChanges =false;
			layer.Label = label;
		
			doc.Layers.Add(layer);
			return 	layer;
		} 
		public static  bool CkLayerExist(string LayerName,SvgDocument doc)
		{
			bool ret=false;

			foreach(Layer layer in doc.Layers)
			{
				if(layer.Label == LayerName)
				{
					ret =true;
					break;
				}
			}
			return ret;
		}
		public Layer Copy()
		{
			Layer layer = CreateNew(this.Label +" 副本",this.OwnerDocument);
			
			int num = graphList.Count;
			if (num>0)
			{
				OwnerDocument.NumberOfUndoOperations = (2*graphList.Count) + 200;
				SvgElementCollection sc =graphList.Clone();
				for(int i=num-1;i>=0;i--)
				{
					SvgElement element= sc[i] as SvgElement;
					
					IGraph graph =(IGraph)AddElement(element.Clone() as SvgElement);
					
					graph.Layer = layer;
				}
				this.OwnerDocument.NotifyUndo();
			}
			
			return layer;
		}
		public SvgElement AddElement(ISvgElement mypath)
		{
//			AttributeFunc.SetAttributeValue((XmlElement)mypath,"layer",SvgDocument.currentLayer);
			XmlNode node1 = OwnerDocument.RootElement;
			XmlNode newNode =null;
			
				Matrix matrix1 = new Matrix();
//				if (node1 is IGraph)
//				{
//					matrix1 = ((IGraph) node1).GraphTransform.Matrix.Clone();
//					Matrix matrix2 = this.coordTransform.Clone();
//					matrix2.Invert();
//					matrix1.Multiply(matrix2, MatrixOrder.Append);
//				}
//				matrix1.Invert();
//				matrix1 = TransformFunc.RoundMatrix(matrix1, 2);
				bool flag1 = OwnerDocument.AcceptChanges;
				//				OwnerDocument.AcceptChanges = false;
				OwnerDocument.AcceptChanges = true;
				if (mypath is IGraphPath)
				{
					ISvgBrush brush1 = ((IGraphPath) mypath).GraphBrush;
					if ((brush1 is ITransformBrush) && (((SvgElement) brush1).ParentNode == null))
					{
						bool flag2 = OwnerDocument.AcceptChanges;
						OwnerDocument.AcceptChanges = true;
						OwnerDocument.NumberOfUndoOperations++;
						XmlNode node2 = OwnerDocument.AddDefsElement((SvgElement) brush1);
						OwnerDocument.AcceptChanges = false;
						if (node2 is ITransformBrush)
						{
							string text1 = ((SvgElement) node2).ID;
							AttributeFunc.SetAttributeValue((SvgElement) mypath, "fill", "url(#" + text1 + ")");
						}
						OwnerDocument.AcceptChanges = flag2;
					}
					brush1 = ((IGraphPath) mypath).GraphStroke.Brush;
					if ((brush1 is ITransformBrush) && (((SvgElement) brush1).ParentNode == null))
					{
						bool flag3 = OwnerDocument.AcceptChanges;
						OwnerDocument.AcceptChanges = true;
						OwnerDocument.NumberOfUndoOperations++;
						XmlNode node3 = OwnerDocument.AddDefsElement((SvgElement) brush1);
						OwnerDocument.AcceptChanges = false;
						if (node3 is ITransformBrush)
						{
							string text2 = ((SvgElement) node3).ID;
							AttributeFunc.SetAttributeValue((SvgElement) mypath, "stroke", "url(#" + text2 + ")");
						}
						OwnerDocument.AcceptChanges = flag3;
					}
				}
				if (!matrix1.IsIdentity && (mypath is IGraph))
				{
					bool flag4 = OwnerDocument.AcceptChanges;
					OwnerDocument.AcceptChanges = false;
					Matrix matrix3 = ((IGraph) mypath).Transform.Matrix.Clone();
					matrix1.Multiply(matrix3);
					Transf transf1 = new Transf();
					transf1.setMatrix(matrix1);
					AttributeFunc.SetAttributeValue((SvgElement) mypath, "transform", transf1.ToString());
					OwnerDocument.AcceptChanges = flag4;
				}
				if (((SvgElement) mypath).ParentNode != node1)
				{
					if (((ContainerElement) node1).IsValidChild((SvgElement) mypath))
					{
						//						node1.AppendChild((SvgElement) mypath);
						SvgElement element1 =(SvgElement) mypath;//(SvgElement)OwnerDocument.ImportNode((SvgElement) mypath,true);
						newNode = node1.AppendChild(element1);
						OwnerDocument.Render(element1);						
					}					
				}
				OwnerDocument.AcceptChanges = flag1;

			return newNode!=null?newNode as SvgElement:null;
		}

		/// <summary>
		/// 显示的图层名称
		/// </summary>
		public string Label
		{
			get
			{
				return this.GetAttribute("label");
			}
			set
			{
				this.OwnerDocument.AcceptChanges = true;
				this.SetAttribute("label",value);
				this.OwnerDocument.AcceptChanges = false;
			}
		}
		public override string ToString()
		{
			return this.Label;
		}
		
		public SvgElementCollection GraphList
		{
			get
			{				
				return graphList;
			}
		}
		#region ILayer 成员 图层移动

		public void GoUp()
		{
			SvgElement element = this as SvgElement;
			if (element.ParentNode != null && !(element is SVG))
			{
				SvgElement element1 = (SvgElement) element.ParentNode;
				XmlNode node1 = element.PreviousSibling;
				while (!(node1 is Layer) && (node1 != null))
				{
					node1 = node1.PreviousSibling;
				}
				if (node1 != null)
				{
					bool flag1 = this.OwnerDocument.AcceptChanges;
					this.OwnerDocument.AcceptChanges = false;
					element1.RemoveChild(element);
					element.AllowRename =false;
					element1.InsertBefore(element, node1);
					element.AllowRename =true;
					this.OwnerDocument.AcceptChanges = flag1;

					SvgElementCollection layers = OwnerDocument.Layers;
					int index =layers.IndexOf(element);
					layers.Remove(element);
					layers.Insert(index -1,element);
					AppliedVisibility();
				}
			}
		}

		public void GoBottom()
		{
			
		}

		public void GoTop()
		{
			
		}

		public void GoDown()
		{
			SvgElement element = this as SvgElement;
			if (element.ParentNode != null&& !(element is SVG))
			{
				SvgElement element1 = (SvgElement) element.ParentNode;
				XmlNode node1 = element.NextSibling;
				while (!(node1 is Layer) && (node1 != null))
				{
					node1 = node1.NextSibling;
				}
				if (node1 != null)
				{
					bool flag1 = this.OwnerDocument.AcceptChanges;
					this.OwnerDocument.AcceptChanges = false;
					element1.RemoveChild(element);
					element.AllowRename =false;
					element1.InsertAfter(element, node1);
					element.AllowRename =true;
					this.OwnerDocument.AcceptChanges = flag1;
					SvgElementCollection layers = OwnerDocument.Layers;
					int index =layers.IndexOf(element);
					layers.Remove(element);
					layers.Insert(index+1,element);
					AppliedVisibility();
				}
			}
		}

		public void GoTo(int index)
		{
			
		}

		#endregion

		#region ILayer 成员

		public new string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID =value;
			}
		}

		public void Add(ItopVector.Core.Interface.ISvgElement graph)
		{
			graphList.Add(graph);
		}

		void ItopVector.Core.Interface.Figure.ILayer.Remove(ItopVector.Core.Interface.ISvgElement graph)
		{
            graphList.Remove(graph);
		}
		

		#endregion
	}
}