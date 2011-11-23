using System;
using System.Collections;
namespace ItopVector.Core.Interface.Figure
{
	/// <summary>
	/// ILayer 的摘要说明。
	/// </summary>
	public interface ILayer
	{
		void AppliedVisibility();
		void AppliedVisibility(ICollection elements);
		void AppliedVisibility(IGraph element);
		bool CanSelect{get;set;}
		bool IsLock{get;set;}
		bool Visible{get;set;}
		void Remove();
		string Label{get;set;}
		SvgElementCollection GraphList{get;}
		void GoUp();
		void GoBottom();
		void GoTop();
		void GoDown();
		void GoTo(int index);
		string ID{get;set;}
		void Add(ISvgElement graph);
		void Remove(ISvgElement graph);
	}
}
