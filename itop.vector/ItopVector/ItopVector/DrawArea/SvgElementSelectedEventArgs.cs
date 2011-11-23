using System;
using ItopVector.Core.Interface;
using ItopVector.Core;
using System.Collections;
using System.Windows.Forms;

namespace ItopVector.DrawArea
{
	/// <summary>
	/// SvgElementSelectedEventArgs 的摘要说明。
	/// </summary>
	public class SvgElementSelectedEventArgs:EventArgs
	{
		public SvgElementSelectedEventArgs(ISvgElement element)
		{
            
			Elements =new ISvgElement[1]{element};
		}
		public SvgElementSelectedEventArgs(SvgElementCollection list)
		{
			if (list != null)
			{
				this.Elements = new ISvgElement[list.Count];
				list.CopyTo(this.Elements, 0);
			}
		}

		public SvgElementSelectedEventArgs(ISvgElement[] list)
		{
			this.Elements = list;
		}


		public ISvgElement SvgElement
		{
			get
			{
				if(Elements.Length>0)
				{
					return Elements[0];
				}
				return null;
			}
			set
			{
				if (Elements==null)
				{
					Elements =new ISvgElement[1]{value};
				}
				else
				{
					Elements[0] = value;

				}
			}
		}
		public ISvgElement[] Elements;
		public MouseEventArgs Mouse;
	}
}
