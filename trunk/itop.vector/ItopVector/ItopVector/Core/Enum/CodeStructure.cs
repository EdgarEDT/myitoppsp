/***********************************************************************
 * Module:  CodeStructure.cs
 * Author:  Administrator
 * Purpose: Definition of the Struct CodeStructure
 ***********************************************************************/

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct CodeStructure
{
   public CodeStructure(int offset, int length, string code, string textContent)
   {
       this.Offset = offset;
       this.Length = length;
       this.Code = code;
       this.TextContent = textContent;
   }

   public int Offset;
   public int Length;
   public string Code;
   public string TextContent;

}