using ItopVector.Core.Document;
using ItopVector.Core.Figure;
using ItopVector.Core.Interface;

namespace ItopVector.Core.Figure
{
	/// <summary>
	/// ��չԪ�ؼ�¼use״̬symbol ��svg��׼Ԫ��
	/// </summary>
	public class State : ContainerElement
	{
		//fields
		private SvgElementCollection graphList;

		// Methods
		internal State(string prefix, string localname, string ns, SvgDocument doc) : base(prefix, localname, ns, doc)
		{
			graphList =new SvgElementCollection();
		}	
		public override bool IsValidChild(System.Xml.XmlNode element)
		{
			if(element is Symbol)
				return true;
			else
				return false;
		}
		public override SvgElementCollection ChildList
		{
			get { return this.graphList; }
		}
		

	}
}