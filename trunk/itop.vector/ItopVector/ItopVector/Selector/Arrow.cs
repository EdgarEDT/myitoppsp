namespace ItopVector
{
	using System;
	using ItopVector.Core;

	public class Arrow
	{
		// Methods
		internal Arrow(SvgElement element)
		{
			this.svgElement = null;
			this.id = null;
			if (element.Name != "marker")
			{
				throw new Exception("无效的节点");
			}
			this.svgElement = element;
		}

		public override string ToString()
		{
			if (this.svgElement != null)
			{
				return this.svgElement.GetAttribute("id");
			}
			return base.ToString();
		}


		// Properties
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

		internal SvgElement SvgElement
		{
			get
			{
				return this.svgElement;
			}
		}


		// Fields
		private SvgElement svgElement;
		private string id;
	}
}

