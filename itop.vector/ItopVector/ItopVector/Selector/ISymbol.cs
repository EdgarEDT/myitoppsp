namespace ItopVector.Selector
{
	using System;
	using ItopVector.Core;
	public interface IOutlookBarBand
	{
		// Methods
		 int IndexOf(IShape graphPath);

		// Properties

		 int Count { get; }

		 string Id { get; set; }
	}
	public interface IShape
	{
		SvgElement OwnerElement{get;}
		//对外显示的名称
		string Label{get;}
	}
}

