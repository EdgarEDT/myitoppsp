/***********************************************************************
 * Module:  DelegateDeclarations.cs
 * Author:  Administrator
 * Purpose: Definition of the Delegate container ItopVector.Core.Document.DelegateDeclarations
 ***********************************************************************/

using System;
using System.Runtime.CompilerServices;
 using ItopVector.Core.Interface;

namespace ItopVector.Core.Document
{
	public delegate void OnDocumentChangedEventHandler(object sender, DocumentChangedEventArgs e);
   
	public delegate void ControlTimeChangeEventHandler(object sender, int oldtime, int newtime);


	public delegate void SvgElementChangeEventHandler(object sender,SvgElementChangedEventArgs e);

	public class SvgElementChangedEventArgs:EventArgs
	{
		// Fields
		public ISvgElement newElement;
		public ISvgElement oldElement;		
		// Methods
		public SvgElementChangedEventArgs(ISvgElement newelement,ISvgElement oldelement )
		{	
			newElement = newelement;
			oldElement = oldelement;
		}
		
		public ISvgElement NewElement
		{
			get{return newElement;}
		}

		public ISvgElement OldElement
		{
			get{return oldElement;}
		}
	}
}