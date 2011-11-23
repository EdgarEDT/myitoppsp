/***********************************************************************
 * Module:  DelegateDeclarations.cs
 * Author:  Administrator
 * Purpose: Definition of the Delegate container ItopVector.Core.DelegateDeclarations
 ***********************************************************************/

using System;
using System.Runtime.CompilerServices;

namespace ItopVector.Core
{
   public delegate void ElementExpandChangedEventHandler(object sender, ElementExpandChangedEventArgs e);
   
   public delegate void OnCollectionChangedEventHandler(object sender, CollectionChangedEventArgs e);
}