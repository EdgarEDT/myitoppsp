namespace ItopVector.Selector
{
	using System;
	using System.Drawing.Drawing2D;
	using ItopVector.Core;
	using ItopVector.Core.Figure;
	using ItopVector.Core.Interface.Figure;

	internal class ConnectShape : IShape
	{
		// Methods
		public ConnectShape(SvgElement connect)
		{
			this.graphPath = null;
			this.element = connect;
			this.id = connect.GetAttribute("id");
			this.graphPath = ((ConnectLine)connect).ConnectPath();
		}


		// Properties
		public string Id
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

		public GraphicsPath GraphPath
		{
			get
			{
				return this.graphPath;
			}
		}

		public SvgElement ConnectElement
		{
			get
			{
				return this.element;
			}
		}
		


		// Fields
		private GraphicsPath graphPath;
		private SvgElement element;
		private string id;

		#region IShape ≥…‘±

		public SvgElement OwnerElement
		{
			get
			{
				return this.element;
			}
		}
		public string Label
		{
			get
			{
				string text = this.element.GetAttribute("label");
				if(text==string.Empty)
					text=this.Id;

				return text;
			}
		}

		#endregion
	}
}

