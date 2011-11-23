/***********************************************************************
 * Module:  DelegateDeclarations.cs
 * Author:  Administrator
 * Purpose: Definition of the Delegate container ItopVector.Core.Figure.DelegateDeclarations
 ***********************************************************************/

using System;
using System.Runtime.CompilerServices;

namespace ItopVector.Core.Figure
{
   public delegate void PostTextEditEventHandler(object sender, string attributename, string attributevalue);
}