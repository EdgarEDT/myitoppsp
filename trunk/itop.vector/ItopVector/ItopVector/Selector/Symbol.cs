namespace ItopVector.Selector
{
	using System;
	using System.Collections;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Xml;
	using ItopVector.Core;

	[Serializable]
	public class Symbol : IShape
	{
		// Methods
		internal Symbol()
		{
			
		}

		internal void TransForm(Graphics g, Matrix matrix1)
		{           
		}
		// Methods
		internal Symbol(ItopVector.Core.Figure.Symbol element)
		{
			this.symbolElement = null;
			this.graphPath = null;
			if ((element == null) || (element.Name.ToLower() != "symbol"))
			{
				throw new Exception("原始节点不是Symbol节点");
			}
			this.symbolElement = element;
			this.Id = this.symbolElement.GetAttribute("id");
			this.graphPath =(GraphicsPath)this.symbolElement.GPath.Clone();
		}

		internal void Draw(Graphics g, Matrix matrix1)
		{
			GraphicsContainer container1 = g.BeginContainer();
			g.Transform=matrix1;
			this.symbolElement.Draw(g,0);
			g.EndContainer(container1);
//			g.DrawPath(Pens.Black,path);
		}


		// Properties
		internal ItopVector.Core.Figure.Symbol SymbolElemnet
		{
			get
			{
				return this.symbolElement;
			}
		}

		internal string Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		internal GraphicsPath GraphPath
		{
			get
			{
				return this.graphPath;
			}
		}
		#region IShape 成员

		public SvgElement OwnerElement
		{
			get
			{
				return this.symbolElement;
			}
		}
		public string Label
		{
			get
			{
				string text = this.OwnerElement.GetAttribute("label");
				if(text==string.Empty)
					text=this.Id;

				return text;
			}
		}

		#endregion


		// Fields
		
		private ItopVector.Core.Figure.Symbol symbolElement;
		private GraphicsPath graphPath;
		private string id;
	}
}

