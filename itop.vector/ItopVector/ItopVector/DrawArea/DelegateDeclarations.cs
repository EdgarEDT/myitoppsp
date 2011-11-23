/***********************************************************************
 * Module:  DelegateDeclarations.cs
 * Author:  Administrator
 * Purpose: Definition of the Delegate container ItopVector.DrawArea.DelegateDeclarations
 ***********************************************************************/

using System.Windows.Forms;
using ItopVector.Core.Paint;
using ItopVector.Core;
using ItopVector.Core.Interface;
using System;
namespace ItopVector.DrawArea
{
	public delegate void PostBrushEventHandler(object sender, ISvgBrush brush);
   
	public delegate bool DialogKeyEventHandler(object sender, Keys keydata);

	public delegate void SvgElementEventHandler(object sender,SvgElementSelectedEventArgs e);

	public delegate void SvgElementDragDropEventHandler (object sender,DragEventArgs e);

	public delegate void ViewChangedEventHandler(object sender, ViewChangedEventArgs e);

	public delegate void PaintMapEventHandler(object sender,PaintMapEventArgs e);

    public delegate void PolyLineBreakEventHandler(object sender,BreakElementEventArgs e);

    public delegate void ElementMoveEventHandler(object sender,MoveEventArgs e);

	public delegate void AddSvgElementEventHandler(object sender, AddSvgElementEventArgs e);
     
	public class AddSvgElementEventArgs:EventArgs
	{
		public AddSvgElementEventArgs(ISvgElement element)
		{
            
			svgelement=element;
			Cancel=false;
		}

		public ISvgElement SvgElement
		{
			get
			{
				return svgelement;
			}
			set
			{
				svgelement =value;
			}
		}
		private ISvgElement svgelement;
		public bool Cancel;
	}
}