/***********************************************************************
 * Module:  DelegateDeclarations.cs
 * Author:  Administrator
 * Purpose: Definition of the Delegate container ItopVector.Core.Interface.DelegateDeclarations
 ***********************************************************************/

using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace ItopVector.Core.Interface
{
   public delegate void OnTipEventHandler(object sender, string tooltip, byte TipType);
   
   public delegate void TrackPopupEventHandler(object sender, Point screenPoint);
}